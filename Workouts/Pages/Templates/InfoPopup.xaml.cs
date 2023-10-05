using WorkoutsApp.Dtos;

namespace WorkoutsApp.Pages.Templates;

public partial class InfoPopup
{
	public InfoPopup(InfoPopupViewModel vm)
	{
		vm.Popup = this;
		BindingContext = vm;
		InitializeComponent();
	}
}