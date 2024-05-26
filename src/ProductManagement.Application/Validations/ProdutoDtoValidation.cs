using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Validations
{
    public static class ProdutoDtoValidation
    {
        public static bool Validar(ProdutoDto produto)
        {
            return produto != null && produto.DataValidade >= produto.DataFabricacao;
        }
    }
}