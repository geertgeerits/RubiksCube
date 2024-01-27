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
    public static string cCubeColor1;
    public static string cCubeColor2;
    public static string cCubeColor3;
    public static string cCubeColor4;
    public static string cCubeColor5;
    public static string cCubeColor6;

    public static string[] aFaceColors = new string[7];
    public static string[] aPieces = new string[54];
    public static string[] aPiecesTemp = new string[54];
    public static string[] aStartPieces = new string[54];
    public static List<string> lCubeTurns = [];

    // Cube turns
    public const string TurnFrontCW = "TurnFrontCW";
    public const string TurnFrontCCW = "TurnFrontCCW";
    public const string TurnFront2 = "TurnFront2";
    public const string TurnRightCW = "TurnRightCW";
    public const string TurnRightCCW = "TurnRightCCW";
    public const string TurnRight2 = "TurnRight2";
    public const string TurnBackCW = "TurnBackCW";
    public const string TurnBackCCW = "TurnBackCCW";
    public const string TurnBack2 = "TurnBack2";
    public const string TurnLeftCW = "TurnLeftCW";
    public const string TurnLeftCCW = "TurnLeftCCW";
    public const string TurnLeft2 = "TurnLeft2";
    public const string TurnUpCW = "TurnUpCW";
    public const string TurnUpCCW = "TurnUpCCW";
    public const string TurnUp2 = "TurnUp2";
    public const string TurnDownCW = "TurnDownCW";
    public const string TurnDownCCW = "TurnDownCCW";
    public const string TurnDown2 = "TurnDown2";

    public const string TurnUpHorMiddleRight = "TurnUpHorMiddleRight";
    public const string TurnUpHorMiddleLeft = "TurnUpHorMiddleLeft";
    public const string TurnUpHorMiddle2 = "TurnUpHorMiddle2";
    public const string TurnUpVerMiddleBack = "TurnUpVerMiddleBack";
    public const string TurnUpVerMiddleFront = "TurnUpVerMiddleFront";
    public const string TurnUpVerMiddle2 = "TurnUpVerMiddle2";
    public const string TurnFrontHorMiddleLeft = "TurnFrontHorMiddleLeft";
    public const string TurnFrontHorMiddleRight = "TurnFrontHorMiddleRight";
    public const string TurnFrontHorMiddle2 = "TurnFrontHorMiddle2";

    public const string TurnCubeFrontToRight = "TurnCubeFrontToRight";
    public const string TurnCubeFrontToLeft = "TurnCubeFrontToLeft";
    public const string TurnCubeFrontToUp = "TurnCubeFrontToUp";
    public const string TurnCubeFrontToDown = "TurnCubeFrontToDown";
    public const string TurnCubeUpToRight = "TurnCubeUpToRight";
    public const string TurnCubeUpToLeft = "TurnCubeUpToLeft";

    //public enum Turns
    //{
    //    TurnFrontCW,
    //    TurnFrontCCW,
    //    TurnFront2,
    //    TurnRightCW,
    //    TurnRightCCW,
    //    TurnRight2,
    //    TurnBackCW,
    //    TurnBackCCW,
    //    TurnBack2,
    //    TurnLeftCW,
    //    TurnLeftCCW,
    //    TurnLeft2,
    //    TurnUpCW,
    //    TurnUpCCW,
    //    TurnUp2,
    //    TurnDownCW,
    //    TurnDownCCW,
    //    TurnDown2,

    //    TurnUpHorMiddleRight,
    //    TurnUpHorMiddleLeft,
    //    TurnUpHorMiddle2,
    //    TurnUpVerMiddleBack,
    //    TurnUpVerMiddleFront,
    //    TurnUpVerMiddle2,
    //    TurnFrontHorMiddleLeft,
    //    TurnFrontHorMiddleRight,
    //    TurnFrontHorMiddle2,

    //    TurnCubeFrontToRight,
    //    TurnCubeFrontToLeft,
    //    TurnCubeFrontToUp,
    //    TurnCubeFrontToDown,
    //    TurnCubeUpToRight,
    //    TurnCubeUpToLeft,
    //}

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
