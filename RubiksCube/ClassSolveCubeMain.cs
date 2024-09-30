/* This module tries to solve the cube from minimum 456 different starting positions.
   The solution with the fewest rotations is then used. */

using System.Diagnostics;
using static RubiksCube.Globals;

namespace RubiksCube
{
    internal sealed class ClassSolveCubeMain
    {
        private const string cNone = "None";
        private const char cApos = '\'';
        private const char c2 = '2';
        private static readonly List<string> lCubeTurnsTemp = [];
        private static readonly List<string> lCubePositions = [];
        private static bool bSolveCubeFromMultiplePositions = true;  // Default = true - Enable or disable the solving of the cube from multiple positions for testing

        /// <summary>
        /// Try to solve the cube 2 times
        /// </summary>
        /// <param name="cSolution"></param>
        /// <returns></returns>
        public static async Task<bool> SolveCubeFromMultiplePositionsAsync(string cSolution)
        {
            // Clear the lists
            lCubeTurnsTemp.Clear();
            lCubePositions.Clear();

            // Try to solve the cube for the first time
            bSolveSolution2 = false;
            await SolveCubeFromMultiplePositions1Async(cSolution);

            // Try to solve the cube for the second time
            // Using an other solution in the method ClassSolveCubeCommon.SolveTopLayerEdgesAsync()
            bSolveSolution2 = true;
            await SolveCubeFromMultiplePositions1Async(cSolution);

            // Copy the temp list to the list lCubeTurns
            if (lCubeTurnsTemp.Count > 0)
            {
                lCubeTurns.Clear();
                lCubeTurns.AddRange(lCubeTurnsTemp);

                if (lCubeTurns.Count > 0)
                {
#if DEBUG
                    // Save the list with the cube turns to a file
                    _ = ClassSaveRestoreCube.CubeTurnsSave("CubeTurnsAfterSolved.txt");
#endif
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Try to solve the cube from minimum 456 (19 x 6 x 4) different start positions of the cube
        /// </summary>
        /// <param name="cSolution"></param>
        /// <returns></returns>
        public static async Task SolveCubeFromMultiplePositions1Async(string cSolution)
        {
            // 1. Start position
            await SolveCubeFromMultiplePositions2Async(cSolution);

            if (bSolveCubeFromMultiplePositions)
            {
                // Turn the 6 faces clockwise, counterclockwise and a half turn
                // 2-3-4. Turn the front face
                lCubePositions.Add(turnFrontCW);
                await SolveCubeFromMultiplePositions2Async(cSolution);

                lCubePositions.Add(turnFrontCCW);
                await SolveCubeFromMultiplePositions2Async(cSolution);

                lCubePositions.Add(turnFront2);
                await SolveCubeFromMultiplePositions2Async(cSolution);

                // 5-6-7. Turn the back face
                lCubePositions.Add(turnBackCW);
                await SolveCubeFromMultiplePositions2Async(cSolution);

                lCubePositions.Add(turnBackCCW);
                await SolveCubeFromMultiplePositions2Async(cSolution);

                lCubePositions.Add(turnBack2);
                await SolveCubeFromMultiplePositions2Async(cSolution);

                // 8-9-10. Turn the left face
                lCubePositions.Add(turnLeftCW);
                await SolveCubeFromMultiplePositions2Async(cSolution);

                lCubePositions.Add(turnLeftCCW);
                await SolveCubeFromMultiplePositions2Async(cSolution);

                lCubePositions.Add(turnLeft2);
                await SolveCubeFromMultiplePositions2Async(cSolution);

                // 11-12-13. Turn the right face
                lCubePositions.Add(turnRightCW);
                await SolveCubeFromMultiplePositions2Async(cSolution);

                lCubePositions.Add(turnRightCCW);
                await SolveCubeFromMultiplePositions2Async(cSolution);

                lCubePositions.Add(turnRight2);
                await SolveCubeFromMultiplePositions2Async(cSolution);

                // 14-15-16. Turn the up face
                lCubePositions.Add(turnUpCW);
                await SolveCubeFromMultiplePositions2Async(cSolution);

                lCubePositions.Add(turnUpCCW);
                await SolveCubeFromMultiplePositions2Async(cSolution);

                lCubePositions.Add(turnUp2);
                await SolveCubeFromMultiplePositions2Async(cSolution);

                // 17-18-19. Turn the down face
                lCubePositions.Add(turnDownCW);
                await SolveCubeFromMultiplePositions2Async(cSolution);

                lCubePositions.Add(turnDownCCW);
                await SolveCubeFromMultiplePositions2Async(cSolution);

                lCubePositions.Add(turnDown2);
                await SolveCubeFromMultiplePositions2Async(cSolution);
            }
        }

        /// <summary>
        /// Try to solve the cube from multiple different positions of the cube
        /// </summary>
        /// <param name="cSolution"></param>
        /// <returns></returns>
        public static async Task SolveCubeFromMultiplePositions2Async(string cSolution)
        {
            // 1. Start position
            await SolveCubeFromMultiplePositions3Async(cSolution);

            if (bSolveCubeFromMultiplePositions)
            {
                // 2. Turn the front face to the upper face
                lCubePositions.Add(turnCubeFrontToUp);
                await SolveCubeFromMultiplePositions3Async(cSolution);

                // 3. Turn the front face to the left face and the front face to the upper face
                lCubePositions.Add(turnCubeFrontToLeft);
                lCubePositions.Add(turnCubeFrontToUp);
                await SolveCubeFromMultiplePositions3Async(cSolution);

                // 4. Turn the front face to the right face and the front face to the upper face
                lCubePositions.Add(turnCubeFrontToRight);
                lCubePositions.Add(turnCubeFrontToUp);
                await SolveCubeFromMultiplePositions3Async(cSolution);

                // 5. Turn the front face to the back face and the front face to the upper face
                lCubePositions.Add(turnCubeFrontToLeft2);
                lCubePositions.Add(turnCubeFrontToUp);
                await SolveCubeFromMultiplePositions3Async(cSolution);

                // 6. Turn the upper face to the down face
                lCubePositions.Add(turnCubeUpToRight2);
                await SolveCubeFromMultiplePositions3Async(cSolution);
            }
        }

        /// <summary>
        /// Turn the front to the right, left and back
        /// </summary>
        /// <param name="cSolution"></param>
        /// <returns></returns>
        private static async Task SolveCubeFromMultiplePositions3Async(string cSolution)
        {
            // Add 'None' to the list
            lCubePositions.Add(cNone);

            // 1. Start position
            if (await SolveCubeFromMultiplePositions4Async(cSolution, ""))
            {
                CopyListToTemp();
            }

            if (bSolveCubeFromMultiplePositions)
            {
                // 2. Turn the front face to the left face
                if (await SolveCubeFromMultiplePositions4Async(cSolution, turnCubeFrontToLeft))
                {
                    CopyListToTemp();
                }

                // 3. Turn the front face to the right face
                if (await SolveCubeFromMultiplePositions4Async(cSolution, turnCubeFrontToRight))
                {
                    CopyListToTemp();
                }

                // 4. Turn the front face to the back face
                if (await SolveCubeFromMultiplePositions4Async(cSolution, turnCubeFrontToLeft2))
                {
                    CopyListToTemp();
                }
            }

            lCubePositions.Clear();
        }

        /// <summary>
        /// Solve the cube from the start colors of the cube
        /// </summary>
        /// <param name="cSolution"></param>
        /// <param name="cTurn"></param>
        /// <returns></returns>
        private static async Task<bool> SolveCubeFromMultiplePositions4Async(string cSolution, string cTurn)
        {
            lCubeTurns.Clear();
            nTestedSolutions++;

            if (cTurn != "")
            {
                // Replace the last item in the list with the new turn
                lCubePositions[^1] = cTurn;
            }

            // Copy the start colors of the cube to the array aPieces[]
            Array.Copy(aStartPieces, aPieces, 54);

            // Add the items of the list lCubePositions to the list lCubeTurns without the items with the value 'None'
            if (lCubePositions.Count > 0)
            {
                foreach (string cItem in lCubePositions)
                {
                    if (cItem != cNone)
                    {
                        // Add the turn to the list
                        lCubeTurns.Add(cItem);

                        // Turn the face of the cube
                        await ClassCubeTurns.TurnCubeLayersAsync(cItem);
                    }
                }
            }

            // Try to solve the cube
            // Solve the cube (CFOP solution)
            if (cSolution == "CFOP")
            {
                return await ClassSolveCubeCFOP.SolveTheCubeCFOPAsync();
            }

            // Solve the cube (Basic-80 solution)
            if (cSolution == "Basic")
            {
                return await ClassSolveCubeBasic.SolveTheCubeBasicAsync();
            }

            // Solve the cube (Daisy solution)
            if (cSolution == "Daisy")
            {
                return await ClassSolveCubeDaisy.SolveTheCubeDaisyAsync();
            }

            // Solve the cube (Cross solution)
            if (cSolution == "Cross")
            {
                return await ClassSolveCubeCross.SolveTheCubeCrossAsync();
            }

            return false;
        }

        /// <summary>
        /// Copy the list to the temp list if the list has less items than the temp list
        /// </summary>
        private static void CopyListToTemp()
        {
            Debug.WriteLine($"nTestedSolutions: {nTestedSolutions}, lCubeTurns / lCubeTurnsTemp: {lCubeTurns.Count} / {lCubeTurnsTemp.Count}");

            // If the list lCubeTurns is not empty and the list lCubeTurnsTemp is empty, copy the list to the temp list
            if (lCubeTurns.Count > 0 && lCubeTurnsTemp.Count == 0)
            {
                lCubeTurnsTemp.AddRange(lCubeTurns);
            }

            // Clean the list with the cube turns by replacing or removing turns
            CleanListCubeTurns(lCubeTurns, false);

            // If the list has less items than the temp list, copy the list to the temp list
            if (lCubeTurns.Count > 0 && lCubeTurns.Count < lCubeTurnsTemp.Count)
            {
                lCubeTurnsTemp.Clear();
                lCubeTurnsTemp.AddRange(lCubeTurns);
            }
        }

        /// <summary>
        /// Clean the list with the cube turns by replacing or removing turns
        /// <para>The list is passed by reference. This means that the method receives a reference to the original list, not a copy of it. Therefore, any changes made to the list within the method will affect the original list.</para>
        /// </summary>
        /// <param name="lCubeTurnsToClean"></param>
        /// <param name="bCubeTurnsSave"></param>
        public static void CleanListCubeTurns(List<string> lCubeTurnsToClean, bool bCubeTurnsSave)
        {
#if DEBUG
            // Save the list with the cube turns before the cleaning to a file, for testing purposes
            if (bCubeTurnsSave)
            {
                _ = ClassSaveRestoreCube.CubeTurnsSave("CubeTurnsBeforeClean.txt");
            }
#endif
            if (lCubeTurns.Count < 4)
            {
                return;
            }

            // Do the cleaning five times
            for (int nNumberCleanings = 0; nNumberCleanings < 5; nNumberCleanings++)
            {
                for (int i = 0; i < lCubeTurnsToClean.Count - 1; i++)
                {
                    // Replace two same turns with one turn or no turn
                    if (lCubeTurnsToClean[i] == lCubeTurnsToClean[i + 1])
                    {
                        // U & U -> U2
                        if (lCubeTurnsToClean[i].Length == 1)
                        {
                            lCubeTurnsToClean[i] = lCubeTurnsToClean[i] + c2;
                            lCubeTurnsToClean[i + 1] = cNone;
                        }

                        // U' & U' -> U2
                        else if (lCubeTurnsToClean[i].EndsWith(cApos))
                        {
                            lCubeTurnsToClean[i] = lCubeTurnsToClean[i][0] + "2";
                            lCubeTurnsToClean[i + 1] = cNone;
                        }

                        // U2 & U2 -> None
                        else if (lCubeTurnsToClean[i].EndsWith(c2))
                        {
                            lCubeTurnsToClean[i] = cNone;
                            lCubeTurnsToClean[i + 1] = cNone;
                        }
                    }

                    // Replace two same first letters of a turn with another turn or no turn
                    else if (lCubeTurnsToClean[i][0] == lCubeTurnsToClean[i + 1][0])
                    {
                        // U & U' -> None
                        if (lCubeTurnsToClean[i].Length == 1 && lCubeTurnsToClean[i + 1].EndsWith(cApos))
                        {
                            lCubeTurnsToClean[i] = cNone;
                            lCubeTurnsToClean[i + 1] = cNone;
                        }

                        // U' & U -> None
                        else if (lCubeTurnsToClean[i].EndsWith(cApos) && lCubeTurnsToClean[i + 1].Length == 1)
                        {
                            lCubeTurnsToClean[i] = cNone;
                            lCubeTurnsToClean[i + 1] = cNone;
                        }

                        // U & U2 -> U'
                        else if (lCubeTurnsToClean[i].Length == 1 && lCubeTurnsToClean[i + 1].EndsWith(c2))
                        {
                            lCubeTurnsToClean[i] = lCubeTurnsToClean[i][0] + "'";
                            lCubeTurnsToClean[i + 1] = cNone;
                        }

                        // U2 & U -> U'
                        else if (lCubeTurnsToClean[i].EndsWith(c2) && lCubeTurnsToClean[i + 1].Length == 1)
                        {
                            lCubeTurnsToClean[i] = lCubeTurnsToClean[i + 1] + cApos;
                            lCubeTurnsToClean[i + 1] = cNone;
                        }

                        // U' & U2 -> U
                        else if (lCubeTurnsToClean[i].EndsWith(cApos) && lCubeTurnsToClean[i + 1].EndsWith(c2))
                        {
                            lCubeTurnsToClean[i] = lCubeTurnsToClean[i][0] + "";
                            lCubeTurnsToClean[i + 1] = cNone;
                        }

                        // U2 & U' -> U
                        else if (lCubeTurnsToClean[i].EndsWith(c2) && lCubeTurnsToClean[i + 1].EndsWith(cApos))
                        {
                            lCubeTurnsToClean[i] = lCubeTurnsToClean[i][0] + "";
                            lCubeTurnsToClean[i + 1] = cNone;
                        }
                    }
                }

                // Remove the last turn if it is turning the whole cube (starts with x, y or z)
                if (lCubeTurnsToClean[^1].StartsWith('x') || lCubeTurnsToClean[^1].StartsWith('y') || lCubeTurnsToClean[^1].StartsWith('z'))
                {
                    lCubeTurnsToClean[^1] = cNone;
                }

                // Remove the items with 'None'
                lCubeTurnsToClean.RemoveAll(x => x == cNone);
            }
#if DEBUG
            // Save the list with the cube turns after the cleaning to a file, for testing purposes
            if (bCubeTurnsSave)
            {
                _ = ClassSaveRestoreCube.CubeTurnsSave("CubeTurnsAfterClean.txt");
            }
#endif
        }
    }
}
