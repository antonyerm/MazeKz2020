using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff;
using WebMaze.DbStuff.Model.Life;
using WebMaze.DbStuff.Repository.Life;
using WebMaze.Models.Life;

namespace WebMaze.Controllers
{
    public class LifeController : Controller
    {
        private WebMazeContext context;
        private AccidentRepository repository;
        private IMapper mapper;

        public LifeController(WebMazeContext context, AccidentRepository repository, IMapper mapper)
        {
            this.context = context;
            this.repository = repository;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View("/Views/Life/LifeIndex.cshtml");
        }

        public IActionResult ShowAccidents()
        {
            var accidentsViewModel = new List<AccidentViewModel>();
            var accidentsFromDb = repository.GetAll();
            foreach (var item in accidentsFromDb)
            {
                var individualAccidentViewModel = mapper.Map<AccidentViewModel>(item);
                accidentsViewModel.Add(individualAccidentViewModel);
            }
            //var accidentsViewModel = mapper.Map<AccidentViewModel>(new Accident {
            //    AccidentCategory = AccidentCategoryEnum.Fire });
            //var cat = accidentsViewModel.AccidentCategory;

            return View("/Views/Life/ShowAccidents.cshtml", accidentsViewModel);
        }
    }
}
