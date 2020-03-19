using FluentValidation;

namespace Domain
{
    internal class SampleCommandValidator : AbstractValidator<SampleCommand>
    {
        public SampleCommandValidator()
        {
            RuleFor(c => c.Value).NotEmpty().MinimumLength(10);
            RuleFor(c => c.Comment).NotEmpty();
        }
    }
}
