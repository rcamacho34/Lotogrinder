using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Lotogrinder
{
    public class DB
    {
        public SqlConnection Conn()
        {
            string SQLServer = Environment.MachineName;

            string strConexao = ConfigurationManager.ConnectionStrings["CONN_" + SQLServer].ConnectionString;

            SqlConnection conexao = new SqlConnection(strConexao);

            return conexao;
        }

        public void Exec(StringBuilder sb)
        {
            using (SqlConnection con = Conn())
            {
                if (con.State != ConnectionState.Open)
                    con.Open();

                SqlCommand cmd = new SqlCommand(sb.ToString());
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
            }
        }

        public void BulkCombinacao(List<int[]> lista)
        {
            using (SqlConnection con = Conn())
            {
                try
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                    {
                        DataTable dt = new DataTable();

                        dt.Columns.Add(new DataColumn("d1", typeof(string)));
                        dt.Columns.Add(new DataColumn("d2"));
                        dt.Columns.Add(new DataColumn("d3"));
                        dt.Columns.Add(new DataColumn("d4"));
                        dt.Columns.Add(new DataColumn("d5"));
                        dt.Columns.Add(new DataColumn("d6"));
                        dt.Columns.Add(new DataColumn("d7"));
                        dt.Columns.Add(new DataColumn("d8"));
                        dt.Columns.Add(new DataColumn("d9"));
                        dt.Columns.Add(new DataColumn("d10"));
                        dt.Columns.Add(new DataColumn("d11"));
                        dt.Columns.Add(new DataColumn("d12"));
                        dt.Columns.Add(new DataColumn("d13"));
                        dt.Columns.Add(new DataColumn("d14"));
                        dt.Columns.Add(new DataColumn("d15"));


                        foreach (int[] item in lista)
                        {
                            dt.Rows.Add(item[0], item[1], item[2], item[3], item[4],
                                        item[5], item[6], item[7], item[8], item[9],
                                        item[10], item[11], item[12], item[13], item[14]);
                        }

                        if (con.State != ConnectionState.Open)
                            con.Open();

                        bulkCopy.BatchSize = 50;
                        bulkCopy.DestinationTableName = "tbCombinacao";
                        bulkCopy.BulkCopyTimeout = 0;

                        bulkCopy.ColumnMappings.Add(0, 1);
                        bulkCopy.ColumnMappings.Add(1, 2);
                        bulkCopy.ColumnMappings.Add(2, 3);
                        bulkCopy.ColumnMappings.Add(3, 4);
                        bulkCopy.ColumnMappings.Add(4, 5);
                        bulkCopy.ColumnMappings.Add(5, 6);
                        bulkCopy.ColumnMappings.Add(6, 7);
                        bulkCopy.ColumnMappings.Add(7, 8);
                        bulkCopy.ColumnMappings.Add(8, 9);
                        bulkCopy.ColumnMappings.Add(9, 10);
                        bulkCopy.ColumnMappings.Add(10, 11);
                        bulkCopy.ColumnMappings.Add(11, 12);
                        bulkCopy.ColumnMappings.Add(12, 13);
                        bulkCopy.ColumnMappings.Add(13, 14);
                        bulkCopy.ColumnMappings.Add(14, 15);

                        bulkCopy.WriteToServer(dt);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro no Bulkcopy: {0}", ex.Message);
                    con.Close();
                }
            }
        }

        public void BulkConcurso(List<string[]> lista)
        {
            using (SqlConnection con = Conn())
            {
                try
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                    {
                        DataTable dt = new DataTable();

                        dt.Columns.Add(new DataColumn("Id", typeof(int)));
                        dt.Columns.Add(new DataColumn("DataSorteio", typeof(DateTime)));
                        dt.Columns.Add(new DataColumn("d1", typeof(int)));
                        dt.Columns.Add(new DataColumn("d2", typeof(int)));
                        dt.Columns.Add(new DataColumn("d3", typeof(int)));
                        dt.Columns.Add(new DataColumn("d4", typeof(int)));
                        dt.Columns.Add(new DataColumn("d5", typeof(int)));
                        dt.Columns.Add(new DataColumn("d6", typeof(int)));
                        dt.Columns.Add(new DataColumn("d7", typeof(int)));
                        dt.Columns.Add(new DataColumn("d8", typeof(int)));
                        dt.Columns.Add(new DataColumn("d9", typeof(int)));
                        dt.Columns.Add(new DataColumn("d10", typeof(int)));
                        dt.Columns.Add(new DataColumn("d11", typeof(int)));
                        dt.Columns.Add(new DataColumn("d12", typeof(int)));
                        dt.Columns.Add(new DataColumn("d13", typeof(int)));
                        dt.Columns.Add(new DataColumn("d14", typeof(int)));
                        dt.Columns.Add(new DataColumn("d15", typeof(int)));

                        foreach (string[] item in lista)
                        {
                            dt.Rows.Add(int.Parse(item[0]), DateTime.ParseExact(item[1], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), int.Parse(item[2]), int.Parse(item[3]), int.Parse(item[4]),
                                        int.Parse(item[5]), int.Parse(item[6]), int.Parse(item[7]), int.Parse(item[8]), int.Parse(item[9]),
                                        int.Parse(item[10]), int.Parse(item[11]), int.Parse(item[12]), int.Parse(item[13]), int.Parse(item[14]), int.Parse(item[15]), int.Parse(item[16]));
                        }

                        if (con.State != ConnectionState.Open)
                            con.Open();

                        bulkCopy.BatchSize = 50;
                        bulkCopy.DestinationTableName = "tbConcurso";
                        bulkCopy.BulkCopyTimeout = 0;

                        bulkCopy.ColumnMappings.Add(0, 0);
                        bulkCopy.ColumnMappings.Add(1, 1);
                        bulkCopy.ColumnMappings.Add(2, 2);
                        bulkCopy.ColumnMappings.Add(3, 3);
                        bulkCopy.ColumnMappings.Add(4, 4);
                        bulkCopy.ColumnMappings.Add(5, 5);
                        bulkCopy.ColumnMappings.Add(6, 6);
                        bulkCopy.ColumnMappings.Add(7, 7);
                        bulkCopy.ColumnMappings.Add(8, 8);
                        bulkCopy.ColumnMappings.Add(9, 9);
                        bulkCopy.ColumnMappings.Add(10, 10);
                        bulkCopy.ColumnMappings.Add(11, 11);
                        bulkCopy.ColumnMappings.Add(12, 12);
                        bulkCopy.ColumnMappings.Add(13, 13);
                        bulkCopy.ColumnMappings.Add(14, 14);
                        bulkCopy.ColumnMappings.Add(15, 15);
                        bulkCopy.ColumnMappings.Add(16, 16);

                        bulkCopy.WriteToServer(dt);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro no Bulkcopy: {0}", ex.Message);
                    con.Close();
                }
            }
        }

        public void BulkCombinacaoConcurso(List<int[]> lista)
        {
            using (SqlConnection con = Conn())
            {
                try
                {
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                    {
                        DataTable dt = new DataTable();

                        dt.Columns.Add(new DataColumn("IdCombinacao", typeof(int)));
                        dt.Columns.Add(new DataColumn("IdConcurso", typeof(int)));
                        dt.Columns.Add(new DataColumn("p11", typeof(int)));
                        dt.Columns.Add(new DataColumn("p12", typeof(int)));
                        dt.Columns.Add(new DataColumn("p13", typeof(int)));
                        dt.Columns.Add(new DataColumn("p14", typeof(int)));
                        dt.Columns.Add(new DataColumn("p15", typeof(int)));


                        foreach (int[] item in lista)
                        {
                            dt.Rows.Add(item[0], item[1], item[2], item[3], 
                                        item[4], item[5], item[6]);
                        }

                        if (con.State != ConnectionState.Open)
                            con.Open();

                        bulkCopy.BatchSize = 5000;
                        bulkCopy.DestinationTableName = "tbCombinacaoConcurso";
                        bulkCopy.BulkCopyTimeout = 0;

                        bulkCopy.ColumnMappings.Add(0, 0);
                        bulkCopy.ColumnMappings.Add(1, 1);
                        bulkCopy.ColumnMappings.Add(2, 2);
                        bulkCopy.ColumnMappings.Add(3, 3);
                        bulkCopy.ColumnMappings.Add(4, 4);
                        bulkCopy.ColumnMappings.Add(5, 5);
                        bulkCopy.ColumnMappings.Add(6, 6);

                        bulkCopy.WriteToServer(dt);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro no Bulkcopy: {0}", ex.Message);
                    con.Close();
                }
            }
        }


        public DataSet Select(StringBuilder sb)
        {
            using (SqlConnection con = Conn())
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();

                    SqlCommand cmd = new SqlCommand(sb.ToString());
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    cmd.CommandTimeout = 0;

                    using (DataSet ds = new DataSet())
                    {
                        sda.Fill(ds);

                        return ds;
                    }

                }
            }
        }

        public int GetUltimoLote()
        {
            int retorno = 0;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT ISNULL(MAX(IdCombinacao), 0) As UltimoLote FROM tbCombinacaoConcurso");

            DataTable dt = new DB().Select(sb).Tables[0];

            if (dt.Rows.Count > 0)
            {
                retorno = int.Parse(dt.Rows[0]["UltimoLote"].ToString());
            }

            return retorno;
        }

        public List<int[]> SelectConcursos()
        {
            List<int[]> listaConcursos = new List<int[]>();
            int[] concurso = new int[17];

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@"SELECT Id, d1, d2, d3, d4, d5, d6, d7, d8, d9, d10, d11, d12, d13, d14, d15
                            FROM tbConcurso ORDER BY Id");

            DataTable dt = new DB().Select(sb).Tables[0];

            foreach (DataRow item in dt.Rows)
            {
                concurso = new int[16];

                concurso[0] = int.Parse(item["Id"].ToString());
                concurso[1] = int.Parse(item["d1"].ToString());
                concurso[2] = int.Parse(item["d2"].ToString());
                concurso[3] = int.Parse(item["d3"].ToString());
                concurso[4] = int.Parse(item["d4"].ToString());
                concurso[5] = int.Parse(item["d5"].ToString());
                concurso[6] = int.Parse(item["d6"].ToString());
                concurso[7] = int.Parse(item["d7"].ToString());
                concurso[8] = int.Parse(item["d8"].ToString());
                concurso[9] = int.Parse(item["d9"].ToString());
                concurso[10] = int.Parse(item["d10"].ToString());
                concurso[11] = int.Parse(item["d11"].ToString());
                concurso[12] = int.Parse(item["d12"].ToString());
                concurso[13] = int.Parse(item["d13"].ToString());
                concurso[14] = int.Parse(item["d14"].ToString());
                concurso[15] = int.Parse(item["d15"].ToString());

                listaConcursos.Add(concurso);
            }

            return listaConcursos;
        }

        public void InserirCombinacaoConcurso(int IdCombinacao, int IdConcurso, int p11, int p12, int p13, int p14, int p15)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(@"INSERT INTO tbCombinacaoConcurso VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6})",
                                IdCombinacao, IdConcurso, p11, p12, p13, p14, p15);

            Exec(sb);
        }

        public void InserirCombinacaoConcurso1000(StringBuilder sb)
        {
            Exec(sb);
        }

    }
}
