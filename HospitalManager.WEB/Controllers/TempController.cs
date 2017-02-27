using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using HospitalManager.BLL.DTO;
using HospitalManager.BLL.Interfaces;
using HospitalManager.WEB.ViewModels;

namespace HospitalManager.WEB.Controllers
{
    public class TempController : Controller
    {
        private readonly ITempService _tempService;

        public TempController(ITempService tempService)
        {
            _tempService = tempService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var tempsDto = _tempService.GetAll();
            var tempsViewModel = Mapper.Map<IEnumerable<TempViewModel>>(tempsDto);

            return View(tempsViewModel);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var tempDto = _tempService.Get(id);
            var tempViewModel = Mapper.Map<TempViewModel>(tempDto);

            return View(tempViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TempViewModel tempViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(tempViewModel);
            }

            var tempDto = Mapper.Map<TempDto>(tempViewModel);
            _tempService.Create(tempDto);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int id)
        {
            var tempDto = _tempService.Get(id);
            var tempViewModel = Mapper.Map<TempViewModel>(tempDto);

            return View(tempViewModel);
        }

        [HttpPost]
        public ActionResult Edit(TempViewModel tempViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(tempViewModel);
            }

            var tempDto = Mapper.Map<TempDto>(tempViewModel);
            _tempService.Edit(tempDto);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Delete(int id)
        {
            _tempService.Delete(id);

            return RedirectToAction("Index", "Home");
        }
    }
}