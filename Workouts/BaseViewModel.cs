using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkoutsApp.Dtos;
using WorkoutsApp.Pages.Templates;

namespace WorkoutsApp
{
    public partial class BaseViewModel : ObservableObject, IBaseViewModel
    {

        [ObservableProperty]
        bool isBusy;
        private readonly IServiceProvider _serviceProvider;

        public BaseViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected void ShowPopup(Type pageToShow, object data = null)
        {
            var popup = (BasePopup)_serviceProvider.GetRequiredService(pageToShow);
            popup.Data = data;
            Shell.Current.ShowPopup(popup);
        }

        protected async Task<string> ShowEditorPopup(string title)
        {
            var popup = (BasePopup)_serviceProvider.GetRequiredService(typeof(EditorPopup));
            popup.Data = title;
            return await Shell.Current.ShowPopupAsync(popup) as string;
        }

        protected async Task DisplayAlert(string title, string description)
        {
            var popup = (BasePopup)_serviceProvider.GetRequiredService(typeof(InfoPopup));
            popup.Data = new DisplayAlertDto()
            {
                Title = title,
                Message = description
            };
            await Shell.Current.ShowPopupAsync(popup);
        }
        public virtual void PrepareModel()
        {
        }


        public virtual void ReversePrepareModel(object data = null)
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
