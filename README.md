# ProdutoAPI

## Descrição

A `ProdutoAPI` é uma aplicação ASP.NET Core para gestão de produtos, seguindo os princípios de Domain-Driven Design (DDD) e uma arquitetura em camadas. A API utiliza Entity Framework Core para acesso a dados, AutoMapper para mapeamento de objetos e inclui validações e testes unitários.

## Funcionalidades

A API permite:

- Recuperar um produto por código.
- Listar produtos com suporte a filtragem e paginação.
- Inserir um novo produto com validações.
- Editar um produto existente com validações.
- Excluir (logicamente) um produto.

## Estrutura do Projeto

- **ProductManagement.API**: Contém os controllers e configurações da API.
- **ProductManagement.Application**: Contém os DTOs, interfaces de serviços e implementações de serviços.
- **ProductManagement.Domain**: Contém as entidades de domínio e interfaces de repositórios.
- **ProductManagement.Infrastructure**: Contém a implementação dos repositórios e o DbContext.
- **ProductManagement.Tests**: Contém os testes unitários.

## Tecnologias Utilizadas

- ASP.NET Core 8
- Entity Framework Core
- AutoMapper
- xUnit
- Moq
- SQLite (para desenvolvimento e testes)

## Pré-requisitos

- .NET 8 SDK
- Docker (para rodar um banco de dados SQL Server em contêiner, se necessário)

## Configuração do Projeto

1. **Clone o repositório**:

   ```sh
   git clone https://github.com/arthurodias/ProdutoAPI.git
   cd ProdutoAPI

2. **Configuração do Banco de Dados**:

No arquivo appsettings.json, configure a string de conexão do banco de dados. No exemplo abaixo, usamos SQLite:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=produtos.db"
  }
}
```
3. **Restaurar dependências**:

```sh
dotnet restore
```
4. **Aplicar migrações**:

```sh
dotnet ef database update --project src/ProductManagement.Infrastructure --startup-project src/ProductManagement.API
```

5. **Executando a API**:

Para iniciar a API, execute o seguinte comando na raiz do projeto:

```sh
dotnet run --project src/ProductManagement.API
```
A API estará disponível em http://localhost:5000.

6. **Endpoints da API**:

Recuperar um produto por código
```http
GET /api/produto/{id}
```
Listar produtos
```http
GET /api/produto
```
Filtrar e paginar produtos
```http
GET /api/produto/filter?descricao={descricao}&situacao={situacao}&pageNumber={pageNumber}&pageSize={pageSize}
```
Inserir um novo produto
```http
POST /api/produto
```

Corpo da requisição (JSON):

```json
{
  "descricao": "Produto Teste",
  "situacao": "Ativo",
  "dataFabricacao": "2023-05-01T00:00:00",
  "dataValidade": "2023-10-01T00:00:00",
  "codigoFornecedor": 1,
  "descricaoFornecedor": "Fornecedor Teste",
  "cnpjFornecedor": "12345678000199"
}
```
Editar um produto
```http
PUT /api/produto
```
Corpo da requisição (JSON):

```json
{
  "id": 1,
  "descricao": "Produto Editado",
  "situacao": "Ativo",
  "dataFabricacao": "2023-05-01T00:00:00",
  "dataValidade": "2023-10-01T00:00:00",
  "codigoFornecedor": 1,
  "descricaoFornecedor": "Fornecedor Editado",
  "cnpjFornecedor": "12345678000199"
}
```
Excluir um produto
```http
DELETE /api/produto/{id}
```

7. **Testes**

Para rodar os testes unitários, execute o seguinte comando:

```sh
dotnet test
```