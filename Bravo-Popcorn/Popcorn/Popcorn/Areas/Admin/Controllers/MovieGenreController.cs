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
    public class MovieGenreController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieGenreController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/MovieGenre
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MovieGenre.Include(m => m.GenreModel).Include(m => m.MovieModel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/MovieGenre/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieGenreModel = await _context.MovieGenre
                .Include(m => m.GenreModel)
                .Include(m => m.MovieModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieGenreModel == null)
            {
                return NotFound();
            }

            return View(movieGenreModel);
        }

        // GET: Admin/MovieGenre/Create
        public IActionResult Create()
        {
            ViewData["GenreModelId"] = new SelectList(_context.Genre, "Id", "Description");
            ViewData["MovieModelId"] = new SelectList(_context.Movies, "Id", "Title");
            return View();
        }

        // POST: Admin/MovieGenre/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieModelId,GenreModelId")] MovieGenreModel movieGenreModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieGenreModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreModelId"] = new SelectList(_context.Genre, "Id", "Description", movieGenreModel.GenreModelId);
            ViewData["MovieModelId"] = new SelectList(_context.Movies, "Id", "Title", movieGenreModel.MovieModelId);
            return View(movieGenreModel);
        }

        // GET: Admin/MovieGenre/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieGenreModel = await _context.MovieGenre.FindAsync(id);
            if (movieGenreModel == null)
            {
                return NotFound();
            }
            ViewData["GenreModelId"] = new SelectList(_context.Genre, "Id", "Description", movieGenreModel.GenreModelId);
            ViewData["MovieModelId"] = new SelectList(_context.Movies, "Id", "Title", movieGenreModel.MovieModelId);
            return View(movieGenreModel);
        }

        // POST: Admin/MovieGenre/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieModelId,GenreModelId")] MovieGenreModel movieGenreModel)
        {
            if (id != movieGenreModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieGenreModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieGenreModelExists(movieGenreModel.Id))
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
            ViewData["GenreModelId"] = new SelectList(_context.Genre, "Id", "Description", movieGenreModel.GenreModelId);
            ViewData["MovieModelId"] = new SelectList(_context.Movies, "Id", "Title", movieGenreModel.MovieModelId);
            return View(movieGenreModel);
        }

        // GET: Admin/MovieGenre/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieGenreModel = await _context.MovieGenre
                .Include(m => m.GenreModel)
                .Include(m => m.MovieModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieGenreModel == null)
            {
                return NotFound();
            }

            return View(movieGenreModel);
        }

        // POST: Admin/MovieGenre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieGenreModel = await _context.MovieGenre.FindAsync(id);
            _context.MovieGenre.Remove(movieGenreModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieGenreModelExists(int id)
        {
            return _context.MovieGenre.Any(e => e.Id == id);
        }
    }
}
