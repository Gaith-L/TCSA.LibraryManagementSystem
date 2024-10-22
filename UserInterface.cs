using Spectre.Console;
using TCSA.OOP.LibraryManagementSystem.Controllers;
namespace TCSA.OOP.LibraryManagementSystem;

public class UserInterface
{
    private static BookController _booksController = new();
    private static NewspaperController _newspaperController = new();
    private static MagazineController _magazineController = new();

    public void MainMenu()
    {
        while (true)
        {
            Console.Clear();

            var menuChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("What do you want to do next?")
                    .AddChoices(MenuOptions.AllMenuChoices));

            var itemChoice = AnsiConsole.Prompt(
                                            new SelectionPrompt<string>()
                                            .Title("Select the item type?")
                                            .AddChoices(MenuOptions.AllLibraryItems));

            switch (menuChoice)
            {
                case MenuOptions.ViewItems:
                    {
                        MenuOptions.ViewItems_f(itemChoice);
                        break;
                    }

                case MenuOptions.AddItem:
                    {
                        MenuOptions.AddItem_f(itemChoice);
                        break;
                    }

                case MenuOptions.DeleteItem:
                    {
                        MenuOptions.DeleteItem_f(itemChoice);
                        break;
                    }
            }
        }
    }

    internal static class MenuOptions
    {
        public const string ViewItems = "View Items";
        public const string AddItem = "Add Item";
        public const string DeleteItem = "Delete Item";

        public static string[] AllMenuChoices = new[] { ViewItems, AddItem, DeleteItem };

        public const string Books = "Books";
        public const string Magazines = "Magazines";
        public const string Newspapers = "Newspapers";

        public static string[] AllLibraryItems = new[] { Books, Magazines, Newspapers };

        public static void ViewItems_f(string itemType)
        {
            switch (itemType)
            {
                case Books:
                {
                    _booksController.ViewItems();
                    break;
                }
                case Magazines:
                {
                    _magazineController.ViewItems();
                    break;
                }
                case Newspapers:
                {
                    _newspaperController.ViewItems();
                    break;
                }
            }
        }

        public static void AddItem_f(string itemType)
        {
            switch (itemType)
            {
                case Books:
                {
                    _booksController.AddItems();
                    break;
                }
                case Magazines:
                {
                    _magazineController.AddItems();
                    break;
                }
                case Newspapers:
                {
                    _newspaperController.AddItems();
                    break;
                }
            }
        }

        public static void DeleteItem_f(string itemType)
        {
            switch (itemType)
            {
                case Books:
                {
                    _booksController.DeleteItem();
                    break;
                }
                case Magazines:
                {
                    _magazineController.DeleteItem();
                    break;
                }
                case Newspapers:
                {
                    _newspaperController.DeleteItem();
                    break;
                }
            }
        }
    }
}
