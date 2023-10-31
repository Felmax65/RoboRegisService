using RoboRegisService.View;
namespace RoboRegisService.Service;
public class ServiceRobo{
    private ServiceConsulta _serviceConsulta;
    private ServiceJson _serviceJson;
    private Apresentacao _apresentacao;
    public ServiceRobo(){        
        _serviceConsulta = new ServiceConsulta();
        _serviceJson = new ServiceJson();
        _apresentacao = new Apresentacao();
    }

    #region Consultar Registros
    /*Metodo responsavel por consultar os registros consumindo a API da ANVISA*/
    public async Task<string> ConsultarRegistros(){ 
        _apresentacao.MsgInicial(); //Chamar o metodo para apresentar a mensagem inicial do console
        var consultas = await _serviceConsulta.Consulta(); //Retorna um List String com todos os jsons consultados na API
        _serviceJson.Desserializar(consultas); //Desserializar o List String para o tipo List da classe generica RootVigente
        var msg = "OK";
        return msg;              
    }
    #endregion
}