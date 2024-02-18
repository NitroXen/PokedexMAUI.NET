
namespace MauiAppMVVM.Models
{
    public class PokemonSpecies
    {
           
            public Flavor_Text_Entries[] flavor_text_entries { get; set; }
            

        
        public class Flavor_Text_Entries
        {
            public string flavor_text { get; set; }
            public Language language { get; set; }
            public Version version { get; set; }
        }

        public class Language
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class Version
        {
            public string name { get; set; }
            public string url { get; set; }
        }


    }
}
