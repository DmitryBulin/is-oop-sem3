using MediatR;

namespace Application.Contracts.Messages;

public static class HandleMessage
{
    public record struct Comand(Guid MessageId)
        : IRequest;
}