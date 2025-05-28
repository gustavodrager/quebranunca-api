# quebranunca-api

API oficial da plataforma **QuebraNunca** – Sistema colaborativo de gestão de jogadores, grupos, partidas e rankings de futevôlei.

## 🧱 Arquitetura

- **Framework**: .NET 8
- **Banco de Dados**: PostgreSQL
- **Mensageria**: RabbitMQ
- **Auth**: JWT Bearer
- **Containerização**: Docker
- **Deploy**: AWS ECS Fargate (via ECR)
- **Logs**: CloudWatch
- **Arquitetura**: Clean Architecture + DDD + SOLID

## 🚀 Funcionalidades

- Cadastro e autenticação de jogadores
- Criação e gestão de grupos
- Agendamento e validação de partidas
- Sistema de rankings por grupo
- Validação colaborativa das partidas

## 📁 Estrutura do Projeto

```
/src
  /Application
  /Domain
  /Infrastructure
  /API
/tests
  /Unit
  /Integration
/docker
  /Dockerfile
  /entrypoint.sh
```

## 🧪 Como rodar localmente

### Pré-requisitos

- .NET 8 SDK
- Docker
- PostgreSQL e RabbitMQ (local ou via Docker)

### 1. Configure o ambiente

Crie um arquivo `.env` baseado no [`./.env.example`](./.env.example):

```bash
cp .env.example .env
```

### 2. Suba os serviços com Docker Compose

```bash
docker-compose up -d
```

### 3. Execute a API localmente

```bash
dotnet run --project src/API
```

A API estará disponível em `https://localhost:5001`.

## 🔐 Autenticação

A autenticação é feita via JWT. Para endpoints protegidos, envie o token no header:

```http
Authorization: Bearer <seu-token>
```

## 📦 Deploy

O deploy é feito via imagens Docker publicadas no **ECR**, executadas no **ECS Fargate**.

Veja instruções completas em [`docs/deploy-aws.md`](./docs/deploy-aws.md)

## 📚 Documentação

Em breve será disponibilizada a documentação Swagger em `/swagger`.

## 🛠 Contribuindo

Contribuições são bem-vindas! Confira o [`CONTRIBUTING.md`](./CONTRIBUTING.md) para detalhes.
