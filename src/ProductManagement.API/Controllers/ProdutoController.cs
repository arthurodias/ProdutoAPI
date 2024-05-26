using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.API.Controllers
{
    /// <summary>
    /// Controladora de Produtos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ProdutoController"/>.
        /// </summary>
        /// <param name="produtoService">O serviço para gerenciar produtos.</param>
        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        /// <summary>
        /// Obtém um produto pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do produto.</param>
        /// <returns>O produto com o ID especificado.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoDto>> GetProdutoByCodigo(int id)
        {
            var produto = await _produtoService.GetByIdAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        /// <summary>
        /// Obtém todos os produtos.
        /// </summary>
        /// <returns>Uma lista de todos os produtos.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetProdutos()
        {
            var produtos = await _produtoService.GetAsync();
            return Ok(produtos);
        }

        /// <summary>
        /// Obtém produtos por filtro com paginação.
        /// </summary>
        /// <param name="descricao">A descrição do produto para filtrar.</param>
        /// <param name="situacao">A situação do produto para filtrar.</param>
        /// <param name="pageNumber">O número da página para paginação.</param>
        /// <param name="pageSize">O tamanho da página para paginação.</param>
        /// <returns>Uma lista de produtos que correspondem aos critérios de filtro.</returns>
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> GetProdutosByFilter(string descricao, string situacao, int pageNumber = 1, int pageSize = 10)
        {
            var produtos = await _produtoService.GetByFilterAsync(descricao, situacao, pageNumber, pageSize);
            return Ok(produtos);
        }

        /// <summary>
        /// Adiciona um novo produto.
        /// </summary>
        /// <param name="produtoDto">O produto a ser adicionado.</param>
        /// <returns>O produto recém-criado.</returns>
        [HttpPost]
        public async Task<ActionResult> AddProduto(ProdutoDto produtoDto)
        {
            try
            {
                await _produtoService.InsertAsync(produtoDto);
                return CreatedAtAction(nameof(GetProdutoByCodigo), new { id = produtoDto.Id }, produtoDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza um produto existente.
        /// </summary>
        /// <param name="produtoDto">O produto a ser atualizado.</param>
        /// <returns>Um código de status indicando o resultado da operação.</returns>
        [HttpPut]
        public async Task<ActionResult> UpdateProduto(ProdutoDto produtoDto)
        {
            try
            {
                await _produtoService.UpdateAsync(produtoDto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui um produto marcando-o como inativo.
        /// </summary>
        /// <param name="id">O ID do produto.</param>
        /// <returns>Um código de status indicando o resultado da operação.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduto(int id)
        {
            await _produtoService.DeleteAsync(id);
            return NoContent();
        }
    }
}