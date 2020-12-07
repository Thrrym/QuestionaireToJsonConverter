using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using Newtonsoft.Json;

using QuestionaireToJsonConverter.Models;

namespace QuestionaireToJsonConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            using var reader = new StreamReader($@"C:\Datensatzerstellung.csv");

            JSON json = new JSON(new List<Intent>());

            var questions = reader.ReadLine();
            var intents = GetIntents(questions);

            foreach (string intent in intents)
            {
                json.intents.Add(new Intent(intent, new List<string>()));
            }

            while (!reader.EndOfStream)
            {
                string line = string.Empty;
                int counter = 0;

                bool

                do
                {
                    line = reader.ReadLine();

                    if (!line.Equals(string.Empty))
                    {
                        break;
                    }

                    var values = line.Split("\",\"");

                    if (line.StartsWith("\"2020") || line.StartsWith("\"2021"))
                    {
                        values = values.Skip(1).ToArray();
                    }

                    for (int i = 0; i < values.Length; i++)
                    {
                        if (values.Length == 1 || i == 0)
                        {
                            json.intents[counter].patterns.Add(values[i]);
                        }
                        else
                        {
                            counter++;
                            json.intents[counter].patterns.Add(values[i]);
                        }
                    }

                } while (!line.Equals(string.Empty) || !line.Substring(line.Length - 1).Equals("\""));

                //values = values.Skip(1).ToArray();
                //answers.Add(values);
            }








            List<string[]> answers = new List<string[]> { };

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                while (!line.Substring(line.Length - 1).Equals("\""))
                {
                    var nextLine = reader.ReadLine();
                    line += nextLine;
                }

                line = line.Remove(line.Length - 1);
                var values = line.Split("\",\"");
                values = values.Skip(1).ToArray();
                answers.Add(values);
            }

            for (int i = 0; i < intents.Count; i++)
            {
                if (json.IntentExist(intents[i]))
                {
                    Intent intent = json.GetIntentForTag(intents[i]);

                    foreach (string[] answer in answers)
                    {
                        intent.patterns.Add(answer[i]);
                    }
                }
                else
                {
                    json.intents.Add(new Intent(intents[i], new List<string>()));

                    Intent intent = json.GetIntentForTag(intents[i]);

                    foreach (string[] answer in answers)
                    {
                        intent.patterns.Add(answer[i]);
                    }
                }
            }

            CreateJson(json);
        }

        static private List<string> GetIntents(string questions)
        {
            return Regex.Matches(questions, @"~~(.+?)~~").Select(match => match.Groups[1].Value).ToList();
        }

        static private void CreateJson(JSON json)
        {
            string jsonFileName = "DATA" + "_" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + "." + "json";

            string jsonText = JsonConvert.SerializeObject(json, Formatting.Indented);

            using StreamWriter streamWriter = File.CreateText(jsonFileName);

            streamWriter.Write(jsonText);
        }
    }
}
