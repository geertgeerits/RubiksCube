/* This module tries to solve the cube from minimum 300 different starting positions.
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

        /// <summary>
        /// Try to solve the cube from minimum 300 (12 x 24 + 12) different start positions of the cube
        /// </summary>
        /// <param name="cSolution"></param>
        /// <returns></returns>
        public static async Task<bool> SolveCubeFromMultiplePositionsAsync(string cSolution)
        {
            bool bSolveWithFaceTurns = true;  // For testing

            // Clear the lists
            lCubeTurnsTemp.Clear();
            lCubePositions.Clear();

            // Start position
            await SolveCubeFromMultiplePositions1Async(cSolution);

            if (bSolveWithFaceTurns)
            {
                // Turn the front face clockwise and counterclockwise
                lCubePositions.Add(turnFrontCW);
                await SolveCubeFromMultiplePositions1Async(cSolution);

                lCubePositions.Add(turnFrontCCW);
                await SolveCubeFromMultiplePositions1Async(cSolution);

                // Turn the back face clockwise and counterclockwise
                lCubePositions.Add(turnBackCW);
                await SolveCubeFromMultiplePositions1Async(cSolution);

                lCubePositions.Add(turnBackCCW);
                await SolveCubeFromMultiplePositions1Async(cSolution);

                // Turn the left face clockwise and counterclockwise
                lCubePositions.Add(turnLeftCW);
                await SolveCubeFromMultiplePositions1Async(cSolution);

                lCubePositions.Add(turnLeftCCW);
                await SolveCubeFromMultiplePositions1Async(cSolution);

                // Turn the right face clockwise and counterclockwise
                lCubePositions.Add(turnRightCW);
                await SolveCubeFromMultiplePositions1Async(cSolution);

                lCubePositions.Add(turnRightCCW);
                await SolveCubeFromMultiplePositions1Async(cSolution);

                // Turn the up face clockwise and counterclockwise
                lCubePositions.Add(turnUpCW);
                await SolveCubeFromMultiplePositions1Async(cSolution);

                lCubePositions.Add(turnUpCCW);
                await SolveCubeFromMultiplePositions1Async(cSolution);

                // Turn the down face clockwise and counterclockwise
                lCubePositions.Add(turnDownCW);
                await SolveCubeFromMultiplePositions1Async(cSolution);

                lCubePositions.Add(turnDownCCW);
                await SolveCubeFromMultiplePositions1Async(cSolution);
            }

            // Copy the temp list to the list lCubeTurns
            if (lCubeTurnsTemp.Count > 0)
            {
                lCubeTurns.Clear();
                lCubeTurns.AddRange(lCubeTurnsTemp);

                if (lCubeTurns.Count > 0)
                {
                    // Clean the list with the cube turns by replacing or removing turns
                    CleanListCubeTurns();

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Try to solve the cube from multiple different positions of the cube
        /// </summary>
        /// <param name="cSolution"></param>
        /// <returns></returns>
        public static async Task SolveCubeFromMultiplePositions1Async(string cSolution)
        {
            // 1. Start position
            await SolveCubeFromMultiplePositions2Async(cSolution);

            // 2. Turn the front face to the upper face
            lCubePositions.Add(turnCubeFrontToUp);
            await SolveCubeFromMultiplePositions2Async(cSolution);

            // 3. Turn the front face to the left face and the front face to the upper face
            lCubePositions.Add(turnCubeFrontToLeft);
            lCubePositions.Add(turnCubeFrontToUp);
            await SolveCubeFromMultiplePositions2Async(cSolution);

            // 4. Turn the front face to the right face and the front face to the upper face
            lCubePositions.Add(turnCubeFrontToRight);
            lCubePositions.Add(turnCubeFrontToUp);
            await SolveCubeFromMultiplePositions2Async(cSolution);

            // 5. Turn the front face to the back face and the front face to the upper face
            lCubePositions.Add(turnCubeFrontToLeft2);
            lCubePositions.Add(turnCubeFrontToUp);
            await SolveCubeFromMultiplePositions2Async(cSolution);

            // 6. Turn the upper face to the down face
            lCubePositions.Add(turnCubeUpToRight2);
            await SolveCubeFromMultiplePositions2Async(cSolution);
        }

        /// <summary>
        /// Turn the front to the right, left and back
        /// </summary>
        /// <param name="cSolution"></param>
        /// <returns></returns>
        private static async Task SolveCubeFromMultiplePositions2Async(string cSolution)
        {
            // Add 'None' to the list
            lCubePositions.Add(cNone);

            // 1. Start position
            if (await SolveCubeFromMultiplePositions3Async(cSolution, ""))
            {
                CopyListToTemp();
            }

            // 2. Turn the front face to the left face
            if (await SolveCubeFromMultiplePositions3Async(cSolution, turnCubeFrontToLeft))
            {
                CopyListToTemp();
            }

            // 3. Turn the front face to the right face
            if (await SolveCubeFromMultiplePositions3Async(cSolution, turnCubeFrontToRight))
            {
                CopyListToTemp();
            }

            // 4. Turn the front face to the back face
            if (await SolveCubeFromMultiplePositions3Async(cSolution, turnCubeFrontToLeft2))
            {
                CopyListToTemp();
            }

            lCubePositions.Clear();
        }

        /// <summary>
        /// Solve the cube from the start colors of the cube
        /// </summary>
        /// <param name="cSolution"></param>
        /// <param name="cTurn"></param>
        /// <returns></returns>
        private static async Task<bool> SolveCubeFromMultiplePositions3Async(string cSolution, string cTurn)
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

            // If the list has less items than the temp list, copy the list to the temp list
            if (lCubeTurns.Count > 0 && lCubeTurns.Count < lCubeTurnsTemp.Count)
            {
                lCubeTurnsTemp.Clear();
                lCubeTurnsTemp.AddRange(lCubeTurns);
            }
        }

        /// <summary>
        /// Clean the list with the cube turns by replacing or removing turns
        /// </summary>
        public static void CleanListCubeTurns()
        {
#if DEBUG
            _ = ClassSaveRestoreCube.CubeTurnsSave("CubeTurnsBefore.txt");
#endif
            if (lCubeTurns.Count < 4)
            {
                return;
            }
            
            // Copy the list to the temp list
            lCubeTurnsTemp.Clear();
            lCubeTurnsTemp.AddRange(lCubeTurns);

            // Do the cleaning two times
            for (int NumberCleanings = 0; NumberCleanings < 2; NumberCleanings++)
            {
                for (int i = 0; i < lCubeTurnsTemp.Count - 1; i++)
                {
                    // Replace two same turns with one turn or no turn
                    if (lCubeTurnsTemp[i] == lCubeTurnsTemp[i + 1])
                    {
                        // U & U -> U2
                        if (lCubeTurnsTemp[i].Length == 1)
                        {
                            lCubeTurnsTemp[i] = lCubeTurnsTemp[i] + c2;
                            lCubeTurnsTemp[i + 1] = cNone;
                        }

                        // U' & U' -> U2
                        else if (lCubeTurnsTemp[i].EndsWith(cApos))
                        {
                            lCubeTurnsTemp[i] = lCubeTurnsTemp[i][0] + "2";
                            lCubeTurnsTemp[i + 1] = cNone;
                        }

                        // U2 & U2 -> None
                        else if (lCubeTurnsTemp[i].EndsWith(c2))
                        {
                            lCubeTurnsTemp[i] = cNone;
                            lCubeTurnsTemp[i + 1] = cNone;
                        }
                    }

                    // Replace two same first letters of a turn with another turn or no turn
                    else if (lCubeTurnsTemp[i][0] == lCubeTurnsTemp[i + 1][0])
                    {
                        // U & U' -> None
                        if (lCubeTurnsTemp[i].Length == 1 && lCubeTurnsTemp[i + 1].EndsWith(cApos))
                        {
                            lCubeTurnsTemp[i] = cNone;
                            lCubeTurnsTemp[i + 1] = cNone;
                        }

                        // U' & U -> None
                        else if (lCubeTurnsTemp[i].EndsWith(cApos) && lCubeTurnsTemp[i + 1].Length == 1)
                        {
                            lCubeTurnsTemp[i] = cNone;
                            lCubeTurnsTemp[i + 1] = cNone;
                        }

                        // U & U2 -> U'
                        else if (lCubeTurnsTemp[i].Length == 1 && lCubeTurnsTemp[i + 1].EndsWith(c2))
                        {
                            lCubeTurnsTemp[i] = lCubeTurnsTemp[i][0] + "'";
                            lCubeTurnsTemp[i + 1] = cNone;
                        }

                        // U2 & U -> U'
                        else if (lCubeTurnsTemp[i].EndsWith(c2) && lCubeTurnsTemp[i + 1].Length == 1)
                        {
                            lCubeTurnsTemp[i] = lCubeTurnsTemp[i + 1] + cApos;
                            lCubeTurnsTemp[i + 1] = cNone;
                        }

                        // U' & U2 -> U
                        else if (lCubeTurnsTemp[i].EndsWith(cApos) && lCubeTurnsTemp[i + 1].EndsWith(c2))
                        {
                            lCubeTurnsTemp[i] = lCubeTurnsTemp[i][0] + "";
                            lCubeTurnsTemp[i + 1] = cNone;
                        }

                        // U2 & U' -> U
                        else if (lCubeTurnsTemp[i].EndsWith(c2) && lCubeTurnsTemp[i + 1].EndsWith(cApos))
                        {
                            lCubeTurnsTemp[i] = lCubeTurnsTemp[i][0] + "";
                            lCubeTurnsTemp[i + 1] = cNone;
                        }
                    }
                }

                // Remove the last turn if it is turning the whole cube (starts with x, y or z)
                if (lCubeTurnsTemp[^1].StartsWith('x') || lCubeTurnsTemp[^1].StartsWith('y') || lCubeTurnsTemp[^1].StartsWith('z'))
                {
                    lCubeTurnsTemp[^1] = cNone;
                }

                // Remove the items with 'None'
                lCubeTurnsTemp.RemoveAll(x => x == cNone);
            }

            // Copy the temp list to the list
            lCubeTurns.Clear();
            lCubeTurns.AddRange(lCubeTurnsTemp);
#if DEBUG
            // Save the list with the cube turns to a file
            _ = ClassSaveRestoreCube.CubeTurnsSave("CubeTurnsAfter.txt");
#endif
        }
    }
}
