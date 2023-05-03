using System.ComponentModel.DataAnnotations;

namespace AirportsAndFlights.Models
{
    public class Flight
    {
        public int Id { get; set; }

        [Display(Name = "Origin Airport")]
        public string CodeOriginAirport { get; set; }

        [Display(Name = "Destination Airport")]
        public string CodeDestinationAirport { get; set; }

        [Display(Name = "Departure Time")]
        public int DepartureTimeInMinutesSinceMidnight { get; set; }

        [Display(Name = "Flight Duration")]
        public int FlightDurationInMinutes { get; set; }


    }
}
