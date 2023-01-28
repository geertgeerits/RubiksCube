// Program .....: RubiksCube.sln
// Author ......: Geert Geerits - E-mail: geertgeerits@gmail.com
// Copyright ...: (C) 1981-2023
// Version .....: 2.0.10
// Date ........: 2023-01-28 (YYYY-MM-DD)
// Language ....: Microsoft Visual Studio 2022: .NET MAUI C# 11.0
// Description .: Solving the Rubik's Cube
// Note ........: This program is based on a program I wrote in 1981 in MS Basic-80 for a Commodore PET 2001.
//                The solution for solving the cube is based on a book by Don Taylor, Mastering Rubik's Cube, Dutch version 1981.
// Dependencies : 
// Thanks to ...: 

using Microsoft.Maui.Controls.Shapes;
using RubiksCube.Resources.Languages;
using System.ComponentModel;
using System.Globalization;

namespace RubiksCube;

public partial class MainPage : ContentPage
{
    // Global variables for all pages part of Barcode Generator.
    public static string cTheme;
    public static string cLanguage;
    public static bool bLanguageChanged = false;
    public static string cLanguageSpeech;
    public static string[] cLanguageLocales;
    public static bool bLanguageLocalesExist = false;
    public static string cImageTextToSpeech = "speaker_64p_blue_green.png";
    public static string cImageTextToSpeechCancel = "speaker_cancel_64p_blue_red.png";
    public static bool bExplainText = false;
    public static bool bExplainSpeech = false;
    public static string cCodeColor1;

    // Local variables.
    private string cButtonClose;
    private string cErrorTitle;
    private string cLicenseTitle;
    private string cLicense;
    private string cAgree;
    private string cDisagree;
    private readonly bool bLicense;
    private string cCloseApplication;
    private string cTextToSpeechError;
    private IEnumerable<Locale> locales;
    private CancellationTokenSource cts;
    private bool bTextToSpeechIsBusy = false;

    // Variables for colors.
    private readonly string cColor1 = "FF0000";                 // Red
    private readonly string cColor2 = "FFFF00";                 // Yellow
    private readonly string cColor3 = "0000FF";                 // Blue
    private readonly string cColor4 = "008000";                 // Green
    private readonly string cColor5 = "FFFFFF";                 // White
    private readonly string cColor6 = "FF8000";                 // Orange
    private readonly string cColorArrowNotActive = "E2E2E2";    // Lightgray
    private readonly string cColorArrowActive = "FFD000";       // Light orange

    //private readonly int[] aTop = new int[9];
    //private readonly int[] aFront = new int[9];
    //private readonly int[] aRight = new int[9];
    //private readonly int[] aLeft = new int[9];
    //private readonly int[] aBack = new int[9];
    //private readonly int[] aBottom = new int[9];

    public MainPage()
	{
        try
        {
            InitializeComponent();
        }
        catch (Exception ex)
        {
            DisplayAlert("InitializeComponent: MainPage", ex.Message, "OK");
            return;
        }

        // Get the saved settings.
        cTheme = Preferences.Default.Get("SettingTheme", "System");
        cLanguage = Preferences.Default.Get("SettingLanguage", "");
        cLanguageSpeech = Preferences.Default.Get("SettingLanguageSpeech", "");
        bLicense = Preferences.Default.Get("SettingLicense", false);
        bExplainText = Preferences.Default.Get("SettingExplainText", true);
        bExplainSpeech = Preferences.Default.Get("SettingExplainSpeech", false);
        cCodeColor1 = Preferences.Default.Get("SettingCodeColor1", "000000");

        // Set the theme.
        if (cTheme == "Light")
        {
            Application.Current.UserAppTheme = AppTheme.Light;
        }
        else if (cTheme == "Dark")
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
        }
        else
        {
            Application.Current.UserAppTheme = AppTheme.Unspecified;
        }

        // Get and set the system OS user language.
        try
        {
            if (cLanguage == "")
            {
                cLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            }
        }
        catch (Exception)
        {
            cLanguage = "en";
        }

        SetTextLanguage();

        // Initialize text to speech and get and set the speech language.
        string cCultureName = "";

        try
        {
            if (cLanguageSpeech == "")
            {
                cCultureName = Thread.CurrentThread.CurrentCulture.Name;
            }
        }
        catch (Exception)
        {
            cCultureName = "en-US";
        }
        //DisplayAlert("cCultureName", "*" + cCultureName + "*", "OK");  // For testing.

        InitializeTextToSpeech(cCultureName);

        // Initialize the colors.
        PlgColor1.Fill = Color.FromArgb(cColor1);
        PlgColor2.Fill = Color.FromArgb(cColor2);
        PlgColor3.Fill = Color.FromArgb(cColor3);
        PlgColor4.Fill = Color.FromArgb(cColor4);
        PlgColor5.Fill = Color.FromArgb(cColor5);
        PlgColor6.Fill = Color.FromArgb(cColor6);

        // Reset the colors of the cube.
        ResetCube();
    }

    // TitleView buttons clicked events.
    private async void OnPageAboutClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageAbout());
    }

    private async void OnPageSettingsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageSettings());
    }

    // Select a color for dropping on a cube and put it in a tempory rectangle.
    private void OnDragStarting(object sender, DragStartingEventArgs e)
    {
        Polygon polygon = (sender as Element).Parent as Polygon;
        PlgColorSelect.Fill = polygon.Fill;        
    }

    // Drop the selected color on the cube and fill the cube with the color of the tempory rectangle.
    private void OnDrop(object sender, DropEventArgs e)
    {
        Polygon polygon = (sender as Element).Parent as Polygon;
        polygon.Fill = PlgColorSelect.Fill;

        PlgColorSelect.Fill = Color.FromArgb("000000");
    }
    
    // Solve the cube.
    private void OnBtnSolveClicked(object sender, EventArgs e)
    {
        if (!CheckNumberColorsCube())
        {
            return;
        }

        if (!CheckIfCubeIsSolved(false))
        {
            return;
        }
    }

    // Check the number of colors of the cube.
    private bool CheckNumberColorsCube()
    {
        int nNumberOfColors1 = 0;
        int nNumberOfColors2 = 0;
        int nNumberOfColors3 = 0;
        int nNumberOfColors4 = 0;
        int nNumberOfColors5 = 0;
        int nNumberOfColors6 = 0;

        if (PlgTop1.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgTop2.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgTop3.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgTop4.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgTop5.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgTop6.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgTop7.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgTop8.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgTop9.Fill == PlgColor1.Fill)
            nNumberOfColors1++;

        if (PlgTop1.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgTop2.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgTop3.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgTop4.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgTop5.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgTop6.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgTop7.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgTop8.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgTop9.Fill == PlgColor2.Fill)
            nNumberOfColors2++;

        if (PlgTop1.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgTop2.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgTop3.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgTop4.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgTop5.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgTop6.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgTop7.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgTop8.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgTop9.Fill == PlgColor3.Fill)
            nNumberOfColors3++;

        if (PlgTop1.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgTop2.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgTop3.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgTop4.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgTop5.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgTop6.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgTop7.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgTop8.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgTop9.Fill == PlgColor4.Fill)
            nNumberOfColors4++;

        if (PlgTop1.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgTop2.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgTop3.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgTop4.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgTop5.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgTop6.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgTop7.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgTop8.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgTop9.Fill == PlgColor5.Fill)
            nNumberOfColors5++;

        if (PlgTop1.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgTop2.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgTop3.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgTop4.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgTop5.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgTop6.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgTop7.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgTop8.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgTop9.Fill == PlgColor6.Fill)
            nNumberOfColors6++;

        if (PlgFront1.Fill == PlgColor1.Fill) 
            nNumberOfColors1++;
        if (PlgFront2.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgFront3.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgFront4.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgFront5.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgFront6.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgFront7.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgFront8.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgFront9.Fill == PlgColor1.Fill)
            nNumberOfColors1++;

        if (PlgFront1.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgFront2.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgFront3.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgFront4.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgFront5.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgFront6.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgFront7.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgFront8.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgFront9.Fill == PlgColor2.Fill)
            nNumberOfColors2++;

        if (PlgFront1.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgFront2.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgFront3.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgFront4.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgFront5.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgFront6.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgFront7.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgFront8.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgFront9.Fill == PlgColor3.Fill)
            nNumberOfColors3++;

        if (PlgFront1.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgFront2.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgFront3.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgFront4.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgFront5.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgFront6.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgFront7.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgFront8.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgFront9.Fill == PlgColor4.Fill)
            nNumberOfColors4++;

        if (PlgFront1.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgFront2.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgFront3.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgFront4.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgFront5.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgFront6.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgFront7.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgFront8.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgFront9.Fill == PlgColor5.Fill)
            nNumberOfColors5++;

        if (PlgFront1.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgFront2.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgFront3.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgFront4.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgFront5.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgFront6.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgFront7.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgFront8.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgFront9.Fill == PlgColor6.Fill)
            nNumberOfColors6++;

        if (PlgRight1.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgRight2.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgRight3.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgRight4.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgRight5.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgRight6.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgRight7.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgRight8.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgRight9.Fill == PlgColor1.Fill)
            nNumberOfColors1++;

        if (PlgRight1.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgRight2.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgRight3.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgRight4.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgRight5.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgRight6.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgRight7.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgRight8.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgRight9.Fill == PlgColor2.Fill)
            nNumberOfColors2++;

        if (PlgRight1.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgRight2.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgRight3.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgRight4.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgRight5.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgRight6.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgRight7.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgRight8.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgRight9.Fill == PlgColor3.Fill)
            nNumberOfColors3++;

        if (PlgRight1.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgRight2.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgRight3.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgRight4.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgRight5.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgRight6.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgRight7.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgRight8.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgRight9.Fill == PlgColor4.Fill)
            nNumberOfColors4++;

        if (PlgRight1.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgRight2.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgRight3.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgRight4.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgRight5.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgRight6.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgRight7.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgRight8.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgRight9.Fill == PlgColor5.Fill)
            nNumberOfColors5++;

        if (PlgRight1.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgRight2.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgRight3.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgRight4.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgRight5.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgRight6.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgRight7.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgRight8.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgRight9.Fill == PlgColor6.Fill)
            nNumberOfColors6++;

        if (PlgLeft1.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgLeft2.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgLeft3.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgLeft4.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgLeft5.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgLeft6.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgLeft7.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgLeft8.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgLeft9.Fill == PlgColor1.Fill)
            nNumberOfColors1++;

        if (PlgLeft1.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgLeft2.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgLeft3.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgLeft4.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgLeft5.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgLeft6.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgLeft7.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgLeft8.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgLeft9.Fill == PlgColor2.Fill)
            nNumberOfColors2++;

        if (PlgLeft1.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgLeft2.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgLeft3.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgLeft4.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgLeft5.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgLeft6.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgLeft7.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgLeft8.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgLeft9.Fill == PlgColor3.Fill)
            nNumberOfColors3++;

        if (PlgLeft1.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgLeft2.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgLeft3.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgLeft4.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgLeft5.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgLeft6.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgLeft7.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgLeft8.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgLeft9.Fill == PlgColor4.Fill)
            nNumberOfColors4++;

        if (PlgLeft1.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgLeft2.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgLeft3.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgLeft4.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgLeft5.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgLeft6.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgLeft7.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgLeft8.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgLeft9.Fill == PlgColor5.Fill)
            nNumberOfColors5++;

        if (PlgLeft1.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgLeft2.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgLeft3.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgLeft4.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgLeft5.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgLeft6.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgLeft7.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgLeft8.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgLeft9.Fill == PlgColor6.Fill)
            nNumberOfColors6++;

        if (PlgBack1.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgBack2.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgBack3.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgBack4.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgBack5.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgBack6.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgBack7.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgBack8.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgBack9.Fill == PlgColor1.Fill)
            nNumberOfColors1++;

        if (PlgBack1.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgBack2.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgBack3.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgBack4.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgBack5.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgBack6.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgBack7.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgBack8.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgBack9.Fill == PlgColor2.Fill)
            nNumberOfColors2++;

        if (PlgBack1.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgBack2.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgBack3.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgBack4.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgBack5.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgBack6.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgBack7.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgBack8.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgBack9.Fill == PlgColor3.Fill)
            nNumberOfColors3++;

        if (PlgBack1.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgBack2.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgBack3.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgBack4.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgBack5.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgBack6.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgBack7.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgBack8.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgBack9.Fill == PlgColor4.Fill)
            nNumberOfColors4++;

        if (PlgBack1.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgBack2.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgBack3.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgBack4.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgBack5.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgBack6.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgBack7.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgBack8.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgBack9.Fill == PlgColor5.Fill)
            nNumberOfColors5++;

        if (PlgBack1.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgBack2.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgBack3.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgBack4.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgBack5.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgBack6.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgBack7.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgBack8.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgBack9.Fill == PlgColor6.Fill)
            nNumberOfColors6++;

        if (PlgBottom1.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgBottom2.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgBottom3.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgBottom4.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgBottom5.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgBottom6.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgBottom7.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgBottom8.Fill == PlgColor1.Fill)
            nNumberOfColors1++;
        if (PlgBottom9.Fill == PlgColor1.Fill)
            nNumberOfColors1++;

        if (PlgBottom1.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgBottom2.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgBottom3.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgBottom4.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgBottom5.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgBottom6.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgBottom7.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgBottom8.Fill == PlgColor2.Fill)
            nNumberOfColors2++;
        if (PlgBottom9.Fill == PlgColor2.Fill)
            nNumberOfColors2++;

        if (PlgBottom1.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgBottom2.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgBottom3.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgBottom4.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgBottom5.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgBottom6.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgBottom7.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgBottom8.Fill == PlgColor3.Fill)
            nNumberOfColors3++;
        if (PlgBottom9.Fill == PlgColor3.Fill)
            nNumberOfColors3++;

        if (PlgBottom1.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgBottom2.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgBottom3.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgBottom4.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgBottom5.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgBottom6.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgBottom7.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgBottom8.Fill == PlgColor4.Fill)
            nNumberOfColors4++;
        if (PlgBottom9.Fill == PlgColor4.Fill)
            nNumberOfColors4++;

        if (PlgBottom1.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgBottom2.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgBottom3.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgBottom4.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgBottom5.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgBottom6.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgBottom7.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgBottom8.Fill == PlgColor5.Fill)
            nNumberOfColors5++;
        if (PlgBottom9.Fill == PlgColor5.Fill)
            nNumberOfColors5++;

        if (PlgBottom1.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgBottom2.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgBottom3.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgBottom4.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgBottom5.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgBottom6.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgBottom7.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgBottom8.Fill == PlgColor6.Fill)
            nNumberOfColors6++;
        if (PlgBottom9.Fill == PlgColor6.Fill)
            nNumberOfColors6++;

        if (nNumberOfColors1 != 9 || nNumberOfColors2 != 9 || nNumberOfColors3 != 9 || nNumberOfColors4 != 9 || nNumberOfColors5 != 9 || nNumberOfColors6 != 9)
        {
            DisplayAlert("Error", "There must be nine of the same color from each available color!", "OK");
            return false;
        }

        return true;
    }

    // Check if the cube is solved.
    private bool CheckIfCubeIsSolved(bool bShowMessage)
    {
        bool bColorsTop = false;
        bool bColorsFront = false;
        bool bColorsRight = false;
        bool bColorsLeft = false;
        bool bColorsBack = false;
        bool bColorsBottom = false;

        if (PlgTop1.Fill == PlgTop2.Fill && PlgTop1.Fill == PlgTop3.Fill && PlgTop1.Fill == PlgTop4.Fill && PlgTop1.Fill == PlgTop5.Fill && PlgTop1.Fill == PlgTop6.Fill && PlgTop1.Fill == PlgTop7.Fill && PlgTop1.Fill == PlgTop8.Fill && PlgTop1.Fill == PlgTop9.Fill)
        {
            bColorsTop = true;
        }

        if (PlgFront1.Fill == PlgFront2.Fill && PlgFront1.Fill == PlgFront3.Fill && PlgFront1.Fill == PlgFront4.Fill && PlgFront1.Fill == PlgFront5.Fill && PlgFront1.Fill == PlgFront6.Fill && PlgFront1.Fill == PlgFront7.Fill && PlgFront1.Fill == PlgFront8.Fill && PlgFront1.Fill == PlgFront9.Fill)
        {
            bColorsFront = true;
        }

        if (PlgRight1.Fill == PlgRight2.Fill && PlgRight1.Fill == PlgRight3.Fill && PlgRight1.Fill == PlgRight4.Fill && PlgRight1.Fill == PlgRight5.Fill && PlgRight1.Fill == PlgRight6.Fill && PlgRight1.Fill == PlgRight7.Fill && PlgRight1.Fill == PlgRight8.Fill && PlgRight1.Fill == PlgRight9.Fill)
        {
            bColorsRight = true;
        }

        if (PlgLeft1.Fill == PlgLeft2.Fill && PlgLeft1.Fill == PlgLeft3.Fill && PlgLeft1.Fill == PlgLeft4.Fill && PlgLeft1.Fill == PlgLeft5.Fill && PlgLeft1.Fill == PlgLeft6.Fill && PlgLeft1.Fill == PlgLeft7.Fill && PlgLeft1.Fill == PlgLeft8.Fill && PlgLeft1.Fill == PlgLeft9.Fill)
        {
            bColorsLeft = true;
        }

        if (PlgBack1.Fill == PlgBack2.Fill && PlgBack1.Fill == PlgBack3.Fill && PlgBack1.Fill == PlgBack4.Fill && PlgBack1.Fill == PlgBack5.Fill && PlgBack1.Fill == PlgBack6.Fill && PlgBack1.Fill == PlgBack7.Fill && PlgBack1.Fill == PlgBack8.Fill && PlgBack1.Fill == PlgBack9.Fill)
        {
            bColorsBack = true;
        }

        if (PlgBottom1.Fill == PlgBottom2.Fill && PlgBottom1.Fill == PlgBottom3.Fill && PlgBottom1.Fill == PlgBottom4.Fill && PlgBottom1.Fill == PlgBottom5.Fill && PlgBottom1.Fill == PlgBottom6.Fill && PlgBottom1.Fill == PlgBottom7.Fill && PlgBottom1.Fill == PlgBottom8.Fill && PlgBottom1.Fill == PlgBottom9.Fill)
        {
            bColorsBottom = true;
        }

        if (!bColorsTop || !bColorsFront || !bColorsRight || !bColorsLeft || !bColorsBack || !bColorsBottom)
        {
            if (bShowMessage)
            {
                DisplayAlert("Rubik's Cube", "The cube is not solved!", "OK");
            }
            return false;
        }
        
        DisplayAlert("Rubik's Cube", "Congratulations, the cube has been solved.", "OK");
        return true;
    }

    // Turn the layers of the cube.
    // Turn the front side clockwise (to right +).
    private void ImgbtnTurnFrontSideToRight_Clicked(object sender, EventArgs e)
    {
        ImgbtnTurnFrontSideToRight.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnFrontSideTo("+");
        ExplainTurnCube("Turn the front side 'clockwise' (+).");
        ImgbtnTurnFrontSideToRight.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the top middle to the right side (+).
    private void ImgbtnTurnTopMiddleToRightSide_Clicked(object sender, EventArgs e)
    {
        ImgbtnTurnTopMiddleToRightSide.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnTopMiddleTo("+");
        ExplainTurnCube("Turn the top middle to the right side (+).");
        ImgbtnTurnTopMiddleToRightSide.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the back side counter clockwise (to left -).
    private void ImgbtnTurnBackSideToLeft_Clicked(object sender, EventArgs e)
    {
        ImgbtnTurnBackSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnBackSideTo("-");
        ExplainTurnCube("Turn the back side 'counter clockwise' (-).");
        ImgbtnTurnBackSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the left side clockwise (to right +).
    private void ImgbtnTurnLeftSideToRight_Clicked(object sender, EventArgs e)
    {
        ImgbtnTurnLeftSideToRight.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnLeftSideTo("+");
        ExplainTurnCube("Turn the left side 'clockwise' (+).");
        ImgbtnTurnLeftSideToRight.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the top middle to the front side (-).
    private void ImgbtnTurnTopMiddleToFrontSide_Clicked(object sender, EventArgs e)
    {
        ImgbtnTurnTopMiddleToFrontSide.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnFrontTopMiddleTo("-");
        ExplainTurnCube("Turn the top middle to the front side (-).");
        ImgbtnTurnTopMiddleToFrontSide.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the right side counter clockwise (to left -).
    private void ImgbtnTurnRightSideToLeft_Clicked(object sender, EventArgs e)
    {
        ImgbtnTurnRightSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnRightSideTo("-");
        ExplainTurnCube("Turn the right side 'counter clockwise' (-).");
        ImgbtnTurnRightSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the top side counter clockwise (to left -).
    private void ImgbtnTurnTopSideToLeft_Clicked(object sender, EventArgs e)
    {
        ImgbtnTurnTopSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnTopSideTo("-");
        ExplainTurnCube("Turn the top side 'counter clockwise' (-).");
        ImgbtnTurnTopSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the front middle to the right side (-).
    private void ImgbtnTurnFrontMiddleToRightSide_Clicked(object sender, EventArgs e)
    {
        ImgbtnTurnFrontMiddleToRightSide.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnHorizontalMiddleLayerTo("-");
        ExplainTurnCube("Turn the front middle to the right side (-).");
        ImgbtnTurnFrontMiddleToRightSide.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the bottom side clockwise (to right +).
    private void ImgbtnTurnBottomSideToRight_Clicked(object sender, EventArgs e)
    {
        ImgbtnTurnBottomSideToRight.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnBottomSideTo("+");
        ExplainTurnCube("Turn the bottom side 'clockwise' (+).");
        ImgbtnTurnBottomSideToRight.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the top side clockwise (to right +).
    private void ImgbtnTurnTopSideToRight_Clicked(object sender, EventArgs e)
    {
        ImgbtnTurnTopSideToRight.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnTopSideTo("+");
        ExplainTurnCube("Turn the top side 'clockwise' (+).");
        ImgbtnTurnTopSideToRight.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the right middle to the front side (+).
    private void ImgbtnTurnRightMiddleToFrontSide_Clicked(object sender, EventArgs e)
    {
        ImgbtnTurnRightMiddleToFrontSide.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnHorizontalMiddleLayerTo("+");
        ExplainTurnCube("Turn the right middle to the front side (+).");
        ImgbtnTurnRightMiddleToFrontSide.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the bottom side counter clockwise (to left -).
    private void ImgbtnTurnBottomSideToLeft_Clicked(object sender, EventArgs e)
    {
        ImgbtnTurnBottomSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnBottomSideTo("-");
        ExplainTurnCube("Turn the bottom side 'counter clockwise' (-).");
        ImgbtnTurnBottomSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the left side counter clockwise (to left -).
    private void ImgbtnTurnLeftSideToLeft_Clicked(object sender, EventArgs e)
    {
        ImgbtnTurnLeftSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnLeftSideTo("-");
        ExplainTurnCube("Turn the left side 'counter clockwise' (-).");
        ImgbtnTurnLeftSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the front middle to the top side (+).
    private void ImgbtnTurnFrontMiddleToTopSide_Clicked(object sender, EventArgs e)
    {
        ImgbtnTurnFrontMiddleToTopSide.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnFrontTopMiddleTo("+");
        ExplainTurnCube("Turn the front middle to the top side (+).");
        ImgbtnTurnFrontMiddleToTopSide.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the right side clockwise (to right +).
    private void ImgbtnTurnRightSideToRight_Clicked(object sender, EventArgs e)
    {
        ImgbtnTurnRightSideToRight.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnRightSideTo("+");
        ExplainTurnCube("Turn the right side 'clockwise' (+).");
        ImgbtnTurnRightSideToRight.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the front side counter clockwise (to left -).
    private void ImgbtnTurnFrontSideToLeft_Clicked(object sender, EventArgs e)
    {
        ImgbtnTurnFrontSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnFrontSideTo("-");
        ExplainTurnCube("Turn the front side 'counter clockwise' (-).");
        ImgbtnTurnFrontSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the right middle to the top side (-).
    private void ImgbtnTurnRightMiddleToTopSide_Clicked(object sender, EventArgs e)
    {
        ImgbtnTurnRightMiddleToTopSide.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnTopMiddleTo("-");
        ExplainTurnCube("Turn the right middle to the top side (-).");
        ImgbtnTurnRightMiddleToTopSide.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the back side clockwise (to right +).
    private void ImgbtnTurnBackSideToRight_Clicked(object sender, EventArgs e)
    {
        ImgbtnTurnBackSideToRight.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnBackSideTo("+");
        ExplainTurnCube("Turn the back side 'clockwise' (+).");
        ImgbtnTurnBackSideToRight.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the entire front side clockwise or counter clockwise.
    private void TurnFrontSideTo(string cDirection)
    {
        Brush ColorFront1 = PlgFront1.Fill;
        Brush ColorFront2 = PlgFront2.Fill;
        Brush ColorFront3 = PlgFront3.Fill;
        Brush ColorFront4 = PlgFront4.Fill;
        Brush ColorFront6 = PlgFront6.Fill;
        Brush ColorFront7 = PlgFront7.Fill;
        Brush ColorFront8 = PlgFront8.Fill;
        Brush ColorFront9 = PlgFront9.Fill;

        Brush ColorTop7 = PlgTop7.Fill;
        Brush ColorTop8 = PlgTop8.Fill;
        Brush ColorTop9 = PlgTop9.Fill;

        Brush ColorRight1 = PlgRight1.Fill;
        Brush ColorRight4 = PlgRight4.Fill;
        Brush ColorRight7 = PlgRight7.Fill;

        Brush ColorBottom1 = PlgBottom1.Fill;
        Brush ColorBottom2 = PlgBottom2.Fill;
        Brush ColorBottom3 = PlgBottom3.Fill;

        Brush ColorLeft3 = PlgLeft3.Fill;
        Brush ColorLeft6 = PlgLeft6.Fill;
        Brush ColorLeft9 = PlgLeft9.Fill;

        if (cDirection == "+")
        {
            PlgFront1.Fill = ColorFront7;
            PlgFront2.Fill = ColorFront4;
            PlgFront3.Fill = ColorFront1;
            PlgFront4.Fill = ColorFront8;
            PlgFront6.Fill = ColorFront2;
            PlgFront7.Fill = ColorFront9;
            PlgFront8.Fill = ColorFront6;
            PlgFront9.Fill = ColorFront3;

            PlgTop7.Fill = ColorLeft9;
            PlgTop8.Fill = ColorLeft6;
            PlgTop9.Fill = ColorLeft3;

            PlgRight1.Fill = ColorTop7;
            PlgRight4.Fill = ColorTop8;
            PlgRight7.Fill = ColorTop9;

            PlgBottom1.Fill = ColorRight7;
            PlgBottom2.Fill = ColorRight4;
            PlgBottom3.Fill = ColorRight1;

            PlgLeft3.Fill = ColorBottom1;
            PlgLeft6.Fill = ColorBottom2;
            PlgLeft9.Fill = ColorBottom3;
        }

        if (cDirection == "-")
        {
            PlgFront1.Fill = ColorFront3;
            PlgFront2.Fill = ColorFront6;
            PlgFront3.Fill = ColorFront9;
            PlgFront4.Fill = ColorFront2;
            PlgFront6.Fill = ColorFront8;
            PlgFront7.Fill = ColorFront1;
            PlgFront8.Fill = ColorFront4;
            PlgFront9.Fill = ColorFront7;

            PlgTop7.Fill = ColorRight1;
            PlgTop8.Fill = ColorRight4;
            PlgTop9.Fill = ColorRight7;

            PlgRight1.Fill = ColorBottom3;
            PlgRight4.Fill = ColorBottom2;
            PlgRight7.Fill = ColorBottom1;

            PlgBottom1.Fill = ColorLeft3;
            PlgBottom2.Fill = ColorLeft6;
            PlgBottom3.Fill = ColorLeft9;

            PlgLeft3.Fill = ColorTop9;
            PlgLeft6.Fill = ColorTop8;
            PlgLeft9.Fill = ColorTop7;
        }
    }

    // Turn the top middle to the right or left.
    private void TurnTopMiddleTo(string cDirection)
    {
        Brush ColorTop4 = PlgTop4.Fill;
        Brush ColorTop5 = PlgTop5.Fill;
        Brush ColorTop6 = PlgTop6.Fill;

        Brush ColorRight2 = PlgRight2.Fill;
        Brush ColorRight5 = PlgRight5.Fill;
        Brush ColorRight8 = PlgRight8.Fill;

        Brush ColorBottom4 = PlgBottom4.Fill;
        Brush ColorBottom5 = PlgBottom5.Fill;
        Brush ColorBottom6 = PlgBottom6.Fill;

        Brush ColorLeft2 = PlgLeft2.Fill;
        Brush ColorLeft5 = PlgLeft5.Fill;
        Brush ColorLeft8 = PlgLeft8.Fill;

        if (cDirection == "+")
        {
            PlgTop4.Fill = ColorLeft8;
            PlgTop5.Fill = ColorLeft5;
            PlgTop6.Fill = ColorLeft2;

            PlgRight2.Fill = ColorTop4;
            PlgRight5.Fill = ColorTop5;
            PlgRight8.Fill = ColorTop6;

            PlgBottom4.Fill = ColorRight8;
            PlgBottom5.Fill = ColorRight5;
            PlgBottom6.Fill = ColorRight2;

            PlgLeft2.Fill = ColorBottom4;
            PlgLeft5.Fill = ColorBottom5;
            PlgLeft8.Fill = ColorBottom6;
        }

        if (cDirection == "-")
        {
            PlgTop4.Fill = ColorRight2;
            PlgTop5.Fill = ColorRight5;
            PlgTop6.Fill = ColorRight8;

            PlgRight2.Fill = ColorBottom6;
            PlgRight5.Fill = ColorBottom5;
            PlgRight8.Fill = ColorBottom4;

            PlgBottom4.Fill = ColorLeft2;
            PlgBottom5.Fill = ColorLeft5;
            PlgBottom6.Fill = ColorLeft8;

            PlgLeft2.Fill = ColorTop6;
            PlgLeft5.Fill = ColorTop5;
            PlgLeft8.Fill = ColorTop4;
        }
    }

    // Turn the entire back side clockwise or counter clockwise.
    private void TurnBackSideTo(string cDirection)
    {
        Brush ColorBack1 = PlgBack1.Fill;
        Brush ColorBack2 = PlgBack2.Fill;
        Brush ColorBack3 = PlgBack3.Fill;
        Brush ColorBack4 = PlgBack4.Fill;
        Brush ColorBack6 = PlgBack6.Fill;
        Brush ColorBack7 = PlgBack7.Fill;
        Brush ColorBack8 = PlgBack8.Fill;
        Brush ColorBack9 = PlgBack9.Fill;

        Brush ColorTop1 = PlgTop1.Fill;
        Brush ColorTop2 = PlgTop2.Fill;
        Brush ColorTop3 = PlgTop3.Fill;

        Brush ColorRight3 = PlgRight3.Fill;
        Brush ColorRight6 = PlgRight6.Fill;
        Brush ColorRight9 = PlgRight9.Fill;

        Brush ColorBottom7 = PlgBottom7.Fill;
        Brush ColorBottom8 = PlgBottom8.Fill;
        Brush ColorBottom9 = PlgBottom9.Fill;

        Brush ColorLeft1 = PlgLeft1.Fill;
        Brush ColorLeft4 = PlgLeft4.Fill;
        Brush ColorLeft7 = PlgLeft7.Fill;

        if (cDirection == "+")
        {
            PlgBack1.Fill = ColorBack7;
            PlgBack2.Fill = ColorBack4;
            PlgBack3.Fill = ColorBack1;
            PlgBack4.Fill = ColorBack8;
            PlgBack6.Fill = ColorBack2;
            PlgBack7.Fill = ColorBack9;
            PlgBack8.Fill = ColorBack6;
            PlgBack9.Fill = ColorBack3;

            PlgTop1.Fill = ColorRight3;
            PlgTop2.Fill = ColorRight6;
            PlgTop3.Fill = ColorRight9;

            PlgRight3.Fill = ColorBottom9;
            PlgRight6.Fill = ColorBottom8;
            PlgRight9.Fill = ColorBottom7;

            PlgBottom7.Fill = ColorLeft1;
            PlgBottom8.Fill = ColorLeft4;
            PlgBottom9.Fill = ColorLeft7;

            PlgLeft1.Fill = ColorTop3;
            PlgLeft4.Fill = ColorTop2;
            PlgLeft7.Fill = ColorTop1;
        }

        if (cDirection == "-")
        {
            PlgBack1.Fill = ColorBack3;
            PlgBack2.Fill = ColorBack6;
            PlgBack3.Fill = ColorBack9;
            PlgBack4.Fill = ColorBack2;
            PlgBack6.Fill = ColorBack8;
            PlgBack7.Fill = ColorBack1;
            PlgBack8.Fill = ColorBack4;
            PlgBack9.Fill = ColorBack7;

            PlgTop1.Fill = ColorLeft7;
            PlgTop2.Fill = ColorLeft4;
            PlgTop3.Fill = ColorLeft1;

            PlgRight3.Fill = ColorTop1;
            PlgRight6.Fill = ColorTop2;
            PlgRight9.Fill = ColorTop3;

            PlgBottom7.Fill = ColorRight9;
            PlgBottom8.Fill = ColorRight6;
            PlgBottom9.Fill = ColorRight3;

            PlgLeft1.Fill = ColorBottom7;
            PlgLeft4.Fill = ColorBottom8;
            PlgLeft7.Fill = ColorBottom9;
        }
    }

    // Turn the entire left side clockwise or counter clockwise.
    private void TurnLeftSideTo(string cDirection)
    {
        Brush ColorLeft1 = PlgLeft1.Fill;
        Brush ColorLeft2 = PlgLeft2.Fill;
        Brush ColorLeft3 = PlgLeft3.Fill;
        Brush ColorLeft4 = PlgLeft4.Fill;
        Brush ColorLeft6 = PlgLeft6.Fill;
        Brush ColorLeft7 = PlgLeft7.Fill;
        Brush ColorLeft8 = PlgLeft8.Fill;
        Brush ColorLeft9 = PlgLeft9.Fill;

        Brush ColorTop1 = PlgTop1.Fill;
        Brush ColorTop4 = PlgTop4.Fill;
        Brush ColorTop7 = PlgTop7.Fill;

        Brush ColorFront1 = PlgFront1.Fill;
        Brush ColorFront4 = PlgFront4.Fill;
        Brush ColorFront7 = PlgFront7.Fill;

        Brush ColorBottom1 = PlgBottom1.Fill;
        Brush ColorBottom4 = PlgBottom4.Fill;
        Brush ColorBottom7 = PlgBottom7.Fill;

        Brush ColorBack3 = PlgBack3.Fill;
        Brush ColorBack6 = PlgBack6.Fill;
        Brush ColorBack9 = PlgBack9.Fill;

        if (cDirection == "+")
        {
            PlgLeft1.Fill = ColorLeft7;
            PlgLeft2.Fill = ColorLeft4;
            PlgLeft3.Fill = ColorLeft1;
            PlgLeft4.Fill = ColorLeft8;
            PlgLeft6.Fill = ColorLeft2;
            PlgLeft7.Fill = ColorLeft9;
            PlgLeft8.Fill = ColorLeft6;
            PlgLeft9.Fill = ColorLeft3;

            PlgTop1.Fill = ColorBack9;
            PlgTop4.Fill = ColorBack6;
            PlgTop7.Fill = ColorBack3;

            PlgFront1.Fill = ColorTop1;
            PlgFront4.Fill = ColorTop4;
            PlgFront7.Fill = ColorTop7;

            PlgBottom1.Fill = ColorFront1;
            PlgBottom4.Fill = ColorFront4;
            PlgBottom7.Fill = ColorFront7;

            PlgBack3.Fill = ColorBottom7;
            PlgBack6.Fill = ColorBottom4;
            PlgBack9.Fill = ColorBottom1;
        }

        if (cDirection == "-")
        {
            PlgLeft1.Fill = ColorLeft3;
            PlgLeft2.Fill = ColorLeft6;
            PlgLeft3.Fill = ColorLeft9;
            PlgLeft4.Fill = ColorLeft2;
            PlgLeft6.Fill = ColorLeft8;
            PlgLeft7.Fill = ColorLeft1;
            PlgLeft8.Fill = ColorLeft4;
            PlgLeft9.Fill = ColorLeft7;

            PlgTop1.Fill = ColorFront1;
            PlgTop4.Fill = ColorFront4;
            PlgTop7.Fill = ColorFront7;

            PlgFront1.Fill = ColorBottom1;
            PlgFront4.Fill = ColorBottom4;
            PlgFront7.Fill = ColorBottom7;

            PlgBottom1.Fill = ColorBack9;
            PlgBottom4.Fill = ColorBack6;
            PlgBottom7.Fill = ColorBack3;

            PlgBack3.Fill = ColorTop7;
            PlgBack6.Fill = ColorTop4;
            PlgBack9.Fill = ColorTop1;
        }
    }

    // Turn the top middle layer to right or left.
    private void TurnFrontTopMiddleTo(string cDirection)
    {
        Brush ColorTop2 = PlgTop2.Fill;
        Brush ColorTop5 = PlgTop5.Fill;
        Brush ColorTop8 = PlgTop8.Fill;

        Brush ColorFront2 = PlgFront2.Fill;
        Brush ColorFront5 = PlgFront5.Fill;
        Brush ColorFront8 = PlgFront8.Fill;

        Brush ColorBottom2 = PlgBottom2.Fill;
        Brush ColorBottom5 = PlgBottom5.Fill;
        Brush ColorBottom8 = PlgBottom8.Fill;

        Brush ColorBack2 = PlgBack2.Fill;
        Brush ColorBack5 = PlgBack5.Fill;
        Brush ColorBack8 = PlgBack8.Fill;

        if (cDirection == "+")
        {
            PlgTop2.Fill = ColorFront2;
            PlgTop5.Fill = ColorFront5;
            PlgTop8.Fill = ColorFront8;

            PlgFront2.Fill = ColorBottom2;
            PlgFront5.Fill = ColorBottom5;
            PlgFront8.Fill = ColorBottom8;

            PlgBottom2.Fill = ColorBack8;
            PlgBottom5.Fill = ColorBack5;
            PlgBottom8.Fill = ColorBack2;

            PlgBack2.Fill = ColorTop8;
            PlgBack5.Fill = ColorTop5;
            PlgBack8.Fill = ColorTop2;
        }

        if (cDirection == "-")
        {
            PlgTop2.Fill = ColorBack8;
            PlgTop5.Fill = ColorBack5;
            PlgTop8.Fill = ColorBack2;

            PlgFront2.Fill = ColorTop2;
            PlgFront5.Fill = ColorTop5;
            PlgFront8.Fill = ColorTop8;

            PlgBottom2.Fill = ColorFront2;
            PlgBottom5.Fill = ColorFront5;
            PlgBottom8.Fill = ColorFront8;

            PlgBack2.Fill = ColorBottom8;
            PlgBack5.Fill = ColorBottom5;
            PlgBack8.Fill = ColorBottom2;
        }
    }

    // Turn the entire right side clockwise or counter clockwise.
    private void TurnRightSideTo(string cDirection)
    {
        Brush ColorRight1 = PlgRight1.Fill;
        Brush ColorRight2 = PlgRight2.Fill;
        Brush ColorRight3 = PlgRight3.Fill;
        Brush ColorRight4 = PlgRight4.Fill;
        Brush ColorRight6 = PlgRight6.Fill;
        Brush ColorRight7 = PlgRight7.Fill;
        Brush ColorRight8 = PlgRight8.Fill;
        Brush ColorRight9 = PlgRight9.Fill;

        Brush ColorTop3 = PlgTop3.Fill;
        Brush ColorTop6 = PlgTop6.Fill;
        Brush ColorTop9 = PlgTop9.Fill;

        Brush ColorFront3 = PlgFront3.Fill;
        Brush ColorFront6 = PlgFront6.Fill;
        Brush ColorFront9 = PlgFront9.Fill;

        Brush ColorBottom3 = PlgBottom3.Fill;
        Brush ColorBottom6 = PlgBottom6.Fill;
        Brush ColorBottom9 = PlgBottom9.Fill;

        Brush ColorBack1 = PlgBack1.Fill;
        Brush ColorBack4 = PlgBack4.Fill;
        Brush ColorBack7 = PlgBack7.Fill;

        if (cDirection == "+")
        {
            PlgRight1.Fill = ColorRight7;
            PlgRight2.Fill = ColorRight4;
            PlgRight3.Fill = ColorRight1;
            PlgRight4.Fill = ColorRight8;
            PlgRight6.Fill = ColorRight2;
            PlgRight7.Fill = ColorRight9;
            PlgRight8.Fill = ColorRight6;
            PlgRight9.Fill = ColorRight3;

            PlgTop3.Fill = ColorFront3;
            PlgTop6.Fill = ColorFront6;
            PlgTop9.Fill = ColorFront9;

            PlgFront3.Fill = ColorBottom3;
            PlgFront6.Fill = ColorBottom6;
            PlgFront9.Fill = ColorBottom9;

            PlgBottom3.Fill = ColorBack7;
            PlgBottom6.Fill = ColorBack4;
            PlgBottom9.Fill = ColorBack1;

            PlgBack1.Fill = ColorTop9;
            PlgBack4.Fill = ColorTop6;
            PlgBack7.Fill = ColorTop3;
        }

        if (cDirection == "-")
        {
            PlgRight1.Fill = ColorRight3;
            PlgRight2.Fill = ColorRight6;
            PlgRight3.Fill = ColorRight9;
            PlgRight4.Fill = ColorRight2;
            PlgRight6.Fill = ColorRight8;
            PlgRight7.Fill = ColorRight1;
            PlgRight8.Fill = ColorRight4;
            PlgRight9.Fill = ColorRight7;

            PlgTop3.Fill = ColorBack7;
            PlgTop6.Fill = ColorBack4;
            PlgTop9.Fill = ColorBack1;

            PlgFront3.Fill = ColorTop3;
            PlgFront6.Fill = ColorTop6;
            PlgFront9.Fill = ColorTop9;

            PlgBottom3.Fill = ColorFront3;
            PlgBottom6.Fill = ColorFront6;
            PlgBottom9.Fill = ColorFront9;

            PlgBack1.Fill = ColorBottom9;
            PlgBack4.Fill = ColorBottom6;
            PlgBack7.Fill = ColorBottom3;
        }
    }

    // Turn the entire top side clockwise or counter clockwise.
    private void TurnTopSideTo(string cDirection)
    {
        Brush ColorTop1 = PlgTop1.Fill;
        Brush ColorTop2 = PlgTop2.Fill;
        Brush ColorTop3 = PlgTop3.Fill;
        Brush ColorTop4 = PlgTop4.Fill;
        Brush ColorTop6 = PlgTop6.Fill;
        Brush ColorTop7 = PlgTop7.Fill;
        Brush ColorTop8 = PlgTop8.Fill;
        Brush ColorTop9 = PlgTop9.Fill;

        Brush ColorLeft1 = PlgLeft1.Fill;
        Brush ColorLeft2 = PlgLeft2.Fill;
        Brush ColorLeft3 = PlgLeft3.Fill;

        Brush ColorFront1 = PlgFront1.Fill;
        Brush ColorFront2 = PlgFront2.Fill;
        Brush ColorFront3 = PlgFront3.Fill;

        Brush ColorRight1 = PlgRight1.Fill;
        Brush ColorRight2 = PlgRight2.Fill;
        Brush ColorRight3 = PlgRight3.Fill;

        Brush ColorBack1 = PlgBack1.Fill;
        Brush ColorBack2 = PlgBack2.Fill;
        Brush ColorBack3 = PlgBack3.Fill;

        if (cDirection == "+")
        {
            PlgTop1.Fill = ColorTop7;
            PlgTop2.Fill = ColorTop4;
            PlgTop3.Fill = ColorTop1;
            PlgTop4.Fill = ColorTop8;
            PlgTop6.Fill = ColorTop2;
            PlgTop7.Fill = ColorTop9;
            PlgTop8.Fill = ColorTop6;
            PlgTop9.Fill = ColorTop3;

            PlgLeft1.Fill = ColorFront1;
            PlgLeft2.Fill = ColorFront2;
            PlgLeft3.Fill = ColorFront3;

            PlgFront1.Fill = ColorRight1;
            PlgFront2.Fill = ColorRight2;
            PlgFront3.Fill = ColorRight3;

            PlgRight1.Fill = ColorBack1;
            PlgRight2.Fill = ColorBack2;
            PlgRight3.Fill = ColorBack3;

            PlgBack1.Fill = ColorLeft1;
            PlgBack2.Fill = ColorLeft2;
            PlgBack3.Fill = ColorLeft3;
        }

        if (cDirection == "-")
        {
            PlgTop1.Fill = ColorTop3;
            PlgTop2.Fill = ColorTop6;
            PlgTop3.Fill = ColorTop9;
            PlgTop4.Fill = ColorTop2;
            PlgTop6.Fill = ColorTop8;
            PlgTop7.Fill = ColorTop1;
            PlgTop8.Fill = ColorTop4;
            PlgTop9.Fill = ColorTop7;

            PlgLeft1.Fill = ColorBack1;
            PlgLeft2.Fill = ColorBack2;
            PlgLeft3.Fill = ColorBack3;

            PlgFront1.Fill = ColorLeft1;
            PlgFront2.Fill = ColorLeft2;
            PlgFront3.Fill = ColorLeft3;

            PlgRight1.Fill = ColorFront1;
            PlgRight2.Fill = ColorFront2;
            PlgRight3.Fill = ColorFront3;

            PlgBack1.Fill = ColorRight1;
            PlgBack2.Fill = ColorRight2;
            PlgBack3.Fill = ColorRight3;
        }
    }

    // Turn the horizontal middle layer to right or left.
    private void TurnHorizontalMiddleLayerTo(string cDirection)
    {
        Brush ColorFront4 = PlgFront4.Fill;
        Brush ColorFront5 = PlgFront5.Fill;
        Brush ColorFront6 = PlgFront6.Fill;

        Brush ColorRight4 = PlgRight4.Fill;
        Brush ColorRight5 = PlgRight5.Fill;
        Brush ColorRight6 = PlgRight6.Fill;

        Brush ColorBack4 = PlgBack4.Fill;
        Brush ColorBack5 = PlgBack5.Fill;
        Brush ColorBack6 = PlgBack6.Fill;

        Brush ColorLeft4 = PlgLeft4.Fill;
        Brush ColorLeft5 = PlgLeft5.Fill;
        Brush ColorLeft6 = PlgLeft6.Fill;

        if (cDirection == "+")
        {
            PlgFront4.Fill = ColorRight4;
            PlgFront5.Fill = ColorRight5;
            PlgFront6.Fill = ColorRight6;

            PlgRight4.Fill = ColorBack4;
            PlgRight5.Fill = ColorBack5;
            PlgRight6.Fill = ColorBack6;

            PlgBack4.Fill = ColorLeft4;
            PlgBack5.Fill = ColorLeft5;
            PlgBack6.Fill = ColorLeft6;

            PlgLeft4.Fill = ColorFront4;
            PlgLeft5.Fill = ColorFront5;
            PlgLeft6.Fill = ColorFront6;
        }

        if (cDirection == "-")
        {
            PlgFront4.Fill = ColorLeft4;
            PlgFront5.Fill = ColorLeft5;
            PlgFront6.Fill = ColorLeft6;

            PlgRight4.Fill = ColorFront4;
            PlgRight5.Fill = ColorFront5;
            PlgRight6.Fill = ColorFront6;

            PlgBack4.Fill = ColorRight4;
            PlgBack5.Fill = ColorRight5;
            PlgBack6.Fill = ColorRight6;

            PlgLeft4.Fill = ColorBack4;
            PlgLeft5.Fill = ColorBack5;
            PlgLeft6.Fill = ColorBack6;
        }
    }

    // Turn the entire bottom side clockwise or counter clockwise.
    private void TurnBottomSideTo(string cDirection)
    {
        Brush ColorBottom1 = PlgBottom1.Fill;
        Brush ColorBottom2 = PlgBottom2.Fill;
        Brush ColorBottom3 = PlgBottom3.Fill;
        Brush ColorBottom4 = PlgBottom4.Fill;
        Brush ColorBottom6 = PlgBottom6.Fill;
        Brush ColorBottom7 = PlgBottom7.Fill;
        Brush ColorBottom8 = PlgBottom8.Fill;
        Brush ColorBottom9 = PlgBottom9.Fill;

        Brush ColorLeft7 = PlgLeft7.Fill;
        Brush ColorLeft8 = PlgLeft8.Fill;
        Brush ColorLeft9 = PlgLeft9.Fill;

        Brush ColorFront7 = PlgFront7.Fill;
        Brush ColorFront8 = PlgFront8.Fill;
        Brush ColorFront9 = PlgFront9.Fill;

        Brush ColorRight7 = PlgRight7.Fill;
        Brush ColorRight8 = PlgRight8.Fill;
        Brush ColorRight9 = PlgRight9.Fill;

        Brush ColorBack7 = PlgBack7.Fill;
        Brush ColorBack8 = PlgBack8.Fill;
        Brush ColorBack9 = PlgBack9.Fill;

        if (cDirection == "+")
        {
            PlgBottom1.Fill = ColorBottom7;
            PlgBottom2.Fill = ColorBottom4;
            PlgBottom3.Fill = ColorBottom1;
            PlgBottom4.Fill = ColorBottom8;
            PlgBottom6.Fill = ColorBottom2;
            PlgBottom7.Fill = ColorBottom9;
            PlgBottom8.Fill = ColorBottom6;
            PlgBottom9.Fill = ColorBottom3;

            PlgLeft7.Fill = ColorBack7;
            PlgLeft8.Fill = ColorBack8;
            PlgLeft9.Fill = ColorBack9;

            PlgFront7.Fill = ColorLeft7;
            PlgFront8.Fill = ColorLeft8;
            PlgFront9.Fill = ColorLeft9;

            PlgRight7.Fill = ColorFront7;
            PlgRight8.Fill = ColorFront8;
            PlgRight9.Fill = ColorFront9;

            PlgBack7.Fill = ColorRight7;
            PlgBack8.Fill = ColorRight8;
            PlgBack9.Fill = ColorRight9;
        }

        if (cDirection == "-")
        {
            PlgBottom1.Fill = ColorBottom3;
            PlgBottom2.Fill = ColorBottom6;
            PlgBottom3.Fill = ColorBottom9;
            PlgBottom4.Fill = ColorBottom2;
            PlgBottom6.Fill = ColorBottom8;
            PlgBottom7.Fill = ColorBottom1;
            PlgBottom8.Fill = ColorBottom4;
            PlgBottom9.Fill = ColorBottom7;

            PlgLeft7.Fill = ColorFront7;
            PlgLeft8.Fill = ColorFront8;
            PlgLeft9.Fill = ColorFront9;

            PlgFront7.Fill = ColorRight7;
            PlgFront8.Fill = ColorRight8;
            PlgFront9.Fill = ColorRight9;

            PlgRight7.Fill = ColorBack7;
            PlgRight8.Fill = ColorBack8;
            PlgRight9.Fill = ColorBack9;

            PlgBack7.Fill = ColorLeft7;
            PlgBack8.Fill = ColorLeft8;
            PlgBack9.Fill = ColorLeft9;
        }
    }

    // Explain the turn of the cube.
    private async void ExplainTurnCube(string cTurnCubeText)
    {
        if (bExplainSpeech)
        {
            ConvertTextToSpeech(cTurnCubeText);
        }

        if (bExplainText)
        {
            await DisplayAlert("", cTurnCubeText, "OK");
        }
    }

    // On clicked event: Reset the colors of the cube.
    private void OnBtnResetClicked(object sender, EventArgs e)
    {
        ResetCube();
    }

    // Reset the colors of the cube.
    private void ResetCube()
    {
        PlgTop1.Fill = PlgColor1.Fill;
        PlgTop2.Fill = PlgColor1.Fill;
        PlgTop3.Fill = PlgColor1.Fill;
        PlgTop4.Fill = PlgColor1.Fill;
        PlgTop5.Fill = PlgColor1.Fill;
        PlgTop6.Fill = PlgColor1.Fill;
        PlgTop7.Fill = PlgColor1.Fill;
        PlgTop8.Fill = PlgColor1.Fill;
        PlgTop9.Fill = PlgColor1.Fill;

        PlgFront1.Fill = PlgColor2.Fill;
        PlgFront2.Fill = PlgColor2.Fill;
        PlgFront3.Fill = PlgColor2.Fill;
        PlgFront4.Fill = PlgColor2.Fill;
        PlgFront5.Fill = PlgColor2.Fill;
        PlgFront6.Fill = PlgColor2.Fill;
        PlgFront7.Fill = PlgColor2.Fill;
        PlgFront8.Fill = PlgColor2.Fill;
        PlgFront9.Fill = PlgColor2.Fill;

        PlgRight1.Fill = PlgColor3.Fill;
        PlgRight2.Fill = PlgColor3.Fill;
        PlgRight3.Fill = PlgColor3.Fill;
        PlgRight4.Fill = PlgColor3.Fill;
        PlgRight5.Fill = PlgColor3.Fill;
        PlgRight6.Fill = PlgColor3.Fill;
        PlgRight7.Fill = PlgColor3.Fill;
        PlgRight8.Fill = PlgColor3.Fill;
        PlgRight9.Fill = PlgColor3.Fill;

        PlgLeft1.Fill = PlgColor4.Fill;
        PlgLeft2.Fill = PlgColor4.Fill;
        PlgLeft3.Fill = PlgColor4.Fill;
        PlgLeft4.Fill = PlgColor4.Fill;
        PlgLeft5.Fill = PlgColor4.Fill;
        PlgLeft6.Fill = PlgColor4.Fill;
        PlgLeft7.Fill = PlgColor4.Fill;
        PlgLeft8.Fill = PlgColor4.Fill;
        PlgLeft9.Fill = PlgColor4.Fill;

        PlgBack1.Fill = PlgColor5.Fill;
        PlgBack2.Fill = PlgColor5.Fill;
        PlgBack3.Fill = PlgColor5.Fill;
        PlgBack4.Fill = PlgColor5.Fill;
        PlgBack5.Fill = PlgColor5.Fill;
        PlgBack6.Fill = PlgColor5.Fill;
        PlgBack7.Fill = PlgColor5.Fill;
        PlgBack8.Fill = PlgColor5.Fill;
        PlgBack9.Fill = PlgColor5.Fill;

        PlgBottom1.Fill = PlgColor6.Fill;
        PlgBottom2.Fill = PlgColor6.Fill;
        PlgBottom3.Fill = PlgColor6.Fill;
        PlgBottom4.Fill = PlgColor6.Fill;
        PlgBottom5.Fill = PlgColor6.Fill;
        PlgBottom6.Fill = PlgColor6.Fill;
        PlgBottom7.Fill = PlgColor6.Fill;
        PlgBottom8.Fill = PlgColor6.Fill;
        PlgBottom9.Fill = PlgColor6.Fill;
    }

    // Show license using the Loaded event of the MainPage.xaml.
    private async void OnPageLoad(object sender, EventArgs e)
    {
        // Show license.
        if (bLicense == false)
        {
            bool bAnswer = await Application.Current.MainPage.DisplayAlert(cLicenseTitle, cLicense, cAgree, cDisagree);

            if (bAnswer)
            {
                Preferences.Default.Set("SettingLicense", true);
            }
            else
            {
#if IOS
                //Thread.CurrentThread.Abort();  // Not allowed in iOS.
                ImgbtnAbout.IsEnabled = false;
                ImgbtnSettings.IsEnabled = false;

                await DisplayAlert(cLicenseTitle, cCloseApplication, cButtonClose);
#else
                Application.Current.Quit();
#endif
            }
        }
    }

    // Set language using the Appearing event of the MainPage.xaml.
    private void OnPageAppearing(object sender, EventArgs e)
    {
        if (bLanguageChanged)
        {
            SetTextLanguage();
            bLanguageChanged = false;

            //DisplayAlert("bLanguageChanged", "true", "OK");  // For testing.
        }

        //lblTextToSpeech.Text = GetIsoLanguageCode();
    }

    // Put text in the chosen language in the controls.
    private void SetTextLanguage()
    {
        //cLanguage = "es";  // For testing.
        //App.Current.MainPage.DisplayAlert("cLanguage", cLanguage, "OK");  // For testing.

        // Set the current UI culture of the selected language.
        SetCultureSelectedLanguage();

        //lblTitle.Text = CubeLang.BarcodeGenerator_Text;

        //cButtonClose = CubeLang.ButtonClose_Text;
        //cErrorTitle = CubeLang.ErrorTitle_Text;
        //cLicenseTitle = CubeLang.LicenseTitle_Text;
        //cLicense = CubeLang.License_Text + "\n\n" + CubeLang.LicenseMit2_Text;
        //cAgree = CubeLang.Agree_Text;
        //cDisagree = CubeLang.Disagree_Text;
        //cCloseApplication = CubeLang.CloseApplication_Text;
        //cTextToSpeechError = CubeLang.TextToSpeechError_Text;

        //App.Current.MainPage.DisplayAlert(cErrorTitleText, cLanguage, cButtonCloseText);  // For testing.
    }

    // Set the current UI culture of the selected language.
    public static void SetCultureSelectedLanguage()
    {
        try
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(cLanguage);
        }
        catch
        {
            // Do nothing.
        }
    }

    // Initialize text to speech and fill the the array with the speech languages.
    // .Country = KR ; .Id = ''  ; .Language = ko ; .Name = Korean (South Korea) ; 
    private async void InitializeTextToSpeech(string cCultureName)
    {
        // Initialize text to speech.
        int nTotalItems;

        try
        {
            locales = await TextToSpeech.Default.GetLocalesAsync();

            nTotalItems = locales.Count();

            if (nTotalItems == 0)
            {
                return;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert(cErrorTitle, ex.Message + "\n\n" + cTextToSpeechError, cButtonClose);
            return;
        }

        //lblTextToSpeech.IsVisible = true;
        //imgbtnTextToSpeech.IsVisible = true;
        bLanguageLocalesExist = true;

        // Put the locales in the array and sort the array.
        cLanguageLocales = new string[nTotalItems];
        int nItem = 0;

        foreach (var l in locales)
        {
            cLanguageLocales[nItem] = l.Language + "-" + l.Country + " " + l.Name;
            nItem++;
        }

        Array.Sort(cLanguageLocales);

        // Search for the language after a first start or reset of the application.
        if (cLanguageSpeech == "")
        {
            SearchArrayWithSpeechLanguages(cCultureName);
        }
        //await DisplayAlert("cLanguageSpeech", cLanguageSpeech, "OK");  // For testing.

        //lblTextToSpeech.Text = GetIsoLanguageCode();
    }

    // Search for the language after a first start or reset of the application.
    private void SearchArrayWithSpeechLanguages(string cCultureName)
    {
        try
        {
            int nTotalItems = cLanguageLocales.Length;

            for (int nItem = 0; nItem < nTotalItems; nItem++)
            {
                if (cLanguageLocales[nItem].StartsWith(cCultureName))
                {
                    cLanguageSpeech = cLanguageLocales[nItem];
                    break;
                }
            }

            // If the language is not found try it with the language (cLanguage) of the user setting for this app.
            if (cLanguageSpeech == "")
            {
                for (int nItem = 0; nItem < nTotalItems; nItem++)
                {
                    if (cLanguageLocales[nItem].StartsWith(cLanguage))
                    {
                        cLanguageSpeech = cLanguageLocales[nItem];
                        break;
                    }
                }
            }

            // If the language is still not found use the first language in the array.
            if (cLanguageSpeech == "")
            {
                cLanguageSpeech = cLanguageLocales[0];
            }
        }
        catch (Exception ex)
        {
            DisplayAlert(cErrorTitle, ex.Message, cButtonClose);
        }
    }

    // Convert text to speech.
    private async void ConvertTextToSpeech(string cTurnCubeText)
    {
        // Cancel the text to speech.
        if (bTextToSpeechIsBusy)
        {
            if (cts?.IsCancellationRequested ?? true)
                return;

            cts.Cancel();
            return;
        }

        // Start with the text to speech.
        if (cTurnCubeText != null && cTurnCubeText != "")
        {
            bTextToSpeechIsBusy = true;

            try
            {
                cts = new CancellationTokenSource();

                SpeechOptions options = new SpeechOptions()
                {
                    Locale = locales.Single(l => l.Language + "-" + l.Country + " " + l.Name == cLanguageSpeech)
                };

                await TextToSpeech.Default.SpeakAsync(cTurnCubeText, options, cancelToken: cts.Token);
                bTextToSpeechIsBusy = false;
            }
            catch (Exception ex)
            {
                await DisplayAlert(cErrorTitle, ex.Message, cButtonClose);
            }
        }
    }
}

/*
Numbering of cube surfaces.

    Outside view             Top              Inside view              Back
                     ______ ______ ______                      ______ ______ ______
                   /      /      /      /|                   /|      !      !      |
                 /  To1 /  To2 /  To3 /  |                 /  |  Ba3 !  Ba2 !  Ba1 |
               /______/______/______/ Ri3|               / Le1|______!______!______|
             /      /      /      / !    /       Left  / !    |      !      !      |
           /  To4 /  To5 /  To6 /   !  / |           /   !  / |  Ba6 !  Ba5 !  Ba4 |
         /______/______/______/ Ri2 !/Ri6|         /  Le2!/   |______!______!______|
       /      /      /      / !    /!    /       /  !   /! Le4|      !      !      |
     /  To7 /  To8 /  To9 /   !  /  !  / |     /    ! /  !  / |  Ba9 !  Ba8 !  Ba7 |
   /______/______/______/ Ri1 !/Ri5 !/Ri9|    | Le3 ! Le5!/   |______!______!______|
   |      !      !      |    /!    /!    /    |   / !   /! Le7/      /      /      /
   |  Fr1 !  Fr2 !  Fr3 |  /  !  /  !  /      | /   ! /  !  /  Bo7 /  Bo8 /  Bo9 /
   |______!______!______|/Ri4 !/Ri8 !/        | Le6 ! Le8!/______/______/______/
   |      !      !      |    /!    /          |   / !   /      /      /      /
   |  Fr4 !  Fr5 !  Fr6 |  /  !  /            | /   ! / Bo4  / Bo5  /  Bo6 /
   |______!______!______|/Ri7 !/ Right        | Le9 !______/______/______/
   |      !      !      |    /                |   /      /      /      /
   |  Fr7 !  Fr8 !  Fr9 |  /                  | / Bo1  /  Bo2 / Bo3  /
   |______!______!______|/                    |______/______/______/
           Front                                      Bottom
*/
