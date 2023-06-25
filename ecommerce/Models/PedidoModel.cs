using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Models
{
    public class PedidoModel
    {
        public enum SituacaoPedido
        {
            Carrinho,
            Realizado,
            Verificado,
            Atendido,
            Entregue,
            Cancelado
        }

        [Key]
        [Display(Name = "Código")]
        public int IdPedido { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Data/Hora")]
        public DateTime DataHoraPedido { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Valor Total")]
        [DataType(DataType.Currency)]
        public double ValorTotal { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [DisplayName("Situação")]
        public SituacaoPedido Situacao { get; set; }
        public int? IdCliente { get; set; }
        public string IdCarrinho { get; set; }
        [ForeignKey("IdCliente")]
        public ClienteModel Cliente { get; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public EnderecoModel? Endereco { get; set; }
        public ICollection<ItemPedidoModel> ItensPedido { get; set; }
    }
}