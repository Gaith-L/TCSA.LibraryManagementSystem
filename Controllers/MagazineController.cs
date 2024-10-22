using Spectre.Console;
using TCSA.OOP.LibraryManagementSystem;

class MagazineController : IBaseController
{
    public void ViewItems()
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);

        table.AddColumn("[yellow]ID[/]");
        table.AddColumn("[yellow]Title[/]");
        table.AddColumn("[yellow]Publisher[/]");
        table.AddColumn("[yellow]Publish date[/]");
        table.AddColumn("[yellow]Location[/]");
        table.AddColumn("[yellow]Issue number[/]");

        var magazines = MockDatabase.LibraryItems.OfType<Magazine>();

        foreach (Magazine magazine in magazines)
        {
            table.AddRow(
                magazine.Id.ToString(),
                $"[cyan]{magazine.Title}[/]",
                $"[cyan]{magazine.Publisher}[/]",
                $"[cyan]{magazine.PublishDate}[/]",
                $"[blue]{magazine.Location}[/]",
                magazine.IssueNumber.ToString()
            );
        }
        AnsiConsole.Write(table);

        AnsiConsole.MarkupLine("Press Any Key to Continue.");
        Console.ReadKey();

    }

    public void AddItems()
    {
     var title = AnsiConsole.Ask<string>("Enter the [green]title[/] of the magazine to add:");
     var publisher = AnsiConsole.Ask<string>("Enter the [green]publisher[/] of the magazine:");
     var publishDate = AnsiConsole.Ask<DateTime>("Enter the [green]published date[/] of the magazine:"); // TODO: Date time for publish date
     var location = AnsiConsole.Ask<string>("Enter the [green]location[/] of the magazine:");
     var issueNumber = AnsiConsole.Ask<int>("Enter the [green]the issue number[/] in the magazine:");

     if (MockDatabase.LibraryItems.OfType<Magazine>().Any(m => m.Title.Equals(title, StringComparison.OrdinalIgnoreCase)))
     {
         AnsiConsole.MarkupLine("[red]This magazine already exists.[/]");
     }
     else
     {
         var newMagazine = new Magazine(MockDatabase.LibraryItems.OfType<Magazine>().Count() + 1, title, publisher, publishDate, location, issueNumber);
         MockDatabase.LibraryItems.Add(newMagazine);
         AnsiConsole.MarkupLine("[green]Magazine added successfully![/]");
     }

     AnsiConsole.MarkupLine("Press Any Key to Continue.");
     Console.ReadKey();
    }

    public void DeleteItem()
    {
        if (MockDatabase.LibraryItems.OfType<Magazine>().Count() == 0)
        {
            AnsiConsole.MarkupLine("[bold red] There are no magazines to delete.[/]");
            return;
        }
        /*
            In the case where a Magazine in the Magazines list
            contains a markup character (e.g. "Some_Book [2nd edition]"),
            Prompt(.AddChoices(books)) will throw as it doesn't
            escape the strings. So the selection must be escaped with
            EscapeMarkup()
        */
        string inputMagazineTitle = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Choose a [red]Magazine[/] to delete")
            .AddChoices(MockDatabase.LibraryItems.OfType<Magazine>().Select(Magazine => EscapeStringHelper.EscapeMarkup(Magazine.Title))));

        /*
            Undo the escaped string so it looks okay as output'
        */
        inputMagazineTitle = EscapeStringHelper.UndoEscapeMarkup(inputMagazineTitle);

        Magazine? matchingMagazine = MockDatabase.LibraryItems.OfType<Magazine>().FirstOrDefault(m => m.Title.Equals(inputMagazineTitle));
        if (matchingMagazine != null)
        {
            MockDatabase.LibraryItems.Remove(matchingMagazine); // Surely it can't be null :D
            AnsiConsole.MarkupLineInterpolated($"[bold green]Magazine: [bold red]{inputMagazineTitle}[/] deleted[/].");
        }
        else
        {
            AnsiConsole.MarkupLineInterpolated($"[bold red]{inputMagazineTitle} not found[/]");
        }

        AnsiConsole.MarkupLine("Press Any Key to Continue.");
        Console.ReadKey();
    }
}
