using System.Collections.Generic;

namespace QuestionaireToJsonConverter.Models
{
    public class Intent
    {
        public string tag { get; set; }
        public List<string> patterns { get; set; }
        public List<string> responses { get; private set; }

        public Intent(string tag, List<string> patterns)
        {
            this.tag = tag;
            this.patterns = patterns;
            responses = new List<string> { };
        }
    }
}
