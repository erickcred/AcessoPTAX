namespace Cotacao.Model;

public class CotacaoDolar
{
  public double CotacaoCompra { get; set; }
  public double CotacaoVenda { get; set; }
  public string? DataHoraCotacao { get; set; }
  // public string? TipoBolitim { get; set; }

  public override string ToString()
  {
    return $"Data Cotação: {DataHoraCotacao} \nCompra: {CotacaoCompra}, Venda: {CotacaoVenda}";
  }
}