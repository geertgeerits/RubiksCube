namespace RubiksCube
{
    public sealed partial class App : Application
    {
    	public App()
    	{
    		InitializeComponent();

    		//MainPage = new AppShell();
            MainPage = new NavigationPage(new MainPage());
        }

        /// <summary>
        /// Window dimensions and location for desktop apps
        /// </summary>
        /// <param name="activationState"></param>
        /// <returns></returns>
        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);

            const int newHeight = 970;
            const int newWidth = 850;

            window.X = 300;
            window.Y = 40;

            window.Height = newHeight;
            window.Width = newWidth;

            window.MinimumHeight = 850;
            window.MinimumWidth = newWidth;
            window.MaximumHeight = 1000;
            window.MaximumWidth = newWidth;

            return window;
        }
    }
}
