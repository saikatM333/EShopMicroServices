using BuildingBlocks.CQRS;
using CatalogApis.Models;
using Marten;

namespace CatalogApis.Products.GetProductsById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product product);
    public class GetProductByIdQueryHandler(IDocumentSession session , ILogger<GetProductByIdQueryHandler> logger) :
        IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByIdQueryHandler . Handle called with {@Query}", query);

            var product = await session.LoadAsync<Product>(query.Id , cancellationToken);
            logger.LogInformation("product name is  {product}", product.Name);
            if (product == null) {
                throw new Exception("product not found ");
            }
            return new GetProductByIdResult(product);
        }
    }
}
