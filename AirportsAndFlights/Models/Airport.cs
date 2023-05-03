using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace AirportsAndFlights.Models
{
    public class Airport
    {
        public int Id { get; set; }

        [Display(Name = "Airport Name")]
        public string AirportName { get; set; }

        [Display(Name = "Country Name")]
        public string CountryName { get; set; }

        [Display(Name = "Airport Code")]
        public string AirportCode { get; set; }

        [Display(Name = "Number of passengers annually")]
        public long NoPassengersAnnually { get; set; }

    }
}
