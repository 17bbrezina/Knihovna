// See https://aka.ms/new-console-template for more information
namespace MaturitniPrace;

public class Adress
{
    public string City { get; set; }//město
    public string Street { get; set; }//ulice
    public int HouseNo {  get; set; }//číslo popisné
    public int ZipCode {  get; set; }//PSČ

    public Adress(string city, string street, int houseNo,  int zipCode)
    {
        City = city;
        Street = street;
        HouseNo = houseNo; 
        ZipCode = zipCode;
    }
}

