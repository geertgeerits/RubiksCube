using System.Diagnostics;
using static RubiksCube.Globals;

namespace RubiksCube
{
    internal class ClassTestCubeTurns
    {
        //// Test the turns of the cube
        public static async Task<bool> TestCubeTurnsAsync()
        {
            //await TestCubeTurnsWordAsync();
            await TestCubeTurnsLetterAsync();
            //await TestSolveCubeTurnsCFOP();
            //await TestSolveCubeTurnsBasic();
            //await TestSolveCubeTurnsDaisy();
            //await TestSolveCubeTurnsCross();
            //await TestSolveCubeOnly2Faces();

            return true;
        }

        //// Use turn words to test the turns of the cube
        private static async Task<bool> TestCubeTurnsWordAsync()
        {
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

        //// Use turn letters to test the turns of the cube
        private static async Task<bool> TestCubeTurnsLetterAsync()
        {
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

        //// Test the turns of the cube for the CFOP method
        private static async Task<bool> TestSolveCubeTurnsCFOP()
        {
            await MakeTurnLetterAsync("U2 F' U' F");
            await MakeTurnLetterAsync("U' R U R'");
            await MakeTurnLetterAsync("F' U F");
            await MakeTurnLetterAsync("U' R U");
            await MakeTurnLetterAsync("R U R' y R U' R'");
            await MakeTurnLetterAsync("R' U R");
            await MakeTurnLetterAsync("U' R U' R'");
            await MakeTurnLetterAsync("R' U2 R y U' L' U L");
            await MakeTurnLetterAsync("R U R'");
            await MakeTurnLetterAsync("U2 F' U' R y U' L' U L");
            await MakeTurnLetterAsync("U' R U2 R' U2 R U' R'");
            await MakeTurnLetterAsync("y L' U' L");
            await MakeTurnLetterAsync("y' U R' U2 R y U' R U R'");
            await MakeTurnLetterAsync("R U R'");
            await MakeTurnLetterAsync("F' U' F");
            await MakeTurnLetterAsync("U R U' R'");
            await MakeTurnLetterAsync("U' F' U F");
            await MakeTurnLetterAsync("U' R U' R' U R U R'");
            await MakeTurnLetterAsync("U F' U F U' F' U' F");
            await MakeTurnLetterAsync("U' R U R' U R U R'");
            await MakeTurnLetterAsync("U F' U' F U' F' U' F");
            await MakeTurnLetterAsync("d R' U2 R d' R U R'");
            await MakeTurnLetterAsync("U' R U2 R' d R' U' R");
            await MakeTurnLetterAsync("R U' R' U d R' U' R");
            await MakeTurnLetterAsync("F' U F U' d' F U F'");
            await MakeTurnLetterAsync("U F' U2 F U F' U2 F");
            await MakeTurnLetterAsync("U' R U2 R' U' R U2 R'");
            await MakeTurnLetterAsync("U F' U' F U F' U2 F");
            await MakeTurnLetterAsync("U' R U R' U' R U2 R'");
            await MakeTurnLetterAsync("R U2 R' U' R U R'");
            await MakeTurnLetterAsync("F' U2 F U F' U' F");
            await MakeTurnLetterAsync("U R U2 R' U R U' R'");
            await MakeTurnLetterAsync("U' F' U2 F U' F' U F");
            await MakeTurnLetterAsync("U2 R U R' U R U' R'");
            await MakeTurnLetterAsync("U2 F' U' F U' F' U F");
            await MakeTurnLetterAsync("R U R' U' U' R U R' U' R U R'");
            await MakeTurnLetterAsync("y' R' U' R U U R' U' R U R' U' R");
            await MakeTurnLetterAsync("U F' U F U F' U2 F");
            await MakeTurnLetterAsync("U' R U' R' U' R U2 R'");
            await MakeTurnLetterAsync("U F' U' F d' F U F'");
            await MakeTurnLetterAsync("U' R U R' d R' U' R");
            await MakeTurnLetterAsync("R U' R' d R' U R");
            await MakeTurnLetterAsync("R U R' U' R U R' U' R U R'");
            await MakeTurnLetterAsync("U R U' R' U' F' U F");
            await MakeTurnLetterAsync("U' F' U F U R U' R'");
            await MakeTurnLetterAsync("F' U F U' F' U F");
            await MakeTurnLetterAsync("R U' R' U R U' R'");
            await MakeTurnLetterAsync("R U R' U' R U R'");
            await MakeTurnLetterAsync("F' U' F U F' U' F");
            await MakeTurnLetterAsync("R U' R' U R U2 R' U R U' R'");
            await MakeTurnLetterAsync("R U' R' U' R U R' U' R U2 R'");
            await MakeTurnLetterAsync("R U R' U' R U' R' U d R' U' R");
            await MakeTurnLetterAsync("R U' R' d R' U' R U' R' U' R");
            await MakeTurnLetterAsync("R U' R' d R' U2 R U R' U2 R");

            // Line 704


            return true;
        }

        //// Test the turns of the cube for the Basic method
        private static async Task<bool> TestSolveCubeTurnsBasic()
        {

            return true;
        }

        //// Test the turns of the cube for the Daisy method
        private static async Task<bool> TestSolveCubeTurnsDaisy()
        {

            return true;
        }

        //// Test the turns of the cube for the Cross method
        private static async Task<bool> TestSolveCubeTurnsCross()
        {

            return true;
        }

        //// Test the solution of the cube with the use of only turns of 2 faces L' and U
        private static async Task<bool> TestSolveCubeOnly2Faces()
        {
            int nLoopTimes = 0;

            while (!ClassColorsCube.CheckIfSolved())
            {
                nLoopTimes++;
                if (nLoopTimes > 10000)
                {
                    Debug.WriteLine("nLoopTimes TestSolveCubeOnly2Faces: " + nLoopTimes);
                    return false;
                }
                
                await MakeTurnLetterAsync("L' U");
            }

            return true;
        }
    }
}
