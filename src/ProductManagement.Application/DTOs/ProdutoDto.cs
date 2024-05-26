using ProductManagement.Application.Validations;

namespace ProductManagement.Application.DTOs
{
    public class ProdutoDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Situacao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public int CodigoFornecedor { get; set; }
        public string DescricaoFornecedor { get; set; }
        public string CnpjFornecedor { get; set; }
        public bool IsValid { get { return ProdutoDtoValidation.Validar(this); } }
    }
}