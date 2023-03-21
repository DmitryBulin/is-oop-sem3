using Domain.Devices;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.DataAccess.ValueConverters;

public class EmailLoginConverter : ValueConverter<EmailLogin, string>
{
    public EmailLoginConverter()
        : base(x => x.Value, x => new EmailLogin(x))
    { }
}
