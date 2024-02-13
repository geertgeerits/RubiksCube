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
                return true;
            }

            if (!await SolveTopLayerCornersAsync())
            {
                return false;
            }

            if (!await SolveMiddleLayerAsync())
            {
                return true;
                //return false;
            }

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

                // If solved, break the loop
                if (cB == Globals.aPieces[37] && cB == Globals.aPieces[39] && cB == Globals.aPieces[41] && cB == Globals.aPieces[43])
                {
                    if (Globals.aPieces[1] == Globals.aPieces[4] && Globals.aPieces[10] == Globals.aPieces[13] && Globals.aPieces[19] == Globals.aPieces[22] && Globals.aPieces[28] == Globals.aPieces[31])
                    {
                        break;
                    }
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
                if (cB == Globals.aPieces[7])
                {
                    await SolveTopLayerEdges2Async();
                }

                // Top color is at the bottom-right/back/left face and the second color is at the down face
                if (cB == Globals.aPieces[16])
                {
                    await MakeTurnAsync(Globals.turnDownCCW);
                    await SolveTopLayerEdges2Async();
                }

                if (cB == Globals.aPieces[25])
                {
                    await MakeTurnAsync(Globals.turnDown2);
                    await SolveTopLayerEdges2Async();
                }

                if (cB == Globals.aPieces[34])
                {
                    await MakeTurnAsync(Globals.turnDownCW);
                    await SolveTopLayerEdges2Async();
                }

                // Top color is at the middle face - turn it to the down face
                if (cB == Globals.aPieces[3])
                {
                    await MakeTurnAsync(Globals.turnLeftCW);
                    await MakeTurnAsync(Globals.turnDownCCW);
                    await MakeTurnAsync(Globals.turnLeftCCW);
                }

                if (cB == Globals.aPieces[5])
                {
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnDownCCW);
                    await MakeTurnAsync(Globals.turnRightCW);
                }

                if (cB == Globals.aPieces[12])
                {
                    await MakeTurnAsync(Globals.turnFrontCW);
                    await MakeTurnAsync(Globals.turnDownCCW);
                    await MakeTurnAsync(Globals.turnFrontCCW);
                }

                if (cB == Globals.aPieces[14])
                {
                    await MakeTurnAsync(Globals.turnBackCCW);
                    await MakeTurnAsync(Globals.turnDownCW);
                    await MakeTurnAsync(Globals.turnBackCW);
                }

                if (cB == Globals.aPieces[21])
                {
                    await MakeTurnAsync(Globals.turnRightCW);
                    await MakeTurnAsync(Globals.turnDownCCW);
                    await MakeTurnAsync(Globals.turnRightCCW);
                }

                if (cB == Globals.aPieces[23])
                {
                    await MakeTurnAsync(Globals.turnLeftCCW);
                    await MakeTurnAsync(Globals.turnDownCW);
                    await MakeTurnAsync(Globals.turnLeftCW);
                }

                if (cB == Globals.aPieces[30])
                {
                    await MakeTurnAsync(Globals.turnBackCW);
                    await MakeTurnAsync(Globals.turnDownCCW);
                    await MakeTurnAsync(Globals.turnBackCCW);
                }

                if (cB == Globals.aPieces[32])
                {
                    await MakeTurnAsync(Globals.turnFrontCCW);
                    await MakeTurnAsync(Globals.turnDownCW);
                    await MakeTurnAsync(Globals.turnFrontCW);
                }
            }

            return true;
        }

        /// Solve the edges of the top layer part 2
        private static async Task SolveTopLayerEdges2Async()
        {
            //string cB = Globals.aPieces[40];

            if (Globals.aPieces[4] == Globals.aPieces[46])
            {
                await MakeTurnAsync(Globals.turnFrontCCW);
                await MakeTurnAsync(Globals.turnUpCCW);
                await MakeTurnAsync(Globals.turnRightCW);
                await MakeTurnAsync(Globals.turnUpCW);
            }

            if (Globals.aPieces[4] == Globals.aPieces[50])
            {
                await MakeTurnAsync(Globals.turnDownCCW);
                await MakeTurnAsync(Globals.turnFrontCCW);
                await MakeTurnAsync(Globals.turnUpCCW);
                await MakeTurnAsync(Globals.turnRightCW);
                await MakeTurnAsync(Globals.turnUpCW);
            }

            if (Globals.aPieces[4] == Globals.aPieces[52])
            {
                await MakeTurnAsync(Globals.turnDown2);
                await MakeTurnAsync(Globals.turnFrontCCW);
                await MakeTurnAsync(Globals.turnUpCCW);
                await MakeTurnAsync(Globals.turnRightCW);
                await MakeTurnAsync(Globals.turnUpCW);
            }

            if (Globals.aPieces[4] == Globals.aPieces[48])
            {
                await MakeTurnAsync(Globals.turnDownCW);
                await MakeTurnAsync(Globals.turnFrontCCW);
                await MakeTurnAsync(Globals.turnUpCCW);
                await MakeTurnAsync(Globals.turnRightCW);
                await MakeTurnAsync(Globals.turnUpCW);
            }
        }

        /// Solve the corners of the top layer
        private static async Task<bool> SolveTopLayerCornersAsync()
        {
            // Check if the center of the top layer is the same of the middle layer
            //if (Globals.aPieces[1] == Globals.aPieces[13])
            //{
            //    await MakeTurnAsync(Globals.turnUpCCW);
            //}

            //if (Globals.aPieces[1] == Globals.aPieces[31])
            //{
            //    await MakeTurnAsync(Globals.turnUpCW);
            //}

            //if (Globals.aPieces[1] == Globals.aPieces[22])
            //{
            //    await MakeTurnAsync(Globals.turnUp2);
            //}

            // Turn the cube to the right, front stays at the front
            await MakeTurnAsync(Globals.turnCubeUpToRight2);

            string cB = Globals.aPieces[49];
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("nLoopTimes top layer corners: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                //if (cB == Globals.aPieces[45] && cB == Globals.aPieces[46] && cB == Globals.aPieces[47] && cB == Globals.aPieces[48] && cB == Globals.aPieces[50] && cB == Globals.aPieces[51] && cB == Globals.aPieces[52] && cB == Globals.aPieces[53])
                if (cB == Globals.aPieces[46] && cB == Globals.aPieces[48] && cB == Globals.aPieces[50] && cB == Globals.aPieces[52])
                {
                    break;
                    if (Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[22] == Globals.aPieces[25] && Globals.aPieces[31] == Globals.aPieces[34])
                    {
                        break;
                    }
                }
                else
                {
                    //await MakeTurnAsync(Globals.turnUpCW);
                }

                // The down color (before turning the cube: Up color) is at the back face [18]
                if (cB == Globals.aPieces[18])
                {
                    if (Globals.aPieces[22] == Globals.aPieces[11] || Globals.aPieces[22] == Globals.aPieces[38])
                    {
                        if (Globals.aPieces[13] == Globals.aPieces[11] || Globals.aPieces[13] == Globals.aPieces[38])
                        {
                            await MakeTurnAsync(Globals.turnCubeFrontToLeft );
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the right face [11]
                if (cB == Globals.aPieces[11])
                {
                    if (Globals.aPieces[22] == Globals.aPieces[18] || Globals.aPieces[22] == Globals.aPieces[38])
                    {
                        if (Globals.aPieces[13] == Globals.aPieces[18] || Globals.aPieces[13] == Globals.aPieces[38])
                        {
                            await MakeTurnAsync(Globals.turnCubeFrontToLeft);
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the up face [38]
                if (cB == Globals.aPieces[38])
                {
                    if (Globals.aPieces[22] == Globals.aPieces[11] || Globals.aPieces[22] == Globals.aPieces[18])
                    {
                        if (Globals.aPieces[13] == Globals.aPieces[11] || Globals.aPieces[13] == Globals.aPieces[18])
                        {
                            await MakeTurnAsync(Globals.turnCubeFrontToLeft);
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the back face [20]
                if (cB == Globals.aPieces[20])
                {
                    if (Globals.aPieces[22] == Globals.aPieces[27] || Globals.aPieces[22] == Globals.aPieces[36])
                    {
                        if (Globals.aPieces[31] == Globals.aPieces[27] || Globals.aPieces[31] == Globals.aPieces[36])
                        {
                            await MakeTurnAsync(Globals.turnCubeFrontToRight);
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the left face [27]
                if (cB == Globals.aPieces[27])
                {
                    if (Globals.aPieces[22] == Globals.aPieces[20] || Globals.aPieces[22] == Globals.aPieces[36])
                    {
                        if (Globals.aPieces[31] == Globals.aPieces[20] || Globals.aPieces[31] == Globals.aPieces[36])
                        {
                            await MakeTurnAsync(Globals.turnCubeFrontToRight);
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the up face [36]
                if (cB == Globals.aPieces[36])
                {
                    if (Globals.aPieces[22] == Globals.aPieces[20] || Globals.aPieces[22] == Globals.aPieces[27])
                    {
                        if (Globals.aPieces[31] == Globals.aPieces[20] || Globals.aPieces[31] == Globals.aPieces[27])
                        {
                            await MakeTurnAsync(Globals.turnCubeFrontToRight);
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the front face [0]
                if (cB == Globals.aPieces[0])
                {
                    if (Globals.aPieces[4] == Globals.aPieces[29] || Globals.aPieces[4] == Globals.aPieces[42])
                    {
                        if (Globals.aPieces[31] == Globals.aPieces[29] || Globals.aPieces[31] == Globals.aPieces[42])
                        {
                            while (true)
                            {
                                if (cB == Globals.aPieces[45] && Globals.aPieces[4] == Globals.aPieces[6] && Globals.aPieces[31] == Globals.aPieces[35])
                                {
                                    break;
                                }

                                await MakeTurnAsync(Globals.turnLeftCCW);
                                await MakeTurnAsync(Globals.turnUpCCW);
                                await MakeTurnAsync(Globals.turnLeftCW);
                                await MakeTurnAsync(Globals.turnUpCW);
                            }
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the left face [29]
                if (cB == Globals.aPieces[29])
                {
                    if (Globals.aPieces[4] == Globals.aPieces[0] || Globals.aPieces[4] == Globals.aPieces[42])
                    {
                        if (Globals.aPieces[31] == Globals.aPieces[0] || Globals.aPieces[31] == Globals.aPieces[42])
                        {
                            while (true)
                            {
                                if (cB == Globals.aPieces[45] && Globals.aPieces[4] == Globals.aPieces[6] && Globals.aPieces[31] == Globals.aPieces[35])
                                {
                                    break;
                                }

                                await MakeTurnAsync(Globals.turnLeftCCW);
                                await MakeTurnAsync(Globals.turnUpCCW);
                                await MakeTurnAsync(Globals.turnLeftCW);
                                await MakeTurnAsync(Globals.turnUpCW);
                            }
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the up face [42]
                if (cB == Globals.aPieces[42])
                {
                    if (Globals.aPieces[4] == Globals.aPieces[0] || Globals.aPieces[4] == Globals.aPieces[29])
                    {
                        if (Globals.aPieces[31] == Globals.aPieces[0] || Globals.aPieces[31] == Globals.aPieces[29])
                        {
                            while (true)
                            {
                                if (cB == Globals.aPieces[45] && Globals.aPieces[4] == Globals.aPieces[6] && Globals.aPieces[31] == Globals.aPieces[35])
                                {
                                    break;
                                }

                                await MakeTurnAsync(Globals.turnLeftCCW);
                                await MakeTurnAsync(Globals.turnUpCCW);
                                await MakeTurnAsync(Globals.turnLeftCW);
                                await MakeTurnAsync(Globals.turnUpCW);
                            }
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the front face [2]
                if (cB == Globals.aPieces[2])
                {
                    if (Globals.aPieces[4] == Globals.aPieces[9] || Globals.aPieces[4] == Globals.aPieces[44])
                    {
                        if (Globals.aPieces[13] == Globals.aPieces[9] || Globals.aPieces[13] == Globals.aPieces[44])
                        {
                            while (true)
                            {
                                if (cB == Globals.aPieces[47] && Globals.aPieces[4] == Globals.aPieces[8] && Globals.aPieces[13] == Globals.aPieces[15])
                                {
                                    break;
                                }

                                await MakeTurnAsync(Globals.turnRightCW);
                                await MakeTurnAsync(Globals.turnUpCW);
                                await MakeTurnAsync(Globals.turnRightCCW);
                                await MakeTurnAsync(Globals.turnUpCCW);
                            }
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the right face [9]
                if (cB == Globals.aPieces[9])
                {
                    if (Globals.aPieces[4] == Globals.aPieces[2] || Globals.aPieces[4] == Globals.aPieces[44])
                    {
                        if (Globals.aPieces[13] == Globals.aPieces[2] || Globals.aPieces[13] == Globals.aPieces[44])
                        {
                            while (true)
                            {
                                if (cB == Globals.aPieces[47] && Globals.aPieces[4] == Globals.aPieces[8] && Globals.aPieces[13] == Globals.aPieces[15])
                                {
                                    break;
                                }

                                await MakeTurnAsync(Globals.turnRightCW);
                                await MakeTurnAsync(Globals.turnUpCW);
                                await MakeTurnAsync(Globals.turnRightCCW);
                                await MakeTurnAsync(Globals.turnUpCCW);
                            }
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the up face [44]
                if (cB == Globals.aPieces[44])
                {
                    if (Globals.aPieces[4] == Globals.aPieces[2] || Globals.aPieces[4] == Globals.aPieces[9])
                    {
                        if (Globals.aPieces[13] == Globals.aPieces[2] || Globals.aPieces[13] == Globals.aPieces[9])
                        {
                            while (true)
                            {
                                if (cB == Globals.aPieces[47] && Globals.aPieces[4] == Globals.aPieces[8] && Globals.aPieces[13] == Globals.aPieces[15])
                                {
                                    break;
                                }

                                await MakeTurnAsync(Globals.turnRightCW);
                                await MakeTurnAsync(Globals.turnUpCW);
                                await MakeTurnAsync(Globals.turnRightCCW);
                                await MakeTurnAsync(Globals.turnUpCCW);
                            }
                        }
                    }
                }

                // If there is a color like [49] at the bottom side, turn that color to the top side
                if (cB == Globals.aPieces[6])
                {
                    await MakeTurnAsync(Globals.turnCubeFrontToRight);

                    await MakeTurnAsync(Globals.turnRightCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                }

                if (cB == Globals.aPieces[8])
                {
                    await MakeTurnAsync(Globals.turnCubeFrontToLeft);

                    await MakeTurnAsync(Globals.turnLeftCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnLeftCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                }

                if (cB == Globals.aPieces[15])
                {
                    await MakeTurnAsync(Globals.turnRightCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                }

                if (cB == Globals.aPieces[17])
                {
                    await MakeTurnAsync(Globals.turnCubeFrontToLeft2);

                    await MakeTurnAsync(Globals.turnLeftCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnLeftCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                }

                if (cB == Globals.aPieces[24])
                {
                    await MakeTurnAsync(Globals.turnCubeFrontToLeft);

                    await MakeTurnAsync(Globals.turnRightCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                }

                if (cB == Globals.aPieces[26])
                {
                    await MakeTurnAsync(Globals.turnCubeFrontToRight);

                    await MakeTurnAsync(Globals.turnLeftCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnLeftCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                }

                if (cB == Globals.aPieces[33])
                {
                    await MakeTurnAsync(Globals.turnCubeFrontToLeft2);

                    await MakeTurnAsync(Globals.turnRightCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                }

                if (cB == Globals.aPieces[35])
                {
                    await MakeTurnAsync(Globals.turnLeftCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnLeftCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                }
            }

            return true;
        }

        /// Solve the middle layer - Chapter 10, page 21
        private static async Task<bool> SolveMiddleLayerAsync()
        {

            
            
            return true;
        }




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
