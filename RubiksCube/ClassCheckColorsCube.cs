namespace RubiksCube
{
    class ClassCheckColorsCube
    {
        // Check the number of colors of the cube.
        public static string CheckNumberColors()
        {
            int nNumberOfColors1 = 0;
            int nNumberOfColors2 = 0;
            int nNumberOfColors3 = 0;
            int nNumberOfColors4 = 0;
            int nNumberOfColors5 = 0;
            int nNumberOfColors6 = 0;

            int nRow;

            // Check the number of colors of the cube.
            // Top layer.
            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aUpFace[nRow] == MainPage.aFaceColors[1])
                    nNumberOfColors1++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aUpFace[nRow] == MainPage.aFaceColors[2])
                    nNumberOfColors2++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aUpFace[nRow] == MainPage.aFaceColors[3])
                    nNumberOfColors3++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aUpFace[nRow] == MainPage.aFaceColors[4])
                    nNumberOfColors4++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aUpFace[nRow] == MainPage.aFaceColors[5])
                    nNumberOfColors5++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aUpFace[nRow] == MainPage.aFaceColors[6])
                    nNumberOfColors6++;
            }

            // Front face.
            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aFrontFace[nRow] == MainPage.aFaceColors[1])
                    nNumberOfColors1++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aFrontFace[nRow] == MainPage.aFaceColors[2])
                    nNumberOfColors2++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aFrontFace[nRow] == MainPage.aFaceColors[3])
                    nNumberOfColors3++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aFrontFace[nRow] == MainPage.aFaceColors[4])
                    nNumberOfColors4++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aFrontFace[nRow] == MainPage.aFaceColors[5])
                    nNumberOfColors5++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aFrontFace[nRow] == MainPage.aFaceColors[6])
                    nNumberOfColors6++;
            }

            // Right face.
            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aRightFace[nRow] == MainPage.aFaceColors[1])
                    nNumberOfColors1++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aRightFace[nRow] == MainPage.aFaceColors[2])
                    nNumberOfColors2++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aRightFace[nRow] == MainPage.aFaceColors[3])
                    nNumberOfColors3++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aRightFace[nRow] == MainPage.aFaceColors[4])
                    nNumberOfColors4++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aRightFace[nRow] == MainPage.aFaceColors[5])
                    nNumberOfColors5++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aRightFace[nRow] == MainPage.aFaceColors[6])
                    nNumberOfColors6++;
            }

            // Left face.
            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aLeftFace[nRow] == MainPage.aFaceColors[1])
                    nNumberOfColors1++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aLeftFace[nRow] == MainPage.aFaceColors[2])
                    nNumberOfColors2++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aLeftFace[nRow] == MainPage.aFaceColors[3])
                    nNumberOfColors3++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aLeftFace[nRow] == MainPage.aFaceColors[4])
                    nNumberOfColors4++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aLeftFace[nRow] == MainPage.aFaceColors[5])
                    nNumberOfColors5++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aLeftFace[nRow] == MainPage.aFaceColors[6])
                    nNumberOfColors6++;
            }

            // Back face.
            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aBackFace[nRow] == MainPage.aFaceColors[1])
                    nNumberOfColors1++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aBackFace[nRow] == MainPage.aFaceColors[2])
                    nNumberOfColors2++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aBackFace[nRow] == MainPage.aFaceColors[3])
                    nNumberOfColors3++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aBackFace[nRow] == MainPage.aFaceColors[4])
                    nNumberOfColors4++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aBackFace[nRow] == MainPage.aFaceColors[5])
                    nNumberOfColors5++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aBackFace[nRow] == MainPage.aFaceColors[6])
                    nNumberOfColors6++;
            }

            // Bottom layer.
            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aDownFace[nRow] == MainPage.aFaceColors[1])
                    nNumberOfColors1++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aDownFace[nRow] == MainPage.aFaceColors[2])
                    nNumberOfColors2++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aDownFace[nRow] == MainPage.aFaceColors[3])
                    nNumberOfColors3++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aDownFace[nRow] == MainPage.aFaceColors[4])
                    nNumberOfColors4++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aDownFace[nRow] == MainPage.aFaceColors[5])
                    nNumberOfColors5++;
            }

            for (nRow = 1; nRow < 10; nRow++)
            {
                if (MainPage.aDownFace[nRow] == MainPage.aFaceColors[6])
                    nNumberOfColors6++;
            }

            if (nNumberOfColors1 != 9 || nNumberOfColors2 != 9 || nNumberOfColors3 != 9 || nNumberOfColors4 != 9 || nNumberOfColors5 != 9 || nNumberOfColors6 != 9)
            {
                return CubeLang.MessageNineSameColor_Text;
            }

            // Check the number of colors of the central square of the cube.
            bool bColorCenterCube = true;

            if (MainPage.aUpFace[5] == MainPage.aFrontFace[5] || MainPage.aUpFace[5] == MainPage.aRightFace[5] || MainPage.aUpFace[5] == MainPage.aLeftFace[5] || MainPage.aUpFace[5] == MainPage.aBackFace[5] || MainPage.aUpFace[5] == MainPage.aDownFace[5])
            {
                bColorCenterCube = false;
            }

            if (MainPage.aFrontFace[5] == MainPage.aUpFace[5] || MainPage.aFrontFace[5] == MainPage.aRightFace[5] || MainPage.aFrontFace[5] == MainPage.aLeftFace[5] || MainPage.aFrontFace[5] == MainPage.aBackFace[5] || MainPage.aFrontFace[5] == MainPage.aDownFace[5])
            {
                bColorCenterCube = false;
            }

            if (MainPage.aRightFace[5] == MainPage.aFrontFace[5] || MainPage.aRightFace[5] == MainPage.aUpFace[5] || MainPage.aRightFace[5] == MainPage.aLeftFace[5] || MainPage.aRightFace[5] == MainPage.aBackFace[5] || MainPage.aRightFace[5] == MainPage.aDownFace[5])
            {
                bColorCenterCube = false;
            }

            if (MainPage.aLeftFace[5] == MainPage.aFrontFace[5] || MainPage.aLeftFace[5] == MainPage.aRightFace[5] || MainPage.aLeftFace[5] == MainPage.aUpFace[5] || MainPage.aLeftFace[5] == MainPage.aBackFace[5] || MainPage.aLeftFace[5] == MainPage.aDownFace[5])
            {
                bColorCenterCube = false;
            }

            if (MainPage.aBackFace[5] == MainPage.aFrontFace[5] || MainPage.aBackFace[5] == MainPage.aRightFace[5] || MainPage.aBackFace[5] == MainPage.aLeftFace[5] || MainPage.aBackFace[5] == MainPage.aUpFace[5] || MainPage.aBackFace[5] == MainPage.aDownFace[5])
            {
                bColorCenterCube = false;
            }

            if (MainPage.aDownFace[5] == MainPage.aFrontFace[5] || MainPage.aDownFace[5] == MainPage.aRightFace[5] || MainPage.aDownFace[5] == MainPage.aLeftFace[5] || MainPage.aDownFace[5] == MainPage.aBackFace[5] || MainPage.aDownFace[5] == MainPage.aUpFace[5])
            {
                bColorCenterCube = false;
            }

            if (!bColorCenterCube)
            {
                return CubeLang.MessageColorCentralCube_Text;
            }

            // Check the number of colors of the corner cubes of the cube.
            bool bColorCornerCube = true;

            if (MainPage.aUpFace[7] == MainPage.aLeftFace[3] || MainPage.aUpFace[7] == MainPage.aFrontFace[1] || MainPage.aFrontFace[1] == MainPage.aLeftFace[3])
            {
                bColorCornerCube = false;
            }

            if (MainPage.aUpFace[1] == MainPage.aLeftFace[1] || MainPage.aUpFace[1] == MainPage.aBackFace[3] || MainPage.aLeftFace[1] == MainPage.aBackFace[3])
            {
                bColorCornerCube = false;
            }

            if (MainPage.aUpFace[3] == MainPage.aRightFace[3] || MainPage.aUpFace[3] == MainPage.aBackFace[1] || MainPage.aRightFace[3] == MainPage.aBackFace[1])
            {
                bColorCornerCube = false;
            }

            if (MainPage.aUpFace[9] == MainPage.aFrontFace[3] || MainPage.aUpFace[9] == MainPage.aRightFace[1] || MainPage.aFrontFace[3] == MainPage.aRightFace[1])
            {
                bColorCornerCube = false;
            }

            if (MainPage.aDownFace[1] == MainPage.aLeftFace[9] || MainPage.aDownFace[1] == MainPage.aFrontFace[7] || MainPage.aFrontFace[7] == MainPage.aLeftFace[9])
            {
                bColorCornerCube = false;
            }

            if (MainPage.aDownFace[7] == MainPage.aLeftFace[7] || MainPage.aDownFace[7] == MainPage.aBackFace[9] || MainPage.aBackFace[9] == MainPage.aLeftFace[7])
            {
                bColorCornerCube = false;
            }

            if (MainPage.aDownFace[9] == MainPage.aRightFace[9] || MainPage.aDownFace[9] == MainPage.aBackFace[7] || MainPage.aBackFace[7] == MainPage.aRightFace[9])
            {
                bColorCornerCube = false;
            }

            if (MainPage.aDownFace[3] == MainPage.aRightFace[7] || MainPage.aDownFace[3] == MainPage.aFrontFace[9] || MainPage.aFrontFace[9] == MainPage.aRightFace[7])
            {
                bColorCornerCube = false;
            }

            if (!bColorCornerCube)
            {
                return CubeLang.MessageColorCornerCube_Text;
            }

            // Check the number of colors of the edge cubes of the cube.
            bool bColorEdgeCube = true;

            if (MainPage.aUpFace[2] == MainPage.aBackFace[2] || MainPage.aUpFace[4] == MainPage.aLeftFace[2] || MainPage.aUpFace[6] == MainPage.aRightFace[2] || MainPage.aUpFace[8] == MainPage.aFrontFace[2])
            {
                bColorEdgeCube = false;
            }

            if (MainPage.aDownFace[2] == MainPage.aFrontFace[8] || MainPage.aDownFace[4] == MainPage.aLeftFace[8] || MainPage.aDownFace[6] == MainPage.aRightFace[8] || MainPage.aDownFace[8] == MainPage.aBackFace[8])
            {
                bColorEdgeCube = false;
            }

            if (!bColorEdgeCube)
            {
                return CubeLang.MessageColorEdgeCube_Text;
            }

            return "";
        }

        // Check if the cube is solved.
        public static bool CheckIfSolved()
        {
            bool bColorsUp = false;
            bool bColorsFront = false;
            bool bColorsRight = false;
            bool bColorsLeft = false;
            bool bColorsBack = false;
            bool bColorsDown = false;

            if (MainPage.aUpFace[1] == MainPage.aUpFace[2] && MainPage.aUpFace[1] == MainPage.aUpFace[3] && MainPage.aUpFace[1] == MainPage.aUpFace[4] && MainPage.aUpFace[1] == MainPage.aUpFace[5] && MainPage.aUpFace[1] == MainPage.aUpFace[6] && MainPage.aUpFace[1] == MainPage.aUpFace[7] && MainPage.aUpFace[1] == MainPage.aUpFace[8] && MainPage.aUpFace[1] == MainPage.aUpFace[9])
            {
                bColorsUp = true;
            }

            if (MainPage.aFrontFace[1] == MainPage.aFrontFace[2] && MainPage.aFrontFace[1] == MainPage.aFrontFace[3] && MainPage.aFrontFace[1] == MainPage.aFrontFace[4] && MainPage.aFrontFace[1] == MainPage.aFrontFace[5] && MainPage.aFrontFace[1] == MainPage.aFrontFace[6] && MainPage.aFrontFace[1] == MainPage.aFrontFace[7] && MainPage.aFrontFace[1] == MainPage.aFrontFace[8] && MainPage.aFrontFace[1] == MainPage.aFrontFace[9])
            {
                bColorsFront = true;
            }

            if (MainPage.aRightFace[1] == MainPage.aRightFace[2] && MainPage.aRightFace[1] == MainPage.aRightFace[3] && MainPage.aRightFace[1] == MainPage.aRightFace[4] && MainPage.aRightFace[1] == MainPage.aRightFace[5] && MainPage.aRightFace[1] == MainPage.aRightFace[6] && MainPage.aRightFace[1] == MainPage.aRightFace[7] && MainPage.aRightFace[1] == MainPage.aRightFace[8] && MainPage.aRightFace[1] == MainPage.aRightFace[9])
            {
                bColorsRight = true;
            }

            if (MainPage.aLeftFace[1] == MainPage.aLeftFace[2] && MainPage.aLeftFace[1] == MainPage.aLeftFace[3] && MainPage.aLeftFace[1] == MainPage.aLeftFace[4] && MainPage.aLeftFace[1] == MainPage.aLeftFace[5] && MainPage.aLeftFace[1] == MainPage.aLeftFace[6] && MainPage.aLeftFace[1] == MainPage.aLeftFace[7] && MainPage.aLeftFace[1] == MainPage.aLeftFace[8] && MainPage.aLeftFace[1] == MainPage.aLeftFace[9])
            {
                bColorsLeft = true;
            }

            if (MainPage.aBackFace[1] == MainPage.aBackFace[2] && MainPage.aBackFace[1] == MainPage.aBackFace[3] && MainPage.aBackFace[1] == MainPage.aBackFace[4] && MainPage.aBackFace[1] == MainPage.aBackFace[5] && MainPage.aBackFace[1] == MainPage.aBackFace[6] && MainPage.aBackFace[1] == MainPage.aBackFace[7] && MainPage.aBackFace[1] == MainPage.aBackFace[8] && MainPage.aBackFace[1] == MainPage.aBackFace[9])
            {
                bColorsBack = true;
            }

            if (MainPage.aDownFace[1] == MainPage.aDownFace[2] && MainPage.aDownFace[1] == MainPage.aDownFace[3] && MainPage.aDownFace[1] == MainPage.aDownFace[4] && MainPage.aDownFace[1] == MainPage.aDownFace[5] && MainPage.aDownFace[1] == MainPage.aDownFace[6] && MainPage.aDownFace[1] == MainPage.aDownFace[7] && MainPage.aDownFace[1] == MainPage.aDownFace[8] && MainPage.aDownFace[1] == MainPage.aDownFace[9])
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
