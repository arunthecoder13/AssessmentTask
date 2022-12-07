using AssessmentTask.ModelDTO;
using AssessmentTask.Models;

namespace AssessmentTask.Services.ProductService
{
    public interface IProductService
    {
        List<Product> GetProducts(int pageSize, int skip, DatabaseContext _context);
        Response AddEditProduct(ProductDto productDto, DatabaseContext _context, bool Editflag);
        Response DeleteProduct(Guid Id, DatabaseContext _context);

        void Log(DatabaseContext _context, string originator, string message, Guid UserId, Exception ex = null);
    }
}
