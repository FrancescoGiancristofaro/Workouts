using WorkoutsApp.Dtos;

namespace WorkoutsApp.Pages.Templates;

public partial class InfoPopup
{
	public InfoPopup(InfoPopupViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}