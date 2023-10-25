using RoboRegisService.Model;
using Newtonsoft.Json;

namespace RoboRegisService.Services;
public class ServiceJson
{
    private ProdutosContent _produtos;
    private ServiceCSV _serviceCSV;
    private List<Produtos> _items;
    public ServiceJson()
    {
        _produtos = new ProdutosContent();   
        _items = new List<Produtos>();
        _serviceCSV = new ServiceCSV();
    } 
    public List<Produtos> DesserializarJson(List<string> registros)
    {   
        try
        {
            //Declaracao Tipo Root
            _produtos = new ProdutosContent();

            //Declaracao List Items por Injecao
            _items = new List<Produtos>();
            
            if(registros != null){
            //Conversao de Json para List do tipo Generico
                foreach (var item in registros) 
                {
                    _produtos = JsonConvert.DeserializeObject<ProdutosContent>(item)!;
                    _items.AddRange(_produtos.content);
                }    
            }
            else{
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("-Lista de produtos em branco");               
            }

        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return _items;
    }    
    public void DesserializarRespostas(List<ProdutosContent> contents, string respostas)
    {
        /**
            Desserializa a string de respostas para o tipo ProdutoContent facilitando leitura a conversão para Classe Produtos
        **/
        ProdutosContent resposta = JsonConvert.DeserializeObject<ProdutosContent>(respostas)!;
        contents.Add(resposta);
    } 
}