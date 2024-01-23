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
            string cColorFront1 = Globals.aFrontFace[1];
            string cColorFront2 = Globals.aFrontFace[2];
            string cColorFront3 = Globals.aFrontFace[3];
            string cColorFront4 = Globals.aFrontFace[4];
            string cColorFront6 = Globals.aFrontFace[6];
            string cColorFront7 = Globals.aFrontFace[7];
            string cColorFront8 = Globals.aFrontFace[8];
            string cColorFront9 = Globals.aFrontFace[9];

            string cColorUp7 = Globals.aUpFace[7];
            string cColorUp8 = Globals.aUpFace[8];
            string cColorUp9 = Globals.aUpFace[9];

            string cColorRight1 = Globals.aRightFace[1];
            string cColorRight4 = Globals.aRightFace[4];
            string cColorRight7 = Globals.aRightFace[7];

            string cColorDown1 = Globals.aDownFace[1];
            string cColorDown2 = Globals.aDownFace[2];
            string cColorDown3 = Globals.aDownFace[3];

            string cColorLeft3 = Globals.aLeftFace[3];
            string cColorLeft6 = Globals.aLeftFace[6];
            string cColorLeft9 = Globals.aLeftFace[9];

            if (cDirection == "+")
            {
                Globals.aFrontFace[1] = cColorFront7;
                Globals.aFrontFace[2] = cColorFront4;
                Globals.aFrontFace[3] = cColorFront1;
                Globals.aFrontFace[4] = cColorFront8;
                Globals.aFrontFace[6] = cColorFront2;
                Globals.aFrontFace[7] = cColorFront9;
                Globals.aFrontFace[8] = cColorFront6;
                Globals.aFrontFace[9] = cColorFront3;

                Globals.aUpFace[7] = cColorLeft9;
                Globals.aUpFace[8] = cColorLeft6;
                Globals.aUpFace[9] = cColorLeft3;

                Globals.aRightFace[1] = cColorUp7;
                Globals.aRightFace[4] = cColorUp8;
                Globals.aRightFace[7] = cColorUp9;

                Globals.aDownFace[1] = cColorRight7;
                Globals.aDownFace[2] = cColorRight4;
                Globals.aDownFace[3] = cColorRight1;

                Globals.aLeftFace[3] = cColorDown1;
                Globals.aLeftFace[6] = cColorDown2;
                Globals.aLeftFace[9] = cColorDown3;
            }

            if (cDirection == "-")
            {
                Globals.aFrontFace[1] = cColorFront3;
                Globals.aFrontFace[2] = cColorFront6;
                Globals.aFrontFace[3] = cColorFront9;
                Globals.aFrontFace[4] = cColorFront2;
                Globals.aFrontFace[6] = cColorFront8;
                Globals.aFrontFace[7] = cColorFront1;
                Globals.aFrontFace[8] = cColorFront4;
                Globals.aFrontFace[9] = cColorFront7;

                Globals.aUpFace[7] = cColorRight1;
                Globals.aUpFace[8] = cColorRight4;
                Globals.aUpFace[9] = cColorRight7;

                Globals.aRightFace[1] = cColorDown3;
                Globals.aRightFace[4] = cColorDown2;
                Globals.aRightFace[7] = cColorDown1;

                Globals.aDownFace[1] = cColorLeft3;
                Globals.aDownFace[2] = cColorLeft6;
                Globals.aDownFace[3] = cColorLeft9;

                Globals.aLeftFace[3] = cColorUp9;
                Globals.aLeftFace[6] = cColorUp8;
                Globals.aLeftFace[9] = cColorUp7;
            }
        }

        // Turn the top horizontal middle layer to the right or left
        public static void TurnUpHorMiddleTo(string cDirection)
        {
            string cColorUp4 = Globals.aUpFace[4];
            string cColorUp5 = Globals.aUpFace[5];
            string cColorUp6 = Globals.aUpFace[6];

            string cColorRight2 = Globals.aRightFace[2];
            string cColorRight5 = Globals.aRightFace[5];
            string cColorRight8 = Globals.aRightFace[8];

            string cColorDown4 = Globals.aDownFace[4];
            string cColorDown5 = Globals.aDownFace[5];
            string cColorDown6 = Globals.aDownFace[6];

            string cColorLeft2 = Globals.aLeftFace[2];
            string cColorLeft5 = Globals.aLeftFace[5];
            string cColorLeft8 = Globals.aLeftFace[8];

            if (cDirection == "+")
            {
                Globals.aUpFace[4] = cColorLeft8;
                Globals.aUpFace[5] = cColorLeft5;
                Globals.aUpFace[6] = cColorLeft2;

                Globals.aRightFace[2] = cColorUp4;
                Globals.aRightFace[5] = cColorUp5;
                Globals.aRightFace[8] = cColorUp6;

                Globals.aDownFace[4] = cColorRight8;
                Globals.aDownFace[5] = cColorRight5;
                Globals.aDownFace[6] = cColorRight2;

                Globals.aLeftFace[2] = cColorDown4;
                Globals.aLeftFace[5] = cColorDown5;
                Globals.aLeftFace[8] = cColorDown6;
            }

            if (cDirection == "-")
            {
                Globals.aUpFace[4] = cColorRight2;
                Globals.aUpFace[5] = cColorRight5;
                Globals.aUpFace[6] = cColorRight8;

                Globals.aRightFace[2] = cColorDown6;
                Globals.aRightFace[5] = cColorDown5;
                Globals.aRightFace[8] = cColorDown4;

                Globals.aDownFace[4] = cColorLeft2;
                Globals.aDownFace[5] = cColorLeft5;
                Globals.aDownFace[6] = cColorLeft8;

                Globals.aLeftFace[2] = cColorUp6;
                Globals.aLeftFace[5] = cColorUp5;
                Globals.aLeftFace[8] = cColorUp4;
            }
        }

        // Turn the entire back face clockwise or counter clockwise
        public static void TurnBackFaceTo(string cDirection)
        {
            string cColorBack1 = Globals.aBackFace[1];
            string cColorBack2 = Globals.aBackFace[2];
            string cColorBack3 = Globals.aBackFace[3];
            string cColorBack4 = Globals.aBackFace[4];
            string cColorBack6 = Globals.aBackFace[6];
            string cColorBack7 = Globals.aBackFace[7];
            string cColorBack8 = Globals.aBackFace[8];
            string cColorBack9 = Globals.aBackFace[9];

            string cColorUp1 = Globals.aUpFace[1];
            string cColorUp2 = Globals.aUpFace[2];
            string cColorUp3 = Globals.aUpFace[3];

            string cColorRight3 = Globals.aRightFace[3];
            string cColorRight6 = Globals.aRightFace[6];
            string cColorRight9 = Globals.aRightFace[9];

            string cColorDown7 = Globals.aDownFace[7];
            string cColorDown8 = Globals.aDownFace[8];
            string cColorDown9 = Globals.aDownFace[9];

            string cColorLeft1 = Globals.aLeftFace[1];
            string cColorLeft4 = Globals.aLeftFace[4];
            string cColorLeft7 = Globals.aLeftFace[7];

            if (cDirection == "+")
            {
                Globals.aBackFace[1] = cColorBack7;
                Globals.aBackFace[2] = cColorBack4;
                Globals.aBackFace[3] = cColorBack1;
                Globals.aBackFace[4] = cColorBack8;
                Globals.aBackFace[6] = cColorBack2;
                Globals.aBackFace[7] = cColorBack9;
                Globals.aBackFace[8] = cColorBack6;
                Globals.aBackFace[9] = cColorBack3;

                Globals.aUpFace[1] = cColorRight3;
                Globals.aUpFace[2] = cColorRight6;
                Globals.aUpFace[3] = cColorRight9;

                Globals.aRightFace[3] = cColorDown9;
                Globals.aRightFace[6] = cColorDown8;
                Globals.aRightFace[9] = cColorDown7;

                Globals.aDownFace[7] = cColorLeft1;
                Globals.aDownFace[8] = cColorLeft4;
                Globals.aDownFace[9] = cColorLeft7;

                Globals.aLeftFace[1] = cColorUp3;
                Globals.aLeftFace[4] = cColorUp2;
                Globals.aLeftFace[7] = cColorUp1;
            }

            if (cDirection == "-")
            {
                Globals.aBackFace[1] = cColorBack3;
                Globals.aBackFace[2] = cColorBack6;
                Globals.aBackFace[3] = cColorBack9;
                Globals.aBackFace[4] = cColorBack2;
                Globals.aBackFace[6] = cColorBack8;
                Globals.aBackFace[7] = cColorBack1;
                Globals.aBackFace[8] = cColorBack4;
                Globals.aBackFace[9] = cColorBack7;

                Globals.aUpFace[1] = cColorLeft7;
                Globals.aUpFace[2] = cColorLeft4;
                Globals.aUpFace[3] = cColorLeft1;

                Globals.aRightFace[3] = cColorUp1;
                Globals.aRightFace[6] = cColorUp2;
                Globals.aRightFace[9] = cColorUp3;

                Globals.aDownFace[7] = cColorRight9;
                Globals.aDownFace[8] = cColorRight6;
                Globals.aDownFace[9] = cColorRight3;

                Globals.aLeftFace[1] = cColorDown7;
                Globals.aLeftFace[4] = cColorDown8;
                Globals.aLeftFace[7] = cColorDown9;
            }
        }

        // Turn the entire left face clockwise or counter clockwise
        public static void TurnLeftFaceTo(string cDirection)
        {
            string cColorLeft1 = Globals.aLeftFace[1];
            string cColorLeft2 = Globals.aLeftFace[2];
            string cColorLeft3 = Globals.aLeftFace[3];
            string cColorLeft4 = Globals.aLeftFace[4];
            string cColorLeft6 = Globals.aLeftFace[6];
            string cColorLeft7 = Globals.aLeftFace[7];
            string cColorLeft8 = Globals.aLeftFace[8];
            string cColorLeft9 = Globals.aLeftFace[9];

            string cColorUp1 = Globals.aUpFace[1];
            string cColorUp4 = Globals.aUpFace[4];
            string cColorUp7 = Globals.aUpFace[7];

            string cColorFront1 = Globals.aFrontFace[1];
            string cColorFront4 = Globals.aFrontFace[4];
            string cColorFront7 = Globals.aFrontFace[7];

            string cColorDown1 = Globals.aDownFace[1];
            string cColorDown4 = Globals.aDownFace[4];
            string cColorDown7 = Globals.aDownFace[7];

            string cColorBack3 = Globals.aBackFace[3];
            string cColorBack6 = Globals.aBackFace[6];
            string cColorBack9 = Globals.aBackFace[9];

            if (cDirection == "+")
            {
                Globals.aLeftFace[1] = cColorLeft7;
                Globals.aLeftFace[2] = cColorLeft4;
                Globals.aLeftFace[3] = cColorLeft1;
                Globals.aLeftFace[4] = cColorLeft8;
                Globals.aLeftFace[6] = cColorLeft2;
                Globals.aLeftFace[7] = cColorLeft9;
                Globals.aLeftFace[8] = cColorLeft6;
                Globals.aLeftFace[9] = cColorLeft3;

                Globals.aUpFace[1] = cColorBack9;
                Globals.aUpFace[4] = cColorBack6;
                Globals.aUpFace[7] = cColorBack3;

                Globals.aFrontFace[1] = cColorUp1;
                Globals.aFrontFace[4] = cColorUp4;
                Globals.aFrontFace[7] = cColorUp7;

                Globals.aDownFace[1] = cColorFront1;
                Globals.aDownFace[4] = cColorFront4;
                Globals.aDownFace[7] = cColorFront7;

                Globals.aBackFace[3] = cColorDown7;
                Globals.aBackFace[6] = cColorDown4;
                Globals.aBackFace[9] = cColorDown1;
            }

            if (cDirection == "-")
            {
                Globals.aLeftFace[1] = cColorLeft3;
                Globals.aLeftFace[2] = cColorLeft6;
                Globals.aLeftFace[3] = cColorLeft9;
                Globals.aLeftFace[4] = cColorLeft2;
                Globals.aLeftFace[6] = cColorLeft8;
                Globals.aLeftFace[7] = cColorLeft1;
                Globals.aLeftFace[8] = cColorLeft4;
                Globals.aLeftFace[9] = cColorLeft7;

                Globals.aUpFace[1] = cColorFront1;
                Globals.aUpFace[4] = cColorFront4;
                Globals.aUpFace[7] = cColorFront7;

                Globals.aFrontFace[1] = cColorDown1;
                Globals.aFrontFace[4] = cColorDown4;
                Globals.aFrontFace[7] = cColorDown7;

                Globals.aDownFace[1] = cColorBack9;
                Globals.aDownFace[4] = cColorBack6;
                Globals.aDownFace[7] = cColorBack3;

                Globals.aBackFace[3] = cColorUp7;
                Globals.aBackFace[6] = cColorUp4;
                Globals.aBackFace[9] = cColorUp1;
            }
        }

        // Turn the top vertical middle layer to back or front
        public static void TurnUpVerMiddleTo(string cDirection)
        {
            string cColorUp2 = Globals.aUpFace[2];
            string cColorUp5 = Globals.aUpFace[5];
            string cColorUp8 = Globals.aUpFace[8];

            string cColorFront2 = Globals.aFrontFace[2];
            string cColorFront5 = Globals.aFrontFace[5];
            string cColorFront8 = Globals.aFrontFace[8];

            string cColorDown2 = Globals.aDownFace[2];
            string cColorDown5 = Globals.aDownFace[5];
            string cColorDown8 = Globals.aDownFace[8];

            string cColorBack2 = Globals.aBackFace[2];
            string cColorBack5 = Globals.aBackFace[5];
            string cColorBack8 = Globals.aBackFace[8];

            if (cDirection == "+")
            {
                Globals.aUpFace[2] = cColorFront2;
                Globals.aUpFace[5] = cColorFront5;
                Globals.aUpFace[8] = cColorFront8;

                Globals.aFrontFace[2] = cColorDown2;
                Globals.aFrontFace[5] = cColorDown5;
                Globals.aFrontFace[8] = cColorDown8;

                Globals.aDownFace[2] = cColorBack8;
                Globals.aDownFace[5] = cColorBack5;
                Globals.aDownFace[8] = cColorBack2;

                Globals.aBackFace[2] = cColorUp8;
                Globals.aBackFace[5] = cColorUp5;
                Globals.aBackFace[8] = cColorUp2;
            }

            if (cDirection == "-")
            {
                Globals.aUpFace[2] = cColorBack8;
                Globals.aUpFace[5] = cColorBack5;
                Globals.aUpFace[8] = cColorBack2;

                Globals.aFrontFace[2] = cColorUp2;
                Globals.aFrontFace[5] = cColorUp5;
                Globals.aFrontFace[8] = cColorUp8;

                Globals.aDownFace[2] = cColorFront2;
                Globals.aDownFace[5] = cColorFront5;
                Globals.aDownFace[8] = cColorFront8;

                Globals.aBackFace[2] = cColorDown8;
                Globals.aBackFace[5] = cColorDown5;
                Globals.aBackFace[8] = cColorDown2;
            }
        }

        // Turn the entire right face clockwise or counter clockwise
        public static void TurnRightFaceTo(string cDirection)
        {
            string cColorRight1 = Globals.aRightFace[1];
            string cColorRight2 = Globals.aRightFace[2];
            string cColorRight3 = Globals.aRightFace[3];
            string cColorRight4 = Globals.aRightFace[4];
            string cColorRight6 = Globals.aRightFace[6];
            string cColorRight7 = Globals.aRightFace[7];
            string cColorRight8 = Globals.aRightFace[8];
            string cColorRight9 = Globals.aRightFace[9];

            string cColorUp3 = Globals.aUpFace[3];
            string cColorUp6 = Globals.aUpFace[6];
            string cColorUp9 = Globals.aUpFace[9];

            string cColorFront3 = Globals.aFrontFace[3];
            string cColorFront6 = Globals.aFrontFace[6];
            string cColorFront9 = Globals.aFrontFace[9];

            string cColorDown3 = Globals.aDownFace[3];
            string cColorDown6 = Globals.aDownFace[6];
            string cColorDown9 = Globals.aDownFace[9];

            string cColorBack1 = Globals.aBackFace[1];
            string cColorBack4 = Globals.aBackFace[4];
            string cColorBack7 = Globals.aBackFace[7];

            if (cDirection == "+")
            {
                Globals.aRightFace[1] = cColorRight7;
                Globals.aRightFace[2] = cColorRight4;
                Globals.aRightFace[3] = cColorRight1;
                Globals.aRightFace[4] = cColorRight8;
                Globals.aRightFace[6] = cColorRight2;
                Globals.aRightFace[7] = cColorRight9;
                Globals.aRightFace[8] = cColorRight6;
                Globals.aRightFace[9] = cColorRight3;

                Globals.aUpFace[3] = cColorFront3;
                Globals.aUpFace[6] = cColorFront6;
                Globals.aUpFace[9] = cColorFront9;

                Globals.aFrontFace[3] = cColorDown3;
                Globals.aFrontFace[6] = cColorDown6;
                Globals.aFrontFace[9] = cColorDown9;

                Globals.aDownFace[3] = cColorBack7;
                Globals.aDownFace[6] = cColorBack4;
                Globals.aDownFace[9] = cColorBack1;

                Globals.aBackFace[1] = cColorUp9;
                Globals.aBackFace[4] = cColorUp6;
                Globals.aBackFace[7] = cColorUp3;
            }

            if (cDirection == "-")
            {
                Globals.aRightFace[1] = cColorRight3;
                Globals.aRightFace[2] = cColorRight6;
                Globals.aRightFace[3] = cColorRight9;
                Globals.aRightFace[4] = cColorRight2;
                Globals.aRightFace[6] = cColorRight8;
                Globals.aRightFace[7] = cColorRight1;
                Globals.aRightFace[8] = cColorRight4;
                Globals.aRightFace[9] = cColorRight7;

                Globals.aUpFace[3] = cColorBack7;
                Globals.aUpFace[6] = cColorBack4;
                Globals.aUpFace[9] = cColorBack1;

                Globals.aFrontFace[3] = cColorUp3;
                Globals.aFrontFace[6] = cColorUp6;
                Globals.aFrontFace[9] = cColorUp9;

                Globals.aDownFace[3] = cColorFront3;
                Globals.aDownFace[6] = cColorFront6;
                Globals.aDownFace[9] = cColorFront9;

                Globals.aBackFace[1] = cColorDown9;
                Globals.aBackFace[4] = cColorDown6;
                Globals.aBackFace[7] = cColorDown3;
            }
        }

        // Turn the entire upper face clockwise or counter clockwise
        public static void TurnUpFaceTo(string cDirection)
        {
            string cColorUp1 = Globals.aUpFace[1];
            string cColorUp2 = Globals.aUpFace[2];
            string cColorUp3 = Globals.aUpFace[3];
            string cColorUp4 = Globals.aUpFace[4];
            string cColorUp6 = Globals.aUpFace[6];
            string cColorUp7 = Globals.aUpFace[7];
            string cColorUp8 = Globals.aUpFace[8];
            string cColorUp9 = Globals.aUpFace[9];

            string cColorLeft1 = Globals.aLeftFace[1];
            string cColorLeft2 = Globals.aLeftFace[2];
            string cColorLeft3 = Globals.aLeftFace[3];

            string cColorFront1 = Globals.aFrontFace[1];
            string cColorFront2 = Globals.aFrontFace[2];
            string cColorFront3 = Globals.aFrontFace[3];

            string cColorRight1 = Globals.aRightFace[1];
            string cColorRight2 = Globals.aRightFace[2];
            string cColorRight3 = Globals.aRightFace[3];

            string cColorBack1 = Globals.aBackFace[1];
            string cColorBack2 = Globals.aBackFace[2];
            string cColorBack3 = Globals.aBackFace[3];

            if (cDirection == "+")
            {
                Globals.aUpFace[1] = cColorUp7;
                Globals.aUpFace[2] = cColorUp4;
                Globals.aUpFace[3] = cColorUp1;
                Globals.aUpFace[4] = cColorUp8;
                Globals.aUpFace[6] = cColorUp2;
                Globals.aUpFace[7] = cColorUp9;
                Globals.aUpFace[8] = cColorUp6;
                Globals.aUpFace[9] = cColorUp3;

                Globals.aLeftFace[1] = cColorFront1;
                Globals.aLeftFace[2] = cColorFront2;
                Globals.aLeftFace[3] = cColorFront3;

                Globals.aFrontFace[1] = cColorRight1;
                Globals.aFrontFace[2] = cColorRight2;
                Globals.aFrontFace[3] = cColorRight3;

                Globals.aRightFace[1] = cColorBack1;
                Globals.aRightFace[2] = cColorBack2;
                Globals.aRightFace[3] = cColorBack3;

                Globals.aBackFace[1] = cColorLeft1;
                Globals.aBackFace[2] = cColorLeft2;
                Globals.aBackFace[3] = cColorLeft3;
            }

            if (cDirection == "-")
            {
                Globals.aUpFace[1] = cColorUp3;
                Globals.aUpFace[2] = cColorUp6;
                Globals.aUpFace[3] = cColorUp9;
                Globals.aUpFace[4] = cColorUp2;
                Globals.aUpFace[6] = cColorUp8;
                Globals.aUpFace[7] = cColorUp1;
                Globals.aUpFace[8] = cColorUp4;
                Globals.aUpFace[9] = cColorUp7;

                Globals.aLeftFace[1] = cColorBack1;
                Globals.aLeftFace[2] = cColorBack2;
                Globals.aLeftFace[3] = cColorBack3;

                Globals.aFrontFace[1] = cColorLeft1;
                Globals.aFrontFace[2] = cColorLeft2;
                Globals.aFrontFace[3] = cColorLeft3;

                Globals.aRightFace[1] = cColorFront1;
                Globals.aRightFace[2] = cColorFront2;
                Globals.aRightFace[3] = cColorFront3;

                Globals.aBackFace[1] = cColorRight1;
                Globals.aBackFace[2] = cColorRight2;
                Globals.aBackFace[3] = cColorRight3;
            }
        }

        // Turn the front horizontal middle layer to right or left
        public static void TurnFrontHorMiddleTo(string cDirection)
        {
            string cColorFront4 = Globals.aFrontFace[4];
            string cColorFront5 = Globals.aFrontFace[5];
            string cColorFront6 = Globals.aFrontFace[6];

            string cColorRight4 = Globals.aRightFace[4];
            string cColorRight5 = Globals.aRightFace[5];
            string cColorRight6 = Globals.aRightFace[6];

            string cColorBack4 = Globals.aBackFace[4];
            string cColorBack5 = Globals.aBackFace[5];
            string cColorBack6 = Globals.aBackFace[6];

            string cColorLeft4 = Globals.aLeftFace[4];
            string cColorLeft5 = Globals.aLeftFace[5];
            string cColorLeft6 = Globals.aLeftFace[6];

            if (cDirection == "+")
            {
                Globals.aFrontFace[4] = cColorRight4;
                Globals.aFrontFace[5] = cColorRight5;
                Globals.aFrontFace[6] = cColorRight6;

                Globals.aRightFace[4] = cColorBack4;
                Globals.aRightFace[5] = cColorBack5;
                Globals.aRightFace[6] = cColorBack6;

                Globals.aBackFace[4] = cColorLeft4;
                Globals.aBackFace[5] = cColorLeft5;
                Globals.aBackFace[6] = cColorLeft6;

                Globals.aLeftFace[4] = cColorFront4;
                Globals.aLeftFace[5] = cColorFront5;
                Globals.aLeftFace[6] = cColorFront6;
            }

            if (cDirection == "-")
            {
                Globals.aFrontFace[4] = cColorLeft4;
                Globals.aFrontFace[5] = cColorLeft5;
                Globals.aFrontFace[6] = cColorLeft6;

                Globals.aRightFace[4] = cColorFront4;
                Globals.aRightFace[5] = cColorFront5;
                Globals.aRightFace[6] = cColorFront6;

                Globals.aBackFace[4] = cColorRight4;
                Globals.aBackFace[5] = cColorRight5;
                Globals.aBackFace[6] = cColorRight6;

                Globals.aLeftFace[4] = cColorBack4;
                Globals.aLeftFace[5] = cColorBack5;
                Globals.aLeftFace[6] = cColorBack6;
            }
        }

        // Turn the entire down face clockwise or counter clockwise
        public static void TurnDownFaceTo(string cDirection)
        {
            string cColorDown1 = Globals.aDownFace[1];
            string cColorDown2 = Globals.aDownFace[2];
            string cColorDown3 = Globals.aDownFace[3];
            string cColorDown4 = Globals.aDownFace[4];
            string cColorDown6 = Globals.aDownFace[6];
            string cColorDown7 = Globals.aDownFace[7];
            string cColorDown8 = Globals.aDownFace[8];
            string cColorDown9 = Globals.aDownFace[9];

            string cColorLeft7 = Globals.aLeftFace[7];
            string cColorLeft8 = Globals.aLeftFace[8];
            string cColorLeft9 = Globals.aLeftFace[9];

            string cColorFront7 = Globals.aFrontFace[7];
            string cColorFront8 = Globals.aFrontFace[8];
            string cColorFront9 = Globals.aFrontFace[9];

            string cColorRight7 = Globals.aRightFace[7];
            string cColorRight8 = Globals.aRightFace[8];
            string cColorRight9 = Globals.aRightFace[9];

            string cColorBack7 = Globals.aBackFace[7];
            string cColorBack8 = Globals.aBackFace[8];
            string cColorBack9 = Globals.aBackFace[9];

            if (cDirection == "+")
            {
                Globals.aDownFace[1] = cColorDown7;
                Globals.aDownFace[2] = cColorDown4;
                Globals.aDownFace[3] = cColorDown1;
                Globals.aDownFace[4] = cColorDown8;
                Globals.aDownFace[6] = cColorDown2;
                Globals.aDownFace[7] = cColorDown9;
                Globals.aDownFace[8] = cColorDown6;
                Globals.aDownFace[9] = cColorDown3;

                Globals.aLeftFace[7] = cColorBack7;
                Globals.aLeftFace[8] = cColorBack8;
                Globals.aLeftFace[9] = cColorBack9;

                Globals.aFrontFace[7] = cColorLeft7;
                Globals.aFrontFace[8] = cColorLeft8;
                Globals.aFrontFace[9] = cColorLeft9;

                Globals.aRightFace[7] = cColorFront7;
                Globals.aRightFace[8] = cColorFront8;
                Globals.aRightFace[9] = cColorFront9;

                Globals.aBackFace[7] = cColorRight7;
                Globals.aBackFace[8] = cColorRight8;
                Globals.aBackFace[9] = cColorRight9;
            }

            if (cDirection == "-")
            {
                Globals.aDownFace[1] = cColorDown3;
                Globals.aDownFace[2] = cColorDown6;
                Globals.aDownFace[3] = cColorDown9;
                Globals.aDownFace[4] = cColorDown2;
                Globals.aDownFace[6] = cColorDown8;
                Globals.aDownFace[7] = cColorDown1;
                Globals.aDownFace[8] = cColorDown4;
                Globals.aDownFace[9] = cColorDown7;

                Globals.aLeftFace[7] = cColorFront7;
                Globals.aLeftFace[8] = cColorFront8;
                Globals.aLeftFace[9] = cColorFront9;

                Globals.aFrontFace[7] = cColorRight7;
                Globals.aFrontFace[8] = cColorRight8;
                Globals.aFrontFace[9] = cColorRight9;

                Globals.aRightFace[7] = cColorBack7;
                Globals.aRightFace[8] = cColorBack8;
                Globals.aRightFace[9] = cColorBack9;

                Globals.aBackFace[7] = cColorLeft7;
                Globals.aBackFace[8] = cColorLeft8;
                Globals.aBackFace[9] = cColorLeft9;
            }
            //--------------------------------------------------------
            //Globals.aPiecesTemp = ClassSaveRestoreCube.SaveColorsCube();

            //if (cDirection == "+")
            //{
            //    Globals.aPieces[45] = Globals.aPiecesTemp[51];
            //    Globals.aPieces[46] = Globals.aPiecesTemp[48];
            //    Globals.aPieces[47] = Globals.aPiecesTemp[45];
            //    Globals.aPieces[48] = Globals.aPiecesTemp[52];
            //    Globals.aPieces[50] = Globals.aPiecesTemp[46];
            //    Globals.aPieces[51] = Globals.aPiecesTemp[53];
            //    Globals.aPieces[52] = Globals.aPiecesTemp[50];
            //    Globals.aPieces[53] = Globals.aPiecesTemp[47];

            //    Globals.aPieces[33] = Globals.aPiecesTemp[24];
            //    Globals.aPieces[34] = Globals.aPiecesTemp[25];
            //    Globals.aPieces[35] = Globals.aPiecesTemp[26];

            //    Globals.aPieces[6] = Globals.aPiecesTemp[33];
            //    Globals.aPieces[7] = Globals.aPiecesTemp[34];
            //    Globals.aPieces[8] = Globals.aPiecesTemp[35];

            //    Globals.aPieces[15] = Globals.aPiecesTemp[6];
            //    Globals.aPieces[16] = Globals.aPiecesTemp[7];
            //    Globals.aPieces[17] = Globals.aPiecesTemp[8];

            //    Globals.aPieces[24] = Globals.aPiecesTemp[15];
            //    Globals.aPieces[25] = Globals.aPiecesTemp[16];
            //    Globals.aPieces[26] = Globals.aPiecesTemp[17];
            //}

            //if (cDirection == "-")
            //{
            //    Globals.aPieces[45] = Globals.aPiecesTemp[47];
            //    Globals.aPieces[46] = Globals.aPiecesTemp[50];
            //    Globals.aPieces[47] = Globals.aPiecesTemp[53];
            //    Globals.aPieces[48] = Globals.aPiecesTemp[46];
            //    Globals.aPieces[50] = Globals.aPiecesTemp[52];
            //    Globals.aPieces[51] = Globals.aPiecesTemp[45];
            //    Globals.aPieces[52] = Globals.aPiecesTemp[48];
            //    Globals.aPieces[53] = Globals.aPiecesTemp[51];

            //    Globals.aPieces[33] = Globals.aPiecesTemp[6];
            //    Globals.aPieces[34] = Globals.aPiecesTemp[7];
            //    Globals.aPieces[35] = Globals.aPiecesTemp[8];

            //    Globals.aPieces[6] = Globals.aPiecesTemp[15];
            //    Globals.aPieces[7] = Globals.aPiecesTemp[16];
            //    Globals.aPieces[8] = Globals.aPiecesTemp[17];

            //    Globals.aPieces[15] = Globals.aPiecesTemp[24];
            //    Globals.aPieces[16] = Globals.aPiecesTemp[25];
            //    Globals.aPieces[17] = Globals.aPiecesTemp[26];

            //    Globals.aPieces[24] = Globals.aPiecesTemp[33];
            //    Globals.aPieces[25] = Globals.aPiecesTemp[34];
            //    Globals.aPieces[26] = Globals.aPiecesTemp[35];
            //}
        }
    }
}
