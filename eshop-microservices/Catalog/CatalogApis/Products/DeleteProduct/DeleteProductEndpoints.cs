using Carter;
using CatalogApis.Products.CreateProduct;
using Mapster;
using MediatR;

namespace CatalogApis.Products.DeleteProduct
{
    public record DeleteProductResponse(Guid Id);
    public class DeleteProductEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}"  , async (Guid id , ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductResponse(id));
                var response = result.Adapt<DeleteProductResponse>();
                return Results.Ok(response);
            })
                .WithName("DeleteProduct")
               .Produces<CreateProductResult>(StatusCodes.Status200OK)
               .ProducesProblem(StatusCodes.Status400BadRequest)
               .WithSummary("Delete Product")
               .WithDescription("Delete Product");
        }
    }
}
