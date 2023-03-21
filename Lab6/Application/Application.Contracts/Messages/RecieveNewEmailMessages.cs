using Application.Dto;
using MediatR;

namespace Application.Contracts.Messages;

public static class RecieveNewEmailMessages
{
    public record struct Comand(Guid CatcherDeviceId, Guid CatcherId)
        : IRequest<Response>;

    public record struct Response(IReadOnlyCollection<EmailMessageDto> Messages);
}