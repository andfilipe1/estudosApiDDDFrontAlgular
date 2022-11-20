using Application.Service.Interface;
using Application.Service.Interface.Repositories;
using Domain.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace Application.Service
{

    public class ProdutoService : IProdutoService
    {
        private readonly IProductRepository _productRepository;
        public ProdutoService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public healthcheck Get() => new(Description: "Health Api");

        public async Task<ActionResult<List<MovementManualScreem>>> GetAll()
        {
            IEnumerable<MovementManualScreem> Get = await _productRepository.GetProduct();
            return Get.ToList();
        }

        public async Task<ActionResult<List<GetAllPRoduct>>> GetAllProduct()
        {
            IEnumerable<GetAllPRoduct> Get = await _productRepository.GetAllProduct();
            return Get.ToList();
        }

        public List<GetAllPRoductCosif> GetAllProductCosif(int CodProduto)
        {
            IEnumerable<GetAllPRoductCosif> Get =  _productRepository.GetAllProductCosif(CodProduto);
            return Get.ToList();
        }


        public void InsertProduct(Product product)
        {
            using TransactionScope scope = new TransactionScope();
            _productRepository.InsertProduct(product);

            scope.Complete();
        }

      }
}
