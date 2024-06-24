using System.Text.Json.Serialization;

namespace Cotacao.Model;
public class PTAX<T>
{
  [JsonPropertyName("@odata.context")]
  public string DataContext { get; set; } = string.Empty;
  [JsonPropertyName("value")]
  public ICollection<T>? Values { get; set; }
}