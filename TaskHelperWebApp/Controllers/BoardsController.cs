using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskHelperWebApp;
using TaskHelperWebApp.Data;
using TaskHelperWebApp.ViewModels;

namespace TaskHelperWebApp.Controllers
{
    public class BoardsController : Controller
    {
        private readonly TasksContext _context;

        public BoardsController(TasksContext context)
        {
            _context = context;
        }

        // GET: Boards
        public async Task<IActionResult> Index()
        {
            var tasksContext = _context.Boards.Include(b => b.Project);
            return View(await tasksContext.ToListAsync());
        }

        // GET: Boards/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Boards == null)
            {
                return NotFound();
            }

            var boards = await _context.Boards
                .Include(b => b.Project)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (boards == null)
            {
                return NotFound();
            }

            return View(boards);
        }

        // GET: Boards/Create
        public IActionResult Create()
        {
            //todo dropdown not working correctly
            ViewData["ProjectID"] = new SelectList(_context.Projects.Select(p => new {
                Text = p.Name,
                Value = p.ID
            }));
            return View();
        }

        // POST: Boards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid ProjectID, string Name, String Description, string Color, DateTime CreatedDate, DateTime? ClosedDate)
        {
            var boards = new Boards {
                ID = Guid.NewGuid(),
                Name = Name,
                Description = Description,
                Color = Color,
                CreatedDate = CreatedDate,
                ProjectID = ProjectID,
                Project = _context.Projects.SingleOrDefault(p => p.ID == ProjectID) ?? throw new Exception("Project Could Not Be Found")
            };

            if (ModelState.IsValid)
            {
                boards.ID = Guid.NewGuid();
                _context.Add(boards);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectID"] = new SelectList(_context.Projects.Select(p => new {
                text = p.Name,
                value = p.ID
            }));
            return View(new CreateBoardsViewModel());
        }

        // GET: Boards/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Boards == null)
            {
                return NotFound();
            }

            var boards = await _context.Boards.FindAsync(id);
            if (boards == null)
            {
                return NotFound();
            }
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "ID", boards.ProjectID);
            return View(boards);
        }

        // POST: Boards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,ProjectID,Name,Description,Color,CreatedDate,ClosedDate")] Boards boards)
        {
            if (id != boards.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boards);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoardsExists(boards.ID))
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
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "ID", boards.ProjectID);
            return View(boards);
        }

        // GET: Boards/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Boards == null)
            {
                return NotFound();
            }

            var boards = await _context.Boards
                .Include(b => b.Project)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (boards == null)
            {
                return NotFound();
            }

            return View(boards);
        }

        // POST: Boards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Boards == null)
            {
                return Problem("Entity set 'TasksContext.Boards'  is null.");
            }
            var boards = await _context.Boards.FindAsync(id);
            if (boards != null)
            {
                _context.Boards.Remove(boards);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoardsExists(Guid id)
        {
          return (_context.Boards?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
