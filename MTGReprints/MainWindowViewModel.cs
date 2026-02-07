using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MTGReprints.ScryFall;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGReprints
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial int Count { get; set; }

        [RelayCommand]
        public void IncrementCount()
        {
            Count++;
            //_=GetCards();
            _ = GetAllSets();
        }

        public ScryFallCall scryFallCall { get; set; } = new ScryFallCall();

        List<Card> reprints { get; set; } = null;
        List<SetDef> allSetDef { get; set; } = null;

        public ObservableCollection<ImageLink> ImageGallery { get; } = new();

        public ObservableCollection<SetDef> ImageGallerySET { get; } = new();

        private async Task GetCards()
        {
            reprints = await scryFallCall.GetCardsFromASetTest("");

            ImageGallery.Clear();
            foreach (Card card in reprints)
            {
                ImageGallery.Add(card.ImageUris);
            }
        }

        private async Task GetAllSets()
        {
            allSetDef = await scryFallCall.GetSetIconsAsync();

            ImageGallery.Clear();
            foreach (SetDef s in allSetDef)
            {
                ImageGallerySET.Add(s);
            }
        }

        public async Task GetCardsFromSet(string SetName)
        {
            reprints = await scryFallCall.GetCardsFromASetTest(SetName);

            ImageGallery.Clear();
            if (reprints is not null)
            {
                foreach (Card card in reprints)
                {
                    ImageGallery.Add(card.ImageUris);
                }
            }
            else
            {
                Debug.WriteLine($"FAIL TO FIND REPRINTS or Too many requests");
            }

        }

        public IRelayCommand<SetDef> ClickCommand => new RelayCommand<SetDef>(OnButtonClicked);

        
        private void OnButtonClicked(SetDef selectedSet)
        {
            if (selectedSet != null)
            {
                // Do something with the data here!
                Debug.WriteLine($"Clicked on: {selectedSet.ScryfallUri}");

                _ = GetCardsFromSet(selectedSet.SearchUri);
            }
        }


    }
}
