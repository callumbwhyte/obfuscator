using System;

namespace CBW.Obfuscator.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ObfuscateAttribute : Attribute
    {
        /// <summary>
        /// Indicates which properties should be processed by the obfuscator
        /// </summary>
        public ObfuscateAttribute()
        {
            if (!typeof(IObfuscationService).IsAssignableFrom(ObfuscationService))
            {
                throw new Exception("Supplied type is not a valid obfuscation service");
            }
        }

        /// <summary>
        /// The type for the obfuscation service desired to be used. If null the default service will be used
        /// </summary>
        public Type ObfuscationService = typeof(ObfuscationService);
    }
}