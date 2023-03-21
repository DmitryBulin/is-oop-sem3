namespace Domain.Common.Exceptions;

public class WorkerDepartmentException : DomainException
{
    private WorkerDepartmentException(string? message)
        : base(message) { }

    public static WorkerDepartmentException WorkerAlreadyExists(Guid departmentId, Guid workerId)
        => new WorkerDepartmentException($"Worker {workerId} already exist in department {departmentId}");

    public static WorkerDepartmentException WorkerDoesNotExists(Guid departmentId, Guid workerId)
        => new WorkerDepartmentException($"Worker {workerId} does not exist in department {departmentId}");

    public static WorkerDepartmentException ChangeToSameDirector(Guid directorId)
        => new WorkerDepartmentException($"Worker {directorId} is already director of this department");
}