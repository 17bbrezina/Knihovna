// See https://aka.ms/new-console-template for more information
using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;
using System.Linq;
namespace MaturitniPrace;

public class Library//knihovna
{

    private List<Book> books = new List<Book>();
    private List<Person> persons = new List<Person>();
    private List<BookLoan> bookloans = new List<BookLoan>();



    // Přidání knihy
    public void AddBook(string title, string author, int pieces)
    {
        books.Add(new Book(title, author, pieces));
    }





    //získání dat pro knihu
    public void GetBook()
    {

        while (true) 
        { 
        Console.WriteLine("\nPro přidání knihy napište její název, pro ukončení nic nezadávejte a dejte enter.");
        string title = Console.ReadLine();
            if (title == "")
            { 
                break;
            }
        Console.WriteLine("Jméno autora?");
        string authorName = Console.ReadLine();
        Console.WriteLine("Počet kusů?");
        int pieces = IntProtect(1,1000); 
        AddBook(title, authorName, pieces);
        Console.WriteLine($"Kniha '{title}' byla přidána.");
        }

    }

    // Odstranění knihy
    public void RemoveBook(string bookTitle)
    {
    var book = books.FirstOrDefault(b =>  b.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase));

        if (book != null)
        {
        books.Remove(book);
        Console.WriteLine($"Kniha '{bookTitle}' byla odstraněna.");

        //odstranění všech výpůjček této knihy
        var bookLoans = bookloans.FindAll(b =>
        $"{b.Book.Title}".Equals(bookTitle, StringComparison.OrdinalIgnoreCase));

                foreach (var bookloan in bookLoans)
                {
                bookloan.Book.ReturnCopy();
                bookloans.Remove(bookloan);
                Console.WriteLine($"Výpůjčka osoby '{bookloan.Person.Name}' knihy '{bookTitle}' byla odstraněna.");
                }

        }
        else
        {
        Console.WriteLine($"Kniha '{bookTitle}' nebyla nalezena.");
        }

    }

    //získání dat pro odstranění knihy
    public void GetRemoveBook()
    {
        Console.WriteLine("\nJakou knihu chcete odstranit?");
        string book = Console.ReadLine();
        RemoveBook(book);
    }




    // Odstranění uživatele
    public void RemovePerson(int personId)
    {
    var person = persons.FirstOrDefault (p=>personId == p.Id) ;
        if (person != null)
        {
        persons.Remove(person);
        Console.WriteLine($"Osoba '{person.Name}' s id {person.Id} byla odstraněna.");

            //odstranění všech výpůjček uživatele
        var bookLoans = bookloans.FindAll(b =>
        b.Person.Id==personId);

                foreach (var bookloan in bookLoans)
                {    
                bookloan.Book.ReturnCopy();
                bookloans.Remove(bookloan);
                Console.WriteLine($"Výpůjčka knihy '{bookloan.Book.Title}' osoby '{person.Name}' byla odstraněna.");
                }
        }
        else
        {
        Console.WriteLine($"Osoba nebyla nalezena.");
        }
        
    }

    //získání dat pros odstranění uživatele
    public void GetRemovePerson()
    {
        Console.WriteLine("\nKtereou osobu chcete odstranit? (číslo průkazu) Pokud jste průkaz ztratili a číslo si nepamatujete zadejte celé jméno.");
        string strPersonID = Console.ReadLine();
        if (!int.TryParse(strPersonID, out int personId))
        {
            personId = PersonForgotId(strPersonID);
        }
        RemovePerson(personId);
    }





    // Přidání uživatele
    public void AddPerson(string name,string city,string street,int houseNo,int zipCode,string pNumber,string email)
    {
    int id = persons.Count+1;
        while (true)
        {
        var personId= persons.FirstOrDefault(p => p.Id == id);
                if (personId==null)
                {
                var adress = new Adress(city, street, houseNo, zipCode);
                persons.Add(new Person(name,adress,id,pNumber,email));
                Console.WriteLine($"Číslo vaší prúkazky je {id}, budete se jím prokazovat při vypůjčování knih.");
                Console.ReadKey();
                return; 
                }
        id++;     
        }

    }





    //získání dat pro osobu
    public void GetPerson()
    {
        while (true)
        { 
        Console.WriteLine("\nPro přidání osoby napište celé jméno,pro ukončení nic nezadávejte a dejte enter.");
        string name = Console.ReadLine();
            if (name == "") 
            { break; }
        Console.WriteLine("V jakém městě bydlíte?");

        string city = Console.ReadLine();
            if (city == "")
            { break; }
            Console.WriteLine("V jaké ulici bydlíte(pouze název)?"); 

        string street = Console.ReadLine();
            if (street == "")
            { break; }
            Console.WriteLine("Číslo popisné?");

        int houseNo = IntProtect(0,99999);
            Console.WriteLine("PSČ?");

        int zipCode = IntProtect(0, 99999);
            Console.WriteLine("Vaše telefoní číslo?");

        string PNumber = Console.ReadLine();
            if (PNumber == "")
            { break; }
            Console.WriteLine("Váš email?");

        string Email = Console.ReadLine();
            if (Email == "")
            { break; }

        AddPerson(name,city,street,houseNo,zipCode,PNumber,Email);
        Console.WriteLine($"Osoba '{name} ' byl přidán.");
        }

    }




    // Vytvoření výpůjčky
    public void AddBookLoan(Person person,Book book ,DateTime date,bool a)
    {


        if (a)
        {
        bookloans.Add(new BookLoan(book, person, date));
        return;
        }

        if (book.BorrowCopy())
        {
        bookloans.Add(new BookLoan(book, person, date));
        Console.WriteLine($"Výpůjčka vytvořena: '{book.Title}' pro osobu '{person.Name}'.");

        }
        else
        {
            Console.WriteLine($"Kniha '{book.Title}' není momentálně dostupná.");
        }
    }




    //získání dat pro výpůjčku
    public void GetBookLoan()
    {
        if(books.Any() && persons.Any()) 
        { 
            while (true) { 
            Console.WriteLine("\nPro vypůjčení knihy napište její název, pro ukončení nic nezadávejte a dejte enter");
            string bookTitle= Console.ReadLine();
                if (bookTitle == "")
                {
                    break;
                }
            var book = books.FirstOrDefault(b => b.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase));
                if (book == null)
                {
                    Console.WriteLine("Tuto knihu nemáme");
                    return;
                }

            Console.WriteLine("Číslo průkazky do knihovny? Pokud si ho nepamatujete zadejte celé jméno");
                string strPersonID = Console.ReadLine();
                if (!int.TryParse(strPersonID, out int personId))
                {
                    personId = PersonForgotId(strPersonID);
                }

                var personById = persons.FirstOrDefault(p =>  personId == p.Id);
                if (personById == null)
                {
                    Console.WriteLine("Tuto osobu nemáme zapsanou");
                    return;
                }

                DateTime date = DateTime.Today;
            AddBookLoan(personById, book,date,false);
            }
        }
        else if(books.Any())
        {
            Console.WriteLine("Nelze vypůjčit knihu, přidejte osobu");
        }
        else if(persons.Any())
        {
            Console.WriteLine("Nelze vypůjčit knihu, musíte napřed nějakou přidat");
        }
        else
        {
            Console.WriteLine("Nelze vypůjčit knihu,přidejte alespoň 1 osobu a 1 knihu");
        }

    }



    // Vrácení knihy
    public void ReturnBook(BookLoan bookLoan)
    {

        if (bookLoan != null)
        {
            bookloans.Remove(bookLoan);
            bookLoan.Book.ReturnCopy();
            Console.WriteLine($"Kniha '{bookLoan.Book.Title}' vypůjčena osobou '{bookLoan.Person.Name}' byla vrácena.");
        }
        else
        {
            Console.WriteLine($"Výpůjčka knihy '{bookLoan.Book.Title}' pro osobu '{bookLoan.Person.Name}' nebyla nalezena.");
        }
    }



    //získání dat pro vrácení knihy
    public void GetReturnBook()
    {
        if (bookloans.Any())
        {
            Console.WriteLine("\nJakou knihu chcete vrátit?");
            string bookTitle = Console.ReadLine();

            var bookLoan = bookloans.FirstOrDefault(b => b.Book.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase));
            if (bookLoan == null)
            {
                Console.WriteLine("Tuto knihu nemá momentálně nikdo vypůjčenou");
                return;
            }

            Console.WriteLine("Číslo vaší průkazky do knihovny? Pokud si ho nepamatujete zadejte celé jméno");

            string strPersonID = Console.ReadLine();
            if (!int.TryParse(strPersonID, out int personId))
            {
                personId = PersonForgotId(strPersonID);
            }


            var bookLoanById = bookloans.FirstOrDefault(b => personId == b.Person.Id
            && b.Book.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase));
            if (bookLoanById == null)
            {
                Console.WriteLine("Tato osoba nemá tuto knihu vypůjčenou");
                return;
            }

            ReturnBook(bookLoanById);
        }
        else
        {
            Console.WriteLine("Nikdo u nás nemá půjčenou knihu.");
        }
    }



    // Zobrazení všech knih
    public void ShowBooks()
    {
        if (!books.Any())
        {
            Console.WriteLine("nejsou zapsány žádné knihy");
            return;
        }
        Console.WriteLine("\nSeznam knih:");
        foreach (var book in books)
        {
            Console.WriteLine($"Kniha: {book.Title}, Autor: {book.AuthorName}, Počet dostupných výtisků: {book.Pieces}");
        }
    }



    // Zobrazení uživatelů
    public void ShowPerson()
    {
        if (!persons.Any())
        {
            Console.WriteLine("nejsou zapsány žádné osoby");
            return;
        }
        Console.WriteLine("\nSeznam osob:");
        foreach (var person in persons)
        {
            Console.WriteLine($"Jmeno: {person.Name}, číslo průkazu {person.Id}," +
                $" adresa: {person.Adress.City}, {person.Adress.Street}, {person.Adress.HouseNo}, PSČ: {person.Adress.ZipCode} " +
                $"\ntelefoní číslo: {person.PhoneNumber}, email: {person.Email}");
        }
    }



    // Zobrazení výpůjček
    public void ShowBookLoans()
    {
        if (!bookloans.Any())
        {
            Console.WriteLine("nejsou zapsány žádné výpůjčky");
            return;
        }
        Console.WriteLine("\nSeznam výpůjček:");
        foreach (var bookLoan in bookloans)
        {
            Console.WriteLine($"Knihu: {bookLoan.Book.Title}, si vypujčila osoba:{bookLoan.Person.Name}, " +
                $"s průkazem č. {bookLoan.Person.Id} dne:{bookLoan.Date.ToString("dd/MM/yyyy")}");
        }
    }



    //ukládání osoby do souboru
    public void SavePerson()
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(basePath, "Persons.txt");

        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, append: false))
            {
                foreach (var person in persons)
                {
                    writer.WriteLine($"{person.Name},{person.Id},{person.Adress.City},{person.Adress.Street},{person.Adress.HouseNo},{person.Adress.ZipCode},{person.PhoneNumber},{person.Email}");
                }
            }

            Console.WriteLine("Seznam osob uložen.");
        }
        catch (Exception ex) 
        {

            Console.WriteLine($"Došlo k chybě:{ex.Message}.");
        }
    }

  
  

    //nahrávání osoby ze souboru
    public void LoadPerson()
    {
        string basePath=AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(basePath, "Persons.txt");
        if (File.Exists(filePath)) 
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null) 
                {
                    string[] word  =line.Split(",");
                    string name = word[0];
                    int id=Convert.ToInt32(word[1]);
                    string city = word[2];
                    string street = word[3];
                    int houseNo = Convert.ToInt32(word[4]);
                    int zipCode = Convert.ToInt32(word[5]);
                    var adress = new Adress(city, street, houseNo, zipCode);
                    string pNumber = word[6];
                    string email = word[7];
                    persons.Add(new Person(name, adress, id,pNumber,email));
                }
            }
        }
    }



    //ukládání knih do souboru
    public void SaveBook()
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(basePath, "Books.txt");

        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, append: false))
            {
                foreach (var book in books)
                {
                    writer.WriteLine($"{book.Title},{book.AuthorName},{book.Pieces}");
                }
            }

            Console.WriteLine("Seznam knih uložen.");
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Došlo k chybě:{ex.Message}.");
        }
    }




    //nahrávání knih ze souboru
    public void LoadBook()
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(basePath, "Books.txt");
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null) 
                {
                    string[] word = line.Split(",");
                    string title = word[0];
                    string author = word[1];
                    int pieces = Convert.ToInt32(word[2]);
                    AddBook(title,author, pieces);
                }
            }
        }
    }


    //ukládání výpujček do souboru
    public void SaveBookLoan()
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(basePath, "BookLoans.txt");

        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, append: false))
            {
                foreach (var bookloan in bookloans)
                {
                    writer.WriteLine($"{bookloan.Person.Name},{bookloan.Person.Id},{bookloan.Book.Title},{bookloan.Date}");
                }
            }

            Console.WriteLine("Seznam výpůjček uložen.");
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Došlo k chybě:{ex.Message}.");
        }
    }




    //nahrávání výpůjček ze souboru
    public void LoadBookLoan()
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(basePath, "BookLoans.txt");
        if (File.Exists(filePath)) 
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null) 
                {
                    string[] word = line.Split(",");
                    string personName = word[0];
                    string Idstr = word[1];
                    int.TryParse(Idstr, out int Id);
                    string bookTitle = word[2];
                    DateTime date = DateTime.Parse(word[3]);

                    var person = persons.FirstOrDefault(p => Id == p.Id);
                    var book = books.FirstOrDefault(b => b.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase));

                    AddBookLoan(person, book,date,true);
                }
            }
        }
    }


    //ochrana před neplatným vstupem do int
    public int IntProtect(int min,int max) { 
     while (true)
        {
            string a =Console.ReadLine();
            if (int.TryParse(a, out var b))
            { 
                if (b >= min && b < max)
                { 
                return b;
                }
                else
                {
                    Console.WriteLine("neplatný vstup");
                }                 
            }
            else 
            {
                Console.WriteLine("neplatný vstup, zadejte číslo");
            }   
        }
    }
    //uživatel zapomněl id
    public int PersonForgotId(string personName)
    {
        var personsByName = persons.FindAll(p =>
        $"{p.Name}".Equals(personName, StringComparison.OrdinalIgnoreCase));
            if(personsByName.Count == 1)
            {
            var person =persons.FirstOrDefault(p =>
            $"{p.Name}".Equals(personName, StringComparison.OrdinalIgnoreCase));
            Console.WriteLine($"Jméno: {person.Name}, číslo průkazu {person.Id},telefoní číslo {person.PhoneNumber}" +
             $" adresa:{person.Adress.City}{person.Adress.Street}{person.Adress.HouseNo}");
            return person.Id;
            
            }
            if (personsByName.Any()) 
        {
            Console.WriteLine("Zadejte svoje telefoní číslo");
            string pNumber=Console.ReadLine();
            var person = persons.FirstOrDefault(p =>
            $"{p.Name}".Equals(personName, StringComparison.OrdinalIgnoreCase)&&p.PhoneNumber==pNumber);
                if (person != null)
                {
                return person.Id;
                }
                else
                {
                return -1;
                }
        }
        return -1;
    }
}
