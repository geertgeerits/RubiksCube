namespace RubiksCube
{
    internal class ClassSolveCubeCommon
    {
        private static async Task SolveBottomLayerSwitchCornersAsync()
        {
            // Switch: 36 -> 38 -> 36
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
                        }
                    }
                }
            }

            // Switch: 42 -> 44 -> 42
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
                        }
                    }
                }
            }

            // Switch: 36 -> 44 -> 36
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
                        }
                    }
                }
            }

            // Switch: 38 -> 42 -> 38
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
                        }
                    }
                }
            }

            // Switch: 36 -> 38 -> 42 -> 36
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
                        }
                    }
                }
            }

            // Switch: 36 -> 42 -> 38 -> 36
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
                        }
                    }
                }
            }
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
