namespace RubiksCube
{
    internal class ClassCubeTurns
    {
        // Turn the faces of the cube
        public async Task TurnFaceCubeAsync(string cTurnFaceAndDirection)
        {
            switch (cTurnFaceAndDirection)
            {
                case "TurnFront+":
                    TurnFrontFaceTo("+");
                    break;
                case "TurnFront-":
                    TurnFrontFaceTo("-");
                    break;
                case "TurnUp+":
                    TurnUpFaceTo("+");
                    break;
                case "TurnUp-":
                    TurnUpFaceTo("-");
                    break;
                case "TurnDown+":
                    TurnDownFaceTo("+");
                    break;
                case "TurnDown-":
                    TurnDownFaceTo("-");
                    break;
                case "TurnLeft+":
                    TurnLeftFaceTo("+");
                    break;
                case "TurnLeft-":
                    TurnLeftFaceTo("-");
                    break;
                case "TurnRight+":
                    TurnRightFaceTo("+");
                    break;
                case "TurnRight-":
                    TurnRightFaceTo("-");
                    break;
                case "TurnBack+":
                    TurnBackFaceTo("+");
                    break;
                case "TurnBack-":
                    TurnBackFaceTo("-");
                    break;

                case "TurnFront++":
                    TurnFrontFaceTo("+");
                    TurnFrontFaceTo("+");
                    break;
                case "TurnFront--":
                    TurnFrontFaceTo("-");
                    TurnFrontFaceTo("-");
                    break;
                case "TurnUp++":
                    TurnUpFaceTo("+");
                    TurnUpFaceTo("+");
                    break;
                case "TurnUp--":
                    TurnUpFaceTo("-");
                    TurnUpFaceTo("-");
                    break;
                case "TurnDown++":
                    TurnDownFaceTo("+");
                    TurnDownFaceTo("+");
                    break;
                case "TurnDown--":
                    TurnDownFaceTo("-");
                    TurnDownFaceTo("-");
                    break;
                case "TurnLeft++":
                    TurnLeftFaceTo("+");
                    TurnLeftFaceTo("+");
                    break;
                case "TurnLeft--":
                    TurnLeftFaceTo("-");
                    TurnLeftFaceTo("-");
                    break;
                case "TurnRight++":
                    TurnRightFaceTo("+");
                    TurnRightFaceTo("+");
                    break;
                case "TurnRight--":
                    TurnRightFaceTo("-");
                    TurnRightFaceTo("-");
                    break;
                case "TurnBack++":
                    TurnBackFaceTo("+");
                    TurnBackFaceTo("+");
                    break;
                case "TurnBack--":
                    TurnBackFaceTo("-");
                    TurnBackFaceTo("-");
                    break;

                case "TurnUpHorMiddleRight+":
                    TurnUpHorMiddleTo("+");
                    break;
                case "TurnUpHorMiddleLeft-":
                    TurnUpHorMiddleTo("-");
                    break;
                case "TurnUpVerMiddleBack+":
                    TurnUpVerMiddleTo("+");
                    break;
                case "TurnUpVerMiddleFront-":
                    TurnUpVerMiddleTo("-");
                    break;
                case "TurnFrontHorMiddleLeft+":
                    TurnFrontHorMiddleTo("+");
                    break;
                case "TurnFrontHorMiddleRight-":
                    TurnFrontHorMiddleTo("-");
                    break;

                case "TurnUpHorMiddleRight++":
                    TurnUpHorMiddleTo("+");
                    TurnUpHorMiddleTo("+");
                    break;
                case "TurnUpHorMiddleLeft--":
                    TurnUpHorMiddleTo("-");
                    TurnUpHorMiddleTo("-");
                    break;
                case "TurnUpVerMiddleBack++":
                    TurnUpVerMiddleTo("+");
                    TurnUpVerMiddleTo("+");
                    break;
                case "TurnUpVerMiddleFront--":
                    TurnUpVerMiddleTo("-");
                    TurnUpVerMiddleTo("-");
                    break;
                case "TurnFrontHorMiddleLeft++":
                    TurnFrontHorMiddleTo("+");
                    TurnFrontHorMiddleTo("+");
                    break;
                case "TurnFrontHorMiddleRight--":
                    TurnFrontHorMiddleTo("-");
                    TurnFrontHorMiddleTo("-");
                    break;

                case "TurnCubeFrontToRight":
                    TurnCubeFrontFaceToRightFace();
                    break;
                case "TurnCubeFrontToLeft":
                    TurnCubeFrontFaceToLeftFace();
                    break;
                case "TurnCubeFrontToUp":
                    TurnCubeFrontFaceToUpFace();
                    break;
                case "TurnCubeFrontToDown":
                    TurnCubeFrontFaceToDownFace();
                    break;
                case "TurnCubeUpToRight":
                    TurnCubeUpFaceToRightFace();
                    break;
                case "TurnCubeUpToLeft":
                    TurnCubeUpFaceToLeftFace();
                    break;

                default:
                    //await DisplayAlert(CubeLang.ErrorTitle_Text, "Turn not found", CubeLang.ButtonClose_Text);
                    return;
            }
        }

        // Turn the entire cube a quarter turn
        // Rotate the entire cube so that the front goes to the left face
        private void TurnCubeFrontFaceToLeftFace()
        {
            TurnUpFaceTo("+");
            TurnFrontHorMiddleTo("+");
            TurnDownFaceTo("-");
        }

        // Rotate the entire cube so that the front goes to the right face
        private void TurnCubeFrontFaceToRightFace()
        {
            TurnUpFaceTo("-");
            TurnFrontHorMiddleTo("-");
            TurnDownFaceTo("+");
        }

        // Rotate the entire cube so that the front goes to the upper face
        private void TurnCubeFrontFaceToUpFace()
        {
            TurnRightFaceTo("+");
            TurnUpVerMiddleTo("+");
            TurnLeftFaceTo("-");
        }

        // Rotate the entire cube so that the front goes to the down face
        private void TurnCubeFrontFaceToDownFace()
        {
            TurnRightFaceTo("-");
            TurnUpVerMiddleTo("-");
            TurnLeftFaceTo("+");
        }

        // Rotate the entire cube so that the upper face goes to the right face
        private void TurnCubeUpFaceToRightFace()
        {
            TurnFrontFaceTo("+");
            TurnUpHorMiddleTo("+");
            TurnBackFaceTo("-");
        }

        // Rotate the entire cube so that the upper face goes to the left face
        private void TurnCubeUpFaceToLeftFace()
        {
            TurnFrontFaceTo("-");
            TurnUpHorMiddleTo("-");
            TurnBackFaceTo("+");
        }

        // Turn the entire front face clockwise or counter clockwise
        public static void TurnFrontFaceTo(string cDirection)
        {
            Array.Copy(Globals.aPieces, Globals.aPiecesTemp, 54);

            if (cDirection == "+")
            {
                Globals.aPieces[0] = Globals.aPiecesTemp[6]; // cColorFront7;
                Globals.aPieces[1] = Globals.aPiecesTemp[3]; // cColorFront4;
                Globals.aPieces[2] = Globals.aPiecesTemp[0]; // cColorFront1;
                Globals.aPieces[3] = Globals.aPiecesTemp[7]; // cColorFront8;
                Globals.aPieces[5] = Globals.aPiecesTemp[1]; // cColorFront2;
                Globals.aPieces[6] = Globals.aPiecesTemp[8]; // cColorFront9;
                Globals.aPieces[7] = Globals.aPiecesTemp[5]; // cColorFront6;
                Globals.aPieces[8] = Globals.aPiecesTemp[2]; // cColorFront3;

                Globals.aPieces[42] = Globals.aPiecesTemp[35]; // cColorLeft9;
                Globals.aPieces[43] = Globals.aPiecesTemp[32]; // cColorLeft6;
                Globals.aPieces[44] = Globals.aPiecesTemp[29]; // cColorLeft3;

                Globals.aPieces[9] = Globals.aPiecesTemp[42]; // cColorUp7;
                Globals.aPieces[12] = Globals.aPiecesTemp[43]; // cColorUp8;
                Globals.aPieces[15] = Globals.aPiecesTemp[44]; // cColorUp9;

                Globals.aPieces[45] = Globals.aPiecesTemp[15]; // cColorRight7;
                Globals.aPieces[46] = Globals.aPiecesTemp[12]; // cColorRight4;
                Globals.aPieces[47] = Globals.aPiecesTemp[9]; // cColorRight1;

                Globals.aPieces[29] = Globals.aPiecesTemp[45]; // cColorDown1;
                Globals.aPieces[32] = Globals.aPiecesTemp[46]; // cColorDown2;
                Globals.aPieces[35] = Globals.aPiecesTemp[47]; // cColorDown3;
            }

            if (cDirection == "-")
            {
                Globals.aPieces[0] = Globals.aPiecesTemp[2]; // cColorFront3;
                Globals.aPieces[1] = Globals.aPiecesTemp[5]; // cColorFront6;
                Globals.aPieces[2] = Globals.aPiecesTemp[8]; // cColorFront9;
                Globals.aPieces[3] = Globals.aPiecesTemp[1]; // cColorFront2;
                Globals.aPieces[5] = Globals.aPiecesTemp[7]; // cColorFront8;
                Globals.aPieces[6] = Globals.aPiecesTemp[0]; // cColorFront1;
                Globals.aPieces[7] = Globals.aPiecesTemp[3]; // cColorFront4;
                Globals.aPieces[8] = Globals.aPiecesTemp[6]; // cColorFront7;

                Globals.aPieces[42] = Globals.aPiecesTemp[9]; // cColorRight1;
                Globals.aPieces[43] = Globals.aPiecesTemp[12]; // cColorRight4;
                Globals.aPieces[44] = Globals.aPiecesTemp[15]; // cColorRight7;

                Globals.aPieces[9] = Globals.aPiecesTemp[47]; // cColorDown3;
                Globals.aPieces[12] = Globals.aPiecesTemp[46]; // cColorDown2;
                Globals.aPieces[15] = Globals.aPiecesTemp[45]; // cColorDown1;

                Globals.aPieces[45] = Globals.aPiecesTemp[29]; // cColorLeft3;
                Globals.aPieces[46] = Globals.aPiecesTemp[32]; // cColorLeft6;
                Globals.aPieces[47] = Globals.aPiecesTemp[35]; // cColorLeft9;

                Globals.aPieces[29] = Globals.aPiecesTemp[44]; // cColorUp9;
                Globals.aPieces[32] = Globals.aPiecesTemp[43]; // cColorUp8;
                Globals.aPieces[35] = Globals.aPiecesTemp[42]; // cColorUp7;
            }
        }

        // Turn the top horizontal middle layer to the right or left
        public static void TurnUpHorMiddleTo(string cDirection)
        {
            Array.Copy(Globals.aPieces, Globals.aPiecesTemp, 54);

            if (cDirection == "+")
            {
                Globals.aPieces[39] = Globals.aPiecesTemp[34]; // cColorLeft8;
                Globals.aPieces[40] = Globals.aPiecesTemp[31]; // cColorLeft5;
                Globals.aPieces[41] = Globals.aPiecesTemp[28]; // cColorLeft2;

                Globals.aPieces[10] = Globals.aPiecesTemp[39]; // cColorUp4;
                Globals.aPieces[13] = Globals.aPiecesTemp[40]; // cColorUp5;
                Globals.aPieces[16] = Globals.aPiecesTemp[41]; // cColorUp6;

                Globals.aPieces[48] = Globals.aPiecesTemp[16]; // cColorRight8;
                Globals.aPieces[49] = Globals.aPiecesTemp[13]; // cColorRight5;
                Globals.aPieces[50] = Globals.aPiecesTemp[10]; // cColorRight2;

                Globals.aPieces[28] = Globals.aPiecesTemp[48]; // cColorDown4;
                Globals.aPieces[31] = Globals.aPiecesTemp[49]; // cColorDown5;
                Globals.aPieces[34] = Globals.aPiecesTemp[50]; // cColorDown6;
            }

            if (cDirection == "-")
            {
                Globals.aPieces[39] = Globals.aPiecesTemp[10]; // cColorRight2;
                Globals.aPieces[40] = Globals.aPiecesTemp[13]; // cColorRight5;
                Globals.aPieces[41] = Globals.aPiecesTemp[16]; // cColorRight8;

                Globals.aPieces[10] = Globals.aPiecesTemp[50]; // cColorDown6;
                Globals.aPieces[13] = Globals.aPiecesTemp[49]; // cColorDown5;
                Globals.aPieces[16] = Globals.aPiecesTemp[48]; // cColorDown4;

                Globals.aPieces[48] = Globals.aPiecesTemp[28]; // cColorLeft2;
                Globals.aPieces[49] = Globals.aPiecesTemp[31]; // cColorLeft5;
                Globals.aPieces[50] = Globals.aPiecesTemp[34]; // cColorLeft8;

                Globals.aPieces[28] = Globals.aPiecesTemp[41]; // cColorUp6;
                Globals.aPieces[31] = Globals.aPiecesTemp[40]; // cColorUp5;
                Globals.aPieces[34] = Globals.aPiecesTemp[39]; // cColorUp4;
            }
        }

        // Turn the entire back face clockwise or counter clockwise
        public static void TurnBackFaceTo(string cDirection)
        {
            Array.Copy(Globals.aPieces, Globals.aPiecesTemp, 54);

            if (cDirection == "+")
            {
                Globals.aPieces[18] = Globals.aPiecesTemp[24]; // cColorBack7;
                Globals.aPieces[19] = Globals.aPiecesTemp[21]; // cColorBack4;
                Globals.aPieces[20] = Globals.aPiecesTemp[18]; // cColorBack1;
                Globals.aPieces[21] = Globals.aPiecesTemp[25]; // cColorBack8;
                Globals.aPieces[23] = Globals.aPiecesTemp[19]; // cColorBack2;
                Globals.aPieces[24] = Globals.aPiecesTemp[26]; // cColorBack9;
                Globals.aPieces[25] = Globals.aPiecesTemp[23]; // cColorBack6;
                Globals.aPieces[26] = Globals.aPiecesTemp[20]; // cColorBack3;

                Globals.aPieces[36] = Globals.aPiecesTemp[11]; // cColorRight3;
                Globals.aPieces[37] = Globals.aPiecesTemp[14]; // cColorRight6;
                Globals.aPieces[38] = Globals.aPiecesTemp[17]; // cColorRight9;

                Globals.aPieces[11] = Globals.aPiecesTemp[53]; // cColorDown9;
                Globals.aPieces[14] = Globals.aPiecesTemp[52]; // cColorDown8;
                Globals.aPieces[17] = Globals.aPiecesTemp[51]; // cColorDown7;

                Globals.aPieces[51] = Globals.aPiecesTemp[27]; // cColorLeft1;
                Globals.aPieces[52] = Globals.aPiecesTemp[30]; // cColorLeft4;
                Globals.aPieces[53] = Globals.aPiecesTemp[33]; // cColorLeft7;

                Globals.aPieces[27] = Globals.aPiecesTemp[38]; // cColorUp3;
                Globals.aPieces[30] = Globals.aPiecesTemp[37]; // cColorUp2;
                Globals.aPieces[33] = Globals.aPiecesTemp[36]; // cColorUp1;
            }

            if (cDirection == "-")
            {
                Globals.aPieces[18] = Globals.aPiecesTemp[20]; // cColorBack3;
                Globals.aPieces[19] = Globals.aPiecesTemp[23]; // cColorBack6;
                Globals.aPieces[20] = Globals.aPiecesTemp[26]; // cColorBack9;
                Globals.aPieces[21] = Globals.aPiecesTemp[19]; // cColorBack2;
                Globals.aPieces[23] = Globals.aPiecesTemp[25]; // cColorBack8;
                Globals.aPieces[24] = Globals.aPiecesTemp[18]; // cColorBack1;
                Globals.aPieces[25] = Globals.aPiecesTemp[21]; // cColorBack4;
                Globals.aPieces[26] = Globals.aPiecesTemp[24]; // cColorBack7;

                Globals.aPieces[36] = Globals.aPiecesTemp[33]; // cColorLeft7;
                Globals.aPieces[37] = Globals.aPiecesTemp[30]; // cColorLeft4;
                Globals.aPieces[38] = Globals.aPiecesTemp[27]; // cColorLeft1;

                Globals.aPieces[11] = Globals.aPiecesTemp[36]; // cColorUp1;
                Globals.aPieces[14] = Globals.aPiecesTemp[37]; // cColorUp2;
                Globals.aPieces[17] = Globals.aPiecesTemp[38]; // cColorUp3;

                Globals.aPieces[51] = Globals.aPiecesTemp[17]; // cColorRight9;
                Globals.aPieces[52] = Globals.aPiecesTemp[14]; // cColorRight6;
                Globals.aPieces[53] = Globals.aPiecesTemp[11]; // cColorRight3;

                Globals.aPieces[27] = Globals.aPiecesTemp[51]; // cColorDown7;
                Globals.aPieces[30] = Globals.aPiecesTemp[52]; // cColorDown8;
                Globals.aPieces[33] = Globals.aPiecesTemp[53]; // cColorDown9;
            }
        }

        // Turn the entire left face clockwise or counter clockwise
        public static void TurnLeftFaceTo(string cDirection)
        {
            Array.Copy(Globals.aPieces, Globals.aPiecesTemp, 54);

            if (cDirection == "+")
            {
                Globals.aPieces[27] = Globals.aPiecesTemp[33]; // cColorLeft7;
                Globals.aPieces[28] = Globals.aPiecesTemp[30]; // cColorLeft4;
                Globals.aPieces[29] = Globals.aPiecesTemp[27]; // cColorLeft1;
                Globals.aPieces[30] = Globals.aPiecesTemp[34]; // cColorLeft8;
                Globals.aPieces[32] = Globals.aPiecesTemp[28]; // cColorLeft2;
                Globals.aPieces[33] = Globals.aPiecesTemp[35]; // cColorLeft9;
                Globals.aPieces[34] = Globals.aPiecesTemp[32]; // cColorLeft6;
                Globals.aPieces[35] = Globals.aPiecesTemp[29]; // cColorLeft3;

                Globals.aPieces[36] = Globals.aPiecesTemp[26]; // cColorBack9;
                Globals.aPieces[39] = Globals.aPiecesTemp[23]; // cColorBack6;
                Globals.aPieces[42] = Globals.aPiecesTemp[20]; // cColorBack3;

                Globals.aPieces[0] = Globals.aPiecesTemp[36]; // cColorUp1;
                Globals.aPieces[3] = Globals.aPiecesTemp[39]; // cColorUp4;
                Globals.aPieces[6] = Globals.aPiecesTemp[42]; // cColorUp7;

                Globals.aPieces[45] = Globals.aPiecesTemp[0]; // cColorFront1;
                Globals.aPieces[48] = Globals.aPiecesTemp[3]; // cColorFront4;
                Globals.aPieces[51] = Globals.aPiecesTemp[6]; // cColorFront7;

                Globals.aPieces[20] = Globals.aPiecesTemp[51]; // cColorDown7;
                Globals.aPieces[23] = Globals.aPiecesTemp[48]; // cColorDown4;
                Globals.aPieces[26] = Globals.aPiecesTemp[45]; // cColorDown1;
            }

            if (cDirection == "-")
            {
                Globals.aPieces[27] = Globals.aPiecesTemp[29]; // cColorLeft3;
                Globals.aPieces[28] = Globals.aPiecesTemp[32]; // cColorLeft6;
                Globals.aPieces[29] = Globals.aPiecesTemp[35]; // cColorLeft9;
                Globals.aPieces[30] = Globals.aPiecesTemp[28]; // cColorLeft2;
                Globals.aPieces[32] = Globals.aPiecesTemp[34]; // cColorLeft8;
                Globals.aPieces[33] = Globals.aPiecesTemp[27]; // cColorLeft1;
                Globals.aPieces[34] = Globals.aPiecesTemp[30]; // cColorLeft4;
                Globals.aPieces[35] = Globals.aPiecesTemp[33]; // cColorLeft7;

                Globals.aPieces[36] = Globals.aPiecesTemp[0]; // cColorFront1;
                Globals.aPieces[39] = Globals.aPiecesTemp[3]; // cColorFront4;
                Globals.aPieces[42] = Globals.aPiecesTemp[6]; // cColorFront7;

                Globals.aPieces[0] = Globals.aPiecesTemp[45]; // cColorDown1;
                Globals.aPieces[3] = Globals.aPiecesTemp[48]; // cColorDown4;
                Globals.aPieces[6] = Globals.aPiecesTemp[51]; // cColorDown7;

                Globals.aPieces[45] = Globals.aPiecesTemp[26]; // cColorBack9;
                Globals.aPieces[48] = Globals.aPiecesTemp[23]; // cColorBack6;
                Globals.aPieces[51] = Globals.aPiecesTemp[20]; // cColorBack3;

                Globals.aPieces[20] = Globals.aPiecesTemp[42]; // cColorUp7;
                Globals.aPieces[23] = Globals.aPiecesTemp[39]; // cColorUp4;
                Globals.aPieces[26] = Globals.aPiecesTemp[36]; // cColorUp1;
            }
        }

        // Turn the top vertical middle layer to back or front
        public static void TurnUpVerMiddleTo(string cDirection)
        {
            Array.Copy(Globals.aPieces, Globals.aPiecesTemp, 54);

            if (cDirection == "+")
            {
                Globals.aPieces[37] = Globals.aPiecesTemp[1]; // cColorFront2;
                Globals.aPieces[40] = Globals.aPiecesTemp[4]; // cColorFront5;
                Globals.aPieces[43] = Globals.aPiecesTemp[7]; // cColorFront8;

                Globals.aPieces[1] = Globals.aPiecesTemp[46]; // cColorDown2;
                Globals.aPieces[4] = Globals.aPiecesTemp[49]; // cColorDown5;
                Globals.aPieces[7] = Globals.aPiecesTemp[52]; // cColorDown8;

                Globals.aPieces[46] = Globals.aPiecesTemp[25]; // cColorBack8;
                Globals.aPieces[49] = Globals.aPiecesTemp[22]; // cColorBack5;
                Globals.aPieces[52] = Globals.aPiecesTemp[19]; // cColorBack2;

                Globals.aPieces[19] = Globals.aPiecesTemp[43]; // cColorUp8;
                Globals.aPieces[22] = Globals.aPiecesTemp[40]; // cColorUp5;
                Globals.aPieces[25] = Globals.aPiecesTemp[37]; // cColorUp2;
            }

            if (cDirection == "-")
            {
                Globals.aPieces[37] = Globals.aPiecesTemp[25]; // cColorBack8;
                Globals.aPieces[40] = Globals.aPiecesTemp[22]; // cColorBack5;
                Globals.aPieces[43] = Globals.aPiecesTemp[19]; // cColorBack2;

                Globals.aPieces[1] = Globals.aPiecesTemp[37]; // cColorUp2;
                Globals.aPieces[4] = Globals.aPiecesTemp[40]; // cColorUp5;
                Globals.aPieces[7] = Globals.aPiecesTemp[43]; // cColorUp8;

                Globals.aPieces[46] = Globals.aPiecesTemp[1]; // cColorFront2;
                Globals.aPieces[49] = Globals.aPiecesTemp[4]; // cColorFront5;
                Globals.aPieces[52] = Globals.aPiecesTemp[7]; // cColorFront8;

                Globals.aPieces[19] = Globals.aPiecesTemp[52]; // cColorDown8;
                Globals.aPieces[22] = Globals.aPiecesTemp[49]; // cColorDown5;
                Globals.aPieces[25] = Globals.aPiecesTemp[46]; // cColorDown2;
            }
        }

        // Turn the entire right face clockwise or counter clockwise
        public static void TurnRightFaceTo(string cDirection)
        {
            Array.Copy(Globals.aPieces, Globals.aPiecesTemp, 54);

            if (cDirection == "+")
            {
                Globals.aPieces[9] = Globals.aPiecesTemp[15]; // cColorRight7;
                Globals.aPieces[10] = Globals.aPiecesTemp[12]; // cColorRight4;
                Globals.aPieces[11] = Globals.aPiecesTemp[9]; // cColorRight1;
                Globals.aPieces[12] = Globals.aPiecesTemp[16]; // cColorRight8;
                Globals.aPieces[14] = Globals.aPiecesTemp[10]; // cColorRight2;
                Globals.aPieces[15] = Globals.aPiecesTemp[17]; // cColorRight9;
                Globals.aPieces[16] = Globals.aPiecesTemp[14]; // cColorRight6;
                Globals.aPieces[17] = Globals.aPiecesTemp[11]; // cColorRight3;

                Globals.aPieces[38] = Globals.aPiecesTemp[2]; // cColorFront3;
                Globals.aPieces[41] = Globals.aPiecesTemp[5]; // cColorFront6;
                Globals.aPieces[44] = Globals.aPiecesTemp[8]; // cColorFront9;

                Globals.aPieces[2] = Globals.aPiecesTemp[47]; // cColorDown3;
                Globals.aPieces[5] = Globals.aPiecesTemp[50]; // cColorDown6;
                Globals.aPieces[8] = Globals.aPiecesTemp[53]; // cColorDown9;

                Globals.aPieces[47] = Globals.aPiecesTemp[24]; // cColorBack7;
                Globals.aPieces[50] = Globals.aPiecesTemp[21]; // cColorBack4;
                Globals.aPieces[53] = Globals.aPiecesTemp[18]; // cColorBack1;

                Globals.aPieces[18] = Globals.aPiecesTemp[44]; // cColorUp9;
                Globals.aPieces[21] = Globals.aPiecesTemp[41]; // cColorUp6;
                Globals.aPieces[24] = Globals.aPiecesTemp[38]; // cColorUp3;
            }

            if (cDirection == "-")
            {
                Globals.aPieces[9] = Globals.aPiecesTemp[11]; // cColorRight3;
                Globals.aPieces[10] = Globals.aPiecesTemp[14]; // cColorRight6;
                Globals.aPieces[11] = Globals.aPiecesTemp[17]; // cColorRight9;
                Globals.aPieces[12] = Globals.aPiecesTemp[10]; // cColorRight2;
                Globals.aPieces[14] = Globals.aPiecesTemp[16]; // cColorRight8;
                Globals.aPieces[15] = Globals.aPiecesTemp[9]; // cColorRight1;
                Globals.aPieces[16] = Globals.aPiecesTemp[12]; // cColorRight4;
                Globals.aPieces[17] = Globals.aPiecesTemp[15]; // cColorRight7;

                Globals.aPieces[38] = Globals.aPiecesTemp[24]; // cColorBack7;
                Globals.aPieces[41] = Globals.aPiecesTemp[21]; // cColorBack4;
                Globals.aPieces[44] = Globals.aPiecesTemp[18]; // cColorBack1;

                Globals.aPieces[2] = Globals.aPiecesTemp[38]; // cColorUp3;
                Globals.aPieces[5] = Globals.aPiecesTemp[41]; // cColorUp6;
                Globals.aPieces[8] = Globals.aPiecesTemp[44]; // cColorUp9;

                Globals.aPieces[47] = Globals.aPiecesTemp[2]; // cColorFront3;
                Globals.aPieces[50] = Globals.aPiecesTemp[5]; // cColorFront6;
                Globals.aPieces[53] = Globals.aPiecesTemp[8]; // cColorFront9;

                Globals.aPieces[18] = Globals.aPiecesTemp[53]; // cColorDown9;
                Globals.aPieces[21] = Globals.aPiecesTemp[50]; // cColorDown6;
                Globals.aPieces[24] = Globals.aPiecesTemp[47]; // cColorDown3;
            }
        }

        // Turn the entire upper face clockwise or counter clockwise
        public static void TurnUpFaceTo(string cDirection)
        {
            Array.Copy(Globals.aPieces, Globals.aPiecesTemp, 54);

            if (cDirection == "+")
            {
                Globals.aPieces[36] = Globals.aPiecesTemp[42]; // cColorUp7;
                Globals.aPieces[37] = Globals.aPiecesTemp[39]; // cColorUp4;
                Globals.aPieces[38] = Globals.aPiecesTemp[36]; // cColorUp1;
                Globals.aPieces[39] = Globals.aPiecesTemp[43]; // cColorUp8;
                Globals.aPieces[41] = Globals.aPiecesTemp[37]; // cColorUp2;
                Globals.aPieces[42] = Globals.aPiecesTemp[44]; // cColorUp9;
                Globals.aPieces[43] = Globals.aPiecesTemp[41]; // cColorUp6;
                Globals.aPieces[44] = Globals.aPiecesTemp[38]; // cColorUp3;

                Globals.aPieces[27] = Globals.aPiecesTemp[0]; // cColorFront1;
                Globals.aPieces[28] = Globals.aPiecesTemp[1]; // cColorFront2;
                Globals.aPieces[29] = Globals.aPiecesTemp[2]; // cColorFront3;

                Globals.aPieces[0] = Globals.aPiecesTemp[9]; // cColorRight1;
                Globals.aPieces[1] = Globals.aPiecesTemp[10]; // cColorRight2;
                Globals.aPieces[2] = Globals.aPiecesTemp[11]; // cColorRight3;

                Globals.aPieces[9] = Globals.aPiecesTemp[18]; // cColorBack1;
                Globals.aPieces[10] = Globals.aPiecesTemp[19]; // cColorBack2;
                Globals.aPieces[11] = Globals.aPiecesTemp[20]; // cColorBack3;

                Globals.aPieces[18] = Globals.aPiecesTemp[27]; // cColorLeft1;
                Globals.aPieces[19] = Globals.aPiecesTemp[28]; // cColorLeft2;
                Globals.aPieces[20] = Globals.aPiecesTemp[29]; // cColorLeft3;
            }

            if (cDirection == "-")
            {
                Globals.aPieces[36] = Globals.aPiecesTemp[38]; // cColorUp3;
                Globals.aPieces[37] = Globals.aPiecesTemp[41]; // cColorUp6;
                Globals.aPieces[38] = Globals.aPiecesTemp[44]; // cColorUp9;
                Globals.aPieces[39] = Globals.aPiecesTemp[37]; // cColorUp2;
                Globals.aPieces[41] = Globals.aPiecesTemp[43]; // cColorUp8;
                Globals.aPieces[42] = Globals.aPiecesTemp[36]; // cColorUp1;
                Globals.aPieces[43] = Globals.aPiecesTemp[39]; // cColorUp4;
                Globals.aPieces[44] = Globals.aPiecesTemp[42]; // cColorUp7;

                Globals.aPieces[27] = Globals.aPiecesTemp[18]; // cColorBack1;
                Globals.aPieces[28] = Globals.aPiecesTemp[19]; // cColorBack2;
                Globals.aPieces[29] = Globals.aPiecesTemp[20]; // cColorBack3;

                Globals.aPieces[0] = Globals.aPiecesTemp[27]; // cColorLeft1;
                Globals.aPieces[1] = Globals.aPiecesTemp[28]; // cColorLeft2;
                Globals.aPieces[2] = Globals.aPiecesTemp[29]; // cColorLeft3;

                Globals.aPieces[9] = Globals.aPiecesTemp[0]; // cColorFront1;
                Globals.aPieces[10] = Globals.aPiecesTemp[1]; // cColorFront2;
                Globals.aPieces[11] = Globals.aPiecesTemp[2]; // cColorFront3;

                Globals.aPieces[18] = Globals.aPiecesTemp[9]; // cColorRight1;
                Globals.aPieces[19] = Globals.aPiecesTemp[10]; // cColorRight2;
                Globals.aPieces[20] = Globals.aPiecesTemp[11]; // cColorRight3;
            }
        }

        // Turn the front horizontal middle layer to right or left
        public static void TurnFrontHorMiddleTo(string cDirection)
        {
            Array.Copy(Globals.aPieces, Globals.aPiecesTemp, 54);

            if (cDirection == "+")
            {
                Globals.aPieces[3] = Globals.aPiecesTemp[12]; // cColorRight4;
                Globals.aPieces[4] = Globals.aPiecesTemp[13]; // cColorRight5;
                Globals.aPieces[5] = Globals.aPiecesTemp[14]; // cColorRight6;

                Globals.aPieces[12] = Globals.aPiecesTemp[21]; // cColorBack4;
                Globals.aPieces[13] = Globals.aPiecesTemp[22]; // cColorBack5;
                Globals.aPieces[14] = Globals.aPiecesTemp[23]; // cColorBack6;

                Globals.aPieces[21] = Globals.aPiecesTemp[30]; // cColorLeft4;
                Globals.aPieces[22] = Globals.aPiecesTemp[31]; // cColorLeft5;
                Globals.aPieces[23] = Globals.aPiecesTemp[32]; // cColorLeft6;

                Globals.aPieces[30] = Globals.aPiecesTemp[3]; // cColorFront4;
                Globals.aPieces[31] = Globals.aPiecesTemp[4]; // cColorFront5;
                Globals.aPieces[32] = Globals.aPiecesTemp[5]; // cColorFront6;
            }

            if (cDirection == "-")
            {
                Globals.aPieces[3] = Globals.aPiecesTemp[30]; // cColorLeft4;
                Globals.aPieces[4] = Globals.aPiecesTemp[31]; // cColorLeft5;
                Globals.aPieces[5] = Globals.aPiecesTemp[32]; // cColorLeft6;

                Globals.aPieces[12] = Globals.aPiecesTemp[3]; // cColorFront4;
                Globals.aPieces[13] = Globals.aPiecesTemp[4]; // cColorFront5;
                Globals.aPieces[14] = Globals.aPiecesTemp[5]; // cColorFront6;

                Globals.aPieces[21] = Globals.aPiecesTemp[12]; // cColorRight4;
                Globals.aPieces[22] = Globals.aPiecesTemp[13]; // cColorRight5;
                Globals.aPieces[23] = Globals.aPiecesTemp[14]; // cColorRight6;

                Globals.aPieces[30] = Globals.aPiecesTemp[21]; // cColorBack4;
                Globals.aPieces[31] = Globals.aPiecesTemp[22]; // cColorBack5;
                Globals.aPieces[32] = Globals.aPiecesTemp[23]; // cColorBack6;
            }
        }

        // Turn the entire down face clockwise or counter clockwise
        public static void TurnDownFaceTo(string cDirection)
        {
            Array.Copy(Globals.aPieces, Globals.aPiecesTemp, 54);

            if (cDirection == "+")
            {
                Globals.aPieces[45] = Globals.aPiecesTemp[51]; // cColorDown7;
                Globals.aPieces[46] = Globals.aPiecesTemp[48]; // cColorDown4;
                Globals.aPieces[47] = Globals.aPiecesTemp[45]; // cColorDown1;
                Globals.aPieces[48] = Globals.aPiecesTemp[52]; // cColorDown8;
                Globals.aPieces[50] = Globals.aPiecesTemp[46]; // cColorDown2;
                Globals.aPieces[51] = Globals.aPiecesTemp[53]; // cColorDown9;
                Globals.aPieces[52] = Globals.aPiecesTemp[50]; // cColorDown6;
                Globals.aPieces[53] = Globals.aPiecesTemp[47]; // cColorDown3;

                Globals.aPieces[33] = Globals.aPiecesTemp[24]; // cColorBack7;
                Globals.aPieces[34] = Globals.aPiecesTemp[25]; // cColorBack8;
                Globals.aPieces[35] = Globals.aPiecesTemp[26]; // cColorBack9;

                Globals.aPieces[6] = Globals.aPiecesTemp[33]; // cColorLeft7;
                Globals.aPieces[7] = Globals.aPiecesTemp[34]; // cColorLeft8;
                Globals.aPieces[8] = Globals.aPiecesTemp[35]; // cColorLeft9;

                Globals.aPieces[15] = Globals.aPiecesTemp[6]; // cColorFront7;
                Globals.aPieces[16] = Globals.aPiecesTemp[7]; // cColorFront8;
                Globals.aPieces[17] = Globals.aPiecesTemp[8]; // cColorFront9;

                Globals.aPieces[24] = Globals.aPiecesTemp[15]; // cColorRight7;
                Globals.aPieces[25] = Globals.aPiecesTemp[16]; // cColorRight8;
                Globals.aPieces[26] = Globals.aPiecesTemp[17]; // cColorRight9;
            }

            if (cDirection == "-")
            {
                Globals.aPieces[45] = Globals.aPiecesTemp[47]; // cColorDown3;
                Globals.aPieces[46] = Globals.aPiecesTemp[50]; // cColorDown6;
                Globals.aPieces[47] = Globals.aPiecesTemp[53]; // cColorDown9;
                Globals.aPieces[48] = Globals.aPiecesTemp[46]; // cColorDown2;
                Globals.aPieces[50] = Globals.aPiecesTemp[52]; // cColorDown8;
                Globals.aPieces[51] = Globals.aPiecesTemp[45]; // cColorDown1;
                Globals.aPieces[52] = Globals.aPiecesTemp[48]; // cColorDown4;
                Globals.aPieces[53] = Globals.aPiecesTemp[51]; // cColorDown7;

                Globals.aPieces[33] = Globals.aPiecesTemp[6]; // cColorFront7;
                Globals.aPieces[34] = Globals.aPiecesTemp[7]; // cColorFront8;
                Globals.aPieces[35] = Globals.aPiecesTemp[8]; // cColorFront9;

                Globals.aPieces[6] = Globals.aPiecesTemp[15]; // cColorRight7;
                Globals.aPieces[7] = Globals.aPiecesTemp[16]; // cColorRight8;
                Globals.aPieces[8] = Globals.aPiecesTemp[17]; // cColorRight9;

                Globals.aPieces[15] = Globals.aPiecesTemp[24]; // cColorBack7;
                Globals.aPieces[16] = Globals.aPiecesTemp[25]; // cColorBack8;
                Globals.aPieces[17] = Globals.aPiecesTemp[26]; // cColorBack9;

                Globals.aPieces[24] = Globals.aPiecesTemp[33]; // cColorLeft7;
                Globals.aPieces[25] = Globals.aPiecesTemp[34]; // cColorLeft8;
                Globals.aPieces[26] = Globals.aPiecesTemp[35]; // cColorLeft9;
            }
        }
    }
}
