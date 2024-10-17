using BuildingBlocks.CQRS;
using CatalogApis.Models;
using CatalogApis.Products.CreateProduct;
using Marten;
using MediatR;

namespace CatalogApis.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id , string Name, List<string> Category, string Description, string ImageFile, decimal Price) 
        : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccsess);
    public class UpdateProductCommandHandler
        (IDocumentSession session , ILogger<UpdateProductCommandHandler> logger) : ICommandHandler<UpdateProductCommand ,UpdateProductResult>
    {
       

        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("CatalogApis.Products.UpdateProduct.UpdateProductCommandHandler Handle  {@command}", command);

            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product == null)
            {
                throw new Exception("product not found");

            }
            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;
            product.Category = command.Category;
            product.ImageFile = command.ImageFile;
            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}
