using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppMVVM.Models;
using MauiAppMVVM.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppMVVM.ViewModels
{
    public partial class PokemonViewModel : ObservableObject
    {
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string sprite;
        [ObservableProperty]
        private string description;


        private Pokemon p;
        private PokemonSpecies ps;
        Random rand;

        public PokemonViewModel()
        {
            Id = 1;
            DeclararPokemon();
            rand = new Random();

        }

        public async void DeclararPokemon()
        {
            p = await new PokemonService().RefreshDataAsync(Id);
            ps = await new PokemonSpeciesService().RefreshDataAsync(Id);

            if (p != null && ps != null)
            {
                Name =  p.name;
                Sprite = p.sprites.front_default;

                List<string> descipcions = new List<string>();
                for (int i = 0; i < ps.flavor_text_entries.Length; i++)
                {
                    if (ps.flavor_text_entries[i].language.name.Equals("es"))
                    {
                        descipcions.Add(ps.flavor_text_entries[i].flavor_text);
                    }
                }

                int index = descipcions.Count;

                Description = descipcions[rand.Next(index - 1)].Replace('\n', ' ');
            }

        }


        [RelayCommand]
        public void Incrementar()
        {
            Id++;

            if (Id > 151)
            {
                Id = 1;
            }

            DeclararPokemon();

        }
        [RelayCommand]
        public void Decrementar()
        {

            Id--;

            if (Id < 1)
            {
                Id = 151;
            }

            DeclararPokemon();
        }

    }
}
