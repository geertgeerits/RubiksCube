namespace RubiksCube
{
    class ClassCubeTurns
    {
        public static string CubeTurns(string cDirection, string[] aPieces, string[] aPiecesTemp)
        {
            //DisplayAlert("cDirection", cDirection + aPieces[0] + aPiecesTemp[0], "OK");
            Console.WriteLine(cDirection + aPieces[0] + aPiecesTemp[0]);
            return "Geert";
        }

        // Turn the entire front face clockwise or counter clockwise.
        public static void TurnFrontFaceTo(string cDirection)
        {
            string cColorFront1 = MainPage.aFrontFace[1];
            string cColorFront2 = MainPage.aFrontFace[2];
            string cColorFront3 = MainPage.aFrontFace[3];
            string cColorFront4 = MainPage.aFrontFace[4];
            string cColorFront6 = MainPage.aFrontFace[6];
            string cColorFront7 = MainPage.aFrontFace[7];
            string cColorFront8 = MainPage.aFrontFace[8];
            string cColorFront9 = MainPage.aFrontFace[9];

            string cColorUp7 = MainPage.aUpFace[7];
            string cColorUp8 = MainPage.aUpFace[8];
            string cColorUp9 = MainPage.aUpFace[9];

            string cColorRight1 = MainPage.aRightFace[1];
            string cColorRight4 = MainPage.aRightFace[4];
            string cColorRight7 = MainPage.aRightFace[7];

            string cColorDown1 = MainPage.aDownFace[1];
            string cColorDown2 = MainPage.aDownFace[2];
            string cColorDown3 = MainPage.aDownFace[3];

            string cColorLeft3 = MainPage.aLeftFace[3];
            string cColorLeft6 = MainPage.aLeftFace[6];
            string cColorLeft9 = MainPage.aLeftFace[9];

            if (cDirection == "+")
            {
                MainPage.aFrontFace[1] = cColorFront7;
                MainPage.aFrontFace[2] = cColorFront4;
                MainPage.aFrontFace[3] = cColorFront1;
                MainPage.aFrontFace[4] = cColorFront8;
                MainPage.aFrontFace[6] = cColorFront2;
                MainPage.aFrontFace[7] = cColorFront9;
                MainPage.aFrontFace[8] = cColorFront6;
                MainPage.aFrontFace[9] = cColorFront3;

                MainPage.aUpFace[7] = cColorLeft9;
                MainPage.aUpFace[8] = cColorLeft6;
                MainPage.aUpFace[9] = cColorLeft3;

                MainPage.aRightFace[1] = cColorUp7;
                MainPage.aRightFace[4] = cColorUp8;
                MainPage.aRightFace[7] = cColorUp9;

                MainPage.aDownFace[1] = cColorRight7;
                MainPage.aDownFace[2] = cColorRight4;
                MainPage.aDownFace[3] = cColorRight1;

                MainPage.aLeftFace[3] = cColorDown1;
                MainPage.aLeftFace[6] = cColorDown2;
                MainPage.aLeftFace[9] = cColorDown3;
            }

            if (cDirection == "-")
            {
                MainPage.aFrontFace[1] = cColorFront3;
                MainPage.aFrontFace[2] = cColorFront6;
                MainPage.aFrontFace[3] = cColorFront9;
                MainPage.aFrontFace[4] = cColorFront2;
                MainPage.aFrontFace[6] = cColorFront8;
                MainPage.aFrontFace[7] = cColorFront1;
                MainPage.aFrontFace[8] = cColorFront4;
                MainPage.aFrontFace[9] = cColorFront7;

                MainPage.aUpFace[7] = cColorRight1;
                MainPage.aUpFace[8] = cColorRight4;
                MainPage.aUpFace[9] = cColorRight7;

                MainPage.aRightFace[1] = cColorDown3;
                MainPage.aRightFace[4] = cColorDown2;
                MainPage.aRightFace[7] = cColorDown1;

                MainPage.aDownFace[1] = cColorLeft3;
                MainPage.aDownFace[2] = cColorLeft6;
                MainPage.aDownFace[3] = cColorLeft9;

                MainPage.aLeftFace[3] = cColorUp9;
                MainPage.aLeftFace[6] = cColorUp8;
                MainPage.aLeftFace[9] = cColorUp7;
            }
        }

        // Turn the top horizontal middle layer to the right or left.
        public static void TurnUpHorMiddleTo(string cDirection)
        {
            string cColorUp4 = MainPage.aUpFace[4];
            string cColorUp5 = MainPage.aUpFace[5];
            string cColorUp6 = MainPage.aUpFace[6];

            string cColorRight2 = MainPage.aRightFace[2];
            string cColorRight5 = MainPage.aRightFace[5];
            string cColorRight8 = MainPage.aRightFace[8];

            string cColorDown4 = MainPage.aDownFace[4];
            string cColorDown5 = MainPage.aDownFace[5];
            string cColorDown6 = MainPage.aDownFace[6];

            string cColorLeft2 = MainPage.aLeftFace[2];
            string cColorLeft5 = MainPage.aLeftFace[5];
            string cColorLeft8 = MainPage.aLeftFace[8];

            if (cDirection == "+")
            {
                MainPage.aUpFace[4] = cColorLeft8;
                MainPage.aUpFace[5] = cColorLeft5;
                MainPage.aUpFace[6] = cColorLeft2;

                MainPage.aRightFace[2] = cColorUp4;
                MainPage.aRightFace[5] = cColorUp5;
                MainPage.aRightFace[8] = cColorUp6;

                MainPage.aDownFace[4] = cColorRight8;
                MainPage.aDownFace[5] = cColorRight5;
                MainPage.aDownFace[6] = cColorRight2;

                MainPage.aLeftFace[2] = cColorDown4;
                MainPage.aLeftFace[5] = cColorDown5;
                MainPage.aLeftFace[8] = cColorDown6;
            }

            if (cDirection == "-")
            {
                MainPage.aUpFace[4] = cColorRight2;
                MainPage.aUpFace[5] = cColorRight5;
                MainPage.aUpFace[6] = cColorRight8;

                MainPage.aRightFace[2] = cColorDown6;
                MainPage.aRightFace[5] = cColorDown5;
                MainPage.aRightFace[8] = cColorDown4;

                MainPage.aDownFace[4] = cColorLeft2;
                MainPage.aDownFace[5] = cColorLeft5;
                MainPage.aDownFace[6] = cColorLeft8;

                MainPage.aLeftFace[2] = cColorUp6;
                MainPage.aLeftFace[5] = cColorUp5;
                MainPage.aLeftFace[8] = cColorUp4;
            }
        }

        // Turn the entire back face clockwise or counter clockwise.
        public static void TurnBackFaceTo(string cDirection)
        {
            string cColorBack1 = MainPage.aBackFace[1];
            string cColorBack2 = MainPage.aBackFace[2];
            string cColorBack3 = MainPage.aBackFace[3];
            string cColorBack4 = MainPage.aBackFace[4];
            string cColorBack6 = MainPage.aBackFace[6];
            string cColorBack7 = MainPage.aBackFace[7];
            string cColorBack8 = MainPage.aBackFace[8];
            string cColorBack9 = MainPage.aBackFace[9];

            string cColorUp1 = MainPage.aUpFace[1];
            string cColorUp2 = MainPage.aUpFace[2];
            string cColorUp3 = MainPage.aUpFace[3];

            string cColorRight3 = MainPage.aRightFace[3];
            string cColorRight6 = MainPage.aRightFace[6];
            string cColorRight9 = MainPage.aRightFace[9];

            string cColorDown7 = MainPage.aDownFace[7];
            string cColorDown8 = MainPage.aDownFace[8];
            string cColorDown9 = MainPage.aDownFace[9];

            string cColorLeft1 = MainPage.aLeftFace[1];
            string cColorLeft4 = MainPage.aLeftFace[4];
            string cColorLeft7 = MainPage.aLeftFace[7];

            if (cDirection == "+")
            {
                MainPage.aBackFace[1] = cColorBack7;
                MainPage.aBackFace[2] = cColorBack4;
                MainPage.aBackFace[3] = cColorBack1;
                MainPage.aBackFace[4] = cColorBack8;
                MainPage.aBackFace[6] = cColorBack2;
                MainPage.aBackFace[7] = cColorBack9;
                MainPage.aBackFace[8] = cColorBack6;
                MainPage.aBackFace[9] = cColorBack3;

                MainPage.aUpFace[1] = cColorRight3;
                MainPage.aUpFace[2] = cColorRight6;
                MainPage.aUpFace[3] = cColorRight9;

                MainPage.aRightFace[3] = cColorDown9;
                MainPage.aRightFace[6] = cColorDown8;
                MainPage.aRightFace[9] = cColorDown7;

                MainPage.aDownFace[7] = cColorLeft1;
                MainPage.aDownFace[8] = cColorLeft4;
                MainPage.aDownFace[9] = cColorLeft7;

                MainPage.aLeftFace[1] = cColorUp3;
                MainPage.aLeftFace[4] = cColorUp2;
                MainPage.aLeftFace[7] = cColorUp1;
            }

            if (cDirection == "-")
            {
                MainPage.aBackFace[1] = cColorBack3;
                MainPage.aBackFace[2] = cColorBack6;
                MainPage.aBackFace[3] = cColorBack9;
                MainPage.aBackFace[4] = cColorBack2;
                MainPage.aBackFace[6] = cColorBack8;
                MainPage.aBackFace[7] = cColorBack1;
                MainPage.aBackFace[8] = cColorBack4;
                MainPage.aBackFace[9] = cColorBack7;

                MainPage.aUpFace[1] = cColorLeft7;
                MainPage.aUpFace[2] = cColorLeft4;
                MainPage.aUpFace[3] = cColorLeft1;

                MainPage.aRightFace[3] = cColorUp1;
                MainPage.aRightFace[6] = cColorUp2;
                MainPage.aRightFace[9] = cColorUp3;

                MainPage.aDownFace[7] = cColorRight9;
                MainPage.aDownFace[8] = cColorRight6;
                MainPage.aDownFace[9] = cColorRight3;

                MainPage.aLeftFace[1] = cColorDown7;
                MainPage.aLeftFace[4] = cColorDown8;
                MainPage.aLeftFace[7] = cColorDown9;
            }
        }

        // Turn the entire left face clockwise or counter clockwise.
        public static void TurnLeftFaceTo(string cDirection)
        {
            string cColorLeft1 = MainPage.aLeftFace[1];
            string cColorLeft2 = MainPage.aLeftFace[2];
            string cColorLeft3 = MainPage.aLeftFace[3];
            string cColorLeft4 = MainPage.aLeftFace[4];
            string cColorLeft6 = MainPage.aLeftFace[6];
            string cColorLeft7 = MainPage.aLeftFace[7];
            string cColorLeft8 = MainPage.aLeftFace[8];
            string cColorLeft9 = MainPage.aLeftFace[9];

            string cColorUp1 = MainPage.aUpFace[1];
            string cColorUp4 = MainPage.aUpFace[4];
            string cColorUp7 = MainPage.aUpFace[7];

            string cColorFront1 = MainPage.aFrontFace[1];
            string cColorFront4 = MainPage.aFrontFace[4];
            string cColorFront7 = MainPage.aFrontFace[7];

            string cColorDown1 = MainPage.aDownFace[1];
            string cColorDown4 = MainPage.aDownFace[4];
            string cColorDown7 = MainPage.aDownFace[7];

            string cColorBack3 = MainPage.aBackFace[3];
            string cColorBack6 = MainPage.aBackFace[6];
            string cColorBack9 = MainPage.aBackFace[9];

            if (cDirection == "+")
            {
                MainPage.aLeftFace[1] = cColorLeft7;
                MainPage.aLeftFace[2] = cColorLeft4;
                MainPage.aLeftFace[3] = cColorLeft1;
                MainPage.aLeftFace[4] = cColorLeft8;
                MainPage.aLeftFace[6] = cColorLeft2;
                MainPage.aLeftFace[7] = cColorLeft9;
                MainPage.aLeftFace[8] = cColorLeft6;
                MainPage.aLeftFace[9] = cColorLeft3;

                MainPage.aUpFace[1] = cColorBack9;
                MainPage.aUpFace[4] = cColorBack6;
                MainPage.aUpFace[7] = cColorBack3;

                MainPage.aFrontFace[1] = cColorUp1;
                MainPage.aFrontFace[4] = cColorUp4;
                MainPage.aFrontFace[7] = cColorUp7;

                MainPage.aDownFace[1] = cColorFront1;
                MainPage.aDownFace[4] = cColorFront4;
                MainPage.aDownFace[7] = cColorFront7;

                MainPage.aBackFace[3] = cColorDown7;
                MainPage.aBackFace[6] = cColorDown4;
                MainPage.aBackFace[9] = cColorDown1;
            }

            if (cDirection == "-")
            {
                MainPage.aLeftFace[1] = cColorLeft3;
                MainPage.aLeftFace[2] = cColorLeft6;
                MainPage.aLeftFace[3] = cColorLeft9;
                MainPage.aLeftFace[4] = cColorLeft2;
                MainPage.aLeftFace[6] = cColorLeft8;
                MainPage.aLeftFace[7] = cColorLeft1;
                MainPage.aLeftFace[8] = cColorLeft4;
                MainPage.aLeftFace[9] = cColorLeft7;

                MainPage.aUpFace[1] = cColorFront1;
                MainPage.aUpFace[4] = cColorFront4;
                MainPage.aUpFace[7] = cColorFront7;

                MainPage.aFrontFace[1] = cColorDown1;
                MainPage.aFrontFace[4] = cColorDown4;
                MainPage.aFrontFace[7] = cColorDown7;

                MainPage.aDownFace[1] = cColorBack9;
                MainPage.aDownFace[4] = cColorBack6;
                MainPage.aDownFace[7] = cColorBack3;

                MainPage.aBackFace[3] = cColorUp7;
                MainPage.aBackFace[6] = cColorUp4;
                MainPage.aBackFace[9] = cColorUp1;
            }
        }

        // Turn the top vertical middle layer to back or front.
        public static void TurnUpVerMiddleTo(string cDirection)
        {
            string cColorUp2 = MainPage.aUpFace[2];
            string cColorUp5 = MainPage.aUpFace[5];
            string cColorUp8 = MainPage.aUpFace[8];

            string cColorFront2 = MainPage.aFrontFace[2];
            string cColorFront5 = MainPage.aFrontFace[5];
            string cColorFront8 = MainPage.aFrontFace[8];

            string cColorDown2 = MainPage.aDownFace[2];
            string cColorDown5 = MainPage.aDownFace[5];
            string cColorDown8 = MainPage.aDownFace[8];

            string cColorBack2 = MainPage.aBackFace[2];
            string cColorBack5 = MainPage.aBackFace[5];
            string cColorBack8 = MainPage.aBackFace[8];

            if (cDirection == "+")
            {
                MainPage.aUpFace[2] = cColorFront2;
                MainPage.aUpFace[5] = cColorFront5;
                MainPage.aUpFace[8] = cColorFront8;

                MainPage.aFrontFace[2] = cColorDown2;
                MainPage.aFrontFace[5] = cColorDown5;
                MainPage.aFrontFace[8] = cColorDown8;

                MainPage.aDownFace[2] = cColorBack8;
                MainPage.aDownFace[5] = cColorBack5;
                MainPage.aDownFace[8] = cColorBack2;

                MainPage.aBackFace[2] = cColorUp8;
                MainPage.aBackFace[5] = cColorUp5;
                MainPage.aBackFace[8] = cColorUp2;
            }

            if (cDirection == "-")
            {
                MainPage.aUpFace[2] = cColorBack8;
                MainPage.aUpFace[5] = cColorBack5;
                MainPage.aUpFace[8] = cColorBack2;

                MainPage.aFrontFace[2] = cColorUp2;
                MainPage.aFrontFace[5] = cColorUp5;
                MainPage.aFrontFace[8] = cColorUp8;

                MainPage.aDownFace[2] = cColorFront2;
                MainPage.aDownFace[5] = cColorFront5;
                MainPage.aDownFace[8] = cColorFront8;

                MainPage.aBackFace[2] = cColorDown8;
                MainPage.aBackFace[5] = cColorDown5;
                MainPage.aBackFace[8] = cColorDown2;
            }
        }

        // Turn the entire right face clockwise or counter clockwise.
        public static void TurnRightFaceTo(string cDirection)
        {
            string cColorRight1 = MainPage.aRightFace[1];
            string cColorRight2 = MainPage.aRightFace[2];
            string cColorRight3 = MainPage.aRightFace[3];
            string cColorRight4 = MainPage.aRightFace[4];
            string cColorRight6 = MainPage.aRightFace[6];
            string cColorRight7 = MainPage.aRightFace[7];
            string cColorRight8 = MainPage.aRightFace[8];
            string cColorRight9 = MainPage.aRightFace[9];

            string cColorUp3 = MainPage.aUpFace[3];
            string cColorUp6 = MainPage.aUpFace[6];
            string cColorUp9 = MainPage.aUpFace[9];

            string cColorFront3 = MainPage.aFrontFace[3];
            string cColorFront6 = MainPage.aFrontFace[6];
            string cColorFront9 = MainPage.aFrontFace[9];

            string cColorDown3 = MainPage.aDownFace[3];
            string cColorDown6 = MainPage.aDownFace[6];
            string cColorDown9 = MainPage.aDownFace[9];

            string cColorBack1 = MainPage.aBackFace[1];
            string cColorBack4 = MainPage.aBackFace[4];
            string cColorBack7 = MainPage.aBackFace[7];

            if (cDirection == "+")
            {
                MainPage.aRightFace[1] = cColorRight7;
                MainPage.aRightFace[2] = cColorRight4;
                MainPage.aRightFace[3] = cColorRight1;
                MainPage.aRightFace[4] = cColorRight8;
                MainPage.aRightFace[6] = cColorRight2;
                MainPage.aRightFace[7] = cColorRight9;
                MainPage.aRightFace[8] = cColorRight6;
                MainPage.aRightFace[9] = cColorRight3;

                MainPage.aUpFace[3] = cColorFront3;
                MainPage.aUpFace[6] = cColorFront6;
                MainPage.aUpFace[9] = cColorFront9;

                MainPage.aFrontFace[3] = cColorDown3;
                MainPage.aFrontFace[6] = cColorDown6;
                MainPage.aFrontFace[9] = cColorDown9;

                MainPage.aDownFace[3] = cColorBack7;
                MainPage.aDownFace[6] = cColorBack4;
                MainPage.aDownFace[9] = cColorBack1;

                MainPage.aBackFace[1] = cColorUp9;
                MainPage.aBackFace[4] = cColorUp6;
                MainPage.aBackFace[7] = cColorUp3;
            }

            if (cDirection == "-")
            {
                MainPage.aRightFace[1] = cColorRight3;
                MainPage.aRightFace[2] = cColorRight6;
                MainPage.aRightFace[3] = cColorRight9;
                MainPage.aRightFace[4] = cColorRight2;
                MainPage.aRightFace[6] = cColorRight8;
                MainPage.aRightFace[7] = cColorRight1;
                MainPage.aRightFace[8] = cColorRight4;
                MainPage.aRightFace[9] = cColorRight7;

                MainPage.aUpFace[3] = cColorBack7;
                MainPage.aUpFace[6] = cColorBack4;
                MainPage.aUpFace[9] = cColorBack1;

                MainPage.aFrontFace[3] = cColorUp3;
                MainPage.aFrontFace[6] = cColorUp6;
                MainPage.aFrontFace[9] = cColorUp9;

                MainPage.aDownFace[3] = cColorFront3;
                MainPage.aDownFace[6] = cColorFront6;
                MainPage.aDownFace[9] = cColorFront9;

                MainPage.aBackFace[1] = cColorDown9;
                MainPage.aBackFace[4] = cColorDown6;
                MainPage.aBackFace[7] = cColorDown3;
            }
        }

        // Turn the entire upper face clockwise or counter clockwise.
        public static void TurnUpFaceTo(string cDirection)
        {
            string cColorUp1 = MainPage.aUpFace[1];
            string cColorUp2 = MainPage.aUpFace[2];
            string cColorUp3 = MainPage.aUpFace[3];
            string cColorUp4 = MainPage.aUpFace[4];
            string cColorUp6 = MainPage.aUpFace[6];
            string cColorUp7 = MainPage.aUpFace[7];
            string cColorUp8 = MainPage.aUpFace[8];
            string cColorUp9 = MainPage.aUpFace[9];

            string cColorLeft1 = MainPage.aLeftFace[1];
            string cColorLeft2 = MainPage.aLeftFace[2];
            string cColorLeft3 = MainPage.aLeftFace[3];

            string cColorFront1 = MainPage.aFrontFace[1];
            string cColorFront2 = MainPage.aFrontFace[2];
            string cColorFront3 = MainPage.aFrontFace[3];

            string cColorRight1 = MainPage.aRightFace[1];
            string cColorRight2 = MainPage.aRightFace[2];
            string cColorRight3 = MainPage.aRightFace[3];

            string cColorBack1 = MainPage.aBackFace[1];
            string cColorBack2 = MainPage.aBackFace[2];
            string cColorBack3 = MainPage.aBackFace[3];

            if (cDirection == "+")
            {
                MainPage.aUpFace[1] = cColorUp7;
                MainPage.aUpFace[2] = cColorUp4;
                MainPage.aUpFace[3] = cColorUp1;
                MainPage.aUpFace[4] = cColorUp8;
                MainPage.aUpFace[6] = cColorUp2;
                MainPage.aUpFace[7] = cColorUp9;
                MainPage.aUpFace[8] = cColorUp6;
                MainPage.aUpFace[9] = cColorUp3;

                MainPage.aLeftFace[1] = cColorFront1;
                MainPage.aLeftFace[2] = cColorFront2;
                MainPage.aLeftFace[3] = cColorFront3;

                MainPage.aFrontFace[1] = cColorRight1;
                MainPage.aFrontFace[2] = cColorRight2;
                MainPage.aFrontFace[3] = cColorRight3;

                MainPage.aRightFace[1] = cColorBack1;
                MainPage.aRightFace[2] = cColorBack2;
                MainPage.aRightFace[3] = cColorBack3;

                MainPage.aBackFace[1] = cColorLeft1;
                MainPage.aBackFace[2] = cColorLeft2;
                MainPage.aBackFace[3] = cColorLeft3;
            }

            if (cDirection == "-")
            {
                MainPage.aUpFace[1] = cColorUp3;
                MainPage.aUpFace[2] = cColorUp6;
                MainPage.aUpFace[3] = cColorUp9;
                MainPage.aUpFace[4] = cColorUp2;
                MainPage.aUpFace[6] = cColorUp8;
                MainPage.aUpFace[7] = cColorUp1;
                MainPage.aUpFace[8] = cColorUp4;
                MainPage.aUpFace[9] = cColorUp7;

                MainPage.aLeftFace[1] = cColorBack1;
                MainPage.aLeftFace[2] = cColorBack2;
                MainPage.aLeftFace[3] = cColorBack3;

                MainPage.aFrontFace[1] = cColorLeft1;
                MainPage.aFrontFace[2] = cColorLeft2;
                MainPage.aFrontFace[3] = cColorLeft3;

                MainPage.aRightFace[1] = cColorFront1;
                MainPage.aRightFace[2] = cColorFront2;
                MainPage.aRightFace[3] = cColorFront3;

                MainPage.aBackFace[1] = cColorRight1;
                MainPage.aBackFace[2] = cColorRight2;
                MainPage.aBackFace[3] = cColorRight3;
            }
        }

        // Turn the front horizontal middle layer to right or left.
        public static void TurnFrontHorMiddleTo(string cDirection)
        {
            string cColorFront4 = MainPage.aFrontFace[4];
            string cColorFront5 = MainPage.aFrontFace[5];
            string cColorFront6 = MainPage.aFrontFace[6];

            string cColorRight4 = MainPage.aRightFace[4];
            string cColorRight5 = MainPage.aRightFace[5];
            string cColorRight6 = MainPage.aRightFace[6];

            string cColorBack4 = MainPage.aBackFace[4];
            string cColorBack5 = MainPage.aBackFace[5];
            string cColorBack6 = MainPage.aBackFace[6];

            string cColorLeft4 = MainPage.aLeftFace[4];
            string cColorLeft5 = MainPage.aLeftFace[5];
            string cColorLeft6 = MainPage.aLeftFace[6];

            if (cDirection == "+")
            {
                MainPage.aFrontFace[4] = cColorRight4;
                MainPage.aFrontFace[5] = cColorRight5;
                MainPage.aFrontFace[6] = cColorRight6;

                MainPage.aRightFace[4] = cColorBack4;
                MainPage.aRightFace[5] = cColorBack5;
                MainPage.aRightFace[6] = cColorBack6;

                MainPage.aBackFace[4] = cColorLeft4;
                MainPage.aBackFace[5] = cColorLeft5;
                MainPage.aBackFace[6] = cColorLeft6;

                MainPage.aLeftFace[4] = cColorFront4;
                MainPage.aLeftFace[5] = cColorFront5;
                MainPage.aLeftFace[6] = cColorFront6;
            }

            if (cDirection == "-")
            {
                MainPage.aFrontFace[4] = cColorLeft4;
                MainPage.aFrontFace[5] = cColorLeft5;
                MainPage.aFrontFace[6] = cColorLeft6;

                MainPage.aRightFace[4] = cColorFront4;
                MainPage.aRightFace[5] = cColorFront5;
                MainPage.aRightFace[6] = cColorFront6;

                MainPage.aBackFace[4] = cColorRight4;
                MainPage.aBackFace[5] = cColorRight5;
                MainPage.aBackFace[6] = cColorRight6;

                MainPage.aLeftFace[4] = cColorBack4;
                MainPage.aLeftFace[5] = cColorBack5;
                MainPage.aLeftFace[6] = cColorBack6;
            }
        }

        // Turn the entire down face clockwise or counter clockwise.
        public static void TurnDownFaceTo(string cDirection)
        {
            string cColorDown1 = MainPage.aDownFace[1];
            string cColorDown2 = MainPage.aDownFace[2];
            string cColorDown3 = MainPage.aDownFace[3];
            string cColorDown4 = MainPage.aDownFace[4];
            string cColorDown6 = MainPage.aDownFace[6];
            string cColorDown7 = MainPage.aDownFace[7];
            string cColorDown8 = MainPage.aDownFace[8];
            string cColorDown9 = MainPage.aDownFace[9];

            string cColorLeft7 = MainPage.aLeftFace[7];
            string cColorLeft8 = MainPage.aLeftFace[8];
            string cColorLeft9 = MainPage.aLeftFace[9];

            string cColorFront7 = MainPage.aFrontFace[7];
            string cColorFront8 = MainPage.aFrontFace[8];
            string cColorFront9 = MainPage.aFrontFace[9];

            string cColorRight7 = MainPage.aRightFace[7];
            string cColorRight8 = MainPage.aRightFace[8];
            string cColorRight9 = MainPage.aRightFace[9];

            string cColorBack7 = MainPage.aBackFace[7];
            string cColorBack8 = MainPage.aBackFace[8];
            string cColorBack9 = MainPage.aBackFace[9];

            if (cDirection == "+")
            {
                MainPage.aDownFace[1] = cColorDown7;
                MainPage.aDownFace[2] = cColorDown4;
                MainPage.aDownFace[3] = cColorDown1;
                MainPage.aDownFace[4] = cColorDown8;
                MainPage.aDownFace[6] = cColorDown2;
                MainPage.aDownFace[7] = cColorDown9;
                MainPage.aDownFace[8] = cColorDown6;
                MainPage.aDownFace[9] = cColorDown3;

                MainPage.aLeftFace[7] = cColorBack7;
                MainPage.aLeftFace[8] = cColorBack8;
                MainPage.aLeftFace[9] = cColorBack9;

                MainPage.aFrontFace[7] = cColorLeft7;
                MainPage.aFrontFace[8] = cColorLeft8;
                MainPage.aFrontFace[9] = cColorLeft9;

                MainPage.aRightFace[7] = cColorFront7;
                MainPage.aRightFace[8] = cColorFront8;
                MainPage.aRightFace[9] = cColorFront9;

                MainPage.aBackFace[7] = cColorRight7;
                MainPage.aBackFace[8] = cColorRight8;
                MainPage.aBackFace[9] = cColorRight9;
            }

            if (cDirection == "-")
            {
                MainPage.aDownFace[1] = cColorDown3;
                MainPage.aDownFace[2] = cColorDown6;
                MainPage.aDownFace[3] = cColorDown9;
                MainPage.aDownFace[4] = cColorDown2;
                MainPage.aDownFace[6] = cColorDown8;
                MainPage.aDownFace[7] = cColorDown1;
                MainPage.aDownFace[8] = cColorDown4;
                MainPage.aDownFace[9] = cColorDown7;

                MainPage.aLeftFace[7] = cColorFront7;
                MainPage.aLeftFace[8] = cColorFront8;
                MainPage.aLeftFace[9] = cColorFront9;

                MainPage.aFrontFace[7] = cColorRight7;
                MainPage.aFrontFace[8] = cColorRight8;
                MainPage.aFrontFace[9] = cColorRight9;

                MainPage.aRightFace[7] = cColorBack7;
                MainPage.aRightFace[8] = cColorBack8;
                MainPage.aRightFace[9] = cColorBack9;

                MainPage.aBackFace[7] = cColorLeft7;
                MainPage.aBackFace[8] = cColorLeft8;
                MainPage.aBackFace[9] = cColorLeft9;
            }
            //--------------------------------------------------------
            //CopyCubePieceColorsToTemporaryArray();

            //if (cDirection == "+")
            //{
            //    MainPage.aPieces[45] = MainPage.aPiecesTemp[51];
            //    MainPage.aPieces[46] = MainPage.aPiecesTemp[48];
            //    MainPage.aPieces[47] = MainPage.aPiecesTemp[45];
            //    MainPage.aPieces[48] = MainPage.aPiecesTemp[52];
            //    MainPage.aPieces[50] = MainPage.aPiecesTemp[46];
            //    MainPage.aPieces[51] = MainPage.aPiecesTemp[53];
            //    MainPage.aPieces[52] = MainPage.aPiecesTemp[50];
            //    MainPage.aPieces[53] = MainPage.aPiecesTemp[47];

            //    MainPage.aPieces[33] = MainPage.aPiecesTemp[24];
            //    MainPage.aPieces[34] = MainPage.aPiecesTemp[25];
            //    MainPage.aPieces[35] = MainPage.aPiecesTemp[26];

            //    MainPage.aPieces[6] = MainPage.aPiecesTemp[33];
            //    MainPage.aPieces[7] = MainPage.aPiecesTemp[34];
            //    MainPage.aPieces[8] = MainPage.aPiecesTemp[35];

            //    MainPage.aPieces[15] = MainPage.aPiecesTemp[6];
            //    MainPage.aPieces[16] = MainPage.aPiecesTemp[7];
            //    MainPage.aPieces[17] = MainPage.aPiecesTemp[8];

            //    MainPage.aPieces[24] = MainPage.aPiecesTemp[15];
            //    MainPage.aPieces[25] = MainPage.aPiecesTemp[16];
            //    MainPage.aPieces[26] = MainPage.aPiecesTemp[17];
            //}

            //if (cDirection == "-")
            //{
            //    MainPage.aPieces[45] = MainPage.aPiecesTemp[47];
            //    MainPage.aPieces[46] = MainPage.aPiecesTemp[50];
            //    MainPage.aPieces[47] = MainPage.aPiecesTemp[53];
            //    MainPage.aPieces[48] = MainPage.aPiecesTemp[46];
            //    MainPage.aPieces[50] = MainPage.aPiecesTemp[52];
            //    MainPage.aPieces[51] = MainPage.aPiecesTemp[45];
            //    MainPage.aPieces[52] = MainPage.aPiecesTemp[48];
            //    MainPage.aPieces[53] = MainPage.aPiecesTemp[51];

            //    MainPage.aPieces[33] = MainPage.aPiecesTemp[6];
            //    MainPage.aPieces[34] = MainPage.aPiecesTemp[7];
            //    MainPage.aPieces[35] = MainPage.aPiecesTemp[8];

            //    MainPage.aPieces[6] = MainPage.aPiecesTemp[15];
            //    MainPage.aPieces[7] = MainPage.aPiecesTemp[16];
            //    MainPage.aPieces[8] = MainPage.aPiecesTemp[17];

            //    MainPage.aPieces[15] = MainPage.aPiecesTemp[24];
            //    MainPage.aPieces[16] = MainPage.aPiecesTemp[25];
            //    MainPage.aPieces[17] = MainPage.aPiecesTemp[26];

            //    MainPage.aPieces[24] = MainPage.aPiecesTemp[33];
            //    MainPage.aPieces[25] = MainPage.aPiecesTemp[34];
            //    MainPage.aPieces[26] = MainPage.aPiecesTemp[35];
            //}
        }
    }
}
