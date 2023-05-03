using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AirportsAndFlights.Models;

namespace AirportsAndFlights.Data
{
    public class AirportsAndFlightsContext : DbContext
    {
        public AirportsAndFlightsContext (DbContextOptions<AirportsAndFlightsContext> options)
            : base(options)
        {
        }

        public DbSet<AirportsAndFlights.Models.Airport> Airport { get; set; } = default!;

        public DbSet<AirportsAndFlights.Models.Flight> Flight { get; set; } = default!;
    }
}
