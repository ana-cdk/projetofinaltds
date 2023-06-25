using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ecommerce.Models;
using ecommerce.Data;
using Microsoft.AspNetCore.Hosting;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ecommerce.Pages.Produto
{
    public class Alterar : PageModel
    {        
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public ProdutoModel Produto { get; set; }
        public string CaminhoImagem { get; set; }

        [BindProperty]
        [Display(Name = "Imagem do Produto")]        
        public IFormFile ImagemProduto { get; set; }

        public Alterar(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;          
        }        

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Produto = await _context.Produto.FirstOrDefaultAsync(m => m.IdProduto == id && m.Estoque >= 0);

            if (Produto == null)
            {
                return NotFound();
            }  

            CaminhoImagem = $"~/img/produto/{Produto.IdProduto:D6}.jpg";          

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("aquiiiiiiiiiiiiiiiiiiiiii");
                return Page();
            }

            _context.Attach(Produto).State = EntityState.Modified;       

            try
            {
                await _context.SaveChangesAsync();
                if (ImagemProduto != null)
                    await AppUtils.ProcessarArquivoDeImagem(Produto.IdProduto,
                        ImagemProduto, _webHostEnvironment);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExisteProduto(Produto.IdProduto))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }    

            return RedirectToPage("./Listar");
        }

        private bool ExisteProduto(int id)
        {
            return _context.Produto.Any(e => e.IdProduto == id);
        }
    }
}