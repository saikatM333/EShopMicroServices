using BuildingBlocks.CQRS;
using CatalogApis.Models;
using MediatR;

namespace CatalogApis.Products.CreateProduct
{
    public record CreateProductCommand(string Name , List<string> Category, string Description, string ImageFile, decimal Price  ):ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    public class CreateProductCommondHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
       
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {  // create product entity
            // save to database 
            // return creat product entity 

            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                Price = command.Price,
                ImageFile = command.ImageFile
            };
            return  new CreateProductResult(Guid.NewGuid());
            
        }
    }
}
