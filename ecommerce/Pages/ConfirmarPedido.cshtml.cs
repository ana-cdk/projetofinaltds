using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ecommerce.Data;
using ecommerce.Models;

namespace ecommerce.Pages
{
    [Authorize(Roles = "cliente")]
    public class ConfirmarPedido : PageModel
    {
        private AppDbContext _context;
        public string COOKIE_NAME
        {
            get { return ".AspNetCore.CartId"; }
        }

        public PedidoModel Pedido { get; set; }
        public ClienteModel Cliente { get; set; }

        public ConfirmarPedido(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Request.Cookies.ContainsKey(COOKIE_NAME))
            {
                var cartId = Request.Cookies[COOKIE_NAME];

                Pedido = await _context.Pedido.Include(p => p.ItensPedido).ThenInclude(ip => ip.Produto).
                    FirstOrDefaultAsync(p => p.IdCarrinho == cartId);                

                if (Pedido != null)
                {
                    if ((Pedido.ItensPedido != null) && (Pedido.ItensPedido.Count > 0))
                    {
                        if (Pedido.Situacao == PedidoModel.SituacaoPedido.Carrinho)
                        {
                            Cliente = await _context.Cliente.FirstOrDefaultAsync(c => c.Email == User.Identity.Name);
                            Pedido.IdCliente = Cliente.IdCliente;
                            Pedido.Endereco = Cliente.Endereco;
                            Pedido.ValorTotal = Pedido.ItensPedido.Sum(x => x.Quantidade * Convert.ToDouble(x.ValorUnitario));
                            await _context.SaveChangesAsync();
                            return Page();
                        }
                    }                    
                }
            }

            return RedirectToPage("/Carrinho");
        }
    }
}