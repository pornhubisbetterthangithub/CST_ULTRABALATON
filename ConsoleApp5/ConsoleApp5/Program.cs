using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST_Ultrabalaton
{
    class Program
    {
        struct maraton
        {
            public string name;
            public int startnumber;
            public string sex;
            public int hour;
            public int min;
            public int sec;
            public int percentage;
        }
        static maraton[] runner = new maraton[500];
        static double timeinhourfv(int hour, int min, int sec)
        {
            double hourfix = hour + min / 60 + sec / 3600;
            return hourfix;
        }
        static void Main(string[] args)
        {
            string[] fromfile = File.ReadAllLines("ub2017egyeni.txt");
            int lines = 0;
            int i;
            for (int k = 1; k < fromfile.Count(); k++)
            {
                string[] countlines = fromfile[k].Split(';');
                runner[lines].name = countlines[0];
                runner[lines].startnumber = Convert.ToInt32(countlines[1]);
                runner[lines].sex = countlines[2];
                string[] countlines2 = countlines[3].Split(':');
                runner[lines].hour = Convert.ToInt32(countlines2[0]);
                runner[lines].min = Convert.ToInt32(countlines2[1]);
                runner[lines].sec = Convert.ToInt32(countlines2[2]);
                runner[lines].percentage = Convert.ToInt32(countlines[4]);
                lines++;
            }
            int runners = lines;
            Console.WriteLine("3. feladat: Egyéni indulók: "+ runners + " fő");
            int femalerunners = 0;
            for (i = 0; i < runners; i++)
            {
                if (runner[i].sex == "Noi" && runner[i].percentage == 100)
                {
                    femalerunners++;
                }
            }
            Console.WriteLine("4. feladat: Célba érkező női sportolók: "+ femalerunners + " fő" );
            Console.Write("5. feladat: Kérem a sportoló nevét: ");
            string runnername = Console.ReadLine();
            i = 0;
            while (i < runners && runnername != runner[i].name)
            {
                i++;
            }
            if (i < runners)
            {
                Console.WriteLine("\tIndult egyéniben  a sportoló? Igen");
                if (runner[i].percentage == 100)
                {
                    Console.WriteLine("\tTeljesítette a teljes távot? Igen");
                }
                else 
                {
                    Console.WriteLine("\tTeljesítette a teljes távot? Nem");
                }
            }
            else
            {
                Console.WriteLine("\tIndult egyéniben  a sportoló? Nem");
            }
            double timeinhour = 0;
            int malerunners = 0;
            foreach (var check in runner)
            {
                if (check.sex == "Ferfi" && check.percentage == 100)
                {
                    timeinhour += timeinhourfv(check.hour, check.min, check.sec);
                    malerunners++;
                }
            }
            Console.WriteLine("7. feladat: Átlagos idő: " + timeinhour / malerunners + " óra");
            string winnermale = runner[0].name;
            string winnerfemale = runner[0].name;
            int winnermalelinenumber = 0;
            int winnerfemalelinenumber = 0;
            double gyoztesidoferfi = 100;
            double gyoztesidonoi = 100;
            for (i = 0; i < runners; i++)
            {
                if (runner[i].sex == "Ferfi" && runner[i].percentage == 100)
                {
                    if ((timeinhourfv(runner[i].hour, runner[i].min, runner[i].sec)) < gyoztesidoferfi)
                    {
                        gyoztesidoferfi = timeinhourfv(runner[i].hour, runner[i].min, runner[i].sec);
                        winnermalelinenumber = i;
                        winnermale = runner[i].name;
                    }
                }
                if (runner[i].sex == "Noi" && runner[i].percentage == 100)
                {
                    if ((timeinhourfv(runner[i].hour, runner[i].min, runner[i].sec)) < gyoztesidonoi)
                    {
                        gyoztesidonoi = timeinhourfv(runner[i].hour, runner[i].min, runner[i].sec);
                        winnerfemalelinenumber = i;
                        winnerfemale = runner[i].name;
                    }
                }
            }
            Console.WriteLine("8. feladat: Verseny győztesei\n\tNők " + winnerfemale + " " + runner[winnerfemalelinenumber].startnumber + " " + runner[winnerfemalelinenumber].ora + " " + runner[winnerfemalelinenumber].min + " " + runner[winnerfemalelinenumber].sec);
            Console.WriteLine("\tFérfiak "+ winnermale + " " + runner[winnermalelinenumber].startnumber + " " + runner[winnermalelinenumber].ora + " " + runner[winnermalelinenumber].min + " " + runner[winnermalelinenumber].sec);
            Console.ReadKey();
        }
    }
}