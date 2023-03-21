using Domain.Users;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.DataAccess.ValueConverters;

public class PersonNameConverter : ValueConverter<PersonName, string>
{
    public PersonNameConverter()
        : base(x => x.Value, x => new PersonName(x))
    { }
}
