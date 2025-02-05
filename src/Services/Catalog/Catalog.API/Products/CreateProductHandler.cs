using BuildingBlocks.CQRS;
using Catalog.API.Model;


namespace Catalog.API.Products
{

    public record CreateProductCommand(string Name ,List<string> Category,string Description,string ImageFile,decimal Price):ICommand<CreateProdictResult>;
    public record CreateProdictResult(Guid Id);


    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProdictResult>
    {
        public async Task<CreateProdictResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // 1-create Product Entity from command object

            var Product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,

            };

            //2- Save in DataBase 


            //3-Return Result 

            return new CreateProdictResult(Guid.NewGuid());


            throw new NotImplementedException();
        }
    }
}
