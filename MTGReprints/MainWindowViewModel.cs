using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTGReprints.ScryFall;

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
            foreach (Card card in reprints)
            {
                ImageGallery.Add(card.ImageUris);
            }
        }

        public async Task TestFunction(SetDef selectedItem)
        {

        }


    }
}
