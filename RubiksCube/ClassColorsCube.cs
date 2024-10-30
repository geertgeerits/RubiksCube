using static RubiksCube.Globals;

namespace RubiksCube
{
    internal sealed class ClassColorsCube
    {
        /// <summary>
        /// Check the number of colors of the cube
        /// </summary>
        /// <returns></returns>
        public static bool CheckNumberColors()
        {
            // Create a span for the arrays
            ReadOnlySpan<string> aFaceColorsSpan = aFaceColors.AsSpan();
            ReadOnlySpan<string> aPiecesSpan = aPieces.AsSpan();

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
                if (aPiecesSpan[nItem] == aFaceColorsSpan[1])
                {
                    nNumberOfColors1++;
                }
            }

            for (nItem = 0; nItem < 9; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[2])
                {
                    nNumberOfColors2++;
                }
            }

            for (nItem = 0; nItem < 9; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[3])
                {
                    nNumberOfColors3++;
                }
            }

            for (nItem = 0; nItem < 9; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[4])
                {
                    nNumberOfColors4++;
                }
            }

            for (nItem = 0; nItem < 9; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[5])
                {
                    nNumberOfColors5++;
                }
            }

            for (nItem = 0; nItem < 9; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[6])
                {
                    nNumberOfColors6++;
                }
            }

            // Right face
            for (nItem = 9; nItem < 18; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[1])
                {
                    nNumberOfColors1++;
                }
            }

            for (nItem = 9; nItem < 18; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[2])
                {
                    nNumberOfColors2++;
                }
            }

            for (nItem = 9; nItem < 18; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[3])
                {
                    nNumberOfColors3++;
                }
            }

            for (nItem = 9; nItem < 18; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[4])
                {
                    nNumberOfColors4++;
                }
            }

            for (nItem = 9; nItem < 18; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[5])
                {
                    nNumberOfColors5++;
                }
            }

            for (nItem = 9; nItem < 18; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[6])
                {
                    nNumberOfColors6++;
                }
            }

            // Back face
            for (nItem = 18; nItem < 27; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[1])
                {
                    nNumberOfColors1++;
                }
            }

            for (nItem = 18; nItem < 27; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[2])
                {
                    nNumberOfColors2++;
                }
            }

            for (nItem = 18; nItem < 27; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[3])
                {
                    nNumberOfColors3++;
                }
            }

            for (nItem = 18; nItem < 27; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[4])
                {
                    nNumberOfColors4++;
                }
            }

            for (nItem = 18; nItem < 27; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[5])
                {
                    nNumberOfColors5++;
                }
            }

            for (nItem = 18; nItem < 27; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[6])
                {
                    nNumberOfColors6++;
                }
            }

            // Left face
            for (nItem = 27; nItem < 36; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[1])
                {
                    nNumberOfColors1++;
                }
            }

            for (nItem = 27; nItem < 36; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[2])
                {
                    nNumberOfColors2++;
                }
            }

            for (nItem = 27; nItem < 36; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[3])
                {
                    nNumberOfColors3++;
                }
            }

            for (nItem = 27; nItem < 36; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[4])
                {
                    nNumberOfColors4++;
                }
            }

            for (nItem = 27; nItem < 36; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[5])
                {
                    nNumberOfColors5++;
                }
            }

            for (nItem = 27; nItem < 36; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[6])
                {
                    nNumberOfColors6++;
                }
            }

            // Top layer
            for (nItem = 36; nItem < 45; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[1])
                {
                    nNumberOfColors1++;
                }
            }

            for (nItem = 36; nItem < 45; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[2])
                {
                    nNumberOfColors2++;
                }
            }

            for (nItem = 36; nItem < 45; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[3])
                {
                    nNumberOfColors3++;
                }
            }

            for (nItem = 36; nItem < 45; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[4])
                {
                    nNumberOfColors4++;
                }
            }

            for (nItem = 36; nItem < 45; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[5])
                {
                    nNumberOfColors5++;
                }
            }

            for (nItem = 36; nItem < 45; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[6])
                {
                    nNumberOfColors6++;
                }
            }

            // Bottom layer
            for (nItem = 45; nItem < 54; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[1])
                {
                    nNumberOfColors1++;
                }
            }

            for (nItem = 45; nItem < 54; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[2])
                {
                    nNumberOfColors2++;
                }
            }

            for (nItem = 45; nItem < 54; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[3])
                {
                    nNumberOfColors3++;
                }
            }

            for (nItem = 45; nItem < 54; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[4])
                {
                    nNumberOfColors4++;
                }
            }

            for (nItem = 45; nItem < 54; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[5])
                {
                    nNumberOfColors5++;
                }
            }

            for (nItem = 45; nItem < 54; nItem++)
            {
                if (aPiecesSpan[nItem] == aFaceColorsSpan[6])
                {
                    nNumberOfColors6++;
                }
            }

            if (nNumberOfColors1 != 9 || nNumberOfColors2 != 9 || nNumberOfColors3 != 9 || nNumberOfColors4 != 9 || nNumberOfColors5 != 9 || nNumberOfColors6 != 9)
            {
                _ = Application.Current!.MainPage!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageNineSameColor_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            //// Check the number of colors of the central square of the cube
            bool bColorCenterCube = true;

            if (aPiecesSpan[40] == aPiecesSpan[4] || aPiecesSpan[40] == aPiecesSpan[13] || aPiecesSpan[40] == aPiecesSpan[31] || aPiecesSpan[40] == aPiecesSpan[22] || aPiecesSpan[40] == aPiecesSpan[49])
            {
                bColorCenterCube = false;
            }

            if (aPiecesSpan[4] == aPiecesSpan[40] || aPiecesSpan[4] == aPiecesSpan[13] || aPiecesSpan[4] == aPiecesSpan[31] || aPiecesSpan[4] == aPiecesSpan[22] || aPiecesSpan[4] == aPiecesSpan[49])
            {
                bColorCenterCube = false;
            }

            if (aPiecesSpan[13] == aPiecesSpan[4] || aPiecesSpan[13] == aPiecesSpan[40] || aPiecesSpan[13] == aPiecesSpan[31] || aPiecesSpan[13] == aPiecesSpan[22] || aPiecesSpan[13] == aPiecesSpan[49])
            {
                bColorCenterCube = false;
            }

            if (aPiecesSpan[31] == aPiecesSpan[4] || aPiecesSpan[31] == aPiecesSpan[13] || aPiecesSpan[31] == aPiecesSpan[40] || aPiecesSpan[31] == aPiecesSpan[22] || aPiecesSpan[31] == aPiecesSpan[49])
            {
                bColorCenterCube = false;
            }

            if (aPiecesSpan[22] == aPiecesSpan[4] || aPiecesSpan[22] == aPiecesSpan[13] || aPiecesSpan[22] == aPiecesSpan[31] || aPiecesSpan[22] == aPiecesSpan[40] || aPiecesSpan[22] == aPiecesSpan[49])
            {
                bColorCenterCube = false;
            }

            if (aPiecesSpan[49] == aPiecesSpan[4] || aPiecesSpan[49] == aPiecesSpan[13] || aPiecesSpan[49] == aPiecesSpan[31] || aPiecesSpan[49] == aPiecesSpan[22] || aPiecesSpan[49] == aPiecesSpan[40])
            {
                bColorCenterCube = false;
            }

            if (!bColorCenterCube)
            {
                _ = Application.Current!.MainPage!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCentralCube_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            //// Check the number of colors of the corner cubes of the cube if there are no corner cubes with the same color
            bool bColorCornerCube = true;

            if (aPiecesSpan[42] == aPiecesSpan[29] || aPiecesSpan[42] == aPiecesSpan[0] || aPiecesSpan[0] == aPiecesSpan[29])
            {
                bColorCornerCube = false;
            }

            if (aPiecesSpan[36] == aPiecesSpan[27] || aPiecesSpan[36] == aPiecesSpan[20] || aPiecesSpan[27] == aPiecesSpan[20])
            {
                bColorCornerCube = false;
            }

            if (aPiecesSpan[38] == aPiecesSpan[11] || aPiecesSpan[38] == aPiecesSpan[18] || aPiecesSpan[11] == aPiecesSpan[18])
            {
                bColorCornerCube = false;
            }

            if (aPiecesSpan[44] == aPiecesSpan[2] || aPiecesSpan[44] == aPiecesSpan[9] || aPiecesSpan[2] == aPiecesSpan[9])
            {
                bColorCornerCube = false;
            }

            if (aPiecesSpan[45] == aPiecesSpan[35] || aPiecesSpan[45] == aPiecesSpan[6] || aPiecesSpan[6] == aPiecesSpan[35])
            {
                bColorCornerCube = false;
            }

            if (aPiecesSpan[51] == aPiecesSpan[33] || aPiecesSpan[51] == aPiecesSpan[26] || aPiecesSpan[26] == aPiecesSpan[33])
            {
                bColorCornerCube = false;
            }

            if (aPiecesSpan[53] == aPiecesSpan[17] || aPiecesSpan[53] == aPiecesSpan[24] || aPiecesSpan[24] == aPiecesSpan[17])
            {
                bColorCornerCube = false;
            }

            if (aPiecesSpan[47] == aPiecesSpan[15] || aPiecesSpan[47] == aPiecesSpan[8] || aPiecesSpan[8] == aPiecesSpan[15])
            {
                bColorCornerCube = false;
            }

            if (!bColorCornerCube)
            {
                _ = Application.Current!.MainPage!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCornerCube_Text, CubeLang.ButtonClose_Text);
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
                _ = Application.Current!.MainPage!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageFourSameColorCornerCube_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            //// Check the number of colors of the edge cubes if there are no edge cubes with the same color
            bColorCornerCube = true;

            if (aPiecesSpan[37] == aPiecesSpan[19] || aPiecesSpan[39] == aPiecesSpan[28] || aPiecesSpan[41] == aPiecesSpan[10] || aPiecesSpan[43] == aPiecesSpan[1])
            {
                bColorCornerCube = false;
            }

            if (aPiecesSpan[46] == aPiecesSpan[7] || aPiecesSpan[48] == aPiecesSpan[34] || aPiecesSpan[50] == aPiecesSpan[16] || aPiecesSpan[52] == aPiecesSpan[25])
            {
                bColorCornerCube = false;
            }

            if (!bColorCornerCube)
            {
                _ = Application.Current!.MainPage!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorEdgeCube_Text, CubeLang.ButtonClose_Text);
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
                _ = Application.Current!.MainPage!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageFourSameColorEdgeCube_Text, CubeLang.ButtonClose_Text);
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
                _ = Application.Current!.MainPage!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCenterPiece_Text, CubeLang.ButtonClose_Text);
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
                _ = Application.Current!.MainPage!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCenterPiece_Text, CubeLang.ButtonClose_Text);
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
                _ = Application.Current!.MainPage!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCenterPiece_Text, CubeLang.ButtonClose_Text);
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
                _ = Application.Current!.MainPage!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCenterPiece_Text, CubeLang.ButtonClose_Text);
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
                _ = Application.Current!.MainPage!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCenterPiece_Text, CubeLang.ButtonClose_Text);
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
                _ = Application.Current!.MainPage!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCenterPiece_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            // Check the neighbors and opposite center pieces of the cube
            if (!CheckNeighborsCenterPieces())
            {
                _ = Application.Current!.MainPage!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCenterPiece_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check the opposite center pieces of the cube
        /// </summary>
        /// <param name="nPiece1"></param>
        /// <param name="nColor1"></param>
        /// <param name="nPiece2"></param>
        /// <param name="nColor2"></param>
        /// <returns></returns>
        private static bool CheckOppositeCenterPieces(int nPiece1, int nColor1, int nPiece2, int nColor2)
        {
            // Create a span for the arrays
            ReadOnlySpan<string> aFaceColorsSpan = aFaceColors.AsSpan();
            ReadOnlySpan<string> aPiecesSpan = aPieces.AsSpan();

            if (aPiecesSpan[nPiece1] == aFaceColorsSpan[nColor1] && aPiecesSpan[nPiece2] != aFaceColorsSpan[nColor2])
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check the neighbors and opposite center pieces of the cube
        /// </summary>
        /// <returns></returns>
        private static bool CheckNeighborsCenterPieces()
        {
            /* aFaceColorsSpan[1] -> Front face: Red 
               aFaceColorsSpan[2] -> Right face: Blue 
               aFaceColorsSpan[3] -> Back face: Orange 
               aFaceColorsSpan[4] -> Left face: Green 
               aFaceColorsSpan[5] -> Up face: White 
               aFaceColorsSpan[6] -> Down face: Yellow */

            // Create a span for the arrays
            ReadOnlySpan<string> aFaceColorsSpan = aFaceColors.AsSpan();
            ReadOnlySpan<string> aPiecesSpan = aPieces.AsSpan();

            // Front face Red and Up face White
            if (aPiecesSpan[4] == aFaceColorsSpan[1] && aPiecesSpan[40] == aFaceColorsSpan[5])
            {
                if (aPiecesSpan[13] != aFaceColorsSpan[2] || aPiecesSpan[22] != aFaceColorsSpan[3] || aPiecesSpan[31] != aFaceColorsSpan[4] || aPiecesSpan[49] != aFaceColorsSpan[6])
                {
                    return false;
                }
            }

            // Front face Blue and Up face White
            if (aPiecesSpan[4] == aFaceColorsSpan[2] && aPiecesSpan[40] == aFaceColorsSpan[5])
            {
                if (aPiecesSpan[13] != aFaceColorsSpan[3] || aPiecesSpan[22] != aFaceColorsSpan[4] || aPiecesSpan[31] != aFaceColorsSpan[1] || aPiecesSpan[49] != aFaceColorsSpan[6])
                {
                    return false;
                }
            }

            // Front face Orange and Up face White
            if (aPiecesSpan[4] == aFaceColorsSpan[3] && aPiecesSpan[40] == aFaceColorsSpan[5])
            {
                if (aPiecesSpan[13] != aFaceColorsSpan[4] || aPiecesSpan[22] != aFaceColorsSpan[1] || aPiecesSpan[31] != aFaceColorsSpan[2] || aPiecesSpan[49] != aFaceColorsSpan[6])
                {
                    return false;
                }
            }

            // Front face Green and Up face White
            if (aPiecesSpan[4] == aFaceColorsSpan[4] && aPiecesSpan[40] == aFaceColorsSpan[5])
            {
                if (aPiecesSpan[13] != aFaceColorsSpan[1] || aPiecesSpan[22] != aFaceColorsSpan[2] || aPiecesSpan[31] != aFaceColorsSpan[3] || aPiecesSpan[49] != aFaceColorsSpan[6])
                {
                    return false;
                }
            }

            // Front face White and Up face Red
            if (aPiecesSpan[4] == aFaceColorsSpan[5] && aPiecesSpan[40] == aFaceColorsSpan[1])
            {
                if (aPiecesSpan[13] != aFaceColorsSpan[4] || aPiecesSpan[22] != aFaceColorsSpan[6] || aPiecesSpan[31] != aFaceColorsSpan[2] || aPiecesSpan[49] != aFaceColorsSpan[3])
                {
                    return false;
                }
            }

            // Front face Green and Up face Red
            if (aPiecesSpan[4] == aFaceColorsSpan[4] && aPiecesSpan[40] == aFaceColorsSpan[1])
            {
                if (aPiecesSpan[13] != aFaceColorsSpan[6] || aPiecesSpan[22] != aFaceColorsSpan[2] || aPiecesSpan[31] != aFaceColorsSpan[5] || aPiecesSpan[49] != aFaceColorsSpan[3])
                {
                    return false;
                }
            }

            // Front face Yellow and Up face Red
            if (aPiecesSpan[4] == aFaceColorsSpan[6] && aPiecesSpan[40] == aFaceColorsSpan[1])
            {
                if (aPiecesSpan[13] != aFaceColorsSpan[2] || aPiecesSpan[22] != aFaceColorsSpan[5] || aPiecesSpan[31] != aFaceColorsSpan[4] || aPiecesSpan[49] != aFaceColorsSpan[3])
                {
                    return false;
                }
            }

            // Front face Blue and Up face Red
            if (aPiecesSpan[4] == aFaceColorsSpan[2] && aPiecesSpan[40] == aFaceColorsSpan[1])
            {
                if (aPiecesSpan[13] != aFaceColorsSpan[5] || aPiecesSpan[22] != aFaceColorsSpan[4] || aPiecesSpan[31] != aFaceColorsSpan[6] || aPiecesSpan[49] != aFaceColorsSpan[3])
                {
                    return false;
                }
            }

            // Front face White and Up face Green
            if (aPiecesSpan[4] == aFaceColorsSpan[5] && aPiecesSpan[40] == aFaceColorsSpan[4])
            {
                if (aPiecesSpan[13] != aFaceColorsSpan[3] || aPiecesSpan[22] != aFaceColorsSpan[6] || aPiecesSpan[31] != aFaceColorsSpan[1] || aPiecesSpan[49] != aFaceColorsSpan[2])
                {
                    return false;
                }
            }

            // Front face Orange and Up face Green
            if (aPiecesSpan[4] == aFaceColorsSpan[3] && aPiecesSpan[40] == aFaceColorsSpan[4])
            {
                if (aPiecesSpan[13] != aFaceColorsSpan[6] || aPiecesSpan[22] != aFaceColorsSpan[1] || aPiecesSpan[31] != aFaceColorsSpan[5] || aPiecesSpan[49] != aFaceColorsSpan[2])
                {
                    return false;
                }
            }

            // Front face Yellow and Up face Green
            if (aPiecesSpan[4] == aFaceColorsSpan[6] && aPiecesSpan[40] == aFaceColorsSpan[4])
            {
                if (aPiecesSpan[13] != aFaceColorsSpan[1] || aPiecesSpan[22] != aFaceColorsSpan[5] || aPiecesSpan[31] != aFaceColorsSpan[3] || aPiecesSpan[49] != aFaceColorsSpan[2])
                {
                    return false;
                }
            }

            // Front face Red and Up face Green
            if (aPiecesSpan[4] == aFaceColorsSpan[1] && aPiecesSpan[40] == aFaceColorsSpan[4])
            {
                if (aPiecesSpan[13] != aFaceColorsSpan[5] || aPiecesSpan[22] != aFaceColorsSpan[3] || aPiecesSpan[31] != aFaceColorsSpan[6] || aPiecesSpan[49] != aFaceColorsSpan[2])
                {
                    return false;
                }
            }

            // Front face White and Up face Orange
            if (aPiecesSpan[4] == aFaceColorsSpan[5] && aPiecesSpan[40] == aFaceColorsSpan[3])
            {
                if (aPiecesSpan[13] != aFaceColorsSpan[2] || aPiecesSpan[22] != aFaceColorsSpan[6] || aPiecesSpan[31] != aFaceColorsSpan[4] || aPiecesSpan[49] != aFaceColorsSpan[1])
                {
                    return false;
                }
            }

            // Front face Blue and Up face Orange
            if (aPiecesSpan[4] == aFaceColorsSpan[2] && aPiecesSpan[40] == aFaceColorsSpan[3])
            {
                if (aPiecesSpan[13] != aFaceColorsSpan[6] || aPiecesSpan[22] != aFaceColorsSpan[4] || aPiecesSpan[31] != aFaceColorsSpan[5] || aPiecesSpan[49] != aFaceColorsSpan[1])
                {
                    return false;
                }
            }

            // Front face Yellow and Up face Orange
            if (aPiecesSpan[4] == aFaceColorsSpan[6] && aPiecesSpan[40] == aFaceColorsSpan[3])
            {
                if (aPiecesSpan[13] != aFaceColorsSpan[4] || aPiecesSpan[22] != aFaceColorsSpan[5] || aPiecesSpan[31] != aFaceColorsSpan[2] || aPiecesSpan[49] != aFaceColorsSpan[1])
                {
                    return false;
                }
            }

            // Front face Green and Up face Orange
            if (aPiecesSpan[4] == aFaceColorsSpan[4] && aPiecesSpan[40] == aFaceColorsSpan[3])
            {
                if (aPiecesSpan[13] != aFaceColorsSpan[5] || aPiecesSpan[22] != aFaceColorsSpan[2] || aPiecesSpan[31] != aFaceColorsSpan[6] || aPiecesSpan[49] != aFaceColorsSpan[1])
                {
                    return false;
                }
            }

            // Front face White and Up face Blue
            if (aPiecesSpan[4] == aFaceColorsSpan[5] && aPiecesSpan[40] == aFaceColorsSpan[2])
            {
                if (aPiecesSpan[13] != aFaceColorsSpan[1] || aPiecesSpan[22] != aFaceColorsSpan[6] || aPiecesSpan[31] != aFaceColorsSpan[3] || aPiecesSpan[49] != aFaceColorsSpan[4])
                {
                    return false;
                }
            }

            // Front face Red and Up face Blue
            if (aPiecesSpan[4] == aFaceColorsSpan[1] && aPiecesSpan[40] == aFaceColorsSpan[2])
            {
                if (aPiecesSpan[13] != aFaceColorsSpan[6] || aPiecesSpan[22] != aFaceColorsSpan[3] || aPiecesSpan[31] != aFaceColorsSpan[5] || aPiecesSpan[49] != aFaceColorsSpan[4])
                {
                    return false;
                }
            }

            // Front face Yellow and Up face Blue
            if (aPiecesSpan[4] == aFaceColorsSpan[6] && aPiecesSpan[40] == aFaceColorsSpan[2])
            {
                if (aPiecesSpan[13] != aFaceColorsSpan[3] || aPiecesSpan[22] != aFaceColorsSpan[5] || aPiecesSpan[31] != aFaceColorsSpan[1] || aPiecesSpan[49] != aFaceColorsSpan[4])
                {
                    return false;
                }
            }

            // Front face Orange and Up face Blue
            if (aPiecesSpan[4] == aFaceColorsSpan[3] && aPiecesSpan[40] == aFaceColorsSpan[2])
            {
                if (aPiecesSpan[13] != aFaceColorsSpan[5] || aPiecesSpan[22] != aFaceColorsSpan[1] || aPiecesSpan[31] != aFaceColorsSpan[6] || aPiecesSpan[49] != aFaceColorsSpan[4])
                {
                    return false;
                }
            }

            // Front face Red and Up face Yellow
            if (aPiecesSpan[4] == aFaceColorsSpan[1] && aPiecesSpan[40] == aFaceColorsSpan[6])
            {
                if (aPiecesSpan[13] != aFaceColorsSpan[4] || aPiecesSpan[22] != aFaceColorsSpan[3] || aPiecesSpan[31] != aFaceColorsSpan[2] || aPiecesSpan[49] != aFaceColorsSpan[5])
                {
                    return false;
                }
            }

            // Front face Green and Up face Yellow
            if (aPiecesSpan[4] == aFaceColorsSpan[4] && aPiecesSpan[40] == aFaceColorsSpan[6])
            {
                if (aPiecesSpan[13] != aFaceColorsSpan[3] || aPiecesSpan[22] != aFaceColorsSpan[2] || aPiecesSpan[31] != aFaceColorsSpan[1] || aPiecesSpan[49] != aFaceColorsSpan[5])
                {
                    return false;
                }
            }

            // Front face Orange and Up face Yellow
            if (aPiecesSpan[4] == aFaceColorsSpan[3] && aPiecesSpan[40] == aFaceColorsSpan[6])
            {
                if (aPiecesSpan[13] != aFaceColorsSpan[2] || aPiecesSpan[22] != aFaceColorsSpan[1] || aPiecesSpan[31] != aFaceColorsSpan[4] || aPiecesSpan[49] != aFaceColorsSpan[5])
                {
                    return false;
                }
            }

            // Front face Blue and Up face Yellow
            if (aPiecesSpan[4] == aFaceColorsSpan[2] && aPiecesSpan[40] == aFaceColorsSpan[6])
            {
                if (aPiecesSpan[13] != aFaceColorsSpan[1] || aPiecesSpan[22] != aFaceColorsSpan[4] || aPiecesSpan[31] != aFaceColorsSpan[3] || aPiecesSpan[49] != aFaceColorsSpan[5])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check the number of colors on the corner cubes
        /// </summary>
        /// <param name="aNumberOfColors"></param>
        /// <param name="nColor"></param>
        private static void CheckNumberColorsCornerCube(int[] aNumberOfColors, int nColor)
        {
            // Create a span for the arrays
            ReadOnlySpan<string> aFaceColorsSpan = aFaceColors.AsSpan();
            ReadOnlySpan<string> aPiecesSpan = aPieces.AsSpan();

            // Up face
            if (aPiecesSpan[0] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[2] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[6] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[8] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            // Right face
            if (aPiecesSpan[9] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[11] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[15] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[17] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            // Back face
            if (aPiecesSpan[18] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[20] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[24] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[26] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            // Left face
            if (aPiecesSpan[27] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[29] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[33] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[35] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            // Up face
            if (aPiecesSpan[36] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[38] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[42] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[44] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            // Down face
            if (aPiecesSpan[45] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[47] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[51] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[53] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }
        }

        /// <summary>
        /// Check the number of colors on the edge cubes
        /// </summary>
        /// <param name="aNumberOfColors"></param>
        /// <param name="nColor"></param>
        private static void CheckNumberColorsEdgeCube(int[] aNumberOfColors, int nColor)
        {
            // Create a span for the arrays
            ReadOnlySpan<string> aFaceColorsSpan = aFaceColors.AsSpan();
            ReadOnlySpan<string> aPiecesSpan = aPieces.AsSpan();

            // Up face
            if (aPiecesSpan[1] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[3] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[5] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[7] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            // Right face
            if (aPiecesSpan[10] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[12] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[14] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[16] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            // Back face
            if (aPiecesSpan[19] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[21] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[23] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[25] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            // Left face
            if (aPiecesSpan[28] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[30] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[32] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[34] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            // Up face
            if (aPiecesSpan[37] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[39] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[41] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[43] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            // Down face
            if (aPiecesSpan[46] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[48] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[50] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPiecesSpan[52] == aFaceColorsSpan[nColor])
            {
                aNumberOfColors[nColor]++;
            }
        }

        /// <summary>
        /// Check if the cube is solved
        /// </summary>
        /// <returns></returns>
        public static bool CheckIfSolved()
        {
            // Create a span for the arrays
            ReadOnlySpan<string> aFaceColorsSpan = aFaceColors.AsSpan();
            ReadOnlySpan<string> aPiecesSpan = aPieces.AsSpan();

            bool bColorsUp = false;
            bool bColorsFront = false;
            bool bColorsRight = false;
            bool bColorsLeft = false;
            bool bColorsBack = false;
            bool bColorsDown = false;

            if (aPiecesSpan[36] == aPiecesSpan[37] && aPiecesSpan[36] == aPiecesSpan[38] && aPiecesSpan[36] == aPiecesSpan[39] && aPiecesSpan[36] == aPiecesSpan[40] && aPiecesSpan[36] == aPiecesSpan[41] && aPiecesSpan[36] == aPiecesSpan[42] && aPiecesSpan[36] == aPiecesSpan[43] && aPiecesSpan[36] == aPiecesSpan[44])
            {
                bColorsUp = true;
            }

            if (aPiecesSpan[0] == aPiecesSpan[1] && aPiecesSpan[0] == aPiecesSpan[2] && aPiecesSpan[0] == aPiecesSpan[3] && aPiecesSpan[0] == aPiecesSpan[4] && aPiecesSpan[0] == aPiecesSpan[5] && aPiecesSpan[0] == aPiecesSpan[6] && aPiecesSpan[0] == aPiecesSpan[7] && aPiecesSpan[0] == aPiecesSpan[8])
            {
                bColorsFront = true;
            }

            if (aPiecesSpan[9] == aPiecesSpan[10] && aPiecesSpan[9] == aPiecesSpan[11] && aPiecesSpan[9] == aPiecesSpan[12] && aPiecesSpan[9] == aPiecesSpan[13] && aPiecesSpan[9] == aPiecesSpan[14] && aPiecesSpan[9] == aPiecesSpan[15] && aPiecesSpan[9] == aPiecesSpan[16] && aPiecesSpan[9] == aPiecesSpan[17])
            {
                bColorsRight = true;
            }

            if (aPiecesSpan[27] == aPiecesSpan[28] && aPiecesSpan[27] == aPiecesSpan[29] && aPiecesSpan[27] == aPiecesSpan[30] && aPiecesSpan[27] == aPiecesSpan[31] && aPiecesSpan[27] == aPiecesSpan[32] && aPiecesSpan[27] == aPiecesSpan[33] && aPiecesSpan[27] == aPiecesSpan[34] && aPiecesSpan[27] == aPiecesSpan[35])
            {
                bColorsLeft = true;
            }

            if (aPiecesSpan[18] == aPiecesSpan[19] && aPiecesSpan[18] == aPiecesSpan[20] && aPiecesSpan[18] == aPiecesSpan[21] && aPiecesSpan[18] == aPiecesSpan[22] && aPiecesSpan[18] == aPiecesSpan[23] && aPiecesSpan[18] == aPiecesSpan[24] && aPiecesSpan[18] == aPiecesSpan[25] && aPiecesSpan[18] == aPiecesSpan[26])
            {
                bColorsBack = true;
            }

            if (aPiecesSpan[45] == aPiecesSpan[46] && aPiecesSpan[45] == aPiecesSpan[47] && aPiecesSpan[45] == aPiecesSpan[48] && aPiecesSpan[45] == aPiecesSpan[49] && aPiecesSpan[45] == aPiecesSpan[50] && aPiecesSpan[45] == aPiecesSpan[51] && aPiecesSpan[45] == aPiecesSpan[52] && aPiecesSpan[45] == aPiecesSpan[53])
            {
                bColorsDown = true;
            }

            if (!bColorsUp || !bColorsFront || !bColorsRight || !bColorsLeft || !bColorsBack || !bColorsDown)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Reset the colors of the cube
        /// </summary>
        public static void ResetCube()
        {
            // Create a span for the arrays
            ReadOnlySpan<string> aFaceColorsSpan = aFaceColors.AsSpan();
            Span<string> aPiecesSpan = aPieces.AsSpan();

            int nItem;

            for (nItem = 0; nItem < 9; nItem++)
            {
                aPiecesSpan[nItem] = aFaceColorsSpan[1];
            }

            for (nItem = 9; nItem < 18; nItem++)
            {
                aPiecesSpan[nItem] = aFaceColorsSpan[2];
            }

            for (nItem = 18; nItem < 27; nItem++)
            {
                aPiecesSpan[nItem] = aFaceColorsSpan[3];
            }

            for (nItem = 27; nItem < 36; nItem++)
            {
                aPiecesSpan[nItem] = aFaceColorsSpan[4];
            }

            for (nItem = 36; nItem < 45; nItem++)
            {
                aPiecesSpan[nItem] = aFaceColorsSpan[5];
            }

            for (nItem = 45; nItem < 54; nItem++)
            {
                aPiecesSpan[nItem] = aFaceColorsSpan[6];
            }
        }
    }
}
