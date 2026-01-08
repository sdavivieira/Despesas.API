Despesas API
Projeto desenvolvido em .NET 8 utilizando Entity Framework Core com SQLite.

A aplicação consiste em uma API REST responsável pelo gerenciamento de pessoas, categorias e transações financeiras, sendo consumida por um frontend desenvolvido em React.
Tecnologias
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- C#
Regras de Negócio
As regras abaixo são aplicadas exclusivamente no back-end:

Caso a pessoa informada seja menor de 18 anos, apenas transações do tipo despesa são permitidas.
A categoria utilizada em uma transação deve ser compatível com o tipo da transação.
Exemplo: uma transação do tipo despesa não pode utilizar uma categoria cuja finalidade seja receita.
Caso uma pessoa for excluída toda a transação será excluída.
Funcionalidades
Cadastro, Alteração, Consulta, Exclusão de pessoas
Cadastro e Consulta de categorias
Cadastro e listagem de transações
Consulta de totais por pessoa
Consulta de totais por categoria

API
A API está disponível para consumo do frontend React através da seguinte URL base:
http://localhost:5234/api

Documentação
A documentação da API pode ser acessada via Swagger após a execução do projeto:

http://localhost:5234/swagger
Execução
1. Clone o repositório
2. Execute o projeto da API
3. Faça a inicialização e update (add-migration initialcreate update-database) do migrations para utilizar o sqllite
4. Acesse o Swagger para testar os endpoints
5. Execute o frontend React apontando para a URL da API
