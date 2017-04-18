using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaRooms.AutomationFramework.Tests
{
    public class CSVReader : IDisposable
    {
        private StreamReader reader;
        private string currentLine;
        private string[] data;
        private int dataLength;
        private bool isDataPopulated;
        private Dictionary<string, int?> header;
        private bool isFirstRowHeader;

        public CSVReader(string path, bool isFirstRowHeader)
        {
            this.reader = new StreamReader(path);
            this.isFirstRowHeader = isFirstRowHeader;
            if (this.isFirstRowHeader)
            {
                this.ReadHeader();
                this.data = new string[this.header.Count];
            }
            else
            {
                this.data = new string[100];
            }
            this.dataLength = 0;
        }

        private void ReadHeader()
        {
            this.currentLine = this.reader.ReadLine();
            this.header = this.CreateHeader();
        }

        private Dictionary<string, int?> CreateHeader()
        {
            Dictionary<string, int?> header = new Dictionary<string, int?>();
            string[] values = this.currentLine.ToLower().Split(',');
            for (int index = 0; index < values.Length; index++)
            {
                header.Add(values[index], index);
            }
            return header;
        }

        public bool Read()
        {
            this.currentLine = this.reader.ReadLine();
            this.isDataPopulated = false;
            return (!this.reader.EndOfStream || !string.IsNullOrEmpty(this.currentLine));
        }

        private void EnsureData()
        {
            if (!this.isDataPopulated) this.PopulateData();
        }

        private void ClearData()
        {
            for (int index = 0; index < this.dataLength; index++)
            {
                this.data[index] = null;
            }
        }

        private void PopulateData()
        {
            int count = this.currentLine.Length, startIndex = 0, dataIndex = 0;
            bool isInParenteses = false, isUsingParenteses = false;
            char c = (char)0;
            int index;
            for (index = 0; index < count; index++) {
                c = currentLine[index];
                if (c == '\"')
                {
                    isInParenteses = !isInParenteses;
                    isUsingParenteses = true;
                }
                else if (c == ',' && !isInParenteses)
                {
                    if (isUsingParenteses) {
                        int parentesesStart = this.currentLine.IndexOf('\"', startIndex, index - startIndex) + 1;
                        int parentesesEnd = this.currentLine.LastIndexOf('\"', index, index - startIndex);
                        this.data[dataIndex] = (parentesesStart < parentesesEnd ? this.currentLine.Substring(parentesesStart, parentesesEnd - parentesesStart).Replace("\"\"", "\"") : null);
                    } else {
                        this.data[dataIndex] = (startIndex < index ? this.currentLine.Substring(startIndex, index - startIndex) : null);
                    }
                    isUsingParenteses = false;
                    isInParenteses = false;
                    dataIndex++;
                    startIndex = index + 1;
                }
            }
            if (!isInParenteses)
            {
                index--;
                if (startIndex == count) startIndex--;
                if (isUsingParenteses) {
                    int parentesesStart = this.currentLine.IndexOf('\"', startIndex, index - startIndex) + 1;
                    int parentesesEnd = this.currentLine.LastIndexOf('\"', index, index - startIndex);
                    this.data[dataIndex] = (parentesesStart < parentesesEnd ? this.currentLine.Substring(parentesesStart, parentesesEnd - parentesesStart).Replace("\"\"", "\"") : null);
                } else {
                    this.data[dataIndex] = (startIndex < index ? this.currentLine.Substring(startIndex, index - startIndex) : null);
                }
            }

            this.dataLength = dataIndex;
            this.isDataPopulated = true;
        }

        private int GetColumnIndex(string columnName)
        {
            int? index;
            this.header.TryGetValue(columnName.ToLower(), out index);
            if (index == null) throw new Exception(string.Format("Column {0} not found.", columnName));
            return index.Value;            
        }

        public void Close()
        {
            this.reader.Close();
        }

        public void Dispose()
        {
            this.data = null;
            this.currentLine = null;
            this.reader.Dispose();
            this.reader = null;
        }

        public string this[int index]
        {
            get { 
                this.EnsureData();
                return this.data[index];
            }
        }

        public string this[string columnName]
        {
            get
            {
                this.EnsureData();
                return this.data[this.GetColumnIndex(columnName)];
            }
        }
    }
}
