﻿// This solution is based on:
// https://ruwix.com/the-rubiks-cube/notation/advanced/
// https://ruwix.com/the-rubiks-cube/advanced-cfop-fridrich/
// https://ruwix.com/the-rubiks-cube/advanced-cfop-fridrich/white-cross/

using System.Diagnostics;

namespace RubiksCube
{
    internal class ClassSolveCubeCFOP
    {
        //// Declare variables
        private const int nLoopTimesMax = 200;

        //// Solve the cube.
        public static async Task<bool> SolveTheCubeCFOPAsync()
        {
            if (!await ClassSolveCubeDaisy.SolveTopLayerEdgesAsync())
            {
                return false;
            }

            if (!await ClassSolveCubeDaisy.SolveTopLayerEdges2Async())
            {
                return false;
            }

            if (!await SolveFirstTwoLayersAsync())
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

            // Check if the cube is solved
            if (ClassColorsCube.CheckIfSolved())
            {
                return true;
            }

            return true;
        }

        /// Solve the first two layers (F2L)
        private static async Task<bool> SolveFirstTwoLayersAsync()
        {
            //await MakeTurnAsync(Globals.turnCubeUpToRight2);

            string cB = Globals.aPieces[49];
            string cT;
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("CFOP: nLoopTimes first two layers: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (cB == Globals.aPieces[45] && cB == Globals.aPieces[46] && cB == Globals.aPieces[47] && cB == Globals.aPieces[48] && cB == Globals.aPieces[50] && cB == Globals.aPieces[51] && cB == Globals.aPieces[52] && cB == Globals.aPieces[53])
                {
                    cT = Globals.aPieces[4];
                    if (cT == Globals.aPieces[3] && cT == Globals.aPieces[5] && cT == Globals.aPieces[6] && cT == Globals.aPieces[7] && cT == Globals.aPieces[8])
                    {
                        cT = Globals.aPieces[13];
                        if (cT == Globals.aPieces[12] && cT == Globals.aPieces[14] && cT == Globals.aPieces[15] && cT == Globals.aPieces[16] && cT == Globals.aPieces[17])
                        {
                            cT = Globals.aPieces[22];
                            if (cT == Globals.aPieces[21] && cT == Globals.aPieces[23] && cT == Globals.aPieces[24] && cT == Globals.aPieces[25] && cT == Globals.aPieces[26])
                            {
                                cT = Globals.aPieces[31];
                                if (cT == Globals.aPieces[30] && cT == Globals.aPieces[32] && cT == Globals.aPieces[33] && cT == Globals.aPieces[34] && cT == Globals.aPieces[35])
                                {
                                    break;
                                }
                            }
                        }
                    }
                }

                if (nLoopTimes > 1)
                {
                    await MakeTurnAsync(Globals.turnCubeFrontToLeft);
                }

                // Bring the corner on the top layer to the correct place on the top layer
                if (cB == Globals.aPieces[11] || cB == Globals.aPieces[18] || cB == Globals.aPieces[38])
                {
                    if (Globals.aPieces[4] == Globals.aPieces[11] || Globals.aPieces[4] == Globals.aPieces[18] || Globals.aPieces[4] == Globals.aPieces[38])
                    {
                        if (Globals.aPieces[13] == Globals.aPieces[11] || Globals.aPieces[13] == Globals.aPieces[18] || Globals.aPieces[13] == Globals.aPieces[38])
                        {
                            await MakeTurnAsync(Globals.turnUpCW);
                        }
                    }
                }

                if (cB == Globals.aPieces[0] || cB == Globals.aPieces[29] || cB == Globals.aPieces[42])
                {
                    if (Globals.aPieces[4] == Globals.aPieces[0] || Globals.aPieces[4] == Globals.aPieces[29] || Globals.aPieces[4] == Globals.aPieces[42])
                    {
                        if (Globals.aPieces[13] == Globals.aPieces[0] || Globals.aPieces[13] == Globals.aPieces[29] || Globals.aPieces[13] == Globals.aPieces[42])
                        {
                            await MakeTurnAsync(Globals.turnUpCCW);
                        }
                    }
                }

                if (cB == Globals.aPieces[20] || cB == Globals.aPieces[27] || cB == Globals.aPieces[36])
                {
                    if (Globals.aPieces[4] == Globals.aPieces[20] || Globals.aPieces[4] == Globals.aPieces[27] || Globals.aPieces[4] == Globals.aPieces[36])
                    {
                        if (Globals.aPieces[13] == Globals.aPieces[20] || Globals.aPieces[13] == Globals.aPieces[27] || Globals.aPieces[13] == Globals.aPieces[36])
                        {
                            await MakeTurnAsync(Globals.turnUp2);
                        }
                    }
                }

                if (cB == Globals.aPieces[42] || Globals.aPieces[4] == Globals.aPieces[0] || Globals.aPieces[13] == Globals.aPieces[29])
                {
                    await MakeTurnAsync(Globals.turnRightCW);
                    await MakeTurnAsync(Globals.turnUp2);
                    await MakeTurnAsync(Globals.turnRightCCW);
                }


                // 1. Easy cases
                // 1.1   R U R'
                if (cB == Globals.aPieces[9] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[2] && Globals.aPieces[4] == Globals.aPieces[37])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[44])
                    {
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCCW);

                        continue;
                    }
                }

                // 1.2   F' U' F
                if (cB == Globals.aPieces[2] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[44])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[9] && Globals.aPieces[13] == Globals.aPieces[39])
                    {
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnFrontCW);

                        continue;
                    }
                }

                // 1.3   U' F' U F
                if (cB == Globals.aPieces[9] && Globals.aPieces[4] == Globals.aPieces[1] && Globals.aPieces[4] == Globals.aPieces[2] && Globals.aPieces[4] == Globals.aPieces[7])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[43] && Globals.aPieces[13] == Globals.aPieces[44])
                    {
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCW);

                        continue;
                    }
                }

                // 1.4    U R U' R'
                if (cB == Globals.aPieces[2] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[41] && Globals.aPieces[4] == Globals.aPieces[44])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[9] && Globals.aPieces[13] == Globals.aPieces[10] && Globals.aPieces[13] == Globals.aPieces[16])
                    {
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCCW);

                        continue;
                    }
                }

                // 2. Case: Corner in bottom, edge in top layer
                // 2.1   (U R U' R') (U' F' U F)
                if (Globals.aPieces[4] == Globals.aPieces[1] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[8])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[15] && Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[43])
                    {
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCW);

                        continue;
                    }
                }

                // 2.2   (U' F' U F) (U R U' R')
                if (Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[8] && Globals.aPieces[4] == Globals.aPieces[41])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[10] && Globals.aPieces[13] == Globals.aPieces[15] && Globals.aPieces[13] == Globals.aPieces[16])
                    {
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCCW);

                        continue;
                    }
                }

                // 2.3   (F' U F) (U' F' U F)
                if (cB == Globals.aPieces[15] && Globals.aPieces[4] == Globals.aPieces[1] && Globals.aPieces[4] == Globals.aPieces[7])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[8] && Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[43])
                    {
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCW);

                        continue;
                    }
                }

                // 2.4   (R U R') (U' R U R')
                if (cB == Globals.aPieces[15] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[41])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[8] && Globals.aPieces[13] == Globals.aPieces[10] && Globals.aPieces[13] == Globals.aPieces[16])
                    {
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCCW);

                        continue;
                    }
                }

                // 2.5   (R U' R') (U R U' R')
                if (cB == Globals.aPieces[8] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[15] && Globals.aPieces[4] == Globals.aPieces[41])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[10] && Globals.aPieces[13] == Globals.aPieces[16])
                    {
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCCW);

                        continue;
                    }
                }

                // 2.6   (F' U' F) (U F' U' F)
                if (cB == Globals.aPieces[8] && Globals.aPieces[4] == Globals.aPieces[1] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[15])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[43])
                    {
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnFrontCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnFrontCW);

                        continue;
                    }
                }

                // 3. case: Corner in top, edge in middle
                // 3.1   (R U R' U') (R U R' U') (R U R')
                if (cB == Globals.aPieces[44] && Globals.aPieces[4] == Globals.aPieces[5] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[9])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[12] && Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[2])
                    {
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCCW);

                        continue;
                    }
                }

                // 3.2   (R U' R') (d R' U R)
                if (cB == Globals.aPieces[44] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[9] && Globals.aPieces[4] == Globals.aPieces[12])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[2] && Globals.aPieces[13] == Globals.aPieces[5])
                    {
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCCW);

                        // Turn d = clockwise rotation of the two bottom layers
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnCubeFrontToRight);

                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCW);

                        continue;
                    }
                }

                // 3.3   (U F' U F) (U F' U2 F)
                if (cB == Globals.aPieces[9] && Globals.aPieces[4] == Globals.aPieces[2] && Globals.aPieces[4] == Globals.aPieces[5] && Globals.aPieces[4] == Globals.aPieces[7])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[12] && Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[44])
                    {
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUp2);
                        await MakeTurnAsync(Globals.turnFrontCW);

                        continue;
                    }
                }

                // 3.4   (U F' U' F) (d' F U F')
                if (cB == Globals.aPieces[9] && Globals.aPieces[4] == Globals.aPieces[2] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[12])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[5] && Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[44])
                    {
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnFrontCW);

                        // Turn d' = counter clockwise rotation of the two bottom layers
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnCubeFrontToLeft);

                        await MakeTurnAsync(Globals.turnFrontCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCCW);

                        continue;
                    }
                }

                // 3.5   (U' R U' R') (U' R U2 R')
                if (cB == Globals.aPieces[2] && Globals.aPieces[4] == Globals.aPieces[5] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[44])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[9] && Globals.aPieces[13] == Globals.aPieces[12] && Globals.aPieces[13] == Globals.aPieces[16])
                    {
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUp2);
                        await MakeTurnAsync(Globals.turnRightCCW);

                        continue;
                    }
                }

                // 3.6   (U' R U R') (d R' U' R)
                if (cB == Globals.aPieces[2] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[12] && Globals.aPieces[4] == Globals.aPieces[44])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[5] && Globals.aPieces[13] == Globals.aPieces[9] && Globals.aPieces[13] == Globals.aPieces[16])
                    {
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCCW);

                        // Turn d = clockwise rotation of the two bottom layers
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnCubeFrontToRight);

                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);

                        continue;
                    }
                }

                // 4. case: Corner pointing outwards, edge in top layer
                // 4.1   (R U' R' U) (d R' U' R)
                if (cB == Globals.aPieces[9] && Globals.aPieces[4] == Globals.aPieces[2] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[10])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[41] && Globals.aPieces[13] == Globals.aPieces[44])
                    {
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCW);

                        // Turn d = clockwise rotation of the two bottom layers
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnCubeFrontToRight);

                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);

                        continue;
                    }
                }

                // 4.2   (F' U F U') (d' F U F')
                if (cB == Globals.aPieces[2] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[43] && Globals.aPieces[4] == Globals.aPieces[44])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[1] && Globals.aPieces[13] == Globals.aPieces[9] && Globals.aPieces[13] == Globals.aPieces[16])
                    {
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCW);
                        await MakeTurnAsync(Globals.turnUpCCW);

                        // Turn d' = counter clockwise rotation of the two bottom layers
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnCubeFrontToLeft);

                        await MakeTurnAsync(Globals.turnFrontCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCCW);

                        continue;
                    }
                }

                // 4.3   (U F' U2 F) (U F' U2 F)
                if (cB == Globals.aPieces[9] && Globals.aPieces[4] == Globals.aPieces[2] && Globals.aPieces[4] == Globals.aPieces[7])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[37] && Globals.aPieces[13] == Globals.aPieces[44])
                    {
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUp2);
                        await MakeTurnAsync(Globals.turnFrontCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUp2);
                        await MakeTurnAsync(Globals.turnFrontCW);

                        continue;
                    }
                }

                // 4.4   (U' R U2 R') (U' R U2 R')
                if (cB == Globals.aPieces[2] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[39] && Globals.aPieces[4] == Globals.aPieces[44])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[9] && Globals.aPieces[13] == Globals.aPieces[16])
                    {
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUp2);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUp2);
                        await MakeTurnAsync(Globals.turnRightCCW);

                        continue;
                    }
                }

                // 4.5   (U F' U' F) (U F' U2 F)
                if (cB == Globals.aPieces[9] && Globals.aPieces[4] == Globals.aPieces[2] && Globals.aPieces[4] == Globals.aPieces[7])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[39] && Globals.aPieces[13] == Globals.aPieces[44])
                    {
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnFrontCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUp2);
                        await MakeTurnAsync(Globals.turnFrontCW);

                        continue;
                    }
                }

                // 4.6   (U' R U R') (U' R U2 R')
                if (cB == Globals.aPieces[2] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[37] && Globals.aPieces[4] == Globals.aPieces[44])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[9] && Globals.aPieces[13] == Globals.aPieces[16])
                    {
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUp2);
                        await MakeTurnAsync(Globals.turnRightCCW);

                        continue;
                    }
                }

                // 4.7   (U' R U' R' U) (R U R')
                if (cB == Globals.aPieces[9] && Globals.aPieces[4] == Globals.aPieces[2] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[41])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[10] && Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[44])
                    {
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCCW);

                        continue;
                    }
                }

                // 4.8   (U F' U F U') (F' U' F)
                if (cB == Globals.aPieces[2] && Globals.aPieces[4] == Globals.aPieces[1] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[44])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[9] && Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[43])
                    {
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnFrontCW);

                        continue;
                    }
                }

                // 4.9   (U' R U R' U) (R U R')
                if (cB == Globals.aPieces[9] && Globals.aPieces[4] == Globals.aPieces[2] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[39])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[44])
                    {
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCCW);

                        continue;
                    }
                }

                // 4.10   (U F' U' F U') (F' U' F)
                if (cB == Globals.aPieces[2] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[44])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[9] && Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[37])
                    {
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnFrontCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnFrontCW);

                        continue;
                    }
                }

                // 4.11   (U F' U2 F U') (R U R')
                if (cB == Globals.aPieces[9] && Globals.aPieces[4] == Globals.aPieces[2] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[43])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[1] && Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[44])
                    {
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUp2);
                        await MakeTurnAsync(Globals.turnFrontCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCCW);

                        continue;
                    }
                }

                // 4.12   (U' R U2 R' U) (F' U' F)
                if (cB == Globals.aPieces[2] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[10] && Globals.aPieces[4] == Globals.aPieces[44])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[9] && Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[41])
                    {
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUp2);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnFrontCW);

                        continue;
                    }
                }

                // 5. case: Corner pointing upwards, edge in top layer
                // 5.1   (R U R' U') U' (R U R' U') (R U R')
                if (cB == Globals.aPieces[44] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[9] && Globals.aPieces[4] == Globals.aPieces[43])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[1] && Globals.aPieces[13] == Globals.aPieces[2] && Globals.aPieces[13] == Globals.aPieces[16])
                    {
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCCW);

                        continue;
                    }
                }

                // 5.2   y' (R' U' R U) U (R' U' R U) (R' U' R)
                if (cB == Globals.aPieces[44] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[9] && Globals.aPieces[4] == Globals.aPieces[10])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[2] && Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[41])
                    {
                        // y' - rotate the entire cube on U
                        await MakeTurnAsync(Globals.turnCubeFrontToRight);

                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);

                        continue;
                    }
                }

                // 5.3   (U2 R U R') (U R U' R')
                if (cB == Globals.aPieces[44] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[9] && Globals.aPieces[4] == Globals.aPieces[39])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[2] && Globals.aPieces[13] == Globals.aPieces[16])
                    {
                        await MakeTurnAsync(Globals.turnUp2);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCCW);

                        continue;
                    }
                }

                // 5.4   (U2 F' U' F) (U' F' U F)
                if (cB == Globals.aPieces[44] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[9])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[2] && Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[37])
                    {
                        await MakeTurnAsync(Globals.turnUp2);
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnFrontCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCW);

                        continue;
                    }
                }

                // 5.5   (U R U2 R') (U R U' R')
                if (cB == Globals.aPieces[44] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[9] && Globals.aPieces[4] == Globals.aPieces[37])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[2] && Globals.aPieces[13] == Globals.aPieces[16])
                    {
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUp2);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCCW);

                        continue;
                    }
                }

                // 5.6   (U' F' U2 F) (U' F' U F)
                if (cB == Globals.aPieces[44] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[9])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[2] && Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[39])
                    {
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUp2);
                        await MakeTurnAsync(Globals.turnFrontCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCW);

                        continue;
                    }
                }

                // 5.7   (R U2 R') (U' R U R')
                if (cB == Globals.aPieces[44] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[9] && Globals.aPieces[4] == Globals.aPieces[41])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[2] && Globals.aPieces[13] == Globals.aPieces[10] && Globals.aPieces[13] == Globals.aPieces[16])
                    {
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUp2);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCCW);

                        continue;
                    }
                }

                // 5.8   (F' U2 F) (U F' U' F)
                if (cB == Globals.aPieces[44] && Globals.aPieces[4] == Globals.aPieces[1] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[9])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[2] && Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[43])
                    {
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUp2);
                        await MakeTurnAsync(Globals.turnFrontCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnFrontCW);

                        continue;
                    }
                }

                // 6. case: Corner in bottom, edge in middle
                // 6.1   (R U' R' d R' U2 R) (U R' U2 R)
                if (Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[8] && Globals.aPieces[4] == Globals.aPieces[12])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[5] && Globals.aPieces[13] == Globals.aPieces[15] && Globals.aPieces[13] == Globals.aPieces[16])
                    {
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCCW);

                        // Turn d = clockwise rotation of the two bottom layers
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnCubeFrontToRight);

                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUp2);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUp2);
                        await MakeTurnAsync(Globals.turnRightCW);

                        continue;
                    }
                }
                
                // 6.2   Does not exist

                // 6.3   (R U' R' U R U2 R') (U R U' R')
                if (cB == Globals.aPieces[15] && Globals.aPieces[4] == Globals.aPieces[5] && Globals.aPieces[4] == Globals.aPieces[7])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[8] && Globals.aPieces[13] == Globals.aPieces[12] && Globals.aPieces[13] == Globals.aPieces[16])
                    {
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUp2);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCCW);

                        continue;
                    }
                }


                // 6.4   (R U' R' U' R U R') (U' R U2 R')
                if (cB == Globals.aPieces[8] && Globals.aPieces[4] == Globals.aPieces[5] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[15])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[12] && Globals.aPieces[13] == Globals.aPieces[16])
                    {
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUp2);
                        await MakeTurnAsync(Globals.turnRightCCW);

                        continue;
                    }
                }

                // 6.5   (R U R' U' R U' R') (U d R' U' R)
                if (cB == Globals.aPieces[15] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[12])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[5] && Globals.aPieces[13] == Globals.aPieces[8] && Globals.aPieces[13] == Globals.aPieces[16])
                    {
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCW);

                        // Turn d = clockwise rotation of the two bottom layers
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnCubeFrontToRight);

                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);

                        continue;
                    }
                }

                // 6.6   (R U' R' d R' U' R) (U' R' U' R)
                if (cB == Globals.aPieces[8] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[12] && Globals.aPieces[4] == Globals.aPieces[15])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[5] && Globals.aPieces[13] == Globals.aPieces[16])
                    {
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCCW);

                        // Turn d = clockwise rotation of the two bottom layers
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnCubeFrontToRight);

                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnRightCW);

                        continue;
                    }
                }
            }

            return true;
        }









        /// Make a turn of the cube/face/side
        private static async Task MakeTurnAsync(string cTurn)
        {
            // Add the turn to the list
            Globals.lCubeTurns.Add(cTurn);

            // Turn the cube/face/side
            await ClassCubeTurns.TurnFaceCubeAsync(cTurn);
        }
    }
}