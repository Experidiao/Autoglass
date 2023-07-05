# Autoglass
Neste projeto esta contido duas parte, back-end e Front-end. Todo o CRUD foi testado e esta funcionando.

## Ferramenta utilizada
- Visual studio 2022

## Front-End
- Aplicatiov Web do Asp.Net Core (Model-View-Controller) (.net 5.0)
- Razor

## Back-End
- API Web do Asp.Net Core (.net 5.0)
- MVC
- X.PagedList / X.PagedList.Mvc
- Para subir o projeto Back-End foi usado o Swagger.

## SGDB
- Sql-Server (2018)

## Script para criação da tabela usada no teste:
- USE [Autoglass]
- GO
- CREATE TABLE [dbo].[TblProduto](
	[idProduto] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](100) NOT NULL,
	[DtFabricacao] [date] NULL,
	[DtValidade] [date] NULL,
	[CodigoFornecedor] [varchar](20) NULL,
	[DescricaoFornecedor] [varchar](100) NULL,
	[CnpjFornecedor] [varchar](20) NULL,
	[Situacao] [int] NULL,
 CONSTRAINT [PK_TblProduto] PRIMARY KEY CLUSTERED 
(
	[idProduto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

# Estrutura do projeto


