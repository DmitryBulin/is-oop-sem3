using Application.Abstractions.DataAccess;
using Application.Extensions;
using Domain.Messages;
using MediatR;
using static Application.Contracts.Messages.HandleMessage;

namespace Application.Messages;

internal class HandleMessageHandler : IRequestHandler<Comand>
{
    private readonly IDatabaseContext _context;

    public HandleMessageHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(Comand request, CancellationToken cancellationToken)
    {
        Message message = await _context.Messages.GetEntityAsync(request.MessageId, cancellationToken);
        message.Handled();

        _context.Messages.Update(message);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
