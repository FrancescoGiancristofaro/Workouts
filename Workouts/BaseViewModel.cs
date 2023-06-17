using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WorkoutsApp
{
    public partial class BaseViewModel : ObservableObject, IBaseViewModel
    {

        [ObservableProperty]
        bool isBusy;


        [RelayCommand]
        protected virtual async Task Appearing()
        {
        }

        [RelayCommand]
        protected virtual async Task Disappearing()
        {

        }
        [RelayCommand]
        protected virtual async Task BaseAppearing()
        {

            await Appearing();

        }

        [RelayCommand]
        protected virtual async Task BaseDisappearing()
        {
            await Disappearing();
        }
        public async Task ManageException(object ex)
        {
            await Shell.Current.DisplayAlert("Attenzione", ex.ToString(), "OK");
        }
    }
}
