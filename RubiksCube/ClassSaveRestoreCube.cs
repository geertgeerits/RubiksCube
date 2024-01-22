namespace RubiksCube
{
    internal class ClassSaveRestoreCube
    {
        // Save the colors of the cube pieces to an array.
        public static string[] SaveColorsCube()
        {
            int nRow;
            int nElement = 0;
            string[] aColor = new string[54];

            for (nRow = 1; nRow < 10; nRow++)
            {
                aColor[nElement] = Globals.aFrontFace[nRow];
                nElement++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                aColor[nElement] = Globals.aRightFace[nRow];
                nElement++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                aColor[nElement] = Globals.aBackFace[nRow];
                nElement++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                aColor[nElement] = Globals.aLeftFace[nRow];
                nElement++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                aColor[nElement] = Globals.aUpFace[nRow];
                nElement++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                aColor[nElement] = Globals.aDownFace[nRow];
                nElement++;
            }

            return aColor;
        }

        // Restore the colors of the cube pieces from an array.
        public static void RestoreColorsCube(string[] aColor)
        {
            int nRow;
            int nElement = 0;

            for (nRow = 1; nRow < 10; nRow++)
            {
                Globals.aFrontFace[nRow] = aColor[nElement];
                nElement++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                Globals.aRightFace[nRow] = aColor[nElement];
                nElement++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                Globals.aBackFace[nRow] = aColor[nElement];
                nElement++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                Globals.aLeftFace[nRow] = aColor[nElement];
                nElement++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                Globals.aUpFace[nRow] = aColor[nElement];
                nElement++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                Globals.aDownFace[nRow] = aColor[nElement];
                nElement++;
            }
        }

        // Save the cube.
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

                for (nRow = 1; nRow < 10; nRow++)
                {
                    sw.WriteLine(Globals.aFrontFace[nRow]);
                }

                for (nRow = 1; nRow < 10; nRow++)
                {
                    sw.WriteLine(Globals.aRightFace[nRow]);
                }

                for (nRow = 1; nRow < 10; nRow++)
                {
                    sw.WriteLine(Globals.aBackFace[nRow]);
                }

                for (nRow = 1; nRow < 10; nRow++)
                {
                    sw.WriteLine(Globals.aLeftFace[nRow]);
                }

                for (nRow = 1; nRow < 10; nRow++)
                {
                    sw.WriteLine(Globals.aUpFace[nRow]);
                }

                for (nRow = 1; nRow < 10; nRow++)
                {
                    sw.WriteLine(Globals.aDownFace[nRow]);
                }

                // Close the StreamWriter object.
                sw.Close();
            }
            catch (Exception ex)
            {
                //DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
                return;
            }
        }

        // Restore the cube.
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
                // Open the text file using a stream reader.
                using StreamReader sr = new(cFileName, false);

                for (nRow = 1; nRow < 10; nRow++)
                {
                    Globals.aFrontFace[nRow] = sr.ReadLine();
                }

                for (nRow = 1; nRow < 10; nRow++)
                {
                    Globals.aRightFace[nRow] = sr.ReadLine();
                }

                for (nRow = 1; nRow < 10; nRow++)
                {
                    Globals.aBackFace[nRow] = sr.ReadLine();
                }

                for (nRow = 1; nRow < 10; nRow++)
                {
                    Globals.aLeftFace[nRow] = sr.ReadLine();
                }

                for (nRow = 1; nRow < 10; nRow++)
                {
                    Globals.aUpFace[nRow] = sr.ReadLine();
                }

                for (nRow = 1; nRow < 10; nRow++)
                {
                    Globals.aDownFace[nRow] = sr.ReadLine();
                }

                // Close the StreamReader object.
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
