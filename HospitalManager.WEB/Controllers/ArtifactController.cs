using System;
using System.Drawing;
using System.IO;
using System.Web.Hosting;
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
        private readonly IArtifactService _illnessHistoryService;

        public ArtifactController(IArtifactService illnessHistoryService)
        {
            _illnessHistoryService = illnessHistoryService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ArtifactCreateViewModel artifact)
        {
            if (ModelState.IsValid)
            {
                var artifactDto = Mapper.Map<ArtifactDto>(artifact);
                var userId = User.Identity.GetUserId();
                _illnessHistoryService.Create(artifactDto, userId);
            }

            return RedirectToAction("UserPage", "User");
        }

        public ActionResult Delete(int id)
        {
            _illnessHistoryService.Delete(id);

            return RedirectToAction("UserPage", "User");
        }

        public ActionResult GetArtifact(int id)
        {
            var artifactDto = _illnessHistoryService.GetArtifact(id);

            var artifact = Mapper.Map<ArtifactCreateViewModel>(artifactDto);

            return View("", artifact);
        }

        public ActionResult GetArtifactBytes(int id)
        {
            var artifactDto = _illnessHistoryService.GetArtifact(id);
            var isImage = IsValidimage(artifactDto.Content);
            
            if (isImage)
            {
                return File(artifactDto.Content, "image/png");
            }

            var fileBytes = System.IO.File.ReadAllBytes(HostingEnvironment.ApplicationPhysicalPath + "/Content/Images/placeholder50x50.png");
            return File(fileBytes, "image/png");
        }

        private bool IsValidimage(byte[] bytes)
        {
            try
            {
                using(var ms = new MemoryStream(bytes))
                    Image.FromStream(ms);
            }
            catch(ArgumentException)
            {
                return false;
            }
            return true;
        }
    }
}