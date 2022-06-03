using System.Threading;
using System.Threading.Tasks;
using CleanCompanyName.DDDMicroservice.Application.Common.Interfaces;
using CleanCompanyName.DDDMicroservice.Application.Products.Commands.DeleteProduct;

namespace CleanCompanyName.DDDMicroservice.Application.UnitTests.Products.ExposedHandlers;

internal class DeleteProductCommandHandlerExposed : DeleteProductCommandHandler
{
    public DeleteProductCommandHandlerExposed(IProductRepository productRepository)
        : base(productRepository)
    {
    }

    public async Task ExposedHandle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await base.Handle(request, cancellationToken);
    }
}