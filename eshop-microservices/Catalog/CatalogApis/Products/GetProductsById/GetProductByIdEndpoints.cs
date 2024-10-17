using Carter;
using CatalogApis.Models;

using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CatalogApis.Products.GetProductsById
{
    public record GetProductByIdResponse(Product product);
    public class GetProductByIdEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapGet("/products/{id}", async (Guid id , ISender sender) =>{
               var result = await sender.Send(new GetProductByIdQuery(id));
               var response = result.product.Adapt<GetProductByIdResponse>();// adapt not working properly
              // logger.LogInformation(" response {@product}", response);
               return Results.Ok(result.product);

            })
            .WithName("GetProductById")
               .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
               .ProducesProblem(StatusCodes.Status400BadRequest)
               .WithSummary("Create Product")
               .WithDescription("Create Product");
        }
    }
}
