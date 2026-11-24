using Freelance.Core;

internal class Program
{
    private static void Main(string[] args)
    {
        var project = new ProjectWorkflow();

        Console.WriteLine($"Новый проект: {project.Status}");

        project.Publish();
        Console.WriteLine($"После публикации: {project.Status}");

        project.AssignExecutor("Иван");
        Console.WriteLine(
            $"Исполнитель: {project.ExecutorName}. Статус: {project.Status}");

        project.SubmitResult();
        Console.WriteLine($"После отправки результата: {project.Status}");

        project.AcceptResult();
        Console.WriteLine($"После принятия результата: {project.Status}");

        Console.WriteLine();
        Console.WriteLine("Попытка повторного принятия результата:");

        try
        {
            project.AcceptResult();
        }
        catch (InvalidOperationException exception)
        {
            Console.WriteLine($"Ошибка: {exception.Message}");
        }
    }
}