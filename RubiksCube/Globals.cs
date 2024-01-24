// Global usings
global using RubiksCube.Resources.Languages;
global using System.Globalization;

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
