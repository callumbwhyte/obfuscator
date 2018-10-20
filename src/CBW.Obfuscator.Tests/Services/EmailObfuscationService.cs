using System;

namespace CBW.Obfuscator.Tests.Services
{
    class EmailObfuscationService : IObfuscationService
    {
        public object Obfuscate(object oldValue)
        {
            return Guid.NewGuid().ToString() + "@deleted.com";
        }
    }
}