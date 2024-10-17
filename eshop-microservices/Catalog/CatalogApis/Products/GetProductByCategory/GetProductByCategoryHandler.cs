using BuildingBlocks.CQRS;
using CatalogApis.Models;
using Marten;

namespace CatalogApis.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> product);
    public class GetProductByCategoryQueryHandler
    (IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
        :
        IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("CatalogApis.Products.GetProductByCategory . Handle is {@Query}", query);

            var products = await session.Query<Product>().
                Where(p=>p.Category.Contains(query.Category)).ToListAsync();

            return new GetProductByCategoryResult(products);
        }
    }
}
