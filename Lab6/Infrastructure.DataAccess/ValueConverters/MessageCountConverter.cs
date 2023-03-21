using Domain.Messages;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.DataAccess.ValueConverters;

public class MessageCountConverter : ValueConverter<MessageCount, int>
{
    public MessageCountConverter()
        : base(x => x.Value, x => new MessageCount(x))
    { }
}
