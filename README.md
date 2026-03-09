# Royal Games Sistema de Catálogo Digital

> **Status do Projeto:** Concluido (Projeto Acadêmico)

## Descrição
O **Royal Games Digital Catalog** é uma solução desenvolvida para centralizar e gerenciar o fluxo de vendas e visualização dos produtos. O projeto Foca em resolve problemas de inconsistência de preços, duplicidade de registros e falta de informações do Produto para o cliente final.

---

## Desenvolvimento e Metodologias
O Projeto foi desenvolvido em dupla, simulando o fluco de trabalho em uma **Software House**. Para isso utilizamos:

### Metodologias Ágeis
Utilizamos conceitos de **Scrum** e **Kanban**, para garantir a organização e comprimento dos prazos.

* **Trello:** Utilizado para o quadro Kanban, permitindo a melhor visualização de tarefas a fazer, pentendes e Concluidas.
* **Sprints:** O desenvolvimento foi dividido em ciclos para entrega de funcionalidades específicas.
* **Dailies:** Reuniões diárias rápidas para alinhar o que foi feito, o que seria feito e identificar possíveis impedimentos.

---

## Arquitetura e Tecnologias
O projeto utiliza o paradigma de Domain-Driven Design (DDD) para garantir uma separação clara de responsabilidades e facilitar a manutenção.

### Stack técnica
* **Linguagem:** C# (.NET 8.0)
* **Banco de Dados:** MySQL (Via Entity Framework core)
* **Autenticaçõa** JWT (Jason Web Token)
* **Documentação:** Swagger (Swashbuckle)

### 📦 Dependências (NuGet)
Conforme as especificações do projeto, utilizamos os seguintes pacotes:
* `Microsoft.AspNetCore.Authentication.JwtBearer`: Segurança e proteção de rotas.
* `Microsoft.EntityFrameworkCore.SqlServer`: Provedor de dados (configurado para MySQL).
* `Microsoft.EntityFrameworkCore.Tools` & `Design`: Gestão de Migrations.
* `Swashbuckle.AspNetCore`: Documentação interativa da API.

---

## 📋 Regras de Negócio (RN)
Para garantir a integridade dos dados da Royal Games, o sistema segue estas diretrizes:
* **RN02:** Operações de escrita (CUD: Criar, Atualizar, Excluir) são restritas a usuários autorizados.
* **RN03:** O sistema emite logs de todas as alterações feitas nos produtos.
* **Integridade:** Não podem existir dois jogos com o mesmo nome.
* **Disponibilidade:** Itens descontinuados aparecem apenas para consulta, sem opção de venda.
* **Preificação:** O valor do produto não pode ser negativo ou zero.

---

## 📂 Estrutura do Projeto (DDD)

```text
src/
├── RoyalGames.Domain/         # Entidades, Interfaces e Regras de Negócio
├── RoyalGames.Application/    # Serviços, DTOs e Mapeamentos (AutoMapper)
├── RoyalGames.Infrastructure/ # Repositórios, Contexto EF e Migrations
└── RoyalGames.API/            # Controllers e Configurações de Middlewares
```
---

## Endpoints Principais

A API utilizou o Swagger (Swashbuckle) como Documentação de Referência, para testes do endpoints.
Abaixo estão os principais:

### Autenticação e Usuário
Para acessar funções administrativas, é necessário obter um token de acesso.

(`**POST** /api/Autenticacao/login `)

* **Request Body:**
```json
{
  "email": "admin@royalgames.com",
  "senha": "123"
}
```

* **Response (200 OK):**
```json
{
  "Token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
}
```
## Gerenciamento de Produto
Endpoints responsáveis pela gestão dos itens da loja.

(`**GET** /api/Jogo `)
* **Descrição:** Retrona Todos os produtos registrado no banco(ativo ou não no Catálogo).
* **Response (200 OK):**
```json
  {
		"jogoID": 1,
		"nome": "Shadow Blade",
		"preco": 59.90,
		"descricao": "Jogo de ação com combates rápidos e ambientação sombria.",
    "Imagem": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAY....",
		"statusJogo": true,
		"generoIds": [
			1
		],
		"generos": [
			"Ação"
		],
		"usuarioID": 1,
		"usuarioNome": "admin",
		"usuarioEmail": "admin@royalgames.com"
	}
```

(`**POST** /api/Jogo/ `)
* **Descrição:** Adiciona Produtos no Catálogo(Requer Token JWT). 

* **Request Body:**
```json
{
  "jogoID": 1,
		"nome": "Shadow Blade",
		"preco": 59.90,
		"descricao": "Jogo de ação com combates rápidos e ambientação sombria.",
    "Imagem": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAY....",
		"generoIds": [
			1
		],
		"generos": [
			"Ação"
		]
}
```
