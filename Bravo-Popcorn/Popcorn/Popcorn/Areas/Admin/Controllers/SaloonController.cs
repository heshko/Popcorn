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
    public class SaloonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SaloonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/SaloonModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Saloons.Include(x => x.Cinema).ToListAsync());
        }

        // GET: Admin/SaloonModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saloonModel = await _context.Saloons
                .Include(x => x.Cinema)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saloonModel == null)
            {
                return NotFound();
            }

            return View(saloonModel);
        }

        // GET: Admin/SaloonModels/Create
        public IActionResult Create()
        {
            ViewBag.CinemaList = _context.Cinemas
                .ToList()
                .Select(x => new SelectListItem 
                {
                    Value = x.id.ToString(),
                    Text = x.name
                });

            return View();
        }

        // POST: Admin/SaloonModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Rows,Columns,CinemaId")] SaloonModel saloonModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saloonModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(saloonModel);
        }

        // GET: Admin/SaloonModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.CinemaList = _context.Cinemas
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

            var saloonModel = await _context.Saloons.Include(x => x.Cinema).FirstOrDefaultAsync(x => x.Id == id);
            if (saloonModel == null)
            {
                return NotFound();
            }
            return View(saloonModel);
        }

        // POST: Admin/SaloonModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Rows,Columns,CinemaId")] SaloonModel saloonModel)
        {
            if (id != saloonModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saloonModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaloonModelExists(saloonModel.Id))
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
            return View(saloonModel);
        }

        // GET: Admin/SaloonModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saloonModel = await _context.Saloons
                .Include(x => x.Cinema)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (saloonModel == null)
            {
                return NotFound();
            }

            return View(saloonModel);
        }

        // POST: Admin/SaloonModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saloonModel = await _context.Saloons.FindAsync(id);
            _context.Saloons.Remove(saloonModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaloonModelExists(int id)
        {
            return _context.Saloons.Any(e => e.Id == id);
        }
    }
}
