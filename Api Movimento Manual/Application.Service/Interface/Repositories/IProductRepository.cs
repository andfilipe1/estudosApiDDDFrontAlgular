using Domain.Model.Entities;

namespace Application.Service.Interface.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<MovementManualScreem>> GetProduct();
        Task<IEnumerable<GetAllPRoduct>> GetAllProduct();
        List<GetAllPRoductCosif> GetAllProductCosif(int CodProduto);
        void InsertProduct(Product produto);
        List<MovementManualScreem> LaunchValidated(Product produto);

    }
}
