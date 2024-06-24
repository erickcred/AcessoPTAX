using System.Text.Json;
using Cotacao.Model;

namespace Cotacao.Services;
public class ApiService
{
  private string _baseUrl = "https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/";
  private readonly HttpClient _httpClient;
  JsonSerializerOptions _serializerOptions;

  public ApiService(HttpClient httpClient)
  {
    _httpClient = httpClient;
    _serializerOptions = new JsonSerializerOptions
    {
      PropertyNameCaseInsensitive = true
    };
  }

  /// <summary>
  /// Retorna a Cotação de Compra e a Cotação de Venda da moeda Dólar contra a unidade monetária corrente para a data informada.
  /// </summary>
  /// <remarks>
  /// <code>
  /// dataCotacao > dd-MM-yyyy
  /// </code>
  /// <remarks>
  /// <param name="dataCotacao">dd-MM-yyyy</param>
  /// <returns></returns>
  public async Task<APiServiceResponse<CotacaoDolar>> RetornarCotacaoDolarDia(DateTime dataCotacao)
  {
    var dataConvertida = dataCotacao.ToString("MM-dd-yyyy");
    APiServiceResponse<CotacaoDolar> apiServiceResponse;

    try
    {
      var response = await GetRequestAsync(
        $"CotacaoDolarDia(dataCotacao=@dataCotacao)?%40dataCotacao='{dataConvertida}'&%24format=json&%24top=100");
      if (response.IsSuccessStatusCode)
      {
        var result = await response.Content.ReadAsStringAsync();
        
        var json = JsonSerializer.Deserialize<PTAX<CotacaoDolar>>(result, _serializerOptions);
        apiServiceResponse = new APiServiceResponse<CotacaoDolar>
        {
          Data = json!.Values,
          StatusCode = response.StatusCode.ToString(),
        };

        return apiServiceResponse;
      }
      apiServiceResponse = new APiServiceResponse<CotacaoDolar>
      {
        Data = new List<CotacaoDolar>(),
        MensagemErro = $"Erro ao realizar consulta PTAX: {response.Content}",
        Erro = true,
        StatusCode = response.StatusCode.ToString(),
      };
      return apiServiceResponse;
    }
    catch (JsonException ex)
    {
      apiServiceResponse = new APiServiceResponse<CotacaoDolar>
      {
        Data = new List<CotacaoDolar>(),
        MensagemErro = $"Erro na desserialização do objeto: {ex.Message}",
        Erro = true,
        StatusCode = "0",
      };
      return apiServiceResponse;
    }
    catch (Exception ex)
    {
      apiServiceResponse = new APiServiceResponse<CotacaoDolar>
      {
        Data = new List<CotacaoDolar>(),
        MensagemErro = $"Erro inesperado na comunicação: {ex.Message}",
        Erro = true,
        StatusCode = "0",
      };
      return apiServiceResponse;
    }
  }

  /// <summary>
  /// Retornas as Moedas contidas no endpoint da PTAX
  /// </summary>
  /// <returns></returns>
  public async Task<APiServiceResponse<Moeda>> RetornarMoedas()
  {
    APiServiceResponse<Moeda> apiServiceResponse;

    try
    {
      var response = await GetRequestAsync("Moedas?%24format=json");
      if (response.IsSuccessStatusCode)
      {
        var result = await response.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<PTAX<Moeda>>(result, _serializerOptions);

        apiServiceResponse = new APiServiceResponse<Moeda>
        {
          Data = json!.Values,
          StatusCode = response.StatusCode.ToString()
        };
        return apiServiceResponse;
      }

      apiServiceResponse = new APiServiceResponse<Moeda>
      {
        Data = new List<Moeda>(),
        Erro = true,
        MensagemErro = $"Erro ao realizar consulta: PTAX {response.Content}",
      };
      return apiServiceResponse;      
    }
    catch (JsonException ex)
    {
      apiServiceResponse = new APiServiceResponse<Moeda>
      {
        Data = new List<Moeda>(),
        MensagemErro = $"Erro na desserialização do objeto: {ex.Message}",
        Erro = true,
        StatusCode = "0",
      };
      return apiServiceResponse;
    }
    catch (Exception ex)
    {
      apiServiceResponse = new APiServiceResponse<Moeda>
      {
        Data = new List<Moeda>(),
        MensagemErro = $"Erro inesperado na comunicação: {ex.Message}",
        Erro = true,
        StatusCode = "0",
      };
      return apiServiceResponse;
    }
  }

  /// <summary>
  /// Retorna os boletins diários com a Paridade de Compra e a Paridade de Venda, a Cotação de Compra e a Cotação de Venda para a data da moeda consultada. São cinco boletins para cada data, um de abertura, três intermediários e um de fechamento.
  /// </summary>
  /// <remarks>
  /// <code>
  /// simboloMoeda > Exp: 'USD'
  /// dataCotacao > dd-MM-yyyy
  /// </code>
  /// </remarks>
  /// <param name="simboloMoeda">Exp: 'USD'</param>
  /// <param name="dataCotacao">dd-MM-yyyy</param>
  /// <returns></returns>
  public async Task<APiServiceResponse<CotacaoMoedaDia>> RetornarCotacaoMoedaDia(string simboloMoeda, DateTime dataCotacao)
  {
    var dataConvertida = dataCotacao.ToString("MM-dd-yyyy");
    APiServiceResponse<CotacaoMoedaDia> apiServiceResponse;

    try
    {
      var response = GetRequestAsync(
        $"CotacaoMoedaDia(moeda=@moeda,dataCotacao=@dataCotacao)?%40moeda='{simboloMoeda}'&%40dataCotacao='{dataConvertida}'&%24format=json").Result;
      if (response.IsSuccessStatusCode)
      {
        var result = await response.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<PTAX<CotacaoMoedaDia>>(result, _serializerOptions);

        apiServiceResponse = new APiServiceResponse<CotacaoMoedaDia>
        {
          Data = json!.Values,
          StatusCode = response.StatusCode.ToString()
        };
        return apiServiceResponse;
      }

      apiServiceResponse = new APiServiceResponse<CotacaoMoedaDia>
      {
        Data = new List<CotacaoMoedaDia>(),
        Erro = true,
        MensagemErro = $"Erro ao realizar consulta: PTAX {response.Content}",
      };
      return apiServiceResponse;
    }
    catch (JsonException ex)
    {
      apiServiceResponse = new APiServiceResponse<CotacaoMoedaDia>
      {
        Data = new List<CotacaoMoedaDia>(),
        MensagemErro = $"Erro na desserialização do objeto: {ex.Message}",
        Erro = true,
        StatusCode = "0",
      };
      return apiServiceResponse;
    }
    catch (Exception ex)
    {
      apiServiceResponse = new APiServiceResponse<CotacaoMoedaDia>
      {
        Data = new List<CotacaoMoedaDia>(),
        MensagemErro = $"Erro inesperado na comunicação: {ex.Message}",
        Erro = true,
        StatusCode = "0",
      };
      return apiServiceResponse;
    }
  }

  #region Requests
  private async Task<HttpResponseMessage> GetRequestAsync(string uri)
  {
    var enderecoUrl = $"{_baseUrl}{uri}";
    try
    {
      var request = new HttpRequestMessage(HttpMethod.Get, enderecoUrl);
      var responseResult = await _httpClient.SendAsync(request);

      return responseResult;
    }
    catch (HttpRequestException ex)
    {
      throw new HttpRequestException(ex.Message);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
  }
  #endregion
}
