using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Storage.Internal;
using SPF.Data;

namespace SPF.Controllers
{
    public class Catalog : Controller
    {

        private readonly ApplicationDbContext _context;

        public Catalog(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string searchName, string[] Type)
        {
            var item = from i in _context.Items
                       select i;
            if (Type.Length > 0)
            {
                item = item.Where(o => o.Id == -1);
                foreach (var i in Type)
                {
                    var type = from items in _context.Items
                               join itemTypes in _context.ItemTypes on items.Id equals itemTypes.ItemId
                               join types in _context.Types on itemTypes.TypeId equals types.Id
                               where (types.Name == i)
                               select items;
                    item = item.Union(type);
                }
                var test = from items in _context.Items
                           select items;
            }
            if (!String.IsNullOrEmpty(searchName))
            {
                item = item.Where(o => o.Name.Contains(searchName));
            }
            item = item.OrderByDescending(o => o.Top).ThenBy(o => o.Id).Take(30);
            return View(item);
        }
        public async Task<IActionResult> Item(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.ItemsSpecification
                .Include(x => x.Item)
                .Where(o => o.Item.Id == id).FirstAsync();
            if (item == null)
            {
                return NotFound();
            }
            item.Item.Top += 1;
            _context.SaveChangesAsync();
            return View(item);
        }
    }
}
