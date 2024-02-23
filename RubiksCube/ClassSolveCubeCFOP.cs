// This solution is based on:
// https://ruwix.com/the-rubiks-cube/advanced-cfop-fridrich/
// https://ruwix.com/the-rubiks-cube/advanced-cfop-fridrich/white-cross/

using System.Diagnostics;

namespace RubiksCube
{
    internal class ClassSolveCubeCFOP
    {
        //// Declare variables
        private const int nLoopTimesMax = 200;

        //// Solve the cube.
        public static async Task<bool> SolveTheCubeCFOPAsync()
        {
            if (!await ClassSolveCubeDaisy.SolveTopLayerEdgesAsync())
            {
                return false;
            }

            if (!await ClassSolveCubeDaisy.SolveTopLayerEdges2Async())
            {
                return false;
            }

            if (!await ClassSolveCubeDaisy.SolveTopLayerCornersAsync())
            {
                return false;
            }

            if (!await SolveFirstTwoLayersAsync())
            {
                return true;
            }

            //if (!await SolveBottomLayerEdgesAsync())
            //{
            //    return false;
            //}

            //if (!await SolveBottomLayerEdges2Async())
            //{
            //    return false;
            //}

            //if (!await SolveBottomLayerCornersAsync())
            //{
            //    return false;
            //}

            //if (!await SolveBottomLayerTumblingCornersAsync())
            //{
            //    return false;
            //}

            // Check if the cube is solved
            if (ClassColorsCube.CheckIfSolved())
            {
                return true;
            }

            return true;
        }

        /// Solve the first two layers (F2L)
        private static async Task<bool> SolveFirstTwoLayersAsync()
        {
            string cB = Globals.aPieces[49];
            string cT;
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("CFOP: nLoopTimes bottom layer cross: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (cB == Globals.aPieces[45] && cB == Globals.aPieces[46] && cB == Globals.aPieces[47] && cB == Globals.aPieces[48] && cB == Globals.aPieces[50] && cB == Globals.aPieces[51] && cB == Globals.aPieces[52] && cB == Globals.aPieces[53])
                {
                    cT = Globals.aPieces[4];
                    if (cT == Globals.aPieces[3] && cT == Globals.aPieces[5] && cT == Globals.aPieces[6] && cT == Globals.aPieces[7] && cT == Globals.aPieces[8])
                    {
                        cT = Globals.aPieces[13];
                        if (cT == Globals.aPieces[12] && cT == Globals.aPieces[14] && cT == Globals.aPieces[15] && cT == Globals.aPieces[16] && cT == Globals.aPieces[17])
                        {
                            cT = Globals.aPieces[22];
                            if (cT == Globals.aPieces[21] && cT == Globals.aPieces[23] && cT == Globals.aPieces[24] && cT == Globals.aPieces[25] && cT == Globals.aPieces[26])
                            {
                                cT = Globals.aPieces[31];
                                if (cT == Globals.aPieces[30] && cT == Globals.aPieces[32] && cT == Globals.aPieces[33] && cT == Globals.aPieces[34] && cT == Globals.aPieces[35])
                                {
                                    break;
                                }
                            }
                        }
                    }
                }

                if (cB == Globals.aPieces[9] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[2] && Globals.aPieces[4] == Globals.aPieces[37])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[44])
                    {
                        await MakeTurnAsync(Globals.turnRightCW);
                        await MakeTurnAsync(Globals.turnUpCW);
                        await MakeTurnAsync(Globals.turnRightCCW);
                    }
                }

                if (cB == Globals.aPieces[2] && Globals.aPieces[4] == Globals.aPieces[7] && Globals.aPieces[4] == Globals.aPieces[44])
                {
                    if (Globals.aPieces[13] == Globals.aPieces[16] && Globals.aPieces[13] == Globals.aPieces[9] && Globals.aPieces[13] == Globals.aPieces[39])
                    {
                        await MakeTurnAsync(Globals.turnFrontCCW);
                        await MakeTurnAsync(Globals.turnUpCCW);
                        await MakeTurnAsync(Globals.turnFrontCW);
                    }
                }





                await MakeTurnAsync(Globals.turnCubeFrontToLeft);
            }

            return true;
        }









        /// Make a turn of the cube/face/side
        private static async Task MakeTurnAsync(string cTurn)
        {
            // Add the turn to the list
            Globals.lCubeTurns.Add(cTurn);

            // Turn the cube/face/side
            await ClassCubeTurns.TurnFaceCubeAsync(cTurn);
        }
    }
}