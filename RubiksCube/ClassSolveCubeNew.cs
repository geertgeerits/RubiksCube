// This solution is based on the video: https://www.youtube.com/watch?v=7Ron6MN45LY&t=34s

using System.Diagnostics;

namespace RubiksCube
{
    internal class ClassSolveCubeNew
    {
        //// Declare variables
        private const int nLoopTimesMax = 500;

        //// Solve the cube.
        public static async Task<bool> SolveTheCubeNewAsync()
        {
            if (!await SolveTopLayerEdgesAsync())
            {
                return false;
            }

            if (!await SolveTopLayerCornersAsync())
            {
                return true;
                //return false;
            }

            //if (!await SolveMiddleLayerAsync())
            //{
            //    return false;
            //}

            //if (!await SolveBottomLayerCornersAsync())
            //{
            //    return false;
            //}

            //if (!await SolveBottomLayerEdgesAsync())
            //{
            //    return false;
            //}

            //if (!await SolveBottomLayerTumblingCornersAsync())
            //{
            //    return false;
            //}

            //if (!await SolveBottomLayerTumblingEdgesAsync())
            //{
            //    return false;
            //}

            // Check if the cube is solved
            if (ClassColorsCube.CheckIfSolved())
            {
                return true;
            }

            return true;
            //return false;
        }

        /// Solve the edges of the top layer
        private static async Task<bool> SolveTopLayerEdgesAsync()
        {
            string cB = Globals.aPieces[40];

            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("nLoopTimes top layer edges: " + nLoopTimes);
                    return false;
                }

                if (cB == Globals.aPieces[37] && cB == Globals.aPieces[39] && cB == Globals.aPieces[41] && cB == Globals.aPieces[43])
                {
                    break;
                }

                // Top color is at the down face and the second color is at the right face
                if (cB == Globals.aPieces[46] && Globals.aPieces[13] == Globals.aPieces[7])
                {
                    await MakeTurnAsync(Globals.turnDownCW);
                    await MakeTurnAsync(Globals.turnRight2);
                }

                if (cB == Globals.aPieces[48] && Globals.aPieces[13] == Globals.aPieces[34])
                {
                    await MakeTurnAsync(Globals.turnDown2);
                    await MakeTurnAsync(Globals.turnRight2);
                }

                if (cB == Globals.aPieces[50] && Globals.aPieces[13] == Globals.aPieces[16])
                {
                    await MakeTurnAsync(Globals.turnRight2);
                }

                if (cB == Globals.aPieces[52] && Globals.aPieces[13] == Globals.aPieces[25])
                {
                    await MakeTurnAsync(Globals.turnDownCCW);
                    await MakeTurnAsync(Globals.turnRight2);
                }

                // Top color is at the down face and the second color is at the front face
                if (cB == Globals.aPieces[46] && Globals.aPieces[4] == Globals.aPieces[7])
                {
                    await MakeTurnAsync(Globals.turnFront2);
                }

                if (cB == Globals.aPieces[48] && Globals.aPieces[4] == Globals.aPieces[34])
                {
                    await MakeTurnAsync(Globals.turnDownCW);
                    await MakeTurnAsync(Globals.turnFront2);
                }

                if (cB == Globals.aPieces[50] && Globals.aPieces[4] == Globals.aPieces[16])
                {
                    await MakeTurnAsync(Globals.turnDownCCW);
                    await MakeTurnAsync(Globals.turnFront2);
                }

                if (cB == Globals.aPieces[52] && Globals.aPieces[4] == Globals.aPieces[25])
                {
                    await MakeTurnAsync(Globals.turnDown2);
                    await MakeTurnAsync(Globals.turnFront2);
                }

                // Top color is at the down face and the second color is at the left face
                if (cB == Globals.aPieces[46] && Globals.aPieces[31] == Globals.aPieces[7])
                {
                    await MakeTurnAsync(Globals.turnDownCCW);
                    await MakeTurnAsync(Globals.turnLeft2);
                }

                if (cB == Globals.aPieces[48] && Globals.aPieces[31] == Globals.aPieces[34])
                {
                    await MakeTurnAsync(Globals.turnLeft2);
                }

                if (cB == Globals.aPieces[50] && Globals.aPieces[31] == Globals.aPieces[16])
                {
                    await MakeTurnAsync(Globals.turnDown2);
                    await MakeTurnAsync(Globals.turnLeft2);
                }

                if (cB == Globals.aPieces[52] && Globals.aPieces[31] == Globals.aPieces[25])
                {
                    await MakeTurnAsync(Globals.turnDownCW);
                    await MakeTurnAsync(Globals.turnLeft2);
                }

                // Top color is at the down face and the second color is at the back face
                if (cB == Globals.aPieces[46] && Globals.aPieces[22] == Globals.aPieces[7])
                {
                    await MakeTurnAsync(Globals.turnDown2);
                    await MakeTurnAsync(Globals.turnBack2);
                }

                if (cB == Globals.aPieces[48] && Globals.aPieces[22] == Globals.aPieces[34])
                {
                    await MakeTurnAsync(Globals.turnDownCCW);
                    await MakeTurnAsync(Globals.turnBack2);
                }

                if (cB == Globals.aPieces[50] && Globals.aPieces[22] == Globals.aPieces[16])
                {
                    await MakeTurnAsync(Globals.turnDownCW);
                    await MakeTurnAsync(Globals.turnBack2);
                }

                if (cB == Globals.aPieces[52] && Globals.aPieces[22] == Globals.aPieces[25])
                {
                    await MakeTurnAsync(Globals.turnBack2);
                }

                // Top color is at the bottom-front face and the second color is at the down face
                if (cB == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[46])
                {
                    await MakeTurnAsync(Globals.turnFrontCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnRightCW);
                }

                if (cB == Globals.aPieces[16] && Globals.aPieces[4] == Globals.aPieces[50])
                {
                    await MakeTurnAsync(Globals.turnDownCCW);
                    await MakeTurnAsync(Globals.turnFrontCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnRightCW);
                }

                if (cB == Globals.aPieces[34] && Globals.aPieces[4] == Globals.aPieces[48])
                {
                    await MakeTurnAsync(Globals.turnDownCW);
                    await MakeTurnAsync(Globals.turnFrontCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnRightCW);
                }

                if (cB == Globals.aPieces[25] && Globals.aPieces[4] == Globals.aPieces[52])
                {
                    await MakeTurnAsync(Globals.turnDown2);
                    await MakeTurnAsync(Globals.turnFrontCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnRightCW);
                }

                // Top color is at the bottom-right face and the second color is at the down face
                if (cB == Globals.aPieces[7] && Globals.aPieces[13] == Globals.aPieces[46])
                {
                    await MakeTurnAsync(Globals.turnDownCW);
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnBackCW);
                }

                if (cB == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[50])
                {
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnBackCW);
                }

                if (cB == Globals.aPieces[34] && Globals.aPieces[13] == Globals.aPieces[48])
                {
                    await MakeTurnAsync(Globals.turnDown2);
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnBackCW);
                }

                if (cB == Globals.aPieces[25] && Globals.aPieces[13] == Globals.aPieces[52])
                {
                    await MakeTurnAsync(Globals.turnDownCCW);
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnBackCW);
                }

                // Top color is at the bottom-left face and the second color is at the down face
                if (cB == Globals.aPieces[7] && Globals.aPieces[31] == Globals.aPieces[46])
                {
                    await MakeTurnAsync(Globals.turnDownCCW);
                    await MakeTurnAsync(Globals.turnLeftCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnFrontCW);
                }

                if (cB == Globals.aPieces[16] && Globals.aPieces[31] == Globals.aPieces[50])
                {
                    await MakeTurnAsync(Globals.turnDown2);
                    await MakeTurnAsync(Globals.turnLeftCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnFrontCW);
                }

                if (cB == Globals.aPieces[34] && Globals.aPieces[31] == Globals.aPieces[48])
                {
                    await MakeTurnAsync(Globals.turnLeftCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnFrontCW);
                }

                if (cB == Globals.aPieces[25] && Globals.aPieces[31] == Globals.aPieces[52])
                {
                    await MakeTurnAsync(Globals.turnDownCW);
                    await MakeTurnAsync(Globals.turnLeftCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnFrontCW);
                }

                // Top color is at the bottom-back face and the second color is at the down face
                if (cB == Globals.aPieces[7] && Globals.aPieces[22] == Globals.aPieces[46])
                {
                    await MakeTurnAsync(Globals.turnDown2);
                    await MakeTurnAsync(Globals.turnBackCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnRightCCW);
                }

                if (cB == Globals.aPieces[16] && Globals.aPieces[22] == Globals.aPieces[50])
                {
                    await MakeTurnAsync(Globals.turnDownCW);
                    await MakeTurnAsync(Globals.turnBackCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnRightCCW);
                }

                if (cB == Globals.aPieces[34] && Globals.aPieces[22] == Globals.aPieces[48])
                {
                    await MakeTurnAsync(Globals.turnDownCCW);
                    await MakeTurnAsync(Globals.turnBackCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnRightCCW);
                }

                if (cB == Globals.aPieces[25] && Globals.aPieces[22] == Globals.aPieces[52])
                {
                    await MakeTurnAsync(Globals.turnBackCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnRightCCW);
                }

                // Top color is at the middle face - turn it to the down face
                if (cB == Globals.aPieces[3])
                {
                    await MakeTurnAsync(Globals.turnLeftCW);
                    await MakeTurnAsync(Globals.turnDownCCW);
                }

                if (cB == Globals.aPieces[5])
                {
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnDownCW);
                }

                if (cB == Globals.aPieces[12])
                {
                    await MakeTurnAsync(Globals.turnFrontCW);
                    await MakeTurnAsync(Globals.turnDownCCW);
                }

                if (cB == Globals.aPieces[14])
                {
                    await MakeTurnAsync(Globals.turnBackCCW);
                    await MakeTurnAsync(Globals.turnDownCW);
                }

                if (cB == Globals.aPieces[21])
                {
                    await MakeTurnAsync(Globals.turnRightCW);
                    await MakeTurnAsync(Globals.turnDownCCW);
                }

                if (cB == Globals.aPieces[23])
                {
                    await MakeTurnAsync(Globals.turnLeftCCW);
                    await MakeTurnAsync(Globals.turnDownCW);
                }

                if (cB == Globals.aPieces[30])
                {
                    await MakeTurnAsync(Globals.turnBackCW);
                    await MakeTurnAsync(Globals.turnDownCCW);
                }

                if (cB == Globals.aPieces[32])
                {
                    await MakeTurnAsync(Globals.turnFrontCCW);
                    await MakeTurnAsync(Globals.turnDownCW);
                }
            }

            return true;
        }

        /// Solve the corners of the top layer
        private static async Task<bool> SolveTopLayerCornersAsync()
        {
            string cB = Globals.aPieces[40];

            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("nLoopTimes top layer corners: " + nLoopTimes);
                    return false;
                }

                if (cB == Globals.aPieces[36] && cB == Globals.aPieces[38] && cB == Globals.aPieces[42] && cB == Globals.aPieces[44])
                {
                    break;
                }
                
                if (Globals.aPieces[1] == Globals.aPieces[13])
                {
                    await MakeTurnAsync(Globals.turnUpCCW);
                }

                if (Globals.aPieces[1] == Globals.aPieces[31])
                {
                    await MakeTurnAsync(Globals.turnUpCW);
                }

                if (Globals.aPieces[1] == Globals.aPieces[22])
                {
                    await MakeTurnAsync(Globals.turnUp2);
                }

                await MakeTurnAsync(Globals.turnCubeUpToRight2);

                cB = Globals.aPieces[49];
                
                if (cB == Globals.aPieces[0] && Globals.aPieces[4] == Globals.aPieces[42] && Globals.aPieces[31] == Globals.aPieces[29])
                {
                    await MakeTurnAsync(Globals.turnLeftCW);
                }

                //await MakeTurnAsync(Globals.turnRightCW);
                //await MakeTurnAsync(Globals.turnUpCW);
                //await MakeTurnAsync(Globals.turnRightCCW);
                //await MakeTurnAsync(Globals.turnUpCCW);

                //await MakeTurnAsync(Globals.turnLeftCCW);
                //await MakeTurnAsync(Globals.turnUpCCW);
                //await MakeTurnAsync(Globals.turnLeftCW);
                //await MakeTurnAsync(Globals.turnUpCW);
            }

            return true;
        }




        /// Solve the middle layer - Chapter 10, page 21

        /// Solve the bottom layer - Chapter 11, page 23

        // Put the edges on the correct place

        // Flip the corners

        // Turning the edges



        // Make a turn of the cube/face/side
        private static async Task MakeTurnAsync(string cTurnFaceAndDirection)
        {
            // Add the turn to the list
            Globals.lCubeTurns.Add(cTurnFaceAndDirection);

            // Turn the cube/face/side
            await ClassCubeTurns.TurnFaceCubeAsync(cTurnFaceAndDirection);
        }
    }
}
