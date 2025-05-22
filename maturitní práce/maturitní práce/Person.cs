// See https://aka.ms/new-console-template for more information
namespace MaturitniPrace;
public class Person//osoba
{
    public string Name { get; set; }//jméno
    public Adress Adress { get; set; }//adresa
    public int Id {  get; set; }//číslo průkazu do knihovny
    public string PhoneNumber { get; set; }//telefoní číslo
    public string Email { get; set; }


    public Person(string name,Adress adress,int id,string pNumber,string email)
    {
        Name = name;
        Adress= adress;
        Id= id;
        PhoneNumber = pNumber;
        Email = email;
    }
}