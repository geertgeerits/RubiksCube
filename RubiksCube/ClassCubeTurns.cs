namespace RubiksCube
{
    internal class ClassCubeTurns
    {
        // Turn the faces of the cube
        public static async Task TurnFaceCubeAsync(string cTurnFaceAndDirection)
        {
            switch (cTurnFaceAndDirection)
            {
                case Globals.turnFrontCW:
                    TurnFrontFaceTo("CW");
                    break;
                case Globals.turnFrontCCW:
                    TurnFrontFaceTo("CCW");
                    break;
                case Globals.turnFront2:
                    TurnFrontFaceTo("CW");
                    TurnFrontFaceTo("CW");
                    break;
                case Globals.turnRightCW:
                    TurnRightFaceTo("CW");
                    break;
                case Globals.turnRightCCW:
                    TurnRightFaceTo("CCW");
                    break;
                case Globals.turnRight2:
                    TurnRightFaceTo("CW");
                    TurnRightFaceTo("CW");
                    break;
                case Globals.turnBackCW:
                    TurnBackFaceTo("CW");
                    break;
                case Globals.turnBackCCW:
                    TurnBackFaceTo("CCW");
                    break;
                case Globals.turnBack2:
                    TurnBackFaceTo("CW");
                    TurnBackFaceTo("CW");
                    break;
                case Globals.turnLeftCW:
                    TurnLeftFaceTo("CW");
                    break;
                case Globals.turnLeftCCW:
                    TurnLeftFaceTo("CCW");
                    break;
                case Globals.turnLeft2:
                    TurnLeftFaceTo("CW");
                    TurnLeftFaceTo("CW");
                    break;
                case Globals.turnUpCW:
                    TurnUpFaceTo("CW");
                    break;
                case Globals.turnUpCCW:
                    TurnUpFaceTo("CCW");
                    break;
                case Globals.turnUp2:
                    TurnUpFaceTo("CW");
                    TurnUpFaceTo("CW");
                    break;
                case Globals.turnDownCW:
                    TurnDownFaceTo("CW");
                    break;
                case Globals.turnDownCCW:
                    TurnDownFaceTo("CCW");
                    break;
                case Globals.turnDown2:
                    TurnDownFaceTo("CW");
                    TurnDownFaceTo("CW");
                    break;

                case Globals.turnUpHorMiddleRight:
                    TurnUpHorMiddleTo("CW");
                    break;
                case Globals.turnUpHorMiddleLeft:
                    TurnUpHorMiddleTo("CCW");
                    break;
                case Globals.turnUpHorMiddle2:
                    TurnUpHorMiddleTo("CW");
                    TurnUpHorMiddleTo("CW");
                    break;

                case Globals.turnUpVerMiddleBack:
                    TurnUpVerMiddleTo("CW");
                    break;
                case Globals.turnUpVerMiddleFront:
                    TurnUpVerMiddleTo("CCW");
                    break;
                case Globals.turnUpVerMiddle2:
                    TurnUpVerMiddleTo("CW");
                    TurnUpVerMiddleTo("CW");
                    break;

                case Globals.turnFrontHorMiddleLeft:
                    TurnFrontHorMiddleTo("CW");
                    break;
                case Globals.turnFrontHorMiddleRight:
                    TurnFrontHorMiddleTo("CCW");
                    break;
                case Globals.turnFrontHorMiddle2:
                    TurnFrontHorMiddleTo("CW");
                    TurnFrontHorMiddleTo("CW");
                    break;

                case Globals.turnCubeFrontToRight:
                    TurnCubeFrontFaceToRightFace();
                    break;
                case Globals.turnCubeFrontToLeft:
                    TurnCubeFrontFaceToLeftFace();
                    break;
                case Globals.turnCubeFrontToLeft2:
                    TurnCubeFrontFaceToLeftFace();
                    TurnCubeFrontFaceToLeftFace();
                    break;
                case Globals.turnCubeFrontToUp:
                    TurnCubeFrontFaceToUpFace();
                    break;
                case Globals.turnCubeFrontToUp2:
                    TurnCubeFrontFaceToUpFace();
                    TurnCubeFrontFaceToUpFace();
                    break;
                case Globals.turnCubeFrontToDown:
                    TurnCubeFrontFaceToDownFace();
                    break;
                case Globals.turnCubeUpToRight:
                    TurnCubeUpFaceToRightFace();
                    break;
                case Globals.turnCubeUpToRight2:
                    TurnCubeUpFaceToRightFace();
                    TurnCubeUpFaceToRightFace();
                    break;
                case Globals.turnCubeUpToLeft:
                    TurnCubeUpFaceToLeftFace();
                    break;

                default:
                    await Application.Current.MainPage.DisplayAlert(CubeLang.ErrorTitle_Text, "Turn not found", CubeLang.ButtonClose_Text);
                    return;
            }
        }

        // Turn the entire cube a quarter turn
        // Rotate the entire cube so that the front goes to the left face
        private static void TurnCubeFrontFaceToLeftFace()
        {
            TurnUpFaceTo("CW");
            TurnFrontHorMiddleTo("CW");
            TurnDownFaceTo("CCW");
        }

        // Rotate the entire cube so that the front goes to the right face
        private static void TurnCubeFrontFaceToRightFace()
        {
            TurnUpFaceTo("CCW");
            TurnFrontHorMiddleTo("CCW");
            TurnDownFaceTo("CW");
        }

        // Rotate the entire cube so that the front goes to the upper face
        private static void TurnCubeFrontFaceToUpFace()
        {
            TurnRightFaceTo("CW");
            TurnUpVerMiddleTo("CW");
            TurnLeftFaceTo("CCW");
        }

        // Rotate the entire cube so that the front goes to the down face
        private static void TurnCubeFrontFaceToDownFace()
        {
            TurnRightFaceTo("CCW");
            TurnUpVerMiddleTo("CCW");
            TurnLeftFaceTo("CW");
        }

        // Rotate the entire cube so that the upper face goes to the right face
        private static void TurnCubeUpFaceToRightFace()
        {
            TurnFrontFaceTo("CW");
            TurnUpHorMiddleTo("CW");
            TurnBackFaceTo("CCW");
        }

        // Rotate the entire cube so that the upper face goes to the left face
        private static void TurnCubeUpFaceToLeftFace()
        {
            TurnFrontFaceTo("CCW");
            TurnUpHorMiddleTo("CCW");
            TurnBackFaceTo("CW");
        }

        // Turn the entire front face clockwise or counter clockwise
        public static void TurnFrontFaceTo(string cDirection)
        {
            Array.Copy(Globals.aPieces, Globals.aPiecesTemp, 54);

            if (cDirection == "CW")
            {
                Globals.aPieces[0] = Globals.aPiecesTemp[6];
                Globals.aPieces[1] = Globals.aPiecesTemp[3];
                Globals.aPieces[2] = Globals.aPiecesTemp[0];
                Globals.aPieces[3] = Globals.aPiecesTemp[7];
                Globals.aPieces[5] = Globals.aPiecesTemp[1];
                Globals.aPieces[6] = Globals.aPiecesTemp[8];
                Globals.aPieces[7] = Globals.aPiecesTemp[5];
                Globals.aPieces[8] = Globals.aPiecesTemp[2];

                Globals.aPieces[42] = Globals.aPiecesTemp[35];
                Globals.aPieces[43] = Globals.aPiecesTemp[32];
                Globals.aPieces[44] = Globals.aPiecesTemp[29];

                Globals.aPieces[9] = Globals.aPiecesTemp[42];
                Globals.aPieces[12] = Globals.aPiecesTemp[43];
                Globals.aPieces[15] = Globals.aPiecesTemp[44];

                Globals.aPieces[45] = Globals.aPiecesTemp[15];
                Globals.aPieces[46] = Globals.aPiecesTemp[12];
                Globals.aPieces[47] = Globals.aPiecesTemp[9];

                Globals.aPieces[29] = Globals.aPiecesTemp[45];
                Globals.aPieces[32] = Globals.aPiecesTemp[46];
                Globals.aPieces[35] = Globals.aPiecesTemp[47];
            }

            if (cDirection == "CCW")
            {
                Globals.aPieces[0] = Globals.aPiecesTemp[2];
                Globals.aPieces[1] = Globals.aPiecesTemp[5];
                Globals.aPieces[2] = Globals.aPiecesTemp[8];
                Globals.aPieces[3] = Globals.aPiecesTemp[1];
                Globals.aPieces[5] = Globals.aPiecesTemp[7];
                Globals.aPieces[6] = Globals.aPiecesTemp[0];
                Globals.aPieces[7] = Globals.aPiecesTemp[3];
                Globals.aPieces[8] = Globals.aPiecesTemp[6];

                Globals.aPieces[42] = Globals.aPiecesTemp[9];
                Globals.aPieces[43] = Globals.aPiecesTemp[12];
                Globals.aPieces[44] = Globals.aPiecesTemp[15];

                Globals.aPieces[9] = Globals.aPiecesTemp[47];
                Globals.aPieces[12] = Globals.aPiecesTemp[46];
                Globals.aPieces[15] = Globals.aPiecesTemp[45];

                Globals.aPieces[45] = Globals.aPiecesTemp[29];
                Globals.aPieces[46] = Globals.aPiecesTemp[32];
                Globals.aPieces[47] = Globals.aPiecesTemp[35];

                Globals.aPieces[29] = Globals.aPiecesTemp[44];
                Globals.aPieces[32] = Globals.aPiecesTemp[43];
                Globals.aPieces[35] = Globals.aPiecesTemp[42];
            }
        }

        // Turn the top horizontal middle layer to the right or left
        public static void TurnUpHorMiddleTo(string cDirection)
        {
            Array.Copy(Globals.aPieces, Globals.aPiecesTemp, 54);

            if (cDirection == "CW")
            {
                Globals.aPieces[39] = Globals.aPiecesTemp[34];
                Globals.aPieces[40] = Globals.aPiecesTemp[31];
                Globals.aPieces[41] = Globals.aPiecesTemp[28];

                Globals.aPieces[10] = Globals.aPiecesTemp[39];
                Globals.aPieces[13] = Globals.aPiecesTemp[40];
                Globals.aPieces[16] = Globals.aPiecesTemp[41];

                Globals.aPieces[48] = Globals.aPiecesTemp[16];
                Globals.aPieces[49] = Globals.aPiecesTemp[13];
                Globals.aPieces[50] = Globals.aPiecesTemp[10];

                Globals.aPieces[28] = Globals.aPiecesTemp[48];
                Globals.aPieces[31] = Globals.aPiecesTemp[49];
                Globals.aPieces[34] = Globals.aPiecesTemp[50];
            }

            if (cDirection == "CCW")
            {
                Globals.aPieces[39] = Globals.aPiecesTemp[10];
                Globals.aPieces[40] = Globals.aPiecesTemp[13];
                Globals.aPieces[41] = Globals.aPiecesTemp[16];

                Globals.aPieces[10] = Globals.aPiecesTemp[50];
                Globals.aPieces[13] = Globals.aPiecesTemp[49];
                Globals.aPieces[16] = Globals.aPiecesTemp[48];

                Globals.aPieces[48] = Globals.aPiecesTemp[28];
                Globals.aPieces[49] = Globals.aPiecesTemp[31];
                Globals.aPieces[50] = Globals.aPiecesTemp[34];

                Globals.aPieces[28] = Globals.aPiecesTemp[41];
                Globals.aPieces[31] = Globals.aPiecesTemp[40];
                Globals.aPieces[34] = Globals.aPiecesTemp[39];
            }
        }

        // Turn the entire back face clockwise or counter clockwise
        public static void TurnBackFaceTo(string cDirection)
        {
            Array.Copy(Globals.aPieces, Globals.aPiecesTemp, 54);

            if (cDirection == "CW")
            {
                Globals.aPieces[18] = Globals.aPiecesTemp[24];
                Globals.aPieces[19] = Globals.aPiecesTemp[21];
                Globals.aPieces[20] = Globals.aPiecesTemp[18];
                Globals.aPieces[21] = Globals.aPiecesTemp[25];
                Globals.aPieces[23] = Globals.aPiecesTemp[19];
                Globals.aPieces[24] = Globals.aPiecesTemp[26];
                Globals.aPieces[25] = Globals.aPiecesTemp[23];
                Globals.aPieces[26] = Globals.aPiecesTemp[20];

                Globals.aPieces[36] = Globals.aPiecesTemp[11];
                Globals.aPieces[37] = Globals.aPiecesTemp[14];
                Globals.aPieces[38] = Globals.aPiecesTemp[17];

                Globals.aPieces[11] = Globals.aPiecesTemp[53];
                Globals.aPieces[14] = Globals.aPiecesTemp[52];
                Globals.aPieces[17] = Globals.aPiecesTemp[51];

                Globals.aPieces[51] = Globals.aPiecesTemp[27];
                Globals.aPieces[52] = Globals.aPiecesTemp[30];
                Globals.aPieces[53] = Globals.aPiecesTemp[33];

                Globals.aPieces[27] = Globals.aPiecesTemp[38];
                Globals.aPieces[30] = Globals.aPiecesTemp[37];
                Globals.aPieces[33] = Globals.aPiecesTemp[36];
            }

            if (cDirection == "CCW")
            {
                Globals.aPieces[18] = Globals.aPiecesTemp[20];
                Globals.aPieces[19] = Globals.aPiecesTemp[23];
                Globals.aPieces[20] = Globals.aPiecesTemp[26];
                Globals.aPieces[21] = Globals.aPiecesTemp[19];
                Globals.aPieces[23] = Globals.aPiecesTemp[25];
                Globals.aPieces[24] = Globals.aPiecesTemp[18];
                Globals.aPieces[25] = Globals.aPiecesTemp[21];
                Globals.aPieces[26] = Globals.aPiecesTemp[24];

                Globals.aPieces[36] = Globals.aPiecesTemp[33];
                Globals.aPieces[37] = Globals.aPiecesTemp[30];
                Globals.aPieces[38] = Globals.aPiecesTemp[27];

                Globals.aPieces[11] = Globals.aPiecesTemp[36];
                Globals.aPieces[14] = Globals.aPiecesTemp[37];
                Globals.aPieces[17] = Globals.aPiecesTemp[38];

                Globals.aPieces[51] = Globals.aPiecesTemp[17];
                Globals.aPieces[52] = Globals.aPiecesTemp[14];
                Globals.aPieces[53] = Globals.aPiecesTemp[11];

                Globals.aPieces[27] = Globals.aPiecesTemp[51];
                Globals.aPieces[30] = Globals.aPiecesTemp[52];
                Globals.aPieces[33] = Globals.aPiecesTemp[53];
            }
        }

        // Turn the entire left face clockwise or counter clockwise
        public static void TurnLeftFaceTo(string cDirection)
        {
            Array.Copy(Globals.aPieces, Globals.aPiecesTemp, 54);

            if (cDirection == "CW")
            {
                Globals.aPieces[27] = Globals.aPiecesTemp[33];
                Globals.aPieces[28] = Globals.aPiecesTemp[30];
                Globals.aPieces[29] = Globals.aPiecesTemp[27];
                Globals.aPieces[30] = Globals.aPiecesTemp[34];
                Globals.aPieces[32] = Globals.aPiecesTemp[28];
                Globals.aPieces[33] = Globals.aPiecesTemp[35];
                Globals.aPieces[34] = Globals.aPiecesTemp[32];
                Globals.aPieces[35] = Globals.aPiecesTemp[29];

                Globals.aPieces[36] = Globals.aPiecesTemp[26];
                Globals.aPieces[39] = Globals.aPiecesTemp[23];
                Globals.aPieces[42] = Globals.aPiecesTemp[20];

                Globals.aPieces[0] = Globals.aPiecesTemp[36];
                Globals.aPieces[3] = Globals.aPiecesTemp[39];
                Globals.aPieces[6] = Globals.aPiecesTemp[42];

                Globals.aPieces[45] = Globals.aPiecesTemp[0];
                Globals.aPieces[48] = Globals.aPiecesTemp[3];
                Globals.aPieces[51] = Globals.aPiecesTemp[6];

                Globals.aPieces[20] = Globals.aPiecesTemp[51];
                Globals.aPieces[23] = Globals.aPiecesTemp[48];
                Globals.aPieces[26] = Globals.aPiecesTemp[45];
            }

            if (cDirection == "CCW")
            {
                Globals.aPieces[27] = Globals.aPiecesTemp[29];
                Globals.aPieces[28] = Globals.aPiecesTemp[32];
                Globals.aPieces[29] = Globals.aPiecesTemp[35];
                Globals.aPieces[30] = Globals.aPiecesTemp[28];
                Globals.aPieces[32] = Globals.aPiecesTemp[34];
                Globals.aPieces[33] = Globals.aPiecesTemp[27];
                Globals.aPieces[34] = Globals.aPiecesTemp[30];
                Globals.aPieces[35] = Globals.aPiecesTemp[33];

                Globals.aPieces[36] = Globals.aPiecesTemp[0];
                Globals.aPieces[39] = Globals.aPiecesTemp[3];
                Globals.aPieces[42] = Globals.aPiecesTemp[6];

                Globals.aPieces[0] = Globals.aPiecesTemp[45];
                Globals.aPieces[3] = Globals.aPiecesTemp[48];
                Globals.aPieces[6] = Globals.aPiecesTemp[51];

                Globals.aPieces[45] = Globals.aPiecesTemp[26];
                Globals.aPieces[48] = Globals.aPiecesTemp[23];
                Globals.aPieces[51] = Globals.aPiecesTemp[20];

                Globals.aPieces[20] = Globals.aPiecesTemp[42];
                Globals.aPieces[23] = Globals.aPiecesTemp[39];
                Globals.aPieces[26] = Globals.aPiecesTemp[36];
            }
        }

        // Turn the top vertical middle layer to back or front
        public static void TurnUpVerMiddleTo(string cDirection)
        {
            Array.Copy(Globals.aPieces, Globals.aPiecesTemp, 54);

            if (cDirection == "CW")
            {
                Globals.aPieces[37] = Globals.aPiecesTemp[1];
                Globals.aPieces[40] = Globals.aPiecesTemp[4];
                Globals.aPieces[43] = Globals.aPiecesTemp[7];

                Globals.aPieces[1] = Globals.aPiecesTemp[46];
                Globals.aPieces[4] = Globals.aPiecesTemp[49];
                Globals.aPieces[7] = Globals.aPiecesTemp[52];

                Globals.aPieces[46] = Globals.aPiecesTemp[25];
                Globals.aPieces[49] = Globals.aPiecesTemp[22];
                Globals.aPieces[52] = Globals.aPiecesTemp[19];

                Globals.aPieces[19] = Globals.aPiecesTemp[43];
                Globals.aPieces[22] = Globals.aPiecesTemp[40];
                Globals.aPieces[25] = Globals.aPiecesTemp[37];
            }

            if (cDirection == "CCW")
            {
                Globals.aPieces[37] = Globals.aPiecesTemp[25];
                Globals.aPieces[40] = Globals.aPiecesTemp[22];
                Globals.aPieces[43] = Globals.aPiecesTemp[19];

                Globals.aPieces[1] = Globals.aPiecesTemp[37];
                Globals.aPieces[4] = Globals.aPiecesTemp[40];
                Globals.aPieces[7] = Globals.aPiecesTemp[43];

                Globals.aPieces[46] = Globals.aPiecesTemp[1];
                Globals.aPieces[49] = Globals.aPiecesTemp[4];
                Globals.aPieces[52] = Globals.aPiecesTemp[7];

                Globals.aPieces[19] = Globals.aPiecesTemp[52];
                Globals.aPieces[22] = Globals.aPiecesTemp[49];
                Globals.aPieces[25] = Globals.aPiecesTemp[46];
            }
        }

        // Turn the entire right face clockwise or counter clockwise
        public static void TurnRightFaceTo(string cDirection)
        {
            Array.Copy(Globals.aPieces, Globals.aPiecesTemp, 54);

            if (cDirection == "CW")
            {
                Globals.aPieces[9] = Globals.aPiecesTemp[15];
                Globals.aPieces[10] = Globals.aPiecesTemp[12];
                Globals.aPieces[11] = Globals.aPiecesTemp[9];
                Globals.aPieces[12] = Globals.aPiecesTemp[16];
                Globals.aPieces[14] = Globals.aPiecesTemp[10];
                Globals.aPieces[15] = Globals.aPiecesTemp[17];
                Globals.aPieces[16] = Globals.aPiecesTemp[14];
                Globals.aPieces[17] = Globals.aPiecesTemp[11];

                Globals.aPieces[38] = Globals.aPiecesTemp[2];
                Globals.aPieces[41] = Globals.aPiecesTemp[5];
                Globals.aPieces[44] = Globals.aPiecesTemp[8];

                Globals.aPieces[2] = Globals.aPiecesTemp[47];
                Globals.aPieces[5] = Globals.aPiecesTemp[50];
                Globals.aPieces[8] = Globals.aPiecesTemp[53];

                Globals.aPieces[47] = Globals.aPiecesTemp[24];
                Globals.aPieces[50] = Globals.aPiecesTemp[21];
                Globals.aPieces[53] = Globals.aPiecesTemp[18];

                Globals.aPieces[18] = Globals.aPiecesTemp[44];
                Globals.aPieces[21] = Globals.aPiecesTemp[41];
                Globals.aPieces[24] = Globals.aPiecesTemp[38];
            }

            if (cDirection == "CCW")
            {
                Globals.aPieces[9] = Globals.aPiecesTemp[11];
                Globals.aPieces[10] = Globals.aPiecesTemp[14];
                Globals.aPieces[11] = Globals.aPiecesTemp[17];
                Globals.aPieces[12] = Globals.aPiecesTemp[10];
                Globals.aPieces[14] = Globals.aPiecesTemp[16];
                Globals.aPieces[15] = Globals.aPiecesTemp[9];
                Globals.aPieces[16] = Globals.aPiecesTemp[12];
                Globals.aPieces[17] = Globals.aPiecesTemp[15];

                Globals.aPieces[38] = Globals.aPiecesTemp[24];
                Globals.aPieces[41] = Globals.aPiecesTemp[21];
                Globals.aPieces[44] = Globals.aPiecesTemp[18];

                Globals.aPieces[2] = Globals.aPiecesTemp[38];
                Globals.aPieces[5] = Globals.aPiecesTemp[41];
                Globals.aPieces[8] = Globals.aPiecesTemp[44];

                Globals.aPieces[47] = Globals.aPiecesTemp[2];
                Globals.aPieces[50] = Globals.aPiecesTemp[5];
                Globals.aPieces[53] = Globals.aPiecesTemp[8];

                Globals.aPieces[18] = Globals.aPiecesTemp[53];
                Globals.aPieces[21] = Globals.aPiecesTemp[50];
                Globals.aPieces[24] = Globals.aPiecesTemp[47];
            }
        }

        // Turn the entire upper face clockwise or counter clockwise
        public static void TurnUpFaceTo(string cDirection)
        {
            Array.Copy(Globals.aPieces, Globals.aPiecesTemp, 54);

            if (cDirection == "CW")
            {
                Globals.aPieces[36] = Globals.aPiecesTemp[42];
                Globals.aPieces[37] = Globals.aPiecesTemp[39];
                Globals.aPieces[38] = Globals.aPiecesTemp[36];
                Globals.aPieces[39] = Globals.aPiecesTemp[43];
                Globals.aPieces[41] = Globals.aPiecesTemp[37];
                Globals.aPieces[42] = Globals.aPiecesTemp[44];
                Globals.aPieces[43] = Globals.aPiecesTemp[41];
                Globals.aPieces[44] = Globals.aPiecesTemp[38];

                Globals.aPieces[27] = Globals.aPiecesTemp[0];
                Globals.aPieces[28] = Globals.aPiecesTemp[1];
                Globals.aPieces[29] = Globals.aPiecesTemp[2];

                Globals.aPieces[0] = Globals.aPiecesTemp[9];
                Globals.aPieces[1] = Globals.aPiecesTemp[10];
                Globals.aPieces[2] = Globals.aPiecesTemp[11];

                Globals.aPieces[9] = Globals.aPiecesTemp[18];
                Globals.aPieces[10] = Globals.aPiecesTemp[19];
                Globals.aPieces[11] = Globals.aPiecesTemp[20];

                Globals.aPieces[18] = Globals.aPiecesTemp[27];
                Globals.aPieces[19] = Globals.aPiecesTemp[28];
                Globals.aPieces[20] = Globals.aPiecesTemp[29];
            }

            if (cDirection == "CCW")
            {
                Globals.aPieces[36] = Globals.aPiecesTemp[38];
                Globals.aPieces[37] = Globals.aPiecesTemp[41];
                Globals.aPieces[38] = Globals.aPiecesTemp[44];
                Globals.aPieces[39] = Globals.aPiecesTemp[37];
                Globals.aPieces[41] = Globals.aPiecesTemp[43];
                Globals.aPieces[42] = Globals.aPiecesTemp[36];
                Globals.aPieces[43] = Globals.aPiecesTemp[39];
                Globals.aPieces[44] = Globals.aPiecesTemp[42];

                Globals.aPieces[27] = Globals.aPiecesTemp[18];
                Globals.aPieces[28] = Globals.aPiecesTemp[19];
                Globals.aPieces[29] = Globals.aPiecesTemp[20];

                Globals.aPieces[0] = Globals.aPiecesTemp[27];
                Globals.aPieces[1] = Globals.aPiecesTemp[28];
                Globals.aPieces[2] = Globals.aPiecesTemp[29];

                Globals.aPieces[9] = Globals.aPiecesTemp[0];
                Globals.aPieces[10] = Globals.aPiecesTemp[1];
                Globals.aPieces[11] = Globals.aPiecesTemp[2];

                Globals.aPieces[18] = Globals.aPiecesTemp[9];
                Globals.aPieces[19] = Globals.aPiecesTemp[10];
                Globals.aPieces[20] = Globals.aPiecesTemp[11];
            }
        }

        // Turn the front horizontal middle layer to right or left
        public static void TurnFrontHorMiddleTo(string cDirection)
        {
            Array.Copy(Globals.aPieces, Globals.aPiecesTemp, 54);

            if (cDirection == "CW")
            {
                Globals.aPieces[3] = Globals.aPiecesTemp[12];
                Globals.aPieces[4] = Globals.aPiecesTemp[13];
                Globals.aPieces[5] = Globals.aPiecesTemp[14];

                Globals.aPieces[12] = Globals.aPiecesTemp[21];
                Globals.aPieces[13] = Globals.aPiecesTemp[22];
                Globals.aPieces[14] = Globals.aPiecesTemp[23];

                Globals.aPieces[21] = Globals.aPiecesTemp[30];
                Globals.aPieces[22] = Globals.aPiecesTemp[31];
                Globals.aPieces[23] = Globals.aPiecesTemp[32];

                Globals.aPieces[30] = Globals.aPiecesTemp[3];
                Globals.aPieces[31] = Globals.aPiecesTemp[4];
                Globals.aPieces[32] = Globals.aPiecesTemp[5];
            }

            if (cDirection == "CCW")
            {
                Globals.aPieces[3] = Globals.aPiecesTemp[30];
                Globals.aPieces[4] = Globals.aPiecesTemp[31];
                Globals.aPieces[5] = Globals.aPiecesTemp[32];

                Globals.aPieces[12] = Globals.aPiecesTemp[3];
                Globals.aPieces[13] = Globals.aPiecesTemp[4];
                Globals.aPieces[14] = Globals.aPiecesTemp[5];

                Globals.aPieces[21] = Globals.aPiecesTemp[12];
                Globals.aPieces[22] = Globals.aPiecesTemp[13];
                Globals.aPieces[23] = Globals.aPiecesTemp[14];

                Globals.aPieces[30] = Globals.aPiecesTemp[21];
                Globals.aPieces[31] = Globals.aPiecesTemp[22];
                Globals.aPieces[32] = Globals.aPiecesTemp[23];
            }
        }

        // Turn the entire down face clockwise or counter clockwise
        public static void TurnDownFaceTo(string cDirection)
        {
            Array.Copy(Globals.aPieces, Globals.aPiecesTemp, 54);

            if (cDirection == "CW")
            {
                Globals.aPieces[45] = Globals.aPiecesTemp[51];
                Globals.aPieces[46] = Globals.aPiecesTemp[48];
                Globals.aPieces[47] = Globals.aPiecesTemp[45];
                Globals.aPieces[48] = Globals.aPiecesTemp[52];
                Globals.aPieces[50] = Globals.aPiecesTemp[46];
                Globals.aPieces[51] = Globals.aPiecesTemp[53];
                Globals.aPieces[52] = Globals.aPiecesTemp[50];
                Globals.aPieces[53] = Globals.aPiecesTemp[47];

                Globals.aPieces[33] = Globals.aPiecesTemp[24];
                Globals.aPieces[34] = Globals.aPiecesTemp[25];
                Globals.aPieces[35] = Globals.aPiecesTemp[26];

                Globals.aPieces[6] = Globals.aPiecesTemp[33];
                Globals.aPieces[7] = Globals.aPiecesTemp[34];
                Globals.aPieces[8] = Globals.aPiecesTemp[35];

                Globals.aPieces[15] = Globals.aPiecesTemp[6];
                Globals.aPieces[16] = Globals.aPiecesTemp[7];
                Globals.aPieces[17] = Globals.aPiecesTemp[8];

                Globals.aPieces[24] = Globals.aPiecesTemp[15];
                Globals.aPieces[25] = Globals.aPiecesTemp[16];
                Globals.aPieces[26] = Globals.aPiecesTemp[17];
            }

            if (cDirection == "CCW")
            {
                Globals.aPieces[45] = Globals.aPiecesTemp[47];
                Globals.aPieces[46] = Globals.aPiecesTemp[50];
                Globals.aPieces[47] = Globals.aPiecesTemp[53];
                Globals.aPieces[48] = Globals.aPiecesTemp[46];
                Globals.aPieces[50] = Globals.aPiecesTemp[52];
                Globals.aPieces[51] = Globals.aPiecesTemp[45];
                Globals.aPieces[52] = Globals.aPiecesTemp[48];
                Globals.aPieces[53] = Globals.aPiecesTemp[51];

                Globals.aPieces[33] = Globals.aPiecesTemp[6];
                Globals.aPieces[34] = Globals.aPiecesTemp[7];
                Globals.aPieces[35] = Globals.aPiecesTemp[8];

                Globals.aPieces[6] = Globals.aPiecesTemp[15];
                Globals.aPieces[7] = Globals.aPiecesTemp[16];
                Globals.aPieces[8] = Globals.aPiecesTemp[17];

                Globals.aPieces[15] = Globals.aPiecesTemp[24];
                Globals.aPieces[16] = Globals.aPiecesTemp[25];
                Globals.aPieces[17] = Globals.aPiecesTemp[26];

                Globals.aPieces[24] = Globals.aPiecesTemp[33];
                Globals.aPieces[25] = Globals.aPiecesTemp[34];
                Globals.aPieces[26] = Globals.aPiecesTemp[35];
            }
        }
    }
}
