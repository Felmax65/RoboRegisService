using System.Net;
using RoboRegisService.Model;

namespace RoboRegisService.Services;
public class ServiceAnvisaAPI
{   
    private ServiceJson _serviceJson;
    public ServiceAnvisaAPI()
    {
        _serviceJson = new ServiceJson();
    }
  
    public async Task<List<string>> ConsumirAPI3(HttpClient client, List<Registros> registros)
    {
        //Declaracao List Contetens
        List<string> contents = new List<string>();
        HttpResponseMessage response ;
        string content;
        try
        {
            //Adiciona o Guest ao Header da API  
            client.DefaultRequestHeaders.Add("Authorization", "Guest");

            //Insercao de Json no List contents
            foreach (var itens in registros)
            {
                //URL para chamar o metodo GET de consulta da API ANVISA
                string url = $"https://consultas.anvisa.gov.br/api/consulta/genericos?count=10&filter%5BnumeroRegistro%5D={itens.Registro}&page=1";

                //Resposta da API
                response = await client.GetAsync(url);
                if(response.StatusCode == HttpStatusCode.InternalServerError){
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("-API Offiline");
                    break;
                }
                if (response.StatusCode == HttpStatusCode.OK){                    
                    content = await response.Content.ReadAsStringAsync();
                    contents.Add(content);
                }
                else{
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine($"-Registro do item: {itens.Registro} \nCodigo do status = {response.StatusCode}");
                    contents = null;                    
                } 
            }
                
        }
        catch(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine($"-Erro ao se conectar com a API :",e.Message);
            
        }

        return contents;
    } 
   
}