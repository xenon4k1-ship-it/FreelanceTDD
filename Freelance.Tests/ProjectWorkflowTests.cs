using Freelance.Core;
using NUnit.Framework;

namespace Freelance.Tests;

[TestFixture]
public class ProjectWorkflowTests
{
    [Test]
    public void NewProject_HasCreatedStatus()
    {
        var project = new ProjectWorkflow();

        Assert.That(project.Status, Is.EqualTo("Создан"));
    }

    [Test]
    public void Publish_ChangesStatusToOpen()
    {
        var project = new ProjectWorkflow();

        project.Publish();

        Assert.That(project.Status, Is.EqualTo("Открыт"));
    }

    private static ProjectWorkflow CreatePublishedProject()
    {
        var project = new ProjectWorkflow();
        project.Publish();
        return project;
    }

    [Test]
    public void AssignExecutor_SavesExecutorAndStartsWork()
    {
        var project = CreatePublishedProject();

        project.AssignExecutor("Иван");

        Assert.Multiple(() =>
        {
            Assert.That(project.ExecutorName, Is.EqualTo("Иван"));
            Assert.That(project.Status, Is.EqualTo("В работе"));
        });
    }

    [Test]
    public void SubmitResult_ChangesStatusToReview()
    {
        var project = CreatePublishedProject(); ;
        project.AssignExecutor("Иван");

        project.SubmitResult();

        Assert.That(project.Status, Is.EqualTo("На проверке"));
    }


    [Test]
    public void AcceptResult_ChangesStatusToCompleted()
    {
        var project = CreatePublishedProject();
        project.AssignExecutor("Иван");
        project.SubmitResult();

        project.AcceptResult();

        Assert.That(project.Status, Is.EqualTo("Завершён"));
    }

    [TestCase("Publish")]
    [TestCase("AssignExecutor")]
    [TestCase("SubmitResult")]
    [TestCase("AcceptResult")]
    public void Operation_FromWrongStatus_ThrowsException(string operation)
    {
        var project = new ProjectWorkflow();

        if (operation == "Publish")
        {
            project.Publish();
        }

        Action action = operation switch
        {
            "Publish" => () => project.Publish(),
            "AssignExecutor" => () => project.AssignExecutor("Иван"),
            "SubmitResult" => () => project.SubmitResult(),
            "AcceptResult" => () => project.AcceptResult(),
            _ => throw new ArgumentException("Неизвестная операция")
        };

        Assert.Throws<InvalidOperationException>(() => action());
    }

}