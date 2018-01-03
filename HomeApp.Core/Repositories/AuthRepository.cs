using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HomeApp.Core.Db;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Db.Entities.Models;
using HomeApp.Core.Db.Entities.Models.Enums;
using HomeApp.Core.Exceptions;
using HomeApp.Core.Extentions;
using HomeApp.Core.Repositories.Contracts;
using MimeKit;
using MimeKit.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HomeApp.Core.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IMongoDatabase _database;
        private readonly ISessionRepository _sessionRepository;

        public AuthRepository(ISessionRepository sessionRepository, string conn = null)
        {
            _sessionRepository = sessionRepository ?? throw new Exception("ISessionRepository is NULL");
            MongoClient client = new MongoClient(conn);
            _database = client.GetDatabase(DbSet.DB_NAME);
        }

        public async Task<Session> LogIn(string login, string password)
        {
            IMongoCollection<User> collection = _database.GetCollection<User>(DbSet.USERS_COLLECTION);

            FilterDefinitionBuilder<User> filterBuilder = Builders<User>.Filter;
            FilterDefinition<User> filter = filterBuilder.Eq(u => u.Membership.Login, login.ToLower());
            IAsyncCursor<User> cursor = await collection.FindAsync(filter);
            User user = await cursor.FirstOrDefaultAsync();

            if (user == null) throw new UserNotFoundException("User with this login does't exist");
            if (user.Membership.UserStatus != UserStatus.Active) throw new UserNotActivatedException("User is not active");

            user.CheckPassword(password);

            Session session = await _sessionRepository.OpenSession(user);

            return session;
        }

        public async Task LogOut(Session session)
        {
            await _sessionRepository.CloseSession(session.Id);
        }

        public async Task Register(string email, string password, string confirmPass, Language lan)
        {
            bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (!isEmail) throw new EmailInvalidException("Email is invalid");

            if (password != confirmPass)
                throw new LoginOrPasswordException("password not equeals confirmPass");

            if (password.Length < 6)
                throw new PasswordLengthException("Your password does not security requirements. Minimum 6 characters long");

            IMongoCollection<User> collection = _database.GetCollection<User>(DbSet.USERS_COLLECTION);

            FilterDefinitionBuilder<User> filterBuilder = Builders<User>.Filter;
            FilterDefinition<User> filter = filterBuilder.Eq(u => u.Membership.Login, email.ToLower());
            IAsyncCursor<User> cursor = await collection.FindAsync(filter);
            User user = await cursor.FirstOrDefaultAsync();

            if (user != null) throw new UserAllReadyExistException("User with this login allready exist");

            user = new User
            {
                Language = lan,
                Membership = new Membership
                {
                    Login = email.ToLower(),
                    Email = email.ToLower(),
                    DateReg = DateTime.UtcNow,
                    UserStatus = UserStatus.OnConfirmation
                }
            };
            user.BuildSecure(password);
            user.Membership.UserRoles = new List<UserRole> {UserRole.User};

            await collection.InsertOneAsync(user);

            var message = new MimeMessage();

            if (lan == Language.En)
            {
                message.From.Add(new MailboxAddress("HOMEAPP PRO", "info@rabun.ru"));
                message.Subject = "Registration confirmation";

                message.Body = new TextPart(TextFormat.Html)
                {
                    //Text = _viewRender.Render("Emails/ConfirmEn", new object())
                };
            }
            else if (lan == Language.Ru)
            {
                message.From.Add(new MailboxAddress("HOMEAPP PRO", "info@rabun.ru"));
                message.Subject = "Подтверждение регистрации";

                message.Body = new TextPart(TextFormat.Html)
                {
                    //Text = _viewRender.Render("Emails/ConfirmRu", user)
                };
            }

            message.To.Add(new MailboxAddress(user.Membership.Login, user.Membership.Email));

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.mail.ru", 465, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("info@rabun.ru", "TM_1Qt_2Bd_3RPM");

                client.Send(message);
                client.Disconnect(true);
            }
        }

        public async Task<Session> ConfirmUser(string userId, string activeCode)
        {
            IMongoCollection<User> collection = _database.GetCollection<User>(DbSet.USERS_COLLECTION);

            FilterDefinitionBuilder<User> filterBuilder = Builders<User>.Filter;
            UpdateDefinitionBuilder<User> up = Builders<User>.Update;
            FilterDefinition<User> filter = filterBuilder.Eq(u => u.Id, new ObjectId(userId));

            IAsyncCursor<User> cursor = await collection.FindAsync(filter);
            User user = await cursor.FirstOrDefaultAsync();

            if (user == null) throw new UserNotFoundException("User with this id doesn't exist");
            if (user.Membership.ActiveCode != new Guid(activeCode)) throw new ActiveCodeException("Active code is wrong");
            if (user.Membership.UserStatus == UserStatus.Active) throw new ActiveCodeException("User allready active");

            UpdateResult result = await collection.UpdateOneAsync(filter, up.Set(u => u.Membership.UserStatus, UserStatus.Active));
            if (result.ModifiedCount == 0) throw new UpdateException("Can't activate user!");

            return await _sessionRepository.OpenSession(user);
        }
    }
}
