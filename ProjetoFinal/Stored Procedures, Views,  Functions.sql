CREATE PROCEDURE SP_CriarPedido
(
    @ClienteId INT,
    @EnderecoId INT,
    @ProdutoId INT,
    @Quantidade INT
)
AS
BEGIN

    DECLARE @Preco DECIMAL(18,2)
    DECLARE @ValorTotal DECIMAL(18,2)
    DECLARE @PedidoId INT

    -- Buscar preço
    SELECT @Preco = Preco
    FROM Produtos
    WHERE ProdutoId = @ProdutoId

    -- Calcular total
    SET @ValorTotal = @Preco * @Quantidade

    -- Criar pedido
    INSERT INTO Pedidos
    (
        ClienteId,
        EnderecoId,
        Data,
        ValorTotal,
        Ativo
    )
    VALUES
    (
        @ClienteId,
        @EnderecoId,
        GETDATE(),
        @ValorTotal,
        1
    )

    -- Pegar ID
    SET @PedidoId = SCOPE_IDENTITY()

    -- Criar item
    INSERT INTO ItensPedido
    (
        PedidoId,
        ProdutoId,
        Quantidade,
        PrecoUnitario,
        Ativo
    )
    VALUES
    (
        @PedidoId,
        @ProdutoId,
        @Quantidade,
        @Preco,
        1
    )

    -- Atualizar estoque
    UPDATE Produtos
    SET Quantidade = Quantidade - @Quantidade
    WHERE ProdutoId = @ProdutoId

END
------------------------------------------------------------------------------------------------------
CREATE PROCEDURE SP_ListarProdutos
AS
BEGIN

    SELECT
        ProdutoId,
        Nome,
        Descricao,
        Preco,
        Quantidade
    FROM Produtos
    WHERE Ativo = 1

END
------------------------------------------------------------------------------------------------------
CREATE PROCEDURE SP_AtualizarEstoque
(
    @ProdutoId INT,
    @Quantidade INT
)
AS
BEGIN

    UPDATE Produtos
    SET Quantidade = @Quantidade
    WHERE ProdutoId = @ProdutoId

END
--------------------------------------------------------------------------------------------------------
CREATE PROCEDURE SP_ExcluirPedido
(
    @PedidoId INT
)
AS
BEGIN

    UPDATE Pedidos
    SET Ativo = 0
    WHERE PedidoId = @PedidoId

END
---------------------------------------------------------------------------------------------------------
CREATE VIEW VW_PedidosCompletos
AS

SELECT
    p.PedidoId,
    c.Nome AS Cliente,
    pr.Nome AS Produto,
    ip.Quantidade,
    ip.PrecoUnitario,
    p.ValorTotal,
    p.Data
FROM Pedidos p
INNER JOIN Clientes c
ON p.ClienteId = c.ClientId

INNER JOIN ItensPedido ip
ON p.PedidoId = ip.PedidoId

INNER JOIN Produtos pr
ON ip.ProdutoId = pr.ProdutoId
----------------------------------------------------------------------------------------------------------

CREATE VIEW VW_ProdutosEstoque
AS

SELECT
    ProdutoId,
    Nome,
    Quantidade,
    Preco
FROM Produtos
WHERE Quantidade > 0

------------------------------------------------------------------------------------------------------------

CREATE VIEW VW_ClientesEnderecos
AS

SELECT
    c.ClientId,
    c.Nome,
    c.Email,
    e.Rua,
    e.Numero,
    e.Bairro,
    e.Cidade,
    e.Estado
FROM Clientes c
INNER JOIN Enderecos e
ON c.ClientId = e.ClienteId

------------------------------------------------------------------------------------------------------------

CREATE FUNCTION FN_ValorTotalPedido
(
    @PedidoId INT
)
RETURNS DECIMAL(18,2)
AS
BEGIN

    DECLARE @Total DECIMAL(18,2)

    SELECT @Total =
    SUM(Quantidade * PrecoUnitario)
    FROM ItensPedido
    WHERE PedidoId = @PedidoId

    RETURN @Total

END

SELECT 
    PedidoId,
    dbo.FN_ValorTotalPedido(PedidoId) AS Total
FROM Pedidos
-----------------------------------------------------------------------------------------------------------

CREATE FUNCTION FN_VerificarEstoque
(
    @ProdutoId INT
)
RETURNS INT
AS
BEGIN

    DECLARE @Quantidade INT

    SELECT @Quantidade = Quantidade
    FROM Produtos
    WHERE ProdutoId = @ProdutoId

    RETURN @Quantidade

END

SELECT 
    ProdutoId,
    dbo.FN_VerificarEstoque (ProdutoId) AS Quantidade
FROM Produtos
-----------------------------------------------------------------------------------------------------------

USE ProjetoFinalLoja
GO

CREATE FUNCTION FN_NomeFormatado
(
    @Nome NVARCHAR(100)
)
RETURNS NVARCHAR(100)
AS
BEGIN
    RETURN UPPER(LEFT(@Nome, 1)) + LOWER(SUBSTRING(@Nome, 2, LEN(@Nome)))
END

SELECT dbo.FN_NomeFormatado(Nome) AS NomeClientes FROM Clientes