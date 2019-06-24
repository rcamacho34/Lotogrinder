using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotogrinder
{
    public class Combinatoria
    {
        public static long TotalCombinacoes(int n, int r)
        {
            return (DivisaoFatorial(n, r) / Fatorial(n - r));
        }

        public static long DivisaoFatorial(int dividendo, int divisor)
        {
            long retorno = 1;

            for (int i = dividendo; i > divisor; i--)
            {
                retorno *= i;
            }

            return retorno;
        }

        public static long Fatorial(int i)
        {
            if (i <= 1)
                return 1;

            return (i * Fatorial(i - 1));
        }

        public static List<int[]> GerarCombinacoes(int n, int k)
        {
            List<int[]> listaCombinacoes = new List<int[]>();
            int[] arr = new int[n];

            // Monta o array de números para combinar
            for (int i = 0; i < n; i++)
            {
                arr[i] = i + 1;
            }

            listaCombinacoes = GerarCombinacoes(n, k, arr);

            return listaCombinacoes;
        }

        public static List<int[]> GerarCombinacoes(int n, int k, int[] arr)
        {
            List<int[]> listaCombinacoes = new List<int[]>();

            listaCombinacoes = combinations2(arr, k, 0, new int[k], listaCombinacoes);

            return listaCombinacoes;
        }

        // Método recursivo que gera as combinações
        public static List<int[]> combinations2(int[] arr, int len, int startPosition, int[] result, List<int[]> lista)
        {
            if (len == 0)
            {
                //foreach (int item in result)
                //    Console.Write(item.ToString().PadLeft(2, '0') + " ");

                //Console.WriteLine();

                int[] linha = new int[result.Length];

                for (int i = 0; i < result.Length; i++)
                {
                    linha[i] = result[i];
                }
                lista.Add(linha);

                return lista;
            }

            for (int i = startPosition; i <= arr.Length - len; i++)
            {
                result[result.Length - len] = arr[i];
                combinations2(arr, len - 1, i + 1, result, lista);
            }

            return lista;

        }

        public static void GravarCombinacoesTXT(List<int[]> listaCombinacoes)
        {
            string caminho = @"D:\LF\";
            string arquivo = "LFcombinacoes.txt";
            string linha = "";

            if (!Directory.Exists(caminho))
                Directory.CreateDirectory(caminho);

            using (StreamWriter w = File.CreateText(caminho + arquivo))
            {
                int l = 0;

                foreach (int[] item in listaCombinacoes)
                {
                    linha = "";

                    for (int i = 0; i < item.Length; i++)
                    {
                        linha += item[i].ToString().PadLeft(2, '0') + " ";
                    }

                    linha = linha.Substring(0, linha.Length - 1);

                    w.WriteLine(linha);
                    l++;

                    Console.Write("\rGravando linha {0}...", l);
                }                
            }

            Console.WriteLine();
            Console.WriteLine("Gravação concluída com sucesso!");
            Console.WriteLine();
        }






        public static void GerarCombinacoes2(int n, int k)
        {
            long totalCombinacoes = TotalCombinacoes(n, k);
            int[,] retorno = new int[totalCombinacoes, k];
            int combinacaoAtual = 0;
            int numerosEscolhidos = 0;
            int numeroAtual = 0;

            // posição do último número que irá iniciar uma combinação
            int ultimoInicial = n - k;

            // loop dos números que iniciam combinação
            for (int i = 0; i < ultimoInicial; i++)
            {
                numerosEscolhidos = 0;

                for (int j = i; j < n; j++)
                {
                    retorno[combinacaoAtual, numerosEscolhidos] = j + 1;

                    numerosEscolhidos++;

                    if (numerosEscolhidos == k)
                    {
                        numerosEscolhidos = 0;
                        combinacaoAtual++;
                    }
                }
            }

        }
    }



}
