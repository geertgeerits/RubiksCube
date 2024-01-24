namespace RubiksCube
{
    internal class ClassSolveCube
    {
        // Solve the cube.
        public async Task<bool> SolveTheCubeAsync()
        {
            // Solve the edges of the top layer - Chapter 4, page 14-3
            await SolveEdgesTopLayerAsync();

            // Solve the edges of the top layer - Chapter 4, page 14-2
            if (Globals.aPieces[40] == Globals.aPieces[3])
            {
                await MakeTurnAsync("TurnLeft+");

                if (Globals.aPieces[34] == Globals.aPieces[4])
                {
                    await MakeTurnAsync("TurnDown+");
                }

                if (Globals.aPieces[34] == Globals.aPieces[22])
                {
                    await MakeTurnAsync("TurnDown-");
                }

                if (Globals.aPieces[34] == Globals.aPieces[13])
                {
                    await MakeTurnAsync("TurnDown++");
                }
            }

            if (Globals.aPieces[40] == Globals.aPieces[5])
            {
                await MakeTurnAsync("TurnRight-");

                if (Globals.aPieces[16] == Globals.aPieces[4])
                {
                    await MakeTurnAsync("TurnDown-");
                }

                if (Globals.aPieces[16] == Globals.aPieces[22])
                {
                    await MakeTurnAsync("TurnDown+");
                }

                if (Globals.aPieces[16] == Globals.aPieces[31])
                {
                    await MakeTurnAsync("TurnDown++");
                }
            }



            // Solve the edges of the top layer - Chapter 4, page 14-3

            await SolveEdgesTopLayerAsync();

            // Solve the corners of the top layer - Chapter 6, page 16

            // Solve the middle layer - Chapter 10, page 21

            // Solve the bottom layer - Chapter 11, page 23

            // Put the edges on the correct place

            // Flip the corners

            // Turning the edges


            // Check if the cube is solved
            if (ClassCheckColorsCube.CheckIfSolved())
            {
                return true;
            }

            return false;
        }

        // Solve the edges of the top layer - Chapter 4, page 14-3
        private async Task SolveEdgesTopLayerAsync()
        {
            for (int nTimes = 1; nTimes < 11; nTimes++)
            {
                if (Globals.aPieces[40] == Globals.aPieces[46] && Globals.aPieces[4] == Globals.aPieces[7])
                {
                    await MakeTurnAsync("TurnFront++");
                }

                if (Globals.aPieces[40] == Globals.aPieces[48] && Globals.aPieces[31] == Globals.aPieces[34])
                {
                    await MakeTurnAsync("TurnLeft++");
                }

                if (Globals.aPieces[40] == Globals.aPieces[50] && Globals.aPieces[13] == Globals.aPieces[16])
                {
                    await MakeTurnAsync("TurnRight++");
                }

                if (Globals.aPieces[40] == Globals.aPieces[52] && Globals.aPieces[22] == Globals.aPieces[25])
                {
                    await MakeTurnAsync("TurnBack++");
                }
            }
        }

        // Make a turn of the cube/face/side
        private async Task MakeTurnAsync(string cTurnFaceAndDirection)
        {
            // Add the turn to the list
            Globals.lCubeTurns.Add(cTurnFaceAndDirection);

            // Turn the cube/face/side
            ClassCubeTurns classCubeTurns = new();
            await classCubeTurns.TurnFaceCubeAsync(cTurnFaceAndDirection);
        }
    }
}
