using System;
using System.Reflection;

namespace HospitalManager.WEB.Infrastructure.MimeTypesResolver
{
    public static class GetEnumStringAttribute
    {
        public static string GetStringValue(this Enum value)
        {
            string output = null;
            var type = value.GetType();

            FieldInfo fi = type.GetField(value.ToString());
            EnumStringValue[] attrs =
               fi.GetCustomAttributes(typeof(EnumStringValue),
                                       false) as EnumStringValue[];
            if(attrs != null && attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }
    }
}