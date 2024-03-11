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
    public const string turnFrontCW = "F";
    public const string turnFrontCCW = "F'";
    public const string turnFront2 = "F2";
    public const string turnRightCW = "R";
    public const string turnRightCCW = "R'";
    public const string turnRight2 = "R2";
    public const string turnBackCW = "B";
    public const string turnBackCCW = "B'";
    public const string turnBack2 = "B2";
    public const string turnLeftCW = "L";
    public const string turnLeftCCW = "L'";
    public const string turnLeft2 = "L2";
    public const string turnUpCW = "U";
    public const string turnUpCCW = "U'";
    public const string turnUp2 = "U2";
    public const string turnDownCW = "D";
    public const string turnDownCCW = "D'";
    public const string turnDown2 = "D2";

    // Slice turns
    public const string turnUpHorMiddleRight = "S";
    public const string turnUpHorMiddleLeft = "S'";
    public const string turnUpHorMiddle2 = "S2";
    public const string turnUpVerMiddleBack = "M'";
    public const string turnUpVerMiddleFront = "M";
    public const string turnUpVerMiddle2 = "M2";
    public const string turnFrontHorMiddleLeft = "E'";
    public const string turnFrontHorMiddleRight = "E";
    public const string turnFrontHorMiddle2 = "E2";

    // Two layers at the same time
    public const string turn2LayersFrontCW = "f";
    public const string turn2LayersFrontCCW = "f'";
    public const string turn2LayersFront2 = "f2";
    public const string turn2LayersRightCW = "r";
    public const string turn2LayersRightCCW = "r'";
    public const string turn2LayersRight2 = "r2";
    public const string turn2LayersBackCW = "b";
    public const string turn2LayersBackCCW = "b'";
    public const string turn2LayersBack2 = "b2";
    public const string turn2LayersLeftCW = "l";
    public const string turn2LayersLeftCCW = "l'";
    public const string turn2LayersLeft2 = "l2";
    public const string turn2LayersUpCW = "u";
    public const string turn2LayersUpCCW = "u'";
    public const string turn2LayersUp2 = "u2";
    public const string turn2LayersDownCW = "d";
    public const string turn2LayersDownCCW = "d'";
    public const string turn2LayersDown2 = "d2";

    // Whole cube turns
    public const string turnCubeFrontToRight = "y'";
    public const string turnCubeFrontToLeft = "y";
    public const string turnCubeFrontToLeft2 = "y2";
    public const string turnCubeFrontToUp = "x";
    public const string turnCubeFrontToUp2 = "x2";
    public const string turnCubeFrontToDown = "x'";
    public const string turnCubeUpToRight = "z";
    public const string turnCubeUpToRight2 = "z2";
    public const string turnCubeUpToLeft = "z'";

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

    //// Make a turn (with 1 letter) of the cube/face/side
    public static async Task MakeTurnLetterAsync(string cTurn)
    {
        // Remove leading and trailing whitespace
        cTurn = cTurn.Trim();

        // Split the string into individual turns
        foreach (string cTurnPart in cTurn.Split(' '))
        {
            // Add the turn to the list
            lCubeTurns.Add(cTurnPart);

            // Turn the cube/face/side
            await ClassCubeTurns.TurnCubeLayersAsync(cTurnPart);
        }
    }
}
