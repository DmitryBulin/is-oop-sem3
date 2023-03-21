using Application.Abstractions.DataAccess;
using Application.Extensions;
using Application.Mapping;
using Domain.Devices;
using Domain.Messages;
using Domain.Users;
using MediatR;
using static Application.Contracts.Messages.CreatePhoneMessage;

namespace Application.Messages;

internal class CreatePhoneMessageHandler : IRequestHandler<Comand, Response>
{
    private readonly IDatabaseContext _context;

    public CreatePhoneMessageHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Comand request, CancellationToken cancellationToken)
    {
        Device senderDevice = await _context.Devices.GetEntityAsync(request.SenderDeviceId, cancellationToken);
        Device catcherDevice = await _context.Devices.GetEntityAsync(request.CatcherDeviceId, cancellationToken);

        if ((PhoneDevice)senderDevice is null || (PhoneDevice)catcherDevice is null)
        {
            throw Exceptions.DeviceException.WrongDeviceType<PhoneMessage>();
        }

        Worker sender = await _context.Workers.GetEntityAsync(request.SenderId, cancellationToken);
        Worker catcher = await _context.Workers.GetEntityAsync(request.CatcherId, cancellationToken);

        var phoneMessage = new PhoneMessage(
            Guid.NewGuid(),
            senderDevice,
            catcherDevice,
            sender,
            catcher,
            request.CreationTime,
            new MessageData(request.Body));

        sender.AddMessage(phoneMessage);
        ((PhoneDevice)senderDevice).AddMessage(phoneMessage);
        ((PhoneDevice)catcherDevice).AddMessage(phoneMessage);

        _context.Messages.Add(phoneMessage);
        _context.Devices.Update(senderDevice);
        _context.Devices.Update(catcherDevice);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(phoneMessage.AsDto());
    }
}
