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
        private SqlConnection Conn()
        {
            string strConexao = ConfigurationManager.ConnectionStrings["CONN_LF"].ConnectionString;

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

                        dt.Columns.Add(new DataColumn("Numero", typeof(int)));
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
                        bulkCopy.ColumnMappings.Add(15, 16);
                        bulkCopy.ColumnMappings.Add(16, 17);

                        bulkCopy.WriteToServer(dt);
                    }
                }
                catch (Exception ex)
                {
                    con.Close();
                }
            }
        }

    }
}
