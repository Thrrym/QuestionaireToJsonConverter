using System;
using System.Collections.Generic;
using System.Linq;

namespace QuestionaireToJsonConverter.Models
{
    public class JSON
    {
        public List<Intent> intents { get; set; }

        public JSON(List<Intent> intents)
        {
            this.intents = intents;
        }

        public bool IntentExist(string tagName)
        {
            return intents.Select(intent => intent.tag).Contains(tagName);
        }

        public Intent GetIntentForTag(string tag)
        {
            Intent result = null;
            foreach (Intent intent in intents)
            {
                if (intent.tag.Equals(tag))
                {
                    result = intent;
                }
            }
            return result;
        }

        public void Beautify() { }
    }
}
