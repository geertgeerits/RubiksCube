// This solution is based on:
// https://www.youtube.com/watch?v=Lm9jRkikhlI

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
                return true;
            }

            //if (!await SolveTopLayerCornersAsync())
            //{
            //    return false;
            //}

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

        /// Solve the edges of the top layer - Part 2
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
                if (cB == Globals.aPieces[37] && cB == Globals.aPieces[39] && cB == Globals.aPieces[41] && cB == Globals.aPieces[43])
                {
                    if (Globals.aPieces[1] == Globals.aPieces[4] && Globals.aPieces[10] == Globals.aPieces[13] && Globals.aPieces[19] == Globals.aPieces[22] && Globals.aPieces[28] == Globals.aPieces[31])
                    {
                        break;
                    }
                    else
                    {
                        //await SwitchEdgeCubesTopLayerAsync();
                    }
                }

            }



            //if (Globals.aPieces[46] == Globals.aPieces[13])
            //{
            //    await MakeTurnAsync(Globals.turnDownCW);
            //    await MakeTurnAsync(Globals.turnCubeFrontToLeft);
            //}

            //if (Globals.aPieces[46] == Globals.aPieces[31])
            //{
            //    await MakeTurnAsync(Globals.turnDownCCW);
            //    await MakeTurnAsync(Globals.turnCubeFrontToRight);
            //}

            //if (Globals.aPieces[46] == Globals.aPieces[22])
            //{
            //    await MakeTurnAsync(Globals.turnDown2);
            //    await MakeTurnAsync(Globals.turnCubeFrontToLeft2);
            //}

            //await MakeTurnAsync(Globals.turnFront2);
            //await MakeTurnAsync(Globals.turnFrontCW);
            //await MakeTurnAsync(Globals.turnUpCCW);
            //await MakeTurnAsync(Globals.turnRightCW);
            //await MakeTurnAsync(Globals.turnUpCW);

            return true;
        }

        /// Solve the corners of the top layer











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
