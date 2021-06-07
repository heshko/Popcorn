using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Popcorn.Data;
using Popcorn.Models;

namespace Popcorn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CinemaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CinemaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Cinema
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cinemas.Include(x => x.city).ToListAsync());
        }

        // GET: Admin/Cinema/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinemaModel = await _context.Cinemas
                .Include(x => x.city)
                .FirstOrDefaultAsync(m => m.id == id);
            if (cinemaModel == null)
            {
                return NotFound();
            }

            return View(cinemaModel);
        }

        // GET: Admin/Cinema/Create
        public IActionResult Create()
        {

            ViewBag.CityList = _context.Cities
            .ToList()
            .Select(x => new SelectListItem
            {
                Value = x.id.ToString(),
                Text = x.name
            });

            return View();
        }

        // POST: Admin/Cinema/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,cityId,street,zipcode")] CinemaModel cinemaModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cinemaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cinemaModel);
        }
      
        // GET: Admin/Cinema/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.CityList = _context.Cities
              .ToList()
              .Select(x => new SelectListItem
              {
                  Value = x.id.ToString(),
                  Text = x.name
              });

            if (id == null)
            {
                return NotFound();
            }

            var cinemaModel = await _context.Cinemas.Include(x => x.city).FirstOrDefaultAsync(x => x.id == id);
            if (cinemaModel == null)
            {
                return NotFound();
            }
            return View(cinemaModel);
        }

        // POST: Admin/Cinema/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,cityId,street,zipcode")] CinemaModel cinemaModel)
        {
            if (id != cinemaModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cinemaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CinemaModelExists(cinemaModel.id))
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
            return View(cinemaModel);
        }

        // GET: Admin/Cinema/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinemaModel = await _context.Cinemas
                 .Include(x => x.city).FirstOrDefaultAsync(m => m.id == id);
            if (cinemaModel == null)
            {
                return NotFound();
            }

            return View(cinemaModel);
        }

        // POST: Admin/Cinema/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinemaModel = await _context.Cinemas.FindAsync(id);
            _context.Cinemas.Remove(cinemaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CinemaModelExists(int id)
        {
            return _context.Cinemas.Any(e => e.id == id);
        }
    }
}
