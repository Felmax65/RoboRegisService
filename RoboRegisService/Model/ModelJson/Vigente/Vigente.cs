namespace RoboRegisService.Model.ModelJson.Vigente;
public class Empresa{
    public string cnpj { get; set; }
    public string razaoSocial { get; set; }
    public string autorizacao { get; set; }
}

public class Mensagem{
    public string situacao { get; set; }
    public string resolucao { get; set; }
    public string motivo { get; set; }
    public bool negativo { get; set; }
}

public class Apresentacao{
    public string modelo { get; set; }
    public string componente { get; set; }
    public string apresentacao { get; set; }
}

public class Fabricante{
    public string atividade { get; set; }
    public string razaoSocial { get; set; }
    public string pais { get; set; }
    public string local { get; set; }
}

public class Risco{
    public string sigla { get; set; }
    public string descricao { get; set; }
}

public class Vencimento{
    public string data { get; set; }
    public string descricao { get; set; }
}

public class Arquivo{
    public string anexoCod { get; set; }
    public string nuExpediente { get; set; }
    public string nomeArquivo { get; set; }
    public string tipoAnexo { get; set; }
    public string tipoArquivo { get; set; }
    public string dtEnvio { get; set; }
    public string nuProcesso { get; set; }
    public string descricaoTipoAnexo { get; set; }
    public string nomeCompleto { get; set; }
}

public class RootVigente{
    public string produto { get; set; }
    public Empresa empresa = new Empresa();
    public Mensagem mensagem = new Mensagem();
    public string nomeTecnico { get; set; }
    public string registro { get; set; }
    public string cancelado { get; set; }
    public string dataCancelamento { get; set; }
    public string processo { get; set; }
    public List<Apresentacao> apresentacoes = new List<Apresentacao>();
    public List<Fabricante> fabricantes =  new List<Fabricante>();
    public Risco risco = new Risco();
    public Vencimento vencimento = new Vencimento();
    public string publicacao { get; set; }
    public string apresentacaoModelo { get; set; }
    public List<Arquivo> arquivos = new List<Arquivo>();
    public string processoMedidaCautelar { get; set; }
    public string tooltip { get; set; }
}
