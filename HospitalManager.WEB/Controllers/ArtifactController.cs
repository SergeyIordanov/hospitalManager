using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using HospitalManager.BLL.DTO;
using HospitalManager.BLL.Interfaces;
using HospitalManager.WEB.ViewModels;
using Microsoft.AspNet.Identity;

namespace HospitalManager.WEB.Controllers
{
    public class ArtifactController : Controller
    {
        private readonly IIllnessHistoryService _illnessHistoryService;

        public ArtifactController(IIllnessHistoryService illnessHistoryService)
        {
            _illnessHistoryService = illnessHistoryService;
        }

        public ActionResult GetUserArtifacts()
        {
            var artifactDtos = _illnessHistoryService.GetUserIllnessHistory(User.Identity.GetUserId());
            var artifactViewModels = Mapper.Map<IEnumerable<ArtifactViewModel>>(artifactDtos);

            return View("");
        }

        public ActionResult Create(ArtifactViewModel artifact)
        {
            if (ModelState.IsValid)
            {
                var artifactDto = Mapper.Map<ArtifactDto>(artifact);
                _illnessHistoryService.Create(artifactDto);
            }

            return RedirectToAction("");
        }

        public ActionResult Delete(int id)
        {
            _illnessHistoryService.Delete(id);

            return RedirectToAction("");
        }

        public ActionResult Update(ArtifactViewModel artifact)
        {
            if (ModelState.IsValid)
            {
                var artifactDto = Mapper.Map<ArtifactDto>(artifact);
                _illnessHistoryService.Update(artifactDto);
            }

            return RedirectToAction("");
        }

        public ActionResult GetArtifact(int id)
        {
            var artifactDto = _illnessHistoryService.GetIllnessHistoryArtifact(id);

            var artifact = Mapper.Map<ArtifactViewModel>(artifactDto);

            return View("", artifact);
        }
    }
}