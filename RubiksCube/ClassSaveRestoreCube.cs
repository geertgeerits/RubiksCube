﻿using System.Diagnostics;

namespace RubiksCube
{
    internal sealed class ClassSaveRestoreCube
    {
        /// <summary>
        /// Save the cube data
        /// </summary>
        /// <returns></returns>
        public static bool CubeDataSave()
        {
            string cFileName = Path.Combine(FileSystem.CacheDirectory, "RubiksCube.txt");

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
                    sw.WriteLine(Globals.aFaceColors[nRow]);
                }

                for (nRow = 0; nRow < 54; nRow++)
                {
                    sw.WriteLine(Globals.aPieces[nRow]);
                }

                // Close the StreamWriter object
                sw.Close();
            }
            catch (Exception ex)
            {
                _ = Application.Current!.MainPage!.DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Open, restore the cube
        /// </summary>
        /// <returns></returns>
        public static bool CubeDataOpen()
        {
            string cFileName = FileSystem.CacheDirectory + "/RubiksCube.txt";

            if (File.Exists(cFileName) == false)
            {
                return false;
            }

            int nRow;

            try
            {
                // Open the text file using a stream reader
                using StreamReader sr = new(cFileName, false);

                for (nRow = 1; nRow < 7; nRow++)
                {
                    Globals.aFaceColors[nRow] = sr.ReadLine() ?? string.Empty;
                }

                for (nRow = 0; nRow < 54; nRow++)
                {
                    Globals.aPieces[nRow] = sr.ReadLine() ?? string.Empty;
                }

                // Close the StreamReader object
                sr.Close();
            }
            catch (Exception ex)
            {
                _ = Application.Current!.MainPage!.DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Save the cube turns (for testing)
        /// </summary>
        /// <param name="cFile"></param>
        /// <returns></returns>
        public static bool CubeTurnsSave(string cFile)
        {
            string cFileName = Path.Combine(FileSystem.AppDataDirectory, cFile);

            if (File.Exists(cFileName))
            {
                File.Delete(cFileName);
            }

            try
            {
                using StreamWriter sw = new(cFileName, false);

                sw.WriteLine($"Turns: {Globals.lCubeTurns.Count}");
                sw.WriteLine();

                foreach (string cItem in Globals.lCubeTurns)
                {
                    sw.WriteLine(cItem);
                }

                // Close the StreamWriter object
                sw.Close();
            }
            catch (Exception ex)
            {
                _ = Application.Current!.MainPage!.DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
                return false;
            }
            
            Debug.WriteLine($"CubeTurnsSave cFileName:\n{cFileName}");

            return true;
        }
    }
}
