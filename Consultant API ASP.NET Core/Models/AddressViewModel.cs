namespace Consultant_API_ASP.NET_Core.Models
{
    public class AddressViewModel
    {
        public int AddressId { get; set; }
        public string AddressLine { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string CountryRegion { get; set; }
        public string Comment { get; set; }
        public ConsultantViewModel consultant { get; set; }
    }
}