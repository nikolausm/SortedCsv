using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CsvSorter
{
    public class SortedCsvFile
    {
        private int _columnIndex;
        private char _delimitter;
        private Encoding _encoding;
        private string _filename;
        private bool _hasHeader;
        private string _headerValue;
        private Dictionary<string, IdFileInfo> _tmpFiles;
        private char _enclosingChar;
        private string _lineBreak;

        internal class IdFileInfo
        {
            public StreamWriter StreamWriter;
            public string FilePath;
        }
        public SortedCsvFile(string filename, Encoding encoding, int columnIndex, char delimitter, bool hasHeader, char enclosingCharacter)
        {
            _filename = filename;
            _encoding = encoding;
            _delimitter = delimitter;
            _columnIndex = columnIndex;
            _hasHeader = hasHeader;
            _tmpFiles = new Dictionary<string, IdFileInfo>();
            _enclosingChar = enclosingCharacter;
        }

        private string Header()
        {
            if (!_hasHeader)
            {
                throw new Exception($"{nameof(_hasHeader)} was set to false");
            }

            if (_headerValue == null)
            {
                using (var reader = OpenFile())
                {
                    _headerValue = ReadRow(reader);
                }
            }

            return _headerValue;

        }

        public StreamReader OpenFile(string file = null)
        {
            return new StreamReader(
                new FileStream(
                    file ?? _filename,
                    FileMode.Open,
                    FileAccess.Read
                ),
                _encoding,
                true
            );
        }

        private string ReadRow(StreamReader reader)
        {
            var line = reader.ReadLine();
            throw new NotImplementedException();
        }

        public void SaveToFile(string fileName, int limit = -1)
        {
            var dir = (new FileInfo(fileName)).Directory;
            if (!dir.Exists)
            {
                Directory.CreateDirectory(dir.FullName);
            }

            throw new NotImplementedException();
        }
    }
}
