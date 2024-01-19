namespace RubiksCube
{
    class ClassSolveCube
    {
        //public async Task<string> SolveCubeAsyncTest(string cube)
        //{
        //    //await MainPage.MakeTurnAsync("TurnUp-");

        //    var mainPage = new MainPage();
        //    await mainPage.MakeTurnAsync("TurnUp-");

        //    return "Solved";
        //}

        // Solve the cube.
        public async Task SolveTheCubeAsync()
        {
            // Link to the class MainPage.
            MainPage mainPage = new();

            await mainPage.MakeTurnAsync("TurnFront+");  // For testing.
            return;

            // Solve the edges of the top layer - Chapter 4, page 14-3.
            await SolveEdgesTopLayerAsync();

            // Solve the edges of the top layer - Chapter 4, page 14-2.
            if (MainPage.aUpFace[5] == MainPage.aFrontFace[4])
            {
                await mainPage.MakeTurnAsync("TurnLeft+");

                if (MainPage.aLeftFace[8] == MainPage.aFrontFace[5])
                {
                    await mainPage.MakeTurnAsync("TurnDown+");
                }

                if (MainPage.aLeftFace[8] == MainPage.aBackFace[5])
                {
                    await mainPage.MakeTurnAsync("TurnDown-");
                }

                if (MainPage.aLeftFace[8] == MainPage.aRightFace[5])
                {
                    await mainPage.MakeTurnAsync("TurnDown++");
                }
            }

            if (MainPage.aUpFace[5] == MainPage.aFrontFace[6])
            {
                await mainPage.MakeTurnAsync("TurnRight-");

                if (MainPage.aRightFace[8] == MainPage.aFrontFace[5])
                {
                    await mainPage.MakeTurnAsync("TurnDown-");
                }

                if (MainPage.aRightFace[8] == MainPage.aBackFace[5])
                {
                    await mainPage.MakeTurnAsync("TurnDown+");
                }

                if (MainPage.aRightFace[8] == MainPage.aLeftFace[5])
                {
                    await mainPage.MakeTurnAsync("TurnDown++");
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




            if (!mainPage.CheckIfCubeIsSolved(false))
            {
                return;
            }
        }

        // Solve the edges of the top layer - Chapter 4, page 14-3.
        private async Task SolveEdgesTopLayerAsync()
        {
            // Link to the class MainPage.
            MainPage mainPage = new();

            for (int nTimes = 1; nTimes < 11; nTimes++)
            {
                if (MainPage.aUpFace[5] == MainPage.aDownFace[2] && MainPage.aFrontFace[5] == MainPage.aFrontFace[8])
                {
                    await mainPage.MakeTurnAsync("TurnFront++");
                }

                if (MainPage.aUpFace[5] == MainPage.aDownFace[4] && MainPage.aLeftFace[5] == MainPage.aLeftFace[8])
                {
                    await mainPage.MakeTurnAsync("TurnLeft++");
                }

                if (MainPage.aUpFace[5] == MainPage.aDownFace[6] && MainPage.aRightFace[5] == MainPage.aRightFace[8])
                {
                    await mainPage.MakeTurnAsync("TurnRight++");
                }

                if (MainPage.aUpFace[5] == MainPage.aDownFace[8] && MainPage.aBackFace[5] == MainPage.aBackFace[8])
                {
                    await mainPage.MakeTurnAsync("TurnBack++");
                    mainPage.SetCubeColorsFromArrays();
                }
            }
        }
    }
}
