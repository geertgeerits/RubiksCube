namespace RubiksCube
{
    internal class ClassSolveCube
    {
        private int nItem;

        // Solve the cube.

        public async Task<bool> SolveTheCubeAsync()
        {
            // Link to the class MainPage.
            MainPage mainPage = new();

            //Array.Clear(Globals.aCubeTurns, 0, Globals.aCubeTurns.Length);
            nItem = 0;

            //Globals.aCubeTurns[nItem] = "TurnLeft+";
            //nItem++;
            //Globals.aCubeTurns[nItem] = "TurnLeft-";
            //nItem++;
            //return;

            // Solve the edges of the top layer - Chapter 4, page 14-3.

            await SolveEdgesTopLayerAsync();

            // Solve the edges of the top layer - Chapter 4, page 14-2.
            if (Globals.aUpFace[5] == Globals.aFrontFace[4])
            {
                //await mainPage.MakeTurnAsync("TurnLeft+");
                Globals.aCubeTurns[nItem] = "TurnLeft+";
                nItem++;

                if (Globals.aLeftFace[8] == Globals.aFrontFace[5])
                {
                    //await mainPage.MakeTurnAsync("TurnDown+");
                    Globals.aCubeTurns[nItem] = "TurnDown+";
                    nItem++;
                }

                if (Globals.aLeftFace[8] == Globals.aBackFace[5])
                {
                    //await mainPage.MakeTurnAsync("TurnDown-");
                    Globals.aCubeTurns[nItem] = "TurnDown-";
                    nItem++;
                }

                if (Globals.aLeftFace[8] == Globals.aRightFace[5])
                {
                    //await mainPage.MakeTurnAsync("TurnDown++");
                    Globals.aCubeTurns[nItem] = "TurnDown++";
                    nItem++;
                }
            }

            if (Globals.aUpFace[5] == Globals.aFrontFace[6])
            {
                //await mainPage.MakeTurnAsync("TurnRight-");
                Globals.aCubeTurns[nItem] = "TurnRight-";
                nItem++;

                if (Globals.aRightFace[8] == Globals.aFrontFace[5])
                {
                    //await mainPage.MakeTurnAsync("TurnDown-");
                    Globals.aCubeTurns[nItem] = "TurnDown-";
                    nItem++;
                }

                if (Globals.aRightFace[8] == Globals.aBackFace[5])
                {
                    //await mainPage.MakeTurnAsync("TurnDown+");
                    Globals.aCubeTurns[nItem] = "TurnDown+";
                    nItem++;
                }

                if (Globals.aRightFace[8] == Globals.aLeftFace[5])
                {
                    //await mainPage.MakeTurnAsync("TurnDown++");
                    Globals.aCubeTurns[nItem] = "TurnDown++";
                    nItem++;
                }
            }



            // Solve the edges of the top layer - Chapter 4, page 14-3.

            await SolveEdgesTopLayerAsync();

            // Solve the corners of the top layer - Chapter 6, page 16.

            // Solve the middle layer - Chapter 10, page 21.

            // Solve the bottom layer - Chapter 11, page 23.

            // Put the edges on the correct place.

            // Flip the corners.

            // Turning the edges.




            if (ClassCheckColorsCube.CheckIfSolved())
            {
                return true;
            }

            return false;
        }

        // Solve the edges of the top layer - Chapter 4, page 14-3.
        private async Task SolveEdgesTopLayerAsync()
        {
            // Link to the class MainPage.
            MainPage mainPage = new();

            for (int nTimes = 1; nTimes < 11; nTimes++)
            {
                if (Globals.aUpFace[5] == Globals.aDownFace[2] && Globals.aFrontFace[5] == Globals.aFrontFace[8])
                {
                    //await mainPage.MakeTurnAsync("TurnFront++");
                    Globals.aCubeTurns[nItem] = "TurnFront++";
                    nItem++;
                }

                if (Globals.aUpFace[5] == Globals.aDownFace[4] && Globals.aLeftFace[5] == Globals.aLeftFace[8])
                {
                    //await mainPage.MakeTurnAsync("TurnLeft++");
                    Globals.aCubeTurns[nItem] = "TurnLeft++";
                    nItem++;
                }

                if (Globals.aUpFace[5] == Globals.aDownFace[6] && Globals.aRightFace[5] == Globals.aRightFace[8])
                {
                    //await mainPage.MakeTurnAsync("TurnRight++");
                    Globals.aCubeTurns[nItem] = "TurnRight++";
                    nItem++;
                }

                if (Globals.aUpFace[5] == Globals.aDownFace[8] && Globals.aBackFace[5] == Globals.aBackFace[8])
                {
                    //await mainPage.MakeTurnAsync("TurnBack++");
                    Globals.aCubeTurns[nItem] = "TurnBack++";
                    nItem++;
                    //mainPage.SetCubeColorsFromArrays();
                }
            }
        }
    }
}
