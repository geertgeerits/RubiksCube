// Program .....: RubiksCube.sln
// Author ......: Geert Geerits - E-mail: geertgeerits@gmail.com
// Copyright ...: (C) 1981-2023
// Version .....: 2.0.11
// Date ........: 2023-05-08 (YYYY-MM-DD)
// Language ....: Microsoft Visual Studio 2022: .NET MAUI C# 11.0
// Description .: Solving the Rubik's Cube
// Note ........: This program is based on the program 'SolCube' I wrote in 1981 in MS Basic-80 for a Commodore PET 2001.
//                The solution for solving the cube is based on a book by Don Taylor, Mastering Rubik's Cube, Dutch version 1981.
// Dependencies : 
// Thanks to ...: Gerald Versluis

using Microsoft.Maui.Controls.Shapes;
using RubiksCube.Resources.Languages;
using System.Globalization;

namespace RubiksCube;

public partial class MainPage : ContentPage
{
    // Global variables for all pages part of Rubik's Cube.
    public static string cTheme;
    public static string cLanguage;
    public static bool bLanguageChanged = false;
    public static string cLanguageSpeech;
    public static string[] cLanguageLocales;
    public static bool bLanguageLocalesExist = false;
    public static bool bExplainText = false;
    public static bool bExplainSpeech = false;
    public static string cCubeColor1;
    public static string cCubeColor2;
    public static string cCubeColor3;
    public static string cCubeColor4;
    public static string cCubeColor5;
    public static string cCubeColor6;

    // Local variables.
    private readonly bool bLicense;
    private IEnumerable<Locale> locales;
    private CancellationTokenSource cts;
    private bool bTextToSpeechIsBusy = false;
    private bool bColorDrop = false;
    private readonly string cColorArrowNotActive = "#E2E2E2";    // Lightgray
    private readonly string cColorArrowActive = "#FFD000";       // Light orange
    private readonly string[] aCubeColors = new string[7];
    private readonly string[] aTopSide = new string[10];
    private readonly string[] aFrontSide = new string[10];
    private readonly string[] aRightSide = new string[10];
    private readonly string[] aLeftSide = new string[10];
    private readonly string[] aBackSide = new string[10];
    private readonly string[] aBottomSide = new string[10];

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
        cCubeColor1 = Preferences.Default.Get("SettingCubeColor1", "#FF0000");   // Red
        cCubeColor2 = Preferences.Default.Get("SettingCubeColor2", "#FFFF00");   // Yellow
        cCubeColor3 = Preferences.Default.Get("SettingCubeColor3", "#0000FF");   // Blue
        cCubeColor4 = Preferences.Default.Get("SettingCubeColor4", "#008000");   // Green
        cCubeColor5 = Preferences.Default.Get("SettingCubeColor5", "#FFFFFF");   // White
        cCubeColor6 = Preferences.Default.Get("SettingCubeColor6", "#FF8000");   // Orange

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
        aCubeColors[1] = cCubeColor1;
        aCubeColors[2] = cCubeColor2;
        aCubeColors[3] = cCubeColor3;
        aCubeColors[4] = cCubeColor4;
        aCubeColors[5] = cCubeColor5;
        aCubeColors[6] = cCubeColor6;

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

    // Select a color for dropping on a cube and put it in a tempory polygon.
    private void OnColorDragStarting(object sender, DragStartingEventArgs e)
    {
        Polygon polygon = (sender as Element).Parent as Polygon;
        plgCubeColorSelect.Fill = polygon.Fill;        
    }

    // Drop the selected color on the cube and fill the cube with the color of the tempory polygon.
    private void OnColorDrop(object sender, DropEventArgs e)
    {
        Polygon polygon = (sender as Element).Parent as Polygon;
        polygon.Fill = plgCubeColorSelect.Fill;
        
        plgCubeColorSelect.Fill = Color.FromArgb("#000000");
        SetCubeColorsInArrays();
    }
    
    // Drag and drop colors on the cube.
    private void OnButtonSetColorsClicked(object sender, EventArgs e)
    {
        bColorDrop = !bColorDrop;

        if (bColorDrop)
        {
            BtnSolve.IsEnabled= false;
            
            hslCubeColorSelect.BackgroundColor = Color.FromArgb("#969696");
            IsVisibleCubeColors(true);
            IsEnabledArrows(false);

            imgbtnTurnTopMiddleToRightSide.IsEnabled = true;
            imgbtnTurnTopMiddleToFrontSide.IsEnabled = true;
            imgbtnTurnFrontMiddleToRightSide.IsEnabled = true;
            imgbtnTurnRightMiddleToFrontSide.IsEnabled = true;
            imgbtnTurnFrontMiddleToTopSide.IsEnabled = true;
            imgbtnTurnRightMiddleToTopSide.IsEnabled = true;

            imgbtnTurnTopMiddleToRightSide.BackgroundColor = Color.FromArgb(cColorArrowActive);
            imgbtnTurnTopMiddleToFrontSide.BackgroundColor = Color.FromArgb(cColorArrowActive);
            imgbtnTurnFrontMiddleToRightSide.BackgroundColor = Color.FromArgb(cColorArrowActive);
            imgbtnTurnRightMiddleToFrontSide.BackgroundColor = Color.FromArgb(cColorArrowActive);
            imgbtnTurnFrontMiddleToTopSide.BackgroundColor = Color.FromArgb(cColorArrowActive);
            imgbtnTurnRightMiddleToTopSide.BackgroundColor = Color.FromArgb(cColorArrowActive);

            ToolTipProperties.SetText(imgbtnTurnTopMiddleToRightSide, CubeLang.TurnCubeTopSideToRightSide_Text);
            ToolTipProperties.SetText(imgbtnTurnTopMiddleToFrontSide, CubeLang.TurnCubeFrontSideToBottomSide_Text);
            ToolTipProperties.SetText(imgbtnTurnFrontMiddleToRightSide, CubeLang.TurnCubeFrontSideToRightSide_Text);
            ToolTipProperties.SetText(imgbtnTurnRightMiddleToFrontSide, CubeLang.TurnCubeFrontSideToLeftSide_Text);
            ToolTipProperties.SetText(imgbtnTurnFrontMiddleToTopSide, CubeLang.TurnCubeFrontSideToTopSide_Text);
            ToolTipProperties.SetText(imgbtnTurnRightMiddleToTopSide, CubeLang.TurnCubeTopSideToLeftSide_Text);
        }
        else
        {
            if (!CheckNumberColorsCube())
            {
                bColorDrop = true;
                return;
            }

            imgbtnTurnTopMiddleToRightSide.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
            imgbtnTurnTopMiddleToFrontSide.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
            imgbtnTurnFrontMiddleToRightSide.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
            imgbtnTurnRightMiddleToFrontSide.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
            imgbtnTurnFrontMiddleToTopSide.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
            imgbtnTurnRightMiddleToTopSide.BackgroundColor = Color.FromArgb(cColorArrowNotActive);

            ToolTipProperties.SetText(imgbtnTurnTopMiddleToRightSide, CubeLang.TurnTopMiddleToRightSide_Text);
            ToolTipProperties.SetText(imgbtnTurnTopMiddleToFrontSide, CubeLang.TurnTopMiddleToFrontSide_Text);
            ToolTipProperties.SetText(imgbtnTurnFrontMiddleToRightSide, CubeLang.TurnFrontMiddleToRightSide_Text);
            ToolTipProperties.SetText(imgbtnTurnRightMiddleToFrontSide, CubeLang.TurnRightMiddleToFrontSide_Text);
            ToolTipProperties.SetText(imgbtnTurnFrontMiddleToTopSide, CubeLang.TurnFrontMiddleToTopSide_Text);
            ToolTipProperties.SetText(imgbtnTurnRightMiddleToTopSide, CubeLang.TurnRightMiddleToTopSide_Text);

            IsEnabledArrows(true);
            IsVisibleCubeColors(false);
            hslCubeColorSelect.BackgroundColor = Color.FromArgb("#00000000");

            BtnSolve.IsEnabled = true;
        }
    }

    // Solve the cube.
    private async void OnBtnSolveClicked(object sender, EventArgs e)
    {
        if (!CheckNumberColorsCube())
        {
            return;
        }

        bColorDrop = false;

        //IsEnabledArrows(false);
        IsVisibleArrows(false);

        bool bExplainTextSaved = bExplainText;
        bExplainText = false;
        
        await SolveTheCube();
        
        bExplainText = bExplainTextSaved;

        //IsEnabledArrows(true);
        IsVisibleArrows(true);
    }

    // Solve the cube.
    private async Task SolveTheCube()
    {
        // Solve the edges of the top layer - Chapter 4, page 14-3.
        if (await SolveEdgesTopLayer() == false)
        {
            return;
        }

        // Solve the edges of the top layer - Chapter 4, page 14-2.
        if (aTopSide[5] == aFrontSide[4])
        {
            if (await ExplainTurnCubeQuestion(CubeLang.TurnLeftSideToRight_Text) == false)
                return;
            TurnLeftSideTo("+");
            GetCubeColorsFromArrays();

            if (aLeftSide[8] == aFrontSide[5])
            {
                if (await ExplainTurnCubeQuestion(CubeLang.TurnBottomSideToRight_Text) == false)
                    return;
                TurnBottomSideTo("+");
                GetCubeColorsFromArrays();
            }

            if (aLeftSide[8] == aBackSide[5])
            {
                if (await ExplainTurnCubeQuestion(CubeLang.TurnBottomSideToLeft_Text) == false)
                    return;
                TurnBottomSideTo("-");
                GetCubeColorsFromArrays();
            }

            if (aLeftSide[8] == aRightSide[5])
            {
                if (await ExplainTurnCubeQuestion(CubeLang.TurnBottomSideHalfTurn_Text) == false)
                    return;
                TurnBottomSideTo("+");
                TurnBottomSideTo("+");
                GetCubeColorsFromArrays();
            }
        }

        if (aTopSide[5] == aFrontSide[6])
        {
            if (await ExplainTurnCubeQuestion(CubeLang.TurnRightSideToLeft_Text) == false)
                return;
            TurnRightSideTo("-");
            GetCubeColorsFromArrays();

            if (aRightSide[8] == aFrontSide[5])
            {
                if (await ExplainTurnCubeQuestion(CubeLang.TurnBottomSideToLeft_Text) == false)
                    return;
                TurnBottomSideTo("-");
                GetCubeColorsFromArrays();
            }

            if (aRightSide[8] == aBackSide[5])
            {
                if (await ExplainTurnCubeQuestion(CubeLang.TurnBottomSideToRight_Text) == false)
                    return;
                TurnBottomSideTo("+");
                GetCubeColorsFromArrays();
            }

            if (aRightSide[8] == aLeftSide[5])
            {
                if (await ExplainTurnCubeQuestion(CubeLang.TurnBottomSideHalfTurn_Text) == false)
                    return;
                TurnBottomSideTo("+");
                TurnBottomSideTo("+");
                GetCubeColorsFromArrays();
            }
        }



        // Solve the edges of the top layer - Chapter 4, page 14-3.
        if (await SolveEdgesTopLayer() == false)
        {
            return;
        }

        // Solve the corners of the top layer - Chapter 6, page 16.

        // Solve the middle layer - Chapter 10, page 21.

        // Solve the bottom layer - Chapter 11, page 23.

        // Put the edges on the correct place.

        // Flip the corners.

        // Turning the edges.



        // For testing.
        //if (await ExplainTurnCubeQuestion(CubeLang.TurnCubeFrontSideToLeftSide_Text) == false)
        //    return;
        //TurnCubeFrontSideToLeftSide(true);

        //if (await ExplainTurnCubeQuestion(CubeLang.TurnCubeFrontSideToRightSide_Text) == false)
        //    return;
        //TurnCubeFrontSideToRightSide(true);

        //if (await ExplainTurnCubeQuestion(CubeLang.TurnCubeFrontSideToTopSide_Text) == false)
        //    return;
        //TurnCubeFrontSideToTopSide(true);

        //if (await ExplainTurnCubeQuestion(CubeLang.TurnCubeFrontSideToBottomSide_Text) == false)
        //    return;
        //TurnCubeFrontSideToBottomSide(true);

        if (!CheckIfCubeIsSolved(false))
        {
            return;
        }
    }

    // Solve the edges of the top layer - Chapter 4, page 14-3.
    private async Task<bool> SolveEdgesTopLayer()
    {
        for (int nTimes = 1; nTimes < 5; nTimes++)
        {
            if (aTopSide[5] == aBottomSide[2] && aFrontSide[5] == aFrontSide[8])
            {
                if (await ExplainTurnCubeQuestion(CubeLang.TurnFrontSideHalfTurn_Text) == false)
                    return false;
                TurnFrontSideTo("+");
                TurnFrontSideTo("+");
                GetCubeColorsFromArrays();
            }

            if (aTopSide[5] == aBottomSide[4] && aLeftSide[5] == aLeftSide[8])
            {
                if (await ExplainTurnCubeQuestion(CubeLang.TurnLeftSideHalfTurn_Text) == false)
                    return false;
                TurnLeftSideTo("+");
                TurnLeftSideTo("+");
                GetCubeColorsFromArrays();
            }

            if (aTopSide[5] == aBottomSide[6] && aRightSide[5] == aRightSide[8])
            {
                if (await ExplainTurnCubeQuestion(CubeLang.TurnRightSideHalfTurn_Text) == false)
                    return false;
                TurnRightSideTo("+");
                TurnRightSideTo("+");
                GetCubeColorsFromArrays();
            }

            if (aTopSide[5] == aBottomSide[8] && aBackSide[5] == aBackSide[8])
            {
                if (await ExplainTurnCubeQuestion(CubeLang.TurnBackSideHalfTurn_Text) == false)
                    return false;
                TurnBackSideTo("+");
                TurnBackSideTo("+");
                GetCubeColorsFromArrays();
            }
        }

        return true;
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

        SetCubeColorsInArrays();

        int nRow;

        // Check the number of colors of the cube.
        // Top side.
        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aTopSide[nRow] == aCubeColors[1])
                nNumberOfColors1++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aTopSide[nRow] == aCubeColors[2])
                nNumberOfColors2++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aTopSide[nRow] == aCubeColors[3])
                nNumberOfColors3++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aTopSide[nRow] == aCubeColors[4])
                nNumberOfColors4++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aTopSide[nRow] == aCubeColors[5])
                nNumberOfColors5++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aTopSide[nRow] == aCubeColors[6])
                nNumberOfColors6++;
        }

        // Front side.
        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aFrontSide[nRow] == aCubeColors[1])
                nNumberOfColors1++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aFrontSide[nRow] == aCubeColors[2])
                nNumberOfColors2++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aFrontSide[nRow] == aCubeColors[3])
                nNumberOfColors3++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aFrontSide[nRow] == aCubeColors[4])
                nNumberOfColors4++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aFrontSide[nRow] == aCubeColors[5])
                nNumberOfColors5++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aFrontSide[nRow] == aCubeColors[6])
                nNumberOfColors6++;
        }

        // Right side.
        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aRightSide[nRow] == aCubeColors[1])
                nNumberOfColors1++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aRightSide[nRow] == aCubeColors[2])
                nNumberOfColors2++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aRightSide[nRow] == aCubeColors[3])
                nNumberOfColors3++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aRightSide[nRow] == aCubeColors[4])
                nNumberOfColors4++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aRightSide[nRow] == aCubeColors[5])
                nNumberOfColors5++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aRightSide[nRow] == aCubeColors[6])
                nNumberOfColors6++;
        }

        // Left side.
        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aLeftSide[nRow] == aCubeColors[1])
                nNumberOfColors1++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aLeftSide[nRow] == aCubeColors[2])
                nNumberOfColors2++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aLeftSide[nRow] == aCubeColors[3])
                nNumberOfColors3++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aLeftSide[nRow] == aCubeColors[4])
                nNumberOfColors4++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aLeftSide[nRow] == aCubeColors[5])
                nNumberOfColors5++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aLeftSide[nRow] == aCubeColors[6])
                nNumberOfColors6++;
        }

        // Back side.
        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aBackSide[nRow] == aCubeColors[1])
                nNumberOfColors1++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aBackSide[nRow] == aCubeColors[2])
                nNumberOfColors2++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aBackSide[nRow] == aCubeColors[3])
                nNumberOfColors3++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aBackSide[nRow] == aCubeColors[4])
                nNumberOfColors4++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aBackSide[nRow] == aCubeColors[5])
                nNumberOfColors5++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aBackSide[nRow] == aCubeColors[6])
                nNumberOfColors6++;
        }

        // Bottom side.
        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aBottomSide[nRow] == aCubeColors[1])
                nNumberOfColors1++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aBottomSide[nRow] == aCubeColors[2])
                nNumberOfColors2++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aBottomSide[nRow] == aCubeColors[3])
                nNumberOfColors3++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aBottomSide[nRow] == aCubeColors[4])
                nNumberOfColors4++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aBottomSide[nRow] == aCubeColors[5])
                nNumberOfColors5++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aBottomSide[nRow] == aCubeColors[6])
                nNumberOfColors6++;
        }

        if (nNumberOfColors1 != 9 || nNumberOfColors2 != 9 || nNumberOfColors3 != 9 || nNumberOfColors4 != 9 || nNumberOfColors5 != 9 || nNumberOfColors6 != 9)
        {
            DisplayAlert("Error", CubeLang.MessageNineSameColor_Text, CubeLang.ButtonClose_Text);
            return false;
        }

        // Check the number of colors of the central square of the cube.
        bool bColorCenterCube = true;

        if (aTopSide[5] == aFrontSide[5] || aTopSide[5] == aRightSide[5] || aTopSide[5] == aLeftSide[5] || aTopSide[5] == aBackSide[5] || aTopSide[5] == aBottomSide[5])
        {
            bColorCenterCube = false;
        }

        if (aFrontSide[5] == aTopSide[5] || aFrontSide[5] == aRightSide[5] || aFrontSide[5] == aLeftSide[5] || aFrontSide[5] == aBackSide[5] || aFrontSide[5] == aBottomSide[5])
        {
            bColorCenterCube = false;
        }

        if (aRightSide[5] == aFrontSide[5] || aRightSide[5] == aTopSide[5] || aRightSide[5] == aLeftSide[5] || aRightSide[5] == aBackSide[5] || aRightSide[5] == aBottomSide[5])
        {
            bColorCenterCube = false;
        }

        if (aLeftSide[5] == aFrontSide[5] || aLeftSide[5] == aRightSide[5] || aLeftSide[5] == aTopSide[5] || aLeftSide[5] == aBackSide[5] || aLeftSide[5] == aBottomSide[5])
        {
            bColorCenterCube = false;
        }

        if (aBackSide[5] == aFrontSide[5] || aBackSide[5] == aRightSide[5] || aBackSide[5] == aLeftSide[5] || aBackSide[5] == aTopSide[5] || aBackSide[5] == aBottomSide[5])
        {
            bColorCenterCube = false;
        }

        if (aBottomSide[5] == aFrontSide[5] || aBottomSide[5] == aRightSide[5] || aBottomSide[5] == aLeftSide[5] || aBottomSide[5] == aBackSide[5] || aBottomSide[5] == aTopSide[5])
        {
            bColorCenterCube = false;
        }

        if (!bColorCenterCube)
        {
            DisplayAlert("Error", CubeLang.MessageColorCentralCube_Text, CubeLang.ButtonClose_Text);
            return false;
        }

        // Check the number of colors of the corner cubes of the cube.
        bool bColorCornerCube = true;
        
        if (aTopSide[7] == aLeftSide[3] || aTopSide[7] == aFrontSide[1] || aFrontSide[1] == aLeftSide[3])
        {
            bColorCornerCube = false;
        }

        if (aTopSide[1] == aLeftSide[1] || aTopSide[1] == aBackSide[3] || aLeftSide[1] == aBackSide[3])
        {
            bColorCornerCube = false;
        }

        if (aTopSide[3] == aRightSide[3] || aTopSide[3] == aBackSide[1] || aRightSide[3] == aBackSide[1])
        {
            bColorCornerCube = false;
        }

        if (aTopSide[9] == aFrontSide[3] || aTopSide[9] == aRightSide[1] || aFrontSide[3] == aRightSide[1])
        {
            bColorCornerCube = false;
        }

        if (aBottomSide[1] == aLeftSide[9] || aBottomSide[1] == aFrontSide[7] || aFrontSide[7] == aLeftSide[9])
        {
            bColorCornerCube = false;
        }

        if (aBottomSide[7] == aLeftSide[7] || aBottomSide[7] == aBackSide[9] || aBackSide[9] == aLeftSide[7])
        {
            bColorCornerCube = false;
        }

        if (aBottomSide[9] == aRightSide[9] || aBottomSide[9] == aBackSide[7] || aBackSide[7] == aRightSide[9])
        {
            bColorCornerCube = false;
        }

        if (aBottomSide[3] == aRightSide[7] || aBottomSide[3] == aFrontSide[9] || aFrontSide[9] == aRightSide[7])
        {
            bColorCornerCube = false;
        }

        if (!bColorCornerCube)
        {
            DisplayAlert("Error", CubeLang.MessageColorCornerCube_Text, CubeLang.ButtonClose_Text);
            return false;
        }

        // Check the number of colors of the edge cubes of the cube.
        bool bColorEdgeCube = true;

        if (aTopSide[2] == aBackSide[2] || aTopSide[4] == aLeftSide[2] || aTopSide[6] == aRightSide[2] || aTopSide[8] == aFrontSide[2])
        {
            bColorEdgeCube = false;
        }

        if (aBottomSide[2] == aFrontSide[8] || aBottomSide[4] == aLeftSide[8] || aBottomSide[6] == aRightSide[8] || aBottomSide[8] == aBackSide[8])
        {
            bColorEdgeCube = false;
        }

        if (!bColorEdgeCube)
        {
            DisplayAlert("Error", CubeLang.MessageColorEdgeCube_Text, CubeLang.ButtonClose_Text);
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
        
        if (aTopSide[1] == aTopSide[2] && aTopSide[1] == aTopSide[3] && aTopSide[1] == aTopSide[4] && aTopSide[1] == aTopSide[5] && aTopSide[1] == aTopSide[6] && aTopSide[1] == aTopSide[7] && aTopSide[1] == aTopSide[8] && aTopSide[1] == aTopSide[9])
        {
            bColorsTop = true;
        }

        if (aFrontSide[1] == aFrontSide[2] && aFrontSide[1] == aFrontSide[3] && aFrontSide[1] == aFrontSide[4] && aFrontSide[1] == aFrontSide[5] && aFrontSide[1] == aFrontSide[6] && aFrontSide[1] == aFrontSide[7] && aFrontSide[1] == aFrontSide[8] && aFrontSide[1] == aFrontSide[9])
        {
            bColorsFront = true;
        }

        if (aRightSide[1] == aRightSide[2] && aRightSide[1] == aRightSide[3] && aRightSide[1] == aRightSide[4] && aRightSide[1] == aRightSide[5] && aRightSide[1] == aRightSide[6] && aRightSide[1] == aRightSide[7] && aRightSide[1] == aRightSide[8] && aRightSide[1] == aRightSide[9])
        {
            bColorsRight = true;
        }

        if (aLeftSide[1] == aLeftSide[2] && aLeftSide[1] == aLeftSide[3] && aLeftSide[1] == aLeftSide[4] && aLeftSide[1] == aLeftSide[5] && aLeftSide[1] == aLeftSide[6] && aLeftSide[1] == aLeftSide[7] && aLeftSide[1] == aLeftSide[8] && aLeftSide[1] == aLeftSide[9])
        {
            bColorsLeft = true;
        }

        if (aBackSide[1] == aBackSide[2] && aBackSide[1] == aBackSide[3] && aBackSide[1] == aBackSide[4] && aBackSide[1] == aBackSide[5] && aBackSide[1] == aBackSide[6] && aBackSide[1] == aBackSide[7] && aBackSide[1] == aBackSide[8] && aBackSide[1] == aBackSide[9])
        {
            bColorsBack = true;
        }

        if (aBottomSide[1] == aBottomSide[2] && aBottomSide[1] == aBottomSide[3] && aBottomSide[1] == aBottomSide[4] && aBottomSide[1] == aBottomSide[5] && aBottomSide[1] == aBottomSide[6] && aBottomSide[1] == aBottomSide[7] && aBottomSide[1] == aBottomSide[8] && aBottomSide[1] == aBottomSide[9])
        {
            bColorsBottom = true;
        }

        if (!bColorsTop || !bColorsFront || !bColorsRight || !bColorsLeft || !bColorsBack || !bColorsBottom)
        {
            if (bShowMessage)
            {
                DisplayAlert("Rubik's Cube", CubeLang.MessageCubeNotSolved_Text, CubeLang.ButtonClose_Text);
            }
            return false;
        }

        if (bExplainSpeech)
        {
            ConvertTextToSpeech(CubeLang.MessageCubeIsSolved_Text);
        }

        DisplayAlert("Rubik's Cube", CubeLang.MessageCubeIsSolved_Text, CubeLang.ButtonClose_Text);
        return true;
    }

    // Turn the layers of the cube.
    // Turn the front side clockwise (to right +).
    private void OnTurnFrontSideToRightClicked(object sender, EventArgs e)
    {
        ExplainTurnCube(sender, CubeLang.TurnFrontSideToRight_Text, false);
        TurnFrontSideTo("+");
        GetCubeColorsFromArrays();
    }

    // Turn the top middle to the right side (+).
    private void OnTurnTopMiddleToRightSideClicked(object sender, EventArgs e)
    {
        if (bColorDrop)
        {
            TurnCubeTopSideToRightSide(false);
            return;
        }
        
        ExplainTurnCube(sender, CubeLang.TurnTopMiddleToRightSide_Text, false);
        TurnTopMiddleTo("+");
        GetCubeColorsFromArrays();
    }

    // Turn the back side counter clockwise (to left -).
    private void OnTurnBackSideToLeftClicked(object sender, EventArgs e)
    {
        ExplainTurnCube(sender, CubeLang.TurnBackSideToLeft_Text, false);
        TurnBackSideTo("-");
        GetCubeColorsFromArrays();
    }

    // Turn the left side clockwise (to right +).
    private void OnTurnLeftSideToRightClicked(object sender, EventArgs e)
    {
        ExplainTurnCube(sender, CubeLang.TurnLeftSideToRight_Text, false);
        TurnLeftSideTo("+");
        GetCubeColorsFromArrays();
    }

    // Turn the top middle to the front side (-).
    private void OnTurnTopMiddleToFrontSideClicked(object sender, EventArgs e)
    {
        if (bColorDrop)
        {
            TurnCubeFrontSideToBottomSide(false);
            return;
        }

        ExplainTurnCube(sender, CubeLang.TurnTopMiddleToFrontSide_Text, false);
        TurnFrontTopMiddleTo("-");
        GetCubeColorsFromArrays();
    }

    // Turn the right side counter clockwise (to left -).
    private void OnTurnRightSideToLeftClicked(object sender, EventArgs e)
    {
        ExplainTurnCube(sender, CubeLang.TurnRightSideToLeft_Text, false);
        TurnRightSideTo("-");
        GetCubeColorsFromArrays();
    }

    // Turn the top side counter clockwise (to left -).
    private void OnTurnTopSideToLeftClicked(object sender, EventArgs e)
    {
        ExplainTurnCube(sender, CubeLang.TurnTopSideToLeft_Text, false);
        TurnTopSideTo("-");
        GetCubeColorsFromArrays();
    }

    // Turn the front middle to the right side (-).
    private void OnTurnFrontMiddleToRightSideClicked(object sender, EventArgs e)
    {
        if (bColorDrop)
        {
            TurnCubeFrontSideToRightSide(false);
            return;
        }

        ExplainTurnCube(sender, CubeLang.TurnFrontMiddleToRightSide_Text, false);
        TurnHorizontalMiddleLayerTo("-");
        GetCubeColorsFromArrays();
    }

    // Turn the bottom side clockwise (to right +).
    private void OnTurnBottomSideToRightClicked(object sender, EventArgs e)
    {
        ExplainTurnCube(sender, CubeLang.TurnBottomSideToRight_Text, false);
        TurnBottomSideTo("+");
        GetCubeColorsFromArrays();
    }

    // Turn the top side clockwise (to right +).
    private void OnTurnTopSideToRightClicked(object sender, EventArgs e)
    {
        ExplainTurnCube(sender, CubeLang.TurnTopSideToRight_Text, false);
        TurnTopSideTo("+");
        GetCubeColorsFromArrays();
    }

    // Turn the right middle to the front side (+).
    private void OnTurnRightMiddleToFrontSideClicked(object sender, EventArgs e)
    {
        if (bColorDrop)
        {
            TurnCubeFrontSideToLeftSide(false);
            return;
        }

        ExplainTurnCube(sender, CubeLang.TurnRightMiddleToFrontSide_Text, false);
        TurnHorizontalMiddleLayerTo("+");
        GetCubeColorsFromArrays();
    }

    // Turn the bottom side counter clockwise (to left -).
    private void OnTurnBottomSideToLeftClicked(object sender, EventArgs e)
    {
        ExplainTurnCube(sender, CubeLang.TurnBottomSideToLeft_Text, false);
        TurnBottomSideTo("-");
        GetCubeColorsFromArrays();
    }

    // Turn the left side counter clockwise (to left -).
    private void OnTurnLeftSideToLeftClicked(object sender, EventArgs e)
    {
        ExplainTurnCube(sender, CubeLang.TurnLeftSideToLeft_Text, false);
        TurnLeftSideTo("-");
        GetCubeColorsFromArrays();
    }

    // Turn the front middle to the top side (+).
    private void OnTurnFrontMiddleToTopSideClicked(object sender, EventArgs e)
    {
        if (bColorDrop)
        {
            TurnCubeFrontSideToTopSide(false);
            return;
        }

        ExplainTurnCube(sender, CubeLang.TurnFrontMiddleToTopSide_Text, false);
        TurnFrontTopMiddleTo("+");
        GetCubeColorsFromArrays();
    }

    // Turn the right side clockwise (to right +).
    private void OnTurnRightSideToRightClicked(object sender, EventArgs e)
    {
        ExplainTurnCube(sender, CubeLang.TurnRightSideToRight_Text, false);
        TurnRightSideTo("+");
        GetCubeColorsFromArrays();
    }

    // Turn the front side counter clockwise (to left -).
    private void OnTurnFrontSideToLeftClicked(object sender, EventArgs e)
    {
        ExplainTurnCube(sender, CubeLang.TurnFrontSideToLeft_Text, false);
        TurnFrontSideTo("-");
        GetCubeColorsFromArrays();
    }

    // Turn the right middle to the top side (-).
    private void OnTurnRightMiddleToTopSideClicked(object sender, EventArgs e)
    {
        if (bColorDrop)
        {
            TurnCubeTopSideToLeftSide(false);
            return;
        }

        ExplainTurnCube(sender, CubeLang.TurnRightMiddleToTopSide_Text, false);
        TurnTopMiddleTo("-");
        GetCubeColorsFromArrays();
    }

    // Turn the back side clockwise (to right +).
    private void OnTurnBackSideToRightClicked(object sender, EventArgs e)
    {
        ExplainTurnCube(sender, CubeLang.TurnBackSideToRight_Text, false);
        TurnBackSideTo("+");
        GetCubeColorsFromArrays();
    }

    // Turn the entire cube a quarter turn.
    // Rotate the entire cube so that the front goes to the left side.
    private void TurnCubeFrontSideToLeftSide(bool bExplainQuestion)
    {
        ExplainTurnCube(null, CubeLang.TurnCubeFrontSideToLeftSide_Text, bExplainQuestion);
        TurnTopSideTo("+");
        TurnHorizontalMiddleLayerTo("+");
        TurnBottomSideTo("-");
        GetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the front goes to the right side.
    private void TurnCubeFrontSideToRightSide(bool bExplainQuestion)
    {
        ExplainTurnCube(null, CubeLang.TurnCubeFrontSideToRightSide_Text, bExplainQuestion);
        TurnTopSideTo("-");
        TurnHorizontalMiddleLayerTo("-");
        TurnBottomSideTo("+");
        GetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the front goes to the top side.
    private void TurnCubeFrontSideToTopSide(bool bExplainQuestion)
    {
        ExplainTurnCube(null, CubeLang.TurnCubeFrontSideToTopSide_Text, bExplainQuestion);
        TurnRightSideTo("+");
        TurnFrontTopMiddleTo("+");
        TurnLeftSideTo("-");
        GetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the front goes to the bottom side.
    private void TurnCubeFrontSideToBottomSide(bool bExplainQuestion)
    {
        ExplainTurnCube(null, CubeLang.TurnCubeFrontSideToBottomSide_Text, bExplainQuestion);
        TurnRightSideTo("-");
        TurnFrontTopMiddleTo("-");
        TurnLeftSideTo("+");
        GetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the top goes to the right side.
    private void TurnCubeTopSideToRightSide(bool bExplainQuestion)
    {
        ExplainTurnCube(null, CubeLang.TurnCubeTopSideToRightSide_Text, bExplainQuestion);
        TurnFrontSideTo("+");
        TurnTopMiddleTo("+");
        TurnBackSideTo("-");
        GetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the top goes to the left side.
    private void TurnCubeTopSideToLeftSide(bool bExplainQuestion)
    {
        ExplainTurnCube(null, CubeLang.TurnCubeTopSideToLeftSide_Text, bExplainQuestion);
        TurnFrontSideTo("-");
        TurnTopMiddleTo("-");
        TurnBackSideTo("+");
        GetCubeColorsFromArrays();
    }

    // Turn the entire front side clockwise or counter clockwise.
    private void TurnFrontSideTo(string cDirection)
    {
        string cColorFront1 = aFrontSide[1];
        string cColorFront2 = aFrontSide[2];
        string cColorFront3 = aFrontSide[3];
        string cColorFront4 = aFrontSide[4];
        string cColorFront6 = aFrontSide[6];
        string cColorFront7 = aFrontSide[7];
        string cColorFront8 = aFrontSide[8];
        string cColorFront9 = aFrontSide[9];

        string cColorTop7 = aTopSide[7];
        string cColorTop8 = aTopSide[8];
        string cColorTop9 = aTopSide[9];

        string cColorRight1 = aRightSide[1];
        string cColorRight4 = aRightSide[4];
        string cColorRight7 = aRightSide[7];

        string cColorBottom1 = aBottomSide[1];
        string cColorBottom2 = aBottomSide[2];
        string cColorBottom3 = aBottomSide[3];

        string cColorLeft3 = aLeftSide[3];
        string cColorLeft6 = aLeftSide[6];
        string cColorLeft9 = aLeftSide[9];

        if (cDirection == "+")
        {
            aFrontSide[1] = cColorFront7;
            aFrontSide[2] = cColorFront4;
            aFrontSide[3] = cColorFront1;
            aFrontSide[4] = cColorFront8;
            aFrontSide[6] = cColorFront2;
            aFrontSide[7] = cColorFront9;
            aFrontSide[8] = cColorFront6;
            aFrontSide[9] = cColorFront3;

            aTopSide[7] = cColorLeft9;
            aTopSide[8] = cColorLeft6;
            aTopSide[9] = cColorLeft3;

            aRightSide[1] = cColorTop7;
            aRightSide[4] = cColorTop8;
            aRightSide[7] = cColorTop9;

            aBottomSide[1] = cColorRight7;
            aBottomSide[2] = cColorRight4;
            aBottomSide[3] = cColorRight1;

            aLeftSide[3] = cColorBottom1;
            aLeftSide[6] = cColorBottom2;
            aLeftSide[9] = cColorBottom3;
        }

        if (cDirection == "-")
        {
            aFrontSide[1] = cColorFront3;
            aFrontSide[2] = cColorFront6;
            aFrontSide[3] = cColorFront9;
            aFrontSide[4] = cColorFront2;
            aFrontSide[6] = cColorFront8;
            aFrontSide[7] = cColorFront1;
            aFrontSide[8] = cColorFront4;
            aFrontSide[9] = cColorFront7;

            aTopSide[7] = cColorRight1;
            aTopSide[8] = cColorRight4;
            aTopSide[9] = cColorRight7;

            aRightSide[1] = cColorBottom3;
            aRightSide[4] = cColorBottom2;
            aRightSide[7] = cColorBottom1;

            aBottomSide[1] = cColorLeft3;
            aBottomSide[2] = cColorLeft6;
            aBottomSide[3] = cColorLeft9;

            aLeftSide[3] = cColorTop9;
            aLeftSide[6] = cColorTop8;
            aLeftSide[9] = cColorTop7;
        }
    }

    // Turn the top middle to the right or left.
    private void TurnTopMiddleTo(string cDirection)
    {
        string cColorTop4 = aTopSide[4];
        string cColorTop5 = aTopSide[5];
        string cColorTop6 = aTopSide[6];

        string cColorRight2 = aRightSide[2];
        string cColorRight5 = aRightSide[5];
        string cColorRight8 = aRightSide[8];

        string cColorBottom4 = aBottomSide[4];
        string cColorBottom5 = aBottomSide[5];
        string cColorBottom6 = aBottomSide[6];

        string cColorLeft2 = aLeftSide[2];
        string cColorLeft5 = aLeftSide[5];
        string cColorLeft8 = aLeftSide[8];

        if (cDirection == "+")
        {
            aTopSide[4] = cColorLeft8;
            aTopSide[5] = cColorLeft5;
            aTopSide[6] = cColorLeft2;

            aRightSide[2] = cColorTop4;
            aRightSide[5] = cColorTop5;
            aRightSide[8] = cColorTop6;

            aBottomSide[4] = cColorRight8;
            aBottomSide[5] = cColorRight5;
            aBottomSide[6] = cColorRight2;

            aLeftSide[2] = cColorBottom4;
            aLeftSide[5] = cColorBottom5;
            aLeftSide[8] = cColorBottom6;
        }

        if (cDirection == "-")
        {
            aTopSide[4] = cColorRight2;
            aTopSide[5] = cColorRight5;
            aTopSide[6] = cColorRight8;

            aRightSide[2] = cColorBottom6;
            aRightSide[5] = cColorBottom5;
            aRightSide[8] = cColorBottom4;

            aBottomSide[4] = cColorLeft2;
            aBottomSide[5] = cColorLeft5;
            aBottomSide[6] = cColorLeft8;

            aLeftSide[2] = cColorTop6;
            aLeftSide[5] = cColorTop5;
            aLeftSide[8] = cColorTop4;
        }
    }

    // Turn the entire back side clockwise or counter clockwise.
    private void TurnBackSideTo(string cDirection)
    {
        string cColorBack1 = aBackSide[1];
        string cColorBack2 = aBackSide[2];
        string cColorBack3 = aBackSide[3];
        string cColorBack4 = aBackSide[4];
        string cColorBack6 = aBackSide[6];
        string cColorBack7 = aBackSide[7];
        string cColorBack8 = aBackSide[8];
        string cColorBack9 = aBackSide[9];

        string cColorTop1 = aTopSide[1];
        string cColorTop2 = aTopSide[2];
        string cColorTop3 = aTopSide[3];

        string cColorRight3 = aRightSide[3];
        string cColorRight6 = aRightSide[6];
        string cColorRight9 = aRightSide[9];

        string cColorBottom7 = aBottomSide[7];
        string cColorBottom8 = aBottomSide[8];
        string cColorBottom9 = aBottomSide[9];

        string cColorLeft1 = aLeftSide[1];
        string cColorLeft4 = aLeftSide[4];
        string cColorLeft7 = aLeftSide[7];

        if (cDirection == "+")
        {
            aBackSide[1] = cColorBack7;
            aBackSide[2] = cColorBack4;
            aBackSide[3] = cColorBack1;
            aBackSide[4] = cColorBack8;
            aBackSide[6] = cColorBack2;
            aBackSide[7] = cColorBack9;
            aBackSide[8] = cColorBack6;
            aBackSide[9] = cColorBack3;

            aTopSide[1] = cColorRight3;
            aTopSide[2] = cColorRight6;
            aTopSide[3] = cColorRight9;

            aRightSide[3] = cColorBottom9;
            aRightSide[6] = cColorBottom8;
            aRightSide[9] = cColorBottom7;

            aBottomSide[7] = cColorLeft1;
            aBottomSide[8] = cColorLeft4;
            aBottomSide[9] = cColorLeft7;

            aLeftSide[1] = cColorTop3;
            aLeftSide[4] = cColorTop2;
            aLeftSide[7] = cColorTop1;
        }

        if (cDirection == "-")
        {
            aBackSide[1] = cColorBack3;
            aBackSide[2] = cColorBack6;
            aBackSide[3] = cColorBack9;
            aBackSide[4] = cColorBack2;
            aBackSide[6] = cColorBack8;
            aBackSide[7] = cColorBack1;
            aBackSide[8] = cColorBack4;
            aBackSide[9] = cColorBack7;

            aTopSide[1] = cColorLeft7;
            aTopSide[2] = cColorLeft4;
            aTopSide[3] = cColorLeft1;

            aRightSide[3] = cColorTop1;
            aRightSide[6] = cColorTop2;
            aRightSide[9] = cColorTop3;

            aBottomSide[7] = cColorRight9;
            aBottomSide[8] = cColorRight6;
            aBottomSide[9] = cColorRight3;

            aLeftSide[1] = cColorBottom7;
            aLeftSide[4] = cColorBottom8;
            aLeftSide[7] = cColorBottom9;
        }
    }

    // Turn the entire left side clockwise or counter clockwise.
    private void TurnLeftSideTo(string cDirection)
    {
        string cColorLeft1 = aLeftSide[1];
        string cColorLeft2 = aLeftSide[2];
        string cColorLeft3 = aLeftSide[3];
        string cColorLeft4 = aLeftSide[4];
        string cColorLeft6 = aLeftSide[6];
        string cColorLeft7 = aLeftSide[7];
        string cColorLeft8 = aLeftSide[8];
        string cColorLeft9 = aLeftSide[9];

        string cColorTop1 = aTopSide[1];
        string cColorTop4 = aTopSide[4];
        string cColorTop7 = aTopSide[7];

        string cColorFront1 = aFrontSide[1];
        string cColorFront4 = aFrontSide[4];
        string cColorFront7 = aFrontSide[7];

        string cColorBottom1 = aBottomSide[1];
        string cColorBottom4 = aBottomSide[4];
        string cColorBottom7 = aBottomSide[7];

        string cColorBack3 = aBackSide[3];
        string cColorBack6 = aBackSide[6];
        string cColorBack9 = aBackSide[9];

        if (cDirection == "+")
        {
            aLeftSide[1] = cColorLeft7;
            aLeftSide[2] = cColorLeft4;
            aLeftSide[3] = cColorLeft1;
            aLeftSide[4] = cColorLeft8;
            aLeftSide[6] = cColorLeft2;
            aLeftSide[7] = cColorLeft9;
            aLeftSide[8] = cColorLeft6;
            aLeftSide[9] = cColorLeft3;

            aTopSide[1] = cColorBack9;
            aTopSide[4] = cColorBack6;
            aTopSide[7] = cColorBack3;

            aFrontSide[1] = cColorTop1;
            aFrontSide[4] = cColorTop4;
            aFrontSide[7] = cColorTop7;

            aBottomSide[1] = cColorFront1;
            aBottomSide[4] = cColorFront4;
            aBottomSide[7] = cColorFront7;

            aBackSide[3] = cColorBottom7;
            aBackSide[6] = cColorBottom4;
            aBackSide[9] = cColorBottom1;
        }

        if (cDirection == "-")
        {
            aLeftSide[1] = cColorLeft3;
            aLeftSide[2] = cColorLeft6;
            aLeftSide[3] = cColorLeft9;
            aLeftSide[4] = cColorLeft2;
            aLeftSide[6] = cColorLeft8;
            aLeftSide[7] = cColorLeft1;
            aLeftSide[8] = cColorLeft4;
            aLeftSide[9] = cColorLeft7;

            aTopSide[1] = cColorFront1;
            aTopSide[4] = cColorFront4;
            aTopSide[7] = cColorFront7;

            aFrontSide[1] = cColorBottom1;
            aFrontSide[4] = cColorBottom4;
            aFrontSide[7] = cColorBottom7;

            aBottomSide[1] = cColorBack9;
            aBottomSide[4] = cColorBack6;
            aBottomSide[7] = cColorBack3;

            aBackSide[3] = cColorTop7;
            aBackSide[6] = cColorTop4;
            aBackSide[9] = cColorTop1;
        }
    }

    // Turn the top middle layer to right or left.
    private void TurnFrontTopMiddleTo(string cDirection)
    {
        string cColorTop2 = aTopSide[2];
        string cColorTop5 = aTopSide[5];
        string cColorTop8 = aTopSide[8];

        string cColorFront2 = aFrontSide[2];
        string cColorFront5 = aFrontSide[5];
        string cColorFront8 = aFrontSide[8];

        string cColorBottom2 = aBottomSide[2];
        string cColorBottom5 = aBottomSide[5];
        string cColorBottom8 = aBottomSide[8];

        string cColorBack2 = aBackSide[2];
        string cColorBack5 = aBackSide[5];
        string cColorBack8 = aBackSide[8];

        if (cDirection == "+")
        {
            aTopSide[2] = cColorFront2;
            aTopSide[5] = cColorFront5;
            aTopSide[8] = cColorFront8;

            aFrontSide[2] = cColorBottom2;
            aFrontSide[5] = cColorBottom5;
            aFrontSide[8] = cColorBottom8;

            aBottomSide[2] = cColorBack8;
            aBottomSide[5] = cColorBack5;
            aBottomSide[8] = cColorBack2;

            aBackSide[2] = cColorTop8;
            aBackSide[5] = cColorTop5;
            aBackSide[8] = cColorTop2;
        }

        if (cDirection == "-")
        {
            aTopSide[2] = cColorBack8;
            aTopSide[5] = cColorBack5;
            aTopSide[8] = cColorBack2;

            aFrontSide[2] = cColorTop2;
            aFrontSide[5] = cColorTop5;
            aFrontSide[8] = cColorTop8;

            aBottomSide[2] = cColorFront2;
            aBottomSide[5] = cColorFront5;
            aBottomSide[8] = cColorFront8;

            aBackSide[2] = cColorBottom8;
            aBackSide[5] = cColorBottom5;
            aBackSide[8] = cColorBottom2;
        }
    }

    // Turn the entire right side clockwise or counter clockwise.
    private void TurnRightSideTo(string cDirection)
    {
        string cColorRight1 = aRightSide[1];
        string cColorRight2 = aRightSide[2];
        string cColorRight3 = aRightSide[3];
        string cColorRight4 = aRightSide[4];
        string cColorRight6 = aRightSide[6];
        string cColorRight7 = aRightSide[7];
        string cColorRight8 = aRightSide[8];
        string cColorRight9 = aRightSide[9];

        string cColorTop3 = aTopSide[3];
        string cColorTop6 = aTopSide[6];
        string cColorTop9 = aTopSide[9];

        string cColorFront3 = aFrontSide[3];
        string cColorFront6 = aFrontSide[6];
        string cColorFront9 = aFrontSide[9];

        string cColorBottom3 = aBottomSide[3];
        string cColorBottom6 = aBottomSide[6];
        string cColorBottom9 = aBottomSide[9];

        string cColorBack1 = aBackSide[1];
        string cColorBack4 = aBackSide[4];
        string cColorBack7 = aBackSide[7];

        if (cDirection == "+")
        {
            aRightSide[1] = cColorRight7;
            aRightSide[2] = cColorRight4;
            aRightSide[3] = cColorRight1;
            aRightSide[4] = cColorRight8;
            aRightSide[6] = cColorRight2;
            aRightSide[7] = cColorRight9;
            aRightSide[8] = cColorRight6;
            aRightSide[9] = cColorRight3;

            aTopSide[3] = cColorFront3;
            aTopSide[6] = cColorFront6;
            aTopSide[9] = cColorFront9;

            aFrontSide[3] = cColorBottom3;
            aFrontSide[6] = cColorBottom6;
            aFrontSide[9] = cColorBottom9;

            aBottomSide[3] = cColorBack7;
            aBottomSide[6] = cColorBack4;
            aBottomSide[9] = cColorBack1;

            aBackSide[1] = cColorTop9;
            aBackSide[4] = cColorTop6;
            aBackSide[7] = cColorTop3;
        }

        if (cDirection == "-")
        {
            aRightSide[1] = cColorRight3;
            aRightSide[2] = cColorRight6;
            aRightSide[3] = cColorRight9;
            aRightSide[4] = cColorRight2;
            aRightSide[6] = cColorRight8;
            aRightSide[7] = cColorRight1;
            aRightSide[8] = cColorRight4;
            aRightSide[9] = cColorRight7;

            aTopSide[3] = cColorBack7;
            aTopSide[6] = cColorBack4;
            aTopSide[9] = cColorBack1;

            aFrontSide[3] = cColorTop3;
            aFrontSide[6] = cColorTop6;
            aFrontSide[9] = cColorTop9;

            aBottomSide[3] = cColorFront3;
            aBottomSide[6] = cColorFront6;
            aBottomSide[9] = cColorFront9;

            aBackSide[1] = cColorBottom9;
            aBackSide[4] = cColorBottom6;
            aBackSide[7] = cColorBottom3;
        }
    }

    // Turn the entire top side clockwise or counter clockwise.
    private void TurnTopSideTo(string cDirection)
    {
        string cColorTop1 = aTopSide[1];
        string cColorTop2 = aTopSide[2];
        string cColorTop3 = aTopSide[3];
        string cColorTop4 = aTopSide[4];
        string cColorTop6 = aTopSide[6];
        string cColorTop7 = aTopSide[7];
        string cColorTop8 = aTopSide[8];
        string cColorTop9 = aTopSide[9];

        string cColorLeft1 = aLeftSide[1];
        string cColorLeft2 = aLeftSide[2];
        string cColorLeft3 = aLeftSide[3];

        string cColorFront1 = aFrontSide[1];
        string cColorFront2 = aFrontSide[2];
        string cColorFront3 = aFrontSide[3];

        string cColorRight1 = aRightSide[1];
        string cColorRight2 = aRightSide[2];
        string cColorRight3 = aRightSide[3];

        string cColorBack1 = aBackSide[1];
        string cColorBack2 = aBackSide[2];
        string cColorBack3 = aBackSide[3];

        if (cDirection == "+")
        {
            aTopSide[1] = cColorTop7;
            aTopSide[2] = cColorTop4;
            aTopSide[3] = cColorTop1;
            aTopSide[4] = cColorTop8;
            aTopSide[6] = cColorTop2;
            aTopSide[7] = cColorTop9;
            aTopSide[8] = cColorTop6;
            aTopSide[9] = cColorTop3;

            aLeftSide[1] = cColorFront1;
            aLeftSide[2] = cColorFront2;
            aLeftSide[3] = cColorFront3;

            aFrontSide[1] = cColorRight1;
            aFrontSide[2] = cColorRight2;
            aFrontSide[3] = cColorRight3;

            aRightSide[1] = cColorBack1;
            aRightSide[2] = cColorBack2;
            aRightSide[3] = cColorBack3;

            aBackSide[1] = cColorLeft1;
            aBackSide[2] = cColorLeft2;
            aBackSide[3] = cColorLeft3;
        }

        if (cDirection == "-")
        {
            aTopSide[1] = cColorTop3;
            aTopSide[2] = cColorTop6;
            aTopSide[3] = cColorTop9;
            aTopSide[4] = cColorTop2;
            aTopSide[6] = cColorTop8;
            aTopSide[7] = cColorTop1;
            aTopSide[8] = cColorTop4;
            aTopSide[9] = cColorTop7;

            aLeftSide[1] = cColorBack1;
            aLeftSide[2] = cColorBack2;
            aLeftSide[3] = cColorBack3;

            aFrontSide[1] = cColorLeft1;
            aFrontSide[2] = cColorLeft2;
            aFrontSide[3] = cColorLeft3;

            aRightSide[1] = cColorFront1;
            aRightSide[2] = cColorFront2;
            aRightSide[3] = cColorFront3;

            aBackSide[1] = cColorRight1;
            aBackSide[2] = cColorRight2;
            aBackSide[3] = cColorRight3;
        }
    }

    // Turn the horizontal middle layer to right or left.
    private void TurnHorizontalMiddleLayerTo(string cDirection)
    {
        string cColorFront4 = aFrontSide[4];
        string cColorFront5 = aFrontSide[5];
        string cColorFront6 = aFrontSide[6];

        string cColorRight4 = aRightSide[4];
        string cColorRight5 = aRightSide[5];
        string cColorRight6 = aRightSide[6];

        string cColorBack4 = aBackSide[4];
        string cColorBack5 = aBackSide[5];
        string cColorBack6 = aBackSide[6];

        string cColorLeft4 = aLeftSide[4];
        string cColorLeft5 = aLeftSide[5];
        string cColorLeft6 = aLeftSide[6];

        if (cDirection == "+")
        {
            aFrontSide[4] = cColorRight4;
            aFrontSide[5] = cColorRight5;
            aFrontSide[6] = cColorRight6;

            aRightSide[4] = cColorBack4;
            aRightSide[5] = cColorBack5;
            aRightSide[6] = cColorBack6;

            aBackSide[4] = cColorLeft4;
            aBackSide[5] = cColorLeft5;
            aBackSide[6] = cColorLeft6;

            aLeftSide[4] = cColorFront4;
            aLeftSide[5] = cColorFront5;
            aLeftSide[6] = cColorFront6;
        }

        if (cDirection == "-")
        {
            aFrontSide[4] = cColorLeft4;
            aFrontSide[5] = cColorLeft5;
            aFrontSide[6] = cColorLeft6;

            aRightSide[4] = cColorFront4;
            aRightSide[5] = cColorFront5;
            aRightSide[6] = cColorFront6;

            aBackSide[4] = cColorRight4;
            aBackSide[5] = cColorRight5;
            aBackSide[6] = cColorRight6;

            aLeftSide[4] = cColorBack4;
            aLeftSide[5] = cColorBack5;
            aLeftSide[6] = cColorBack6;
        }
    }

    // Turn the entire bottom side clockwise or counter clockwise.
    private void TurnBottomSideTo(string cDirection)
    {
        string cColorBottom1 = aBottomSide[1];
        string cColorBottom2 = aBottomSide[2];
        string cColorBottom3 = aBottomSide[3];
        string cColorBottom4 = aBottomSide[4];
        string cColorBottom6 = aBottomSide[6];
        string cColorBottom7 = aBottomSide[7];
        string cColorBottom8 = aBottomSide[8];
        string cColorBottom9 = aBottomSide[9];

        string cColorLeft7 = aLeftSide[7];
        string cColorLeft8 = aLeftSide[8];
        string cColorLeft9 = aLeftSide[9];

        string cColorFront7 = aFrontSide[7];
        string cColorFront8 = aFrontSide[8];
        string cColorFront9 = aFrontSide[9];

        string cColorRight7 = aRightSide[7];
        string cColorRight8 = aRightSide[8];
        string cColorRight9 = aRightSide[9];

        string cColorBack7 = aBackSide[7];
        string cColorBack8 = aBackSide[8];
        string cColorBack9 = aBackSide[9];

        if (cDirection == "+")
        {
            aBottomSide[1] = cColorBottom7;
            aBottomSide[2] = cColorBottom4;
            aBottomSide[3] = cColorBottom1;
            aBottomSide[4] = cColorBottom8;
            aBottomSide[6] = cColorBottom2;
            aBottomSide[7] = cColorBottom9;
            aBottomSide[8] = cColorBottom6;
            aBottomSide[9] = cColorBottom3;

            aLeftSide[7] = cColorBack7;
            aLeftSide[8] = cColorBack8;
            aLeftSide[9] = cColorBack9;

            aFrontSide[7] = cColorLeft7;
            aFrontSide[8] = cColorLeft8;
            aFrontSide[9] = cColorLeft9;

            aRightSide[7] = cColorFront7;
            aRightSide[8] = cColorFront8;
            aRightSide[9] = cColorFront9;

            aBackSide[7] = cColorRight7;
            aBackSide[8] = cColorRight8;
            aBackSide[9] = cColorRight9;
        }

        if (cDirection == "-")
        {
            aBottomSide[1] = cColorBottom3;
            aBottomSide[2] = cColorBottom6;
            aBottomSide[3] = cColorBottom9;
            aBottomSide[4] = cColorBottom2;
            aBottomSide[6] = cColorBottom8;
            aBottomSide[7] = cColorBottom1;
            aBottomSide[8] = cColorBottom4;
            aBottomSide[9] = cColorBottom7;

            aLeftSide[7] = cColorFront7;
            aLeftSide[8] = cColorFront8;
            aLeftSide[9] = cColorFront9;

            aFrontSide[7] = cColorRight7;
            aFrontSide[8] = cColorRight8;
            aFrontSide[9] = cColorRight9;

            aRightSide[7] = cColorBack7;
            aRightSide[8] = cColorBack8;
            aRightSide[9] = cColorBack9;

            aBackSide[7] = cColorLeft7;
            aBackSide[8] = cColorLeft8;
            aBackSide[9] = cColorLeft9;
        }
    }

    // Explain the turn of the cube.
    private async void ExplainTurnCube(object sender, string cTurnCubeText, bool bExplainQuestion)
    {
        if (bExplainQuestion)
        {
            return;
        }

        if (sender != null)
        {
            var imagebutton = (ImageButton)sender;
            imagebutton.BackgroundColor = Color.FromArgb(cColorArrowActive);
        }

        string cTurnCubeSpeech = cTurnCubeText;

        if (bExplainSpeech)
        {
            // If the right of the text = ' (+).' or ' (-).' remove it.
            if (cTurnCubeText.Substring(cTurnCubeText.Length - 2, 2) == ").")
            {
                cTurnCubeSpeech = cTurnCubeSpeech.Substring(0, cTurnCubeSpeech.Length - 5);
            }
            
            ConvertTextToSpeech(cTurnCubeSpeech);
        }

        if (bExplainText)
        {
            await DisplayAlert("", cTurnCubeText, CubeLang.ButtonClose_Text);
        }

        if (sender != null)
        {
            var imagebutton = (ImageButton)sender;
            imagebutton.BackgroundColor = Color.FromArgb(cColorArrowNotActive);
        }
    }

    // Explain the turn of the cube with the option to continue or to stop.
    private async Task<bool> ExplainTurnCubeQuestion(string cTurnCubeText)
    {
        string cTurnCubeSpeech = cTurnCubeText;

        if (bExplainSpeech)
        {
            // If the right of the text = ' (+).' or ' (-).' remove it.
            if (cTurnCubeText.Substring(cTurnCubeText.Length - 2, 2) == ").")
            {
                cTurnCubeSpeech = cTurnCubeSpeech.Substring(0, cTurnCubeSpeech.Length - 5);
            }

            ConvertTextToSpeech(cTurnCubeSpeech);
        }

        if (await DisplayAlert(CubeLang.Solve_Text, cTurnCubeText, CubeLang.Continue_Text, CubeLang.Stop_Text) == false)
        {
            return false;
        }

        return true;
    }


    // On clicked event: Save the cube.
    private void OnButtonSaveClicked(object sender, EventArgs e)
    {
        SetCubeColorsInArrays();

        string cFileName = System.IO.Path.Combine(FileSystem.CacheDirectory, "RubiksCube.txt");

        if (File.Exists(cFileName))
        {
            File.Delete(cFileName);
        }

        int nRow;

        try
        {
            using StreamWriter sw = new(cFileName, false);

            for (nRow = 1; nRow < 7; nRow++)
            {
                sw.WriteLine(aCubeColors[nRow]);
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                sw.WriteLine(aTopSide[nRow]);
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                sw.WriteLine(aFrontSide[nRow]);
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                sw.WriteLine(aRightSide[nRow]);
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                sw.WriteLine(aLeftSide[nRow]);
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                sw.WriteLine(aBackSide[nRow]);
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                sw.WriteLine(aBottomSide[nRow]);
            }

            // Close the StreamWriter object.
            sw.Close();
        }
        catch (Exception ex)
        {
            DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
            return;
        }
    }

    // On clicked event: Open, restore the cube.
    private void OnButtonOpenClicked(object sender, EventArgs e)
    {
        string cFileName = FileSystem.CacheDirectory + "/RubiksCube.txt";

        if (File.Exists(cFileName) == false)
        {
            return;
        }

        int nRow;
       
        try
        {
            // Open the text file using a stream reader.
            using StreamReader sr = new(cFileName, false);

            for (nRow = 1; nRow < 7; nRow++)
            {
                aCubeColors[nRow] = sr.ReadLine();
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                aTopSide[nRow] = sr.ReadLine();
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                aFrontSide[nRow] = sr.ReadLine();
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                aRightSide[nRow] = sr.ReadLine();
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                aLeftSide[nRow] = sr.ReadLine();
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                aBackSide[nRow] = sr.ReadLine();
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                aBottomSide[nRow] = sr.ReadLine();
            }

            // Close the StreamReader object.
            sr.Close();
        }
        catch (Exception ex)
        {
            DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
            return;
        }

        GetCubeColorsFromArrays();
    }

    // On clicked event: Reset the colors of the cube.
    private void OnBtnResetClicked(object sender, EventArgs e)
    {
        ResetCube();
    }

    // Reset the colors of the cube.
    private void ResetCube()
    {
        int nRow;

        for (nRow = 1; nRow < 10; nRow++)
        {
            aTopSide[nRow] = aCubeColors[1];
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            aFrontSide[nRow] = aCubeColors[2];
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            aRightSide[nRow] = aCubeColors[3];
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            aLeftSide[nRow] = aCubeColors[4];
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            aBackSide[nRow] = aCubeColors[5];
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            aBottomSide[nRow] = aCubeColors[6];
        }

        GetCubeColorsFromArrays();

        //IsEnabledArrows(true);
        IsVisibleArrows(true);
    }

    // Store the cube colors in arrays.
    private void SetCubeColorsInArrays()
    {
        aCubeColors[1] = GetHexColorPolygon(plgCubeColor1);
        aCubeColors[2] = GetHexColorPolygon(plgCubeColor2);
        aCubeColors[3] = GetHexColorPolygon(plgCubeColor3);
        aCubeColors[4] = GetHexColorPolygon(plgCubeColor4);
        aCubeColors[5] = GetHexColorPolygon(plgCubeColor5);
        aCubeColors[6] = GetHexColorPolygon(plgCubeColor6);

        aTopSide[1] = GetHexColorPolygon(plgTop1);
        aTopSide[2] = GetHexColorPolygon(plgTop2);
        aTopSide[3] = GetHexColorPolygon(plgTop3);
        aTopSide[4] = GetHexColorPolygon(plgTop4);
        aTopSide[5] = GetHexColorPolygon(plgTop5);
        aTopSide[6] = GetHexColorPolygon(plgTop6);
        aTopSide[7] = GetHexColorPolygon(plgTop7);
        aTopSide[8] = GetHexColorPolygon(plgTop8);
        aTopSide[9] = GetHexColorPolygon(plgTop9);

        aFrontSide[1] = GetHexColorPolygon(plgFront1);
        aFrontSide[2] = GetHexColorPolygon(plgFront2);
        aFrontSide[3] = GetHexColorPolygon(plgFront3);
        aFrontSide[4] = GetHexColorPolygon(plgFront4);
        aFrontSide[5] = GetHexColorPolygon(plgFront5);
        aFrontSide[6] = GetHexColorPolygon(plgFront6);
        aFrontSide[7] = GetHexColorPolygon(plgFront7);
        aFrontSide[8] = GetHexColorPolygon(plgFront8);
        aFrontSide[9] = GetHexColorPolygon(plgFront9);

        aRightSide[1] = GetHexColorPolygon(plgRight1);
        aRightSide[2] = GetHexColorPolygon(plgRight2);
        aRightSide[3] = GetHexColorPolygon(plgRight3);
        aRightSide[4] = GetHexColorPolygon(plgRight4);
        aRightSide[5] = GetHexColorPolygon(plgRight5);
        aRightSide[6] = GetHexColorPolygon(plgRight6);
        aRightSide[7] = GetHexColorPolygon(plgRight7);
        aRightSide[8] = GetHexColorPolygon(plgRight8);
        aRightSide[9] = GetHexColorPolygon(plgRight9);

        aLeftSide[1] = GetHexColorPolygon(plgLeft1);
        aLeftSide[2] = GetHexColorPolygon(plgLeft2);
        aLeftSide[3] = GetHexColorPolygon(plgLeft3);
        aLeftSide[4] = GetHexColorPolygon(plgLeft4);
        aLeftSide[5] = GetHexColorPolygon(plgLeft5);
        aLeftSide[6] = GetHexColorPolygon(plgLeft6);
        aLeftSide[7] = GetHexColorPolygon(plgLeft7);
        aLeftSide[8] = GetHexColorPolygon(plgLeft8);
        aLeftSide[9] = GetHexColorPolygon(plgLeft9);

        aBackSide[1] = GetHexColorPolygon(plgBack1);
        aBackSide[2] = GetHexColorPolygon(plgBack2);
        aBackSide[3] = GetHexColorPolygon(plgBack3);
        aBackSide[4] = GetHexColorPolygon(plgBack4);
        aBackSide[5] = GetHexColorPolygon(plgBack5);
        aBackSide[6] = GetHexColorPolygon(plgBack6);
        aBackSide[7] = GetHexColorPolygon(plgBack7);
        aBackSide[8] = GetHexColorPolygon(plgBack8);
        aBackSide[9] = GetHexColorPolygon(plgBack9);

        aBottomSide[1] = GetHexColorPolygon(plgBottom1);
        aBottomSide[2] = GetHexColorPolygon(plgBottom2);
        aBottomSide[3] = GetHexColorPolygon(plgBottom3);
        aBottomSide[4] = GetHexColorPolygon(plgBottom4);
        aBottomSide[5] = GetHexColorPolygon(plgBottom5);
        aBottomSide[6] = GetHexColorPolygon(plgBottom6);
        aBottomSide[7] = GetHexColorPolygon(plgBottom7);
        aBottomSide[8] = GetHexColorPolygon(plgBottom8);
        aBottomSide[9] = GetHexColorPolygon(plgBottom9);
    }

    // Restore the cube colors from the arrays.
    private void GetCubeColorsFromArrays()
    {
        cCubeColor1 = aCubeColors[1];
        cCubeColor2 = aCubeColors[2];
        cCubeColor3 = aCubeColors[3];
        cCubeColor4 = aCubeColors[4];
        cCubeColor5 = aCubeColors[5];
        cCubeColor6 = aCubeColors[6];

        plgCubeColor1.Fill = Color.FromArgb(aCubeColors[1]);
        plgCubeColor2.Fill = Color.FromArgb(aCubeColors[2]);
        plgCubeColor3.Fill = Color.FromArgb(aCubeColors[3]);
        plgCubeColor4.Fill = Color.FromArgb(aCubeColors[4]);
        plgCubeColor5.Fill = Color.FromArgb(aCubeColors[5]);
        plgCubeColor6.Fill = Color.FromArgb(aCubeColors[6]);

        plgTop1.Fill = Color.FromArgb(aTopSide[1]);
        plgTop2.Fill = Color.FromArgb(aTopSide[2]);
        plgTop3.Fill = Color.FromArgb(aTopSide[3]);
        plgTop4.Fill = Color.FromArgb(aTopSide[4]);
        plgTop5.Fill = Color.FromArgb(aTopSide[5]);
        plgTop6.Fill = Color.FromArgb(aTopSide[6]);
        plgTop7.Fill = Color.FromArgb(aTopSide[7]);
        plgTop8.Fill = Color.FromArgb(aTopSide[8]);
        plgTop9.Fill = Color.FromArgb(aTopSide[9]);

        plgFront1.Fill = Color.FromArgb(aFrontSide[1]);
        plgFront2.Fill = Color.FromArgb(aFrontSide[2]);
        plgFront3.Fill = Color.FromArgb(aFrontSide[3]);
        plgFront4.Fill = Color.FromArgb(aFrontSide[4]);
        plgFront5.Fill = Color.FromArgb(aFrontSide[5]);
        plgFront6.Fill = Color.FromArgb(aFrontSide[6]);
        plgFront7.Fill = Color.FromArgb(aFrontSide[7]);
        plgFront8.Fill = Color.FromArgb(aFrontSide[8]);
        plgFront9.Fill = Color.FromArgb(aFrontSide[9]);

        plgRight1.Fill = Color.FromArgb(aRightSide[1]);
        plgRight2.Fill = Color.FromArgb(aRightSide[2]);
        plgRight3.Fill = Color.FromArgb(aRightSide[3]);
        plgRight4.Fill = Color.FromArgb(aRightSide[4]);
        plgRight5.Fill = Color.FromArgb(aRightSide[5]);
        plgRight6.Fill = Color.FromArgb(aRightSide[6]);
        plgRight7.Fill = Color.FromArgb(aRightSide[7]);
        plgRight8.Fill = Color.FromArgb(aRightSide[8]);
        plgRight9.Fill = Color.FromArgb(aRightSide[9]);

        plgLeft1.Fill = Color.FromArgb(aLeftSide[1]);
        plgLeft2.Fill = Color.FromArgb(aLeftSide[2]);
        plgLeft3.Fill = Color.FromArgb(aLeftSide[3]);
        plgLeft4.Fill = Color.FromArgb(aLeftSide[4]);
        plgLeft5.Fill = Color.FromArgb(aLeftSide[5]);
        plgLeft6.Fill = Color.FromArgb(aLeftSide[6]);
        plgLeft7.Fill = Color.FromArgb(aLeftSide[7]);
        plgLeft8.Fill = Color.FromArgb(aLeftSide[8]);
        plgLeft9.Fill = Color.FromArgb(aLeftSide[9]);

        plgBack1.Fill = Color.FromArgb(aBackSide[1]);
        plgBack2.Fill = Color.FromArgb(aBackSide[2]);
        plgBack3.Fill = Color.FromArgb(aBackSide[3]);
        plgBack4.Fill = Color.FromArgb(aBackSide[4]);
        plgBack5.Fill = Color.FromArgb(aBackSide[5]);
        plgBack6.Fill = Color.FromArgb(aBackSide[6]);
        plgBack7.Fill = Color.FromArgb(aBackSide[7]);
        plgBack8.Fill = Color.FromArgb(aBackSide[8]);
        plgBack9.Fill = Color.FromArgb(aBackSide[9]);

        plgBottom1.Fill = Color.FromArgb(aBottomSide[1]);
        plgBottom2.Fill = Color.FromArgb(aBottomSide[2]);
        plgBottom3.Fill = Color.FromArgb(aBottomSide[3]);
        plgBottom4.Fill = Color.FromArgb(aBottomSide[4]);
        plgBottom5.Fill = Color.FromArgb(aBottomSide[5]);
        plgBottom6.Fill = Color.FromArgb(aBottomSide[6]);
        plgBottom7.Fill = Color.FromArgb(aBottomSide[7]);
        plgBottom8.Fill = Color.FromArgb(aBottomSide[8]);
        plgBottom9.Fill = Color.FromArgb(aBottomSide[9]);
    }

    // Enable or Disable the cube colors for drag and drop.
    private void IsEnabledCubeColors(bool bEnableDisable)
    {
        plgCubeColor1.IsEnabled = bEnableDisable;
        plgCubeColor2.IsEnabled = bEnableDisable;
        plgCubeColor3.IsEnabled = bEnableDisable;
        plgCubeColor4.IsEnabled = bEnableDisable;
        plgCubeColor5.IsEnabled = bEnableDisable;
        plgCubeColor6.IsEnabled = bEnableDisable;
    }

    // Set the cube colors for drag and drop to visible or invisible.
    private void IsVisibleCubeColors(bool bEnableDisable)
    {
        plgCubeColorSelect.IsVisible = bEnableDisable;
        plgCubeColor1.IsVisible = bEnableDisable;
        plgCubeColor2.IsVisible = bEnableDisable;
        plgCubeColor3.IsVisible = bEnableDisable;
        plgCubeColor4.IsVisible = bEnableDisable;
        plgCubeColor5.IsVisible = bEnableDisable;
        plgCubeColor6.IsVisible = bEnableDisable;
    }

    // Enable or Disable the arrows.
    private void IsEnabledArrows(bool bEnableDisable)
    {
        imgbtnTurnFrontSideToRight.IsEnabled = bEnableDisable;
        imgbtnTurnTopMiddleToRightSide.IsEnabled = bEnableDisable;
        imgbtnTurnBackSideToLeft.IsEnabled = bEnableDisable;
        imgbtnTurnLeftSideToRight.IsEnabled = bEnableDisable;
        imgbtnTurnTopMiddleToFrontSide.IsEnabled = bEnableDisable;
        imgbtnTurnRightSideToLeft.IsEnabled = bEnableDisable;
        imgbtnTurnTopSideToLeft.IsEnabled = bEnableDisable;
        imgbtnTurnFrontMiddleToRightSide.IsEnabled = bEnableDisable;
        imgbtnTurnBottomSideToRight.IsEnabled = bEnableDisable;
        imgbtnTurnTopSideToRight.IsEnabled = bEnableDisable;
        imgbtnTurnRightMiddleToFrontSide.IsEnabled = bEnableDisable;
        imgbtnTurnBottomSideToLeft.IsEnabled = bEnableDisable;
        imgbtnTurnLeftSideToLeft.IsEnabled = bEnableDisable;
        imgbtnTurnFrontMiddleToTopSide.IsEnabled = bEnableDisable;
        imgbtnTurnRightSideToRight.IsEnabled = bEnableDisable;
        imgbtnTurnFrontSideToLeft.IsEnabled = bEnableDisable;
        imgbtnTurnRightMiddleToTopSide.IsEnabled = bEnableDisable;
        imgbtnTurnBackSideToRight.IsEnabled = bEnableDisable;
    }

    // Set the arrows to visible or invisible.
    private void IsVisibleArrows(bool bVisibleInvisible)
    {
        imgbtnTurnFrontSideToRight.IsVisible = bVisibleInvisible;
        imgbtnTurnTopMiddleToRightSide.IsVisible = bVisibleInvisible;
        imgbtnTurnBackSideToLeft.IsVisible = bVisibleInvisible;
        imgbtnTurnLeftSideToRight.IsVisible = bVisibleInvisible;
        imgbtnTurnTopMiddleToFrontSide.IsVisible = bVisibleInvisible;
        imgbtnTurnRightSideToLeft.IsVisible = bVisibleInvisible;
        imgbtnTurnTopSideToLeft.IsVisible = bVisibleInvisible;
        imgbtnTurnFrontMiddleToRightSide.IsVisible = bVisibleInvisible;
        imgbtnTurnBottomSideToRight.IsVisible = bVisibleInvisible;
        imgbtnTurnTopSideToRight.IsVisible = bVisibleInvisible;
        imgbtnTurnRightMiddleToFrontSide.IsVisible = bVisibleInvisible;
        imgbtnTurnBottomSideToLeft.IsVisible = bVisibleInvisible;
        imgbtnTurnLeftSideToLeft.IsVisible = bVisibleInvisible;
        imgbtnTurnFrontMiddleToTopSide.IsVisible = bVisibleInvisible;
        imgbtnTurnRightSideToRight.IsVisible = bVisibleInvisible;
        imgbtnTurnFrontSideToLeft.IsVisible = bVisibleInvisible;
        imgbtnTurnRightMiddleToTopSide.IsVisible = bVisibleInvisible;
        imgbtnTurnBackSideToRight.IsVisible = bVisibleInvisible;
    }

    // Get the hex color code from a polygon fill property.
    // Based on: https://stackoverflow.com/questions/12842003/c-sharp-brush-to-string
    private static string GetHexColorPolygon(object sender)
    {
        var polygon = (Polygon)sender;

        SolidColorBrush brush = (SolidColorBrush)polygon.Fill;
        Color c = brush.Color;

        var colorname = (from p in typeof(System.Drawing.Color).GetProperties()
                         where p.PropertyType.Equals(typeof(System.Drawing.Color))
                         let value = (System.Drawing.Color)p.GetValue(null, null)
                         where value.R == c.Red &&
                               value.G == c.Green &&
                               value.B == c.Blue &&
                               value.A == c.Alpha
                         select p.Name).DefaultIfEmpty("unknown").First();

        c = Color.FromRgb(c.Red, c.Green, c.Blue);
        return c.ToHex();
    }
    
    // Show license using the Loaded event of the MainPage.xaml.
    private async void OnPageLoad(object sender, EventArgs e)
    {
        // Show license.
        string cLicense = CubeLang.License_Text + "\n\n" + CubeLang.LicenseMit2_Text;

        if (bLicense == false)
        {
            bool bAnswer = await Application.Current.MainPage.DisplayAlert(CubeLang.LicenseTitle_Text, cLicense, CubeLang.Agree_Text, CubeLang.Disagree_Text);

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
                BtnSolve.IsEnabled = false;
                imgbtnSetColors.IsEnabled = false;
                imgbtnOpen.IsEnabled = false;
                imgbtnSave.IsEnabled = false;
                BtnReset.IsEnabled = false;
                IsEnabledArrows(false);

                await DisplayAlert(CubeLang.LicenseTitle_Text, CubeLang.CloseApplication_Text, CubeLang.ButtonClose_Text);
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
        }
    }

    // Put text in the chosen language in the controls.
    private static void SetTextLanguage()
    {
        // Set the current UI culture of the selected language.
        SetCultureSelectedLanguage();
    }

    // Set the current UI culture of the selected language.
    public static void SetCultureSelectedLanguage()
    {
        try
        {
            var switchToCulture = new CultureInfo(cLanguage);
            LocalizationResourceManager.Instance.SetCulture(switchToCulture);
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
            await DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message + "\n\n" + CubeLang.TextToSpeechError_Text, CubeLang.ButtonClose_Text);
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
            DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
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
        }

        // Start with the text to speech.
        if (cTurnCubeText != null && cTurnCubeText != "")
        {
            bTextToSpeechIsBusy = true;

            try
            {
                cts = new CancellationTokenSource();

                SpeechOptions options = new()
                {
                    Locale = locales.Single(l => l.Language + "-" + l.Country + " " + l.Name == cLanguageSpeech)
                };

                await TextToSpeech.Default.SpeakAsync(cTurnCubeText, options, cancelToken: cts.Token);
                bTextToSpeechIsBusy = false;
            }
            catch (Exception ex)
            {
                await DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
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
