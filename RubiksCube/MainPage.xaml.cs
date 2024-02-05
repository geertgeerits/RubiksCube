// Program .....: RubiksCube.sln
// Author ......: Geert Geerits - E-mail: geertgeerits@gmail.com
// Copyright ...: (C) 1981-2024
// Version .....: 2.0.11
// Date ........: 2024-02-05 (YYYY-MM-DD)
// Language ....: Microsoft Visual Studio 2022: .NET MAUI 8 - C# 12.0
// Description .: Solving the Rubik's Cube
// Note ........: This program is based on the program 'SolCube' I wrote in 1981 in MS Basic-80 for a Commodore PET 2001
//                The solution for solving the cube is based on a book by Don Taylor, Mastering Rubik's Cube, Dutch version 1981
// Dependencies : None
// Thanks to ...: Gerald Versluis

using Microsoft.Maui.Controls.Shapes;
using System.Diagnostics;

namespace RubiksCube;

public partial class MainPage : ContentPage
{
    // Local variables
    private bool bColorDrop;
    private bool bSolvingCube;
    private bool bSolved;
    private bool bArrowButtonPressed;

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

        // Get the saved settings
        Globals.cTheme = Preferences.Default.Get("SettingTheme", "System");
        Globals.cLanguage = Preferences.Default.Get("SettingLanguage", "");
        Globals.cLanguageSpeech = Preferences.Default.Get("SettingLanguageSpeech", "");
        Globals.bExplainText = Preferences.Default.Get("SettingExplainText", false);
        Globals.bExplainSpeech = Preferences.Default.Get("SettingExplainSpeech", false);
        Globals.aFaceColors[1] = Preferences.Default.Get("SettingCubeColor1", "#FF0000");   // Front face: Red
        Globals.aFaceColors[2] = Preferences.Default.Get("SettingCubeColor2", "#0000FF");   // Right face: Blue
        Globals.aFaceColors[3] = Preferences.Default.Get("SettingCubeColor3", "#FF8000");   // Back face: Orange
        Globals.aFaceColors[4] = Preferences.Default.Get("SettingCubeColor4", "#008000");   // Left face: Green
        Globals.aFaceColors[5] = Preferences.Default.Get("SettingCubeColor5", "#FFFFFF");   // Up face: White
        Globals.aFaceColors[6] = Preferences.Default.Get("SettingCubeColor6", "#FFFF00");   // Down face: Yellow
        Globals.bLicense = Preferences.Default.Get("SettingLicense", false);

        // Set the theme
        Globals.SetTheme();

        // Get and set the system OS user language
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

        // Initialize text to speech and get and set the speech language
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

        ClassSpeech.InitializeTextToSpeech(cCultureName);

        // Reset the colors of the cube
        ClassColorsCube.ResetCube();
        GetCubeColorsFromArrays();
    }

    // TitleView buttons clicked events
    private async void OnPageAboutClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageAbout());
    }

    private async void OnPageSettingsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageSettings());
    }

    // Select a color by tapping on a cube and put it in a tempory polygon
    private void OnGetColorTapped(object sender, TappedEventArgs args)
    {
        Polygon polygon = sender as Polygon;
        plgCubeColorSelect.Fill = polygon.Fill;
    }

    // Set the color by tapping on a cube and fill the cube with the color from the tempory polygon
    private void OnSetColorTapped(object sender, TappedEventArgs args)
    {
        if (plgCubeColorSelect.Fill != null && bColorDrop)
        {
            Polygon polygon = sender as Polygon;
            polygon.Fill = plgCubeColorSelect.Fill;

            SetCubeColorsInArrays();
        }
    }

    // Drag and drop colors on the cube
    private void OnButtonSetColorsCubeClicked(object sender, EventArgs e)
    {
        bColorDrop = !bColorDrop;

        if (bColorDrop)
        {
            btnSolveCube.IsEnabled = false;
            grdCubeColorSelect.BackgroundColor = Color.FromArgb("#969696");
            plgCubeColorSelect.Fill = null;
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
            Globals.lCubeTurns.Clear();

            
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

            IsEnabledArrows(false);
            IsVisibleCubeColors(false);
            grdCubeColorSelect.BackgroundColor = Color.FromArgb("#00000000");

            btnSolveCube.IsEnabled = true;
        }
    }

    // Solve the cube
    private async void OnBtnSolveCubeClicked(object sender, EventArgs e)
    {
        // Check the number of colors of the cube
        if (!CheckNumberColorsCube())
        {
            return;
        }

        // Check if the cube is already solved
        if (ClassColorsCube.CheckIfSolved())
        {
            Globals.lCubeTurns.Clear();
            return;
        }

        // Settings
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

        // Start the activity indicator
        activityIndicator.IsRunning = true;
        await Task.Delay(200);

        // Create and start a stopwatch instance
        Stopwatch stopwatch = Stopwatch.StartNew();

        // Solve the cube from the turns the user has made in reverse order
        if (Globals.lCubeTurns.Count > 0)
        {
            Globals.lCubeTurns.Reverse();
            bSolved = true;
        }
        // Solve the cube from the turns the program has made
        else
        {
            // Save the start colors of the cube to array aStartPieces[]
            Array.Copy(Globals.aPieces, Globals.aStartPieces, 54);

            // Test the turns of the cube
            //bSolved = await ClassTestCubeTurns.TestCubeTurnsAsync();

            // Solve the cube from Basic-80 to C#
            bSolved = await ClassCubePositions.SolveCubeFromMultiplePositionsAsync();

            // Solve the cube in C#
            //bSolved = await ClassSolveCube.SolveTheCubeAsync();

            // Restore the start colors of the cube from array aStartPieces[]
            Array.Copy(Globals.aStartPieces, Globals.aPieces, 54);
        }

        // Stop the activity indicator
        activityIndicator.IsRunning = false;

        // Stop the stopwatch and get the elapsed time
        stopwatch.Stop();
        var elapsedMs = stopwatch.ElapsedMilliseconds;

        if (bSolved)
        {
            // Display the number of turns and the elapsed time in milliseconds
            int nTurnsBeforeClean = Globals.lCubeTurns.Count;

            // Clean the list with the cube turns by replacing the double 1/4 turns with a half turn
            ClassCubePositions.CleanDoublesListCubeTurns();
            
            int nTurnsAfterClean = Globals.lCubeTurns.Count;
            await DisplayAlert("", $"{CubeLang.ResultTurns_Text} {nTurnsBeforeClean} -> {nTurnsAfterClean}\n{CubeLang.ResultTime_Text} {elapsedMs}", CubeLang.ButtonClose_Text);

            // Make the turns of the cube
            int nTurns = -1;

            foreach (string cItem in Globals.lCubeTurns)
            {
                nTurns++;
                lblNumberTurns.Text = $"{nTurns}/{nTurnsAfterClean}";
                await MakeTurnAsync(cItem);
            }

            lblNumberTurns.Text = $"{nTurns + 1}/{nTurnsAfterClean}";

            if (ClassColorsCube.CheckIfSolved())
            {
                await DisplayAlert("", CubeLang.MessageCubeIsSolved_Text, CubeLang.ButtonClose_Text);
            }
            else
            {
                await DisplayAlert("", CubeLang.MessageCubeBackInPreviousState_Text, CubeLang.ButtonClose_Text);
            }

            lblNumberTurns.Text = "";
        }

        if (!bSolved)
        {
            await DisplayAlert("", CubeLang.MessageCubeCannotBeSolved_Text, CubeLang.ButtonClose_Text);
        }

        // Clear the list with the cube turns
        Globals.lCubeTurns.Clear();

        // Settings
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

    // Check the number of colors of the cube
    private bool CheckNumberColorsCube()
    {
        SetCubeColorsInArrays();
        return ClassColorsCube.CheckNumberColors();
    }

    // Turn the faces of the cube
    // Turn the front face clockwise (to right +)
    private void OnTurnFrontFaceToRightClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        // Reverse the turn when solving the cube in reverse order
        if (!bSolvingCube && !bColorDrop)
        {
            Globals.lCubeTurns.Add(Globals.turnFrontCCW);
        }

        ExplainTurnCube(CubeLang.TurnFrontFaceToRight_Text);
        ClassCubeTurns.TurnFrontFaceTo("CW");
        GetCubeColorsFromArrays();
    }

    // Turn the upper horizontal middle to the right face (+)
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

        // Reverse the turn when solving the cube in reverse order
        if (!bSolvingCube && !bColorDrop)
        {
            Globals.lCubeTurns.Add(Globals.turnUpHorMiddleLeft);
        }

        ExplainTurnCube(CubeLang.TurnUpMiddleToRightFace_Text);
        ClassCubeTurns.TurnUpHorMiddleTo("CW");
        GetCubeColorsFromArrays();
    }

    // Turn the back face counter clockwise (to left -)
    private void OnTurnBackFaceToLeftClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        // Reverse the turn when solving the cube in reverse order
        if (!bSolvingCube && !bColorDrop)
        {
            Globals.lCubeTurns.Add(Globals.turnBackCW);
        }

        ExplainTurnCube(CubeLang.TurnBackFaceToLeft_Text);
        ClassCubeTurns.TurnBackFaceTo("CCW");
        GetCubeColorsFromArrays();
    }

    // Turn the left face clockwise (to right +)
    private void OnTurnLeftFaceToRightClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        // Reverse the turn when solving the cube in reverse order
        if (!bSolvingCube && !bColorDrop)
        {
            Globals.lCubeTurns.Add(Globals.turnLeftCCW);
        }

        ExplainTurnCube(CubeLang.TurnLeftFaceToRight_Text);
        ClassCubeTurns.TurnLeftFaceTo("CW");
        GetCubeColorsFromArrays();
    }

    // Turn the upper vertical middle to the front face (-)
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

        // Reverse the turn when solving the cube in reverse order
        if (!bSolvingCube && !bColorDrop)
        {
            Globals.lCubeTurns.Add(Globals.turnUpVerMiddleBack);
        }

        ExplainTurnCube(CubeLang.TurnUpMiddleToFrontFace_Text);
        ClassCubeTurns.TurnUpVerMiddleTo("CCW");
        GetCubeColorsFromArrays();
    }

    // Turn the right face counter clockwise (to left -)
    private void OnTurnRightFaceToLeftClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        // Reverse the turn when solving the cube in reverse order
        if (!bSolvingCube && !bColorDrop)
        {
            Globals.lCubeTurns.Add(Globals.turnRightCW);
        }

        ExplainTurnCube(CubeLang.TurnRightFaceToLeft_Text);
        ClassCubeTurns.TurnRightFaceTo("CCW");
        GetCubeColorsFromArrays();
    }

    // Turn the upper face counter clockwise (to left -)
    private void OnTurnUpFaceToLeftClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        // Reverse the turn when solving the cube in reverse order
        if (!bSolvingCube && !bColorDrop)
        {
            Globals.lCubeTurns.Add(Globals.turnUpCW);
        }

        ExplainTurnCube(CubeLang.TurnUpFaceToLeft_Text);
        ClassCubeTurns.TurnUpFaceTo("CCW");
        GetCubeColorsFromArrays();
    }

    // Turn the front horizontal middle to the right face (-)
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

        // Reverse the turn when solving the cube in reverse order
        if (!bSolvingCube && !bColorDrop)
        {
            Globals.lCubeTurns.Add(Globals.turnFrontHorMiddleLeft);
        }

        ExplainTurnCube(CubeLang.TurnFrontMiddleToRightFace_Text);
        ClassCubeTurns.TurnFrontHorMiddleTo("CCW");
        GetCubeColorsFromArrays();
    }

    // Turn the down face clockwise (to right +)
    private void OnTurnDownFaceToRightClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        // Reverse the turn when solving the cube in reverse order
        if (!bSolvingCube && !bColorDrop)
        {
            Globals.lCubeTurns.Add(Globals.turnDownCCW);
        }

        ExplainTurnCube(CubeLang.TurnDownFaceToRight_Text);
        ClassCubeTurns.TurnDownFaceTo("CW");
        GetCubeColorsFromArrays();
    }

    // Turn the upper face clockwise (to right +)
    private void OnTurnUpFaceToRightClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        // Reverse the turn when solving the cube in reverse order
        if (!bSolvingCube && !bColorDrop)
        {
            Globals.lCubeTurns.Add(Globals.turnUpCCW);
        }

        ExplainTurnCube(CubeLang.TurnUpFaceToRight_Text);
        ClassCubeTurns.TurnUpFaceTo("CW");
        GetCubeColorsFromArrays();
    }

    // Turn the front horizontal middle to the left face (+)
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

        // Reverse the turn when solving the cube in reverse order
        if (!bSolvingCube && !bColorDrop)
        {
            Globals.lCubeTurns.Add(Globals.turnFrontHorMiddleRight);
        }

        ExplainTurnCube(CubeLang.TurnRightMiddleToFrontFace_Text);
        ClassCubeTurns.TurnFrontHorMiddleTo("CW");
        GetCubeColorsFromArrays();
    }

    // Turn the down face counter clockwise (to left -)
    private void OnTurnDownFaceToLeftClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        // Reverse the turn when solving the cube in reverse order
        if (!bSolvingCube && !bColorDrop)
        {
            Globals.lCubeTurns.Add(Globals.turnDownCW);
        }

        ExplainTurnCube(CubeLang.TurnDownFaceToLeft_Text);
        ClassCubeTurns.TurnDownFaceTo("CCW");
        GetCubeColorsFromArrays();
    }

    // Turn the left face counter clockwise (to left -)
    private void OnTurnLeftFaceToLeftClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        // Reverse the turn when solving the cube in reverse order
        if (!bSolvingCube && !bColorDrop)
        {
            Globals.lCubeTurns.Add(Globals.turnLeftCW);
        }

        ExplainTurnCube(CubeLang.TurnLeftFaceToLeft_Text);
        ClassCubeTurns.TurnLeftFaceTo("CCW");
        GetCubeColorsFromArrays();
    }

    // Turn the upper vertical middle to the back face (+)
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

        // Reverse the turn when solving the cube in reverse order
        if (!bSolvingCube && !bColorDrop)
        {
            Globals.lCubeTurns.Add(Globals.turnUpVerMiddleFront);
        }

        ExplainTurnCube(CubeLang.TurnFrontMiddleToUpFace_Text);
        ClassCubeTurns.TurnUpVerMiddleTo("CW");
        GetCubeColorsFromArrays();
    }

    // Turn the right face clockwise (to right +)
    private void OnTurnRightFaceToRightClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        // Reverse the turn when solving the cube in reverse order
        if (!bSolvingCube && !bColorDrop)
        {
            Globals.lCubeTurns.Add(Globals.turnRightCCW);
        }

        ExplainTurnCube(CubeLang.TurnRightFaceToRight_Text);
        ClassCubeTurns.TurnRightFaceTo("CW");
        GetCubeColorsFromArrays();
    }

    // Turn the front face counter clockwise (to left -)
    private void OnTurnFrontFaceToLeftClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        // Reverse the turn when solving the cube in reverse order
        if (!bSolvingCube && !bColorDrop)
        {
            Globals.lCubeTurns.Add(Globals.turnFrontCW);
        }

        ExplainTurnCube(CubeLang.TurnFrontFaceToLeft_Text);
        ClassCubeTurns.TurnFrontFaceTo("CCW");
        GetCubeColorsFromArrays();
    }

    // Turn the upper horizontal middle to the left face (-)
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

        // Reverse the turn when solving the cube in reverse order
        if (!bSolvingCube && !bColorDrop)
        {
            Globals.lCubeTurns.Add(Globals.turnUpHorMiddleRight);
        }

        ExplainTurnCube(CubeLang.TurnRightMiddleToUpFace_Text);
        ClassCubeTurns.TurnUpHorMiddleTo("CCW");
        GetCubeColorsFromArrays();
    }

    // Turn the back face clockwise (to right +)
    private void OnTurnBackFaceToRightClicked(object sender, EventArgs e)
    {
        if (bSolvingCube)
        {
            bArrowButtonPressed = true;
            return;
        }

        // Reverse the turn when solving the cube in reverse order
        if (!bSolvingCube && !bColorDrop)
        {
            Globals.lCubeTurns.Add(Globals.turnBackCCW);
        }

        ExplainTurnCube(CubeLang.TurnBackFaceToRight_Text);
        ClassCubeTurns.TurnBackFaceTo("CW");
        GetCubeColorsFromArrays();
    }

    // Turn the entire cube a quarter turn
    // Rotate the entire cube so that the front goes to the left face
    private void TurnCubeFrontFaceToLeftFace()
    {
        if (!bSolvingCube)
        {
            ExplainTurnCube(CubeLang.TurnCubeFrontFaceToLeftFace_Text);
        }
            
        ClassCubeTurns.TurnUpFaceTo("CW");
        ClassCubeTurns.TurnFrontHorMiddleTo("CW");
        ClassCubeTurns.TurnDownFaceTo("CCW");
        GetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the front goes to the right face
    private void TurnCubeFrontFaceToRightFace()
    {
        if (!bSolvingCube)
        {
            ExplainTurnCube(CubeLang.TurnCubeFrontFaceToRightFace_Text);
        }

        ClassCubeTurns.TurnUpFaceTo("CCW");
        ClassCubeTurns.TurnFrontHorMiddleTo("CCW");
        ClassCubeTurns.TurnDownFaceTo("CW");
        GetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the front goes to the upper face
    private void TurnCubeFrontFaceToUpFace()
    {
        if (!bSolvingCube)
        {
            ExplainTurnCube(CubeLang.TurnCubeFrontFaceToUpFace_Text);
        }

        ClassCubeTurns.TurnRightFaceTo("CW");
        ClassCubeTurns.TurnUpVerMiddleTo("CW");
        ClassCubeTurns.TurnLeftFaceTo("CCW");
        GetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the front goes to the down face
    private void TurnCubeFrontFaceToDownFace()
    {
        if (!bSolvingCube)
        {
            ExplainTurnCube(CubeLang.TurnCubeFrontFaceToDownFace_Text);
        }

        ClassCubeTurns.TurnRightFaceTo("CCW");
        ClassCubeTurns.TurnUpVerMiddleTo("CCW");
        ClassCubeTurns.TurnLeftFaceTo("CW");
        GetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the upper face goes to the right face
    private void TurnCubeUpFaceToRightFace()
    {
        if (!bSolvingCube)
        {
            ExplainTurnCube(CubeLang.TurnCubeUpFaceToRightFace_Text);
        }

        ClassCubeTurns.TurnFrontFaceTo("CW");
        ClassCubeTurns.TurnUpHorMiddleTo("CW");
        ClassCubeTurns.TurnBackFaceTo("CCW");
        GetCubeColorsFromArrays();
    }

    // Rotate the entire cube so that the upper face goes to the left face
    private void TurnCubeUpFaceToLeftFace()
    {
        if (!bSolvingCube)
        {
            ExplainTurnCube(CubeLang.TurnCubeUpFaceToLeftFace_Text);
        }

        ClassCubeTurns.TurnFrontFaceTo("CCW");
        ClassCubeTurns.TurnUpHorMiddleTo("CCW");
        ClassCubeTurns.TurnBackFaceTo("CW");
        GetCubeColorsFromArrays();
    }

    // Explain the turn of the cube called from OnTurn....Clicked and Turn.... methods
    private async void ExplainTurnCube(string cTurnCubeText)
    {
        // Convert text to speech
        ExplainTurnCubeSpeech(cTurnCubeText);

        if (Globals.bExplainText)
        {
            await DisplayAlert("", cTurnCubeText, CubeLang.ButtonClose_Text);
        }
    }

    // Make and explain the turn of the cube called from the main task SolveTheCubeAsync()
    private async Task MakeTurnAsync(string cTurnFaceAndDirection)
    {
        // Enable the arrow button and set the background color to Active
        await SetImageButtonArrowIsEnabledAsync(cTurnFaceAndDirection, true);

        // Show the text
        string cTurnCubeText = await SetExplainTextAsync(cTurnFaceAndDirection);
        lblExplainTurnCube1.Text = cTurnCubeText;
        lblExplainTurnCube2.Text = cTurnCubeText;

        // Convert text to speech
        ExplainTurnCubeSpeech(cTurnCubeText);

        // Start a program loop and wait for the arrow button to be pressed
        while (true)
        {
            // Wait for 300 milliseconds on the button click event handler

            await Task.Delay(300);

            // Check if the button has been clicked and stop the loop if clicked
            if (bArrowButtonPressed)
            {
                break;
            }
        }

        // Restore settings
        bArrowButtonPressed = false;
        await SetImageButtonArrowIsEnabledAsync(cTurnFaceAndDirection, false);

        // Turn the faces of the cube
        await ClassCubeTurns.TurnFaceCubeAsync(cTurnFaceAndDirection);
        
        // Set the cube colors from the arrays in the polygons
        GetCubeColorsFromArrays();
    }

    // Enalbe or disable the arrow imagebuttons
    private async Task SetImageButtonArrowIsEnabledAsync(string cTurnFaceAndDirection, bool bIsEnabled)
    {
        switch (cTurnFaceAndDirection)
        {
            case Globals.turnFrontCW:
            case Globals.turnFront2:
                imgbtnTurnFrontFaceToRight.IsEnabled = bIsEnabled;
                break;
            case Globals.turnFrontCCW:
                imgbtnTurnFrontFaceToLeft.IsEnabled = bIsEnabled;
                break;
            case Globals.turnRightCW:
            case Globals.turnRight2:
                imgbtnTurnRightFaceToRight.IsEnabled = bIsEnabled;
                break;
            case Globals.turnRightCCW:
                imgbtnTurnRightFaceToLeft.IsEnabled = bIsEnabled;
                break;
            case Globals.turnBackCW:
            case Globals.turnBack2:
                imgbtnTurnBackFaceToRight.IsEnabled = bIsEnabled;
                break;
            case Globals.turnBackCCW:
                imgbtnTurnBackFaceToLeft.IsEnabled = bIsEnabled;
                break;
            case Globals.turnLeftCW:
            case Globals.turnLeft2:
                imgbtnTurnLeftFaceToRight.IsEnabled = bIsEnabled;
                break;
            case Globals.turnLeftCCW:
                imgbtnTurnLeftFaceToLeft.IsEnabled = bIsEnabled;
                break;
            case Globals.turnUpCW:
            case Globals.turnUp2:
                imgbtnTurnUpFaceToRight.IsEnabled = bIsEnabled;
                break;
            case Globals.turnUpCCW:
                imgbtnTurnUpFaceToLeft.IsEnabled = bIsEnabled;
                break;
            case Globals.turnDownCW:
            case Globals.turnDown2:
                imgbtnTurnDownFaceToRight.IsEnabled = bIsEnabled;
                break;
            case Globals.turnDownCCW:
                imgbtnTurnDownFaceToLeft.IsEnabled = bIsEnabled;
                break;

            case Globals.turnUpHorMiddleRight:
            case Globals.turnUpHorMiddle2:
                imgbtnTurnUpHorMiddleToRightFace.IsEnabled = bIsEnabled;
                break;
            case Globals.turnUpHorMiddleLeft:
                imgbtnTurnUpHorMiddleToLeftFace.IsEnabled = bIsEnabled;
                break;
            case Globals.turnUpVerMiddleBack:
            case Globals.turnUpVerMiddle2:
                imgbtnTurnUpVerMiddleToBackFace.IsEnabled = bIsEnabled;
                break;
            case Globals.turnUpVerMiddleFront:
                imgbtnTurnUpVerMiddleToFrontFace.IsEnabled = bIsEnabled;
                break;
            case Globals.turnFrontHorMiddleLeft:
            case Globals.turnFrontHorMiddle2:
                imgbtnTurnFrontHorMiddleToLeftFace.IsEnabled = bIsEnabled;
                break;
            case Globals.turnFrontHorMiddleRight:
                imgbtnTurnFrontHorMiddleToRightFace.IsEnabled = bIsEnabled;
                break;

            case Globals.turnCubeFrontToRight:
                imgbtnTurnFrontHorMiddleToRightFace.IsEnabled = bIsEnabled;
                break;
            case Globals.turnCubeFrontToLeft:
            case Globals.turnCubeFrontToLeft2:
                imgbtnTurnFrontHorMiddleToLeftFace.IsEnabled = bIsEnabled;
                break;
            case Globals.turnCubeFrontToUp:
            case Globals.turnCubeFrontToUp2:
                imgbtnTurnUpVerMiddleToBackFace.IsEnabled = bIsEnabled;
                break;
            case Globals.turnCubeFrontToDown:
                imgbtnTurnUpVerMiddleToFrontFace.IsEnabled = bIsEnabled;
                break;
            case Globals.turnCubeUpToRight:
            case Globals.turnCubeUpToRight2:
                imgbtnTurnUpHorMiddleToRightFace.IsEnabled = bIsEnabled;
                break;
            case Globals.turnCubeUpToLeft:
                imgbtnTurnUpHorMiddleToLeftFace.IsEnabled = bIsEnabled;
                break;

            default:
                await DisplayAlert(CubeLang.ErrorTitle_Text, "Turn not found", CubeLang.ButtonClose_Text);
                break;
        }
    }

    // Set the explain text depending on the direction of rotation of the cube face
    private async Task<string> SetExplainTextAsync(string cTurnFaceAndDirection)
    {
        string cTurnCubeText = "";

        switch (cTurnFaceAndDirection)
        {
            case Globals.turnFrontCW:
                cTurnCubeText = CubeLang.TurnFrontFaceToRight_Text;
                break;
            case Globals.turnFrontCCW:
                cTurnCubeText = CubeLang.TurnFrontFaceToLeft_Text;
                break;
            case Globals.turnFront2:
                cTurnCubeText = CubeLang.TurnFrontFaceHalfTurn_Text;
                break;
            case Globals.turnRightCW:
                cTurnCubeText = CubeLang.TurnRightFaceToRight_Text;
                break;
            case Globals.turnRightCCW:
                cTurnCubeText = CubeLang.TurnRightFaceToLeft_Text;
                break;
            case Globals.turnRight2:
                cTurnCubeText = CubeLang.TurnRightFaceHalfTurn_Text;
                break;
            case Globals.turnBackCW:
                cTurnCubeText = CubeLang.TurnBackFaceToRight_Text;
                break;
            case Globals.turnBackCCW:
                cTurnCubeText = CubeLang.TurnBackFaceToLeft_Text;
                break;
            case Globals.turnBack2:
                cTurnCubeText = CubeLang.TurnBackFaceHalfTurn_Text;
                break;
            case Globals.turnLeftCW:
                cTurnCubeText = CubeLang.TurnLeftFaceToRight_Text;
                break;
            case Globals.turnLeftCCW:
                cTurnCubeText = CubeLang.TurnLeftFaceToLeft_Text;
                break;
            case Globals.turnLeft2:
                cTurnCubeText = CubeLang.TurnLeftFaceHalfTurn_Text;
                break;
            case Globals.turnUpCW:
                cTurnCubeText = CubeLang.TurnUpFaceToRight_Text;
                break;
            case Globals.turnUpCCW:
                cTurnCubeText = CubeLang.TurnUpFaceToLeft_Text;
                break;
            case Globals.turnUp2:
                cTurnCubeText = CubeLang.TurnUpFaceHalfTurn_Text;
                break;
            case Globals.turnDownCW:
                cTurnCubeText = CubeLang.TurnDownFaceToRight_Text;
                break;
            case Globals.turnDownCCW:
                cTurnCubeText = CubeLang.TurnDownFaceToLeft_Text;
                break;
            case Globals.turnDown2:
                cTurnCubeText = CubeLang.TurnDownFaceHalfTurn_Text;
                break;

            case Globals.turnUpHorMiddleRight:
                cTurnCubeText = CubeLang.TurnUpMiddleToRightFace_Text ;
                break;
            case Globals.turnUpHorMiddleLeft:
                cTurnCubeText = CubeLang.TurnRightMiddleToUpFace_Text;
                break;
            case Globals.turnUpHorMiddle2:
                cTurnCubeText = CubeLang.TurnMiddleLayerHalfTurn_Text;
                break;

            case Globals.turnUpVerMiddleBack:
                cTurnCubeText = CubeLang.TurnFrontMiddleToUpFace_Text;
                break;
            case Globals.turnUpVerMiddleFront:
                cTurnCubeText = CubeLang.TurnUpMiddleToFrontFace_Text;
                break;
            case Globals.turnUpVerMiddle2:
                cTurnCubeText = CubeLang.TurnMiddleLayerHalfTurn_Text;
                break;

            case Globals.turnFrontHorMiddleLeft:
                cTurnCubeText = CubeLang.TurnRightMiddleToFrontFace_Text;
                break;
            case Globals.turnFrontHorMiddleRight:
                cTurnCubeText = CubeLang.TurnFrontMiddleToRightFace_Text;
                break;
            case Globals.turnFrontHorMiddle2:
                cTurnCubeText = CubeLang.TurnMiddleLayerHalfTurn_Text;
                break;

            case Globals.turnCubeFrontToRight:
                cTurnCubeText = CubeLang.TurnCubeFrontFaceToRightFace_Text;
                break;
            case Globals.turnCubeFrontToLeft:
                cTurnCubeText = CubeLang.TurnCubeFrontFaceToLeftFace_Text;
                break;
            case Globals.turnCubeFrontToLeft2:
                cTurnCubeText = CubeLang.TurnCubeFrontFaceToBackFaceUpStays_Text;
                break;
            case Globals.turnCubeFrontToUp:
                cTurnCubeText = CubeLang.TurnCubeFrontFaceToUpFace_Text;
                break;
            case Globals.turnCubeFrontToUp2:
                cTurnCubeText = CubeLang.TurnCubeFrontFaceToBackFaceRightStays_Text;
                break;
            case Globals.turnCubeFrontToDown:
                cTurnCubeText = CubeLang.TurnCubeFrontFaceToDownFace_Text;
                break;
            case Globals.turnCubeUpToRight:
                cTurnCubeText = CubeLang.TurnCubeUpFaceToRightFace_Text;
                break;
            case Globals.turnCubeUpToRight2:
                cTurnCubeText = CubeLang.TurnCubeUpFaceToDownFaceFrontStays_Text;
                break;
            case Globals.turnCubeUpToLeft:
                cTurnCubeText = CubeLang.TurnCubeUpFaceToLeftFace_Text;
                break;
            
            default:
                await DisplayAlert(CubeLang.ErrorTitle_Text, "Turn not found", CubeLang.ButtonClose_Text);
                break;
        }

        return cTurnCubeText;
    }

    // Explain the turn of the cube with speech
    private static void ExplainTurnCubeSpeech(string cTurnCubeText)
    {
        if (Globals.bExplainSpeech)
        {
            if (cTurnCubeText.Substring(cTurnCubeText.Length - 2, 2) == ").")
            {
                cTurnCubeText = cTurnCubeText.Substring(0, cTurnCubeText.Length - 5);
            }

            _ = ClassSpeech.ConvertTextToSpeechAsync(cTurnCubeText);
        }
    }

    // On clicked event: Save the cube
    private void OnButtonSaveCubeClicked(object sender, EventArgs e)
    {
        SetCubeColorsInArrays();
        _ = ClassSaveRestoreCube.CubeDataSave();
    }

    // On clicked event: Open, restore the cube
    private void OnButtonOpenCubeClicked(object sender, EventArgs e)
    {
        _ = ClassSaveRestoreCube.CubeDataOpen();

        GetCubeColorsFromArrays();
        Globals.lCubeTurns.Clear();
    }

    // On clicked event: Reset the colors of the cube or restart the app
    private void OnBtnResetClicked(object sender, EventArgs e)
    {
        Globals.lCubeTurns.Clear();

        if (bSolvingCube)
        {
            // Restart the application to get out of the loop in the Task MakeTurnAsync()
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
        else
        {
            ClassColorsCube.ResetCube();
            GetCubeColorsFromArrays();

            if (!bColorDrop)
            {
                IsEnabledArrows(true);
            }
        }
    }

    // Store the cube colors from the polygons in the arrays
    private void SetCubeColorsInArrays()
    {
        for (int i = 1; i < 7; i++)
        {
            Polygon polygon = this.FindByName<Polygon>($"plgCubeColor{i}");
            Globals.aFaceColors[i] = GetHexColorPolygon(polygon);
        }

        for (int i = 0; i < 54; i++)
        {
            Polygon polygon = this.FindByName<Polygon>($"plgPiece{i}");
            Globals.aPieces[i] = GetHexColorPolygon(polygon);
        }
    }

    // Set the cube colors from the arrays in the polygons
    private void GetCubeColorsFromArrays()
    {
        for (int i = 1; i < 7; i++)
        {
            Polygon polygon = this.FindByName<Polygon>($"plgCubeColor{i}");
            polygon.Fill = Color.FromArgb(Globals.aFaceColors[i]);
        }

        for (int i = 0; i < 54; i++)
        {
            Polygon polygon = this.FindByName<Polygon>($"plgPiece{i}");
            polygon.Fill = Color.FromArgb(Globals.aPieces[i]);
        }
    }

    // Get the hex color code from the polygon fill property
    private static string GetHexColorPolygon(Polygon polygon)
    {
        SolidColorBrush brush = (SolidColorBrush)polygon.Fill;
        Color color = brush.Color;

        color = Color.FromRgb(color.Red, color.Green, color.Blue);
        return color.ToHex();
    }

    // Set the cube colors for drag and drop to visible or invisible
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

    // Set the arrow buttons tooltip
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

    // Enable or Disable the arrows
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

    // Show license using the Loaded event of the MainPage.xaml
    private async void OnPageLoad(object sender, EventArgs e)
    {
        // Show license
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
                //Thread.CurrentThread.Abort();  // Not allowed in iOS
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

    // Set language using the Appearing event of the MainPage.xaml
    private void OnPageAppearing(object sender, EventArgs e)
    {
        if (Globals.bLanguageChanged)
        {
            SetTextLanguage();
            Globals.bLanguageChanged = false;
        }
    }

    // Put text in the chosen language in the controls
    private void SetTextLanguage()
    {
        // Set the current UI culture of the selected language
        Globals.SetCultureSelectedLanguage();

        // Set the text of the controls
        if (!bSolvingCube)
        {
            SetArrowTooltips(true);
        }
    }
}
