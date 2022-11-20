using Application.Service.Interface;
using Domain.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProdutoService _productService;
        public ProductController(ILogger<ProductController> logger, IProdutoService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        [Route("HealthCheck")]
        public IActionResult Get()
        {
            return Ok(_productService.Get());
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<MovementManualScreem>> GetAll()
        {
            try
            {
                return Ok(await _productService.GetAll());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAll Products: {ex}");
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("GetAllProduct")]
        public async Task<ActionResult<MovementManualScreem>> GetAllProduct()
        {
            try
            {
                return Ok(await _productService.GetAllProduct());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAll Products: {ex}");
                return BadRequest(ex);
            }
        }


        [HttpGet]
        [Route("GetAllProductCosif{CodProduto}")]
        public IActionResult GetAllProductCosif(int CodProduto)
        {
            try
            {
                return Ok( _productService.GetAllProductCosif(CodProduto));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAll Products: {ex}");
                return BadRequest(ex);
            }
        }


        [HttpPost]
        [Route("InsertProduct")]
        public ActionResult InsertProduct(Product product)
        {
            try
            {
                _productService.InsertProduct(product);
                return StatusCode(201, product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in InsertProduct: {ex}");
                return BadRequest(ex);
            }
        }

     }
}
