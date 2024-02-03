// Global usings
global using RubiksCube.Resources.Languages;
global using System.Globalization;
//global using static RubiksCube.Globals;

namespace RubiksCube;

// Global variables and methods
internal static class Globals
{
    // Global variables
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

    public const string turnUpHorMiddleRight = "TurnUpHorMiddleRight";
    public const string turnUpHorMiddleLeft = "TurnUpHorMiddleLeft";
    public const string turnUpHorMiddle2 = "TurnUpHorMiddle2";
    public const string turnUpVerMiddleBack = "TurnUpVerMiddleBack";
    public const string turnUpVerMiddleFront = "TurnUpVerMiddleFront";
    public const string turnUpVerMiddle2 = "TurnUpVerMiddle2";
    public const string turnFrontHorMiddleLeft = "TurnFrontHorMiddleLeft";
    public const string turnFrontHorMiddleRight = "TurnFrontHorMiddleRight";
    public const string turnFrontHorMiddle2 = "TurnFrontHorMiddle2";

    public const string turnCubeFrontToRight = "TurnCubeFrontToRight";
    public const string turnCubeFrontToLeft = "TurnCubeFrontToLeft";
    public const string turnCubeFrontToLeft2 = "TurnCubeFrontToLeft2";
    public const string turnCubeFrontToUp = "TurnCubeFrontToUp";
    public const string turnCubeFrontToUp2 = "TurnCubeFrontToUp2";
    public const string turnCubeFrontToDown = "TurnCubeFrontToDown";
    public const string turnCubeUpToRight = "TurnCubeUpToRight";
    public const string turnCubeUpToRight2 = "TurnCubeUpToRight2";
    public const string turnCubeUpToLeft = "TurnCubeUpToLeft";

    // Global methods
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

    // Set the current UI culture of the selected language
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
}
