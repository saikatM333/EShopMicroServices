using MediatR;

namespace CatalogApi.Products.CreateProduct
{
    public record CreateProductCommand(string Name , List<string> Category, string Description, decimal Price):IRequest<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    public class CreateProductCommondHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
       
        Task<CreateProductResult> IRequestHandler<CreateProductCommand, CreateProductResult>.Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {  // bisnuss logic
            throw new NotImplementedException();
        }
    }
}
