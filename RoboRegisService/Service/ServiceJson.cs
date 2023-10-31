using RoboRegisService.Model.ModelJson.Vigente;
using Newtonsoft.Json;
namespace RoboRegisService.Service;
public class ServiceJson{    
    private ServicePlanilha _servicePlanilha;
    public ServiceJson(){
        _servicePlanilha = new ServicePlanilha();
    }

    #region Desserializar
    //Desserializa as respostas para o tipo LIST RootVigente
    public void Desserializar(List<string> consultas){        
        List<RootVigente> rootVigentes = new List<RootVigente>();

        try
        {
            foreach (var jsonString in consultas)
            {
                var vigentes = JsonConvert.DeserializeObject<RootVigente>(jsonString); // Desserializacao do json
                rootVigentes.Add(vigentes);  //Armazena resposta desserializada no List    
                         
            }
            _servicePlanilha.GerarPlanilha(rootVigentes);  // Chama o metodo gerar planilha a partir do List do tipo RootVigente         
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"-Erro ao Desserializar as consultas: {e.Message}");
        }
    }

    #endregion
}