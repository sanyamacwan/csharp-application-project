using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab_5.Data;
using lab_5.Models;
using lab_5.Models.ViewModels;

namespace lab_5.Controllers
{
    public class FoodDeliveryServicesController : Controller
    {
        private readonly DealsFinderDbContext _context;

        public FoodDeliveryServicesController(DealsFinderDbContext context)
        {
            _context = context;
        }

        // GET: FoodDeliveryServices
        public async Task<IActionResult> Index(string? id) // id = selected service id
        {
            var services = await _context.FoodDeliveryServices
                .Include(s => s.Subscriptions)
                    .ThenInclude(sub => sub.Customer)
                .OrderBy(s => s.Id)
                .ToListAsync();

            var vm = new DealsViewModel
            {
                FoodDeliveryServices = services,
                SelectedServiceId = id
            };

            if (!string.IsNullOrEmpty(id))
            {
                // All customers subscribed to selected service
                vm.Customers = services
                    .Where(s => s.Id == id)
                    .SelectMany(s => s.Subscriptions.Select(sub => sub.Customer))
                    .Distinct()
                    .OrderBy(c => c.LastName)
                    .ToList();
            }

            return View(vm);
        }

        // GET: FoodDeliveryServices/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodDeliveryService = await _context.FoodDeliveryServices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodDeliveryService == null)
            {
                return NotFound();
            }

            return View(foodDeliveryService);
        }

        // GET: FoodDeliveryServices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FoodDeliveryServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Fee")] FoodDeliveryService foodDeliveryService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foodDeliveryService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(foodDeliveryService);
        }

        // GET: FoodDeliveryServices/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodDeliveryService = await _context.FoodDeliveryServices.FindAsync(id);
            if (foodDeliveryService == null)
            {
                return NotFound();
            }
            return View(foodDeliveryService);
        }

        // POST: FoodDeliveryServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,Fee")] FoodDeliveryService foodDeliveryService)
        {
            if (id != foodDeliveryService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodDeliveryService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodDeliveryServiceExists(foodDeliveryService.Id))
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
            return View(foodDeliveryService);
        }

        // GET: FoodDeliveryServices/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodDeliveryService = await _context.FoodDeliveryServices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foodDeliveryService == null)
            {
                return NotFound();
            }

            return View(foodDeliveryService);
        }

        // POST: FoodDeliveryServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var foodDeliveryService = await _context.FoodDeliveryServices.FindAsync(id);
            if (foodDeliveryService != null)
            {
                _context.FoodDeliveryServices.Remove(foodDeliveryService);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodDeliveryServiceExists(string id)
        {
            return _context.FoodDeliveryServices.Any(e => e.Id == id);
        }
    }
}
