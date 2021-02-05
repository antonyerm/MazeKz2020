using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using WebMaze.DbStuff;
using WebMaze.DbStuff.Model.Life;
using WebMaze.DbStuff.Repository.Life;
using WebMaze.Models.Life;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using WebMaze.DbStuff.Repository;
using WebMaze.DbStuff.Model;
using WebMaze.Models.Account;
using System.Linq;

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
        private IMapper mapper;

        public LifeController(WebMazeContext context, IMapper mapper,
                              AccidentRepository accidentRepository,
                              AdressRepository addressRepository,
                              CitizenUserRepository citizenUserRepository,
                              VictimRepository victimRepository,
                              FireDetailRepository fireDetailRepository)
        {
            this.context = context;
            this.accidentRepository = accidentRepository;
            this.addressRepository = addressRepository;
            this.citizenUserRepository = citizenUserRepository;
            this.victimRepository = victimRepository;
            this.fireDetailRepository = fireDetailRepository;
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
            foreach (var item in accidentsFromDb)
            {
                var individualAccidentViewModel = mapper.Map<AccidentViewModel>(item);
                individualAccidentViewModel.AddressVM = mapper.Map<AdressViewModel>(item.AccidentAddress);
                accidentsViewModel.Add(individualAccidentViewModel);
            }

            return View("~/Views/Life/ShowAccidents.cshtml", accidentsViewModel);
        }

        [HttpGet]
        public IActionResult AddEditAccident(long id)
        {
            var accidentCategoryList = GetSelectListFromEnum<AccidentCategoryEnum>();
            var accidentAddressList = GetSelectListOfAddressesFromDb();

            // view model for Add Accident
            var accidentViewModel = new AccidentViewModel()
            {
                Id = 0,
                AccidentDate = DateTime.Now,
                AddressVM = null,
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

            return PartialView("~/Views/Life/_AddEditAccident.cshtml", accidentViewModel);
        }

        [HttpPost]
        public IActionResult AddEditAccident(AccidentViewModel accidentViewModel)
        {
            accidentViewModel.AccidentCategoryList = GetSelectListFromEnum<AccidentCategoryEnum>();
            accidentViewModel.AccidentAddressList = GetSelectListOfAddressesFromDb();

            // validation for combination of fields
            if ((accidentViewModel.SelectedAccidentAddress == null)
                && (accidentViewModel.AccidentDescription == null))
            {
                ModelState.AddModelError("", "Надо заполнить хотя бы одно из полей: адрес происшествия или описание");
                return PartialView("~/Views/Life/_AddEditAccident.cshtml", accidentViewModel);
            }

            // validation for particular fields
            if (!ModelState.IsValid)
            {
                return PartialView("~/Views/Life/_AddEditAccident.cshtml", accidentViewModel);
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

        public IActionResult AddUsersToDb()
        {
            int quantity = 10;
            var generators = new GeneratorsForLife(citizenUserRepository, addressRepository);
            generators.GenerateCitizenUsers(quantity);
            return Content($"Было создано {quantity} пользователей");
        }

        public IActionResult AddAddressesToDb()
        {
            int quantity = 10;
            var generators = new GeneratorsForLife(citizenUserRepository, addressRepository);
            generators.GenerateAddresses(quantity);
            return Content($"Было создано {quantity} адресов");
        }

        [HttpGet]
        public IActionResult AccidentDetails(long id)
        {
            var accidentDetailsViewModel = GetAccidentDetailsViewModel(id);

            return PartialView("~/Views/Life/_AccidentDetails.cshtml", accidentDetailsViewModel);
        }

        [HttpGet]
        public IActionResult EditFire(long id)
        {
            var accidentDetailsViewModel = GetAccidentDetailsViewModel(id);
            return View("~/Views/Life/EditFire.cshtml", accidentDetailsViewModel);
        }

        [HttpGet]
        public IActionResult EditCriminalOffence(long id)
        {
            var accidentDetailsViewModel = GetAccidentDetailsViewModel(id);
            return View("~/Views/Life/EditCriminalOffence.cshtml", accidentDetailsViewModel);
        }

        [HttpGet]
        public IActionResult AddEditVictim(long victimId, long accidentId, AccidentCategoryEnum accidentCategory)
        {
            AccidentVictimViewModel victimViewModel;
            var citizenSelectList = GetSelectListOfCitizensFromDb();
            var bodilyHarmSelectList = GetSelectListFromEnum<BodilyHarmEnum>();
            if (victimId == 0)
            {
                victimViewModel = new AccidentVictimViewModel
                {
                    AccidentId = accidentId,
                    BodilyHarm = 0,
                    CitizenId = 0,
                    EconomicLoss = 0,
                };
            }
            else
            {
                var victimFromDb = victimRepository.Get(victimId);
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
            var accidentVictim = mapper.Map<AccidentVictim>(accidentVictimViewModel);
            var citizenUser = citizenUserRepository.Get(accidentVictimViewModel.CitizenId);
            accidentVictim.Victim = citizenUser;
            var accident = accidentRepository.Get(accidentVictimViewModel.AccidentId);
            accidentVictim.Accident = accident;

            victimRepository.Save(accidentVictim);
            return RedirectToAction(nameof(EditFire), new { id = accidentVictimViewModel.AccidentId});
        }

        public IActionResult DeleteVictim(long victimId, long accidentId, AccidentCategoryEnum accidentCategory)
        {
            victimRepository.Delete(victimId);
            return RedirectToAction(nameof(EditFire), new { id = accidentId });
        }

        [HttpGet]
        public IActionResult EditFireDetails(long id)
        {
            //var fireDetailsFromDb = fireDetailRepository.Get(id);
            //var fireDetailsViewModel = mapper.Map<FireClassificationViewModel>(fireDetailFromDb);
            //fireClassificationViewModel.FireCauseList = GetSelectListFromEnum<FireCauseEnum>();
            //fireClassificationViewModel.FireClassList = GetSelectListFromEnum<FireClassEnum>();
            //return View("~/Views/Life/EditFireClassification.cshtml", fireClassificationViewModel);

            var accidentFromDb = accidentRepository.Get(id);
            var fireDetailFromDb = accidentFromDb.FireDetail;
            var fireDetailViewModel = mapper.Map<FireDetailViewModel>(fireDetailFromDb);
            
            if (fireDetailViewModel == null)
            {
                fireDetailViewModel = new FireDetailViewModel()
                {
                    AccidentId = id,
                    FireCause = FireCauseEnum.NotAvailable,
                    FireClass = FireClassEnum.NotAvailable,
                };
            }

            fireDetailViewModel.FireCauseList = GetSelectListFromEnum<FireCauseEnum>();
            fireDetailViewModel.FireClassList = GetSelectListFromEnum<FireClassEnum>();

            return View("~/Views/Life/EditFireDetails.cshtml", fireDetailViewModel);
        }

        [HttpPost]
        public IActionResult EditFireDetails(FireDetailViewModel fireDetailViewModel)
        {
            var fireDetail = mapper.Map<FireDetail>(fireDetailViewModel);
            var accident = accidentRepository.Get(fireDetailViewModel.AccidentId);
            fireDetail.Accident = accident;

            fireDetailRepository.Save(fireDetail);
            return RedirectToAction(nameof(EditFire), new { id = fireDetailViewModel.AccidentId });
        }

            private AccidentDetailsViewModel GetAccidentDetailsViewModel(long id)
        {
            var accidentFromDb = accidentRepository.Get(id);
            var accidentDetailsViewModel = mapper.Map<AccidentDetailsViewModel>(accidentFromDb);

            // Lists for select tag
            accidentDetailsViewModel.CriminalOffenceArticlesList = GetSelectListFromEnum<CriminalCodeEnum>();

            // victims list
            // no items in the list = there were no victims (not "no info available")
            var victimsFromDb = accidentFromDb.AccidentVictims;
            var accidentVictimsViewModel = new List<AccidentVictimViewModel>();
            if (victimsFromDb.Count != 0)
            {
                foreach (var victim in victimsFromDb)
                {
                    accidentVictimsViewModel.Add(mapper.Map<AccidentVictimViewModel>(victim));
                }
            }
            accidentDetailsViewModel.AccidentVictimsViewModel = accidentVictimsViewModel;

            // address
            var accidentAddressFromDb = accidentFromDb.AccidentAddress;
            var addressViewModel = mapper.Map<AdressViewModel>(accidentAddressFromDb);
            if (addressViewModel == null)
            {
                addressViewModel = new AdressViewModel()
                {
                    City = Dictionaries.NotAvailable,
                    Street = String.Empty,
                };
            }
            accidentDetailsViewModel.AccidentAddressViewModel = addressViewModel;

            // Fire details
            var fireDetailFromDb = accidentFromDb.FireDetail;
            var fireDetailViewModel = mapper.Map<FireDetailViewModel>(fireDetailFromDb);
            if (fireDetailViewModel == null)
            {
                fireDetailViewModel = new FireDetailViewModel()
                {
                    AccidentId = accidentFromDb.Id,
                    FireCause = FireCauseEnum.NotAvailable,
                    FireCauseText = Dictionaries.GetText<FireCauseEnum>(FireCauseEnum.NotAvailable),
                    FireClass = FireClassEnum.NotAvailable,
                    FireClassText = Dictionaries.GetText<FireClassEnum>(FireClassEnum.NotAvailable),
                };
            }
            accidentDetailsViewModel.FireDetailViewModel = fireDetailViewModel;

            // Houses Destroyed in fire list
            // no items in the list = there were no destroyed houes (not "no info available")
            var housesDestroyedFromDb = accidentFromDb.HousesDestroyedInFire;
            var housesDestroyedInFireViewModel = new List<AdressViewModel>();
            if (housesDestroyedFromDb.Count != 0)
            {
                foreach (var house in housesDestroyedFromDb)
                {
                    housesDestroyedInFireViewModel.Add(mapper.Map<AdressViewModel>(house));
                }
            }
            accidentDetailsViewModel.HousesDestroyedInFireViewModel = housesDestroyedInFireViewModel;

            // Criminal Offence
            // Criminal offence articles
            var criminalArticlesFromDb = accidentFromDb.CriminalOffenceArticles;
            var criminalOffenceArticlesViewModel = new List<CriminalOffenceArticleViewModel>();
            if (criminalArticlesFromDb.Count != 0)
            {
                foreach (var article in criminalArticlesFromDb)
                {
                    criminalOffenceArticlesViewModel.Add(new CriminalOffenceArticleViewModel
                    {
                        SelectedCriminalOffenceArticle = (int)article.OffenceArticle,
                        CriminalOffenceArticleText = Dictionaries.GetText<CriminalCodeEnum>(article.OffenceArticle),
                    });
                }
            }
            accidentDetailsViewModel.CriminalOffenceArticlesViewModel = criminalOffenceArticlesViewModel;

            // Criminal offenders
            var offendersFromDb = accidentFromDb.CriminalOffenders;
            var accidentOffendersViewModel = new List<CriminalOffenderViewModel>();
            if (offendersFromDb.Count != 0)
            {
                foreach (var offender in offendersFromDb)
                {
                    accidentOffendersViewModel.Add(mapper.Map<CriminalOffenderViewModel>(offender));
                }
            }
            accidentDetailsViewModel.CriminalOffendersViewModel = accidentOffendersViewModel;
            return accidentDetailsViewModel;
        }

        private SelectList GetSelectListFromEnum<T>() where T : Enum
        {
            var listOfValues = new List<SelectListItem>();
            foreach (var valueAsObject in Enum.GetValues(typeof(T)))
            {
                listOfValues.Add(new SelectListItem
                {
                    Value = ((int)valueAsObject).ToString(),
                    Text = Dictionaries.GetText((T)valueAsObject)
                });
            };
            var selectList = new SelectList(listOfValues, "Value", "Text");
            return selectList;
        }

        private SelectList GetSelectListOfAddressesFromDb()
        {
            // TODO: take only a few records from address table
            var addressListFromDb = this.addressRepository.GetAll().
                Select(a => new { Id = a.Id, Address = $"{a.City}, ул.{a.Street}, д.{a.HouseNumber}"});
            var selectList = new SelectList(addressListFromDb, "Id", "Address");
            return selectList;
        }

        private SelectList GetSelectListOfCitizensFromDb()
        {
            // TODO: take only a few records from citizens table
            var citizensListFromDb = this.citizenUserRepository.GetAll().
                Select(c => new { Id = c.Id, Name = $"{c.FirstName} {c.LastName}" });

            //var citizensListFromDb = this.citizenUserRepository.GetAll();
            var selectList = new SelectList(citizensListFromDb, "Id", "Name"); 
            return selectList;
        }
    }
}
