namespace RubiksCube;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		//MainPage = new AppShell();
        MainPage = new NavigationPage(new MainPage());
    }

    // Window dimensions and location for desktop apps
    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);

        const int newHeight = 1080;
        const int newWidth = 900;

        window.X = 300;
        window.Y = 10;

        window.Height = newHeight;
        window.Width = newWidth;

        window.MinimumHeight = 800;
        window.MinimumWidth = 800;

        return window;
    }
}
