using System.Net.Http.Headers;
namespace RoboRegisService.Service;
public class ServiceApi{
    public HttpClient _client;
    private HttpResponseMessage _serviceResponse;
    public ServiceApi(){
        _client = new HttpClient();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _client.DefaultRequestHeaders.Add("Authorization","Guest");
        _serviceResponse = new HttpResponseMessage();
    }

    #region ConsumirApi
    // Metodo responsavel por consumir a A API da ANVISA apartir da Injeção de dependencia no Construtor
     public async Task<string> ConsumirApi(string Url){        
        _serviceResponse = await _client.GetAsync(Url); // Resposta da API
        await Task.Delay(5000); // Delay de 5s para verificar o status da respota
        if(_serviceResponse.IsSuccessStatusCode){ //verificacao de status 200
            var content = await _serviceResponse.Content.ReadAsStringAsync(); // tranforma a resposta em string
            return content;// retorna a jesposta   
        }
        else{
            Console.ForegroundColor = ConsoleColor.Red;
            throw new Exception($"-Erro na Requisicao: {_serviceResponse.StatusCode}");
        }
    }
    #endregion
}