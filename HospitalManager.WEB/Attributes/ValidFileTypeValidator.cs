using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace HospitalManager.WEB.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidFileTypeValidator : ValidationAttribute
    {
        private const int MaxFileSize = 5000000;

        private static readonly string[] ValidFileFormats = {".pdf", ".docx", ".png", ".jpg", ".jpeg", ".txt", ".zip"};

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as HttpPostedFileBase;

            if (file == null)
            {
                return new ValidationResult("Please upload a file!");
            }

            return file.ContentLength > MaxFileSize 
                ? new ValidationResult("This file is too big! (5MB maximum)") 
                : ValidExtension(Path.GetExtension(file.FileName));
        }

        private ValidationResult ValidExtension(string ext)
        {
            if (string.IsNullOrEmpty(ext))
            {
                return new ValidationResult("Not valid file extension");
            }
            return ValidFileFormats.Any(x => x.Equals(ext, StringComparison.OrdinalIgnoreCase)) 
                ? ValidationResult.Success 
                : new ValidationResult("Possible file extensions are PDF, DOCX, JPG, PNG, JPEG, ZIP and TXT!");
        }
    }
}