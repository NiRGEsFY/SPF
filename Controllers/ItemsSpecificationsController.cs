using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SPF.Data;
using SPF.Models.Items;

namespace SPF.Controllers
{
    [Authorize]
    [Authorize(Policy = "Moder")]
    public class ItemsSpecificationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemsSpecificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ItemsSpecifications
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ItemsSpecification.Include(i => i.Item);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ItemsSpecifications/Create
        public IActionResult Create(int id)
        {
            if (_context.Items.Where(o => o.Id == id).FirstOrDefault() != null)
            {
                var item = _context.Items.Where(o => o.Id == id).FirstOrDefault();
                var itemsSpecification = new ItemsSpecification();
                var list = from itemType in _context.ItemTypes
                           join type in _context.Types on itemType.TypeId equals type.Id
                           where itemType.ItemId == id
                           select new { TypeName = type.Name };
                switch (list.FirstOrDefault().TypeName)
                {
                    case ("Ботинки утепленные"):
                    case ("Обувь войлочная"):
                    case ("Галоши"):
                    case ("Полуботинки"):
                    case ("Тапочки"):
                    case ("Ботинки"):
                        itemsSpecification.Name = "Ботинки";
                        break;
                    case ("Сапоги"):
                    case ("Сапоги утепленные"):
                    case ("Чулки утепляющие"):
                        itemsSpecification.Name = "Сапоги";
                        break;
                    case ("Перчатки"):
                    case ("Рукавицы"):
                    case ("Краги"):
                        itemsSpecification.Name = "Перчатки";
                        break;
                    case ("Дыхания"):
                    case ("Зрения"):
                    case ("Головы"):
                        itemsSpecification.Name = "СИЗ";
                        break;
                    case ("Костюм"):
                    case ("Халат"):
                    case ("Трикотаж"):
                    case ("Куртка"):
                    case ("Жилет"):
                    case ("Брюки"):
                    case ("Полукомбинезоны"):
                    case ("Костюмы сварщика утепленные"):
                    case ("Влагозащитная"):
                    case ("Спецодежда от кислот и агрессивной среды"):
                    case ("Спецодежда от высоких температур"):
                    case ("Фартуки"):
                    case ("Сигнальные жилеты"):
                        itemsSpecification.Name = "СпецОдежда";
                        break;
                }
                itemsSpecification.ItemId = id;
                return View(itemsSpecification);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemId,Name,Size,Material,Model,Color,ProtectiveClass,Weight,Character,MaterialInside,Growth,MatingClass,Thread,Height")] ItemsSpecification itemsSpecification)
        {
            var list = from items in _context.ItemsSpecification
                       select new { Name = items.Name };
            ViewData["TypeName"] = new SelectList(list.Distinct(), "Name", "Name");
            itemsSpecification.Item = _context.Items.Where(o => o.Id == itemsSpecification.ItemId).FirstOrDefault();
            ModelState.Remove("Item");
            if (ModelState.IsValid)
            {
                _context.Add(itemsSpecification);
                await _context.SaveChangesAsync();
                return Redirect("/Items/Index");
            }
            return View(itemsSpecification);
        }

        // GET: ItemsSpecifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ItemsSpecification == null)
            {
                return NotFound();
            }

            var itemsSpecification = _context.ItemsSpecification.Include(x => x.Item).Where(x => x.Id == id).FirstOrDefault();
            if (itemsSpecification == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "HighDescription", itemsSpecification.ItemId);
            return View(itemsSpecification);
        }

        // POST: ItemsSpecifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ItemId,Name,Size,Material,Model,Color,ProtectiveClass,Weight,Character,MaterialInside,Growth,MatingClass,Thread,Height")] ItemsSpecification itemsSpecification)
        {
            if (id != itemsSpecification.Id)
            {
                return NotFound();
            }
            ModelState.Remove("Id");
            ModelState.Remove("ItemId");
            ModelState.Remove("Name");
            ModelState.Remove("Item");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemsSpecification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemsSpecificationExists(itemsSpecification.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("/Items/Index");
            }
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "HighDescription", itemsSpecification.ItemId);
            return View(itemsSpecification);
        }

        private bool ItemsSpecificationExists(int id)
        {
          return (_context.ItemsSpecification?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
