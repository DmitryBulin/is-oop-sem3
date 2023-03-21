using Domain.Common.Exceptions;
using Domain.Messages;
using RichEntity.Annotations;

namespace Domain.Users;

public partial class Worker : IEntity<Guid>
{
    private readonly HashSet<Message> _messages = new HashSet<Message>();
    public Worker(Guid id, PersonName name, PersonName secondName, WorkerDepartment department)
        : this(id)
    {
        Name = name;
        SecondName = secondName;
        Department = department;
    }

    public PersonName Name { get; protected init; }
    public PersonName SecondName { get; protected init; }
    public virtual WorkerDepartment Department { get; protected set; }

    public virtual IReadOnlyCollection<Message> Messages => _messages;

    public void TransferTo(WorkerDepartment department)
    {
        if (Department.Equals(department))
        {
            throw WorkerException.TransferToSameDepartment(Id);
        }

        department.AddWorker(this);
        Department.RemoveWorker(this);

        Department = department;
    }

    public void AddMessage(Message message)
    {
        if (_messages.Add(message) is false)
        {
            throw WorkerException.MessageAlreadyExists(Id, message.Id);
        }
    }

    public void RemoveMessage(Message message)
    {
        if (_messages.Remove(message) is false)
        {
            throw WorkerException.MessageDoesNotExist(Id, message.Id);
        }
    }
}
