using Newtonsoft.Json;

namespace Bibliotek
{
    internal class Bibliotek
    {
        public List<Bok> bibliotekBöcker;
        public List<Låntagare> låntagarLista;
        

        public Bibliotek() // För framtida nya Bibliotek
        {
            bibliotekBöcker = new List<Bok>();
            låntagarLista = new List<Låntagare>();
        }

        // metoder i program
        public void LäggTillBok() // metod för att lägga till böcker
        {
            Console.WriteLine("Titel på bok?");
            string titel = Console.ReadLine().ToLower();
            Console.WriteLine("Författare?");
            string författare = Console.ReadLine();

            // använder mig utan metoder BokFinns för felhantering, om allt är okej så läggs boken till i listan.
            if (BokFinns(titel, författare))
            {
                Console.WriteLine("Denna boken finns redan, vi behöver inte fler!");
            }
            else
            {
                Bok bok = new Bok(titel, författare);
                bibliotekBöcker.Add(bok); // tar användar inmatning och lägger till i listan bibliotekBöcker
                Console.WriteLine($"Nu har vi lagt till boken {titel} i systemet");
            }
        }
        
        private bool BokFinns(string titel, string författare) // För att kolla så att samma bok inte blir registrerad två gånger.
        {
            foreach (var bok in bibliotekBöcker)
            {
                if (bok.Titel == titel && bok.Författare == författare)
                {
                    return true;
                }
            }
            return false;
        }

        public string Tillgänglig(bool utlånad) // Kollar om bok är tillgänglig
        {
            if (utlånad == false) { return "Är tillgänglig"; }
            else { return "Utlånad"; }
        }

        public void VisaBöcker() // metod för att visa böcker
        {
            Console.WriteLine();
            // Visar alla böcker i listan
            foreach (Bok item in bibliotekBöcker)
            {
                Console.WriteLine($"Titel: {item.Titel}\nFörfattare: {item.Författare}\nTillgänglighet: {Tillgänglig(item.Utlånad)}");
                Console.WriteLine("---------------------------------------");
            }
        }

        public void LånaUt() // metod för att låna ut böcker och registrera/logga in
        {
            bool finnsBöcker = false;
            foreach (Bok bok in bibliotekBöcker) //kolla så det finns tillgängliga böcker att låna ut
            {
                if (bok.Utlånad == false) 
                { 
                    finnsBöcker = true;
                }
            }
            if(finnsBöcker == false) //om det inte finns någon tillgänglig bok så går vi tillbaka till menyn
            {
                Console.WriteLine("Det finns tyvärr inga tillgängliga böcker");
                return; 
            }

            long Personnummer;
            Console.WriteLine("Logga in eller registrera dig");
            Console.WriteLine("---------------------------------------\n");

            Console.WriteLine("Vad är ditt personnummer YYMMDDXXXX");
            while (true)
            {
                // Gör om personnummret till en string och räknar hur många tecken som finns, kollar även så rätt inmatning används
                if (long.TryParse(Console.ReadLine(), out Personnummer) && Personnummer.ToString().Length == 10)
                {
                    break;
                }
                Console.WriteLine("\nVänligen använd 10 siffror, YYMMDDXXXX");
            }


            bool match = false;
            // Skapar tillfällig användare
            Låntagare currentLåntagare = null;
            // Tillåter listan att vara null
            if (låntagarLista != null)
            {
                foreach (Låntagare person in låntagarLista)
                {
                    // kollar igenom lista och ifall personnummer som registreras redan finns så används befintligt
                    if (Personnummer == person.Personnummer)
                    {
                        Console.Clear();
                        Console.WriteLine($"Inloggad som {person.Namn} : {person.Personnummer}\nTryck enter för att gå vidare");
                        Console.ReadKey();
                        currentLåntagare = person;
                        match = true; break;
                    }
                }
            }

            // skapar en ny användare om personnummer inte finns
            if (match == false)
            {
                Console.WriteLine("Vad är ditt namn?");
                string Namn = Console.ReadLine().ToLower();

                Låntagare låntagare = new Låntagare(Namn, Personnummer);
                låntagarLista.Add(låntagare);
                // tillfällig blir låntagare
                currentLåntagare = låntagare;
                Console.Clear();
                Console.WriteLine($"Skapar användare {Namn} : {Personnummer}\nTryck enter för att gå vidare");
                Console.ReadKey();
            }

            Console.Clear();
            Console.WriteLine("Välj vilken bok du vill låna\n");
            int count = 1;
            foreach (Bok item in bibliotekBöcker)
            {
                Console.WriteLine($"Bok {count}:\nTitel: {item.Titel}\nFörfattare: {item.Författare}\nTillgänglighet: {Tillgänglig(item.Utlånad)}");
                Console.WriteLine("---------------------------------------");
                count++;
            }
            // användare väljer bok och titeln läggs in som utlånad, registrering av boken och val av bok är ToLower så man inte kan välja fel.
            while (true)
            {
                string bookChoice = Console.ReadLine().ToLower();
                foreach (Bok item in bibliotekBöcker)
                {

                    if (bookChoice == item.Titel && item.Utlånad == false) //om boken finns och är tillgänglig så lånas den ut
                    {
                        item.Utlånad = true;
                        currentLåntagare.lånadeböcker.Add(item);
                        Console.WriteLine("Bok utlånad");
                        return;
                    }
                }
                Console.WriteLine("Den boken har vi inte tillgänglig, välj en annan");
            }
        }

        public void ÅterLämna() // metod för att lämna tillbaka böcker som är lånade
        {
            // Loggar in anvädare med personnummer
            long Personnummer;
            Console.WriteLine("Vad är användarens personummer YYMMDDXXXX");
            while (true)
            {
                // Gör om personnummret till en string och räknar hur många tecken som finns, kollar även så rätt inmatning används
                if (long.TryParse(Console.ReadLine(), out Personnummer) && Personnummer.ToString().Length == 10)
                {
                    break;
                }
                Console.WriteLine("\nVänligen använd 10 siffror, YYMMDDXXXX");
            }
            // kollar igenom personer i låntagarlistan
            // Om personnummret inte finns så får man testa igen.
            foreach (Låntagare person in låntagarLista)
            {
                if (person.Personnummer == Personnummer)
                {
                    //går igenom personens lånadeböcker
                    if (person.lånadeböcker.Count != 0)
                    {
                        Console.WriteLine("\nVilken bok ska lämnas tillbaka");
                        Console.WriteLine("Skriv in titel");
                        Console.WriteLine("---------------------------------------");
                        foreach (Bok böcker in person.lånadeböcker)
                        {
                            Console.WriteLine($"{böcker.Titel}");
                        }
                        Console.WriteLine("---------------------------------------");

                        while (true) // Användare måste skriva rätt bok titel annars får man välja igen
                        {
                            string choice = Console.ReadLine();
                            for (int i = 0; i < person.lånadeböcker.Count; i++)
                            {
                                Bok bok = person.lånadeböcker[i];
                                if (choice == bok.Titel)
                                {
                                    foreach (Bok b in bibliotekBöcker)
                                    {
                                        if (b.Titel == bok.Titel)
                                        {
                                            b.Utlånad = false;
                                            break;
                                        }
                                    }
                                    person.lånadeböcker.Remove(bok);
                                    Console.WriteLine("Bok har lämnats tillbaka");
                                    return;
                                }
                            }
                            Console.WriteLine();
                            Console.WriteLine("Den boken lånar du inte, välj en annan");
                        }
                    }
                    else { Console.WriteLine("\nInga böcker lånade"); return; }
                } 
            }
            Console.WriteLine("\nDet finns ingen användare med det personnummret");
        }

        public void VisaLåntagare() // metod för att visa alla låntagare
        {
            Console.WriteLine();
            foreach (Låntagare person in låntagarLista)
            {
                Console.WriteLine($"Namn:{person.Namn}\nPersonnumer: {person.Personnummer}\nLånade böcker: ");
                foreach (Bok lånadeböcker in person.lånadeböcker)
                {
                    Console.WriteLine(lånadeböcker.Titel);
                }
                Console.WriteLine();
            }
        }



        // samtliga metoder används för att synka programmet med Json och att uppdatera listor
        public void SparaBokJson()
        {
            string jsonFilePath = "Böcker.json";

            if (File.Exists(jsonFilePath))
            {
                File.Delete(jsonFilePath);
            }

            var json = JsonConvert.SerializeObject(bibliotekBöcker, Formatting.Indented);
            File.WriteAllText(jsonFilePath, json);
            Console.WriteLine("Bok sparad till filen: " + jsonFilePath);
        }

        public void SparaLåntagareJson()
        {
            string jsonFilePath = "Låntagare.json";
            if (File.Exists(jsonFilePath))
            {
                File.Delete(jsonFilePath);
            }

            var json = JsonConvert.SerializeObject(låntagarLista, Formatting.Indented);
            File.WriteAllText(jsonFilePath, json);
            Console.WriteLine("låntagare sparad till filen: " + jsonFilePath);
        }

        public List<Bok> LaddaUppBokInfo()
        {
            string jsonFilePath = "Böcker.json";

            if (File.Exists(jsonFilePath))
            {
                var json = File.ReadAllText(jsonFilePath);
                return JsonConvert.DeserializeObject<List<Bok>>(json);
            }
            else
            {
                List<Bok> bok = new List<Bok>();
                return bok;
            }
        }

        public List<Låntagare> LaddaUppLåntagarInfo()
        {
            string jsonFilePath = "Låntagare.json";

            if (File.Exists(jsonFilePath))
            {
                var json = File.ReadAllText(jsonFilePath);
                return JsonConvert.DeserializeObject<List<Låntagare>>(json);
            }
            else
            {
                List<Låntagare> lånare = new List<Låntagare>();
                return lånare;
            }
        }
    }
}