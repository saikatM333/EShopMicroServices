using Carter;
using CatalogApis.Products.CreateProduct;
using Mapster;
using MediatR;

namespace CatalogApis.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);

    public record UpdateProductResponse(bool IsSuccess);
    public class UpdateProductEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", 
                async(UpdateProductRequest request , ISender sender) =>
            {
                var commond = request.Adapt<UpdateProductCommand>();
                var result = await sender.Send(commond);
                var response = result.Adapt<UpdateProductResponse>();
                return Results.Ok(response);
            })
                .WithName("UpdateProduct")
               .Produces<CreateProductResult>(StatusCodes.Status200OK)
               .ProducesProblem(StatusCodes.Status400BadRequest)
               .WithSummary("update Product")
               .WithDescription("update Product");
        }
    }
}
