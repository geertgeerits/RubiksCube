namespace RubiksCube
{
    internal class ClassSaveRestoreCube
    {
        // Save the colors of the cube pieces to an array
        public static string[] SaveColorsCube()
        {
            int nRow;
            int nElement = 0;
            string[] aColor = new string[54];

            for (nRow = 0; nRow < 54; nRow++)
            {
                aColor[nElement] = Globals.aPieces[nRow];
                nElement++;
            }

            return aColor;
        }

        // Restore the colors of the cube pieces from an array
        public static void RestoreColorsCube(string[] aColor)
        {
            int nRow;
            int nElement = 0;

            for (nRow = 0; nRow < 54; nRow++)
            {
                Globals.aPieces[nRow] = aColor[nElement];
                nElement++;
            }
        }

        // Save the cube
        public void SaveCube()
        {
            string cFileName = System.IO.Path.Combine(FileSystem.CacheDirectory, "CubePieces.txt");

            if (File.Exists(cFileName))
            {
                File.Delete(cFileName);
            }

            int nRow;

            try
            {
                using StreamWriter sw = new(cFileName, false);

                for (nRow = 0; nRow < 54; nRow++)
                {
                    sw.WriteLine(Globals.aPieces[nRow]);
                }

                // Close the StreamWriter object
                sw.Close();
            }
            catch (Exception ex)
            {
                //DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
                return;
            }
        }

        // Restore the cube
        public void RestoreCube()
        {
            string cFileName = FileSystem.CacheDirectory + "/CubePieces.txt";

            if (File.Exists(cFileName) == false)
            {
                return;
            }

            int nRow;

            try
            {
                // Open the text file using a stream reader
                using StreamReader sr = new(cFileName, false);

                for (nRow = 0; nRow < 54; nRow++)
                {
                    Globals.aPieces[nRow] = sr.ReadLine();
                }

                // Close the StreamReader object
                sr.Close();
            }
            catch (Exception ex)
            {
                //DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
                return;
            }
        }
    }
}
