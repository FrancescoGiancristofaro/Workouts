namespace WorkoutsApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
        InitializeComponent();
	}

    private bool _isNavigationActive = false;
    protected override bool OnBackButtonPressed()
    {
        if (_isNavigationActive)
        {
            return true;
        }
        if (Shell.Current.CurrentPage.BindingContext is BaseViewModel bvm)
        {
            bvm.NavigateBack();
            return true;
        }
        if (Shell.Current.CurrentPage.BindingContext is BasePopupViewModel bpvm)
        {
            return true;
        }
        throw new Exception("Evento non gestito");
    }

    protected override void OnNavigated(ShellNavigatedEventArgs e)
    {
        try
        {
            if (Shell.Current.CurrentPage.BindingContext is BaseViewModel bvm)
            {
                if (e.Source is ShellNavigationSource.Push)
                {
                    bvm.PrepareModel();
                    return;
                }

                if (e.Source is ShellNavigationSource.Pop or ShellNavigationSource.PopToRoot)
                {
                    bvm.ReversePrepareModel();
                    return;
                }

                if (e.Source is ShellNavigationSource.ShellItemChanged or ShellNavigationSource.ShellSectionChanged)
                {
                    bvm.OnAppearing();
                    return;
                }

                throw new Exception("Evento non gestito");
            }

            if (Shell.Current.CurrentPage.BindingContext is BasePopupViewModel bpvm)
            {
                return;

            }

            throw new Exception("La pagina che si sta tentando di pushare non è di una classe base");
        }
        finally
        {
            _isNavigationActive = false;
        }
    }

    protected override void OnNavigating(ShellNavigatingEventArgs  args)
    {
        _isNavigationActive = true;
        Console.WriteLine("Navigating");
    }
}
