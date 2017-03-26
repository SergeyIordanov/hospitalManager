using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using AutoMapper;
using HospitalManager.BLL.DTO;
using HospitalManager.BLL.Esign;
using HospitalManager.BLL.Interfaces;
using HospitalManager.Core.Encryption;
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
                var userFolder = HostingEnvironment.ApplicationPhysicalPath + $"Content\\Artifacts\\{userId}";
                artifactDto.Extension = Path.GetExtension(artifact.Content.FileName);
                artifactDto.Path = userFolder + "\\" + artifact.Description;
                Directory.CreateDirectory(userFolder);

                using (var fs = System.IO.File.Create(userFolder + "\\" + artifact.Description))
                {
                    using (var ms = new MemoryStream())
                    {
                        artifact.Content.InputStream.CopyTo(ms);
                        var test = ms.GetBuffer().ProtectBytes(Entropy.EntropyBytes);
                        fs.Write(test, 0, test.Length);
                    }
                }

                _artifactService.Create(artifactDto, userId);

                return RedirectToAction("UserPage", "User");
            }

            return View("Create", artifact);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _artifactService.Delete(id);

            return RedirectToAction("UserPage", "User");
        }

        public ActionResult GetArtifactBytes(string extension)
        {
            switch (extension)
            {
                case ".png":
                    return File(System.IO.File.ReadAllBytes(HostingEnvironment.ApplicationPhysicalPath
                        + "/Content/Images/pngPlaceHolder.png"), "image/png");
                case ".jpg":
                    return File(System.IO.File.ReadAllBytes(HostingEnvironment.ApplicationPhysicalPath
                        + "/Content/Images/jpgPlaceHolder.png"), "image/png");
                case ".jpeg":
                    return File(System.IO.File.ReadAllBytes(HostingEnvironment.ApplicationPhysicalPath
                        + "/Content/Images/jpgPlaceHolder.png"), "image/png");
                case ".pdf":
                    return File(System.IO.File.ReadAllBytes(HostingEnvironment.ApplicationPhysicalPath
                        + "/Content/Images/pdfPlaceHolder.png"), "image/png");
                case ".docx":
                    return File(System.IO.File.ReadAllBytes(HostingEnvironment.ApplicationPhysicalPath
                        + "/Content/Images/wordPlaceHolder.png"), "image/png");
                case ".zip":
                    return File(System.IO.File.ReadAllBytes(HostingEnvironment.ApplicationPhysicalPath
                        + "/Content/Images/zipPlaceHolder.png"), "image/png");
                default:
                    return File(System.IO.File.ReadAllBytes(HostingEnvironment.ApplicationPhysicalPath
                        + "/Content/Images/txtPlaceHolder.png"), "image/png");
            }
        }

        public ActionResult Download(int id)
        {
            var imageBytes = _artifactService.GetArtifact(id);
            var userId = User.Identity.GetUserId();
            var artifactPath = HostingEnvironment.ApplicationPhysicalPath + $"Content\\Artifacts\\{userId}\\{imageBytes.Description}";
            var bytes = System.IO.File.ReadAllBytes(artifactPath).UnProtectBytes(Entropy.EntropyBytes);
            var ms = new MemoryStream(bytes);
            var mimetype = GetMimeType(bytes);
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