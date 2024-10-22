using System;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace TCSA.OOP.LibraryManagementSystem.Controllers;

class BookController : IBaseController
{
    public void ViewItems()
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);

        table.AddColumn("[yellow]ID[/]");
        table.AddColumn("[yellow]Title[/]");
        table.AddColumn("[yellow]Author[/]");
        table.AddColumn("[yellow]Category[/]");
        table.AddColumn("[yellow]Location[/]");
        table.AddColumn("[yellow]Pages[/]");

        var books = MockDatabase.LibraryItems.OfType<Book>();

        foreach (Book book in books)
        {
            table.AddRow(
                book.Id.ToString(),
                $"[cyan]{book.Title}[/]",
                $"[cyan]{book.Author}[/]",
                $"[green]{book.Category}[/]",
                $"[blue]{book.Location}[/]",
                book.Pages.ToString()
            );
        }
        AnsiConsole.Write(table);

        AnsiConsole.MarkupLine("Press Any Key to Continue.");
        Console.ReadKey();

    }

    public void AddItems()
    {
     var title = AnsiConsole.Ask<string>("Enter the [green]title[/] of the book to add:");
     var author = AnsiConsole.Ask<string>("Enter the [green]author[/] of the book:");
     var category = AnsiConsole.Ask<string>("Enter the [green]category[/] of the book:");
     var location = AnsiConsole.Ask<string>("Enter the [green]location[/] of the book:");
     var pages = AnsiConsole.Ask<int>("Enter the [green]number of pages[/] in the book:");

     if (MockDatabase.LibraryItems.OfType<Book>().Any(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase)))
     {
         AnsiConsole.MarkupLine("[red]This book already exists.[/]");
     }
     else
     {
         var newBook = new Book(MockDatabase.LibraryItems.OfType<Book>().Count() + 1, title, author, category, location, pages);
         MockDatabase.LibraryItems.Add(newBook);
         AnsiConsole.MarkupLine("[green]Book added successfully![/]");
     }

     AnsiConsole.MarkupLine("Press Any Key to Continue.");
     Console.ReadKey();
    }

    public void DeleteItem()
    {
        if (MockDatabase.LibraryItems.OfType<Book>().Count() == 0)
        {
            AnsiConsole.MarkupLine("[bold red] There are no books to delete.[/]");
            return;
        }
        /*
            In the case where a book in the books list
            contains a markup character (e.g. "Some_Book [2nd edition]"),
            Prompt(.AddChoices(books)) will throw as it doesn't
            escape the strings. So the selection must be escaped with
            EscapeMarkup()
        */
        string inputBookTitle = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Choose a [red]book[/] to delete")
            .AddChoices(MockDatabase.LibraryItems.OfType<Book>().Select(book => EscapeStringHelper.EscapeMarkup(book.Title))));

        /*
            Undo the escaped string so it looks okay as output'
        */
        inputBookTitle = EscapeStringHelper.UndoEscapeMarkup(inputBookTitle);

        Book? matchingBook = MockDatabase.LibraryItems.OfType<Book>().FirstOrDefault(b => b.Title.Equals(inputBookTitle));
        if (matchingBook != null)
        {
            MockDatabase.LibraryItems.Remove(matchingBook); // Surely it can't be null :D
            AnsiConsole.MarkupLineInterpolated($"[bold green]Book: [bold red]{inputBookTitle}[/] deleted[/].");
        }
        else
        {
            AnsiConsole.MarkupLineInterpolated($"[bold red]{inputBookTitle} not found[/]");
        }

        AnsiConsole.MarkupLine("Press Any Key to Continue.");
        Console.ReadKey();
    }
}
