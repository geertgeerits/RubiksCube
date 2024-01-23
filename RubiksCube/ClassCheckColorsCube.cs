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
            // Top layer
            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aUpFace[nRow] == Globals.aFaceColors[1])
                    nNumberOfColors1++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aUpFace[nRow] == Globals.aFaceColors[2])
                    nNumberOfColors2++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aUpFace[nRow] == Globals.aFaceColors[3])
                    nNumberOfColors3++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aUpFace[nRow] == Globals.aFaceColors[4])
                    nNumberOfColors4++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aUpFace[nRow] == Globals.aFaceColors[5])
                    nNumberOfColors5++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aUpFace[nRow] == Globals.aFaceColors[6])
                    nNumberOfColors6++;
            }

            // Front face
            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aFrontFace[nRow] == Globals.aFaceColors[1])
                    nNumberOfColors1++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aFrontFace[nRow] == Globals.aFaceColors[2])
                    nNumberOfColors2++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aFrontFace[nRow] == Globals.aFaceColors[3])
                    nNumberOfColors3++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aFrontFace[nRow] == Globals.aFaceColors[4])
                    nNumberOfColors4++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aFrontFace[nRow] == Globals.aFaceColors[5])
                    nNumberOfColors5++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aFrontFace[nRow] == Globals.aFaceColors[6])
                    nNumberOfColors6++;
            }

            // Right face
            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aRightFace[nRow] == Globals.aFaceColors[1])
                    nNumberOfColors1++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aRightFace[nRow] == Globals.aFaceColors[2])
                    nNumberOfColors2++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aRightFace[nRow] == Globals.aFaceColors[3])
                    nNumberOfColors3++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aRightFace[nRow] == Globals.aFaceColors[4])
                    nNumberOfColors4++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aRightFace[nRow] == Globals.aFaceColors[5])
                    nNumberOfColors5++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aRightFace[nRow] == Globals.aFaceColors[6])
                    nNumberOfColors6++;
            }

            // Left face
            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aLeftFace[nRow] == Globals.aFaceColors[1])
                    nNumberOfColors1++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aLeftFace[nRow] == Globals.aFaceColors[2])
                    nNumberOfColors2++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aLeftFace[nRow] == Globals.aFaceColors[3])
                    nNumberOfColors3++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aLeftFace[nRow] == Globals.aFaceColors[4])
                    nNumberOfColors4++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aLeftFace[nRow] == Globals.aFaceColors[5])
                    nNumberOfColors5++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aLeftFace[nRow] == Globals.aFaceColors[6])
                    nNumberOfColors6++;
            }

            // Back face
            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aBackFace[nRow] == Globals.aFaceColors[1])
                    nNumberOfColors1++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aBackFace[nRow] == Globals.aFaceColors[2])
                    nNumberOfColors2++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aBackFace[nRow] == Globals.aFaceColors[3])
                    nNumberOfColors3++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aBackFace[nRow] == Globals.aFaceColors[4])
                    nNumberOfColors4++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aBackFace[nRow] == Globals.aFaceColors[5])
                    nNumberOfColors5++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aBackFace[nRow] == Globals.aFaceColors[6])
                    nNumberOfColors6++;
            }

            // Bottom layer
            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aDownFace[nRow] == Globals.aFaceColors[1])
                    nNumberOfColors1++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aDownFace[nRow] == Globals.aFaceColors[2])
                    nNumberOfColors2++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aDownFace[nRow] == Globals.aFaceColors[3])
                    nNumberOfColors3++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aDownFace[nRow] == Globals.aFaceColors[4])
                    nNumberOfColors4++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aDownFace[nRow] == Globals.aFaceColors[5])
                    nNumberOfColors5++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (Globals.aDownFace[nRow] == Globals.aFaceColors[6])
                    nNumberOfColors6++;
            }

            if (nNumberOfColors1 != 9 || nNumberOfColors2 != 9 || nNumberOfColors3 != 9 || nNumberOfColors4 != 9 || nNumberOfColors5 != 9 || nNumberOfColors6 != 9)
            {
                return CubeLang.MessageNineSameColor_Text;
            }

            // Check the number of colors of the central square of the cube
            bool bColorCenterCube = true;

            if (Globals.aUpFace[5] == Globals.aFrontFace[5] || Globals.aUpFace[5] == Globals.aRightFace[5] || Globals.aUpFace[5] == Globals.aLeftFace[5] || Globals.aUpFace[5] == Globals.aBackFace[5] || Globals.aUpFace[5] == Globals.aDownFace[5])
            {
                bColorCenterCube = false;
            }

            if (Globals.aFrontFace[5] == Globals.aUpFace[5] || Globals.aFrontFace[5] == Globals.aRightFace[5] || Globals.aFrontFace[5] == Globals.aLeftFace[5] || Globals.aFrontFace[5] == Globals.aBackFace[5] || Globals.aFrontFace[5] == Globals.aDownFace[5])
            {
                bColorCenterCube = false;
            }

            if (Globals.aRightFace[5] == Globals.aFrontFace[5] || Globals.aRightFace[5] == Globals.aUpFace[5] || Globals.aRightFace[5] == Globals.aLeftFace[5] || Globals.aRightFace[5] == Globals.aBackFace[5] || Globals.aRightFace[5] == Globals.aDownFace[5])
            {
                bColorCenterCube = false;
            }

            if (Globals.aLeftFace[5] == Globals.aFrontFace[5] || Globals.aLeftFace[5] == Globals.aRightFace[5] || Globals.aLeftFace[5] == Globals.aUpFace[5] || Globals.aLeftFace[5] == Globals.aBackFace[5] || Globals.aLeftFace[5] == Globals.aDownFace[5])
            {
                bColorCenterCube = false;
            }

            if (Globals.aBackFace[5] == Globals.aFrontFace[5] || Globals.aBackFace[5] == Globals.aRightFace[5] || Globals.aBackFace[5] == Globals.aLeftFace[5] || Globals.aBackFace[5] == Globals.aUpFace[5] || Globals.aBackFace[5] == Globals.aDownFace[5])
            {
                bColorCenterCube = false;
            }

            if (Globals.aDownFace[5] == Globals.aFrontFace[5] || Globals.aDownFace[5] == Globals.aRightFace[5] || Globals.aDownFace[5] == Globals.aLeftFace[5] || Globals.aDownFace[5] == Globals.aBackFace[5] || Globals.aDownFace[5] == Globals.aUpFace[5])
            {
                bColorCenterCube = false;
            }

            if (!bColorCenterCube)
            {
                return CubeLang.MessageColorCentralCube_Text;
            }

            // Check the number of colors of the corner cubes of the cube
            bool bColorCornerCube = true;

            if (Globals.aUpFace[7] == Globals.aLeftFace[3] || Globals.aUpFace[7] == Globals.aFrontFace[1] || Globals.aFrontFace[1] == Globals.aLeftFace[3])
            {
                bColorCornerCube = false;
            }

            if (Globals.aUpFace[1] == Globals.aLeftFace[1] || Globals.aUpFace[1] == Globals.aBackFace[3] || Globals.aLeftFace[1] == Globals.aBackFace[3])
            {
                bColorCornerCube = false;
            }

            if (Globals.aUpFace[3] == Globals.aRightFace[3] || Globals.aUpFace[3] == Globals.aBackFace[1] || Globals.aRightFace[3] == Globals.aBackFace[1])
            {
                bColorCornerCube = false;
            }

            if (Globals.aUpFace[9] == Globals.aFrontFace[3] || Globals.aUpFace[9] == Globals.aRightFace[1] || Globals.aFrontFace[3] == Globals.aRightFace[1])
            {
                bColorCornerCube = false;
            }

            if (Globals.aDownFace[1] == Globals.aLeftFace[9] || Globals.aDownFace[1] == Globals.aFrontFace[7] || Globals.aFrontFace[7] == Globals.aLeftFace[9])
            {
                bColorCornerCube = false;
            }

            if (Globals.aDownFace[7] == Globals.aLeftFace[7] || Globals.aDownFace[7] == Globals.aBackFace[9] || Globals.aBackFace[9] == Globals.aLeftFace[7])
            {
                bColorCornerCube = false;
            }

            if (Globals.aDownFace[9] == Globals.aRightFace[9] || Globals.aDownFace[9] == Globals.aBackFace[7] || Globals.aBackFace[7] == Globals.aRightFace[9])
            {
                bColorCornerCube = false;
            }

            if (Globals.aDownFace[3] == Globals.aRightFace[7] || Globals.aDownFace[3] == Globals.aFrontFace[9] || Globals.aFrontFace[9] == Globals.aRightFace[7])
            {
                bColorCornerCube = false;
            }

            if (!bColorCornerCube)
            {
                return CubeLang.MessageColorCornerCube_Text;
            }

            // Check the number of colors of the edge cubes of the cube
            bool bColorEdgeCube = true;

            if (Globals.aUpFace[2] == Globals.aBackFace[2] || Globals.aUpFace[4] == Globals.aLeftFace[2] || Globals.aUpFace[6] == Globals.aRightFace[2] || Globals.aUpFace[8] == Globals.aFrontFace[2])
            {
                bColorEdgeCube = false;
            }

            if (Globals.aDownFace[2] == Globals.aFrontFace[8] || Globals.aDownFace[4] == Globals.aLeftFace[8] || Globals.aDownFace[6] == Globals.aRightFace[8] || Globals.aDownFace[8] == Globals.aBackFace[8])
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

            if (Globals.aUpFace[1] == Globals.aUpFace[2] && Globals.aUpFace[1] == Globals.aUpFace[3] && Globals.aUpFace[1] == Globals.aUpFace[4] && Globals.aUpFace[1] == Globals.aUpFace[5] && Globals.aUpFace[1] == Globals.aUpFace[6] && Globals.aUpFace[1] == Globals.aUpFace[7] && Globals.aUpFace[1] == Globals.aUpFace[8] && Globals.aUpFace[1] == Globals.aUpFace[9])
            {
                bColorsUp = true;
            }

            if (Globals.aFrontFace[1] == Globals.aFrontFace[2] && Globals.aFrontFace[1] == Globals.aFrontFace[3] && Globals.aFrontFace[1] == Globals.aFrontFace[4] && Globals.aFrontFace[1] == Globals.aFrontFace[5] && Globals.aFrontFace[1] == Globals.aFrontFace[6] && Globals.aFrontFace[1] == Globals.aFrontFace[7] && Globals.aFrontFace[1] == Globals.aFrontFace[8] && Globals.aFrontFace[1] == Globals.aFrontFace[9])
            {
                bColorsFront = true;
            }

            if (Globals.aRightFace[1] == Globals.aRightFace[2] && Globals.aRightFace[1] == Globals.aRightFace[3] && Globals.aRightFace[1] == Globals.aRightFace[4] && Globals.aRightFace[1] == Globals.aRightFace[5] && Globals.aRightFace[1] == Globals.aRightFace[6] && Globals.aRightFace[1] == Globals.aRightFace[7] && Globals.aRightFace[1] == Globals.aRightFace[8] && Globals.aRightFace[1] == Globals.aRightFace[9])
            {
                bColorsRight = true;
            }

            if (Globals.aLeftFace[1] == Globals.aLeftFace[2] && Globals.aLeftFace[1] == Globals.aLeftFace[3] && Globals.aLeftFace[1] == Globals.aLeftFace[4] && Globals.aLeftFace[1] == Globals.aLeftFace[5] && Globals.aLeftFace[1] == Globals.aLeftFace[6] && Globals.aLeftFace[1] == Globals.aLeftFace[7] && Globals.aLeftFace[1] == Globals.aLeftFace[8] && Globals.aLeftFace[1] == Globals.aLeftFace[9])
            {
                bColorsLeft = true;
            }

            if (Globals.aBackFace[1] == Globals.aBackFace[2] && Globals.aBackFace[1] == Globals.aBackFace[3] && Globals.aBackFace[1] == Globals.aBackFace[4] && Globals.aBackFace[1] == Globals.aBackFace[5] && Globals.aBackFace[1] == Globals.aBackFace[6] && Globals.aBackFace[1] == Globals.aBackFace[7] && Globals.aBackFace[1] == Globals.aBackFace[8] && Globals.aBackFace[1] == Globals.aBackFace[9])
            {
                bColorsBack = true;
            }

            if (Globals.aDownFace[1] == Globals.aDownFace[2] && Globals.aDownFace[1] == Globals.aDownFace[3] && Globals.aDownFace[1] == Globals.aDownFace[4] && Globals.aDownFace[1] == Globals.aDownFace[5] && Globals.aDownFace[1] == Globals.aDownFace[6] && Globals.aDownFace[1] == Globals.aDownFace[7] && Globals.aDownFace[1] == Globals.aDownFace[8] && Globals.aDownFace[1] == Globals.aDownFace[9])
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
