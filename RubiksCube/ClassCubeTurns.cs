using static RubiksCube.Globals;

namespace RubiksCube
{
    internal sealed class ClassCubeTurns
    {
        /// <summary>
        /// Turn the layers of the cube (CW = Clockwise, CCW = Counter clockwise, 2 = two quarter turns or 1 half turn)
        /// </summary>
        /// <param name="cTurn"></param>
        /// <returns></returns>
        public static async Task<bool> TurnCubeLayersAsync(string cTurn)
        {
            switch (cTurn)
            {
                // Face rotations
                case turnFrontCW:
                    TurnFrontFaceTo("CW");
                    break;
                case turnFrontCCW:
                    TurnFrontFaceTo("CCW");
                    break;
                case turnFront2:
                    TurnFrontFaceTo("CW");
                    TurnFrontFaceTo("CW");
                    break;
                case turnRightCW:
                    TurnRightFaceTo("CW");
                    break;
                case turnRightCCW:
                    TurnRightFaceTo("CCW");
                    break;
                case turnRight2:
                    TurnRightFaceTo("CW");
                    TurnRightFaceTo("CW");
                    break;
                case turnBackCW:
                    TurnBackFaceTo("CW");
                    break;
                case turnBackCCW:
                    TurnBackFaceTo("CCW");
                    break;
                case turnBack2:
                    TurnBackFaceTo("CW");
                    TurnBackFaceTo("CW");
                    break;
                case turnLeftCW:
                    TurnLeftFaceTo("CW");
                    break;
                case turnLeftCCW:
                    TurnLeftFaceTo("CCW");
                    break;
                case turnLeft2:
                    TurnLeftFaceTo("CW");
                    TurnLeftFaceTo("CW");
                    break;
                case turnUpCW:
                    TurnUpFaceTo("CW");
                    break;
                case turnUpCCW:
                    TurnUpFaceTo("CCW");
                    break;
                case turnUp2:
                    TurnUpFaceTo("CW");
                    TurnUpFaceTo("CW");
                    break;
                case turnDownCW:
                    TurnDownFaceTo("CW");
                    break;
                case turnDownCCW:
                    TurnDownFaceTo("CCW");
                    break;
                case turnDown2:
                    TurnDownFaceTo("CW");
                    TurnDownFaceTo("CW");
                    break;

                // Middle layer rotations
                case turnUpHorMiddleRight:
                    TurnUpHorMiddleTo("CW");
                    break;
                case turnUpHorMiddleLeft:
                    TurnUpHorMiddleTo("CCW");
                    break;
                case turnUpHorMiddle2:
                    TurnUpHorMiddleTo("CW");
                    TurnUpHorMiddleTo("CW");
                    break;

                case turnUpVerMiddleBack:
                    TurnUpVerMiddleTo("CW");
                    break;
                case turnUpVerMiddleFront:
                    TurnUpVerMiddleTo("CCW");
                    break;
                case turnUpVerMiddle2:
                    TurnUpVerMiddleTo("CW");
                    TurnUpVerMiddleTo("CW");
                    break;

                case turnFrontHorMiddleLeft:
                    TurnFrontHorMiddleTo("CW");
                    break;
                case turnFrontHorMiddleRight:
                    TurnFrontHorMiddleTo("CCW");
                    break;
                case turnFrontHorMiddle2:
                    TurnFrontHorMiddleTo("CW");
                    TurnFrontHorMiddleTo("CW");
                    break;

                // Two layers at the same time
                case turn2LayersFrontCW:
                    TurnFrontFaceTo("CW");
                    TurnUpHorMiddleTo("CW");
                    break;
                case turn2LayersFrontCCW:
                    TurnFrontFaceTo("CCW");
                    TurnUpHorMiddleTo("CCW");
                    break;
                case turn2LayersFront2:
                    TurnFrontFaceTo("CW");
                    TurnUpHorMiddleTo("CW");
                    TurnFrontFaceTo("CW");
                    TurnUpHorMiddleTo("CW");
                    break;

                case turn2LayersRightCW:
                    TurnRightFaceTo("CW");
                    TurnUpVerMiddleTo("CW");
                    break;
                case turn2LayersRightCCW:
                    TurnRightFaceTo("CCW");
                    TurnUpVerMiddleTo("CCW");
                    break;
                case turn2LayersRight2:
                    TurnRightFaceTo("CW");
                    TurnUpVerMiddleTo("CW");
                    TurnRightFaceTo("CW");
                    TurnUpVerMiddleTo("CW");
                    break;

                case turn2LayersBackCW:
                    TurnBackFaceTo("CW");
                    TurnUpHorMiddleTo("CCW");
                    break;
                case turn2LayersBackCCW:
                    TurnBackFaceTo("CCW");
                    TurnUpHorMiddleTo("CW");
                    break;
                case turn2LayersBack2:
                    TurnBackFaceTo("CW");
                    TurnUpHorMiddleTo("CCW");
                    TurnBackFaceTo("CW");
                    TurnUpHorMiddleTo("CCW");
                    break;

                case turn2LayersLeftCW:
                    TurnLeftFaceTo("CW");
                    TurnUpVerMiddleTo("CCW");
                    break;
                case turn2LayersLeftCCW:
                    TurnLeftFaceTo("CCW");
                    TurnUpVerMiddleTo("CW");
                    break;
                case turn2LayersLeft2:
                    TurnLeftFaceTo("CW");
                    TurnUpVerMiddleTo("CCW");
                    TurnLeftFaceTo("CW");
                    TurnUpVerMiddleTo("CCW");
                    break;

                case turn2LayersUpCW:
                    TurnUpFaceTo("CW");
                    TurnFrontHorMiddleTo("CW");
                    break;
                case turn2LayersUpCCW:
                    TurnUpFaceTo("CCW");
                    TurnFrontHorMiddleTo("CCW");
                    break;
                case turn2LayersUp2:
                    TurnUpFaceTo("CW");
                    TurnFrontHorMiddleTo("CW");
                    TurnUpFaceTo("CW");
                    TurnFrontHorMiddleTo("CW");
                    break;

                case turn2LayersDownCW:
                    TurnDownFaceTo("CW");
                    TurnFrontHorMiddleTo("CCW");
                    break;
                case turn2LayersDownCCW:
                    TurnDownFaceTo("CCW");
                    TurnFrontHorMiddleTo("CW");
                    break;
                case turn2LayersDown2:
                    TurnDownFaceTo("CW");
                    TurnFrontHorMiddleTo("CCW");
                    TurnDownFaceTo("CW");
                    TurnFrontHorMiddleTo("CCW");
                    break;

                // Cube rotations
                case turnCubeFrontToRight:
                    TurnCubeFrontFaceToRightFace();
                    break;
                case turnCubeFrontToLeft:
                    TurnCubeFrontFaceToLeftFace();
                    break;
                case turnCubeFrontToLeft2:
                    TurnCubeFrontFaceToLeftFace();
                    TurnCubeFrontFaceToLeftFace();
                    break;
                case turnCubeFrontToUp:
                    TurnCubeFrontFaceToUpFace();
                    break;
                case turnCubeFrontToUp2:
                    TurnCubeFrontFaceToUpFace();
                    TurnCubeFrontFaceToUpFace();
                    break;
                case turnCubeFrontToDown:
                    TurnCubeFrontFaceToDownFace();
                    break;
                case turnCubeUpToRight:
                    TurnCubeUpFaceToRightFace();
                    break;
                case turnCubeUpToRight2:
                    TurnCubeUpFaceToRightFace();
                    TurnCubeUpFaceToRightFace();
                    break;
                case turnCubeUpToLeft:
                    TurnCubeUpFaceToLeftFace();
                    break;

                default:
                    await Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, $"TurnCubeLayersAsync\ncTurn not found:\n{cTurn}", CubeLang.ButtonClose_Text);
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Turn the layers of the cube in reverse order
        /// </summary>
        /// <param name="cTurn"></param>
        /// <returns></returns>
        public static async Task<bool> TurnCubeLayersReversedAsync(string cTurn)
        {
            switch (cTurn)
            {
                // Face rotations
                case turnFrontCW:
                    TurnFrontFaceTo("CCW");
                    break;
                case turnFrontCCW:
                    TurnFrontFaceTo("CW");
                    break;
                case turnFront2:
                    TurnFrontFaceTo("CW");
                    TurnFrontFaceTo("CW");
                    break;
                case turnRightCW:
                    TurnRightFaceTo("CCW");
                    break;
                case turnRightCCW:
                    TurnRightFaceTo("CW");
                    break;
                case turnRight2:
                    TurnRightFaceTo("CW");
                    TurnRightFaceTo("CW");
                    break;
                case turnBackCW:
                    TurnBackFaceTo("CCW");
                    break;
                case turnBackCCW:
                    TurnBackFaceTo("CW");
                    break;
                case turnBack2:
                    TurnBackFaceTo("CW");
                    TurnBackFaceTo("CW");
                    break;
                case turnLeftCW:
                    TurnLeftFaceTo("CCW");
                    break;
                case turnLeftCCW:
                    TurnLeftFaceTo("CW");
                    break;
                case turnLeft2:
                    TurnLeftFaceTo("CW");
                    TurnLeftFaceTo("CW");
                    break;
                case turnUpCW:
                    TurnUpFaceTo("CCW");
                    break;
                case turnUpCCW:
                    TurnUpFaceTo("CW");
                    break;
                case turnUp2:
                    TurnUpFaceTo("CW");
                    TurnUpFaceTo("CW");
                    break;
                case turnDownCW:
                    TurnDownFaceTo("CCW");
                    break;
                case turnDownCCW:
                    TurnDownFaceTo("CW");
                    break;
                case turnDown2:
                    TurnDownFaceTo("CW");
                    TurnDownFaceTo("CW");
                    break;

                // Middle layer rotations
                case turnUpHorMiddleRight:
                    TurnUpHorMiddleTo("CCW");
                    break;
                case turnUpHorMiddleLeft:
                    TurnUpHorMiddleTo("CW");
                    break;
                case turnUpHorMiddle2:
                    TurnUpHorMiddleTo("CW");
                    TurnUpHorMiddleTo("CW");
                    break;

                case turnUpVerMiddleBack:
                    TurnUpVerMiddleTo("CCW");
                    break;
                case turnUpVerMiddleFront:
                    TurnUpVerMiddleTo("CW");
                    break;
                case turnUpVerMiddle2:
                    TurnUpVerMiddleTo("CW");
                    TurnUpVerMiddleTo("CW");
                    break;

                case turnFrontHorMiddleLeft:
                    TurnFrontHorMiddleTo("CCW");
                    break;
                case turnFrontHorMiddleRight:
                    TurnFrontHorMiddleTo("CW");
                    break;
                case turnFrontHorMiddle2:
                    TurnFrontHorMiddleTo("CW");
                    TurnFrontHorMiddleTo("CW");
                    break;

                // Two layers at the same time
                case turn2LayersFrontCW:
                    TurnFrontFaceTo("CCW");
                    TurnUpHorMiddleTo("CCW");
                    break;
                case turn2LayersFrontCCW:
                    TurnFrontFaceTo("CW");
                    TurnUpHorMiddleTo("CW");
                    break;
                case turn2LayersFront2:
                    TurnFrontFaceTo("CW");
                    TurnUpHorMiddleTo("CW");
                    TurnFrontFaceTo("CW");
                    TurnUpHorMiddleTo("CW");
                    break;

                case turn2LayersRightCW:
                    TurnRightFaceTo("CCW");
                    TurnUpVerMiddleTo("CCW");
                    break;
                case turn2LayersRightCCW:
                    TurnRightFaceTo("CW");
                    TurnUpVerMiddleTo("CW");
                    break;
                case turn2LayersRight2:
                    TurnRightFaceTo("CW");
                    TurnUpVerMiddleTo("CW");
                    TurnRightFaceTo("CW");
                    TurnUpVerMiddleTo("CW");
                    break;

                case turn2LayersBackCW:
                    TurnBackFaceTo("CCW");
                    TurnUpHorMiddleTo("CW");
                    break;
                case turn2LayersBackCCW:
                    TurnBackFaceTo("CW");
                    TurnUpHorMiddleTo("CCW");
                    break;
                case turn2LayersBack2:
                    TurnBackFaceTo("CW");
                    TurnUpHorMiddleTo("CCW");
                    TurnBackFaceTo("CW");
                    TurnUpHorMiddleTo("CCW");
                    break;

                case turn2LayersLeftCW:
                    TurnLeftFaceTo("CCW");
                    TurnUpVerMiddleTo("CW");
                    break;
                case turn2LayersLeftCCW:
                    TurnLeftFaceTo("CW");
                    TurnUpVerMiddleTo("CCW");
                    break;
                case turn2LayersLeft2:
                    TurnLeftFaceTo("CW");
                    TurnUpVerMiddleTo("CCW");
                    TurnLeftFaceTo("CW");
                    TurnUpVerMiddleTo("CCW");
                    break;

                case turn2LayersUpCW:
                    TurnUpFaceTo("CCW");
                    TurnFrontHorMiddleTo("CCW");
                    break;
                case turn2LayersUpCCW:
                    TurnUpFaceTo("CW");
                    TurnFrontHorMiddleTo("CW");
                    break;
                case turn2LayersUp2:
                    TurnUpFaceTo("CW");
                    TurnFrontHorMiddleTo("CW");
                    TurnUpFaceTo("CW");
                    TurnFrontHorMiddleTo("CW");
                    break;

                case turn2LayersDownCW:
                    TurnDownFaceTo("CCW");
                    TurnFrontHorMiddleTo("CW");
                    break;
                case turn2LayersDownCCW:
                    TurnDownFaceTo("CW");
                    TurnFrontHorMiddleTo("CCW");
                    break;
                case turn2LayersDown2:
                    TurnDownFaceTo("CW");
                    TurnFrontHorMiddleTo("CCW");
                    TurnDownFaceTo("CW");
                    TurnFrontHorMiddleTo("CCW");
                    break;

                // Cube rotations
                case turnCubeFrontToRight:
                    TurnCubeFrontFaceToLeftFace();
                    break;
                case turnCubeFrontToLeft:
                    TurnCubeFrontFaceToRightFace();
                    break;
                case turnCubeFrontToLeft2:
                    TurnCubeFrontFaceToLeftFace();
                    TurnCubeFrontFaceToLeftFace();
                    break;
                case turnCubeFrontToUp:
                    TurnCubeFrontFaceToDownFace();
                    break;
                case turnCubeFrontToUp2:
                    TurnCubeFrontFaceToUpFace();
                    TurnCubeFrontFaceToUpFace();
                    break;
                case turnCubeFrontToDown:
                    TurnCubeFrontFaceToUpFace();
                    break;
                case turnCubeUpToRight:
                    TurnCubeUpFaceToLeftFace();
                    break;
                case turnCubeUpToRight2:
                    TurnCubeUpFaceToRightFace();
                    TurnCubeUpFaceToRightFace();
                    break;
                case turnCubeUpToLeft:
                    TurnCubeUpFaceToRightFace();
                    break;

                default:
                    await Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, $"TurnCubeLayersAsync\ncTurn not found:\n{cTurn}", CubeLang.ButtonClose_Text);
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Rotate the entire cube a quarter turn so that the front goes to the left face
        /// </summary>
        private static void TurnCubeFrontFaceToLeftFace()
        {
            TurnUpFaceTo("CW");
            TurnFrontHorMiddleTo("CW");
            TurnDownFaceTo("CCW");
        }

        /// <summary>
        /// Rotate the entire cube a quarter turn so that the front goes to the right face 
        /// </summary>
        private static void TurnCubeFrontFaceToRightFace()
        {
            TurnUpFaceTo("CCW");
            TurnFrontHorMiddleTo("CCW");
            TurnDownFaceTo("CW");
        }

        /// <summary>
        /// Rotate the entire cube a quarter turn so that the front goes to the upper face 
        /// </summary>
        private static void TurnCubeFrontFaceToUpFace()
        {
            TurnRightFaceTo("CW");
            TurnUpVerMiddleTo("CW");
            TurnLeftFaceTo("CCW");
        }

        /// <summary>
        /// Rotate the entire cube a quarter turn so that the front goes to the down face 
        /// </summary>
        private static void TurnCubeFrontFaceToDownFace()
        {
            TurnRightFaceTo("CCW");
            TurnUpVerMiddleTo("CCW");
            TurnLeftFaceTo("CW");
        }

        /// <summary>
        /// Rotate the entire cube a quarter turn so that the upper face goes to the right face 
        /// </summary>
        private static void TurnCubeUpFaceToRightFace()
        {
            TurnFrontFaceTo("CW");
            TurnUpHorMiddleTo("CW");
            TurnBackFaceTo("CCW");
        }

        /// <summary>
        /// Rotate the entire cube a quarter turn so that the upper face goes to the left face 
        /// </summary>
        private static void TurnCubeUpFaceToLeftFace()
        {
            TurnFrontFaceTo("CCW");
            TurnUpHorMiddleTo("CCW");
            TurnBackFaceTo("CW");
        }

        /// <summary>
        /// Turn the entire front face clockwise or counter clockwise
        /// </summary>
        /// <param name="cDirection"></param>
        public static void TurnFrontFaceTo(string cDirection)
        {
            // Create a span for the arrays
            Span<string> aPiecesSpan = aPieces.AsSpan();
            Span<string> aPiecesTempSpan = aPiecesTemp.AsSpan();

            // Copy the pieces to the temporary array
            aPiecesSpan[..54].CopyTo(aPiecesTempSpan);

            if (cDirection == "CW")
            {
                aPiecesSpan[0] = aPiecesTempSpan[6];
                aPiecesSpan[1] = aPiecesTempSpan[3];
                aPiecesSpan[2] = aPiecesTempSpan[0];
                aPiecesSpan[3] = aPiecesTempSpan[7];
                aPiecesSpan[5] = aPiecesTempSpan[1];
                aPiecesSpan[6] = aPiecesTempSpan[8];
                aPiecesSpan[7] = aPiecesTempSpan[5];
                aPiecesSpan[8] = aPiecesTempSpan[2];

                aPiecesSpan[42] = aPiecesTempSpan[35];
                aPiecesSpan[43] = aPiecesTempSpan[32];
                aPiecesSpan[44] = aPiecesTempSpan[29];

                aPiecesSpan[9] = aPiecesTempSpan[42];
                aPiecesSpan[12] = aPiecesTempSpan[43];
                aPiecesSpan[15] = aPiecesTempSpan[44];

                aPiecesSpan[45] = aPiecesTempSpan[15];
                aPiecesSpan[46] = aPiecesTempSpan[12];
                aPiecesSpan[47] = aPiecesTempSpan[9];

                aPiecesSpan[29] = aPiecesTempSpan[45];
                aPiecesSpan[32] = aPiecesTempSpan[46];
                aPiecesSpan[35] = aPiecesTempSpan[47];
            }

            if (cDirection == "CCW")
            {
                aPiecesSpan[0] = aPiecesTempSpan[2];
                aPiecesSpan[1] = aPiecesTempSpan[5];
                aPiecesSpan[2] = aPiecesTempSpan[8];
                aPiecesSpan[3] = aPiecesTempSpan[1];
                aPiecesSpan[5] = aPiecesTempSpan[7];
                aPiecesSpan[6] = aPiecesTempSpan[0];
                aPiecesSpan[7] = aPiecesTempSpan[3];
                aPiecesSpan[8] = aPiecesTempSpan[6];

                aPiecesSpan[42] = aPiecesTempSpan[9];
                aPiecesSpan[43] = aPiecesTempSpan[12];
                aPiecesSpan[44] = aPiecesTempSpan[15];

                aPiecesSpan[9] = aPiecesTempSpan[47];
                aPiecesSpan[12] = aPiecesTempSpan[46];
                aPiecesSpan[15] = aPiecesTempSpan[45];

                aPiecesSpan[45] = aPiecesTempSpan[29];
                aPiecesSpan[46] = aPiecesTempSpan[32];
                aPiecesSpan[47] = aPiecesTempSpan[35];

                aPiecesSpan[29] = aPiecesTempSpan[44];
                aPiecesSpan[32] = aPiecesTempSpan[43];
                aPiecesSpan[35] = aPiecesTempSpan[42];
            }
        }

        /// <summary>
        /// Turn the top horizontal middle layer to the right or left
        /// </summary>
        /// <param name="cDirection"></param>
        public static void TurnUpHorMiddleTo(string cDirection)
        {
            // Create a span for the arrays
            Span<string> aPiecesSpan = aPieces.AsSpan();
            Span<string> aPiecesTempSpan = aPiecesTemp.AsSpan();
            
            // Copy the pieces to the temporary array
            aPiecesSpan[..54].CopyTo(aPiecesTempSpan);

            if (cDirection == "CW")
            {
                aPiecesSpan[39] = aPiecesTempSpan[34];
                aPiecesSpan[40] = aPiecesTempSpan[31];
                aPiecesSpan[41] = aPiecesTempSpan[28];

                aPiecesSpan[10] = aPiecesTempSpan[39];
                aPiecesSpan[13] = aPiecesTempSpan[40];
                aPiecesSpan[16] = aPiecesTempSpan[41];

                aPiecesSpan[48] = aPiecesTempSpan[16];
                aPiecesSpan[49] = aPiecesTempSpan[13];
                aPiecesSpan[50] = aPiecesTempSpan[10];

                aPiecesSpan[28] = aPiecesTempSpan[48];
                aPiecesSpan[31] = aPiecesTempSpan[49];
                aPiecesSpan[34] = aPiecesTempSpan[50];
            }

            if (cDirection == "CCW")
            {
                aPiecesSpan[39] = aPiecesTempSpan[10];
                aPiecesSpan[40] = aPiecesTempSpan[13];
                aPiecesSpan[41] = aPiecesTempSpan[16];

                aPiecesSpan[10] = aPiecesTempSpan[50];
                aPiecesSpan[13] = aPiecesTempSpan[49];
                aPiecesSpan[16] = aPiecesTempSpan[48];

                aPiecesSpan[48] = aPiecesTempSpan[28];
                aPiecesSpan[49] = aPiecesTempSpan[31];
                aPiecesSpan[50] = aPiecesTempSpan[34];

                aPiecesSpan[28] = aPiecesTempSpan[41];
                aPiecesSpan[31] = aPiecesTempSpan[40];
                aPiecesSpan[34] = aPiecesTempSpan[39];
            }
        }

        /// <summary>
        /// Turn the entire back face clockwise or counter clockwise
        /// </summary>
        /// <param name="cDirection"></param>
        public static void TurnBackFaceTo(string cDirection)
        {
            // Create a span for the arrays
            Span<string> aPiecesSpan = aPieces.AsSpan();
            Span<string> aPiecesTempSpan = aPiecesTemp.AsSpan();

            // Copy the pieces to the temporary array
            aPiecesSpan[..54].CopyTo(aPiecesTempSpan);

            if (cDirection == "CW")
            {
                aPiecesSpan[18] = aPiecesTempSpan[24];
                aPiecesSpan[19] = aPiecesTempSpan[21];
                aPiecesSpan[20] = aPiecesTempSpan[18];
                aPiecesSpan[21] = aPiecesTempSpan[25];
                aPiecesSpan[23] = aPiecesTempSpan[19];
                aPiecesSpan[24] = aPiecesTempSpan[26];
                aPiecesSpan[25] = aPiecesTempSpan[23];
                aPiecesSpan[26] = aPiecesTempSpan[20];

                aPiecesSpan[36] = aPiecesTempSpan[11];
                aPiecesSpan[37] = aPiecesTempSpan[14];
                aPiecesSpan[38] = aPiecesTempSpan[17];

                aPiecesSpan[11] = aPiecesTempSpan[53];
                aPiecesSpan[14] = aPiecesTempSpan[52];
                aPiecesSpan[17] = aPiecesTempSpan[51];

                aPiecesSpan[51] = aPiecesTempSpan[27];
                aPiecesSpan[52] = aPiecesTempSpan[30];
                aPiecesSpan[53] = aPiecesTempSpan[33];

                aPiecesSpan[27] = aPiecesTempSpan[38];
                aPiecesSpan[30] = aPiecesTempSpan[37];
                aPiecesSpan[33] = aPiecesTempSpan[36];
            }

            if (cDirection == "CCW")
            {
                aPiecesSpan[18] = aPiecesTempSpan[20];
                aPiecesSpan[19] = aPiecesTempSpan[23];
                aPiecesSpan[20] = aPiecesTempSpan[26];
                aPiecesSpan[21] = aPiecesTempSpan[19];
                aPiecesSpan[23] = aPiecesTempSpan[25];
                aPiecesSpan[24] = aPiecesTempSpan[18];
                aPiecesSpan[25] = aPiecesTempSpan[21];
                aPiecesSpan[26] = aPiecesTempSpan[24];

                aPiecesSpan[36] = aPiecesTempSpan[33];
                aPiecesSpan[37] = aPiecesTempSpan[30];
                aPiecesSpan[38] = aPiecesTempSpan[27];

                aPiecesSpan[11] = aPiecesTempSpan[36];
                aPiecesSpan[14] = aPiecesTempSpan[37];
                aPiecesSpan[17] = aPiecesTempSpan[38];

                aPiecesSpan[51] = aPiecesTempSpan[17];
                aPiecesSpan[52] = aPiecesTempSpan[14];
                aPiecesSpan[53] = aPiecesTempSpan[11];

                aPiecesSpan[27] = aPiecesTempSpan[51];
                aPiecesSpan[30] = aPiecesTempSpan[52];
                aPiecesSpan[33] = aPiecesTempSpan[53];
            }
        }

        /// <summary>
        /// Turn the entire left face clockwise or counter clockwise
        /// </summary>
        /// <param name="cDirection"></param>
        public static void TurnLeftFaceTo(string cDirection)
        {
            // Create a span for the arrays
            Span<string> aPiecesSpan = aPieces.AsSpan();
            Span<string> aPiecesTempSpan = aPiecesTemp.AsSpan();

            // Copy the pieces to the temporary array
            aPiecesSpan[..54].CopyTo(aPiecesTempSpan);

            if (cDirection == "CW")
            {
                aPiecesSpan[27] = aPiecesTempSpan[33];
                aPiecesSpan[28] = aPiecesTempSpan[30];
                aPiecesSpan[29] = aPiecesTempSpan[27];
                aPiecesSpan[30] = aPiecesTempSpan[34];
                aPiecesSpan[32] = aPiecesTempSpan[28];
                aPiecesSpan[33] = aPiecesTempSpan[35];
                aPiecesSpan[34] = aPiecesTempSpan[32];
                aPiecesSpan[35] = aPiecesTempSpan[29];

                aPiecesSpan[36] = aPiecesTempSpan[26];
                aPiecesSpan[39] = aPiecesTempSpan[23];
                aPiecesSpan[42] = aPiecesTempSpan[20];

                aPiecesSpan[0] = aPiecesTempSpan[36];
                aPiecesSpan[3] = aPiecesTempSpan[39];
                aPiecesSpan[6] = aPiecesTempSpan[42];

                aPiecesSpan[45] = aPiecesTempSpan[0];
                aPiecesSpan[48] = aPiecesTempSpan[3];
                aPiecesSpan[51] = aPiecesTempSpan[6];

                aPiecesSpan[20] = aPiecesTempSpan[51];
                aPiecesSpan[23] = aPiecesTempSpan[48];
                aPiecesSpan[26] = aPiecesTempSpan[45];
            }

            if (cDirection == "CCW")
            {
                aPiecesSpan[27] = aPiecesTempSpan[29];
                aPiecesSpan[28] = aPiecesTempSpan[32];
                aPiecesSpan[29] = aPiecesTempSpan[35];
                aPiecesSpan[30] = aPiecesTempSpan[28];
                aPiecesSpan[32] = aPiecesTempSpan[34];
                aPiecesSpan[33] = aPiecesTempSpan[27];
                aPiecesSpan[34] = aPiecesTempSpan[30];
                aPiecesSpan[35] = aPiecesTempSpan[33];

                aPiecesSpan[36] = aPiecesTempSpan[0];
                aPiecesSpan[39] = aPiecesTempSpan[3];
                aPiecesSpan[42] = aPiecesTempSpan[6];

                aPiecesSpan[0] = aPiecesTempSpan[45];
                aPiecesSpan[3] = aPiecesTempSpan[48];
                aPiecesSpan[6] = aPiecesTempSpan[51];

                aPiecesSpan[45] = aPiecesTempSpan[26];
                aPiecesSpan[48] = aPiecesTempSpan[23];
                aPiecesSpan[51] = aPiecesTempSpan[20];

                aPiecesSpan[20] = aPiecesTempSpan[42];
                aPiecesSpan[23] = aPiecesTempSpan[39];
                aPiecesSpan[26] = aPiecesTempSpan[36];
            }
        }
        
        /// <summary>
        /// Turn the top vertical middle layer to back or front
        /// </summary>
        /// <param name="cDirection"></param>
        public static void TurnUpVerMiddleTo(string cDirection)
        {
            // Create a span for the arrays
            Span<string> aPiecesSpan = aPieces.AsSpan();
            Span<string> aPiecesTempSpan = aPiecesTemp.AsSpan();

            // Copy the pieces to the temporary array
            aPiecesSpan[..54].CopyTo(aPiecesTempSpan);

            if (cDirection == "CW")
            {
                aPiecesSpan[37] = aPiecesTempSpan[1];
                aPiecesSpan[40] = aPiecesTempSpan[4];
                aPiecesSpan[43] = aPiecesTempSpan[7];

                aPiecesSpan[1] = aPiecesTempSpan[46];
                aPiecesSpan[4] = aPiecesTempSpan[49];
                aPiecesSpan[7] = aPiecesTempSpan[52];

                aPiecesSpan[46] = aPiecesTempSpan[25];
                aPiecesSpan[49] = aPiecesTempSpan[22];
                aPiecesSpan[52] = aPiecesTempSpan[19];

                aPiecesSpan[19] = aPiecesTempSpan[43];
                aPiecesSpan[22] = aPiecesTempSpan[40];
                aPiecesSpan[25] = aPiecesTempSpan[37];
            }

            if (cDirection == "CCW")
            {
                aPiecesSpan[37] = aPiecesTempSpan[25];
                aPiecesSpan[40] = aPiecesTempSpan[22];
                aPiecesSpan[43] = aPiecesTempSpan[19];

                aPiecesSpan[1] = aPiecesTempSpan[37];
                aPiecesSpan[4] = aPiecesTempSpan[40];
                aPiecesSpan[7] = aPiecesTempSpan[43];

                aPiecesSpan[46] = aPiecesTempSpan[1];
                aPiecesSpan[49] = aPiecesTempSpan[4];
                aPiecesSpan[52] = aPiecesTempSpan[7];

                aPiecesSpan[19] = aPiecesTempSpan[52];
                aPiecesSpan[22] = aPiecesTempSpan[49];
                aPiecesSpan[25] = aPiecesTempSpan[46];
            }
        }

        /// <summary>
        /// Turn the entire right face clockwise or counter clockwise
        /// </summary>
        /// <param name="cDirection"></param>
        public static void TurnRightFaceTo(string cDirection)
        {
            // Create a span for the arrays
            Span<string> aPiecesSpan = aPieces.AsSpan();
            Span<string> aPiecesTempSpan = aPiecesTemp.AsSpan();

            // Copy the pieces to the temporary array
            aPiecesSpan[..54].CopyTo(aPiecesTempSpan);

            if (cDirection == "CW")
            {
                aPiecesSpan[9] = aPiecesTempSpan[15];
                aPiecesSpan[10] = aPiecesTempSpan[12];
                aPiecesSpan[11] = aPiecesTempSpan[9];
                aPiecesSpan[12] = aPiecesTempSpan[16];
                aPiecesSpan[14] = aPiecesTempSpan[10];
                aPiecesSpan[15] = aPiecesTempSpan[17];
                aPiecesSpan[16] = aPiecesTempSpan[14];
                aPiecesSpan[17] = aPiecesTempSpan[11];

                aPiecesSpan[38] = aPiecesTempSpan[2];
                aPiecesSpan[41] = aPiecesTempSpan[5];
                aPiecesSpan[44] = aPiecesTempSpan[8];

                aPiecesSpan[2] = aPiecesTempSpan[47];
                aPiecesSpan[5] = aPiecesTempSpan[50];
                aPiecesSpan[8] = aPiecesTempSpan[53];

                aPiecesSpan[47] = aPiecesTempSpan[24];
                aPiecesSpan[50] = aPiecesTempSpan[21];
                aPiecesSpan[53] = aPiecesTempSpan[18];

                aPiecesSpan[18] = aPiecesTempSpan[44];
                aPiecesSpan[21] = aPiecesTempSpan[41];
                aPiecesSpan[24] = aPiecesTempSpan[38];
            }

            if (cDirection == "CCW")
            {
                aPiecesSpan[9] = aPiecesTempSpan[11];
                aPiecesSpan[10] = aPiecesTempSpan[14];
                aPiecesSpan[11] = aPiecesTempSpan[17];
                aPiecesSpan[12] = aPiecesTempSpan[10];
                aPiecesSpan[14] = aPiecesTempSpan[16];
                aPiecesSpan[15] = aPiecesTempSpan[9];
                aPiecesSpan[16] = aPiecesTempSpan[12];
                aPiecesSpan[17] = aPiecesTempSpan[15];

                aPiecesSpan[38] = aPiecesTempSpan[24];
                aPiecesSpan[41] = aPiecesTempSpan[21];
                aPiecesSpan[44] = aPiecesTempSpan[18];

                aPiecesSpan[2] = aPiecesTempSpan[38];
                aPiecesSpan[5] = aPiecesTempSpan[41];
                aPiecesSpan[8] = aPiecesTempSpan[44];

                aPiecesSpan[47] = aPiecesTempSpan[2];
                aPiecesSpan[50] = aPiecesTempSpan[5];
                aPiecesSpan[53] = aPiecesTempSpan[8];

                aPiecesSpan[18] = aPiecesTempSpan[53];
                aPiecesSpan[21] = aPiecesTempSpan[50];
                aPiecesSpan[24] = aPiecesTempSpan[47];
            }
        }

        /// <summary>
        /// Turn the entire upper face clockwise or counter clockwise
        /// </summary>
        /// <param name="cDirection"></param>
        public static void TurnUpFaceTo(string cDirection)
        {
            // Create a span for the arrays
            Span<string> aPiecesSpan = aPieces.AsSpan();
            Span<string> aPiecesTempSpan = aPiecesTemp.AsSpan();

            // Copy the pieces to the temporary array
            aPiecesSpan[..54].CopyTo(aPiecesTempSpan);

            if (cDirection == "CW")
            {
                aPiecesSpan[36] = aPiecesTempSpan[42];
                aPiecesSpan[37] = aPiecesTempSpan[39];
                aPiecesSpan[38] = aPiecesTempSpan[36];
                aPiecesSpan[39] = aPiecesTempSpan[43];
                aPiecesSpan[41] = aPiecesTempSpan[37];
                aPiecesSpan[42] = aPiecesTempSpan[44];
                aPiecesSpan[43] = aPiecesTempSpan[41];
                aPiecesSpan[44] = aPiecesTempSpan[38];

                aPiecesSpan[27] = aPiecesTempSpan[0];
                aPiecesSpan[28] = aPiecesTempSpan[1];
                aPiecesSpan[29] = aPiecesTempSpan[2];

                aPiecesSpan[0] = aPiecesTempSpan[9];
                aPiecesSpan[1] = aPiecesTempSpan[10];
                aPiecesSpan[2] = aPiecesTempSpan[11];

                aPiecesSpan[9] = aPiecesTempSpan[18];
                aPiecesSpan[10] = aPiecesTempSpan[19];
                aPiecesSpan[11] = aPiecesTempSpan[20];

                aPiecesSpan[18] = aPiecesTempSpan[27];
                aPiecesSpan[19] = aPiecesTempSpan[28];
                aPiecesSpan[20] = aPiecesTempSpan[29];
            }

            if (cDirection == "CCW")
            {
                aPiecesSpan[36] = aPiecesTempSpan[38];
                aPiecesSpan[37] = aPiecesTempSpan[41];
                aPiecesSpan[38] = aPiecesTempSpan[44];
                aPiecesSpan[39] = aPiecesTempSpan[37];
                aPiecesSpan[41] = aPiecesTempSpan[43];
                aPiecesSpan[42] = aPiecesTempSpan[36];
                aPiecesSpan[43] = aPiecesTempSpan[39];
                aPiecesSpan[44] = aPiecesTempSpan[42];

                aPiecesSpan[27] = aPiecesTempSpan[18];
                aPiecesSpan[28] = aPiecesTempSpan[19];
                aPiecesSpan[29] = aPiecesTempSpan[20];

                aPiecesSpan[0] = aPiecesTempSpan[27];
                aPiecesSpan[1] = aPiecesTempSpan[28];
                aPiecesSpan[2] = aPiecesTempSpan[29];

                aPiecesSpan[9] = aPiecesTempSpan[0];
                aPiecesSpan[10] = aPiecesTempSpan[1];
                aPiecesSpan[11] = aPiecesTempSpan[2];

                aPiecesSpan[18] = aPiecesTempSpan[9];
                aPiecesSpan[19] = aPiecesTempSpan[10];
                aPiecesSpan[20] = aPiecesTempSpan[11];
            }
        }

        /// <summary>
        /// Turn the front horizontal middle layer to right or left
        /// </summary>
        /// <param name="cDirection"></param>
        public static void TurnFrontHorMiddleTo(string cDirection)
        {
            // Create a span for the arrays
            Span<string> aPiecesSpan = aPieces.AsSpan();
            Span<string> aPiecesTempSpan = aPiecesTemp.AsSpan();

            // Copy the pieces to the temporary array
            aPiecesSpan[..54].CopyTo(aPiecesTempSpan);

            if (cDirection == "CW")
            {
                aPiecesSpan[3] = aPiecesTempSpan[12];
                aPiecesSpan[4] = aPiecesTempSpan[13];
                aPiecesSpan[5] = aPiecesTempSpan[14];

                aPiecesSpan[12] = aPiecesTempSpan[21];
                aPiecesSpan[13] = aPiecesTempSpan[22];
                aPiecesSpan[14] = aPiecesTempSpan[23];

                aPiecesSpan[21] = aPiecesTempSpan[30];
                aPiecesSpan[22] = aPiecesTempSpan[31];
                aPiecesSpan[23] = aPiecesTempSpan[32];

                aPiecesSpan[30] = aPiecesTempSpan[3];
                aPiecesSpan[31] = aPiecesTempSpan[4];
                aPiecesSpan[32] = aPiecesTempSpan[5];
            }

            if (cDirection == "CCW")
            {
                aPiecesSpan[3] = aPiecesTempSpan[30];
                aPiecesSpan[4] = aPiecesTempSpan[31];
                aPiecesSpan[5] = aPiecesTempSpan[32];

                aPiecesSpan[12] = aPiecesTempSpan[3];
                aPiecesSpan[13] = aPiecesTempSpan[4];
                aPiecesSpan[14] = aPiecesTempSpan[5];

                aPiecesSpan[21] = aPiecesTempSpan[12];
                aPiecesSpan[22] = aPiecesTempSpan[13];
                aPiecesSpan[23] = aPiecesTempSpan[14];

                aPiecesSpan[30] = aPiecesTempSpan[21];
                aPiecesSpan[31] = aPiecesTempSpan[22];
                aPiecesSpan[32] = aPiecesTempSpan[23];
            }
        }

        /// <summary>
        /// Turn the entire down face clockwise or counter clockwise
        /// </summary>
        /// <param name="cDirection"></param>
        public static void TurnDownFaceTo(string cDirection)
        {
            // Create a span for the arrays
            Span<string> aPiecesSpan = aPieces.AsSpan();
            Span<string> aPiecesTempSpan = aPiecesTemp.AsSpan();

            // Copy the pieces to the temporary array
            aPiecesSpan[..54].CopyTo(aPiecesTempSpan);

            if (cDirection == "CW")
            {
                aPiecesSpan[45] = aPiecesTempSpan[51];
                aPiecesSpan[46] = aPiecesTempSpan[48];
                aPiecesSpan[47] = aPiecesTempSpan[45];
                aPiecesSpan[48] = aPiecesTempSpan[52];
                aPiecesSpan[50] = aPiecesTempSpan[46];
                aPiecesSpan[51] = aPiecesTempSpan[53];
                aPiecesSpan[52] = aPiecesTempSpan[50];
                aPiecesSpan[53] = aPiecesTempSpan[47];

                aPiecesSpan[33] = aPiecesTempSpan[24];
                aPiecesSpan[34] = aPiecesTempSpan[25];
                aPiecesSpan[35] = aPiecesTempSpan[26];

                aPiecesSpan[6] = aPiecesTempSpan[33];
                aPiecesSpan[7] = aPiecesTempSpan[34];
                aPiecesSpan[8] = aPiecesTempSpan[35];

                aPiecesSpan[15] = aPiecesTempSpan[6];
                aPiecesSpan[16] = aPiecesTempSpan[7];
                aPiecesSpan[17] = aPiecesTempSpan[8];

                aPiecesSpan[24] = aPiecesTempSpan[15];
                aPiecesSpan[25] = aPiecesTempSpan[16];
                aPiecesSpan[26] = aPiecesTempSpan[17];
            }

            if (cDirection == "CCW")
            {
                aPiecesSpan[45] = aPiecesTempSpan[47];
                aPiecesSpan[46] = aPiecesTempSpan[50];
                aPiecesSpan[47] = aPiecesTempSpan[53];
                aPiecesSpan[48] = aPiecesTempSpan[46];
                aPiecesSpan[50] = aPiecesTempSpan[52];
                aPiecesSpan[51] = aPiecesTempSpan[45];
                aPiecesSpan[52] = aPiecesTempSpan[48];
                aPiecesSpan[53] = aPiecesTempSpan[51];

                aPiecesSpan[33] = aPiecesTempSpan[6];
                aPiecesSpan[34] = aPiecesTempSpan[7];
                aPiecesSpan[35] = aPiecesTempSpan[8];

                aPiecesSpan[6] = aPiecesTempSpan[15];
                aPiecesSpan[7] = aPiecesTempSpan[16];
                aPiecesSpan[8] = aPiecesTempSpan[17];

                aPiecesSpan[15] = aPiecesTempSpan[24];
                aPiecesSpan[16] = aPiecesTempSpan[25];
                aPiecesSpan[17] = aPiecesTempSpan[26];

                aPiecesSpan[24] = aPiecesTempSpan[33];
                aPiecesSpan[25] = aPiecesTempSpan[34];
                aPiecesSpan[26] = aPiecesTempSpan[35];
            }
        }
    }
}
