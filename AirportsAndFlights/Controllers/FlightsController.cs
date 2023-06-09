﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AirportsAndFlights.Data;
using AirportsAndFlights.Models;

namespace AirportsAndFlights.Controllers
{
    public class FlightsController : Controller
    {
        private readonly AirportsAndFlightsContext _context;

        public FlightsController(AirportsAndFlightsContext context)
        {
            _context = context;
        }

        // GET: Flights
        public async Task<IActionResult> Index()
        {
              return _context.Flight != null ? 
                          View(await _context.Flight.ToListAsync()) :
                          Problem("Entity set 'AirportsAndFlightsContext.Flight'  is null.");
        }

        // GET: Flights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Flight == null)
            {
                return NotFound();
            }

            var flight = await _context.Flight
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Flights/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CodeOriginAirport,CodeDestinationAirport,DepartureTimeInMinutesSinceMidnight,FlightDurationInMinutes")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ListOfAirports"] = _context.Airport.ToList();

            return View(flight);
        }

        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Flight == null)
            {
                return NotFound();
            }

            var flight = await _context.Flight.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CodeOriginAirport,CodeDestinationAirport,DepartureTimeInMinutesSinceMidnight,FlightDurationInMinutes")] Flight flight)
        {
            if (id != flight.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.Id))
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
            return View(flight);
        }

        // GET: Flights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Flight == null)
            {
                return NotFound();
            }

            var flight = await _context.Flight
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Flight == null)
            {
                return Problem("Entity set 'AirportsAndFlightsContext.Flight'  is null.");
            }
            var flight = await _context.Flight.FindAsync(id);
            if (flight != null)
            {
                _context.Flight.Remove(flight);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Route("Flights/DirectFlights/{codeOriginAirport}/{codeDestinationAirport}")]
        public async Task<IActionResult> DirectFlights()
        {
            var codeOriginAirport = ControllerContext.RouteData.Values.Values.ElementAt(2);
            var codeDestinationAirport = ControllerContext.RouteData.Values.Values.ElementAt(3);
            return _context.Flight != null ?
                          View("Index", await _context.Flight
                          .Where(f => f.CodeOriginAirport == codeOriginAirport && f.CodeDestinationAirport == codeDestinationAirport)
                          .ToListAsync()) :
                          Problem("Entity set 'AirportsAndFlightsContext.Flight'  is null.");
        }

        private bool FlightExists(int id)
        {
          return (_context.Flight?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
