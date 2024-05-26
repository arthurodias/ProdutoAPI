using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Services;

namespace ProductManagement.Application.Interfaces
{
    public interface IProdutoService : IService<ProdutoDto>
    {
    }
}