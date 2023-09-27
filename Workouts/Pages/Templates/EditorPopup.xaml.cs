namespace WorkoutsApp.Pages.Templates;

public partial class EditorPopup 
{
	public EditorPopup(EditorPopupViewModel vm)
	{
		vm.Popup = this;
		BindingContext = vm;
		InitializeComponent();
	}
}