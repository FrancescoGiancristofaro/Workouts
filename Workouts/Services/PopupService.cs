using CommunityToolkit.Maui.Views;
using WorkoutsApp.Dtos;
using WorkoutsApp.Pages.Templates;

namespace WorkoutsApp.Services
{
    public interface IPopupService
    {
        Task DisplayAlert(string title, string message);
        Task<string> DisplayEditorPopup(string title,string text);
        Task<object> ShowPopup(Type pageToShow, object data=null);
        object GetPopupData();
        void DismissPopup(object data = null);
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

        public void DismissPopup(object data = null)
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

        public async Task DisplayAlert(string title, string message)
        {
            try
            {
                if (_currentPopup is not null)
                {
                    _currentPopup.Close(new InfoPopupDto()
                    {
                        Title = title,
                        Message = message
                    });
                    _currentPopup = null;
                }
            }
            catch (ObjectDisposedException ex)
            {
                _currentPopup = null;
            }
            var popup = (BasePopup)_serviceProvider.GetRequiredService(typeof(InfoPopup));
            popup.Data = new InfoPopupDto()
            {
                Title = title,
                Message = message
            };
            _currentPopup = popup;

            await Shell.Current.CurrentPage.ShowPopupAsync(popup);
        }

        public async Task<string> DisplayEditorPopup(string title, string text)
        {
            try
            {
                if (_currentPopup is not null)
                {
                    _currentPopup.Close(new EditorPopupDto()
                    {
                        Title = title,
                        Text = text
                    });
                    _currentPopup = null;
                }
            }
            catch (ObjectDisposedException ex)
            {
                _currentPopup = null;
            }
            var popup = (BasePopup)_serviceProvider.GetRequiredService(typeof(EditorPopup));
            popup.Data = new EditorPopupDto()
            {
                Title = title,
                Text = text
            };
            _currentPopup = popup;

            return await Shell.Current.CurrentPage.ShowPopupAsync(popup) as string;
        }
    }
}
