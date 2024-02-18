using MauiAppMVVM.Models;
using System.Diagnostics;
using System.Text.Json;

namespace MauiAppMVVM.Services
{
    public class PokemonSpeciesService
    {

        HttpClient _client;
        JsonSerializerOptions _serializerOptions;

        
        public PokemonSpecies Item { get; private set; }

        public PokemonSpeciesService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<PokemonSpecies> RefreshDataAsync(int id)
        {
            Item = new PokemonSpecies();

            Uri uri = new Uri(string.Format("https://pokeapi.co/api/v2/pokemon-species/" + id, string.Empty));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    //JsonNode node = JsonNode.Parse(content);

                    Item = JsonSerializer.Deserialize<PokemonSpecies>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                Item = null;
            }

            return Item;
        }
    }
}
