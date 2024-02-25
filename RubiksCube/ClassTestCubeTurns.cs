using static RubiksCube.Globals;

namespace RubiksCube
{
    internal class ClassTestCubeTurns
    {
        //// Test the turns of the cube
        public static async Task<bool> TestCubeTurnsAsync()
        {
            // Test the face turns
            await MakeTurnAsync(turnFrontCW);
            await MakeTurnAsync(turnFrontCCW);
            await MakeTurnAsync(turnFront2);
            await MakeTurnAsync(turnRightCW);
            await MakeTurnAsync(turnRightCCW);
            await MakeTurnAsync(turnRight2);
            await MakeTurnAsync(turnBackCW);
            await MakeTurnAsync(turnBackCCW);
            await MakeTurnAsync(turnBack2);
            await MakeTurnAsync(turnLeftCW);
            await MakeTurnAsync(turnLeftCCW);
            await MakeTurnAsync(turnLeft2);
            await MakeTurnAsync(turnUpCW);
            await MakeTurnAsync(turnUpCCW);
            await MakeTurnAsync(turnUp2);
            await MakeTurnAsync(turnDownCW);
            await MakeTurnAsync(turnDownCCW);
            await MakeTurnAsync(turnDown2);

            // Test the middle layer turns
            await MakeTurnAsync(turnUpHorMiddleRight);
            await MakeTurnAsync(turnUpHorMiddleLeft);
            await MakeTurnAsync(turnUpHorMiddle2);
            await MakeTurnAsync(turnUpVerMiddleBack);
            await MakeTurnAsync(turnUpVerMiddleFront);
            await MakeTurnAsync(turnUpVerMiddle2);
            await MakeTurnAsync(turnFrontHorMiddleLeft);
            await MakeTurnAsync(turnFrontHorMiddleRight);
            await MakeTurnAsync(turnFrontHorMiddle2);

            // Test the cube turns
            await MakeTurnAsync(turnCubeFrontToRight);
            await MakeTurnAsync(turnCubeFrontToLeft);
            await MakeTurnAsync(turnCubeFrontToLeft2);
            await MakeTurnAsync(turnCubeFrontToUp);
            await MakeTurnAsync(turnCubeFrontToUp2);
            await MakeTurnAsync(turnCubeFrontToDown);
            await MakeTurnAsync(turnCubeUpToRight);
            await MakeTurnAsync(turnCubeUpToRight2);
            await MakeTurnAsync(turnCubeUpToLeft);

            return true;
        }

        // Make a turn of the cube/face/side
        private static async Task MakeTurnAsync(string cTurn)
        {
            // Add the turn to the list
            lCubeTurns.Add(cTurn);

            // Turn the cube/face/side
            await ClassCubeTurns.TurnFaceCubeAsync(cTurn);
        }
    }
}
