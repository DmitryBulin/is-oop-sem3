using Application.Abstractions.DataAccess;
using Application.Extensions;
using Application.Mapping;
using Domain.Devices;
using Domain.Messages;
using MediatR;
using static Application.Contracts.Messages.RecieveNewEmailMessages;

namespace Application.Messages;

internal class RecieveNewEmailMessagesHandler : IRequestHandler<Comand, Response>
{
    private readonly IDatabaseContext _context;

    public RecieveNewEmailMessagesHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Comand request, CancellationToken cancellationToken)
    {
        Device device = await _context.Devices.GetEntityAsync(request.CatcherDeviceId, cancellationToken);

        if ((EmailDevice)device is null)
        {
            throw Exceptions.DeviceException.WrongDeviceType<EmailDevice>();
        }

        var messages = device.Messages.Where(x => x.Catcher.Equals(request.CatcherId)).ToList();

        messages.ForEach(x => x.Recieved());

        _context.Messages.UpdateRange(messages);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(messages.Select(x => ((EmailMessage)x).AsDto()).ToList());
    }
}
