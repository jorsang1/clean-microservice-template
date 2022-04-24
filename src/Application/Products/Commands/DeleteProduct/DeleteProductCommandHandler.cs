using Application.Common.Interfaces;
using MediatR;

namespace Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : AsyncRequestHandler<DeleteProductCommand>
{
    private IProductRepository _productRepository;
    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    protected override async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(request.Id);

        if (product is not null)
        {
            await _productRepository.Delete(product);
        }
        else
        {
            //TODO: Switch to a EntityDoesntExistException or similar.
            throw new KeyNotFoundException("The product to delete doesn't exists.");
        }
    }

}

