namespace RubiksCube
{
    internal class ClassColorsCube
    {
        //// Check the number of colors of the cube
        public static bool CheckNumberColors()
        {
            int nNumberOfColors1 = 0;
            int nNumberOfColors2 = 0;
            int nNumberOfColors3 = 0;
            int nNumberOfColors4 = 0;
            int nNumberOfColors5 = 0;
            int nNumberOfColors6 = 0;

            int nItem;

            //// Check the number of colors of the cube
            // Front face
            for (nItem = 0; nItem < 9; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[1])
                    nNumberOfColors1++;
            }

            for (nItem = 0; nItem < 9; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[2])
                    nNumberOfColors2++;
            }

            for (nItem = 0; nItem < 9; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[3])
                    nNumberOfColors3++;
            }

            for (nItem = 0; nItem < 9; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[4])
                    nNumberOfColors4++;
            }

            for (nItem = 0; nItem < 9; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[5])
                    nNumberOfColors5++;
            }

            for (nItem = 0; nItem < 9; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[6])
                    nNumberOfColors6++;
            }

            // Right face
            for (nItem = 9; nItem < 18; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[1])
                    nNumberOfColors1++;
            }

            for (nItem = 9; nItem < 18; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[2])
                    nNumberOfColors2++;
            }

            for (nItem = 9; nItem < 18; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[3])
                    nNumberOfColors3++;
            }

            for (nItem = 9; nItem < 18; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[4])
                    nNumberOfColors4++;
            }

            for (nItem = 9; nItem < 18; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[5])
                    nNumberOfColors5++;
            }

            for (nItem = 9; nItem < 18; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[6])
                    nNumberOfColors6++;
            }

            // Back face
            for (nItem = 18; nItem < 27; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[1])
                    nNumberOfColors1++;
            }

            for (nItem = 18; nItem < 27; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[2])
                    nNumberOfColors2++;
            }

            for (nItem = 18; nItem < 27; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[3])
                    nNumberOfColors3++;
            }

            for (nItem = 18; nItem < 27; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[4])
                    nNumberOfColors4++;
            }

            for (nItem = 18; nItem < 27; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[5])
                    nNumberOfColors5++;
            }

            for (nItem = 18; nItem < 27; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[6])
                    nNumberOfColors6++;
            }

            // Left face
            for (nItem = 27; nItem < 36; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[1])
                    nNumberOfColors1++;
            }

            for (nItem = 27; nItem < 36; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[2])
                    nNumberOfColors2++;
            }

            for (nItem = 27; nItem < 36; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[3])
                    nNumberOfColors3++;
            }

            for (nItem = 27; nItem < 36; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[4])
                    nNumberOfColors4++;
            }

            for (nItem = 27; nItem < 36; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[5])
                    nNumberOfColors5++;
            }

            for (nItem = 27; nItem < 36; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[6])
                    nNumberOfColors6++;
            }

            // Top layer
            for (nItem = 36; nItem < 45; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[1])
                    nNumberOfColors1++;
            }

            for (nItem = 36; nItem < 45; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[2])
                    nNumberOfColors2++;
            }

            for (nItem = 36; nItem < 45; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[3])
                    nNumberOfColors3++;
            }

            for (nItem = 36; nItem < 45; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[4])
                    nNumberOfColors4++;
            }

            for (nItem = 36; nItem < 45; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[5])
                    nNumberOfColors5++;
            }

            for (nItem = 36; nItem < 45; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[6])
                    nNumberOfColors6++;
            }

            // Bottom layer
            for (nItem = 45; nItem < 54; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[1])
                    nNumberOfColors1++;
            }

            for (nItem = 45; nItem < 54; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[2])
                    nNumberOfColors2++;
            }

            for (nItem = 45; nItem < 54; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[3])
                    nNumberOfColors3++;
            }

            for (nItem = 45; nItem < 54; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[4])
                    nNumberOfColors4++;
            }

            for (nItem = 45; nItem < 54; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[5])
                    nNumberOfColors5++;
            }

            for (nItem = 45; nItem < 54; nItem++)
            {
                if (Globals.aPieces[nItem] == Globals.aFaceColors[6])
                    nNumberOfColors6++;
            }

            if (nNumberOfColors1 != 9 || nNumberOfColors2 != 9 || nNumberOfColors3 != 9 || nNumberOfColors4 != 9 || nNumberOfColors5 != 9 || nNumberOfColors6 != 9)
            {
                _ = Application.Current.MainPage.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageNineSameColor_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            //// Check the number of colors of the central square of the cube
            bool bColorCenterCube = true;

            if (Globals.aPieces[40] == Globals.aPieces[4] || Globals.aPieces[40] == Globals.aPieces[13] || Globals.aPieces[40] == Globals.aPieces[31] || Globals.aPieces[40] == Globals.aPieces[22] || Globals.aPieces[40] == Globals.aPieces[49])
                bColorCenterCube = false;

            if (Globals.aPieces[4] == Globals.aPieces[40] || Globals.aPieces[4] == Globals.aPieces[13] || Globals.aPieces[4] == Globals.aPieces[31] || Globals.aPieces[4] == Globals.aPieces[22] || Globals.aPieces[4] == Globals.aPieces[49])
                bColorCenterCube = false;

            if (Globals.aPieces[13] == Globals.aPieces[4] || Globals.aPieces[13] == Globals.aPieces[40] || Globals.aPieces[13] == Globals.aPieces[31] || Globals.aPieces[13] == Globals.aPieces[22] || Globals.aPieces[13] == Globals.aPieces[49])
                bColorCenterCube = false;

            if (Globals.aPieces[31] == Globals.aPieces[4] || Globals.aPieces[31] == Globals.aPieces[13] || Globals.aPieces[31] == Globals.aPieces[40] || Globals.aPieces[31] == Globals.aPieces[22] || Globals.aPieces[31] == Globals.aPieces[49])
                bColorCenterCube = false;

            if (Globals.aPieces[22] == Globals.aPieces[4] || Globals.aPieces[22] == Globals.aPieces[13] || Globals.aPieces[22] == Globals.aPieces[31] || Globals.aPieces[22] == Globals.aPieces[40] || Globals.aPieces[22] == Globals.aPieces[49])
                bColorCenterCube = false;

            if (Globals.aPieces[49] == Globals.aPieces[4] || Globals.aPieces[49] == Globals.aPieces[13] || Globals.aPieces[49] == Globals.aPieces[31] || Globals.aPieces[49] == Globals.aPieces[22] || Globals.aPieces[49] == Globals.aPieces[40])
                bColorCenterCube = false;

            if (!bColorCenterCube)
            {
                _ = Application.Current.MainPage.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCentralCube_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            //// Check the number of colors of the corner cubes of the cube if there are no corner cubes with the same color
            bool bColorCornerCube = true;

            if (Globals.aPieces[42] == Globals.aPieces[29] || Globals.aPieces[42] == Globals.aPieces[0] || Globals.aPieces[0] == Globals.aPieces[29])
                bColorCornerCube = false;

            if (Globals.aPieces[36] == Globals.aPieces[27] || Globals.aPieces[36] == Globals.aPieces[20] || Globals.aPieces[27] == Globals.aPieces[20])
                bColorCornerCube = false;

            if (Globals.aPieces[38] == Globals.aPieces[11] || Globals.aPieces[38] == Globals.aPieces[18] || Globals.aPieces[11] == Globals.aPieces[18])
                bColorCornerCube = false;

            if (Globals.aPieces[44] == Globals.aPieces[2] || Globals.aPieces[44] == Globals.aPieces[9] || Globals.aPieces[2] == Globals.aPieces[9])
                bColorCornerCube = false;

            if (Globals.aPieces[45] == Globals.aPieces[35] || Globals.aPieces[45] == Globals.aPieces[6] || Globals.aPieces[6] == Globals.aPieces[35])
                bColorCornerCube = false;

            if (Globals.aPieces[51] == Globals.aPieces[33] || Globals.aPieces[51] == Globals.aPieces[26] || Globals.aPieces[26] == Globals.aPieces[33])
                bColorCornerCube = false;

            if (Globals.aPieces[53] == Globals.aPieces[17] || Globals.aPieces[53] == Globals.aPieces[24] || Globals.aPieces[24] == Globals.aPieces[17])
                bColorCornerCube = false;

            if (Globals.aPieces[47] == Globals.aPieces[15] || Globals.aPieces[47] == Globals.aPieces[8] || Globals.aPieces[8] == Globals.aPieces[15])
                bColorCornerCube = false;

            if (!bColorCornerCube)
            {
                _ = Application.Current.MainPage.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCornerCube_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            //// Check the number of colors of the corner cubes if there are no more than 4 of the same colors on an corner
            int[] aNumberOfColors = [0, 0, 0, 0, 0, 0, 0];

            CheckNumberColorsCornerCube(aNumberOfColors, 1);
            CheckNumberColorsCornerCube(aNumberOfColors, 2);
            CheckNumberColorsCornerCube(aNumberOfColors, 3);
            CheckNumberColorsCornerCube(aNumberOfColors, 4);
            CheckNumberColorsCornerCube(aNumberOfColors, 5);
            CheckNumberColorsCornerCube(aNumberOfColors, 6);

            if (aNumberOfColors[1] > 4 || aNumberOfColors[2] > 4 || aNumberOfColors[3] > 4 || aNumberOfColors[4] > 4 || aNumberOfColors[5] > 4 || aNumberOfColors[6] > 4)
            {
                _ = Application.Current.MainPage.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageFourSameColorCornerCube_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            //// Check the number of colors of the edge cubes if there are no edge cubes with the same color
            bColorCornerCube = true;

            if (Globals.aPieces[37] == Globals.aPieces[19] || Globals.aPieces[39] == Globals.aPieces[28] || Globals.aPieces[41] == Globals.aPieces[10] || Globals.aPieces[43] == Globals.aPieces[1])
                bColorCornerCube = false;

            if (Globals.aPieces[46] == Globals.aPieces[7] || Globals.aPieces[48] == Globals.aPieces[34] || Globals.aPieces[50] == Globals.aPieces[16] || Globals.aPieces[52] == Globals.aPieces[25])
                bColorCornerCube = false;

            if (!bColorCornerCube)
            {
                _ = Application.Current.MainPage.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorEdgeCube_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            //// Check the number of colors of the edge cubes if there are no more than 4 of the same colors on an edge
            aNumberOfColors = [0, 0, 0, 0, 0, 0, 0];

            CheckNumberColorsEdgeCube(aNumberOfColors, 1);
            CheckNumberColorsEdgeCube(aNumberOfColors, 2);
            CheckNumberColorsEdgeCube(aNumberOfColors, 3);
            CheckNumberColorsEdgeCube(aNumberOfColors, 4);
            CheckNumberColorsEdgeCube(aNumberOfColors, 5);
            CheckNumberColorsEdgeCube(aNumberOfColors, 6);

            if (aNumberOfColors[1] > 4 || aNumberOfColors[2] > 4 || aNumberOfColors[3] > 4 || aNumberOfColors[4] > 4 || aNumberOfColors[5] > 4 || aNumberOfColors[6] > 4)
            {
                _ = Application.Current.MainPage.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageFourSameColorEdgeCube_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            //// Check the opposite center pieces of the cube
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

            // Check the neighbors of the center pieces of the cube
            if (!CheckNeighborsCenterPieces())
            {
                _ = Application.Current.MainPage.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCenterPiece_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            return true;
        }

        //// Check the opposite center pieces of the cube
        private static bool CheckOppositeCenterPieces(int nPiece1, int nColor1, int nPiece2, int nColor2)
        {
            if (Globals.aPieces[nPiece1] == Globals.aFaceColors[nColor1] && Globals.aPieces[nPiece2] != Globals.aFaceColors[nColor2])
            {
                return false;
            }

            return true;
        }

        //// Check the neighbors and opposite center pieces of the cube
        // Globals.aFaceColors[1] -> Front face: Red
        // Globals.aFaceColors[2] -> Right face: Blue
        // Globals.aFaceColors[3] -> Back face: Orange
        // Globals.aFaceColors[4] -> Left face: Green
        // Globals.aFaceColors[5] -> Up face: White
        // Globals.aFaceColors[6] -> Down face: Yellow
        private static bool CheckNeighborsCenterPieces()
        {
            // Front face Red and Up face White
            if (Globals.aPieces[4] == Globals.aFaceColors[1] && Globals.aPieces[40] == Globals.aFaceColors[5])
            {
                if (Globals.aPieces[13] != Globals.aFaceColors[2] || Globals.aPieces[22] != Globals.aFaceColors[3] || Globals.aPieces[31] != Globals.aFaceColors[4] || Globals.aPieces[49] != Globals.aFaceColors[6])
                {
                    return false;
                }
            }

            // Front face Blue and Up face White
            if (Globals.aPieces[4] == Globals.aFaceColors[2] && Globals.aPieces[40] == Globals.aFaceColors[5])
            {
                if (Globals.aPieces[13] != Globals.aFaceColors[3] || Globals.aPieces[22] != Globals.aFaceColors[4] || Globals.aPieces[31] != Globals.aFaceColors[1] || Globals.aPieces[49] != Globals.aFaceColors[6])
                {
                    return false;
                }
            }

            // Front face Orange and Up face White
            if (Globals.aPieces[4] == Globals.aFaceColors[3] && Globals.aPieces[40] == Globals.aFaceColors[5])
            {
                if (Globals.aPieces[13] != Globals.aFaceColors[4] || Globals.aPieces[22] != Globals.aFaceColors[1] || Globals.aPieces[31] != Globals.aFaceColors[2] || Globals.aPieces[49] != Globals.aFaceColors[6])
                {
                    return false;
                }
            }

            // Front face Green and Up face White
            if (Globals.aPieces[4] == Globals.aFaceColors[4] && Globals.aPieces[40] == Globals.aFaceColors[5])
            {
                if (Globals.aPieces[13] != Globals.aFaceColors[1] || Globals.aPieces[22] != Globals.aFaceColors[2] || Globals.aPieces[31] != Globals.aFaceColors[3] || Globals.aPieces[49] != Globals.aFaceColors[6])
                {
                    return false;
                }
            }

            // Front face White and Up face Red
            if (Globals.aPieces[4] == Globals.aFaceColors[5] && Globals.aPieces[40] == Globals.aFaceColors[1])
            {
                if (Globals.aPieces[13] != Globals.aFaceColors[4] || Globals.aPieces[22] != Globals.aFaceColors[6] || Globals.aPieces[31] != Globals.aFaceColors[2] || Globals.aPieces[49] != Globals.aFaceColors[3])
                {
                    return false;
                }
            }

            // Front face Green and Up face Red
            if (Globals.aPieces[4] == Globals.aFaceColors[4] && Globals.aPieces[40] == Globals.aFaceColors[1])
            {
                if (Globals.aPieces[13] != Globals.aFaceColors[6] || Globals.aPieces[22] != Globals.aFaceColors[2] || Globals.aPieces[31] != Globals.aFaceColors[5] || Globals.aPieces[49] != Globals.aFaceColors[3])
                {
                    return false;
                }
            }

            // Front face Yellow and Up face Red
            if (Globals.aPieces[4] == Globals.aFaceColors[6] && Globals.aPieces[40] == Globals.aFaceColors[1])
            {
                if (Globals.aPieces[13] != Globals.aFaceColors[2] || Globals.aPieces[22] != Globals.aFaceColors[5] || Globals.aPieces[31] != Globals.aFaceColors[4] || Globals.aPieces[49] != Globals.aFaceColors[3])
                {
                    return false;
                }
            }

            // Front face Blue and Up face Red
            if (Globals.aPieces[4] == Globals.aFaceColors[2] && Globals.aPieces[40] == Globals.aFaceColors[1])
            {
                if (Globals.aPieces[13] != Globals.aFaceColors[5] || Globals.aPieces[22] != Globals.aFaceColors[4] || Globals.aPieces[31] != Globals.aFaceColors[6] || Globals.aPieces[49] != Globals.aFaceColors[3])
                {
                    return false;
                }
            }

            // Front face White and Up face Green
            if (Globals.aPieces[4] == Globals.aFaceColors[5] && Globals.aPieces[40] == Globals.aFaceColors[4])
            {
                if (Globals.aPieces[13] != Globals.aFaceColors[3] || Globals.aPieces[22] != Globals.aFaceColors[6] || Globals.aPieces[31] != Globals.aFaceColors[1] || Globals.aPieces[49] != Globals.aFaceColors[2])
                {
                    return false;
                }
            }

            // Front face Orange and Up face Green
            if (Globals.aPieces[4] == Globals.aFaceColors[3] && Globals.aPieces[40] == Globals.aFaceColors[4])
            {
                if (Globals.aPieces[13] != Globals.aFaceColors[6] || Globals.aPieces[22] != Globals.aFaceColors[1] || Globals.aPieces[31] != Globals.aFaceColors[5] || Globals.aPieces[49] != Globals.aFaceColors[2])
                {
                    return false;
                }
            }

            // Front face Yellow and Up face Green
            if (Globals.aPieces[4] == Globals.aFaceColors[6] && Globals.aPieces[40] == Globals.aFaceColors[4])
            {
                if (Globals.aPieces[13] != Globals.aFaceColors[1] || Globals.aPieces[22] != Globals.aFaceColors[5] || Globals.aPieces[31] != Globals.aFaceColors[3] || Globals.aPieces[49] != Globals.aFaceColors[2])
                {
                    return false;
                }
            }

            // Front face Red and Up face Green
            if (Globals.aPieces[4] == Globals.aFaceColors[1] && Globals.aPieces[40] == Globals.aFaceColors[4])
            {
                if (Globals.aPieces[13] != Globals.aFaceColors[5] || Globals.aPieces[22] != Globals.aFaceColors[3] || Globals.aPieces[31] != Globals.aFaceColors[6] || Globals.aPieces[49] != Globals.aFaceColors[2])
                {
                    return false;
                }
            }

            // Front face White and Up face Orange
            if (Globals.aPieces[4] == Globals.aFaceColors[5] && Globals.aPieces[40] == Globals.aFaceColors[3])
            {
                if (Globals.aPieces[13] != Globals.aFaceColors[2] || Globals.aPieces[22] != Globals.aFaceColors[6] || Globals.aPieces[31] != Globals.aFaceColors[4] || Globals.aPieces[49] != Globals.aFaceColors[1])
                {
                    return false;
                }
            }

            // Front face Blue and Up face Orange
            if (Globals.aPieces[4] == Globals.aFaceColors[2] && Globals.aPieces[40] == Globals.aFaceColors[3])
            {
                if (Globals.aPieces[13] != Globals.aFaceColors[6] || Globals.aPieces[22] != Globals.aFaceColors[4] || Globals.aPieces[31] != Globals.aFaceColors[5] || Globals.aPieces[49] != Globals.aFaceColors[1])
                {
                    return false;
                }
            }

            // Front face Yellow and Up face Orange
            if (Globals.aPieces[4] == Globals.aFaceColors[6] && Globals.aPieces[40] == Globals.aFaceColors[3])
            {
                if (Globals.aPieces[13] != Globals.aFaceColors[4] || Globals.aPieces[22] != Globals.aFaceColors[5] || Globals.aPieces[31] != Globals.aFaceColors[2] || Globals.aPieces[49] != Globals.aFaceColors[1])
                {
                    return false;
                }
            }

            // Front face Green and Up face Orange
            if (Globals.aPieces[4] == Globals.aFaceColors[4] && Globals.aPieces[40] == Globals.aFaceColors[3])
            {
                if (Globals.aPieces[13] != Globals.aFaceColors[5] || Globals.aPieces[22] != Globals.aFaceColors[2] || Globals.aPieces[31] != Globals.aFaceColors[6] || Globals.aPieces[49] != Globals.aFaceColors[1])
                {
                    return false;
                }
            }

            // Front face White and Up face Blue
            if (Globals.aPieces[4] == Globals.aFaceColors[5] && Globals.aPieces[40] == Globals.aFaceColors[2])
            {
                if (Globals.aPieces[13] != Globals.aFaceColors[1] || Globals.aPieces[22] != Globals.aFaceColors[6] || Globals.aPieces[31] != Globals.aFaceColors[3] || Globals.aPieces[49] != Globals.aFaceColors[4])
                {
                    return false;
                }
            }

            // Front face Red and Up face Blue
            if (Globals.aPieces[4] == Globals.aFaceColors[1] && Globals.aPieces[40] == Globals.aFaceColors[2])
            {
                if (Globals.aPieces[13] != Globals.aFaceColors[6] || Globals.aPieces[22] != Globals.aFaceColors[3] || Globals.aPieces[31] != Globals.aFaceColors[5] || Globals.aPieces[49] != Globals.aFaceColors[4])
                {
                    return false;
                }
            }

            // Front face Yellow and Up face Blue
            if (Globals.aPieces[4] == Globals.aFaceColors[6] && Globals.aPieces[40] == Globals.aFaceColors[2])
            {
                if (Globals.aPieces[13] != Globals.aFaceColors[3] || Globals.aPieces[22] != Globals.aFaceColors[5] || Globals.aPieces[31] != Globals.aFaceColors[1] || Globals.aPieces[49] != Globals.aFaceColors[4])
                {
                    return false;
                }
            }

            // Front face Orange and Up face Blue
            if (Globals.aPieces[4] == Globals.aFaceColors[3] && Globals.aPieces[40] == Globals.aFaceColors[2])
            {
                if (Globals.aPieces[13] != Globals.aFaceColors[5] || Globals.aPieces[22] != Globals.aFaceColors[1] || Globals.aPieces[31] != Globals.aFaceColors[6] || Globals.aPieces[49] != Globals.aFaceColors[4])
                {
                    return false;
                }
            }

            // Front face Red and Up face Yellow
            if (Globals.aPieces[4] == Globals.aFaceColors[1] && Globals.aPieces[40] == Globals.aFaceColors[6])
            {
                if (Globals.aPieces[13] != Globals.aFaceColors[4] || Globals.aPieces[22] != Globals.aFaceColors[3] || Globals.aPieces[31] != Globals.aFaceColors[2] || Globals.aPieces[49] != Globals.aFaceColors[5])
                {
                    return false;
                }
            }

            // Front face Green and Up face Yellow
            if (Globals.aPieces[4] == Globals.aFaceColors[4] && Globals.aPieces[40] == Globals.aFaceColors[6])
            {
                if (Globals.aPieces[13] != Globals.aFaceColors[3] || Globals.aPieces[22] != Globals.aFaceColors[2] || Globals.aPieces[31] != Globals.aFaceColors[1] || Globals.aPieces[49] != Globals.aFaceColors[5])
                {
                    return false;
                }
            }

            // Front face Orange and Up face Yellow
            if (Globals.aPieces[4] == Globals.aFaceColors[3] && Globals.aPieces[40] == Globals.aFaceColors[6])
            {
                if (Globals.aPieces[13] != Globals.aFaceColors[2] || Globals.aPieces[22] != Globals.aFaceColors[1] || Globals.aPieces[31] != Globals.aFaceColors[4] || Globals.aPieces[49] != Globals.aFaceColors[5])
                {
                    return false;
                }
            }

            // Front face Blue and Up face Yellow
            if (Globals.aPieces[4] == Globals.aFaceColors[2] && Globals.aPieces[40] == Globals.aFaceColors[6])
            {
                if (Globals.aPieces[13] != Globals.aFaceColors[1] || Globals.aPieces[22] != Globals.aFaceColors[4] || Globals.aPieces[31] != Globals.aFaceColors[3] || Globals.aPieces[49] != Globals.aFaceColors[5])
                {
                    return false;
                }
            }

            return true;
        }


        //// Check the number of colors on the corner cubes
        private static void CheckNumberColorsCornerCube(int[] aNumberOfColors, int nColor)
        {
            // Up face
            if (Globals.aPieces[0] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[2] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[6] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[8] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;

            // Right face
            if (Globals.aPieces[9] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[11] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[15] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[17] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;

            // Back face
            if (Globals.aPieces[18] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[20] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[24] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[26] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;

            // Left face
            if (Globals.aPieces[27] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[29] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[33] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[35] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;

            // Up face
            if (Globals.aPieces[36] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[38] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[42] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[44] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;

            // Down face
            if (Globals.aPieces[45] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[47] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[51] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[53] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
        }

        //// Check the number of colors on the edge cubes
        private static void CheckNumberColorsEdgeCube(int[] aNumberOfColors, int nColor)
        {
            // Up face
            if (Globals.aPieces[1] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[3] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[5] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[7] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;

            // Right face
            if (Globals.aPieces[10] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[12] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[14] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[16] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;

            // Back face
            if (Globals.aPieces[19] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[21] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[23] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[25] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;

            // Left face
            if (Globals.aPieces[28] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[30] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[32] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[34] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;

            // Up face
            if (Globals.aPieces[37] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[39] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[41] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[43] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;

            // Down face
            if (Globals.aPieces[46] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[48] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[50] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
            if (Globals.aPieces[52] == Globals.aFaceColors[nColor])
                aNumberOfColors[nColor]++;
        }

        //// Check if the cube is solved
        public static bool CheckIfSolved()
        {
            bool bColorsUp = false;
            bool bColorsFront = false;
            bool bColorsRight = false;
            bool bColorsLeft = false;
            bool bColorsBack = false;
            bool bColorsDown = false;

            if (Globals.aPieces[36] == Globals.aPieces[37] && Globals.aPieces[36] == Globals.aPieces[38] && Globals.aPieces[36] == Globals.aPieces[39] && Globals.aPieces[36] == Globals.aPieces[40] && Globals.aPieces[36] == Globals.aPieces[41] && Globals.aPieces[36] == Globals.aPieces[42] && Globals.aPieces[36] == Globals.aPieces[43] && Globals.aPieces[36] == Globals.aPieces[44])
                bColorsUp = true;

            if (Globals.aPieces[0] == Globals.aPieces[1] && Globals.aPieces[0] == Globals.aPieces[2] && Globals.aPieces[0] == Globals.aPieces[3] && Globals.aPieces[0] == Globals.aPieces[4] && Globals.aPieces[0] == Globals.aPieces[5] && Globals.aPieces[0] == Globals.aPieces[6] && Globals.aPieces[0] == Globals.aPieces[7] && Globals.aPieces[0] == Globals.aPieces[8])
                bColorsFront = true;

            if (Globals.aPieces[9] == Globals.aPieces[10] && Globals.aPieces[9] == Globals.aPieces[11] && Globals.aPieces[9] == Globals.aPieces[12] && Globals.aPieces[9] == Globals.aPieces[13] && Globals.aPieces[9] == Globals.aPieces[14] && Globals.aPieces[9] == Globals.aPieces[15] && Globals.aPieces[9] == Globals.aPieces[16] && Globals.aPieces[9] == Globals.aPieces[17])
                bColorsRight = true;

            if (Globals.aPieces[27] == Globals.aPieces[28] && Globals.aPieces[27] == Globals.aPieces[29] && Globals.aPieces[27] == Globals.aPieces[30] && Globals.aPieces[27] == Globals.aPieces[31] && Globals.aPieces[27] == Globals.aPieces[32] && Globals.aPieces[27] == Globals.aPieces[33] && Globals.aPieces[27] == Globals.aPieces[34] && Globals.aPieces[27] == Globals.aPieces[35])
                bColorsLeft = true;

            if (Globals.aPieces[18] == Globals.aPieces[19] && Globals.aPieces[18] == Globals.aPieces[20] && Globals.aPieces[18] == Globals.aPieces[21] && Globals.aPieces[18] == Globals.aPieces[22] && Globals.aPieces[18] == Globals.aPieces[23] && Globals.aPieces[18] == Globals.aPieces[24] && Globals.aPieces[18] == Globals.aPieces[25] && Globals.aPieces[18] == Globals.aPieces[26])
                bColorsBack = true;

            if (Globals.aPieces[45] == Globals.aPieces[46] && Globals.aPieces[45] == Globals.aPieces[47] && Globals.aPieces[45] == Globals.aPieces[48] && Globals.aPieces[45] == Globals.aPieces[49] && Globals.aPieces[45] == Globals.aPieces[50] && Globals.aPieces[45] == Globals.aPieces[51] && Globals.aPieces[45] == Globals.aPieces[52] && Globals.aPieces[45] == Globals.aPieces[53])
                bColorsDown = true;

            if (!bColorsUp || !bColorsFront || !bColorsRight || !bColorsLeft || !bColorsBack || !bColorsDown)
                return false;

            return true;
        }

        //// Reset the colors of the cube
        public static void ResetCube()
        {
            int nItem;

            for (nItem = 0; nItem < 9; nItem++)
            {
                Globals.aPieces[nItem] = Globals.aFaceColors[1];
            }

            for (nItem = 9; nItem < 18; nItem++)
            {
                Globals.aPieces[nItem] = Globals.aFaceColors[2];
            }

            for (nItem = 18; nItem < 27; nItem++)
            {
                Globals.aPieces[nItem] = Globals.aFaceColors[3];
            }

            for (nItem = 27; nItem < 36; nItem++)
            {
                Globals.aPieces[nItem] = Globals.aFaceColors[4];
            }

            for (nItem = 36; nItem < 45; nItem++)
            {
                Globals.aPieces[nItem] = Globals.aFaceColors[5];
            }

            for (nItem = 45; nItem < 54; nItem++)
            {
                Globals.aPieces[nItem] = Globals.aFaceColors[6];
            }
        }
    }
}
