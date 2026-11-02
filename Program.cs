using System.Net;
using System.Text.Json;
using Consumo_API_CSharp.Models;

namespace Consumo_API_CSharp;

internal class Program
{
    private static readonly HttpClient HttpClient = CriarHttpClient();

    private const string ApiUrlPortugues =
        "https://bible-api.com/john%203:16?translation=almeida";

    private const string ApiUrlIngles =
        "https://bible-api.com/john%203:16";

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private static HttpClient CriarHttpClient()
    {
        HttpClient cliente = new()
        {
            Timeout = TimeSpan.FromSeconds(20)
        };

        cliente.DefaultRequestHeaders.UserAgent.ParseAdd("Consumo_API_CSharp/1.0");
        return cliente;
    }

    private static async Task Main(string[] args)
    {
        Console.Title = "Consumo de Web API em C#";
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("==============================================");
        Console.WriteLine("       SISTEMA DE CONSUMO DE WEB API");
        Console.WriteLine("==============================================");
        Console.WriteLine("API utilizada: Bible API");
        Console.WriteLine("Projeto: Consumo_API_CSharp");
        Console.WriteLine();

        bool continuar = true;

        while (continuar)
        {
            Console.WriteLine("MENU PRINCIPAL");
            Console.WriteLine("1 - Consultar João 3:16 (Português - Almeida)");
            Console.WriteLine("2 - Consultar John 3:16 (Inglês - World English Bible)");
            Console.WriteLine("3 - Consultar outra passagem (Português ou Inglês)");
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
                    await ConsultarPassagemAsync(ApiUrlPortugues);
                    break;

                case "2":
                    await ConsultarPassagemAsync(ApiUrlIngles);
                    break;

                case "3":
                    await ConsultarOutraPassagemAsync();
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

    private static async Task ConsultarOutraPassagemAsync()
    {
        Console.WriteLine("Escolha o idioma da passagem:");
        Console.WriteLine("1 - Português (João Ferreira de Almeida)");
        Console.WriteLine("2 - Inglês (World English Bible)");
        Console.Write("Idioma: ");

        string? idioma = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(idioma))
        {
            Console.WriteLine("Idioma inválido.");
            return;
        }

        string sufixoUrl;
        string exemplos;

        switch (idioma.Trim())
        {
            case "1":
                sufixoUrl = "?translation=almeida";
                exemplos = "Gênesis 1 | Salmos 23 | Mateus 5:1-12 | João 3:16";
                break;

            case "2":
                sufixoUrl = string.Empty;
                exemplos = "Genesis 1 | Psalms 23 | Matthew 5:1-12 | John 3:16";
                break;

            default:
                Console.WriteLine("Idioma inválido. Escolha 1 ou 2.");
                return;
        }

        Console.WriteLine();
        Console.WriteLine("Introduza uma referência bíblica.");
        Console.WriteLine($"Exemplos: {exemplos}");
        Console.Write("Referência: ");

        string? referencia = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(referencia))
        {
            Console.WriteLine("Não foi indicada nenhuma referência.");
            return;
        }

        // Codifica espaços, acentos e outros caracteres para o URL.
        string referenciaCodificada = Uri.EscapeDataString(referencia.Trim());
        string url =
            $"https://bible-api.com/{referenciaCodificada}{sufixoUrl}";

        await ConsultarPassagemAsync(url);
    }

    private static async Task ConsultarPassagemAsync(string url)
    {
        Console.WriteLine("A estabelecer ligação à API...");
        Console.WriteLine($"Endpoint: {url}");
        Console.WriteLine();

        try
        {
            // 1. Envia uma requisição HTTP GET para a API.
            using HttpResponseMessage resposta =
                await HttpClient.GetAsync(url);

            // 2. Verifica se o servidor respondeu com sucesso.
            if (resposta.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine("Erro: a passagem indicada não foi encontrada (404).");
                return;
            }

            resposta.EnsureSuccessStatusCode();

            // 3. Lê o conteúdo JSON recebido.
            await using Stream fluxo = await resposta.Content.ReadAsStreamAsync();

            // 4. Converte (desserializa) o JSON para objetos C#.
            BibleResponse? dados =
                await JsonSerializer.DeserializeAsync<BibleResponse>(
                    fluxo, JsonOptions);

            if (dados?.Verses is null || dados.Verses.Count == 0)
            {
                Console.WriteLine("A API não devolveu nenhum versículo.");
                return;
            }

            // 5. Apresenta os dados ao utilizador.
            Console.WriteLine("CONSULTA REALIZADA COM SUCESSO!");
            Console.WriteLine($"Referência: {dados.Reference}");
            Console.WriteLine($"Tradução: {dados.TranslationName} ({dados.TranslationId})");
            Console.WriteLine($"Versículos recebidos: {dados.Verses.Count}");

            if (!string.IsNullOrWhiteSpace(dados.TranslationNote))
            {
                Console.WriteLine($"Nota: {dados.TranslationNote}");
            }

            Console.WriteLine("----------------------------------------------");

            foreach (Verse versiculo in dados.Verses)
            {
                Console.WriteLine(
                    $"{versiculo.BookName} {versiculo.Chapter}:{versiculo.Number}");
                Console.WriteLine(versiculo.Text.Trim());
                Console.WriteLine("----------------------------------------------");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("ERRO DE COMUNICAÇÃO COM A API.");
            Console.WriteLine($"Detalhes: {ex.Message}");

            if (ex.StatusCode is not null)
            {
                Console.WriteLine(
                    $"Código HTTP: {(int)ex.StatusCode} ({ex.StatusCode})");
            }

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
