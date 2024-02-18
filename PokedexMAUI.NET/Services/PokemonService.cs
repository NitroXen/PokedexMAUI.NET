using MauiAppMVVM.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MauiAppMVVM.Services
{
    public class PokemonService
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;

        public List<Pokemon> Items { get; private set; }
        public Pokemon Item { get; private set; }

        public PokemonService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<Pokemon> RefreshDataAsync(int id)
        {
            Item = new Pokemon();

            Uri uri = new Uri(string.Format("https://pokeapi.co/api/v2/pokemon/" + id, string.Empty));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    //JsonNode node = JsonNode.Parse(content);
                    
                    Item = JsonSerializer.Deserialize<Pokemon>(content, _serializerOptions);
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
