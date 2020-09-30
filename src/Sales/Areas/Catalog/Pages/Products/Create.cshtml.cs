using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sales.Data;

namespace Sales.Areas.Catalog.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly SalesDbContext _context;


        [BindProperty]
        public CreateCommand Command { get; set; }

        public CreateModel(SalesDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var product = new Product
            {
                Name = Command.Name,
                Description = Command.Description
            };

            _context.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }

    public class CreateCommand
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
