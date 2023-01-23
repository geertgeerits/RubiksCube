﻿namespace RubiksCube;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}

    // Window dimensions and location for desktop apps.
    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);

        const int newHeight = 900;
        const int newWidth = 450;

        window.X = 200;
        window.Y = 50;

        window.Height = newHeight;
        window.Width = newWidth;

        window.MinimumHeight = 720;
        window.MinimumWidth = 380;

        return window;
    }
}
