using System;

namespace TCSA.OOP.LibraryManagementSystem;

internal static class EscapeStringHelper
{
    internal static string EscapeMarkup(string text)
    {
        return text.Replace("[", "[[").Replace("]", "]]");
    }

    internal static string UndoEscapeMarkup(string text)
    {
        return text.Replace("[[", "[").Replace("]]", "]");
    }
}
