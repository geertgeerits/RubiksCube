namespace RubiksCube
{
    internal class ClassTestCubeTurns
    {
        // Test the turns of the cube.
        public async Task<bool> TestCubeTurnsAsync()
        {
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

            return true;
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
