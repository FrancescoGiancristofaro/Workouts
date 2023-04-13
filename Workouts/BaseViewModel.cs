using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WorkoutsApp
{
    public partial class BaseViewModel : ObservableObject
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

    }
}
