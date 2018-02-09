using System;
using Xunit;
using CsvSorter;
using System.Text;

namespace SortedCsvTests
{
    public class UnitTest1
    {
        [Fact]
        public void SortedCsvInitialize()
        {
            /* @todo #3 First there should only be a stream as Input to read the csv.
            e.g. something like: 
                CsvStream(StreamReader stream)
            */
            var sorted = new SortedCsvFile("SampleCSVFile_10600kb.csv", Encoding.UTF8, 0, '"', true, '"');
        }
    }
}
