using Application.Service.Interface.Repositories;
using Dapper;
using Domain.Model.Entities;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Data.Repository
{
    public class ProdutoRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;

        public ProdutoRepository(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public async Task<IEnumerable<MovementManualScreem>> GetProduct()
        {
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return await conn.QueryAsync<MovementManualScreem>($"PR_LIS_LISTA_MOVIMENTOS_MANUAIS");
        }

        public async Task<IEnumerable<GetAllPRoduct>> GetAllProduct()
        {
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return await conn.QueryAsync<GetAllPRoduct>($"SELECT * FROM PRODUTO");
        }

        public List<GetAllPRoductCosif> GetAllProductCosif(int CodProduto)
        {
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            var produto = (@" select * from PRODUTO_COSIF
                                where COD_PRODUTO = @COD_PRODUTO;
                                ");
            return  conn.Query<GetAllPRoductCosif>(produto, new
            {
                COD_PRODUTO = CodProduto,
            }).ToList();

        }


        public void InsertProduct(Product product)
        {
            var mov = new MovimentoManualUtil();
            var validated = LaunchValidated(product);
            var count = validated.Count;
            mov.DataMovimento = DateTime.Now;
            mov.NumeroLancamento = count += 1;
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            try
            {
                var produto = (@" INSERT INTO PRODUTO 
                                    (DES_PRODUTO
                                    ,STA_STATUS)
                                    VALUES (
                                    @DES_PRODUTO
                                    ,@STA_STATUS);
                                SELECT SCOPE_IDENTITY(); ");
                int res = conn.Query<int>(produto, new
                {
                    DES_PRODUTO = product.DescProduto,
                    STA_STATUS = product.Status
                }).First();




                var produtocosif = (@" INSERT INTO [PRODUTO_COSIF] 
                                        (COD_PRODUTO
                                        ,COD_CLASSIFICACAO
                                        ,STA_STATUS)
                                        VALUES (
                                        @COD_PRODUTO
                                        ,@COD_CLASSIFICACAO
                                        ,@STA_STATUS);
                                        SELECT SCOPE_IDENTITY();");

                int idcosif = conn.Query<int>(produtocosif, new
                {
                    COD_PRODUTO = res,
                    COD_CLASSIFICACAO = product.PordutoCosif.CodClassificacao,
                    STA_STATUS = product.Status
                }).First();


                var movmentomanual = (@" INSERT INTO MOVIMENTO_MANUAL 
                                            (DAT_MES
                                            ,DAT_ANO
                                            ,NUM_LANCAMENTO
                                            ,COD_PRODUTO
                                            ,COD_COSIF
                                            ,DES_DESCRICAO
                                            ,DAT_MOVIMENTO
                                            ,COD_USUARIO
                                            ,VAL_VALOR)
                                            VALUES (
                                             @DAT_MES
                                            ,@DAT_ANO
                                            ,@NUM_LANCAMENTO
                                            ,@COD_PRODUTO
                                            ,@COD_COSIF
                                            ,@DES_DESCRICAO
                                            ,@DAT_MOVIMENTO
                                            ,@COD_USUARIO
                                            ,@VAL_VALOR);");

                conn.Query(movmentomanual, new
                {
                    DAT_MES = product.MovimentoManual.DataMes,
                    DAT_ANO = product.MovimentoManual.DataAno,
                    NUM_LANCAMENTO = mov.NumeroLancamento,
                    COD_PRODUTO = res,
                    COD_COSIF = idcosif,
                    DES_DESCRICAO = product.MovimentoManual.Descricao,
                    DAT_MOVIMENTO = mov.DataMovimento,
                    COD_USUARIO = product.MovimentoManual.IdUsuario,
                    VAL_VALOR = product.MovimentoManual.ValValor
                }); ;

                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        public List<MovementManualScreem> LaunchValidated(Product produto)
        {
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            var sql = (@" SELECT * FROM MOVIMENTO_MANUAL WHERE 
                            DAT_MES = @DAT_MES AND 
                            DAT_ANO = @DAT_ANO");

            return conn.Query<MovementManualScreem>(sql, new
            {
                DAT_MES = produto.MovimentoManual.DataMes,
                DAT_ANO = produto.MovimentoManual.DataAno
            }).ToList();


        }

    }
}