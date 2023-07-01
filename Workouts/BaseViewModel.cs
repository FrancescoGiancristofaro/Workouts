using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WorkoutsApp
{
    public partial class BaseViewModel : ObservableObject, IBaseViewModel
    {

        [ObservableProperty]
        static bool isBusy;

        public BaseViewModel()
        {
        }

        public virtual void PrepareModel()
        {
        }


        public virtual void ReversePrepareModel()
        {

        }

        public virtual void OnAppearing()
        {
        }


        public virtual void OnDisappearing()
        {

        }

        public virtual void NavigateBack()
        {
            Shell.Current.GoToAsync("..");
        }

        public async Task ManageException(object ex)
        {
            await Shell.Current.DisplayAlert("Attenzione", ex.ToString(), "OK");
        }
    }
}
