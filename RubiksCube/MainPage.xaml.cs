// Program .....: RubiksCube.sln
// Author ......: Geert Geerits - E-mail: geertgeerits@gmail.com
// Copyright ...: (C) 1981-2024
// Version .....: 2.0.11
// Date ........: 2024-01-19 (YYYY-MM-DD)
// Language ....: Microsoft Visual Studio 2022: .NET MAUI 8 - C# 12.0
// Description .: Solving the Rubik's Cube
// Note ........: This program is based on the program 'SolCube' I wrote in 1981 in MS Basic-80 for a Commodore PET 2001.
//                The solution for solving the cube is based on a book by Don Taylor, Mastering Rubik's Cube, Dutch version 1981.
// Dependencies : 
// Thanks to ...: Gerald Versluis

using Microsoft.Maui.Controls.Shapes;
using System.Diagnostics;

namespace RubiksCube;

public partial class MainPage : ContentPage
{
    // Local variables.
    private IEnumerable<Locale> locales;
    public static bool bColorDrop;
    public static bool bSolvingCube;
    public static bool bArrowButtonPressed;
    public static string[] aFaceColors = new string[7];
    public static string[] aUpFace = new string[10];
    public static string[] aFrontFace = new string[10];
    public static string[] aRightFace = new string[10];
    public static string[] aLeftFace = new string[10];
    public static string[] aBackFace = new string[10];
    public static string[] aDownFace = new string[10];
    public static string[] aPieces = new string[54];
    public static string[] aPiecesTemp = new string[54];
    //public static string cCubeTurn;

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
        aFaceColors[1] = Globals.cCubeColor1;
        aFaceColors[2] = Globals.cCubeColor2;
        aFaceColors[3] = Globals.cCubeColor3;
        aFaceColors[4] = Globals.cCubeColor4;
        aFaceColors[5] = Globals.cCubeColor5;
        aFaceColors[6] = Globals.cCubeColor6;

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
    private void OnButtonSetColorsCubeClicked(object sender, EventArgs e)
    {
        bColorDrop = !bColorDrop;

        if (bColorDrop)
        {
            btnSolveCube.IsEnabled = false;
            grdCubeColorSelect.BackgroundColor = Color.FromArgb("#969696");
            IsVisibleCubeColors(true);
            IsEnabledArrows(false);

            imgbtnTurnUpHorMiddleToRightFace.IsEnabled = true;
            imgbtnTurnUpVerMiddleToFrontFace.IsEnabled = true;
            imgbtnTurnFrontHorMiddleToRightFace.IsEnabled = true;
            imgbtnTurnFrontHorMiddleToLeftFace.IsEnabled = true;
            imgbtnTurnUpVerMiddleToBackFace.IsEnabled = true;
            imgbtnTurnUpHorMiddleToLeftFace.IsEnabled = true;

            ToolTipProperties.SetText(imgbtnTurnUpHorMiddleToRightFace, CubeLang.TurnCubeUpFaceToRightFace_Text);
            ToolTipProperties.SetText(imgbtnTurnUpVerMiddleToFrontFace, CubeLang.TurnCubeFrontFaceToDownFace_Text);
            ToolTipProperties.SetText(imgbtnTurnFrontHorMiddleToRightFace, CubeLang.TurnCubeFrontFaceToRightFace_Text);
            ToolTipProperties.SetText(imgbtnTurnFrontHorMiddleToLeftFace, CubeLang.TurnCubeFrontFaceToLeftFace_Text);
            ToolTipProperties.SetText(imgbtnTurnUpVerMiddleToBackFace, CubeLang.TurnCubeFrontFaceToUpFace_Text);
            ToolTipProperties.SetText(imgbtnTurnUpHorMiddleToLeftFace, CubeLang.TurnCubeUpFaceToLeftFace_Text);
        }
        else
        {
            if (!CheckNumberColorsCube())
            {
                bColorDrop = true;
                return;
            }

            ToolTipProperties.SetText(imgbtnTurnUpHorMiddleToRightFace, CubeLang.TurnUpMiddleToRightFace_Text);
            ToolTipProperties.SetText(imgbtnTurnUpVerMiddleToFrontFace, CubeLang.TurnUpMiddleToFrontFace_Text);
            ToolTipProperties.SetText(imgbtnTurnFrontHorMiddleToRightFace, CubeLang.TurnFrontMiddleToRightFace_Text);
            ToolTipProperties.SetText(imgbtnTurnFrontHorMiddleToLeftFace, CubeLang.TurnRightMiddleToFrontFace_Text);
            ToolTipProperties.SetText(imgbtnTurnUpVerMiddleToBackFace, CubeLang.TurnFrontMiddleToUpFace_Text);
            ToolTipProperties.SetText(imgbtnTurnUpHorMiddleToLeftFace, CubeLang.TurnRightMiddleToUpFace_Text);

            IsEnabledArrows(true);
            IsVisibleCubeColors(false);
            grdCubeColorSelect.BackgroundColor = Color.FromArgb("#00000000");

            btnSolveCube.IsEnabled = true;
        }
    }

    // Solve the cube.
    private async void OnBtnSolveCubeClicked(object sender, EventArgs e)
    {
        // Check the number of colors of the cube.
        if (!CheckNumberColorsCube())
        {
            return;
        }

        // Settings.
        btnSolveCube.IsEnabled = false;
        imgbtnSetColorsCube.IsEnabled = false;
        imgbtnOpenCube.IsEnabled = false;
        imgbtnSaveCube.IsEnabled = false;

        bColorDrop = false;
        bSolvingCube = true;
        SetArrowTooltips(false);
        IsEnabledArrows(false);

        lblCubeOutsideView.IsVisible = false;
        lblExplainTurnCube1.IsVisible = true;
        lblCubeInsideView.IsVisible = false;
        lblExplainTurnCube2.IsVisible = true;

        // Solve the cube.
        //await SolveTheCubeAsyncBASIC();
        await SolveTheCubeAsyncNEW();
        //ClassSolveCube classSolveCube = new();
        //await classSolveCube.SolveTheCubeAsync();

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

        btnSolveCube.IsEnabled = true;
        imgbtnSetColorsCube.IsEnabled = true;
        imgbtnOpenCube.IsEnabled = true;
        imgbtnSaveCube.IsEnabled = true;
    }

    // Solve the cube.  From Basic-80 to C#.
    private async Task SolveTheCubeAsyncBASIC()
    {
        // Declare variables.
        int O, P, Q, R, V, X, Y, Z;
        string cB;
        string cX;
        int nLoopTimes = 0;
    // 500
    // Top layer.
    // Solve the edges of the top layer - Chapter 4, page 14-3.

    // !!!!!!!!!!!!! Does not get out of the loop between line 510 and 695 !!!!!!!!!!!!!

    Line510:
        cB = aUpFace[5];
        V = 0;
        X = 0;
        Y = 0;
        Z = 0;
        nLoopTimes++;
        if (nLoopTimes > 100)
        {
            await DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.CubeCannotBeSolved_Text, CubeLang.ButtonClose_Text);
            return;
        }
        if (cB == aUpFace[8] && aFrontFace[1] == aFrontFace[2])
            V = 1;
    // 520
        if (cB == aUpFace[6] && aRightFace[1] == aRightFace[2])
            X = 1;
    // 530
        if (cB == aUpFace[2] && aBackFace[1] == aBackFace[2])
            Y = 1;
    // 540
        if (cB == aUpFace[4] && aLeftFace[1] == aLeftFace[2])
            Z = 1;
    // 550
        if (V == 1 && X == 1 && Y == 1 && Z == 1)
            goto Line710;
    // 560
        O = 0;
        P = 0;
        Q = 0;

        if (cB == aFrontFace[6] || cB == aRightFace[2] || cB == aRightFace[4] || cB == aRightFace[6])
            O = 1;
    // 570
        if (cB == aBackFace[4] || cB == aRightFace[8] || cB == aDownFace[6])
            P = 1;
    // 580
        cX = X.ToString();
        //if (cB == aUpFace[6] && cX != aRightFace[2])         // 580 IF B = D(41) AND X <> D(10) THEN Q = 1
        //    Q = 1;
        if (cB == aUpFace[6])                                  // 580 IF B = D(41) AND X <> D(10) THEN Q = 1
            Q = 1;
        // 590
        if (O == 1 || P == 1 || Q == 1)
            goto Line610;
    // 600
        await MakeTurnAsync("TurnCubeFrontToLeft");
        goto Line510;

    Line610:
        if (V == 1 && Y == 1 && Z == 1)
            goto Line650;
    // 620
        if (Y == 1 && Z == 1)
        {
            await MakeTurnAsync("TurnUp-");
            goto Line650;
        }
    // 630
        if (Y == 1)
        {
            await MakeTurnAsync("TurnUp++");
            goto Line650;
        }
    // 640
        await MakeTurnAsync("TurnUp+");
    // 650
    Line650:
        cX = aRightFace[1];
        if (cB == aRightFace[2] && cX == aUpFace[6])
        {
            await MakeTurnAsync("TurnRight-");
            await MakeTurnAsync("TurnUp-");
            await MakeTurnAsync("TurnDown+");
            await MakeTurnAsync("TurnBack+");
            await MakeTurnAsync("TurnUp-");
            await MakeTurnAsync("TurnLeft+");
            await MakeTurnAsync("TurnUp-");
            await MakeTurnAsync("TurnDown+");
            await MakeTurnAsync("TurnFront-");
            goto Line510;
        }
        // 655
        if (cB == aFrontFace[6] && cX == aRightFace[4])
        {
            await MakeTurnAsync("TurnUp-");
            await MakeTurnAsync("TurnBack-");
            await MakeTurnAsync("TurnUp+");
            await MakeTurnAsync("TurnDown-");
            await MakeTurnAsync("TurnRight+");
            goto Line510;
        }
        // 660
        if (cB == aRightFace[4] && cX == aFrontFace[6])
        {
            await MakeTurnAsync("TurnUp-");
            await MakeTurnAsync("TurnUp-");
            await MakeTurnAsync("TurnLeft+");
            await MakeTurnAsync("TurnUp-");
            await MakeTurnAsync("TurnDown+");
            await MakeTurnAsync("TurnFront-");
            goto Line510;
        }
        // 665
        if (cB == aDownFace[6] && cX == aRightFace[8])
        {
            await MakeTurnAsync("TurnRight-");
            await MakeTurnAsync("TurnUp-");
            await MakeTurnAsync("TurnUp-");
            await MakeTurnAsync("TurnDown+");
            await MakeTurnAsync("TurnDown+");
            await MakeTurnAsync("TurnLeft+");
            await MakeTurnAsync("TurnUp-");
            await MakeTurnAsync("TurnFront+");
            await MakeTurnAsync("TurnUp-");
            await MakeTurnAsync("TurnDown+");
            await MakeTurnAsync("TurnRight-");
            goto Line510;
        }
        // 670
        if (cB == aRightFace[8] && cX == aDownFace[6])
        {
            await MakeTurnAsync("TurnRight-");
            await MakeTurnAsync("TurnUp-");
            await MakeTurnAsync("TurnDown+");
            await MakeTurnAsync("TurnBack+");
            goto Line510;
        }
        // 675
        if (cB == aBackFace[4] && cX == aRightFace[6])
        {
            await MakeTurnAsync("TurnUp+");
            await MakeTurnAsync("TurnFront+");
            await MakeTurnAsync("TurnUp-");
            await MakeTurnAsync("TurnDown-");
            await MakeTurnAsync("TurnRight-");
            goto Line510;
        }
        // 680
        if (cB == aRightFace[6] && cX == aBackFace[4])
        {
            await MakeTurnAsync("TurnUp-");
            await MakeTurnAsync("TurnUp-");
            await MakeTurnAsync("TurnLeft-");
            await MakeTurnAsync("TurnUp+");
            await MakeTurnAsync("TurnDown-");
            await MakeTurnAsync("TurnBack+");
            goto Line510;
        }
        // 685
        if (cB == aUpFace[6] && cX != aRightFace[2])
        {
            await MakeTurnAsync("TurnRight-");
            await MakeTurnAsync("TurnUp-");
            await MakeTurnAsync("TurnDown+");
            await MakeTurnAsync("TurnBack+");
            goto Line510;
        }
        // 690
        if (cB == aRightFace[2] && cX != aUpFace[6])
        {
            await MakeTurnAsync("TurnRight-");
            await MakeTurnAsync("TurnUp-");
            await MakeTurnAsync("TurnDown+");
            await MakeTurnAsync("TurnBack+");
        }
        // 695
        goto Line510;
    
        // Solve the corners of the top layer - Chapter 6, page 16.
    Line710:
        cB = aUpFace[5];
        O = 0;
        P = 0;
        Q = 0;
        R = 0;
        // 715
        if (cB == aUpFace[1] && cB == aUpFace[3] && cB == aUpFace[7] && cB == aUpFace[9])
            O = 1;
        // 720
        if (aFrontFace[1] == aFrontFace[3])
            P = 1;
        // 725
        if (aRightFace[1] == aRightFace[3])
            Q = 1;
        // 730
        if (aBackFace[1] == aBackFace[3])
            R = 1;
        // 735
        if (O == 1 && P == 1 && Q == 1 && R == 1)
            goto Line1010;
        // 740
        O = 0;
        if (cB == aUpFace[3] && cB == aUpFace[7] && cB == aUpFace[9])
            O = 1;
        // 745
        if (O == 1 && P == 1 && Q == 1)
        {
            await MakeTurnAsync("TurnUp++");
            goto Line800;
        }
        // 750
        O = 0;
        if (cB == aUpFace[3] && cB == aUpFace[9])
            O = 1;
        // 755
        if (O == 1 && Q == 1)
        {
            await MakeTurnAsync("TurnUp++");
            goto Line800;
        }
        // 760
        O = 0;
        if (cB == aUpFace[7] && cB == aUpFace[9])
            O = 1;
        // 765
        if (O == 1 && P == 1)
        {
            await MakeTurnAsync("TurnUp+");
            goto Line800;
        }
        // 770
        O = 0;
        if (cB == aUpFace[1] && cB == aUpFace[3])
            O = 1;
        // 775
        if (O == 1 && R == 1)
        {
            await MakeTurnAsync("TurnUp-");
            goto Line800;
        }
        // 780
        if (cB != aUpFace[9])
            goto Line800;
        // 785
        if (cB != aUpFace[3])
        {
            await MakeTurnAsync("TurnUp+");
            goto Line800;
        }
        // 790
        if (cB != aUpFace[7])
        {
            await MakeTurnAsync("TurnUp-");
            goto Line800;
        }
        // 795
        if (cB != aUpFace[1])
        {
            await MakeTurnAsync("TurnUp++");
        }
    // 800
    Line800:
        if (cB == aFrontFace[9] || cB == aRightFace[7] || cB == aDownFace[3])
            goto Line880;
        // 805
        if (cB == aRightFace[9] || cB == aBackFace[7] || cB == aDownFace[9])
        {
            await MakeTurnAsync("TurnDown-");
            goto Line880;
        }
        // 810
        if (cB == aFrontFace[7] || cB == aLeftFace[9] || cB == aDownFace[1])
        {
            await MakeTurnAsync("TurnDown+");
            goto Line880;
        }
        // 815
        if (cB == aBackFace[9] || cB == aLeftFace[7] || cB == aDownFace[7])
        {
            await MakeTurnAsync("TurnDown++");
            goto Line880;
        }
        // 870
        await MakeTurnAsync("TurnRight++");
        goto Line710;
    // 880
    Line880:
        if (cB == aFrontFace[9])
        {
            await MakeTurnAsync("TurnFront+");
            await MakeTurnAsync("TurnDown+");
            await MakeTurnAsync("TurnFront-");
            goto Line710;
        }
    // 885
        if (cB == aRightFace[7])
        {
            await MakeTurnAsync("TurnRight-");
            await MakeTurnAsync("TurnDown-");
            await MakeTurnAsync("TurnRight+");
            goto Line710;
        }
    // 890
        if (cB == aDownFace[3])
        {
            await MakeTurnAsync("TurnRight-");
            await MakeTurnAsync("TurnDown+");
            await MakeTurnAsync("TurnRight+");
            await MakeTurnAsync("TurnDown+");
            await MakeTurnAsync("TurnDown+");
            await MakeTurnAsync("TurnRight-");
            await MakeTurnAsync("TurnDown-");
            await MakeTurnAsync("TurnRight+");
        }
    // 895
        goto Line710;

    // 1000
        // Solve the middle layer - Chapter 10, page 21.
        Line1010:
        await DisplayAlert("Line", "1010", "OK");







        if (!CheckIfCubeIsSolved(false))
        {
            return;
        }
    }

    // Solve the cube.
    private async Task SolveTheCubeAsyncNEW()
    {
        // Test the cube turns.
        //await TestCubeTurnsAsync();
        //return;

        // Solve the edges of the top layer - Chapter 4, page 14-3.
        await SolveEdgesTopLayerAsync();

        // Solve the edges of the top layer - Chapter 4, page 14-2.
        if (aUpFace[5] == aFrontFace[4])
        {
            await MakeTurnAsync("TurnLeft+");

            if (aLeftFace[8] == aFrontFace[5])
            {
                await MakeTurnAsync("TurnDown+");
            }

            if (aLeftFace[8] == aBackFace[5])
            {
                await MakeTurnAsync("TurnDown-");
            }

            if (aLeftFace[8] == aRightFace[5])
            {
                await MakeTurnAsync("TurnDown++");
            }
        }

        if (aUpFace[5] == aFrontFace[6])
        {
            await MakeTurnAsync("TurnRight-");

            if (aRightFace[8] == aFrontFace[5])
            {
                await MakeTurnAsync("TurnDown-");
            }

            if (aRightFace[8] == aBackFace[5])
            {
                await MakeTurnAsync("TurnDown+");
            }

            if (aRightFace[8] == aLeftFace[5])
            {
                await MakeTurnAsync("TurnDown++");
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




        if (!CheckIfCubeIsSolved(false))
        {
            return;
        }
    }

    // Solve the edges of the top layer - Chapter 4, page 14-3.
    private async Task SolveEdgesTopLayerAsync()
    {
        for (int nTimes = 1; nTimes < 11; nTimes++)
        {
            if (aUpFace[5] == aDownFace[2] && aFrontFace[5] == aFrontFace[8])
            {
                await MakeTurnAsync("TurnFront++");
            }

            if (aUpFace[5] == aDownFace[4] && aLeftFace[5] == aLeftFace[8])
            {
                await MakeTurnAsync("TurnLeft++");
            }

            if (aUpFace[5] == aDownFace[6] && aRightFace[5] == aRightFace[8])
            {
                await MakeTurnAsync("TurnRight++");
            }

            if (aUpFace[5] == aDownFace[8] && aBackFace[5] == aBackFace[8])
            {
                await MakeTurnAsync("TurnBack++");
                SetCubeColorsFromArrays();
            }
        }
    }

    // Check the number of colors of the cube.
    private bool CheckNumberColorsCube()
    {
        SetCubeColorsInArrays();

        string cMessage = ClassCheckColorsCube.CheckNumberColors();
        if (cMessage == "")
        {
            return true;
        }
        else
        {
            DisplayAlert(CubeLang.ErrorTitle_Text, cMessage, CubeLang.ButtonClose_Text);
            return false;
        }
    }

    // Check if the cube is solved.
    public bool CheckIfCubeIsSolved(bool bShowMessage)
    {
        bool bResult = ClassCheckColorsCube.CheckIfSolved();

        if (bResult)
        {
            if (Globals.bExplainSpeech)
            {
                ConvertTextToSpeech(CubeLang.MessageCubeIsSolved_Text);
            }

            DisplayAlert("Rubik's Cube", CubeLang.MessageCubeIsSolved_Text, CubeLang.ButtonClose_Text);
            return true;
        }

        if (!bResult)
        {
            if (bShowMessage)
            {
                DisplayAlert("Rubik's Cube", CubeLang.MessageCubeNotSolved_Text, CubeLang.ButtonClose_Text);
            }
        }

        return false;
    }

    // Turn the faces of the cube.
    // Turn the front face clockwise (to right +).
    private void OnTurnFrontFaceToRightClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnFrontFaceToRight_Text);
        ClassCubeTurns.TurnFrontFaceTo("+");
        SetCubeColorsFromArrays();
    }

    // Turn the upper horizontal middle to the right face (+).
    private void OnTurnUpHorMiddleToRightFaceClicked(object sender, EventArgs e)
    {
        if (bColorDrop)
        {
            TurnCubeUpFaceToRightFace();
            return;
        }

        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnUpMiddleToRightFace_Text);
        ClassCubeTurns.TurnUpHorMiddleTo("+");
        SetCubeColorsFromArrays();
    }

    // Turn the back face counter clockwise (to left -).
    private void OnTurnBackFaceToLeftClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnBackFaceToLeft_Text);
        ClassCubeTurns.TurnBackFaceTo("-");
        SetCubeColorsFromArrays();
    }

    // Turn the left face clockwise (to right +).
    private void OnTurnLeftFaceToRightClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnLeftFaceToRight_Text);
        ClassCubeTurns.TurnLeftFaceTo("+");
        SetCubeColorsFromArrays();
    }

    // Turn the upper vertical middle to the front face (-).
    private void OnTurnUpVerMiddleToFrontFaceClicked(object sender, EventArgs e)
    {
        if (bColorDrop)
        {
            TurnCubeFrontFaceToDownFace();
            return;
        }

        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnUpMiddleToFrontFace_Text);
        ClassCubeTurns.TurnUpVerMiddleTo("-");
        SetCubeColorsFromArrays();
    }

    // Turn the right face counter clockwise (to left -).
    private void OnTurnRightFaceToLeftClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnRightFaceToLeft_Text);
        ClassCubeTurns.TurnRightFaceTo("-");
        SetCubeColorsFromArrays();
    }

    // Turn the upper face counter clockwise (to left -).
    private void OnTurnUpFaceToLeftClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnUpFaceToLeft_Text);
        ClassCubeTurns.TurnUpFaceTo("-");
        SetCubeColorsFromArrays();
    }

    // Turn the front horizontal middle to the right face (-).
    private void OnTurnFrontHorMiddleToRightFaceClicked(object sender, EventArgs e)
    {
        if (bColorDrop)
        {
            TurnCubeFrontFaceToRightFace();
            return;
        }

        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnFrontMiddleToRightFace_Text);
        ClassCubeTurns.TurnFrontHorMiddleTo("-");
        SetCubeColorsFromArrays();
    }

    // Turn the down face clockwise (to right +).
    private void OnTurnDownFaceToRightClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnDownFaceToRight_Text);
        ClassCubeTurns.TurnDownFaceTo("+");
        SetCubeColorsFromArrays();
    }

    // Turn the upper face clockwise (to right +).
    private void OnTurnUpFaceToRightClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnUpFaceToRight_Text);
        ClassCubeTurns.TurnUpFaceTo("+");
        SetCubeColorsFromArrays();
    }

    // Turn the front horizontal middle to the left face (+).
    private void OnTurnFrontHorMiddleToLeftFaceClicked(object sender, EventArgs e)
    {
        if (bColorDrop)
        {
            TurnCubeFrontFaceToLeftFace();
            return;
        }

        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnRightMiddleToFrontFace_Text);
        ClassCubeTurns.TurnFrontHorMiddleTo("+");
        SetCubeColorsFromArrays();
    }

    // Turn the down face counter clockwise (to left -).
    private void OnTurnDownFaceToLeftClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnDownFaceToLeft_Text);
        ClassCubeTurns.TurnDownFaceTo("-");
        SetCubeColorsFromArrays();
    }

    // Turn the left face counter clockwise (to left -).
    private void OnTurnLeftFaceToLeftClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnLeftFaceToLeft_Text);
        ClassCubeTurns.TurnLeftFaceTo("-");
        SetCubeColorsFromArrays();
    }

    // Turn the upper vertical middle to the back face (+).
    private void OnTurnUpVerMiddleToBackFaceClicked(object sender, EventArgs e)
    {
        if (bColorDrop)
        {
            TurnCubeFrontFaceToUpFace();
            return;
        }

        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnFrontMiddleToUpFace_Text);
        ClassCubeTurns.TurnUpVerMiddleTo("+");
        SetCubeColorsFromArrays();
    }

    // Turn the right face clockwise (to right +).
    private void OnTurnRightFaceToRightClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }
        
        ExplainTurnCube(CubeLang.TurnRightFaceToRight_Text);
        ClassCubeTurns.TurnRightFaceTo("+");
        SetCubeColorsFromArrays();
    }

    // Turn the front face counter clockwise (to left -).
    private void OnTurnFrontFaceToLeftClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnFrontFaceToLeft_Text);
        ClassCubeTurns.TurnFrontFaceTo("-");
        SetCubeColorsFromArrays();
    }

    // Turn the upper horizontal middle to the left face (-).
    private void OnTurnUpHorMiddleToLeftFaceClicked(object sender, EventArgs e)
    {
        if (bColorDrop)
        {
            TurnCubeUpFaceToLeftFace();
            return;
        }

        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnRightMiddleToUpFace_Text);
        ClassCubeTurns.TurnUpHorMiddleTo("-");
        SetCubeColorsFromArrays();
    }

    // Turn the back face clockwise (to right +).
    private void OnTurnBackFaceToRightClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        ExplainTurnCube(CubeLang.TurnBackFaceToRight_Text);
        ClassCubeTurns.TurnBackFaceTo("+");
        SetCubeColorsFromArrays();
    }

    // Turn the entire cube a quarter turn.
    // Rotate the entire cube so that the front goes to the left face.
    private void TurnCubeFrontFaceToLeftFace()
    {
        if (!bSolvingCube)
        {
            ExplainTurnCube(CubeLang.TurnCubeFrontFaceToLeftFace_Text);
        }
            
        ClassCubeTurns.TurnUpFaceTo("+");
        ClassCubeTurns.TurnFrontHorMiddleTo("+");
        ClassCubeTurns.TurnDownFaceTo("-");
        SetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the front goes to the right face.
    private void TurnCubeFrontFaceToRightFace()
    {
        if (!bSolvingCube)
        {
            ExplainTurnCube(CubeLang.TurnCubeFrontFaceToRightFace_Text);
        }

        ClassCubeTurns.TurnUpFaceTo("-");
        ClassCubeTurns.TurnFrontHorMiddleTo("-");
        ClassCubeTurns.TurnDownFaceTo("+");
        SetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the front goes to the upper face.
    private void TurnCubeFrontFaceToUpFace()
    {
        if (!bSolvingCube)
        {
            ExplainTurnCube(CubeLang.TurnCubeFrontFaceToUpFace_Text);
        }

        ClassCubeTurns.TurnRightFaceTo("+");
        ClassCubeTurns.TurnUpVerMiddleTo("+");
        ClassCubeTurns.TurnLeftFaceTo("-");
        SetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the front goes to the down face.
    private void TurnCubeFrontFaceToDownFace()
    {
        if (!bSolvingCube)
        {
            ExplainTurnCube(CubeLang.TurnCubeFrontFaceToDownFace_Text);
        }

        ClassCubeTurns.TurnRightFaceTo("-");
        ClassCubeTurns.TurnUpVerMiddleTo("-");
        ClassCubeTurns.TurnLeftFaceTo("+");
        SetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the upper face goes to the right face.
    private void TurnCubeUpFaceToRightFace()
    {
        if (!bSolvingCube)
        {
            ExplainTurnCube(CubeLang.TurnCubeUpFaceToRightFace_Text);
        }

        ClassCubeTurns.TurnFrontFaceTo("+");
        ClassCubeTurns.TurnUpHorMiddleTo("+");
        ClassCubeTurns.TurnBackFaceTo("-");
        SetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the upper face goes to the left face.
    private void TurnCubeUpFaceToLeftFace()
    {
        if (!bSolvingCube)
        {
            ExplainTurnCube(CubeLang.TurnCubeUpFaceToLeftFace_Text);
        }

        ClassCubeTurns.TurnFrontFaceTo("-");
        ClassCubeTurns.TurnUpHorMiddleTo("-");
        ClassCubeTurns.TurnBackFaceTo("+");
        SetCubeColorsFromArrays();
    }

    // Copy the cube piece colors to the temporary cube piece colors array.
    //private void CopyCubePieceColorsToTemporaryArray()
    //{
    //    for (int i = 0; i < 54; i++)
    //    {
    //        aPiecesTemp[i] = aPieces[i];
    //    }
    //}

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

    public void MakeTurnAsync3(string cTurnFaceAndDirection)
    {
        //string cTurnFaceAndDirection = cCubeTurn;
        //DisplayAlert("", cTurnFaceAndDirection, "OK");
        Debug.WriteLine(cTurnFaceAndDirection);
        Console.WriteLine(cTurnFaceAndDirection);
        MainPage mainPage = new();
        mainPage.lblExplainTurnCube2.Text = cTurnFaceAndDirection;
        Task.Delay(3000).Wait();
        return;
    }

    // Make and explain the turn of the cube called from the main task SolveTheCubeAsync().
    public async Task MakeTurnAsync(string cTurnFaceAndDirection)
    {
        //Debug.WriteLine(cTurnFaceAndDirection);
        //Console.WriteLine(cTurnFaceAndDirection);
        //MainPage mainPage = new();
        //mainPage.lblExplainTurnCube2.Text = cTurnFaceAndDirection;
        //return;

        // Enable the arrow button and set the background color to Active.
        await SetImageButtonArrowIsEnabledAsync(cTurnFaceAndDirection, true);

        // Show the text.
        string cTurnCubeText = await SetExplainTextAsync(cTurnFaceAndDirection);
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
        await SetImageButtonArrowIsEnabledAsync(cTurnFaceAndDirection, false);

        // Turn the faces of the cube.
        await TurnFaceCubeAsync(cTurnFaceAndDirection);
    }

    // Enalbe or disable the arrow imagebuttons.
    private async Task SetImageButtonArrowIsEnabledAsync(string cTurnFaceAndDirection, bool bIsEnabled)
    {
        switch (cTurnFaceAndDirection)
        {
            case "TurnFront+":
            case "TurnFront++":
                imgbtnTurnFrontFaceToRight.IsEnabled = bIsEnabled;
                break;
            case "TurnFront-":
            case "TurnFront--":
                imgbtnTurnFrontFaceToLeft.IsEnabled = bIsEnabled;
                break;
            case "TurnUp+":
            case "TurnUp++":
                imgbtnTurnUpFaceToRight.IsEnabled = bIsEnabled;
                break;
            case "TurnUp-":
            case "TurnUp--":
                imgbtnTurnUpFaceToLeft.IsEnabled = bIsEnabled;
                break;
            case "TurnDown+":
            case "TurnDown++":
                imgbtnTurnDownFaceToRight.IsEnabled = bIsEnabled;
                break;
            case "TurnDown-":
            case "TurnDown--":
                imgbtnTurnDownFaceToLeft.IsEnabled = bIsEnabled;
                break;
            case "TurnLeft+":
            case "TurnLeft++":
                imgbtnTurnLeftFaceToRight.IsEnabled = bIsEnabled;
                break;
            case "TurnLeft-":
            case "TurnLeft--":
                imgbtnTurnLeftFaceToLeft.IsEnabled = bIsEnabled;
                break;
            case "TurnRight+":
            case "TurnRight++":
                imgbtnTurnRightFaceToRight.IsEnabled = bIsEnabled;
                break;
            case "TurnRight-":
            case "TurnRight--":
                imgbtnTurnRightFaceToLeft.IsEnabled = bIsEnabled;
                break;
            case "TurnBack+":
            case "TurnBack++":
                imgbtnTurnBackFaceToRight.IsEnabled = bIsEnabled;
                break;
            case "TurnBack-":
            case "TurnBack--":
                imgbtnTurnBackFaceToLeft.IsEnabled = bIsEnabled;
                break;

            case "TurnUpHorMiddleRight+":
            case "TurnUpHorMiddleRight++":
                imgbtnTurnUpHorMiddleToRightFace.IsEnabled = bIsEnabled;
                break;
            case "TurnUpHorMiddleLeft-":
            case "TurnUpHorMiddleLeft--":
                imgbtnTurnUpHorMiddleToLeftFace.IsEnabled = bIsEnabled;
                break;
            case "TurnUpVerMiddleBack+":
            case "TurnUpVerMiddleBack++":
                imgbtnTurnUpVerMiddleToBackFace.IsEnabled = bIsEnabled;
                break;
            case "TurnUpVerMiddleFront-":
            case "TurnUpVerMiddleFront--":
                imgbtnTurnUpVerMiddleToFrontFace.IsEnabled = bIsEnabled;
                break;
            case "TurnFrontHorMiddleLeft+":
            case "TurnFrontHorMiddleLeft++":
                imgbtnTurnFrontHorMiddleToLeftFace.IsEnabled = bIsEnabled;
                break;
            case "TurnFrontHorMiddleRight-":
            case "TurnFrontHorMiddleRight--":
                imgbtnTurnFrontHorMiddleToRightFace.IsEnabled = bIsEnabled;
                break;

            case "TurnCubeFrontToRight":
                imgbtnTurnFrontHorMiddleToRightFace.IsEnabled = bIsEnabled;
                break;
            case "TurnCubeFrontToLeft":
                imgbtnTurnFrontHorMiddleToLeftFace.IsEnabled = bIsEnabled;
                break;
            case "TurnCubeFrontToUp":
                imgbtnTurnUpVerMiddleToBackFace.IsEnabled = bIsEnabled;
                break;
            case "TurnCubeFrontToDown":
                imgbtnTurnUpVerMiddleToFrontFace.IsEnabled = bIsEnabled;
                break;
            case "TurnCubeUpToRight":
                imgbtnTurnUpHorMiddleToRightFace.IsEnabled = bIsEnabled;
                break;
            case "TurnCubeUpToLeft":
                imgbtnTurnUpHorMiddleToLeftFace.IsEnabled = bIsEnabled;
                break;

            default:
                await DisplayAlert(CubeLang.ErrorTitle_Text, "Turn not found", CubeLang.ButtonClose_Text);
                break;
        }
    }

    // Set the explain text depending on the direction of rotation of the cube face.
    private async Task<string> SetExplainTextAsync(string cTurnFaceAndDirection)
    {
        string cTurnCubeText = "";

        switch (cTurnFaceAndDirection)
        {
            case "TurnFront+":
                cTurnCubeText = CubeLang.TurnFrontFaceToRight_Text;
                break;
            case "TurnFront-":
                cTurnCubeText = CubeLang.TurnFrontFaceToLeft_Text;
                break;
            case "TurnUp+":
                cTurnCubeText = CubeLang.TurnUpFaceToRight_Text;
                break;
            case "TurnUp-":
                cTurnCubeText = CubeLang.TurnUpFaceToLeft_Text;
                break;
            case "TurnDown+":
                cTurnCubeText = CubeLang.TurnDownFaceToRight_Text;
                break;
            case "TurnDown-":
                cTurnCubeText = CubeLang.TurnDownFaceToLeft_Text;
                break;
            case "TurnLeft+":
                cTurnCubeText = CubeLang.TurnLeftFaceToRight_Text;
                break;
            case "TurnLeft-":
                cTurnCubeText = CubeLang.TurnLeftFaceToLeft_Text;
                break;
            case "TurnRight+":
                cTurnCubeText = CubeLang.TurnRightFaceToRight_Text;
                break;
            case "TurnRight-":
                cTurnCubeText = CubeLang.TurnRightFaceToLeft_Text;
                break;
            case "TurnBack+":
                cTurnCubeText = CubeLang.TurnBackFaceToRight_Text;
                break;
            case "TurnBack-":
                cTurnCubeText = CubeLang.TurnBackFaceToLeft_Text;
                break;

            case "TurnFront++":
            case "TurnFront--":
                cTurnCubeText = CubeLang.TurnFrontFaceHalfTurn_Text;
                break;
            case "TurnUp++":
            case "TurnUp--":
                cTurnCubeText = CubeLang.TurnUpFaceHalfTurn_Text;
                break;
            case "TurnDown++":
            case "TurnDown--":
                cTurnCubeText = CubeLang.TurnDownFaceHalfTurn_Text;
                break;
            case "TurnLeft++":
            case "TurnLeft--":
                cTurnCubeText = CubeLang.TurnLeftFaceHalfTurn_Text;
                break;
            case "TurnRight++":
            case "TurnRight--":
                cTurnCubeText = CubeLang.TurnRightFaceHalfTurn_Text;
                break;
            case "TurnBack++":
            case "TurnBack--":
                cTurnCubeText = CubeLang.TurnBackFaceHalfTurn_Text;
                break;

            case "TurnUpHorMiddleRight+":
                cTurnCubeText = CubeLang.TurnUpMiddleToRightFace_Text ;
                break;
            case "TurnUpHorMiddleLeft-":
                cTurnCubeText = CubeLang.TurnRightMiddleToUpFace_Text;
                break;
            case "TurnUpVerMiddleBack+":
                cTurnCubeText = CubeLang.TurnFrontMiddleToUpFace_Text;
                break;
            case "TurnUpVerMiddleFront-":
                cTurnCubeText = CubeLang.TurnUpMiddleToFrontFace_Text;
                break;
            case "TurnFrontHorMiddleLeft+":
                cTurnCubeText = CubeLang.TurnRightMiddleToFrontFace_Text;
                break;
            case "TurnFrontHorMiddleRight-":
                cTurnCubeText = CubeLang.TurnFrontMiddleToRightFace_Text;
                break;

            case "TurnUpHorMiddleRight++":
            case "TurnUpHorMiddleLeft--":
            case "TurnUpVerMiddleBack++":
            case "TurnUpVerMiddleFront--":
            case "TurnFrontHorMiddleLeft++":
            case "TurnFrontHorMiddleRight--":
                cTurnCubeText = CubeLang.TurnMiddleLayerHalfTurn_Text;
                break;

            case "TurnCubeFrontToRight":
                cTurnCubeText = CubeLang.TurnCubeFrontFaceToRightFace_Text;
                break;
            case "TurnCubeFrontToLeft":
                cTurnCubeText = CubeLang.TurnCubeFrontFaceToLeftFace_Text;
                break;
            case "TurnCubeFrontToUp":
                cTurnCubeText = CubeLang.TurnCubeFrontFaceToUpFace_Text;
                break;
            case "TurnCubeFrontToDown":
                cTurnCubeText = CubeLang.TurnCubeFrontFaceToDownFace_Text;
                break;
            case "TurnCubeUpToRight":
                cTurnCubeText = CubeLang.TurnCubeUpFaceToRightFace_Text;
                break;
            case "TurnCubeUpToLeft":
                cTurnCubeText = CubeLang.TurnCubeUpFaceToLeftFace_Text;
                break;
            
            default:
                await DisplayAlert(CubeLang.ErrorTitle_Text, "Turn not found", CubeLang.ButtonClose_Text);
                break;
        }

        return cTurnCubeText;
    }

    // Turn the faces of the cube
    private async Task TurnFaceCubeAsync(string cTurnFaceAndDirection)
    {
        switch (cTurnFaceAndDirection)
        {
            case "TurnFront+":
                ClassCubeTurns.TurnFrontFaceTo("+");
                break;
            case "TurnFront-":
                ClassCubeTurns.TurnFrontFaceTo("-");
                break;
            case "TurnUp+":
                ClassCubeTurns.TurnUpFaceTo("+");
                break;
            case "TurnUp-":
                ClassCubeTurns.TurnUpFaceTo("-");
                break;
            case "TurnDown+":
                ClassCubeTurns.TurnDownFaceTo("+");
                break;
            case "TurnDown-":
                ClassCubeTurns.TurnDownFaceTo("-");
                break;
            case "TurnLeft+":
                ClassCubeTurns.TurnLeftFaceTo("+");
                break;
            case "TurnLeft-":
                ClassCubeTurns.TurnLeftFaceTo("-");
                break;
            case "TurnRight+":
                ClassCubeTurns.TurnRightFaceTo("+");
                break;
            case "TurnRight-":
                ClassCubeTurns.TurnRightFaceTo("-");
                break;
            case "TurnBack+":
                ClassCubeTurns.TurnBackFaceTo("+");
                break;
            case "TurnBack-":
                ClassCubeTurns.TurnBackFaceTo("-");
                break;

            case "TurnFront++":
                ClassCubeTurns.TurnFrontFaceTo("+");
                ClassCubeTurns.TurnFrontFaceTo("+");
                break;
            case "TurnFront--":
                ClassCubeTurns.TurnFrontFaceTo("-");
                ClassCubeTurns.TurnFrontFaceTo("-");
                break;
            case "TurnUp++":
                ClassCubeTurns.TurnUpFaceTo("+");
                ClassCubeTurns.TurnUpFaceTo("+");
                break;
            case "TurnUp--":
                ClassCubeTurns.TurnUpFaceTo("-");
                ClassCubeTurns.TurnUpFaceTo("-");
                break;
            case "TurnDown++":
                ClassCubeTurns.TurnDownFaceTo("+");
                ClassCubeTurns.TurnDownFaceTo("+");
                break;
            case "TurnDown--":
                ClassCubeTurns.TurnDownFaceTo("-");
                ClassCubeTurns.TurnDownFaceTo("-");
                break;
            case "TurnLeft++":
                ClassCubeTurns.TurnLeftFaceTo("+");
                ClassCubeTurns.TurnLeftFaceTo("+");
                break;
            case "TurnLeft--":
                ClassCubeTurns.TurnLeftFaceTo("-");
                ClassCubeTurns.TurnLeftFaceTo("-");
                break;
            case "TurnRight++":
                ClassCubeTurns.TurnRightFaceTo("+");
                ClassCubeTurns.TurnRightFaceTo("+");
                break;
            case "TurnRight--":
                ClassCubeTurns.TurnRightFaceTo("-");
                ClassCubeTurns.TurnRightFaceTo("-");
                break;
            case "TurnBack++":
                ClassCubeTurns.TurnBackFaceTo("+");
                ClassCubeTurns.TurnBackFaceTo("+");
                break;
            case "TurnBack--":
                ClassCubeTurns.TurnBackFaceTo("-");
                ClassCubeTurns.TurnBackFaceTo("-");
                break;

            case "TurnUpHorMiddleRight+":
                ClassCubeTurns.TurnUpHorMiddleTo("+");
                break;
            case "TurnUpHorMiddleLeft-":
                ClassCubeTurns.TurnUpHorMiddleTo("-");
                break;
            case "TurnUpVerMiddleBack+":
                ClassCubeTurns.TurnUpVerMiddleTo("+");
                break;
            case "TurnUpVerMiddleFront-":
                ClassCubeTurns.TurnUpVerMiddleTo("-");
                break;
            case "TurnFrontHorMiddleLeft+":
                ClassCubeTurns.TurnFrontHorMiddleTo("+");
                break;
            case "TurnFrontHorMiddleRight-":
                ClassCubeTurns.TurnFrontHorMiddleTo("-");
                break;

            case "TurnUpHorMiddleRight++":
                ClassCubeTurns.TurnUpHorMiddleTo("+");
                ClassCubeTurns.TurnUpHorMiddleTo("+");
                break;
            case "TurnUpHorMiddleLeft--":
                ClassCubeTurns.TurnUpHorMiddleTo("-");
                ClassCubeTurns.TurnUpHorMiddleTo("-");
                break;
            case "TurnUpVerMiddleBack++":
                ClassCubeTurns.TurnUpVerMiddleTo("+");
                ClassCubeTurns.TurnUpVerMiddleTo("+");
                break;
            case "TurnUpVerMiddleFront--":
                ClassCubeTurns.TurnUpVerMiddleTo("-");
                ClassCubeTurns.TurnUpVerMiddleTo("-");
                break;
            case "TurnFrontHorMiddleLeft++":
                ClassCubeTurns.TurnFrontHorMiddleTo("+");
                ClassCubeTurns.TurnFrontHorMiddleTo("+");
                break;
            case "TurnFrontHorMiddleRight--":
                ClassCubeTurns.TurnFrontHorMiddleTo("-");
                ClassCubeTurns.TurnFrontHorMiddleTo("-");
                break;

            case "TurnCubeFrontToRight":
                TurnCubeFrontFaceToRightFace();
                break;
            case "TurnCubeFrontToLeft":
                TurnCubeFrontFaceToLeftFace();
                break;
            case "TurnCubeFrontToUp":
                TurnCubeFrontFaceToUpFace();
                break;
            case "TurnCubeFrontToDown":
                TurnCubeFrontFaceToDownFace();
                break;
            case "TurnCubeUpToRight":
                TurnCubeUpFaceToRightFace();
                break;
            case "TurnCubeUpToLeft":
                TurnCubeUpFaceToLeftFace();
                break;

            default:
                await DisplayAlert(CubeLang.ErrorTitle_Text, "Turn not found", CubeLang.ButtonClose_Text);
                return;
        }
        
        SetCubeColorsFromArrays();
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
    private void OnButtonSaveCubeClicked(object sender, EventArgs e)
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
                sw.WriteLine(aFaceColors[nRow]);
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                sw.WriteLine(aUpFace[nRow]);
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                sw.WriteLine(aFrontFace[nRow]);
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                sw.WriteLine(aRightFace[nRow]);
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                sw.WriteLine(aLeftFace[nRow]);
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                sw.WriteLine(aBackFace[nRow]);
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                sw.WriteLine(aDownFace[nRow]);
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
    private void OnButtonOpenCubeClicked(object sender, EventArgs e)
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
                aFaceColors[nRow] = sr.ReadLine();
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                aUpFace[nRow] = sr.ReadLine();
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                aFrontFace[nRow] = sr.ReadLine();
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                aRightFace[nRow] = sr.ReadLine();
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                aLeftFace[nRow] = sr.ReadLine();
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                aBackFace[nRow] = sr.ReadLine();
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                aDownFace[nRow] = sr.ReadLine();
            }

            // Close the StreamReader object.
            sr.Close();
        }
        catch (Exception ex)
        {
            DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
            return;
        }

        SetCubeColorsFromArrays();
    }

    // On clicked event: Reset the colors of the cube or restart the app.
    private void OnBtnResetClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            // Restart the application to get out of the loop in the Task MakeTurnAsync().
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
            aUpFace[nRow] = aFaceColors[1];
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            aFrontFace[nRow] = aFaceColors[2];
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            aRightFace[nRow] = aFaceColors[3];
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            aLeftFace[nRow] = aFaceColors[4];
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            aBackFace[nRow] = aFaceColors[5];
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            aDownFace[nRow] = aFaceColors[6];
        }

        SetCubeColorsFromArrays();
    }

    // Store the cube colors from the polygons in the arrays.
    private void SetCubeColorsInArrays()
    {
        aFaceColors[1] = GetHexColorPolygon(plgCubeColor1);
        aFaceColors[2] = GetHexColorPolygon(plgCubeColor2);
        aFaceColors[3] = GetHexColorPolygon(plgCubeColor3);
        aFaceColors[4] = GetHexColorPolygon(plgCubeColor4);
        aFaceColors[5] = GetHexColorPolygon(plgCubeColor5);
        aFaceColors[6] = GetHexColorPolygon(plgCubeColor6);

        aUpFace[1] = GetHexColorPolygon(plgUp1);
        aUpFace[2] = GetHexColorPolygon(plgUp2);
        aUpFace[3] = GetHexColorPolygon(plgUp3);
        aUpFace[4] = GetHexColorPolygon(plgUp4);
        aUpFace[5] = GetHexColorPolygon(plgUp5);
        aUpFace[6] = GetHexColorPolygon(plgUp6);
        aUpFace[7] = GetHexColorPolygon(plgUp7);
        aUpFace[8] = GetHexColorPolygon(plgUp8);
        aUpFace[9] = GetHexColorPolygon(plgUp9);

        aFrontFace[1] = GetHexColorPolygon(plgFront1);
        aFrontFace[2] = GetHexColorPolygon(plgFront2);
        aFrontFace[3] = GetHexColorPolygon(plgFront3);
        aFrontFace[4] = GetHexColorPolygon(plgFront4);
        aFrontFace[5] = GetHexColorPolygon(plgFront5);
        aFrontFace[6] = GetHexColorPolygon(plgFront6);
        aFrontFace[7] = GetHexColorPolygon(plgFront7);
        aFrontFace[8] = GetHexColorPolygon(plgFront8);
        aFrontFace[9] = GetHexColorPolygon(plgFront9);

        aRightFace[1] = GetHexColorPolygon(plgRight1);
        aRightFace[2] = GetHexColorPolygon(plgRight2);
        aRightFace[3] = GetHexColorPolygon(plgRight3);
        aRightFace[4] = GetHexColorPolygon(plgRight4);
        aRightFace[5] = GetHexColorPolygon(plgRight5);
        aRightFace[6] = GetHexColorPolygon(plgRight6);
        aRightFace[7] = GetHexColorPolygon(plgRight7);
        aRightFace[8] = GetHexColorPolygon(plgRight8);
        aRightFace[9] = GetHexColorPolygon(plgRight9);

        aLeftFace[1] = GetHexColorPolygon(plgLeft1);
        aLeftFace[2] = GetHexColorPolygon(plgLeft2);
        aLeftFace[3] = GetHexColorPolygon(plgLeft3);
        aLeftFace[4] = GetHexColorPolygon(plgLeft4);
        aLeftFace[5] = GetHexColorPolygon(plgLeft5);
        aLeftFace[6] = GetHexColorPolygon(plgLeft6);
        aLeftFace[7] = GetHexColorPolygon(plgLeft7);
        aLeftFace[8] = GetHexColorPolygon(plgLeft8);
        aLeftFace[9] = GetHexColorPolygon(plgLeft9);

        aBackFace[1] = GetHexColorPolygon(plgBack1);
        aBackFace[2] = GetHexColorPolygon(plgBack2);
        aBackFace[3] = GetHexColorPolygon(plgBack3);
        aBackFace[4] = GetHexColorPolygon(plgBack4);
        aBackFace[5] = GetHexColorPolygon(plgBack5);
        aBackFace[6] = GetHexColorPolygon(plgBack6);
        aBackFace[7] = GetHexColorPolygon(plgBack7);
        aBackFace[8] = GetHexColorPolygon(plgBack8);
        aBackFace[9] = GetHexColorPolygon(plgBack9);

        aDownFace[1] = GetHexColorPolygon(plgDown1);
        aDownFace[2] = GetHexColorPolygon(plgDown2);
        aDownFace[3] = GetHexColorPolygon(plgDown3);
        aDownFace[4] = GetHexColorPolygon(plgDown4);
        aDownFace[5] = GetHexColorPolygon(plgDown5);
        aDownFace[6] = GetHexColorPolygon(plgDown6);
        aDownFace[7] = GetHexColorPolygon(plgDown7);
        aDownFace[8] = GetHexColorPolygon(plgDown8);
        aDownFace[9] = GetHexColorPolygon(plgDown9);

        //---------------------------------------
        //aPieces[36] = GetHexColorPolygon(plgUp1);
        //aPieces[37] = GetHexColorPolygon(plgUp2);
        //aPieces[38] = GetHexColorPolygon(plgUp3);
        //aPieces[39] = GetHexColorPolygon(plgUp4);
        //aPieces[40] = GetHexColorPolygon(plgUp5);
        //aPieces[41] = GetHexColorPolygon(plgUp6);
        //aPieces[42] = GetHexColorPolygon(plgUp7);
        //aPieces[43] = GetHexColorPolygon(plgUp8);
        //aPieces[44] = GetHexColorPolygon(plgUp9);

        //aPieces[0] = GetHexColorPolygon(plgFront1);
        //aPieces[1] = GetHexColorPolygon(plgFront2);
        //aPieces[2] = GetHexColorPolygon(plgFront3);
        //aPieces[3] = GetHexColorPolygon(plgFront4);
        //aPieces[4] = GetHexColorPolygon(plgFront5);
        //aPieces[5] = GetHexColorPolygon(plgFront6);
        //aPieces[6] = GetHexColorPolygon(plgFront7);
        //aPieces[7] = GetHexColorPolygon(plgFront8);
        //aPieces[8] = GetHexColorPolygon(plgFront9);

        //aPieces[9] = GetHexColorPolygon(plgRight1);
        //aPieces[10] = GetHexColorPolygon(plgRight2);
        //aPieces[11] = GetHexColorPolygon(plgRight3);
        //aPieces[12] = GetHexColorPolygon(plgRight4);
        //aPieces[13] = GetHexColorPolygon(plgRight5);
        //aPieces[14] = GetHexColorPolygon(plgRight6);
        //aPieces[15] = GetHexColorPolygon(plgRight7);
        //aPieces[16] = GetHexColorPolygon(plgRight8);
        //aPieces[17] = GetHexColorPolygon(plgRight9);

        //aPieces[27] = GetHexColorPolygon(plgLeft1);
        //aPieces[28] = GetHexColorPolygon(plgLeft2);
        //aPieces[29] = GetHexColorPolygon(plgLeft3);
        //aPieces[30] = GetHexColorPolygon(plgLeft4);
        //aPieces[31] = GetHexColorPolygon(plgLeft5);
        //aPieces[32] = GetHexColorPolygon(plgLeft6);
        //aPieces[33] = GetHexColorPolygon(plgLeft7);
        //aPieces[34] = GetHexColorPolygon(plgLeft8);
        //aPieces[35] = GetHexColorPolygon(plgLeft9);

        //aPieces[18] = GetHexColorPolygon(plgBack1);
        //aPieces[19] = GetHexColorPolygon(plgBack2);
        //aPieces[20] = GetHexColorPolygon(plgBack3);
        //aPieces[21] = GetHexColorPolygon(plgBack4);
        //aPieces[22] = GetHexColorPolygon(plgBack5);
        //aPieces[23] = GetHexColorPolygon(plgBack6);
        //aPieces[24] = GetHexColorPolygon(plgBack7);
        //aPieces[25] = GetHexColorPolygon(plgBack8);
        //aPieces[26] = GetHexColorPolygon(plgBack9);

        //aPieces[45] = GetHexColorPolygon(plgDown1);
        //aPieces[46] = GetHexColorPolygon(plgDown2);
        //aPieces[47] = GetHexColorPolygon(plgDown3);
        //aPieces[48] = GetHexColorPolygon(plgDown4);
        //aPieces[49] = GetHexColorPolygon(plgDown5);
        //aPieces[50] = GetHexColorPolygon(plgDown6);
        //aPieces[51] = GetHexColorPolygon(plgDown7);
        //aPieces[52] = GetHexColorPolygon(plgDown8);
        //aPieces[53] = GetHexColorPolygon(plgDown9);
    }

    // Set the cube colors from the arrays in the polygons.
    public void SetCubeColorsFromArrays()
    {
        Globals.cCubeColor1 = aFaceColors[1];
        Globals.cCubeColor2 = aFaceColors[2];
        Globals.cCubeColor3 = aFaceColors[3];
        Globals.cCubeColor4 = aFaceColors[4];
        Globals.cCubeColor5 = aFaceColors[5];
        Globals.cCubeColor6 = aFaceColors[6];

        plgCubeColor1.Fill = Color.FromArgb(aFaceColors[1]);
        plgCubeColor2.Fill = Color.FromArgb(aFaceColors[2]);
        plgCubeColor3.Fill = Color.FromArgb(aFaceColors[3]);
        plgCubeColor4.Fill = Color.FromArgb(aFaceColors[4]);
        plgCubeColor5.Fill = Color.FromArgb(aFaceColors[5]);
        plgCubeColor6.Fill = Color.FromArgb(aFaceColors[6]);

        plgUp1.Fill = Color.FromArgb(aUpFace[1]);
        plgUp2.Fill = Color.FromArgb(aUpFace[2]);
        plgUp3.Fill = Color.FromArgb(aUpFace[3]);
        plgUp4.Fill = Color.FromArgb(aUpFace[4]);
        plgUp5.Fill = Color.FromArgb(aUpFace[5]);
        plgUp6.Fill = Color.FromArgb(aUpFace[6]);
        plgUp7.Fill = Color.FromArgb(aUpFace[7]);
        plgUp8.Fill = Color.FromArgb(aUpFace[8]);
        plgUp9.Fill = Color.FromArgb(aUpFace[9]);

        plgFront1.Fill = Color.FromArgb(aFrontFace[1]);
        plgFront2.Fill = Color.FromArgb(aFrontFace[2]);
        plgFront3.Fill = Color.FromArgb(aFrontFace[3]);
        plgFront4.Fill = Color.FromArgb(aFrontFace[4]);
        plgFront5.Fill = Color.FromArgb(aFrontFace[5]);
        plgFront6.Fill = Color.FromArgb(aFrontFace[6]);
        plgFront7.Fill = Color.FromArgb(aFrontFace[7]);
        plgFront8.Fill = Color.FromArgb(aFrontFace[8]);
        plgFront9.Fill = Color.FromArgb(aFrontFace[9]);

        plgRight1.Fill = Color.FromArgb(aRightFace[1]);
        plgRight2.Fill = Color.FromArgb(aRightFace[2]);
        plgRight3.Fill = Color.FromArgb(aRightFace[3]);
        plgRight4.Fill = Color.FromArgb(aRightFace[4]);
        plgRight5.Fill = Color.FromArgb(aRightFace[5]);
        plgRight6.Fill = Color.FromArgb(aRightFace[6]);
        plgRight7.Fill = Color.FromArgb(aRightFace[7]);
        plgRight8.Fill = Color.FromArgb(aRightFace[8]);
        plgRight9.Fill = Color.FromArgb(aRightFace[9]);

        plgLeft1.Fill = Color.FromArgb(aLeftFace[1]);
        plgLeft2.Fill = Color.FromArgb(aLeftFace[2]);
        plgLeft3.Fill = Color.FromArgb(aLeftFace[3]);
        plgLeft4.Fill = Color.FromArgb(aLeftFace[4]);
        plgLeft5.Fill = Color.FromArgb(aLeftFace[5]);
        plgLeft6.Fill = Color.FromArgb(aLeftFace[6]);
        plgLeft7.Fill = Color.FromArgb(aLeftFace[7]);
        plgLeft8.Fill = Color.FromArgb(aLeftFace[8]);
        plgLeft9.Fill = Color.FromArgb(aLeftFace[9]);

        plgBack1.Fill = Color.FromArgb(aBackFace[1]);
        plgBack2.Fill = Color.FromArgb(aBackFace[2]);
        plgBack3.Fill = Color.FromArgb(aBackFace[3]);
        plgBack4.Fill = Color.FromArgb(aBackFace[4]);
        plgBack5.Fill = Color.FromArgb(aBackFace[5]);
        plgBack6.Fill = Color.FromArgb(aBackFace[6]);
        plgBack7.Fill = Color.FromArgb(aBackFace[7]);
        plgBack8.Fill = Color.FromArgb(aBackFace[8]);
        plgBack9.Fill = Color.FromArgb(aBackFace[9]);

        plgDown1.Fill = Color.FromArgb(aDownFace[1]);
        plgDown2.Fill = Color.FromArgb(aDownFace[2]);
        plgDown3.Fill = Color.FromArgb(aDownFace[3]);
        plgDown4.Fill = Color.FromArgb(aDownFace[4]);
        plgDown5.Fill = Color.FromArgb(aDownFace[5]);
        plgDown6.Fill = Color.FromArgb(aDownFace[6]);
        plgDown7.Fill = Color.FromArgb(aDownFace[7]);
        plgDown8.Fill = Color.FromArgb(aDownFace[8]);
        plgDown9.Fill = Color.FromArgb(aDownFace[9]);

        //----------------------------------------------------
        //plgUp1.Fill = Color.FromArgb(aPieces[36]);
        //plgUp2.Fill = Color.FromArgb(aPieces[37]);
        //plgUp3.Fill = Color.FromArgb(aPieces[38]);
        //plgUp4.Fill = Color.FromArgb(aPieces[39]);
        //plgUp5.Fill = Color.FromArgb(aPieces[40]);
        //plgUp6.Fill = Color.FromArgb(aPieces[41]);
        //plgUp7.Fill = Color.FromArgb(aPieces[42]);
        //plgUp8.Fill = Color.FromArgb(aPieces[43]);
        //plgUp9.Fill = Color.FromArgb(aPieces[44]);

        //plgFront1.Fill = Color.FromArgb(aPieces[0]);
        //plgFront2.Fill = Color.FromArgb(aPieces[1]);
        //plgFront3.Fill = Color.FromArgb(aPieces[2]);
        //plgFront4.Fill = Color.FromArgb(aPieces[3]);
        //plgFront5.Fill = Color.FromArgb(aPieces[4]);
        //plgFront6.Fill = Color.FromArgb(aPieces[5]);
        //plgFront7.Fill = Color.FromArgb(aPieces[6]);
        //plgFront8.Fill = Color.FromArgb(aPieces[7]);
        //plgFront9.Fill = Color.FromArgb(aPieces[8]);

        //plgRight1.Fill = Color.FromArgb(aPieces[9]);
        //plgRight2.Fill = Color.FromArgb(aPieces[10]);
        //plgRight3.Fill = Color.FromArgb(aPieces[11]);
        //plgRight4.Fill = Color.FromArgb(aPieces[12]);
        //plgRight5.Fill = Color.FromArgb(aPieces[13]);
        //plgRight6.Fill = Color.FromArgb(aPieces[14]);
        //plgRight7.Fill = Color.FromArgb(aPieces[15]);
        //plgRight8.Fill = Color.FromArgb(aPieces[16]);
        //plgRight9.Fill = Color.FromArgb(aPieces[17]);

        //plgLeft1.Fill = Color.FromArgb(aPieces[27]);
        //plgLeft2.Fill = Color.FromArgb(aPieces[28]);
        //plgLeft3.Fill = Color.FromArgb(aPieces[29]);
        //plgLeft4.Fill = Color.FromArgb(aPieces[30]);
        //plgLeft5.Fill = Color.FromArgb(aPieces[31]);
        //plgLeft6.Fill = Color.FromArgb(aPieces[32]);
        //plgLeft7.Fill = Color.FromArgb(aPieces[33]);
        //plgLeft8.Fill = Color.FromArgb(aPieces[34]);
        //plgLeft9.Fill = Color.FromArgb(aPieces[35]);

        //plgBack1.Fill = Color.FromArgb(aPieces[18]);
        //plgBack2.Fill = Color.FromArgb(aPieces[19]);
        //plgBack3.Fill = Color.FromArgb(aPieces[20]);
        //plgBack4.Fill = Color.FromArgb(aPieces[21]);
        //plgBack5.Fill = Color.FromArgb(aPieces[22]);
        //plgBack6.Fill = Color.FromArgb(aPieces[23]);
        //plgBack7.Fill = Color.FromArgb(aPieces[24]);
        //plgBack8.Fill = Color.FromArgb(aPieces[25]);
        //plgBack9.Fill = Color.FromArgb(aPieces[26]);

        //plgDown1.Fill = Color.FromArgb(aPieces[45]);
        //plgDown2.Fill = Color.FromArgb(aPieces[46]);
        //plgDown3.Fill = Color.FromArgb(aPieces[47]);
        //plgDown4.Fill = Color.FromArgb(aPieces[48]);
        //plgDown5.Fill = Color.FromArgb(aPieces[49]);
        //plgDown6.Fill = Color.FromArgb(aPieces[50]);
        //plgDown7.Fill = Color.FromArgb(aPieces[51]);
        //plgDown8.Fill = Color.FromArgb(aPieces[52]);
        //plgDown9.Fill = Color.FromArgb(aPieces[53]);
    }

    // Get the hex color code from a polygon fill property.
    private static string GetHexColorPolygon(Polygon polygon)
    {
        SolidColorBrush brush = (SolidColorBrush)polygon.Fill;
        Color color = brush.Color;

        color = Color.FromRgb(color.Red, color.Green, color.Blue);
        return color.ToHex();
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
            ToolTipProperties.SetText(imgbtnTurnFrontFaceToRight, CubeLang.TurnFrontFaceToRight_Text);
            ToolTipProperties.SetText(imgbtnTurnUpHorMiddleToRightFace, CubeLang.TurnUpMiddleToRightFace_Text);
            ToolTipProperties.SetText(imgbtnTurnBackFaceToLeft, CubeLang.TurnBackFaceToLeft_Text);
            ToolTipProperties.SetText(imgbtnTurnLeftFaceToRight, CubeLang.TurnLeftFaceToRight_Text);
            ToolTipProperties.SetText(imgbtnTurnUpVerMiddleToFrontFace, CubeLang.TurnUpMiddleToFrontFace_Text);
            ToolTipProperties.SetText(imgbtnTurnRightFaceToLeft, CubeLang.TurnRightFaceToLeft_Text);
            ToolTipProperties.SetText(imgbtnTurnUpFaceToLeft, CubeLang.TurnUpFaceToLeft_Text);
            ToolTipProperties.SetText(imgbtnTurnFrontHorMiddleToRightFace, CubeLang.TurnFrontMiddleToRightFace_Text);
            ToolTipProperties.SetText(imgbtnTurnDownFaceToRight, CubeLang.TurnDownFaceToRight_Text);
            ToolTipProperties.SetText(imgbtnTurnUpFaceToRight, CubeLang.TurnUpFaceToRight_Text);
            ToolTipProperties.SetText(imgbtnTurnFrontHorMiddleToLeftFace, CubeLang.TurnRightMiddleToFrontFace_Text);
            ToolTipProperties.SetText(imgbtnTurnDownFaceToLeft, CubeLang.TurnDownFaceToLeft_Text);
            ToolTipProperties.SetText(imgbtnTurnLeftFaceToLeft, CubeLang.TurnLeftFaceToLeft_Text);
            ToolTipProperties.SetText(imgbtnTurnUpVerMiddleToBackFace, CubeLang.TurnFrontMiddleToUpFace_Text);
            ToolTipProperties.SetText(imgbtnTurnRightFaceToRight, CubeLang.TurnRightFaceToRight_Text);
            ToolTipProperties.SetText(imgbtnTurnFrontFaceToLeft, CubeLang.TurnFrontFaceToLeft_Text);
            ToolTipProperties.SetText(imgbtnTurnUpHorMiddleToLeftFace, CubeLang.TurnRightMiddleToUpFace_Text);
            ToolTipProperties.SetText(imgbtnTurnBackFaceToRight, CubeLang.TurnBackFaceToRight_Text);
        }
        else
        {
            ToolTipProperties.SetText(imgbtnTurnFrontFaceToRight, null);
            ToolTipProperties.SetText(imgbtnTurnUpHorMiddleToRightFace, null);
            ToolTipProperties.SetText(imgbtnTurnBackFaceToLeft, null);
            ToolTipProperties.SetText(imgbtnTurnLeftFaceToRight, null);
            ToolTipProperties.SetText(imgbtnTurnUpVerMiddleToFrontFace, null);
            ToolTipProperties.SetText(imgbtnTurnRightFaceToLeft, null);
            ToolTipProperties.SetText(imgbtnTurnUpFaceToLeft, null);
            ToolTipProperties.SetText(imgbtnTurnFrontHorMiddleToRightFace, null);
            ToolTipProperties.SetText(imgbtnTurnDownFaceToRight, null);
            ToolTipProperties.SetText(imgbtnTurnUpFaceToRight, null);
            ToolTipProperties.SetText(imgbtnTurnFrontHorMiddleToLeftFace, null);
            ToolTipProperties.SetText(imgbtnTurnDownFaceToLeft, null);
            ToolTipProperties.SetText(imgbtnTurnLeftFaceToLeft, null);
            ToolTipProperties.SetText(imgbtnTurnUpVerMiddleToBackFace, null);
            ToolTipProperties.SetText(imgbtnTurnRightFaceToRight, null);
            ToolTipProperties.SetText(imgbtnTurnFrontFaceToLeft, null);
            ToolTipProperties.SetText(imgbtnTurnUpHorMiddleToLeftFace, null);
            ToolTipProperties.SetText(imgbtnTurnBackFaceToRight, null);
        }
    }

    // Enable or Disable the arrows.
    private void IsEnabledArrows(bool bEnableDisable)
    {
        imgbtnTurnFrontFaceToRight.IsEnabled = bEnableDisable;
        imgbtnTurnUpHorMiddleToRightFace.IsEnabled = bEnableDisable;
        imgbtnTurnBackFaceToLeft.IsEnabled = bEnableDisable;
        imgbtnTurnLeftFaceToRight.IsEnabled = bEnableDisable;
        imgbtnTurnUpVerMiddleToFrontFace.IsEnabled = bEnableDisable;
        imgbtnTurnRightFaceToLeft.IsEnabled = bEnableDisable;
        imgbtnTurnUpFaceToLeft.IsEnabled = bEnableDisable;
        imgbtnTurnFrontHorMiddleToRightFace.IsEnabled = bEnableDisable;
        imgbtnTurnDownFaceToRight.IsEnabled = bEnableDisable;
        imgbtnTurnUpFaceToRight.IsEnabled = bEnableDisable;
        imgbtnTurnFrontHorMiddleToLeftFace.IsEnabled = bEnableDisable;
        imgbtnTurnDownFaceToLeft.IsEnabled = bEnableDisable;
        imgbtnTurnLeftFaceToLeft.IsEnabled = bEnableDisable;
        imgbtnTurnUpVerMiddleToBackFace.IsEnabled = bEnableDisable;
        imgbtnTurnRightFaceToRight.IsEnabled = bEnableDisable;
        imgbtnTurnFrontFaceToLeft.IsEnabled = bEnableDisable;
        imgbtnTurnUpHorMiddleToLeftFace.IsEnabled = bEnableDisable;
        imgbtnTurnBackFaceToRight.IsEnabled = bEnableDisable;
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
                btnSolveCube.IsEnabled = false;
                imgbtnSetColorsCube.IsEnabled = false;
                imgbtnOpenCube.IsEnabled = false;
                imgbtnSaveCube.IsEnabled = false;
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
    
    // Test the turns of the cube.
    private async Task TestCubeTurnsAsync()
    {
        // Test the face turns.
        await MakeTurnAsync("TurnFront+");
        await MakeTurnAsync("TurnFront++");
        await MakeTurnAsync("TurnFront-");
        await MakeTurnAsync("TurnFront--");
        await MakeTurnAsync("TurnUp+");
        await MakeTurnAsync("TurnUp++");
        await MakeTurnAsync("TurnUp-");
        await MakeTurnAsync("TurnUp--");
        await MakeTurnAsync("TurnDown+");
        await MakeTurnAsync("TurnDown++");
        await MakeTurnAsync("TurnDown-");
        await MakeTurnAsync("TurnDown--");
        await MakeTurnAsync("TurnLeft+");
        await MakeTurnAsync("TurnLeft++");
        await MakeTurnAsync("TurnLeft-");
        await MakeTurnAsync("TurnLeft--");
        await MakeTurnAsync("TurnRight+");
        await MakeTurnAsync("TurnRight++");
        await MakeTurnAsync("TurnRight-");
        await MakeTurnAsync("TurnRight--");
        await MakeTurnAsync("TurnBack+");
        await MakeTurnAsync("TurnBack++");
        await MakeTurnAsync("TurnBack-");
        await MakeTurnAsync("TurnBack--");

        // Test the middle layer turns.
        await MakeTurnAsync("TurnUpHorMiddleRight+");
        await MakeTurnAsync("TurnUpHorMiddleRight++");
        await MakeTurnAsync("TurnUpHorMiddleLeft-");
        await MakeTurnAsync("TurnUpHorMiddleLeft--");
        await MakeTurnAsync("TurnUpVerMiddleBack+");
        await MakeTurnAsync("TurnUpVerMiddleBack++");
        await MakeTurnAsync("TurnUpVerMiddleFront-");
        await MakeTurnAsync("TurnUpVerMiddleFront--");
        await MakeTurnAsync("TurnFrontHorMiddleLeft+");
        await MakeTurnAsync("TurnFrontHorMiddleLeft++");
        await MakeTurnAsync("TurnFrontHorMiddleRight-");
        await MakeTurnAsync("TurnFrontHorMiddleRight--");

        // Test the cube turns.
        await MakeTurnAsync("TurnCubeFrontToRight");
        await MakeTurnAsync("TurnCubeFrontToLeft");
        await MakeTurnAsync("TurnCubeFrontToUp");
        await MakeTurnAsync("TurnCubeFrontToDown");
        await MakeTurnAsync("TurnCubeUpToRight");
        await MakeTurnAsync("TurnCubeUpToLeft");
    }
}
