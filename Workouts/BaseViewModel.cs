using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WorkoutsApp
{
    public partial class BaseViewModel : ObservableObject, IBaseViewModel
    {

        [ObservableProperty]
        bool isBusy;

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

        public virtual async Task NavigateBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        public async Task ManageException(object ex)
        {
            await Shell.Current.DisplayAlert("Attenzione", ex.ToString(), "OK");
        }

        protected async Task GoToAsync<T>( string route, string key, T dataToPass)
        {
            var parameters = new Dictionary<string, object>() { { key, dataToPass } };
            await Shell.Current.GoToAsync(route, parameters);
        }
    }
}
