using Spectre.Console;
using TCSA.OOP.LibraryManagementSystem;

class NewspaperController : IBaseController
{
    public void ViewItems()
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);

        table.AddColumn("[yellow]ID[/]");
        table.AddColumn("[yellow]Title[/]");
        table.AddColumn("[yellow]Publisher[/]");
        table.AddColumn("[yellow]Published Date[/]");
        table.AddColumn("[yellow]Location[/]");

        var newspapers = MockDatabase.LibraryItems.OfType<Newspaper>();

        foreach (Newspaper newspaper in newspapers)
        {
            table.AddRow(
                newspaper.Id.ToString(),
                $"[cyan]{newspaper.Title}[/]",
                $"[cyan]{newspaper.Publisher}[/]",
                $"[green]{newspaper.PublishDate}[/]",
                $"[blue]{newspaper.Location}[/]"
            );
        }
        AnsiConsole.Write(table);

        AnsiConsole.MarkupLine("Press Any Key to Continue.");
        Console.ReadKey();

    }

    public void AddItems()
    {
     var title = AnsiConsole.Ask<string>("Enter the [green]title[/] of the newspaper to add:");
     var publisher = AnsiConsole.Ask<string>("Enter the [green]publisher[/] of the newspaper:");
     var publishDate = AnsiConsole.Ask<DateTime>("Enter the [green]published date[/] of the newspaper:");
     var location = AnsiConsole.Ask<string>("Enter the [green]location[/] of the newspaper:");

     if (MockDatabase.LibraryItems.OfType<Newspaper>().Any(n => n.Title.Equals(title, StringComparison.OrdinalIgnoreCase)))
     {
         AnsiConsole.MarkupLine("[red]This newspaper already exists.[/]");
     }
     else
     {
         var newNewspaper = new Newspaper(MockDatabase.LibraryItems.OfType<Newspaper>().Count() + 1, title, publisher, publishDate, location);
         MockDatabase.LibraryItems.Add(newNewspaper);
         AnsiConsole.MarkupLine("[green]Newspaper added successfully![/]");
     }

     AnsiConsole.MarkupLine("Press Any Key to Continue.");
     Console.ReadKey();
    }

    public void DeleteItem()
    {
        if (MockDatabase.LibraryItems.OfType<Newspaper>().Count() == 0)
        {
            AnsiConsole.MarkupLine("[bold red] There are no newspapers to delete.[/]");
            return;
        }
        /*
            In the case where a newspaper in the newspapers list
            contains a markup character (e.g. "Some_Newspaper [2nd edition]"),
            Prompt(.AddChoices(newspapers)) will throw as it doesn't
            escape the strings. So the selection must be escaped with
            EscapeMarkup()
        */
        string inputNewspaperTitle = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Choose a [red]newspaper[/] to delete")
            .AddChoices(MockDatabase.LibraryItems.OfType<Newspaper>().Select(newspaper => EscapeStringHelper.EscapeMarkup(newspaper.Title))));

        /*
            Undo the escaped string so it looks okay as output'
        */
        inputNewspaperTitle = EscapeStringHelper.UndoEscapeMarkup(inputNewspaperTitle);

        Newspaper? matchingNewspaper = MockDatabase.LibraryItems.OfType<Newspaper>().FirstOrDefault(b => b.Title.Equals(inputNewspaperTitle));
        if (matchingNewspaper != null)
        {
            MockDatabase.LibraryItems.Remove(matchingNewspaper); // Surely it can't be null :D
            AnsiConsole.MarkupLineInterpolated($"[bold green]Newspaper: [bold red]{inputNewspaperTitle}[/] deleted[/].");
        }
        else
        {
            AnsiConsole.MarkupLineInterpolated($"[bold red]{inputNewspaperTitle} not found[/]");
        }

        AnsiConsole.MarkupLine("Press Any Key to Continue.");
        Console.ReadKey();
    }
}
