

namespace Catalog.API.Products.CreateProduct
{
    public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator() {

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is Required");

            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is Required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price Must be grater than 0 ");
        }
    }
}
