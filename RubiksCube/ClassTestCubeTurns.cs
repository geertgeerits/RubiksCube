using static RubiksCube.Globals;

namespace RubiksCube
{
    internal class ClassTestCubeTurns
    {
        //// Test the turns of the cube
        public static async Task<bool> TestCubeTurnsAsync()
        {
            //// Use turn words to test the turns of the cube
            // Test the face turns
            await MakeTurnWordAsync(turnFrontCW);
            await MakeTurnWordAsync(turnFrontCCW);
            await MakeTurnWordAsync(turnFront2);
            await MakeTurnWordAsync(turnRightCW);
            await MakeTurnWordAsync(turnRightCCW);
            await MakeTurnWordAsync(turnRight2);
            await MakeTurnWordAsync(turnBackCW);
            await MakeTurnWordAsync(turnBackCCW);
            await MakeTurnWordAsync(turnBack2);
            await MakeTurnWordAsync(turnLeftCW);
            await MakeTurnWordAsync(turnLeftCCW);
            await MakeTurnWordAsync(turnLeft2);
            await MakeTurnWordAsync(turnUpCW);
            await MakeTurnWordAsync(turnUpCCW);
            await MakeTurnWordAsync(turnUp2);
            await MakeTurnWordAsync(turnDownCW);
            await MakeTurnWordAsync(turnDownCCW);
            await MakeTurnWordAsync(turnDown2);

            // Test the middle layer turns
            await MakeTurnWordAsync(turnUpHorMiddleRight);
            await MakeTurnWordAsync(turnUpHorMiddleLeft);
            await MakeTurnWordAsync(turnUpHorMiddle2);
            await MakeTurnWordAsync(turnUpVerMiddleBack);
            await MakeTurnWordAsync(turnUpVerMiddleFront);
            await MakeTurnWordAsync(turnUpVerMiddle2);
            await MakeTurnWordAsync(turnFrontHorMiddleLeft);
            await MakeTurnWordAsync(turnFrontHorMiddleRight);
            await MakeTurnWordAsync(turnFrontHorMiddle2);

            // Test two layers at the same time

            // Test the cube turns
            await MakeTurnWordAsync(turnCubeFrontToRight);
            await MakeTurnWordAsync(turnCubeFrontToLeft);
            await MakeTurnWordAsync(turnCubeFrontToLeft2);
            await MakeTurnWordAsync(turnCubeFrontToUp);
            await MakeTurnWordAsync(turnCubeFrontToUp2);
            await MakeTurnWordAsync(turnCubeFrontToDown);
            await MakeTurnWordAsync(turnCubeUpToRight);
            await MakeTurnWordAsync(turnCubeUpToRight2);
            await MakeTurnWordAsync(turnCubeUpToLeft);

            //// Use turn letters to test the turns of the cube
            // Test the face turns
            await MakeTurnLetterAsync("F F' F2");
            await MakeTurnLetterAsync("R R' R2");
            await MakeTurnLetterAsync("B B' B2");
            await MakeTurnLetterAsync("L L' L2");
            await MakeTurnLetterAsync("U U' U2");
            await MakeTurnLetterAsync("D D' D2");

            // Test the middle layer turns
            await MakeTurnLetterAsync("M M' M2");
            await MakeTurnLetterAsync("E E' E2");
            await MakeTurnLetterAsync("S S' S2");

            // Test two layers at the same time
            await MakeTurnLetterAsync("f f' f2");
            await MakeTurnLetterAsync("r r' r2");
            await MakeTurnLetterAsync("b b' b2");
            await MakeTurnLetterAsync("l l' l2");
            await MakeTurnLetterAsync("u u' u2");
            await MakeTurnLetterAsync("d d' d2");

            // Test the cube turns
            await MakeTurnLetterAsync("x x' x2");
            await MakeTurnLetterAsync("y y' y2");
            await MakeTurnLetterAsync("z z' z2");

            // Test single cube turns
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("U'");
            await MakeTurnLetterAsync("U2");

            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("y'");
            await MakeTurnLetterAsync("y2");

            // Test invalid cube turns
            //await MakeTurnLetterAsync("U ");
            //await MakeTurnLetterAsync(" U");
            //await MakeTurnLetterAsync("U RR U");

            return true;
        }
    }
}
