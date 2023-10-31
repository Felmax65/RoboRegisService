using OfficeOpenXml;
using RoboRegisService.Model;
using RoboRegisService.Model.ModelJson.Vigente;

namespace RoboRegisService.Service;
public class ServicePlanilha{
    private List<Registro> _registros;
    public ServicePlanilha(){
       _registros = new List<Registro>();
    }

    #region ConverterRegistros
    //Metodo Responsavel por transformar a planilha de entrada em um LIST
    public List<Registro> ConverterRegistros(){    
        
        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // Atribui o tipo de licença da planilha
        var caminho = @"C:\RoboRegis\Dados-RoboRegis\Entrada\reg.xlsx";// Caminho da planilha de entrada

        if (caminho != null){ //verificacao de caminho nulo
            var ep = new ExcelPackage(new FileInfo(caminho)); //Iniciliazação para leitura da planilha
            var worksheet = ep.Workbook.Worksheets["Registros"]; // Planilha e aba para realizacao da leitura
            var row = worksheet.Dimension.End.Row; // Quantidade maxima de linhas no arquivo

            for (int rw = 2; rw <= row; rw++)
            {
                string registro = worksheet.Cells[rw, 1].Value?.ToString().Trim(); // Faz a leitura da primeira linha da coluna 1 
                string processo = worksheet.Cells[rw, 2].Value?.ToString().Trim(); // Faz a leitura da primeira linha da coluna 2  

                if (!string.IsNullOrWhiteSpace(registro)){ // Verifica se há espaços nulos ou em branco na planilha
                    _registros.Add(new Registro{
                        NmRegistro = registro,
                        NmProcesso = processo
                    }); // Converte os dados das 2 colunas em um LIST de acordo com a classe Generica Registro
                }   
                
            }
            return _registros;  // retorna o List do tipo Registros
        }
        else{
            Console.ForegroundColor = ConsoleColor.Red; 
            throw new Exception ($"-Algo de errado ocorreu com a planilha");
        }          
    }
    #endregion
   
    #region GerarPlanilha
    //Metodo Responsavel por gerar a planilha apartir o List da classe generica RootVigente
    public void GerarPlanilha(List<RootVigente> vigentes){

        try{
            
            var data = DateTime.Now.ToString("d");
            var caminho = @$"C:\RoboRegis\\Dados-RoboRegis\Saida\Registros-{data.Replace("/","")}.xlsx"; //Caminho para salvar a planilha
            if (caminho != null && vigentes != null){ //Verificação de nulo
                using (var package = new ExcelPackage(new FileInfo(caminho))){

                    var vigentesSheet = package.Workbook.Worksheets.Add("Registros_Vigentes");

                    // Adiciona as colunas iniciais
                    vigentesSheet.Cells[1, 1].Value = "Produto";
                    vigentesSheet.Cells[1, 2].Value = "Registro";
                    vigentesSheet.Cells[1, 3].Value = "Processo";
                    // Adiciona as colunas adicionais                    
                    vigentesSheet.Cells[1, 4].Value = "CNPJ";
                    vigentesSheet.Cells[1, 5].Value = "Razão Social";
                    vigentesSheet.Cells[1, 6].Value = "Cancelado";
                    vigentesSheet.Cells[1, 7].Value = "Data Cancelado";
                    vigentesSheet.Cells[1, 8].Value = "Data Vencimento";
                    vigentesSheet.Cells[1, 9].Value = "Descrição";

                    int row = 2; // Começa a partir da segunda linha

                    foreach (var root in vigentes){
                        // Preenche as colunas iniciais
                        vigentesSheet.Cells[row, 1].Value = root.produto;
                        vigentesSheet.Cells[row, 2].Value = root.registro;
                        vigentesSheet.Cells[row, 3].Value = root.processo;

                        if (root.empresa != null){
                            vigentesSheet.Cells[row, 4].Value = root.empresa.cnpj;
                            vigentesSheet.Cells[row, 5].Value = root.empresa.razaoSocial;
                        }
                        
                        if(root.cancelado == "true"){
                            vigentesSheet.Cells[row, 6].Value = root.cancelado = "CANCELADO";
                        }
                        else{
                            vigentesSheet.Cells[row, 6].Value = root.cancelado = "";    
                        }  
                        vigentesSheet.Cells[row, 7].Value = root.dataCancelamento;

                        if (root.vencimento != null){
                            vigentesSheet.Cells[row, 8].Value = root.vencimento.data;
                            vigentesSheet.Cells[row, 9].Value = root.vencimento.descricao;
                        }

                        row++; // Avança para a próxima linha
                    }

                    package.Save(); //Salva a planilha
                    Console.ForegroundColor = ConsoleColor.Green;   
                    Console.WriteLine("-Planilha gerada com sucesso em C:\\RoboRegis\\Dados-RoboRegis\\Saida\\");         
                }   
            }
        }
        catch(Exception e){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"-Erro ao gerar a planilha: {e.Message}");
        }
    }
    #endregion
}