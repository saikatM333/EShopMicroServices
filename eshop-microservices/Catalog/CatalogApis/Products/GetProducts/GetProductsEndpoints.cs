using Carter;
using CatalogApis.Products.CreateProduct;
using Mapster;
using MediatR;

namespace CatalogApis.Products.GetProducts
{
    public class GetProductsEndpoints : ICarterModule
    {
        async void ICarterModule.AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery());
                var response = result.Adapt<GetProductResult>();
                return Results.Ok(response);
            })
             .WithName("GetProduct")
               .Produces<CreateProductResult>(StatusCodes.Status200OK)
               .ProducesProblem(StatusCodes.Status400BadRequest)
               .WithSummary("Create Product")
               .WithDescription("Create Product");
        }
    }
}

