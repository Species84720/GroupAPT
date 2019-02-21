using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Group_APT.Models;

namespace Group_APT.Controllers
{
    public class ExaminationsController : Controller
    {
        private readonly ExaminationContext _context;

        public ExaminationsController(ExaminationContext context)
        {
            _context = context;
        }

        // GET: Examinations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Examinations.ToListAsync());
        }

        // GET: Examinations/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examination = await _context.Examinations
                .FirstOrDefaultAsync(m => m.Paper_Id == id);
            if (examination == null)
            {
                return NotFound();
            }

            return View(examination);
        }

        // GET: Examinations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Examinations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Paper_Id,Time,Location_Id,QuestionAmount,SelectionType")] Examination examination)
        {
            if (ModelState.IsValid)
            {
                _context.Add(examination);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(examination);
        }

        // GET: Examinations/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examination = await _context.Examinations.FindAsync(id);
            if (examination == null)
            {
                return NotFound();
            }
            return View(examination);
        }

        // POST: Examinations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Paper_Id,Time,Location_Id,QuestionAmount,SelectionType")] Examination examination)
        {
            if (id != examination.Paper_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examination);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExaminationExists(examination.Paper_Id))
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
            return View(examination);
        }

        // GET: Examinations/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examination = await _context.Examinations
                .FirstOrDefaultAsync(m => m.Paper_Id == id);
            if (examination == null)
            {
                return NotFound();
            }

            return View(examination);
        }

        // POST: Examinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var examination = await _context.Examinations.FindAsync(id);
            _context.Examinations.Remove(examination);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExaminationExists(string id)
        {
            return _context.Examinations.Any(e => e.Paper_Id == id);
        }
    }
}
