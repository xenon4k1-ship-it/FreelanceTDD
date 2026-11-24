namespace Freelance.Core;

public class ProjectWorkflow
{
    public string Status { get; private set; } = "Создан";

    public string? ExecutorName { get; private set; }

    public void Publish()
    {
        RequireStatus("Создан");
        Status = "Открыт";
    }

    public void AssignExecutor(string executorName)
    {
        RequireStatus("Открыт");

        ExecutorName = executorName;
        Status = "В работе";
    }

    public void SubmitResult()
    {
        RequireStatus("В работе");
        Status = "На проверке";
    }

    public void AcceptResult()
    {
        RequireStatus("На проверке");
        Status = "Завершён";
    }

    private void RequireStatus(string expectedStatus)
    {
        if (Status != expectedStatus)
        {
            throw new InvalidOperationException(
                $"Ожидался статус \"{expectedStatus}\", " +
                $"текущий статус: \"{Status}\".");
        }
    }
}