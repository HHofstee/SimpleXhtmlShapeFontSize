using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TallComponents.PDF.Layout;
using TallComponents.PDF.Layout.Paragraphs;
using TallComponents.PDF.Layout.Shapes;
using TallComponents.PDF.Layout.Brushes;
using TallComponents.PDF.Layout.Pens;

namespace SimpleXhtmlShapeFontSize
{
    class Program
    {
        static void Main(string[] args)
        {
            Document document = new Document();
            Section section = document.Sections.Add();

            FontsMapping(section, 8);
            FontsMapping(section, 12);
            FontsMapping(section, 18);

            using (var file = new FileStream(@"..\..\result.pdf", FileMode.Create, FileAccess.Write))
            {
                document.Write(file);
            }
        }
        static private Drawing FontsMapping(Section section, Int32 fontsize)
        {
            Drawing drawing = new Drawing(
                    section.PageSize.Width - section.Margin.Left - section.Margin.Right,
                    section.PageSize.Height - section.Margin.Top - section.Margin.Bottom);
            section.Paragraphs.Add(drawing);

            SimpleXhtmlShape shape = new SimpleXhtmlShape();
            shape.Dock = DockStyle.Top;
            shape.DefaultFontSize = fontsize;
            shape.Text = Html(fontsize);
            shape.Width = drawing.Width;
            drawing.Shapes.Add(shape);

            return drawing;
        }


        static private String Html(Int32 fontsize)
        {
            return (@"
                        <body>
                            <p style='font-size: xx-large;'>Fontsizes in html: @FS@</p>
                            <p></p>
                    " +
                            Table(fontsize) +
                   @"       <p>'larger' and 'smaller' ar relative to the surrounding or default fontsize.</p>
                            <p></p>
                        </body>
                    ").Replace("@FS@", fontsize.ToString() + "pt");
        }

        static private String Table(Int32 fontsize)
        {
            return
                    Row(1, "xx-small", 8, "8pt") +
                    Row(1, "x-small", 10, "10pt") +
                    Row(1, "small", 12, "12pt") +
                    Row(1, "medium", 16, "16pt") +
                    Row(1, "large", 20, "20pt") +
                    Row(1, "x-large", 24, "24pt") +
                    Row(1, "xx-large", 36, "36pt") +
                    Row(fontsize, "smaller", 0.8, "0.8 * @FS@") +
                    Row(fontsize, "larger", 1.2, "1.2 * @FS@") +
                    "";
        }

        static private String Row(Int32 fontsize, String named, double factor, String coded)
        {
            string fs = (fontsize * factor).ToString() + "pt";

            return ("<p>" + Column(named) + "<span style='font-size: @fs@;'> -- </span>" + Column(fs, coded) + "</p>").Replace("@fs@", named);
        }

        static private String Column(String name)
        {
            return @"<span style='font-size: @Name@;'> @Name@</span>".Replace("@Name@", name);
        }

        static private String Column(string fs, String name)
        {
            return @"<span style='font-size: @FontSize@;'> @Name@</span>".Replace("@Name@", name).Replace("@FontSize@", fs);
        }
    }
}
