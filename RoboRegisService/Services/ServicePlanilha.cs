using OfficeOpenXml; 
using RoboRegisService.Model;

namespace RoboRegisService.Services;
public class ServicePlanilha
{
    public List<Registros> TranformarList2()
    {
        /**
            Metodo respnsave por ler a planilha com os registros na coluna 2 
            e tranformar e retornar uma Lista do tipo String
        **/
        List<Registros> reg = new List<Registros>();
        try
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var caminho = @"C:\RoboRegis\Dados\Entrada\registros.xlsx";
            //caminho=@"G:\Meu Drive\registros.xlsx";
            if (caminho == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("-Caminho da planilha ou nome da planilha de entrada estão errados!!!");
                return reg = null;
            }
            var ep = new ExcelPackage(new FileInfo(caminho));
            var worksheet = ep.Workbook.Worksheets["Registros_Geral"];

            for (int rw = 2; rw <= worksheet.Dimension.End.Row; rw++)
            {
                string registro = worksheet.Cells[rw, 2].Value?.ToString();
                string status = worksheet.Cells[rw, 3].Value?.ToString();

                if (!string.IsNullOrWhiteSpace(registro))
                    reg.Add(new Registros
                    {
                        Registro = registro,
                        Status = status
                    });
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        var regFiltrado = reg.Where(x => x.Status != "VENCIDO" && x.Status != "CANCELADO").ToList();

        return regFiltrado;
    }

    public void ConverterItemtoPlanilhaList(List<Produtos> item)
    {
        /**
            Metodo reponsavel por o List do tipo Produtos para planilha
        **/
        try
        {
            if(item != null){              

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Registros");

                    // Cabeçalhos
                    CabecalhoExcel(worksheet);

                    // Preencher os dados            
                    PreencherCelulas(item, worksheet);

                    // Salvar a planilha no arquivo
                    SalvarPlanilha(package);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("-Planilha gerada com sucesso!.\nSalvo em C:\\RoboRegis\\Dados\\Saida\\.\n\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else{
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("-Falha ao criar a planilha : Lista de produtos nula \n\n");
                Console.ForegroundColor = ConsoleColor.White;
            }    

            
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    private static void SalvarPlanilha(ExcelPackage package)
    {
        /**
            Metodo Responsavel por salvar a planilha
        **/
        try
        {
            byte[] bin = package.GetAsByteArray();
            var data = DateTime.Now.ToString("d").Replace("/","");    
            File.WriteAllBytes(@$"C:\RoboRegis\Dados\Saida\Registros_Anvisa-{data}.xlsx", bin);

        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    private static void PreencherCelulas(List<Produtos> item, ExcelWorksheet worksheet)
    {
        /**
            Metodo responsavel por Preencher as Linhas da planilhas
        **/
        try
        {
            for (int i = 0; i < item.Count; i++)
            {
                var itens = item[i]; 
                worksheet.Cells[i + 2, 1].Value = Convert.ToInt32(i+1);
                worksheet.Cells[i + 2, 2].Value = itens.nomeProduto;
                worksheet.Cells[i + 2, 3].Value = itens.processo;
                worksheet.Cells[i + 2, 4].Value = itens.registro;
                worksheet.Cells[i + 2, 5].Value = itens.razaoSocial;
                worksheet.Cells[i + 2, 6].Value = itens.cnpj;
                worksheet.Cells[i + 2, 7].Value = Convert.ToDateTime(itens.dataVencimento).ToString("d");
                worksheet.Cells[i + 2, 8].Value = itens.descSituacao;
            }

        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    private static void CabecalhoExcel(ExcelWorksheet worksheet)
    {   
        /**Metodo Responsavel por definir o nome do cabecalho das colunas**/
        try
        {
            worksheet.Cells[1, 1].Value = "ID Produto";
            worksheet.Cells[1, 2].Value = "Nome do Produto";
            worksheet.Cells[1, 3].Value = "Processo";
            worksheet.Cells[1, 4].Value = "Registro";
            worksheet.Cells[1, 5].Value = "Razão Social";
            worksheet.Cells[1, 6].Value = "CNPJ";
            worksheet.Cells[1, 7].Value = "Data de Vencimento";
            worksheet.Cells[1, 8].Value = "Descrição da Situação";

        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
}