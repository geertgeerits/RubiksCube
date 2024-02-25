// This solution is based on:
// https://www.youtube.com/watch?v=Lm9jRkikhlI
// https://www.youtube.com/watch?v=lgm7NuQGgtw&list=PLfZ_bKS9WEOA-woYuj-_y3EmQqzhRboNw&index=57&t=706s
// file:///C:/Sources/MAUI/RubiksCube/Miscellaneous/Manuals/RubiksCubeBeginnerInstructions.pdf

using System.Diagnostics;
using static RubiksCube.Globals;

namespace RubiksCube
{
    internal class ClassSolveCubeDaisy
    {
        //// Declare variables
        private const int nLoopTimesMax = 200;

        //// Solve the cube.
        public static async Task<bool> SolveTheCubeDaisyAsync()
        {
            if (!await SolveTopLayerEdgesAsync())
            {
                return false;
            }

            if (!await SolveTopLayerEdges2Async())
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

            return true;
        }

        /// Solve the edges of the top layer - Part 1
        public static async Task<bool> SolveTopLayerEdgesAsync()
        {
            string cB = aPieces[49];
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Daisy: nLoopTimes top layer edges: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (cB == aPieces[37] && cB == aPieces[39] && cB == aPieces[41] && cB == aPieces[43])
                {
                    break;
                }

                // Move 1 -> 3
                if (aPieces[49] == aPieces[1])
                {
                    await MakeTurnWordAsync(turnFrontCCW);
                }

                if (aPieces[49] == aPieces[3])
                {
                    if (aPieces[49] == aPieces[39])
                    {
                        while (true)
                        {
                            if (aPieces[49] != aPieces[37])
                            {
                                await MakeTurnWordAsync(turnUpCCW);
                                break;
                            }

                            if (aPieces[49] != aPieces[43])
                            {
                                await MakeTurnWordAsync(turnUpCW);
                                break;
                            }

                            if (aPieces[49] != aPieces[41])
                            {
                                await MakeTurnWordAsync(turnUp2);
                                break;
                            }
                            
                            break;
                        }

                    }

                    await MakeTurnWordAsync(turnLeftCCW);
                    continue;
                }

                // Move 7 -> 5
                if (aPieces[49] == aPieces[7])
                {
                    await MakeTurnWordAsync(turnFrontCCW);
                }

                if (aPieces[49] == aPieces[5])
                {
                    if (aPieces[49] == aPieces[41])
                    {
                        while (true)
                        {
                            if (aPieces[49] != aPieces[37])
                            {
                                await MakeTurnWordAsync(turnUpCW);
                                break;
                            }

                            if (aPieces[49] != aPieces[43])
                            {
                                await MakeTurnWordAsync(turnUpCCW);
                                break;
                            }

                            if (aPieces[49] != aPieces[39])
                            {
                                await MakeTurnWordAsync(turnUp2);
                                break;
                            }

                            break;
                        }
                    }

                    await MakeTurnWordAsync(turnRightCW);
                    continue;
                }

                if (aPieces[49] == aPieces[50])
                {
                    if (aPieces[49] == aPieces[41])
                    {
                        while (true)
                        {
                            if (aPieces[49] != aPieces[37])
                            {
                                await MakeTurnWordAsync(turnUpCW);
                                break;
                            }

                            if (aPieces[49] != aPieces[43])
                            {
                                await MakeTurnWordAsync(turnUpCCW);
                                break;
                            }

                            if (aPieces[49] != aPieces[39])
                            {
                                await MakeTurnWordAsync(turnUp2);
                                break;
                            }

                            break;
                        }
                    }

                    await MakeTurnWordAsync(turnRight2);
                    continue;
                }

                if (cB == aPieces[10] || cB == aPieces[12] || cB == aPieces[14] || cB == aPieces[16] || cB == aPieces[52])
                {
                    await MakeTurnWordAsync(turnCubeFrontToLeft);
                    continue;
                }

                if (cB == aPieces[19] || cB == aPieces[21] || cB == aPieces[23] || cB == aPieces[25] || cB == aPieces[48])
                {
                    await MakeTurnWordAsync(turnCubeFrontToLeft2);
                    continue;
                }

                if (cB == aPieces[28] || cB == aPieces[30] || cB == aPieces[32] || cB == aPieces[34] || cB == aPieces[46])
                {
                    await MakeTurnWordAsync(turnCubeFrontToRight);
                    continue;
                }

                //await MakeTurnWordAsync(turnCubeFrontToLeft);
                continue;
            }

            return true;
        }

        /// Solve the edges of the top layer but turned at the bottom - Part 2
        public static async Task<bool> SolveTopLayerEdges2Async()
        {
            string cB = aPieces[49];
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Daisy: nLoopTimes top layer edges 2: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (cB == aPieces[46] && cB == aPieces[48] && cB == aPieces[50] && cB == aPieces[52])
                {
                    if (aPieces[4] == aPieces[7] && aPieces[13] == aPieces[16] && aPieces[22] == aPieces[25] && aPieces[31] == aPieces[34])
                    {
                        break;
                    }
                    else
                    {
                        //await SwitchEdgeCubesTopLayerAsync();
                    }
                }

                while (true)
                {
                    if (cB == aPieces[43] && aPieces[1] == aPieces[4])
                    {
                        await MakeTurnWordAsync(turnFront2);
                        break;
                    }

                    if (cB == aPieces[43] && aPieces[1] == aPieces[13])
                    {
                        await MakeTurnWordAsync(turnUpCCW);
                        await MakeTurnWordAsync(turnRight2);
                        break;
                    }

                    if (cB == aPieces[43] && aPieces[1] == aPieces[31])
                    {
                        await MakeTurnWordAsync(turnUpCW);
                        await MakeTurnWordAsync(turnLeft2);
                        break;
                    }

                    if (cB == aPieces[43] && aPieces[1] == aPieces[22])
                    {
                        await MakeTurnWordAsync(turnUp2);
                        await MakeTurnWordAsync(turnBack2);
                        break;
                    }

                    break;
                }

                await MakeTurnWordAsync(turnCubeFrontToLeft);
            }

            return true;
        }

        /// Solve the corners of the top layer but turned at the bottom
        private static async Task<bool> SolveTopLayerCornersAsync()
        {
            string cB = aPieces[49];
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Daisy: nLoopTimes top layer corners: " + nLoopTimes);
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
                            await MakeTurnWordAsync(turnCubeFrontToLeft);
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
                            await MakeTurnWordAsync(turnCubeFrontToLeft);
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
                            await MakeTurnWordAsync(turnCubeFrontToLeft);
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
                            await MakeTurnWordAsync(turnCubeFrontToRight);
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
                            await MakeTurnWordAsync(turnCubeFrontToRight);
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
                            await MakeTurnWordAsync(turnCubeFrontToRight);
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

                                await MakeTurnWordAsync(turnLeftCCW);
                                await MakeTurnWordAsync(turnUpCCW);
                                await MakeTurnWordAsync(turnLeftCW);
                                await MakeTurnWordAsync(turnUpCW);
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

                                await MakeTurnWordAsync(turnLeftCCW);
                                await MakeTurnWordAsync(turnUpCCW);
                                await MakeTurnWordAsync(turnLeftCW);
                                await MakeTurnWordAsync(turnUpCW);
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

                                await MakeTurnWordAsync(turnLeftCCW);
                                await MakeTurnWordAsync(turnUpCCW);
                                await MakeTurnWordAsync(turnLeftCW);
                                await MakeTurnWordAsync(turnUpCW);
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

                                await MakeTurnWordAsync(turnRightCW);
                                await MakeTurnWordAsync(turnUpCW);
                                await MakeTurnWordAsync(turnRightCCW);
                                await MakeTurnWordAsync(turnUpCCW);
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

                                await MakeTurnWordAsync(turnRightCW);
                                await MakeTurnWordAsync(turnUpCW);
                                await MakeTurnWordAsync(turnRightCCW);
                                await MakeTurnWordAsync(turnUpCCW);
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

                                await MakeTurnWordAsync(turnRightCW);
                                await MakeTurnWordAsync(turnUpCW);
                                await MakeTurnWordAsync(turnRightCCW);
                                await MakeTurnWordAsync(turnUpCCW);
                            }
                        }
                    }
                }

                // If there is a color like [49] at the bottom side, turn that color to the top side
                if (cB == aPieces[6])
                {
                    await MakeTurnWordAsync(turnCubeFrontToRight);

                    await MakeTurnWordAsync(turnRightCW);
                    await MakeTurnWordAsync(turnUpCW);
                    await MakeTurnWordAsync(turnRightCCW);
                    await MakeTurnWordAsync(turnUpCCW);
                }

                if (cB == aPieces[8])
                {
                    await MakeTurnWordAsync(turnCubeFrontToLeft);

                    await MakeTurnWordAsync(turnLeftCCW);
                    await MakeTurnWordAsync(turnUpCCW);
                    await MakeTurnWordAsync(turnLeftCW);
                    await MakeTurnWordAsync(turnUpCW);
                }

                if (cB == aPieces[15])
                {
                    await MakeTurnWordAsync(turnRightCW);
                    await MakeTurnWordAsync(turnUpCW);
                    await MakeTurnWordAsync(turnRightCCW);
                    await MakeTurnWordAsync(turnUpCCW);
                }

                if (cB == aPieces[17])
                {
                    await MakeTurnWordAsync(turnCubeFrontToLeft2);

                    await MakeTurnWordAsync(turnLeftCCW);
                    await MakeTurnWordAsync(turnUpCCW);
                    await MakeTurnWordAsync(turnLeftCW);
                    await MakeTurnWordAsync(turnUpCW);
                }

                if (cB == aPieces[24])
                {
                    await MakeTurnWordAsync(turnCubeFrontToLeft);

                    await MakeTurnWordAsync(turnRightCW);
                    await MakeTurnWordAsync(turnUpCW);
                    await MakeTurnWordAsync(turnRightCCW);
                    await MakeTurnWordAsync(turnUpCCW);
                }

                if (cB == aPieces[26])
                {
                    await MakeTurnWordAsync(turnCubeFrontToRight);

                    await MakeTurnWordAsync(turnLeftCCW);
                    await MakeTurnWordAsync(turnUpCCW);
                    await MakeTurnWordAsync(turnLeftCW);
                    await MakeTurnWordAsync(turnUpCW);
                }

                if (cB == aPieces[33])
                {
                    await MakeTurnWordAsync(turnCubeFrontToLeft2);

                    await MakeTurnWordAsync(turnRightCW);
                    await MakeTurnWordAsync(turnUpCW);
                    await MakeTurnWordAsync(turnRightCCW);
                    await MakeTurnWordAsync(turnUpCCW);
                }

                if (cB == aPieces[35])
                {
                    await MakeTurnWordAsync(turnLeftCCW);
                    await MakeTurnWordAsync(turnUpCCW);
                    await MakeTurnWordAsync(turnLeftCW);
                    await MakeTurnWordAsync(turnUpCW);
                }

                await MakeTurnWordAsync(turnUpCW);
            }

            return true;
        }

        /// Solve the middle layer
        private static async Task<bool> SolveMiddleLayerAsync()
        {
            string cB = aPieces[49];
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Daisy: nLoopTimes middle layer: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (aPieces[4] == aPieces[3] && aPieces[4] == aPieces[5] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[14] && aPieces[22] == aPieces[21] && aPieces[22] == aPieces[23] && aPieces[31] == aPieces[30] && aPieces[31] == aPieces[32])
                {
                    break;
                }

                // 1-43 has to go to the left 3-32
                if (aPieces[4] == aPieces[10] && aPieces[31] == aPieces[41])
                {
                    await MakeTurnWordAsync(turnUpCW);
                }

                if (aPieces[4] == aPieces[28] && aPieces[31] == aPieces[39])
                {
                    await MakeTurnWordAsync(turnUpCCW);
                }

                if (aPieces[4] == aPieces[19] && aPieces[31] == aPieces[37])
                {
                    await MakeTurnWordAsync(turnUp2);
                }

                if (aPieces[4] == aPieces[1] && aPieces[31] == aPieces[43])
                {
                    await MakeTurnWordAsync(turnUpCCW);
                    await MakeTurnWordAsync(turnLeftCCW);
                    await MakeTurnWordAsync(turnUpCW);
                    await MakeTurnWordAsync(turnLeftCW);
                    await MakeTurnWordAsync(turnUpCW);
                    await MakeTurnWordAsync(turnFrontCW);
                    await MakeTurnWordAsync(turnUpCCW);
                    await MakeTurnWordAsync(turnFrontCCW);
                }

                // 1-43 has to go to the right 5-12
                if (aPieces[4] == aPieces[10] && aPieces[13] == aPieces[41])
                {
                    await MakeTurnWordAsync(turnUpCW);
                }

                if (aPieces[4] == aPieces[28] && aPieces[13] == aPieces[39])
                {
                    await MakeTurnWordAsync(turnUpCCW);
                }

                if (aPieces[4] == aPieces[19] && aPieces[13] == aPieces[37])
                {
                    await MakeTurnWordAsync(turnUp2);
                }

                if (aPieces[4] == aPieces[1] && aPieces[13] == aPieces[43])
                {
                    await MakeTurnWordAsync(turnUpCW);
                    await MakeTurnWordAsync(turnRightCW);
                    await MakeTurnWordAsync(turnUpCCW);
                    await MakeTurnWordAsync(turnRightCCW);
                    await MakeTurnWordAsync(turnUpCCW);
                    await MakeTurnWordAsync(turnFrontCCW);
                    await MakeTurnWordAsync(turnUpCW);
                    await MakeTurnWordAsync(turnFrontCW);
                }

                // Edge in the right place but flipped
                if (aPieces[4] == aPieces[12] && aPieces[13] == aPieces[5])
                {
                    await MakeTurnWordAsync(turnUpCW);
                    await MakeTurnWordAsync(turnRightCW);
                    await MakeTurnWordAsync(turnUpCCW);
                    await MakeTurnWordAsync(turnRightCCW);
                    await MakeTurnWordAsync(turnUpCCW);
                    await MakeTurnWordAsync(turnFrontCCW);
                    await MakeTurnWordAsync(turnUpCW);
                    await MakeTurnWordAsync(turnFrontCW);
                }

                // Edge in the wrong place within the middle layer
                if (aPieces[4] != aPieces[12] && aPieces[13] == aPieces[5])
                {
                    await MakeTurnWordAsync(turnUpCW);
                    await MakeTurnWordAsync(turnRightCW);
                    await MakeTurnWordAsync(turnUpCCW);
                    await MakeTurnWordAsync(turnRightCCW);
                    await MakeTurnWordAsync(turnUpCCW);
                    await MakeTurnWordAsync(turnFrontCCW);
                    await MakeTurnWordAsync(turnUpCW);
                    await MakeTurnWordAsync(turnFrontCW);
                }

                await MakeTurnWordAsync(turnCubeFrontToLeft);
            }

            return true;
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
                    Debug.WriteLine("Daisy: nLoopTimes bottom layer edges: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (cB == aPieces[37] && cB == aPieces[39] && cB == aPieces[41] && cB == aPieces[43])
                {
                    break;
                }

                // Make a cross - L shape
                if (cB == aPieces[37] && cB == aPieces[41])
                {
                    await MakeTurnWordAsync(turnUpCCW);
                }

                if (cB == aPieces[39] && cB == aPieces[43])
                {
                    await MakeTurnWordAsync(turnUpCW);
                }

                if (cB == aPieces[41] && cB == aPieces[43])
                {
                    await MakeTurnWordAsync(turnUp2);
                }

                if (cB == aPieces[37] && cB == aPieces[39])
                {
                    await MakeTurnWordAsync(turnFrontCW);
                    await MakeTurnWordAsync(turnUpCW);
                    await MakeTurnWordAsync(turnRightCW);
                    await MakeTurnWordAsync(turnUpCCW);
                    await MakeTurnWordAsync(turnRightCCW);
                    await MakeTurnWordAsync(turnFrontCCW);
                    
                    break;
                }

                // Make a cross - Horizontal line
                if (cB == aPieces[37] && cB == aPieces[43])
                {
                    await MakeTurnWordAsync(turnUpCW);
                }

                if (cB == aPieces[39] && cB == aPieces[41])
                {
                    await MakeTurnWordAsync(turnFrontCW);
                    await MakeTurnWordAsync(turnRightCW);
                    await MakeTurnWordAsync(turnUpCW);
                    await MakeTurnWordAsync(turnRightCCW);
                    await MakeTurnWordAsync(turnUpCCW);
                    await MakeTurnWordAsync(turnFrontCCW);
                    
                    break;
                }

                // Make a cross - Only the upper center piece is correct
                if (cB != aPieces[36] && cB != aPieces[37] && cB != aPieces[38] && cB != aPieces[39] && cB != aPieces[41] && cB != aPieces[42] && cB != aPieces[43] && cB != aPieces[44])
                {
                    await MakeTurnWordAsync(turnFrontCW);
                    await MakeTurnWordAsync(turnRightCW);
                    await MakeTurnWordAsync(turnUpCW);
                    await MakeTurnWordAsync(turnRightCCW);
                    await MakeTurnWordAsync(turnUpCCW);
                    await MakeTurnWordAsync(turnFrontCCW);
                    await MakeTurnWordAsync(turnUp2);
                    await MakeTurnWordAsync(turnFrontCW);
                    await MakeTurnWordAsync(turnUpCW);
                    await MakeTurnWordAsync(turnRightCW);
                    await MakeTurnWordAsync(turnUpCCW);
                    await MakeTurnWordAsync(turnRightCCW);
                    await MakeTurnWordAsync(turnFrontCCW);
                    
                    break;
                }

                await MakeTurnWordAsync(turnCubeFrontToLeft);
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
                    Debug.WriteLine("Daisy: nLoopTimes bottom layer edges 2: " + nLoopTimes);
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
                    await MakeTurnWordAsync(turnFrontCW);
                    await MakeTurnWordAsync(turnRightCW);
                    await MakeTurnWordAsync(turnUpCW);
                    await MakeTurnWordAsync(turnRightCCW);
                    await MakeTurnWordAsync(turnUpCCW);
                    await MakeTurnWordAsync(turnFrontCCW);
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
                    Debug.WriteLine("Daisy: nLoopTimes bottom layer corners: " + nLoopTimes);
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
                        await MakeTurnWordAsync(turnCubeFrontToLeft);
                        goto Line1010;
                    }
                }

                if (aPieces[22] == aPieces[20] || aPieces[22] == aPieces[27] || aPieces[22] == aPieces[36])
                {
                    if (aPieces[31] == aPieces[20] || aPieces[31] == aPieces[27] || aPieces[31] == aPieces[36])
                    {
                        await MakeTurnWordAsync(turnCubeFrontToLeft2);
                        goto Line1010;
                    }
                }

                if (aPieces[4] == aPieces[0] || aPieces[4] == aPieces[29] || aPieces[4] == aPieces[42])
                {
                    if (aPieces[31] == aPieces[0] || aPieces[31] == aPieces[29] || aPieces[31] == aPieces[42])
                    {
                        await MakeTurnWordAsync(turnCubeFrontToRight);
                        goto Line1010;
                    }
                }

                // If no corner is on its place
                await MakeTurnWordAsync(turnUpCW);
                await MakeTurnWordAsync(turnRightCW);
                await MakeTurnWordAsync(turnUpCCW);
                await MakeTurnWordAsync(turnLeftCCW);
                await MakeTurnWordAsync(turnUpCW);
                await MakeTurnWordAsync(turnRightCCW);
                await MakeTurnWordAsync(turnUpCCW);
                await MakeTurnWordAsync(turnLeftCW);

                continue;

            // If a corner is on its place
            Line1010:
                await MakeTurnWordAsync(turnUpCW);
                await MakeTurnWordAsync(turnRightCW);
                await MakeTurnWordAsync(turnUpCCW);
                await MakeTurnWordAsync(turnLeftCCW);
                await MakeTurnWordAsync(turnUpCW);
                await MakeTurnWordAsync(turnRightCCW);
                await MakeTurnWordAsync(turnUpCCW);
                await MakeTurnWordAsync(turnLeftCW);

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

            await MakeTurnWordAsync(turnCubeUpToRight2);

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Daisy: nLoopTimes bottom layer corners 2: " + nLoopTimes);
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
                        await MakeTurnWordAsync(turnCubeFrontToLeft);
                    }
                }

                if (aPieces[22] != aPieces[26] || aPieces[22] != aPieces[33] || aPieces[22] != aPieces[51])
                {
                    if (aPieces[31] != aPieces[26] || aPieces[31] != aPieces[33] || aPieces[31] != aPieces[51])
                    {
                        await MakeTurnWordAsync(turnCubeFrontToLeft2);
                    }
                }

                if (aPieces[31] != aPieces[6] || aPieces[31] != aPieces[35] || aPieces[31] != aPieces[45])
                {
                    if (aPieces[4] != aPieces[6] || aPieces[4] != aPieces[35] || aPieces[4] != aPieces[45])
                    {
                        await MakeTurnWordAsync(turnCubeFrontToRight);
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

                        await MakeTurnWordAsync(turnRightCW);
                        await MakeTurnWordAsync(turnUpCW);
                        await MakeTurnWordAsync(turnRightCCW);
                        await MakeTurnWordAsync(turnUpCCW);
                    }

                    // Check if the cube is solved
                    if (ClassColorsCube.CheckIfSolved())
                    {
                        return true;
                    }

                    await MakeTurnWordAsync(turnDownCCW);
                }
            }
        }

        /// Switch the edge cubes at the top layer and bottom layer - Part 1
        private static async Task SwitchEdgeCubesTopLayerAsync()
        {
            string cB = aPieces[40];

            if (cB == aPieces[37] && cB == aPieces[39] && cB == aPieces[41] && cB == aPieces[43])
            {
                if (aPieces[4] == aPieces[28] && aPieces[31] == aPieces[1])
                {
                    await SwitchEdgeCubesTopLayer2Async();
                }

                if (aPieces[4] == aPieces[10] && aPieces[13] == aPieces[1])
                {
                    await MakeTurnWordAsync(turnCubeFrontToLeft);
                    await SwitchEdgeCubesTopLayer2Async();
                }

                if (aPieces[13] == aPieces[19] && aPieces[22] == aPieces[10])
                {
                    await MakeTurnWordAsync(turnCubeFrontToLeft2);
                    await SwitchEdgeCubesTopLayer2Async();
                }

                if (aPieces[22] == aPieces[28] && aPieces[31] == aPieces[19])
                {
                    await MakeTurnWordAsync(turnCubeFrontToRight);
                    await SwitchEdgeCubesTopLayer2Async();
                }

                if (aPieces[13] == aPieces[28] && aPieces[31] == aPieces[10])
                {
                    await MakeTurnWordAsync(turnUpCW);
                    await SwitchEdgeCubesTopLayer2Async();
                    await MakeTurnWordAsync(turnCubeFrontToLeft2);
                    await SwitchEdgeCubesTopLayer2Async();
                }

                if (aPieces[4] == aPieces[19] && aPieces[22] == aPieces[1])
                {
                    await MakeTurnWordAsync(turnCubeFrontToLeft);
                    await MakeTurnWordAsync(turnUpCW);
                    await SwitchEdgeCubesTopLayer2Async();
                    await MakeTurnWordAsync(turnCubeFrontToLeft2);
                    await SwitchEdgeCubesTopLayer2Async();
                }
            }
        }

        /// Switch the edge cubes at the top layer - Part 2
        private static async Task SwitchEdgeCubesTopLayer2Async()
        {
            await MakeTurnWordAsync(turnRightCW);
            await MakeTurnWordAsync(turnUpCW);
            await MakeTurnWordAsync(turnRightCCW);
            await MakeTurnWordAsync(turnUpCW);
            await MakeTurnWordAsync(turnRightCW);
            await MakeTurnWordAsync(turnUp2);
            await MakeTurnWordAsync(turnRightCCW);
            await MakeTurnWordAsync(turnUpCW);
        }
    }
}
