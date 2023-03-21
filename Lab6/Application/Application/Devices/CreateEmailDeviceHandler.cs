using Application.Abstractions.DataAccess;
using Application.Mapping;
using Domain.Devices;
using MediatR;
using static Application.Contracts.Devices.CreateEmailDevice;

namespace Application.Devices;

internal class CreateEmailDeviceHandler : IRequestHandler<Comand, Response>
{
    private readonly IDatabaseContext _context;

    public CreateEmailDeviceHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Comand request, CancellationToken cancellationToken)
    {
        var device = new EmailDevice(Guid.NewGuid(), new EmailLogin(request.Login));

        _context.Devices.Add(device);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(device.AsDto());
    }
}
