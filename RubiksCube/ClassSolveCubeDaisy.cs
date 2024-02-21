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
                return true;
            }

            //if (!await SolveMiddleLayerAsync())
            //{
            //    return false;
            //}

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

            // Check if the cube is solved
            if (ClassColorsCube.CheckIfSolved())
            {
                return true;
            }

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
