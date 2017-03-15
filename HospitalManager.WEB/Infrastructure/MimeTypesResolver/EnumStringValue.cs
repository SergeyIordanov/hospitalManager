using System;

namespace HospitalManager.WEB.Infrastructure.MimeTypesResolver
{
    public class EnumStringValue : Attribute
    {
        private readonly string _value;

        public EnumStringValue(string value)
        {
            _value = value;
        }

        public string Value => _value;
    }
}