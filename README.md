# Consumo_API_CSharp

Trabalho Prático 2 — Consumo de Web API em C#

## 1. Objetivo

Desenvolver uma aplicação em C# que consome uma API REST, recebe dados em JSON, converte esses dados para objetos C# e apresenta o resultado ao utilizador.

## 2. API escolhida

**Bible API**

Site: https://bible-api.com

Endpoints utilizados:

- Português (João Ferreira de Almeida):
  `https://bible-api.com/john%203:16?translation=almeida`
- Inglês (World English Bible):
  `https://bible-api.com/john%203:16`

A API devolve uma resposta JSON com a referência, a lista de versículos, o texto completo, a tradução utilizada e uma nota sobre a tradução.

Também é possível consultar qualquer outro livro, capítulo, versículo ou intervalo. Exemplos:

- Capítulo inteiro: `https://bible-api.com/genesis%201?translation=almeida`
- Salmo inteiro: `https://bible-api.com/salmos%2023?translation=almeida`
- Intervalo de versículos: `https://bible-api.com/mateus%205:1-12?translation=almeida`

A API permite consultar qualquer passagem da Bíblia, mas não foi feita para descarregar a Bíblia inteira num único pedido. A documentação indica um limite de **15 pedidos por cada 30 segundos** e recomenda utilizar o repositório de dados de origem para obter a Bíblia completa.

Exemplo simplificado da resposta:

```json
{
  "reference": "João 3:16",
  "verses": [
    {
      "book_id": "JHN",
      "book_name": "João",
      "chapter": 3,
      "verse": 16,
      "text": "Porque Deus amou o mundo de tal maneira..."
    }
  ],
  "text": "Porque Deus amou o mundo de tal maneira...",
  "translation_id": "almeida",
  "translation_name": "João Ferreira de Almeida",
  "translation_note": "Public Domain"
}
```

## 3. Requisitos implementados

- Projeto Console em C# chamado `Consumo_API_CSharp`.
- Requisição HTTP do tipo GET através de `HttpClient`.
- Leitura da resposta JSON.
- Desserialização com `System.Text.Json`.
- Classes `BibleResponse` e `Verse` para representar os dados.
- Atributos `[JsonPropertyName]` para associar os campos JSON às propriedades C#.
- Tratamento de erros com `try-catch`.
- Apresentação da referência, tradução e versículos na consola.
- Menu com opções para consultar passagens fixas e qualquer referência indicada pelo utilizador, em português ou em inglês.

## 4. Requisitos de software

- Visual Studio 2022 ou posterior, com a carga de trabalho **Desenvolvimento para desktop com .NET**; ou
- .NET 8 SDK instalado.

## 5. Como abrir e executar no Visual Studio

1. Clone o repositório ou abra a pasta `Consumo_API_CSharp`.
2. Abra o ficheiro `Consumo_API_CSharp.csproj` no Visual Studio.
3. Aguarde a restauração dos componentes do projeto.
4. Se necessário, selecione o projeto como projeto de arranque.
5. Execute com `Ctrl + F5` ou clique em **Iniciar sem depurar**.
6. No menu, escreva `1` ou `2` para as passagens de exemplo, ou `3` para introduzir outra referência e escolher a língua (português ou inglês).
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
│   ├── BibleResponse.cs
│   └── Verse.cs
├── Program.cs
├── Consumo_API_CSharp.csproj
├── .gitignore
└── README.md
```

## 9. Funcionamento do código

1. `HttpClient` envia a requisição GET para o endpoint escolhido no menu.
2. Na opção `3`, o utilizador escolhe primeiro a língua e depois introduz a referência; `Uri.EscapeDataString()` codifica essa referência para que possa ser usada no URL.
3. A resposta 404 é tratada e `EnsureSuccessStatusCode()` verifica os restantes códigos HTTP.
4. `ReadAsStreamAsync()` permite ler o conteúdo devolvido pelo servidor.
5. `JsonSerializer.DeserializeAsync<BibleResponse>()` converte o JSON em objetos C#.
6. Os atributos `[JsonPropertyName]` ligam os nomes do JSON (`book_name`, `translation_name`, etc.) às propriedades das classes.
7. O ciclo `foreach` percorre os versículos e apresenta o livro, capítulo, número do versículo e texto.
8. Os blocos `catch` tratam falhas de comunicação, erros de JSON, timeout e outros erros inesperados.

## 10. Exemplo de execução

```text
CONSULTA REALIZADA COM SUCESSO!
Referência: João 3:16
Tradução: João Ferreira de Almeida (almeida)
Versículos recebidos: 1
Nota: Public Domain
----------------------------------------------
João 3:16
Porque Deus amou o mundo de tal maneira que deu o seu Filho unigênito,
para que todo aquele que nele crê não pereça, mas tenha a vida eterna.
----------------------------------------------
```

## 11. Conclusão

O projeto demonstra o consumo de uma Web API REST em C#, utilizando o método HTTP GET, a classe `HttpClient` e a biblioteca `System.Text.Json` para processar dados recebidos em formato JSON. Também apresenta os resultados na consola e inclui tratamento de exceções para melhorar a robustez da aplicação.
