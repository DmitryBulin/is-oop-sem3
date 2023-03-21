using Application.Dto;
using MediatR;

namespace Application.Contracts.Messages;

public static class CreatePhoneMessage
{
    public record struct Comand(
        Guid SenderDeviceId,
        Guid CatcherDeviceId,
        Guid SenderId,
        Guid CatcherId,
        DateTime CreationTime,
        string Body)
        : IRequest<Response>;

    public record struct Response(PhoneMessageDto Message);
}
