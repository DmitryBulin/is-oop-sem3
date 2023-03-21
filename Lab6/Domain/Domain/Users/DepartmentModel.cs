using RichEntity.Annotations;

namespace Domain.Users;

public partial class DepartmentModel : IEntity<Guid>
{
    public DepartmentModel(Guid id, DepartmentName name, WorkerModel? director, IReadOnlyCollection<WorkerModel> workers)
    {
        Id = id;
        Name = name;
        Director = director;
        Workers = workers;
    }

    public virtual Guid Id { get; protected init; }
    public virtual DepartmentName Name { get; protected init; }
    public virtual WorkerModel? Director { get; protected init; }
    public virtual IReadOnlyCollection<WorkerModel> Workers { get; protected init; }

    public static DepartmentModel FromDepartment(WorkerDepartment department)
        => new DepartmentModel(
            department.Id,
            department.Name,
            department.Director is null ? null : WorkerModel.FromWorker(department.Director),
            department.Workers.Select(x => WorkerModel.FromWorker(x)).ToArray());
}