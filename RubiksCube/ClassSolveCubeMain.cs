// This module tries to solve the cube from 24 different starting positions.
// The solution with the fewest rotations is then used.

using System.Diagnostics;
using static RubiksCube.Globals;

namespace RubiksCube
{
    internal class ClassSolveCubeMain
    {
        private const string cNone = "None";
        private static readonly List<string> lCubeTurnsTemp = [];
        private static readonly List<string> lCubePositions = [];

        //// Try to solve the cube from 24 different positions of the cube
        public static async Task<bool> SolveCubeFromMultiplePositionsAsync(string cSolution)
        {
            // Clear the lists
            lCubeTurnsTemp.Clear();
            lCubePositions.Clear();

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

            // Copy the temp list to the list
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

        //// Turn the front to the right, left and back
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

        //// Solve the cube from the start colors of the cube
        private static async Task<bool> SolveCubeFromMultiplePositions3Async(string cSolution, string cTurn)
        {
            lCubeTurns.Clear();

            if (cTurn != "")
            {
                // Replace the last item in the list with the new turn
                lCubePositions[^1] = cTurn;
            }

            // Copy the start colors of the cube to the array aPieces[]
            Array.Copy(aStartPieces, aPieces, 54);

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

        //// Copy the list to the temp list if the list has less items than the temp list
        private static void CopyListToTemp()
        {
            Debug.WriteLine($"lCubeTurns / lCubeTurnsTemp: {lCubeTurns.Count} / {lCubeTurnsTemp.Count}");

            // If the list is empty, copy the list to the temp list
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

        //// Clean the list with the cube turns by replacing or removing turns
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
                // Replace two same values with one value
                for (int i = 0; i < lCubeTurnsTemp.Count - 1; i++)
                {
                    if (lCubeTurnsTemp[i] == lCubeTurnsTemp[i + 1])
                    {
                        // U & U -> U2
                        //if (lCubeTurnsTemp[i].Length == 1)
                        //{
                        //    lCubeTurnsTemp[i] = lCubeTurnsTemp[i] + "2";
                        //    lCubeTurnsTemp[i + 1] = cNone;
                        //}

                        //// U' & U' -> U2
                        //if (lCubeTurnsTemp[i].EndsWith("'"))
                        //{
                        //    lCubeTurnsTemp[i] = lCubeTurnsTemp[i][..^1] + "2";
                        //    lCubeTurnsTemp[i + 1] = cNone;
                        //}

                        //// U2 & U2 -> None
                        //if (lCubeTurnsTemp[i].EndsWith("2"))
                        //{
                        //    lCubeTurnsTemp[i] = cNone;
                        //    lCubeTurnsTemp[i + 1] = cNone;
                        //}

                        if (lCubeTurnsTemp[i].EndsWith("CCW"))
                        {
                            lCubeTurnsTemp[i] = lCubeTurnsTemp[i][..^3] + "2";
                            lCubeTurnsTemp[i + 1] = cNone;
                        }
                        else if (lCubeTurnsTemp[i].EndsWith("CW"))
                        {
                            lCubeTurnsTemp[i] = lCubeTurnsTemp[i][..^2] + "2";
                            lCubeTurnsTemp[i + 1] = cNone;
                        }
                        else if (lCubeTurnsTemp[i] == turnUpHorMiddleRight || lCubeTurnsTemp[i] == turnUpHorMiddleLeft)
                        {
                            lCubeTurnsTemp[i] = turnUpHorMiddle2;
                            lCubeTurnsTemp[i + 1] = cNone;
                        }
                        else if (lCubeTurnsTemp[i] == turnUpVerMiddleBack || lCubeTurnsTemp[i] == turnUpVerMiddleFront)
                        {
                            lCubeTurnsTemp[i] = turnUpVerMiddle2;
                            lCubeTurnsTemp[i + 1] = cNone;
                        }
                        else if (lCubeTurnsTemp[i] == turnFrontHorMiddleLeft || lCubeTurnsTemp[i] == turnFrontHorMiddleRight)
                        {
                            lCubeTurnsTemp[i] = turnFrontHorMiddle2;
                            lCubeTurnsTemp[i + 1] = cNone;
                        }
                        else if (lCubeTurnsTemp[i] == turnCubeFrontToRight || lCubeTurnsTemp[i] == turnCubeFrontToLeft)
                        {
                            lCubeTurnsTemp[i] = turnCubeFrontToLeft2;
                            lCubeTurnsTemp[i + 1] = cNone;
                        }
                        else if (lCubeTurnsTemp[i] == turnCubeFrontToUp || lCubeTurnsTemp[i] == turnCubeFrontToDown)
                        {
                            lCubeTurnsTemp[i] = turnCubeFrontToUp2;
                            lCubeTurnsTemp[i + 1] = cNone;
                        }
                        else if (lCubeTurnsTemp[i] == turnCubeUpToRight || lCubeTurnsTemp[i] == turnCubeUpToLeft)
                        {
                            lCubeTurnsTemp[i] = turnCubeUpToRight2;
                            lCubeTurnsTemp[i + 1] = cNone;
                        }
                    }
                }

                // Remove the items with 'None'
                lCubeTurnsTemp.RemoveAll(x => x == cNone);

                // Remove the opposite turns
                for (int i = 0; i < lCubeTurnsTemp.Count - 1; i++)
                {
                    if (lCubeTurnsTemp[i] == turnFrontCW && lCubeTurnsTemp[i + 1] == turnFrontCCW)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnFrontCCW && lCubeTurnsTemp[i + 1] == turnFrontCW)
                    {
                        lCubeTurnsTemp.RemoveAt(i + 1);
                        lCubeTurnsTemp.RemoveAt(i);
                    }

                    if (lCubeTurnsTemp[i] == turnRightCW && lCubeTurnsTemp[i + 1] == turnRightCCW)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnRightCCW && lCubeTurnsTemp[i + 1] == turnRightCW)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnBackCW && lCubeTurnsTemp[i + 1] == turnBackCCW)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnBackCCW && lCubeTurnsTemp[i + 1] == turnBackCW)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnLeftCW && lCubeTurnsTemp[i + 1] == turnLeftCCW)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnLeftCCW && lCubeTurnsTemp[i + 1] == turnLeftCW)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnUpCW && lCubeTurnsTemp[i + 1] == turnUpCCW)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnUpCCW && lCubeTurnsTemp[i + 1] == turnUpCW)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnDownCW && lCubeTurnsTemp[i + 1] == turnDownCCW)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnDownCCW && lCubeTurnsTemp[i + 1] == turnDownCW)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnUpHorMiddleRight && lCubeTurnsTemp[i + 1] == turnUpHorMiddleLeft)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnUpHorMiddleLeft && lCubeTurnsTemp[i + 1] == turnUpHorMiddleRight)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnUpVerMiddleBack && lCubeTurnsTemp[i + 1] == turnUpVerMiddleFront)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnUpVerMiddleFront && lCubeTurnsTemp[i + 1] == turnUpVerMiddleBack)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnFrontHorMiddleLeft && lCubeTurnsTemp[i + 1] == turnFrontHorMiddleRight)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnFrontHorMiddleRight && lCubeTurnsTemp[i + 1] == turnFrontHorMiddleLeft)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnCubeFrontToLeft && lCubeTurnsTemp[i + 1] == turnCubeFrontToRight)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnCubeFrontToRight && lCubeTurnsTemp[i + 1] == turnCubeFrontToLeft)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnCubeUpToLeft && lCubeTurnsTemp[i + 1] == turnCubeUpToRight)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnCubeUpToRight && lCubeTurnsTemp[i + 1] == turnCubeUpToLeft)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnCubeFrontToUp && lCubeTurnsTemp[i + 1] == turnCubeFrontToDown)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnCubeFrontToDown && lCubeTurnsTemp[i + 1] == turnCubeFrontToUp)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }
                }

                // Remove the items with 'None'
                lCubeTurnsTemp.RemoveAll(x => x == cNone);

                // Remove or change turns that turn the whole cube
                for (int i = 0; i < lCubeTurnsTemp.Count - 1; i++)
                {
                    if (lCubeTurnsTemp[i] == turnCubeFrontToLeft2 && lCubeTurnsTemp[i + 1] == turnCubeFrontToLeft2)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnCubeFrontToUp2 && lCubeTurnsTemp[i + 1] == turnCubeFrontToUp2)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnCubeUpToRight2 && lCubeTurnsTemp[i + 1] == turnCubeUpToRight2)
                    {
                        lCubeTurnsTemp[i] = cNone;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnCubeFrontToLeft2 && lCubeTurnsTemp[i + 1] == turnCubeFrontToLeft)
                    {
                        lCubeTurnsTemp[i] = turnCubeFrontToRight;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnCubeFrontToUp2 && lCubeTurnsTemp[i + 1] == turnCubeFrontToUp)
                    {
                        lCubeTurnsTemp[i] = turnCubeFrontToDown;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }

                    if (lCubeTurnsTemp[i] == turnCubeUpToRight2 && lCubeTurnsTemp[i + 1] == turnCubeUpToRight)
                    {
                        lCubeTurnsTemp[i] = turnCubeUpToLeft;
                        lCubeTurnsTemp[i + 1] = cNone;
                    }
                }

                // Remove the last turn if it is turning the whole cube (starts with "TurnCube")
                if (lCubeTurnsTemp[^1].StartsWith(turnCubeUpToLeft[..8]))
                {
                    lCubeTurnsTemp[^1] = cNone;
                }

                // Remove the items with 'None'
                lCubeTurnsTemp.RemoveAll(x => x == cNone);
            }

            // Copy the temp list to the list
            lCubeTurns.Clear();
            
            for (int i = 0; i < lCubeTurnsTemp.Count; i++)
            {
                if (lCubeTurnsTemp[i] != cNone)
                {
                    lCubeTurns.Add(lCubeTurnsTemp[i]);
                }
            }
#if DEBUG
            _ = ClassSaveRestoreCube.CubeTurnsSave("CubeTurnsAfter.txt");
#endif
        }
    }
}
