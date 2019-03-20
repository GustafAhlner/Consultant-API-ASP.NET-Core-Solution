namespace Consultant_API_ASP.NET_Core.Data.Entities
{
    public class Address
    {
        public int AddressId { get; set; }
        public string AddressLine { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string CountryRegion { get; set; }
        public string Comment { get; set; }
        public int ConsultantId { get; set; }
    }
}
