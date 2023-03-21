using Application.Abstractions.DataAccess;
using Application.Extensions;
using Application.Mapping;
using Domain.Devices;
using Domain.Messages;
using Domain.Users;
using MediatR;
using static Application.Contracts.Messages.CreateEmailMessage;

namespace Application.Messages;

internal class CreateEmailMessageHandler : IRequestHandler<Comand, Response>
{
    private readonly IDatabaseContext _context;

    public CreateEmailMessageHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Comand request, CancellationToken cancellationToken)
    {
        Device senderDevice = await _context.Devices.GetEntityAsync(request.SenderDeviceId, cancellationToken);
        Device catcherDevice = await _context.Devices.GetEntityAsync(request.CatcherDeviceId, cancellationToken);

        if ((EmailDevice)senderDevice is null || (EmailDevice)catcherDevice is null)
        {
            throw Exceptions.DeviceException.WrongDeviceType<EmailDevice>();
        }

        Worker sender = await _context.Workers.GetEntityAsync(request.SenderId, cancellationToken);
        Worker catcher = await _context.Workers.GetEntityAsync(request.CatcherId, cancellationToken);

        var emailMessage = new EmailMessage(
            Guid.NewGuid(),
            senderDevice,
            catcherDevice,
            sender,
            catcher,
            request.CreationTime,
            new MessageData(request.Theme),
            new MessageData(request.Body));

        sender.AddMessage(emailMessage);
        ((EmailDevice)senderDevice).AddMessage(emailMessage);
        ((EmailDevice)catcherDevice).AddMessage(emailMessage);

        _context.Messages.Add(emailMessage);
        _context.Devices.Update(senderDevice);
        _context.Devices.Update(catcherDevice);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response(emailMessage.AsDto());
    }
}
