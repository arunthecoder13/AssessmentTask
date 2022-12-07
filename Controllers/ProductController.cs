using AssessmentTask.ModelDTO;
using AssessmentTask.Models;
using AssessmentTask.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AssessmentTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IProductService _productService;

        public ProductController(DatabaseContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        [HttpPost("AddorEditProduct/{Editflag}"), Authorize]
        public async Task<ActionResult<Response>> AddorEditProduct(ProductDto product,bool Editflag)
        {
            Response response = new Response();
            Guid UserId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                response = _productService.AddEditProduct(product, _context,Editflag);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _productService.Log(_context, "AssessmentTask.ProductController.AddorEditProduct", ex.Message, UserId, ex);
                response.IsError = 'Y';
                response.Message = ex.Message;
                return Ok(response);
            }
        }

        [HttpDelete("DeleteProduct/{Id}"), Authorize]
        public async Task<ActionResult<Response>> DeleteProduct(string Id)
        {
            Response response = new Response();
            Guid UserId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                response = _productService.DeleteProduct(new Guid(Id), _context);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _productService.Log(_context, "AssessmentTask.ProductController.DeleteProduct", ex.Message, UserId, ex);
                response.IsError = 'Y';
                response.Message = ex.Message;
                return Ok(response);
            }
        }

        [HttpGet("GetProducts"), Authorize]
        public async Task<ActionResult<List<ProductResponseDto>>> GetProducts(int pageSize, int index)
        {
            int Skip = index * pageSize;
            ProductResponseDto productResponseDto = new ProductResponseDto();
            Guid UserId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            try
            {
                var list = _productService.GetProducts(pageSize, Skip, _context);
                productResponseDto.Products = list;
                productResponseDto.IsError = 'N';
                if(list.Count() > 0)
                {
                    productResponseDto.Message = "Success";
                }
                else if(list.Count() == 0)
                {
                    productResponseDto.Message = "No Product Found.";
                }
                else
                {
                    productResponseDto.Message = "Something went Wrong.";
                }
                return Ok(productResponseDto);
            }
            catch (Exception ex)
            {
                _productService.Log(_context, "AssessmentTask.ProductController.GetProducts", ex.Message, UserId, ex);
                productResponseDto.IsError = 'Y';
                productResponseDto.Message = ex.Message;
                return Ok(productResponseDto);
            }
        }
    }
}
