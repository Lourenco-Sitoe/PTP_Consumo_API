using System.Net;
using System.Text.Json;
using Consumo_API_CSharp.Models;

namespace Consumo_API_CSharp;

internal class Program
{
    private static readonly HttpClient HttpClient = new()
    {
        Timeout = TimeSpan.FromSeconds(20)
    };

    private const string ApiUrl = "https://dummyjson.com/quotes";

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private static async Task Main(string[] args)
    {
        Console.Title = "Consumo de Web API em C#";
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("==============================================");
        Console.WriteLine("       SISTEMA DE CONSUMO DE WEB API");
        Console.WriteLine("==============================================");
        Console.WriteLine("API utilizada: DummyJSON - Quotes");
        Console.WriteLine("Projeto: Consumo_API_CSharp");
        Console.WriteLine();

        bool continuar = true;

        while (continuar)
        {
            Console.WriteLine("MENU PRINCIPAL");
            Console.WriteLine("1 - Consultar citações na API");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            string? opcao = Console.ReadLine();

            // Se a entrada terminar (por exemplo, quando o programa é
            // executado sem interação), encerra em vez de entrar em ciclo.
            if (opcao is null)
            {
                Console.WriteLine();
                Console.WriteLine("Entrada terminada. O programa vai encerrar.");
                break;
            }

            Console.WriteLine();

            switch (opcao)
            {
                case "1":
                    await ConsultarCitacoesAsync();
                    break;

                case "0":
                    continuar = false;
                    Console.WriteLine("Programa terminado. Obrigado!");
                    break;

                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private static async Task ConsultarCitacoesAsync()
    {
        Console.WriteLine("A estabelecer ligação à API...");
        Console.WriteLine($"Endpoint: {ApiUrl}");
        Console.WriteLine();

        try
        {
            // 1. Envia uma requisição HTTP GET para a API.
            using HttpResponseMessage resposta =
                await HttpClient.GetAsync(ApiUrl);

            // 2. Verifica se o servidor respondeu com sucesso.
            if (resposta.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine("Erro: o endereço da API não foi encontrado (404).");
                return;
            }

            resposta.EnsureSuccessStatusCode();

            // 3. Lê o conteúdo JSON recebido.
            await using Stream fluxo = await resposta.Content.ReadAsStreamAsync();

            // 4. Converte (desserializa) o JSON para objetos C#.
            QuotesResponse? dados =
                await JsonSerializer.DeserializeAsync<QuotesResponse>(
                    fluxo, JsonOptions);

            if (dados?.Quotes is null || dados.Quotes.Count == 0)
            {
                Console.WriteLine("A API não devolveu citações.");
                return;
            }

            // 5. Apresenta os dados ao utilizador.
            Console.WriteLine("CONSULTA REALIZADA COM SUCESSO!");
            Console.WriteLine($"Total de citações informado pela API: {dados.Total}");
            Console.WriteLine($"Citações recebidas nesta resposta: {dados.Quotes.Count}");
            Console.WriteLine("----------------------------------------------");

            foreach (Quote citacao in dados.Quotes)
            {
                Console.WriteLine($"ID: {citacao.Id}");
                Console.WriteLine($"Citação: {citacao.Text}");
                Console.WriteLine($"Autor: {citacao.Author}");
                Console.WriteLine("----------------------------------------------");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("ERRO DE COMUNICAÇÃO COM A API.");
            Console.WriteLine($"Detalhes: {ex.Message}");
            Console.WriteLine("Verifique a ligação à Internet e tente novamente.");
        }
        catch (JsonException ex)
        {
            Console.WriteLine("ERRO AO PROCESSAR OS DADOS JSON.");
            Console.WriteLine($"Detalhes: {ex.Message}");
        }
        catch (TaskCanceledException)
        {
            Console.WriteLine("A requisição demorou demasiado tempo ou foi cancelada.");
            Console.WriteLine("Verifique a ligação e tente novamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("OCORREU UM ERRO INESPERADO.");
            Console.WriteLine($"Detalhes: {ex.Message}");
        }
    }
}
