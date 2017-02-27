using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using HospitalManager.BLL.DTO;
using HospitalManager.BLL.Interfaces;
using HospitalManager.WEB.ViewModels;

namespace HospitalManager.WEB.Controllers
{
    public class ExampleController : Controller
    {
        private readonly IExampleService _exampleService;

        public ExampleController(IExampleService exampleService)
        {
            _exampleService = exampleService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var examplesDto = _exampleService.GetAll();
            var examplesViewModel = Mapper.Map<IEnumerable<ExampleViewModel>>(examplesDto);

            return View(examplesViewModel);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var exampleDto = _exampleService.Get(id);
            var exampleViewModel = Mapper.Map<ExampleViewModel>(exampleDto);

            return View(exampleViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ExampleViewModel exampleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(exampleViewModel);
            }

            var exampleDto = Mapper.Map<ExampleDto>(exampleViewModel);
            _exampleService.Create(exampleDto);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int id)
        {
            var exampleDto = _exampleService.Get(id);
            var exampleViewModel = Mapper.Map<ExampleViewModel>(exampleDto);

            return View(exampleViewModel);
        }

        [HttpPost]
        public ActionResult Edit(ExampleViewModel exampleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(exampleViewModel);
            }

            var exampleDto = Mapper.Map<ExampleDto>(exampleViewModel);
            _exampleService.Edit(exampleDto);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Delete(int id)
        {
            _exampleService.Delete(id);

            return RedirectToAction("Index", "Home");
        }
    }
}