using System;
using Xunit;
using CsvSorter;
using System.Text;
using System.Net.Http;
using System.IO;
using System.Collections.Generic;

namespace SortedCsvTests
{
    public class UnitTest1
    {
        [Fact]
        public void SortedCsvInitialize()
        {
            /* @todo #3:90min First there should only be a stream as Input to read the csv.
            e.g. something like: 
                CsvStream(StreamReader stream)
            */
            var sorted = new SortedCsvFile("SampleCSVFile_10600kb.csv", Encoding.UTF8, 0, '"', true, '"');
        }

        [Fact]
        public void OneSingleLineTest()
        {
            var test = "default;value;\"bla\";test\r";
            var result = new CsvLineReader(new StringReader(test)).ReadLine();
            var expected = "bla";
            Assert.Equal(expected, result[2]);
        }

        [Fact]
        public void OneMutipleLineLineTest()
        {
            var test = "first\r\nsecond";
            var reader = new CsvLineReader(new StringReader(test));
            reader.ReadLine();
            var second = reader.ReadLine();
            var expected = "second";
            Assert.Equal(expected, second[0]);
        }

        [Fact]
        public void OneMutipleLineLineWithDifferentLineSeparatorsTest()
        {
            var test = "default;value;\"bla\";test\r\nsecond;line\nthird\rfourth";
            var reader = new CsvLineReader(new StringReader(test));
            var first = reader.ReadLine();
            var second = reader.ReadLine();
            var third = reader.ReadLine();
            var fourth = reader.ReadLine();


            Assert.Equal("third", third[0]);
            Assert.Equal("fourth", fourth[0]);
        }

        [Fact]
        public void OneMutipleLineLineWithDifferentLineSeparators2Test()
        {
            var test = "default;value;\"bla\";\"test\r\nsecond\";line\nthird\rfourth";
            var result = new CsvLineReader(new StringReader(test), ';', '"').ReadLine();

            Assert.Equal("test\r\nsecond", result[3]);
        }

        /*
        @todo #1:45min Create possibility for doublequotes  escapes
         */
        public class CsvLineReader
        {
            StringReader _stream;
            private char _delimiter;
            private char _quotationSign;

            public CsvLineReader(StringReader stream, char delimiter = ';', char quotationMark = '"')
            {
                if (delimiter == quotationMark)
                {
                    throw new ArgumentException($"{nameof(delimiter)} must not equal tro {nameof(quotationMark)}");
                }
                _delimiter = delimiter;
                _quotationSign = quotationMark;
                if (stream == null)
                {
                    throw new ArgumentNullException(nameof(stream));
                }
                _stream = stream;
            }


            /* 
            @todo #1:15min Move this to an extra file an clean up the old mess. 
            */
            public string[] ReadLine(string currentValue = null, List<string> columns = null)
            {
                var open = (currentValue != null);
                
                if (columns == null)
                {
                    columns = new List<string>();
                }
                
                var line = _stream.ReadLine();

                var tmpValue = currentValue ?? "" ;
                foreach (string entry in line.Split(_delimiter))
                {
                    if ((entry.Length == 0) && !open)
                    {
                        columns.Add("");
                    }
                    else if (entry[0] == _quotationSign && entry[entry.Length - 1] == _quotationSign && entry.Length >= 2)
                    {
                        columns.Add(entry.Substring(1, entry.Length - 2));
                    }
                    else if (entry[0] == '"' && !open)
                    {
                        tmpValue = entry.Substring(1);
                        open = true;
                    }
                    else if (entry[entry.Length - 1] == _quotationSign && open)
                    {
                        columns.Add(tmpValue + "\r\n" + entry.Substring(0, entry.Length -1));
                        tmpValue = "";
                        open = false;
                    }
                    else
                    {
                        if (open)
                        {
                            tmpValue += entry;
                        }
                        else
                        {
                            columns.Add((tmpValue != "" ? tmpValue + _delimiter : tmpValue) + entry);
                            tmpValue = "";
                        }
                    }
                }
                if (open)
                {
                    return ReadLine(tmpValue, columns);
                }
                return columns.ToArray();
            }
        }
    }
}
