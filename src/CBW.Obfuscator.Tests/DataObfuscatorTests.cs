using CBW.Obfuscator.Attributes;
using CBW.Obfuscator.Tests.Attributes;
using Xunit;

namespace CBW.Obfuscator.Tests
{
    public class DataObfuscatorTests
    {
        private IDataObfuscator _dataObfuscator;

        #region Sample data

        private Person _samplePerson;

        internal class Person
        {
            public string FirstName { get; set; }

            [Obfuscate]
            public string LastName { get; set; }

            [ObfuscateEmail]
            public string EmailAddress { get; set; }
        }

        #endregion

        public DataObfuscatorTests()
        {
            _dataObfuscator = new DataObfuscator();

            _samplePerson = new Person
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "john@doe.com"
            };
        }

        [Fact]
        public void Given_NoObfuscationAttribute_Expect_NoChange()
        {
            _dataObfuscator.Obfuscate(_samplePerson);

            Assert.Equal("John", _samplePerson.FirstName);
        }

        [Fact]
        public void Given_DefaultObfuscationService_Expect_RandomValue()
        {
            _dataObfuscator.Obfuscate(_samplePerson);

            Assert.NotEqual("Doe", _samplePerson.LastName);
        }

        [Fact]
        public void Given_CustomObfuscationService_Expect_ChangedValue()
        {
            _dataObfuscator.Obfuscate(_samplePerson);

            Assert.Contains("@deleted.com", _samplePerson.EmailAddress);
        }
    }
}