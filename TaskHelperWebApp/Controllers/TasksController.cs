using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskHelperWebApp;
using TaskHelperWebApp.Data;

namespace TaskHelperWebApp.Controllers
{
    public class TasksController : Controller
    {
        private readonly TasksContext _context;

        public TasksController(TasksContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var tasksContext = _context.Tasks.Include(t => t.Board);
            return View(await tasksContext.ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .Include(t => t.Board)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            ViewData["BoardID"] = new SelectList(_context.Set<Boards>(), "ID", "ID");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,BoardID,UserID,Name,Description,IsSubtaskOf,IsComplete,CreatedDate,CompletedDate")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                tasks.ID = Guid.NewGuid();
                _context.Add(tasks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BoardID"] = new SelectList(_context.Set<Boards>(), "ID", "ID", tasks.BoardID);
            return View(tasks);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks == null)
            {
                return NotFound();
            }
            ViewData["BoardID"] = new SelectList(_context.Set<Boards>(), "ID", "ID", tasks.BoardID);
            return View(tasks);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,BoardID,UserID,Name,Description,IsSubtaskOf,IsComplete,CreatedDate,CompletedDate")] Tasks tasks)
        {
            if (id != tasks.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tasks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TasksExists(tasks.ID))
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
            ViewData["BoardID"] = new SelectList(_context.Set<Boards>(), "ID", "ID", tasks.BoardID);
            return View(tasks);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .Include(t => t.Board)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Tasks == null)
            {
                return Problem("Entity set 'TasksContext.Tasks'  is null.");
            }
            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks != null)
            {
                _context.Tasks.Remove(tasks);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TasksExists(Guid id)
        {
          return (_context.Tasks?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
