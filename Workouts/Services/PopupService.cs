using CommunityToolkit.Maui.Views;
using WorkoutsApp.Pages.Templates;

namespace WorkoutsApp.Services
{
    public interface IPopupService
    {
        Task<object> ShowPopup(Type pageToShow, object data=null);
        object GetPopupData();
        void DismissPopup(object data);
    }

    public class PopupService : IPopupService
    {
        private readonly IServiceProvider _serviceProvider;
        private BasePopup _currentPopup;

        public PopupService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<object> ShowPopup(Type pageToShow, object data=null)
        {
            try
            {
                if (_currentPopup is not null)
                {
                    _currentPopup.Close(data);
                    _currentPopup = null;
                }
            }
            catch (ObjectDisposedException ex)
            {
                _currentPopup = null;
            }


            if (!pageToShow.IsSubclassOf(typeof(BasePopup)))
                throw new ArgumentException();
            
            var popup = (BasePopup)_serviceProvider.GetRequiredService(pageToShow);
            popup.Data = data;
            _currentPopup = popup;

            return await Shell.Current.CurrentPage.ShowPopupAsync(popup);
        }

        public object GetPopupData()
        {
            return _currentPopup?.Data;
        }

        public void DismissPopup(object data)
        {
            try
            {
                if (_currentPopup is not null)
                {
                    _currentPopup.Close(data);
                    _currentPopup = null;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
