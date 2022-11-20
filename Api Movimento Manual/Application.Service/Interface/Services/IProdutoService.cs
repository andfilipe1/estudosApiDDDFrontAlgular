using Domain.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.Service.Interface
{
    public interface IProdutoService
    {
        healthcheck Get();
        void InsertProduct(Product product);
        Task<ActionResult<List<MovementManualScreem>>> GetAll();
        Task<ActionResult<List<GetAllPRoduct>>> GetAllProduct();
        List<GetAllPRoductCosif> GetAllProductCosif(int CodProduto);


    }
}
