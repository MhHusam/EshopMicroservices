namespace Catalog.API.Products.CreateProduct
{

    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProdictResult>;
    public record CreateProdictResult(Guid Id);


    internal class CreateProductCommandHandler(IDocumentSession session ,ILogger<CreateProductCommandHandler> logger ) : ICommandHandler<CreateProductCommand, CreateProdictResult>
    {


        public async Task<CreateProdictResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {

            logger.LogInformation("CreateProductCommandHandler.Handler called with {@Command}", command);


            //Validat 

            //var result =await validator.ValidateAsync(command, cancellationToken);
            //var errors=result.Errors.Select(x=>x.ErrorMessage).ToList();
            //if (errors.Any()) {
            //    throw new ValidationException(errors.FirstOrDefault());
            //}

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
            session.Store(Product);
            await session.SaveChangesAsync(cancellationToken);

            //3-Return Result 

            return new CreateProdictResult(Product.Id);



        }
    }
}
