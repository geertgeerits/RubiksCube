namespace RubiksCube
{
    internal class ClassColorsCube
    {
        // Check the number of colors of the cube
        public static bool CheckNumberColors()
        {
            int nNumberOfColors1 = 0;
            int nNumberOfColors2 = 0;
            int nNumberOfColors3 = 0;
            int nNumberOfColors4 = 0;
            int nNumberOfColors5 = 0;
            int nNumberOfColors6 = 0;

            int nRow;

            // Check the number of colors of the cube
            // Front face
            for (nRow = 0; nRow < 9; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[1])
                    nNumberOfColors1++;
            }

            for (nRow = 0; nRow < 9; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[2])
                    nNumberOfColors2++;
            }

            for (nRow = 0; nRow < 9; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[3])
                    nNumberOfColors3++;
            }

            for (nRow = 0; nRow < 9; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[4])
                    nNumberOfColors4++;
            }

            for (nRow = 0; nRow < 9; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[5])
                    nNumberOfColors5++;
            }

            for (nRow = 0; nRow < 9; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[6])
                    nNumberOfColors6++;
            }

            // Right face
            for (nRow = 9; nRow < 18; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[1])
                    nNumberOfColors1++;
            }

            for (nRow = 9; nRow < 18; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[2])
                    nNumberOfColors2++;
            }

            for (nRow = 9; nRow < 18; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[3])
                    nNumberOfColors3++;
            }

            for (nRow = 9; nRow < 18; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[4])
                    nNumberOfColors4++;
            }

            for (nRow = 9; nRow < 18; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[5])
                    nNumberOfColors5++;
            }

            for (nRow = 9; nRow < 18; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[6])
                    nNumberOfColors6++;
            }

            // Back face
            for (nRow = 18; nRow < 27; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[1])
                    nNumberOfColors1++;
            }

            for (nRow = 18; nRow < 27; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[2])
                    nNumberOfColors2++;
            }

            for (nRow = 18; nRow < 27; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[3])
                    nNumberOfColors3++;
            }

            for (nRow = 18; nRow < 27; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[4])
                    nNumberOfColors4++;
            }

            for (nRow = 18; nRow < 27; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[5])
                    nNumberOfColors5++;
            }

            for (nRow = 18; nRow < 27; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[6])
                    nNumberOfColors6++;
            }

            // Left face
            for (nRow = 27; nRow < 36; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[1])
                    nNumberOfColors1++;
            }

            for (nRow = 27; nRow < 36; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[2])
                    nNumberOfColors2++;
            }

            for (nRow = 27; nRow < 36; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[3])
                    nNumberOfColors3++;
            }

            for (nRow = 27; nRow < 36; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[4])
                    nNumberOfColors4++;
            }

            for (nRow = 27; nRow < 36; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[5])
                    nNumberOfColors5++;
            }

            for (nRow = 27; nRow < 36; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[6])
                    nNumberOfColors6++;
            }

            // Top layer
            for (nRow = 36; nRow < 45; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[1])
                    nNumberOfColors1++;
            }

            for (nRow = 36; nRow < 45; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[2])
                    nNumberOfColors2++;
            }

            for (nRow = 36; nRow < 45; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[3])
                    nNumberOfColors3++;
            }

            for (nRow = 36; nRow < 45; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[4])
                    nNumberOfColors4++;
            }

            for (nRow = 36; nRow < 45; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[5])
                    nNumberOfColors5++;
            }

            for (nRow = 36; nRow < 45; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[6])
                    nNumberOfColors6++;
            }

            // Bottom layer
            for (nRow = 45; nRow < 54; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[1])
                    nNumberOfColors1++;
            }

            for (nRow = 45; nRow < 54; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[2])
                    nNumberOfColors2++;
            }

            for (nRow = 45; nRow < 54; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[3])
                    nNumberOfColors3++;
            }

            for (nRow = 45; nRow < 54; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[4])
                    nNumberOfColors4++;
            }

            for (nRow = 45; nRow < 54; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[5])
                    nNumberOfColors5++;
            }

            for (nRow = 45; nRow < 54; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[6])
                    nNumberOfColors6++;
            }

            if (nNumberOfColors1 != 9 || nNumberOfColors2 != 9 || nNumberOfColors3 != 9 || nNumberOfColors4 != 9 || nNumberOfColors5 != 9 || nNumberOfColors6 != 9)
            {
                _ = Application.Current.MainPage.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageNineSameColor_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            // Check the number of colors of the central square of the cube
            bool bColorCenterCube = true;

            if (Globals.aPieces[40] == Globals.aPieces[4] || Globals.aPieces[40] == Globals.aPieces[13] || Globals.aPieces[40] == Globals.aPieces[31] || Globals.aPieces[40] == Globals.aPieces[22] || Globals.aPieces[40] == Globals.aPieces[49])
            {
                bColorCenterCube = false;
            }

            if (Globals.aPieces[4] == Globals.aPieces[40] || Globals.aPieces[4] == Globals.aPieces[13] || Globals.aPieces[4] == Globals.aPieces[31] || Globals.aPieces[4] == Globals.aPieces[22] || Globals.aPieces[4] == Globals.aPieces[49])
            {
                bColorCenterCube = false;
            }

            if (Globals.aPieces[13] == Globals.aPieces[4] || Globals.aPieces[13] == Globals.aPieces[40] || Globals.aPieces[13] == Globals.aPieces[31] || Globals.aPieces[13] == Globals.aPieces[22] || Globals.aPieces[13] == Globals.aPieces[49])
            {
                bColorCenterCube = false;
            }

            if (Globals.aPieces[31] == Globals.aPieces[4] || Globals.aPieces[31] == Globals.aPieces[13] || Globals.aPieces[31] == Globals.aPieces[40] || Globals.aPieces[31] == Globals.aPieces[22] || Globals.aPieces[31] == Globals.aPieces[49])
            {
                bColorCenterCube = false;
            }

            if (Globals.aPieces[22] == Globals.aPieces[4] || Globals.aPieces[22] == Globals.aPieces[13] || Globals.aPieces[22] == Globals.aPieces[31] || Globals.aPieces[22] == Globals.aPieces[40] || Globals.aPieces[22] == Globals.aPieces[49])
            {
                bColorCenterCube = false;
            }

            if (Globals.aPieces[49] == Globals.aPieces[4] || Globals.aPieces[49] == Globals.aPieces[13] || Globals.aPieces[49] == Globals.aPieces[31] || Globals.aPieces[49] == Globals.aPieces[22] || Globals.aPieces[49] == Globals.aPieces[40])
            {
                bColorCenterCube = false;
            }

            if (!bColorCenterCube)
            {
                _ = Application.Current.MainPage.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCentralCube_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            // Check the number of colors of the corner cubes of the cube
            bool bColorCornerCube = true;

            if (Globals.aPieces[42] == Globals.aPieces[29] || Globals.aPieces[42] == Globals.aPieces[0] || Globals.aPieces[0] == Globals.aPieces[29])
            {
                bColorCornerCube = false;
            }

            if (Globals.aPieces[36] == Globals.aPieces[27] || Globals.aPieces[36] == Globals.aPieces[20] || Globals.aPieces[27] == Globals.aPieces[20])
            {
                bColorCornerCube = false;
            }

            if (Globals.aPieces[38] == Globals.aPieces[11] || Globals.aPieces[38] == Globals.aPieces[18] || Globals.aPieces[11] == Globals.aPieces[18])
            {
                bColorCornerCube = false;
            }

            if (Globals.aPieces[44] == Globals.aPieces[2] || Globals.aPieces[44] == Globals.aPieces[9] || Globals.aPieces[2] == Globals.aPieces[9])
            {
                bColorCornerCube = false;
            }

            if (Globals.aPieces[45] == Globals.aPieces[35] || Globals.aPieces[45] == Globals.aPieces[6] || Globals.aPieces[6] == Globals.aPieces[35])
            {
                bColorCornerCube = false;
            }

            if (Globals.aPieces[51] == Globals.aPieces[33] || Globals.aPieces[51] == Globals.aPieces[26] || Globals.aPieces[26] == Globals.aPieces[33])
            {
                bColorCornerCube = false;
            }

            if (Globals.aPieces[53] == Globals.aPieces[17] || Globals.aPieces[53] == Globals.aPieces[24] || Globals.aPieces[24] == Globals.aPieces[17])
            {
                bColorCornerCube = false;
            }

            if (Globals.aPieces[47] == Globals.aPieces[15] || Globals.aPieces[47] == Globals.aPieces[8] || Globals.aPieces[8] == Globals.aPieces[15])
            {
                bColorCornerCube = false;
            }

            if (!bColorCornerCube)
            {
                _ = Application.Current.MainPage.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCornerCube_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            // Check the number of colors of the edge cubes of the cube
            bool bColorEdgeCube = true;

            if (Globals.aPieces[37] == Globals.aPieces[19] || Globals.aPieces[39] == Globals.aPieces[28] || Globals.aPieces[41] == Globals.aPieces[10] || Globals.aPieces[43] == Globals.aPieces[1])
            {
                bColorEdgeCube = false;
            }

            if (Globals.aPieces[46] == Globals.aPieces[7] || Globals.aPieces[48] == Globals.aPieces[34] || Globals.aPieces[50] == Globals.aPieces[16] || Globals.aPieces[52] == Globals.aPieces[25])
            {
                bColorEdgeCube = false;
            }

            if (!bColorEdgeCube)
            {
                _ = Application.Current.MainPage.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorEdgeCube_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            // Check the opposite center pieces of the cube
            // Colors: 1= red, 2= blue, 3= orange, 4= green, 5= white, 6= yellow
            // Center pieces: 4= front/red, 13= right/blue, 22= back/orange, 31= left/green, 40= up/white, 49= down/yellow
            // Opposite colors: red-orange 1-3, blue-green 2-4, white-yellow 5-6
            // Opposite center pieces: front-back 4-22, right-left 13-31, up-down 40-49
            bool bResult1 = CheckOppositeCenterPieces(4, 1, 22, 3);
            bool bResult2 = CheckOppositeCenterPieces(4, 2, 22, 4);
            bool bResult3 = CheckOppositeCenterPieces(4, 3, 22, 1);
            bool bResult4 = CheckOppositeCenterPieces(4, 4, 22, 2);
            bool bResult5 = CheckOppositeCenterPieces(4, 5, 22, 6);
            bool bResult6 = CheckOppositeCenterPieces(4, 6, 22, 5);

            if (!bResult1 || !bResult2 || !bResult3 || !bResult4 || !bResult5 || !bResult6)
            {
                _ = Application.Current.MainPage.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCenterPiece_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            bResult1 = CheckOppositeCenterPieces(13, 1, 31, 3);
            bResult2 = CheckOppositeCenterPieces(13, 2, 31, 4);
            bResult3 = CheckOppositeCenterPieces(13, 3, 31, 1);
            bResult4 = CheckOppositeCenterPieces(13, 4, 31, 2);
            bResult5 = CheckOppositeCenterPieces(13, 5, 31, 6);
            bResult6 = CheckOppositeCenterPieces(13, 6, 31, 5);

            if (!bResult1 || !bResult2 || !bResult3 || !bResult4 || !bResult5 || !bResult6)
            {
                _ = Application.Current.MainPage.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCenterPiece_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            bResult1 = CheckOppositeCenterPieces(22, 1, 4, 3);
            bResult2 = CheckOppositeCenterPieces(22, 2, 4, 4);
            bResult3 = CheckOppositeCenterPieces(22, 3, 4, 1);
            bResult4 = CheckOppositeCenterPieces(22, 4, 4, 2);
            bResult5 = CheckOppositeCenterPieces(22, 5, 4, 6);
            bResult6 = CheckOppositeCenterPieces(22, 6, 4, 5);

            if (!bResult1 || !bResult2 || !bResult3 || !bResult4 || !bResult5 || !bResult6)
            {
                _ = Application.Current.MainPage.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCenterPiece_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            bResult1 = CheckOppositeCenterPieces(31, 1, 13, 3);
            bResult2 = CheckOppositeCenterPieces(31, 2, 13, 4);
            bResult3 = CheckOppositeCenterPieces(31, 3, 13, 1);
            bResult4 = CheckOppositeCenterPieces(31, 4, 13, 2);
            bResult5 = CheckOppositeCenterPieces(31, 5, 13, 6);
            bResult6 = CheckOppositeCenterPieces(31, 6, 13, 5);

            if (!bResult1 || !bResult2 || !bResult3 || !bResult4 || !bResult5 || !bResult6)
            {
                _ = Application.Current.MainPage.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCenterPiece_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            bResult1 = CheckOppositeCenterPieces(40, 1, 49, 3);
            bResult2 = CheckOppositeCenterPieces(40, 2, 49, 4);
            bResult3 = CheckOppositeCenterPieces(40, 3, 49, 1);
            bResult4 = CheckOppositeCenterPieces(40, 4, 49, 2);
            bResult5 = CheckOppositeCenterPieces(40, 5, 49, 6);
            bResult6 = CheckOppositeCenterPieces(40, 6, 49, 5);

            if (!bResult1 || !bResult2 || !bResult3 || !bResult4 || !bResult5 || !bResult6)
            {
                _ = Application.Current.MainPage.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCenterPiece_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            bResult1 = CheckOppositeCenterPieces(49, 1, 40, 3);
            bResult2 = CheckOppositeCenterPieces(49, 2, 40, 4);
            bResult3 = CheckOppositeCenterPieces(49, 3, 40, 1);
            bResult4 = CheckOppositeCenterPieces(49, 4, 40, 2);
            bResult5 = CheckOppositeCenterPieces(49, 5, 40, 6);
            bResult6 = CheckOppositeCenterPieces(49, 6, 40, 5);

            if (!bResult1 || !bResult2 || !bResult3 || !bResult4 || !bResult5 || !bResult6)
            {
                _ = Application.Current.MainPage.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCenterPiece_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            return true;
        }

        // Check the opposite center pieces of the cube
        private static bool CheckOppositeCenterPieces(int nPiece1, int nColor1, int nPiece2, int nColor2)
        {
            if (Globals.aPieces[nPiece1] == Globals.aFaceColors[nColor1] && Globals.aPieces[nPiece2] != Globals.aFaceColors[nColor2])
            {
                return false;
            }

            return true;
        }

        // Check if the cube is solved
        public static bool CheckIfSolved()
        {
            bool bColorsUp = false;
            bool bColorsFront = false;
            bool bColorsRight = false;
            bool bColorsLeft = false;
            bool bColorsBack = false;
            bool bColorsDown = false;

            if (Globals.aPieces[36] == Globals.aPieces[37] && Globals.aPieces[36] == Globals.aPieces[38] && Globals.aPieces[36] == Globals.aPieces[39] && Globals.aPieces[36] == Globals.aPieces[40] && Globals.aPieces[36] == Globals.aPieces[41] && Globals.aPieces[36] == Globals.aPieces[42] && Globals.aPieces[36] == Globals.aPieces[43] && Globals.aPieces[36] == Globals.aPieces[44])
            {
                bColorsUp = true;
            }

            if (Globals.aPieces[0] == Globals.aPieces[1] && Globals.aPieces[0] == Globals.aPieces[2] && Globals.aPieces[0] == Globals.aPieces[3] && Globals.aPieces[0] == Globals.aPieces[4] && Globals.aPieces[0] == Globals.aPieces[5] && Globals.aPieces[0] == Globals.aPieces[6] && Globals.aPieces[0] == Globals.aPieces[7] && Globals.aPieces[0] == Globals.aPieces[8])
            {
                bColorsFront = true;
            }

            if (Globals.aPieces[9] == Globals.aPieces[10] && Globals.aPieces[9] == Globals.aPieces[11] && Globals.aPieces[9] == Globals.aPieces[12] && Globals.aPieces[9] == Globals.aPieces[13] && Globals.aPieces[9] == Globals.aPieces[14] && Globals.aPieces[9] == Globals.aPieces[15] && Globals.aPieces[9] == Globals.aPieces[16] && Globals.aPieces[9] == Globals.aPieces[17])
            {
                bColorsRight = true;
            }

            if (Globals.aPieces[27] == Globals.aPieces[28] && Globals.aPieces[27] == Globals.aPieces[29] && Globals.aPieces[27] == Globals.aPieces[30] && Globals.aPieces[27] == Globals.aPieces[31] && Globals.aPieces[27] == Globals.aPieces[32] && Globals.aPieces[27] == Globals.aPieces[33] && Globals.aPieces[27] == Globals.aPieces[34] && Globals.aPieces[27] == Globals.aPieces[35])
            {
                bColorsLeft = true;
            }

            if (Globals.aPieces[18] == Globals.aPieces[19] && Globals.aPieces[18] == Globals.aPieces[20] && Globals.aPieces[18] == Globals.aPieces[21] && Globals.aPieces[18] == Globals.aPieces[22] && Globals.aPieces[18] == Globals.aPieces[23] && Globals.aPieces[18] == Globals.aPieces[24] && Globals.aPieces[18] == Globals.aPieces[25] && Globals.aPieces[18] == Globals.aPieces[26])
            {
                bColorsBack = true;
            }

            if (Globals.aPieces[45] == Globals.aPieces[46] && Globals.aPieces[45] == Globals.aPieces[47] && Globals.aPieces[45] == Globals.aPieces[48] && Globals.aPieces[45] == Globals.aPieces[49] && Globals.aPieces[45] == Globals.aPieces[50] && Globals.aPieces[45] == Globals.aPieces[51] && Globals.aPieces[45] == Globals.aPieces[52] && Globals.aPieces[45] == Globals.aPieces[53])
            {
                bColorsDown = true;
            }

            if (!bColorsUp || !bColorsFront || !bColorsRight || !bColorsLeft || !bColorsBack || !bColorsDown)
            {
                return false;
            }

            return true;
        }

        // Reset the colors of the cube
        public static void ResetCube()
        {
            int nRow;

            for (nRow = 0; nRow < 9; nRow++)
            {
                Globals.aPieces[nRow] = Globals.aFaceColors[1];
            }

            for (nRow = 9; nRow < 18; nRow++)
            {
                Globals.aPieces[nRow] = Globals.aFaceColors[2];
            }

            for (nRow = 18; nRow < 27; nRow++)
            {
                Globals.aPieces[nRow] = Globals.aFaceColors[3];
            }

            for (nRow = 27; nRow < 36; nRow++)
            {
                Globals.aPieces[nRow] = Globals.aFaceColors[4];
            }

            for (nRow = 36; nRow < 45; nRow++)
            {
                Globals.aPieces[nRow] = Globals.aFaceColors[5];
            }

            for (nRow = 45; nRow < 54; nRow++)
            {
                Globals.aPieces[nRow] = Globals.aFaceColors[6];
            }
        }
    }
}
