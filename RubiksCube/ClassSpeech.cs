namespace RubiksCube
{
    internal class ClassSpeech
    {
        private static IEnumerable<Locale> locales;

        //// Initialize text to speech and fill the the array with the speech languages
        //   .Country = KR ; .Id = ''  ; .Language = ko ; .Name = Korean (South Korea) ; 
        public static async void InitializeTextToSpeech(string cCultureName)
        {
            // Initialize text to speech
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
                // Text to speech is not supported on this device
                await Application.Current.MainPage.DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message + "\n\n" + CubeLang.TextToSpeechError_Text, CubeLang.ButtonClose_Text);
                Globals.bExplainSpeech = false;
                return;
            }

            Globals.bLanguageLocalesExist = true;

            // Put the locales in the array and sort the array
            Globals.cLanguageLocales = new string[nTotalItems];
            int nItem = 0;

            foreach (var l in locales)
            {
                Globals.cLanguageLocales[nItem] = l.Language + "-" + l.Country + " " + l.Name;
                nItem++;
            }

            Array.Sort(Globals.cLanguageLocales);

            // Search for the language after a first start or reset of the application
            if (Globals.cLanguageSpeech == "")
            {
                SearchArrayWithSpeechLanguages(cCultureName);
            }
            //await Application.Current.MainPage.DisplayAlert("Globals.cLanguageSpeech", Globals.cLanguageSpeech, "OK");  // For testing
        }

        //// Search for the language after a first start or reset of the application
        private static void SearchArrayWithSpeechLanguages(string cCultureName)
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

                // If the language is not found try it with the language (Globals.cLanguage) of the user setting for this app
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

                // If the language is still not found use the first language in the array
                if (Globals.cLanguageSpeech == "")
                {
                    Globals.cLanguageSpeech = Globals.cLanguageLocales[0];
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
            }
        }

        //// Convert text to speech
        // If you do not wait long enough to press the arrow key in the Task 'MakeExplainTurnAsync()',
        // an error message will sometimes appear: 'The operation was canceled'.
        // This only occurs if the 'Explained by speech' setting is enabled.
        // The error occurs in the method 'ConvertTextToSpeechAsync()'.
        public static async Task ConvertTextToSpeechAsync(string cTurnCubeText)
        {
            // Cancel the text to speech
            if (Globals.bTextToSpeechIsBusy)
            {
                if (Globals.cts?.IsCancellationRequested ?? true)
                    return;

                Globals.cts.Cancel();
            }

            // Start with the text to speech
            if (cTurnCubeText is not null and not "")
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
#if DEBUG
                    await Application.Current.MainPage.DisplayAlert(CubeLang.ErrorTitle_Text, $"{ex.Message}\n{ex.StackTrace}", CubeLang.ButtonClose_Text);
#endif
                }
            }
        }
        
        //// Cancel speech if a cancellation token exists & hasn't been already requested
        public static void CancelTextToSpeech()
        {
            if (Globals.bTextToSpeechIsBusy)
            {
                if (Globals.cts?.IsCancellationRequested ?? true)
                    return;

                Globals.cts.Cancel();
                Globals.bTextToSpeechIsBusy = false;
            }
        }
    }
}
