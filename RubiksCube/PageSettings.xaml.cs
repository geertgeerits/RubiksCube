using RubiksCube.Resources.Languages;
using System.Diagnostics;
using System.Globalization;

namespace RubiksCube;

public partial class PageSettings : ContentPage
{
    // Local variables.
    private readonly string cButtonClose;
    private readonly string cErrorTitle;
    private readonly string cAllowedChar;
    private readonly string cAllowedCharNot;
    private readonly string cHexColorCodes;
    private readonly string cHexCharacters = "0123456789ABCDEFabcdef";
    private readonly Stopwatch stopWatch = new();

    public PageSettings()
	{
        try
        {
            InitializeComponent();
        }
        catch (Exception ex)
        {
            DisplayAlert("InitializeComponent: PageSettings", ex.Message, "OK");
            return;
        }

        // Put text in the chosen language in the controls and variables.
        lblTitle.Text = CubeLang.Settings_Text;

        lblExplanation.Text = CubeLang.SettingsSaved_Text;
        lblLanguage.Text = CubeLang.Language_Text;
        lblLanguageSpeech.Text = CubeLang.LanguageSpeech_Text;
        lblTheme.Text = CubeLang.Theme_Text;
        lblCubeColors.Text = CubeLang.CubeColors_Text;
        btnSettingsSave.Text = CubeLang.SettingsSave_Text;
        btnSettingsReset.Text = CubeLang.SettingsReset_Text;

        var ThemeList = new List<string>
        {
            CubeLang.ThemeSystem_Text,
            CubeLang.ThemeLight_Text,
            CubeLang.ThemeDark_Text
        };
        pckTheme.ItemsSource = ThemeList;

        cButtonClose = CubeLang.ButtonClose_Text;
        cErrorTitle = CubeLang.ErrorTitle_Text;
        cAllowedChar = CubeLang.AllowedChar_Text;
        cAllowedCharNot = CubeLang.AllowedCharNot_Text;
        cHexColorCodes = CubeLang.HexColorCodes_Text;

        // Set the current language in the picker.
        pckLanguage.SelectedIndex = MainPage.cLanguage switch
        {
            // German (Deutsch).
            "de" => 0,

            // Spanish (Español).
            "es" => 2,

            // French (Français).
            "fr" => 3,

            // Italian (Italiano).
            "it" => 4,

            // Dutch (Nederlands).
            "nl" => 5,

            // Portuguese (Português).
            "pt" => 6,

            // English.
            _ => 1,
        };

        // Fill the picker with the speech languages and set the saved language in the picker.
        FillPickerWithSpeechLanguages();

        // Set the current theme in the picker.
        pckTheme.SelectedIndex = MainPage.cTheme switch
        {
            // Light.
            "Light" => 1,

            // Dark.
            "Dark" => 2,

            // System.
            _ => 0,
        };

        // Set the explaination of text and speech to false or true.
        swtExplainText.IsToggled = MainPage.bExplainText;
        swtExplainSpeech.IsToggled = MainPage.bExplainSpeech;

        // Initialize the cube colors.
        plgCubeColor1.Fill = Color.FromArgb(MainPage.cCubeColor1);
        plgCubeColor2.Fill = Color.FromArgb(MainPage.cCubeColor2);
        plgCubeColor3.Fill = Color.FromArgb(MainPage.cCubeColor3);
        plgCubeColor4.Fill = Color.FromArgb(MainPage.cCubeColor4);
        plgCubeColor5.Fill = Color.FromArgb(MainPage.cCubeColor5);
        plgCubeColor6.Fill = Color.FromArgb(MainPage.cCubeColor6);

        // Workaround for !!!BUG!!! in iOS with the Slider right margin.
#if IOS
        Slider slider = new Slider
        {
            Margin = new Thickness(0, 0, 25, 0)
        };

        sldColorRed.Margin = slider.Margin;
        sldColorGreen.Margin = slider.Margin;
        sldColorBlue.Margin = slider.Margin;
#endif

        // // Set the first hex colorcode in the entry field and set the slider positions.
        SetCubeHexColor();

        // Start the stopWatch for resetting all the settings.
        stopWatch.Start();
    }

    // Picker language clicked event.
    private void OnPickerLanguageChanged(object sender, EventArgs e)
    {
        string cLanguageOld = MainPage.cLanguage;

        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            MainPage.cLanguage = selectedIndex switch
            {
                // German (Deutsch).
                0 => "de",

                // Spanish (Español).
                2 => "es",

                // French (Français).
                3 => "fr",

                // Italian (Italiano).
                4 => "it",

                // Dutch (Nederlands).
                5 => "nl",

                // Portuguese (Português).
                6 => "pt",

                // English.
                _ => "en",
            };
        }

        if (cLanguageOld != MainPage.cLanguage)
        {
            MainPage.bLanguageChanged = true;

            // Set the current UI culture of the selected language.
            MainPage.SetCultureSelectedLanguage();
        }
    }

    // Fill the picker with the speech languages from the array.
    // .Country = KR ; .Id = ''  ; .Language = ko ; .Name = Korean (South Korea) ; 
    private void FillPickerWithSpeechLanguages()
    {
        // If there are no locales then return.
        bool bIsSetSelectedIndex = false;

        if (!MainPage.bLanguageLocalesExist)
        {
            pckLanguageSpeech.IsEnabled = false;
            return;
        }

        // Put the sorted locales from the array in the picker and select the saved language.
        int nTotalItems = MainPage.cLanguageLocales.Length;

        for (int nItem = 0; nItem < nTotalItems; nItem++)
        {
            pckLanguageSpeech.Items.Add(MainPage.cLanguageLocales[nItem]);

            if (MainPage.cLanguageSpeech == MainPage.cLanguageLocales[nItem])
            {
                pckLanguageSpeech.SelectedIndex = nItem;
                bIsSetSelectedIndex = true;
            }
        }

        // If the language is not found set the picker to the first item.
        if (!bIsSetSelectedIndex)
        {
            pckLanguageSpeech.SelectedIndex = 0;
        }
    }

    // Picker speech language clicked event.
    private void OnPickerLanguageSpeechChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            MainPage.cLanguageSpeech = picker.Items[selectedIndex];
        }
    }

    // Picker theme clicked event.
    private void OnPickerThemeChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            MainPage.cTheme = selectedIndex switch
            {
                // Light.
                1 => "Light",

                // Dark.
                2 => "Dark",

                // System.
                _ => "System",
            };
        }
    }

    // Switch explain text toggled.
    private void swtExplainText_Toggled(object sender, ToggledEventArgs e)
    {
        MainPage.bExplainText = swtExplainText.IsToggled;
    }

    // Switch explain speech toggled.
    private void swtExplainSpeech_Toggled(object sender, ToggledEventArgs e)
    {
        MainPage.bExplainSpeech = swtExplainSpeech.IsToggled;
    }

    // On entry HexColor text changed event.
    private void EntryHexColorTextChanged(object sender, EventArgs e)
    {
        var entry = (Entry)sender;

        if (TestAllowedCharacters(cHexCharacters, entry.Text) == false)
        {
            entry.Focus();
        }
    }

    // Radiobutton checked changed event.
    private void rbnCubeColorCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        SetCubeHexColor();
    }

    // Set the hex colorcode in the entry field and set the slider positions.
    private void SetCubeHexColor()
    {
        int nRed = 0;
        int nGreen = 0;
        int nBlue = 0;

        if (rbnCubeColor1.IsChecked)
        {
            entHexColor.Text = MainPage.cCubeColor1;
            HexToRgbColor(MainPage.cCubeColor1, ref nRed, ref nGreen, ref nBlue);
        }
        else if (rbnCubeColor2.IsChecked)
        {
            entHexColor.Text = MainPage.cCubeColor2;
            HexToRgbColor(MainPage.cCubeColor2, ref nRed, ref nGreen, ref nBlue);
        }
        else if (rbnCubeColor3.IsChecked)
        {
            entHexColor.Text = MainPage.cCubeColor3;
            HexToRgbColor(MainPage.cCubeColor3, ref nRed, ref nGreen, ref nBlue);
        }
        else if (rbnCubeColor4.IsChecked)
        {
            entHexColor.Text = MainPage.cCubeColor4;
            HexToRgbColor(MainPage.cCubeColor4, ref nRed, ref nGreen, ref nBlue);
        }
        else if (rbnCubeColor5.IsChecked)
        {
            entHexColor.Text = MainPage.cCubeColor5;
            HexToRgbColor(MainPage.cCubeColor5, ref nRed, ref nGreen, ref nBlue);
        }
        else if (rbnCubeColor6.IsChecked)
        {
            entHexColor.Text = MainPage.cCubeColor6;
            HexToRgbColor(MainPage.cCubeColor6, ref nRed, ref nGreen, ref nBlue);
        }

        sldColorRed.Value = nRed;
        sldColorGreen.Value = nGreen;
        sldColorBlue.Value = nBlue;
    }

    // Display help for Hex color.
    private async void OnSettingsHexColorClicked(object sender, EventArgs e)
    {
        await DisplayAlert("?", cHexColorCodes, cButtonClose);
    }

    // Entry HexColor Unfocused event.
    private void EntryHexColorUnfocused(object sender, EventArgs e)
    {
        var entry = (Entry)sender;

        //Test for allowed characters.
        if (TestAllowedCharacters(cHexCharacters, entry.Text) == false)
        {
            entry.Focus();
            return;
        }

        // Length must be 6 characters.
        if (entry.Text.Length != 6)
        {
            entry.Focus();
            return;
        }

        // Set the sliders position.
        int nRed = 0;
        int nGreen = 0;
        int nBlue = 0;

        if (entry == entHexColor)
        {
            if (rbnCubeColor1.IsChecked)
            {
                MainPage.cCubeColor1 = entHexColor.Text;
                HexToRgbColor(MainPage.cCubeColor1, ref nRed, ref nGreen, ref nBlue);
            }
            else if (rbnCubeColor2.IsChecked)
            {
                MainPage.cCubeColor2 = entHexColor.Text;
                HexToRgbColor(MainPage.cCubeColor2, ref nRed, ref nGreen, ref nBlue);
            }
            else if (rbnCubeColor3.IsChecked)
            {
                MainPage.cCubeColor3 = entHexColor.Text;
                HexToRgbColor(MainPage.cCubeColor3, ref nRed, ref nGreen, ref nBlue);
            }
            else if (rbnCubeColor4.IsChecked)
            {
                MainPage.cCubeColor4 = entHexColor.Text;
                HexToRgbColor(MainPage.cCubeColor4, ref nRed, ref nGreen, ref nBlue);
            }
            else if (rbnCubeColor5.IsChecked)
            {
                MainPage.cCubeColor5 = entHexColor.Text;
                HexToRgbColor(MainPage.cCubeColor5, ref nRed, ref nGreen, ref nBlue);
            }
            else if (rbnCubeColor6.IsChecked)
            {
                MainPage.cCubeColor6 = entHexColor.Text;
                HexToRgbColor(MainPage.cCubeColor6, ref nRed, ref nGreen, ref nBlue);
            }

            sldColorRed.Value = nRed;
            sldColorGreen.Value = nGreen;
            sldColorBlue.Value = nBlue;
        }

        // Set focus to the next or save button.
        if (sender.Equals(entHexColor))
        {
            //entHexColorBg.Focus();
        }
        else
        {
            // Hide the keyboard.
            entry.IsEnabled = false;
            entry.IsEnabled = true;

            _ = btnSettingsSave.Focus();
        }
    }

    // Test for allowed characters.
    private bool TestAllowedCharacters(string cAllowedCharacters, string cTextToCode)
    {
        foreach (char cChar in cTextToCode)
        {
            bool bResult = cAllowedCharacters.Contains(cChar);

            if (bResult == false)
            {
                DisplayAlert(cErrorTitle, cAllowedChar + "\n" + cAllowedCharacters + "\n\n" + cAllowedCharNot + " " + cChar, cButtonClose);
                return false;
            }
        }

        return true;
    }

    // Slider color cube value change.
    private void OnSliderColorValueChanged(object sender, ValueChangedEventArgs args)
    {
        int nColorRed = 0;
        int nColorGreen = 0;
        int nColorBlue = 0;

        var slider = (Slider)sender;

        if (slider == sldColorRed)
        {
            nColorRed = (int)args.NewValue;
            nColorGreen = (int)sldColorGreen.Value;
            nColorBlue = (int)sldColorBlue.Value;
        }
        else if (slider == sldColorGreen)
        {
            nColorRed = (int)sldColorRed.Value;
            nColorGreen = (int)args.NewValue;
            nColorBlue = (int)sldColorBlue.Value;
        }
        else if (slider == sldColorBlue)
        {
            nColorRed = (int)sldColorRed.Value;
            nColorGreen = (int)sldColorGreen.Value;
            nColorBlue = (int)args.NewValue;
        }

        string cColorFgHex = nColorRed.ToString("X2") + nColorGreen.ToString("X2") + nColorBlue.ToString("X2");
        entHexColor.Text = cColorFgHex;
        
        if (rbnCubeColor1.IsChecked)
        {
            plgCubeColor1.Fill = Color.FromArgb(cColorFgHex);
            MainPage.cCubeColor1 = cColorFgHex;
        }
        else if(rbnCubeColor2.IsChecked)
        {
            plgCubeColor2.Fill = Color.FromArgb(cColorFgHex);
            MainPage.cCubeColor2 = cColorFgHex;
        }
        else if (rbnCubeColor3.IsChecked)
        {
            plgCubeColor3.Fill = Color.FromArgb(cColorFgHex);
            MainPage.cCubeColor3 = cColorFgHex;
        }
        else if (rbnCubeColor4.IsChecked)
        {
            plgCubeColor4.Fill = Color.FromArgb(cColorFgHex);
            MainPage.cCubeColor4 = cColorFgHex;
        }
        else if (rbnCubeColor5.IsChecked)
        {
            plgCubeColor5.Fill = Color.FromArgb(cColorFgHex);
            MainPage.cCubeColor5 = cColorFgHex;
        }
        else if (rbnCubeColor6.IsChecked)
        {
            plgCubeColor6.Fill = Color.FromArgb(cColorFgHex);
            MainPage.cCubeColor6 = cColorFgHex;
        }
    }

    // Convert RRGGBB Hex color to RGB color.
    private static void HexToRgbColor(string cHexColor, ref int nRed, ref int nGreen, ref int nBlue)
    {
        // Remove leading # if present.
        if (cHexColor[..1] == "#")
        {
            cHexColor = cHexColor[1..];
        }

        nRed = int.Parse(cHexColor[..2], NumberStyles.AllowHexSpecifier);
        nGreen = int.Parse(cHexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier);
        nBlue = int.Parse(cHexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier);
    }

    // Button save settings clicked event.
    private static void OnSettingsSaveClicked(object sender, EventArgs e)
    {
        Preferences.Default.Set("SettingTheme", MainPage.cTheme);
        Preferences.Default.Set("SettingLanguage", MainPage.cLanguage);
        Preferences.Default.Set("SettingLanguageSpeech", MainPage.cLanguageSpeech);
        Preferences.Default.Set("SettingExplainText", MainPage.bExplainText);
        Preferences.Default.Set("SettingExplainSpeech", MainPage.bExplainSpeech);
        Preferences.Default.Set("SettingCubeColor1", MainPage.cCubeColor1);
        Preferences.Default.Set("SettingCubeColor2", MainPage.cCubeColor2);
        Preferences.Default.Set("SettingCubeColor3", MainPage.cCubeColor3);
        Preferences.Default.Set("SettingCubeColor4", MainPage.cCubeColor4);
        Preferences.Default.Set("SettingCubeColor5", MainPage.cCubeColor5);
        Preferences.Default.Set("SettingCubeColor6", MainPage.cCubeColor6);

        // Wait 500 milliseconds otherwise the settings are not saved in Android.
        Task.Delay(500).Wait();

        // Restart the application.
        //Application.Current.MainPage = new AppShell();
        Application.Current.MainPage = new NavigationPage(new MainPage());
    }

    // Button reset settings clicked event.
    private void OnSettingsResetClicked(object sender, EventArgs e)
    {
        // Get the elapsed time in milli seconds.
        stopWatch.Stop();

        if (stopWatch.ElapsedMilliseconds < 2001)
        {
            // Clear all settings after the first clicked event within the first 2 seconds after opening the setting page.
            Preferences.Default.Clear();
        }
        else
        {
            // Reset some settings.
            Preferences.Default.Remove("SettingTheme");
            Preferences.Default.Remove("SettingLanguage");
            Preferences.Default.Remove("SettingLanguageSpeech");
            Preferences.Default.Remove("SettingExplainText");
            Preferences.Default.Remove("SettingExplainSpeech");
            Preferences.Default.Remove("SettingCubeColor1");
            Preferences.Default.Remove("SettingCubeColor2");
            Preferences.Default.Remove("SettingCubeColor3");
            Preferences.Default.Remove("SettingCubeColor4");
            Preferences.Default.Remove("SettingCubeColor5");
            Preferences.Default.Remove("SettingCubeColor6");
        }

        // Wait 500 milliseconds otherwise the settings are not saved in Android.
        Task.Delay(500).Wait();

        // Restart the application.
        //Application.Current.MainPage = new AppShell();
        Application.Current.MainPage = new NavigationPage(new MainPage());
    }
}