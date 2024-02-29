﻿// This solution is based on:
// https://ruwix.com/the-rubiks-cube/notation/advanced/
// https://ruwix.com/the-rubiks-cube/advanced-cfop-fridrich/
// https://ruwix.com/the-rubiks-cube/advanced-cfop-fridrich/white-cross/
// https://solvethecube.com/speedcubing#betterf2l
// https://www.cubeskills.com/categories/3x3-algs
// https://www.rubiksplace.com/speedcubing/guide/
// https://www.youtube.com/watch?v=MS5jByTX_pk
// https://www.youtube.com/watch?v=q_Hslj2aXuE&t=114s
// https://www.youtube.com/watch?v=z6c6ll_rpBo
// https://www.youtube.com/watch?v=JHxLRfN4rSQ

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
            // Cross part 1 (Solving the first layer 4 edge pieces completely
            if (!await ClassSolveCubeDaisy.SolveTopLayerEdgesAsync())
            {
                return false;
            }

            // Cross part 2
            if (!await ClassSolveCubeDaisy.SolveTopLayerEdges2Async())
            {
                return false;
            }

            // F2L (Solving the first two layers completely)
            if (!await SolveFirstTwoLayersAsync())
            {
                return true;
            }

            // OLL (Orientation of Last Layer)
            //if (!await SolveBottomLayerOrientationAsync())
            //{
            //    return false;
            //}

            // PLL (Permutation if Last Layer)
            //if (!await SolveBottomLayerPermutationAsync())
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
                cT = aPieces[49];
                if (cT == aPieces[45] && cT == aPieces[46] && cT == aPieces[47] && cT == aPieces[48] && cT == aPieces[50] && cT == aPieces[51] && cT == aPieces[52] && cT == aPieces[53])
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
                                    Debug.WriteLine("CFOP: number of turns first two layers: " + lCubeTurns.Count);
                                    break;
                                }
                            }
                        }
                    }
                }

                // Turn the cube
                if (nLoopTimes > 1)
                {
                    await MakeTurnWordAsync(turnCubeFrontToLeft);
                }

                // Bring the corner on the top layer to the correct place on the top layer
                if (aPieces[49] == aPieces[2] || aPieces[49] == aPieces[9] || aPieces[49] == aPieces[44])
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
                    if (aPieces[49] == aPieces[11] || aPieces[49] == aPieces[18] || aPieces[49] == aPieces[38])
                    {
                        if (aPieces[4] == aPieces[11] || aPieces[4] == aPieces[18] || aPieces[4] == aPieces[38])
                        {
                            if (aPieces[13] == aPieces[11] || aPieces[13] == aPieces[18] || aPieces[13] == aPieces[38])
                            {
                                await MakeTurnWordAsync(turnUpCW);
                            }
                        }
                    }

                    if (aPieces[49] == aPieces[0] || aPieces[49] == aPieces[29] || aPieces[49] == aPieces[42])
                    {
                        if (aPieces[4] == aPieces[0] || aPieces[4] == aPieces[29] || aPieces[4] == aPieces[42])
                        {
                            if (aPieces[13] == aPieces[0] || aPieces[13] == aPieces[29] || aPieces[13] == aPieces[42])
                            {
                                await MakeTurnWordAsync(turnUpCCW);
                            }
                        }
                    }

                    if (aPieces[49] == aPieces[20] || aPieces[49] == aPieces[27] || aPieces[49] == aPieces[36])
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

                // ---------------------------------------------------------------------------------------

                // https://www.youtube.com/watch?v=JHxLRfN4rSQ
                
                await MakeTurnWordAsync(turnCubeUpToRight2);

                // Sune case
                if (aPieces[40] == aPieces[2] && aPieces[40] == aPieces[42] && aPieces[40] != aPieces[36] && aPieces[40] != aPieces[38] && aPieces[40] != aPieces[44])
                {
                    await MakeTurnLetterAsync("R U R' U R U2 R'");
                    continue;
                }

                if (aPieces[40] == aPieces[11] && aPieces[40] == aPieces[44] && aPieces[40] != aPieces[36] && aPieces[40] != aPieces[38] && aPieces[40] != aPieces[42])
                {
                    await MakeTurnLetterAsync("y R U R' U R U2 R'");
                    continue;
                }

                if (aPieces[40] == aPieces[29] && aPieces[40] == aPieces[36] && aPieces[40] != aPieces[38] && aPieces[40] != aPieces[42] && aPieces[40] != aPieces[44])
                {
                    await MakeTurnLetterAsync("y' R U R' U R U2 R'");
                    continue;
                }

                if (aPieces[40] == aPieces[20] && aPieces[40] == aPieces[38] && aPieces[40] != aPieces[36] && aPieces[40] != aPieces[42] && aPieces[40] != aPieces[44])
                {
                    await MakeTurnLetterAsync("y2 R U R' U R U2 R'");
                    continue;
                }

                // Anti-Sune case
                if (aPieces[40] == aPieces[9] && aPieces[40] == aPieces[42] && aPieces[40] != aPieces[36] && aPieces[40] != aPieces[38] && aPieces[40] != aPieces[44])
                {
                    await MakeTurnLetterAsync("U R' U' R U' R' U2 R");
                    continue;
                }

                if (aPieces[40] == aPieces[0] && aPieces[40] == aPieces[36] && aPieces[40] != aPieces[38] && aPieces[40] != aPieces[42] && aPieces[40] != aPieces[44])
                {
                    await MakeTurnLetterAsync("R' U' R U' R' U2 R");
                    continue;
                }

                if (aPieces[40] == aPieces[18] && aPieces[40] == aPieces[44] && aPieces[40] != aPieces[36] && aPieces[40] != aPieces[38] && aPieces[40] != aPieces[42])
                {
                    await MakeTurnLetterAsync("U2 R' U' R U' R' U2 R");
                    continue;
                }

                if (aPieces[40] == aPieces[27] && aPieces[40] == aPieces[38] && aPieces[40] != aPieces[36] && aPieces[40] != aPieces[42] && aPieces[40] != aPieces[44])
                {
                    await MakeTurnLetterAsync("U' R' U' R U' R' U2 R");
                    continue;
                }

                // H case
                if (aPieces[40] == aPieces[9] && aPieces[40] == aPieces[11] && aPieces[40] == aPieces[27] && aPieces[40] == aPieces[29])
                {
                    await MakeTurnLetterAsync("R' U' R U' R' U R U' R' U2 R");
                    continue;
                }

                if (aPieces[40] == aPieces[0] && aPieces[40] == aPieces[2] && aPieces[40] == aPieces[18] && aPieces[40] == aPieces[20])
                {
                    await MakeTurnLetterAsync("y R' U' R U' R' U R U' R' U2 R");
                    continue;
                }

                // PI case
                if (aPieces[40] == aPieces[27] && aPieces[40] == aPieces[29] && aPieces[40] == aPieces[2] && aPieces[40] == aPieces[18])
                {
                    await MakeTurnLetterAsync("R U2 R2 U' R2 U' R2 U2 R");
                    continue;
                }

                if (aPieces[40] == aPieces[9] && aPieces[40] == aPieces[11] && aPieces[40] == aPieces[0] && aPieces[40] == aPieces[20])
                {
                    await MakeTurnLetterAsync("y2 R U2 R2 U' R2 U' R2 U2 R");
                    continue;
                }

                if (aPieces[40] == aPieces[0] && aPieces[40] == aPieces[2] && aPieces[40] == aPieces[11] && aPieces[40] == aPieces[27])
                {
                    await MakeTurnLetterAsync("y R U2 R2 U' R2 U' R2 U2 R");
                    continue;
                }

                if (aPieces[40] == aPieces[18] && aPieces[40] == aPieces[20] && aPieces[40] == aPieces[9] && aPieces[40] == aPieces[29])
                {
                    await MakeTurnLetterAsync("y' R U2 R2 U' R2 U' R2 U2 R");
                    continue;
                }

                // L case
                if (aPieces[40] == aPieces[36] && aPieces[40] == aPieces[44] && aPieces[40] == aPieces[0] && aPieces[40] == aPieces[11])
                {
                    await MakeTurnLetterAsync("F R' F' r U R U' r'");
                    continue;
                }

                if (aPieces[40] == aPieces[38] && aPieces[40] == aPieces[42] && aPieces[40] == aPieces[2] && aPieces[40] == aPieces[27])
                {
                    await MakeTurnLetterAsync("y F R' F' r U R U' r'");
                    continue;
                }

                if (aPieces[40] == aPieces[36] && aPieces[40] == aPieces[44] && aPieces[40] == aPieces[18] && aPieces[40] == aPieces[29])
                {
                    await MakeTurnLetterAsync("y2 F R' F' r U R U' r'");
                    continue;
                }

                if (aPieces[40] == aPieces[38] && aPieces[40] == aPieces[42] && aPieces[40] == aPieces[9] && aPieces[40] == aPieces[20])
                {
                    await MakeTurnLetterAsync("y' F R' F' r U R U' r'");
                    continue;
                }

                // T case
                if (aPieces[40] == aPieces[38] && aPieces[40] == aPieces[44] && aPieces[40] == aPieces[0] && aPieces[40] == aPieces[20])
                {
                    await MakeTurnLetterAsync("r U R' U' r' F R F'");
                    continue;
                }

                if (aPieces[40] == aPieces[36] && aPieces[40] == aPieces[42] && aPieces[40] == aPieces[2] && aPieces[40] == aPieces[18])
                {
                    await MakeTurnLetterAsync("y2 r U R' U' r' F R F'");
                    continue;
                }

                if (aPieces[40] == aPieces[42] && aPieces[40] == aPieces[44] && aPieces[40] == aPieces[11] && aPieces[40] == aPieces[27])
                {
                    await MakeTurnLetterAsync("y r U R' U' r' F R F'");
                    continue;
                }

                if (aPieces[40] == aPieces[36] && aPieces[40] == aPieces[38] && aPieces[40] == aPieces[9] && aPieces[40] == aPieces[29])
                {
                    await MakeTurnLetterAsync("y' r U R' U' r' F R F'");
                    continue;
                }

                // U case
                if (aPieces[40] == aPieces[36] && aPieces[40] == aPieces[38] && aPieces[40] == aPieces[0] && aPieces[40] == aPieces[2])
                {
                    await MakeTurnLetterAsync("R2 D R' U2 R D' R' U2 R'");
                    continue;
                }

                if (aPieces[40] == aPieces[42] && aPieces[40] == aPieces[44] && aPieces[40] == aPieces[18] && aPieces[40] == aPieces[20])
                {
                    await MakeTurnLetterAsync("y2 R2 D R' U2 R D' R' U2 R'");
                    continue;
                }

                if (aPieces[40] == aPieces[38] && aPieces[40] == aPieces[44] && aPieces[40] == aPieces[27] && aPieces[40] == aPieces[29])
                {
                    await MakeTurnLetterAsync("y R2 D R' U2 R D' R' U2 R'");
                    continue;
                }

                if (aPieces[40] == aPieces[36] && aPieces[40] == aPieces[42] && aPieces[40] == aPieces[9] && aPieces[40] == aPieces[11])
                {
                    await MakeTurnLetterAsync("y' R2 D R' U2 R D' R' U2 R'");
                    continue;
                }

                //continue;

                // ---------------------------------------------------------------------------------------

                // Bring the corner on the bottom layer to the correct place on the bottom layer
                //if (aPieces[49] == aPieces[8] || aPieces[49] == aPieces[15] || aPieces[49] == aPieces[47])
                //{
                //    if (aPieces[4] == aPieces[8] || aPieces[4] == aPieces[15] || aPieces[4] == aPieces[47])
                //    {
                //        if (aPieces[13] == aPieces[8] || aPieces[13] == aPieces[15] || aPieces[13] == aPieces[47])
                //        {
                //            // Do nothing
                //        }
                //    }
                //}
                //else
                //{
                //    if (aPieces[49] == aPieces[17] || aPieces[49] == aPieces[24] || aPieces[49] == aPieces[53])
                //    {
                //        if (aPieces[4] == aPieces[17] || aPieces[4] == aPieces[24] || aPieces[4] == aPieces[53])
                //        {
                //            if (aPieces[13] == aPieces[17] || aPieces[13] == aPieces[24] || aPieces[13] == aPieces[53])
                //            {
                //                await MakeTurnWordAsync(turnDownCCW);
                //            }
                //        }
                //    }

                //    if (aPieces[49] == aPieces[6] || aPieces[49] == aPieces[35] || aPieces[49] == aPieces[45])
                //    {
                //        if (aPieces[4] == aPieces[6] || aPieces[4] == aPieces[35] || aPieces[4] == aPieces[45])
                //        {
                //            if (aPieces[13] == aPieces[6] || aPieces[13] == aPieces[35] || aPieces[13] == aPieces[45])
                //            {
                //                await MakeTurnWordAsync(turnDownCW);
                //            }
                //        }
                //    }

                //    if (aPieces[49] == aPieces[26] || aPieces[49] == aPieces[33] || aPieces[49] == aPieces[51])
                //    {
                //        if (aPieces[4] == aPieces[26] || aPieces[4] == aPieces[33] || aPieces[4] == aPieces[51])
                //        {
                //            if (aPieces[13] == aPieces[26] || aPieces[13] == aPieces[33] || aPieces[13] == aPieces[51])
                //            {
                //                await MakeTurnWordAsync(turnDown2);
                //            }
                //        }
                //    }
                //}

                //    if (aPieces[49] == aPieces[26] || aPieces[49] == aPieces[33] || aPieces[49] == aPieces[51])
                //    {
                //        if (aPieces[4] == aPieces[26] || aPieces[4] == aPieces[33] || aPieces[4] == aPieces[51])
                //        {
                //            if (aPieces[13] == aPieces[26] || aPieces[13] == aPieces[33] || aPieces[13] == aPieces[51])
                //            {
                //                await MakeTurnWordAsync(turnDown2);
                //            }
                //        }
                //    }
                //}
                // ---------------------------------------------------------------------------------------

                // https://www.youtube.com/watch?v=Hx9ZbPdX8zM
                // Part 1: Step 1 and 2
                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[14] && aPieces[4] == aPieces[47])
                {
                    if (aPieces[13] == aPieces[8] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[21])
                    {
                        await MakeTurnLetterAsync("R U R' y R U' R'");
                        continue;
                    }
                }

                // Step 3
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[31] == aPieces[9] && aPieces[31] == aPieces[43])
                    {
                        await MakeTurnLetterAsync("R' U R");
                        continue;
                    }
                }

                // Hiding trick (variation 2)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("U' R U' R'");
                        continue;
                    }
                }

                // Part 2: Five fundamental cases
                // Case 1: Matching pair
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[19])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[37] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("R' U2 R y U' L' U L");
                        continue;
                    }
                }

                // Case 2: Non-matching
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[37])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[19] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("R U R'");
                        continue;
                    }
                }

                // Case 3: White on top
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[28])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnLetterAsync("U2 F' U' F y U' L' U L");
                        continue;
                    }
                }

                // Case 4: Mirrored matching
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[39] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[28])
                    {
                        await MakeTurnLetterAsync("U' R U2 R' U R U' R'");
                        continue;
                    }
                }

                // Case 5: Mirrored non-matching
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[28] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnLetterAsync("y L' U' L");
                        continue;
                    }
                }

                // Part 3: Solving the last pair
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[1] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("y' U R' U2 R y U'");
                        continue;
                    }
                }



                // ---------------------------------------------------------------------------------------

                // R U2 R'
                if (aPieces[49] == aPieces[42] && aPieces[4] == aPieces[0] && aPieces[13] == aPieces[29])
                {
                    await MakeTurnLetterAsync("R U2 R'");
                    continue;
                }

                // F U2 F' or U' R U2 R'
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[39] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("U' R U2 R'");
                        continue;
                    }
                }

                // (R U2 R') (U R U' R')
                if (aPieces[49] == aPieces[42] && aPieces[4] == aPieces[0] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[10] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("R U2 R' U R U' R'");
                        continue;
                    }
                }

                // (R U' R') U (F' U' F) or (R U' R') d (R' U' R)
                if (aPieces[49] == aPieces[11] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[38])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[18] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnLetterAsync("R U' R' U F' U' F");
                        continue;
                    }
                }

                // (L' U L) U (L' U' L) or y' U R U' R' U y' R' U' R
                if (aPieces[49] == aPieces[11] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[8] && aPieces[4] == aPieces[39])
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

                // ---------------------------------------------------------------------------------------

                // https://ruwix.com/the-rubiks-cube/advanced-cfop-fridrich/first-two-layers-f2l/
                // 1. Easy cases
                // 1.1   R U R'
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[37])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("R U R'");
                        continue;
                    }
                }

                // 1.2   F' U' F
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnLetterAsync("F' U' F");
                        continue;
                    }
                }

                // 1.3   U' F' U F
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[43] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("U' F' U F");
                        continue;
                    }
                }

                // 1.4    U R U' R'
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[41] && aPieces[4] == aPieces[44])
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
                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[8] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnLetterAsync("F' U F U' F' U F");
                        continue;
                    }
                }

                // 2.4   (R U R') (U' R U R')
                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[8] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("R U R' U' R U R'");
                        continue;
                    }
                }

                // 2.5   (R U' R') (U R U' R')
                if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[15] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[10] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("R U' R' U R U' R'");
                        continue;
                    }
                }

                // 2.6   (F' U' F) (U F' U' F)
                if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[15])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnLetterAsync("F' U' F U F' U' F");
                        continue;
                    }
                }

                // 3. case: Corner in top, edge in middle
                // 3.1   (R U R' U') (R U R' U') (R U R')
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("R U R' U' R U R' U' R U R'");
                        continue;
                    }
                }

                // 3.2   (R U' R') (d R' U R)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[12])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[5] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("R U' R' d R' U R");
                        continue;
                    }
                }

                // 3.3   (U F' U F) (U F' U2 F)
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[12] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("U F' U F U F' U2 F");
                        continue;
                    }
                }

                // 3.4   (U F' U' F) (d' F U F')
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[12])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("U F' U' F d' F U F'");
                        continue;
                    }
                }

                // 3.5   (U' R U' R') (U' R U2 R')
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("U' R U' R' U' R U2 R'");
                        continue;
                    }
                }

                // 3.6   (U' R U R') (d R' U' R)
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("U' R U R' d R' U' R");
                        continue;
                    }
                }

                // 4. case: Corner pointing outwards, edge in top layer
                // 4.1   (R U' R' U) (d R' U' R)
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[10])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[41] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("R U' R' U d R' U' R");
                        continue;
                    }
                }

                // 4.2   (F' U F U') (d' F U F')
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[43] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[1] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("F' U F U' d' F U F'");
                        continue;
                    }
                }

                // 4.3   (U F' U2 F) (U F' U2 F)
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[37] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("U F' U2 F U F' U2 F");
                        continue;
                    }
                }

                // 4.4   (U' R U2 R') (U' R U2 R')
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[39] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("U' R U2 R' U' R U2 R'");
                        continue;
                    }
                }

                // 4.5   (U F' U' F) (U F' U2 F)
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[39] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("U F' U' F U F' U2 F");
                        continue;
                    }
                }

                // 4.6   (U' R U R') (U' R U2 R')
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[37] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("U' R U R' U' R U2 R'");
                        continue;
                    }
                }

                // 4.7   (U' R U' R' U) (R U R')
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[10] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("U' R U' R' U R U R'");
                        continue;
                    }
                }

                // 4.8   (U F' U F U') (F' U' F)
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnLetterAsync("U F' U F U' F' U' F");
                        continue;
                    }
                }

                // 4.9   (U' R U R' U) (R U R')
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[39])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("U' R U R' U R U R'");
                        continue;
                    }
                }

                // 4.10   (U F' U' F U') (F' U' F)
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[37])
                    {
                        await MakeTurnLetterAsync("U F' U' F U' F' U' F");
                        continue;
                    }
                }

                // 4.11   (U F' U2 F U') (R U R')
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[43])
                {
                    if (aPieces[13] == aPieces[1] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("U F' U2 F U' R U R'");
                        continue;
                    }
                }

                // 4.12   (U' R U2 R' U) (F' U' F)
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[10] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[41])
                    {
                        await MakeTurnLetterAsync("U' R U2 R' U F' U' F");
                        continue;
                    }
                }

                // 5. case: Corner pointing upwards, edge in top layer
                // 5.1   (R U R' U') U' (R U R' U') (R U R')
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[43])
                {
                    if (aPieces[13] == aPieces[1] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("R U R' U' U' R U R' U' R U R'");
                        continue;
                    }
                }

                // 5.2   y' (R' U' R U) U (R' U' R U) (R' U' R)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[10])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[41])
                    {
                        await MakeTurnLetterAsync("y' R' U' R U U R' U' R U R' U' R");
                        continue;
                    }
                }

                // 5.3   (U2 R U R') (U R U' R')
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[39])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("U2 R U R' U R U' R'");
                        continue;
                    }
                }

                // 5.4   (U2 F' U' F) (U' F' U F)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[37])
                    {
                        await MakeTurnLetterAsync("U2 F' U' F U' F' U F");
                        continue;
                    }
                }

                // 5.5   (U R U2 R') (U R U' R')
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[37])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("U R U2 R' U R U' R'");
                        continue;
                    }
                }

                // 5.6   (U' F' U2 F) (U' F' U F)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnLetterAsync("U' F' U2 F U' F' U F");
                        continue;
                    }
                }

                // 5.7   (R U2 R') (U' R U R')
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("R U2 R' U' R U R'");
                        continue;
                    }
                }

                // 5.8   (F' U2 F) (U F' U' F)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9])
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
                        await MakeTurnLetterAsync("R U' R' d R' U2 R U R' U2 R");
                        continue;
                    }
                }

                // 6.2   Does not exist

                // 6.3   (R U' R' U R U2 R') (U R U' R')
                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[8] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("R U' R' U R U2 R' U R U' R'");
                        continue;
                    }
                }


                // 6.4   (R U' R' U' R U R') (U' R U2 R')
                if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[15])
                {
                    if (aPieces[13] == aPieces[12] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("R U' R' U' R U R' U' R U2 R'");
                        continue;
                    }
                }

                // 6.5   (R U R' U' R U' R') (U d R' U' R)
                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[12])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[8] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("R U R' U' R U' R' U d R' U' R");
                        continue;
                    }
                }

                // 6.6   (R U' R' d R' U' R) (U' R' U' R)
                if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[15])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnLetterAsync("R U' R' d R' U' R U' R' U' R");
                        continue;
                    }
                }

                // ---------------------------------------------------------------------------------------

                // https://www.rubiksplace.com/speedcubing/F2L-algorithms/
                // Corner on top, FL color facing side, edge colors match
                // 1   U (R U' R') or  R' F R F'
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[41] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnLetterAsync("U R U' R'");
                        continue;
                    }
                }

                // 2   y' U' (R' U R) or F R' F' R
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[43] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("F R' F' R");
                        continue;
                    }
                }

                // 3   U' R U R' U2 (R U' R')
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[37] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnLetterAsync("U' R U R' U2 R U' R'");
                        continue;
                    }
                }

                // 4   d R' U' R U2' (R' U R) or y' (U R' U' R) U2 (R' U R)
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[39] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("d R' U' R U2' R' U R");
                        continue;
                    }
                }

                // 5   U' R U2' R' U2 (R U' R')
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[39] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnLetterAsync("U' R U2' R' U2 R U' R'");
                        continue;
                    }
                }

                // 6   d R' U2 R U2' (R' U R) or R' F R F'
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[37] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("R' F R F'");
                        continue;
                    }
                }

                // 7   y' R' U R U' d' (R U R') or y L' U L U2 y (R U R')
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[43] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[1] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnLetterAsync("y L' U L U2 y R U R'");
                        continue;
                    }
                }

                // 8   R U' R' U d (R' U' R) or R U' R' U2 y' (R' U' R) or (R U' R') U2 (F' U' F)
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[10])
                {
                    if (aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[41] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("R U' R' U2 F' U' F");
                        continue;
                    }
                }

                // Corner on top, FL color facing side, edge colors opposite
                // 9   y' (R' U' R)
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnLetterAsync("y' R' U' R");
                        continue;
                    }
                }

                // 10   (R U R')
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[37])
                {
                    if (aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("R U R'");
                        continue;
                    }
                }

                // 11   d R' U' R U' (R' U' R) or U' R U' R' d R' U' R or U' R U' R' U y' R' U' R
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[37])
                    {
                        await MakeTurnLetterAsync("d R' U' R U' R' U' R");
                        continue;
                    }
                }

                // 12   U' R U R' U (R U R')
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[39])
                {
                    if (aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("U' R U R' U R U R'");
                        continue;
                    }
                }

                // 13   U' R U2' R' d (R' U' R)
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[10] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[41])
                    {
                        await MakeTurnLetterAsync("U' R U2' R' d R' U' R");
                        continue;
                    }
                }

                // 14   R' U2 R2 U R2' U R or R U' R' U R U' R' U2(R U' R') or d R' U2 R d'(R U R')	*Last R' U R can be avoided if back slot is empty.
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[43])
                {
                    if (aPieces[13] == aPieces[1] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("R' U2 R2 U R2' U R");
                        continue;
                    }
                }

                // 15   d R' U R U' (R' U' R)
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnLetterAsync("d R' U R U' R' U' R");
                        continue;
                    }
                }

                // 16   U' R U' R' U (R U R')
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[10] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("U' R U' R' U R U R'");
                        continue;
                    }
                }

                // Corner on top, FL color facing up
                // 17   R U2' R' U' (R U R')
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnLetterAsync("R U2' R' U' R U R'");
                        continue;
                    }
                }

                // 18   y' R' U2 R U (R' U' R) or y (L' U2 L) U (L' U' L)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnLetterAsync("y' R' U2 R U R' U' R");
                        continue;
                    }
                }

                // 19   U R U2 R' U (R U' R') or U R U2 R2 F R F'
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[37])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnLetterAsync("U R U2 R2 F R F'");
                        continue;
                    }
                }

                // 20   y' U' R' U2 R U' (R' U R)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnLetterAsync("y' U' R' U2 R U' R' U R");
                        continue;
                    }
                }

                // 21   U2 R U R' U (R U' R') or (R U' R') U2' (R U R')
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[39])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnLetterAsync("R U' R' U2' R U R'");
                        continue;
                    }
                }

                // 22   y' U2 R' U' R U' (R' U R) or y' R' U R U2 (R' U' R)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[37])
                    {
                        await MakeTurnLetterAsync("y' R' U R U2 R' U' R");
                        continue;
                    }
                }

                // 23   y' U R' U2 R y R U2 R' U R U' R' or U2 R2 U2 R' U' R U' R2 or R U R' U2' R U R' U'(R U R')
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[43])
                {
                    if (aPieces[13] == aPieces[1] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnLetterAsync("U2 R2 U2 R' U' R U' R2");
                        continue;
                    }
                }

                // 24   U' R U2' R' y' R' U2 R U' R' U R or R U R' d R' U R U' (R' U R) or y' U2 R2 U2 R U R' U R2
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[10])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[41])
                    {
                        await MakeTurnLetterAsync("y' U2 R2 U2 R U R' U R2");
                        continue;
                    }
                }

                // Corner down, edge on top
                // 25   U R U' R' d' (L' U L) or U R U' R' U' y (L' U L)
                if (aPieces[49] == aPieces[47] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[8])
                {
                    if (aPieces[13] == aPieces[14] && aPieces[13] == aPieces[15] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnLetterAsync("U R U' R' U' y L' U L");
                        continue;
                    }
                }

                // 26   y' U' R' U R r' U' R U M' or d' L' U L d(R U' R') or y U' (L' U L) y' U (R U' R')
                if (aPieces[49] == aPieces[47] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[8] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[10] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[15] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnLetterAsync("y U' L' U L y' U R U' R'");
                        continue;
                    }
                }

                // 27   y' R' U' R U (R' U' R)
                if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[15])
                {
                    if (aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnLetterAsync("y' R' U' R U R' U' R");
                        continue;
                    }
                }

                // 28   R U R' U' (R U R')
                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[8] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnLetterAsync("R U R' U' R U R'");
                        continue;
                    }
                }

                // 29   R U' R' U (R U' R')
                if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[15] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[10] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnLetterAsync("R U' R' U R U' R'");
                        continue;
                    }
                }

                // 30   y' R' U R U' (R' U R) or R U R' d (R' U2 R)
                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[8] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnLetterAsync("R U R' d R' U2 R");
                        continue;
                    }
                }

                // Edge down, corner on top
                // 31   U' R U' R' U2 (R U' R') or d' L' U' R' U L U' R
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnLetterAsync("U' R U' R' U2 R U' R'");
                        continue;
                    }
                }

                // 32   d R' U R U2 (R' U R) or U' (R U2' R') U (R U R') or U R U R' U2 (R U R')
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[12] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("U R U R' U2 R U R'");
                        continue;
                    }
                }

                // 33   U' R U R' d (R' U' R)
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnLetterAsync("U' R U R' d R' U' R");
                        continue;
                    }
                }

                // 34   d R' U' R d' (R U R') or y U2 (L' U L) U y (L U L')
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[12])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnLetterAsync("d R' U' R d' R U R'");
                        continue;
                    }
                }

                // 35   R U' R' d (R' U R)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[12])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[5] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnLetterAsync("R U' R' d R' U R");
                        continue;
                    }
                }

                // 36   [R U R' U'][R U R' U'](R U R') or U [R U' R' U][R U' R' U](R U' R')
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnLetterAsync("R U R' U' R U R' U' R U R'");
                        continue;
                    }
                }

                // Corner down, edge down
                // 37   R U' R' U' R U R' U2 (R U' R') or y' R' U' R U2 R' U R U' (R' U' R)
                if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[15])
                {
                    if (aPieces[13] == aPieces[12] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[47])
                    {
                        await MakeTurnLetterAsync("R U' R' U' R U R' U2 R U' R'");
                        continue;
                    }
                }

                // 38   R U R' U2 R U' R' U (R U R') or R U' R' U R U2' R' U (R U' R')
                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[47])
                {
                    if (aPieces[13] == aPieces[8] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnLetterAsync("R U R' U2 R U' R' U R U R'");
                        continue;
                    }
                }

                // 39   R U' R' d R' U' R U' (R' U' R)
                if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[15])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[47])
                    {
                        await MakeTurnLetterAsync("R U' R' d R' U' R U' (R' U' R");
                        continue;
                    }
                }

                // 40   R U R' U' R U' R' U2 y' (R' U' R) or R U' R' U d R' U' R U' (R' U R)
                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[47])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[8] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnLetterAsync("R U' R' U d R' U' R U' R' U R");
                        continue;
                    }
                }

                // 41   R U' R' U y' R' U2 R U2' (R' U R) or R U' R' d R' U2 R U2' (R' U R) or [R' F R F'][R U' R' U][R U' R' U2](R U' R')
                if (aPieces[49] == aPieces[47] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[8] && aPieces[4] == aPieces[12])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[15] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnLetterAsync("R U' R' d R' U2 R U2' R' U R");
                        continue;
                    }
                }

                // ---------------------------------------------------------------------------------------



            }

            //await MakeTurnWordAsync(turnCubeFrontToLeft);

            return true;
        }
    }
}