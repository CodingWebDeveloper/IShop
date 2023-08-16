using ReactQuery_Server.Shared.Products;
using System.Runtime.InteropServices;

namespace ReactQuery_Server.Services
{
    public interface IProductsService
    {

        IEnumerable<ProductViewModel> GetAll();

        Task<IEnumerable<ProductViewModel>> GetAllFiltered(FilterInputModel inptuModel);

        Task<ProductById> GetById(int id);

        Task LikeProductById(int id);

        Task Create(CreateInputModel inputmodel);

        Task Update(UpdateInputModel inputModel);

        Task Delete(int id);

    }
}
