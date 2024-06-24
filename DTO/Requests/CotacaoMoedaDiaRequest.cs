namespace Cotacao.DTO.Requests;

/// <summary>
/// Passagem da data em que se deseja o retorno da cotação e o Simbolo da Moeda ('USD', 'EUR') <br />
/// Deve ser passado a data com dia retroativo, para que o processo possa retornar o dado (TipoBoletim > Fechamento PTAX)
/// </summary>
/// <param name="SimboloMoeda"></param>
/// <param name="DataCotacao"></param>
public record CotacaoMoedaDiaRequest(
  string SimboloMoeda,
  DateTime DataCotacao
)
{
  public string Simbolo()
  {
    return SimboloMoeda.ToUpper();
  }
}