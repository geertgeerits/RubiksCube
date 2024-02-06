namespace RubiksCube
{
    internal class ClassSolveCubeNew
    {
        // Declare variables
        //private const int nLoopTimesMax = 2000;

        // Solve the cube.
        public static async Task<bool> SolveTheCubeNewAsync()
        {
            if (!await SolveTopLayerEdgesAsync())
            {
                return false;
            }

            //if (!await SolveTopLayerCornersAsync())
            //{
            //    return false;
            //}

            //if (!await SolveMiddleLayerAsync())
            //{
            //    return false;
            //}

            //if (!await SolveBottomLayerCornersAsync())
            //{
            //    return false;
            //}

            //if (!await SolveBottomLayerEdgesAsync())
            //{
            //    return false;
            //}

            //if (!await SolveBottomLayerTumblingCornersAsync())
            //{
            //    return false;
            //}

            //if (!await SolveBottomLayerTumblingEdgesAsync())
            //{
            //    return false;
            //}

            // Check if the cube is solved
            if (ClassColorsCube.CheckIfSolved())
            {
                return true;
            }

            return false;
        }

        // Solve the edges of the top layer - Chapter 4, page 14-3
        private static async Task<bool> SolveTopLayerEdgesAsync()
        {
            await SolveEdgesTopLayerAsync();

            // Solve the edges of the top layer - Chapter 4, page 14-2
            if (Globals.aPieces[40] == Globals.aPieces[3])
            {
                await MakeTurnAsync(Globals.turnLeftCW);

                if (Globals.aPieces[34] == Globals.aPieces[4])
                {
                    await MakeTurnAsync(Globals.turnDownCW);
                }

                if (Globals.aPieces[34] == Globals.aPieces[22])
                {
                    await MakeTurnAsync(Globals.turnDownCCW);
                }

                if (Globals.aPieces[34] == Globals.aPieces[13])
                {
                    await MakeTurnAsync(Globals.turnDown2);
                }
            }

            if (Globals.aPieces[40] == Globals.aPieces[5])
            {
                await MakeTurnAsync(Globals.turnRightCCW);

                if (Globals.aPieces[16] == Globals.aPieces[4])
                {
                    await MakeTurnAsync(Globals.turnDownCCW);
                }

                if (Globals.aPieces[16] == Globals.aPieces[22])
                {
                    await MakeTurnAsync(Globals.turnDownCW);
                }

                if (Globals.aPieces[16] == Globals.aPieces[31])
                {
                    await MakeTurnAsync(Globals.turnDown2);
                }
            }

            // Solve the edges of the top layer - Chapter 4, page 14-3

            await SolveEdgesTopLayerAsync();

            // Check if the cube is solved
            if (ClassColorsCube.CheckIfSolved())
            {
                return true;
            }

            return false;

        }


        // Solve the corners of the top layer - Chapter 6, page 16

        // Solve the middle layer - Chapter 10, page 21

        // Solve the bottom layer - Chapter 11, page 23

        // Put the edges on the correct place

        // Flip the corners

        // Turning the edges


        // Solve the edges of the top layer - Chapter 4, page 14-3
        private static async Task SolveEdgesTopLayerAsync()
        {
            for (int nTimes = 1; nTimes < 11; nTimes++)
            {
                if (Globals.aPieces[40] == Globals.aPieces[46] && Globals.aPieces[4] == Globals.aPieces[7])
                {
                    await MakeTurnAsync(Globals.turnFront2);
                }

                if (Globals.aPieces[40] == Globals.aPieces[48] && Globals.aPieces[31] == Globals.aPieces[34])
                {
                    await MakeTurnAsync(Globals.turnLeft2);
                }

                if (Globals.aPieces[40] == Globals.aPieces[50] && Globals.aPieces[13] == Globals.aPieces[16])
                {
                    await MakeTurnAsync(Globals.turnRight2);
                }

                if (Globals.aPieces[40] == Globals.aPieces[52] && Globals.aPieces[22] == Globals.aPieces[25])
                {
                    await MakeTurnAsync(Globals.turnBack2);
                }
            }
        }

        // Make a turn of the cube/face/side
        private static async Task MakeTurnAsync(string cTurnFaceAndDirection)
        {
            // Add the turn to the list
            Globals.lCubeTurns.Add(cTurnFaceAndDirection);

            // Turn the cube/face/side
            await ClassCubeTurns.TurnFaceCubeAsync(cTurnFaceAndDirection);
        }
    }
}
