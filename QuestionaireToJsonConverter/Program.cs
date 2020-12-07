using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace QuestionaireToJsonConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var reader = new StreamReader($@"C:\Datensatzerstellung.csv"))
            {
                //while (!reader.EndOfStream)
                //{
                    var questions = reader.ReadLine();

                    var intents = GetIntents(questions);

                    var values = questions.Split(',');
                //}
            }
        }

        static private List<string> GetIntents(string questions)
        {
            var a = Regex.Matches(questions, @"~~(.+?)~~");
            return a.Select(bla => bla.Groups[1].Value).ToList();

        }
    }
}
