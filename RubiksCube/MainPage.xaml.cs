﻿// Program .....: RubiksCube.sln
// Author ......: Geert Geerits - E-mail: geertgeerits@gmail.com
// Copyright ...: (C) 1981-2023
// Version .....: 2.0.10
// Date ........: 2023-02-04 (YYYY-MM-DD)
// Language ....: Microsoft Visual Studio 2022: .NET MAUI C# 11.0
// Description .: Solving the Rubik's Cube
// Note ........: This program is based on a program I wrote in 1981 in MS Basic-80 for a Commodore PET 2001.
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
    private readonly string cColorArrowNotActive = "E2E2E2";    // Lightgray
    private readonly string cColorArrowActive = "FFD000";       // Light orange
    private string[] aCubeColors = new string[7];
    private string[] aTopSide = new string[10];
    private string[] aFrontSide = new string[10];
    private string[] aRightSide = new string[10];
    private string[] aLeftSide = new string[10];
    private string[] aBackSide = new string[10];
    private string[] aBottomSide = new string[10];

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

        plgCubeColorSelect.Fill = Color.FromArgb("000000");
    }
    
    // Solve the cube.
    private async void OnBtnSolveClicked(object sender, EventArgs e)
    {
        if (!CheckNumberColorsCube(sender, e))
        {
            return;
        }

        //EnableDisableArrows(false);
        VisibleInvisibleArrows(false);

        bool bExplainTextSaved = bExplainText;
        bExplainText = false;
        
        await SolveTheCube();
        
        bExplainText = bExplainTextSaved;
        
        EnableDisableArrows(true);
        VisibleInvisibleArrows(true);
    }

    // Solve the cube.
    private async Task SolveTheCube()
    {
        // Solve the edges of the top layer.
        if (plgTop5.Fill == plgBottom2.Fill && plgFront5.Fill == plgFront8.Fill)
        {
            TurnFrontSideTo("+");
            TurnFrontSideTo("+");
            if (await DisplayAlert(CubeLang.Solve_Text, CubeLang.TurnFrontSideHalfTurn_Text, CubeLang.Continue_Text, CubeLang.Stop_Text) == false)
                return;
        }

        if (plgTop5.Fill == plgBottom4.Fill && plgLeft5.Fill == plgLeft8.Fill)
        {
            TurnLeftSideTo("+");
            TurnLeftSideTo("+");
            if (await DisplayAlert(CubeLang.Solve_Text, CubeLang.TurnLeftSideHalfTurn_Text, CubeLang.Continue_Text, CubeLang.Stop_Text) == false)
                return;
        }

            // Solve the corners of the top layer.

            // Solve the middle layer.

            // Solve the bottom layer.

            // Put the edges on the correct place.

            // Flip the corners.

            // Turning the edges.



            //TurnCubeFrontSideToLeftSide();
            //if (await DisplayAlert(CubeLang.Solve_Text, CubeLang.TurnCubeFrontSideToLeftSide_Text, CubeLang.Continue_Text, CubeLang.Stop_Text) == false)
            //    return;

            //TurnCubeFrontSideToRightSide();
            //if (await DisplayAlert(CubeLang.Solve_Text, CubeLang.TurnCubeFrontSideToRightSide_Text, CubeLang.Continue_Text, CubeLang.Stop_Text) == false)
            //    return;

            //TurnCubeFrontSideToTopSide();
            //if (await DisplayAlert(CubeLang.Solve_Text, CubeLang.TurnCubeFrontSideToTopSide_Text, CubeLang.Continue_Text, CubeLang.Stop_Text) == false)
            //    return;

            //TurnCubeFrontSideToBottomSide();
            //if (await DisplayAlert(CubeLang.Solve_Text, CubeLang.TurnCubeFrontSideToBottomSide_Text, CubeLang.Continue_Text, CubeLang.Stop_Text) == false)
            //    return;

            if (!CheckIfCubeIsSolved(false))
        {
            return;
        }
    }

    // Check the number of colors of the cube.
    private bool CheckNumberColorsCube(object sender, EventArgs e)
    {
        int nNumberOfColors1 = 0;
        int nNumberOfColors2 = 0;
        int nNumberOfColors3 = 0;
        int nNumberOfColors4 = 0;
        int nNumberOfColors5 = 0;
        int nNumberOfColors6 = 0;
        
        SetCubeColorsInArrays(sender, e);
        
        int nRow;

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

        if (plgFront1.Fill == plgFront2.Fill && plgFront1.Fill == plgFront3.Fill && plgFront1.Fill == plgFront4.Fill && plgFront1.Fill == plgFront5.Fill && plgFront1.Fill == plgFront6.Fill && plgFront1.Fill == plgFront7.Fill && plgFront1.Fill == plgFront8.Fill && plgFront1.Fill == plgFront9.Fill)
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
    private void On_imgbtnTurnFrontSideToRight_Clicked(object sender, EventArgs e)
    {
        TurnFrontSideTo("+");
        ExplainTurnCube(sender, CubeLang.TurnFrontSideToRight_Text);
    }

    // Turn the top middle to the right side (+).
    private void On_imgbtnTurnTopMiddleToRightSide_Clicked(object sender, EventArgs e)
    {
        TurnTopMiddleTo("+");
        ExplainTurnCube(sender, CubeLang.TurnTopMiddleToRightSide_Text);
    }

    // Turn the back side counter clockwise (to left -).
    private void On_imgbtnTurnBackSideToLeft_Clicked(object sender, EventArgs e)
    {
        TurnBackSideTo("-");
        ExplainTurnCube(sender, CubeLang.TurnBackSideToLeft_Text);
    }

    // Turn the left side clockwise (to right +).
    private void On_imgbtnTurnLeftSideToRight_Clicked(object sender, EventArgs e)
    {
        TurnLeftSideTo("+");
        ExplainTurnCube(sender, CubeLang.TurnLeftSideToRight_Text);
    }

    // Turn the top middle to the front side (-).
    private void On_imgbtnTurnTopMiddleToFrontSide_Clicked(object sender, EventArgs e)
    {
        TurnFrontTopMiddleTo("-");
        ExplainTurnCube(sender, CubeLang.TurnTopMiddleToFrontSide_Text);
    }

    // Turn the right side counter clockwise (to left -).
    private void On_imgbtnTurnRightSideToLeft_Clicked(object sender, EventArgs e)
    {
        TurnRightSideTo("-");
        ExplainTurnCube(sender, CubeLang.TurnRightSideToLeft_Text);
    }

    // Turn the top side counter clockwise (to left -).
    private void On_imgbtnTurnTopSideToLeft_Clicked(object sender, EventArgs e)
    {
        TurnTopSideTo("-");
        ExplainTurnCube(sender, CubeLang.TurnTopSideToLeft_Text);
    }

    // Turn the front middle to the right side (-).
    private void On_imgbtnTurnFrontMiddleToRightSide_Clicked(object sender, EventArgs e)
    {
        TurnHorizontalMiddleLayerTo("-");
        ExplainTurnCube(sender, CubeLang.TurnFrontMiddleToRightSide_Text);
    }

    // Turn the bottom side clockwise (to right +).
    private void On_imgbtnTurnBottomSideToRight_Clicked(object sender, EventArgs e)
    {
        TurnBottomSideTo("+");
        ExplainTurnCube(sender, CubeLang.TurnBottomSideToRight_Text);
    }

    // Turn the top side clockwise (to right +).
    private void On_imgbtnTurnTopSideToRight_Clicked(object sender, EventArgs e)
    {
        TurnTopSideTo("+");
        ExplainTurnCube(sender, CubeLang.TurnTopSideToRight_Text);
    }

    // Turn the right middle to the front side (+).
    private void On_imgbtnTurnRightMiddleToFrontSide_Clicked(object sender, EventArgs e)
    {
        TurnHorizontalMiddleLayerTo("+");
        ExplainTurnCube(sender, CubeLang.TurnRightMiddleToFrontSide_Text);
    }

    // Turn the bottom side counter clockwise (to left -).
    private void On_imgbtnTurnBottomSideToLeft_Clicked(object sender, EventArgs e)
    {
        TurnBottomSideTo("-");
        ExplainTurnCube(sender, CubeLang.TurnBottomSideToLeft_Text);
    }

    // Turn the left side counter clockwise (to left -).
    private void On_imgbtnTurnLeftSideToLeft_Clicked(object sender, EventArgs e)
    {
        TurnLeftSideTo("-");
        ExplainTurnCube(sender, CubeLang.TurnLeftSideToLeft_Text);
    }

    // Turn the front middle to the top side (+).
    private void On_imgbtnTurnFrontMiddleToTopSide_Clicked(object sender, EventArgs e)
    {
        TurnFrontTopMiddleTo("+");
        ExplainTurnCube(sender, CubeLang.TurnFrontMiddleToTopSide_Text);
    }

    // Turn the right side clockwise (to right +).
    private void On_imgbtnTurnRightSideToRight_Clicked(object sender, EventArgs e)
    {
        TurnRightSideTo("+");
        ExplainTurnCube(sender, CubeLang.TurnRightSideToRight_Text);
    }

    // Turn the front side counter clockwise (to left -).
    private void On_imgbtnTurnFrontSideToLeft_Clicked(object sender, EventArgs e)
    {
        TurnFrontSideTo("-");
        ExplainTurnCube(sender, CubeLang.TurnFrontSideToLeft_Text);
    }

    // Turn the right middle to the top side (-).
    private void On_imgbtnTurnRightMiddleToTopSide_Clicked(object sender, EventArgs e)
    {
        TurnTopMiddleTo("-");
        ExplainTurnCube(sender, CubeLang.TurnRightMiddleToTopSide_Text);
    }

    // Turn the back side clockwise (to right +).
    private void On_imgbtnTurnBackSideToRight_Clicked(object sender, EventArgs e)
    {
        TurnBackSideTo("+");
        ExplainTurnCube(sender, CubeLang.TurnBackSideToRight_Text);
    }

    // Turn the the cube.
    // Turn the cube front side to the left side.
    private void TurnCubeFrontSideToLeftSide()
    {
        TurnTopSideTo("+");
        TurnHorizontalMiddleLayerTo("+");
        TurnBottomSideTo("-");
        ExplainTurnCube(null, CubeLang.TurnCubeFrontSideToLeftSide_Text);
    }

    // Turn the cube front side to the right side.
    private void TurnCubeFrontSideToRightSide()
    {
        TurnTopSideTo("-");
        TurnHorizontalMiddleLayerTo("-");
        TurnBottomSideTo("+");
        ExplainTurnCube(null, CubeLang.TurnCubeFrontSideToRightSide_Text);
    }

    // Turn the cube front side to the top side.
    private void TurnCubeFrontSideToTopSide()
    {
        TurnRightSideTo("+");
        TurnFrontTopMiddleTo("+");
        TurnLeftSideTo("-");
        ExplainTurnCube(null, CubeLang.TurnCubeFrontSideToTopSide_Text);
    }

    // Turn the cube front side to the bottom side.
    private void TurnCubeFrontSideToBottomSide()
    {
        TurnRightSideTo("-");
        TurnFrontTopMiddleTo("-");
        TurnLeftSideTo("+");
        ExplainTurnCube(null, CubeLang.TurnCubeFrontSideToBottomSide_Text);
    }

    // Turn the entire front side clockwise or counter clockwise.
    private void TurnFrontSideTo(string cDirection)
    {
        Brush ColorFront1 = plgFront1.Fill;
        Brush ColorFront2 = plgFront2.Fill;
        Brush ColorFront3 = plgFront3.Fill;
        Brush ColorFront4 = plgFront4.Fill;
        Brush ColorFront6 = plgFront6.Fill;
        Brush ColorFront7 = plgFront7.Fill;
        Brush ColorFront8 = plgFront8.Fill;
        Brush ColorFront9 = plgFront9.Fill;

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
            plgFront1.Fill = ColorFront7;
            plgFront2.Fill = ColorFront4;
            plgFront3.Fill = ColorFront1;
            plgFront4.Fill = ColorFront8;
            plgFront6.Fill = ColorFront2;
            plgFront7.Fill = ColorFront9;
            plgFront8.Fill = ColorFront6;
            plgFront9.Fill = ColorFront3;

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
            plgFront1.Fill = ColorFront3;
            plgFront2.Fill = ColorFront6;
            plgFront3.Fill = ColorFront9;
            plgFront4.Fill = ColorFront2;
            plgFront6.Fill = ColorFront8;
            plgFront7.Fill = ColorFront1;
            plgFront8.Fill = ColorFront4;
            plgFront9.Fill = ColorFront7;

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

        Brush ColorFront1 = plgFront1.Fill;
        Brush ColorFront4 = plgFront4.Fill;
        Brush ColorFront7 = plgFront7.Fill;

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

            plgFront1.Fill = ColorTop1;
            plgFront4.Fill = ColorTop4;
            plgFront7.Fill = ColorTop7;

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

            plgFront1.Fill = ColorBottom1;
            plgFront4.Fill = ColorBottom4;
            plgFront7.Fill = ColorBottom7;

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

        Brush ColorFront2 = plgFront2.Fill;
        Brush ColorFront5 = plgFront5.Fill;
        Brush ColorFront8 = plgFront8.Fill;

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

            plgFront2.Fill = ColorBottom2;
            plgFront5.Fill = ColorBottom5;
            plgFront8.Fill = ColorBottom8;

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

            plgFront2.Fill = ColorTop2;
            plgFront5.Fill = ColorTop5;
            plgFront8.Fill = ColorTop8;

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

        Brush ColorFront3 = plgFront3.Fill;
        Brush ColorFront6 = plgFront6.Fill;
        Brush ColorFront9 = plgFront9.Fill;

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

            plgFront3.Fill = ColorBottom3;
            plgFront6.Fill = ColorBottom6;
            plgFront9.Fill = ColorBottom9;

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

            plgFront3.Fill = ColorTop3;
            plgFront6.Fill = ColorTop6;
            plgFront9.Fill = ColorTop9;

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

        Brush ColorFront1 = plgFront1.Fill;
        Brush ColorFront2 = plgFront2.Fill;
        Brush ColorFront3 = plgFront3.Fill;

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

            plgFront1.Fill = ColorRight1;
            plgFront2.Fill = ColorRight2;
            plgFront3.Fill = ColorRight3;

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

            plgFront1.Fill = ColorLeft1;
            plgFront2.Fill = ColorLeft2;
            plgFront3.Fill = ColorLeft3;

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
        Brush ColorFront4 = plgFront4.Fill;
        Brush ColorFront5 = plgFront5.Fill;
        Brush ColorFront6 = plgFront6.Fill;

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
            plgFront4.Fill = ColorRight4;
            plgFront5.Fill = ColorRight5;
            plgFront6.Fill = ColorRight6;

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
            plgFront4.Fill = ColorLeft4;
            plgFront5.Fill = ColorLeft5;
            plgFront6.Fill = ColorLeft6;

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

        Brush ColorFront7 = plgFront7.Fill;
        Brush ColorFront8 = plgFront8.Fill;
        Brush ColorFront9 = plgFront9.Fill;

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

            plgFront7.Fill = ColorLeft7;
            plgFront8.Fill = ColorLeft8;
            plgFront9.Fill = ColorLeft9;

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

            plgFront7.Fill = ColorRight7;
            plgFront8.Fill = ColorRight8;
            plgFront9.Fill = ColorRight9;

            plgRight7.Fill = ColorBack7;
            plgRight8.Fill = ColorBack8;
            plgRight9.Fill = ColorBack9;

            plgBack7.Fill = ColorLeft7;
            plgBack8.Fill = ColorLeft8;
            plgBack9.Fill = ColorLeft9;
        }
    }

    // Explain the turn of the cube.
    private async void ExplainTurnCube(object sender, string cTurnCubeText)
    {
        if (sender != null)
        {
            var imagebutton = (ImageButton)sender;
            imagebutton.BackgroundColor = Color.FromArgb(cColorArrowActive);
        }

        string cTurnCubeSpeech = cTurnCubeText;

        if (bExplainSpeech)
        {
            // if the right of the text = ' (+).' or ' (-).' remove it.
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

    // On clicked event: Save the cube.
    private void OnBtnSaveClicked(object sender, EventArgs e)
    {
        //string cFileName = Path.Combine(FileSystem.CacheDirectory, "RubiksCube");
        string cFileName = FileSystem.CacheDirectory + "/RubiksCube.txt";
        int nRow;

        SetCubeColorsInArrays(sender, e);
        
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
    private void OnBtnOpenClicked(object sender, EventArgs e)
    {
        string cFileName = FileSystem.CacheDirectory + "/RubiksCube.txt";

        //File.Delete(cFileName);

        if (File.Exists(cFileName) == false)
        {
            return;
        }

        int nRow;
       
        try
        {
            // Open the text file using a stream reader.
            //using (var sr = new StreamReader(cFileName))
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

            // Close the StreamWriter object.
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

    // Store the cube colors in arrays.
    private void SetCubeColorsInArrays(object sender, EventArgs e)
    {
        //string test = GetHexColorPolygon(plgFront1, e);
        //DisplayAlert("", test, "OK");

        aCubeColors[1] = GetHexColorPolygon(plgCubeColor1, e);
        aCubeColors[2] = GetHexColorPolygon(plgCubeColor2, e);
        aCubeColors[3] = GetHexColorPolygon(plgCubeColor3, e);
        aCubeColors[4] = GetHexColorPolygon(plgCubeColor4, e);
        aCubeColors[5] = GetHexColorPolygon(plgCubeColor5, e);
        aCubeColors[6] = GetHexColorPolygon(plgCubeColor6, e);

        aTopSide[1] = GetHexColorPolygon(plgTop1, e);
        aTopSide[2] = GetHexColorPolygon(plgTop2, e);
        aTopSide[3] = GetHexColorPolygon(plgTop3, e);
        aTopSide[4] = GetHexColorPolygon(plgTop4, e);
        aTopSide[5] = GetHexColorPolygon(plgTop5, e);
        aTopSide[6] = GetHexColorPolygon(plgTop6, e);
        aTopSide[7] = GetHexColorPolygon(plgTop7, e);
        aTopSide[8] = GetHexColorPolygon(plgTop8, e);
        aTopSide[9] = GetHexColorPolygon(plgTop9, e);

        aFrontSide[1] = GetHexColorPolygon(plgFront1, e);
        aFrontSide[2] = GetHexColorPolygon(plgFront2, e);
        aFrontSide[3] = GetHexColorPolygon(plgFront3, e);
        aFrontSide[4] = GetHexColorPolygon(plgFront4, e);
        aFrontSide[5] = GetHexColorPolygon(plgFront5, e);
        aFrontSide[6] = GetHexColorPolygon(plgFront6, e);
        aFrontSide[7] = GetHexColorPolygon(plgFront7, e);
        aFrontSide[8] = GetHexColorPolygon(plgFront8, e);
        aFrontSide[9] = GetHexColorPolygon(plgFront9, e);

        aRightSide[1] = GetHexColorPolygon(plgRight1, e);
        aRightSide[2] = GetHexColorPolygon(plgRight2, e);
        aRightSide[3] = GetHexColorPolygon(plgRight3, e);
        aRightSide[4] = GetHexColorPolygon(plgRight4, e);
        aRightSide[5] = GetHexColorPolygon(plgRight5, e);
        aRightSide[6] = GetHexColorPolygon(plgRight6, e);
        aRightSide[7] = GetHexColorPolygon(plgRight7, e);
        aRightSide[8] = GetHexColorPolygon(plgRight8, e);
        aRightSide[9] = GetHexColorPolygon(plgRight9, e);

        aLeftSide[1] = GetHexColorPolygon(plgLeft1, e);
        aLeftSide[2] = GetHexColorPolygon(plgLeft2, e);
        aLeftSide[3] = GetHexColorPolygon(plgLeft3, e);
        aLeftSide[4] = GetHexColorPolygon(plgLeft4, e);
        aLeftSide[5] = GetHexColorPolygon(plgLeft5, e);
        aLeftSide[6] = GetHexColorPolygon(plgLeft6, e);
        aLeftSide[7] = GetHexColorPolygon(plgLeft7, e);
        aLeftSide[8] = GetHexColorPolygon(plgLeft8, e);
        aLeftSide[9] = GetHexColorPolygon(plgLeft9, e);

        aBackSide[1] = GetHexColorPolygon(plgBack1, e);
        aBackSide[2] = GetHexColorPolygon(plgBack2, e);
        aBackSide[3] = GetHexColorPolygon(plgBack3, e);
        aBackSide[4] = GetHexColorPolygon(plgBack4, e);
        aBackSide[5] = GetHexColorPolygon(plgBack5, e);
        aBackSide[6] = GetHexColorPolygon(plgBack6, e);
        aBackSide[7] = GetHexColorPolygon(plgBack7, e);
        aBackSide[8] = GetHexColorPolygon(plgBack8, e);
        aBackSide[9] = GetHexColorPolygon(plgBack9, e);

        aBottomSide[1] = GetHexColorPolygon(plgBottom1, e);
        aBottomSide[2] = GetHexColorPolygon(plgBottom2, e);
        aBottomSide[3] = GetHexColorPolygon(plgBottom3, e);
        aBottomSide[4] = GetHexColorPolygon(plgBottom4, e);
        aBottomSide[5] = GetHexColorPolygon(plgBottom5, e);
        aBottomSide[6] = GetHexColorPolygon(plgBottom6, e);
        aBottomSide[7] = GetHexColorPolygon(plgBottom7, e);
        aBottomSide[8] = GetHexColorPolygon(plgBottom8, e);
        aBottomSide[9] = GetHexColorPolygon(plgBottom9, e);
    }

    // Restore the cube colors from the arrays.
    private void GetCubeColorsFromArrays()
    {
        //cCubeColor1 = aCubeColors[1];
        //cCubeColor2 = aCubeColors[2];
        //cCubeColor3 = aCubeColors[3];
        //cCubeColor4 = aCubeColors[4];
        //cCubeColor5 = aCubeColors[5];
        //cCubeColor6 = aCubeColors[6];
        //DisplayAlert("", cCubeColor1, "OK");

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

        plgFront1.Fill = plgCubeColor2.Fill;
        plgFront2.Fill = plgCubeColor2.Fill;
        plgFront3.Fill = plgCubeColor2.Fill;
        plgFront4.Fill = plgCubeColor2.Fill;
        plgFront5.Fill = plgCubeColor2.Fill;
        plgFront6.Fill = plgCubeColor2.Fill;
        plgFront7.Fill = plgCubeColor2.Fill;
        plgFront8.Fill = plgCubeColor2.Fill;
        plgFront9.Fill = plgCubeColor2.Fill;

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

        EnableDisableArrows(true);
    }

    // Enable or Disable the arrows.
    private void EnableDisableArrows(bool bEnableDisable)
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

    // Make the arrows visible or invisible.
    private void VisibleInvisibleArrows(bool bVisibleInvisible)
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
    private string GetHexColorPolygon(object sender, EventArgs e)
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
                BtnReset.IsEnabled = false;
                EnableDisableArrows(false);

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

                SpeechOptions options = new SpeechOptions()
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
