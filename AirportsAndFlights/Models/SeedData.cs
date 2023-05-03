using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AirportsAndFlights.Data;
using System;
using System.Linq;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using System.Globalization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportsAndFlights.Models
{
    public static class SeedData
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AirportsAndFlightsContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AirportsAndFlightsContext>>()))
            {
                var tableEmptyFlag = true;
                
                // Look for any airports.
                if (context.Airport.Any())
                {
                     tableEmptyFlag=false;   // DB has been seeded
                }
                StreamReader reader = new StreamReader(File.OpenRead("SeedData/airports.csv"));
                reader.ReadLine(); // to skip reading column Headers

                while (tableEmptyFlag && !reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');
                    context.Airport.Add(
                        new Airport
                        {
                            AirportName = values[0],
                            CountryName = values[1],
                            AirportCode = values[2],
                            NoPassengersAnnually = long.Parse(values[3])
                        }
                        );
                }

                // Look for any flights.
                if (context.Flight.Any())
                {
                    return;   // DB has been seeded
                }

                reader = new StreamReader(File.OpenRead("SeedData/flights.csv"));
                reader.ReadLine(); // to skip reading column Headers
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');
                    context.Flight.Add(
                        new Flight
                        {
                            CodeOriginAirport = values[0],
                            CodeDestinationAirport = values[1],
                            DepartureTimeInMinutesSinceMidnight = Int32.Parse(values[2]),
                            FlightDurationInMinutes = Int32.Parse(values[3])
                        }
                        );
                }
                context.SaveChanges();
            }
        }
    }
}
