using RoboRegisService.Model;

namespace RoboRegisService.Services;
public class ServiceRobo
{
    private HttpClient _httpclient;
    private ServiceJson _serviceJson;   
    private ServicePlanilha _servicePlanilha;
    private ServiceAnvisaAPI _anvisaApi;
    private Apresentacao _apresentacao;
    
    public ServiceRobo()
    {
        _serviceJson = new ServiceJson();       
        _servicePlanilha = new ServicePlanilha();
        _anvisaApi = new ServiceAnvisaAPI();
        _httpclient  = new HttpClient();
        _apresentacao = new Apresentacao();
    }   
    private HttpClient GetHttpClient()
    {       
        //retorna o cliente Http
        return _httpclient;
    }    
    
    public async Task ConsultarProdutosPlanilhaFiltrada()//Metodo gera um planilha com todas as Consultas
    {
        _apresentacao.MsgInicial();
        try
        {
            using (HttpClient client = new HttpClient())
            {
                //Lista de Registros para consultar
                List<Registros> registros = RegistrosParaConsultarFiltrada();

                if(registros == null){
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("-Lista de consulta em Branco");
                }

                //retorna o cliente Http
                var cliente = GetHttpClient();

                //Consumir Api da Anvisa
                List<string> contents = await _anvisaApi.ConsumirAPI3(cliente, registros);

                if(contents == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("\n\n-Lista de conteudo em branco");
                }

                //Desseriaizar o List Contents para o Tipo Class Produtos
                List<Produtos> produtos = DesserializarList(contents);

                //Convert o List Produto para Planilha
                ConverterParaPlanilha(produtos);
            }
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }   
    private void ConverterParaPlanilha(List<Produtos> produtos)
    {
        //Converte o List para Planilha
        _servicePlanilha.ConverterItemtoPlanilhaList(produtos);
    }  

    private List<Produtos> DesserializarList(List<string> contents)
    {
        //Desserializar o List
        return _serviceJson.DesserializarJson(contents);
    }

    private List<Registros> RegistrosParaConsultarFiltrada()
    {        
        //Retorna uma Lista com todos os registros para consultar na API
        return _servicePlanilha.TranformarList2();
    }

}