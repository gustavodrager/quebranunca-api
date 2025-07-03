using MediatR;

namespace MyApp.Application.Commands
{
    public class DeleteGameCommand : IRequest
    {
        public Guid Id { get; }

        public DeleteGameCommand(Guid id)
        {
            Id = id;
        }
    }
}