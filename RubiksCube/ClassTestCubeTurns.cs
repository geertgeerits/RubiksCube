namespace RubiksCube
{
    internal class ClassTestCubeTurns
    {
        // Test the turns of the cube
        public async Task<bool> TestCubeTurnsAsync()
        {
            // Test the face turns
            await MakeTurnAsync(Globals.TurnFrontCW);
            await MakeTurnAsync(Globals.TurnFrontCCW);
            await MakeTurnAsync(Globals.TurnFront2);
            await MakeTurnAsync(Globals.TurnRightCW);
            await MakeTurnAsync(Globals.TurnRightCCW);
            await MakeTurnAsync(Globals.TurnRight2);
            await MakeTurnAsync(Globals.TurnBackCW);
            await MakeTurnAsync(Globals.TurnBackCCW);
            await MakeTurnAsync(Globals.TurnBack2);
            await MakeTurnAsync(Globals.TurnLeftCW);
            await MakeTurnAsync(Globals.TurnLeftCCW);
            await MakeTurnAsync(Globals.TurnLeft2);
            await MakeTurnAsync(Globals.TurnUpCW);
            await MakeTurnAsync(Globals.TurnUpCCW);
            await MakeTurnAsync(Globals.TurnUp2);
            await MakeTurnAsync(Globals.TurnDownCW);
            await MakeTurnAsync(Globals.TurnDownCCW);
            await MakeTurnAsync(Globals.TurnDown2);

            // Test the middle layer turns
            await MakeTurnAsync(Globals.TurnUpHorMiddleRight);
            await MakeTurnAsync(Globals.TurnUpHorMiddleLeft);
            await MakeTurnAsync(Globals.TurnUpHorMiddle2);
            await MakeTurnAsync(Globals.TurnUpVerMiddleBack);
            await MakeTurnAsync(Globals.TurnUpVerMiddleFront);
            await MakeTurnAsync(Globals.TurnUpVerMiddle2);
            await MakeTurnAsync(Globals.TurnFrontHorMiddleLeft);
            await MakeTurnAsync(Globals.TurnFrontHorMiddleRight);
            await MakeTurnAsync(Globals.TurnFrontHorMiddle2);

            // Test the cube turns
            await MakeTurnAsync(Globals.TurnCubeFrontToRight);
            await MakeTurnAsync(Globals.TurnCubeFrontToLeft);
            await MakeTurnAsync(Globals.TurnCubeFrontToLeft2);
            await MakeTurnAsync(Globals.TurnCubeFrontToUp);
            await MakeTurnAsync(Globals.TurnCubeFrontToUp2);
            await MakeTurnAsync(Globals.TurnCubeFrontToDown);
            await MakeTurnAsync(Globals.TurnCubeUpToRight);
            await MakeTurnAsync(Globals.TurnCubeUpToRight2);
            await MakeTurnAsync(Globals.TurnCubeUpToLeft);

            return true;
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
