using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Models
{
    public class ItemPedidoModel
    {
        [Required]        
        public int IdPedido { get; set; }

        [Required]        
        public int IdProduto { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public float Quantidade { get; set; }

        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]        
        [Display(Name = "Valor Unitário")]
        [DataType(DataType.Currency)]
        public double ValorUnitario { get; set; }

        [NotMapped]
        public double ValorItem
        {
            get
            {
                return Quantidade * ValorUnitario;
            }
        }
        [ForeignKey("IdPedido")]
        public PedidoModel Pedido { get; set; }

        [ForeignKey("IdProduto")]
        public ProdutoModel Produto { get; set; }
    }
}