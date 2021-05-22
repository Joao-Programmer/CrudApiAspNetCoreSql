using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudApiAspNetCoreSql.Data;
using CrudApiAspNetCoreSql.Models;
using Microsoft.AspNetCore.Authorization;

namespace CrudApiAspNetCoreSql.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class MenuItemsController : Controller
    {
        private readonly AppDbContext _context;

        public MenuItemsController(AppDbContext context)
        {
            _context = context;
        }

        // ---------------------------- USANDO VIEW -----------------------------------
        // GET: MenuItems
        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["IdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Id_desc" : "";
            ViewData["NameSortParm"] = sortOrder == "Name" ? "Name_desc" : "Name";

            var menuItems = from m in _context.MenuItem.Include(mc => mc.MenuItemCategory)
                             select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                menuItems = menuItems.Where(m => m.MenuItemName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Id_desc":
                    menuItems = menuItems.OrderByDescending(u => u.MenuItemID);
                    break;
                case "Name":
                    menuItems = menuItems.OrderBy(u => u.MenuItemName);
                    break;
                case "Name_desc":
                    menuItems = menuItems.OrderByDescending(u => u.MenuItemName);
                    break;
                default:
                    menuItems = menuItems.OrderBy(u => u.MenuItemID);
                    break;
            }

            return View(await menuItems.AsNoTracking().ToListAsync());

            //var appDbContext = _context.MenuItem.Include(m => m.MenuItemCategory);
        }

        // ---------------------------- USANDO POSTMAN --------------------------------
        [HttpGet("/MenuItems/GetAllMenuItems")]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetAllMenuItems()
        {
            return await _context.MenuItem.ToListAsync();
        }

        // ---------------------------- USANDO VIEW -----------------------------------
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

        // ---------------------------- USANDO POSTMAN --------------------------------
        [HttpGet("/MenuItems/GetMenuItemId/{id}")]
        public async Task<MenuItem> GetMenuItemId(int? id)
        {            
            var menuItem = await _context.MenuItem.FirstOrDefaultAsync(m => m.MenuItemID == id);

            return menuItem;
        }

        // ---------------------------- USANDO POSTMAN --------------------------------
        [HttpGet("/MenuItems/GetMenuItemCategory/{shortName}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetMenuItemCategory(string shortName)
        {
            return await _context.MenuItem.Where(m => m.MenuItemCategory.CategoryShortName == shortName).Include(mc => mc.MenuItemCategory).ToListAsync(); 
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
        public async Task<IActionResult> Create([Bind("MenuItemID,MenuItemShortName,MenuItemName,MenuItemDescription,MenuItemSmallPortionName,MenuItemLargePortionName,MenuItemPriceSmall,MenuItemPriceLarge,MenuItemCategoryIdFk")] MenuItem menuItem)
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
