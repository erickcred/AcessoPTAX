namespace Cotacao.Model;

public class CotacaoDolar
{
  public double CotacaoCompra { get; set; }
  public double CotacaoVenda { get; set; }
  public string DataHoraCotacao { get; set; } = string.Empty;
}