using System.Collections.Generic;

namespace Consultant_API_ASP.NET_Core.Data.Entities
{
    public class Competence
    {
        public int CompetenceId { get; set; }
        public string CompetenceName { get; set; }
        //Measurement of competency according to
        // http://sijinjoseph.com/programmer-competency-matrix/
        // 0 [very low] to 3 [very high]
        public int CompetenceLevel { get; set; }
        
        public IList<ConsultantCompetence> CompetenceConsultants { get; set; }
    }
}
