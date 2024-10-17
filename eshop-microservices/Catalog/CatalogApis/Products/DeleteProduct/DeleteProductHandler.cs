using BuildingBlocks.CQRS;
using CatalogApis.Models;
using Marten;

namespace CatalogApis.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id ) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccsess);
    public class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) :
        ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("CatalogApis.Products.DeleteProduct.DeleteProductCommandHandler handle {@Commond}", command);
            session.Delete<Product>(command.Id);

            await session.SaveChangesAsync();

            return new DeleteProductResult(true);

        }
    }
}
