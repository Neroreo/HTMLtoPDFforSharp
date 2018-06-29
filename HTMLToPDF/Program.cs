using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HTMLToPDF
{
    class Program
    {
        static void Main(string[] args)
        {
            // ref : https://stackoverflow.com/questions/31261981/create-pdf-from-html-snippet-using-pdfsharp-having-external-css-classes-included
            string testHtml1 = @"<head>
        <style>
            .test {
                background-color: linen;
                color: maroon;
            }
        </style>
    </head>
    <body class=""test"">
        <p>
            <h1>Hello World</h1>
            This is html rendered text with css and image.
        </p>
    </body>";

            //string testHtml2 = "<p><h1>Hello World</h1>This is html rendered text</p>";

            string outputPath = "Result.pdf";

            //예제1
            HTML2PDF(testHtml1, "Result1.pdf");

            //예제2
            var data = PdfSharpConvert(testHtml1);
            WriteByte("Result2.pdf", data);


            Console.WriteLine("End...");
            Console.ReadKey();
        }

        public static void HTML2PDF(string html, string output)
        {
            var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
            pdf.Save(output);
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

        public static void WriteByte(string output, Byte[] data)
        {
            using (FileStream fs = new FileStream(output, FileMode.Create, FileAccess.ReadWrite))
            {
                fs.Write(data, 0, data.Length);
            }
        }
    }
}