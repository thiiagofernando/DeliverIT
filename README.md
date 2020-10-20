# API De Cadastro de Contas a Pagar
[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

#
API de Cadastro de Contas a Pagar utilizando JWT , Com Autenticação e com as funções de

  - Cadastro de Usuários.
  - Login de Usuários Utilizando Autenticação com JWT.
  - Gerador de Token de Acesso.
  - Cadastro de Contas.
  - Listagem de Contas.
  - Cálculo de Contas e Multas por Atraso.

# Tecnologias  e Ferramentas Utilizados!

  - .NET Core 3.1.402
  - Entity Framework Core 3.1.9
  - Autenticação com JwtBearer
  - Banco de Dados SQL Server
  - Testes unitários com o xUnit
  - Swagger
  - Visual Studio 2019
  
  
# Requisitos para executar o Projeto

  - Altere a ConnectionStrings do Banco de Dados nos arquivos appsettings.json e appsettings.Development.json 
  - Execute um Clean e um Rebuild no Projeto para baixar o pacotes
  - Não é necessario gerar um novo migrations para cria o banco e as tabelas
  - No Statup.cs possui um InitializeDatabase que ao executar o projeto irá criar o banco e as tabelas se a ConnectionStrings estiver correta