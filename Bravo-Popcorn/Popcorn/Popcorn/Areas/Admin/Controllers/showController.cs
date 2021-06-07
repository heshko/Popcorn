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
    public class ShowController : Controller
    {
        private readonly ApplicationDbContext _context;

 
        public ShowController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Show
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Shows.Include(s => s.movie).Include(s => s.saloon).ThenInclude(x=>x.Cinema);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Show/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showModel = await _context.Shows
                .Include(s => s.cinema)
                .Include(s => s.movie)
                .Include(s => s.saloon)
                .FirstOrDefaultAsync(m => m.id == id);
            if (showModel == null)
            {
                return NotFound();
            }

            return View(showModel);
        }

        // GET: Admin/Show/Create
        public IActionResult Create()
        {
           
            ViewData["cinemaId"] = new SelectList(_context.Cinemas.Include(x => x.city).Include(x=>x.Saloons),"id", "name",_context.Cities,"city.name");
            ViewData["movieId"] = new SelectList(_context.Movies, "Id", "Title");
            ViewData["saloonId"] = new SelectList(_context.Saloons.Include(x=>x.Cinema), "Id", "Name",_context.Cinemas,"Cinema.name");
            return View();
        }

        // POST: Admin/Show/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,date,ExpireShowDate,Price,movieId,saloonId,cinemaId")] ShowModel showModel)
        {
            if (ModelState.IsValid)
            {
                var shows = await _context.Shows.ToListAsync();
                foreach(var show in shows)
                {
                    if(show.date == showModel.date)
                    {
                        return View("NotValidCreate");
                    }
                }
                var cinema = await _context.Cinemas.Include(x => x.Saloons).FirstOrDefaultAsync(x => x.id == showModel.cinemaId);
                foreach (var saloon in cinema.Saloons)
                {
                    if(saloon.Id == showModel.saloonId)
                    {
                        _context.Add(showModel);
                        await _context.SaveChangesAsync();
                      
                        return RedirectToAction(nameof(Index));
                    
                    }
                }
              
            }
   

          
            return View("NotValidCreate");
        }

        // GET: Admin/Show/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showModel = await _context.Shows.FindAsync(id);
            if (showModel == null)
            {
                return NotFound();
            }
            ViewData["cinemaId"] = new SelectList(_context.Cinemas.Include(x => x.city), "id", "name", _context.Cities, "city.name");
            ViewData["movieId"] = new SelectList(_context.Movies, "Id", "Title", showModel.movieId);
            ViewData["saloonId"] = new SelectList(_context.Saloons.Include(x => x.Cinema), "Id", "Name", _context.Cinemas, "Cinema.name");
            return View(showModel);
        }

        // POST: Admin/Show/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,date,ExpireShowDate,Price,movieId,saloonId,cinemaId")] ShowModel showModel)
        {
            if (id != showModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var cinema = await _context.Cinemas.Include(x => x.Saloons).FirstOrDefaultAsync(x => x.id == showModel.cinemaId);
                    foreach (var saloon in cinema.Saloons)
                    {
                        if (saloon.Id == showModel.saloonId)
                        {
                            _context.Update(showModel);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                    }
                    return View("NotValidEdit");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowModelExists(showModel.id))
                    {
                        return View("NotValidEdit");
                    }
                    else
                    {
                        throw;
                    }
                }

            }
        
            return View("NotValidEdit");
        }

        // GET: Admin/Show/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showModel = await _context.Shows
                .Include(s => s.cinema)
                .Include(s => s.movie)
                .Include(s => s.saloon)
                .FirstOrDefaultAsync(m => m.id == id);
            if (showModel == null)
            {
                return NotFound();
            }

            return View(showModel);
        }

        // POST: Admin/Show/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var showModel = await _context.Shows.FindAsync(id);
            _context.Shows.Remove(showModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShowModelExists(int id)
        {
            return _context.Shows.Any(e => e.id == id);
        }
    }
}
