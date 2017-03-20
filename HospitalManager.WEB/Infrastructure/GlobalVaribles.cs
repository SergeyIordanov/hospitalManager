using System.Collections.Generic;
using System.Configuration;

namespace HospitalManager.WEB.Infrastructure
{
    public static class GlobalVaribles
    {
        public static string MerchantId => ConfigurationManager.AppSettings["MerchantId"];
        public static List<string> TemplateUserNames => new List<string> { "Alexey Turuta" , "Алексей Турута", "Oleksii Budianskyi"};

    }
}