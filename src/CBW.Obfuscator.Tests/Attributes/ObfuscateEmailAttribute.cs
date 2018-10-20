using CBW.Obfuscator.Attributes;
using CBW.Obfuscator.Tests.Services;

namespace CBW.Obfuscator.Tests.Attributes
{
    public class ObfuscateEmailAttribute : ObfuscateAttribute
    {
        public ObfuscateEmailAttribute()
        {
            base.ObfuscationService = typeof(EmailObfuscationService);
        }
    }
}