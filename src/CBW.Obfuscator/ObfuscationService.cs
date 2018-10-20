using System;

namespace CBW.Obfuscator
{
    public interface IObfuscationService
    {
        object Obfuscate(object oldValue);
    }

    public class ObfuscationService : IObfuscationService
    {
        public virtual object Obfuscate(object oldValue)
        {
            return Guid.NewGuid().ToString();
        }
    }
}