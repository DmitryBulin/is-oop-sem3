namespace Domain.Common.Exceptions;

public class WorkerException : DomainException
{
    private WorkerException(string? message)
        : base(message) { }

    public static WorkerException TransferToSameDepartment(Guid workerId)
        => new WorkerException($"Worker {workerId} is already in this department");

    public static WorkerException MessageAlreadyExists(Guid workerId, Guid messageId)
        => new WorkerException($"Message {messageId} already exist in worker {workerId}");

    public static WorkerException MessageDoesNotExist(Guid workerId, Guid messageId)
        => new WorkerException($"Message {messageId} does not exist in worker {workerId}");
}