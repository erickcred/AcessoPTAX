namespace Cotacao.DTO.Requests;

/// <summary>
/// Passagem da data em que se deseja o retorno da cotação <br />
/// Deve ser passado a data com dia retroativo, para que o processo possa retornar o dado (TipoBoletim > Fechamento PTAX)
/// </summary>
/// <param name="DataCotacao"></param>
public record CotacaoDolarRequest(
  DateTime DataCotacao
);