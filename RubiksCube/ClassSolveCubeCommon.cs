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
                await MakeTurnAsync(turnUpCW);
            }

            if (aPieces[4] == aPieces[19])
            {
                await MakeTurnAsync(turnUp2);
            }

            if (aPieces[4] == aPieces[28])
            {
                await MakeTurnAsync(turnUpCCW);
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
                            await MakeTurnAsync(turnRight2);
                            await MakeTurnAsync(turnLeft2);
                            await MakeTurnAsync(turnDownCW);
                            await MakeTurnAsync(turnRight2);
                            await MakeTurnAsync(turnLeft2);
                            await MakeTurnAsync(turnUp2);
                            await MakeTurnAsync(turnRight2);
                            await MakeTurnAsync(turnLeft2);
                            await MakeTurnAsync(turnDownCW);
                            await MakeTurnAsync(turnRight2);
                            await MakeTurnAsync(turnLeft2);

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

                            await MakeTurnAsync(turnRightCW);
                            await MakeTurnAsync(turnBackCW);
                            await MakeTurnAsync(turnUpCW);
                            await MakeTurnAsync(turnBackCCW);
                            await MakeTurnAsync(turnUpCCW);
                            await MakeTurnAsync(turnRight2);
                            await MakeTurnAsync(turnFrontCCW);
                            await MakeTurnAsync(turnUpCCW);
                            await MakeTurnAsync(turnFrontCW);
                            await MakeTurnAsync(turnUpCW);
                            await MakeTurnAsync(turnRightCW);

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
                await MakeTurnAsync(turnFront2);
                await MakeTurnAsync(turnUpCW);
                await MakeTurnAsync(turnLeftCW);
                await MakeTurnAsync(turnRightCCW);
                await MakeTurnAsync(turnFront2);
                await MakeTurnAsync(turnLeftCCW);
                await MakeTurnAsync(turnRightCW);
                await MakeTurnAsync(turnUpCW);
                await MakeTurnAsync(turnFront2);
            }

            // If one of the edge pieces is in the correct position, orient the cube so this edge is in the back
            // Then execute one of the sequences below
            if (aPieces[40] == aPieces[39])
            {
                await MakeTurnAsync(turnCubeFrontToLeft);
            }

            if (aPieces[40] == aPieces[41])
            {
                await MakeTurnAsync(turnCubeFrontToRight);
            }

            if (aPieces[40] == aPieces[43])
            {
                await MakeTurnAsync(turnCubeFrontToLeft2);
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
                            await MakeTurnAsync(turnFront2);
                            await MakeTurnAsync(turnUpCW);
                            await MakeTurnAsync(turnLeftCW);
                            await MakeTurnAsync(turnRightCCW);
                            await MakeTurnAsync(turnFront2);
                            await MakeTurnAsync(turnLeftCCW);
                            await MakeTurnAsync(turnRightCW);
                            await MakeTurnAsync(turnUpCW);
                            await MakeTurnAsync(turnFront2);

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
                            await MakeTurnAsync(turnFront2);
                            await MakeTurnAsync(turnUpCCW);
                            await MakeTurnAsync(turnLeftCW);
                            await MakeTurnAsync(turnRightCCW);
                            await MakeTurnAsync(turnFront2);
                            await MakeTurnAsync(turnLeftCCW);
                            await MakeTurnAsync(turnRightCW);
                            await MakeTurnAsync(turnUpCCW);
                            await MakeTurnAsync(turnFront2);

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
                            await MakeTurnAsync(turnFrontCW);
                            await MakeTurnAsync(turnUpCCW);
                            await MakeTurnAsync(turnBackCCW);
                            await MakeTurnAsync(turnUpCW);
                            await MakeTurnAsync(turnFrontCCW);
                            await MakeTurnAsync(turnUpCCW);
                            await MakeTurnAsync(turnBackCW);
                            await MakeTurnAsync(turnUp2);

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
                            await MakeTurnAsync(turnRightCCW);
                            await MakeTurnAsync(turnFrontCW);
                            await MakeTurnAsync(turnRightCCW);
                            await MakeTurnAsync(turnBack2);
                            await MakeTurnAsync(turnRightCW);
                            await MakeTurnAsync(turnFrontCCW);
                            await MakeTurnAsync(turnRightCCW);
                            await MakeTurnAsync(turnBack2);
                            await MakeTurnAsync(turnRight2);

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
                            await MakeTurnAsync(turnFrontCW);
                            await MakeTurnAsync(turnRightCW);
                            await MakeTurnAsync(turnUpCCW);
                            await MakeTurnAsync(turnRightCCW);
                            await MakeTurnAsync(turnUpCCW);
                            await MakeTurnAsync(turnRightCW);
                            await MakeTurnAsync(turnUpCW);
                            await MakeTurnAsync(turnRightCCW);
                            await MakeTurnAsync(turnFrontCCW);
                            await MakeTurnAsync(turnRightCW);
                            await MakeTurnAsync(turnUpCW);
                            await MakeTurnAsync(turnRightCCW);
                            await MakeTurnAsync(turnUpCCW);
                            await MakeTurnAsync(turnRightCCW);
                            await MakeTurnAsync(turnFrontCW);
                            await MakeTurnAsync(turnRightCW);
                            await MakeTurnAsync(turnFrontCCW);

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
                            await MakeTurnAsync(turnUpCW);
                            await MakeTurnAsync(turnFrontCW);
                            await MakeTurnAsync(turnUpCW);
                            await MakeTurnAsync(turnRightCW);
                            await MakeTurnAsync(turnUpCCW);
                            await MakeTurnAsync(turnRightCCW);
                            await MakeTurnAsync(turnFrontCCW);

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
                            await MakeTurnAsync(turnLeftCCW);
                            await MakeTurnAsync(turnUpCW);
                            await MakeTurnAsync(turnRightCW);
                            await MakeTurnAsync(turnUpCCW);
                            await MakeTurnAsync(turnLeftCW);
                            await MakeTurnAsync(turnUpCW);
                            await MakeTurnAsync(turnRightCCW);
                            await MakeTurnAsync(turnUpCCW);

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
                            await MakeTurnAsync(turnUpCW);
                            await MakeTurnAsync(turnRightCW);
                            await MakeTurnAsync(turnUpCCW);
                            await MakeTurnAsync(turnLeftCCW);
                            await MakeTurnAsync(turnUpCW);
                            await MakeTurnAsync(turnRightCCW);
                            await MakeTurnAsync(turnUpCCW);
                            await MakeTurnAsync(turnLeftCW);

                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
