using Cotacao.DTO.Requests;
using Cotacao.Services;

var cotacaoService = new ApiService(new HttpClient());
DateTime dataHoje = DateTime.Now;

//var caotacaoDia = await cotacaoService.RetornarCotacaoDolarDia(new CotacaoDolarRequest(dataHoje));
// Console.WriteLine(caotacaoDia.Erro);
// Console.WriteLine(caotacaoDia.StatusCode);
// Console.WriteLine(caotacaoDia.MensagemErro);
// foreach (var item in caotacaoDia.Data!)
//   Console.WriteLine(item.CotacaoVenda);

var moedas = await cotacaoService.RetornarMoedas();
var cotacaoMoedaDia = await cotacaoService.RetornarCotacaoMoedaDia(new CotacaoMoedaDiaRequest("EUR", dataHoje));


