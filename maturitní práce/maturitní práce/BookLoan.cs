// See https://aka.ms/new-console-template for more information
namespace MaturitniPrace;
public class BookLoan//výpůjčka
{
    public Book Book { get; private set; }//kniha
    public Person Person { get; private set; }//osoba
    public DateTime Date { get;set; }//datum

    public BookLoan(Book book, Person person,DateTime date)
    {
        Book = book;
        Person = person;
        Date = date;
    }
}

