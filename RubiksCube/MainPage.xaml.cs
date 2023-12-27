// Program .....: RubiksCube.sln
// Author ......: Geert Geerits - E-mail: geertgeerits@gmail.com
// Copyright ...: (C) 1981-2023
// Version .....: 2.0.11
// Date ........: 2023-12-27 (YYYY-MM-DD)
// Language ....: Microsoft Visual Studio 2022: .NET MAUI 8 - C# 12.0
// Description .: Solving the Rubik's Cube
// Note ........: This program is based on the program 'SolCube' I wrote in 1981 in MS Basic-80 for a Commodore PET 2001.
//                The solution for solving the cube is based on a book by Don Taylor, Mastering Rubik's Cube, Dutch version 1981.
// Dependencies : 
// Thanks to ...: Gerald Versluis

using Microsoft.Maui.Controls.Shapes;

namespace RubiksCube;

public partial class MainPage : ContentPage
{
    // Local variables.
    private IEnumerable<Locale> locales;
    private bool bColorDrop;
    private bool bSolvingCube;
    private bool bArrowButtonPressed;
    private readonly string cColorArrowInactive = "#E2E2E2";    // Lightgray
    private readonly string cColorArrowActive = "#FFD000";      // Light orange
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
        Globals.cTheme = Preferences.Default.Get("SettingTheme", "System");
        Globals.cLanguage = Preferences.Default.Get("SettingLanguage", "");
        Globals.cLanguageSpeech = Preferences.Default.Get("SettingLanguageSpeech", "");
        Globals.bExplainText = Preferences.Default.Get("SettingExplainText", false);
        Globals.bExplainSpeech = Preferences.Default.Get("SettingExplainSpeech", false);
        Globals.cCubeColor1 = Preferences.Default.Get("SettingCubeColor1", "#FFFFFF");   // White
        Globals.cCubeColor2 = Preferences.Default.Get("SettingCubeColor2", "#FF0000");   // Red
        Globals.cCubeColor3 = Preferences.Default.Get("SettingCubeColor3", "#0000FF");   // Blue
        Globals.cCubeColor4 = Preferences.Default.Get("SettingCubeColor4", "#008000");   // Green
        Globals.cCubeColor5 = Preferences.Default.Get("SettingCubeColor5", "#FF8000");   // Orange
        Globals.cCubeColor6 = Preferences.Default.Get("SettingCubeColor6", "#FFFF00");   // Yellow
        Globals.bLicense = Preferences.Default.Get("SettingLicense", false);

        // Set the theme.
        Globals.SetTheme();

        // Get and set the system OS user language.
        try
        {
            if (Globals.cLanguage == "")
            {
                Globals.cLanguage = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            }
        }
        catch (Exception)
        {
            Globals.cLanguage = "en";
        }

        SetTextLanguage();

        // Initialize text to speech and get and set the speech language.
        string cCultureName = "";

        try
        {
            if (Globals.cLanguageSpeech == "")
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
        aCubeColors[1] = Globals.cCubeColor1;
        aCubeColors[2] = Globals.cCubeColor2;
        aCubeColors[3] = Globals.cCubeColor3;
        aCubeColors[4] = Globals.cCubeColor4;
        aCubeColors[5] = Globals.cCubeColor5;
        aCubeColors[6] = Globals.cCubeColor6;

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
            SetArrowBackgroundColorToInactive();

            btnSolve.IsEnabled = false;
            grdCubeColorSelect.BackgroundColor = Color.FromArgb("#969696");
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

            SetArrowBackgroundColorToActive();

            ToolTipProperties.SetText(imgbtnTurnTopMiddleToRightSide, CubeLang.TurnTopMiddleToRightSide_Text);
            ToolTipProperties.SetText(imgbtnTurnTopMiddleToFrontSide, CubeLang.TurnTopMiddleToFrontSide_Text);
            ToolTipProperties.SetText(imgbtnTurnFrontMiddleToRightSide, CubeLang.TurnFrontMiddleToRightSide_Text);
            ToolTipProperties.SetText(imgbtnTurnRightMiddleToFrontSide, CubeLang.TurnRightMiddleToFrontSide_Text);
            ToolTipProperties.SetText(imgbtnTurnFrontMiddleToTopSide, CubeLang.TurnFrontMiddleToTopSide_Text);
            ToolTipProperties.SetText(imgbtnTurnRightMiddleToTopSide, CubeLang.TurnRightMiddleToTopSide_Text);

            IsEnabledArrows(true);
            IsVisibleCubeColors(false);
            grdCubeColorSelect.BackgroundColor = Color.FromArgb("#00000000");

            btnSolve.IsEnabled = true;
        }
    }

    // Solve the cube.
    private async void OnBtnSolveClicked(object sender, EventArgs e)
    {
        // Check the number of colors of the cube.
        if (!CheckNumberColorsCube())
        {
            return;
        }

        // Settings.
        SetArrowBackgroundColorToInactive();

        btnSolve.IsEnabled = false;
        imgbtnSetColors.BackgroundColor = Color.FromArgb(cColorArrowInactive);
        imgbtnSetColors.IsEnabled = false;
        imgbtnOpen.BackgroundColor = Color.FromArgb(cColorArrowInactive);
        imgbtnOpen.IsEnabled = false;
        imgbtnSave.BackgroundColor = Color.FromArgb(cColorArrowInactive);
        imgbtnSave.IsEnabled = false;

        bColorDrop = false;
        bSolvingCube = true;
        SetArrowTooltips(false);
        IsEnabledArrows(false);

        lblCubeOutsideView.IsVisible = false;
        lblExplainTurnCube1.IsVisible = true;
        lblCubeInsideView.IsVisible = false;
        lblExplainTurnCube2.IsVisible = true;

        // Solve the cube.
        await SolveTheCubeAsync();

        // Settings.
        lblExplainTurnCube1.Text = "";
        lblExplainTurnCube1.IsVisible = false;
        lblCubeOutsideView.IsVisible = true;
        lblExplainTurnCube2.Text = "";
        lblExplainTurnCube2.IsVisible = false;
        lblCubeInsideView.IsVisible = true;

        IsEnabledArrows(true);
        SetArrowTooltips(true);
        bSolvingCube = false;

        btnSolve.IsEnabled = true;
        imgbtnSetColors.IsEnabled = true;
        imgbtnSetColors.BackgroundColor = Color.FromArgb(cColorArrowActive);
        imgbtnOpen.IsEnabled = true;
        imgbtnOpen.BackgroundColor = Color.FromArgb(cColorArrowActive);
        imgbtnSave.IsEnabled = true;
        imgbtnSave.BackgroundColor = Color.FromArgb(cColorArrowActive);

        SetArrowBackgroundColorToActive();
    }

    // Solve the cube.
    private async Task SolveTheCubeAsync()
    {
        // Solve the edges of the top layer - Chapter 4, page 14-3.
        await SolveEdgesTopLayerAsync();

        // Solve the edges of the top layer - Chapter 4, page 14-2.
        if (aTopSide[5] == aFrontSide[4])
        {
            await ExplainSolveTurnCubeAsync(imgbtnTurnLeftSideToRight, CubeLang.TurnLeftSideToRight_Text);
            TurnLeftSideTo("+");
            GetCubeColorsFromArrays();

            if (aLeftSide[8] == aFrontSide[5])
            {
                await ExplainSolveTurnCubeAsync(imgbtnTurnBottomSideToRight, CubeLang.TurnBottomSideToRight_Text);
                TurnBottomSideTo("+");
                GetCubeColorsFromArrays();
            }

            if (aLeftSide[8] == aBackSide[5])
            {
                await ExplainSolveTurnCubeAsync(imgbtnTurnBottomSideToLeft, CubeLang.TurnBottomSideToLeft_Text);
                TurnBottomSideTo("-");
                GetCubeColorsFromArrays();
            }

            if (aLeftSide[8] == aRightSide[5])
            {
                await ExplainSolveTurnCubeAsync(imgbtnTurnBottomSideToRight, CubeLang.TurnBottomSideHalfTurn_Text);
                TurnBottomSideTo("+");
                TurnBottomSideTo("+");
                GetCubeColorsFromArrays();
            }
        }

        if (aTopSide[5] == aFrontSide[6])
        {
            await ExplainSolveTurnCubeAsync(imgbtnTurnRightSideToLeft, CubeLang.TurnRightSideToLeft_Text);
            TurnRightSideTo("-");
            GetCubeColorsFromArrays();

            if (aRightSide[8] == aFrontSide[5])
            {
                await ExplainSolveTurnCubeAsync(imgbtnTurnBottomSideToLeft, CubeLang.TurnBottomSideToLeft_Text);
                TurnBottomSideTo("-");
                GetCubeColorsFromArrays();
            }

            if (aRightSide[8] == aBackSide[5])
            {
                await ExplainSolveTurnCubeAsync(imgbtnTurnBottomSideToRight, CubeLang.TurnBottomSideToRight_Text);
                TurnBottomSideTo("+");
                GetCubeColorsFromArrays();
            }

            if (aRightSide[8] == aLeftSide[5])
            {
                await ExplainSolveTurnCubeAsync(imgbtnTurnBottomSideToRight, CubeLang.TurnBottomSideHalfTurn_Text);
                TurnBottomSideTo("+");
                TurnBottomSideTo("+");
                GetCubeColorsFromArrays();
            }
        }



        // Solve the edges of the top layer - Chapter 4, page 14-3.
        await SolveEdgesTopLayerAsync();

        // Solve the corners of the top layer - Chapter 6, page 16.

        // Solve the middle layer - Chapter 10, page 21.

        // Solve the bottom layer - Chapter 11, page 23.

        // Put the edges on the correct place.

        // Flip the corners.

        // Turning the edges.



        // For testing.
        //await ExplainSolveTurnCubeAsync(imgbtnTurnTopMiddleToRightSide, CubeLang.TurnCubeTopSideToRightSide_Text);
        //TurnCubeTopSideToRightSide();

        //await ExplainSolveTurnCubeAsync(imgbtnTurnTopMiddleToFrontSide, CubeLang.TurnCubeFrontSideToBottomSide_Text);
        //TurnCubeFrontSideToBottomSide();

        //await ExplainSolveTurnCubeAsync(imgbtnTurnFrontMiddleToRightSide, CubeLang.TurnCubeFrontSideToRightSide_Text);
        //TurnCubeFrontSideToRightSide();

        //await ExplainSolveTurnCubeAsync(imgbtnTurnRightMiddleToFrontSide, CubeLang.TurnCubeFrontSideToLeftSide_Text);
        //TurnCubeFrontSideToLeftSide();

        //await ExplainSolveTurnCubeAsync(imgbtnTurnFrontMiddleToTopSide, CubeLang.TurnCubeFrontSideToTopSide_Text);
        //TurnCubeFrontSideToTopSide();

        //await ExplainSolveTurnCubeAsync(imgbtnTurnRightMiddleToTopSide, CubeLang.TurnCubeTopSideToLeftSide_Text);
        //TurnCubeTopSideToLeftSide();

        if (!CheckIfCubeIsSolved(false))
        {
            return;
        }
    }

    // Solve the edges of the top layer - Chapter 4, page 14-3.
    private async Task SolveEdgesTopLayerAsync()
    {
        for (int nTimes = 1; nTimes < 5; nTimes++)
        {
            if (aTopSide[5] == aBottomSide[2] && aFrontSide[5] == aFrontSide[8])
            {
                await ExplainSolveTurnCubeAsync(imgbtnTurnFrontSideToRight, CubeLang.TurnFrontSideHalfTurn_Text);
                TurnFrontSideTo("+");
                TurnFrontSideTo("+");
                GetCubeColorsFromArrays();
            }

            if (aTopSide[5] == aBottomSide[4] && aLeftSide[5] == aLeftSide[8])
            {
                await ExplainSolveTurnCubeAsync(imgbtnTurnLeftSideToRight, CubeLang.TurnLeftSideHalfTurn_Text);
                TurnLeftSideTo("+");
                TurnLeftSideTo("+");
                GetCubeColorsFromArrays();
            }

            if (aTopSide[5] == aBottomSide[6] && aRightSide[5] == aRightSide[8])
            {
                await ExplainSolveTurnCubeAsync(imgbtnTurnRightSideToRight, CubeLang.TurnRightSideHalfTurn_Text);
                TurnRightSideTo("+");
                TurnRightSideTo("+");
                GetCubeColorsFromArrays();
            }

            if (aTopSide[5] == aBottomSide[8] && aBackSide[5] == aBackSide[8])
            {
                await ExplainSolveTurnCubeAsync(imgbtnTurnBackSideToRight, CubeLang.TurnBackSideHalfTurn_Text);
                TurnBackSideTo("+");
                TurnBackSideTo("+");
                GetCubeColorsFromArrays();
            }
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

        if (Globals.bExplainSpeech)
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
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnFrontSideToRight_Text);
        TurnFrontSideTo("+");
        GetCubeColorsFromArrays();
    }

    // Turn the top middle to the right side (+).
    private void OnTurnTopMiddleToRightSideClicked(object sender, EventArgs e)
    {
        if (bColorDrop)
        {
            TurnCubeTopSideToRightSide();
            return;
        }

        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnTopMiddleToRightSide_Text);
        TurnTopMiddleTo("+");
        GetCubeColorsFromArrays();
    }

    // Turn the back side counter clockwise (to left -).
    private void OnTurnBackSideToLeftClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnBackSideToLeft_Text);
        TurnBackSideTo("-");
        GetCubeColorsFromArrays();
    }

    // Turn the left side clockwise (to right +).
    private void OnTurnLeftSideToRightClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnLeftSideToRight_Text);
        TurnLeftSideTo("+");
        GetCubeColorsFromArrays();
    }

    // Turn the top middle to the front side (-).
    private void OnTurnTopMiddleToFrontSideClicked(object sender, EventArgs e)
    {
        if (bColorDrop)
        {
            TurnCubeFrontSideToBottomSide();
            return;
        }

        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnTopMiddleToFrontSide_Text);
        TurnFrontTopMiddleTo("-");
        GetCubeColorsFromArrays();
    }

    // Turn the right side counter clockwise (to left -).
    private void OnTurnRightSideToLeftClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnRightSideToLeft_Text);
        TurnRightSideTo("-");
        GetCubeColorsFromArrays();
    }

    // Turn the top side counter clockwise (to left -).
    private void OnTurnTopSideToLeftClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnTopSideToLeft_Text);
        TurnTopSideTo("-");
        GetCubeColorsFromArrays();
    }

    // Turn the front middle to the right side (-).
    private void OnTurnFrontMiddleToRightSideClicked(object sender, EventArgs e)
    {
        if (bColorDrop)
        {
            TurnCubeFrontSideToRightSide();
            return;
        }

        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnFrontMiddleToRightSide_Text);
        TurnHorizontalMiddleLayerTo("-");
        GetCubeColorsFromArrays();
    }

    // Turn the bottom side clockwise (to right +).
    private void OnTurnBottomSideToRightClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnBottomSideToRight_Text);
        TurnBottomSideTo("+");
        GetCubeColorsFromArrays();
    }

    // Turn the top side clockwise (to right +).
    private void OnTurnTopSideToRightClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnTopSideToRight_Text);
        TurnTopSideTo("+");
        GetCubeColorsFromArrays();
    }

    // Turn the right middle to the front side (+).
    private void OnTurnRightMiddleToFrontSideClicked(object sender, EventArgs e)
    {
        if (bColorDrop)
        {
            TurnCubeFrontSideToLeftSide();
            return;
        }

        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnRightMiddleToFrontSide_Text);
        TurnHorizontalMiddleLayerTo("+");
        GetCubeColorsFromArrays();
    }

    // Turn the bottom side counter clockwise (to left -).
    private void OnTurnBottomSideToLeftClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnBottomSideToLeft_Text);
        TurnBottomSideTo("-");
        GetCubeColorsFromArrays();
    }

    // Turn the left side counter clockwise (to left -).
    private void OnTurnLeftSideToLeftClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnLeftSideToLeft_Text);
        TurnLeftSideTo("-");
        GetCubeColorsFromArrays();
    }

    // Turn the front middle to the top side (+).
    private void OnTurnFrontMiddleToTopSideClicked(object sender, EventArgs e)
    {
        if (bColorDrop)
        {
            TurnCubeFrontSideToTopSide();
            return;
        }

        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnFrontMiddleToTopSide_Text);
        TurnFrontTopMiddleTo("+");
        GetCubeColorsFromArrays();
    }

    // Turn the right side clockwise (to right +).
    private void OnTurnRightSideToRightClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }
        
        ExplainTurnCube(CubeLang.TurnRightSideToRight_Text);
        TurnRightSideTo("+");
        GetCubeColorsFromArrays();
    }

    // Turn the front side counter clockwise (to left -).
    private void OnTurnFrontSideToLeftClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnFrontSideToLeft_Text);
        TurnFrontSideTo("-");
        GetCubeColorsFromArrays();
    }

    // Turn the right middle to the top side (-).
    private void OnTurnRightMiddleToTopSideClicked(object sender, EventArgs e)
    {
        if (bColorDrop)
        {
            TurnCubeTopSideToLeftSide();
            return;
        }

        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnRightMiddleToTopSide_Text);
        TurnTopMiddleTo("-");
        GetCubeColorsFromArrays();
    }

    // Turn the back side clockwise (to right +).
    private void OnTurnBackSideToRightClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnBackSideToRight_Text);
        TurnBackSideTo("+");
        GetCubeColorsFromArrays();
    }

    // Turn the entire cube a quarter turn.
    // Rotate the entire cube so that the front goes to the left side.
    private void TurnCubeFrontSideToLeftSide()
    {
        if (!bSolvingCube)
        {
            ExplainTurnCube(CubeLang.TurnCubeFrontSideToLeftSide_Text);
        }
            
        TurnTopSideTo("+");
        TurnHorizontalMiddleLayerTo("+");
        TurnBottomSideTo("-");
        GetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the front goes to the right side.
    private void TurnCubeFrontSideToRightSide()
    {
        if (!bSolvingCube)
        {
            ExplainTurnCube(CubeLang.TurnCubeFrontSideToRightSide_Text);
        }

        TurnTopSideTo("-");
        TurnHorizontalMiddleLayerTo("-");
        TurnBottomSideTo("+");
        GetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the front goes to the top side.
    private void TurnCubeFrontSideToTopSide()
    {
        if (!bSolvingCube)
        {
            ExplainTurnCube(CubeLang.TurnCubeFrontSideToTopSide_Text);
        }

        TurnRightSideTo("+");
        TurnFrontTopMiddleTo("+");
        TurnLeftSideTo("-");
        GetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the front goes to the bottom side.
    private void TurnCubeFrontSideToBottomSide()
    {
        if (!bSolvingCube)
        {
            ExplainTurnCube(CubeLang.TurnCubeFrontSideToBottomSide_Text);
        }

        TurnRightSideTo("-");
        TurnFrontTopMiddleTo("-");
        TurnLeftSideTo("+");
        GetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the top goes to the right side.
    private void TurnCubeTopSideToRightSide()
    {
        if (!bSolvingCube)
        {
            ExplainTurnCube(CubeLang.TurnCubeTopSideToRightSide_Text);
        }

        TurnFrontSideTo("+");
        TurnTopMiddleTo("+");
        TurnBackSideTo("-");
        GetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the top goes to the left side.
    private void TurnCubeTopSideToLeftSide()
    {
        if (!bSolvingCube)
        {
            ExplainTurnCube(CubeLang.TurnCubeTopSideToLeftSide_Text);
        }

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

    // Explain the turn of the cube called from OnTurn....Clicked and Turn.... methods.
    private async void ExplainTurnCube(string cTurnCubeText)
    {
        // Convert text to speech.
        ExplainTurnCubeSpeech(cTurnCubeText);

        if (Globals.bExplainText)
        {
            await DisplayAlert("", cTurnCubeText, CubeLang.ButtonClose_Text);
        }
    }

    // Explain the turn of the cube called from the main task SolveTheCubeAsync().
    private async Task ExplainSolveTurnCubeAsync(ImageButton imgbtnArrow, string cTurnCubeText)
    {
        // Enable the arrow button and set the background color to Active.
        imgbtnArrow.BackgroundColor = Color.FromArgb(cColorArrowActive);
        imgbtnArrow.IsEnabled = true;

        // Show the text
        lblExplainTurnCube1.Text = cTurnCubeText;
        lblExplainTurnCube2.Text = cTurnCubeText;

        // Convert text to speech.
        ExplainTurnCubeSpeech(cTurnCubeText);

        // Start a program loop and wait for the arrow button to be pressed.
        while (true)
        {
            // Wait for 100 milliseconds on the button click event handler.
            await Task.Delay(100);

            // Check if the button has been clicked and stop the loop if clicked.
            if (bArrowButtonPressed)
            {
                break;
            }
        }

        // Restore settings.
        bArrowButtonPressed = false;
        imgbtnArrow.IsEnabled = false;
        imgbtnArrow.BackgroundColor = Color.FromArgb(cColorArrowInactive);
    }

    // Explain the turn of the cube with speech.
    private void ExplainTurnCubeSpeech(string cTurnCubeText)
    {
        if (Globals.bExplainSpeech)
        {
            if (cTurnCubeText.Substring(cTurnCubeText.Length - 2, 2) == ").")
            {
                cTurnCubeText = cTurnCubeText.Substring(0, cTurnCubeText.Length - 5);
            }

            ConvertTextToSpeech(cTurnCubeText);
        }
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

    // On clicked event: Reset the colors of the cube or restart the app.
    private void OnBtnResetClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            // Restart the application to get out of the loop in the Task ExplainSolveTurnCubeAsync().
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
        else
        {
            ResetCube();
        }
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
        Globals.cCubeColor1 = aCubeColors[1];
        Globals.cCubeColor2 = aCubeColors[2];
        Globals.cCubeColor3 = aCubeColors[3];
        Globals.cCubeColor4 = aCubeColors[4];
        Globals.cCubeColor5 = aCubeColors[5];
        Globals.cCubeColor6 = aCubeColors[6];

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

    // Set the arrow buttons tooltip.
    private void SetArrowTooltips(bool bSetArrowTooltip)
    {
        if (bSetArrowTooltip)
        {
            ToolTipProperties.SetText(imgbtnTurnFrontSideToRight, CubeLang.TurnFrontSideToRight_Text);
            ToolTipProperties.SetText(imgbtnTurnTopMiddleToRightSide, CubeLang.TurnTopMiddleToRightSide_Text);
            ToolTipProperties.SetText(imgbtnTurnBackSideToLeft, CubeLang.TurnBackSideToLeft_Text);
            ToolTipProperties.SetText(imgbtnTurnLeftSideToRight, CubeLang.TurnLeftSideToRight_Text);
            ToolTipProperties.SetText(imgbtnTurnTopMiddleToFrontSide, CubeLang.TurnTopMiddleToFrontSide_Text);
            ToolTipProperties.SetText(imgbtnTurnRightSideToLeft, CubeLang.TurnRightSideToLeft_Text);
            ToolTipProperties.SetText(imgbtnTurnTopSideToLeft, CubeLang.TurnTopSideToLeft_Text);
            ToolTipProperties.SetText(imgbtnTurnFrontMiddleToRightSide, CubeLang.TurnFrontMiddleToRightSide_Text);
            ToolTipProperties.SetText(imgbtnTurnBottomSideToRight, CubeLang.TurnBottomSideToRight_Text);
            ToolTipProperties.SetText(imgbtnTurnTopSideToRight, CubeLang.TurnTopSideToRight_Text);
            ToolTipProperties.SetText(imgbtnTurnRightMiddleToFrontSide, CubeLang.TurnRightMiddleToFrontSide_Text);
            ToolTipProperties.SetText(imgbtnTurnBottomSideToLeft, CubeLang.TurnBottomSideToLeft_Text);
            ToolTipProperties.SetText(imgbtnTurnLeftSideToLeft, CubeLang.TurnLeftSideToLeft_Text);
            ToolTipProperties.SetText(imgbtnTurnFrontMiddleToTopSide, CubeLang.TurnFrontMiddleToTopSide_Text);
            ToolTipProperties.SetText(imgbtnTurnRightSideToRight, CubeLang.TurnRightSideToRight_Text);
            ToolTipProperties.SetText(imgbtnTurnFrontSideToLeft, CubeLang.TurnFrontSideToLeft_Text);
            ToolTipProperties.SetText(imgbtnTurnRightMiddleToTopSide, CubeLang.TurnRightMiddleToTopSide_Text);
            ToolTipProperties.SetText(imgbtnTurnBackSideToRight, CubeLang.TurnBackSideToRight_Text);
        }
        else
        {
            ToolTipProperties.SetText(imgbtnTurnFrontSideToRight, null);
            ToolTipProperties.SetText(imgbtnTurnTopMiddleToRightSide, null);
            ToolTipProperties.SetText(imgbtnTurnBackSideToLeft, null);
            ToolTipProperties.SetText(imgbtnTurnLeftSideToRight, null);
            ToolTipProperties.SetText(imgbtnTurnTopMiddleToFrontSide, null);
            ToolTipProperties.SetText(imgbtnTurnRightSideToLeft, null);
            ToolTipProperties.SetText(imgbtnTurnTopSideToLeft, null);
            ToolTipProperties.SetText(imgbtnTurnFrontMiddleToRightSide, null);
            ToolTipProperties.SetText(imgbtnTurnBottomSideToRight, null);
            ToolTipProperties.SetText(imgbtnTurnTopSideToRight, null);
            ToolTipProperties.SetText(imgbtnTurnRightMiddleToFrontSide, null);
            ToolTipProperties.SetText(imgbtnTurnBottomSideToLeft, null);
            ToolTipProperties.SetText(imgbtnTurnLeftSideToLeft, null);
            ToolTipProperties.SetText(imgbtnTurnFrontMiddleToTopSide, null);
            ToolTipProperties.SetText(imgbtnTurnRightSideToRight, null);
            ToolTipProperties.SetText(imgbtnTurnFrontSideToLeft, null);
            ToolTipProperties.SetText(imgbtnTurnRightMiddleToTopSide, null);
            ToolTipProperties.SetText(imgbtnTurnBackSideToRight, null);
        }
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
    //private void IsVisibleArrows(bool bVisibleInvisible)
    //{
    //    imgbtnTurnFrontSideToRight.IsVisible = bVisibleInvisible;
    //    imgbtnTurnTopMiddleToRightSide.IsVisible = bVisibleInvisible;
    //    imgbtnTurnBackSideToLeft.IsVisible = bVisibleInvisible;
    //    imgbtnTurnLeftSideToRight.IsVisible = bVisibleInvisible;
    //    imgbtnTurnTopMiddleToFrontSide.IsVisible = bVisibleInvisible;
    //    imgbtnTurnRightSideToLeft.IsVisible = bVisibleInvisible;
    //    imgbtnTurnTopSideToLeft.IsVisible = bVisibleInvisible;
    //    imgbtnTurnFrontMiddleToRightSide.IsVisible = bVisibleInvisible;
    //    imgbtnTurnBottomSideToRight.IsVisible = bVisibleInvisible;
    //    imgbtnTurnTopSideToRight.IsVisible = bVisibleInvisible;
    //    imgbtnTurnRightMiddleToFrontSide.IsVisible = bVisibleInvisible;
    //    imgbtnTurnBottomSideToLeft.IsVisible = bVisibleInvisible;
    //    imgbtnTurnLeftSideToLeft.IsVisible = bVisibleInvisible;
    //    imgbtnTurnFrontMiddleToTopSide.IsVisible = bVisibleInvisible;
    //    imgbtnTurnRightSideToRight.IsVisible = bVisibleInvisible;
    //    imgbtnTurnFrontSideToLeft.IsVisible = bVisibleInvisible;
    //    imgbtnTurnRightMiddleToTopSide.IsVisible = bVisibleInvisible;
    //    imgbtnTurnBackSideToRight.IsVisible = bVisibleInvisible;
    //}

    // Set the arrow background color to Inactive.
    private void SetArrowBackgroundColorToInactive()
    {
        imgbtnTurnFrontSideToRight.BackgroundColor = Color.FromArgb(cColorArrowInactive);
        imgbtnTurnTopMiddleToRightSide.BackgroundColor = Color.FromArgb(cColorArrowInactive);
        imgbtnTurnBackSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowInactive);
        imgbtnTurnLeftSideToRight.BackgroundColor = Color.FromArgb(cColorArrowInactive);
        imgbtnTurnTopMiddleToFrontSide.BackgroundColor = Color.FromArgb(cColorArrowInactive);
        imgbtnTurnRightSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowInactive);
        imgbtnTurnTopSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowInactive);
        imgbtnTurnFrontMiddleToRightSide.BackgroundColor = Color.FromArgb(cColorArrowInactive);
        imgbtnTurnBottomSideToRight.BackgroundColor = Color.FromArgb(cColorArrowInactive);
        imgbtnTurnTopSideToRight.BackgroundColor = Color.FromArgb(cColorArrowInactive);
        imgbtnTurnRightMiddleToFrontSide.BackgroundColor = Color.FromArgb(cColorArrowInactive);
        imgbtnTurnBottomSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowInactive);
        imgbtnTurnLeftSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowInactive);
        imgbtnTurnFrontMiddleToTopSide.BackgroundColor = Color.FromArgb(cColorArrowInactive);
        imgbtnTurnRightSideToRight.BackgroundColor = Color.FromArgb(cColorArrowInactive);
        imgbtnTurnFrontSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowInactive);
        imgbtnTurnRightMiddleToTopSide.BackgroundColor = Color.FromArgb(cColorArrowInactive);
        imgbtnTurnBackSideToRight.BackgroundColor = Color.FromArgb(cColorArrowInactive);
    }

    // Set the arrow background color to Active.
    private void SetArrowBackgroundColorToActive()
    {
        imgbtnTurnFrontSideToRight.BackgroundColor = Color.FromArgb(cColorArrowActive);
        imgbtnTurnTopMiddleToRightSide.BackgroundColor = Color.FromArgb(cColorArrowActive);
        imgbtnTurnBackSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowActive);
        imgbtnTurnLeftSideToRight.BackgroundColor = Color.FromArgb(cColorArrowActive);
        imgbtnTurnTopMiddleToFrontSide.BackgroundColor = Color.FromArgb(cColorArrowActive);
        imgbtnTurnRightSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowActive);
        imgbtnTurnTopSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowActive);
        imgbtnTurnFrontMiddleToRightSide.BackgroundColor = Color.FromArgb(cColorArrowActive);
        imgbtnTurnBottomSideToRight.BackgroundColor = Color.FromArgb(cColorArrowActive);
        imgbtnTurnTopSideToRight.BackgroundColor = Color.FromArgb(cColorArrowActive);
        imgbtnTurnRightMiddleToFrontSide.BackgroundColor = Color.FromArgb(cColorArrowActive);
        imgbtnTurnBottomSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowActive);
        imgbtnTurnLeftSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowActive);
        imgbtnTurnFrontMiddleToTopSide.BackgroundColor = Color.FromArgb(cColorArrowActive);
        imgbtnTurnRightSideToRight.BackgroundColor = Color.FromArgb(cColorArrowActive);
        imgbtnTurnFrontSideToLeft.BackgroundColor = Color.FromArgb(cColorArrowActive);
        imgbtnTurnRightMiddleToTopSide.BackgroundColor = Color.FromArgb(cColorArrowActive);
        imgbtnTurnBackSideToRight.BackgroundColor = Color.FromArgb(cColorArrowActive);
    }
   
    // Show license using the Loaded event of the MainPage.xaml.
    private async void OnPageLoad(object sender, EventArgs e)
    {
        // Show license.
        string cLicense = CubeLang.License_Text + "\n\n" + CubeLang.LicenseMit2_Text;

        if (Globals.bLicense == false)
        {
            Globals.bLicense = await Application.Current.MainPage.DisplayAlert(CubeLang.LicenseTitle_Text, cLicense, CubeLang.Agree_Text, CubeLang.Disagree_Text);

            if (Globals.bLicense)
            {
                Preferences.Default.Set("SettingLicense", true);
            }
            else
            {
#if IOS
                //Thread.CurrentThread.Abort();  // Not allowed in iOS.
                imgbtnAbout.IsEnabled = false;
                imgbtnSettings.IsEnabled = false;
                btnSolve.IsEnabled = false;
                imgbtnSetColors.IsEnabled = false;
                imgbtnOpen.IsEnabled = false;
                imgbtnSave.IsEnabled = false;
                btnReset.IsEnabled = false;
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
        if (Globals.bLanguageChanged)
        {
            SetTextLanguage();
            Globals.bLanguageChanged = false;
        }
    }

    // Put text in the chosen language in the controls.
    private void SetTextLanguage()
    {
        // Set the current UI culture of the selected language.
        Globals.SetCultureSelectedLanguage();

        // Set the text of the controls.
        if (!bSolvingCube)
        {
            SetArrowTooltips(true);
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
            // Text to speech is not supported on this device.
            await DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message + "\n\n" + CubeLang.TextToSpeechError_Text, CubeLang.ButtonClose_Text);
            Globals.bExplainSpeech = false;
            return;
        }

        Globals.bLanguageLocalesExist = true;

        // Put the locales in the array and sort the array.
        Globals.cLanguageLocales = new string[nTotalItems];
        int nItem = 0;

        foreach (var l in locales)
        {
            Globals.cLanguageLocales[nItem] = l.Language + "-" + l.Country + " " + l.Name;
            nItem++;
        }

        Array.Sort(Globals.cLanguageLocales);

        // Search for the language after a first start or reset of the application.
        if (Globals.cLanguageSpeech == "")
        {
            SearchArrayWithSpeechLanguages(cCultureName);
        }
        //await DisplayAlert("Globals.cLanguageSpeech", Globals.cLanguageSpeech, "OK");  // For testing.
    }

    // Search for the language after a first start or reset of the application.
    private void SearchArrayWithSpeechLanguages(string cCultureName)
    {
        try
        {
            int nTotalItems = Globals.cLanguageLocales.Length;

            for (int nItem = 0; nItem < nTotalItems; nItem++)
            {
                if (Globals.cLanguageLocales[nItem].StartsWith(cCultureName))
                {
                    Globals.cLanguageSpeech = Globals.cLanguageLocales[nItem];
                    break;
                }
            }

            // If the language is not found try it with the language (Globals.cLanguage) of the user setting for this app.
            if (Globals.cLanguageSpeech == "")
            {
                for (int nItem = 0; nItem < nTotalItems; nItem++)
                {
                    if (Globals.cLanguageLocales[nItem].StartsWith(Globals.cLanguage))
                    {
                        Globals.cLanguageSpeech = Globals.cLanguageLocales[nItem];
                        break;
                    }
                }
            }

            // If the language is still not found use the first language in the array.
            if (Globals.cLanguageSpeech == "")
            {
                Globals.cLanguageSpeech = Globals.cLanguageLocales[0];
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
        if (Globals.bTextToSpeechIsBusy)
        {
            if (Globals.cts?.IsCancellationRequested ?? true)
                return;

            Globals.cts.Cancel();
        }

        // Start with the text to speech.
        if (cTurnCubeText != null && cTurnCubeText != "")
        {
            Globals.bTextToSpeechIsBusy = true;

            try
            {
                Globals.cts = new CancellationTokenSource();

                SpeechOptions options = new()
                {
                    Locale = locales.Single(l => l.Language + "-" + l.Country + " " + l.Name == Globals.cLanguageSpeech)
                };

                await TextToSpeech.Default.SpeakAsync(cTurnCubeText, options, cancelToken: Globals.cts.Token);
                Globals.bTextToSpeechIsBusy = false;
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

 
                            _________________________
                            |       |       |       |
                            |  Ba9  |  Ba8  |  Ba7  |
                            |_______|_______|_______|
                            |       |       |       |
                            |  Ba6  |  Ba5  |  Ba4  |
                            |_______|_______|_______|
                            |       |       |       |
                            |  Ba3  |  Ba2  |  Ba1  |
    ________________________|_______|_______|_______|________________________________________________
    |       |       |       |       |       |       |       |       |       |       |       |       |
    |  Le7  |  Le4  |  Le1  |  To1  |  To2  |  To3  |  Ri3  |  Ri6  |  Ri9  |  Bo9  |  Bo8  |  Bo7  |
    |_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|
    |       |       |       |       |       |       |       |       |       |       |       |       |
    |  Le8  |  Le5  |  Le2  |  To4  |  To5  |  To6  |  Ri2  |  Ri5  |  Ri8  |  Bo6  |  Bo5  |  Bo4  |
    |_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|
    |       |       |       |       |       |       |       |       |       |       |       |       |
    |  Le9  |  Le6  |  Le3  |  To7  |  To8  |  To9  |  Ri1  |  Ri4  |  Ri7  |  Bo3  |  Bo2  |  Bo1  |
    |_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|
                            |       |       |       |
                            |  Fr1  |  Fr2  |  Fr3  |
                            |_______|_______|_______|
                            |       |       |       |
                            |  Fr4  |  Fr5  |  Fr6  |
                            |_______|_______|_______|
                            |       |       |       |
                            |  Fr7  |  Fr8  |  Fr9  |
                            |_______|_______|_______|
 
                            _________________________
                            |       |       |       |
                            | YO=40 | Y1=41 | Y2=42 |
                            |_______|_______|_______|
                            |       |       |       |
                            |       |       |       |
                            |_______|_______|_______|
                            |       |       |       |
                            |       |       |       |
    ________________________|_______|_______|_______|________________________________________________
    |       |       |       |       |       |       |       |       |       |       |       |       |
    |       |       |       |       |       |       |       |       |       |       |       |       |
    |_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|
    |       |       |       |       |       |       |       |       |       |       |       |       |
    |       |       |       |       |       |       |       |       |       |       |       |       |
    |_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|
    |       |       |       |       |       |       |       |       |       |       |       |       |
    |       |       |       |       |       |       |       |       |       |       |       |       |
    |_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|
                            |       |       |       |
                            |       |       |       |
                            |_______|_______|_______|
                            |       |       |       |
                            |       |       |       |
                            |_______|_______|_______|
                            |       |       |       |
                            |       |       |       |
                            |_______|_______|_______|

 */
