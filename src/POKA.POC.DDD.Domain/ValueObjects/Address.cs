using POKA.POC.DDD.Domain.Enums;

namespace POKA.POC.DDD.Domain.ValueObjects
{
    public record Address
    {
        public CountryEnum Country { get; private set; } = null!;
        public string PostalCode { get; private set; } = null!;
        public string City { get; private set; } = null!;
        public string Line1 { get; private set; } = null!;
        public string? Line2 { get; private set; }
        public string? State { get; private set; }

        public Address()
        {
        }

        public Address(CountryEnum country)
        {
            Country = country;
        }

        public Address(CountryEnum country, string city, string line1, string postalCode)
        {
            PostalCode = postalCode;
            Country = country;
            Line1 = line1;
            City = city;
        }

        public Address(string countryCodeISO2, string city, string line1, string postalCode)
        {
            Country = CountryEnum.FromValueCodeISO2(countryCodeISO2);
            PostalCode = postalCode;
            Line1 = line1;
            City = city;
        }

        public Address(string countryCodeISO2, string state, string city, string line1, string line2, string postalCode)
        {
            Country = CountryEnum.FromValueCodeISO2(countryCodeISO2);
            PostalCode = postalCode;
            Line1 = line1;
            Line2 = line2;
            State = state;
            City = city;
        }

        public Address(string countryCodeISO2)
        {
            Country = CountryEnum.FromValueCodeISO2(countryCodeISO2);
        }

        public void ChangeCountry(CountryEnum value) => this.Country = value;
    }
}
