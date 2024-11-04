namespace RubiksCube
{
    public sealed partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Window dimensions and location for desktop apps
        /// </summary>
        /// <param name="activationState"></param>
        /// <returns></returns>
        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Microsoft.Maui.Controls.Window(new NavigationPage(new MainPage()))
            {
                X = 300,
                Y = 40,
                Height = 1000,
                Width = 850,
                MinimumHeight = 700,
                MinimumWidth = 850,
                MaximumHeight = 1000,
                MaximumWidth = 850
            };

            return window;
        }
    }
}
