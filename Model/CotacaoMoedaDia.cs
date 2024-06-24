namespace Cotacao.Model;
public class CotacaoMoedaDia
{
  public decimal ParidadeCompra { get; set; }
  public decimal ParidadeVenda { get; set; }
  public decimal CotacaoCompra { get; set; }
  public decimal CotacaoVenda { get; set; }
  public string DataHoraCotacao { get; set; } = string.Empty;
  public string TipoBoletim { get; set; } = string.Empty;
}