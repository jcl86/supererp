using FluentValidation;
using SuperErp.Core;

namespace SuperErp.Sales.Domain
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage(ErrorMessages.Empty(nameof(Product.Name)));

            RuleFor(x => x.Name).MaximumLength(Model.Restrictions.Products.NameMaxLength)
                .WithMessage(x => ErrorMessages.MaximunLength(nameof(Product.Name), Model.Restrictions.Products.NameMaxLength));

            RuleFor(x => x.Description).MaximumLength(Model.Restrictions.Products.DescriptionMaxLength)
                .WithMessage(x => ErrorMessages.MaximunLength(nameof(Product.Description), Model.Restrictions.Products.DescriptionMaxLength));
        }
    }
}
