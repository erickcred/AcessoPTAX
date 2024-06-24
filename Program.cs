using Cotacao.Services;

var cotacaoService = new ApiService(new HttpClient());
DateTime dataHoje = DateTime.Now.AddDays(-3);
// Console.WriteLine(cotacaoService.RetornarCotacaoDolarDia(dataHoje));

// var caotacaoDia = await cotacaoService.RetornarCotacaoDolarDia(dataHoje);
// Console.WriteLine(caotacaoDia.Erro);
// Console.WriteLine(caotacaoDia.StatusCode);
// Console.WriteLine(caotacaoDia.MensagemErro);
// foreach (var item in caotacaoDia.Data!)
//   Console.WriteLine(item.CotacaoVenda);

var moedas = await cotacaoService.RetornarMoedas();
var cotacaoMoedaDia = await cotacaoService.RetornarCotacaoMoedaDia("USD", dataHoje);


