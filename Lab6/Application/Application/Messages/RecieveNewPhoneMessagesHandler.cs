using Application.Abstractions.DataAccess;
using Application.Extensions;
using Application.Mapping;
using Domain.Devices;
using Domain.Messages;
using MediatR;
using static Application.Contracts.Messages.RecieveNewPhoneMessages;

namespace Application.Messages;

internal class RecieveNewPhoneMessagesHandler : IRequestHandler<Comand, Response>
{
    private readonly IDatabaseContext _context;

    public RecieveNewPhoneMessagesHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Comand request, CancellationToken cancellationToken)
    {
        Device device = await _context.Devices.GetEntityAsync(request.CatcherDeviceId, cancellationToken);

        if ((PhoneDevice)device is null)
        {
            throw Exceptions.DeviceException.WrongDeviceType<PhoneMessage>();
        }

        var messages = device.Messages.Where(x => x.Catcher.Equals(request.CatcherId)).ToList();

        messages.ForEach(x => x.Recieved());

        _context.Messages.UpdateRange(messages);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(messages.Select(x => ((PhoneMessage)x).AsDto()).ToList());
    }
}
