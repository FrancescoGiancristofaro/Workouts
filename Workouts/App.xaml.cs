﻿using Services.Services;

namespace WorkoutsApp;
public partial class App : Application
{

    public App()
	{
		InitializeComponent();
        MainPage = new AppShell();
    }

    protected override async void OnStart()
    { 
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
    }


    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        ((IBaseViewModel)Shell.Current.CurrentPage.BindingContext).ManageException(e);
    }
}
