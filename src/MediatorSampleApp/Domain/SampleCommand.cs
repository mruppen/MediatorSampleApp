using MediatR;

namespace Domain
{
    public class SampleCommand : IRequest<CommandResult>
    {
        public string? Comment { get; set; }

        public string Value { get; set; } = string.Empty;
    }
}
