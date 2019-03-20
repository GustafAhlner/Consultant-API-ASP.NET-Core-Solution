using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultant_API_ASP.NET_Core.Data.Entities
{
    public class ConsultantCompetence
    {
        public int ConsultantId { get; set; }
        public Consultant Consultant { get; set; }

        public int CompetenceId { get; set; }
        public Competence Competence { get; set; }
    }
}
