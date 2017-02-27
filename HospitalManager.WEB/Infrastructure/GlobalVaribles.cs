using System.Configuration;

namespace HospitalManager.WEB.Infrastructure
{
    public class GlobalVaribles
    {
        public static string ExampleVariable => ConfigurationManager.AppSettings["Example"];
    }
}