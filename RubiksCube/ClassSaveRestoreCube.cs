﻿namespace RubiksCube
{
    internal class ClassSaveRestoreCube
    {
        // Save the cube
        public bool CubeDataSave()
        {
            //SetCubeColorsInArrays();

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
                _ = Application.Current.MainPage.DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
                return false;
            }

            return true;
        }

        // Open, restore the cube
        public bool CubeDataOpen()
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
                    Globals.aFaceColors[nRow] = sr.ReadLine();
                }

                for (nRow = 0; nRow < 54; nRow++)
                {
                    Globals.aPieces[nRow] = sr.ReadLine();
                }

                // Close the StreamReader object
                sr.Close();
            }
            catch (Exception ex)
            {
                _ = Application.Current.MainPage.DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
                return false;
            }

            //SetCubeColorsFromArrays();
            return true;
        }
    }
}