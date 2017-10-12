using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _8LMBackend.Service
{
    public class EmailItem
    {
        public string FullName;
        public string FirstName;
        public string LastName;
        public string email;
    }

    public class EmailListParser
    {
        List<EmailItem> eItems;

        string delimiter = ",";
        string FileName;

        public EmailListParser(string FileName)
        {
            this.FileName = FileName;
            eItems = new List<EmailItem>();
        }

        public void Parse()
        {
            foreach (var s in File.ReadAllLines(FileName))
            {
                EmailItem item = new EmailItem();

                var i = s.IndexOf(delimiter);
                if (i < 0)
                    throw new Exception("Incorrect format");

                var j = s.IndexOf(delimiter, i + 1);
                item.FirstName = s.Substring(0, i);
                if (j > 0)
                {
                    item.LastName = s.Substring(i, j - i);
                    item.email = s.Substring(j);
                }
                else
                {
                    item.LastName = "";
                    item.email = s.Substring(i);
                }

                item.FullName = item.FirstName + item.LastName;
                eItems.Add(item);
            }
        }

        public void Save()
        {

        }
    }
}
