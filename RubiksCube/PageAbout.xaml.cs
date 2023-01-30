using RubiksCube.Resources.Languages;

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

        // Put text in the chosen language in the controls and variables.
        lblVersion.Text = CubeLang.Version_Text + " 2.0.10";
        lblCopyright.Text = CubeLang.Copyright_Text + " � 1981-2023 Geert Geerits";
        lblEmail.Text = CubeLang.Email_Text + " geertgeerits@gmail.com";
        lblWebsite.Text = CubeLang.Website_Text + " ../rubikscube";
        lblPrivacyPolicy.Text = CubeLang.PrivacyPolicyTitle_Text + " " + CubeLang.PrivacyPolicy_Text;
        lblLicense.Text = CubeLang.LicenseTitle_Text + ": " + CubeLang.License_Text;
    }

    // Open e-mail program.
    private async void OnBtnEmailLinkClicked(object sender, EventArgs e)
    {
#if IOS || MACCATALYST
        string cAddress = "geertgeerits@gmail.com";

        try
        {
            await Launcher.OpenAsync(new Uri($"mailto:{cAddress}"));
        }
        catch (Exception ex)
        {
            await DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
        }
#else
        if (Email.Default.IsComposeSupported)
        {
            string subject = "Rubik's Cube";
            string body = "";
            string[] recipients = new[] { "geertgeerits@gmail.com" };

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
#endif
    }

    // Open website in default browser.
    private async void OnBtnWebsiteLinkClicked(object sender, EventArgs e)
    {
        try
        {
            Uri uri = new Uri("https://geertgeerits.wixsite.com/rubikscube");
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
            await DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
        }
    }
}