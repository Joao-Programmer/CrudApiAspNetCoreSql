using CrudApiAspNetCoreSql.Data;
using CrudApiAspNetCoreSql.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApiAspNetCoreSql.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {            
            _context = context;
        }

        // ---------------------------- USANDO VIEW -----------------------------------
        // GET: Categories
        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["IdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Id_desc" : "";
            ViewData["NameSortParm"] = sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["CurrentFilter"] = searchString;

            var categories = from c in _context.Category
                        select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(c => c.CategoryName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Id_desc":
                    categories = categories.OrderByDescending(u => u.CategoryId);
                    break;
                case "Name":
                    categories = categories.OrderBy(u => u.CategoryName);
                    break;
                case "Name_desc":
                    categories = categories.OrderByDescending(u => u.CategoryName);
                    break;
                default:
                    categories = categories.OrderBy(u => u.CategoryId);
                    break;
            }

            return View(await categories.AsNoTracking().ToListAsync());
        }

        // ---------------------------- USANDO POSTMAN --------------------------------
        [AllowAnonymous]
        [HttpGet("/Categories/GetAllCategories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            return await _context.Category.ToListAsync();
        }

        // ---------------------------- USANDO VIEW -----------------------------------
        // GET: Categories/Details/5        
        [HttpGet("/Categories/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FirstOrDefaultAsync(m => m.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // ---------------------------- USANDO POSTMAN --------------------------------
        [AllowAnonymous]
        [HttpGet("/Categories/GetCategoryId/{id}")]
        public async Task<Category> GetCategoryId(int? id)
        {
            var category = await _context.Category.FirstOrDefaultAsync(m => m.CategoryId == id);

            return category;
        }

        // ---------------------------- USANDO POSTMAN --------------------------------
        [HttpGet("/Categories/GetCategoryMenuItems/{shortName}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoryMenuItems(string shortName)
        {         
            return await _context.Category.Where(c => c.CategoryShortName == shortName).Include(m => m.CategoryMenuItemsList).ToListAsync();
        }

        // GET: Categories/Create 
        [HttpGet("/Categories/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // ---------------------------- USANDO VIEW -----------------------------------
        // POST: Categories/Create
        [HttpPost("/Categories/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryShortName,CategoryName,CategorySpecialInstructions,CategoryImagePath,CategoryCreateDate")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }


        // GET: Categories/Edit/5       
        [HttpGet("/Categories/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/Categories/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryShortName,CategoryName,CategorySpecialInstructions,CategoryImagePath,CategoryCreateDate")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
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
            return View(category);
        }

        // GET: Categories/Delete/5        
        [HttpGet("/Categories/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        //[HttpPost, ActionName("Delete")]
        [HttpPost("/Categories/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Category.FindAsync(id);
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.CategoryId == id);
        }
    }
}
