// This module tries to solve the cube in 24 different starting positions.
// The solution with the fewest rotations is then used.

using System.Diagnostics;

namespace RubiksCube
{
    internal class ClassCubePositions
    {
        private static List<string> lCubeTurnsTemp = [];
        private static List<string> lCubePositions = [];

        // Try to solve the cube from other positions of the cube
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
            return await ClassSolveCubeBas.SolveTheCubeBasAsync();
        }

        // Copy the list to the temp list
        private static void CopyListToTemp()
        {
            //_ = Application.Current.MainPage.DisplayAlert("lCubeTurns / lCubeTurnsTemp", $"{Globals.lCubeTurns.Count} / {lCubeTurnsTemp.Count}", "OK");
            Debug.WriteLine($"lCubeTurns / lCubeTurnsTemp: {Globals.lCubeTurns.Count} / {lCubeTurnsTemp.Count}");

            if (Globals.lCubeTurns.Count > 0 && lCubeTurnsTemp.Count == 0)
            {
                lCubeTurnsTemp.AddRange(Globals.lCubeTurns);
            }

            if (Globals.lCubeTurns.Count > 0 && Globals.lCubeTurns.Count < lCubeTurnsTemp.Count)
            {
                lCubeTurnsTemp.Clear();
                lCubeTurnsTemp.AddRange(Globals.lCubeTurns);
            }
        }

        // Clean the list with the cube turns by replacing the double 1/4 turns with a half turn
        public static void CleanDoublesListCubeTurns()
        {
#if DEBUG
            _ = ClassSaveRestoreCube.CubeTurnsSave("CubeTurnsBefore.txt");
#endif
            for (int i = 0; i < Globals.lCubeTurns.Count - 1; i++)
            {
                if (Globals.lCubeTurns[i] == Globals.lCubeTurns[i + 1])
                {
                    if (Globals.lCubeTurns[i].EndsWith("CCW"))
                    {
                        Globals.lCubeTurns[i] = Globals.lCubeTurns[i][..^3] + "2";
                        Globals.lCubeTurns.RemoveAt(i + 1);
                    }
                    else if (Globals.lCubeTurns[i].EndsWith("CW"))
                    {
                        Globals.lCubeTurns[i] = Globals.lCubeTurns[i][..^2] + "2";
                        Globals.lCubeTurns.RemoveAt(i + 1);
                    }
                    else if (Globals.lCubeTurns[i] == Globals.turnUpHorMiddleRight || Globals.lCubeTurns[i] == Globals.turnUpHorMiddleLeft)
                    {
                        Globals.lCubeTurns[i] = Globals.turnUpHorMiddle2;
                        Globals.lCubeTurns.RemoveAt(i + 1);
                    }
                    else if (Globals.lCubeTurns[i] == Globals.turnUpVerMiddleBack || Globals.lCubeTurns[i] == Globals.turnUpVerMiddleFront)
                    {
                        Globals.lCubeTurns[i] = Globals.turnUpVerMiddle2;
                        Globals.lCubeTurns.RemoveAt(i + 1);
                    }
                    else if (Globals.lCubeTurns[i] == Globals.turnFrontHorMiddleLeft || Globals.lCubeTurns[i] == Globals.turnFrontHorMiddleRight)
                    {
                        Globals.lCubeTurns[i] = Globals.turnFrontHorMiddle2;
                        Globals.lCubeTurns.RemoveAt(i + 1);
                    }
                    else if (Globals.lCubeTurns[i] == Globals.turnCubeFrontToRight || Globals.lCubeTurns[i] == Globals.turnCubeFrontToLeft)
                    {
                        Globals.lCubeTurns[i] = Globals.turnCubeFrontToLeft2;
                        Globals.lCubeTurns.RemoveAt(i + 1);
                    }
                    else if (Globals.lCubeTurns[i] == Globals.turnCubeFrontToUp || Globals.lCubeTurns[i] == Globals.turnCubeFrontToDown)
                    {
                        Globals.lCubeTurns[i] = Globals.turnCubeFrontToUp2;
                        Globals.lCubeTurns.RemoveAt(i + 1);
                    }
                    else if (Globals.lCubeTurns[i] == Globals.turnCubeUpToRight || Globals.lCubeTurns[i] == Globals.turnCubeUpToLeft)
                    {
                        Globals.lCubeTurns[i] = Globals.turnCubeUpToRight2;
                        Globals.lCubeTurns.RemoveAt(i + 1);
                    }
                }
            }

#if DEBUG
            _ = ClassSaveRestoreCube.CubeTurnsSave("CubeTurnsAfter.txt");
#endif
        }
    }
}
