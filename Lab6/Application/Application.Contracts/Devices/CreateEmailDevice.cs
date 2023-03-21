using Application.Dto;
using MediatR;

namespace Application.Contracts.Devices;

public static class CreateEmailDevice
{
    public record struct Comand(string Login)
        : IRequest<Response>;

    public record struct Response(EmailDeviceDto Device);
}
