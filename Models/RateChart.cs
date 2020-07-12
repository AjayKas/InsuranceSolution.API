using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InsuranceSolution.API.Models
{
    public class RateChart
    {
        public int Id { get; set; }

        public CoveragePlan CoveragePlan { get; set; }
        public int CoveragePlanId { get; set; }
        public string Gender { get; set; }
        public int? AgeBelow { get; set; }
        public int? AgeAbove { get; set; }
        public decimal NetPrice { get; set; }
        [JsonIgnore]
        public IList<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
