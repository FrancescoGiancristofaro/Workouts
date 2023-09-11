namespace WorkoutsApp.Pages.Templates;

public partial class EditorPopup 
{
	public EditorPopup(EditorPopupViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}