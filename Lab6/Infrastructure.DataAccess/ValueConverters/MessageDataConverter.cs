using Domain.Messages;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.DataAccess.ValueConverters;

public class MessageDataConverter : ValueConverter<MessageData, string>
{
    public MessageDataConverter()
        : base(x => x.Value, x => new MessageData(x))
    { }
}
