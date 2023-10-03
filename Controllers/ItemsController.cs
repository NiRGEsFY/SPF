using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SPF.Data;
using SPF.Models.Items;
using System;

namespace SPF.Controllers
{
    [Authorize]
    [Authorize(Policy = "Moder")]
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string _folderWay;

        public ItemsController(ApplicationDbContext context, IConfiguration configRoot)
        {
            _context = context;
            _folderWay = configRoot["FolderString:ImgWay"] ?? throw new InvalidOperationException("Connection string 'FolderString:ImgWay' not found.");
        }

        public async Task<IActionResult> Index(string searchId,string searchName)
        {
            var item = from i in _context.Items
                       select i;
            if (!String.IsNullOrEmpty(searchId))
            {
                item = item.Where(o => o.Id == int.Parse(searchId));
            }
            if (!String.IsNullOrEmpty(searchName))
            {
                item = item.Where(o => o.Name.Contains(searchName));
            }
            return View(item.Take(50));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }
            var item = _context.ItemsSpecification
                .Include(x => x.Item)
                .Where(o => o.Item.Id == id).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        public IActionResult Create()
        {
            ViewData["TypeName"] = new SelectList(_context.Types, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int TypeName, [Bind("Id,Name,LowDescription,HighDescription,Price,PriceLow,ImgUrl,ImgListUrl")] Item item, IFormFile UploadImg = null)
        {
            ViewData["TypeName"] = new SelectList(_context.Types, "Id", "Name");
            ModelState.Remove("PriceLow");
            if (ModelState.IsValid && UploadImg != null)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                var itemContext = _context.Items.Where(o => o.HighDescription == item.HighDescription && o.LowDescription == item.LowDescription).FirstOrDefault();
                var itemId = itemContext.Id;
                string smallWay = @$"/img/item/{itemContext.Id}.{UploadImg.FileName.Split('.').Last()}";
                string fullWay = _folderWay + smallWay;
                itemContext.ImgUrl = smallWay;
                itemContext.ImgListUrl = fullWay;
                FileStream stream = new FileStream(fullWay, FileMode.OpenOrCreate);
                UploadImg.CopyTo(stream);
                stream.Close();
                _context.ItemTypes.Add(new ItemType { ItemId = itemId, TypeId = TypeName });
                _context.SaveChanges();
                if (itemId != null)
                {
                    return Redirect($"/ItemsSpecifications/Create?id={itemId}");
                }
                else
                {
                    _context.Items.Remove(item);
                    await _context.SaveChangesAsync();
                }
            }
            return View(item);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LowDescription,HighDescription,Price,PriceLow,ImgUrl,ImgListUrl")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
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
            return View(item);
        }
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }
        [Authorize(Policy = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Items'  is null.");
            }
            var item = await _context.Items.FindAsync(id);
            var itemSpec = await _context.ItemsSpecification.Where(o => o.ItemId == id).FirstOrDefaultAsync();
            if (itemSpec != null)
            {
                _context.ItemsSpecification.Remove(itemSpec);
            }
            if (item != null)
            {
                _context.Items.Remove(item);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
          return (_context.Items?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
