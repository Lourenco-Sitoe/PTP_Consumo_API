# Consumo_API_CSharp

Trabalho Prático 2 — Consumo de Web API em C#

## 1. Objetivo

Desenvolver uma aplicação em C# que consome uma API REST, recebe dados em JSON, converte esses dados para objetos C# e apresenta o resultado ao utilizador.

## 2. API escolhida

**DummyJSON – Quotes**

Endpoint:
`https://dummyjson.com/quotes`

A API devolve uma resposta JSON com uma lista de citações, os respetivos autores e metadados como `total`, `skip` e `limit`.

## 3. Requisitos implementados

- Projeto Console em C# chamado `Consumo_API_CSharp`.
- Requisição HTTP do tipo GET através de `HttpClient`.
- Leitura da resposta JSON.
- Desserialização com `System.Text.Json`.
- Classes `Quote` e `QuotesResponse` para representar os dados.
- Tratamento de erros com `try-catch`.
- Apresentação das citações na consola.

## 4. Requisitos de software

- Visual Studio 2022 ou posterior, com a carga de trabalho **Desenvolvimento para desktop com .NET**; ou
- .NET 8 SDK instalado.

## 5. Como abrir e executar no Visual Studio

1. Clone o repositório ou abra a pasta `Consumo_API_CSharp`.
2. Abra o ficheiro `Consumo_API_CSharp.csproj` no Visual Studio.
3. Aguarde a restauração dos componentes do projeto.
4. Se necessário, selecione o projeto como projeto de arranque.
5. Execute com `Ctrl + F5` ou clique em **Iniciar sem depurar**.
6. No menu, escreva `1` para consultar a API.
7. Para terminar, escreva `0`.

É necessária uma ligação à Internet para consultar a API.

## 6. Como executar pelo terminal

Na pasta onde está o ficheiro `.csproj`, execute:

```bash
dotnet restore
dotnet run
```

## 7. Como publicar no GitHub

1. Entre em https://github.com e inicie sessão.
2. Clique em **New repository**.
3. Escreva exatamente `PTP_Consumo_API` como nome.
4. Escolha **Public**.
5. Crie o repositório. Para este procedimento, não marque a opção de criar README, `.gitignore` ou licença, pois o projeto já contém os seus ficheiros.
6. Abra o terminal na pasta do projeto e execute:

```bash
git init
git add .
git commit -m "Primeira versão - Consumo de Web API em C#"
git branch -M main
git remote add origin https://github.com/SEU_UTILIZADOR/PTP_Consumo_API.git
git push -u origin main
```

Substitua `SEU_UTILIZADOR` pelo seu nome de utilizador real do GitHub. Se o Git pedir autenticação, conclua o processo de autenticação apresentado.

7. Abra a página do repositório no GitHub e confirme que os ficheiros aparecem e que a visibilidade está como **Public**.
8. Copie o endereço do repositório e submeta-o no Google Classroom.

## 8. Estrutura do projeto

```text
Consumo_API_CSharp/
├── Models/
│   ├── Quote.cs
│   └── QuotesResponse.cs
├── Program.cs
├── Consumo_API_CSharp.csproj
├── .gitignore
└── README.md
```

## 9. Funcionamento do código

1. `HttpClient` envia a requisição GET para o endpoint.
2. `EnsureSuccessStatusCode()` verifica se a resposta HTTP indica sucesso.
3. `ReadAsStreamAsync()` permite ler o conteúdo devolvido pelo servidor.
4. `JsonSerializer.DeserializeAsync<QuotesResponse>()` converte o JSON em objetos C#.
5. O atributo `[JsonPropertyName("quote")]` associa o campo `quote` do JSON à propriedade `Text` da classe `Quote`, porque uma propriedade não pode ter o mesmo nome da sua classe em C#.
6. O ciclo `foreach` percorre as citações e apresenta ID, texto e autor.
7. Os blocos `catch` tratam falhas de comunicação, erros de JSON, timeout e outros erros inesperados.

## 10. Conclusão

O projeto demonstra o consumo de uma Web API REST em C#, utilizando o método HTTP GET, a classe `HttpClient` e a biblioteca `System.Text.Json` para processar dados recebidos em formato JSON. Também apresenta os resultados na consola e inclui tratamento de exceções para melhorar a robustez da aplicação.
