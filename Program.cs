using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallComponents.PDF.Layout;
using TallComponents.PDF.Layout.Paragraphs;

namespace SimpleXhtmlShapeFontSize
{
    class Program
    {
        static void Main(string[] args)
        {
            Document document = new Document();
            Section section = document.Sections.Add();

            XhtmlParagraph xhtmlParagraph = new XhtmlParagraph();

            ConversionSettings conversionSettings = new ConversionSettings();

            xhtmlParagraph.Settings = conversionSettings;
            xhtmlParagraph.Text = Html();

            section.Paragraphs.Add(xhtmlParagraph);

            using (var file = new FileStream(@"..\..\result.pdf", FileMode.Create, FileAccess.Write))
            {
                document.Write(file);
            }
        }

        static private String Html()
        {
            return @"
                        <body>
                            <h1>Fontsizes in html</h1>
                    " +
                            Table() +
                   @"       <p>
                                'larger' and 'smaller' ar relative to the surrounding or default fontsize.
                            </p>
                        </body>
                    ";
        }

        static private String Table()
        {
            return @" 
                    <table style='border: 1px solid black;'>
                        <tr>
                            <th style='border: 1px solid black;'>Named</th>
                            <th style='border: 1px solid black;'>Coded</th>
                        </tr>
                    " +
                    Row("xx-small", "4pt") +
                    Row("x-small",  "6pt") +
                    Row("smaller",  "80%") +
                    Row("small",    "10pt") +
                    Row("medium",   "12pt") +
                    Row("large",    "14pt") +
                    Row("larger",   "120%") +
                    Row("x-large",  "18pt") +
                    Row("xx-large", "20pt") +
                    @"
                    </table>
                    ";
        }

        static private String Row(String named, String coded)
        {
            return @"
                     <tr>
                    " + Column(named) +
                        Column(coded) +
                  @"
                    </tr>
                   ";
        }

        static private String Column(String name)
        {
            return @"
                        <td style='border: 1px solid black; font-size: @Name@;'> @Name@</td>
                    ".Replace("@Name@", name);
        }
    }
}
