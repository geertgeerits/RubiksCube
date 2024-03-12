using System.Diagnostics;
using static RubiksCube.Globals;

namespace RubiksCube
{
    internal class ClassTestCubeTurns
    {
        //// Test the turns of the cube
        public static async Task<bool> TestCubeTurnsAsync()
        {
            //await TestCubeTurnsLetterAsync();
            await TestCubeTurnsToCleanAsync();
            //await TestSolveCubeTurnsCFOP();
            //await TestSolveCubeTurnsBasic();
            //await TestSolveCubeTurnsDaisy();
            //await TestSolveCubeTurnsCross();
            //await TestSolveCubeOnly2Faces();

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
            await MakeTurnLetterAsync("U ");
            await MakeTurnLetterAsync(" U");
            await MakeTurnLetterAsync("U RR R2' U");

            return true;
        }

        //// Test the cleaning process of the turns
        private static async Task<bool> TestCubeTurnsToCleanAsync()
        {
            await MakeTurnLetterAsync("R");
            await MakeTurnLetterAsync("L");

            await MakeTurnLetterAsync("U U");       // -> U2
            await MakeTurnLetterAsync("U' U'");     // -> U2
            await MakeTurnLetterAsync("U2 U2");     // -> None
            
            await MakeTurnLetterAsync("U U'");      // -> None
            await MakeTurnLetterAsync("U' U");      // -> None
            await MakeTurnLetterAsync("U U2");      // -> U'
            await MakeTurnLetterAsync("U2 U");      // -> U'
            await MakeTurnLetterAsync("U' U2");     // -> U
            await MakeTurnLetterAsync("U2 U'");     // -> U
            
            await MakeTurnLetterAsync("F");
            await MakeTurnLetterAsync("x");
            await MakeTurnLetterAsync("y'");

            ClassSolveCubeMain.CleanListCubeTurns();

            return true;
        }

        //// Test the turns of the cube for the CFOP method
        private static async Task<bool> TestSolveCubeTurnsCFOP()
        {
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("U2");
            await MakeTurnLetterAsync("U'");
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
            await MakeTurnLetterAsync("R U R'");
            await MakeTurnLetterAsync("F' U' F");
            await MakeTurnLetterAsync("U R U' R'");
            await MakeTurnLetterAsync("U' F' U F");
            await MakeTurnLetterAsync("U' R U' R' U");
            await MakeTurnLetterAsync("U F' U F U'");
            await MakeTurnLetterAsync("U' R U R' U");
            await MakeTurnLetterAsync("U F' U' F U'");
            await MakeTurnLetterAsync("U F' U2 F U'");
            await MakeTurnLetterAsync("U' R U2 R' U");
            await MakeTurnLetterAsync("U F' U2 F U'");
            await MakeTurnLetterAsync("U' R U2 R' U");
            await MakeTurnLetterAsync("U F' U' F U'");
            await MakeTurnLetterAsync("U' R U R' U");
            await MakeTurnLetterAsync("R U' R'");
            await MakeTurnLetterAsync("F' U F");
            await MakeTurnLetterAsync("R U2 R'");
            await MakeTurnLetterAsync("F' U2 F");
            await MakeTurnLetterAsync("U R U2 R'");
            await MakeTurnLetterAsync("U' F' U2 F");
            await MakeTurnLetterAsync("U2 R U R'");
            await MakeTurnLetterAsync("U2 F' U' F");
            await MakeTurnLetterAsync("U R U' R'");
            await MakeTurnLetterAsync("U' F' U F");
            await MakeTurnLetterAsync("F' U F");
            await MakeTurnLetterAsync("R U' R'");
            await MakeTurnLetterAsync("R U R'");
            await MakeTurnLetterAsync("F' U' F");
            await MakeTurnLetterAsync("U F' U F");
            await MakeTurnLetterAsync("U' R U' R'");
            await MakeTurnLetterAsync("U F' U' F");
            await MakeTurnLetterAsync("U' R U R'");
            await MakeTurnLetterAsync("R U2 R'");
            await MakeTurnLetterAsync("R U R' U' R U R'");
            await MakeTurnLetterAsync("R U2 R U R' U R U2 R2");
            await MakeTurnLetterAsync("R2 U2 R' U' R U' R' U2 R'");
            await MakeTurnLetterAsync("R U' R' F' L' U2 L F");
            await MakeTurnLetterAsync("R U' R U y' L U' L' B2");
            await MakeTurnLetterAsync("U F' U F U'");
            await MakeTurnLetterAsync("U' R U R' U");
            await MakeTurnLetterAsync("R U' R'");
            await MakeTurnLetterAsync("R U R' U'");
            await MakeTurnLetterAsync("U R' D' F D R");
            await MakeTurnLetterAsync("R' U' R' U R");
            await MakeTurnLetterAsync("y L' R U' L R'");
            await MakeTurnLetterAsync("L' R U L R'");
            await MakeTurnLetterAsync("R U2 R2 U R");
            await MakeTurnLetterAsync("y' R' U2 R2 U' R'");
            await MakeTurnLetterAsync("R' U' R2 U R'");
            await MakeTurnLetterAsync("y' R U R2 U' R");
            await MakeTurnLetterAsync("R2 U' R2 U R2");
            await MakeTurnLetterAsync("y' R2 U R2 U' R2");
            await MakeTurnLetterAsync("R U R'");
            await MakeTurnLetterAsync("F' U' F");
            await MakeTurnLetterAsync("U' F' U F");
            await MakeTurnLetterAsync("U R U' R'");
            await MakeTurnLetterAsync("U R U' R' U' F' U F");
            await MakeTurnLetterAsync("U' F' U F U R U' R'");
            await MakeTurnLetterAsync("F' U F U' F' U F");
            await MakeTurnLetterAsync("R U R' U' R U R'");
            await MakeTurnLetterAsync("R U' R' U R U' R'");
            await MakeTurnLetterAsync("F' U' F U F' U' F");
            await MakeTurnLetterAsync("R U R' U' R U R' U' R U R'");
            await MakeTurnLetterAsync("R U' R' d R' U R");
            await MakeTurnLetterAsync("U F' U F U F' U2 F");
            await MakeTurnLetterAsync("U F' U' F d' F U F'");
            await MakeTurnLetterAsync("U' R U' R' U' R U2 R'");
            await MakeTurnLetterAsync("U' R U R' d R' U' R");
            await MakeTurnLetterAsync("R U' R' U d R' U' R");
            await MakeTurnLetterAsync("F' U F U' d' F U F'");
            await MakeTurnLetterAsync("U F' U2 F U F' U2 F");
            await MakeTurnLetterAsync("U' R U2 R' U' R U2 R'");
            await MakeTurnLetterAsync("U F' U' F U F' U2 F");
            await MakeTurnLetterAsync("U' R U R' U' R U2 R'");
            await MakeTurnLetterAsync("U' R U' R' U R U R'");
            await MakeTurnLetterAsync("U F' U F U' F' U' F");
            await MakeTurnLetterAsync("U' R U R' U R U R'");
            await MakeTurnLetterAsync("U F' U' F U' F' U' F");
            await MakeTurnLetterAsync("U F' U2 F U' R U R'");
            await MakeTurnLetterAsync("U' R U2 R' U F' U' F");
            await MakeTurnLetterAsync("R U R' U' U' R U R' U' R U R'");
            await MakeTurnLetterAsync("y' R' U' R U U R' U' R U R' U' R");
            await MakeTurnLetterAsync("U2 R U R' U R U' R'");
            await MakeTurnLetterAsync("U2 F' U' F U' F' U F");
            await MakeTurnLetterAsync("U R U2 R' U R U' R'");
            await MakeTurnLetterAsync("U' F' U2 F U' F' U F");
            await MakeTurnLetterAsync("R U2 R' U' R U R'");
            await MakeTurnLetterAsync("F' U2 F U F' U' F");
            await MakeTurnLetterAsync("R U' R' d R' U2 R U R' U2 R");
            await MakeTurnLetterAsync("R U' R' U R U2 R' U R U' R'");
            await MakeTurnLetterAsync("U R U' R'");
            await MakeTurnLetterAsync("F R' F' R");
            await MakeTurnLetterAsync("U' R U R' U2 R U' R'");
            await MakeTurnLetterAsync("d R' U' R U2 R' U R");
            await MakeTurnLetterAsync("U' R U2 R' U2 R U' R'");
            await MakeTurnLetterAsync("R' F R F'");
            await MakeTurnLetterAsync("y L' U L U2 y R U R'");
            await MakeTurnLetterAsync("R U' R' U2 F' U' F");
            await MakeTurnLetterAsync("y' R' U' R");
            await MakeTurnLetterAsync("R U R'");
            await MakeTurnLetterAsync("d R' U' R U' R' U' R");
            await MakeTurnLetterAsync("U' R U R' U R U R'");
            await MakeTurnLetterAsync("U' R U2 R' d R' U' R");
            await MakeTurnLetterAsync("R' U2 R2 U R2 U R");
            await MakeTurnLetterAsync("d R' U R U' R' U' R");
            await MakeTurnLetterAsync("U' R U' R' U R U R'");
            await MakeTurnLetterAsync("R U2 R' U' R U R'");
            await MakeTurnLetterAsync("y' R' U2 R U R' U' R");
            await MakeTurnLetterAsync("U R U2 R2 F R F'");
            await MakeTurnLetterAsync("y' U' R' U2 R U' R' U R");
            await MakeTurnLetterAsync("R U' R' U2 R U R'");
            await MakeTurnLetterAsync("y' R' U R U2 R' U' R");
            await MakeTurnLetterAsync("U2 R2 U2 R' U' R U' R2");
            await MakeTurnLetterAsync("y' U2 R2 U2 R U R' U R2");
            await MakeTurnLetterAsync("U R U' R' U' y L' U L");
            await MakeTurnLetterAsync("y U' L' U L y' U R U' R'");
            await MakeTurnLetterAsync("y' R' U' R U R' U' R");
            await MakeTurnLetterAsync("R U R' U' R U R'");
            await MakeTurnLetterAsync("R U' R' U R U' R'");
            await MakeTurnLetterAsync("R U R' d R' U2 R");
            await MakeTurnLetterAsync("U' R U' R' U2 R U' R'");
            await MakeTurnLetterAsync("U R U R' U2 R U R'");
            await MakeTurnLetterAsync("U' R U R' d R' U' R");
            await MakeTurnLetterAsync("d R' U' R d' R U R'");
            await MakeTurnLetterAsync("R U' R' d R' U R");
            await MakeTurnLetterAsync("R U R' U' R U R' U' R U R'");
            await MakeTurnLetterAsync("R U' R' U' R U R' U2 R U' R'");
            await MakeTurnLetterAsync("R U R' U2 R U' R' U R U R'");
            await MakeTurnLetterAsync("R U' R' d R' U' R U' R' U' R");
            await MakeTurnLetterAsync("R U' R' U d R' U' R U' R' U R");
            await MakeTurnLetterAsync("R U' R' d R' U2 R U2 R' U R");
            await MakeTurnLetterAsync("R U' R'");
            await MakeTurnLetterAsync("L' U L");
            await MakeTurnLetterAsync("R U R'");
            await MakeTurnLetterAsync("L' U' L");
            await MakeTurnLetterAsync("R U R'");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("F' U' F U'");
            await MakeTurnLetterAsync("U' L' U L");
            await MakeTurnLetterAsync("U' U' ");
            await MakeTurnLetterAsync("R U' R'");
            await MakeTurnLetterAsync("U L' U' L");
            await MakeTurnLetterAsync("y' U2 R U2 R'");
            await MakeTurnLetterAsync("R U' R'");
            await MakeTurnLetterAsync("R' U R ");
            await MakeTurnLetterAsync("U' R' U R2 U' R'");
            await MakeTurnLetterAsync("y U L U' L2 U L");
            await MakeTurnLetterAsync("U2 R' U R U' S R S'");
            await MakeTurnLetterAsync("y U2 L U' L' U S' L' S");
            await MakeTurnLetterAsync("R U B' l U l2 x' U' R' F R F'");
            await MakeTurnLetterAsync("R' F R F' U2 R' F R y' R2 U2 R");
            await MakeTurnLetterAsync("y L' R2 B R' B L U2 L' B M'");
            await MakeTurnLetterAsync("R' U2 x R' U R U' y R' U' R' U R' F");
            await MakeTurnLetterAsync("R U R' U R' F R F' U2 R' F R F'");
            await MakeTurnLetterAsync("M' U2 M U2 M' U M U2 M' U2 M");
            await MakeTurnLetterAsync("R' U2 F R U R' U' y' R2 U2 x' R U");
            await MakeTurnLetterAsync("F R U R' U y' R' U2 R' F R F'");
            await MakeTurnLetterAsync("R' U' y L' U L' y' L F L' F R");
            await MakeTurnLetterAsync("R U' y R2 D R' U2 R D' R2 d R'");
            await MakeTurnLetterAsync("F U R U' R' U R U' R' F'");
            await MakeTurnLetterAsync("L' B' L U' R' U R U' R' U R L' B L");
            await MakeTurnLetterAsync("L U' R' U L' U R U R' U R");
            await MakeTurnLetterAsync("R U R' U R U' R' U R U2 R'");
            await MakeTurnLetterAsync("L' U R U' L U R'");
            await MakeTurnLetterAsync("R' U2 R U R' U R");
            await MakeTurnLetterAsync("R' F' L F R F' L' F");
            await MakeTurnLetterAsync("R2 D R' U2 R D' R' U2 R'");
            await MakeTurnLetterAsync("R' F' L' F R F' L F");
            await MakeTurnLetterAsync("M' U' M U2 M' U' M");
            await MakeTurnLetterAsync("L' R U R' U' L R' F R F'");
            await MakeTurnLetterAsync("L F R' F R F2 L'");
            await MakeTurnLetterAsync("F R' F' R U R U' R'");
            await MakeTurnLetterAsync("R' U' R y' x' R U' R' F R U R'");
            await MakeTurnLetterAsync("U' R U2 R' U' R U' R2 y' R' U' R U B");
            await MakeTurnLetterAsync("F R U R' U' R U R' U' F'");
            await MakeTurnLetterAsync("L F' L' F U2 L2 y' L F L' F");
            await MakeTurnLetterAsync("U' R' U2 R U R' U R2 y R U R' U' F'");
            await MakeTurnLetterAsync("r U2 R' U' R U' r'");
            await MakeTurnLetterAsync("R' U2 l R U' R' U l' U2 R");
            await MakeTurnLetterAsync("F' L' U' L U L' U' L U F");
            await MakeTurnLetterAsync("R' F R' F' R2 U2 x' U' R U R'");
            await MakeTurnLetterAsync("R' F R F' U2 R2 y R' F' R F'");
            await MakeTurnLetterAsync("R U R' y R' F R U' R' F' R");
            await MakeTurnLetterAsync("L' B' L U' R' U R L' B L");
            await MakeTurnLetterAsync("U2 r R2 U' R U' R' U2 R U' M");
            await MakeTurnLetterAsync("x' U' R U' R2 F x R U R' U' R B2");
            await MakeTurnLetterAsync("L U' y' R' U2 R' U R U' R U2 R d' L'");
            await MakeTurnLetterAsync("U2 l' L2 U L' U L U2 L' U M");
            await MakeTurnLetterAsync("R2 U R' B' R U' R2 U l U l'");
            await MakeTurnLetterAsync("r' U2 R U R' U r");
            await MakeTurnLetterAsync("R U x' R U' R' U x U' R'");
            await MakeTurnLetterAsync("R U R' U' x D' R' U R E'");
            await MakeTurnLetterAsync("R' F R U R' F' R y L U' L'");
            await MakeTurnLetterAsync("L F' L' U' L F L' y' R' U R");
            await MakeTurnLetterAsync("L' B' L R' U' R U L' B L");
            await MakeTurnLetterAsync("R B R' L U L' U' R B' R'");
            await MakeTurnLetterAsync("F U R U' R' F'");
            await MakeTurnLetterAsync("R' d' L d R U' R' F' R");
            await MakeTurnLetterAsync("L d R' d' L' U L F L'");
            await MakeTurnLetterAsync("F' U' L' U L F");
            await MakeTurnLetterAsync("F R U R' U' F'");
            await MakeTurnLetterAsync("R U R' U' R' F R F'");
            await MakeTurnLetterAsync("L U L' U L U' L' U' y2 R' F R F'");
            await MakeTurnLetterAsync("R' U' R U' R' U R U y F R' F' R");
            await MakeTurnLetterAsync("R' F R U R' U' y L' d R");
            await MakeTurnLetterAsync("L F' L' U' L U y' R d' L'");
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("M2 U M2 U2 M2 U M2");
            await MakeTurnLetterAsync("R' U' R2 U R U R' U' R U R U' R U' R' U2");
            await MakeTurnLetterAsync("R2 U' R' U' R U R U R U' R");
            await MakeTurnLetterAsync("R' U R' U' R' U' R' U R U R2");
            await MakeTurnLetterAsync("x z' R2 U2 R' D' R U2 R' D R' z x'");
            await MakeTurnLetterAsync("x R2 D2 R U R' D2 R U' R x'");
            await MakeTurnLetterAsync("R2 U R' U' y R U R' U' R U R' U' R U R' y' R U' R2");
            await MakeTurnLetterAsync("R U R' U' R' F R2 U' R' U' R U R' F'");
            await MakeTurnLetterAsync("F R U' R' U' R U R' F' R U R' U' R' F R F'");
            await MakeTurnLetterAsync("U' R' U R U' R2 F' U' F U x R U R' U' R2 x'");
            await MakeTurnLetterAsync("R' U R' U' y R' D R' D' R2 y' R' B' R B R");
            await MakeTurnLetterAsync("L' U' L F L' U' L U L F' L2 U L U");
            await MakeTurnLetterAsync("R U R' F' R U R' U' R' F R2 U' R' U'");
            await MakeTurnLetterAsync("L U2 L' U2 L F' L' U' L U L F L2 U");
            await MakeTurnLetterAsync("R' U2 R U2 R' F R U R' U' R' F' R2 U'");
            await MakeTurnLetterAsync("R U R' U R U R' F' R U R' U' R' F R2 U' R' U2 R U' R'");
            await MakeTurnLetterAsync("R' U R U' R' F' U' F R U R' F R' F' R U' R");
            await MakeTurnLetterAsync("y R2 u R' U R' U' R u' R2 y' R' U R");
            await MakeTurnLetterAsync("R' U' R y R2 u R' U R U' R u' R2");
            await MakeTurnLetterAsync("y R2 u' R U' R U R' u R2 y R U' R'");
            await MakeTurnLetterAsync("y2 R U R' y' R2 u' R U' R' U R' u R2");
            await MakeTurnLetterAsync("x R' U R' D2 R U' R' D2 R2");
            await MakeTurnLetterAsync("x' R U' R D2 R' U R D2 R2");
            await MakeTurnLetterAsync("R2 U R U R' U' R' U' R' U R'");
            await MakeTurnLetterAsync("R U' R U R U R U' R' U' R2");
            await MakeTurnLetterAsync("M2 U M2 U2 M2 U M2");
            await MakeTurnLetterAsync("R U R' U' R' F R2 U' R' U' R U R' F'");
            await MakeTurnLetterAsync("R' U L' U2 R U' R' U2 R L U'");
            await MakeTurnLetterAsync("R U R' F' R U R' U' R' F R2 U' R' U'");
            await MakeTurnLetterAsync("L U2 L' U2 L F' L' U' L U L F L2 U");
            await MakeTurnLetterAsync("R' U2 R U2 R' F R U R' U' R' F' R2 U'");
            await MakeTurnLetterAsync("R' U R' d' R' F' R2 U' R' U R' F R F");
            await MakeTurnLetterAsync("R2 u R' U R' U' R u' R2 y' R' U R");
            await MakeTurnLetterAsync("R' U' R y R2 u R' U R U' R u' R2");
            await MakeTurnLetterAsync("R2 u' R U' R U R' u R2 y R U' R'");
            await MakeTurnLetterAsync("R U R' y' R2 u' R U' R' U R' u R2");
            await MakeTurnLetterAsync("R' U2 R' d' R' F' R2 U' R' U R' F R U' F");
            await MakeTurnLetterAsync("M2 U M2 U M' U2 M2 U2 M' U2");
            await MakeTurnLetterAsync("F R U' R' U' R U R' F' R U R' U' R' F R F'");
            await MakeTurnLetterAsync("L U' R U2 L' U R' L U' R U2 L' U R' U");
            await MakeTurnLetterAsync("R' U L' U2 R U' L R' U L' U2 R U' L U'");
            await MakeTurnLetterAsync("x' R U' R' D R U R' u2 R' U R D R' U' R");

            return true;
        }

        //// Test the turns of the cube for the Basic method
        private static async Task<bool> TestSolveCubeTurnsBasic()
        {
            await MakeTurnLetterAsync("U2");
            await MakeTurnLetterAsync("U2");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("U'");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("U'");
            await MakeTurnLetterAsync("U2");
            await MakeTurnLetterAsync("D'");
            await MakeTurnLetterAsync("D");
            await MakeTurnLetterAsync("D2");
            await MakeTurnLetterAsync("R2");
            await MakeTurnLetterAsync("F D F'");
            await MakeTurnLetterAsync("R' D' R");
            await MakeTurnLetterAsync("R' D R D2 R' D' R");
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("U'");
            await MakeTurnLetterAsync("U2");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("R' U' D B U' L U' D F'");
            await MakeTurnLetterAsync("U' B' U D' R");
            await MakeTurnLetterAsync("U2 L U' D F'");
            await MakeTurnLetterAsync("R' U2 D2 L U' F U' D R'");
            await MakeTurnLetterAsync("R' U' D B");
            await MakeTurnLetterAsync("U F U' D' R'");
            await MakeTurnLetterAsync("U2 L' U D' B");
            await MakeTurnLetterAsync("R' U' D B");
            await MakeTurnLetterAsync("R' U' D B");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("U'");
            await MakeTurnLetterAsync("U2");
            await MakeTurnLetterAsync("D'");
            await MakeTurnLetterAsync("D'");
            await MakeTurnLetterAsync("D2");
            await MakeTurnLetterAsync("D2");
            await MakeTurnLetterAsync("D");
            await MakeTurnLetterAsync("D");
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("D' R' D R D F D' F'");
            await MakeTurnLetterAsync("D L D' L' D' F' D F");
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("y'");
            await MakeTurnLetterAsync("y2");
            await MakeTurnLetterAsync("z2");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("U'");
            await MakeTurnLetterAsync("U2");
            await MakeTurnLetterAsync("L' U R U' L U R' U'");
            await MakeTurnLetterAsync("U R U' L' U R' U' L");
            await MakeTurnLetterAsync("F U' B' U F' U' B U2");
            await MakeTurnLetterAsync("U F U R U' R' F'");
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("L2 U F' B L2 F B' U L2");
            await MakeTurnLetterAsync("L2 U' F' B L2 F B' U' L2");
            await MakeTurnLetterAsync("R2 L2 D R2 L2 U2 R2 L2 D R2 L2");
            await MakeTurnLetterAsync("R B U B' U' R2 F' U' F U R");
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("U'");
            await MakeTurnLetterAsync("U2");
            await MakeTurnLetterAsync("R' D R F D F'");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("U'");
            await MakeTurnLetterAsync("U2");
            await MakeTurnLetterAsync("F D' F' R' D' R");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("U'");
            await MakeTurnLetterAsync("U2");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("U'");
            await MakeTurnLetterAsync("U2");
            await MakeTurnLetterAsync("F U D' L2 U2 D2 R");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("U'");
            await MakeTurnLetterAsync("U2");
            await MakeTurnLetterAsync("R' D2 U2 L2 D U' F'");

            return true;
        }

        //// Test the turns of the cube for the Daisy method
        private static async Task<bool> TestSolveCubeTurnsDaisy()
        {
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("y'");
            await MakeTurnLetterAsync("y'");
            await MakeTurnLetterAsync("y'");
            await MakeTurnLetterAsync("L' U' L U");
            await MakeTurnLetterAsync("L' U' L U");
            await MakeTurnLetterAsync("L' U' L U");
            await MakeTurnLetterAsync("R U R' U'");
            await MakeTurnLetterAsync("R U R' U'");
            await MakeTurnLetterAsync("R U R' U'");
            await MakeTurnLetterAsync("y' R U R' U'");
            await MakeTurnLetterAsync("y L' U' L U");
            await MakeTurnLetterAsync("R U R' U'");
            await MakeTurnLetterAsync("y2 L' U' L U");
            await MakeTurnLetterAsync("y R U R' U'");
            await MakeTurnLetterAsync("y' L' U' L U");
            await MakeTurnLetterAsync("y2 R U R' U'");
            await MakeTurnLetterAsync("L' U' L U");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("U'");
            await MakeTurnLetterAsync("U2");
            await MakeTurnLetterAsync("U' L' U L U F U' F'");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("U'");
            await MakeTurnLetterAsync("U2");
            await MakeTurnLetterAsync("U R U' R' U' F' U F");
            await MakeTurnLetterAsync("U R U' R' U' F' U F");
            await MakeTurnLetterAsync("U R U' R' U' F' U F");
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("U'");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("U2");
            await MakeTurnLetterAsync("F U R U' R' F'");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("F R U R' U' F'");
            await MakeTurnLetterAsync("F R U R' U' F' U2 F U R U' R' F'");
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("F R U R' U' F'");
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("y2");
            await MakeTurnLetterAsync("y'");
            await MakeTurnLetterAsync("U R U' L' U R' U' L");
            await MakeTurnLetterAsync("U R U' L' U R' U' L");
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("y2");
            await MakeTurnLetterAsync("y'");
            await MakeTurnLetterAsync("R U R' U'");
            await MakeTurnLetterAsync("D'");
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("y2");
            await MakeTurnLetterAsync("y'");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("y2");
            await MakeTurnLetterAsync("y U");
            await MakeTurnLetterAsync("y2");
            await MakeTurnLetterAsync("R U R' U R U2 R' U");

            return true;
        }

        //// Test the turns of the cube for the Cross method
        private static async Task<bool> TestSolveCubeTurnsCross()
        {
            await MakeTurnLetterAsync("F2");
            await MakeTurnLetterAsync("D R2");
            await MakeTurnLetterAsync("D2 B2");
            await MakeTurnLetterAsync("D' L2");
            await MakeTurnLetterAsync("L2");
            await MakeTurnLetterAsync("D F2");
            await MakeTurnLetterAsync("D2 R2");
            await MakeTurnLetterAsync("D' B2");
            await MakeTurnLetterAsync("R2");
            await MakeTurnLetterAsync("D' F2");
            await MakeTurnLetterAsync("D B2");
            await MakeTurnLetterAsync("D2 L2");
            await MakeTurnLetterAsync("B2");
            await MakeTurnLetterAsync("D2 F2");
            await MakeTurnLetterAsync("D' R2");
            await MakeTurnLetterAsync("D L2");
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("y2");
            await MakeTurnLetterAsync("y'");
            await MakeTurnLetterAsync("L D' L'");
            await MakeTurnLetterAsync("R' D' R");
            await MakeTurnLetterAsync("F D' F'");
            await MakeTurnLetterAsync("B' D B");
            await MakeTurnLetterAsync("R D' R'");
            await MakeTurnLetterAsync("L' D L");
            await MakeTurnLetterAsync("B D' B'");
            await MakeTurnLetterAsync("F' D F");
            await MakeTurnLetterAsync("D y");
            await MakeTurnLetterAsync("D' y'");
            await MakeTurnLetterAsync("D2 y2");
            await MakeTurnLetterAsync("F2 F U' R U");
            await MakeTurnLetterAsync("U'");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("U2");
            await MakeTurnLetterAsync("z2");
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("y'");
            await MakeTurnLetterAsync("y'");
            await MakeTurnLetterAsync("y'");
            await MakeTurnLetterAsync("L' U' L U");
            await MakeTurnLetterAsync("L' U' L U");
            await MakeTurnLetterAsync("L' U' L U");
            await MakeTurnLetterAsync("R U R' U'");
            await MakeTurnLetterAsync("R U R' U'");
            await MakeTurnLetterAsync("R U R' U'");
            await MakeTurnLetterAsync("y' R U R' U'");
            await MakeTurnLetterAsync("y L' U' L U");
            await MakeTurnLetterAsync("R U R' U'");
            await MakeTurnLetterAsync("y2 L' U' L U");
            await MakeTurnLetterAsync("y R U R' U'");
            await MakeTurnLetterAsync("y' L' U' L U");
            await MakeTurnLetterAsync("y2 R U R' U'");
            await MakeTurnLetterAsync("L' U' L U");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("U' y");
            await MakeTurnLetterAsync("U2 y2");
            await MakeTurnLetterAsync("U y'");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("U' y2");
            await MakeTurnLetterAsync("U2 y'");
            await MakeTurnLetterAsync("U2");
            await MakeTurnLetterAsync("U y");
            await MakeTurnLetterAsync("y2");
            await MakeTurnLetterAsync("U' y'");
            await MakeTurnLetterAsync("U'");
            await MakeTurnLetterAsync("U2 y");
            await MakeTurnLetterAsync("U y2");
            await MakeTurnLetterAsync("y'");
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("y2");
            await MakeTurnLetterAsync("y'");
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("y2");
            await MakeTurnLetterAsync("y2");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("U2");
            await MakeTurnLetterAsync("U'");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("U2");
            await MakeTurnLetterAsync("U'");
            await MakeTurnLetterAsync("U R U R' U' y L' U' L U");
            await MakeTurnLetterAsync("U' L' U' L U y' R U R' U'");
            await MakeTurnLetterAsync("U R U' R' U' F' U F U2 U R U' R' U' F' U F");
            await MakeTurnLetterAsync("U R U' R' U' F' U F");
            await MakeTurnLetterAsync("U' L' U L U F U' F'");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("U2");
            await MakeTurnLetterAsync("U'");
            await MakeTurnLetterAsync("F R U R' U' F'");
            await MakeTurnLetterAsync("U2");
            await MakeTurnLetterAsync("U'");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("F R U R' U' F'");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("F R U R' U' F'");
            await MakeTurnLetterAsync("F R U R' U' F'");
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("y2");
            await MakeTurnLetterAsync("y'");
            await MakeTurnLetterAsync("U R U' L' U R' U' L");
            await MakeTurnLetterAsync("z2");
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("y2");
            await MakeTurnLetterAsync("y'");
            await MakeTurnLetterAsync("R U R' U'");
            await MakeTurnLetterAsync("D'");
            await MakeTurnLetterAsync("F R U R' U' F'");
            await MakeTurnLetterAsync("y");
            await MakeTurnLetterAsync("y2");
            await MakeTurnLetterAsync("y'");
            await MakeTurnLetterAsync("U");
            await MakeTurnLetterAsync("y2");
            await MakeTurnLetterAsync("y U");
            await MakeTurnLetterAsync("y2");
            await MakeTurnLetterAsync("R U R' U R U2 R' U");

            return true;
        }

        //// Test the solution of the cube with the use of only turns of 2 faces L' and U
        private static async Task<bool> TestSolveCubeOnly2Faces()
        {
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > 10000)
                {
                    Debug.WriteLine("nLoopTimes TestSolveCubeOnly2Faces: " + nLoopTimes);
                    return false;
                }
                
                await MakeTurnLetterAsync("L' U");
                
                if (ClassColorsCube.CheckIfSolved())
                {
                    break;
                }
            }

            return true;
        }
    }
}
