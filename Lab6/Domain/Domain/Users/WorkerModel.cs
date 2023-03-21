namespace Domain.Users;

public record WorkerModel(Guid Id, PersonName Name, PersonName SecondName)
{
    public static WorkerModel FromWorker(Worker worker)
        => new WorkerModel(worker.Id, worker.Name, worker.SecondName);
}