# Curso de C# + MVC + Razor + Angular

[Building a Web App with ASP.NET Core, MVC, Entity Framework Core, Bootstrap, and Angular](https://app.pluralsight.com/library/courses/aspnetcore-mvc-efcore-bootstrap-angular-web/table-of-contents)

## Conceitos e Frameworks Abordadas
- WebAPI com .Net Core
- Entity Framework Core
	- Migrations
	- Seeding
- AutoMapper (mapear de um objeto para outro)
- MVC
	- Razor Pages
	- Node Modules
- Autenticação
	- Lib Identity da Microsoft
	- JWT
- Angular
	- Componentes
	- Formulários
	- Validação
	- Rotas
	- Shopping Cart
	- Services
- Deploy
	- Gulp
	- Azure
	- IIS
	- Folder
	- Deploy com Runtime

## Execução
Para executar o projeto, é necessário acessar a pasta raíz pelo terminal, e utilizar o **cli** do **Angular** para efetuar o build:
```shell
ng build --watch
```
A Flag `--watch` serve para fazer com que os arquivos da pasta do Angular definida no arquivo `angular.json` sejam monitorados e, caso ocorra alguma alteração, o build seja refeito.

Após isso, basta executar normalmente o projeto pelo `Visual Studio`:
- Selecione o projeto `DutchTreat` na Toolbar
- Execute-o

## IdentityDbContext
A classe de conexão com o banco `DutchContext`, está herdando da classe `IdentityDbContext` ao invés da tradicional `DbContext`.
Essa classe é responsável por facilitar a Autenticação dos usuários. Ela já possuí entidades padrões (que também podem ser modificadas, veja: entidade ``StoreUser`) que poupam o trabalho do desenvolvedor.

Também possuí suporte a validação de `Roles` e, caso em determinado momento o método de autenticação seja trocado (de Banco de dados para API por exemplo), não será necessário alterar as chamadas da classe, apenas desenvolver os novos métodos.

## ActionResult
Na API ao colocar o retorno do método como `IActionResult` ou `ActionResult`, estamos delegando a responsabilidade do formato de retorno para a Framework MVC.

## Anotações
O código abaixo é importante ser utilizando quando vamos estar documentando nossa API através de `Annotations`. Dessa forma, ao converter a API para o Swagger ela já estará documentada.
```C#
services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
```

[Documentação](https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-2.2)

O `ActionResult` complica um pouco o trabalho de auto-geração da documentação, pois ele não indica diretamente qual Modelo o método estará retornando, por conta disso, é importante que o usemos em um formato semelhante à `IActionResult<IList<MODEL>>` de modo à indicar qual seu retorno.

## Visualizar Erros
Caso algum erro ocorra, podemos visualizá-lo através do **Output**.

- No Visual Studio, aperte `Ctrl + W, O` (ou pesquise Output através do `Ctrl + Q`)
- Em **Show Output from:** selecione a opção `ASP.NET Core Web Server`
- Arrume.
- Não erre mais.
- Seja feliz.

## Plugins
- Automapper (mapear classes de um objeto para outro)
- Automapper.Extensions.Microsoft.DependencyInjection





