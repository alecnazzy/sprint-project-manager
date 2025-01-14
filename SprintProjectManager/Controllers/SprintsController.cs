using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SprintProjectManager.Data;
using SprintProjectManager.Models;

namespace SprintProjectManager.Controllers
{
    public class SprintsController : Controller
    {
        private readonly SprintProjectManagerContext _context;

        public SprintsController(SprintProjectManagerContext context)
        {
            _context = context;
        }

        // GET: Sprints
        public async Task<IActionResult> Index(string sprintStatus, string searchString)
        {
            if (_context.Sprint == null)
            {
                return Problem("Entity set 'SprintProjectManagerContext.Sprint'  is null.");
            }

            // search status
            IQueryable<string> statusQuery = from s in _context.Sprint
                                             orderby s.Status
                                             select s.Status;
            var sprints = from s in _context.Sprint
                         select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                sprints = sprints.Where(s => s.Name!.ToUpper().Contains(searchString.ToUpper()));
            }
            if (!string.IsNullOrEmpty(sprintStatus))
            {
                sprints = sprints.Where(x => x.Status == sprintStatus);
            }

            var sprintStatusVM = new SprintStatusViewModel
            {
                Statuses = new SelectList(await statusQuery.Distinct().ToListAsync()),
                Sprints = await sprints.ToListAsync()
            };

            return View(sprintStatusVM);
        }


        // GET: Sprints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sprint = await _context.Sprint
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sprint == null)
            {
                return NotFound();
            }

            return View(sprint);
        }

        // GET: Sprints/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sprints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartDate,EndDate,Goal,Status")] Sprint sprint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sprint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sprint);
        }

        // GET: Sprints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sprint = await _context.Sprint.FindAsync(id);
            if (sprint == null)
            {
                return NotFound();
            }
            return View(sprint);
        }

        // POST: Sprints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartDate,EndDate,Goal,Status")] Sprint sprint)
        {
            if (id != sprint.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sprint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SprintExists(sprint.Id))
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
            return View(sprint);
        }

        // GET: Sprints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sprint = await _context.Sprint
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sprint == null)
            {
                return NotFound();
            }

            return View(sprint);
        }

        // POST: Sprints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sprint = await _context.Sprint.FindAsync(id);
            if (sprint != null)
            {
                _context.Sprint.Remove(sprint);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SprintExists(int id)
        {
            return _context.Sprint.Any(e => e.Id == id);
        }
    }
}
