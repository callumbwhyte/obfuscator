using CBW.Obfuscator.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CBW.Obfuscator
{
    public interface IDataObfuscator
    {
        void Obfuscate(object objectToObfuscate, Type modelType);
        void Obfuscate<T>(T objectToObfuscate) where T : class;
    }

    public class DataObfuscator : IDataObfuscator
    {
        private IDictionary<PropertyInfo, Type> _propertyObfuscatorsList;

        public void Obfuscate(object objectToObfuscate, Type modelType)
        {
            // build list of properties and their obfuscators
            MapPropertiesToObfuscators(modelType);

            foreach (var propertyInfo in _propertyObfuscatorsList)
            {
                if (propertyInfo.Value != null)
                {
                    // fetch obfuscator
                    var obfuscator = (IObfuscationService)Activator.CreateInstance(propertyInfo.Value);

                    // run obfuscation
                    ObfuscateProperty(objectToObfuscate, propertyInfo.Key, obfuscator);
                }
            }
        }

        public void Obfuscate<T>(T objectToObfuscate)
            where T : class
        {
            Obfuscate(objectToObfuscate, typeof(T));
        }

        protected virtual void ObfuscateProperty<T>(T objectToObfuscate, PropertyInfo propertyInfo, IObfuscationService obfuscator)
        {
            // get property value
            var oldValue = propertyInfo.GetValue(objectToObfuscate);

            if (oldValue == null)
            {
                return;
            }

            // run obfuscator
            var obfuscatedValue = obfuscator.Obfuscate(oldValue);

            // set new value to property
            propertyInfo.SetValue(objectToObfuscate, obfuscatedValue);
        }

        private void MapPropertiesToObfuscators(Type modelType)
        {
            _propertyObfuscatorsList = modelType.GetProperties()
                .ToDictionary(k => k, v =>
                {
                    // get obfuscation attribute on property
                    var obfuscateAttribute = v.GetCustomAttribute<ObfuscateAttribute>();

                    if (obfuscateAttribute != null)
                    {
                        return obfuscateAttribute.ObfuscationService;
                    }

                    return null;
                });
        }
    }
}