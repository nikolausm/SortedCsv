using System;
using System.IO;
using System.Text;

namespace CsvSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            /* @todo #1 Add some Examples here */
            System.Console.WriteLine("Starting");
            var sortedCsv = new SortedCsvFile(filename: "asd", encoding: Encoding.GetEncoding("utf-8"), columnIndex: 0,  delimitter: ';',  hasHeader: true, enclosingCharacter: '"');
            System.Console.WriteLine("Done");
        }
    }
}
