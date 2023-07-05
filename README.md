# Autoglass
Neste projeto esta contido duas parte, back-end e Front-end. Todo o CRUD foi testado e esta funcionando.

## Ferramenta utilizada
- Visual studio 2022
  Soluction
     Front - AutoglassWeb
     Back  - Autoglass
## Front-End
   ## Usado Framework Razor
- Aplicatiov Web do Asp.Net Core (Model-View-Controller) (.net 5.0)
- Microsoft.AspNetCore.Mvc.Core (2.2.0)
- Microsoft.AspNetCore.Mvc.Razor (2.2.0)
- Microsoft.AspNetCore.Mvc.RazorPages (2.2.0)
- Microsoft.EntityFrameworkCore (5.0.0)
- X.PagedList.Mvc.Core (7.6.0)
  
  Bootstrap/Jquery
- <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">
- <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
-  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"></script>

## Back-End
- API Web do Asp.Net Core (.net 5.0)
- MVC
- X.PagedList / X.PagedList.Mvc
- FluentValidation.AspNetCore (10.3.6)
- Microsoft.EntityFrameworkCore (5.0.13)
- Microsoft.EntityFrameworkCore.Abstractions (5.0.13)
- Microsoft.EntityFrameworkCore.Design (5.0.13)
- Microsoft.EntityFrameworkCore.SqlServer (5.0.13)
- Microsoft.EntityFrameworkCore.Tools (5.0.13)
- Swashbuckle.AspNetCore (5.6.3)
- AutoMapper (12.0.1)
- AutoMapper.Extensions.Microsoft.DependencyInjection (12.0.1)
- Dapper (2.0.123)

## Para subir o projeto Back-End foi usado o Swagger.

## SGDB
- Sql-Server (2018)

## Script para criação da tabela usada no teste:
- USE [Autoglass]
- GO
- CREATE TABLE [dbo].[TblProduto](
-	[idProduto] [int] IDENTITY(1,1) NOT NULL,
-	[Descricao] [varchar](100) NOT NULL,
-	[DtFabricacao] [date] NULL,
-	[DtValidade] [date] NULL,
-	[CodigoFornecedor] [varchar](20) NULL,
-	[DescricaoFornecedor] [varchar](100) NULL,
-	[CnpjFornecedor] [varchar](20) NULL,
-	[Situacao] [int] NULL,
- CONSTRAINT [PK_TblProduto] PRIMARY KEY CLUSTERED 
- (
-	[idProduto] ASC
- )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = --- OFF) ON [PRIMARY]
- ) ON [PRIMARY]
- GO

# Estrutura do projeto
- Application
  - Autoglass.Application
    - AutoMapper
    - DTO
    - Interface
    - Services
  - Domain
  - Autoglass.Domain
    - Interfaces
    - Models
- Infra
    - CrossCutting
      - Autoglass.Infra.CrossCutting.IoC
        - InjetorDependencias
    - Data
      - Autoglass.Infra.Data
        - Context
        - Mappings
        - Repository
        - UoW
- Service Api
  - Autoglass.Services.Api
    - Controllers



