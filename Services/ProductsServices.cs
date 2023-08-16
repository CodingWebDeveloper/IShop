using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using ReactQuery_Server.Data;
using ReactQuery_Server.Data.Models;
using ReactQuery_Server.Data.Models.Enums;
using ReactQuery_Server.Shared.Products;
using ReactQuery_Server.Utils;

namespace ReactQuery_Server.Services
{
    public class ProductsServices : IProductsService
    {
        private readonly ShopDbContext dbContext;

        public ProductsServices(ShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Create(CreateInputModel inputModel)
        {
            Product product = new Product() {
                Name = inputModel.Name,
                ImageUrl = inputModel.ImageUrl,
                Price = inputModel.Price,
                Category = (Category) Enum.Parse(typeof(Category), inputModel.Category, true)
            };

            await this.dbContext.Products.AddAsync(product);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Product? product = await this.dbContext.Products.FirstOrDefaultAsync(product => product.Id == id);

            if(product == null)
            {
                throw new ArgumentException(ErrorMessages.NOT_FOUND);
            }

            this.dbContext.Products.Remove(product);
            await this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            return this.dbContext.Products.Select(product => new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                ImageUrl= product.ImageUrl,
                Price = product.Price,
                Category = product.Category.ToString(),
            });
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllFiltered(FilterInputModel inputModel)
        {
            IEnumerable<Product> products = await this.dbContext
                    .Products
                    .ToListAsync();

             IEnumerable<Product> filteredProducts = products.Where(product => 
                            FilterBySearchString(product.Name, inputModel.SearchString) &&
                            FilterByToPrice(product.Price,inputModel.PriceRange));

            return filteredProducts.Select(product => new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Category = product.Category.ToString(),
            });
        }

        public async Task<UpdateInputModel> GetByIdToUpdate(int id)
        {
            UpdateInputModel? product = await this.dbContext.Products.Select(p => new UpdateInputModel()
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                Category= p.Category.ToString(),
            }).FirstOrDefaultAsync(p => p.Id == id);

            if(product == null)
            {
                throw new ArgumentException(ErrorMessages.NOT_FOUND);
            }

            return product;

        }

        public async Task<ProductById> GetById(int id)
        {
            ProductById? product = await this.dbContext.Products.Select(p => new ProductById()
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                Category = p.Category.ToString(),
            }).FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                throw new ArgumentException(ErrorMessages.NOT_FOUND);
            }

            return product;
        }

        public async Task Update(UpdateInputModel inputModel)
        {
            Product? product =  await this.dbContext.Products.FirstOrDefaultAsync(p => p.Id == inputModel.Id);

            if(product == null)
            {
                throw new ArgumentException(ErrorMessages.NOT_FOUND);
            }

            product.Name = inputModel.Name;
            product.Price = inputModel.Price;
            product.ImageUrl = inputModel.ImageUrl;
            product.Category = (Category)Enum.Parse(typeof(Category),inputModel.Category, true);


            this.dbContext.Products.Update(product);
            await this.dbContext.SaveChangesAsync();
        }

        private bool FilterByToPrice(decimal productPrice, decimal priceRange)
        {
            if(priceRange == 0 )
            {
                return true;
            }

            return productPrice < priceRange;
        }

        private bool FilterBySearchString(string productName, string? searchString)
        {
            if(String.IsNullOrEmpty(searchString))
            {
                return true;
            }

            return productName.Contains(searchString.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        public async Task LikeProductById(int id)
        {
            Product? product =  await this.dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            if(product == null)
            {
                throw new ArgumentNullException("Product does not exist!");
            }

            product.isFavorite = !product.isFavorite;
            await this.dbContext.SaveChangesAsync();
        }
    }
}
