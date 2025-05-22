// See https://aka.ms/new-console-template for more information
using System;
using System.Security.Cryptography.X509Certificates;

namespace MaturitniPrace;
internal class Program
{

   

    static void Main(string[] args)
    {
       Library library = new Library();
        library.LoadPerson();
        library.LoadBook();
        library.LoadBookLoan();
        choice(library);
    }

    public static void choice(Library library)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine
                (

                "Knihovna\n" +
                "\n1.Zobrazit knihy.\n" +
                "2.Zobraz výpůjčky.\n" +
                "3.Zobraz osoby\n" +
                "\n4.Přidat knihu\n" +
                "5.Přidat osobu\n" +
                "6.Vypůjčit knihu\n" +
                "\n7.Vrátit knihu\n" +
                "8.Odstranit knihu\n" +
                "9.Odstranit osobu\n" +
                "\n10.Konec programu"
                );
            string entry = Console.ReadLine();
            if(!int.TryParse(entry, out int intEntry))
                {
                Console.WriteLine("Neplatný vstup.");
                }
            else 
            {
                Console.Clear();
                switch (intEntry)
                {
                    default:
                    Console.WriteLine("Neplatný vstup zkuste to znovu.");
                    break;

                    case 1:
                    library.ShowBooks();
                    Console.WriteLine();
                        Console.ReadKey();
                        break;

                    case 2:
                    library.ShowBookLoans();
                    Console.WriteLine();
                        Console.ReadKey();
                        break;

                    case 3:
                    library.ShowPerson();
                    Console.WriteLine();
                        Console.ReadKey();
                        break;

                    case 4:
                    library.GetBook();
                    Console.WriteLine();
                    break;
                        
                    case 5:
                    library.GetPerson();
                    Console.WriteLine();
                        break;

                    case 6:
                    library.GetBookLoan();
                    Console.WriteLine();
                    break;   
                        
                    case 7:
                    library.GetReturnBook();
                    Console.WriteLine();
                        Console.ReadKey();
                        break;

                    case 8:
                    library.GetRemoveBook();
                    Console.WriteLine();
                        Console.ReadKey();
                        break;

                    case 9:
                    library.GetRemovePerson();
                    Console.WriteLine();
                        Console.ReadKey();
                        break;

                    case 10:
                    library.SavePerson();
                    library.SaveBook();
                    library.SaveBookLoan();
                    return;
                }
                
            }
        }
    }
}
