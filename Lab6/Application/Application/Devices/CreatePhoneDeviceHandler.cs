using Application.Abstractions.DataAccess;
using Application.Mapping;
using Domain.Devices;
using MediatR;
using static Application.Contracts.Devices.CreatePhoneDevice;

namespace Application.Devices;

internal class CreatePhoneDeviceHandler : IRequestHandler<Comand, Response>
{
    private readonly IDatabaseContext _context;

    public CreatePhoneDeviceHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Comand request, CancellationToken cancellationToken)
    {
        var device = new PhoneDevice(Guid.NewGuid(), new PhoneNumber(request.Number));

        _context.Devices.Add(device);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(device.AsDto());
    }
}
