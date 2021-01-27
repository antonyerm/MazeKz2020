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

namespace WebMaze.Controllers
{
    public class LifeController : Controller
    {
        private WebMazeContext context;
        private AccidentRepository accidentRepository;
        private AdressRepository addresRepository;
        private CitizenUserRepository citizenUserRepository;
        private IMapper mapper;

        public LifeController(WebMazeContext context, IMapper mapper,
                              AccidentRepository accidentRepository,
                              AdressRepository addresRepository,
                              CitizenUserRepository citizenUserRepository)
        {
            this.context = context;
            this.accidentRepository = accidentRepository;
            this.addresRepository = addresRepository;
            this.citizenUserRepository = citizenUserRepository;
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
                accidentsViewModel.Add(individualAccidentViewModel);
            }

            return View("~/Views/Life/ShowAccidents.cshtml", accidentsViewModel);
        }

        [HttpGet]
        public IActionResult AddEditAccident(long? id)
        {
            var accidentCategoryList = GetSelectListFromEnum<AccidentCategoryEnum>();
            // TODO: take only a few records from address table
            var addressListFromDb = this.addresRepository.GetAll();
            var accidentAddressList = new SelectList(addressListFromDb,
                                                     nameof(Adress.Id),
                                                     nameof(Adress.City)); // address string needs building

            var accidentViewModel = new AccidentViewModel()
            {
                AccidentDate = DateTime.Now,
                SelectedAccidentAddress = null,
                AccidentAddressList = accidentAddressList,
                SelectedAccidentCategory = null,
                AccidentCategoryList = accidentCategoryList,
                AccidentDescription = ""
            };
            if (id != null)
            {
                // edit accident
                var accidentFromDb = this.accidentRepository.Get((int)id);
                accidentViewModel = mapper.Map<AccidentViewModel>(accidentFromDb);
            }

            return PartialView("~/Views/Life/_AddEditAccident.cshtml", accidentViewModel);
        }

        public IActionResult AddUsers()
        {
            
        }
        




        private List<SelectListItem> GetSelectListFromEnum<T>() where T : Enum
        {
            var listOfValues = new List<SelectListItem>();
            foreach (var valueAsObject in Enum.GetValues(typeof(T)))
            {
                var valueAsInt = (int)valueAsObject;
                listOfValues.Add(new SelectListItem
                {
                    Value = ((int)valueAsObject).ToString(),
                    Text = Dictionaries.GetText((T)valueAsObject)
                });
            };
            return listOfValues;
        }
    }
}
