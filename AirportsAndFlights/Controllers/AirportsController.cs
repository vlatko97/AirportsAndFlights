using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AirportsAndFlights.Data;
using AirportsAndFlights.Models;
using NuGet.Protocol;

namespace AirportsAndFlights.Controllers
{
    public class AirportsController : Controller
    {
        private readonly AirportsAndFlightsContext _context;

        public AirportsController(AirportsAndFlightsContext context)
        {
            _context = context;
        }

        // GET: Airports
        public async Task<IActionResult> Index()
        {
              return _context.Airport != null ? 
                          View(await _context.Airport.ToListAsync()) :
                          Problem("Entity set 'AirportsAndFlightsContext.Airport'  is null.");
        }

        // GET: Airports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Airport == null)
            {
                return NotFound();
            }

            var airport = await _context.Airport
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airport == null)
            {
                return NotFound();
            }

            return View(airport);
        }

        // GET: Airports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Airports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AirportName,CountryName,AirportCode,NoPassengersAnnually")] Airport airport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(airport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(airport);
        }

        // GET: Airports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Airport == null)
            {
                return NotFound();
            }

            var airport = await _context.Airport.FindAsync(id);
            if (airport == null)
            {
                return NotFound();
            }
            return View(airport);
        }

        // POST: Airports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AirportName,CountryName,AirportCode,NoPassengersAnnually")] Airport airport)
        {
            if (id != airport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(airport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AirportExists(airport.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(airport);
        }

        // GET: Airports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Airport == null)
            {
                return NotFound();
            }

            var airport = await _context.Airport
                .FirstOrDefaultAsync(m => m.Id == id);
            if (airport == null)
            {
                return NotFound();
            }

            return View(airport);
        }

        // POST: Airports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Airport == null)
            {
                return Problem("Entity set 'AirportsAndFlightsContext.Airport'  is null.");
            }
            var airport = await _context.Airport.FindAsync(id);
            if (airport != null)
            {
                _context.Airport.Remove(airport);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Route("Airports/GetFlightsFrom/{airportCode}")]
        public async Task<IActionResult> GetFlightsFrom()
        {
            var airportCode = ControllerContext.RouteData.Values.Values.ElementAt(2);
            if (airportCode == null || _context.Airport == null)
            {
                return NotFound();
            }
            
            var airport = await _context.Airport
                .FirstOrDefaultAsync(m => m.AirportCode == airportCode);
            
            if (airport == null)
            {
                return NotFound();
            }
            
            ViewData["ListOfFlights"] = _context.Flight.Where(f => f.CodeOriginAirport == airport.AirportCode)
                .OrderBy(f => f.CodeDestinationAirport)
                .ThenBy(f => f.DepartureTimeInMinutesSinceMidnight)
                .ToList();

            return View("GetFlights", airport);
        }

        [Route("Airports/GetFlightsTo/{airportCode}")]
        public async Task<IActionResult> GetFlightsTo()
        {
            var airportCode = ControllerContext.RouteData.Values.Values.ElementAt(2);
            if (airportCode == null || _context.Airport == null)
            {
                return NotFound();
            }

            var airport = await _context.Airport
                .FirstOrDefaultAsync(m => m.AirportCode == airportCode);

            if (airport == null)
            {
                return NotFound();
            }

            ViewData["ListOfFlights"] = _context.Flight.Where(f => f.CodeDestinationAirport == airport.AirportCode)
                .OrderBy(f => f.CodeOriginAirport)
                .ThenBy(f => f.DepartureTimeInMinutesSinceMidnight)
                .ToList();

            return View("GetFlights", airport);
        }

        [Route("Airports/MostPassengers/{countryName}")]
        public async Task<IActionResult> MostPassengers()
        {
            var countryName = ControllerContext.RouteData.Values.Values.ElementAt(2);
            if (countryName == null || _context.Airport == null)
            {
                return NotFound();
            }

            var airport = await _context.Airport
                .FirstOrDefaultAsync(a => a.CountryName == countryName);

            if (airport == null)
            {
                return NotFound();
            }

            var countryAirports = _context.Airport.Where(a => a.CountryName == countryName);
            var mostPassengers = countryAirports.Max(a => a.NoPassengersAnnually);

            return Json(countryAirports.Where(a => a.NoPassengersAnnually == mostPassengers));
        }
        private bool AirportExists(int id)
        {
          return (_context.Airport?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
