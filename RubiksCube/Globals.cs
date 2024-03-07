//// Global usings
global using RubiksCube.Resources.Languages;
global using System.Globalization;

namespace RubiksCube;

//// Global variables and methods
internal static class Globals
{
    //// Global variables
    public static string cTheme;
    public static string cLanguage;
    public static bool bLanguageChanged = false;
    public static string cLanguageSpeech;
    public static string[] cLanguageLocales;
    public static bool bLanguageLocalesExist = false;
    public static bool bExplainText = false;
    public static bool bExplainSpeech = false;
    public static bool bTextToSpeechIsBusy = false;
    public static CancellationTokenSource cts;
    public static bool bLicense;

    public static string[] aFaceColors = new string[7];
    public static string[] aPieces = new string[54];
    public static string[] aPiecesTemp = new string[54];
    public static string[] aStartPieces = new string[54];
    public static List<string> lCubeTurns = [];

    // Cube turns
    // Face rotations
    public const string turnFrontCW = "TurnFrontCW";
    public const string turnFrontCCW = "TurnFrontCCW";
    public const string turnFront2 = "TurnFront2";
    public const string turnRightCW = "TurnRightCW";
    public const string turnRightCCW = "TurnRightCCW";
    public const string turnRight2 = "TurnRight2";
    public const string turnBackCW = "TurnBackCW";
    public const string turnBackCCW = "TurnBackCCW";
    public const string turnBack2 = "TurnBack2";
    public const string turnLeftCW = "TurnLeftCW";
    public const string turnLeftCCW = "TurnLeftCCW";
    public const string turnLeft2 = "TurnLeft2";
    public const string turnUpCW = "TurnUpCW";
    public const string turnUpCCW = "TurnUpCCW";
    public const string turnUp2 = "TurnUp2";
    public const string turnDownCW = "TurnDownCW";
    public const string turnDownCCW = "TurnDownCCW";
    public const string turnDown2 = "TurnDown2";

    // Slice turns
    public const string turnUpHorMiddleRight = "TurnUpHorMiddleRight";
    public const string turnUpHorMiddleLeft = "TurnUpHorMiddleLeft";
    public const string turnUpHorMiddle2 = "TurnUpHorMiddle2";
    public const string turnUpVerMiddleBack = "TurnUpVerMiddleBack";
    public const string turnUpVerMiddleFront = "TurnUpVerMiddleFront";
    public const string turnUpVerMiddle2 = "TurnUpVerMiddle2";
    public const string turnFrontHorMiddleLeft = "TurnFrontHorMiddleLeft";
    public const string turnFrontHorMiddleRight = "TurnFrontHorMiddleRight";
    public const string turnFrontHorMiddle2 = "TurnFrontHorMiddle2";

    // Two layers at the same time
    public const string turn2LayersFrontCW = "Turn2LayersFrontCW";
    public const string turn2LayersFrontCCW = "Turn2LayersFrontCCW";
    public const string turn2LayersFront2 = "Turn2LayersFront2";
    public const string turn2LayersRightCW = "Turn2LayersRightCW";
    public const string turn2LayersRightCCW = "Turn2LayersRightCCW";
    public const string turn2LayersRight2 = "Turn2LayersRight2";
    public const string turn2LayersBackCW = "Turn2LayersBackCW";
    public const string turn2LayersBackCCW = "Turn2LayersBackCCW";
    public const string turn2LayersBack2 = "Turn2LayersBack2";
    public const string turn2LayersLeftCW = "Turn2LayersLeftCW";
    public const string turn2LayersLeftCCW = "Turn2LayersLeftCCW";
    public const string turn2LayersLeft2 = "Turn2LayersLeft2";
    public const string turn2LayersUpCW = "Turn2LayersUpCW";
    public const string turn2LayersUpCCW = "Turn2LayersUpCCW";
    public const string turn2LayersUp2 = "Turn2LayersUp2";
    public const string turn2LayersDownCW = "Turn2LayersDownCW";
    public const string turn2LayersDownCCW = "Turn2LayersDownCCW";
    public const string turn2LayersDown2 = "Turn2LayersDown2";

    // Whole cube turns
    public const string turnCubeFrontToRight = "TurnCubeFrontToRight";
    public const string turnCubeFrontToLeft = "TurnCubeFrontToLeft";
    public const string turnCubeFrontToLeft2 = "TurnCubeFrontToLeft2";
    public const string turnCubeFrontToUp = "TurnCubeFrontToUp";
    public const string turnCubeFrontToUp2 = "TurnCubeFrontToUp2";
    public const string turnCubeFrontToDown = "TurnCubeFrontToDown";
    public const string turnCubeUpToRight = "TurnCubeUpToRight";
    public const string turnCubeUpToRight2 = "TurnCubeUpToRight2";
    public const string turnCubeUpToLeft = "TurnCubeUpToLeft";

    //// Global methods
    // Set the theme
    public static void SetTheme()
    {
        Application.Current.UserAppTheme = cTheme switch
        {
            "Light" => AppTheme.Light,
            "Dark" => AppTheme.Dark,
            _ => AppTheme.Unspecified,
        };
    }

    //// Set the current UI culture of the selected language
    public static void SetCultureSelectedLanguage()
    {
        try
        {
            CultureInfo switchToCulture = new(cLanguage);
            LocalizationResourceManager.Instance.SetCulture(switchToCulture);
        }
        catch
        {
            // Do nothing
        }
    }

    //// Make a turn (with 1 word) of the cube/face/side
    public static async Task MakeTurnWordAsync(string cTurn)
    {
        // Add the turn to the list
        lCubeTurns.Add(cTurn);

        // Turn the cube/face/side
        await ClassCubeTurns.TurnCubeLayersAsync(cTurn);
    }

    //// Make a turn (with 1 or more letters) of the cube/face/side
    public static async Task MakeTurnLetterAsync(string cTurn)
    {
        // Remove leading and trailing whitespace
        cTurn = cTurn.Trim();

        // Split the string into individual turns
        foreach (string cTurnPart in cTurn.Split(' '))
        {
            switch (cTurnPart)
            {
                // Face rotations
                case "F":
                    cTurn = turnFrontCW;
                    break;
                case "F'":
                    cTurn = turnFrontCCW;
                    break;
                case "F2":
                case "F2'":
                    cTurn = turnFront2;
                    break;
                case "R":
                    cTurn = turnRightCW;
                    break;
                case "R'":
                    cTurn = turnRightCCW;
                    break;
                case "R2":
                case "R2'":
                    cTurn = turnRight2;
                    break;
                case "B":
                    cTurn = turnBackCW;
                    break;
                case "B'":
                    cTurn = turnBackCCW;
                    break;
                case "B2":
                case "B2'":
                    cTurn = turnBack2;
                    break;
                case "L":
                    cTurn = turnLeftCW;
                    break;
                case "L'":
                    cTurn = turnLeftCCW;
                    break;
                case "L2":
                case "L2'":
                    cTurn = turnLeft2;
                    break;
                case "U":
                    cTurn = turnUpCW;
                    break;
                case "U'":
                    cTurn = turnUpCCW;
                    break;
                case "U2":
                case "U2'":
                    cTurn = turnUp2;
                    break;
                case "D":
                    cTurn = turnDownCW;
                    break;
                case "D'":
                    cTurn = turnDownCCW;
                    break;
                case "D2":
                case "D2'":
                    cTurn = turnDown2;
                    break;
                
                // Slice turns
                case "M":
                    cTurn = turnUpVerMiddleFront;
                    break;
                case "M'":
                    cTurn = turnUpVerMiddleBack;
                    break;
                case "M2":
                case "M2'":
                    cTurn = turnUpVerMiddle2;
                    break;
                case "E":
                    cTurn = turnFrontHorMiddleRight;
                    break;
                case "E'":
                    cTurn = turnFrontHorMiddleLeft;
                    break;
                case "E2":
                case "E2'":
                    cTurn = turnFrontHorMiddle2;
                    break;
                case "S":
                    cTurn = turnUpHorMiddleRight;
                    break;
                case "S'":
                    cTurn = turnUpHorMiddleLeft;
                    break;
                case "S2":
                case "S2'":
                    cTurn = turnUpHorMiddle2;
                    break;
                
                // Two layers at the same time
                case "f":
                    cTurn = turn2LayersFrontCW;
                    break;
                case "f'":
                    cTurn = turn2LayersFrontCCW;
                    break;
                case "f2":
                case "f2'":
                    cTurn = turn2LayersFront2;
                    break;
                case "r":
                    cTurn = turn2LayersRightCW;
                    break;
                case "r'":
                    cTurn = turn2LayersRightCCW;
                    break;
                case "r2":
                case "r2'":
                    cTurn = turn2LayersRight2;
                    break;
                case "b":
                    cTurn = turn2LayersBackCW;
                    break;
                case "b'":
                    cTurn = turn2LayersBackCCW;
                    break;
                case "b2":
                case "b2'":
                    cTurn = turn2LayersBack2;
                    break;
                case "l":
                    cTurn = turn2LayersLeftCW;
                    break;
                case "l'":
                    cTurn = turn2LayersLeftCCW;
                    break;
                case "l2":
                case "l2'":
                    cTurn = turn2LayersLeft2;
                    break;
                case "u":
                    cTurn = turn2LayersUpCW;
                    break;
                case "u'":
                    cTurn = turn2LayersUpCCW;
                    break;
                case "u2":
                case "u2'":
                    cTurn = turn2LayersUp2;
                    break;
                case "d":
                    cTurn = turn2LayersDownCW;
                    break;
                case "d'":
                    cTurn = turn2LayersDownCCW;
                    break;
                case "d2":
                case "d2'":
                    cTurn = turn2LayersDown2;
                    break;
                
                // Whole cube turns
                case "x":
                    cTurn = turnCubeFrontToUp;
                    break;
                case "x'":
                    cTurn = turnCubeFrontToDown;
                    break;
                case "x2":
                case "x2'":
                    cTurn = turnCubeFrontToUp2;
                    break;
                case "y":
                    cTurn = turnCubeFrontToLeft;
                    break;
                case "y'":
                    cTurn = turnCubeFrontToRight;
                    break;
                case "y2":
                case "y2'":
                    cTurn = turnCubeFrontToLeft2;
                    break;
                case "z":
                    cTurn = turnCubeUpToRight;
                    break;
                case "z'":
                    cTurn = turnCubeUpToLeft;
                    break;
                case "z2":
                case "z2'":
                    cTurn = turnCubeUpToRight2;
                    break;
                default:
                    await Application.Current.MainPage.DisplayAlert(CubeLang.ErrorTitle_Text, $"MakeTurnLetterAsync\ncTurnPart not found:\n{cTurnPart}", CubeLang.ButtonClose_Text);
                    break;
            }

            // Add the turn to the list
            lCubeTurns.Add(cTurn);

            // Turn the cube/face/side
            await ClassCubeTurns.TurnCubeLayersAsync(cTurn);
        }
    }
}
