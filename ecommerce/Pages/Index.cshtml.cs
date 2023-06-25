using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Pages;

public class IndexModel : PageModel
{
    private const int tamanhoPagina = 12;
    private readonly ILogger<IndexModel> _logger;
    private AppDbContext _context;
    public int PaginaAtual { get; set; }
    public int QuantidadePaginas { get; set; }
    public IList<ProdutoModel> Produtos;
    public IndexModel(ILogger<IndexModel> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task OnGetAsync([FromQuery(Name ="q")]string termoBusca, 
    [FromQuery(Name = "o")]int? ordem = 1, [FromQuery(Name = "p")]int? pagina = 1)
    {
        this.PaginaAtual = pagina.Value;

        var query = _context.Produto.AsQueryable();

        if (!string.IsNullOrEmpty(termoBusca))
        {
            query = query.Where(
                p => p.Nome.ToUpper().Contains(termoBusca.ToUpper())).OrderBy(p => p.Preco);
        }

        if (ordem.HasValue)
        {
            switch(ordem.Value)
            {
                case 1:
                    query = query.OrderBy(p => p.Nome.ToUpper());
                    break;
                case 2:
                    query = query.OrderBy(p => p.Preco);
                    break;
                case 3:
                    query = query.OrderByDescending(p => p.Preco);
                    break;
            }
        }

        var queryCount = query;
        int qtdeProdutos = queryCount.Count();
        this.QuantidadePaginas = Convert.ToInt32(Math.Ceiling(qtdeProdutos*1M / tamanhoPagina));

        query = query.Skip(tamanhoPagina * (this.PaginaAtual - 1)).Take(tamanhoPagina);

        Produtos = await query.ToListAsync();
    }
}
