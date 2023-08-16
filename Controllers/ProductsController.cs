using Microsoft.AspNetCore.Mvc;
using ReactQuery_Server.Services;
using ReactQuery_Server.Shared.Products;

namespace ReactQuery_Server.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateInputModel inputmodel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await this.productsService.Create(inputmodel);

            return this.Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await this.productsService.Update(inputModel);
            }
            catch (Exception ex)
            {
                return this.NotFound(ex.Message);
            }

            return this.Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            AllProductsViewModel viewModel = new AllProductsViewModel();
            viewModel.Products = this.productsService.GetAll();
            return this.Ok(viewModel);
        }

        [HttpGet("filtered")]
        public async Task<IActionResult> GetAllFiltered([FromQuery]FilterInputModel inputModel)
        {
            AllProductsViewModel viewModel = new AllProductsViewModel();
            viewModel.Products = await this.productsService.GetAllFiltered(inputModel);

            return this.Ok(viewModel);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            try
            {
                await this.productsService.Delete(id);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }

            return this.Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdToUpdate([FromRoute] int id)
        {
            try
            {
                ProductById product = await this.productsService.GetById(id);
                return this.Ok(product);
            }
            catch (Exception ex)
            {
                return this.Ok(new {Message= "Error occured", Status= 404 });
            }
        }

        [HttpPatch("like")]
        public async Task<IActionResult> LikeProductById([FromQuery] int id)
        {
            try
            {
                await this.productsService.LikeProductById(id);
                return this.Ok();
            }
            catch (Exception err)
            {

                return this.BadRequest(err.Message);
            }
        }
    }
}
