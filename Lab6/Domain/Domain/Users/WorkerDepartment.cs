using Domain.Common.Exceptions;
using RichEntity.Annotations;

namespace Domain.Users;

public partial class WorkerDepartment : IEntity<Guid>
{
    private readonly HashSet<Worker> _workers = new HashSet<Worker>();

    public WorkerDepartment(Guid id, DepartmentName name)
        : this(id)
    {
        Name = name;
    }

    public virtual DepartmentName Name { get; protected init; }
    public virtual Worker? Director { get; protected set; }
    public virtual IReadOnlyCollection<Worker> Workers => _workers;

    public void AddWorker(Worker worker)
    {
        if (_workers.Add(worker) is false)
        {
            throw WorkerDepartmentException.WorkerAlreadyExists(Id, worker.Id);
        }
    }

    public void RemoveWorker(Worker worker)
    {
        if (_workers.Remove(worker) is false)
        {
            throw WorkerDepartmentException.WorkerDoesNotExists(Id, worker.Id);
        }
    }

    public void ChangeDirector(Worker director)
    {
        if (director.Equals(Director))
        {
            throw WorkerDepartmentException.ChangeToSameDirector(director.Id);
        }

        Director = director;
    }
}
