// Program .....: RubiksCube.sln
// Author ......: Geert Geerits - E-mail: geertgeerits@gmail.com
// Copyright ...: (C) 1981-2024
// Version .....: 2.0.11
// Date ........: 2024-01-10 (YYYY-MM-DD)
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
    private readonly string[] aCubeColors = new string[7];
    private readonly string[] aUpFace = new string[10];
    private readonly string[] aFrontFace = new string[10];
    private readonly string[] aRightFace = new string[10];
    private readonly string[] aLeftFace = new string[10];
    private readonly string[] aBackFace = new string[10];
    private readonly string[] aDownFace = new string[10];

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

        btnSolveCube.IsEnabled = true;
        imgbtnSetColorsCube.IsEnabled = true;
        imgbtnOpenCube.IsEnabled = true;
        imgbtnSaveCube.IsEnabled = true;
    }

    // Solve the cube.  From Basic-80 to C#.
    private async Task SolveTheCubeAsyncNEW()
    {
        // Declare variables.
        int O, P, Q, R, V, X, Y, Z;
        string cB;
        string cX = "";
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
        if (X == 0)
        {
            cX = aFrontFace[1];
        }
        else if (X == 1)
        {
            cX = aFrontFace[2];
        }
        if (cB == aUpFace[6] && cX != aRightFace[2])         // 580 IF B = D(41) AND X <> D(10) THEN Q = 1
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
    private async Task SolveTheCubeAsync()
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
        for (int nTimes = 1; nTimes < 5; nTimes++)
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
        // Top layer.
        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aUpFace[nRow] == aCubeColors[1])
                nNumberOfColors1++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aUpFace[nRow] == aCubeColors[2])
                nNumberOfColors2++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aUpFace[nRow] == aCubeColors[3])
                nNumberOfColors3++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aUpFace[nRow] == aCubeColors[4])
                nNumberOfColors4++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aUpFace[nRow] == aCubeColors[5])
                nNumberOfColors5++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aUpFace[nRow] == aCubeColors[6])
                nNumberOfColors6++;
        }

        // Front face.
        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aFrontFace[nRow] == aCubeColors[1])
                nNumberOfColors1++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aFrontFace[nRow] == aCubeColors[2])
                nNumberOfColors2++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aFrontFace[nRow] == aCubeColors[3])
                nNumberOfColors3++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aFrontFace[nRow] == aCubeColors[4])
                nNumberOfColors4++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aFrontFace[nRow] == aCubeColors[5])
                nNumberOfColors5++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aFrontFace[nRow] == aCubeColors[6])
                nNumberOfColors6++;
        }

        // Right face.
        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aRightFace[nRow] == aCubeColors[1])
                nNumberOfColors1++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aRightFace[nRow] == aCubeColors[2])
                nNumberOfColors2++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aRightFace[nRow] == aCubeColors[3])
                nNumberOfColors3++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aRightFace[nRow] == aCubeColors[4])
                nNumberOfColors4++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aRightFace[nRow] == aCubeColors[5])
                nNumberOfColors5++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aRightFace[nRow] == aCubeColors[6])
                nNumberOfColors6++;
        }

        // Left face.
        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aLeftFace[nRow] == aCubeColors[1])
                nNumberOfColors1++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aLeftFace[nRow] == aCubeColors[2])
                nNumberOfColors2++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aLeftFace[nRow] == aCubeColors[3])
                nNumberOfColors3++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aLeftFace[nRow] == aCubeColors[4])
                nNumberOfColors4++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aLeftFace[nRow] == aCubeColors[5])
                nNumberOfColors5++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aLeftFace[nRow] == aCubeColors[6])
                nNumberOfColors6++;
        }

        // Back face.
        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aBackFace[nRow] == aCubeColors[1])
                nNumberOfColors1++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aBackFace[nRow] == aCubeColors[2])
                nNumberOfColors2++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aBackFace[nRow] == aCubeColors[3])
                nNumberOfColors3++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aBackFace[nRow] == aCubeColors[4])
                nNumberOfColors4++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aBackFace[nRow] == aCubeColors[5])
                nNumberOfColors5++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aBackFace[nRow] == aCubeColors[6])
                nNumberOfColors6++;
        }

        // Bottom layer.
        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aDownFace[nRow] == aCubeColors[1])
                nNumberOfColors1++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aDownFace[nRow] == aCubeColors[2])
                nNumberOfColors2++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aDownFace[nRow] == aCubeColors[3])
                nNumberOfColors3++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aDownFace[nRow] == aCubeColors[4])
                nNumberOfColors4++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aDownFace[nRow] == aCubeColors[5])
                nNumberOfColors5++;
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            if (aDownFace[nRow] == aCubeColors[6])
                nNumberOfColors6++;
        }

        if (nNumberOfColors1 != 9 || nNumberOfColors2 != 9 || nNumberOfColors3 != 9 || nNumberOfColors4 != 9 || nNumberOfColors5 != 9 || nNumberOfColors6 != 9)
        {
            DisplayAlert("Error", CubeLang.MessageNineSameColor_Text, CubeLang.ButtonClose_Text);
            return false;
        }

        // Check the number of colors of the central square of the cube.
        bool bColorCenterCube = true;

        if (aUpFace[5] == aFrontFace[5] || aUpFace[5] == aRightFace[5] || aUpFace[5] == aLeftFace[5] || aUpFace[5] == aBackFace[5] || aUpFace[5] == aDownFace[5])
        {
            bColorCenterCube = false;
        }

        if (aFrontFace[5] == aUpFace[5] || aFrontFace[5] == aRightFace[5] || aFrontFace[5] == aLeftFace[5] || aFrontFace[5] == aBackFace[5] || aFrontFace[5] == aDownFace[5])
        {
            bColorCenterCube = false;
        }

        if (aRightFace[5] == aFrontFace[5] || aRightFace[5] == aUpFace[5] || aRightFace[5] == aLeftFace[5] || aRightFace[5] == aBackFace[5] || aRightFace[5] == aDownFace[5])
        {
            bColorCenterCube = false;
        }

        if (aLeftFace[5] == aFrontFace[5] || aLeftFace[5] == aRightFace[5] || aLeftFace[5] == aUpFace[5] || aLeftFace[5] == aBackFace[5] || aLeftFace[5] == aDownFace[5])
        {
            bColorCenterCube = false;
        }

        if (aBackFace[5] == aFrontFace[5] || aBackFace[5] == aRightFace[5] || aBackFace[5] == aLeftFace[5] || aBackFace[5] == aUpFace[5] || aBackFace[5] == aDownFace[5])
        {
            bColorCenterCube = false;
        }

        if (aDownFace[5] == aFrontFace[5] || aDownFace[5] == aRightFace[5] || aDownFace[5] == aLeftFace[5] || aDownFace[5] == aBackFace[5] || aDownFace[5] == aUpFace[5])
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
        
        if (aUpFace[7] == aLeftFace[3] || aUpFace[7] == aFrontFace[1] || aFrontFace[1] == aLeftFace[3])
        {
            bColorCornerCube = false;
        }

        if (aUpFace[1] == aLeftFace[1] || aUpFace[1] == aBackFace[3] || aLeftFace[1] == aBackFace[3])
        {
            bColorCornerCube = false;
        }

        if (aUpFace[3] == aRightFace[3] || aUpFace[3] == aBackFace[1] || aRightFace[3] == aBackFace[1])
        {
            bColorCornerCube = false;
        }

        if (aUpFace[9] == aFrontFace[3] || aUpFace[9] == aRightFace[1] || aFrontFace[3] == aRightFace[1])
        {
            bColorCornerCube = false;
        }

        if (aDownFace[1] == aLeftFace[9] || aDownFace[1] == aFrontFace[7] || aFrontFace[7] == aLeftFace[9])
        {
            bColorCornerCube = false;
        }

        if (aDownFace[7] == aLeftFace[7] || aDownFace[7] == aBackFace[9] || aBackFace[9] == aLeftFace[7])
        {
            bColorCornerCube = false;
        }

        if (aDownFace[9] == aRightFace[9] || aDownFace[9] == aBackFace[7] || aBackFace[7] == aRightFace[9])
        {
            bColorCornerCube = false;
        }

        if (aDownFace[3] == aRightFace[7] || aDownFace[3] == aFrontFace[9] || aFrontFace[9] == aRightFace[7])
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

        if (aUpFace[2] == aBackFace[2] || aUpFace[4] == aLeftFace[2] || aUpFace[6] == aRightFace[2] || aUpFace[8] == aFrontFace[2])
        {
            bColorEdgeCube = false;
        }

        if (aDownFace[2] == aFrontFace[8] || aDownFace[4] == aLeftFace[8] || aDownFace[6] == aRightFace[8] || aDownFace[8] == aBackFace[8])
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
        bool bColorsUp = false;
        bool bColorsFront = false;
        bool bColorsRight = false;
        bool bColorsLeft = false;
        bool bColorsBack = false;
        bool bColorsDown = false;
        
        if (aUpFace[1] == aUpFace[2] && aUpFace[1] == aUpFace[3] && aUpFace[1] == aUpFace[4] && aUpFace[1] == aUpFace[5] && aUpFace[1] == aUpFace[6] && aUpFace[1] == aUpFace[7] && aUpFace[1] == aUpFace[8] && aUpFace[1] == aUpFace[9])
        {
            bColorsUp = true;
        }

        if (aFrontFace[1] == aFrontFace[2] && aFrontFace[1] == aFrontFace[3] && aFrontFace[1] == aFrontFace[4] && aFrontFace[1] == aFrontFace[5] && aFrontFace[1] == aFrontFace[6] && aFrontFace[1] == aFrontFace[7] && aFrontFace[1] == aFrontFace[8] && aFrontFace[1] == aFrontFace[9])
        {
            bColorsFront = true;
        }

        if (aRightFace[1] == aRightFace[2] && aRightFace[1] == aRightFace[3] && aRightFace[1] == aRightFace[4] && aRightFace[1] == aRightFace[5] && aRightFace[1] == aRightFace[6] && aRightFace[1] == aRightFace[7] && aRightFace[1] == aRightFace[8] && aRightFace[1] == aRightFace[9])
        {
            bColorsRight = true;
        }

        if (aLeftFace[1] == aLeftFace[2] && aLeftFace[1] == aLeftFace[3] && aLeftFace[1] == aLeftFace[4] && aLeftFace[1] == aLeftFace[5] && aLeftFace[1] == aLeftFace[6] && aLeftFace[1] == aLeftFace[7] && aLeftFace[1] == aLeftFace[8] && aLeftFace[1] == aLeftFace[9])
        {
            bColorsLeft = true;
        }

        if (aBackFace[1] == aBackFace[2] && aBackFace[1] == aBackFace[3] && aBackFace[1] == aBackFace[4] && aBackFace[1] == aBackFace[5] && aBackFace[1] == aBackFace[6] && aBackFace[1] == aBackFace[7] && aBackFace[1] == aBackFace[8] && aBackFace[1] == aBackFace[9])
        {
            bColorsBack = true;
        }

        if (aDownFace[1] == aDownFace[2] && aDownFace[1] == aDownFace[3] && aDownFace[1] == aDownFace[4] && aDownFace[1] == aDownFace[5] && aDownFace[1] == aDownFace[6] && aDownFace[1] == aDownFace[7] && aDownFace[1] == aDownFace[8] && aDownFace[1] == aDownFace[9])
        {
            bColorsDown = true;
        }

        if (!bColorsUp || !bColorsFront || !bColorsRight || !bColorsLeft || !bColorsBack || !bColorsDown)
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
        TurnFrontFaceTo("+");
        GetCubeColorsFromArrays();
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
        TurnUpHorMiddleTo("+");
        GetCubeColorsFromArrays();
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
        TurnBackFaceTo("-");
        GetCubeColorsFromArrays();
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
        TurnLeftFaceTo("+");
        GetCubeColorsFromArrays();
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
        TurnUpVerMiddleTo("-");
        GetCubeColorsFromArrays();
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
        TurnRightFaceTo("-");
        GetCubeColorsFromArrays();
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
        TurnUpFaceTo("-");
        GetCubeColorsFromArrays();
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
        TurnFrontHorMiddleTo("-");
        GetCubeColorsFromArrays();
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
        TurnDownFaceTo("+");
        GetCubeColorsFromArrays();
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
        TurnUpFaceTo("+");
        GetCubeColorsFromArrays();
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
        TurnFrontHorMiddleTo("+");
        GetCubeColorsFromArrays();
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
        TurnDownFaceTo("-");
        GetCubeColorsFromArrays();
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
        TurnLeftFaceTo("-");
        GetCubeColorsFromArrays();
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
        TurnUpVerMiddleTo("+");
        GetCubeColorsFromArrays();
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
        TurnRightFaceTo("+");
        GetCubeColorsFromArrays();
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
        TurnFrontFaceTo("-");
        GetCubeColorsFromArrays();
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
        TurnUpHorMiddleTo("-");
        GetCubeColorsFromArrays();
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
        TurnBackFaceTo("+");
        GetCubeColorsFromArrays();
    }

    // Turn the entire cube a quarter turn.
    // Rotate the entire cube so that the front goes to the left face.
    private void TurnCubeFrontFaceToLeftFace()
    {
        if (!bSolvingCube)
        {
            ExplainTurnCube(CubeLang.TurnCubeFrontFaceToLeftFace_Text);
        }
            
        TurnUpFaceTo("+");
        TurnFrontHorMiddleTo("+");
        TurnDownFaceTo("-");
        GetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the front goes to the right face.
    private void TurnCubeFrontFaceToRightFace()
    {
        if (!bSolvingCube)
        {
            ExplainTurnCube(CubeLang.TurnCubeFrontFaceToRightFace_Text);
        }

        TurnUpFaceTo("-");
        TurnFrontHorMiddleTo("-");
        TurnDownFaceTo("+");
        GetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the front goes to the upper face.
    private void TurnCubeFrontFaceToUpFace()
    {
        if (!bSolvingCube)
        {
            ExplainTurnCube(CubeLang.TurnCubeFrontFaceToUpFace_Text);
        }

        TurnRightFaceTo("+");
        TurnUpVerMiddleTo("+");
        TurnLeftFaceTo("-");
        GetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the front goes to the down face.
    private void TurnCubeFrontFaceToDownFace()
    {
        if (!bSolvingCube)
        {
            ExplainTurnCube(CubeLang.TurnCubeFrontFaceToDownFace_Text);
        }

        TurnRightFaceTo("-");
        TurnUpVerMiddleTo("-");
        TurnLeftFaceTo("+");
        GetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the upper face goes to the right face.
    private void TurnCubeUpFaceToRightFace()
    {
        if (!bSolvingCube)
        {
            ExplainTurnCube(CubeLang.TurnCubeUpFaceToRightFace_Text);
        }

        TurnFrontFaceTo("+");
        TurnUpHorMiddleTo("+");
        TurnBackFaceTo("-");
        GetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the upper face goes to the left face.
    private void TurnCubeUpFaceToLeftFace()
    {
        if (!bSolvingCube)
        {
            ExplainTurnCube(CubeLang.TurnCubeUpFaceToLeftFace_Text);
        }

        TurnFrontFaceTo("-");
        TurnUpHorMiddleTo("-");
        TurnBackFaceTo("+");
        GetCubeColorsFromArrays();
    }

    // Turn the entire front face clockwise or counter clockwise.
    private void TurnFrontFaceTo(string cDirection)
    {
        string cColorFront1 = aFrontFace[1];
        string cColorFront2 = aFrontFace[2];
        string cColorFront3 = aFrontFace[3];
        string cColorFront4 = aFrontFace[4];
        string cColorFront6 = aFrontFace[6];
        string cColorFront7 = aFrontFace[7];
        string cColorFront8 = aFrontFace[8];
        string cColorFront9 = aFrontFace[9];

        string cColorUp7 = aUpFace[7];
        string cColorUp8 = aUpFace[8];
        string cColorUp9 = aUpFace[9];

        string cColorRight1 = aRightFace[1];
        string cColorRight4 = aRightFace[4];
        string cColorRight7 = aRightFace[7];

        string cColorDown1 = aDownFace[1];
        string cColorDown2 = aDownFace[2];
        string cColorDown3 = aDownFace[3];

        string cColorLeft3 = aLeftFace[3];
        string cColorLeft6 = aLeftFace[6];
        string cColorLeft9 = aLeftFace[9];

        if (cDirection == "+")
        {
            aFrontFace[1] = cColorFront7;
            aFrontFace[2] = cColorFront4;
            aFrontFace[3] = cColorFront1;
            aFrontFace[4] = cColorFront8;
            aFrontFace[6] = cColorFront2;
            aFrontFace[7] = cColorFront9;
            aFrontFace[8] = cColorFront6;
            aFrontFace[9] = cColorFront3;

            aUpFace[7] = cColorLeft9;
            aUpFace[8] = cColorLeft6;
            aUpFace[9] = cColorLeft3;

            aRightFace[1] = cColorUp7;
            aRightFace[4] = cColorUp8;
            aRightFace[7] = cColorUp9;

            aDownFace[1] = cColorRight7;
            aDownFace[2] = cColorRight4;
            aDownFace[3] = cColorRight1;

            aLeftFace[3] = cColorDown1;
            aLeftFace[6] = cColorDown2;
            aLeftFace[9] = cColorDown3;
        }

        if (cDirection == "-")
        {
            aFrontFace[1] = cColorFront3;
            aFrontFace[2] = cColorFront6;
            aFrontFace[3] = cColorFront9;
            aFrontFace[4] = cColorFront2;
            aFrontFace[6] = cColorFront8;
            aFrontFace[7] = cColorFront1;
            aFrontFace[8] = cColorFront4;
            aFrontFace[9] = cColorFront7;

            aUpFace[7] = cColorRight1;
            aUpFace[8] = cColorRight4;
            aUpFace[9] = cColorRight7;

            aRightFace[1] = cColorDown3;
            aRightFace[4] = cColorDown2;
            aRightFace[7] = cColorDown1;

            aDownFace[1] = cColorLeft3;
            aDownFace[2] = cColorLeft6;
            aDownFace[3] = cColorLeft9;

            aLeftFace[3] = cColorUp9;
            aLeftFace[6] = cColorUp8;
            aLeftFace[9] = cColorUp7;
        }
    }

    // Turn the top horizontal middle layer to the right or left.
    private void TurnUpHorMiddleTo(string cDirection)
    {
        string cColorUp4 = aUpFace[4];
        string cColorUp5 = aUpFace[5];
        string cColorUp6 = aUpFace[6];

        string cColorRight2 = aRightFace[2];
        string cColorRight5 = aRightFace[5];
        string cColorRight8 = aRightFace[8];

        string cColorDown4 = aDownFace[4];
        string cColorDown5 = aDownFace[5];
        string cColorDown6 = aDownFace[6];

        string cColorLeft2 = aLeftFace[2];
        string cColorLeft5 = aLeftFace[5];
        string cColorLeft8 = aLeftFace[8];

        if (cDirection == "+")
        {
            aUpFace[4] = cColorLeft8;
            aUpFace[5] = cColorLeft5;
            aUpFace[6] = cColorLeft2;

            aRightFace[2] = cColorUp4;
            aRightFace[5] = cColorUp5;
            aRightFace[8] = cColorUp6;

            aDownFace[4] = cColorRight8;
            aDownFace[5] = cColorRight5;
            aDownFace[6] = cColorRight2;

            aLeftFace[2] = cColorDown4;
            aLeftFace[5] = cColorDown5;
            aLeftFace[8] = cColorDown6;
        }

        if (cDirection == "-")
        {
            aUpFace[4] = cColorRight2;
            aUpFace[5] = cColorRight5;
            aUpFace[6] = cColorRight8;

            aRightFace[2] = cColorDown6;
            aRightFace[5] = cColorDown5;
            aRightFace[8] = cColorDown4;

            aDownFace[4] = cColorLeft2;
            aDownFace[5] = cColorLeft5;
            aDownFace[6] = cColorLeft8;

            aLeftFace[2] = cColorUp6;
            aLeftFace[5] = cColorUp5;
            aLeftFace[8] = cColorUp4;
        }
    }

    // Turn the entire back face clockwise or counter clockwise.
    private void TurnBackFaceTo(string cDirection)
    {
        string cColorBack1 = aBackFace[1];
        string cColorBack2 = aBackFace[2];
        string cColorBack3 = aBackFace[3];
        string cColorBack4 = aBackFace[4];
        string cColorBack6 = aBackFace[6];
        string cColorBack7 = aBackFace[7];
        string cColorBack8 = aBackFace[8];
        string cColorBack9 = aBackFace[9];

        string cColorUp1 = aUpFace[1];
        string cColorUp2 = aUpFace[2];
        string cColorUp3 = aUpFace[3];

        string cColorRight3 = aRightFace[3];
        string cColorRight6 = aRightFace[6];
        string cColorRight9 = aRightFace[9];

        string cColorDown7 = aDownFace[7];
        string cColorDown8 = aDownFace[8];
        string cColorDown9 = aDownFace[9];

        string cColorLeft1 = aLeftFace[1];
        string cColorLeft4 = aLeftFace[4];
        string cColorLeft7 = aLeftFace[7];

        if (cDirection == "+")
        {
            aBackFace[1] = cColorBack7;
            aBackFace[2] = cColorBack4;
            aBackFace[3] = cColorBack1;
            aBackFace[4] = cColorBack8;
            aBackFace[6] = cColorBack2;
            aBackFace[7] = cColorBack9;
            aBackFace[8] = cColorBack6;
            aBackFace[9] = cColorBack3;

            aUpFace[1] = cColorRight3;
            aUpFace[2] = cColorRight6;
            aUpFace[3] = cColorRight9;

            aRightFace[3] = cColorDown9;
            aRightFace[6] = cColorDown8;
            aRightFace[9] = cColorDown7;

            aDownFace[7] = cColorLeft1;
            aDownFace[8] = cColorLeft4;
            aDownFace[9] = cColorLeft7;

            aLeftFace[1] = cColorUp3;
            aLeftFace[4] = cColorUp2;
            aLeftFace[7] = cColorUp1;
        }

        if (cDirection == "-")
        {
            aBackFace[1] = cColorBack3;
            aBackFace[2] = cColorBack6;
            aBackFace[3] = cColorBack9;
            aBackFace[4] = cColorBack2;
            aBackFace[6] = cColorBack8;
            aBackFace[7] = cColorBack1;
            aBackFace[8] = cColorBack4;
            aBackFace[9] = cColorBack7;

            aUpFace[1] = cColorLeft7;
            aUpFace[2] = cColorLeft4;
            aUpFace[3] = cColorLeft1;

            aRightFace[3] = cColorUp1;
            aRightFace[6] = cColorUp2;
            aRightFace[9] = cColorUp3;

            aDownFace[7] = cColorRight9;
            aDownFace[8] = cColorRight6;
            aDownFace[9] = cColorRight3;

            aLeftFace[1] = cColorDown7;
            aLeftFace[4] = cColorDown8;
            aLeftFace[7] = cColorDown9;
        }
    }

    // Turn the entire left face clockwise or counter clockwise.
    private void TurnLeftFaceTo(string cDirection)
    {
        string cColorLeft1 = aLeftFace[1];
        string cColorLeft2 = aLeftFace[2];
        string cColorLeft3 = aLeftFace[3];
        string cColorLeft4 = aLeftFace[4];
        string cColorLeft6 = aLeftFace[6];
        string cColorLeft7 = aLeftFace[7];
        string cColorLeft8 = aLeftFace[8];
        string cColorLeft9 = aLeftFace[9];

        string cColorUp1 = aUpFace[1];
        string cColorUp4 = aUpFace[4];
        string cColorUp7 = aUpFace[7];

        string cColorFront1 = aFrontFace[1];
        string cColorFront4 = aFrontFace[4];
        string cColorFront7 = aFrontFace[7];

        string cColorDown1 = aDownFace[1];
        string cColorDown4 = aDownFace[4];
        string cColorDown7 = aDownFace[7];

        string cColorBack3 = aBackFace[3];
        string cColorBack6 = aBackFace[6];
        string cColorBack9 = aBackFace[9];

        if (cDirection == "+")
        {
            aLeftFace[1] = cColorLeft7;
            aLeftFace[2] = cColorLeft4;
            aLeftFace[3] = cColorLeft1;
            aLeftFace[4] = cColorLeft8;
            aLeftFace[6] = cColorLeft2;
            aLeftFace[7] = cColorLeft9;
            aLeftFace[8] = cColorLeft6;
            aLeftFace[9] = cColorLeft3;

            aUpFace[1] = cColorBack9;
            aUpFace[4] = cColorBack6;
            aUpFace[7] = cColorBack3;

            aFrontFace[1] = cColorUp1;
            aFrontFace[4] = cColorUp4;
            aFrontFace[7] = cColorUp7;

            aDownFace[1] = cColorFront1;
            aDownFace[4] = cColorFront4;
            aDownFace[7] = cColorFront7;

            aBackFace[3] = cColorDown7;
            aBackFace[6] = cColorDown4;
            aBackFace[9] = cColorDown1;
        }

        if (cDirection == "-")
        {
            aLeftFace[1] = cColorLeft3;
            aLeftFace[2] = cColorLeft6;
            aLeftFace[3] = cColorLeft9;
            aLeftFace[4] = cColorLeft2;
            aLeftFace[6] = cColorLeft8;
            aLeftFace[7] = cColorLeft1;
            aLeftFace[8] = cColorLeft4;
            aLeftFace[9] = cColorLeft7;

            aUpFace[1] = cColorFront1;
            aUpFace[4] = cColorFront4;
            aUpFace[7] = cColorFront7;

            aFrontFace[1] = cColorDown1;
            aFrontFace[4] = cColorDown4;
            aFrontFace[7] = cColorDown7;

            aDownFace[1] = cColorBack9;
            aDownFace[4] = cColorBack6;
            aDownFace[7] = cColorBack3;

            aBackFace[3] = cColorUp7;
            aBackFace[6] = cColorUp4;
            aBackFace[9] = cColorUp1;
        }
    }

    // Turn the top vertical middle layer to back or front.
    private void TurnUpVerMiddleTo(string cDirection)
    {
        string cColorUp2 = aUpFace[2];
        string cColorUp5 = aUpFace[5];
        string cColorUp8 = aUpFace[8];

        string cColorFront2 = aFrontFace[2];
        string cColorFront5 = aFrontFace[5];
        string cColorFront8 = aFrontFace[8];

        string cColorDown2 = aDownFace[2];
        string cColorDown5 = aDownFace[5];
        string cColorDown8 = aDownFace[8];

        string cColorBack2 = aBackFace[2];
        string cColorBack5 = aBackFace[5];
        string cColorBack8 = aBackFace[8];

        if (cDirection == "+")
        {
            aUpFace[2] = cColorFront2;
            aUpFace[5] = cColorFront5;
            aUpFace[8] = cColorFront8;

            aFrontFace[2] = cColorDown2;
            aFrontFace[5] = cColorDown5;
            aFrontFace[8] = cColorDown8;

            aDownFace[2] = cColorBack8;
            aDownFace[5] = cColorBack5;
            aDownFace[8] = cColorBack2;

            aBackFace[2] = cColorUp8;
            aBackFace[5] = cColorUp5;
            aBackFace[8] = cColorUp2;
        }

        if (cDirection == "-")
        {
            aUpFace[2] = cColorBack8;
            aUpFace[5] = cColorBack5;
            aUpFace[8] = cColorBack2;

            aFrontFace[2] = cColorUp2;
            aFrontFace[5] = cColorUp5;
            aFrontFace[8] = cColorUp8;

            aDownFace[2] = cColorFront2;
            aDownFace[5] = cColorFront5;
            aDownFace[8] = cColorFront8;

            aBackFace[2] = cColorDown8;
            aBackFace[5] = cColorDown5;
            aBackFace[8] = cColorDown2;
        }
    }

    // Turn the entire right face clockwise or counter clockwise.
    private void TurnRightFaceTo(string cDirection)
    {
        string cColorRight1 = aRightFace[1];
        string cColorRight2 = aRightFace[2];
        string cColorRight3 = aRightFace[3];
        string cColorRight4 = aRightFace[4];
        string cColorRight6 = aRightFace[6];
        string cColorRight7 = aRightFace[7];
        string cColorRight8 = aRightFace[8];
        string cColorRight9 = aRightFace[9];

        string cColorUp3 = aUpFace[3];
        string cColorUp6 = aUpFace[6];
        string cColorUp9 = aUpFace[9];

        string cColorFront3 = aFrontFace[3];
        string cColorFront6 = aFrontFace[6];
        string cColorFront9 = aFrontFace[9];

        string cColorDown3 = aDownFace[3];
        string cColorDown6 = aDownFace[6];
        string cColorDown9 = aDownFace[9];

        string cColorBack1 = aBackFace[1];
        string cColorBack4 = aBackFace[4];
        string cColorBack7 = aBackFace[7];

        if (cDirection == "+")
        {
            aRightFace[1] = cColorRight7;
            aRightFace[2] = cColorRight4;
            aRightFace[3] = cColorRight1;
            aRightFace[4] = cColorRight8;
            aRightFace[6] = cColorRight2;
            aRightFace[7] = cColorRight9;
            aRightFace[8] = cColorRight6;
            aRightFace[9] = cColorRight3;

            aUpFace[3] = cColorFront3;
            aUpFace[6] = cColorFront6;
            aUpFace[9] = cColorFront9;

            aFrontFace[3] = cColorDown3;
            aFrontFace[6] = cColorDown6;
            aFrontFace[9] = cColorDown9;

            aDownFace[3] = cColorBack7;
            aDownFace[6] = cColorBack4;
            aDownFace[9] = cColorBack1;

            aBackFace[1] = cColorUp9;
            aBackFace[4] = cColorUp6;
            aBackFace[7] = cColorUp3;
        }

        if (cDirection == "-")
        {
            aRightFace[1] = cColorRight3;
            aRightFace[2] = cColorRight6;
            aRightFace[3] = cColorRight9;
            aRightFace[4] = cColorRight2;
            aRightFace[6] = cColorRight8;
            aRightFace[7] = cColorRight1;
            aRightFace[8] = cColorRight4;
            aRightFace[9] = cColorRight7;

            aUpFace[3] = cColorBack7;
            aUpFace[6] = cColorBack4;
            aUpFace[9] = cColorBack1;

            aFrontFace[3] = cColorUp3;
            aFrontFace[6] = cColorUp6;
            aFrontFace[9] = cColorUp9;

            aDownFace[3] = cColorFront3;
            aDownFace[6] = cColorFront6;
            aDownFace[9] = cColorFront9;

            aBackFace[1] = cColorDown9;
            aBackFace[4] = cColorDown6;
            aBackFace[7] = cColorDown3;
        }
    }

    // Turn the entire upper face clockwise or counter clockwise.
    private void TurnUpFaceTo(string cDirection)
    {
        string cColorUp1 = aUpFace[1];
        string cColorUp2 = aUpFace[2];
        string cColorUp3 = aUpFace[3];
        string cColorUp4 = aUpFace[4];
        string cColorUp6 = aUpFace[6];
        string cColorUp7 = aUpFace[7];
        string cColorUp8 = aUpFace[8];
        string cColorUp9 = aUpFace[9];

        string cColorLeft1 = aLeftFace[1];
        string cColorLeft2 = aLeftFace[2];
        string cColorLeft3 = aLeftFace[3];

        string cColorFront1 = aFrontFace[1];
        string cColorFront2 = aFrontFace[2];
        string cColorFront3 = aFrontFace[3];

        string cColorRight1 = aRightFace[1];
        string cColorRight2 = aRightFace[2];
        string cColorRight3 = aRightFace[3];

        string cColorBack1 = aBackFace[1];
        string cColorBack2 = aBackFace[2];
        string cColorBack3 = aBackFace[3];

        if (cDirection == "+")
        {
            aUpFace[1] = cColorUp7;
            aUpFace[2] = cColorUp4;
            aUpFace[3] = cColorUp1;
            aUpFace[4] = cColorUp8;
            aUpFace[6] = cColorUp2;
            aUpFace[7] = cColorUp9;
            aUpFace[8] = cColorUp6;
            aUpFace[9] = cColorUp3;

            aLeftFace[1] = cColorFront1;
            aLeftFace[2] = cColorFront2;
            aLeftFace[3] = cColorFront3;

            aFrontFace[1] = cColorRight1;
            aFrontFace[2] = cColorRight2;
            aFrontFace[3] = cColorRight3;

            aRightFace[1] = cColorBack1;
            aRightFace[2] = cColorBack2;
            aRightFace[3] = cColorBack3;

            aBackFace[1] = cColorLeft1;
            aBackFace[2] = cColorLeft2;
            aBackFace[3] = cColorLeft3;
        }

        if (cDirection == "-")
        {
            aUpFace[1] = cColorUp3;
            aUpFace[2] = cColorUp6;
            aUpFace[3] = cColorUp9;
            aUpFace[4] = cColorUp2;
            aUpFace[6] = cColorUp8;
            aUpFace[7] = cColorUp1;
            aUpFace[8] = cColorUp4;
            aUpFace[9] = cColorUp7;

            aLeftFace[1] = cColorBack1;
            aLeftFace[2] = cColorBack2;
            aLeftFace[3] = cColorBack3;

            aFrontFace[1] = cColorLeft1;
            aFrontFace[2] = cColorLeft2;
            aFrontFace[3] = cColorLeft3;

            aRightFace[1] = cColorFront1;
            aRightFace[2] = cColorFront2;
            aRightFace[3] = cColorFront3;

            aBackFace[1] = cColorRight1;
            aBackFace[2] = cColorRight2;
            aBackFace[3] = cColorRight3;
        }
    }

    // Turn the front horizontal middle layer to right or left.
    private void TurnFrontHorMiddleTo(string cDirection)
    {
        string cColorFront4 = aFrontFace[4];
        string cColorFront5 = aFrontFace[5];
        string cColorFront6 = aFrontFace[6];

        string cColorRight4 = aRightFace[4];
        string cColorRight5 = aRightFace[5];
        string cColorRight6 = aRightFace[6];

        string cColorBack4 = aBackFace[4];
        string cColorBack5 = aBackFace[5];
        string cColorBack6 = aBackFace[6];

        string cColorLeft4 = aLeftFace[4];
        string cColorLeft5 = aLeftFace[5];
        string cColorLeft6 = aLeftFace[6];

        if (cDirection == "+")
        {
            aFrontFace[4] = cColorRight4;
            aFrontFace[5] = cColorRight5;
            aFrontFace[6] = cColorRight6;

            aRightFace[4] = cColorBack4;
            aRightFace[5] = cColorBack5;
            aRightFace[6] = cColorBack6;

            aBackFace[4] = cColorLeft4;
            aBackFace[5] = cColorLeft5;
            aBackFace[6] = cColorLeft6;

            aLeftFace[4] = cColorFront4;
            aLeftFace[5] = cColorFront5;
            aLeftFace[6] = cColorFront6;
        }

        if (cDirection == "-")
        {
            aFrontFace[4] = cColorLeft4;
            aFrontFace[5] = cColorLeft5;
            aFrontFace[6] = cColorLeft6;

            aRightFace[4] = cColorFront4;
            aRightFace[5] = cColorFront5;
            aRightFace[6] = cColorFront6;

            aBackFace[4] = cColorRight4;
            aBackFace[5] = cColorRight5;
            aBackFace[6] = cColorRight6;

            aLeftFace[4] = cColorBack4;
            aLeftFace[5] = cColorBack5;
            aLeftFace[6] = cColorBack6;
        }
    }

    // Turn the entire down face clockwise or counter clockwise.
    private void TurnDownFaceTo(string cDirection)
    {
        string cColorDown1 = aDownFace[1];
        string cColorDown2 = aDownFace[2];
        string cColorDown3 = aDownFace[3];
        string cColorDown4 = aDownFace[4];
        string cColorDown6 = aDownFace[6];
        string cColorDown7 = aDownFace[7];
        string cColorDown8 = aDownFace[8];
        string cColorDown9 = aDownFace[9];

        string cColorLeft7 = aLeftFace[7];
        string cColorLeft8 = aLeftFace[8];
        string cColorLeft9 = aLeftFace[9];

        string cColorFront7 = aFrontFace[7];
        string cColorFront8 = aFrontFace[8];
        string cColorFront9 = aFrontFace[9];

        string cColorRight7 = aRightFace[7];
        string cColorRight8 = aRightFace[8];
        string cColorRight9 = aRightFace[9];

        string cColorBack7 = aBackFace[7];
        string cColorBack8 = aBackFace[8];
        string cColorBack9 = aBackFace[9];

        if (cDirection == "+")
        {
            aDownFace[1] = cColorDown7;
            aDownFace[2] = cColorDown4;
            aDownFace[3] = cColorDown1;
            aDownFace[4] = cColorDown8;
            aDownFace[6] = cColorDown2;
            aDownFace[7] = cColorDown9;
            aDownFace[8] = cColorDown6;
            aDownFace[9] = cColorDown3;

            aLeftFace[7] = cColorBack7;
            aLeftFace[8] = cColorBack8;
            aLeftFace[9] = cColorBack9;

            aFrontFace[7] = cColorLeft7;
            aFrontFace[8] = cColorLeft8;
            aFrontFace[9] = cColorLeft9;

            aRightFace[7] = cColorFront7;
            aRightFace[8] = cColorFront8;
            aRightFace[9] = cColorFront9;

            aBackFace[7] = cColorRight7;
            aBackFace[8] = cColorRight8;
            aBackFace[9] = cColorRight9;
        }

        if (cDirection == "-")
        {
            aDownFace[1] = cColorDown3;
            aDownFace[2] = cColorDown6;
            aDownFace[3] = cColorDown9;
            aDownFace[4] = cColorDown2;
            aDownFace[6] = cColorDown8;
            aDownFace[7] = cColorDown1;
            aDownFace[8] = cColorDown4;
            aDownFace[9] = cColorDown7;

            aLeftFace[7] = cColorFront7;
            aLeftFace[8] = cColorFront8;
            aLeftFace[9] = cColorFront9;

            aFrontFace[7] = cColorRight7;
            aFrontFace[8] = cColorRight8;
            aFrontFace[9] = cColorRight9;

            aRightFace[7] = cColorBack7;
            aRightFace[8] = cColorBack8;
            aRightFace[9] = cColorBack9;

            aBackFace[7] = cColorLeft7;
            aBackFace[8] = cColorLeft8;
            aBackFace[9] = cColorLeft9;
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

    // Make and explain the turn of the cube called from the main task SolveTheCubeAsync().
    private async Task MakeTurnAsync(string cTurnFaceAndDirection)
    {
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
                await DisplayAlert("Error", "Turn not found", "OK");
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
                await DisplayAlert("Error", "Turn not found", "OK");
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
                TurnFrontFaceTo("+");
                break;
            case "TurnFront-":
                TurnFrontFaceTo("-");
                break;
            case "TurnUp+":
                TurnUpFaceTo("+");
                break;
            case "TurnUp-":
                TurnUpFaceTo("-");
                break;
            case "TurnDown+":
                TurnDownFaceTo("+");
                break;
            case "TurnDown-":
                TurnDownFaceTo("-");
                break;
            case "TurnLeft+":
                TurnLeftFaceTo("+");
                break;
            case "TurnLeft-":
                TurnLeftFaceTo("-");
                break;
            case "TurnRight+":
                TurnRightFaceTo("+");
                break;
            case "TurnRight-":
                TurnRightFaceTo("-");
                break;
            case "TurnBack+":
                TurnBackFaceTo("+");
                break;
            case "TurnBack-":
                TurnBackFaceTo("-");
                break;

            case "TurnFront++":
                TurnFrontFaceTo("+");
                TurnFrontFaceTo("+");
                break;
            case "TurnFront--":
                TurnFrontFaceTo("-");
                TurnFrontFaceTo("-");
                break;
            case "TurnUp++":
                TurnUpFaceTo("+");
                TurnUpFaceTo("+");
                break;
            case "TurnUp--":
                TurnUpFaceTo("-");
                TurnUpFaceTo("-");
                break;
            case "TurnDown++":
                TurnDownFaceTo("+");
                TurnDownFaceTo("+");
                break;
            case "TurnDown--":
                TurnDownFaceTo("-");
                TurnDownFaceTo("-");
                break;
            case "TurnLeft++":
                TurnLeftFaceTo("+");
                TurnLeftFaceTo("+");
                break;
            case "TurnLeft--":
                TurnLeftFaceTo("-");
                TurnLeftFaceTo("-");
                break;
            case "TurnRight++":
                TurnRightFaceTo("+");
                TurnRightFaceTo("+");
                break;
            case "TurnRight--":
                TurnRightFaceTo("-");
                TurnRightFaceTo("-");
                break;
            case "TurnBack++":
                TurnBackFaceTo("+");
                TurnBackFaceTo("+");
                break;
            case "TurnBack--":
                TurnBackFaceTo("-");
                TurnBackFaceTo("-");
                break;

            case "TurnUpHorMiddleRight+":
                TurnUpHorMiddleTo("+");
                break;
            case "TurnUpHorMiddleLeft-":
                TurnUpHorMiddleTo("-");
                break;
            case "TurnUpVerMiddleBack+":
                TurnUpVerMiddleTo("+");
                break;
            case "TurnUpVerMiddleFront-":
                TurnUpVerMiddleTo("-");
                break;
            case "TurnFrontHorMiddleLeft+":
                TurnFrontHorMiddleTo("+");
                break;
            case "TurnFrontHorMiddleRight-":
                TurnFrontHorMiddleTo("-");
                break;

            case "TurnUpHorMiddleRight++":
                TurnUpHorMiddleTo("+");
                TurnUpHorMiddleTo("+");
                break;
            case "TurnUpHorMiddleLeft--":
                TurnUpHorMiddleTo("-");
                TurnUpHorMiddleTo("-");
                break;
            case "TurnUpVerMiddleBack++":
                TurnUpVerMiddleTo("+");
                TurnUpVerMiddleTo("+");
                break;
            case "TurnUpVerMiddleFront--":
                TurnUpVerMiddleTo("-");
                TurnUpVerMiddleTo("-");
                break;
            case "TurnFrontHorMiddleLeft++":
                TurnFrontHorMiddleTo("+");
                TurnFrontHorMiddleTo("+");
                break;
            case "TurnFrontHorMiddleRight--":
                TurnFrontHorMiddleTo("-");
                TurnFrontHorMiddleTo("-");
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
                await DisplayAlert("Error", "Turn not found", "OK");
                return;
        }
        
        GetCubeColorsFromArrays();
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
                sw.WriteLine(aCubeColors[nRow]);
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
                aCubeColors[nRow] = sr.ReadLine();
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

        GetCubeColorsFromArrays();
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
            aUpFace[nRow] = aCubeColors[1];
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            aFrontFace[nRow] = aCubeColors[2];
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            aRightFace[nRow] = aCubeColors[3];
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            aLeftFace[nRow] = aCubeColors[4];
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            aBackFace[nRow] = aCubeColors[5];
        }

        for (nRow = 1; nRow < 10; nRow++)
        {
            aDownFace[nRow] = aCubeColors[6];
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

/*
Numbering of cube surfaces.

    Outside view              Up               Inside view             Back
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
           Front                                       Down

 
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
|  Le7  |  Le4  |  Le1  |  Up1  |  Up2  |  Up3  |  Ri3  |  Ri6  |  Ri9  |  Do9  |  Do8  |  Do7  |
|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|
|       |       |       |       |       |       |       |       |       |       |       |       |
|  Le8  |  Le5  |  Le2  |  Up4  |  Up5  |  Up6  |  Ri2  |  Ri5  |  Ri8  |  Do6  |  Do5  |  Do4  |
|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|
|       |       |       |       |       |       |       |       |       |       |       |       |
|  Le9  |  Le6  |  Le3  |  Up7  |  Up8  |  Up9  |  Ri1  |  Ri4  |  Ri7  |  Do3  |  Do2  |  Do1  |
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


REM ** SOLCUBE ** 1981
----------------------
    Outside view              Up              Inside view              Back
                     ______ ______ ______                      ______ ______ ______
                   /      /      /      /|                   /|      !      !      |
                 /  36  /  37  /  38  /  |                 /  |  20  !  19  !  18  |
               /______/______/______/ 11 |               / 27 |______!______!______|
             /      /      /      / !    /       Left  / !    |      !      !      |
           /  39  /  40  /  41  /   !  / |           /   !  / |  23  !  22  !  21  |
         /______/______/______/  10 !/ 14|         /  28 !/   |______!______!______|
       /      /      /      / !    /!    /       /  !   /! 30 |      !      !      |
     /  42  /  43  /  44  /   !  /  !  / |     /    ! /  !  / |  26  !  25  !  24  |
   /______/______/______/  9  !/ 13 !/17 |    | 29  ! 31 !/   |______!______!______|
   |      !      !      |    /!    /!    /    |   / !   /!33 /      /      /      /
   |  0   !  1   !  2   |  /  !  /  !  /      | /   ! /  !  /  51  /  52 /  53  /
   |______!______!______|/ 12 !/ 16 !/        | 32  ! 34 !/______/______/______/
   |      !      !      |    /!    /          |   / !   /      /      /      /
   |  3   !  4   !  5   |  /  !  /            | /   ! / 48   / 49   /  50  /
   |______!______!______|/ 15 !/ Right        | 35  !______/______/______/
   |      !      !      |    /                |   /      /      /      /
   |  6   !  7   !  8   |  /                  | /  45  /  46  / 47   /
   |______!______!______|/                    |______/______/______/
           Front                                       Down



imgbtnTurnFrontFaceToRight  imgbtnTurnUpHorMiddleToRightFace  imgbtnTurnBackFaceToLeft
    imgbtnTurnLeftFaceToRight  imgbtnTurnUpVerMiddleToFrontFace  imgbtnTurnRightFaceToLeft

imgbtnTurnUpFaceToLeft                                  imgbtnTurnUpFaceToRight
imgbtnTurnFrontHorMiddleToRightFace                     imgbtnTurnFrontHorMiddleToLeftFace
imgbtnTurnDownFaceToRight                               imgbtnTurnDownFaceToLeft

imgbtnTurnLeftFaceToLeft  imgbtnTurnUpVerMiddleToBackFace  imgbtnTurnRightFaceToRight
    imgbtnTurnFrontFaceToLeft  imgbtnTurnUpHorMiddleToLeftFace  imgbtnTurnBackFaceToRight


REM ** SOLCUBE ** ARRAY 'D(53)' - CUBE COLORS - From BASIC-80 to C#. 
-------------------------------------------------------------------------------------
|     Front   |     Right   |     Back    |     Left    |     Up      |     Down    |
-------------------------------------------------------------------------------------
|  0  Front 1 |  9  Right 1 | 18  Back 1  | 27  Left 1  | 36  Up 1    | 45  Down 1  |
|  1  Front 2 | 10  Right 2 | 19  Back 2  | 28  Left 2  | 37  Up 2    | 46  Down 2  |
|  2  Front 3 | 11  Right 3 | 20  Back 3  | 29  Left 3  | 38  Up 3    | 47  Down 3  |
|  3  Front 4 | 12  Right 4 | 21  Back 4  | 30  Left 4  | 39  Up 4    | 48  Down 4  |
|  4  Front 5 | 13  Right 5 | 22  Back 5  | 31  Left 5  | 40  Up 5    | 49  Down 5  |
|  5  Front 6 | 14  Right 6 | 23  Back 6  | 32  Left 6  | 41  Up 6    | 50  Down 6  |
|  6  Front 7 | 15  Right 7 | 24  Back 7  | 33  Left 7  | 42  Up 7    | 51  Down 7  |
|  7  Front 8 | 16  Right 8 | 25  Back 8  | 34  Left 8  | 43  Up 8    | 52  Down 8  |
|  8  Front 9 | 17  Right 9 | 26  Back 9  | 35  Left 9  | 44  Up 9    | 53  Down 9  |
-------------------------------------------------------------------------------------


REM ** SOLCUBE ** ARRAY 'A$(217)' - INSTRUCTIONS - From Basic-80 to C#.

Boven -> Up         Achter -> Back      M1  Down to Front      M2  Up to Right
Rechts -> Right     Links -> Left      -M1  Up to Front       -M2  Down to Right
Voor -> Front       Onder -> Down
---------------------------------------------------------------------------------
|   0   V   Front+  |  54   L   Left+   | 108   R   Right+  | 163   -B  Up-     |
|   1   V   Front+  |  55   -B  Up-     | 109   V   Front+  | 164   O   Down+   |
|   2   A   Back+   |  56   -V  Front-  | 110   B   Up+     | 165   A   Back+   |
|   3   A   Back+   |  57   A   Back+   | 111   -O  Down-   | 166   -B  Up-     |
|   4   M2          |  58   L   Left+   | 112   L   Left+   | 167   L   Left+   |
|   5   M2          |  59   L   Left+	| 113   L   Left+   | 168   -B  Up-     |
|   6   B   Up+     |  60   V   Front+  | 114   B   Up+     | 169   O   Down+   |
|   7   M   Middle+ |  61   -A  Back-   | 115   B   Up+     | 170   -V  Front-  |
|   8   -O  Down-   |  62   -B  Up-     | 116   O   Down+   | 171   -B  Up-     |
|   9   -L  Left-   |  63   L   Left+   | 117   O   Down+   | 172   -A  Back-   |
|  10   B   Up+     |  64   L   Left+   | 118   R   Right+  | 173   B   Up+     |
|  11   R   Right+  |  65   R   Right+  | 119   -R  Right-  | 174   -O  Down-   |
|  12   -B  Up-     |  66   R   Right+  | 120   O   Down+   | 175   R   Right+  |
|  13   L   Left+   |  67   L   Left+   | 121   O   Down+   | 176   -B  Up-     |
|  14   B   Up+     |  68   L   Left+   | 122   B   Up+     | 177   -B  Up-     |
|  15   -R  Right-  |  69   O   Down+   | 123   B   Up+     | 178   L   Left+   |
|  16   -B  Up-     |  70   R   Right+  | 124   L   Left+   | 179   -B  Up-     |
|  17   B   Up+     |  71   R   Right+  | 125   L   Left+   | 180   O   Down+   |
|  18   R   Right+  |  72   L   Left+   | 126   O   Down+   | 181   -V  Front-  |
|  19   -B  Up-     |  73   L   Left+   | 127   -B  Up-     | 182   -R  Right-  |
|  20   -L  Left-   |  74   B   Up+     | 128   -V  Front-  | 183   -B  Up-     |
|  21   B   Up+     |  75   B   Up+     | 129   -O  Down-   | 184   -B  Up-     |
|  22   -R  Right-  |  76   R   Right+  | 130   -R  Right-  | 185   O   Down+   |
|  23   -B  Up-     |  77   R   Right+  | 131   O   Down+   | 186   O   Down+   |
|  24   L   Left+   |  78   L   Left+   | 132   R   Right+  | 187   L   Left+   |
|  25   V   Front+  |  79   L   Left+   | 133   O   Down+   | 188   -B  Up-     |
|  26   -B  Up-     |  80   O   Down+   | 134   V   Front+  | 189   V   Front+  |
|  27   -A  Back-   |  81   R   Right+  | 135   -O  Down-   | 190   -B  Up-     |
|  28   B   Up+     |  82   R   Right+  | 136   -V  Front-  | 191   O   Down+   |
|  29   -V  Front-  |  83   L   Left+   | 137   O   Down    | 192   -R  Right-  |
|  30   -B  Up-     |  84   L   Left+   | 138   L   Left+   | 193   -R  Right-  |
|  31   A   Back+   |  85   R   Right+  | 139   -O  Down-   | 194   -B  Up-     |
|  32   B   Up+     |  86   A   Back+   | 140   -L  Left-   | 195   O   Down+   |
|  33   B   Up+     |  87   B   Up+     | 141   -O  Down-   | 196   A   Back+   |
|  34   B   Up+     |  88   -A  Back-   | 142   -V  Front-  | 197   B   Up+     |
|  35   V   Front+  |  89   -B  Up-     | 143   O   Down+   | 198   V   Front+  |
|  36   B   Up+     |  90   R   Right+  | 144   V   Front+  | 199   -B  Up-     |
|  37   R   Right+  |  91   R   Right+  | 145   V   Front+  | 200   -O  Down-   |
|  38   -B  Up-     |  92   -V  Front-  | 146   O   Down+   | 201   -R  Right-  |
|  39   -R  Right-  |  93   -B  Up-     | 147   -V  Front-  | 202   -B  Up-     |
|  40   -V  Front-  |  94   V   Front+  | 148   -R  Right-  | 203   -B  Up-     |
|  41   L   Left+   |  95   B   Up+     | 149   -O  Down-   | 204   -L  Left-   |
|  42   L   Left+   |  96   R   Right+  | 150   R   Right+  | 205   B   Up+     |
|  43   B   Up+     |  97   -R  Right-  | 151   -R  Right-  | 206   -O  Down-   |
|  44   -V  Front-  |  98   O   Down+   | 152   O   Down+   | 207   A   Back+   |
|  45   A   Back+   |  99   R   Right+  | 153   R   Right+  | 208   -R  Right-  |
|  46   L   Left+   | 100   V   Front+  | 154   O   Down+   | 209   -B  Up-     |
|  47   L   Left+   | 101   O   Down+   | 155   O   Down+   | 210   O   Down+   |
|  48   V   Front+  | 102   -V  Front-  | 156   -R  Right-  | 211   A   Back+   |
|  49   -A  Back-   | 103   V   Front+  | 157   -O  Down-   | 212   B   Up+     |
|  50   B   Up+     | 104   -O  Down-   | 158   R   Right+  | 213   B   Up+     |
|  51   L   Left+   | 105   -V  Front-  | 159   -B  Up-     | 214   M   Middle+ |
|  52   L   Left+   | 106   -R  Right-  | 160   -M  Middle- | 215   M   Middle+ |
|  53   L   Left+   | 107   -O  Down-   | 161   O   Down+   | 216   -O  Down-   |
|                   |                   | 162   -R  Right-  | 217   -O  Down-   |
---------------------------------------------------------------------------------


                        _________________________
                        |       |       |       |
                        | Y0=40 | Y1=41 | Y2=42 |
                        |_______|_______|_______|
                        |       |       |       |
                        | Y7=47 | Yellow| Y3=43 |
                        |_______|_______|_______|
                        |       |       |       |
                        | Y6=46 | Y5=45 | Y4=44 |
________________________|_______|_______|_______|________________________________________________
|       |       |       |       |       |       |       |       |       |       |       |       |
| B0=8  | B1=9  | B2=10 | R0=16 | R1=17 | R2=18 | G0=24 | G1=25 | G2=26 | O0=32 | O1=33 | O2=34 |
|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|
|       |       |       |       |       |       |       |       |       |       |       |       |
| B7=15 | Blue  | B3=11 | R7=23 | Red   | R3=19 | G7=31 | Green | G3=27 | O7=39 | Orange| O3=35 |
|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|
|       |       |       |       |       |       |       |       |       |       |       |       |
| B6=14 | B5=13 | B4=12 | R6=22 | R5=21 | R4=20 | G6=30 | G5=29 | G4=28 | O6=38 | O5=37 | O4=36 |
|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|_______|
                        |       |       |       |
                        | W0=0  | W1=1  | W2=2  |
                        |_______|_______|_______|
                        |       |       |       |
                        | W7=7  | White | W3=3  |
                        |_______|_______|_______|
                        |       |       |       |
                        | W6=6  | W5=5  | W4=4  |
                        |_______|_______|_______|

 */
