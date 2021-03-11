using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudApiAspNetCoreSql.Data;
using CrudApiAspNetCoreSql.Models;

namespace CrudApiAspNetCoreSql.Controllers
{
    [Route("api/[controller]")]
    public class MenuItemsController : Controller
    {
        private readonly AppDbContext _context;

        public MenuItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MenuItems
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.MenuItem.Include(m => m.MenuItemCategory);
            return View(await appDbContext.ToListAsync());
        }

        // GET: MenuItems/Details/5
        [HttpGet("/MenuItems/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItem
                .Include(m => m.MenuItemCategory)
                .FirstOrDefaultAsync(m => m.MenuItemID == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        // GET: MenuItems/Create
        [HttpGet("/MenuItems/Create")]
        public IActionResult Create()
        {
            ViewData["MenuItemCategoryIdFk"] = new SelectList(_context.Category, "CategoryId", "CategoryName");
            return View();
        }

        // POST: MenuItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/MenuItems/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MenuItemID,MenuItemDescription,MenuItemLargePortionName,MenuItemName,MenuItemPriceLarge,MenuItemPriceSmall,MenuItemShortName,MenuItemSmallPortionName,MenuItemCategoryIdFk")] MenuItem menuItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuItemCategoryIdFk"] = new SelectList(_context.Category, "CategoryId", "CategoryName", menuItem.MenuItemCategoryIdFk);
            return View(menuItem);
        }

        // GET: MenuItems/Edit/5
        [HttpGet("/MenuItems/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItem.FindAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            ViewData["MenuItemCategoryIdFk"] = new SelectList(_context.Category, "CategoryId", "CategoryName", menuItem.MenuItemCategoryIdFk);
            return View(menuItem);
        }

        // POST: MenuItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("/MenuItems/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MenuItemID,MenuItemDescription,MenuItemLargePortionName,MenuItemName,MenuItemPriceLarge,MenuItemPriceSmall,MenuItemShortName,MenuItemSmallPortionName,MenuItemCategoryIdFk")] MenuItem menuItem)
        {
            if (id != menuItem.MenuItemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuItemExists(menuItem.MenuItemID))
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
            ViewData["MenuItemCategoryIdFk"] = new SelectList(_context.Category, "CategoryId", "CategoryName", menuItem.MenuItemCategoryIdFk);
            return View(menuItem);
        }

        // GET: MenuItems/Delete/5
        [HttpGet("/MenuItems/Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItem
                .Include(m => m.MenuItemCategory)
                .FirstOrDefaultAsync(m => m.MenuItemID == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        // POST: MenuItems/Delete/5
        [HttpPost("/MenuItems/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuItem = await _context.MenuItem.FindAsync(id);
            _context.MenuItem.Remove(menuItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuItemExists(int id)
        {
            return _context.MenuItem.Any(e => e.MenuItemID == id);
        }
    }
}
