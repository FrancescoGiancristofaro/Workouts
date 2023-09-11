using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using WorkoutsApp.Dtos;
using WorkoutsApp.Services;

namespace WorkoutsApp.Pages.Templates
{
    public partial class InfoPopupViewModel : BasePopupViewModel
    {
        [ObservableProperty] string _title;
        [ObservableProperty] string _message;
        public InfoPopupViewModel(IPopupService popupService) : base(popupService)
        {
        }

        public override void Opened()
        {
            
            var data = _popupService.GetPopupData() as InfoPopupDto;
            Title = data.Title;
            Message = data.Message;

        }
    }
}
