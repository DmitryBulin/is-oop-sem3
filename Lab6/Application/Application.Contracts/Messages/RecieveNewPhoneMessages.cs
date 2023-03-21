using Application.Dto;
using MediatR;

namespace Application.Contracts.Messages;

public static class RecieveNewPhoneMessages
{
    public record struct Comand(Guid CatcherDeviceId, Guid CatcherId)
        : IRequest<Response>;

    public record struct Response(IReadOnlyCollection<PhoneMessageDto> Messages);
}
