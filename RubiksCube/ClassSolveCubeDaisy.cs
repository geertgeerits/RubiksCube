// This solution is based on:
// https://www.youtube.com/watch?v=Lm9jRkikhlI
// https://www.youtube.com/watch?v=lgm7NuQGgtw&list=PLfZ_bKS9WEOA-woYuj-_y3EmQqzhRboNw&index=57&t=706s

using System.Diagnostics;

namespace RubiksCube
{
    internal class ClassSolveCubeDaisy
    {
        //// Declare variables
        private const int nLoopTimesMax = 500;

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
                return true;
            }

            //if (!await SolveBottomLayerEdgesAsync())
            //{
            //    return false;
            //}

            //if (!await SolveBottomLayerEdges2Async())
            //{
            //    return false;
            //}

            //if (!await SolveBottomLayerCornersAsync())
            //{
            //    return false;
            //}

            //if (!await SolveBottomLayerTumblingCornersAsync())
            //{
            //    return false;
            //}

            //// Check if the cube is solved
            //if (ClassColorsCube.CheckIfSolved())
            //{
            //    return true;
            //}

            return true;
        }

        /// Solve the edges of the top layer - Part 1
        private static async Task<bool> SolveTopLayerEdgesAsync()
        {
            string cB = Globals.aPieces[49];
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
                    break;
                }

                // Move 1 -> 3
                if (Globals.aPieces[49] == Globals.aPieces[1])
                {
                    await MakeTurnAsync(Globals.turnFrontCCW);
                }

                if (Globals.aPieces[49] == Globals.aPieces[3])
                {
                    if (Globals.aPieces[49] == Globals.aPieces[39])
                    {
                        while (true)
                        {
                            if (Globals.aPieces[49] != Globals.aPieces[37])
                            {
                                await MakeTurnAsync(Globals.turnUpCCW);
                                break;
                            }

                            if (Globals.aPieces[49] != Globals.aPieces[43])
                            {
                                await MakeTurnAsync(Globals.turnUpCW);
                                break;
                            }

                            if (Globals.aPieces[49] != Globals.aPieces[41])
                            {
                                await MakeTurnAsync(Globals.turnUp2);
                                break;
                            }
                            
                            break;
                        }

                    }

                    await MakeTurnAsync(Globals.turnLeftCCW);
                    continue;
                }

                // Move 7 -> 5
                if (Globals.aPieces[49] == Globals.aPieces[7])
                {
                    await MakeTurnAsync(Globals.turnFrontCCW);
                }

                if (Globals.aPieces[49] == Globals.aPieces[5])
                {
                    if (Globals.aPieces[49] == Globals.aPieces[41])
                    {
                        while (true)
                        {
                            if (Globals.aPieces[49] != Globals.aPieces[37])
                            {
                                await MakeTurnAsync(Globals.turnUpCW);
                                break;
                            }

                            if (Globals.aPieces[49] != Globals.aPieces[43])
                            {
                                await MakeTurnAsync(Globals.turnUpCCW);
                                break;
                            }

                            if (Globals.aPieces[49] != Globals.aPieces[39])
                            {
                                await MakeTurnAsync(Globals.turnUp2);
                                break;
                            }

                            break;
                        }
                    }

                    await MakeTurnAsync(Globals.turnRightCW);
                    continue;
                }

                if (Globals.aPieces[49] == Globals.aPieces[50])
                {
                    if (Globals.aPieces[49] == Globals.aPieces[41])
                    {
                        while (true)
                        {
                            if (Globals.aPieces[49] != Globals.aPieces[37])
                            {
                                await MakeTurnAsync(Globals.turnUpCW);
                                break;
                            }

                            if (Globals.aPieces[49] != Globals.aPieces[43])
                            {
                                await MakeTurnAsync(Globals.turnUpCCW);
                                break;
                            }

                            if (Globals.aPieces[49] != Globals.aPieces[39])
                            {
                                await MakeTurnAsync(Globals.turnUp2);
                                break;
                            }

                            break;
                        }
                    }

                    await MakeTurnAsync(Globals.turnRight2);
                    continue;
                }

                if (cB == Globals.aPieces[10] || cB == Globals.aPieces[12] || cB == Globals.aPieces[14] || cB == Globals.aPieces[16] || cB == Globals.aPieces[52])
                {
                    await MakeTurnAsync(Globals.turnCubeFrontToLeft);
                    continue;
                }

                if (cB == Globals.aPieces[19] || cB == Globals.aPieces[21] || cB == Globals.aPieces[23] || cB == Globals.aPieces[25] || cB == Globals.aPieces[48])
                {
                    await MakeTurnAsync(Globals.turnCubeFrontToLeft2);
                    continue;
                }

                if (cB == Globals.aPieces[28] || cB == Globals.aPieces[30] || cB == Globals.aPieces[32] || cB == Globals.aPieces[34] || cB == Globals.aPieces[46])
                {
                    await MakeTurnAsync(Globals.turnCubeFrontToRight);
                    continue;
                }

                //await MakeTurnAsync(Globals.turnCubeFrontToLeft);
                continue;
            }

            return true;
        }

        /// Solve the edges of the top layer but turned at the bottom - Part 2
        private static async Task<bool> SolveTopLayerEdges2Async()
        {
            string cB = Globals.aPieces[49];
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("nLoopTimes top layer edges 2: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (cB == Globals.aPieces[46] && cB == Globals.aPieces[48] && cB == Globals.aPieces[50] && cB == Globals.aPieces[52])
                {
                    if (Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[22] == Globals.aPieces[25] && Globals.aPieces[31] == Globals.aPieces[34])
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
                    if (cB == Globals.aPieces[43] && Globals.aPieces[1] == Globals.aPieces[4])
                    {
                        await MakeTurnAsync(Globals.turnFront2);
                        break;
                    }

                    if (cB == Globals.aPieces[43] && Globals.aPieces[1] == Globals.aPieces[13])
                    {
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRight2);
                        break;
                    }

                    if (cB == Globals.aPieces[43] && Globals.aPieces[1] == Globals.aPieces[31])
                    {
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnLeft2);
                        break;
                    }

                    if (cB == Globals.aPieces[43] && Globals.aPieces[1] == Globals.aPieces[22])
                    {
                        await MakeTurnAsync(Globals.turnUp2);
                        await MakeTurnAsync(Globals.turnBack2);
                        break;
                    }

                    break;
                }

                await MakeTurnAsync(Globals.turnCubeFrontToLeft);
            }

            return true;
        }

        /// Solve the corners of the top layer but turned at the bottom
        private static async Task<bool> SolveTopLayerCornersAsync()
        {
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
                if (cB == Globals.aPieces[45] && cB == Globals.aPieces[46] && cB == Globals.aPieces[47] && cB == Globals.aPieces[48] && cB == Globals.aPieces[50] && cB == Globals.aPieces[51] && cB == Globals.aPieces[52] && cB == Globals.aPieces[53])
                {
                    break;
                }

                // The down color (before turning the cube: Up color) is at the back face [18]
                if (cB == Globals.aPieces[18])
                {
                    if (Globals.aPieces[22] == Globals.aPieces[11] || Globals.aPieces[22] == Globals.aPieces[38])
                    {
                        if (Globals.aPieces[13] == Globals.aPieces[11] || Globals.aPieces[13] == Globals.aPieces[38])
                        {
                            await MakeTurnAsync(Globals.turnCubeFrontToLeft);
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

                await MakeTurnAsync(Globals.turnUpCW);
            }

            return true;
        }

        // Solve the middle layer - Part 1
        private static async Task<bool> SolveMiddleLayerAsync()
        {
            string cB = Globals.aPieces[49];
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("nLoopTimes middle layer: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (Globals.aPieces[4] == Globals.aPieces[3] && Globals.aPieces[4] == Globals.aPieces[5] && Globals.aPieces[13] == Globals.aPieces[12] && Globals.aPieces[13] == Globals.aPieces[14] && Globals.aPieces[22] == Globals.aPieces[21] && Globals.aPieces[22] == Globals.aPieces[23] && Globals.aPieces[31] == Globals.aPieces[30] && Globals.aPieces[31] == Globals.aPieces[32])
                {
                    break;
                }

                // Edge cube is at the top layer
                if (Globals.aPieces[40] != Globals.aPieces[1] && Globals.aPieces[40] != Globals.aPieces[43])
                {
                    //if (Globals.aPieces[4] == Globals.aPieces[1])
                    //{

                    //}

                    if (Globals.aPieces[13] == Globals.aPieces[1])
                    {
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnCubeFrontToLeft);
                    }

                    if (Globals.aPieces[31] == Globals.aPieces[1])
                    {
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnCubeFrontToRight);
                    }

                    if (Globals.aPieces[22] == Globals.aPieces[1])
                    {
                        await MakeTurnAsync(Globals.turnUp2);
                        await MakeTurnAsync(Globals.turnCubeFrontToLeft2);
                    }

                    if (Globals.aPieces[31] == Globals.aPieces[43])
                    {
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnLeftCCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnLeftCW);

                        await MakeTurnAsync(Globals.turnCubeFrontToRight);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                    }
                }

                // Edge cube is at the middle layer
                if (Globals.aPieces[40] != Globals.aPieces[5] && Globals.aPieces[40] != Globals.aPieces[12])
                {
                    if (Globals.aPieces[40] == Globals.aPieces[1])
                    {
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCCW);

                        await MakeTurnAsync(Globals.turnCubeFrontToLeft);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnLeftCCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnLeftCW);
                    }
                }

                // Edge cube is at top middle layer
                //if (Globals.aPieces[4] == Globals.aPieces[1] && Globals.aPieces[13] == Globals.aPieces[43])
                //{
                
                //}

                if (Globals.aPieces[13] == Globals.aPieces[1] && Globals.aPieces[22] == Globals.aPieces[43])
                {
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnCubeFrontToLeft);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnRightCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnRightCCW);

                    await MakeTurnAsync(Globals.turnCubeFrontToLeft);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnLeftCCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnLeftCW);
                }

                // Edge cube is at top middle layer
                if (Globals.aPieces[4] == Globals.aPieces[32] && Globals.aPieces[31] == Globals.aPieces[3])
                {
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnLeftCCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnLeftCW);
                    
                    await MakeTurnAsync(Globals.turnCubeFrontToRight);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnRightCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnRightCCW);
                }

                if (Globals.aPieces[4] == Globals.aPieces[39] && Globals.aPieces[13] == Globals.aPieces[28])
                {
                    await MakeTurnAsync(Globals.turnUp2);
                    await MakeTurnAsync(Globals.turnCubeFrontToLeft);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnLeftCCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnLeftCW);
                }

                //if (Globals.aPieces[4] == Globals.aPieces[39] && Globals.aPieces[13] == Globals.aPieces[28])
                //{
                //    await MakeTurnAsync(Globals.turnCubeFrontToRight);
                //    await MakeTurnAsync(Globals.turnUpCW);
                //    await MakeTurnAsync(Globals.turnRightCW);
                //    await MakeTurnAsync(Globals.turnUpCCW);
                //    await MakeTurnAsync(Globals.turnRightCCW);
                //}

                //if (Globals.aPieces[4] == Globals.aPieces[39] && Globals.aPieces[13] == Globals.aPieces[28])
                //{
                //    await MakeTurnAsync(Globals.turnLeftCCW);
                //    await MakeTurnAsync(Globals.turnUpCW);
                //    await MakeTurnAsync(Globals.turnLeftCW);
                //}
            }

            await MakeTurnAsync(Globals.turnCubeFrontToLeft);

            return true;
        }

                ///// Solve the middle layer - Part 1
                //private static async Task<bool> SolveMiddleLayerAsync()
                //{
                //    string cB = Globals.aPieces[40];
                //    int nLoopTimes = 0;

                //    while (true)
                //    {
                //        nLoopTimes++;
                //        if (nLoopTimes > nLoopTimesMax)
                //        {
                //            Debug.WriteLine("nLoopTimes middle layer: " + nLoopTimes);
                //            return false;
                //        }

                //        // If solved, break the loop
                //        if (Globals.aPieces[4] == Globals.aPieces[3] && Globals.aPieces[4] == Globals.aPieces[5] && Globals.aPieces[13] == Globals.aPieces[12] && Globals.aPieces[13] == Globals.aPieces[14] && Globals.aPieces[22] == Globals.aPieces[21] && Globals.aPieces[22] == Globals.aPieces[23] && Globals.aPieces[31] == Globals.aPieces[30] && Globals.aPieces[31] == Globals.aPieces[32])
                //        {
                //            break;
                //        }

                //        // If an edge cube at the top layer does not have the color of the up face center cube
                //        if (Globals.aPieces[1] == Globals.aPieces[4] && cB != Globals.aPieces[43])
                //        {
                //            await SolveMiddleLayer2Async();
                //        }

                //        if (Globals.aPieces[1] == Globals.aPieces[13] && cB != Globals.aPieces[43])
                //        {
                //            await MakeTurnAsync(Globals.turnUpCCW);
                //            await MakeTurnAsync(Globals.turnCubeFrontToLeft);
                //            await SolveMiddleLayer2Async();
                //        }

                //        if (Globals.aPieces[1] == Globals.aPieces[22] && cB != Globals.aPieces[43])
                //        {
                //            await MakeTurnAsync(Globals.turnUp2);
                //            await MakeTurnAsync(Globals.turnCubeFrontToLeft2);
                //            await SolveMiddleLayer2Async();
                //        }

                //        if (Globals.aPieces[1] == Globals.aPieces[31] && cB != Globals.aPieces[43])
                //        {
                //            await MakeTurnAsync(Globals.turnUpCW);
                //            await MakeTurnAsync(Globals.turnCubeFrontToRight);
                //            await SolveMiddleLayer2Async();
                //        }

                //        if (Globals.aPieces[10] == Globals.aPieces[4] && cB != Globals.aPieces[41])
                //        {
                //            await MakeTurnAsync(Globals.turnUpCW);
                //            await SolveMiddleLayer2Async();
                //        }

                //        if (Globals.aPieces[10] == Globals.aPieces[13] && cB != Globals.aPieces[41])
                //        {
                //            await MakeTurnAsync(Globals.turnCubeFrontToLeft);
                //            await SolveMiddleLayer2Async();
                //        }

                //        if (Globals.aPieces[10] == Globals.aPieces[22] && cB != Globals.aPieces[41])
                //        {
                //            await MakeTurnAsync(Globals.turnUpCCW);
                //            await MakeTurnAsync(Globals.turnCubeFrontToLeft2);
                //            await SolveMiddleLayer2Async();
                //        }

                //        if (Globals.aPieces[10] == Globals.aPieces[31] && cB != Globals.aPieces[41])
                //        {
                //            await MakeTurnAsync(Globals.turnUp2);
                //            await MakeTurnAsync(Globals.turnCubeFrontToRight);
                //            await SolveMiddleLayer2Async();
                //        }

                //        if (Globals.aPieces[19] == Globals.aPieces[4] && cB != Globals.aPieces[37])
                //        {
                //            await MakeTurnAsync(Globals.turnUp2);
                //            await SolveMiddleLayer2Async();
                //        }

                //        if (Globals.aPieces[19] == Globals.aPieces[13] && cB != Globals.aPieces[37])
                //        {
                //            await MakeTurnAsync(Globals.turnUpCW);
                //            await MakeTurnAsync(Globals.turnCubeFrontToLeft);
                //            await SolveMiddleLayer2Async();
                //        }

                //        if (Globals.aPieces[19] == Globals.aPieces[22] && cB != Globals.aPieces[37])
                //        {
                //            await MakeTurnAsync(Globals.turnCubeFrontToLeft2);
                //            await SolveMiddleLayer2Async();
                //        }

                //        if (Globals.aPieces[19] == Globals.aPieces[31] && cB != Globals.aPieces[37])
                //        {
                //            await MakeTurnAsync(Globals.turnUpCCW);
                //            await MakeTurnAsync(Globals.turnCubeFrontToRight);
                //            await SolveMiddleLayer2Async();
                //        }

                //        if (Globals.aPieces[28] == Globals.aPieces[4] && cB != Globals.aPieces[39])
                //        {
                //            await MakeTurnAsync(Globals.turnUpCCW);
                //            await SolveMiddleLayer2Async();
                //        }

                //        if (Globals.aPieces[28] == Globals.aPieces[13] && cB != Globals.aPieces[39])
                //        {
                //            await MakeTurnAsync(Globals.turnUp2);
                //            await MakeTurnAsync(Globals.turnCubeFrontToLeft);
                //            await SolveMiddleLayer2Async();
                //        }

                //        if (Globals.aPieces[28] == Globals.aPieces[22] && cB != Globals.aPieces[39])
                //        {
                //            await MakeTurnAsync(Globals.turnUpCW);
                //            await MakeTurnAsync(Globals.turnCubeFrontToLeft2);
                //            await SolveMiddleLayer2Async();
                //        }

                //        if (Globals.aPieces[28] == Globals.aPieces[31] && cB != Globals.aPieces[39])
                //        {
                //            await MakeTurnAsync(Globals.turnCubeFrontToRight);
                //            await SolveMiddleLayer2Async();
                //        }

                //        // Wrong orientation of the edge cubes at the middle layer
                //        if (Globals.aPieces[4] == Globals.aPieces[12] && Globals.aPieces[13] == Globals.aPieces[5])
                //        {
                //            await SolveMiddleLayer3Async();
                //        }

                //        if (Globals.aPieces[13] == Globals.aPieces[21] && Globals.aPieces[22] == Globals.aPieces[14])
                //        {
                //            await MakeTurnAsync(Globals.turnCubeFrontToLeft);
                //            await SolveMiddleLayer3Async();
                //        }

                //        if (Globals.aPieces[22] == Globals.aPieces[30] && Globals.aPieces[31] == Globals.aPieces[23])
                //        {
                //            await MakeTurnAsync(Globals.turnCubeFrontToLeft2);
                //            await SolveMiddleLayer3Async();
                //        }

                //        if (Globals.aPieces[31] == Globals.aPieces[3] && Globals.aPieces[4] == Globals.aPieces[32])
                //        {
                //            await MakeTurnAsync(Globals.turnCubeFrontToRight);
                //            await SolveMiddleLayer3Async();
                //        }

                //        // If an edge cube at the top layer must switch with a cube at the middle layer - Turn the cube
                //        if (Globals.aPieces[13] == Globals.aPieces[10] && Globals.aPieces[22] == Globals.aPieces[41])
                //        {
                //            await MakeTurnAsync(Globals.turnCubeFrontToLeft);
                //        }

                //        if (Globals.aPieces[22] == Globals.aPieces[19] && Globals.aPieces[31] == Globals.aPieces[37])
                //        {
                //            await MakeTurnAsync(Globals.turnCubeFrontToLeft2);
                //        }

                //        if (Globals.aPieces[31] == Globals.aPieces[28] && Globals.aPieces[4] == Globals.aPieces[39])
                //        {
                //            await MakeTurnAsync(Globals.turnCubeFrontToLeft2);
                //        }

                //        // If an edge cube at the top layer must switch with a cube at the middle layer - Right algorithm
                //        if (Globals.aPieces[4] == Globals.aPieces[1] && Globals.aPieces[13] == Globals.aPieces[43])
                //        {
                //            await SolveMiddleLayer4RightAsync();
                //        }

                //        if (Globals.aPieces[4] == Globals.aPieces[10] && Globals.aPieces[13] == Globals.aPieces[41])
                //        {
                //            await MakeTurnAsync(Globals.turnUpCW);
                //            await SolveMiddleLayer4RightAsync();
                //        }

                //        if (Globals.aPieces[4] == Globals.aPieces[19] && Globals.aPieces[13] == Globals.aPieces[37])
                //        {
                //            await MakeTurnAsync(Globals.turnUp2);
                //            await SolveMiddleLayer4RightAsync();
                //        }

                //        if (Globals.aPieces[4] == Globals.aPieces[28] && Globals.aPieces[13] == Globals.aPieces[39])
                //        {
                //            await MakeTurnAsync(Globals.turnUpCCW);
                //            await SolveMiddleLayer4RightAsync();
                //        }

                //        // If an edge cube at the top layer must switch with a cube at the middle layer - Left algorithm
                //        if (Globals.aPieces[4] == Globals.aPieces[1] && Globals.aPieces[31] == Globals.aPieces[43])
                //        {
                //            await SolveMiddleLayer4LeftAsync();
                //        }

                //        if (Globals.aPieces[4] == Globals.aPieces[10] && Globals.aPieces[31] == Globals.aPieces[41])
                //        {
                //            await MakeTurnAsync(Globals.turnUpCW);
                //            await SolveMiddleLayer4LeftAsync();
                //        }

                //        if (Globals.aPieces[4] == Globals.aPieces[19] && Globals.aPieces[31] == Globals.aPieces[37])
                //        {
                //            await MakeTurnAsync(Globals.turnUp2);
                //            await SolveMiddleLayer4LeftAsync();
                //        }

                //        if (Globals.aPieces[4] == Globals.aPieces[28] && Globals.aPieces[31] == Globals.aPieces[39])
                //        {
                //            await MakeTurnAsync(Globals.turnUpCCW);
                //            await SolveMiddleLayer4LeftAsync();
                //        }
                //    }

                //    return true;
                //}

                ///// Solve the middle layer - Part 2
                ///// If an edge cube at the top layer does not have the color of the up face center cube
                //private static async Task SolveMiddleLayer2Async()
                //{
                //    if (Globals.aPieces[43] == Globals.aPieces[13])
                //    {
                //        await MakeTurnAsync(Globals.turnUpCW);
                //        await MakeTurnAsync(Globals.turnRightCW);
                //        await MakeTurnAsync(Globals.turnUpCW);
                //        await MakeTurnAsync(Globals.turnRightCCW);
                //        await MakeTurnAsync(Globals.turnUpCCW);
                //        await MakeTurnAsync(Globals.turnCubeFrontToLeft);
                //        await MakeTurnAsync(Globals.turnLeftCCW);
                //        await MakeTurnAsync(Globals.turnUpCCW);
                //        await MakeTurnAsync(Globals.turnLeftCW);
                //        await MakeTurnAsync(Globals.turnUpCW);
                //    }

                //    if (Globals.aPieces[43] == Globals.aPieces[28])
                //    {
                //        await MakeTurnAsync(Globals.turnUpCCW);
                //        await MakeTurnAsync(Globals.turnLeftCCW);
                //        await MakeTurnAsync(Globals.turnUpCCW);
                //        await MakeTurnAsync(Globals.turnLeftCW);
                //        await MakeTurnAsync(Globals.turnUpCW);
                //        await MakeTurnAsync(Globals.turnCubeFrontToRight);
                //        await MakeTurnAsync(Globals.turnRightCW);
                //        await MakeTurnAsync(Globals.turnUpCW);
                //        await MakeTurnAsync(Globals.turnRightCCW);
                //        await MakeTurnAsync(Globals.turnUpCCW);
                //    }
                //}

                ///// Solve the middle layer - Part 3
                ///// Wrong orientation of the edge cubes at the middle layer
                //private static async Task SolveMiddleLayer3Async()
                //{
                //    await MakeTurnAsync(Globals.turnUpCW);
                //    await MakeTurnAsync(Globals.turnRightCW);
                //    await MakeTurnAsync(Globals.turnUpCCW);
                //    await MakeTurnAsync(Globals.turnRightCCW);
                //    await MakeTurnAsync(Globals.turnUpCCW);
                //    await MakeTurnAsync(Globals.turnFrontCCW);
                //    await MakeTurnAsync(Globals.turnUpCW);
                //    await MakeTurnAsync(Globals.turnFrontCW);
                //    await MakeTurnAsync(Globals.turnUp2);
                //    await MakeTurnAsync(Globals.turnUpCW);
                //    await MakeTurnAsync(Globals.turnRightCW);
                //    await MakeTurnAsync(Globals.turnUpCCW);
                //    await MakeTurnAsync(Globals.turnRightCCW);
                //    await MakeTurnAsync(Globals.turnUpCCW);
                //    await MakeTurnAsync(Globals.turnFrontCCW);
                //    await MakeTurnAsync(Globals.turnUpCW);
                //    await MakeTurnAsync(Globals.turnFrontCW);
                //}

                ///// Solve the middle layer - Part 4
                ///// If an edge cube at the top layer must switch with a cube at the middle layer - Right algorithm
                //private static async Task SolveMiddleLayer4RightAsync()
                //{
                //    await MakeTurnAsync(Globals.turnUpCW);
                //    await MakeTurnAsync(Globals.turnRightCW);
                //    await MakeTurnAsync(Globals.turnUpCCW);
                //    await MakeTurnAsync(Globals.turnRightCCW);
                //    await MakeTurnAsync(Globals.turnUpCCW);
                //    await MakeTurnAsync(Globals.turnFrontCCW);
                //    await MakeTurnAsync(Globals.turnUpCW);
                //    await MakeTurnAsync(Globals.turnFrontCW);
                //}

                ///// Solve the middle layer - Part 4
                ///// If an edge cube at the top layer must switch with a cube at the middle layer - Left algorithm
                //private static async Task SolveMiddleLayer4LeftAsync()
                //{
                //    await MakeTurnAsync(Globals.turnUpCCW);
                //    await MakeTurnAsync(Globals.turnLeftCCW);
                //    await MakeTurnAsync(Globals.turnUpCW);
                //    await MakeTurnAsync(Globals.turnLeftCW);
                //    await MakeTurnAsync(Globals.turnUpCW);
                //    await MakeTurnAsync(Globals.turnFrontCW);
                //    await MakeTurnAsync(Globals.turnUpCCW);
                //    await MakeTurnAsync(Globals.turnFrontCCW);
                //}

                /// Solve the bottom layer
                /// Make a cross
                private static async Task<bool> SolveBottomLayerEdgesAsync()
        {
            string cB = Globals.aPieces[40];
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("nLoopTimes bottom layer edges: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (cB == Globals.aPieces[37] && cB == Globals.aPieces[39] && cB == Globals.aPieces[41] && cB == Globals.aPieces[43])
                {
                    break;
                }

                if (cB != Globals.aPieces[1] && cB == Globals.aPieces[10])
                {
                    await MakeTurnAsync(Globals.turnUpCW);
                }

                if (cB != Globals.aPieces[1] && cB == Globals.aPieces[19])
                {
                    await MakeTurnAsync(Globals.turnUp2);
                }

                if (cB != Globals.aPieces[1] && cB == Globals.aPieces[28])
                {
                    await MakeTurnAsync(Globals.turnUpCCW);
                }

                if (cB == Globals.aPieces[1])
                {
                    await MakeTurnAsync(Globals.turnFrontCW);
                    await MakeTurnAsync(Globals.turnRightCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnFrontCCW);
                }

                if (cB == Globals.aPieces[41] && cB == Globals.aPieces[43])
                {
                    await MakeTurnAsync(Globals.turnUp2);
                }

                if (cB == Globals.aPieces[37] && cB == Globals.aPieces[41])
                {
                    await MakeTurnAsync(Globals.turnUpCCW);
                }

                if (cB == Globals.aPieces[39] && cB == Globals.aPieces[43])
                {
                    await MakeTurnAsync(Globals.turnUpCW);
                }

                if (cB == Globals.aPieces[37] && cB == Globals.aPieces[39])
                {
                    await MakeTurnAsync(Globals.turnFrontCW);
                    await MakeTurnAsync(Globals.turnRightCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnFrontCCW);
                }

                if (cB == Globals.aPieces[37] && cB == Globals.aPieces[43])
                {
                    await MakeTurnAsync(Globals.turnUpCW);
                }

                if (cB == Globals.aPieces[39] && cB == Globals.aPieces[41])
                {
                    await MakeTurnAsync(Globals.turnFrontCW);
                    await MakeTurnAsync(Globals.turnRightCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnFrontCCW);
                }

            }

            return true;
        }

        /// Solve the bottom layer
        /// Put the edges on the correct place
        private static async Task<bool> SolveBottomLayerEdges2Async()
        {
            string cB = Globals.aPieces[40];
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("nLoopTimes bottom layer edges 2: " + nLoopTimes);
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

                if (cB != Globals.aPieces[37] && cB != Globals.aPieces[39] && cB != Globals.aPieces[41] && cB != Globals.aPieces[43])
                {
                    await MakeTurnAsync(Globals.turnFrontCW);
                    await MakeTurnAsync(Globals.turnRightCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnFrontCCW);
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
                    Debug.WriteLine("nLoopTimes bottom layer corners: " + nLoopTimes);
                    return false;
                }

                // If all corners are on their places, break the loop
                if (await SolveBottomLayerCheckCornersInRightPlaceAsync())
                {
                    break;
                }

                // Corners on their places
                if (Globals.aPieces[4] == Globals.aPieces[2] || Globals.aPieces[4] == Globals.aPieces[9] || Globals.aPieces[4] == Globals.aPieces[44])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[2] || Globals.aPieces[13] == Globals.aPieces[9] || Globals.aPieces[13] == Globals.aPieces[44])
                    {
                        goto Line1010;
                    }
                }

                if (Globals.aPieces[13] == Globals.aPieces[11] || Globals.aPieces[13] == Globals.aPieces[18] || Globals.aPieces[13] == Globals.aPieces[38])
                {
                    if (Globals.aPieces[22] == Globals.aPieces[11] || Globals.aPieces[22] == Globals.aPieces[18] || Globals.aPieces[22] == Globals.aPieces[38])
                    {
                        await MakeTurnAsync(Globals.turnCubeFrontToLeft);
                        goto Line1010;
                    }
                }

                if (Globals.aPieces[22] == Globals.aPieces[20] || Globals.aPieces[22] == Globals.aPieces[27] || Globals.aPieces[22] == Globals.aPieces[36])
                {
                    if (Globals.aPieces[31] == Globals.aPieces[20] || Globals.aPieces[31] == Globals.aPieces[27] || Globals.aPieces[31] == Globals.aPieces[36])
                    {
                        await MakeTurnAsync(Globals.turnCubeFrontToLeft2);
                        goto Line1010;
                    }
                }

                if (Globals.aPieces[4] == Globals.aPieces[0] || Globals.aPieces[4] == Globals.aPieces[29] || Globals.aPieces[4] == Globals.aPieces[42])
                {
                    if (Globals.aPieces[31] == Globals.aPieces[0] || Globals.aPieces[31] == Globals.aPieces[29] || Globals.aPieces[31] == Globals.aPieces[42])
                    {
                        await MakeTurnAsync(Globals.turnCubeFrontToRight);
                        goto Line1010;
                    }
                }

                // If no corner is on its place
                await MakeTurnAsync(Globals.turnUpCW);
                await MakeTurnAsync(Globals.turnRightCW);
                await MakeTurnAsync(Globals.turnUpCCW);
                await MakeTurnAsync(Globals.turnLeftCCW);
                await MakeTurnAsync(Globals.turnUpCW);
                await MakeTurnAsync(Globals.turnRightCCW);
                await MakeTurnAsync(Globals.turnUpCCW);
                await MakeTurnAsync(Globals.turnLeftCW);

                continue;

            // If a corner is on its place
            Line1010:
                await MakeTurnAsync(Globals.turnUpCW);
                await MakeTurnAsync(Globals.turnRightCW);
                await MakeTurnAsync(Globals.turnUpCCW);
                await MakeTurnAsync(Globals.turnLeftCCW);
                await MakeTurnAsync(Globals.turnUpCW);
                await MakeTurnAsync(Globals.turnRightCCW);
                await MakeTurnAsync(Globals.turnUpCCW);
                await MakeTurnAsync(Globals.turnLeftCW);

                // If there is still one or more corners not in their place
                if (Globals.aPieces[4] == Globals.aPieces[11] || Globals.aPieces[4] == Globals.aPieces[18] || Globals.aPieces[4] == Globals.aPieces[38])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[11] || Globals.aPieces[13] == Globals.aPieces[18] || Globals.aPieces[13] == Globals.aPieces[38])
                    {
                        goto Line1010;
                    }
                }

                if (Globals.aPieces[4] == Globals.aPieces[20] || Globals.aPieces[4] == Globals.aPieces[27] || Globals.aPieces[4] == Globals.aPieces[36])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[20] || Globals.aPieces[13] == Globals.aPieces[27] || Globals.aPieces[13] == Globals.aPieces[36])
                    {
                        goto Line1010;
                    }
                }

                if (Globals.aPieces[4] == Globals.aPieces[0] || Globals.aPieces[4] == Globals.aPieces[29] || Globals.aPieces[4] == Globals.aPieces[42])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[0] || Globals.aPieces[13] == Globals.aPieces[29] || Globals.aPieces[13] == Globals.aPieces[42])
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

            if (Globals.aPieces[4] == Globals.aPieces[2] || Globals.aPieces[4] == Globals.aPieces[9] || Globals.aPieces[4] == Globals.aPieces[44])
            {
                if (Globals.aPieces[13] == Globals.aPieces[2] || Globals.aPieces[13] == Globals.aPieces[9] || Globals.aPieces[13] == Globals.aPieces[44])
                {
                    bCorner44 = true;
                }
            }

            if (Globals.aPieces[13] == Globals.aPieces[11] || Globals.aPieces[13] == Globals.aPieces[18] || Globals.aPieces[13] == Globals.aPieces[38])
            {
                if (Globals.aPieces[22] == Globals.aPieces[11] || Globals.aPieces[22] == Globals.aPieces[18] || Globals.aPieces[22] == Globals.aPieces[38])
                {
                    bCorner38 = true;
                }
            }

            if (Globals.aPieces[22] == Globals.aPieces[20] || Globals.aPieces[22] == Globals.aPieces[27] || Globals.aPieces[22] == Globals.aPieces[36])
            {
                if (Globals.aPieces[31] == Globals.aPieces[20] || Globals.aPieces[31] == Globals.aPieces[27] || Globals.aPieces[31] == Globals.aPieces[36])
                {
                    bCorner36 = true;
                }
            }

            if (Globals.aPieces[31] == Globals.aPieces[0] || Globals.aPieces[31] == Globals.aPieces[29] || Globals.aPieces[31] == Globals.aPieces[42])
            {
                if (Globals.aPieces[4] == Globals.aPieces[0] || Globals.aPieces[4] == Globals.aPieces[29] || Globals.aPieces[4] == Globals.aPieces[42])
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

            await MakeTurnAsync(Globals.turnCubeUpToRight2);

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("nLoopTimes bottom layer corners 2: " + nLoopTimes);
                    return false;
                }

                if (Globals.aPieces[4] != Globals.aPieces[8] || Globals.aPieces[4] != Globals.aPieces[15] || Globals.aPieces[4] != Globals.aPieces[47])
                {
                    if (Globals.aPieces[13] != Globals.aPieces[8] || Globals.aPieces[13] != Globals.aPieces[15] || Globals.aPieces[13] != Globals.aPieces[47])
                    {
                        // Do nothing
                    }
                }

                if (Globals.aPieces[13] != Globals.aPieces[17] || Globals.aPieces[13] != Globals.aPieces[24] || Globals.aPieces[13] != Globals.aPieces[53])
                {
                    if (Globals.aPieces[22] != Globals.aPieces[17] || Globals.aPieces[22] != Globals.aPieces[24] || Globals.aPieces[22] != Globals.aPieces[53])
                    {
                        await MakeTurnAsync(Globals.turnCubeFrontToLeft);
                    }
                }

                if (Globals.aPieces[22] != Globals.aPieces[26] || Globals.aPieces[22] != Globals.aPieces[33] || Globals.aPieces[22] != Globals.aPieces[51])
                {
                    if (Globals.aPieces[31] != Globals.aPieces[26] || Globals.aPieces[31] != Globals.aPieces[33] || Globals.aPieces[31] != Globals.aPieces[51])
                    {
                        await MakeTurnAsync(Globals.turnCubeFrontToLeft2);
                    }
                }

                if (Globals.aPieces[31] != Globals.aPieces[6] || Globals.aPieces[31] != Globals.aPieces[35] || Globals.aPieces[31] != Globals.aPieces[45])
                {
                    if (Globals.aPieces[4] != Globals.aPieces[6] || Globals.aPieces[4] != Globals.aPieces[35] || Globals.aPieces[4] != Globals.aPieces[45])
                    {
                        await MakeTurnAsync(Globals.turnCubeFrontToRight);
                    }
                }

                while (true)
                {
                    while (true)
                    {
                        if (Globals.aPieces[4] == Globals.aPieces[8] && Globals.aPieces[13] == Globals.aPieces[15] || Globals.aPieces[49] == Globals.aPieces[47])
                        {
                            break;
                        }

                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                    }

                    // Check if the cube is solved
                    if (ClassColorsCube.CheckIfSolved())
                    {
                        return true;
                    }

                    await MakeTurnAsync(Globals.turnDownCCW);
                }
            }
        }

        /// Switch the edge cubes at the top layer and bottom layer - Part 1
        private static async Task SwitchEdgeCubesTopLayerAsync()
        {
            string cB = Globals.aPieces[40];

            if (cB == Globals.aPieces[37] && cB == Globals.aPieces[39] && cB == Globals.aPieces[41] && cB == Globals.aPieces[43])
            {
                if (Globals.aPieces[4] == Globals.aPieces[28] && Globals.aPieces[31] == Globals.aPieces[1])
                {
                    await SwitchEdgeCubesTopLayer2Async();
                }

                if (Globals.aPieces[4] == Globals.aPieces[10] && Globals.aPieces[13] == Globals.aPieces[1])
                {
                    await MakeTurnAsync(Globals.turnCubeFrontToLeft);
                    await SwitchEdgeCubesTopLayer2Async();
                }

                if (Globals.aPieces[13] == Globals.aPieces[19] && Globals.aPieces[22] == Globals.aPieces[10])
                {
                    await MakeTurnAsync(Globals.turnCubeFrontToLeft2);
                    await SwitchEdgeCubesTopLayer2Async();
                }

                if (Globals.aPieces[22] == Globals.aPieces[28] && Globals.aPieces[31] == Globals.aPieces[19])
                {
                    await MakeTurnAsync(Globals.turnCubeFrontToRight);
                    await SwitchEdgeCubesTopLayer2Async();
                }

                if (Globals.aPieces[13] == Globals.aPieces[28] && Globals.aPieces[31] == Globals.aPieces[10])
                {
                    await MakeTurnAsync(Globals.turnUpCW);
                    await SwitchEdgeCubesTopLayer2Async();
                    await MakeTurnAsync(Globals.turnCubeFrontToLeft2);
                    await SwitchEdgeCubesTopLayer2Async();
                }

                if (Globals.aPieces[4] == Globals.aPieces[19] && Globals.aPieces[22] == Globals.aPieces[1])
                {
                    await MakeTurnAsync(Globals.turnCubeFrontToLeft);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await SwitchEdgeCubesTopLayer2Async();
                    await MakeTurnAsync(Globals.turnCubeFrontToLeft2);
                    await SwitchEdgeCubesTopLayer2Async();
                }
            }
        }

        /// Switch the edge cubes at the top layer - Part 2
        private static async Task SwitchEdgeCubesTopLayer2Async()
        {
            await MakeTurnAsync(Globals.turnRightCW);
            await MakeTurnAsync(Globals.turnUpCW);
            await MakeTurnAsync(Globals.turnRightCCW);
            await MakeTurnAsync(Globals.turnUpCW);
            await MakeTurnAsync(Globals.turnRightCW);
            await MakeTurnAsync(Globals.turnUp2);
            await MakeTurnAsync(Globals.turnRightCCW);
            await MakeTurnAsync(Globals.turnUpCW);
        }

        /// Make a turn of the cube/face/side
        private static async Task MakeTurnAsync(string cTurnFaceAndDirection)
        {
            // Add the turn to the list
            Globals.lCubeTurns.Add(cTurnFaceAndDirection);

            // Turn the cube/face/side
            await ClassCubeTurns.TurnFaceCubeAsync(cTurnFaceAndDirection);
        }
    }
}
