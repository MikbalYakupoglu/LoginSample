using Entity.Concrete;
using FluentValidation;

namespace Business.Validation;

public class ArticleForCreateValidator : AbstractValidator<Article>
{
    public ArticleForCreateValidator()
    {
        RuleFor(a => a.Title).NotNull().MinimumLength(5);
        RuleFor(a => a.Content).NotNull().MinimumLength(50);
    }
}