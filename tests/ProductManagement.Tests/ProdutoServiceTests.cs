using AutoMapper;
using Moq;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Mapping;
using ProductManagement.Application.Services;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Repositories;

namespace ProductManagement.Tests
{
    public class ProdutoServiceTests
    {
        private readonly Mock<IProdutoRepository> _mockProdutoRepository;
        private readonly IMapper _mapper;
        private readonly ProdutoService _produtoService;

        public ProdutoServiceTests()
        {
            _mockProdutoRepository = new Mock<IProdutoRepository>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            _mapper = mappingConfig.CreateMapper();
            _produtoService = new ProdutoService(_mockProdutoRepository.Object, _mapper);
        }

        [Fact]
        public async Task AddProdutoAsync_ShouldThrowArgumentException_WhenDataFabricacaoIsGreaterThanDataValidade()
        {
            var produtoDto = new ProdutoDto
            {
                Descricao = "Produto Teste",
                Situacao = "Ativo",
                DataFabricacao = DateTime.Now,
                DataValidade = DateTime.Now.AddDays(-1),
                CodigoFornecedor = 1,
                DescricaoFornecedor = "Fornecedor Teste",
                CnpjFornecedor = "12345678000199"
            };

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _produtoService.InsertAsync(produtoDto));
            Assert.Equal("Data de fabricação não pode ser maior ou igual à data de validade.", exception.Message);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnAllProdutos()
        {
            var produtos = new List<Produto>
        {
            new Produto { Id = 1, Descricao = "Produto 1" },
            new Produto { Id = 2, Descricao = "Produto 2" }
        };

            _mockProdutoRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(produtos);

            var result = await _produtoService.GetAsync();

            Assert.Equal(2, result.Count());
            Assert.Equal("Produto 1", result.First().Descricao);
        }

        [Fact]
        public async Task GetByFilterAsync_ShouldReturnFilteredProdutos()
        {
            var produtos = new List<Produto>
        {
            new Produto { Id = 1, Descricao = "Produto 1", Situacao = "Ativo" },
            new Produto { Id = 2, Descricao = "Produto 2", Situacao = "Inativo" }
        };

            _mockProdutoRepository.Setup(repo => repo.GetByFilterAsync("Produto 1", "Ativo", 1, 10)).ReturnsAsync(produtos.Where(p => p.Descricao == "Produto 1" && p.Situacao == "Ativo"));

            var result = await _produtoService.GetByFilterAsync("Produto 1", "Ativo", 1, 10);

            Assert.Single(result);
            Assert.Equal("Produto 1", result.First().Descricao);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProdutoById()
        {
            var produto = new Produto { Id = 1, Descricao = "Produto 1" };

            _mockProdutoRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(produto);

            var result = await _produtoService.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Produto 1", result.Descricao);
        }

        [Fact]
        public async Task InsertAsync_ShouldInsertProduto_WhenDatesAreValid()
        {
            var produtoDto = new ProdutoDto
            {
                Descricao = "Produto Teste",
                Situacao = "Ativo",
                DataFabricacao = DateTime.Now,
                DataValidade = DateTime.Now.AddDays(10),
                CodigoFornecedor = 1,
                DescricaoFornecedor = "Fornecedor Teste",
                CnpjFornecedor = "12345678000199"
            };

            await _produtoService.InsertAsync(produtoDto);

            _mockProdutoRepository.Verify(repo => repo.AddAsync(It.IsAny<Produto>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateProduto_WhenDatesAreValid()
        {
            var produtoDto = new ProdutoDto
            {
                Id = 1,
                Descricao = "Produto Atualizado",
                Situacao = "Ativo",
                DataFabricacao = DateTime.Now,
                DataValidade = DateTime.Now.AddDays(10),
                CodigoFornecedor = 1,
                DescricaoFornecedor = "Fornecedor Teste",
                CnpjFornecedor = "12345678000199"
            };

            await _produtoService.UpdateAsync(produtoDto);

            _mockProdutoRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Produto>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldMarkProdutoAsInactive()
        {
            var produto = new Produto { Id = 1, Descricao = "Produto 1", Situacao = "Ativo" };

            _mockProdutoRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(produto);

            await _produtoService.DeleteAsync(1);

            _mockProdutoRepository.Verify(repo => repo.UpdateAsync(It.Is<Produto>(p => p.Situacao == "Inativo")), Times.Once);
        }
    }
}