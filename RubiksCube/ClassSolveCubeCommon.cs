/* These turns are based on:
   the book: Mastering Rubik's Cube, by Don Taylor, Dutch version 1981
   file:///C:/Sources/MAUI/RubiksCube/Miscellaneous/Manuals/RubiksCubeBeginnerInstructions.pdf
   https://www.rubiksplace.com/speedcubing/guide/ */

using System.Diagnostics;
using static RubiksCube.Globals;

namespace RubiksCube
{
    internal sealed class ClassSolveCubeCommon
    {
        //// Declare variables
        private const int nLoopTimesMax = 200;

        /// <summary>
        /// Solve the edges of the top layer - Part 1
        /// </summary>
        /// <returns></returns>
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
                    Debug.WriteLine("Daisy: number of turns top layer edges: " + lCubeTurns.Count);
                    break;
                }

                //---------------------------------------------------------------------------------------
                // With this direct method there is a profit of the average number of turns of 1.00%
                // There are slightly more situations of profit than of loss in the number of turns

                if (bSolveNewSolutionsTest)
                {
                    // Edge is at the bottom
                    if (cB == aPieces[46])
                    {
                        if (cB != aPieces[43])
                        {
                        }
                        else if (cB != aPieces[41])
                        {
                            await MakeTurnAsync("U");
                        }
                        else if (cB != aPieces[39])
                        {
                            await MakeTurnAsync("U'");
                        }
                        else if (cB != aPieces[37])
                        {
                            await MakeTurnAsync("U2");
                        }

                        await MakeTurnAsync("F2");
                    }

                    if (cB == aPieces[50])
                    {
                        if (cB != aPieces[41])
                        {
                        }
                        else if (cB != aPieces[37])
                        {
                            await MakeTurnAsync("U");
                        }
                        else if (cB != aPieces[43])
                        {
                            await MakeTurnAsync("U'");
                        }
                        else if (cB != aPieces[39])
                        {
                            await MakeTurnAsync("U2");
                        }

                        await MakeTurnAsync("R2");
                    }

                    if (cB == aPieces[52])
                    {
                        if (cB != aPieces[37])
                        {
                        }
                        else if (cB != aPieces[39])
                        {
                            await MakeTurnAsync("U");
                        }
                        else if (cB != aPieces[41])
                        {
                            await MakeTurnAsync("U'");
                        }
                        else if (cB != aPieces[43])
                        {
                            await MakeTurnAsync("U2");
                        }

                        await MakeTurnAsync("B2");
                    }

                    if (cB == aPieces[48])
                    {
                        if (cB != aPieces[39])
                        {
                        }
                        else if (cB != aPieces[43])
                        {
                            await MakeTurnAsync("U");
                        }
                        else if (cB != aPieces[37])
                        {
                            await MakeTurnAsync("U'");
                        }
                        else if (cB != aPieces[41])
                        {
                            await MakeTurnAsync("U2");
                        }

                        await MakeTurnAsync("L2");
                    }

                    // Edge is at the front
                    if (cB == aPieces[5])
                    {
                        if (cB != aPieces[41])
                        {
                        }
                        else if (cB != aPieces[37])
                        {
                            await MakeTurnAsync("U");
                        }
                        else if (cB != aPieces[43])
                        {
                            await MakeTurnAsync("U'");
                        }
                        else if (cB != aPieces[39])
                        {
                            await MakeTurnAsync("U2");
                        }

                        await MakeTurnAsync("R");
                    }

                    if (cB == aPieces[3])
                    {
                        if (cB != aPieces[39])
                        {
                        }
                        else if (cB != aPieces[43])
                        {
                            await MakeTurnAsync("U");
                        }
                        else if (cB != aPieces[37])
                        {
                            await MakeTurnAsync("U'");
                        }
                        else if (cB != aPieces[41])
                        {
                            await MakeTurnAsync("U2");
                        }

                        await MakeTurnAsync("L'");
                    }

                    // Edge is at the right
                    if (cB == aPieces[12])
                    {
                        if (cB != aPieces[43])
                        {
                        }
                        else if (cB != aPieces[41])
                        {
                            await MakeTurnAsync("U");
                        }
                        else if (cB != aPieces[39])
                        {
                            await MakeTurnAsync("U'");
                        }
                        else if (cB != aPieces[37])
                        {
                            await MakeTurnAsync("U2");
                        }

                        await MakeTurnAsync("F'");
                    }

                    if (cB == aPieces[14])
                    {
                        if (cB != aPieces[37])
                        {
                        }
                        else if (cB != aPieces[39])
                        {
                            await MakeTurnAsync("U");
                        }
                        else if (cB != aPieces[41])
                        {
                            await MakeTurnAsync("U'");
                        }
                        else if (cB != aPieces[43])
                        {
                            await MakeTurnAsync("U2");
                        }

                        await MakeTurnAsync("B");
                    }

                    // Edge is at the back
                    if (cB == aPieces[21])
                    {
                        if (cB != aPieces[41])
                        {
                        }
                        else if (cB != aPieces[37])
                        {
                            await MakeTurnAsync("U");
                        }
                        else if (cB != aPieces[43])
                        {
                            await MakeTurnAsync("U'");
                        }
                        else if (cB != aPieces[39])
                        {
                            await MakeTurnAsync("U2");
                        }

                        await MakeTurnAsync("R'");
                    }

                    if (cB == aPieces[23])
                    {
                        if (cB != aPieces[39])
                        {
                        }
                        else if (cB != aPieces[43])
                        {
                            await MakeTurnAsync("U");
                        }
                        else if (cB != aPieces[37])
                        {
                            await MakeTurnAsync("U'");
                        }
                        else if (cB != aPieces[41])
                        {
                            await MakeTurnAsync("U2");
                        }

                        await MakeTurnAsync("L");
                    }

                    // Edge is at the left
                    if (cB == aPieces[30])
                    {
                        if (cB != aPieces[37])
                        {
                        }
                        else if (cB != aPieces[39])
                        {
                            await MakeTurnAsync("U");
                        }
                        else if (cB != aPieces[41])
                        {
                            await MakeTurnAsync("U'");
                        }
                        else if (cB != aPieces[43])
                        {
                            await MakeTurnAsync("U2");
                        }

                        await MakeTurnAsync("B'");
                    }

                    if (cB == aPieces[32])
                    {
                        if (cB != aPieces[43])
                        {
                        }
                        else if (cB != aPieces[41])
                        {
                            await MakeTurnAsync("U");
                        }
                        else if (cB != aPieces[39])
                        {
                            await MakeTurnAsync("U'");
                        }
                        else if (cB != aPieces[37])
                        {
                            await MakeTurnAsync("U2");
                        }

                        await MakeTurnAsync("F");
                    }
                }

                //---------------------------------------------------------------------------------------

                // Move 1 -> 3
                if (aPieces[49] == aPieces[1])
                {
                    await MakeTurnAsync("F'");
                }

                if (aPieces[49] == aPieces[3])
                {
                    if (aPieces[49] == aPieces[39])
                    {
                        while (true)
                        {
                            if (aPieces[49] != aPieces[37])
                            {
                                await MakeTurnAsync("U'");
                                break;
                            }

                            if (aPieces[49] != aPieces[43])
                            {
                                await MakeTurnAsync("U");
                                break;
                            }

                            if (aPieces[49] != aPieces[41])
                            {
                                await MakeTurnAsync("U2");
                                break;
                            }

                            break;
                        }
                    }

                    await MakeTurnAsync("L'");
                    continue;
                }

                // Move 7 -> 5
                if (aPieces[49] == aPieces[7])
                {
                    await MakeTurnAsync("F'");
                }

                if (aPieces[49] == aPieces[5])
                {
                    if (aPieces[49] == aPieces[41])
                    {
                        while (true)
                        {
                            if (aPieces[49] != aPieces[37])
                            {
                                await MakeTurnAsync("U");
                                break;
                            }

                            if (aPieces[49] != aPieces[43])
                            {
                                await MakeTurnAsync("U'");
                                break;
                            }

                            if (aPieces[49] != aPieces[39])
                            {
                                await MakeTurnAsync("U2");
                                break;
                            }

                            break;
                        }
                    }

                    await MakeTurnAsync("R");
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
                                await MakeTurnAsync("U");
                                break;
                            }

                            if (aPieces[49] != aPieces[43])
                            {
                                await MakeTurnAsync("U'");
                                break;
                            }

                            if (aPieces[49] != aPieces[39])
                            {
                                await MakeTurnAsync("U2");
                                break;
                            }

                            break;
                        }
                    }

                    await MakeTurnAsync("R2");
                    continue;
                }

                if (cB == aPieces[10] || cB == aPieces[12] || cB == aPieces[14] || cB == aPieces[16] || cB == aPieces[52])
                {
                    await MakeTurnAsync("y");
                    continue;
                }

                if (cB == aPieces[19] || cB == aPieces[21] || cB == aPieces[23] || cB == aPieces[25] || cB == aPieces[48])
                {
                    await MakeTurnAsync("y2");
                    continue;
                }

                if (cB == aPieces[28] || cB == aPieces[30] || cB == aPieces[32] || cB == aPieces[34] || cB == aPieces[46])
                {
                    await MakeTurnAsync("y'");
                    continue;
                }
            }

            return true;
        }

        /// <summary>
        /// Solve the edges of the top layer but turned at the bottom - Part 2
        /// </summary>
        /// <returns></returns>
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
                        Debug.WriteLine("Daisy: number of turns top layer edges 2: " + lCubeTurns.Count);
                        break;
                    }
                }

                // The upper edge and the side center are in the correct position
                if (cB == aPieces[43] && aPieces[1] == aPieces[4])
                {
                    await MakeTurnAsync("F2");
                }

                if (cB == aPieces[41] && aPieces[10] == aPieces[13])
                {
                    await MakeTurnAsync("R2");
                }

                if (cB == aPieces[37] && aPieces[19] == aPieces[22])
                {
                    await MakeTurnAsync("B2");
                }

                if (cB == aPieces[39] && aPieces[28] == aPieces[31])
                {
                    await MakeTurnAsync("L2");
                }

                // The upper edge and the side center are not in the correct position
                while (true)
                {
                    if (cB == aPieces[43] && aPieces[1] == aPieces[4])
                    {
                        await MakeTurnAsync("F2");
                        break;
                    }

                    if (cB == aPieces[43] && aPieces[1] == aPieces[13])
                    {
                        await MakeTurnAsync("U' R2");
                        break;
                    }

                    if (cB == aPieces[43] && aPieces[1] == aPieces[31])
                    {
                        await MakeTurnAsync("U L2");
                        break;
                    }

                    if (cB == aPieces[43] && aPieces[1] == aPieces[22])
                    {
                        await MakeTurnAsync("U2 B2");
                        break;
                    }

                    break;
                }

                await MakeTurnAsync("y");
            }

            return true;
        }

        /// <summary>
        /// Lign up the center cube with the cube above the center cube
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> SolveTopLayerLineUpCenterAsync()
        {
            if (aPieces[4] == aPieces[1])
            {
                // Do nothing
                return true;
            }

            if (aPieces[4] == aPieces[10])
            {
                await MakeTurnAsync("U");
                return true;
            }

            if (aPieces[4] == aPieces[19])
            {
                await MakeTurnAsync("U2");
                return true;
            }

            if (aPieces[4] == aPieces[28])
            {
                await MakeTurnAsync("U'");
                return true;
            }

            if (aPieces[4] == aPieces[1] && aPieces[13] == aPieces[10] && aPieces[22] == aPieces[19] && aPieces[31] == aPieces[28])
            {
                return true;
            }
                
            return false;
        }

        /// <summary>
        /// Swap edges on the top layer
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> SolveTopLayerSwapEdgesAsync()
        {
            // Swap 37 -> 43 -> 37 and 39 -> 41 -> 39
            if (aPieces[4] == aPieces[19] || aPieces[4] == aPieces[37])
            {
                if (aPieces[22] == aPieces[1] || aPieces[22] == aPieces[43])
                {
                    if (aPieces[13] == aPieces[28] || aPieces[13] == aPieces[39])
                    {
                        if (aPieces[31] == aPieces[10] || aPieces[31] == aPieces[41])
                        {
                            await MakeTurnAsync("R2 L2 D R2 L2 U2 R2 L2 D R2 L2");
                            return true;
                        }
                    }
                }
            }

            // Swap 37 -> 41 -> 37 and swap 39 -> 43 -> 39
            if (aPieces[4] == aPieces[28] || aPieces[4] == aPieces[39])
            {
                if (aPieces[31] == aPieces[1] || aPieces[31] == aPieces[43])
                {
                    if (aPieces[13] == aPieces[19] || aPieces[13] == aPieces[37])
                    {
                        if (aPieces[22] == aPieces[10] || aPieces[22] == aPieces[41])
                        {
                            await MakeTurnAsync("R B U B' U' R2 F' U' F U R");
                            return true;
                        }
                    }
                }
            }

            // Rotate 39-41-43 (EFG) clockwise or counter-clockwise
            // If no edges are in the correct position, then execute either sequence
            // This will put one of the pieces into the correct position and then execute the appropriate sequence
            if (aPieces[40] != aPieces[37] && aPieces[40] != aPieces[39] && aPieces[40] != aPieces[41] && aPieces[40] != aPieces[43])
            {
                await MakeTurnAsync("F2 U L R' F2 L' R U F2");
            }

            // If one of the edge pieces is in the correct position, orient the cube so this edge is in the back
            // Then execute one of the sequences below
            if (aPieces[40] == aPieces[39])
            {
                await MakeTurnAsync("y");
            }

            if (aPieces[40] == aPieces[41])
            {
                await MakeTurnAsync("y'");
            }

            if (aPieces[40] == aPieces[43])
            {
                await MakeTurnAsync("y2");
            }

            if (aPieces[40] == aPieces[37])
            {
                // Swap 39 -> 41 -> 43 -> 39 - Rotate EFG clockwise
                if (aPieces[4] == aPieces[10] || aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[28] || aPieces[13] == aPieces[39])
                    {
                        if (aPieces[31] == aPieces[1] || aPieces[31] == aPieces[43])
                        {
                            await MakeTurnAsync("F2 U L R' F2 L' R U F2");
                            return true;
                        }
                    }
                }

                // Swap 39 -> 43 -> 41 -> 39 - Rotate EFG counter-clockwise
                if (aPieces[4] == aPieces[28] || aPieces[4] == aPieces[39])
                {
                    if (aPieces[13] == aPieces[1] || aPieces[13] == aPieces[43])
                    {
                        if (aPieces[31] == aPieces[10] || aPieces[31] == aPieces[41])
                        {
                            await MakeTurnAsync("F2 U' L R' F2 L' R U' F2");
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Swap corners on the top layer
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> SolveTopLayerSwapCornersAsync()
        {
            // Swap 36 -> 38 -> 36
            if (aPieces[13] == aPieces[20] || aPieces[13] == aPieces[27] || aPieces[13] == aPieces[36])
            {
                if (aPieces[22] == aPieces[20] || aPieces[22] == aPieces[27] || aPieces[22] == aPieces[36])
                {
                    if (aPieces[31] == aPieces[11] || aPieces[31] == aPieces[18] || aPieces[31] == aPieces[38])
                    {
                        if (aPieces[22] == aPieces[11] || aPieces[22] == aPieces[18] || aPieces[22] == aPieces[38])
                        {
                            await MakeTurnAsync("F U' B' U F' U' B U2");
                            return true;
                        }
                    }
                }
            }

            // Swap 42 -> 44 -> 42
            if (aPieces[4] == aPieces[0] || aPieces[4] == aPieces[29] || aPieces[4] == aPieces[42])
            {
                if (aPieces[13] == aPieces[0] || aPieces[13] == aPieces[29] || aPieces[13] == aPieces[42])
                {
                    if (aPieces[4] == aPieces[2] || aPieces[4] == aPieces[9] || aPieces[4] == aPieces[44])
                    {
                        if (aPieces[31] == aPieces[2] || aPieces[31] == aPieces[9] || aPieces[31] == aPieces[44])
                        {
                            await MakeTurnAsync("R' F R' B2 R F' R' B2 R2");
                            return true;
                        }
                    }
                }
            }

            // Swap 36 -> 44 -> 36
            if (aPieces[4] == aPieces[20] || aPieces[4] == aPieces[27] || aPieces[4] == aPieces[36])
            {
                if (aPieces[13] == aPieces[20] || aPieces[13] == aPieces[27] || aPieces[13] == aPieces[36])
                {
                    if (aPieces[22] == aPieces[2] || aPieces[22] == aPieces[9] || aPieces[22] == aPieces[44])
                    {
                        if (aPieces[31] == aPieces[2] || aPieces[31] == aPieces[9] || aPieces[31] == aPieces[44])
                        {
                            await MakeTurnAsync("F R U' R' U' R U R' F' R U R' U' R' F R F'");
                            return true;
                        }
                    }
                }
            }

            // Swap 38 -> 42 -> 38
            if (aPieces[4] == aPieces[11] || aPieces[4] == aPieces[18] || aPieces[4] == aPieces[38])
            {
                if (aPieces[31] == aPieces[11] || aPieces[31] == aPieces[18] || aPieces[31] == aPieces[38])
                {
                    if (aPieces[13] == aPieces[0] || aPieces[13] == aPieces[29] || aPieces[13] == aPieces[42])
                    {
                        if (aPieces[22] == aPieces[0] || aPieces[22] == aPieces[29] || aPieces[22] == aPieces[42])
                        {
                            await MakeTurnAsync("U F U R U' R' F'");
                            return true;
                        }
                    }
                }
            }

            // Swap 36 -> 38 -> 42 -> 36
            if (aPieces[4] == aPieces[11] || aPieces[4] == aPieces[18] || aPieces[4] == aPieces[38])
            {
                if (aPieces[31] == aPieces[11] || aPieces[31] == aPieces[18] || aPieces[31] == aPieces[38])
                {
                    if (aPieces[22] == aPieces[0] || aPieces[22] == aPieces[29] || aPieces[22] == aPieces[42])
                    {
                        if (aPieces[31] == aPieces[0] || aPieces[31] == aPieces[29] || aPieces[31] == aPieces[42])
                        {
                            await MakeTurnAsync("L' U R U' L U R' U'");
                            return true;
                        }
                    }
                }
            }

            // Swap 36 -> 42 -> 38 -> 36
            if (aPieces[4] == aPieces[20] || aPieces[4] == aPieces[27] || aPieces[4] == aPieces[36])
            {
                if (aPieces[31] == aPieces[20] || aPieces[31] == aPieces[27] || aPieces[31] == aPieces[36])
                {
                    if (aPieces[22] == aPieces[11] || aPieces[22] == aPieces[18] || aPieces[22] == aPieces[38])
                    {
                        if (aPieces[31] == aPieces[11] || aPieces[31] == aPieces[18] || aPieces[31] == aPieces[38])
                        {
                            await MakeTurnAsync("U R U' L' U R' U' L");
                            return true;
                        }
                    }
                }
            }

            // Swap 36 -> 38 -> 44 -> 36   l' U R' D2 R U' R' D2 R2
            if (aPieces[4] == aPieces[11] || aPieces[4] == aPieces[18] || aPieces[4] == aPieces[38])
            {
                if (aPieces[13] == aPieces[20] || aPieces[13] == aPieces[27] || aPieces[13] == aPieces[36])
                {
                    if (aPieces[22] == aPieces[2] || aPieces[22] == aPieces[9] || aPieces[22] == aPieces[44])
                    {
                        if (aPieces[31] == aPieces[2] || aPieces[31] == aPieces[9] || aPieces[31] == aPieces[44])
                        {
                            await MakeTurnAsync("l' U R' D2 R U' R' D2 R2");
                            return true;
                        }
                    }
                }
            }

            // Swap 36 -> 42 -> 36 and 38 -> 44 -> 38   x' [R U' R' D] [R U R' D'] [R U R' D] [R U' R' D']
            if (aPieces[4] == aPieces[20] || aPieces[4] == aPieces[27] || aPieces[4] == aPieces[36])
            {
                if (aPieces[22] == aPieces[0] || aPieces[22] == aPieces[29] || aPieces[22] == aPieces[42])
                {
                    if (aPieces[4] == aPieces[11] || aPieces[4] == aPieces[18] || aPieces[4] == aPieces[38])
                    {
                        if (aPieces[22] == aPieces[2] || aPieces[22] == aPieces[9] || aPieces[22] == aPieces[44])
                        {
                            await MakeTurnAsync("x' R U' R' D R U R' D' R U R' D R U' R' D'");
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
