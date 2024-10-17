using BuildingBlocks.CQRS;
using Carter;
using CatalogApis.Models;
using CatalogApis.Products.CreateProduct;
using Mapster;
using Marten;
using MediatR;

namespace CatalogApis.Products.GetProductByCategory
{
    public record GetProductByCategoryResponse(IEnumerable<Product> products);

    public class GetProductByCategoryEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapGet("/products/category/{category}", async (string category , ISender sender) =>
           {
               var result = await sender.Send(new GetProductByCategoryQuery(category));
               var response = result.Adapt<GetProductByCategoryResponse>();// adapt is not working need to rectify
               return Results.Ok(result.product);
               
           })
                .WithName("GetProductByCategory")
               .Produces<CreateProductResult>(StatusCodes.Status200OK)
               .ProducesProblem(StatusCodes.Status400BadRequest)
               .WithSummary("get product by category")
               .WithDescription("get product by category");
        }
    }
}
