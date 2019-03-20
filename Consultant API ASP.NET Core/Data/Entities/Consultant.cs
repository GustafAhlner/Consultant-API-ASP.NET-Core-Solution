using System;
using System.Collections.Generic;

namespace Consultant_API_ASP.NET_Core.Data.Entities
{
    public class Consultant
    {
        public Consultant()
        {
            this.Addresses = new List<Address>();
        }

        public int ConsultantId { get; set; }
        public DateTime BirthDate { get; set; }
        public string NameFirst { get; set; }
        public string NameSecond { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string ImageURL { get; set; }

        public IList<Address> Addresses { get; set; }
        public IList<ConsultantCompetence> CompetenceConsultants { get; set; }
        
    }
}
