using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkoutsApp.Dtos;
using WorkoutsApp.Services;

namespace WorkoutsApp.Pages.Templates
{
    public partial class EditorPopupViewModel : BasePopupViewModel
    {
        [ObservableProperty] string _title;
        [ObservableProperty] string _text;
        [ObservableProperty] string _input;
        public EditorPopupViewModel(IPopupService popupService) : base(popupService)
        {
        }

        [RelayCommand]
        void EditNote()
        {
           _popupService.DismissPopup(Text);
            return;
        }

        public override void Opened()
        {

            var dto = _popupService.GetPopupData() as EditorPopupDto;
            Title = dto.Title;
            Text = dto.Text;
            Input = dto.Text;
        }

        public override void Dismissed()
        {
            _popupService.DismissPopup(Input);
            return;
        }
    }
}
