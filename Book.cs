using System.Data.Common;
using Spectre.Console;

namespace TCSA.OOP.LibraryManagementSystem;

internal abstract class LibraryItem
{
    public int Id { get; set; } = 0;
    public string Title { get; set; } = "";
    public string Location { get; set; } = "";

    public LibraryItem(int id, string title, string location)
    {
        this.Id = id;
        this.Title = title;
        this.Location = location;
    }

    public abstract void DisplayDetails();
}

internal class Book : LibraryItem
{
    public string Author { get; set; } = "";
    public string Category { get; set; } = "";
    public int Pages { get; set; } = 0;

    public Book(int id, string title, string location, string author, string category, int pages)
        : base(id, title, location)
    {
        Author = author;
        Category = category;
        Pages = pages;
    }

    public override void DisplayDetails()
    {
        var panel = new Panel(new Markup($"[bold]Book:[/] [cyan]{Title} by {Author}[/]") +
                                $"\n[bold]Pages:[/] {Pages}" +
                                $"\n[bold]Category:[/] {Category}" +
                                $"\n[bold]Location:[/] {Location}")
        {
            Border = BoxBorder.Rounded
        };

        AnsiConsole.Write(panel);
    }
}

internal class Magazine : LibraryItem
{
    public string Publisher { get; set; }
    public DateTime PublishDate { get; set; }
    public int IssueNumber { get; set; }

    public Magazine(int id, string Title, string publisher, DateTime publishDate, string location, int issueNumber)
        : base(id, Title, location)
    {
        Publisher = publisher;
        PublishDate = publishDate;
        IssueNumber = issueNumber;
    }

    public override void DisplayDetails()
    {
        var panel = new Panel(new Markup($"[bold]Magazine:[/] [cyan]{Title}[/] published by [cyan]{Publisher}[/]") +
                              $"\n[bold]Publish Date:[/] {PublishDate:yyyy-MM-dd}" +
                              $"\n[bold]Issue Number:[/] {IssueNumber}" +
                              $"\n[bold]Location:[/] [blue]{Location}[/]")
        {
            Border = BoxBorder.Rounded
        };

        AnsiConsole.Write(panel);
    }
}

internal class Newspaper : LibraryItem
{
    public string Publisher { get; set; }
    public DateTime PublishDate { get; set; }

    public Newspaper(int id, string title, string publisher, DateTime publishDate, string location)
        : base(id, title, location)
    {
        Publisher = publisher;
        PublishDate = publishDate;
    }

    public override void DisplayDetails()
    {
        var panel = new Panel(new Markup($"[bold]Newspaper:[/] [cyan]{Title}[/] published by [cyan]{Publisher}[/]") +
                              $"\n[bold]Publish Date:[/] {PublishDate:yyyy-MM-dd}" +
                              $"\n[bold]Location:[/] [blue]{Location}[/]")
        {
            Border = BoxBorder.Rounded
        };

        AnsiConsole.Write(panel);
    }
}
