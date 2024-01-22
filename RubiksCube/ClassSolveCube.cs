using System.Diagnostics;

namespace RubiksCube
{
    internal class ClassSolveCube
    {
        // Solve the cube.
        public async Task<bool> SolveTheCubeAsync()
        {
            // Solve the edges of the top layer - Chapter 4, page 14-3.
            await SolveEdgesTopLayerAsync();

            // Solve the edges of the top layer - Chapter 4, page 14-2.
            if (Globals.aUpFace[5] == Globals.aFrontFace[4])
            {
                await MakeTurnAsync("TurnLeft+");

                if (Globals.aLeftFace[8] == Globals.aFrontFace[5])
                {
                    await MakeTurnAsync("TurnDown+");
                }

                if (Globals.aLeftFace[8] == Globals.aBackFace[5])
                {
                    await MakeTurnAsync("TurnDown-");
                }

                if (Globals.aLeftFace[8] == Globals.aRightFace[5])
                {
                    await MakeTurnAsync("TurnDown++");
                }
            }

            if (Globals.aUpFace[5] == Globals.aFrontFace[6])
            {
                await MakeTurnAsync("TurnRight-");

                if (Globals.aRightFace[8] == Globals.aFrontFace[5])
                {
                    await MakeTurnAsync("TurnDown-");
                }

                if (Globals.aRightFace[8] == Globals.aBackFace[5])
                {
                    await MakeTurnAsync("TurnDown+");
                }

                if (Globals.aRightFace[8] == Globals.aLeftFace[5])
                {
                    await MakeTurnAsync("TurnDown++");
                }
            }



            // Solve the edges of the top layer - Chapter 4, page 14-3.

            await SolveEdgesTopLayerAsync();

            // Solve the corners of the top layer - Chapter 6, page 16.

            // Solve the middle layer - Chapter 10, page 21.

            // Solve the bottom layer - Chapter 11, page 23.

            // Put the edges on the correct place.

            // Flip the corners.

            // Turning the edges.


            // Check if the cube is solved.
            if (ClassCheckColorsCube.CheckIfSolved())
            {
                return true;
            }

            return false;
        }

        // Solve the edges of the top layer - Chapter 4, page 14-3.
        private async Task SolveEdgesTopLayerAsync()
        {
            for (int nTimes = 1; nTimes < 11; nTimes++)
            {
                if (Globals.aUpFace[5] == Globals.aDownFace[2] && Globals.aFrontFace[5] == Globals.aFrontFace[8])
                {
                    await MakeTurnAsync("TurnFront++");
                }

                if (Globals.aUpFace[5] == Globals.aDownFace[4] && Globals.aLeftFace[5] == Globals.aLeftFace[8])
                {
                    await MakeTurnAsync("TurnLeft++");
                }

                if (Globals.aUpFace[5] == Globals.aDownFace[6] && Globals.aRightFace[5] == Globals.aRightFace[8])
                {
                    await MakeTurnAsync("TurnRight++");
                }

                if (Globals.aUpFace[5] == Globals.aDownFace[8] && Globals.aBackFace[5] == Globals.aBackFace[8])
                {
                    await MakeTurnAsync("TurnBack++");
                }
            }
        }

        // Make a turn of the cube/face/side.
        private async Task MakeTurnAsync(string cTurnFaceAndDirection)
        {
            // Add the turn to the list.
            Globals.lCubeTurns.Add(cTurnFaceAndDirection);

            // Turn the cube/face/side.
            ClassCubeTurns classCubeTurns = new();
            await classCubeTurns.TurnFaceCubeAsync(cTurnFaceAndDirection);
        }
    }
}
