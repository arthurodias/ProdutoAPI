using AutoMapper;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Repositories;
using ProductManagement.Domain.Services;

namespace ProductManagement.Application.Services
{
    public class ProdutoService : IService<ProdutoDto>, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task DeleteAsync(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            if (produto != null)
            {
                produto.Situacao = "Inativo";
                await _produtoRepository.UpdateAsync(produto);
            }
        }

        public async Task<IEnumerable<ProdutoDto>> GetAsync()
        {
            var produtos = await _produtoRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
        }

        public async Task<IEnumerable<ProdutoDto>> GetByFilterAsync(string descricao, string situacao, int pageNumber, int pageSize)
        {
            var produtos = await _produtoRepository.GetByFilterAsync(descricao, situacao, pageNumber, pageSize);
            return _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
        }

        public async Task<ProdutoDto> GetByIdAsync(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            return _mapper.Map<ProdutoDto>(produto);
        }

        public async Task InsertAsync(ProdutoDto dto)
        {
            if (!dto.IsValid)
            {
                throw new ArgumentException("Data de fabricação não pode ser maior ou igual à data de validade.");
            }

            var produto = _mapper.Map<Produto>(dto);
            await _produtoRepository.AddAsync(produto);
        }

        public async Task UpdateAsync(ProdutoDto dto)
        {
            if (!dto.IsValid)
            {
                throw new ArgumentException("Data de fabricação não pode ser maior ou igual à data de validade.");
            }

            var produto = _mapper.Map<Produto>(dto);
            await _produtoRepository.UpdateAsync(produto);
        }
    }
}