namespace RubiksCube
{
    internal class ClassCheckColorsCube
    {
        // Check the number of colors of the cube
        public static string CheckNumberColors()
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
                return CubeLang.MessageNineSameColor_Text;
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
                return CubeLang.MessageColorCentralCube_Text;
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
                return CubeLang.MessageColorCornerCube_Text;
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
                return CubeLang.MessageColorEdgeCube_Text;
            }

            return "";
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
    }
}
