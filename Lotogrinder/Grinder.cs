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
using System.Collections;

namespace Lotogrinder
{
    public class Grinder
    {
        public static List<string[]> LerConcursos()
        {
            List<string[]> listaConcursos = new List<string[]>();
            string[] concurso = new string[15];
            string[] concursoSorted = new string[17];

            HtmlDocument htmlDoc = new HtmlDocument();
            string ArquivoResultados = ConfigurationManager.AppSettings["CAMINHO_RESULTADOS"].ToString();

            htmlDoc.Load(ArquivoResultados);

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
                        concurso = new string[15];
                        concursoSorted = new string[17];

                        if (item.ChildNodes.Count < 15)
                            continue;

                        HtmlNodeCollection tdNodes = item.SelectNodes("td");

                        concursoSorted[0] = tdNodes[0].InnerText;
                        concursoSorted[1] = tdNodes[1].InnerText;

                        concurso[0] = tdNodes[2].InnerText;
                        concurso[1] = tdNodes[3].InnerText;
                        concurso[2] = tdNodes[4].InnerText;
                        concurso[3] = tdNodes[5].InnerText;
                        concurso[4] = tdNodes[6].InnerText;
                        concurso[5] = tdNodes[7].InnerText;
                        concurso[6] = tdNodes[8].InnerText;
                        concurso[7] = tdNodes[9].InnerText;
                        concurso[8] = tdNodes[10].InnerText;
                        concurso[9] = tdNodes[11].InnerText;
                        concurso[10] = tdNodes[12].InnerText;
                        concurso[11] = tdNodes[13].InnerText;
                        concurso[12] = tdNodes[14].InnerText;
                        concurso[13] = tdNodes[15].InnerText;
                        concurso[14] = tdNodes[16].InnerText;

                        Array.Sort(concurso, StringComparer.InvariantCulture);

                        concursoSorted[2] = concurso[0];
                        concursoSorted[3] = concurso[1];
                        concursoSorted[4] = concurso[2];
                        concursoSorted[5] = concurso[3];
                        concursoSorted[6] = concurso[4];
                        concursoSorted[7] = concurso[5];
                        concursoSorted[8] = concurso[6];
                        concursoSorted[9] = concurso[7];
                        concursoSorted[10] = concurso[8];
                        concursoSorted[11] = concurso[9];
                        concursoSorted[12] = concurso[10];
                        concursoSorted[13] = concurso[11];
                        concursoSorted[14] = concurso[12];
                        concursoSorted[15] = concurso[13];
                        concursoSorted[16] = concurso[14];

                        listaConcursos.Add(concursoSorted);
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
                                sb.AppendFormat("IdUltimo11 = {0}, ", item[1]);
                            if (item[2] != 0)
                                sb.AppendFormat("IdUltimo12 = {0}, ", item[2]);
                            if (item[3] != 0)
                                sb.AppendFormat("IdUltimo13 = {0}, ", item[3]);
                            if (item[4] != 0)
                                sb.AppendFormat("IdUltimo14 = {0}, ", item[4]);
                            if (item[5] != 0)
                                sb.AppendFormat("IdUltimo15 = {0}, ", item[5]);

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

        public static void ProcessarUltimosAtrasos(int inicio, int fim)
        {
            List<int[]> listaCombinacaoAtrasos = new List<int[]>();
            List<int> cincoAtrasos = new List<int>();

            int[] combinacao = new int[16];

            int atraso = 0;

            List<int> atrasos = new List<int>();

            List<int[]> listaConcursos = new List<int[]>();

            listaConcursos = new DB().SelectConcursos(false);

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("SELECT * FROM tbCombinacao WHERE Id BETWEEN {0} AND {1}", inicio, fim);
            //sb.AppendLine("SELECT * FROM tbCombinacao WHERE Id IN (260055)");

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

                            atrasos.Clear();
                            cincoAtrasos.Clear();

                            atraso = 0;

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

                                // Se não premiou, contabiliza mais um atraso
                                if (contadorDezena <= 10)
                                {
                                    atraso++;
                                }
                                // Se premiou, zera os atrasos e inclui o valor na lista de atrasos
                                else
                                {
                                    atrasos.Add(atraso);
                                    atraso = 0;
                                }

                            } // loop Concursos                            

                            // Adiciona o último atraso
                            if (atraso > 0)
                            {
                                atrasos.Add(atraso);
                            }

                            cincoAtrasos = atrasos.OrderByDescending(i => i).ToList<int>();

                            listaCombinacaoAtrasos.Add(
                                new int[] { combinacao[0], cincoAtrasos[0], cincoAtrasos[1], cincoAtrasos[2],
                                            cincoAtrasos[3], cincoAtrasos[4] }
                            );

                        } // loop Combinações

                    }

                    Console.WriteLine();

                    sb.Clear();

                    int m = 0;
                    int n = 0;

                    foreach (int[] item in listaCombinacaoAtrasos)
                    {
                        m++;

                        if (item[1] != 0 || item[2] != 0 || item[3] != 0 || item[4] != 0 || item[5] != 0)
                        {
                            n++;
                            Console.Write("\rProcessando combinação {0} - Gravando combinação {1}", m, n);

                            sb.AppendLine(@"UPDATE tbCombinacao SET ");

                            if (item[1] != 0)
                                sb.AppendFormat("Atraso01 = {0}, ", item[1]);
                            if (item[2] != 0)
                                sb.AppendFormat("Atraso02 = {0}, ", item[2]);
                            if (item[3] != 0)
                                sb.AppendFormat("Atraso03 = {0}, ", item[3]);
                            if (item[4] != 0)
                                sb.AppendFormat("Atraso04 = {0}, ", item[4]);
                            if (item[5] != 0)
                                sb.AppendFormat("Atraso05 = {0}, ", item[5]);

                            sb.AppendFormat("WHERE Id = {0} \n", item[0]);

                            sb.Replace(", WHERE", " WHERE");

                            if (n % 5000 == 0 || m == 3268760 || listaCombinacaoAtrasos.Count == 1)
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

        public static void ProcessarPremiacoes(int inicio, int fim)
        {
            List<int[]> listaCombinacaoPremiacao = new List<int[]>();

            int[] combinacao = new int[16];

            int p11 = 0;
            int p12 = 0;
            int p13 = 0;
            int p14 = 0;
            int p15 = 0;

            List<int[]> listaConcursos = new List<int[]>();

            listaConcursos = new DB().SelectConcursos(false);

            int UltimoConcurso = listaConcursos[listaConcursos.Count - 1][0];

            int TotalPremiacao = 0;

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

                            Console.Write("\rProcessando premiações para a combinação {0}...", combinacao[0]);

                            int contadorDezena = 0;

                            p11 = 0;
                            p12 = 0;
                            p13 = 0;
                            p14 = 0;
                            p15 = 0;

                            TotalPremiacao = 0;

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
                                    p11 = contadorDezena == 11 ? ++p11 : p11;
                                    p12 = contadorDezena == 12 ? ++p12 : p12;
                                    p13 = contadorDezena == 13 ? ++p13 : p13;
                                    p14 = contadorDezena == 14 ? ++p14 : p14;
                                    p15 = contadorDezena == 15 ? ++p15 : p15;

                                }

                            } // loop Concursos

                            TotalPremiacao = p11 + p12 + p13 + p14 + p15;

                            listaCombinacaoPremiacao.Add(
                                new int[] { combinacao[0], p11, p12, p13, p14, p15, TotalPremiacao }
                            );

                        } // loop Combinações

                    }

                    Console.WriteLine();

                    sb.Clear();

                    int m = 0;
                    int n = 0;

                    foreach (int[] item in listaCombinacaoPremiacao)
                    {
                        m++;

                        if (item[1] != 0 || item[2] != 0 || item[3] != 0 || item[4] != 0 || item[5] != 0)
                        {
                            n++;
                            Console.Write("\rProcessando combinação {0} - Gravando combinação {1}", m, n);

                            sb.AppendLine(@"UPDATE tbCombinacao SET ");

                            if (item[1] != 0)
                                sb.AppendFormat("Total11 = {0}, ", item[1]);
                            if (item[2] != 0)
                                sb.AppendFormat("Total12 = {0}, ", item[2]);
                            if (item[3] != 0)
                                sb.AppendFormat("Total13 = {0}, ", item[3]);
                            if (item[4] != 0)
                                sb.AppendFormat("Total14 = {0}, ", item[4]);
                            if (item[5] != 0)
                                sb.AppendFormat("Total15 = {0}, ", item[5]);
                            if (item[6] != 0)
                                sb.AppendFormat("TotalPremiacao = {0}, ", item[6]);

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

        // Processa o concurso atual, atualizando:
        // 1 - Últimas premiações
        // 2 - Quantidade de premiações acumuladas
        // 3 - Cinco maiores atrasos
        public static void ProcessarConcursoAtual(int inicio, int fim)
        {
            List<int[]> listaCombinacao = new List<int[]>();

            int[] combinacao = new int[16];

            int u11 = 0;
            int u12 = 0;
            int u13 = 0;
            int u14 = 0;
            int u15 = 0;

            int p11 = 0;
            int p12 = 0;
            int p13 = 0;
            int p14 = 0;
            int p15 = 0;

            List<int[]> listaConcursos = new List<int[]>();
            List<int> atrasos = new List<int>();
            List<int> cincoAtrasos = new List<int>();

            listaConcursos = new DB().SelectConcursos(true);

            Console.WriteLine();
            Console.WriteLine("Processando o concurso atual: {0}", listaConcursos[0][0].ToString());
            Console.WriteLine();

            int TotalPremiacao = 0;

            int AtrasoAtual = 0;

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
                            combinacao = new int[33];

                            // Id Combinação
                            combinacao[0] = reader.GetInt32(0);

                            // Números Combinação
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

                            // Últimas Premiações
                            combinacao[16] = reader.IsDBNull(16) ? 0 : reader.GetInt16(16);
                            combinacao[17] = reader.IsDBNull(17) ? 0 : reader.GetInt16(17);
                            combinacao[18] = reader.IsDBNull(18) ? 0 : reader.GetInt16(18);
                            combinacao[19] = reader.IsDBNull(19) ? 0 : reader.GetInt16(19);
                            combinacao[20] = reader.IsDBNull(20) ? 0 : reader.GetInt16(20);

                            // Total de Premiações
                            combinacao[21] = reader.IsDBNull(21) ? 0 : reader.GetInt16(21);
                            combinacao[22] = reader.IsDBNull(22) ? 0 : reader.GetInt16(22);
                            combinacao[23] = reader.IsDBNull(23) ? 0 : reader.GetInt16(23);
                            combinacao[24] = reader.IsDBNull(24) ? 0 : reader.GetInt16(24);
                            combinacao[25] = reader.IsDBNull(25) ? 0 : reader.GetInt16(25);
                            combinacao[26] = reader.IsDBNull(26) ? 0 : reader.GetInt16(26);

                            // Cinco maiores atrasos e Atraso Atual
                            combinacao[27] = reader.IsDBNull(27) ? 0 : reader.GetInt16(27);
                            combinacao[28] = reader.IsDBNull(28) ? 0 : reader.GetInt16(28);
                            combinacao[29] = reader.IsDBNull(29) ? 0 : reader.GetInt16(29);
                            combinacao[30] = reader.IsDBNull(30) ? 0 : reader.GetInt16(30);
                            combinacao[31] = reader.IsDBNull(31) ? 0 : reader.GetInt16(31);
                            combinacao[32] = reader.IsDBNull(32) ? 0 : reader.GetInt16(32);

                            Console.Write("\rProcessando a combinação {0}...", combinacao[0]);

                            int contadorDezena = 0;

                            u11 = 0;
                            u12 = 0;
                            u13 = 0;
                            u14 = 0;
                            u15 = 0;

                            p11 = combinacao[21];
                            p12 = combinacao[22];
                            p13 = combinacao[23];
                            p14 = combinacao[24];
                            p15 = combinacao[25];

                            TotalPremiacao = combinacao[26];

                            // Cinco maiores atrasos
                            atrasos.Add(combinacao[27]);
                            atrasos.Add(combinacao[28]);
                            atrasos.Add(combinacao[29]);
                            atrasos.Add(combinacao[30]);
                            atrasos.Add(combinacao[31]);

                            AtrasoAtual = combinacao[32];

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

                                // Premiado
                                if (contadorDezena > 10)
                                {
                                    // Últimas Premiações
                                    u11 = contadorDezena == 11 ? concurso[0] : u11;
                                    u12 = contadorDezena == 12 ? concurso[0] : u12;
                                    u13 = contadorDezena == 13 ? concurso[0] : u13;
                                    u14 = contadorDezena == 14 ? concurso[0] : u14;
                                    u15 = contadorDezena == 15 ? concurso[0] : u15;

                                    // Total de Premiações
                                    p11 = contadorDezena == 11 ? ++p11 : p11;
                                    p12 = contadorDezena == 12 ? ++p12 : p12;
                                    p13 = contadorDezena == 13 ? ++p13 : p13;
                                    p14 = contadorDezena == 14 ? ++p14 : p14;
                                    p15 = contadorDezena == 15 ? ++p15 : p15;

                                    TotalPremiacao = p11 + p12 + p13 + p14 + p15;

                                    // Se premiado, cinco maiores atrasos não mudam, e zera o atraso atual
                                    AtrasoAtual = 0;

                                    cincoAtrasos = atrasos;
                                }
                                // Não Premiado
                                else
                                {
                                    // Se não premiado, recalcula os cinco maiores atrasos.

                                    for (int k = 0; k < 5; k++)
                                    {
                                        if (AtrasoAtual == atrasos[k])
                                        {
                                            atrasos[k] = atrasos[k] + 1;
                                            break;
                                        }                                        
                                    }

                                    AtrasoAtual++;
                                }

                                cincoAtrasos = atrasos.OrderByDescending(i => i).ToList<int>();


                            } // loop Concursos

                            listaCombinacao.Add(
                                new int[] { combinacao[0], u11, u12, u13, u14, u15, p11, p12, p13, p14, p15, TotalPremiacao,
                                            cincoAtrasos[0], cincoAtrasos[1], cincoAtrasos[2], cincoAtrasos[3], cincoAtrasos[4],
                                            AtrasoAtual }
                            );

                            atrasos.Clear();
                            cincoAtrasos.Clear();

                        } // loop Combinações

                    }

                    Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));

                    sb.Clear();

                    int m = 0;
                    int n = 0;

                    foreach (int[] item in listaCombinacao)
                    {
                        m++;

                        if (item[1] != 0 || item[2] != 0 || item[3] != 0 || item[4] != 0 || item[5] != 0 ||
                            item[6] != 0 || item[7] != 0 || item[8] != 0 || item[9] != 0 || item[10] != 0 || item[11] != 0 ||
                            item[12] != 0 || item[13] != 0 || item[14] != 0 || item[15] != 0 || item[16] != 0)
                        {
                            n++;
                            Console.Write("\rProcessando combinação {0} - Gravando combinação {1}", m, n);

                            sb.AppendLine(@"UPDATE tbCombinacao SET ");

                            if (item[1] != 0)
                                sb.AppendFormat("IdUltimo11 = {0}, ", item[1]);
                            if (item[2] != 0)
                                sb.AppendFormat("IdUltimo12 = {0}, ", item[2]);
                            if (item[3] != 0)
                                sb.AppendFormat("IdUltimo13 = {0}, ", item[3]);
                            if (item[4] != 0)
                                sb.AppendFormat("IdUltimo14 = {0}, ", item[4]);
                            if (item[5] != 0)
                                sb.AppendFormat("IdUltimo15 = {0}, ", item[5]);

                            if (item[6] != 0)
                                sb.AppendFormat("Total11 = {0}, ", item[6]);
                            if (item[7] != 0)
                                sb.AppendFormat("Total12 = {0}, ", item[7]);
                            if (item[8] != 0)
                                sb.AppendFormat("Total13 = {0}, ", item[8]);
                            if (item[9] != 0)
                                sb.AppendFormat("Total14 = {0}, ", item[9]);
                            if (item[10] != 0)
                                sb.AppendFormat("Total15 = {0}, ", item[10]);
                            if (item[11] != 0)
                                sb.AppendFormat("TotalPremiacao = {0}, ", item[11]);

                            if (item[12] != 0)
                                sb.AppendFormat("Atraso01 = {0}, ", item[12]);
                            if (item[13] != 0)
                                sb.AppendFormat("Atraso02 = {0}, ", item[13]);
                            if (item[14] != 0)
                                sb.AppendFormat("Atraso03 = {0}, ", item[14]);
                            if (item[15] != 0)
                                sb.AppendFormat("Atraso04 = {0}, ", item[15]);
                            if (item[16] != 0)
                                sb.AppendFormat("Atraso05 = {0}, ", item[16]);
                            
                            sb.AppendFormat("AtrasoAtual = {0}, ", item[17]);

                            sb.AppendFormat("WHERE Id = {0} \n", item[0]);

                            sb.Replace(", WHERE", " WHERE");

                            if (n % 5000 == 0 || m == 3268760 || listaCombinacao.Count == 1)
                            {
                                new DB().Exec(sb);
                                sb.Clear();
                                Console.Write("\rGravando {0}...", n);
                            }
                        }
                    }
                    Console.WriteLine("\rGravação realizada com sucesso!");

                    DateTime fimLote = DateTime.Now;

                    TimeSpan duracaoLote = fimLote - inicioLote;

                    Console.WriteLine("Gravação realizada com sucesso! Duração: {0}", duracaoLote.ToString());
                    Console.WriteLine();
                }
            }
        }



        public static void ProcessarTodosConcursos(int inicio, int fim)
        {
            List<int[]> listaCombinacao = new List<int[]>();

            int[] combinacao = new int[16];

            int u11 = 0;
            int u12 = 0;
            int u13 = 0;
            int u14 = 0;
            int u15 = 0;

            int p11 = 0;
            int p12 = 0;
            int p13 = 0;
            int p14 = 0;
            int p15 = 0;

            List<int[]> listaConcursos = new List<int[]>();
            List<int> atrasos = new List<int>();
            List<int> cincoAtrasos = new List<int>();
            List<int> ultimasPremiacoes = new List<int>();
            List<int> ultimasPremiacoesSort = new List<int>();

            listaConcursos = new DB().SelectConcursos(false);
            int concursoAtual = listaConcursos[listaConcursos.Count - 1][0];

            int TotalPremiacao = 0;
            int AtrasoAtual = 0;
            int atraso = 0;

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("SELECT * FROM tbCombinacao WHERE Id BETWEEN {0} AND {1}", inicio, fim);

            Console.WriteLine();
            Console.WriteLine("Processando até o último concurso ({0})...", concursoAtual);

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
                            combinacao = new int[33];

                            // Id Combinação
                            combinacao[0] = reader.GetInt32(0);

                            // Números Combinação
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

                            // Últimas Premiações
                            combinacao[16] = reader.IsDBNull(16) ? 0 : reader.GetInt16(16);
                            combinacao[17] = reader.IsDBNull(17) ? 0 : reader.GetInt16(17);
                            combinacao[18] = reader.IsDBNull(18) ? 0 : reader.GetInt16(18);
                            combinacao[19] = reader.IsDBNull(19) ? 0 : reader.GetInt16(19);
                            combinacao[20] = reader.IsDBNull(20) ? 0 : reader.GetInt16(20);

                            // Total de Premiações
                            combinacao[21] = reader.IsDBNull(21) ? 0 : reader.GetInt16(21);
                            combinacao[22] = reader.IsDBNull(22) ? 0 : reader.GetInt16(22);
                            combinacao[23] = reader.IsDBNull(23) ? 0 : reader.GetInt16(23);
                            combinacao[24] = reader.IsDBNull(24) ? 0 : reader.GetInt16(24);
                            combinacao[25] = reader.IsDBNull(25) ? 0 : reader.GetInt16(25);
                            combinacao[26] = reader.IsDBNull(26) ? 0 : reader.GetInt16(26);

                            // Cinco maiores atrasos e Atraso Atual
                            combinacao[27] = reader.IsDBNull(27) ? 0 : reader.GetInt16(27);
                            combinacao[28] = reader.IsDBNull(28) ? 0 : reader.GetInt16(28);
                            combinacao[29] = reader.IsDBNull(29) ? 0 : reader.GetInt16(29);
                            combinacao[30] = reader.IsDBNull(30) ? 0 : reader.GetInt16(30);
                            combinacao[31] = reader.IsDBNull(31) ? 0 : reader.GetInt16(31);
                            combinacao[32] = reader.IsDBNull(32) ? 0 : reader.GetInt16(32);

                            Console.Write("\rProcessando a combinação {0}...", combinacao[0]);

                            int contadorDezena = 0;

                            u11 = 0;
                            u12 = 0;
                            u13 = 0;
                            u14 = 0;
                            u15 = 0;

                            p11 = combinacao[21];
                            p12 = combinacao[22];
                            p13 = combinacao[23];
                            p14 = combinacao[24];
                            p15 = combinacao[25];

                            TotalPremiacao = combinacao[26];

                            atrasos.Clear();
                            cincoAtrasos.Clear();
                            ultimasPremiacoes.Clear();
                            ultimasPremiacoesSort.Clear();

                            atraso = 0;

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

                                // Premiado
                                if (contadorDezena > 10)
                                {
                                    // Últimas Premiações
                                    u11 = contadorDezena == 11 ? concurso[0] : u11;
                                    u12 = contadorDezena == 12 ? concurso[0] : u12;
                                    u13 = contadorDezena == 13 ? concurso[0] : u13;
                                    u14 = contadorDezena == 14 ? concurso[0] : u14;
                                    u15 = contadorDezena == 15 ? concurso[0] : u15;

                                    // Total de Premiações
                                    p11 = contadorDezena == 11 ? ++p11 : p11;
                                    p12 = contadorDezena == 12 ? ++p12 : p12;
                                    p13 = contadorDezena == 13 ? ++p13 : p13;
                                    p14 = contadorDezena == 14 ? ++p14 : p14;
                                    p15 = contadorDezena == 15 ? ++p15 : p15;


                                    // Se premiado, zera os atrasos e inclui o valor na lista de atrasos
                                    atrasos.Add(atraso);
                                    atraso = 0;
                                }
                                // Não Premiado
                                else
                                {
                                    // Se não premiou, contabiliza mais um atraso
                                    atraso++;
                                }

                            } // loop Concursos

                            // Calcula o Atraso Atual
                            ultimasPremiacoes.Add(u11);
                            ultimasPremiacoes.Add(u12);
                            ultimasPremiacoes.Add(u13);
                            ultimasPremiacoes.Add(u14);
                            ultimasPremiacoes.Add(u15);

                            ultimasPremiacoesSort = ultimasPremiacoes.OrderByDescending(i => i).ToList<int>();

                            AtrasoAtual = concursoAtual - ultimasPremiacoesSort[0];

                            TotalPremiacao = p11 + p12 + p13 + p14 + p15;

                            // Adiciona o último atraso
                            if (atraso > 0)
                            {
                                atrasos.Add(atraso);
                            }

                            cincoAtrasos = atrasos.OrderByDescending(i => i).ToList<int>();

                            listaCombinacao.Add(
                                new int[] { combinacao[0], u11, u12, u13, u14, u15, p11, p12, p13, p14, p15, TotalPremiacao,
                                            cincoAtrasos[0], cincoAtrasos[1], cincoAtrasos[2], cincoAtrasos[3], cincoAtrasos[4],
                                            AtrasoAtual }
                            );

                        } // loop Combinações

                    }

                    Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));

                    sb.Clear();

                    int m = 0;
                    int n = 0;

                    foreach (int[] item in listaCombinacao)
                    {
                        m++;

                        if (item[1] != 0 || item[2] != 0 || item[3] != 0 || item[4] != 0 || item[5] != 0 ||
                            item[6] != 0 || item[7] != 0 || item[8] != 0 || item[9] != 0 || item[10] != 0 || item[11] != 0 ||
                            item[12] != 0 || item[13] != 0 || item[14] != 0 || item[15] != 0 || item[16] != 0)
                        {
                            n++;
                            Console.Write("\rProcessando combinação {0} - Gravando combinação {1}", m, n);

                            sb.AppendLine(@"UPDATE tbCombinacao SET ");

                            if (item[1] != 0)
                                sb.AppendFormat("IdUltimo11 = {0}, ", item[1]);
                            if (item[2] != 0)
                                sb.AppendFormat("IdUltimo12 = {0}, ", item[2]);
                            if (item[3] != 0)
                                sb.AppendFormat("IdUltimo13 = {0}, ", item[3]);
                            if (item[4] != 0)
                                sb.AppendFormat("IdUltimo14 = {0}, ", item[4]);
                            if (item[5] != 0)
                                sb.AppendFormat("IdUltimo15 = {0}, ", item[5]);

                            if (item[6] != 0)
                                sb.AppendFormat("Total11 = {0}, ", item[6]);
                            if (item[7] != 0)
                                sb.AppendFormat("Total12 = {0}, ", item[7]);
                            if (item[8] != 0)
                                sb.AppendFormat("Total13 = {0}, ", item[8]);
                            if (item[9] != 0)
                                sb.AppendFormat("Total14 = {0}, ", item[9]);
                            if (item[10] != 0)
                                sb.AppendFormat("Total15 = {0}, ", item[10]);
                            if (item[11] != 0)
                                sb.AppendFormat("TotalPremiacao = {0}, ", item[11]);

                            if (item[12] != 0)
                                sb.AppendFormat("Atraso01 = {0}, ", item[12]);
                            if (item[13] != 0)
                                sb.AppendFormat("Atraso02 = {0}, ", item[13]);
                            if (item[14] != 0)
                                sb.AppendFormat("Atraso03 = {0}, ", item[14]);
                            if (item[15] != 0)
                                sb.AppendFormat("Atraso04 = {0}, ", item[15]);
                            if (item[16] != 0)
                                sb.AppendFormat("Atraso05 = {0}, ", item[16]);

                            sb.AppendFormat("AtrasoAtual = {0}, ", item[17]);

                            sb.AppendFormat("WHERE Id = {0} \n", item[0]);

                            sb.Replace(", WHERE", " WHERE");

                            if (n % 5000 == 0 || m == 3268760 || listaCombinacao.Count == 1)
                            {
                                new DB().Exec(sb);
                                sb.Clear();
                                Console.Write("\rGravando {0}...", n);
                            }
                        }
                    }
                    Console.WriteLine("\rGravação realizada com sucesso!");

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
