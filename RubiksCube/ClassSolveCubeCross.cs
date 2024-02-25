﻿// This solution is based on:
// https://www.youtube.com/watch?v=7Ron6MN45LY&t=34s
// https://ruwix.com/the-rubiks-cube/how-to-solve-the-rubiks-cube-beginners-method/#step1
// https://www.learnhowtosolvearubikscube.com/step-7-moving-the-corners-into-place
// https://www.youtube.com/@RichardSchouw
// https://www.bing.com/videos/riverview/relatedvideo?q=how+to+move+corners+on+rubik%27s&mid=D8975C707A0A2C50FCEFD8975C707A0A2C50FCEF&FORM=VIRE
// file:///C:/Sources/MAUI/RubiksCube/Miscellaneous/Manuals/RubiksCubeBeginnerInstructions.pdf
// https://www.youtube.com/watch?v=Lm9jRkikhlI
// https://www.youtube.com/watch?v=lgm7NuQGgtw&list=PLfZ_bKS9WEOA-woYuj-_y3EmQqzhRboNw&index=57&t=706s

using System.Diagnostics;
using static RubiksCube.Globals;

namespace RubiksCube
{
    internal class ClassSolveCubeCross
    {
        //// Declare variables
        private const int nLoopTimesMax = 200;

        //// Solve the cube.
        public static async Task<bool> SolveTheCubeCrossAsync()
        {
            if (!await SolveTopLayerEdgesAsync())
            {
                return false;
            }

            if (!await SolveTopLayerCornersAsync())
            {
                return false;
            }

            if (!await SolveMiddleLayerAsync())
            {
                return false;
            }

            if (!await SolveBottomLayerEdgesAsync())
            {
                return false;
            }

            if (!await SolveBottomLayerEdges2Async())
            {
                return false;
            }

            if (!await SolveBottomLayerCornersAsync())
            {
                return false;
            }

            if (!await SolveBottomLayerTumblingCornersAsync())
            {
                return false;
            }

            // Check if the cube is solved
            if (ClassColorsCube.CheckIfSolved())
            {
                return true;
            }

            return false;
        }

        /// Solve the edges of the top layer - Part 1
        private static async Task<bool> SolveTopLayerEdgesAsync()
        {
            string cB = aPieces[40];
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Cross: nLoopTimes top layer edges: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (cB == aPieces[37] && cB == aPieces[39] && cB == aPieces[41] && cB == aPieces[43])
                {
                    if (aPieces[1] == aPieces[4] && aPieces[10] == aPieces[13] && aPieces[19] == aPieces[22] && aPieces[28] == aPieces[31])
                    {
                        break;
                    }
                    else
                    {
                        await SwitchEdgeCubesTopLayerAsync();
                    }
                }

                // Top color is at the down face and the second color is at another face of the bottom layer
                if (cB == aPieces[46])
                {
                    if (aPieces[7] == aPieces[4])
                    {
                        await MakeTurnAsync(turnFront2);
                    }

                    if (aPieces[7] == aPieces[13])
                    {
                        await MakeTurnAsync(turnDownCW);
                        await MakeTurnAsync(turnRight2);
                    }

                    if (aPieces[7] == aPieces[22])
                    {
                        await MakeTurnAsync(turnDown2);
                        await MakeTurnAsync(turnBack2);
                    }

                    if (aPieces[7] == aPieces[31])
                    {
                        await MakeTurnAsync(turnDownCCW);
                        await MakeTurnAsync(turnLeft2);
                    }
                }

                if (cB == aPieces[48])
                {
                    if (aPieces[34] == aPieces[31])
                    {
                        await MakeTurnAsync(turnLeft2);
                    }

                    if (aPieces[34] == aPieces[4])
                    {
                        await MakeTurnAsync(turnDownCW);
                        await MakeTurnAsync(turnFront2);
                    }

                    if (aPieces[34] == aPieces[13])
                    {
                        await MakeTurnAsync(turnDown2);
                        await MakeTurnAsync(turnRight2);
                    }

                    if (aPieces[34] == aPieces[22])
                    {
                        await MakeTurnAsync(turnDownCCW);
                        await MakeTurnAsync(turnBack2);
                    }
                }

                if (cB == aPieces[50])
                {
                    if (aPieces[16] == aPieces[13])
                    {
                        await MakeTurnAsync(turnRight2);
                    }

                    if (aPieces[16] == aPieces[4])
                    {
                        await MakeTurnAsync(turnDownCCW);
                        await MakeTurnAsync(turnFront2);
                    }

                    if (aPieces[16] == aPieces[22])
                    {
                        await MakeTurnAsync(turnDownCW);
                        await MakeTurnAsync(turnBack2);
                    }

                    if (aPieces[16] == aPieces[31])
                    {
                        await MakeTurnAsync(turnDown2);
                        await MakeTurnAsync(turnLeft2);
                    }
                }

                if (cB == aPieces[52])
                {
                    if (aPieces[25] == aPieces[22])
                    {
                        await MakeTurnAsync(turnBack2);
                    }

                    if (aPieces[25] == aPieces[4])
                    {
                        await MakeTurnAsync(turnDown2);
                        await MakeTurnAsync(turnFront2);
                    }

                    if (aPieces[25] == aPieces[13])
                    {
                        await MakeTurnAsync(turnDownCCW);
                        await MakeTurnAsync(turnRight2);
                    }

                    if (aPieces[25] == aPieces[31])
                    {
                        await MakeTurnAsync(turnDownCW);
                        await MakeTurnAsync(turnLeft2);
                    }
                }

                // Top color is at the bottom - front / right / back / left face and the second color is at the down face
                if (cB == aPieces[7])
                {
                    await SolveTopLayerEdges2Async();
                }

                if (cB == aPieces[16])
                {
                    await MakeTurnAsync(turnCubeFrontToLeft);
                    await SolveTopLayerEdges2Async();
                }

                if (cB == aPieces[25])
                {
                    await MakeTurnAsync(turnCubeFrontToLeft2);
                    await SolveTopLayerEdges2Async();
                }

                if (cB == aPieces[34])
                {
                    await MakeTurnAsync(turnCubeFrontToRight);
                    await SolveTopLayerEdges2Async();
                }

                // Top color is at the middle face - turn it to the down face
                if (cB == aPieces[3])
                {
                    await MakeTurnAsync(turnLeftCW);
                    await MakeTurnAsync(turnDownCCW);
                    await MakeTurnAsync(turnLeftCCW);
                }

                if (cB == aPieces[5])
                {
                    await MakeTurnAsync(turnRightCCW);
                    await MakeTurnAsync(turnDownCCW);
                    await MakeTurnAsync(turnRightCW);
                }

                if (cB == aPieces[12])
                {
                    await MakeTurnAsync(turnFrontCW);
                    await MakeTurnAsync(turnDownCCW);
                    await MakeTurnAsync(turnFrontCCW);
                }

                if (cB == aPieces[14])
                {
                    await MakeTurnAsync(turnBackCCW);
                    await MakeTurnAsync(turnDownCW);
                    await MakeTurnAsync(turnBackCW);
                }

                if (cB == aPieces[21])
                {
                    await MakeTurnAsync(turnRightCW);
                    await MakeTurnAsync(turnDownCCW);
                    await MakeTurnAsync(turnRightCCW);
                }

                if (cB == aPieces[23])
                {
                    await MakeTurnAsync(turnLeftCCW);
                    await MakeTurnAsync(turnDownCW);
                    await MakeTurnAsync(turnLeftCW);
                }

                if (cB == aPieces[30])
                {
                    await MakeTurnAsync(turnBackCW);
                    await MakeTurnAsync(turnDownCCW);
                    await MakeTurnAsync(turnBackCCW);
                }

                if (cB == aPieces[32])
                {
                    await MakeTurnAsync(turnFrontCCW);
                    await MakeTurnAsync(turnDownCW);
                    await MakeTurnAsync(turnFrontCW);
                }
            }

            return true;
        }

        /// Solve the edges of the top layer - Part 2
        private static async Task SolveTopLayerEdges2Async()
        {
            if (aPieces[46] == aPieces[13])
            {
                await MakeTurnAsync(turnDownCW);
                await MakeTurnAsync(turnCubeFrontToLeft);
            }

            if (aPieces[46] == aPieces[31])
            {
                await MakeTurnAsync(turnDownCCW);
                await MakeTurnAsync(turnCubeFrontToRight);
            }

            if (aPieces[46] == aPieces[22])
            {
                await MakeTurnAsync(turnDown2);
                await MakeTurnAsync(turnCubeFrontToLeft2);
            }

            await MakeTurnAsync(turnFront2);
            await MakeTurnAsync(turnFrontCW);
            await MakeTurnAsync(turnUpCCW);
            await MakeTurnAsync(turnRightCW);
            await MakeTurnAsync(turnUpCW);
        }

        /// Solve the corners of the top layer
        private static async Task<bool> SolveTopLayerCornersAsync()
        {
            // Check if the center of the top layer is the same of the middle layer
            if (aPieces[1] == aPieces[13])
            {
                await MakeTurnAsync(turnUpCCW);
            }

            if (aPieces[1] == aPieces[31])
            {
                await MakeTurnAsync(turnUpCW);
            }

            if (aPieces[1] == aPieces[22])
            {
                await MakeTurnAsync(turnUp2);
            }

            // Turn the cube to the right, front stays at the front
            await MakeTurnAsync(turnCubeUpToRight2);

            string cB = aPieces[49];
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Cross: nLoopTimes top layer corners: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (cB == aPieces[45] && cB == aPieces[46] && cB == aPieces[47] && cB == aPieces[48] && cB == aPieces[50] && cB == aPieces[51] && cB == aPieces[52] && cB == aPieces[53])
                {
                    break;
                }

                // The down color (before turning the cube: Up color) is at the back face [18]
                if (cB == aPieces[18])
                {
                    if (aPieces[22] == aPieces[11] || aPieces[22] == aPieces[38])
                    {
                        if (aPieces[13] == aPieces[11] || aPieces[13] == aPieces[38])
                        {
                            await MakeTurnAsync(turnCubeFrontToLeft);
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the right face [11]
                if (cB == aPieces[11])
                {
                    if (aPieces[22] == aPieces[18] || aPieces[22] == aPieces[38])
                    {
                        if (aPieces[13] == aPieces[18] || aPieces[13] == aPieces[38])
                        {
                            await MakeTurnAsync(turnCubeFrontToLeft);
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the up face [38]
                if (cB == aPieces[38])
                {
                    if (aPieces[22] == aPieces[11] || aPieces[22] == aPieces[18])
                    {
                        if (aPieces[13] == aPieces[11] || aPieces[13] == aPieces[18])
                        {
                            await MakeTurnAsync(turnCubeFrontToLeft);
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the back face [20]
                if (cB == aPieces[20])
                {
                    if (aPieces[22] == aPieces[27] || aPieces[22] == aPieces[36])
                    {
                        if (aPieces[31] == aPieces[27] || aPieces[31] == aPieces[36])
                        {
                            await MakeTurnAsync(turnCubeFrontToRight);
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the left face [27]
                if (cB == aPieces[27])
                {
                    if (aPieces[22] == aPieces[20] || aPieces[22] == aPieces[36])
                    {
                        if (aPieces[31] == aPieces[20] || aPieces[31] == aPieces[36])
                        {
                            await MakeTurnAsync(turnCubeFrontToRight);
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the up face [36]
                if (cB == aPieces[36])
                {
                    if (aPieces[22] == aPieces[20] || aPieces[22] == aPieces[27])
                    {
                        if (aPieces[31] == aPieces[20] || aPieces[31] == aPieces[27])
                        {
                            await MakeTurnAsync(turnCubeFrontToRight);
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the front face [0]
                if (cB == aPieces[0])
                {
                    if (aPieces[4] == aPieces[29] || aPieces[4] == aPieces[42])
                    {
                        if (aPieces[31] == aPieces[29] || aPieces[31] == aPieces[42])
                        {
                            while (true)
                            {
                                if (cB == aPieces[45] && aPieces[4] == aPieces[6] && aPieces[31] == aPieces[35])
                                {
                                    break;
                                }

                                await MakeTurnAsync(turnLeftCCW);
                                await MakeTurnAsync(turnUpCCW);
                                await MakeTurnAsync(turnLeftCW);
                                await MakeTurnAsync(turnUpCW);
                            }
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the left face [29]
                if (cB == aPieces[29])
                {
                    if (aPieces[4] == aPieces[0] || aPieces[4] == aPieces[42])
                    {
                        if (aPieces[31] == aPieces[0] || aPieces[31] == aPieces[42])
                        {
                            while (true)
                            {
                                if (cB == aPieces[45] && aPieces[4] == aPieces[6] && aPieces[31] == aPieces[35])
                                {
                                    break;
                                }

                                await MakeTurnAsync(turnLeftCCW);
                                await MakeTurnAsync(turnUpCCW);
                                await MakeTurnAsync(turnLeftCW);
                                await MakeTurnAsync(turnUpCW);
                            }
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the up face [42]
                if (cB == aPieces[42])
                {
                    if (aPieces[4] == aPieces[0] || aPieces[4] == aPieces[29])
                    {
                        if (aPieces[31] == aPieces[0] || aPieces[31] == aPieces[29])
                        {
                            while (true)
                            {
                                if (cB == aPieces[45] && aPieces[4] == aPieces[6] && aPieces[31] == aPieces[35])
                                {
                                    break;
                                }

                                await MakeTurnAsync(turnLeftCCW);
                                await MakeTurnAsync(turnUpCCW);
                                await MakeTurnAsync(turnLeftCW);
                                await MakeTurnAsync(turnUpCW);
                            }
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the front face [2]
                if (cB == aPieces[2])
                {
                    if (aPieces[4] == aPieces[9] || aPieces[4] == aPieces[44])
                    {
                        if (aPieces[13] == aPieces[9] || aPieces[13] == aPieces[44])
                        {
                            while (true)
                            {
                                if (cB == aPieces[47] && aPieces[4] == aPieces[8] && aPieces[13] == aPieces[15])
                                {
                                    break;
                                }

                                await MakeTurnAsync(turnRightCW);
                                await MakeTurnAsync(turnUpCW);
                                await MakeTurnAsync(turnRightCCW);
                                await MakeTurnAsync(turnUpCCW);
                            }
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the right face [9]
                if (cB == aPieces[9])
                {
                    if (aPieces[4] == aPieces[2] || aPieces[4] == aPieces[44])
                    {
                        if (aPieces[13] == aPieces[2] || aPieces[13] == aPieces[44])
                        {
                            while (true)
                            {
                                if (cB == aPieces[47] && aPieces[4] == aPieces[8] && aPieces[13] == aPieces[15])
                                {
                                    break;
                                }

                                await MakeTurnAsync(turnRightCW);
                                await MakeTurnAsync(turnUpCW);
                                await MakeTurnAsync(turnRightCCW);
                                await MakeTurnAsync(turnUpCCW);
                            }
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the up face [44]
                if (cB == aPieces[44])
                {
                    if (aPieces[4] == aPieces[2] || aPieces[4] == aPieces[9])
                    {
                        if (aPieces[13] == aPieces[2] || aPieces[13] == aPieces[9])
                        {
                            while (true)
                            {
                                if (cB == aPieces[47] && aPieces[4] == aPieces[8] && aPieces[13] == aPieces[15])
                                {
                                    break;
                                }

                                await MakeTurnAsync(turnRightCW);
                                await MakeTurnAsync(turnUpCW);
                                await MakeTurnAsync(turnRightCCW);
                                await MakeTurnAsync(turnUpCCW);
                            }
                        }
                    }
                }

                // If there is a color like [49] at the bottom side, turn that color to the top side
                if (cB == aPieces[6])
                {
                    await MakeTurnAsync(turnCubeFrontToRight);

                    await MakeTurnAsync(turnRightCW);
                    await MakeTurnAsync(turnUpCW);
                    await MakeTurnAsync(turnRightCCW);
                    await MakeTurnAsync(turnUpCCW);
                }

                if (cB == aPieces[8])
                {
                    await MakeTurnAsync(turnCubeFrontToLeft);

                    await MakeTurnAsync(turnLeftCCW);
                    await MakeTurnAsync(turnUpCCW);
                    await MakeTurnAsync(turnLeftCW);
                    await MakeTurnAsync(turnUpCW);
                }

                if (cB == aPieces[15])
                {
                    await MakeTurnAsync(turnRightCW);
                    await MakeTurnAsync(turnUpCW);
                    await MakeTurnAsync(turnRightCCW);
                    await MakeTurnAsync(turnUpCCW);
                }

                if (cB == aPieces[17])
                {
                    await MakeTurnAsync(turnCubeFrontToLeft2);

                    await MakeTurnAsync(turnLeftCCW);
                    await MakeTurnAsync(turnUpCCW);
                    await MakeTurnAsync(turnLeftCW);
                    await MakeTurnAsync(turnUpCW);
                }

                if (cB == aPieces[24])
                {
                    await MakeTurnAsync(turnCubeFrontToLeft);

                    await MakeTurnAsync(turnRightCW);
                    await MakeTurnAsync(turnUpCW);
                    await MakeTurnAsync(turnRightCCW);
                    await MakeTurnAsync(turnUpCCW);
                }

                if (cB == aPieces[26])
                {
                    await MakeTurnAsync(turnCubeFrontToRight);

                    await MakeTurnAsync(turnLeftCCW);
                    await MakeTurnAsync(turnUpCCW);
                    await MakeTurnAsync(turnLeftCW);
                    await MakeTurnAsync(turnUpCW);
                }

                if (cB == aPieces[33])
                {
                    await MakeTurnAsync(turnCubeFrontToLeft2);

                    await MakeTurnAsync(turnRightCW);
                    await MakeTurnAsync(turnUpCW);
                    await MakeTurnAsync(turnRightCCW);
                    await MakeTurnAsync(turnUpCCW);
                }

                if (cB == aPieces[35])
                {
                    await MakeTurnAsync(turnLeftCCW);
                    await MakeTurnAsync(turnUpCCW);
                    await MakeTurnAsync(turnLeftCW);
                    await MakeTurnAsync(turnUpCW);
                }

                await MakeTurnAsync(turnUpCW);
            }

            return true;
        }
        
        /// Solve the middle layer - Part 1
        private static async Task<bool> SolveMiddleLayerAsync()
        {
            string cB = aPieces[40];
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Cross: nLoopTimes middle layer: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (aPieces[4] == aPieces[3] && aPieces[4] == aPieces[5] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[14] && aPieces[22] == aPieces[21] && aPieces[22] == aPieces[23] && aPieces[31] == aPieces[30] && aPieces[31] == aPieces[32])
                {
                    break;
                }

                // If an edge cube at the top layer does not have the color of the up face center cube
                if (aPieces[1] == aPieces[4] && cB != aPieces[43])
                {
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[1] == aPieces[13] && cB != aPieces[43])
                {
                    await MakeTurnAsync(turnUpCCW);
                    await MakeTurnAsync(turnCubeFrontToLeft);
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[1] == aPieces[22] && cB != aPieces[43])
                {
                    await MakeTurnAsync(turnUp2);
                    await MakeTurnAsync(turnCubeFrontToLeft2);
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[1] == aPieces[31] && cB != aPieces[43])
                {
                    await MakeTurnAsync(turnUpCW);
                    await MakeTurnAsync(turnCubeFrontToRight);
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[10] == aPieces[4] && cB != aPieces[41])
                {
                    await MakeTurnAsync(turnUpCW);
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[10] == aPieces[13] && cB != aPieces[41])
                {
                    await MakeTurnAsync(turnCubeFrontToLeft);
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[10] == aPieces[22] && cB != aPieces[41])
                {
                    await MakeTurnAsync(turnUpCCW);
                    await MakeTurnAsync(turnCubeFrontToLeft2);
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[10] == aPieces[31] && cB != aPieces[41])
                {
                    await MakeTurnAsync(turnUp2);
                    await MakeTurnAsync(turnCubeFrontToRight);
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[19] == aPieces[4] && cB != aPieces[37])
                {
                    await MakeTurnAsync(turnUp2);
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[19] == aPieces[13] && cB != aPieces[37])
                {
                    await MakeTurnAsync(turnUpCW);
                    await MakeTurnAsync(turnCubeFrontToLeft);
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[19] == aPieces[22] && cB != aPieces[37])
                {
                    await MakeTurnAsync(turnCubeFrontToLeft2);
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[19] == aPieces[31] && cB != aPieces[37])
                {
                    await MakeTurnAsync(turnUpCCW);
                    await MakeTurnAsync(turnCubeFrontToRight);
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[28] == aPieces[4] && cB != aPieces[39])
                {
                    await MakeTurnAsync(turnUpCCW);
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[28] == aPieces[13] && cB != aPieces[39])
                {
                    await MakeTurnAsync(turnUp2);
                    await MakeTurnAsync(turnCubeFrontToLeft);
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[28] == aPieces[22] && cB != aPieces[39])
                {
                    await MakeTurnAsync(turnUpCW);
                    await MakeTurnAsync(turnCubeFrontToLeft2);
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[28] == aPieces[31] && cB != aPieces[39])
                {
                    await MakeTurnAsync(turnCubeFrontToRight);
                    await SolveMiddleLayer2Async();
                }

                // Wrong orientation of the edge cubes at the middle layer
                if (aPieces[4] == aPieces[12] && aPieces[13] == aPieces[5])
                {
                    await SolveMiddleLayer3Async();
                }

                if (aPieces[13] == aPieces[21] && aPieces[22] == aPieces[14])
                {
                    await MakeTurnAsync(turnCubeFrontToLeft);
                    await SolveMiddleLayer3Async();
                }

                if (aPieces[22] == aPieces[30] && aPieces[31] == aPieces[23])
                {
                    await MakeTurnAsync(turnCubeFrontToLeft2);
                    await SolveMiddleLayer3Async();
                }

                if (aPieces[31] == aPieces[3] && aPieces[4] == aPieces[32])
                {
                    await MakeTurnAsync(turnCubeFrontToRight);
                    await SolveMiddleLayer3Async();
                }

                // If an edge cube at the top layer must switch with a cube at the middle layer - Turn the cube
                if (aPieces[13] == aPieces[10] && aPieces[22] == aPieces[41])
                {
                    await MakeTurnAsync(turnCubeFrontToLeft);
                }

                if (aPieces[22] == aPieces[19] && aPieces[31] == aPieces[37])
                {
                    await MakeTurnAsync(turnCubeFrontToLeft2);
                }

                if (aPieces[31] == aPieces[28] && aPieces[4] == aPieces[39])
                {
                    await MakeTurnAsync(turnCubeFrontToLeft2);
                }

                // If an edge cube at the top layer must switch with a cube at the middle layer - Right algorithm
                if (aPieces[4] == aPieces[1] && aPieces[13] == aPieces[43])
                {
                    await SolveMiddleLayer4RightAsync();
                }

                if (aPieces[4] == aPieces[10] && aPieces[13] == aPieces[41])
                {
                    await MakeTurnAsync(turnUpCW);
                    await SolveMiddleLayer4RightAsync();
                }

                if (aPieces[4] == aPieces[19] && aPieces[13] == aPieces[37])
                {
                    await MakeTurnAsync(turnUp2);
                    await SolveMiddleLayer4RightAsync();
                }

                if (aPieces[4] == aPieces[28] && aPieces[13] == aPieces[39])
                {
                    await MakeTurnAsync(turnUpCCW);
                    await SolveMiddleLayer4RightAsync();
                }

                // If an edge cube at the top layer must switch with a cube at the middle layer - Left algorithm
                if (aPieces[4] == aPieces[1] && aPieces[31] == aPieces[43])
                {
                    await SolveMiddleLayer4LeftAsync();
                }

                if (aPieces[4] == aPieces[10] && aPieces[31] == aPieces[41])
                {
                    await MakeTurnAsync(turnUpCW);
                    await SolveMiddleLayer4LeftAsync();
                }

                if (aPieces[4] == aPieces[19] && aPieces[31] == aPieces[37])
                {
                    await MakeTurnAsync(turnUp2);
                    await SolveMiddleLayer4LeftAsync();
                }

                if (aPieces[4] == aPieces[28] && aPieces[31] == aPieces[39])
                {
                    await MakeTurnAsync(turnUpCCW);
                    await SolveMiddleLayer4LeftAsync();
                }
            }
            
            return true;
        }

        /// Solve the middle layer - Part 2
        /// If an edge cube at the top layer does not have the color of the up face center cube
        private static async Task SolveMiddleLayer2Async()
        {
            if (aPieces[43] == aPieces[13])
            {
                await MakeTurnAsync(turnUpCW);
                await MakeTurnAsync(turnRightCW);
                await MakeTurnAsync(turnUpCW);
                await MakeTurnAsync(turnRightCCW);
                await MakeTurnAsync(turnUpCCW);
                await MakeTurnAsync(turnCubeFrontToLeft);
                await MakeTurnAsync(turnLeftCCW);
                await MakeTurnAsync(turnUpCCW);
                await MakeTurnAsync(turnLeftCW);
                await MakeTurnAsync(turnUpCW);
            }

            if (aPieces[43] == aPieces[28])
            {
                await MakeTurnAsync(turnUpCCW);
                await MakeTurnAsync(turnLeftCCW);
                await MakeTurnAsync(turnUpCCW);
                await MakeTurnAsync(turnLeftCW);
                await MakeTurnAsync(turnUpCW);
                await MakeTurnAsync(turnCubeFrontToRight);
                await MakeTurnAsync(turnRightCW);
                await MakeTurnAsync(turnUpCW);
                await MakeTurnAsync(turnRightCCW);
                await MakeTurnAsync(turnUpCCW);
            }
        }

        /// Solve the middle layer - Part 3
        /// Wrong orientation of the edge cubes at the middle layer
        private static async Task SolveMiddleLayer3Async()
        {
            await MakeTurnAsync(turnUpCW);
            await MakeTurnAsync(turnRightCW);
            await MakeTurnAsync(turnUpCCW);
            await MakeTurnAsync(turnRightCCW);
            await MakeTurnAsync(turnUpCCW);
            await MakeTurnAsync(turnFrontCCW);
            await MakeTurnAsync(turnUpCW);
            await MakeTurnAsync(turnFrontCW);
            await MakeTurnAsync(turnUp2);
            await MakeTurnAsync(turnUpCW);
            await MakeTurnAsync(turnRightCW);
            await MakeTurnAsync(turnUpCCW);
            await MakeTurnAsync(turnRightCCW);
            await MakeTurnAsync(turnUpCCW);
            await MakeTurnAsync(turnFrontCCW);
            await MakeTurnAsync(turnUpCW);
            await MakeTurnAsync(turnFrontCW);
        }

        /// Solve the middle layer - Part 4
        /// If an edge cube at the top layer must switch with a cube at the middle layer - Right algorithm
        private static async Task SolveMiddleLayer4RightAsync()
        {
            await MakeTurnAsync(turnUpCW);
            await MakeTurnAsync(turnRightCW);
            await MakeTurnAsync(turnUpCCW);
            await MakeTurnAsync(turnRightCCW);
            await MakeTurnAsync(turnUpCCW);
            await MakeTurnAsync(turnFrontCCW);
            await MakeTurnAsync(turnUpCW);
            await MakeTurnAsync(turnFrontCW);
        }

        /// Solve the middle layer - Part 4
        /// If an edge cube at the top layer must switch with a cube at the middle layer - Left algorithm
        private static async Task SolveMiddleLayer4LeftAsync()
        {
            await MakeTurnAsync(turnUpCCW);
            await MakeTurnAsync(turnLeftCCW);
            await MakeTurnAsync(turnUpCW);
            await MakeTurnAsync(turnLeftCW);
            await MakeTurnAsync(turnUpCW);
            await MakeTurnAsync(turnFrontCW);
            await MakeTurnAsync(turnUpCCW);
            await MakeTurnAsync(turnFrontCCW);
        }

        /// Solve the bottom layer
        /// Make a cross
        private static async Task<bool> SolveBottomLayerEdgesAsync()
        {
            string cB = aPieces[40];
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Cross: nLoopTimes bottom layer edges: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (cB == aPieces[37] && cB == aPieces[39] && cB == aPieces[41] && cB == aPieces[43])
                {
                    break;
                }

                if (cB != aPieces[1] && cB == aPieces[10])
                {
                    await MakeTurnAsync(turnUpCW);
                }

                if (cB != aPieces[1] && cB == aPieces[19])
                {
                    await MakeTurnAsync(turnUp2);
                }

                if (cB != aPieces[1] && cB == aPieces[28])
                {
                    await MakeTurnAsync(turnUpCCW);
                }

                if (cB == aPieces[1])
                {
                    await MakeTurnAsync(turnFrontCW);
                    await MakeTurnAsync(turnRightCW);
                    await MakeTurnAsync(turnUpCW);
                    await MakeTurnAsync(turnRightCCW);
                    await MakeTurnAsync(turnUpCCW);
                    await MakeTurnAsync(turnFrontCCW);
                }

                if (cB == aPieces[41] && cB == aPieces[43])
                {
                    await MakeTurnAsync(turnUp2);
                }

                if (cB == aPieces[37] && cB == aPieces[41])
                {
                    await MakeTurnAsync(turnUpCCW);
                }

                if (cB == aPieces[39] && cB == aPieces[43])
                {
                    await MakeTurnAsync(turnUpCW);
                }

                if (cB == aPieces[37] && cB == aPieces[39])
                {
                    await MakeTurnAsync(turnFrontCW);
                    await MakeTurnAsync(turnRightCW);
                    await MakeTurnAsync(turnUpCW);
                    await MakeTurnAsync(turnRightCCW);
                    await MakeTurnAsync(turnUpCCW);
                    await MakeTurnAsync(turnFrontCCW);
                }

                if (cB == aPieces[37] && cB == aPieces[43])
                {
                    await MakeTurnAsync(turnUpCW);
                }

                if (cB == aPieces[39] && cB == aPieces[41])
                {
                    await MakeTurnAsync(turnFrontCW);
                    await MakeTurnAsync(turnRightCW);
                    await MakeTurnAsync(turnUpCW);
                    await MakeTurnAsync(turnRightCCW);
                    await MakeTurnAsync(turnUpCCW);
                    await MakeTurnAsync(turnFrontCCW);
                }

            }

            return true;
        }

        /// Solve the bottom layer
        /// Put the edges on the correct place
        private static async Task<bool> SolveBottomLayerEdges2Async()
        {
            string cB = aPieces[40];
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Cross: nLoopTimes bottom layer edges 2: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (cB == aPieces[37] && cB == aPieces[39] && cB == aPieces[41] && cB == aPieces[43])
                {
                    if (aPieces[1] == aPieces[4] && aPieces[10] == aPieces[13] && aPieces[19] == aPieces[22] && aPieces[28] == aPieces[31])
                    {
                        break;
                    }
                }

                if (cB != aPieces[37] && cB != aPieces[39] && cB != aPieces[41] && cB != aPieces[43])
                {
                    await MakeTurnAsync(turnFrontCW);
                    await MakeTurnAsync(turnRightCW);
                    await MakeTurnAsync(turnUpCW);
                    await MakeTurnAsync(turnRightCCW);
                    await MakeTurnAsync(turnUpCCW);
                    await MakeTurnAsync(turnFrontCCW);
                }

                await SwitchEdgeCubesTopLayerAsync();
            }

            return true;
        }

        /// Solve the bottom layer
        /// Corners on their places
        private static async Task<bool> SolveBottomLayerCornersAsync()
        {
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Cross: nLoopTimes bottom layer corners: " + nLoopTimes);
                    return false;
                }

                // If all corners are on their places, break the loop
                if (await SolveBottomLayerCheckCornersInRightPlaceAsync())
                {
                    break;
                }

                // Corners on their places
                if (aPieces[4] == aPieces[2] || aPieces[4] == aPieces[9] || aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[2] || aPieces[13] == aPieces[9] || aPieces[13] == aPieces[44])
                    {
                        goto Line1010;
                    }
                }

                if (aPieces[13] == aPieces[11] || aPieces[13] == aPieces[18] || aPieces[13] == aPieces[38])
                {
                    if (aPieces[22] == aPieces[11] || aPieces[22] == aPieces[18] || aPieces[22] == aPieces[38])
                    {
                        await MakeTurnAsync(turnCubeFrontToLeft);
                        goto Line1010;
                    }
                }

                if (aPieces[22] == aPieces[20] || aPieces[22] == aPieces[27] || aPieces[22] == aPieces[36])
                {
                    if (aPieces[31] == aPieces[20] || aPieces[31] == aPieces[27] || aPieces[31] == aPieces[36])
                    {
                        await MakeTurnAsync(turnCubeFrontToLeft2);
                        goto Line1010;
                    }
                }

                if (aPieces[4] == aPieces[0] || aPieces[4] == aPieces[29] || aPieces[4] == aPieces[42])
                {
                    if (aPieces[31] == aPieces[0] || aPieces[31] == aPieces[29] || aPieces[31] == aPieces[42])
                    {
                        await MakeTurnAsync(turnCubeFrontToRight);
                        goto Line1010;
                    }
                }

                // If no corner is on its place
                await MakeTurnAsync(turnUpCW);
                await MakeTurnAsync(turnRightCW);
                await MakeTurnAsync(turnUpCCW);
                await MakeTurnAsync(turnLeftCCW);
                await MakeTurnAsync(turnUpCW);
                await MakeTurnAsync(turnRightCCW);
                await MakeTurnAsync(turnUpCCW);
                await MakeTurnAsync(turnLeftCW);

                continue;

                // If a corner is on its place
            Line1010:
                await MakeTurnAsync(turnUpCW);
                await MakeTurnAsync(turnRightCW);
                await MakeTurnAsync(turnUpCCW);
                await MakeTurnAsync(turnLeftCCW);
                await MakeTurnAsync(turnUpCW);
                await MakeTurnAsync(turnRightCCW);
                await MakeTurnAsync(turnUpCCW);
                await MakeTurnAsync(turnLeftCW);

                // If there is still one or more corners not in their place
                if (aPieces[4] == aPieces[11] || aPieces[4] == aPieces[18] || aPieces[4] == aPieces[38])
                {
                    if (aPieces[13] == aPieces[11] || aPieces[13] == aPieces[18] || aPieces[13] == aPieces[38])
                    {
                        goto Line1010;
                    }
                }

                if (aPieces[4] == aPieces[20] || aPieces[4] == aPieces[27] || aPieces[4] == aPieces[36])
                {
                    if (aPieces[13] == aPieces[20] || aPieces[13] == aPieces[27] || aPieces[13] == aPieces[36])
                    {
                        goto Line1010;
                    }
                }

                if (aPieces[4] == aPieces[0] || aPieces[4] == aPieces[29] || aPieces[4] == aPieces[42])
                {
                    if (aPieces[13] == aPieces[0] || aPieces[13] == aPieces[29] || aPieces[13] == aPieces[42])
                    {
                        goto Line1010;
                    }
                }

                if (await SolveBottomLayerCheckCornersInRightPlaceAsync())
                {
                    return true;
                }

                continue;
            }

            return true;
        }

        /// Corners on their places - Part 2
        private static async Task<bool> SolveBottomLayerCheckCornersInRightPlaceAsync()
        {
            // Check if the corners are in the right place
            // If all corners are on their places, break the loop
            bool bCorner36 = false;
            bool bCorner38 = false;
            bool bCorner42 = false;
            bool bCorner44 = false;

            if (aPieces[4] == aPieces[2] || aPieces[4] == aPieces[9] || aPieces[4] == aPieces[44])
            {
                if (aPieces[13] == aPieces[2] || aPieces[13] == aPieces[9] || aPieces[13] == aPieces[44])
                {
                    bCorner44 = true;
                }
            }

            if (aPieces[13] == aPieces[11] || aPieces[13] == aPieces[18] || aPieces[13] == aPieces[38])
            {
                if (aPieces[22] == aPieces[11] || aPieces[22] == aPieces[18] || aPieces[22] == aPieces[38])
                {
                    bCorner38 = true;
                }
            }

            if (aPieces[22] == aPieces[20] || aPieces[22] == aPieces[27] || aPieces[22] == aPieces[36])
            {
                if (aPieces[31] == aPieces[20] || aPieces[31] == aPieces[27] || aPieces[31] == aPieces[36])
                {
                    bCorner36 = true;
                }
            }

            if (aPieces[31] == aPieces[0] || aPieces[31] == aPieces[29] || aPieces[31] == aPieces[42])
            {
                if (aPieces[4] == aPieces[0] || aPieces[4] == aPieces[29] || aPieces[4] == aPieces[42])
                {
                    bCorner42 = true;
                }
            }

            return bCorner36 && bCorner38 && bCorner42 && bCorner44;
        }

        /// Solve the bottom layer
        /// Tumbling the corners
        private static async Task<bool> SolveBottomLayerTumblingCornersAsync()
        {
            int nLoopTimes = 0;

            await MakeTurnAsync(turnCubeUpToRight2);

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Cross: nLoopTimes bottom layer corners 2: " + nLoopTimes);
                    return false;
                }

                if (aPieces[4] != aPieces[8] || aPieces[4] != aPieces[15] || aPieces[4] != aPieces[47])
                {
                    if (aPieces[13] != aPieces[8] || aPieces[13] != aPieces[15] || aPieces[13] != aPieces[47])
                    {
                        // Do nothing
                    }
                }

                if (aPieces[13] != aPieces[17] || aPieces[13] != aPieces[24] || aPieces[13] != aPieces[53])
                {
                    if (aPieces[22] != aPieces[17] || aPieces[22] != aPieces[24] || aPieces[22] != aPieces[53])
                    {
                        await MakeTurnAsync(turnCubeFrontToLeft);
                    }
                }

                if (aPieces[22] != aPieces[26] || aPieces[22] != aPieces[33] || aPieces[22] != aPieces[51])
                {
                    if (aPieces[31] != aPieces[26] || aPieces[31] != aPieces[33] || aPieces[31] != aPieces[51])
                    {
                        await MakeTurnAsync(turnCubeFrontToLeft2);
                    }
                }

                if (aPieces[31] != aPieces[6] || aPieces[31] != aPieces[35] || aPieces[31] != aPieces[45])
                {
                    if (aPieces[4] != aPieces[6] || aPieces[4] != aPieces[35] || aPieces[4] != aPieces[45])
                    {
                        await MakeTurnAsync(turnCubeFrontToRight);
                    }
                }

                while (true)
                {
                    while (true)
                    {
                        if (aPieces[4] == aPieces[8] && aPieces[13] == aPieces[15] || aPieces[49] == aPieces[47])
                        {
                            break;
                        }

                        await MakeTurnAsync(turnRightCW);
                        await MakeTurnAsync(turnUpCW);
                        await MakeTurnAsync(turnRightCCW);
                        await MakeTurnAsync(turnUpCCW);
                    }

                    // Check if the cube is solved
                    if (ClassColorsCube.CheckIfSolved())
                    {
                        return true;
                    }

                    await MakeTurnAsync(turnDownCCW);
                }
            }
        }

        /// Switch the edge cubes at the top layer - Part 1
        private static async Task SwitchEdgeCubesTopLayerAsync()
        {
            string cB = aPieces[40];

            //if (cB != aPieces[37] && cB != aPieces[39] && cB != aPieces[41] && cB != aPieces[43])
            //{
            //    await MakeTurnAsync(turnFrontCW);
            //    await MakeTurnAsync(turnRightCW);
            //    await MakeTurnAsync(turnUpCW);
            //    await MakeTurnAsync(turnRightCCW);
            //    await MakeTurnAsync(turnUpCCW);
            //    await MakeTurnAsync(turnFrontCCW);
            //}

            if (cB == aPieces[37] && cB == aPieces[39] && cB == aPieces[41] && cB == aPieces[43])
            {
                if (aPieces[4] == aPieces[28] && aPieces[31] == aPieces[1])
                {
                    await SwitchEdgeCubesTopLayer2Async();
                }

                if (aPieces[4] == aPieces[10] && aPieces[13] == aPieces[1])
                {
                    await MakeTurnAsync(turnCubeFrontToLeft);
                    await SwitchEdgeCubesTopLayer2Async();
                }

                if (aPieces[13] == aPieces[19] && aPieces[22] == aPieces[10])
                {
                    await MakeTurnAsync(turnCubeFrontToLeft2);
                    await SwitchEdgeCubesTopLayer2Async();
                }

                if (aPieces[22] == aPieces[28] && aPieces[31] == aPieces[19])
                {
                    await MakeTurnAsync(turnCubeFrontToRight);
                    await SwitchEdgeCubesTopLayer2Async();
                }

                if (aPieces[13] == aPieces[28] && aPieces[31] == aPieces[10])
                {
                    await MakeTurnAsync(turnUpCW);
                    await SwitchEdgeCubesTopLayer2Async();
                    await MakeTurnAsync(turnCubeFrontToLeft2);
                    await SwitchEdgeCubesTopLayer2Async();
                }

                if (aPieces[4] == aPieces[19] && aPieces[22] == aPieces[1])
                {
                    await MakeTurnAsync(turnCubeFrontToLeft);
                    await MakeTurnAsync(turnUpCW);
                    await SwitchEdgeCubesTopLayer2Async();
                    await MakeTurnAsync(turnCubeFrontToLeft2);
                    await SwitchEdgeCubesTopLayer2Async();
                }
            }
        }

        /// Switch the edge cubes at the top layer - Part 2
        private static async Task SwitchEdgeCubesTopLayer2Async()
        {
            await MakeTurnAsync(turnRightCW);
            await MakeTurnAsync(turnUpCW);
            await MakeTurnAsync(turnRightCCW);
            await MakeTurnAsync(turnUpCW);
            await MakeTurnAsync(turnRightCW);
            await MakeTurnAsync(turnUp2);
            await MakeTurnAsync(turnRightCCW);
            await MakeTurnAsync(turnUpCW);
        }
    }
}