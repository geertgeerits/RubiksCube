namespace RubiksCube;

public partial class PageAbout : ContentPage
{
    public PageAbout()
	{
        try
        {
            InitializeComponent();
        }
        catch (Exception ex)
        {
            DisplayAlert("InitializeComponent: PageAbout", ex.Message, "OK");
            return;
        }

        // Put text in the chosen language in the controls and variables
        lblVersion.Text = $"{CubeLang.Version_Text} 2.0.11";
        lblCopyright.Text = $"{CubeLang.Copyright_Text} © 1981-2024 Geert Geerits";
        lblEmail.Text = $"{CubeLang.Email_Text} geertgeerits@gmail.com";
        lblWebsite.Text = $"{CubeLang.Website_Text}: ../rubikscube";
        lblPrivacyPolicy.Text = $"\n{CubeLang.PrivacyPolicyTitle_Text} {CubeLang.PrivacyPolicy_Text}";
        lblLicense.Text = $"\n{CubeLang.LicenseTitle_Text}: {CubeLang.License_Text}";
        lblHelp.Text = $"\n{CubeLang.HelpCube_Text}";
        lblExplanation.Text = $"\n{CubeLang.InfoExplanation_Text}";
    }

    // Open e-mail program
    private async void OnBtnEmailLinkClicked(object sender, EventArgs e)
    {
        if (Email.Default.IsComposeSupported)
        {
            string subject = "Rubik's Cube";
            string body = "";
            string[] recipients = ["geertgeerits@gmail.com"];

            var message = new EmailMessage
            {
                Subject = subject,
                Body = body,
                BodyFormat = EmailBodyFormat.PlainText,
                To = new List<string>(recipients)
            };

            try
            {
                await Email.Default.ComposeAsync(message);
            }
            catch (Exception ex)
            {
                await DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
            }
        }
    }

    // Open the page 'PageWebsite' to open the website in the WebView control
    // !!!BUG!!! in Android: the WebView control gives an error when opening a link to the Google Play Console
    private async void OnBtnWebsiteLinkClicked(object sender, EventArgs e)
    {
#if ANDROID
        try
        {
            Uri uri = new("https://geertgeerits.wixsite.com/geertgeerits/rubiks-cube");
            BrowserLaunchOptions options = new()
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show
            };

            await Browser.Default.OpenAsync(uri, options);
        }
        catch (Exception ex)
        {
            await DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
        }
#else
        await Navigation.PushAsync(new PageWebsite());
#endif
    }
}