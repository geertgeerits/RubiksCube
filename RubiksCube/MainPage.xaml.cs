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
    public static string cCubeColor1;
    public static string cCubeColor2;
    public static string cCubeColor3;
    public static string cCubeColor4;
    public static string cCubeColor5;
    public static string cCubeColor6;

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
        cCubeColor1 = Preferences.Default.Get("SettingCubeColor1", "FF0000");   // Red
        cCubeColor2 = Preferences.Default.Get("SettingCubeColor2", "FFFF00");   // Yellow
        cCubeColor3 = Preferences.Default.Get("SettingCubeColor3", "0000FF");   // Blue
        cCubeColor4 = Preferences.Default.Get("SettingCubeColor4", "008000");   // Green
        cCubeColor5 = Preferences.Default.Get("SettingCubeColor5", "FFFFFF");   // White
        cCubeColor6 = Preferences.Default.Get("SettingCubeColor6", "FF8000");   // Orange

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

        // Initialize the cube colors.
        plgCubeColor1.Fill = Color.FromArgb(cCubeColor1);
        plgCubeColor2.Fill = Color.FromArgb(cCubeColor2);
        plgCubeColor3.Fill = Color.FromArgb(cCubeColor3);
        plgCubeColor4.Fill = Color.FromArgb(cCubeColor4);
        plgCubeColor5.Fill = Color.FromArgb(cCubeColor5);
        plgCubeColor6.Fill = Color.FromArgb(cCubeColor6);

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
        plgCubeColorSelect.Fill = polygon.Fill;        
    }

    // Drop the selected color on the cube and fill the cube with the color of the tempory rectangle.
    private void OnDrop(object sender, DropEventArgs e)
    {
        Polygon polygon = (sender as Element).Parent as Polygon;
        polygon.Fill = plgCubeColorSelect.Fill;

        plgCubeColorSelect.Fill = Color.FromArgb("000000");
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

        if (plgTop1.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgTop2.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgTop3.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgTop4.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgTop5.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgTop6.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgTop7.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgTop8.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgTop9.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;

        if (plgTop1.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgTop2.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgTop3.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgTop4.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgTop5.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgTop6.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgTop7.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgTop8.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgTop9.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;

        if (plgTop1.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgTop2.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgTop3.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgTop4.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgTop5.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgTop6.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgTop7.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgTop8.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgTop9.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;

        if (plgTop1.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgTop2.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgTop3.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgTop4.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgTop5.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgTop6.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgTop7.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgTop8.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgTop9.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;

        if (plgTop1.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgTop2.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgTop3.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgTop4.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgTop5.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgTop6.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgTop7.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgTop8.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgTop9.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;

        if (plgTop1.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgTop2.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgTop3.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgTop4.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgTop5.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgTop6.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgTop7.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgTop8.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgTop9.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;

        if (PlgFront1.Fill == plgCubeColor1.Fill) 
            nNumberOfColors1++;
        if (PlgFront2.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (PlgFront3.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (PlgFront4.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (PlgFront5.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (PlgFront6.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (PlgFront7.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (PlgFront8.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (PlgFront9.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;

        if (PlgFront1.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (PlgFront2.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (PlgFront3.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (PlgFront4.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (PlgFront5.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (PlgFront6.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (PlgFront7.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (PlgFront8.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (PlgFront9.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;

        if (PlgFront1.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (PlgFront2.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (PlgFront3.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (PlgFront4.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (PlgFront5.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (PlgFront6.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (PlgFront7.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (PlgFront8.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (PlgFront9.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;

        if (PlgFront1.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (PlgFront2.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (PlgFront3.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (PlgFront4.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (PlgFront5.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (PlgFront6.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (PlgFront7.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (PlgFront8.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (PlgFront9.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;

        if (PlgFront1.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (PlgFront2.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (PlgFront3.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (PlgFront4.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (PlgFront5.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (PlgFront6.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (PlgFront7.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (PlgFront8.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (PlgFront9.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;

        if (PlgFront1.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (PlgFront2.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (PlgFront3.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (PlgFront4.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (PlgFront5.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (PlgFront6.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (PlgFront7.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (PlgFront8.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (PlgFront9.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;

        if (plgRight1.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgRight2.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgRight3.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgRight4.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgRight5.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgRight6.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgRight7.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgRight8.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgRight9.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;

        if (plgRight1.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgRight2.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgRight3.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgRight4.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgRight5.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgRight6.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgRight7.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgRight8.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgRight9.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;

        if (plgRight1.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgRight2.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgRight3.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgRight4.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgRight5.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgRight6.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgRight7.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgRight8.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgRight9.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;

        if (plgRight1.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgRight2.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgRight3.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgRight4.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgRight5.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgRight6.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgRight7.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgRight8.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgRight9.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;

        if (plgRight1.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgRight2.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgRight3.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgRight4.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgRight5.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgRight6.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgRight7.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgRight8.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgRight9.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;

        if (plgRight1.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgRight2.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgRight3.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgRight4.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgRight5.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgRight6.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgRight7.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgRight8.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgRight9.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;

        if (plgLeft1.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgLeft2.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgLeft3.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgLeft4.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgLeft5.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgLeft6.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgLeft7.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgLeft8.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgLeft9.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;

        if (plgLeft1.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgLeft2.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgLeft3.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgLeft4.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgLeft5.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgLeft6.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgLeft7.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgLeft8.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgLeft9.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;

        if (plgLeft1.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgLeft2.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgLeft3.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgLeft4.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgLeft5.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgLeft6.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgLeft7.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgLeft8.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgLeft9.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;

        if (plgLeft1.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgLeft2.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgLeft3.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgLeft4.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgLeft5.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgLeft6.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgLeft7.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgLeft8.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgLeft9.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;

        if (plgLeft1.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgLeft2.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgLeft3.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgLeft4.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgLeft5.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgLeft6.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgLeft7.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgLeft8.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgLeft9.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;

        if (plgLeft1.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgLeft2.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgLeft3.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgLeft4.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgLeft5.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgLeft6.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgLeft7.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgLeft8.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgLeft9.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;

        if (plgBack1.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgBack2.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgBack3.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgBack4.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgBack5.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgBack6.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgBack7.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgBack8.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgBack9.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;

        if (plgBack1.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgBack2.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgBack3.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgBack4.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgBack5.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgBack6.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgBack7.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgBack8.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgBack9.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;

        if (plgBack1.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgBack2.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgBack3.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgBack4.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgBack5.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgBack6.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgBack7.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgBack8.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgBack9.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;

        if (plgBack1.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgBack2.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgBack3.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgBack4.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgBack5.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgBack6.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgBack7.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgBack8.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgBack9.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;

        if (plgBack1.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgBack2.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgBack3.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgBack4.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgBack5.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgBack6.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgBack7.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgBack8.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgBack9.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;

        if (plgBack1.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgBack2.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgBack3.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgBack4.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgBack5.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgBack6.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgBack7.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgBack8.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgBack9.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;

        if (plgBottom1.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgBottom2.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgBottom3.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgBottom4.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgBottom5.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgBottom6.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgBottom7.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgBottom8.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;
        if (plgBottom9.Fill == plgCubeColor1.Fill)
            nNumberOfColors1++;

        if (plgBottom1.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgBottom2.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgBottom3.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgBottom4.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgBottom5.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgBottom6.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgBottom7.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgBottom8.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;
        if (plgBottom9.Fill == plgCubeColor2.Fill)
            nNumberOfColors2++;

        if (plgBottom1.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgBottom2.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgBottom3.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgBottom4.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgBottom5.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgBottom6.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgBottom7.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgBottom8.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;
        if (plgBottom9.Fill == plgCubeColor3.Fill)
            nNumberOfColors3++;

        if (plgBottom1.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgBottom2.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgBottom3.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgBottom4.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgBottom5.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgBottom6.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgBottom7.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgBottom8.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;
        if (plgBottom9.Fill == plgCubeColor4.Fill)
            nNumberOfColors4++;

        if (plgBottom1.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgBottom2.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgBottom3.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgBottom4.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgBottom5.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgBottom6.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgBottom7.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgBottom8.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;
        if (plgBottom9.Fill == plgCubeColor5.Fill)
            nNumberOfColors5++;

        if (plgBottom1.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgBottom2.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgBottom3.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgBottom4.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgBottom5.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgBottom6.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgBottom7.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgBottom8.Fill == plgCubeColor6.Fill)
            nNumberOfColors6++;
        if (plgBottom9.Fill == plgCubeColor6.Fill)
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

        if (plgTop1.Fill == plgTop2.Fill && plgTop1.Fill == plgTop3.Fill && plgTop1.Fill == plgTop4.Fill && plgTop1.Fill == plgTop5.Fill && plgTop1.Fill == plgTop6.Fill && plgTop1.Fill == plgTop7.Fill && plgTop1.Fill == plgTop8.Fill && plgTop1.Fill == plgTop9.Fill)
        {
            bColorsTop = true;
        }

        if (PlgFront1.Fill == PlgFront2.Fill && PlgFront1.Fill == PlgFront3.Fill && PlgFront1.Fill == PlgFront4.Fill && PlgFront1.Fill == PlgFront5.Fill && PlgFront1.Fill == PlgFront6.Fill && PlgFront1.Fill == PlgFront7.Fill && PlgFront1.Fill == PlgFront8.Fill && PlgFront1.Fill == PlgFront9.Fill)
        {
            bColorsFront = true;
        }

        if (plgRight1.Fill == plgRight2.Fill && plgRight1.Fill == plgRight3.Fill && plgRight1.Fill == plgRight4.Fill && plgRight1.Fill == plgRight5.Fill && plgRight1.Fill == plgRight6.Fill && plgRight1.Fill == plgRight7.Fill && plgRight1.Fill == plgRight8.Fill && plgRight1.Fill == plgRight9.Fill)
        {
            bColorsRight = true;
        }

        if (plgLeft1.Fill == plgLeft2.Fill && plgLeft1.Fill == plgLeft3.Fill && plgLeft1.Fill == plgLeft4.Fill && plgLeft1.Fill == plgLeft5.Fill && plgLeft1.Fill == plgLeft6.Fill && plgLeft1.Fill == plgLeft7.Fill && plgLeft1.Fill == plgLeft8.Fill && plgLeft1.Fill == plgLeft9.Fill)
        {
            bColorsLeft = true;
        }

        if (plgBack1.Fill == plgBack2.Fill && plgBack1.Fill == plgBack3.Fill && plgBack1.Fill == plgBack4.Fill && plgBack1.Fill == plgBack5.Fill && plgBack1.Fill == plgBack6.Fill && plgBack1.Fill == plgBack7.Fill && plgBack1.Fill == plgBack8.Fill && plgBack1.Fill == plgBack9.Fill)
        {
            bColorsBack = true;
        }

        if (plgBottom1.Fill == plgBottom2.Fill && plgBottom1.Fill == plgBottom3.Fill && plgBottom1.Fill == plgBottom4.Fill && plgBottom1.Fill == plgBottom5.Fill && plgBottom1.Fill == plgBottom6.Fill && plgBottom1.Fill == plgBottom7.Fill && plgBottom1.Fill == plgBottom8.Fill && plgBottom1.Fill == plgBottom9.Fill)
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
    private void imgbtnTurnFrontSideToRight_Clicked(object sender, EventArgs e)
    {
        imgbtnTurnFrontSideToRight.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnFrontSideTo("+");
        ExplainTurnCube("Turn the front side 'clockwise' (+).");
        imgbtnTurnFrontSideToRight.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the top middle to the right side (+).
    private void imgbtnTurnTopMiddleToRightSide_Clicked(object sender, EventArgs e)
    {
        imgbtnTurnTopMiddleToRightSide.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnTopMiddleTo("+");
        ExplainTurnCube("Turn the top middle to the right side (+).");
        imgbtnTurnTopMiddleToRightSide.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the back side counter clockwise (to left -).
    private void imgbtnTurnBackSideToLeft_Clicked(object sender, EventArgs e)
    {
        imgbtnTurnBackSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnBackSideTo("-");
        ExplainTurnCube("Turn the back side 'counter clockwise' (-).");
        imgbtnTurnBackSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the left side clockwise (to right +).
    private void imgbtnTurnLeftSideToRight_Clicked(object sender, EventArgs e)
    {
        imgbtnTurnLeftSideToRight.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnLeftSideTo("+");
        ExplainTurnCube("Turn the left side 'clockwise' (+).");
        imgbtnTurnLeftSideToRight.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the top middle to the front side (-).
    private void imgbtnTurnTopMiddleToFrontSide_Clicked(object sender, EventArgs e)
    {
        imgbtnTurnTopMiddleToFrontSide.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnFrontTopMiddleTo("-");
        ExplainTurnCube("Turn the top middle to the front side (-).");
        imgbtnTurnTopMiddleToFrontSide.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the right side counter clockwise (to left -).
    private void imgbtnTurnRightSideToLeft_Clicked(object sender, EventArgs e)
    {
        imgbtnTurnRightSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnRightSideTo("-");
        ExplainTurnCube("Turn the right side 'counter clockwise' (-).");
        imgbtnTurnRightSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the top side counter clockwise (to left -).
    private void imgbtnTurnTopSideToLeft_Clicked(object sender, EventArgs e)
    {
        imgbtnTurnTopSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnTopSideTo("-");
        ExplainTurnCube("Turn the top side 'counter clockwise' (-).");
        imgbtnTurnTopSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the front middle to the right side (-).
    private void imgbtnTurnFrontMiddleToRightSide_Clicked(object sender, EventArgs e)
    {
        imgbtnTurnFrontMiddleToRightSide.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnHorizontalMiddleLayerTo("-");
        ExplainTurnCube("Turn the front middle to the right side (-).");
        imgbtnTurnFrontMiddleToRightSide.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the bottom side clockwise (to right +).
    private void imgbtnTurnBottomSideToRight_Clicked(object sender, EventArgs e)
    {
        imgbtnTurnBottomSideToRight.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnBottomSideTo("+");
        ExplainTurnCube("Turn the bottom side 'clockwise' (+).");
        imgbtnTurnBottomSideToRight.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the top side clockwise (to right +).
    private void imgbtnTurnTopSideToRight_Clicked(object sender, EventArgs e)
    {
        imgbtnTurnTopSideToRight.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnTopSideTo("+");
        ExplainTurnCube("Turn the top side 'clockwise' (+).");
        imgbtnTurnTopSideToRight.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the right middle to the front side (+).
    private void imgbtnTurnRightMiddleToFrontSide_Clicked(object sender, EventArgs e)
    {
        imgbtnTurnRightMiddleToFrontSide.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnHorizontalMiddleLayerTo("+");
        ExplainTurnCube("Turn the right middle to the front side (+).");
        imgbtnTurnRightMiddleToFrontSide.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the bottom side counter clockwise (to left -).
    private void imgbtnTurnBottomSideToLeft_Clicked(object sender, EventArgs e)
    {
        imgbtnTurnBottomSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnBottomSideTo("-");
        ExplainTurnCube("Turn the bottom side 'counter clockwise' (-).");
        imgbtnTurnBottomSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the left side counter clockwise (to left -).
    private void imgbtnTurnLeftSideToLeft_Clicked(object sender, EventArgs e)
    {
        imgbtnTurnLeftSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnLeftSideTo("-");
        ExplainTurnCube("Turn the left side 'counter clockwise' (-).");
        imgbtnTurnLeftSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the front middle to the top side (+).
    private void imgbtnTurnFrontMiddleToTopSide_Clicked(object sender, EventArgs e)
    {
        imgbtnTurnFrontMiddleToTopSide.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnFrontTopMiddleTo("+");
        ExplainTurnCube("Turn the front middle to the top side (+).");
        imgbtnTurnFrontMiddleToTopSide.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the right side clockwise (to right +).
    private void imgbtnTurnRightSideToRight_Clicked(object sender, EventArgs e)
    {
        imgbtnTurnRightSideToRight.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnRightSideTo("+");
        ExplainTurnCube("Turn the right side 'clockwise' (+).");
        imgbtnTurnRightSideToRight.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the front side counter clockwise (to left -).
    private void imgbtnTurnFrontSideToLeft_Clicked(object sender, EventArgs e)
    {
        imgbtnTurnFrontSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnFrontSideTo("-");
        ExplainTurnCube("Turn the front side 'counter clockwise' (-).");
        imgbtnTurnFrontSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the right middle to the top side (-).
    private void imgbtnTurnRightMiddleToTopSide_Clicked(object sender, EventArgs e)
    {
        imgbtnTurnRightMiddleToTopSide.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnTopMiddleTo("-");
        ExplainTurnCube("Turn the right middle to the top side (-).");
        imgbtnTurnRightMiddleToTopSide.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
    }

    // Turn the back side clockwise (to right +).
    private void imgbtnTurnBackSideToRight_Clicked(object sender, EventArgs e)
    {
        imgbtnTurnBackSideToRight.BackgroundColor = Color.FromArgb(cColorArrowActive);
        TurnBackSideTo("+");
        ExplainTurnCube("Turn the back side 'clockwise' (+).");
        imgbtnTurnBackSideToRight.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
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

        Brush ColorTop7 = plgTop7.Fill;
        Brush ColorTop8 = plgTop8.Fill;
        Brush ColorTop9 = plgTop9.Fill;

        Brush ColorRight1 = plgRight1.Fill;
        Brush ColorRight4 = plgRight4.Fill;
        Brush ColorRight7 = plgRight7.Fill;

        Brush ColorBottom1 = plgBottom1.Fill;
        Brush ColorBottom2 = plgBottom2.Fill;
        Brush ColorBottom3 = plgBottom3.Fill;

        Brush ColorLeft3 = plgLeft3.Fill;
        Brush ColorLeft6 = plgLeft6.Fill;
        Brush ColorLeft9 = plgLeft9.Fill;

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

            plgTop7.Fill = ColorLeft9;
            plgTop8.Fill = ColorLeft6;
            plgTop9.Fill = ColorLeft3;

            plgRight1.Fill = ColorTop7;
            plgRight4.Fill = ColorTop8;
            plgRight7.Fill = ColorTop9;

            plgBottom1.Fill = ColorRight7;
            plgBottom2.Fill = ColorRight4;
            plgBottom3.Fill = ColorRight1;

            plgLeft3.Fill = ColorBottom1;
            plgLeft6.Fill = ColorBottom2;
            plgLeft9.Fill = ColorBottom3;
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

            plgTop7.Fill = ColorRight1;
            plgTop8.Fill = ColorRight4;
            plgTop9.Fill = ColorRight7;

            plgRight1.Fill = ColorBottom3;
            plgRight4.Fill = ColorBottom2;
            plgRight7.Fill = ColorBottom1;

            plgBottom1.Fill = ColorLeft3;
            plgBottom2.Fill = ColorLeft6;
            plgBottom3.Fill = ColorLeft9;

            plgLeft3.Fill = ColorTop9;
            plgLeft6.Fill = ColorTop8;
            plgLeft9.Fill = ColorTop7;
        }
    }

    // Turn the top middle to the right or left.
    private void TurnTopMiddleTo(string cDirection)
    {
        Brush ColorTop4 = plgTop4.Fill;
        Brush ColorTop5 = plgTop5.Fill;
        Brush ColorTop6 = plgTop6.Fill;

        Brush ColorRight2 = plgRight2.Fill;
        Brush ColorRight5 = plgRight5.Fill;
        Brush ColorRight8 = plgRight8.Fill;

        Brush ColorBottom4 = plgBottom4.Fill;
        Brush ColorBottom5 = plgBottom5.Fill;
        Brush ColorBottom6 = plgBottom6.Fill;

        Brush ColorLeft2 = plgLeft2.Fill;
        Brush ColorLeft5 = plgLeft5.Fill;
        Brush ColorLeft8 = plgLeft8.Fill;

        if (cDirection == "+")
        {
            plgTop4.Fill = ColorLeft8;
            plgTop5.Fill = ColorLeft5;
            plgTop6.Fill = ColorLeft2;

            plgRight2.Fill = ColorTop4;
            plgRight5.Fill = ColorTop5;
            plgRight8.Fill = ColorTop6;

            plgBottom4.Fill = ColorRight8;
            plgBottom5.Fill = ColorRight5;
            plgBottom6.Fill = ColorRight2;

            plgLeft2.Fill = ColorBottom4;
            plgLeft5.Fill = ColorBottom5;
            plgLeft8.Fill = ColorBottom6;
        }

        if (cDirection == "-")
        {
            plgTop4.Fill = ColorRight2;
            plgTop5.Fill = ColorRight5;
            plgTop6.Fill = ColorRight8;

            plgRight2.Fill = ColorBottom6;
            plgRight5.Fill = ColorBottom5;
            plgRight8.Fill = ColorBottom4;

            plgBottom4.Fill = ColorLeft2;
            plgBottom5.Fill = ColorLeft5;
            plgBottom6.Fill = ColorLeft8;

            plgLeft2.Fill = ColorTop6;
            plgLeft5.Fill = ColorTop5;
            plgLeft8.Fill = ColorTop4;
        }
    }

    // Turn the entire back side clockwise or counter clockwise.
    private void TurnBackSideTo(string cDirection)
    {
        Brush ColorBack1 = plgBack1.Fill;
        Brush ColorBack2 = plgBack2.Fill;
        Brush ColorBack3 = plgBack3.Fill;
        Brush ColorBack4 = plgBack4.Fill;
        Brush ColorBack6 = plgBack6.Fill;
        Brush ColorBack7 = plgBack7.Fill;
        Brush ColorBack8 = plgBack8.Fill;
        Brush ColorBack9 = plgBack9.Fill;

        Brush ColorTop1 = plgTop1.Fill;
        Brush ColorTop2 = plgTop2.Fill;
        Brush ColorTop3 = plgTop3.Fill;

        Brush ColorRight3 = plgRight3.Fill;
        Brush ColorRight6 = plgRight6.Fill;
        Brush ColorRight9 = plgRight9.Fill;

        Brush ColorBottom7 = plgBottom7.Fill;
        Brush ColorBottom8 = plgBottom8.Fill;
        Brush ColorBottom9 = plgBottom9.Fill;

        Brush ColorLeft1 = plgLeft1.Fill;
        Brush ColorLeft4 = plgLeft4.Fill;
        Brush ColorLeft7 = plgLeft7.Fill;

        if (cDirection == "+")
        {
            plgBack1.Fill = ColorBack7;
            plgBack2.Fill = ColorBack4;
            plgBack3.Fill = ColorBack1;
            plgBack4.Fill = ColorBack8;
            plgBack6.Fill = ColorBack2;
            plgBack7.Fill = ColorBack9;
            plgBack8.Fill = ColorBack6;
            plgBack9.Fill = ColorBack3;

            plgTop1.Fill = ColorRight3;
            plgTop2.Fill = ColorRight6;
            plgTop3.Fill = ColorRight9;

            plgRight3.Fill = ColorBottom9;
            plgRight6.Fill = ColorBottom8;
            plgRight9.Fill = ColorBottom7;

            plgBottom7.Fill = ColorLeft1;
            plgBottom8.Fill = ColorLeft4;
            plgBottom9.Fill = ColorLeft7;

            plgLeft1.Fill = ColorTop3;
            plgLeft4.Fill = ColorTop2;
            plgLeft7.Fill = ColorTop1;
        }

        if (cDirection == "-")
        {
            plgBack1.Fill = ColorBack3;
            plgBack2.Fill = ColorBack6;
            plgBack3.Fill = ColorBack9;
            plgBack4.Fill = ColorBack2;
            plgBack6.Fill = ColorBack8;
            plgBack7.Fill = ColorBack1;
            plgBack8.Fill = ColorBack4;
            plgBack9.Fill = ColorBack7;

            plgTop1.Fill = ColorLeft7;
            plgTop2.Fill = ColorLeft4;
            plgTop3.Fill = ColorLeft1;

            plgRight3.Fill = ColorTop1;
            plgRight6.Fill = ColorTop2;
            plgRight9.Fill = ColorTop3;

            plgBottom7.Fill = ColorRight9;
            plgBottom8.Fill = ColorRight6;
            plgBottom9.Fill = ColorRight3;

            plgLeft1.Fill = ColorBottom7;
            plgLeft4.Fill = ColorBottom8;
            plgLeft7.Fill = ColorBottom9;
        }
    }

    // Turn the entire left side clockwise or counter clockwise.
    private void TurnLeftSideTo(string cDirection)
    {
        Brush ColorLeft1 = plgLeft1.Fill;
        Brush ColorLeft2 = plgLeft2.Fill;
        Brush ColorLeft3 = plgLeft3.Fill;
        Brush ColorLeft4 = plgLeft4.Fill;
        Brush ColorLeft6 = plgLeft6.Fill;
        Brush ColorLeft7 = plgLeft7.Fill;
        Brush ColorLeft8 = plgLeft8.Fill;
        Brush ColorLeft9 = plgLeft9.Fill;

        Brush ColorTop1 = plgTop1.Fill;
        Brush ColorTop4 = plgTop4.Fill;
        Brush ColorTop7 = plgTop7.Fill;

        Brush ColorFront1 = PlgFront1.Fill;
        Brush ColorFront4 = PlgFront4.Fill;
        Brush ColorFront7 = PlgFront7.Fill;

        Brush ColorBottom1 = plgBottom1.Fill;
        Brush ColorBottom4 = plgBottom4.Fill;
        Brush ColorBottom7 = plgBottom7.Fill;

        Brush ColorBack3 = plgBack3.Fill;
        Brush ColorBack6 = plgBack6.Fill;
        Brush ColorBack9 = plgBack9.Fill;

        if (cDirection == "+")
        {
            plgLeft1.Fill = ColorLeft7;
            plgLeft2.Fill = ColorLeft4;
            plgLeft3.Fill = ColorLeft1;
            plgLeft4.Fill = ColorLeft8;
            plgLeft6.Fill = ColorLeft2;
            plgLeft7.Fill = ColorLeft9;
            plgLeft8.Fill = ColorLeft6;
            plgLeft9.Fill = ColorLeft3;

            plgTop1.Fill = ColorBack9;
            plgTop4.Fill = ColorBack6;
            plgTop7.Fill = ColorBack3;

            PlgFront1.Fill = ColorTop1;
            PlgFront4.Fill = ColorTop4;
            PlgFront7.Fill = ColorTop7;

            plgBottom1.Fill = ColorFront1;
            plgBottom4.Fill = ColorFront4;
            plgBottom7.Fill = ColorFront7;

            plgBack3.Fill = ColorBottom7;
            plgBack6.Fill = ColorBottom4;
            plgBack9.Fill = ColorBottom1;
        }

        if (cDirection == "-")
        {
            plgLeft1.Fill = ColorLeft3;
            plgLeft2.Fill = ColorLeft6;
            plgLeft3.Fill = ColorLeft9;
            plgLeft4.Fill = ColorLeft2;
            plgLeft6.Fill = ColorLeft8;
            plgLeft7.Fill = ColorLeft1;
            plgLeft8.Fill = ColorLeft4;
            plgLeft9.Fill = ColorLeft7;

            plgTop1.Fill = ColorFront1;
            plgTop4.Fill = ColorFront4;
            plgTop7.Fill = ColorFront7;

            PlgFront1.Fill = ColorBottom1;
            PlgFront4.Fill = ColorBottom4;
            PlgFront7.Fill = ColorBottom7;

            plgBottom1.Fill = ColorBack9;
            plgBottom4.Fill = ColorBack6;
            plgBottom7.Fill = ColorBack3;

            plgBack3.Fill = ColorTop7;
            plgBack6.Fill = ColorTop4;
            plgBack9.Fill = ColorTop1;
        }
    }

    // Turn the top middle layer to right or left.
    private void TurnFrontTopMiddleTo(string cDirection)
    {
        Brush ColorTop2 = plgTop2.Fill;
        Brush ColorTop5 = plgTop5.Fill;
        Brush ColorTop8 = plgTop8.Fill;

        Brush ColorFront2 = PlgFront2.Fill;
        Brush ColorFront5 = PlgFront5.Fill;
        Brush ColorFront8 = PlgFront8.Fill;

        Brush ColorBottom2 = plgBottom2.Fill;
        Brush ColorBottom5 = plgBottom5.Fill;
        Brush ColorBottom8 = plgBottom8.Fill;

        Brush ColorBack2 = plgBack2.Fill;
        Brush ColorBack5 = plgBack5.Fill;
        Brush ColorBack8 = plgBack8.Fill;

        if (cDirection == "+")
        {
            plgTop2.Fill = ColorFront2;
            plgTop5.Fill = ColorFront5;
            plgTop8.Fill = ColorFront8;

            PlgFront2.Fill = ColorBottom2;
            PlgFront5.Fill = ColorBottom5;
            PlgFront8.Fill = ColorBottom8;

            plgBottom2.Fill = ColorBack8;
            plgBottom5.Fill = ColorBack5;
            plgBottom8.Fill = ColorBack2;

            plgBack2.Fill = ColorTop8;
            plgBack5.Fill = ColorTop5;
            plgBack8.Fill = ColorTop2;
        }

        if (cDirection == "-")
        {
            plgTop2.Fill = ColorBack8;
            plgTop5.Fill = ColorBack5;
            plgTop8.Fill = ColorBack2;

            PlgFront2.Fill = ColorTop2;
            PlgFront5.Fill = ColorTop5;
            PlgFront8.Fill = ColorTop8;

            plgBottom2.Fill = ColorFront2;
            plgBottom5.Fill = ColorFront5;
            plgBottom8.Fill = ColorFront8;

            plgBack2.Fill = ColorBottom8;
            plgBack5.Fill = ColorBottom5;
            plgBack8.Fill = ColorBottom2;
        }
    }

    // Turn the entire right side clockwise or counter clockwise.
    private void TurnRightSideTo(string cDirection)
    {
        Brush ColorRight1 = plgRight1.Fill;
        Brush ColorRight2 = plgRight2.Fill;
        Brush ColorRight3 = plgRight3.Fill;
        Brush ColorRight4 = plgRight4.Fill;
        Brush ColorRight6 = plgRight6.Fill;
        Brush ColorRight7 = plgRight7.Fill;
        Brush ColorRight8 = plgRight8.Fill;
        Brush ColorRight9 = plgRight9.Fill;

        Brush ColorTop3 = plgTop3.Fill;
        Brush ColorTop6 = plgTop6.Fill;
        Brush ColorTop9 = plgTop9.Fill;

        Brush ColorFront3 = PlgFront3.Fill;
        Brush ColorFront6 = PlgFront6.Fill;
        Brush ColorFront9 = PlgFront9.Fill;

        Brush ColorBottom3 = plgBottom3.Fill;
        Brush ColorBottom6 = plgBottom6.Fill;
        Brush ColorBottom9 = plgBottom9.Fill;

        Brush ColorBack1 = plgBack1.Fill;
        Brush ColorBack4 = plgBack4.Fill;
        Brush ColorBack7 = plgBack7.Fill;

        if (cDirection == "+")
        {
            plgRight1.Fill = ColorRight7;
            plgRight2.Fill = ColorRight4;
            plgRight3.Fill = ColorRight1;
            plgRight4.Fill = ColorRight8;
            plgRight6.Fill = ColorRight2;
            plgRight7.Fill = ColorRight9;
            plgRight8.Fill = ColorRight6;
            plgRight9.Fill = ColorRight3;

            plgTop3.Fill = ColorFront3;
            plgTop6.Fill = ColorFront6;
            plgTop9.Fill = ColorFront9;

            PlgFront3.Fill = ColorBottom3;
            PlgFront6.Fill = ColorBottom6;
            PlgFront9.Fill = ColorBottom9;

            plgBottom3.Fill = ColorBack7;
            plgBottom6.Fill = ColorBack4;
            plgBottom9.Fill = ColorBack1;

            plgBack1.Fill = ColorTop9;
            plgBack4.Fill = ColorTop6;
            plgBack7.Fill = ColorTop3;
        }

        if (cDirection == "-")
        {
            plgRight1.Fill = ColorRight3;
            plgRight2.Fill = ColorRight6;
            plgRight3.Fill = ColorRight9;
            plgRight4.Fill = ColorRight2;
            plgRight6.Fill = ColorRight8;
            plgRight7.Fill = ColorRight1;
            plgRight8.Fill = ColorRight4;
            plgRight9.Fill = ColorRight7;

            plgTop3.Fill = ColorBack7;
            plgTop6.Fill = ColorBack4;
            plgTop9.Fill = ColorBack1;

            PlgFront3.Fill = ColorTop3;
            PlgFront6.Fill = ColorTop6;
            PlgFront9.Fill = ColorTop9;

            plgBottom3.Fill = ColorFront3;
            plgBottom6.Fill = ColorFront6;
            plgBottom9.Fill = ColorFront9;

            plgBack1.Fill = ColorBottom9;
            plgBack4.Fill = ColorBottom6;
            plgBack7.Fill = ColorBottom3;
        }
    }

    // Turn the entire top side clockwise or counter clockwise.
    private void TurnTopSideTo(string cDirection)
    {
        Brush ColorTop1 = plgTop1.Fill;
        Brush ColorTop2 = plgTop2.Fill;
        Brush ColorTop3 = plgTop3.Fill;
        Brush ColorTop4 = plgTop4.Fill;
        Brush ColorTop6 = plgTop6.Fill;
        Brush ColorTop7 = plgTop7.Fill;
        Brush ColorTop8 = plgTop8.Fill;
        Brush ColorTop9 = plgTop9.Fill;

        Brush ColorLeft1 = plgLeft1.Fill;
        Brush ColorLeft2 = plgLeft2.Fill;
        Brush ColorLeft3 = plgLeft3.Fill;

        Brush ColorFront1 = PlgFront1.Fill;
        Brush ColorFront2 = PlgFront2.Fill;
        Brush ColorFront3 = PlgFront3.Fill;

        Brush ColorRight1 = plgRight1.Fill;
        Brush ColorRight2 = plgRight2.Fill;
        Brush ColorRight3 = plgRight3.Fill;

        Brush ColorBack1 = plgBack1.Fill;
        Brush ColorBack2 = plgBack2.Fill;
        Brush ColorBack3 = plgBack3.Fill;

        if (cDirection == "+")
        {
            plgTop1.Fill = ColorTop7;
            plgTop2.Fill = ColorTop4;
            plgTop3.Fill = ColorTop1;
            plgTop4.Fill = ColorTop8;
            plgTop6.Fill = ColorTop2;
            plgTop7.Fill = ColorTop9;
            plgTop8.Fill = ColorTop6;
            plgTop9.Fill = ColorTop3;

            plgLeft1.Fill = ColorFront1;
            plgLeft2.Fill = ColorFront2;
            plgLeft3.Fill = ColorFront3;

            PlgFront1.Fill = ColorRight1;
            PlgFront2.Fill = ColorRight2;
            PlgFront3.Fill = ColorRight3;

            plgRight1.Fill = ColorBack1;
            plgRight2.Fill = ColorBack2;
            plgRight3.Fill = ColorBack3;

            plgBack1.Fill = ColorLeft1;
            plgBack2.Fill = ColorLeft2;
            plgBack3.Fill = ColorLeft3;
        }

        if (cDirection == "-")
        {
            plgTop1.Fill = ColorTop3;
            plgTop2.Fill = ColorTop6;
            plgTop3.Fill = ColorTop9;
            plgTop4.Fill = ColorTop2;
            plgTop6.Fill = ColorTop8;
            plgTop7.Fill = ColorTop1;
            plgTop8.Fill = ColorTop4;
            plgTop9.Fill = ColorTop7;

            plgLeft1.Fill = ColorBack1;
            plgLeft2.Fill = ColorBack2;
            plgLeft3.Fill = ColorBack3;

            PlgFront1.Fill = ColorLeft1;
            PlgFront2.Fill = ColorLeft2;
            PlgFront3.Fill = ColorLeft3;

            plgRight1.Fill = ColorFront1;
            plgRight2.Fill = ColorFront2;
            plgRight3.Fill = ColorFront3;

            plgBack1.Fill = ColorRight1;
            plgBack2.Fill = ColorRight2;
            plgBack3.Fill = ColorRight3;
        }
    }

    // Turn the horizontal middle layer to right or left.
    private void TurnHorizontalMiddleLayerTo(string cDirection)
    {
        Brush ColorFront4 = PlgFront4.Fill;
        Brush ColorFront5 = PlgFront5.Fill;
        Brush ColorFront6 = PlgFront6.Fill;

        Brush ColorRight4 = plgRight4.Fill;
        Brush ColorRight5 = plgRight5.Fill;
        Brush ColorRight6 = plgRight6.Fill;

        Brush ColorBack4 = plgBack4.Fill;
        Brush ColorBack5 = plgBack5.Fill;
        Brush ColorBack6 = plgBack6.Fill;

        Brush ColorLeft4 = plgLeft4.Fill;
        Brush ColorLeft5 = plgLeft5.Fill;
        Brush ColorLeft6 = plgLeft6.Fill;

        if (cDirection == "+")
        {
            PlgFront4.Fill = ColorRight4;
            PlgFront5.Fill = ColorRight5;
            PlgFront6.Fill = ColorRight6;

            plgRight4.Fill = ColorBack4;
            plgRight5.Fill = ColorBack5;
            plgRight6.Fill = ColorBack6;

            plgBack4.Fill = ColorLeft4;
            plgBack5.Fill = ColorLeft5;
            plgBack6.Fill = ColorLeft6;

            plgLeft4.Fill = ColorFront4;
            plgLeft5.Fill = ColorFront5;
            plgLeft6.Fill = ColorFront6;
        }

        if (cDirection == "-")
        {
            PlgFront4.Fill = ColorLeft4;
            PlgFront5.Fill = ColorLeft5;
            PlgFront6.Fill = ColorLeft6;

            plgRight4.Fill = ColorFront4;
            plgRight5.Fill = ColorFront5;
            plgRight6.Fill = ColorFront6;

            plgBack4.Fill = ColorRight4;
            plgBack5.Fill = ColorRight5;
            plgBack6.Fill = ColorRight6;

            plgLeft4.Fill = ColorBack4;
            plgLeft5.Fill = ColorBack5;
            plgLeft6.Fill = ColorBack6;
        }
    }

    // Turn the entire bottom side clockwise or counter clockwise.
    private void TurnBottomSideTo(string cDirection)
    {
        Brush ColorBottom1 = plgBottom1.Fill;
        Brush ColorBottom2 = plgBottom2.Fill;
        Brush ColorBottom3 = plgBottom3.Fill;
        Brush ColorBottom4 = plgBottom4.Fill;
        Brush ColorBottom6 = plgBottom6.Fill;
        Brush ColorBottom7 = plgBottom7.Fill;
        Brush ColorBottom8 = plgBottom8.Fill;
        Brush ColorBottom9 = plgBottom9.Fill;

        Brush ColorLeft7 = plgLeft7.Fill;
        Brush ColorLeft8 = plgLeft8.Fill;
        Brush ColorLeft9 = plgLeft9.Fill;

        Brush ColorFront7 = PlgFront7.Fill;
        Brush ColorFront8 = PlgFront8.Fill;
        Brush ColorFront9 = PlgFront9.Fill;

        Brush ColorRight7 = plgRight7.Fill;
        Brush ColorRight8 = plgRight8.Fill;
        Brush ColorRight9 = plgRight9.Fill;

        Brush ColorBack7 = plgBack7.Fill;
        Brush ColorBack8 = plgBack8.Fill;
        Brush ColorBack9 = plgBack9.Fill;

        if (cDirection == "+")
        {
            plgBottom1.Fill = ColorBottom7;
            plgBottom2.Fill = ColorBottom4;
            plgBottom3.Fill = ColorBottom1;
            plgBottom4.Fill = ColorBottom8;
            plgBottom6.Fill = ColorBottom2;
            plgBottom7.Fill = ColorBottom9;
            plgBottom8.Fill = ColorBottom6;
            plgBottom9.Fill = ColorBottom3;

            plgLeft7.Fill = ColorBack7;
            plgLeft8.Fill = ColorBack8;
            plgLeft9.Fill = ColorBack9;

            PlgFront7.Fill = ColorLeft7;
            PlgFront8.Fill = ColorLeft8;
            PlgFront9.Fill = ColorLeft9;

            plgRight7.Fill = ColorFront7;
            plgRight8.Fill = ColorFront8;
            plgRight9.Fill = ColorFront9;

            plgBack7.Fill = ColorRight7;
            plgBack8.Fill = ColorRight8;
            plgBack9.Fill = ColorRight9;
        }

        if (cDirection == "-")
        {
            plgBottom1.Fill = ColorBottom3;
            plgBottom2.Fill = ColorBottom6;
            plgBottom3.Fill = ColorBottom9;
            plgBottom4.Fill = ColorBottom2;
            plgBottom6.Fill = ColorBottom8;
            plgBottom7.Fill = ColorBottom1;
            plgBottom8.Fill = ColorBottom4;
            plgBottom9.Fill = ColorBottom7;

            plgLeft7.Fill = ColorFront7;
            plgLeft8.Fill = ColorFront8;
            plgLeft9.Fill = ColorFront9;

            PlgFront7.Fill = ColorRight7;
            PlgFront8.Fill = ColorRight8;
            PlgFront9.Fill = ColorRight9;

            plgRight7.Fill = ColorBack7;
            plgRight8.Fill = ColorBack8;
            plgRight9.Fill = ColorBack9;

            plgBack7.Fill = ColorLeft7;
            plgBack8.Fill = ColorLeft8;
            plgBack9.Fill = ColorLeft9;
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
        plgTop1.Fill = plgCubeColor1.Fill;
        plgTop2.Fill = plgCubeColor1.Fill;
        plgTop3.Fill = plgCubeColor1.Fill;
        plgTop4.Fill = plgCubeColor1.Fill;
        plgTop5.Fill = plgCubeColor1.Fill;
        plgTop6.Fill = plgCubeColor1.Fill;
        plgTop7.Fill = plgCubeColor1.Fill;
        plgTop8.Fill = plgCubeColor1.Fill;
        plgTop9.Fill = plgCubeColor1.Fill;

        PlgFront1.Fill = plgCubeColor2.Fill;
        PlgFront2.Fill = plgCubeColor2.Fill;
        PlgFront3.Fill = plgCubeColor2.Fill;
        PlgFront4.Fill = plgCubeColor2.Fill;
        PlgFront5.Fill = plgCubeColor2.Fill;
        PlgFront6.Fill = plgCubeColor2.Fill;
        PlgFront7.Fill = plgCubeColor2.Fill;
        PlgFront8.Fill = plgCubeColor2.Fill;
        PlgFront9.Fill = plgCubeColor2.Fill;

        plgRight1.Fill = plgCubeColor3.Fill;
        plgRight2.Fill = plgCubeColor3.Fill;
        plgRight3.Fill = plgCubeColor3.Fill;
        plgRight4.Fill = plgCubeColor3.Fill;
        plgRight5.Fill = plgCubeColor3.Fill;
        plgRight6.Fill = plgCubeColor3.Fill;
        plgRight7.Fill = plgCubeColor3.Fill;
        plgRight8.Fill = plgCubeColor3.Fill;
        plgRight9.Fill = plgCubeColor3.Fill;

        plgLeft1.Fill = plgCubeColor4.Fill;
        plgLeft2.Fill = plgCubeColor4.Fill;
        plgLeft3.Fill = plgCubeColor4.Fill;
        plgLeft4.Fill = plgCubeColor4.Fill;
        plgLeft5.Fill = plgCubeColor4.Fill;
        plgLeft6.Fill = plgCubeColor4.Fill;
        plgLeft7.Fill = plgCubeColor4.Fill;
        plgLeft8.Fill = plgCubeColor4.Fill;
        plgLeft9.Fill = plgCubeColor4.Fill;

        plgBack1.Fill = plgCubeColor5.Fill;
        plgBack2.Fill = plgCubeColor5.Fill;
        plgBack3.Fill = plgCubeColor5.Fill;
        plgBack4.Fill = plgCubeColor5.Fill;
        plgBack5.Fill = plgCubeColor5.Fill;
        plgBack6.Fill = plgCubeColor5.Fill;
        plgBack7.Fill = plgCubeColor5.Fill;
        plgBack8.Fill = plgCubeColor5.Fill;
        plgBack9.Fill = plgCubeColor5.Fill;

        plgBottom1.Fill = plgCubeColor6.Fill;
        plgBottom2.Fill = plgCubeColor6.Fill;
        plgBottom3.Fill = plgCubeColor6.Fill;
        plgBottom4.Fill = plgCubeColor6.Fill;
        plgBottom5.Fill = plgCubeColor6.Fill;
        plgBottom6.Fill = plgCubeColor6.Fill;
        plgBottom7.Fill = plgCubeColor6.Fill;
        plgBottom8.Fill = plgCubeColor6.Fill;
        plgBottom9.Fill = plgCubeColor6.Fill;
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
                imgbtnAbout.IsEnabled = false;
                imgbtnSettings.IsEnabled = false;

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

        lblTitle.Text = CubeLang.RubiksCube_Text;

        cButtonClose = CubeLang.ButtonClose_Text;
        cErrorTitle = CubeLang.ErrorTitle_Text;
        cLicenseTitle = CubeLang.LicenseTitle_Text;
        cLicense = CubeLang.License_Text + "\n\n" + CubeLang.LicenseMit2_Text;
        cAgree = CubeLang.Agree_Text;
        cDisagree = CubeLang.Disagree_Text;
        cCloseApplication = CubeLang.CloseApplication_Text;
        cTextToSpeechError = CubeLang.TextToSpeechError_Text;

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
