namespace RubiksCube
{
    internal class ClassTestCubeTurns
    {
        private int nItem;

        // Test the turns of the cube.
        public async Task<bool> TestCubeTurnsAsync()
        {
            nItem = 0;

            // Test the face turns.
            await MakeTurnAsync("TurnFront+");
            await MakeTurnAsync("TurnFront++");
            await MakeTurnAsync("TurnFront-");
            await MakeTurnAsync("TurnFront--");
            await MakeTurnAsync("TurnUp+");
            await MakeTurnAsync("TurnUp++");
            await MakeTurnAsync("TurnUp-");
            await MakeTurnAsync("TurnUp--");
            await MakeTurnAsync("TurnDown+");
            await MakeTurnAsync("TurnDown++");
            await MakeTurnAsync("TurnDown-");
            await MakeTurnAsync("TurnDown--");
            await MakeTurnAsync("TurnLeft+");
            await MakeTurnAsync("TurnLeft++");
            await MakeTurnAsync("TurnLeft-");
            await MakeTurnAsync("TurnLeft--");
            await MakeTurnAsync("TurnRight+");
            await MakeTurnAsync("TurnRight++");
            await MakeTurnAsync("TurnRight-");
            await MakeTurnAsync("TurnRight--");
            await MakeTurnAsync("TurnBack+");
            await MakeTurnAsync("TurnBack++");
            await MakeTurnAsync("TurnBack-");
            await MakeTurnAsync("TurnBack--");

            // Test the middle layer turns.
            await MakeTurnAsync("TurnUpHorMiddleRight+");
            await MakeTurnAsync("TurnUpHorMiddleRight++");
            await MakeTurnAsync("TurnUpHorMiddleLeft-");
            await MakeTurnAsync("TurnUpHorMiddleLeft--");
            await MakeTurnAsync("TurnUpVerMiddleBack+");
            await MakeTurnAsync("TurnUpVerMiddleBack++");
            await MakeTurnAsync("TurnUpVerMiddleFront-");
            await MakeTurnAsync("TurnUpVerMiddleFront--");
            await MakeTurnAsync("TurnFrontHorMiddleLeft+");
            await MakeTurnAsync("TurnFrontHorMiddleLeft++");
            await MakeTurnAsync("TurnFrontHorMiddleRight-");
            await MakeTurnAsync("TurnFrontHorMiddleRight--");

            // Test the cube turns.
            await MakeTurnAsync("TurnCubeFrontToRight");
            await MakeTurnAsync("TurnCubeFrontToLeft");
            await MakeTurnAsync("TurnCubeFrontToUp");
            await MakeTurnAsync("TurnCubeFrontToDown");
            await MakeTurnAsync("TurnCubeUpToRight");
            await MakeTurnAsync("TurnCubeUpToLeft");

            // Check if the cube is solved.
            //if (ClassCheckColorsCube.CheckIfSolved())
            //{
            //    return true;
            //}

            //return false;

            return true;
        }

        // Make a turn of the cube/face/side.
        private async Task MakeTurnAsync(string cTurnFaceAndDirection)
        {
            Globals.aCubeTurns[nItem] = cTurnFaceAndDirection;
            nItem++;

            // Turn the faces of the cube.
            MainPage mainPage = new();
            await mainPage.TurnFaceCubeAsync(cTurnFaceAndDirection);

            // Copy array to array
            //Array.Copy(Globals.aPieces, Globals.aPiecesTemp, 54);


        }
    }
}
