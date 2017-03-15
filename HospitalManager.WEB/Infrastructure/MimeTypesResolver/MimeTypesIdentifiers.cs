namespace HospitalManager.WEB.Infrastructure.MimeTypesResolver
{
    public static class MimeTypesIdentifiers
    {
        public const string Word = "word";
        public static readonly byte[] Jpg = { 255, 216, 255 };
        public static readonly byte[] Pdf = { 37, 80, 68, 70, 45, 49, 46 };
        public static readonly byte[] Png = { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82 };
    }
}