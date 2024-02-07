// This module tries to solve the cube from 24 different starting positions.
// The solution with the fewest rotations is then used.

using System.Diagnostics;

namespace RubiksCube
{
    internal class ClassSolveCubeMain
    {
        private static readonly List<string> lCubeTurnsTemp = [];
        private static readonly List<string> lCubePositions = [];

        // Try to solve the cube from 24 positions of the cube
        public static async Task<bool> SolveCubeFromMultiplePositionsAsync()
        {
            // Clear the lists
            lCubeTurnsTemp.Clear();
            lCubePositions.Clear();

            // 1. Start position
            await SolveCubeFromMultiplePositions2Async();

            // 2. Turn the front face to the upper face
            lCubePositions.Add(Globals.turnCubeFrontToUp);
            await SolveCubeFromMultiplePositions2Async();

            // 3. Turn the front face to the left face and the front face to the upper face
            lCubePositions.Add(Globals.turnCubeFrontToLeft);
            lCubePositions.Add(Globals.turnCubeFrontToUp);
            await SolveCubeFromMultiplePositions2Async();

            // 4. Turn the front face to the right face and the front face to the upper face
            lCubePositions.Add(Globals.turnCubeFrontToRight);
            lCubePositions.Add(Globals.turnCubeFrontToUp);
            await SolveCubeFromMultiplePositions2Async();

            // 5. Turn the front face to the back face and the front face to the upper face
            lCubePositions.Add(Globals.turnCubeFrontToLeft2);
            lCubePositions.Add(Globals.turnCubeFrontToUp);
            await SolveCubeFromMultiplePositions2Async();

            // 6. Turn the upper face to the down face
            lCubePositions.Add(Globals.turnCubeUpToRight2);
            await SolveCubeFromMultiplePositions2Async();

            // Copy the temp list to the list
            if (lCubeTurnsTemp.Count > 0)
            {
                Globals.lCubeTurns.Clear();
                Globals.lCubeTurns.AddRange(lCubeTurnsTemp);

                if (Globals.lCubeTurns.Count > 0)
                {
                    // Clean the list with the cube turns by replacing or removing turns
                    CleanListCubeTurns();
                    
                    return true;
                }
            }

            return false;
        }

        // Turn the front to the right, left and back
        private static async Task SolveCubeFromMultiplePositions2Async()
        {
            // Add 'None' to the list
            lCubePositions.Add("None");

            // 1. Start position
            if (await SolveCubeFromMultiplePositions3Async(""))
            {
                CopyListToTemp();
            }

            // 2. Turn the front face to the left face
            if (await SolveCubeFromMultiplePositions3Async(Globals.turnCubeFrontToLeft))
            {
                CopyListToTemp();
            }

            // 3. Turn the front face to the right face
            if (await SolveCubeFromMultiplePositions3Async(Globals.turnCubeFrontToRight))
            {
                CopyListToTemp();
            }

            // 4. Turn the front face to the back face
            if (await SolveCubeFromMultiplePositions3Async(Globals.turnCubeFrontToLeft2))
            {
                CopyListToTemp();
            }

            lCubePositions.Clear();
        }

        // Solve the cube from the start colors of the cube
        private static async Task<bool> SolveCubeFromMultiplePositions3Async(string cTurn)
        {
            Globals.lCubeTurns.Clear();

            if (cTurn != "")
            {
                // Replace the last item in the list with the new turn
                lCubePositions[lCubePositions.Count - 1] = cTurn;
            }

            // Copy the start colors of the cube to the array aPieces[]
            Array.Copy(Globals.aStartPieces, Globals.aPieces, 54);

            if (lCubePositions.Count > 0)
            {
                foreach (string cItem in lCubePositions)
                {
                    if (cItem != "None")
                    {
                        // Add the turn to the list
                        Globals.lCubeTurns.Add(cItem);

                        // Turn the face of the cube
                        await ClassCubeTurns.TurnFaceCubeAsync(cItem);
                    }
                }
            }

            // Try to solve the cube
            // Solve the cube from Basic-80 to C#
            return await ClassSolveCubeBas.SolveTheCubeBasAsync();

            // Solve the cube (new solution)
            //return await ClassSolveCubeNew.SolveTheCubeNewAsync();
        }

        // Copy the list to the temp list if the list is has less items than the temp list
        private static void CopyListToTemp()
        {
            Debug.WriteLine($"lCubeTurns / lCubeTurnsTemp: {Globals.lCubeTurns.Count} / {lCubeTurnsTemp.Count}");

            // If the list is empty, copy the list to the temp list
            if (Globals.lCubeTurns.Count > 0 && lCubeTurnsTemp.Count == 0)
            {
                lCubeTurnsTemp.AddRange(Globals.lCubeTurns);
            }

            // If the list has less items than the temp list, copy the list to the temp list
            if (Globals.lCubeTurns.Count > 0 && Globals.lCubeTurns.Count < lCubeTurnsTemp.Count)
            {
                lCubeTurnsTemp.Clear();
                lCubeTurnsTemp.AddRange(Globals.lCubeTurns);
            }
        }

        // Clean the list with the cube turns by replacing or removing turns
        public static void CleanListCubeTurns()
        {
#if DEBUG
            _ = ClassSaveRestoreCube.CubeTurnsSave("CubeTurnsBefore.txt");
#endif
            if (Globals.lCubeTurns.Count < 4)
            {
                return;
            }
            
            // Copy the list to the temp list
            lCubeTurnsTemp.Clear();
            lCubeTurnsTemp.AddRange(Globals.lCubeTurns);

            // Replace the double 1/4 turns with a half turn
            for (int i = 0; i < lCubeTurnsTemp.Count - 1; i++)
            {
                if (lCubeTurnsTemp[i] == lCubeTurnsTemp[i + 1])
                {
                    if (lCubeTurnsTemp[i].EndsWith("CCW"))
                    {
                        lCubeTurnsTemp[i] = lCubeTurnsTemp[i][..^3] + "2";
                        lCubeTurnsTemp[i + 1] = "None";
                    }
                    else if (lCubeTurnsTemp[i].EndsWith("CW"))
                    {
                        lCubeTurnsTemp[i] = lCubeTurnsTemp[i][..^2] + "2";
                        lCubeTurnsTemp[i + 1] = "None";
                    }
                    else if (lCubeTurnsTemp[i] == Globals.turnUpHorMiddleRight || lCubeTurnsTemp[i] == Globals.turnUpHorMiddleLeft)
                    {
                        lCubeTurnsTemp[i] = Globals.turnUpHorMiddle2;
                        lCubeTurnsTemp[i + 1] = "None";
                    }
                    else if (lCubeTurnsTemp[i] == Globals.turnUpVerMiddleBack || lCubeTurnsTemp[i] == Globals.turnUpVerMiddleFront)
                    {
                        lCubeTurnsTemp[i] = Globals.turnUpVerMiddle2;
                        lCubeTurnsTemp[i + 1] = "None";
                    }
                    else if (lCubeTurnsTemp[i] == Globals.turnFrontHorMiddleLeft || lCubeTurnsTemp[i] == Globals.turnFrontHorMiddleRight)
                    {
                        lCubeTurnsTemp[i] = Globals.turnFrontHorMiddle2;
                        lCubeTurnsTemp[i + 1] = "None";
                    }
                    else if (lCubeTurnsTemp[i] == Globals.turnCubeFrontToRight || lCubeTurnsTemp[i] == Globals.turnCubeFrontToLeft)
                    {
                        lCubeTurnsTemp[i] = Globals.turnCubeFrontToLeft2;
                        lCubeTurnsTemp[i + 1] = "None";
                    }
                    else if (lCubeTurnsTemp[i] == Globals.turnCubeFrontToUp || lCubeTurnsTemp[i] == Globals.turnCubeFrontToDown)
                    {
                        lCubeTurnsTemp[i] = Globals.turnCubeFrontToUp2;
                        lCubeTurnsTemp[i + 1] = "None";
                    }
                    else if (lCubeTurnsTemp[i] == Globals.turnCubeUpToRight || lCubeTurnsTemp[i] == Globals.turnCubeUpToLeft)
                    {
                        lCubeTurnsTemp[i] = Globals.turnCubeUpToRight2;
                        lCubeTurnsTemp[i + 1] = "None";
                    }
                }
            }

            // Remove the opposite turns
            for (int i = 0; i < lCubeTurnsTemp.Count - 1; i++)
            {
                if (lCubeTurnsTemp[i] == Globals.turnFrontCW && lCubeTurnsTemp[i + 1] == Globals.turnFrontCCW)
                {
                    lCubeTurnsTemp[i] = "None";
                    lCubeTurnsTemp[i + 1] = "None";
                }

                if (lCubeTurnsTemp[i] == Globals.turnFrontCCW && lCubeTurnsTemp[i + 1] == Globals.turnFrontCW)
                {
                    lCubeTurnsTemp.RemoveAt(i + 1);
                    lCubeTurnsTemp.RemoveAt(i);
                }

                if (lCubeTurnsTemp[i] == Globals.turnRightCW && lCubeTurnsTemp[i + 1] == Globals.turnRightCCW)
                {
                    lCubeTurnsTemp[i] = "None";
                    lCubeTurnsTemp[i + 1] = "None";
                }

                if (lCubeTurnsTemp[i] == Globals.turnRightCCW && lCubeTurnsTemp[i + 1] == Globals.turnRightCW)
                {
                    lCubeTurnsTemp[i] = "None";
                    lCubeTurnsTemp[i + 1] = "None";
                }

                if (lCubeTurnsTemp[i] == Globals.turnBackCW && lCubeTurnsTemp[i + 1] == Globals.turnBackCCW)
                {
                    lCubeTurnsTemp[i] = "None";
                    lCubeTurnsTemp[i + 1] = "None";
                }

                if (lCubeTurnsTemp[i] == Globals.turnBackCCW && lCubeTurnsTemp[i + 1] == Globals.turnBackCW)
                {
                    lCubeTurnsTemp[i] = "None";
                    lCubeTurnsTemp[i + 1] = "None";
                }

                if (lCubeTurnsTemp[i] == Globals.turnLeftCW && lCubeTurnsTemp[i + 1] == Globals.turnLeftCCW)
                {
                    lCubeTurnsTemp[i] = "None";
                    lCubeTurnsTemp[i + 1] = "None";
                }

                if (lCubeTurnsTemp[i] == Globals.turnLeftCCW && lCubeTurnsTemp[i + 1] == Globals.turnLeftCW)
                {
                    lCubeTurnsTemp[i] = "None";
                    lCubeTurnsTemp[i + 1] = "None";
                }

                if (lCubeTurnsTemp[i] == Globals.turnUpCW && lCubeTurnsTemp[i + 1] == Globals.turnUpCCW)
                {
                    lCubeTurnsTemp[i] = "None";
                    lCubeTurnsTemp[i + 1] = "None";
                }

                if (lCubeTurnsTemp[i] == Globals.turnUpCCW && lCubeTurnsTemp[i + 1] == Globals.turnUpCW)
                {
                    lCubeTurnsTemp[i] = "None";
                    lCubeTurnsTemp[i + 1] = "None";
                }

                if (lCubeTurnsTemp[i] == Globals.turnDownCW && lCubeTurnsTemp[i + 1] == Globals.turnDownCCW)
                {
                    lCubeTurnsTemp[i] = "None";
                    lCubeTurnsTemp[i + 1] = "None";
                }

                if (lCubeTurnsTemp[i] == Globals.turnDownCCW && lCubeTurnsTemp[i + 1] == Globals.turnDownCW)
                {
                    lCubeTurnsTemp[i] = "None";
                    lCubeTurnsTemp[i + 1] = "None";
                }

                if (lCubeTurnsTemp[i] == Globals.turnUpHorMiddleRight && lCubeTurnsTemp[i + 1] == Globals.turnUpHorMiddleLeft)
                {
                    lCubeTurnsTemp[i] = "None";
                    lCubeTurnsTemp[i + 1] = "None";
                }

                if (lCubeTurnsTemp[i] == Globals.turnUpHorMiddleLeft && lCubeTurnsTemp[i + 1] == Globals.turnUpHorMiddleRight)
                {
                    lCubeTurnsTemp[i] = "None";
                    lCubeTurnsTemp[i + 1] = "None";
                }

                if (lCubeTurnsTemp[i] == Globals.turnUpVerMiddleBack && lCubeTurnsTemp[i + 1] == Globals.turnUpVerMiddleFront)
                {
                    lCubeTurnsTemp[i] = "None";
                    lCubeTurnsTemp[i + 1] = "None";
                }

                if (lCubeTurnsTemp[i] == Globals.turnUpVerMiddleFront && lCubeTurnsTemp[i + 1] == Globals.turnUpVerMiddleBack)
                {
                    lCubeTurnsTemp[i] = "None";
                    lCubeTurnsTemp[i + 1] = "None";
                }

                if (lCubeTurnsTemp[i] == Globals.turnFrontHorMiddleLeft && lCubeTurnsTemp[i + 1] == Globals.turnFrontHorMiddleRight)
                {
                    lCubeTurnsTemp[i] = "None";
                    lCubeTurnsTemp[i + 1] = "None";
                }

                if (lCubeTurnsTemp[i] == Globals.turnFrontHorMiddleRight && lCubeTurnsTemp[i + 1] == Globals.turnFrontHorMiddleLeft)
                {
                    lCubeTurnsTemp[i] = "None";
                    lCubeTurnsTemp[i + 1] = "None";
                }
            }

            // Copy the temp list to the list
            Globals.lCubeTurns.Clear();
            
            for (int i = 0; i < lCubeTurnsTemp.Count; i++)
            {
                if (lCubeTurnsTemp[i] != "None")
                {
                    Globals.lCubeTurns.Add(lCubeTurnsTemp[i]);
                }
            }
#if DEBUG
            _ = ClassSaveRestoreCube.CubeTurnsSave("CubeTurnsAfter.txt");
#endif
        }
    }
}
