// This turns are based on:
// the book by Don Taylor, Mastering Rubik's Cube, Dutch version 1981
// file:///C:/Sources/MAUI/RubiksCube/Miscellaneous/Manuals/RubiksCubeBeginnerInstructions.pdf

namespace RubiksCube
{
    internal class ClassSolveCubeCommon
    {
        // Lign up the center cube with the cube above the center cube
        public static async Task<bool> SolveTopLayerLineUpCenterAsync()
        {
            if (Globals.aPieces[4] == Globals.aPieces[10])
            {
                await MakeTurnAsync(Globals.turnUpCW);
            }

            if (Globals.aPieces[4] == Globals.aPieces[19])
            {
                await MakeTurnAsync(Globals.turnUp2);
            }

            if (Globals.aPieces[4] == Globals.aPieces[28])
            {
                await MakeTurnAsync(Globals.turnUpCCW);
            }

            if (Globals.aPieces[4] == Globals.aPieces[1] && Globals.aPieces[13] == Globals.aPieces[10] && Globals.aPieces[22] == Globals.aPieces[19] && Globals.aPieces[31] == Globals.aPieces[28])
            {
                return true;
            }
                
            return false;
        }
        
        /// Swap edges on the top layer
        public static async Task<bool> SolveTopLayerSwapEdgesAsync()
        {
            // Swap: 39 -> 41 -> 43 -> 39 - Rotate EFG clockwise
            if (Globals.aPieces[4] == Globals.aPieces[10] || Globals.aPieces[4] == Globals.aPieces[41])
            {
                if (Globals.aPieces[13] == Globals.aPieces[28] || Globals.aPieces[13] == Globals.aPieces[39])
                {
                    if (Globals.aPieces[31] == Globals.aPieces[1] || Globals.aPieces[31] == Globals.aPieces[43])
                    {
                        await MakeTurnAsync(Globals.turnFront2);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnLeftCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnFront2);
                        await MakeTurnAsync(Globals.turnLeftCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnFront2);

                        return true;
                    }
                }
            }

            // Swap: 39 -> 43 -> 41 -> 39 - Rotate EFG counter-clockwise
            if (Globals.aPieces[4] == Globals.aPieces[28] || Globals.aPieces[4] == Globals.aPieces[39])
            {
                if (Globals.aPieces[13] == Globals.aPieces[1] || Globals.aPieces[13] == Globals.aPieces[43])
                {
                    if (Globals.aPieces[31] == Globals.aPieces[10] || Globals.aPieces[31] == Globals.aPieces[41])
                    {
                        await MakeTurnAsync(Globals.turnFront2);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnLeftCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                        await MakeTurnAsync(Globals.turnFront2);
                        await MakeTurnAsync(Globals.turnLeftCCW);
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnFront2);

                        return true;
                    }
                }
            }

            // Swap: 37 -> 43 -> 37 and 39 -> 41 -> 39
            if (Globals.aPieces[4] == Globals.aPieces[19] || Globals.aPieces[4] == Globals.aPieces[37])
            {
                if (Globals.aPieces[22] == Globals.aPieces[1] || Globals.aPieces[22] == Globals.aPieces[43])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[28] || Globals.aPieces[13] == Globals.aPieces[39])
                    {
                        if (Globals.aPieces[31] == Globals.aPieces[10] || Globals.aPieces[31] == Globals.aPieces[41])
                        {
                            await MakeTurnAsync(Globals.turnRight2);
                            await MakeTurnAsync(Globals.turnLeft2);
                            await MakeTurnAsync(Globals.turnDownCW);
                            await MakeTurnAsync(Globals.turnRight2);
                            await MakeTurnAsync(Globals.turnLeft2);
                            await MakeTurnAsync(Globals.turnUp2);
                            await MakeTurnAsync(Globals.turnRight2);
                            await MakeTurnAsync(Globals.turnLeft2);
                            await MakeTurnAsync(Globals.turnDownCW);
                            await MakeTurnAsync(Globals.turnRight2);
                            await MakeTurnAsync(Globals.turnLeft2);

                            return true;
                        }
                    }
                }
            }

            // Swap: 37 -> 41 -> 37 and Swap: 39 -> 43 -> 39
            if (Globals.aPieces[4] == Globals.aPieces[28] || Globals.aPieces[4] == Globals.aPieces[39])
            {
                if (Globals.aPieces[31] == Globals.aPieces[1] || Globals.aPieces[31] == Globals.aPieces[43])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[19] || Globals.aPieces[13] == Globals.aPieces[37])
                    {
                        if (Globals.aPieces[22] == Globals.aPieces[10] || Globals.aPieces[22] == Globals.aPieces[41])
                        {
                        
                            await MakeTurnAsync(Globals.turnRightCW);
                            await MakeTurnAsync(Globals.turnBackCW);
                            await MakeTurnAsync(Globals.turnUpCW);
                            await MakeTurnAsync(Globals.turnBackCCW);
                            await MakeTurnAsync(Globals.turnUpCCW);
                            await MakeTurnAsync(Globals.turnRight2);
                            await MakeTurnAsync(Globals.turnFrontCCW);
                            await MakeTurnAsync(Globals.turnUpCCW);
                            await MakeTurnAsync(Globals.turnFrontCW);
                            await MakeTurnAsync(Globals.turnUpCW);
                            await MakeTurnAsync(Globals.turnRightCW);

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
            // Swap: 36 -> 38 -> 36
            if (Globals.aPieces[13] == Globals.aPieces[20] || Globals.aPieces[13] == Globals.aPieces[27] || Globals.aPieces[13] == Globals.aPieces[36])
            {
                if (Globals.aPieces[22] == Globals.aPieces[20] || Globals.aPieces[22] == Globals.aPieces[27] || Globals.aPieces[22] == Globals.aPieces[36])
                {
                    if (Globals.aPieces[31] == Globals.aPieces[11] || Globals.aPieces[31] == Globals.aPieces[18] || Globals.aPieces[31] == Globals.aPieces[38])
                    {
                        if (Globals.aPieces[22] == Globals.aPieces[11] || Globals.aPieces[22] == Globals.aPieces[18] || Globals.aPieces[22] == Globals.aPieces[38])
                        {
                            await MakeTurnAsync(Globals.turnFrontCW);
                            await MakeTurnAsync(Globals.turnUpCCW);
                            await MakeTurnAsync(Globals.turnBackCCW);
                            await MakeTurnAsync(Globals.turnUpCW);
                            await MakeTurnAsync(Globals.turnFrontCCW);
                            await MakeTurnAsync(Globals.turnUpCCW);
                            await MakeTurnAsync(Globals.turnBackCW);
                            await MakeTurnAsync(Globals.turnUp2);

                            return true;
                        }
                    }
                }
            }

            // Swap: 42 -> 44 -> 42
            if (Globals.aPieces[4] == Globals.aPieces[0] || Globals.aPieces[4] == Globals.aPieces[29] || Globals.aPieces[4] == Globals.aPieces[42])
            {
                if (Globals.aPieces[13] == Globals.aPieces[0] || Globals.aPieces[13] == Globals.aPieces[29] || Globals.aPieces[13] == Globals.aPieces[42])
                {
                    if (Globals.aPieces[4] == Globals.aPieces[2] || Globals.aPieces[4] == Globals.aPieces[9] || Globals.aPieces[4] == Globals.aPieces[44])
                    {
                        if (Globals.aPieces[31] == Globals.aPieces[2] || Globals.aPieces[31] == Globals.aPieces[9] || Globals.aPieces[31] == Globals.aPieces[44])
                        {
                            await MakeTurnAsync(Globals.turnRightCCW);
                            await MakeTurnAsync(Globals.turnFrontCW);
                            await MakeTurnAsync(Globals.turnRightCCW);
                            await MakeTurnAsync(Globals.turnBack2);
                            await MakeTurnAsync(Globals.turnRightCW);
                            await MakeTurnAsync(Globals.turnFrontCCW);
                            await MakeTurnAsync(Globals.turnRightCCW);
                            await MakeTurnAsync(Globals.turnBack2);
                            await MakeTurnAsync(Globals.turnRight2);

                            return true;
                        }
                    }
                }
            }

            // Swap: 36 -> 44 -> 36
            if (Globals.aPieces[4] == Globals.aPieces[20] || Globals.aPieces[4] == Globals.aPieces[27] || Globals.aPieces[4] == Globals.aPieces[36])
            {
                if (Globals.aPieces[13] == Globals.aPieces[20] || Globals.aPieces[13] == Globals.aPieces[27] || Globals.aPieces[13] == Globals.aPieces[36])
                {
                    if (Globals.aPieces[22] == Globals.aPieces[2] || Globals.aPieces[22] == Globals.aPieces[9] || Globals.aPieces[22] == Globals.aPieces[44])
                    {
                        if (Globals.aPieces[31] == Globals.aPieces[2] || Globals.aPieces[31] == Globals.aPieces[9] || Globals.aPieces[31] == Globals.aPieces[44])
                        {
                            await MakeTurnAsync(Globals.turnFrontCW);
                            await MakeTurnAsync(Globals.turnRightCW);
                            await MakeTurnAsync(Globals.turnUpCCW);
                            await MakeTurnAsync(Globals.turnRightCCW);
                            await MakeTurnAsync(Globals.turnUpCCW);
                            await MakeTurnAsync(Globals.turnRightCW);
                            await MakeTurnAsync(Globals.turnUpCW);
                            await MakeTurnAsync(Globals.turnRightCCW);
                            await MakeTurnAsync(Globals.turnFrontCCW);
                            await MakeTurnAsync(Globals.turnRightCW);
                            await MakeTurnAsync(Globals.turnUpCW);
                            await MakeTurnAsync(Globals.turnRightCCW);
                            await MakeTurnAsync(Globals.turnUpCCW);
                            await MakeTurnAsync(Globals.turnRightCCW);
                            await MakeTurnAsync(Globals.turnFrontCW);
                            await MakeTurnAsync(Globals.turnRightCW);
                            await MakeTurnAsync(Globals.turnFrontCCW);

                            return true;
                        }
                    }
                }
            }

            // Swap: 38 -> 42 -> 38
            if (Globals.aPieces[4] == Globals.aPieces[11] || Globals.aPieces[4] == Globals.aPieces[18] || Globals.aPieces[4] == Globals.aPieces[38])
            {
                if (Globals.aPieces[31] == Globals.aPieces[11] || Globals.aPieces[31] == Globals.aPieces[18] || Globals.aPieces[31] == Globals.aPieces[38])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[0] || Globals.aPieces[13] == Globals.aPieces[29] || Globals.aPieces[13] == Globals.aPieces[42])
                    {
                        if (Globals.aPieces[22] == Globals.aPieces[0] || Globals.aPieces[22] == Globals.aPieces[29] || Globals.aPieces[22] == Globals.aPieces[42])
                        {
                            await MakeTurnAsync(Globals.turnUpCW);
                            await MakeTurnAsync(Globals.turnFrontCW);
                            await MakeTurnAsync(Globals.turnUpCW);
                            await MakeTurnAsync(Globals.turnRightCW);
                            await MakeTurnAsync(Globals.turnUpCCW);
                            await MakeTurnAsync(Globals.turnRightCCW);
                            await MakeTurnAsync(Globals.turnFrontCCW);

                            return true;
                        }
                    }
                }
            }

            // Swap: 36 -> 38 -> 42 -> 36
            if (Globals.aPieces[4] == Globals.aPieces[11] || Globals.aPieces[4] == Globals.aPieces[18] || Globals.aPieces[4] == Globals.aPieces[38])
            {
                if (Globals.aPieces[31] == Globals.aPieces[11] || Globals.aPieces[31] == Globals.aPieces[18] || Globals.aPieces[31] == Globals.aPieces[38])
                {
                    if (Globals.aPieces[22] == Globals.aPieces[0] || Globals.aPieces[22] == Globals.aPieces[29] || Globals.aPieces[22] == Globals.aPieces[42])
                    {
                        if (Globals.aPieces[31] == Globals.aPieces[0] || Globals.aPieces[31] == Globals.aPieces[29] || Globals.aPieces[31] == Globals.aPieces[42])
                        {
                            await MakeTurnAsync(Globals.turnLeftCCW);
                            await MakeTurnAsync(Globals.turnUpCW);
                            await MakeTurnAsync(Globals.turnRightCW);
                            await MakeTurnAsync(Globals.turnUpCCW);
                            await MakeTurnAsync(Globals.turnLeftCW);
                            await MakeTurnAsync(Globals.turnUpCW);
                            await MakeTurnAsync(Globals.turnRightCCW);
                            await MakeTurnAsync(Globals.turnUpCCW);

                            return true;
                        }
                    }
                }
            }

            // Swap: 36 -> 42 -> 38 -> 36
            if (Globals.aPieces[4] == Globals.aPieces[20] || Globals.aPieces[4] == Globals.aPieces[27] || Globals.aPieces[4] == Globals.aPieces[36])
            {
                if (Globals.aPieces[31] == Globals.aPieces[20] || Globals.aPieces[31] == Globals.aPieces[27] || Globals.aPieces[31] == Globals.aPieces[36])
                {
                    if (Globals.aPieces[22] == Globals.aPieces[11] || Globals.aPieces[22] == Globals.aPieces[18] || Globals.aPieces[22] == Globals.aPieces[38])
                    {
                        if (Globals.aPieces[31] == Globals.aPieces[11] || Globals.aPieces[31] == Globals.aPieces[18] || Globals.aPieces[31] == Globals.aPieces[38])
                        {
                            await MakeTurnAsync(Globals.turnUpCW);
                            await MakeTurnAsync(Globals.turnRightCW);
                            await MakeTurnAsync(Globals.turnUpCCW);
                            await MakeTurnAsync(Globals.turnLeftCCW);
                            await MakeTurnAsync(Globals.turnUpCW);
                            await MakeTurnAsync(Globals.turnRightCCW);
                            await MakeTurnAsync(Globals.turnUpCCW);
                            await MakeTurnAsync(Globals.turnLeftCW);

                            return true;
                        }
                    }
                }
            }

            return false;
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
