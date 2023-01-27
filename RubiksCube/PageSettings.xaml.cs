using RubiksCube.Resources.Languages;
using System.Diagnostics;
using System.Globalization;

namespace RubiksCube;

public partial class PageSettings : ContentPage
{
    // Local variables.
    private readonly string cButtonClose;
    private readonly string cErrorTitle;
    private readonly string cHexColorCodes;
    private readonly Stopwatch stopWatch = new();

    public PageSettings()
	{
		InitializeComponent();

        // Put text in the chosen language in the controls and variables.
        //lblTitle.Text = CubeLang.Settings_Text;

        //lblExplanation.Text = CubeLang.SettingsSaved_Text;
        //lblLanguage.Text = CubeLang.Language_Text;
        //lblLanguageSpeech.Text = CubeLang.LanguageSpeech_Text;
        //lblTheme.Text = CubeLang.Theme_Text;
        //lblForgroundColor.Text = CubeLang.ForgroundColor_Text;
        //btnSettingsSave.Text = CubeLang.SettingsSave_Text;
        //btnSettingsReset.Text = CubeLang.SettingsReset_Text;

        var ThemeList = new List<string>
        {
            //CubeLang.ThemeSystem_Text,
            //CubeLang.ThemeLight_Text,
            //CubeLang.ThemeDark_Text
        };
        pckTheme.ItemsSource = ThemeList;

        //cButtonClose = CubeLang.ButtonClose_Text;
        //cErrorTitle = CubeLang.ErrorTitle_Text;
        //cHexColorCodes = CubeLang.HexColorCodes_Text;

        // Set the current language in the picker.
        pckLanguage.SelectedIndex = MainPage.cLanguage switch
        {
            // German (Deutsch).
            "de" => 0,

            // Spanish (Espa�ol).
            "es" => 2,

            // French (Fran�ais).
            "fr" => 3,

            // Italian (Italiano).
            "it" => 4,

            // Dutch (Nederlands).
            "nl" => 5,

            // Portuguese (Portugu�s).
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

        // Workaround for !!!BUG!!! in iOS with the Slider right margin.
#if IOS
        Slider slider = new Slider
        {
            Margin = new Thickness(0, 0, 25, 0)
        };

        //sldOpacityFg.Margin = slider.Margin;
        sldColorFgRed.Margin = slider.Margin;
        sldColorFgGreen.Margin = slider.Margin;
        sldColorFgBlue.Margin = slider.Margin;
#endif

        // Set the current color in the entry and on the sliders.
        int nOpacity = 0;
        int nRed = 0;
        int nGreen = 0;
        int nBlue = 0;

        //entHexColorFg.Text = MainPage.cCodeColorFg;

        //HexToRgbColor(MainPage.cCodeColorFg, ref nOpacity, ref nRed, ref nGreen, ref nBlue);

        //sldOpacityFg.Value = nOpacity;
        sldColorFgRed.Value = nRed;
        sldColorFgGreen.Value = nGreen;
        sldColorFgBlue.Value = nBlue;

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

                // Spanish (Espa�ol).
                2 => "es",

                // French (Fran�ais).
                3 => "fr",

                // Italian (Italiano).
                4 => "it",

                // Dutch (Nederlands).
                5 => "nl",

                // Portuguese (Portugu�s).
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

    // On entry HexColor text changed event.
    private void EntryHexColorTextChanged(object sender, EventArgs e)
    {
        var entry = (Entry)sender;

        string cTextToCode = entry.Text;

        if (TestAllowedCharacters("0123456789ABCDEFabcdef", cTextToCode) == false)
        {
            entry.Focus();
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
                //DisplayAlert(cErrorTitle, cAllowedChar + "\n" + cAllowedCharacters + "\n\n" + cAllowedCharNot + " " + cChar, cButtonClose);
                return false;
            }
        }

        return true;
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

        // Add the opacity if length = 6 characters.
        if (entry.Text.Length == 6)
        {
            entry.Text = "FF" + entry.Text;
        }

        // Length must be 8 characters.
        if (entry.Text.Length != 8)
        {
            entry.Focus();
            return;
        }

        // Set the sliders position.
        int nOpacity = 0;
        int nRed = 0;
        int nGreen = 0;
        int nBlue = 0;

        if (entry == entHexColorFg)
        {
            //MainPage.cCodeColorFg = entHexColorFg.Text;

            //HexToRgbColor(MainPage.cCodeColorFg, ref nOpacity, ref nRed, ref nGreen, ref nBlue);

            //sldOpacityFg.Value = nOpacity;
            sldColorFgRed.Value = nRed;
            sldColorFgGreen.Value = nGreen;
            sldColorFgBlue.Value = nBlue;
        }

        // Set focus to the next or save button.
        if (sender.Equals(entHexColorFg))
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

    // Slider color barcode forground value change.
    private void OnSliderColorForgroundValueChanged(object sender, ValueChangedEventArgs args)
    {
        int nAmountOpacity = 0;
        int nColorRed = 0;
        int nColorGreen = 0;
        int nColorBlue = 0;

        var slider = (Slider)sender;

        //if (slider == sldOpacityFg)
        //{
        //    nAmountOpacity = (int)args.NewValue;
        //    nColorRed = (int)sldColorFgRed.Value;
        //    nColorGreen = (int)sldColorFgGreen.Value;
        //    nColorBlue = (int)sldColorFgBlue.Value;
        //}
        //else if (slider == sldColorFgRed)
        //{
        //    nAmountOpacity = (int)sldOpacityFg.Value;
        //    nColorRed = (int)args.NewValue;
        //    nColorGreen = (int)sldColorFgGreen.Value;
        //    nColorBlue = (int)sldColorFgBlue.Value;
        //}
        //else if (slider == sldColorFgGreen)
        //{
        //    nAmountOpacity = (int)sldOpacityFg.Value;
        //    nColorRed = (int)sldColorFgRed.Value;
        //    nColorGreen = (int)args.NewValue;
        //    nColorBlue = (int)sldColorFgBlue.Value;
        //}
        //else if (slider == sldColorFgBlue)
        //{
        //    nAmountOpacity = (int)sldOpacityFg.Value;
        //    nColorRed = (int)sldColorFgRed.Value;
        //    nColorGreen = (int)sldColorFgGreen.Value;
        //    nColorBlue = (int)args.NewValue;
        //}

        //string cColorFgHex = nAmountOpacity.ToString("X2") + nColorRed.ToString("X2") + nColorGreen.ToString("X2") + nColorBlue.ToString("X2");
        //entHexColorFg.Text = cColorFgHex;
        //bxvColorFg.Color = Color.FromArgb(cColorFgHex);

        //MainPage.cCodeColorFg = cColorFgHex;
    }

    // Convert OORRGGBB Hex color to RGB color.
    private static void HexToRgbColor(string cHexColor, ref int nOpacity, ref int nRed, ref int nGreen, ref int nBlue)
    {
        // Remove leading # if present.
        if (cHexColor[..1] == "#")
        {
            cHexColor = cHexColor[1..];
        }

        nOpacity = int.Parse(cHexColor[..2], NumberStyles.AllowHexSpecifier);
        nRed = int.Parse(cHexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier);
        nGreen = int.Parse(cHexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier);
        nBlue = int.Parse(cHexColor.Substring(6, 2), NumberStyles.AllowHexSpecifier);
    }

    // Button save settings clicked event.
    private static void OnSettingsSaveClicked(object sender, EventArgs e)
    {
        Preferences.Default.Set("SettingTheme", MainPage.cTheme);
        Preferences.Default.Set("SettingLanguage", MainPage.cLanguage);
        Preferences.Default.Set("SettingLanguageSpeech", MainPage.cLanguageSpeech);

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
            Preferences.Default.Remove("SettingFormatGeneratorIndex");
            Preferences.Default.Remove("SettingFormatScannerIndex");
            Preferences.Default.Remove("SettingCodeColorFg");
            Preferences.Default.Remove("SettingCodeColorBg");
            Preferences.Default.Remove("SettingLanguage");
            Preferences.Default.Remove("SettingLanguageSpeech");
        }

        // Wait 500 milliseconds otherwise the settings are not saved in Android.
        Task.Delay(500).Wait();

        // Restart the application.
        //Application.Current.MainPage = new AppShell();
        Application.Current.MainPage = new NavigationPage(new MainPage());
    }
}