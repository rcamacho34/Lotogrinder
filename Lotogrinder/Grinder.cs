using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace Lotogrinder
{
    public class Grinder
    {
        public static List<string[]> LerConcursos()
        {
            List<string[]> listaConcursos = new List<string[]>();
            string[] concurso = new string[17];

            HtmlDocument htmlDoc = new HtmlDocument();

            htmlDoc.Load(@"C:\Loterias\d_lotfac.htm");

            if (htmlDoc.ParseErrors != null && htmlDoc.ParseErrors.Count() > 0)
            {
                // Handle any parse errors as required

            }
            else
            {

                if (htmlDoc.DocumentNode != null)
                {
                    HtmlNode tableNode = htmlDoc.DocumentNode.SelectSingleNode("//table");

                    HtmlNodeCollection trNodes = tableNode.SelectNodes("tr[position() > 1]");

                    foreach (HtmlNode item in trNodes)
                    {
                        concurso = new string[17];

                        if (item.ChildNodes.Count < 15)
                            continue;

                        HtmlNodeCollection tdNodes = item.SelectNodes("td");

                        concurso[0] = tdNodes[0].InnerText;
                        concurso[1] = tdNodes[1].InnerText;
                        concurso[2] = tdNodes[2].InnerText;
                        concurso[3] = tdNodes[3].InnerText;
                        concurso[4] = tdNodes[4].InnerText;
                        concurso[5] = tdNodes[5].InnerText;
                        concurso[6] = tdNodes[6].InnerText;
                        concurso[7] = tdNodes[7].InnerText;
                        concurso[8] = tdNodes[8].InnerText;
                        concurso[9] = tdNodes[9].InnerText;
                        concurso[10] = tdNodes[10].InnerText;
                        concurso[11] = tdNodes[11].InnerText;
                        concurso[12] = tdNodes[12].InnerText;
                        concurso[13] = tdNodes[13].InnerText;
                        concurso[14] = tdNodes[14].InnerText;
                        concurso[15] = tdNodes[15].InnerText;
                        concurso[16] = tdNodes[16].InnerText;

                        listaConcursos.Add(concurso);
                    }
                }
            }

            return listaConcursos;

        }

        public static void LerConcursoAtual()
        {
            HtmlDocument htmlDoc = new HtmlDocument();

            HtmlWeb web = new HtmlWeb();

            htmlDoc = web.Load("http://loterias.caixa.gov.br/wps/portal/loterias/landing/lotofacil/");

            if (htmlDoc.ParseErrors != null && htmlDoc.ParseErrors.Count() > 0)
            {
                // Handle any parse errors as required

            }
            else
            {
                if (htmlDoc.DocumentNode != null)
                {
                    HtmlNode bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//body");

                    HtmlNode divNode = htmlDoc.DocumentNode.SelectSingleNode("//div//div//div[@id='resultados']");

                }
            }
        }

        public static void ProcessarLotesAtrasoConcursoAtual()
        {
            ProcessarAtraso(1, 3268760, true);
        }

        public static void ProcessarAtraso(int inicio, int fim, bool atual)
        {
            List<int[]> listaCombinacaoConcurso = new List<int[]>();

            int[] combinacao = new int[16];

            int p11 = 0;
            int p12 = 0;
            int p13 = 0;
            int p14 = 0;
            int p15 = 0;

            List<int[]> listaConcursos = new List<int[]>();

            listaConcursos = new DB().SelectConcursos(atual);

            if (atual)
            {
                Console.WriteLine();
                Console.WriteLine("Processando o concurso atual: {0}", listaConcursos[0][0].ToString());
                Console.WriteLine();
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("SELECT * FROM tbCombinacao WHERE Id BETWEEN {0} AND {1}", inicio, fim);

            using (SqlConnection con = new DB().Conn())
            {
                con.Open();

                DateTime inicioLote = DateTime.Now;

                using (SqlCommand cmd = new SqlCommand(sb.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("Processando combinações: {0} a {1}...", inicio, fim);
                        Console.WriteLine();

                        // Loop Combinações
                        while (reader.Read())
                        {
                            combinacao = new int[16];

                            combinacao[0] = reader.GetInt32(0);
                            combinacao[1] = reader.GetByte(1);
                            combinacao[2] = reader.GetByte(2);
                            combinacao[3] = reader.GetByte(3);
                            combinacao[4] = reader.GetByte(4);
                            combinacao[5] = reader.GetByte(5);
                            combinacao[6] = reader.GetByte(6);
                            combinacao[7] = reader.GetByte(7);
                            combinacao[8] = reader.GetByte(8);
                            combinacao[9] = reader.GetByte(9);
                            combinacao[10] = reader.GetByte(10);
                            combinacao[11] = reader.GetByte(11);
                            combinacao[12] = reader.GetByte(12);
                            combinacao[13] = reader.GetByte(13);
                            combinacao[14] = reader.GetByte(14);
                            combinacao[15] = reader.GetByte(15);

                            Console.Write("\rProcessando atrasos para a combinação {0}...", combinacao[0]);

                            int contadorDezena = 0;

                            p11 = 0;
                            p12 = 0;
                            p13 = 0;
                            p14 = 0;
                            p15 = 0;

                            // Loop Concursos
                            foreach (int[] concurso in listaConcursos)
                            {
                                contadorDezena = 0;

                                //Loop dezenas da combinação
                                for (int i = 1; i <= 15; i++)
                                {
                                    // Loop dezenas do concurso
                                    for (int j = 1; j <= 15; j++)
                                    {
                                        // match
                                        if (combinacao[i] == concurso[j])
                                        {
                                            contadorDezena++;
                                        }
                                    }
                                }

                                if (contadorDezena > 10)
                                {
                                    p11 = contadorDezena == 11 ? concurso[0] : p11;
                                    p12 = contadorDezena == 12 ? concurso[0] : p12;
                                    p13 = contadorDezena == 13 ? concurso[0] : p13;
                                    p14 = contadorDezena == 14 ? concurso[0] : p14;
                                    p15 = contadorDezena == 15 ? concurso[0] : p15;

//                                    new DB().UpdateCombinacaoAtraso(combinacao[0], p11, p12, p13, p14, p15);
                                }

                            } // loop Concursos

                            listaCombinacaoConcurso.Add(
                                new int[] { combinacao[0], p11, p12, p13, p14, p15 }
                            );

                        } // loop Combinações

                    }

                    Console.WriteLine();

                    sb.Clear();

                    int m = 0;
                    int n = 0;

                    foreach (int[] item in listaCombinacaoConcurso)
                    {
                        m++;

                        if (item[1] != 0 || item[2] != 0 || item[3] != 0 || item[4] != 0 || item[5] != 0)
                        {
                            n++;
                            Console.Write("\rProcessando combinação {0} - Gravando combinação {1}", m, n);

                            sb.AppendLine(@"UPDATE tbCombinacao SET ");

                            if (item[1] != 0)
                                sb.AppendFormat("IdUltimoConcurso11 = {0}, ", item[1]);
                            if (item[2] != 0)
                                sb.AppendFormat("IdUltimoConcurso12 = {0}, ", item[2]);
                            if (item[3] != 0)
                                sb.AppendFormat("IdUltimoConcurso13 = {0}, ", item[3]);
                            if (item[4] != 0)
                                sb.AppendFormat("IdUltimoConcurso14 = {0}, ", item[4]);
                            if (item[5] != 0)
                                sb.AppendFormat("IdUltimoConcurso15 = {0}, ", item[5]);

                            sb.AppendFormat("WHERE Id = {0} \n", item[0]);

                            sb.Replace(", WHERE", " WHERE");

                            if (n % 5000 == 0 || m == 3268760)
                            {
                                new DB().Exec(sb);
                                sb.Clear();
                                Console.Write("\rGravando {0}...", n);
                            }
                        }
                        //new DB().UpdateCombinacaoAtraso(item[0], item[1], item[2], item[3], item[4], item[5]);
                    }
                    Console.WriteLine("\rGravação realizada com sucesso!");

                    //Console.WriteLine("Gravando {0} linhas...", listaCombinacaoConcurso.Count);
                    //new DB().BulkCombinacaoConcurso(listaCombinacaoConcurso, tabela);

                    DateTime fimLote = DateTime.Now;

                    TimeSpan duracaoLote = fimLote - inicioLote;

                    Console.WriteLine("Gravação realizada com sucesso! Duração: {0}", duracaoLote.ToString());
                    Console.WriteLine();
                }
            }
        }

        public static void GerarScriptCombinacaoConcurso(int qtdTabelas)
        {
            string linha = File.ReadAllText(@"D:\Loteria\DB_LF\combinacaoconcurso.sql");

            string tabela = "";
            string retorno = "";

            using (StreamWriter writetext = new StreamWriter(@"D:\Loteria\DB_LF\combinacaoconcurso2.sql"))
            {
                for (int i = 2; i <= qtdTabelas; i++)
                {
                    tabela = "_" + i.ToString().PadLeft(3, '0');

                    retorno += linha.Replace("_001", tabela);
                    retorno += "\n\n";
                }

                writetext.WriteLine(retorno);
            }
        }
    }
}
