using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Entities
{
    public record healthcheck(string Description);

    public class GetAllPRoduct
    {
        public string COD_PRODUTO { get; set; }
        public string  DES_PRODUTO { get; set; }
        public string  STA_STATUS { get; set; }
    }

    public class GetAllPRoductCosif
    {
        public string COD_PRODUTO { get; set; }
        public string COD_COSIF { get; set; }
        public string COD_CLASSIFICACAO { get; set; }
        public string STA_STATUS { get; set; }
    }

    public class MovementManualScreem
    {
        public int DAT_MES { get; set; }
        public int DAT_ANO { get; set; }
        public int COD_PRODUTO { get; set; }
        public string? DES_PRODUTO { get; set; }
        public string? DES_DESCRICAO { get; set; }
        public int VAL_VALOR { get; set; }
        public int? NUM_LANCAMENTO { get; set; }


    }

    //public class MovimentoManualTela
    //{
    //    public int DataMes { get; set; }
    //    public int DataAno { get; set; }
    //    public int Id { get; set; }
    //    public string? DescricaoProduto { get; set; }
    //    public int NumeroLancamento { get; set; }
    //    public string? Descricao { get; set; }
    //    public int ValValor { get; set; }

    //}



    public class Product
    {
        //public string? Id { get; set; }
        public string? DescProduto { get; set; }
        public string? Status { get; set; }
        public PordutoCosif? PordutoCosif { get; set; }
        public MovimentoManual? MovimentoManual { get; set; }

    }

    public class PordutoCosif
    {
        //public string? IdCosif { get; set; } = string.Empty;
        public string? CodClassificacao { get; set; }

    }


    public class MovimentoManual
    {
        public int? DataMes { get; set; }
        public int? DataAno { get; set; }
        public string? Descricao { get; set; }
        public string? IdUsuario { get; set; }
        public decimal? ValValor { get; set; }

    }

    public class MovimentoManualUtil
    {
        public int? NumeroLancamento { get; set; }
        public DateTime? DataMovimento { get; set; }
    }


}
