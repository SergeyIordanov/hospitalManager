using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using System.Web.Mvc;
using AutoMapper;
using HospitalManager.BLL.DTO;
using HospitalManager.BLL.Interfaces;
using HospitalManager.WEB.Infrastructure.MimeTypesResolver;
using HospitalManager.WEB.ViewModels;
using Microsoft.AspNet.Identity;

namespace HospitalManager.WEB.Controllers
{
    public class ArtifactController : Controller
    {
        private readonly IArtifactService _artifactService;

        public ArtifactController(IArtifactService artifactService)
        {
            _artifactService = artifactService;
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
                _artifactService.Create(artifactDto, userId);

                return RedirectToAction("UserPage", "User");
            }

            return View("Create", artifact);
        }

        public ActionResult Delete(int id)
        {
            _artifactService.Delete(id);

            return RedirectToAction("UserPage", "User");
        }

        public ActionResult GetArtifactBytes(int id)
        {
            var artifactDto = _artifactService.GetArtifact(id);
            byte[] placeholder;

            if (IsValidimage(artifactDto.Content))
            {
                return File(artifactDto.Content, "image/png");
            }
            if (IsPdf(artifactDto.Content))
            {
                placeholder = System.IO.File.ReadAllBytes(HostingEnvironment.ApplicationPhysicalPath + "/Content/Images/pdfPlaceHolder.png");
                return File(placeholder, "image/png");
            }
            if (IsWord(artifactDto.Content))
            {
                placeholder = System.IO.File.ReadAllBytes(HostingEnvironment.ApplicationPhysicalPath + "/Content/Images/wordPlaceHolder.png");
                return File(placeholder, "image/png");
            }
            if (IsZip(artifactDto.Content))
            {
                placeholder = System.IO.File.ReadAllBytes(HostingEnvironment.ApplicationPhysicalPath + "/Content/Images/zipPlaceHolder.png");
                return File(placeholder, "image/png");
            }

            placeholder = System.IO.File.ReadAllBytes(HostingEnvironment.ApplicationPhysicalPath + "/Content/Images/txtPlaceHolder.png");
            return File(placeholder, "image/png");
        }

        public ActionResult Download(int id)
        {
            var imageBytes = _artifactService.GetArtifact(id);
            var ms = new MemoryStream(imageBytes.Content);
            var mimetype = GetMimeType(imageBytes.Content);
            var fstring = mimetype.GetStringValue();

            switch (mimetype)
            {
                case StringValueEnum.Docx:
                    return File(ms, fstring, "Artifact.docx");
                case StringValueEnum.Jpg:
                    return File(ms, fstring, "Artifact.jpg");
                case StringValueEnum.Png:
                    return File(ms, fstring, "Artifact.png");
                case StringValueEnum.Pdf:
                    return File(ms, fstring, "Artifact.pdf");
                case StringValueEnum.Txt:
                    return File(ms, fstring, "Artifact.txt");
                case StringValueEnum.Zip:
                    return File(ms, fstring, "Artifact.zip");
                default:
                    throw new NotImplementedException();
            }
        }

        private StringValueEnum GetMimeType(byte[] file)
        {
            var mime = StringValueEnum.Txt;

            if(IsWord(file))
            {
                mime = StringValueEnum.Docx;
            }
            else if(file.Take(3).SequenceEqual(MimeTypesIdentifiers.Jpg))
            {
                mime = StringValueEnum.Jpg;
            }
            else if(IsPdf(file))
            {
                mime = StringValueEnum.Pdf;
            }
            else if(file.Take(16).SequenceEqual(MimeTypesIdentifiers.Png))
            {
                mime = StringValueEnum.Png;
            }
            else if (IsZip(file))
            {
                mime = StringValueEnum.Zip;
            }

            return mime;
        }

        private bool IsValidimage(byte[] bytes)
        {
            try
            {
                using (var ms = new MemoryStream(bytes))
                {
                    Image.FromStream(ms);
                }
            }
            catch(ArgumentException)
            {
                return false;
            }
            return true;
        }

        private bool IsPdf(byte[] bytes)
        {
            return bytes.Take(7).SequenceEqual(MimeTypesIdentifiers.Pdf);
        }

        private bool IsWord(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes).Contains(MimeTypesIdentifiers.Word);
        }

        private bool IsZip(byte[] bytes)
        {
            return bytes.Take(4).SequenceEqual(MimeTypesIdentifiers.Zip);
        }
    }
}