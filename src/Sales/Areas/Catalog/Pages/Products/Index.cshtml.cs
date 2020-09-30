using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sales.Data;

namespace Sales.Areas.Catalog.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly SalesDbContext _context;

        public List<Product> Products { get; set; }

        public IndexModel(SalesDbContext context)
        {
            _context = context;
        }

        public async Task OnGet()
        {
            Products = await _context.Products.ToListAsync();
        }
    }
}
