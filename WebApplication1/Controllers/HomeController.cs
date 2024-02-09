using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using WebApplication1.DataModels;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly TestContext _context;

        public HomeController(TestContext context)
        {
            _context = context;
        }

        // GET: Home
        public async Task<IActionResult> Index()
        {
              return _context.Students != null ? 
                          View(await _context.Students.ToListAsync()) :
                          Problem("Entity set 'TestContext.Students'  is null.");
        }

        // GET: Home/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }


/*        mycreate fn
*/
        public IActionResult mycreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> mycreate(Student std)
        {
            if (ModelState.IsValid)
            {
                await _context.Students.AddAsync(std);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
                
            }
            return View();
        }



        /*mydetail fn*/


        public async Task<IActionResult> mydetails(int id)
        {
            var stdData = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            return View(stdData);
        }

        /*public async Task<IActionResult> form()
        {
            
            return View();
        }*/
        public IActionResult LoginDet()
        {
            return View(); 
        }
        [HttpPost]
        public async Task<IActionResult> LoginDet(Logindet log)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("Incorrect", "Give appropriate username and password");
                return View(log);
            }
            
            var user = await _context.Logindets.FirstOrDefaultAsync(s => s.Username == log.Username);
            if(user == null)
            {
                ModelState.AddModelError("NotFound", "User not found, please register first..");
                return View(log);
            }
            if (user.Password == log.Password)
            {
                return RedirectToAction("Index", "Home");
                
            }
            else
            {
                ModelState.AddModelError("wrong", "wrong password");
                return View(log);
            }
                
        }







        // GET: Home/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            return View(student);
        }

        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'TestContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(long id)
        {
          return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
