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
    public class StudentUnit_RelationshipController : Controller
    {
        private readonly ExaminationContext _context;

        public StudentUnit_RelationshipController(ExaminationContext context)
        {
            _context = context;
        }

        // GET: StudentUnit_Relationship
        public async Task<IActionResult> Index()
        {
            var examinationContext = _context.StudentUnitRelationships.Include(s => s.StudentRelation).Include(s => s.UnitRelation);
            return View(await examinationContext.ToListAsync());
        }

        // GET: StudentUnit_Relationship/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentUnit_Relationship = await _context.StudentUnitRelationships
                .Include(s => s.StudentRelation)
                .Include(s => s.UnitRelation)
                .FirstOrDefaultAsync(m => m.RelationId == id);
            if (studentUnit_Relationship == null)
            {
                return NotFound();
            }

            return View(studentUnit_Relationship);
        }

        // GET: StudentUnit_Relationship/Create
        public IActionResult Create()
        {
            ViewData["UniversityStudentId"] = new SelectList(_context.Students, "UniversityStudentId", "UniversityStudentId");
            ViewData["Code"] = new SelectList(_context.Units, "Code", "Code");
            return View();
        }

        // POST: StudentUnit_Relationship/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RelationId,UniversityStudentId,Code")] Enrollment studentUnit_Relationship)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentUnit_Relationship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UniversityStudentId"] = new SelectList(_context.Students, "UniversityStudentId", "UniversityStudentId", studentUnit_Relationship.UniversityStudentId);
            ViewData["Code"] = new SelectList(_context.Units, "Code", "Code", studentUnit_Relationship.Code);
            return View(studentUnit_Relationship);
        }

        // GET: StudentUnit_Relationship/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentUnit_Relationship = await _context.StudentUnitRelationships.FindAsync(id);
            if (studentUnit_Relationship == null)
            {
                return NotFound();
            }
            ViewData["UniversityStudentId"] = new SelectList(_context.Students, "UniversityStudentId", "UniversityStudentId", studentUnit_Relationship.UniversityStudentId);
            ViewData["Code"] = new SelectList(_context.Units, "Code", "Code", studentUnit_Relationship.Code);
            return View(studentUnit_Relationship);
        }

        // POST: StudentUnit_Relationship/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RelationId,UniversityStudentId,Code")] Enrollment studentUnit_Relationship)
        {
            if (id != studentUnit_Relationship.RelationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentUnit_Relationship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentUnit_RelationshipExists(studentUnit_Relationship.RelationId))
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
            ViewData["UniversityStudentId"] = new SelectList(_context.Students, "UniversityStudentId", "UniversityStudentId", studentUnit_Relationship.UniversityStudentId);
            ViewData["Code"] = new SelectList(_context.Units, "Code", "Code", studentUnit_Relationship.Code);
            return View(studentUnit_Relationship);
        }

        // GET: StudentUnit_Relationship/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentUnit_Relationship = await _context.StudentUnitRelationships
                .Include(s => s.StudentRelation)
                .Include(s => s.UnitRelation)
                .FirstOrDefaultAsync(m => m.RelationId == id);
            if (studentUnit_Relationship == null)
            {
                return NotFound();
            }

            return View(studentUnit_Relationship);
        }

        // POST: StudentUnit_Relationship/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentUnit_Relationship = await _context.StudentUnitRelationships.FindAsync(id);
            _context.StudentUnitRelationships.Remove(studentUnit_Relationship);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentUnit_RelationshipExists(int id)
        {
            return _context.StudentUnitRelationships.Any(e => e.RelationId == id);
        }
    }
}
