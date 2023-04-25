using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutsApp.Services;

namespace WorkoutsApp
{
    public partial class BasePopupViewModel : ObservableObject , IBaseViewModel
    {
        protected readonly IPopupService _popupService;

        [ObservableProperty]
        bool isBusy;

        public BasePopupViewModel(IPopupService popupService)
        {
            _popupService = popupService;
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
