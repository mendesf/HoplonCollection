using Hoplon.Collections;
using System;

namespace HoplonPassport
{
    class Program
    {
        static void Main(string[] args)
        {
            IHoplonCollection collection = new HoplonCollection();

            collection.Add("ano.nascimento", 1980, "pedro");
            collection.Add("ano.nascimento", 1980, "maria");
            collection.Add("ano.nascimento", 1980, "joao");

            collection.Add("ano.nascimento", 1975, "rodrigo");

            collection.Add("ano.nascimento", 1990, "felipe");
            collection.Add("ano.nascimento", 1990, "gustavo");

            collection.Add("ano.nascimento", 1970, "marcia");

            //var nascimentos = collection.Get("ano.nascimento", 0, -1);
            var nascimentos = collection.Get("ano.nascimento", 2, 10);
            /*Console.WriteLine("Deveria ter 7 elementos: " + nascimentos.Count);
            Console.WriteLine("Deveria ser o elemento 'marcia': " + nascimentos[0]);
            Console.WriteLine("Deveria ser o elemento 'rodrigo': " + nascimentos[1]);
            Console.WriteLine("Deveria ser o elemento 'joao': " + nascimentos[2]);
            Console.WriteLine("Deveria ser o elemento 'maria': " + nascimentos[3]);
            Console.WriteLine("Deveria ser o elemento 'pedro': " + nascimentos[4]);
            Console.WriteLine("Deveria ser o elemento 'felipe': " + nascimentos[5]);
            Console.WriteLine("Deveria ser o elemento 'gustavo': " + nascimentos[6]);*/

            Console.WriteLine("Deveria ser o index 5: " + collection.IndexOf("ano.nascimento", "felipe"));
            Console.WriteLine("Deveria ser o index 0: " + collection.IndexOf("ano.nascimento", "marcia"));
            Console.WriteLine("Deveria ser o index 6: " + collection.IndexOf("ano.nascimento", "gustavo"));
            Console.WriteLine("Deveria ser o index -1: " + collection.IndexOf("ano.nascimento", "ana"));

            Console.ReadKey();
        }
    }
}
