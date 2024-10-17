using BuildingBlocks.CQRS;
using CatalogApis.Models;
using Marten;
using MediatR;

namespace CatalogApis.Products.CreateProduct
{
    public record CreateProductCommand(string Name , List<string> Category, string Description, string ImageFile, decimal Price  ):ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    public class CreateProductCommandHandler (IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
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
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            return  new CreateProductResult(product.Id);
            
        }
    }
}
