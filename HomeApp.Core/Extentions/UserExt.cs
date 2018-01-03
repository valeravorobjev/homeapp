using System;
using System.Security.Cryptography;
using System.Text;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Db.Entities.Models;
using HomeApp.Core.Exceptions;

namespace HomeApp.Core.Extentions
{
    /// <summary>
    /// Методы расширений для работы с пользователями
    /// </summary>
    public static class UserExt
    {
        /// <summary>
        /// Создает хэш SHA512
        /// </summary>
        /// <param name="ssk">Соль</param>
        /// <returns></returns>
        public static string BuildHash(string ssk)
        {
            SHA512 sha = SHA512.Create();

            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(ssk));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash)
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        /// <summary>
        /// Задает пользователю соль, пароль , код активащии
        /// </summary>
        /// <param name="user">Объект пользователя</param>
        /// <param name="password">Пароль</param>
        public static void BuildSecure(this User user, string password)
        {
            Random random = new Random();

            if (user.Membership == null) user.Membership = new Membership();

            int salt = random.Next(100000, 999999);
            string ssk = user.Membership.Login + password + salt;
            string hash = BuildHash(ssk);

            user.Membership.Salt = salt;
            user.Membership.Password = hash;
            user.Membership.ActiveCode = Guid.NewGuid();
        }

        /// <summary>
        /// Проверяет указанный пароль. Если парль не верен, то выбрасывается исключение LoginOrPasswordException
        /// </summary>
        /// <param name="user">Объекто пользователя</param>
        /// <param name="password">Пароль</param>
        public static void CheckPassword(this User user, string password)
        {
            int salt = user.Membership.Salt;

            string ssk = user.Membership.Login + password + salt;
            string hash = BuildHash(ssk);

            if (user.Membership.Password != hash)
                throw new LoginOrPasswordException("Login or Password is incorrect");
        }
    }
}
