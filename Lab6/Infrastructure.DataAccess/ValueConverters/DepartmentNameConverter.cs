using Domain.Users;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.DataAccess.ValueConverters;

public class DepartmentNameConverter : ValueConverter<DepartmentName, string>
{
    public DepartmentNameConverter()
        : base(x => x.Value, x => new DepartmentName(x))
    { }
}
