using Labb2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Labb2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using SchoolDbContext db = new SchoolDbContext();
            


            Menu(db);
           
        }
        public static void Menu(SchoolDbContext db)
        {
            bool keepGoing = true;
            do
            {

                Console.WriteLine("Välkommen, du får nu göra 3 val: ");
                Console.WriteLine(@$"
1: Vill du se alla elever på skolan: 
2. Vill du se alla elever i en specifik klass: 
3. Visa all personal: 
4. Vill du skapa ny personal: ");
                Console.WriteLine("Gör ditt val 1-4");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        ShowAllStudents(db);
                        break;
                    case 2:
                         string userClass = ShowClasses();
                        ShowStudentsInClass(db, userClass);
                        break;
                    case 3:
                        ShowPersonell(db);
                        break;
                    case 4:
                        CreatePersonell(db);
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val...");
                        break;

                }
                Console.WriteLine("Vill du fortsätta? ja/nej");
                string userinput = Console.ReadLine().ToLower();
                if (userinput != "ja")
                {
                    keepGoing = false;
                }
            } while (keepGoing);
        }
        public static void CreatePersonell(SchoolDbContext db)
        {
            Console.WriteLine("Du vill skapa en ny medarbetare! ");
            Console.Write("Ange förnamn: ");
            string firstName = Console.ReadLine();
            Console.Write("Ange efternamn: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Ange roll: \n1 = Lärare \n2 = Rektor \n3 = Administratör");
            Console.Write("Gör ett val 1-3: ");
            int role = Convert.ToInt32(Console.ReadLine());
            var user = AddPersonell(firstName, lastName, role);
            db.Personals.Add(user);
            db.SaveChanges();
        }
        public static void ShowAllStudents(SchoolDbContext db)
        {
            Console.WriteLine($@"
Hur vill du sortera eleverna? 
1. Stigande efter förnamn: 
2. Fallande efter förnamn: 
3. Stigande efter efternamn: 
4. Fallande efter efternamn: ");
            Console.Write("Ange ditt val mellan 1-4: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            IEnumerable<Studenter> sortedStudent;
            switch (choice)
            {
                case 1:
                    sortedStudent = db.Studenters.OrderBy(s => s.Elevförnamn);
                    break;
                case 2:
                    sortedStudent = db.Studenters.OrderByDescending(s => s.Elevförnamn);
                    break;
                    break;
                case 3:
                    sortedStudent = db.Studenters.OrderBy(s => s.Elevefternamn);
                    break;
                case 4:
                    sortedStudent = db.Studenters.OrderByDescending(s => s.Elevefternamn);
                    break;
                default:
                    Console.WriteLine("Ogiltigt val, du får nu en osorterad lista.");
                    sortedStudent = db.Studenters;
                    break;
            }

            //var result = db.Studenters;
            foreach (var s in sortedStudent)
            {
                Console.WriteLine($@"
Förnamn = {s.Elevförnamn}
Efternamn = {s.Elevefternamn}
Klass = {s.Klassnamn}");
                Console.ReadKey();
            }
            Console.Clear();
        }
        public static string ShowClasses()
        {
            Console.WriteLine("Det finns för nuvarande tre klasser");
            Console.WriteLine("SUT21");
            Console.WriteLine("SUT22");
            Console.WriteLine("SUT23");
            Console.WriteLine("Vilken klass vill du se eleverna i? Mata in exakta klassnamnet");
            string userinput = Console.ReadLine();
            return userinput;
        }
        public static void ShowStudentsInClass(SchoolDbContext db, string classname)
        {
            var result = db.Studenters
                .Include(s => s.Betygs)
                 .ThenInclude(b => b.Kurs)
                    .Include(s => s.Betygs)
                        .ThenInclude(b => b.Betygskala)
                            .Where(s => s.Klassnamn == classname);
            //var result = db.Studenters
            //    .Where(s => s.Klassnamn == classname);
            Console.WriteLine($"**************{classname}**************");
            foreach (var s in result)
            {
                Console.WriteLine($@"
ElevID = {s.ElevId}
Förnamn = {s.Elevförnamn}
Efternamn = {s.Elevefternamn}
Personnummer = {s.Personnummer}
");
                foreach (Betyg betyg in s.Betygs)
                {
                    string kursnamn = betyg.Kurs?.Kursnamn;
                    string betygsskala = betyg.Betygskala.Betydelse;
                    Console.WriteLine($"\nKurs = {kursnamn} \nBetyg = {betygsskala}");
                }
                Console.ReadKey();
                Console.Clear();
            }

        }
        public static Personal AddPersonell(string firstName, string lastName, int role)
        {
            Personal p1 = new Personal()
            {
                Personalförnamn = firstName,
                Personalefternamn = lastName,
                BefattningsId = role
            };

            return p1;
        }
        public static void ShowPersonell(SchoolDbContext db)
        {
            Console.WriteLine("Vill du se listan för personal med befattningen \n1. Lärare \n2. Rektor \n3. Admin");
            Console.WriteLine("Välj en siffra av 1/2/3");
            int choice = Convert.ToInt32(Console.ReadLine());
            var result = db.Personals
                   .Include(p => p.Befattnings)
                   .Where(p => p.BefattningsId == choice);
            foreach (Personal p in result)
            {
                Console.WriteLine($@"
Förnamn = {p.Personalförnamn}
Efternamn = {p.Personalefternamn}
PersonalID = {p.PersonalId}
Roll = {p.Befattnings.Befattningstyp}");
                Console.ReadKey();
                Console.Clear();

            }
        }
    }
}
