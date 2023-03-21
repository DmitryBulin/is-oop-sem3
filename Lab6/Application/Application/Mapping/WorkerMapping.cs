using Application.Dto;
using Domain.Users;

namespace Application.Mapping;

public static class WorkerMapping
{
    public static WorkerDto AsDto(this Worker worker)
        => new WorkerDto(
            worker.Id,
            worker.Name.Value,
            worker.SecondName.Value);

    public static WorkerDto AsDto(this WorkerModel worker)
        => new WorkerDto(
            worker.Id,
            worker.Name.Value,
            worker.SecondName.Value);
}
