using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using WorkoutsApp.Dtos;

namespace WorkoutsApp.Pages.Templates
{
   
    public partial class InfoPopupViewModel : BasePopupViewModel
    {
        [ObservableProperty] string _title;
        [ObservableProperty] string _message;
        public InfoPopupViewModel()
        {
        }

        public override void Opened()
        {
            var data = this.Popup.Data as DisplayAlertDto;
            Title = data.Title;
            Message = data.Message;

        }
    }
}
