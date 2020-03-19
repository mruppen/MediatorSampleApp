using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Domain
{
    internal class SampleCommandHandler : IRequestHandler<SampleCommand, CommandResult>
    {
        public Task<CommandResult> Handle(SampleCommand request, CancellationToken _)
        {
            return Task.FromResult(CommandResult.Ok());
        }
    }
}
