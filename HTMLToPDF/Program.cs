using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Markdig;

namespace HTMLToPDF
{
    class Program
    {
        static void Main(string[] args)
        {
            // ref : https://stackoverflow.com/questions/31261981/create-pdf-from-html-snippet-using-pdfsharp-having-external-css-classes-included
            string outputPath = "Result.pdf";

            HtmlToPDF(Markdown.ToHtml("# This is a text with some *emphasis*\n## This is a text with\n* sdfsdf\n* test"), outputPath);
            System.Diagnostics.Process.Start(outputPath);
            //Console.WriteLine("End...");
            //Console.ReadKey();
        }

        /// <summary>
        /// MemoryStream 사용
        /// </summary>
        public static Byte[] PdfSharpConvert(String html)
        {
            Byte[] res = null;
            using (MemoryStream ms = new MemoryStream())
            {
                var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
                pdf.Save(ms);
                res = ms.ToArray();
            }
            return res;
        }

        public static void HtmlToPDF(string html, string output)
        {
            var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
            pdf.Save(output);
        }
    }
}