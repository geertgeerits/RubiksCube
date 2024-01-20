namespace RubiksCube
{
    internal class ClassTestCubeTurns
    {
        private int nItem;

        // Test the turns of the cube.
        public async Task<bool> TestCubeTurnsAsync()
        {
            // Test the face turns.
            Globals.aCubeTurns[nItem] = "TurnFront+";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnFront++";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnFront-";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnFront--";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnUp+";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnUp++";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnUp-";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnUp--";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnDown+";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnDown++";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnDown-";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnDown--";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnLeft+";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnLeft++";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnLeft-";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnLeft--";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnRight+";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnRight++";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnRight-";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnRight--";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnBack+";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnBack++";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnBack-";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnBack--";
            nItem++;

            // Test the middle layer turns.
            Globals.aCubeTurns[nItem] = "TurnUpHorMiddleRight+";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnUpHorMiddleRight++";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnUpHorMiddleLeft-";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnUpHorMiddleLeft--";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnUpVerMiddleBack+";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnUpVerMiddleBack++";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnUpVerMiddleFront-";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnUpVerMiddleFront--";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnFrontHorMiddleLeft+";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnFrontHorMiddleLeft++";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnFrontHorMiddleRight-";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnFrontHorMiddleRight--";
            nItem++;

            // Test the cube turns.
            Globals.aCubeTurns[nItem] = "TurnCubeFrontToRight";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnCubeFrontToLeft";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnCubeFrontToUp";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnCubeFrontToDown";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnCubeUpToRight";
            nItem++;
            Globals.aCubeTurns[nItem] = "TurnCubeUpToLeft";
            nItem++;

            return true;
        }
    }
}
