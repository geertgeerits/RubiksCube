namespace RubiksCube
{
    internal class ClassTestCubeTurns
    {
        //// Test the turns of the cube
        public static async Task<bool> TestCubeTurnsAsync()
        {
            // Test the face turns
            await MakeTurnAsync(Globals.turnFrontCW);
            await MakeTurnAsync(Globals.turnFrontCCW);
            await MakeTurnAsync(Globals.turnFront2);
            await MakeTurnAsync(Globals.turnRightCW);
            await MakeTurnAsync(Globals.turnRightCCW);
            await MakeTurnAsync(Globals.turnRight2);
            await MakeTurnAsync(Globals.turnBackCW);
            await MakeTurnAsync(Globals.turnBackCCW);
            await MakeTurnAsync(Globals.turnBack2);
            await MakeTurnAsync(Globals.turnLeftCW);
            await MakeTurnAsync(Globals.turnLeftCCW);
            await MakeTurnAsync(Globals.turnLeft2);
            await MakeTurnAsync(Globals.turnUpCW);
            await MakeTurnAsync(Globals.turnUpCCW);
            await MakeTurnAsync(Globals.turnUp2);
            await MakeTurnAsync(Globals.turnDownCW);
            await MakeTurnAsync(Globals.turnDownCCW);
            await MakeTurnAsync(Globals.turnDown2);

            // Test the middle layer turns
            await MakeTurnAsync(Globals.turnUpHorMiddleRight);
            await MakeTurnAsync(Globals.turnUpHorMiddleLeft);
            await MakeTurnAsync(Globals.turnUpHorMiddle2);
            await MakeTurnAsync(Globals.turnUpVerMiddleBack);
            await MakeTurnAsync(Globals.turnUpVerMiddleFront);
            await MakeTurnAsync(Globals.turnUpVerMiddle2);
            await MakeTurnAsync(Globals.turnFrontHorMiddleLeft);
            await MakeTurnAsync(Globals.turnFrontHorMiddleRight);
            await MakeTurnAsync(Globals.turnFrontHorMiddle2);

            // Test the cube turns
            await MakeTurnAsync(Globals.turnCubeFrontToRight);
            await MakeTurnAsync(Globals.turnCubeFrontToLeft);
            await MakeTurnAsync(Globals.turnCubeFrontToLeft2);
            await MakeTurnAsync(Globals.turnCubeFrontToUp);
            await MakeTurnAsync(Globals.turnCubeFrontToUp2);
            await MakeTurnAsync(Globals.turnCubeFrontToDown);
            await MakeTurnAsync(Globals.turnCubeUpToRight);
            await MakeTurnAsync(Globals.turnCubeUpToRight2);
            await MakeTurnAsync(Globals.turnCubeUpToLeft);

            return true;
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
