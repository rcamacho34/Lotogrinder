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
            ConsoleKeyInfo opcao;

            Console.WriteLine("*******       GERADOR DE COMBINAÇÒES       *******");
            Console.WriteLine();

            do
            {
                Console.WriteLine("[1] - Gerar combinação (n, r)");
                Console.WriteLine("[2] - Carregar todos os concursos");
                Console.WriteLine("[3] - Baixar resultado do concurso atual");
                Console.WriteLine("[4] - Processar atraso das combinações ");
                Console.WriteLine("[5] - Processar atraso concurso atual ");
                Console.WriteLine("[0] - Sair ");
                Console.WriteLine();
                Console.WriteLine("Escolha a opção desejada:");
                opcao = Console.ReadKey();
            }
            while (opcao.Key != ConsoleKey.D1 && opcao.Key != ConsoleKey.D2 && opcao.Key != ConsoleKey.D3 && opcao.Key != ConsoleKey.D4 && opcao.Key != ConsoleKey.D5 && opcao.Key != ConsoleKey.D0);

            Console.WriteLine();

            if (opcao.Key == ConsoleKey.D0)
                return;
            else
            {
                if (opcao.Key == ConsoleKey.D1)
                {
                    Console.WriteLine();
                    Console.WriteLine("***   Gerar combinação (n, r)   ***");
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
                else if (opcao.Key == ConsoleKey.D2)
                {
                    Console.WriteLine("\nGravando todos os concursos...");
                    List<string[]> listaConcursos = Grinder.LerConcursos();

                    new DB().BulkConcurso(listaConcursos);
                    Console.WriteLine("Gravado com sucesso!");
                }
                else if (opcao.Key == ConsoleKey.D4)
                {
                    do
                    {
                        DateTime inicio = DateTime.Now;

                        Console.WriteLine();
                        Console.WriteLine("Início: {0}", inicio.ToString("dd/MM/yyyy hh:mm:ss"));
                        Console.WriteLine();

                        Grinder.ProcessarAtraso(300001, 500000, false);

                        DateTime termino = DateTime.Now;

                        Console.WriteLine();
                        Console.WriteLine("Término: {0}", termino.ToString("dd/MM/yyyy hh:mm:ss"));
                        Console.WriteLine();

                        TimeSpan duracao = termino - inicio;

                        Console.WriteLine();
                        Console.WriteLine("Duração: {0}", duracao.ToString());
                        Console.WriteLine();

                        Console.WriteLine();
                        Console.Write("Processar novos lotes? [S/N]: ");
                        opcao = Console.ReadKey();
                    }
                    while (opcao.Key != ConsoleKey.N);
                }
                else if (opcao.Key == ConsoleKey.D5)
                {
                    DateTime inicio = DateTime.Now;

                    Console.WriteLine();
                    Console.WriteLine("Início: {0}", inicio.ToString("dd/MM/yyyy hh:mm:ss"));
                    Console.WriteLine();

                    Grinder.ProcessarLotesAtrasoConcursoAtual();

                    DateTime termino = DateTime.Now;

                    Console.WriteLine();
                    Console.WriteLine("Término: {0}", termino.ToString("dd/MM/yyyy hh:mm:ss"));
                    Console.WriteLine();

                    TimeSpan duracao = termino - inicio;

                    Console.WriteLine();
                    Console.WriteLine("Duração: {0}", duracao.ToString());
                    Console.WriteLine();
                    Console.Write("Tecle <ENTER> para encerrar...");
                    Console.Read();
                }


            }
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
