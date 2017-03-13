using System.Configuration;

namespace HospitalManager.WEB.Infrastructure
{
    public static class GlobalVaribles
    {
        public static string MerchantId => ConfigurationManager.AppSettings["MerchantId"];
    }
}