using System;

namespace InsuranceSolution.API.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string  City { get; set; }
        public string Sate { get; set; }
        public string Country { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public DateTime SaleDate { get; set; } 
        public int RateChartId { get; set; }
        public RateChart RateChart { get; set; }
    }
}
