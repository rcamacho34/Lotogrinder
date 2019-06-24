using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotogrinder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*******       GERADOR DE COMBINAÇÒES       *******");
            Console.WriteLine();

            Console.Write("Quantidade de números a combinar: ");
            string sN = Console.ReadLine();

            Console.Write("Tamanho de cada combinação: ");
            string sK = Console.ReadLine();

            DateTime inicio = DateTime.Now;

            Console.WriteLine();
            Console.WriteLine("Início: {0}", inicio.ToString("dd/MM/yyyy hh:mm:ss"));
            Console.WriteLine();

            Combinacoes(int.Parse(sN), int.Parse(sK));

            DateTime termino = DateTime.Now;

            Console.WriteLine();
            Console.WriteLine("Término: {0}", termino.ToString("dd/MM/yyyy hh:mm:ss"));
            Console.WriteLine();

            TimeSpan duracao = termino - inicio;

            Console.WriteLine();
            Console.WriteLine("Duração: {0}", duracao.ToString());
            Console.WriteLine();

            Console.WriteLine();
            Console.Write("Tecle <ENTER> para encerrar...");
            Console.Read();
        }

        private static void Combinacoes(int n, int k)
        {
            int[] arr = new int[n];

            // Monta o array de números para combinar
            for (int i = 0; i < n; i++)
            {
                arr[i] = i + 1;
            }

            Console.WriteLine("Combinação ({0}, {1})", n, k);
            Console.WriteLine();
            Console.WriteLine("Matriz inicial:");
            Console.WriteLine("[" + string.Join(" ", arr) + "]");

            Console.WriteLine();
            Console.WriteLine("Total de {0} combinações. Tecle <ENTER> para gerar...", Combinatoria.TotalCombinacoes(n, k));
            Console.ReadLine();

            List<int[]> listaCombinacoes = Combinatoria.GerarCombinacoes(n, k);

            int totalLinhas = 0;

            foreach (int[] item in listaCombinacoes)
            {
                totalLinhas++;
                Console.Write(totalLinhas.ToString().PadLeft(listaCombinacoes.Count.ToString().Length, '0') + " - ");

                for (int i = 0; i < item.Length; i++)
                {
                    Console.Write(item[i].ToString().PadLeft(2, '0') + " ");
                }

                Console.WriteLine();
            }

            //Console.WriteLine();
            //Console.Write("Gravar em arquivo TXT? [S/N]:");
            //string s = Console.ReadLine();

            //if (s.ToUpper() == "S")
            //{
            //    Combinatoria.GravarCombinacoesTXT(listaCombinacoes);
            //}

            //Console.WriteLine();
            //Console.Write("Gravar em tabela? [S/N]:");
            //string s2 = Console.ReadLine();

            //if (s2.ToUpper() == "S")
            //{
            Console.WriteLine("\nGravando...");
            new DB().BulkCombinacao(listaCombinacoes);
            Console.WriteLine("Gravado com sucesso!");
            //}

        }
    }
}
