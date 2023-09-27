using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkoutsApp.Dtos;

namespace WorkoutsApp.Pages.Templates
{
    public partial class EditorPopupViewModel : BasePopupViewModel
    {
        [ObservableProperty] string _title;
        [ObservableProperty] string _text;
        public EditorPopupViewModel()
        {
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        async Task EditNote()
        {
            await Popup.CloseAsync(Text);
        }

        public override void Opened()
        {
            Title = this.Popup.Data as string;
        }

    }
}
