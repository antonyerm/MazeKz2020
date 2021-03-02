using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebMaze.DbStuff;
using WebMaze.DbStuff.Model.Life;
using WebMaze.DbStuff.Repository.Life;
using WebMaze.Models.Life;
using WebMaze.DbStuff.Repository;
using WebMaze.Models.Account;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using WebMaze.Services;
using WebMaze.Models.CustomAttribute.Life;

namespace WebMaze.Controllers
{
    public class LifeController : Controller
    {
        private WebMazeContext context;
        private AccidentRepository accidentRepository;
        private AdressRepository addressRepository;
        private CitizenUserRepository citizenUserRepository;
        private VictimRepository victimRepository;
        private FireDetailRepository fireDetailRepository;
        private HouseDestroyedInFireRepository houseDestroyedInFireRepository;
        private CriminalOffenceArticleRepository criminalOffenceArticleRepository;
        private CriminalOffenderRepository criminalOffenderRepository;
        private LifeService lifeService;
        private IMapper mapper;

        public LifeController(WebMazeContext context, 
                              IMapper mapper,
                              AccidentRepository accidentRepository,
                              AdressRepository addressRepository,
                              CitizenUserRepository citizenUserRepository,
                              VictimRepository victimRepository,
                              HouseDestroyedInFireRepository houseDestroyedInFireRepository,
                              FireDetailRepository fireDetailRepository,
                              CriminalOffenceArticleRepository criminalOffenceArticleRepository,
                              CriminalOffenderRepository criminalOffenderRepository,
                              LifeService lifeService)
        {
            this.context = context;
            this.accidentRepository = accidentRepository;
            this.addressRepository = addressRepository;
            this.citizenUserRepository = citizenUserRepository;
            this.victimRepository = victimRepository;
            this.fireDetailRepository = fireDetailRepository;
            this.houseDestroyedInFireRepository = houseDestroyedInFireRepository;
            this.criminalOffenceArticleRepository = criminalOffenceArticleRepository;
            this.criminalOffenderRepository = criminalOffenderRepository;
            this.lifeService = lifeService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("/Views/Life/LifeIndex.cshtml");
        }

        [HttpGet]
        public IActionResult ShowAccidents()
        {
            var accidentsViewModel = new List<AccidentViewModel>();
            var accidentsFromDb = this.accidentRepository.GetAll();
            foreach (var accident in accidentsFromDb)
            {
                var individualAccidentViewModel = mapper.Map<AccidentViewModel>(accident);
                individualAccidentViewModel.AddressViewModel = mapper.Map<AdressViewModel>(accident.AccidentAddress);
                accidentsViewModel.Add(individualAccidentViewModel);
            }

            return View("~/Views/Life/ShowAccidents.cshtml", accidentsViewModel);
        }

        [HttpGet]
        public IActionResult AddEditAccident(long id)
        {
            var accidentCategoryList = lifeService.GetSelectListFromEnum<AccidentCategoryEnum>();
            var accidentAddressList = lifeService.GetSelectListOfAddressesFromDb();

            // view model for Add Accident
            var accidentViewModel = new AccidentViewModel()
            {
                Id = 0,
                AccidentDate = DateTime.Now,
                AddressViewModel = null,
                AccidentAddressText = Dictionaries.NotAvailable,
                SelectedAccidentAddress = 0,
                AccidentAddressList = accidentAddressList,
                SelectedAccidentCategory = 0,
                AccidentCategoryList = accidentCategoryList,
                AccidentDescription = String.Empty,
            };
            if (id != 0)
            {
                // view model for Edit Accident
                var accidentFromDb = this.accidentRepository.Get(id);
                accidentViewModel = mapper.Map<AccidentViewModel>(accidentFromDb);

                accidentViewModel.AccidentAddressList = accidentAddressList;
                accidentViewModel.AccidentCategoryList = accidentCategoryList;
            }

            return PartialView("~/Views/Shared/Life/_AddEditAccident.cshtml", accidentViewModel);
        }

        [HttpPost]
        public IActionResult AddEditAccident(AccidentViewModel accidentViewModel)
        {
            accidentViewModel.AccidentCategoryList = lifeService.GetSelectListFromEnum<AccidentCategoryEnum>();
            accidentViewModel.AccidentAddressList = lifeService.GetSelectListOfAddressesFromDb();

            // validation for combination of fields
            if (accidentViewModel.SelectedAccidentAddress == null && accidentViewModel.AccidentDescription == null)
            {
                ModelState.AddModelError(String.Empty, "Надо заполнить хотя бы одно из полей: адрес происшествия или описание");
                return PartialView("~/Views/Shared/Life/_AddEditAccident.cshtml", accidentViewModel);
            }

            // validation for particular fields
            if (!ModelState.IsValid)
            {
                return PartialView("~/Views/Shared/Life/_AddEditAccident.cshtml", accidentViewModel);
            }

            var accident = mapper.Map<Accident>(accidentViewModel);
            if (accidentViewModel.SelectedAccidentAddress != null)
            {
                var address = addressRepository.Get((long)accidentViewModel.SelectedAccidentAddress);
                accident.AccidentAddress = address;
            }

            accident.AccidentCategory = (AccidentCategoryEnum)accidentViewModel.SelectedAccidentCategory;
            accidentRepository.Save(accident);
            return Json(new { success = true });
        }

        public IActionResult DeleteAccident(long id)
        {
            accidentRepository.Delete(id);
            return RedirectToAction(nameof(ShowAccidents));
        }

        [HttpGet]
        public IActionResult AccidentDetails(long id)
        {
            var accidentDetailsViewModel = lifeService.GetAccidentDetailsViewModel(id);

            return PartialView("~/Views/Shared/Life/_AccidentDetails.cshtml", accidentDetailsViewModel);
        }

        [HttpGet]
        [IsFireman]
        public IActionResult EditFire(long id)
        {
            var accidentDetailsViewModel = lifeService.GetAccidentDetailsViewModel(id);
            return View("~/Views/Life/EditFire.cshtml", accidentDetailsViewModel);
        }

        [HttpGet]
        [IsPoliceman]
        public IActionResult EditCriminalOffence(long id)
        {
            var accidentDetailsViewModel = lifeService.GetAccidentDetailsViewModel(id);
            return View("~/Views/Life/EditCriminalOffence.cshtml", accidentDetailsViewModel);
        }

        [HttpGet]
        public IActionResult AddEditVictim(long? victimId, long accidentId, AccidentCategoryEnum accidentCategory)
        {
            AccidentVictimViewModel victimViewModel;
            var citizenSelectList = lifeService.GetSelectListOfCitizensFromDb();
            var bodilyHarmSelectList = lifeService.GetSelectListFromEnum<BodilyHarmEnum>();
            if (victimId == null)
            {
                victimViewModel = new AccidentVictimViewModel
                {
                    AccidentId = accidentId,
                    BodilyHarm = BodilyHarmEnum.NotAvailable,
                    CitizenId = null,
                    InitialCitizenId = null,
                    EconomicLoss = null,
                };
            }
            else
            {
                var victimFromDb = victimRepository.Get((long)victimId);
                victimViewModel = mapper.Map<AccidentVictimViewModel>(victimFromDb);
            }

            victimViewModel.BodilyHarmList = bodilyHarmSelectList;
            victimViewModel.CitizenList = citizenSelectList;
            victimViewModel.AccidentCategory = accidentCategory;

            return View("~/Views/Life/AddEditVictim.cshtml", victimViewModel);
        }

        [HttpPost]
        public IActionResult AddEditVictim(AccidentVictimViewModel accidentVictimViewModel)
        {
            accidentVictimViewModel.CitizenList = lifeService.GetSelectListOfCitizensFromDb();
            accidentVictimViewModel.BodilyHarmList = lifeService.GetSelectListFromEnum<BodilyHarmEnum>();

            // CitizenId is not valid
            if (ModelState[nameof(accidentVictimViewModel.CitizenId)].ValidationState == ModelValidationState.Invalid)
            {
                // remove all ModelStateEntries with key name different from "CitizenId" -> 
                // we demonstrate only CitizenId error here
                ModelState.Keys.Where(key => key != nameof(accidentVictimViewModel.CitizenId)).
                    ToList().Select(key => ModelState.Remove(key)).ToList();
                return View("~/Views/Life/AddEditVictim.cshtml", accidentVictimViewModel);
            }

            // EconomicLoss numeric format is not valid (though the user attempted to enter something)
            if (!ModelState.IsValid && ModelState[nameof(accidentVictimViewModel.EconomicLoss)].AttemptedValue != String.Empty)
            {
                return View("~/Views/Life/AddEditVictim.cshtml", accidentVictimViewModel);
            }

            // validation for combination of properties
            if ((accidentVictimViewModel.EconomicLoss == null || accidentVictimViewModel.EconomicLoss == 0)
                && (accidentVictimViewModel.BodilyHarm == BodilyHarmEnum.None
                    || accidentVictimViewModel.BodilyHarm == BodilyHarmEnum.NotAvailable))
            {
                ModelState.Clear();
                ModelState.AddModelError(String.Empty, "Чтобы человек считался потерпевшим, у него должны быть телесные повреждения или экономический ущерб"); ;
                return View("~/Views/Life/AddEditVictim.cshtml", accidentVictimViewModel);
            }


            var accidentVictim = mapper.Map<AccidentVictim>(accidentVictimViewModel);
            var citizenUser = citizenUserRepository.Get((long)accidentVictimViewModel.CitizenId);
            accidentVictim.Victim = citizenUser;
            var accident = accidentRepository.Get(accidentVictimViewModel.AccidentId);
            accidentVictim.Accident = accident;

            victimRepository.Save(accidentVictim);
            return RedirectToAction(nameof(EditFire), new { id = accidentVictimViewModel.AccidentId });
        }

        public IActionResult DeleteVictim(long victimId, long accidentId, AccidentCategoryEnum accidentCategory)
        {
            victimRepository.Delete(victimId);
            return RedirectToAction(nameof(EditFire), new { id = accidentId });
        }

        [HttpGet]
        public IActionResult AddEditHouseDestroyedInFire(long accidentId, long? houseDestroyedInFireId)
        {
            HouseDestroyedInFireViewModel houseViewModel;
            if (houseDestroyedInFireId == null)
            {
                houseViewModel = new HouseDestroyedInFireViewModel
                {
                    AccidentId = accidentId,
                    HouseAddressId = null,
                    InitialHouseAddressId = null,
                };
            }
            else
            {
                var houseFromDb = houseDestroyedInFireRepository.Get((long)houseDestroyedInFireId);
                houseViewModel = mapper.Map<HouseDestroyedInFireViewModel>(houseFromDb);
            }

            houseViewModel.HouseAddressesList = lifeService.GetSelectListOfAddressesFromDb();
            return View("~/Views/Life/AddEditHouseDestroyedInFire.cshtml", houseViewModel);
        }

        [HttpPost]
        public IActionResult AddEditHouseDestroyedInFire(HouseDestroyedInFireViewModel houseViewModel)
        {
            houseViewModel.HouseAddressesList = lifeService.GetSelectListOfAddressesFromDb();
            if (!ModelState.IsValid)
            {
                return View("~/Views/Life/AddEditHouseDestroyedInFire.cshtml", houseViewModel);
            }

            var house = mapper.Map<HouseDestroyedInFire>(houseViewModel);
            var address = addressRepository.Get((long)houseViewModel.HouseAddressId);
            house.DestroyedHouseAddress = address;
            var accident = accidentRepository.Get(houseViewModel.AccidentId);
            house.Accident = accident;

            houseDestroyedInFireRepository.Save(house);
            return RedirectToAction(nameof(EditFire), new { id = houseViewModel.AccidentId });
        }

        public IActionResult DeleteHouseDestroyedInFire(long accidentId, long houseDestroyedInFireId)
        {
            houseDestroyedInFireRepository.Delete(houseDestroyedInFireId);
            return RedirectToAction(nameof(EditFire), new { id = accidentId });
        }

        [HttpGet]
        public IActionResult EditFireDetails(long id)
        {
            var accidentFromDb = accidentRepository.Get(id);
            var fireDetailFromDb = accidentFromDb.FireDetail;
            var fireDetailViewModel = mapper.Map<FireDetailViewModel>(fireDetailFromDb);

            if (fireDetailViewModel == null)
            {
                fireDetailViewModel = new FireDetailViewModel()
                {
                    AccidentId = id,
                    FireCause = null,
                    FireClass = null,
                };
            }

            fireDetailViewModel.FireCauseList = lifeService.GetSelectListFromEnum<FireCauseEnum>();
            fireDetailViewModel.FireClassList = lifeService.GetSelectListFromEnum<FireClassEnum>();

            return View("~/Views/Life/EditFireDetails.cshtml", fireDetailViewModel);
        }

        [HttpPost]
        public IActionResult EditFireDetails(FireDetailViewModel fireDetailViewModel)
        {
            fireDetailViewModel.FireCauseList = lifeService.GetSelectListFromEnum<FireCauseEnum>();
            fireDetailViewModel.FireClassList = lifeService.GetSelectListFromEnum<FireClassEnum>();
            if (!ModelState.IsValid)
            {
                return View("~/Views/Life/EditFireDetails.cshtml", fireDetailViewModel);
            }

            // validation for combination of properties
            if ((fireDetailViewModel.FireCause == null || fireDetailViewModel.FireCause == FireCauseEnum.NotAvailable)
                && (fireDetailViewModel.FireClass == null || fireDetailViewModel.FireClass == FireClassEnum.NotAvailable))
            {
                ModelState.AddModelError(String.Empty, "Надо заполнить значимой информацией хотя бы одно поле: причина возгорания или класс пожара"); ;
                return View("~/Views/Life/EditFireDetails.cshtml", fireDetailViewModel);
            }

            var fireDetail = mapper.Map<FireDetail>(fireDetailViewModel);
            var accident = accidentRepository.Get(fireDetailViewModel.AccidentId);
            fireDetail.Accident = accident;

            fireDetailRepository.Save(fireDetail);
            return RedirectToAction(nameof(EditFire), new { id = fireDetailViewModel.AccidentId });
        }

        [HttpGet]
        public IActionResult AddEditArticle(long accidentId, long? criminalOffenceArticleId)
        {
            CriminalOffenceArticleViewModel articleViewModel;
            if (criminalOffenceArticleId == null)
            {
                articleViewModel = new CriminalOffenceArticleViewModel
                {
                    AccidentId = accidentId,
                    CriminalOffenceArticleEnum = null,
                };
            }
            else
            {
                var articleFromDb = criminalOffenceArticleRepository.Get((long)criminalOffenceArticleId);
                articleViewModel = mapper.Map<CriminalOffenceArticleViewModel>(articleFromDb);
            }

            articleViewModel.CriminalOffenceArticlesList = lifeService.GetSelectListFromEnum<CriminalCodeEnum>();
            return View("~/Views/Life/AddEditArticle.cshtml", articleViewModel);
        }

        [HttpPost]
        public IActionResult AddEditArticle(CriminalOffenceArticleViewModel articleViewModel)
        {
            articleViewModel.CriminalOffenceArticlesList = lifeService.GetSelectListFromEnum<CriminalCodeEnum>();
            if (!ModelState.IsValid)
            {
                return View("~/Views/Life/AddEditArticle.cshtml", articleViewModel);
            }

            // validation for combination of properties
            if (articleViewModel.CriminalOffenceArticleEnum == CriminalCodeEnum.NotAvailable)
            {
                ModelState.AddModelError(String.Empty, "Пожалуйста, стоит все же подумать и выбрать статью кодекса"); ;
                return View("~/Views/Life/AddEditArticle.cshtml", articleViewModel);
            }

            var article = mapper.Map<CriminalOffenceArticle>(articleViewModel);
            var accidentFromDb = accidentRepository.Get(articleViewModel.AccidentId);
            article.Accident = accidentFromDb;

            criminalOffenceArticleRepository.Save(article);
            return RedirectToAction("EditCriminalOffence", new { id = articleViewModel.AccidentId });
        }

        public IActionResult DeleteCriminalOffenceArticle(long accidentId, long criminalOffenceArticleId)
        {
            criminalOffenceArticleRepository.Delete(criminalOffenceArticleId);
            return RedirectToAction(nameof(EditCriminalOffence), new { id = accidentId });
        }

        [HttpGet]
        public IActionResult AddEditOffender(long accidentId, long? offenderId)
        {
            CriminalOffenderViewModel offenderViewModel;
            if (offenderId == null)
            {
                offenderViewModel = new CriminalOffenderViewModel
                {
                    AccidentId = accidentId,
                    CitizenId = null,
                    InitialCitizenId = null,
                    Verdict = String.Empty,
                };
            }
            else
            {
                var offenderFromDb = criminalOffenderRepository.Get((long)offenderId);
                offenderViewModel = mapper.Map<CriminalOffenderViewModel>(offenderFromDb);
            }

            offenderViewModel.CitizenList = lifeService.GetSelectListOfCitizensFromDb();
            return View("~/Views/Life/AddEditOffender.cshtml", offenderViewModel);
        }

        [HttpPost]
        public IActionResult AddEditOffender(CriminalOffenderViewModel offenderViewModel)
        {
            offenderViewModel.CitizenList = lifeService.GetSelectListOfCitizensFromDb();
            if (!ModelState.IsValid)
            {
                return View("~/Views/Life/AddEditOffender.cshtml", offenderViewModel);
            }

            var offender = mapper.Map<CriminalOffender>(offenderViewModel);
            var accidentFromDb = accidentRepository.Get(offenderViewModel.AccidentId);
            offender.Accident = accidentFromDb;
            var offenderFromDb = citizenUserRepository.Get((long)offenderViewModel.CitizenId);
            offender.Offender = offenderFromDb;

            criminalOffenderRepository.Save(offender);
            return RedirectToAction("EditCriminalOffence", new { id = offenderViewModel.AccidentId });
        }

        public IActionResult DeleteCriminalOffender(long accidentId, long offenderId)
        {
            criminalOffenderRepository.Delete(offenderId);
            return RedirectToAction(nameof(EditCriminalOffence), new { id = accidentId });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            var viewModel = new LifeLoginViewModel();
            viewModel.ReturnUrl = Request.Query["ReturnUrl"];
            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LifeLoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = citizenUserRepository
                .GetUserByNameAndPassword(loginViewModel.Login, loginViewModel.Password);
            if (user == null)
            {
                ModelState.AddModelError(String.Empty, "Такого логина и пароля не существует.");
                return View(loginViewModel);
            }

            var recordId = new Claim(ClaimTypes.NameIdentifier, user.Id.ToString());
            var recordName = new Claim(ClaimTypes.Name, user.Login);
            var recordAuthMethod = new Claim(ClaimTypes.AuthenticationMethod, Startup.LifeAuth);
            var claims = new List<Claim>() { recordId, recordName, recordAuthMethod };
            var claimsIdentity = new ClaimsIdentity(claims, Startup.LifeAuth);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(claimsPrincipal);
            
            if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
            {
                return RedirectToAction("Index", "Life");
            }

            return Redirect(loginViewModel.ReturnUrl);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Life");
        }


        // ------------- utility methods------------------------------------------------------------

        

        /// <summary>
        /// Adds specified amount of unique users to the table. Very simple users with basic fields.
        /// </summary>
        /// <param name="quantity">Number of attempts of finding unique users. The names dictionary is limited, so is the 
        /// ability to create unique names. </param>
        /// <returns>Report of current users</returns>
        public IActionResult AddUsersToDb(int quantity)
        {
            int createdUsers = 0;
            if (quantity > 0)
            {
                createdUsers = lifeService.GenerateCitizenUsers(quantity);
            }
            
            var allUsersViewModel = citizenUserRepository.GetAll().Select(u => mapper.Map<UserViewModel>(u)).ToList();
            ViewData["quantity"] = createdUsers;
            return View("~/Views/Life/ListOfUsers.cshtml", allUsersViewModel);
        }

        /// <summary>
        /// Adds specified amount of addreses to the table. No attempt is made to make addresses unique.
        /// </summary>
        /// <param name="quantity">Number of addresses.</param>
        /// <returns>Report of current addreses</returns>
        public IActionResult AddAddressesToDb(int quantity)
        {
            int createdAddresses = 0;
            if (quantity > 0)
            {
                createdAddresses = lifeService.GenerateAddresses(quantity);
            }
            
            var allAddressesViewModel = addressRepository.GetAll().Select(a => mapper.Map<AddressViewModel>(a)).ToList();
            ViewData["quantity"] = createdAddresses;
            return View("~/Views/Life/ListOfAddresses.cshtml", allAddressesViewModel);
        }

        /// <summary>
        /// Validation method for Remote type validation of input field of destroyed houses (they must be unique).
        /// </summary>
        /// <param name="houseAddressId">Address Id (primary key of Address table).</param>
        /// <param name="accidentId">Accident Id.</param>
        /// <param name="initialHouseAddressId">Address Id which was filled in initially on the form, before user interaction.</param>
        /// <returns>Json object - True if addresses are unique, False if validation failed.</returns>
        [AcceptVerbs("GET", "POST")]
        public IActionResult IsBurntHouseUnique(long houseAddressId, long accidentId, long initialHouseAddressId)
        {
            if (houseAddressId == initialHouseAddressId)
            {
                // user did not choose any other house. No need to validate
                return Json(true);
            }

            return Json(!houseDestroyedInFireRepository.hasHouseAndAccident(houseAddressId, accidentId));
        }

        /// <summary>
        /// Validation method for Remote type validation of input field of offenders (they must be unique).
        /// </summary>
        /// <param name="citizenId">Citizens Id (primary key of CitizenUsers table).</param>
        /// <param name="accidentId">Accident Id.</param>
        /// <param name="initialCitizenId">Citizen Id which was filled in initially on the form, before user interaction.</param>
        /// <returns>Json object - True if addresses are unique, False if validation failed.</returns>
        [AcceptVerbs("GET", "POST")]
        public IActionResult IsOffenderUnique(long citizenId, long accidentId, long initialCitizenId)
        {
            if (citizenId == initialCitizenId)
            {
                // user did not choose any other offender. No need to validate
                return Json(true);
            }

            return Json(!criminalOffenderRepository.hasCitizenAndAccident(citizenId, accidentId));
        }

    }
}
