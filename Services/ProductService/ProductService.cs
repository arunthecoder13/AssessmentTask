using AssessmentTask.ModelDTO;
using AssessmentTask.Models;
using System.Security.Claims;

namespace AssessmentTask.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Response AddEditProduct(ProductDto productDto, DatabaseContext _context, bool Editflag)
        {
            Response response = new Response();
            try
            {
                if(Editflag)
                {
                    var product = _context.Product.Where(x=>x.Id == productDto.Id).FirstOrDefault();    
                    if(product != null)
                    {
                        product.Price = productDto.Price;
                        product.Description = productDto.Description;   
                        product.Name = productDto.Name;
                        product.Quantity = productDto.Quantity;
                        product.UpdatedDate = DateTime.Now;
                        var save = _context.SaveChanges();
                        if(save>0)
                        {
                            response.Message = "Product Updated Successfully";
                            response.IsError = 'N';
                        }
                        else if(save == 0)
                        {
                            response.Message = "No Change while Updating Product";
                            response.IsError = 'N';
                        }
                        else
                        {
                            response.Message = "Something Went Wrong while Updating Product..";
                            response.IsError = 'Y'; 
                        }
                    }
                    else
                    {
                        response.Message = "No Product Found of Given ID: " + productDto.Id;
                        response.IsError = 'N';
                    }
                    
                }
                else
                {
                    Product product = new Product();    
                    product.Id = Guid.NewGuid();
                    product.Price = productDto.Price;
                    product.Description = productDto.Description;
                    product.Name = productDto.Name;
                    product.Quantity = productDto.Quantity;
                    product.CreatedDate = DateTime.Now;
                    _context.Product.Add(product);
                    var save = _context.SaveChanges();
                    if (save > 0)
                    {
                        response.Message = "Product Added Successfully";
                        response.IsError = 'N';
                    }
                    else if (save == 0)
                    {
                        response.Message = "No Product Added";
                        response.IsError = 'N';
                    }
                    else
                    {
                        response.Message = "Something Went Wrong while Adding Product..";
                        response.IsError = 'Y';
                    }
                    
                }
                return response;
            }
            catch (Exception ex)
            {
                #pragma warning disable CS8602 // Dereference of a possibly null reference.
                Log(_context, "AssessmentTask.ProductService.AddEditProduct", ex.Message, Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)), ex);
                #pragma warning restore CS8602 // Dereference of a possibly null reference.
                response.IsError = 'Y';
                response.Message = "Something went wrong. Please Contact Administrator : " + ex.Message;
                return response;
            }
        }

        public Response DeleteProduct(Guid Id, DatabaseContext _context)
        {
            Response response = new Response();
            try
            {
                var product = _context.Product.Where(x => x.Id == Id).FirstOrDefault();
                if (product != null)
                {
                    _context.Product.Remove(product);
                    var save = _context.SaveChanges();
                    if (save > 0)
                    {
                        response.Message = "Product Deleted Successfully";
                        response.IsError = 'N';
                    }
                    else if (save == 0)
                    {
                        response.Message = "No Change while Deleting Product";
                        response.IsError = 'N';
                    }
                    else
                    {
                        response.Message = "Something Went Wrong while Updating Product..";
                        response.IsError = 'Y';
                    }
                }
                else
                {
                    response.Message = "No Product Found of Given ID: " + Id;
                    response.IsError = 'N';
                }
                return response;

            }
            catch (Exception ex)
            {
                #pragma warning disable CS8602 // Dereference of a possibly null reference.
                Log(_context, "AssessmentTask.ProductService.DeleteProduct", ex.Message, Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)), ex);
                #pragma warning restore CS8602 // Dereference of a possibly null reference.
                response.IsError = 'Y';
                response.Message = "Something went wrong. Please Contact Administrator : " + ex.Message;
                return response;

            }
        }


        public List<Product> GetProducts(int pageSize, int skip, DatabaseContext _context)
        {
            try
            {
                var Products = _context.Product.OrderBy(x => x.CreatedDate).Skip(skip).Take(pageSize).ToList();
                return Products;
            }
            catch (Exception ex)
            {
                #pragma warning disable CS8602 // Dereference of a possibly null reference.
                Log(_context, "AssessmentTask.ProductService.GetProducts", ex.Message, Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)), ex);
                #pragma warning restore CS8602 // Dereference of a possibly null reference.
                throw ex;
            }
        }

        public void Log(DatabaseContext _context, string originator, string message, Guid UserId, Exception ex = null)
        {
            try
            {

                ApplicationLog logEntry = new ApplicationLog()
                {
                    ApplicationLogID = Guid.NewGuid(),
                    Exception = (ex != null) ? ex.ToString() : "",
                    LogDate = DateTime.Now,
                    LogOriginator = originator,
                    Message = message,
                    UserID = UserId
                };

                _context.ApplicationLog.Add(logEntry);
                _context.SaveChanges();
            }
            catch
            {
                //Log to some other location
            }
        }
    }
}
