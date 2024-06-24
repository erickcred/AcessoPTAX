namespace Cotacao.DTO.Responses;
public class APiServiceResponse<T> where T : class
{
  public ICollection<T>? Data { get; set; }
  public string? MensagemErro { get; set; } = string.Empty;
  public bool Erro { get; set; } = false;
  public string? StatusCode { get; set; }
        
}