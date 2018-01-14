using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeApp.Core.Db.Entities.Models.Enums;
using HomeApp.Core.Extentions.Filters.Models;
using HomeApp.Core.Extentions.Sorts.Models.Enums;
using HomeApp.Core.Models;
using HomeApp.Core.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HomeApp.Site.Controllers.Api.V1
{
    [Route("api/v1/[controller]")]
    public class SpecialistController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRealEstateRepository _realEstateRepository;
        private readonly IAdRepository _adRepository;

        public SpecialistController(IUserRepository userRepository, IRealEstateRepository realEstateRepository, IAdRepository adRepository)
        {
            _userRepository = userRepository;
            _realEstateRepository = realEstateRepository;
            _adRepository = adRepository;
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetSpecialists(UserType userType, int take, int skip, string sort = "FeaturedListings")
        {
            UsersModel userList = null;

            if (userType == UserType.Realtor)
            {
                PersonProfessionalFilter filter = new PersonProfessionalFilter()
                {
                    Take = take,
                    Skip = skip,
                    UserTypes = new List<UserType> { userType }
                };

                PersonProfessionalSort personProfessionalSort =
                    (PersonProfessionalSort) Enum.Parse(typeof(PersonProfessionalSort), sort);
                userList = await _userRepository.GetPersonProfessionals(filter, personProfessionalSort);
            }
            else if (userType == UserType.Agency || userType == UserType.Developer)
            {
                ProfessionalFilter filter = new ProfessionalFilter()
                {
                    Take = take,
                    Skip = skip,
                    UserTypes = new List<UserType> { userType }
                };

                ProfessionalSort professionalSort =
                    (ProfessionalSort)Enum.Parse(typeof(ProfessionalSort), sort);
                userList = await _userRepository.GetProfessionals(filter, professionalSort);
            }


            return Ok(userList);
        } 

    }
}
