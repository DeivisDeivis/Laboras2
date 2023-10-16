//Duomenys apie saldainius yra faile: pavadinimas, tipas ir kilogramo kaina. Pirmoje eilutėje yra
//savininko pavardė ir vardas. Sukurkite klasę Saldainis vieno saldainio duomenims saugoti. Kiek
//kainuoja saldainiai, jeigu kiekvieno pavadinimo yra po n kilogramų. Kokie nurodyto tipo saldainiai
//brangiausi?
//• Papildykite programą veiksmais su dviejų studentų saldainių rinkiniais. Kiekvieno rinkinio
//duomenys saugomi atskiruose failuose. Kurio studento rinkinys kainuoja brangiau, jeigu pirmasis
//studentas turi visų saldainių pavadinimų po n1 kilogramų, o antrasis – po n2? Surašykite į atskirą
//rinkinį visus abiejų studentų saldainių, kurių kaina didesne už k pinigų, duomenis.

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Saldainis_Laboras2
{

    class Saldainis
    {
        string pavadinimas;
        string tipas;
        double kgKaina;
        double kiekKg;


        public Saldainis(string pavadinimas, string tipas, double kgKaina, double kiekKg)
        {
            this.pavadinimas = pavadinimas;
            this.tipas = tipas;
            this.kgKaina = kgKaina;
            this.kiekKg = kiekKg;


        }
        public string imtiPavadinima() { return pavadinimas; }
        public string imtiTipa() { return tipas; }
        public double imtikgKaina() { return kgKaina; }
        public double ImtiKg() { return kiekKg; }


    }
    internal class Program
    {


        static void Main(string[] args)
        {

            const int Cn = 100;
            const int Cf = 100;
            string fv1 = "TextFile1.txt";
            string fv2 = "TextFile2.txt";
            string fv3 = "Rezultatai.txt";



            int n1, n2;
            double kiekis1, kiekis2;
            string pav1, vard1;
            string pav2, vard2;
            int vard11, brangindeks;
            int a1, a2;

     
            Saldainis[] S1 = new Saldainis[Cn];
            Saldainis[] S2 = new Saldainis[Cf];
            skaityti(fv1, S1, out n1, out kiekis1, out vard1);
            skaityti(fv2, S2, out n2, out kiekis2, out vard2);

            a1 = Indeksas(S1, kiekis1);
            a2 = Indeksas(S2, kiekis2);


            
            string vardas = kurisbrangiau(S1, S2, kiekis1, kiekis2, vard1, vard2, n1,n2);

            spausdinti(fv3, S1, S2, kiekis1, vard1, n1, a2, vardas);
            spausdinti(fv3 , S1, S2, kiekis2, vard2, n2, a2, vardas);

           

        }

        static void skaityti(string FD, Saldainis[] S1, out int n, out double kiekis, out string vardas)

        {
            string pavadinimas;
            string tipas;
            double kgKaina;
            double kiekKg;
            int k;



            using (StreamReader reader = new StreamReader(FD))
            {
                string[] parts;
                string line;
                line = reader.ReadLine();
                vardas = line;
                line = reader.ReadLine();
                kiekis = double.Parse(line);

                for (int i = 0; i < kiekis; i++)
                {
                    line = reader.ReadLine();
                    parts = line.Split(';');
                    pavadinimas = parts[0];
                    tipas = parts[1];
                    kgKaina = double.Parse(parts[2]);
                    kiekKg = double.Parse(parts[3]);



                    S1[i] = new Saldainis(pavadinimas, tipas, kgKaina, kiekKg);

                }
                line = reader.ReadLine();
                n = int.Parse(line);



            }

        }
        static int Indeksas(Saldainis[] S1, double kiekis)
        {

            int brangindeks = 0;
            double brangiausias = S1[0].imtikgKaina() * S1[0].ImtiKg();

            for (int i = 0; i < kiekis; i++)
            {
                if (S1[i].imtikgKaina() * S1[i].ImtiKg() > brangiausias)
                {
                    brangiausias = S1[i].imtikgKaina() * S1[i].ImtiKg();
                    brangindeks = i;

                }
            }
            return brangindeks;
        }

        static void spausdinti(string fV, Saldainis[] S1, Saldainis [] S2, double kiekis,  string vard,  int n, int a, string vardas)
        {
 using (var fr = File.AppendText(fV))
            {
            const string virsus =
              "|-----------------|------------|---------------|---------|\r\n"
              + "| Pavadinimas | Tipas | Kaina kilogramui | esamas kiekis (kg) | \r\n"
              + "|-----------------|------------|---------------|---------|";
            fr.WriteLine("PRADINIAI DUOMENYS :");
            fr.WriteLine(vard);
            fr.WriteLine(virsus);

           

                for (int i = 0; i < kiekis; i++)
                {
                    fr.WriteLine("| {0,-12} | {1,8} | {2,11} | {3,10} |",
                      S1[i].imtiPavadinima(), S1[i].imtiTipa(), S1[i].imtikgKaina(), S1[i].ImtiKg());
                    fr.WriteLine("----------------------------------------------------------");

                }
                int brangindeks = 0;
                for (int i = 0; i < kiekis; i++)
                {
                    fr.WriteLine(S1[i].imtiPavadinima() + "  Saldainiai kainuoja  " + S1[i].imtikgKaina() * S1[i].ImtiKg());
                    fr.WriteLine("---------------------------------------------------------|");

                }
                fr.WriteLine("Studentas kiekvieno saldainio pavadinimo turi po " + n + "Kg");
                fr.WriteLine("-----------------------------------------------------------");

                for (int i = 0; i < kiekis; i++)
                {

                    if (S1[i].imtikgKaina() * S1[i].ImtiKg() == S1[a].imtikgKaina() * S1[a].ImtiKg())
                    {
                        fr.WriteLine("Brangiausias tipas " + S1[i].imtiTipa());

                    }


                }
                fr.WriteLine("Studentas, kurio rinkinys kainuoja brangiau : " + vardas);

            }
            atranka(S1, S2, kiekis, kiekis);
            
        }
  

            static void atranka(Saldainis[] S1, Saldainis[] S2, double kiekis1, double kiekis2)
        {
            int n = 0;
            double k = 200;
            int kiekis = 0;
            double[] C = new double[100];
            for (int i = 0; i < kiekis1; i++)
            {
                C[n] = S1[i].imtikgKaina() * S1[i].ImtiKg();
                n++;
            }
            for (int i = 0; i < kiekis2; i++)
            {
                C[n] = S2[i].imtikgKaina() * S2[i].ImtiKg();
                n++;
            }


            for (int i = 0; i < kiekis1+kiekis2; i++)
            {
                if (C[i] >k) {

                    Console.Write(C[i] + " ");
                    kiekis++;
                }
}
            Console.WriteLine(" : Atrinkti skaiciai didesni uz " + k + " "  );
            const string virsus =
              "|-----------------|------------|---------------|---------|\r\n"
              + "| Pavadinimas | Tipas | Kaina kilogramui | esamas kiekis (kg) | \r\n"
              + "|-----------------|------------|---------------|---------|";
            Console.WriteLine(virsus);
            for (int i =0; i<kiekis; i++)
                {

                    if (C[i]== S1[i].imtikgKaina() * S1[i].ImtiKg()) {
                    Console.WriteLine("| {0,-12} | {1,8} | {2,11} | {3,10} |",
                   S1[i].imtiPavadinima(), S1[i].imtiTipa(), S1[i].imtikgKaina(), S1[i].ImtiKg());
                    Console.WriteLine("----------------------------------------------------------");

                }
                }

            for(int i = 0; i < kiekis; i++)
                {

                if (C[i] == S2[i].imtikgKaina() * S2[i].ImtiKg())
                {
                    Console.WriteLine("| {0,-12} | {1,8} | {2,11} | {3,10} |",
                   S1[i].imtiPavadinima(), S2[i].imtiTipa(), S2[i].imtikgKaina(), S2[i].ImtiKg());
                   Console.WriteLine("------------------------------------------------------");

                }
            }
        }

        static string kurisbrangiau(Saldainis[] S1, Saldainis[] S2, double kiekis1, double kiekis2, string vard1, string vard2, int n1, int n2)
        {

            double sum1 = 0;
            double sum2 = 0;

            for (int i = 0; i < kiekis1; i++)
            {
                sum1 += S1[i].imtikgKaina() * n1;

            }

            for (int i = 0; i < kiekis2; i++)
            {
                sum2 += S2[i].imtikgKaina() * n2;

            }

            if (sum1 > sum2)
            {
                return vard1;
            }

            else return vard2;
        }
    }   

}

    







