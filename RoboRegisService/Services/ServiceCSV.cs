using RoboRegisService.Model;

namespace RoboRegisService.Services;
public class ServiceCSV
{
    public void TrasformarCSV(List<Produtos> item)
    {
        /**Metodo responsavel por Transformar o List Produtos em arquivo CSV**/
        try
        {
            int count = 1;
            using (var escritor = new StreamWriter("Registros-CSV.csv"))
            {
            foreach (var itens in item) 
                {                
                    Convert.ToDateTime(itens.dataVencimento).ToString("d");
                    escritor.WriteLine($"{count};{itens.registro};{itens.razaoSocial};{itens.dataVencimento};{itens.descSituacao};{itens.descTipo};");
                    count++;
                }
            }
            System.Console.WriteLine("CSV Criado");
        }
        catch(Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
    }
    
}