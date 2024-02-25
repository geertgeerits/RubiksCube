// This turns are based on:
// the book: Mastering Rubik's Cube, by Don Taylor, Dutch version 1981
// file:///C:/Sources/MAUI/RubiksCube/Miscellaneous/Manuals/RubiksCubeBeginnerInstructions.pdf

using static RubiksCube.Globals;

namespace RubiksCube
{
    internal class ClassSolveCubeCommon
    {
        // Lign up the center cube with the cube above the center cube
        public static async Task<bool> SolveTopLayerLineUpCenterAsync()
        {
            if (aPieces[4] == aPieces[10])
            {
                await MakeTurnWordAsync(turnUpCW);
            }

            if (aPieces[4] == aPieces[19])
            {
                await MakeTurnWordAsync(turnUp2);
            }

            if (aPieces[4] == aPieces[28])
            {
                await MakeTurnWordAsync(turnUpCCW);
            }

            if (aPieces[4] == aPieces[1] && aPieces[13] == aPieces[10] && aPieces[22] == aPieces[19] && aPieces[31] == aPieces[28])
            {
                return true;
            }
                
            return false;
        }
        
        /// Swap edges on the top layer
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
                            await MakeTurnWordAsync(turnRight2);
                            await MakeTurnWordAsync(turnLeft2);
                            await MakeTurnWordAsync(turnDownCW);
                            await MakeTurnWordAsync(turnRight2);
                            await MakeTurnWordAsync(turnLeft2);
                            await MakeTurnWordAsync(turnUp2);
                            await MakeTurnWordAsync(turnRight2);
                            await MakeTurnWordAsync(turnLeft2);
                            await MakeTurnWordAsync(turnDownCW);
                            await MakeTurnWordAsync(turnRight2);
                            await MakeTurnWordAsync(turnLeft2);

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

                            await MakeTurnWordAsync(turnRightCW);
                            await MakeTurnWordAsync(turnBackCW);
                            await MakeTurnWordAsync(turnUpCW);
                            await MakeTurnWordAsync(turnBackCCW);
                            await MakeTurnWordAsync(turnUpCCW);
                            await MakeTurnWordAsync(turnRight2);
                            await MakeTurnWordAsync(turnFrontCCW);
                            await MakeTurnWordAsync(turnUpCCW);
                            await MakeTurnWordAsync(turnFrontCW);
                            await MakeTurnWordAsync(turnUpCW);
                            await MakeTurnWordAsync(turnRightCW);

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
                await MakeTurnWordAsync(turnFront2);
                await MakeTurnWordAsync(turnUpCW);
                await MakeTurnWordAsync(turnLeftCW);
                await MakeTurnWordAsync(turnRightCCW);
                await MakeTurnWordAsync(turnFront2);
                await MakeTurnWordAsync(turnLeftCCW);
                await MakeTurnWordAsync(turnRightCW);
                await MakeTurnWordAsync(turnUpCW);
                await MakeTurnWordAsync(turnFront2);
            }

            // If one of the edge pieces is in the correct position, orient the cube so this edge is in the back
            // Then execute one of the sequences below
            if (aPieces[40] == aPieces[39])
            {
                await MakeTurnWordAsync(turnCubeFrontToLeft);
            }

            if (aPieces[40] == aPieces[41])
            {
                await MakeTurnWordAsync(turnCubeFrontToRight);
            }

            if (aPieces[40] == aPieces[43])
            {
                await MakeTurnWordAsync(turnCubeFrontToLeft2);
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
                            await MakeTurnWordAsync(turnFront2);
                            await MakeTurnWordAsync(turnUpCW);
                            await MakeTurnWordAsync(turnLeftCW);
                            await MakeTurnWordAsync(turnRightCCW);
                            await MakeTurnWordAsync(turnFront2);
                            await MakeTurnWordAsync(turnLeftCCW);
                            await MakeTurnWordAsync(turnRightCW);
                            await MakeTurnWordAsync(turnUpCW);
                            await MakeTurnWordAsync(turnFront2);

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
                            await MakeTurnWordAsync(turnFront2);
                            await MakeTurnWordAsync(turnUpCCW);
                            await MakeTurnWordAsync(turnLeftCW);
                            await MakeTurnWordAsync(turnRightCCW);
                            await MakeTurnWordAsync(turnFront2);
                            await MakeTurnWordAsync(turnLeftCCW);
                            await MakeTurnWordAsync(turnRightCW);
                            await MakeTurnWordAsync(turnUpCCW);
                            await MakeTurnWordAsync(turnFront2);

                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// Swap corners on the top layer
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
                            await MakeTurnWordAsync(turnFrontCW);
                            await MakeTurnWordAsync(turnUpCCW);
                            await MakeTurnWordAsync(turnBackCCW);
                            await MakeTurnWordAsync(turnUpCW);
                            await MakeTurnWordAsync(turnFrontCCW);
                            await MakeTurnWordAsync(turnUpCCW);
                            await MakeTurnWordAsync(turnBackCW);
                            await MakeTurnWordAsync(turnUp2);

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
                            await MakeTurnWordAsync(turnRightCCW);
                            await MakeTurnWordAsync(turnFrontCW);
                            await MakeTurnWordAsync(turnRightCCW);
                            await MakeTurnWordAsync(turnBack2);
                            await MakeTurnWordAsync(turnRightCW);
                            await MakeTurnWordAsync(turnFrontCCW);
                            await MakeTurnWordAsync(turnRightCCW);
                            await MakeTurnWordAsync(turnBack2);
                            await MakeTurnWordAsync(turnRight2);

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
                            await MakeTurnWordAsync(turnFrontCW);
                            await MakeTurnWordAsync(turnRightCW);
                            await MakeTurnWordAsync(turnUpCCW);
                            await MakeTurnWordAsync(turnRightCCW);
                            await MakeTurnWordAsync(turnUpCCW);
                            await MakeTurnWordAsync(turnRightCW);
                            await MakeTurnWordAsync(turnUpCW);
                            await MakeTurnWordAsync(turnRightCCW);
                            await MakeTurnWordAsync(turnFrontCCW);
                            await MakeTurnWordAsync(turnRightCW);
                            await MakeTurnWordAsync(turnUpCW);
                            await MakeTurnWordAsync(turnRightCCW);
                            await MakeTurnWordAsync(turnUpCCW);
                            await MakeTurnWordAsync(turnRightCCW);
                            await MakeTurnWordAsync(turnFrontCW);
                            await MakeTurnWordAsync(turnRightCW);
                            await MakeTurnWordAsync(turnFrontCCW);

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
                            await MakeTurnWordAsync(turnUpCW);
                            await MakeTurnWordAsync(turnFrontCW);
                            await MakeTurnWordAsync(turnUpCW);
                            await MakeTurnWordAsync(turnRightCW);
                            await MakeTurnWordAsync(turnUpCCW);
                            await MakeTurnWordAsync(turnRightCCW);
                            await MakeTurnWordAsync(turnFrontCCW);

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
                            await MakeTurnWordAsync(turnLeftCCW);
                            await MakeTurnWordAsync(turnUpCW);
                            await MakeTurnWordAsync(turnRightCW);
                            await MakeTurnWordAsync(turnUpCCW);
                            await MakeTurnWordAsync(turnLeftCW);
                            await MakeTurnWordAsync(turnUpCW);
                            await MakeTurnWordAsync(turnRightCCW);
                            await MakeTurnWordAsync(turnUpCCW);

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
                            await MakeTurnWordAsync(turnUpCW);
                            await MakeTurnWordAsync(turnRightCW);
                            await MakeTurnWordAsync(turnUpCCW);
                            await MakeTurnWordAsync(turnLeftCCW);
                            await MakeTurnWordAsync(turnUpCW);
                            await MakeTurnWordAsync(turnRightCCW);
                            await MakeTurnWordAsync(turnUpCCW);
                            await MakeTurnWordAsync(turnLeftCW);

                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
