using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InsuranceSolution.API.Models
{
    public class CoveragePlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public DateTime EligibilityDateFrom { get; set; }
        public DateTime EligibilityDateTo { get; set; }
        public string EligibilityCountry{ get; set; }
        [JsonIgnore]
        public IList<RateChart> RateCharts { get; set; } = new List<RateChart>();
    }
}
