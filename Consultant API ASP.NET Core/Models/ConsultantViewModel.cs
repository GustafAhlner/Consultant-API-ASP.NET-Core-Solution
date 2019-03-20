using System;
using System.Collections.Generic;

namespace Consultant_API_ASP.NET_Core.Models
{
    public class ConsultantViewModel
    {
        public int ConsultantId { get; set; }
        public DateTime BirthDate { get; set; }
        public string NameFirst { get; set; }
        public string NameSecond { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string ImageURL { get; set; }

        public ICollection<AddressViewModel> Addresses { get; set; }
        public ICollection<CompetenceViewModel> Competences { get; set; }
    }
}
