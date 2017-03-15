namespace HospitalManager.WEB.Infrastructure.MimeTypesResolver
{
    public enum StringValueEnum
    {
        [EnumStringValue("application/msword")]
        Docx = 1,

        [EnumStringValue("application/pdf")]
        Pdf = 2,

        [EnumStringValue("image/jpeg")]
        Jpg = 3,

        [EnumStringValue("image/png")]
        Png = 4,

        [EnumStringValue("application/octet-stream")]
        Txt = 5
    }
}