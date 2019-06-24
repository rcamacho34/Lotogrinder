using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Lotogrinder
{
    public class Grinder
    {
        public static List<string[]> LerConcursos()
        {
            List<string[]> listaConcursos = new List<string[]>();
            string[] concurso = new string[17];

            HtmlDocument htmlDoc = new HtmlDocument();

            htmlDoc.Load(@"D:\Loteria\D_lotfac\d_lotfac.htm");

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

        public static void ProcessarAtraso()
        {

        }

    }
}
