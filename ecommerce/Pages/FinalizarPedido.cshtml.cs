using System;
using System.Text;
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
    public class FinalizarPedido : PageModel
    {
        private AppDbContext _context;
     
        public string COOKIE_NAME
        {
            get { return ".AspNetCore.CartId"; }
        }

        public PedidoModel Pedido { get; set; }

        public ClienteModel Cliente { get; set; }

        public FinalizarPedido(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Request.Cookies.ContainsKey(COOKIE_NAME))
            {
                var cartId = Request.Cookies[COOKIE_NAME];

                Pedido = await _context.Pedido.Include("ItensPedido").
                    Include("ItensPedido.Produto").FirstOrDefaultAsync(p => p.IdCarrinho == cartId);

                Cliente = await _context.Cliente.FirstOrDefaultAsync(c => c.Email == User.Identity.Name);

                if ((Pedido.IdCliente > 0) && (Pedido.Endereco != null))
                {
                    Pedido.Situacao = PedidoModel.SituacaoPedido.Realizado;
                    Pedido.DataHoraPedido = DateTime.UtcNow;
                    foreach (var item in Pedido.ItensPedido)
                    {
                        item.Produto.Estoque -= (int)item.Quantidade;
                    }
                    await _context.SaveChangesAsync();
                    Response.Cookies.Delete(COOKIE_NAME);
                    return Page();
                }
                else
                {
                    return RedirectToPage("/ConfirmarPedido");
                }
            }

            return RedirectToPage("/Carrinho");
        }

    }
}