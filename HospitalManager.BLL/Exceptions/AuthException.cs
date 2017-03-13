using System;

namespace HospitalManager.BLL.Exceptions
{
    public class AuthException : Exception
    {
        public string Property { get; private set; }

        public AuthException(string property, string message) : base(message)
        {
            Property = property;
        }
    }
}
