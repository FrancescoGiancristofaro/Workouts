using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using WorkoutsApp.Pages.Templates;

namespace WorkoutsApp
{
    public partial class BasePopupViewModel : ObservableObject
    {

        [ObservableProperty]
        bool isBusy;

        public BasePopup Popup { get; set; }

        public BasePopupViewModel()
        {
        }

        public virtual void Opened()
        {
        }

        public virtual void Dismissed()
        {
            //_popupService.DismissPopup(null);
        }

        public async Task ManageException(object ex)
        {
            await Shell.Current.DisplayAlert("Attenzione", ex.ToString(), "OK");
        }

    }

}
