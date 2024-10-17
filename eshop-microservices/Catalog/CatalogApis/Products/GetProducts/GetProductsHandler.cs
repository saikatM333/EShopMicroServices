using BuildingBlocks.CQRS;
using CatalogApis.Models;
using Marten;

namespace CatalogApis.Products.GetProducts
{  public record GetProductsQuery(): IQuery<GetProductResult>;
    public record GetProductResult(IEnumerable<Product> Products);
    public class GetProductsQueryHandler
        (IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
        : IQueryHandler<GetProductsQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsQuerHAndler.Handle called the {@Query}", query);
            var products = await session.Query<Product>().ToListAsync();
            return new GetProductResult(products);
        }
    }
}
