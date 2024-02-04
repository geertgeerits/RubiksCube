namespace RubiksCube
{
    internal class ClassCubePositions
    {
        private static List<string> lCubeTurnsTemp = [];
        private static List<string> lCubePositions = [];
        private static string[] aStartPiecesTemp = new string[54];

        // Try to solve the cube from other positions of the cube
        public static async Task<bool> SolveCubeFromMultiplePositionsAsync()
        {
            lCubeTurnsTemp.Clear();

            // Copy the start pieces to the temp array
            Array.Copy(Globals.aStartPieces, aStartPiecesTemp, 54);
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

                //_ = Application.Current.MainPage.DisplayAlert("lCubeTurns.Count", Convert.ToString(Globals.lCubeTurns.Count), "OK");
                //_ = Application.Current.MainPage.DisplayAlert("lCubeTurnsTemp.Count", Convert.ToString(lCubeTurnsTemp.Count), "OK");

                if (Globals.lCubeTurns.Count > 3)
                {
                    return true;
                }
            }

            return false;
        }

        // Turn the front to the right, left and back
        private static async Task<bool> SolveCubeFromMultiplePositions2Async()
        {
            // 1. Start position
            Globals.lCubeTurns.Clear();
            if (await SolveCubeFromMultiplePositions3Async(""))
            {
                CopyListToTemp();
            }

            // 2. Turn the front face to the left face
            Globals.lCubeTurns.Clear();
            if (await SolveCubeFromMultiplePositions3Async(Globals.turnCubeFrontToLeft))
            {
                CopyListToTemp();
            }

            // 3. Turn the front face to the right face
            Globals.lCubeTurns.Clear();
            if (await SolveCubeFromMultiplePositions3Async(Globals.turnCubeFrontToRight))
            {
                CopyListToTemp();
            }

            // 4. Turn the front face to the back face
            Globals.lCubeTurns.Clear();
            if (await SolveCubeFromMultiplePositions3Async(Globals.turnCubeFrontToLeft2))
            {
                CopyListToTemp();
            }

            return true;
        }

        // Solve the cube from the start colors of the cube
        private static async Task<bool> SolveCubeFromMultiplePositions3Async(string cTurn)
        {
            // Restore the start colors of the cube from array aStartPiecesTemp[]
            Array.Copy(aStartPiecesTemp, Globals.aPieces, 54);

            if (cTurn != "")
            {
                // Add the turn to the list
                lCubePositions.Add(cTurn);

                // Turn the faces of the cube
                await ClassCubeTurns.TurnFaceCubeAsync(cTurn);
            }

            if (lCubePositions.Count > 0)
            {
                foreach (string cItem in lCubePositions)
                {
                    await ClassCubeTurns.TurnFaceCubeAsync(cItem);
                }
            }

            lCubePositions.Clear();

            // Try to solve the cube
            return await ClassSolveCubeBas.SolveTheCubeBasAsync();
        }

        // Copy the list to the temp list
        private static void CopyListToTemp()
        {
            _ = Application.Current.MainPage.DisplayAlert("lCubeTurns / lCubeTurnsTemp", $"{Globals.lCubeTurns.Count} / {lCubeTurnsTemp.Count}", "OK");

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
    }
}
