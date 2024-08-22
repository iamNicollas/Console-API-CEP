using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

class Program {
  static async Task Main(string[] args)
  {
    Console.WriteLine("Bem-vindo ao Aplicativo de Busca com CEP!");

    Console.WriteLine("\nDigite o CEP para achar o endereço:");
    string cep = Console.ReadLine();
    cep.Replace("-", "");
    
    string url = $"https://viacep.com.br/ws/{cep}/json/";

    using(HttpClient client = new HttpClient())
    {
      try
      {
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();

        Dados dados = JsonSerializer.Deserialize<Dados>(responseBody);

        Console.WriteLine("\nRua: " + dados.logradouro);
        Console.WriteLine("Bairro: " + dados.bairro);
        Console.WriteLine("Cidade: " + dados.localidade);
        Console.WriteLine("Estado: " + dados.uf);
      }

      catch(HttpRequestException)
      {
        Console.WriteLine($"\nNão foi possível obter o local.");
      }
    }
  }
}

class Dados
{
  public string logradouro {get; set;}
  public string bairro {get; set;}
  public string localidade {get; set;}
  public string uf {get; set;}
}