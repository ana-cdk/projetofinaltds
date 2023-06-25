using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Models
{
    public class ProdutoModel
    {
        [Key]
        [Display(Name = "Código")]
        [DisplayFormat(DataFormatString = "{0:d6}")]
        public int IdProduto { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(100)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(1000)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Currency)]
        [Display(Name = "Preço")]
        public double?  Preco { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int? Estoque { get; set; }
    }
}