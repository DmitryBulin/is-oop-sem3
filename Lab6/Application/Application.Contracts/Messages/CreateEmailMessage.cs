using Application.Dto;
using MediatR;

namespace Application.Contracts.Messages;

public static class CreateEmailMessage
{
    public record struct Comand(
        Guid SenderDeviceId,
        Guid CatcherDeviceId,
        Guid SenderId,
        Guid CatcherId,
        DateTime CreationTime,
        string Theme,
        string Body)
        : IRequest<Response>;

    public record struct Response(EmailMessageDto Message);
}
