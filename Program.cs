namespace Bibliotek
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ladda upp info om böcker och låntagare vid start av program från filer
            Bibliotek bibliotek = new Bibliotek();
            bibliotek.bibliotekBöcker = bibliotek.LaddaUppBokInfo();
            bibliotek.låntagarLista = bibliotek.LaddaUppLåntagarInfo();


            string menuChoice;
            int result = 0;

            while (result != 6)
            {
                Console.WriteLine(" 1: Lägga till nya böcker i biblioteket.\n 2: Låna ut böcker till låntagare.\n 3: Återlämna böcker.\n 4: Visa tillgängliga böcker.\n 5: Visa låntagare och deras lånade böcker.\n 6: Stäng av ");

                // En meny som använder metoder från biblioteks klassen
                menuChoice = Console.ReadLine();
                if (int.TryParse(menuChoice, out result)) // // try parse för att varna användare om fel inmatning
                {
                    Console.Clear();
                    switch (result)
                    {

                        case 1:
                            Console.WriteLine("Lägga till nya böcker i biblioteket.");
                            bibliotek.LäggTillBok();
                            break;
                        case 2:
                            Console.WriteLine("Låna ut böcker till låntagare.");
                            bibliotek.LånaUt();
                            break;
                        case 3:
                            Console.WriteLine("Återlämna böcker.");
                            bibliotek.ÅterLämna();
                            break;
                        case 4:
                            Console.WriteLine("Tillgängliga böcker.");
                            bibliotek.VisaBöcker();
                            break;
                        case 5:
                            Console.WriteLine("Visa låntagare och deras lånade böcker.");
                            bibliotek.VisaLåntagare();
                            break;
                        case 6:
                            Console.WriteLine("Stäng av");
                            // spara info här vid slutet av sessionen, användaren måste stänga av programmet med 6 för att spara böcker och låntagar information
                            bibliotek.SparaBokJson();
                            bibliotek.SparaLåntagareJson();
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Ogiltigt val");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Fel inmatning, använd menyval med siffror 1 till 6"); // felmedelande vid fel inmatning
                }
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}