using Application.Dto;
using MediatR;

namespace Application.Contracts.Devices;

public static class CreatePhoneDevice
{
    public record struct Comand(string Number)
        : IRequest<Response>;

    public record struct Response(PhoneDeviceDto Device);
}