namespace RubiksCube
{
    internal class ClassSaveRestoreCube
    {
        // Save the colors of the cube pieces to a temporary array.
        public static void SaveStartColorsCube()
        {
            int nRow;
            int nElement = 0;

            for (nRow = 1; nRow < 10; nRow++)
            {
                Globals.aPiecesTemp[nElement] = Globals.aFrontFace[nRow];
                nElement++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                Globals.aPiecesTemp[nElement] = Globals.aRightFace[nRow];
                nElement++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                Globals.aPiecesTemp[nElement] = Globals.aBackFace[nRow];
                nElement++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                Globals.aPiecesTemp[nElement] = Globals.aLeftFace[nRow];
                nElement++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                Globals.aPiecesTemp[nElement] = Globals.aUpFace[nRow];
                nElement++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                Globals.aPiecesTemp[nElement] = Globals.aDownFace[nRow];
                nElement++;
            }
        }

        // Restore the colors of the cube pieces from a temporary array.
        public static void RestoreStartColorsCube()
        {
            int nRow;
            int nElement = 0;

            for (nRow = 1; nRow < 10; nRow++)
            {
                Globals.aFrontFace[nRow] = Globals.aPiecesTemp[nElement];
                nElement++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                Globals.aRightFace[nRow] = Globals.aPiecesTemp[nElement];
                nElement++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                Globals.aBackFace[nRow] = Globals.aPiecesTemp[nElement];
                nElement++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                Globals.aLeftFace[nRow] = Globals.aPiecesTemp[nElement];
                nElement++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                Globals.aUpFace[nRow] = Globals.aPiecesTemp[nElement];
                nElement++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                Globals.aDownFace[nRow] = Globals.aPiecesTemp[nElement];
                nElement++;
            }
        }

        // Save the cube.
        public void SaveCube()
        {
            //SetCubeColorsInArrays();

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

            //SetCubeColorsFromArrays();
        }
    }
}
