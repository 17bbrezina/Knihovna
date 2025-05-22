// See https://aka.ms/new-console-template for more information
namespace MaturitniPrace;
public class Book//kniha
{
    public string Title { get; set; }//název
    public string AuthorName { get; set; }//autor
    public int Pieces { get; private set; }//počet kusů
    public Book(string title, string author, int pieces)
    {
        Title = title;
        AuthorName = author;
        Pieces = pieces;
    }

    // Odečtení výtisku při výpůjčce
    public bool BorrowCopy()
    {
        if (Pieces > 0)
        {
            Pieces--;
            return true; 
        }
        else
        {
            return false; 
        }
    }
    //přičtení výtisku při vrácení
    public void ReturnCopy()
    {
        Pieces++;
    }
}
