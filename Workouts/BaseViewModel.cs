using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WorkoutsApp
{
    public partial class BaseViewModel : ObservableObject, IBaseViewModel
    {

        [ObservableProperty]
        bool isBusy;


        [RelayCommand]
        protected virtual void Appearing()
        {
        }

        [RelayCommand]
        protected virtual void Disappearing()
        {

        }

        public async Task ManageException(object ex)
        {
            await Shell.Current.DisplayAlert("Attenzione", ex.ToString(), "OK");
        }
    }
}
