namespace RoboRegisService.View;

public class Apresentacao{

    public void MsgInicial(){

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("*************************************************SEJA BEM-VINDO(A)!!!**************************************************\n\n*****************************************************ROBO REGIS********************************************************\n\n");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("-Este software é resposavel por fazer consultas dos produtos na base de dados da ANVISA e ao final da execução será\ngerado uma PLANILHA no formato (.xlsx).\n\n-O tempo de execução pode variar de acordo com requisito da maquina e a quantidade de registros a serem consultados.\n\n-O software só poderar ser executado em DIAS UTEIS,pois a API da ANVISA é fechada aos finais de semana,\nimpossibilitando o retorno da consulta.\n\n\nO setor de Tecnologia da informação agradece a compreensão.\n\n\n");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("***********************************NÃO FECHE A JANELA ESPERE ATÉ QUE O PROCESSO TERMINE!!!*****************************\n\n\n\n");
        
    }
}