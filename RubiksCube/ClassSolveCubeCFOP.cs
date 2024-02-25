// This solution is based on:
// https://ruwix.com/the-rubiks-cube/notation/advanced/
// https://ruwix.com/the-rubiks-cube/advanced-cfop-fridrich/
// https://ruwix.com/the-rubiks-cube/advanced-cfop-fridrich/white-cross/
// https://solvethecube.com/speedcubing#betterf2l
// https://www.youtube.com/watch?v=MS5jByTX_pk

using System.Diagnostics;
using static RubiksCube.Globals;

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
            //await MakeTurnWordAsync(turnCubeUpToRight2);

            string cB = aPieces[49];
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
                if (cB == aPieces[45] && cB == aPieces[46] && cB == aPieces[47] && cB == aPieces[48] && cB == aPieces[50] && cB == aPieces[51] && cB == aPieces[52] && cB == aPieces[53])
                {
                    cT = aPieces[4];
                    if (cT == aPieces[3] && cT == aPieces[5] && cT == aPieces[6] && cT == aPieces[7] && cT == aPieces[8])
                    {
                        cT = aPieces[13];
                        if (cT == aPieces[12] && cT == aPieces[14] && cT == aPieces[15] && cT == aPieces[16] && cT == aPieces[17])
                        {
                            cT = aPieces[22];
                            if (cT == aPieces[21] && cT == aPieces[23] && cT == aPieces[24] && cT == aPieces[25] && cT == aPieces[26])
                            {
                                cT = aPieces[31];
                                if (cT == aPieces[30] && cT == aPieces[32] && cT == aPieces[33] && cT == aPieces[34] && cT == aPieces[35])
                                {
                                    break;
                                }
                            }
                        }
                    }
                }

                if (nLoopTimes > 1)
                {
                    await MakeTurnWordAsync(turnCubeFrontToLeft);
                }

                // Bring the corner on the top layer to the correct place on the top layer
                if (cB == aPieces[2] || cB == aPieces[9] || cB == aPieces[44])
                {
                    if (aPieces[4] == aPieces[2] || aPieces[4] == aPieces[9] || aPieces[4] == aPieces[44])
                    {
                        if (aPieces[13] == aPieces[2] || aPieces[13] == aPieces[9] || aPieces[13] == aPieces[44])
                        {
                            // Do nothing
                        }
                    }
                }
                else
                {
                    if (cB == aPieces[11] || cB == aPieces[18] || cB == aPieces[38])
                    {
                        if (aPieces[4] == aPieces[11] || aPieces[4] == aPieces[18] || aPieces[4] == aPieces[38])
                        {
                            if (aPieces[13] == aPieces[11] || aPieces[13] == aPieces[18] || aPieces[13] == aPieces[38])
                            {
                                await MakeTurnWordAsync(turnUpCW);
                            }
                        }
                    }

                    if (cB == aPieces[0] || cB == aPieces[29] || cB == aPieces[42])
                    {
                        if (aPieces[4] == aPieces[0] || aPieces[4] == aPieces[29] || aPieces[4] == aPieces[42])
                        {
                            if (aPieces[13] == aPieces[0] || aPieces[13] == aPieces[29] || aPieces[13] == aPieces[42])
                            {
                                await MakeTurnWordAsync(turnUpCCW);
                            }
                        }
                    }

                    if (cB == aPieces[20] || cB == aPieces[27] || cB == aPieces[36])
                    {
                        if (aPieces[4] == aPieces[20] || aPieces[4] == aPieces[27] || aPieces[4] == aPieces[36])
                        {
                            if (aPieces[13] == aPieces[20] || aPieces[13] == aPieces[27] || aPieces[13] == aPieces[36])
                            {
                                await MakeTurnWordAsync(turnUp2);
                            }
                        }
                    }
                }



                if (cB == aPieces[42] && aPieces[4] == aPieces[0] && aPieces[13] == aPieces[29])
                {
                    await MakeTurnWordAsync(turnRightCW);
                    await MakeTurnWordAsync(turnUp2);
                    await MakeTurnWordAsync(turnRightCCW);

                    continue;
                }

                if (cB == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[39] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnWordAsync(turnUpCCW);
                        await MakeTurnWordAsync(turnRightCW);
                        await MakeTurnWordAsync(turnUp2);
                        await MakeTurnWordAsync(turnRightCCW);

                        continue;
                    }
                }

                // (R U2 R') (U R U' R')
                if (cB == aPieces[42] && aPieces[4] == aPieces[0] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[10] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("R U2 R' U R U' R'");
                        continue;
                    }
                }

                // (R U' R') U (F' U' F) or (R U' R') d (R' U' R)
                if (cB == aPieces[11] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[38])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[18] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnLetterAsync("R U' R' U F' U' F");
                        continue;
                    }
                }

                // (L' U L) U (L' U' L) or y' U R U' R' U y' R' U' R
                if (cB == aPieces[11] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[8] && aPieces[4] == aPieces[39])
                {
                    if (aPieces[13] == aPieces[12] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[15] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        if (aPieces[31] == aPieces[38])
                        {
                            await MakeTurnLetterAsync("L' U L U L' U' L");
                            continue;
                        }
                    }
                }

                // 1. Easy cases
                // 1.1   R U R'
                if (cB == aPieces[9] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[37])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("R U R'");
                        continue;
                    }
                }

                // 1.2   F' U' F
                if (cB == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnLetterAsync("F' U' F");
                        continue;
                    }
                }

                // 1.3   U' F' U F
                if (cB == aPieces[9] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[43] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("U' F' U F");
                        continue;
                    }
                }

                // 1.4    U R U' R'
                if (cB == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[41] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("U R U' R'");
                        continue;
                    }
                }

                // 2. Case: Corner in bottom, edge in top layer
                // 2.1   (U R U' R') (U' F' U F)
                if (aPieces[4] == aPieces[1] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[8])
                {
                    if (aPieces[13] == aPieces[15] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnLetterAsync("U R U' R' U' F' U F");
                        continue;
                    }
                }

                // 2.2   (U' F' U F) (U R U' R')
                if (aPieces[4] == aPieces[7] && aPieces[4] == aPieces[8] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[10] && aPieces[13] == aPieces[15] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("U' F' U F U R U' R'");
                        continue;
                    }
                }

                // 2.3   (F' U F) (U' F' U F)
                if (cB == aPieces[15] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[8] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnLetterAsync("F' U F U' F' U F");
                        continue;
                    }
                }

                // 2.4   (R U R') (U' R U R')
                if (cB == aPieces[15] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[8] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("R U R' U' R U R'");
                        continue;
                    }
                }

                // 2.5   (R U' R') (U R U' R')
                if (cB == aPieces[8] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[15] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[10] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("R U' R' U R U' R'");
                        continue;
                    }
                }

                // 2.6   (F' U' F) (U F' U' F)
                if (cB == aPieces[8] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[15])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnLetterAsync("F' U' F U F' U' F");
                        continue;
                    }
                }

                // 3. case: Corner in top, edge in middle
                // 3.1   (R U R' U') (R U R' U') (R U R')
                if (cB == aPieces[44] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("R U R' U' R U R' U' R U R'");
                        continue;
                    }
                }

                // 3.2   (R U' R') (d R' U R)
                if (cB == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[12])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[5] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnWordAsync(turnRightCW);
                        await MakeTurnWordAsync(turnUpCCW);
                        await MakeTurnWordAsync(turnRightCCW);

                        // Turn d = clockwise rotation of the two bottom layers
                        await MakeTurnWordAsync(turnUpCW);
                        await MakeTurnWordAsync(turnCubeFrontToRight);

                        await MakeTurnWordAsync(turnRightCCW);
                        await MakeTurnWordAsync(turnUpCW);
                        await MakeTurnWordAsync(turnRightCW);

                        continue;
                    }
                }

                // 3.3   (U F' U F) (U F' U2 F)
                if (cB == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[12] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("U F' U F U F' U2 F");
                        continue;
                    }
                }

                // 3.4   (U F' U' F) (d' F U F')
                if (cB == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[12])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnWordAsync(turnUpCW);
                        await MakeTurnWordAsync(turnFrontCCW);
                        await MakeTurnWordAsync(turnUpCCW);
                        await MakeTurnWordAsync(turnFrontCW);

                        // Turn d' = counter clockwise rotation of the two bottom layers
                        await MakeTurnWordAsync(turnUpCCW);
                        await MakeTurnWordAsync(turnCubeFrontToLeft);

                        await MakeTurnWordAsync(turnFrontCW);
                        await MakeTurnWordAsync(turnUpCW);
                        await MakeTurnWordAsync(turnFrontCCW);

                        continue;
                    }
                }

                // 3.5   (U' R U' R') (U' R U2 R')
                if (cB == aPieces[2] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("U' R U' R' U' R U2 R'");
                        continue;
                    }
                }

                // 3.6   (U' R U R') (d R' U' R)
                if (cB == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnWordAsync(turnUpCCW);
                        await MakeTurnWordAsync(turnRightCW);
                        await MakeTurnWordAsync(turnUpCW);
                        await MakeTurnWordAsync(turnRightCCW);

                        // Turn d = clockwise rotation of the two bottom layers
                        await MakeTurnWordAsync(turnUpCW);
                        await MakeTurnWordAsync(turnCubeFrontToRight);

                        await MakeTurnWordAsync(turnRightCCW);
                        await MakeTurnWordAsync(turnUpCCW);
                        await MakeTurnWordAsync(turnRightCW);

                        continue;
                    }
                }

                // 4. case: Corner pointing outwards, edge in top layer
                // 4.1   (R U' R' U) (d R' U' R)
                if (cB == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[10])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[41] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnWordAsync(turnRightCW);
                        await MakeTurnWordAsync(turnUpCCW);
                        await MakeTurnWordAsync(turnRightCCW);
                        await MakeTurnWordAsync(turnUpCW);

                        // Turn d = clockwise rotation of the two bottom layers
                        await MakeTurnWordAsync(turnUpCW);
                        await MakeTurnWordAsync(turnCubeFrontToRight);

                        await MakeTurnWordAsync(turnRightCCW);
                        await MakeTurnWordAsync(turnUpCCW);
                        await MakeTurnWordAsync(turnRightCW);

                        continue;
                    }
                }

                // 4.2   (F' U F U') (d' F U F')
                if (cB == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[43] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[1] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnWordAsync(turnFrontCCW);
                        await MakeTurnWordAsync(turnUpCW);
                        await MakeTurnWordAsync(turnFrontCW);
                        await MakeTurnWordAsync(turnUpCCW);

                        // Turn d' = counter clockwise rotation of the two bottom layers
                        await MakeTurnWordAsync(turnUpCCW);
                        await MakeTurnWordAsync(turnCubeFrontToLeft);

                        await MakeTurnWordAsync(turnFrontCW);
                        await MakeTurnWordAsync(turnUpCW);
                        await MakeTurnWordAsync(turnFrontCCW);

                        continue;
                    }
                }

                // 4.3   (U F' U2 F) (U F' U2 F)
                if (cB == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[37] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("U F' U2 F U F' U2 F");
                        continue;
                    }
                }

                // 4.4   (U' R U2 R') (U' R U2 R')
                if (cB == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[39] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("U' R U2 R' U' R U2 R'");
                        continue;
                    }
                }

                // 4.5   (U F' U' F) (U F' U2 F)
                if (cB == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[39] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("U F' U' F U F' U2 F");
                        continue;
                    }
                }

                // 4.6   (U' R U R') (U' R U2 R')
                if (cB == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[37] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("U' R U R' U' R U2 R'");
                        continue;
                    }
                }

                // 4.7   (U' R U' R' U) (R U R')
                if (cB == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[10] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("U' R U' R' U R U R'");
                        continue;
                    }
                }

                // 4.8   (U F' U F U') (F' U' F)
                if (cB == aPieces[2] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnLetterAsync("U F' U F U' F' U' F");
                        continue;
                    }
                }

                // 4.9   (U' R U R' U) (R U R')
                if (cB == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[39])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("U' R U R' U R U R'");
                        continue;
                    }
                }

                // 4.10   (U F' U' F U') (F' U' F)
                if (cB == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[37])
                    {
                        await MakeTurnLetterAsync("U F' U' F U' F' U' F");
                        continue;
                    }
                }

                // 4.11   (U F' U2 F U') (R U R')
                if (cB == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[43])
                {
                    if (aPieces[13] == aPieces[1] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("U F' U2 F U' R U R'");
                        continue;
                    }
                }

                // 4.12   (U' R U2 R' U) (F' U' F)
                if (cB == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[10] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[41])
                    {
                        await MakeTurnLetterAsync("U' R U2 R' U F' U' F");
                        continue;
                    }
                }

                // 5. case: Corner pointing upwards, edge in top layer
                // 5.1   (R U R' U') U' (R U R' U') (R U R')
                if (cB == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[43])
                {
                    if (aPieces[13] == aPieces[1] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("R U R' U' U' R U R' U' R U R'");
                        continue;
                    }
                }

                // 5.2   y' (R' U' R U) U (R' U' R U) (R' U' R)
                if (cB == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[10])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[41])
                    {
                        // y' - rotate the entire cube on U
                        await MakeTurnWordAsync(turnCubeFrontToRight);

                        await MakeTurnWordAsync(turnRightCCW);
                        await MakeTurnWordAsync(turnUpCCW);
                        await MakeTurnWordAsync(turnRightCW);
                        await MakeTurnWordAsync(turnUpCW);
                        await MakeTurnWordAsync(turnUpCW);
                        await MakeTurnWordAsync(turnRightCCW);
                        await MakeTurnWordAsync(turnUpCCW);
                        await MakeTurnWordAsync(turnRightCW);
                        await MakeTurnWordAsync(turnUpCW);
                        await MakeTurnWordAsync(turnRightCCW);
                        await MakeTurnWordAsync(turnUpCCW);
                        await MakeTurnWordAsync(turnRightCW);

                        continue;
                    }
                }

                // 5.3   (U2 R U R') (U R U' R')
                if (cB == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[39])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("U2 R U R' U R U' R'");
                        continue;
                    }
                }

                // 5.4   (U2 F' U' F) (U' F' U F)
                if (cB == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[37])
                    {
                        await MakeTurnLetterAsync("U2 F' U' F U' F' U F");
                        continue;
                    }
                }

                // 5.5   (U R U2 R') (U R U' R')
                if (cB == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[37])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("U R U2 R' U R U' R'");
                        continue;
                    }
                }

                // 5.6   (U' F' U2 F) (U' F' U F)
                if (cB == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnLetterAsync("U' F' U2 F U' F' U F");
                        continue;
                    }
                }

                // 5.7   (R U2 R') (U' R U R')
                if (cB == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("R U2 R' U' R U R'");
                        continue;
                    }
                }

                // 5.8   (F' U2 F) (U F' U' F)
                if (cB == aPieces[44] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnLetterAsync("F' U2 F U F' U' F");
                        continue;
                    }
                }

                // 6. case: Corner in bottom, edge in middle
                // 6.1   (R U' R' d R' U2 R) (U R' U2 R)
                if (aPieces[4] == aPieces[7] && aPieces[4] == aPieces[8] && aPieces[4] == aPieces[12])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[15] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnWordAsync(turnRightCW);
                        await MakeTurnWordAsync(turnUpCCW);
                        await MakeTurnWordAsync(turnRightCCW);

                        // Turn d = clockwise rotation of the two bottom layers
                        await MakeTurnWordAsync(turnUpCW);
                        await MakeTurnWordAsync(turnCubeFrontToRight);

                        await MakeTurnWordAsync(turnRightCCW);
                        await MakeTurnWordAsync(turnUp2);
                        await MakeTurnWordAsync(turnRightCW);
                        await MakeTurnWordAsync(turnUpCW);
                        await MakeTurnWordAsync(turnRightCCW);
                        await MakeTurnWordAsync(turnUp2);
                        await MakeTurnWordAsync(turnRightCW);

                        continue;
                    }
                }

                // 6.2   Does not exist

                // 6.3   (R U' R' U R U2 R') (U R U' R')
                if (cB == aPieces[15] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[8] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("R U' R' U R U2 R' U R U' R'");
                        continue;
                    }
                }


                // 6.4   (R U' R' U' R U R') (U' R U2 R')
                if (cB == aPieces[8] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[15])
                {
                    if (aPieces[13] == aPieces[12] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("R U' R' U' R U R' U' R U2 R'");
                        continue;
                    }
                }

                // 6.5   (R U R' U' R U' R') (U d R' U' R)
                if (cB == aPieces[15] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[12])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[8] && aPieces[13] == aPieces[16])
                    {
                        //await MakeTurnLetterAsync("R U R' U' R U' R' U d R' U' R");

                        await MakeTurnWordAsync(turnRightCW);
                        await MakeTurnWordAsync(turnUpCW);
                        await MakeTurnWordAsync(turnRightCCW);
                        await MakeTurnWordAsync(turnUpCCW);
                        await MakeTurnWordAsync(turnRightCW);
                        await MakeTurnWordAsync(turnUpCCW);
                        await MakeTurnWordAsync(turnRightCCW);
                        await MakeTurnWordAsync(turnUpCW);

                        // Turn d = clockwise rotation of the two bottom layers
                        await MakeTurnWordAsync(turnUpCW);
                        await MakeTurnWordAsync(turnCubeFrontToRight);

                        await MakeTurnWordAsync(turnRightCCW);
                        await MakeTurnWordAsync(turnUpCCW);
                        await MakeTurnWordAsync(turnRightCW);

                        continue;
                    }
                }

                // 6.6   (R U' R' d R' U' R) (U' R' U' R)
                if (cB == aPieces[8] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[15])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnWordAsync(turnRightCW);
                        await MakeTurnWordAsync(turnUpCCW);
                        await MakeTurnWordAsync(turnRightCCW);

                        // Turn d = clockwise rotation of the two bottom layers
                        await MakeTurnWordAsync(turnUpCW);
                        await MakeTurnWordAsync(turnCubeFrontToRight);

                        await MakeTurnWordAsync(turnRightCCW);
                        await MakeTurnWordAsync(turnUpCCW);
                        await MakeTurnWordAsync(turnRightCW);
                        await MakeTurnWordAsync(turnUpCCW);
                        await MakeTurnWordAsync(turnRightCCW);
                        await MakeTurnWordAsync(turnUpCCW);
                        await MakeTurnWordAsync(turnRightCW);

                        continue;
                    }
                }
            }

            //await MakeTurnWordAsync(turnCubeFrontToLeft);

            return true;
        }
    }
}