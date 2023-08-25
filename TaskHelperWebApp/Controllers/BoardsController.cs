using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;
using TaskHelperWebApp;
using TaskHelperWebApp.Data;
using TaskHelperWebApp.ViewModels;

namespace TaskHelperWebApp.Controllers
    //refactor to use services!!!!
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
            var activeBoards = await _context.Boards.Select(b => new BoardsViewModel
            {
                ID = b.ID,
                Name = b.Name,
                ProjectID = b.ProjectID,
                Description = b.Description,
                Color = b.Color,
                CreatedDate = b.CreatedDate,
                ClosedDate = b.ClosedDate,
                ProjectName = b.Project.Name,
            }).ToListAsync();
            return View(activeBoards);
        }

        // GET: Boards/Details/id
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Boards == null)
            {
                return NotFound();
            }

           var currentBoard = await _context.Boards.AsNoTracking().Include(b => b.Project)
                .SingleOrDefaultAsync(b => b.ID == id)
                ?? throw new Exception("Could not find current board");

            var boardViewModel = new BoardsViewModel
            {
                ID = currentBoard.ID,
                Name = currentBoard.Name,
                ProjectID = currentBoard.ProjectID,
                Description = currentBoard.Description,
                Color = currentBoard.Color,
                CreatedDate = currentBoard.CreatedDate,
                ClosedDate = currentBoard.ClosedDate,
                ProjectName = currentBoard.Project.Name,
            };
            return View(boardViewModel);
        }

        // GET: Boards/Create
        public async Task<IActionResult> Create()
        {
            var boardViewModel = new BoardsViewModel
            {
                PotentialProjects = await _context.Projects.AsNoTracking()
                .Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.ID.ToString()
                }).ToListAsync()
            };
            return View(boardViewModel);
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid ProjectID, string Name, String Description, string Color)
        {
            try
            {
                var boards = new Boards
                {
                    ID = Guid.NewGuid(),
                    Name = Name,
                    Description = Description,
                    Color = Color,
                    CreatedDate = DateTime.UtcNow,
                    ProjectID = ProjectID,
                    Project = await _context.Projects.SingleOrDefaultAsync(p => p.ID == ProjectID) ?? throw new Exception("Project Could Not Be Found")
                };

                _context.Add(boards);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(new BoardsViewModel());
            }
        }

        // GET: Boards/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Boards == null)
            {
                return NotFound();
            }

            var board = await _context.Boards.Include(b => b.Project).SingleOrDefaultAsync(b => b.ID == id);
            if (board == null)
            {
                return NotFound();
            }
            
            var potentialProjects = await _context.Projects.Where(p => p.ClosedDate == null)
                .Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.ID.ToString()
                }).ToListAsync();

            var boardsViewModel = new BoardsViewModel
            {
                ID = id,
                ProjectID = board.ProjectID,
                Name = board.Name,
                Description = board.Description,
                Color = board.Color,
                CreatedDate = board.CreatedDate,
                ClosedDate = board.ClosedDate,
                ProjectName = board.Project.Name,
                PotentialProjects = potentialProjects
            };
            return View(boardsViewModel);
        }

        // POST: Boards/Edit/5
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
