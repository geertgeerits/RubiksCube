// This solution is based on the program 'SolCube' I wrote in 1981 in Microsoft Basic-80 for a Commodore PET 2001.
// The solution for solving the cube is based on a book by Don Taylor, Mastering Rubik's Cube, Dutch version 1981.
// It is not the most efficient solution, but it works most of the time.
// Basic-80 was a language with line numbers.
// I haven't been able to get rid of all the line numbers, so there are still some 'goto' statements left.
// The majority were replaced by tasks, while loops, continue and break statements.

using System.Diagnostics;

namespace RubiksCube
{
    internal class ClassSolveCubeBas
    {
        //// Declare variables
        private const int nLoopTimesMax = 500;

        //// Solve the cube.  From Basic-80 to C# - 1981-1984
        public static async Task<bool> SolveTheCubeBasAsync()
        {
            if (!await SolveTopLayerCornersAsync())
            {
                return false;
            }

            if (!await SolveTopLayerEdgesAsync())
            {
                return false;
            }

            if (!await SolveMiddleLayerAsync())
            {
                return false;
            }

            if (!await SolveBottomLayerCornersAsync())
            {
                return false;
            }

            if (!await SolveBottomLayerEdgesAsync())
            {
                return false;
            }

            if (!await SolveBottomLayerTumblingCornersAsync())
            {
                return false;
            }

            if (!await SolveBottomLayerTumblingEdgesAsync())
            {
                return false;
            }

            // Check if the cube is solved
            if (ClassColorsCube.CheckIfSolved())
            {
                return true;
            }

            return false;
        }

        /// Solve the top layer of the cube
        // Solve the corners of the top layer - Chapter 6, page 16
        private static async Task<bool> SolveTopLayerCornersAsync()
        {
            bool bO, bP, bQ, bR;
            string cB;
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("nLoopTimes top layer corners: " + nLoopTimes);
                    return false;
                }

                // 510
                cB = Globals.aPieces[40];
                bO = false;
                bP = false;
                bQ = false;
                bR = false;
                // 515
                if (cB == Globals.aPieces[36] && cB == Globals.aPieces[38] && cB == Globals.aPieces[42] && cB == Globals.aPieces[44])
                {
                    bO = true;
                }
                // 520
                if (Globals.aPieces[0] == Globals.aPieces[2])
                {
                    bP = true;
                }
                // 525
                if (Globals.aPieces[9] == Globals.aPieces[11])
                {
                    bQ = true;
                }
                // 530
                if (Globals.aPieces[18] == Globals.aPieces[20])
                {
                    bR = true;
                }
                // 535
                if (bO && bP && bQ && bR)
                {
                    break;
                }
                // 540
                bO = false;
                if (cB == Globals.aPieces[38] && cB == Globals.aPieces[42] && cB == Globals.aPieces[44])
                {
                    bO = true;
                }
                // 545
                if (bO && bP && bQ)
                {
                    await MakeTurnAsync(Globals.turnUp2);
                    goto Line600;
                }
                // 550
                bO = false;
                if (cB == Globals.aPieces[38] && cB == Globals.aPieces[44])
                {
                    bO = true;
                }
                // 555
                if (bO && bQ)
                {
                    await MakeTurnAsync(Globals.turnUp2);
                    goto Line600;
                }
                // 560
                bO = false;
                if (cB == Globals.aPieces[42] && cB == Globals.aPieces[44])
                {
                    bO = true;
                }
                // 565
                if (bO && bP)
                {
                    await MakeTurnAsync(Globals.turnUpCW);
                    goto Line600;
                }
                // 570
                bO = false;
                if (cB == Globals.aPieces[36] && cB == Globals.aPieces[38])
                    bO = true;
                // 575
                if (bO && bR)
                {
                    await MakeTurnAsync(Globals.turnUpCCW);
                    goto Line600;
                }
                // 580
                if (cB != Globals.aPieces[44])
                {
                    goto Line600;
                }
                // 585
                if (cB != Globals.aPieces[38])
                {
                    await MakeTurnAsync(Globals.turnUpCW);
                    goto Line600;
                }
                // 590
                if (cB != Globals.aPieces[42])
                {
                    await MakeTurnAsync(Globals.turnUpCCW);
                    goto Line600;
                }
                // 595
                if (cB != Globals.aPieces[36])
                {
                    await MakeTurnAsync(Globals.turnUp2);
                }

            // 600
            Line600:
                if (cB == Globals.aPieces[8] || cB == Globals.aPieces[15] || cB == Globals.aPieces[47])
                {
                    goto Line680;
                }
                // 605
                if (cB == Globals.aPieces[17] || cB == Globals.aPieces[24] || cB == Globals.aPieces[53])
                {
                    await MakeTurnAsync(Globals.turnDownCCW);
                    goto Line680;
                }
                // 610
                if (cB == Globals.aPieces[6] || cB == Globals.aPieces[35] || cB == Globals.aPieces[45])
                {
                    await MakeTurnAsync(Globals.turnDownCW);
                    goto Line680;
                }
                // 615
                if (cB == Globals.aPieces[26] || cB == Globals.aPieces[33] || cB == Globals.aPieces[51])
                {
                    await MakeTurnAsync(Globals.turnDown2);
                    goto Line680;
                }
                // 670
                await MakeTurnAsync(Globals.turnRight2);
                continue;

            // 680
            Line680:
                if (cB == Globals.aPieces[8])
                {
                    await MakeTurnAsync(Globals.turnFrontCW);
                    await MakeTurnAsync(Globals.turnDownCW);
                    await MakeTurnAsync(Globals.turnFrontCCW);
                    continue;
                }
                // 685
                if (cB == Globals.aPieces[15])
                {
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnDownCCW);
                    await MakeTurnAsync(Globals.turnRightCW);
                    continue;
                }
                // 690
                if (cB == Globals.aPieces[47])
                {
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnDownCW);
                    await MakeTurnAsync(Globals.turnRightCW);
                    await MakeTurnAsync(Globals.turnDown2);
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnDownCCW);
                    await MakeTurnAsync(Globals.turnRightCW);
                }
            }

            return true;
        }

        // Solve the edges of the top layer - Chapter 4, page 14-3
        private static async Task<bool> SolveTopLayerEdgesAsync()
        {
            bool bO, bP, bQ, bV, bX, bY, bZ;
            string cB, cX;
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("nLoopTimes top layer edges: " + nLoopTimes);
                    return false;
                }
                //Debug.WriteLine("nLoopTimes top layer edges: " + nLoopTimes);

                // 710
                cB = Globals.aPieces[40];
                bV = false;
                bX = false;
                bY = false;
                bZ = false;

                if (cB == Globals.aPieces[43] && Globals.aPieces[0] == Globals.aPieces[1])
                {
                    bV = true;
                }
                // 720
                if (cB == Globals.aPieces[41] && Globals.aPieces[9] == Globals.aPieces[10])
                {
                    bX = true;
                }
                // 730
                if (cB == Globals.aPieces[37] && Globals.aPieces[18] == Globals.aPieces[19])
                {
                    bY = true;
                }
                // 740
                if (cB == Globals.aPieces[39] && Globals.aPieces[27] == Globals.aPieces[28])
                {
                    bZ = true;
                }
                // 750
                if (bV && bX && bY && bZ)
                {
                    break;
                }
                // 760
                bO = false;
                bP = false;
                bQ = false;

                if (cB == Globals.aPieces[5] || cB == Globals.aPieces[10] || cB == Globals.aPieces[12] || cB == Globals.aPieces[14])
                {
                    bO = true;
                }
                // 770
                if (cB == Globals.aPieces[21] || cB == Globals.aPieces[16] || cB == Globals.aPieces[50])
                {
                    bP = true;
                }
                // 780
                if (cB == Globals.aPieces[41])                  // 780 IF B = D(41) AND X <> D(10) THEN Q = 1
                {
                    bQ = true;
                }
                // 790
                if (bO || bP || bQ)
                {
                    goto Line810;
                }
                // 800
                await MakeTurnAsync(Globals.turnCubeFrontToLeft);
                continue;

            // 810
            Line810:
                if (bV && bY && bZ)
                {
                    goto Line910;
                }
                // 820
                if (bY && bZ)
                {
                    await MakeTurnAsync(Globals.turnUpCCW);
                    goto Line910;
                }
                // 830
                if (bY)
                {
                    await MakeTurnAsync(Globals.turnUp2);
                    goto Line910;
                }
                // 840
                await MakeTurnAsync(Globals.turnUpCW);

            // 910
            Line910:
                cX = Globals.aPieces[9];
                if (cB == Globals.aPieces[10] && cX == Globals.aPieces[41])
                {
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnDownCW);
                    await MakeTurnAsync(Globals.turnBackCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnLeftCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnDownCW);
                    await MakeTurnAsync(Globals.turnFrontCCW);
                    continue;
                }
                // 915
                if (cB == Globals.aPieces[5] && cX == Globals.aPieces[12])
                {
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnBackCCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnDownCCW);
                    await MakeTurnAsync(Globals.turnRightCW);
                    continue;
                }
                // 920
                if (cB == Globals.aPieces[12] && cX == Globals.aPieces[5])
                {
                    await MakeTurnAsync(Globals.turnUp2);
                    await MakeTurnAsync(Globals.turnLeftCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnDownCW);
                    await MakeTurnAsync(Globals.turnFrontCCW);
                    continue;
                }
                // 925
                if (cB == Globals.aPieces[50] && cX == Globals.aPieces[16])
                {
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnUp2);
                    await MakeTurnAsync(Globals.turnDown2);
                    await MakeTurnAsync(Globals.turnLeftCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnFrontCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnDownCW);
                    await MakeTurnAsync(Globals.turnRightCCW);
                    continue;
                }
                // 930
                if (cB == Globals.aPieces[16] && cX == Globals.aPieces[50])
                {
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnDownCW);
                    await MakeTurnAsync(Globals.turnBackCW);
                    continue;
                }
                // 935
                if (cB == Globals.aPieces[21] && cX == Globals.aPieces[14])
                {
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnFrontCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnDownCCW);
                    await MakeTurnAsync(Globals.turnRightCCW);
                    continue;
                }
                // 940
                if (cB == Globals.aPieces[14] && cX == Globals.aPieces[21])
                {
                    await MakeTurnAsync(Globals.turnUp2);
                    await MakeTurnAsync(Globals.turnLeftCCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnDownCCW);
                    await MakeTurnAsync(Globals.turnBackCW);
                    continue;
                }
                // 945
                if (cB == Globals.aPieces[41] && cX != Globals.aPieces[10])
                {
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnDownCW);
                    await MakeTurnAsync(Globals.turnBackCW);
                    continue;
                }
                // 950
                if (cB == Globals.aPieces[10] && cX != Globals.aPieces[41])
                {
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnDownCW);
                    await MakeTurnAsync(Globals.turnBackCW);
                }
                // 960
            }

            return true;
        }

        /// Solve the middle layer of the cube - Chapter 10, page 21
        private static async Task<bool> SolveMiddleLayerAsync()
        {
            bool bO, bP, bQ, bR, bS;
            string cO, cP, cQ, cR, cV, cX, cY, cZ;
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("nLoopTimes middle layer: " + nLoopTimes);
                    return false;
                }

                // 1010
                cV = Globals.aPieces[4];
                cX = Globals.aPieces[13];
                cY = Globals.aPieces[22];
                cZ = Globals.aPieces[31];
                bO = false;
                bP = false;
                bQ = false;
                bR = false;
                bS = false;

                // 1015
                if (cV == Globals.aPieces[1] && cX == Globals.aPieces[10] && cY == Globals.aPieces[19])
                {
                    bO = true;
                }
                //1020
                if (cV == Globals.aPieces[3] && cV == Globals.aPieces[5])
                {
                    bP = true;
                }
                //1025
                if (cX == Globals.aPieces[12] && cX == Globals.aPieces[14])
                {
                    bQ = true;
                }
                //1030
                if (cY == Globals.aPieces[21] && cY == Globals.aPieces[23])
                {
                    bR = true;
                }
                //1035
                if (cZ == Globals.aPieces[30] && cZ == Globals.aPieces[32])
                {
                    bS = true;
                }
                //1040
                if (bO && bP && bQ && bR && bS)
                {
                    break;
                }
                //1050
                if (cV == Globals.aPieces[10])
                {
                    await MakeTurnAsync(Globals.turnUpCW);
                    continue;
                }
                //1060
                if (cV == Globals.aPieces[28])
                {
                    await MakeTurnAsync(Globals.turnUpCCW);
                    continue;
                }
                //1070
                if (cV == Globals.aPieces[19])
                {
                    await MakeTurnAsync(Globals.turnUp2);
                    continue;
                }
                //1080
                if (bP && cX == Globals.aPieces[12] && cZ == Globals.aPieces[32])
                {
                    goto Line1460;
                }

                //1100
                cO = Globals.aPieces[7];
                cP = Globals.aPieces[16];
                cQ = Globals.aPieces[25];
                cR = Globals.aPieces[34];

                //1110
                if (cV == cO && cX == Globals.aPieces[46])
                {
                    goto Line1410;
                }
                //1120
                if (cV == cO && cZ == Globals.aPieces[46])
                {
                    goto Line1420;
                }
                //1130
                if (cV == cP && cX == Globals.aPieces[50])
                {
                    await MakeTurnAsync(Globals.turnDownCCW);
                    goto Line1410;
                }
                //1140
                if (cV == cP && cZ == Globals.aPieces[50])
                {
                    await MakeTurnAsync(Globals.turnDownCCW);
                    goto Line1420;
                }
                //1150
                if (cV == cQ && cX == Globals.aPieces[52])
                {
                    await MakeTurnAsync(Globals.turnDown2);
                    goto Line1410;
                }
                //1160
                if (cV == cQ && cZ == Globals.aPieces[52])
                {
                    await MakeTurnAsync(Globals.turnDown2);
                    goto Line1420;
                }
                //1170
                if (cV == cR && cX == Globals.aPieces[48])
                {
                    await MakeTurnAsync(Globals.turnDownCW);
                    goto Line1410;
                }
                //1180
                if (cV == cR && cZ == Globals.aPieces[48])
                {
                    await MakeTurnAsync(Globals.turnDownCW);
                    goto Line1420;
                }
                //1210
                if (cX == cO && cV == Globals.aPieces[46])
                {
                    goto Line1460;
                }
                //1215
                if (cX == cO && cY == Globals.aPieces[46])
                {
                    goto Line1460;
                }
                //1220
                if (cX == cP && cV == Globals.aPieces[50])
                {
                    goto Line1460;
                }
                //1225
                if (cX == cP && cY == Globals.aPieces[50])
                {
                    goto Line1460;
                }
                //1230
                if (cX == cQ && cV == Globals.aPieces[52])
                {
                    goto Line1460;
                }
                //1235
                if (cX == cQ && cY == Globals.aPieces[52])
                {
                    goto Line1460;
                }
                //1240
                if (cX == cR && cV == Globals.aPieces[48])
                {
                    goto Line1460;
                }
                //1245
                if (cX == cR && cY == Globals.aPieces[48])
                {
                    goto Line1460;
                }
                //1250
                if (cZ == cO && cV == Globals.aPieces[46])
                {
                    goto Line1470;
                }
                //1255
                if (cZ == cO && cY == Globals.aPieces[46])
                {
                    goto Line1470;
                }
                //1260
                if (cZ == cP && cV == Globals.aPieces[50])
                {
                    goto Line1470;
                }
                //1265
                if (cZ == cP && cY == Globals.aPieces[50])
                {
                    goto Line1470;
                }
                //1270
                if (cZ == cQ && cV == Globals.aPieces[52])
                {
                    goto Line1470;
                }
                //1275
                if (cZ == cQ && cY == Globals.aPieces[52])
                {
                    goto Line1470;
                }
                //1280
                if (cZ == cR && cV == Globals.aPieces[48])
                {
                    goto Line1470;
                }
                //1285
                if (cZ == cR && cY == Globals.aPieces[48])
                {
                    goto Line1470;
                }
                //1290
                if (cY == cO && cX == Globals.aPieces[46])
                {
                    goto Line1480;
                }
                //1295
                if (cY == cO && cZ == Globals.aPieces[46])
                {
                    goto Line1480;
                }
                //1300
                if (cY == cP && cX == Globals.aPieces[50])
                {
                    goto Line1480;
                }
                //1305
                if (cY == cP && cZ == Globals.aPieces[50])
                {
                    goto Line1480;
                }
                //1310
                if (cY == cQ && cX == Globals.aPieces[52])
                {
                    goto Line1480;
                }
                //1315
                if (cY == cQ && cZ == Globals.aPieces[52])
                {
                    goto Line1480;
                }
                //1320
                if (cY == cR && cX == Globals.aPieces[48])
                {
                    goto Line1480;
                }
                //1325
                if (cY == cR && cZ == Globals.aPieces[48])
                {
                    goto Line1480;
                }
                //1360
                if (cV != Globals.aPieces[5])
                {
                    goto Line1410;
                }
                //1370
                if (cV != Globals.aPieces[3])
                {
                    goto Line1420;
                }
                //1380
                await MakeTurnAsync(Globals.turnCubeFrontToLeft);

            //1410
            Line1410:
                await MakeTurnAsync(Globals.turnDownCCW);
                await MakeTurnAsync(Globals.turnRightCCW);
                await MakeTurnAsync(Globals.turnDownCW);
                await MakeTurnAsync(Globals.turnRightCW);
                await MakeTurnAsync(Globals.turnDownCW);
                await MakeTurnAsync(Globals.turnFrontCW);
                await MakeTurnAsync(Globals.turnDownCCW);
                await MakeTurnAsync(Globals.turnFrontCCW);
                continue;

            //1420
            Line1420:
                await MakeTurnAsync(Globals.turnDownCW);
                await MakeTurnAsync(Globals.turnLeftCW);
                await MakeTurnAsync(Globals.turnDownCCW);
                await MakeTurnAsync(Globals.turnLeftCCW);
                await MakeTurnAsync(Globals.turnDownCCW);
                await MakeTurnAsync(Globals.turnFrontCCW);
                await MakeTurnAsync(Globals.turnDownCW);
                await MakeTurnAsync(Globals.turnFrontCW);
                continue;

            //1460
            Line1460:
                await MakeTurnAsync(Globals.turnCubeFrontToLeft);
                continue;

            //1470
            Line1470:
                await MakeTurnAsync(Globals.turnCubeFrontToRight);
                continue;

            //1480
            Line1480:
                await MakeTurnAsync(Globals.turnCubeFrontToLeft2);
            }
            
            return true;
        }

        /// Solve the bottom layer of the cube
        // Corners on the right place
        private static async Task<bool> SolveBottomLayerCornersAsync()
        {
            bool bO, bP, bQ, bR;
            string cV, cX, cZ;
            int nLoopTimes = 0;

            await MakeTurnAsync(Globals.turnCubeUpToRight2);

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("nLoopTimes bottom layer corners: " + nLoopTimes);
                    return false;
                }

                //1515
                cV = Globals.aPieces[4];
                cX = Globals.aPieces[13];
                //cY = Globals.aPieces[22];
                cZ = Globals.aPieces[31];
                //1520
                bO = false;
                bP = false;
                bQ = false;
                bR = false;
                if (cV == Globals.aPieces[2] || cV == Globals.aPieces[9] || cV == Globals.aPieces[44])
                {
                    bO = true;
                }
                //1525
                if (cX == Globals.aPieces[2] || cX == Globals.aPieces[9] || cX == Globals.aPieces[44])
                {
                    bP = true;
                }
                //1530
                if (cX == Globals.aPieces[11] || cX == Globals.aPieces[18] || cX == Globals.aPieces[38])
                {
                    bQ = true;
                }
                //1535
                if (cV == Globals.aPieces[0] || cV == Globals.aPieces[29] || cV == Globals.aPieces[42])
                {
                    bR = true;
                }
                //1540
                if (bO && bP && bQ && bR)
                {
                    break;
                }
                //1545
                if (bO && bP)
                {
                    goto Line1560;
                }
                //1550
                bO = false;
                if (cV == Globals.aPieces[11] || cV == Globals.aPieces[18] || cV == Globals.aPieces[38])
                {
                    bO = true;
                }
                //1552
                if (bO && bQ)
                {
                    await MakeTurnAsync(Globals.turnUpCW);
                    continue;
                }
                //1554
                bO = false;
                if (cX == Globals.aPieces[0] || cX == Globals.aPieces[29] || cX == Globals.aPieces[42])
                {
                    bO = true;
                }
                //1556
                if (bO && bR)
                {
                    await MakeTurnAsync(Globals.turnUpCCW);
                    continue;
                }
                //1558
                await MakeTurnAsync(Globals.turnUp2);
                continue;

            //1560
            Line1560:
                bO = false;
                bP = false;
                if (cV == Globals.aPieces[11] || cV == Globals.aPieces[18] || cV == Globals.aPieces[38])
                {
                    bO = true;
                }
                //1565
                if (cX == Globals.aPieces[20] || cX == Globals.aPieces[27] || cX == Globals.aPieces[36])
                {
                    bP = true;
                }
                //1570
                if (bO && bP)
                {
                    await MakeTurnAsync(Globals.turnLeftCCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnRightCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnLeftCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    continue;
                }
                //1575
                bO = false;
                bP = false;
                if (cV == Globals.aPieces[20] || cV == Globals.aPieces[27] || cV == Globals.aPieces[36])
                {
                    bO = true;
                }
                //1580
                if (cX == Globals.aPieces[0] || cX == Globals.aPieces[29] || cX == Globals.aPieces[42])
                {
                    bP = true;
                }
                //1582
                if (bO && bP)
                {
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnRightCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnLeftCCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnLeftCW);
                    continue;
                }
                //1584
                bO = false;
                bP = false;
                if (cX == Globals.aPieces[20] || cX == Globals.aPieces[27] || cX == Globals.aPieces[36])
                {
                    bO = true;
                }
                //1586
                if (cZ == Globals.aPieces[11] || cZ == Globals.aPieces[18] || cZ == Globals.aPieces[38])
                {
                    bP = true;
                }
                //1588
                if (bO && bP)
                {
                    await MakeTurnAsync(Globals.turnFrontCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnBackCCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnFrontCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnBackCW);
                    await MakeTurnAsync(Globals.turnUp2);
                    continue;
                }
                //1590
                bO = false;
                bP = false;
                if (cV == Globals.aPieces[11] || cV == Globals.aPieces[18] || cV == Globals.aPieces[38])
                {
                    bO = true;
                }
                //1592
                if (cX == Globals.aPieces[0] || cX == Globals.aPieces[29] || cX == Globals.aPieces[42])
                {
                    bP = true;
                }
                //1594
                if (bO && bP)
                {
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnFrontCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnRightCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnRightCCW);
                    await MakeTurnAsync(Globals.turnFrontCCW);
                    continue;
                }
                //1596
                await MakeTurnAsync(Globals.turnCubeFrontToLeft);
            }
            
            return true;
        }

        // Edges on the right place
        private static async Task<bool> SolveBottomLayerEdgesAsync()
        {
            bool bO, bP, bQ, bR;
            string cV, cX, cY, cZ;
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("nLoopTimes bottom layer edges: " + nLoopTimes);
                    return false;
                }

                //1610
                cV = Globals.aPieces[4];
                cX = Globals.aPieces[13];
                cY = Globals.aPieces[22];
                cZ = Globals.aPieces[31];
                //1615
                bO = false;
                bP = false;
                bQ = false;
                bR = false;
                if (cV == Globals.aPieces[1] || cV == Globals.aPieces[43])
                {
                    bO = true;
                }
                //1620
                if (cX == Globals.aPieces[10] || cX == Globals.aPieces[41])
                {
                    bP = true;
                }
                //1625
                if (cY == Globals.aPieces[19] || cY == Globals.aPieces[37])
                {
                    bQ = true;
                }
                //1630
                if (cZ == Globals.aPieces[28] || cZ == Globals.aPieces[39])
                {
                    bR = true;
                }
                //1635
                if (bO && bP && bQ && bR)
                {
                    break;
                }
                //1640
                bO = false;
                bP = false;
                if (cV == Globals.aPieces[19] || cV == Globals.aPieces[37])
                {
                    bO = true;
                }
                //1645
                if (cY == Globals.aPieces[28] || cY == Globals.aPieces[39])
                {
                    bP = true;
                }
                //1650
                if (bO && bP)
                {
                    await MakeTurnAsync(Globals.turnLeft2);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnFrontCCW);
                    await MakeTurnAsync(Globals.turnBackCW);
                    await MakeTurnAsync(Globals.turnLeft2);
                    await MakeTurnAsync(Globals.turnFrontCW);
                    await MakeTurnAsync(Globals.turnBackCCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnLeft2);
                    continue;
                }
                //1655
                bO = false;
                bP = false;
                if (cV == Globals.aPieces[28] || cV == Globals.aPieces[39])
                {
                    bO = true;
                }
                //1660
                if (cY == Globals.aPieces[1] || cY == Globals.aPieces[43])
                {
                    bP = true;
                }
                //1665
                if (bO && bP)
                {
                    await MakeTurnAsync(Globals.turnLeft2);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnFrontCCW);
                    await MakeTurnAsync(Globals.turnBackCW);
                    await MakeTurnAsync(Globals.turnLeft2);
                    await MakeTurnAsync(Globals.turnFrontCW);
                    await MakeTurnAsync(Globals.turnBackCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnLeft2);
                    continue;
                }
                //1670
                bO = false;
                bP = false;
                if (cV == Globals.aPieces[19] || cV == Globals.aPieces[37])
                {
                    bO = true;
                }
                //1675
                if (cY == Globals.aPieces[1] || cY == Globals.aPieces[43])
                {
                    bP = true;
                }
                //1680
                if (bO && bP)
                {
                    await MakeTurnAsync(Globals.turnRight2);
                    await MakeTurnAsync(Globals.turnLeft2);
                    await MakeTurnAsync(Globals.turnDownCW);
                    await MakeTurnAsync(Globals.turnRight2);
                    await MakeTurnAsync(Globals.turnLeft2);
                    await MakeTurnAsync(Globals.turnUp2);
                    await MakeTurnAsync(Globals.turnRight2);
                    await MakeTurnAsync(Globals.turnLeft2);
                    await MakeTurnAsync(Globals.turnDownCW);
                    await MakeTurnAsync(Globals.turnRight2);
                    await MakeTurnAsync(Globals.turnLeft2);
                    continue;
                }
                //1685
                bO = false;
                bP = false;
                if (cV == Globals.aPieces[28] || cV == Globals.aPieces[39])
                {
                    bO = true;
                }
                //1690
                if (cX == Globals.aPieces[19] || cX == Globals.aPieces[37])
                {
                    bP = true;
                }
                //1692
                if (bO && bP)
                {
                    await MakeTurnAsync(Globals.turnRightCW);
                    await MakeTurnAsync(Globals.turnBackCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnBackCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnRight2);
                    await MakeTurnAsync(Globals.turnFrontCCW);
                    await MakeTurnAsync(Globals.turnUpCCW);
                    await MakeTurnAsync(Globals.turnFrontCW);
                    await MakeTurnAsync(Globals.turnUpCW);
                    await MakeTurnAsync(Globals.turnRightCW);
                }
                //1694
                await MakeTurnAsync(Globals.turnCubeFrontToLeft);
                continue;
            }

            return true;
        }

        // Tumbling corners
        private static async Task<bool> SolveBottomLayerTumblingCornersAsync()
        {
            string cB;
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("nLoopTimes bottom layer tumbling corners: " + nLoopTimes);
                    return false;
                }

                //1710
                cB = Globals.aPieces[40];
                if (cB == Globals.aPieces[36] && cB == Globals.aPieces[38] && cB == Globals.aPieces[42] && cB == Globals.aPieces[44])
                {
                    break;
                }
                //1715
                if (cB == Globals.aPieces[2])
                {
                    goto Line1735;
                }
                //1720
                if (cB == Globals.aPieces[11])
                {
                    await MakeTurnAsync(Globals.turnUpCW);
                    goto Line1735;
                }
                //1725
                if (cB == Globals.aPieces[29])
                {
                    await MakeTurnAsync(Globals.turnUpCCW);
                    goto Line1735;
                }
                //1730
                if (cB == Globals.aPieces[20])
                {
                    await MakeTurnAsync(Globals.turnUp2);
                }
            
            //1735
            Line1735:
                await MakeTurnAsync(Globals.turnRightCCW);
                await MakeTurnAsync(Globals.turnDownCW);
                await MakeTurnAsync(Globals.turnRightCW);
                await MakeTurnAsync(Globals.turnFrontCW);
                await MakeTurnAsync(Globals.turnDownCW);
                await MakeTurnAsync(Globals.turnFrontCCW);
                //1740
                if (cB == Globals.aPieces[9])
                {
                    goto Line1760;
                }
                //1745
                if (cB == Globals.aPieces[18])
                {
                    await MakeTurnAsync(Globals.turnUpCW);
                    goto Line1760;
                }
                //1750
                if (cB == Globals.aPieces[0])
                {
                    await MakeTurnAsync(Globals.turnUpCCW);
                    goto Line1760;
                }
                //1755
                await MakeTurnAsync(Globals.turnUp2);
            
            //1760
            Line1760:
                await MakeTurnAsync(Globals.turnFrontCW);
                await MakeTurnAsync(Globals.turnDownCCW);
                await MakeTurnAsync(Globals.turnFrontCCW);
                await MakeTurnAsync(Globals.turnRightCCW);
                await MakeTurnAsync(Globals.turnDownCCW);
                await MakeTurnAsync(Globals.turnRightCW);
                //1765
                if (cB == Globals.aPieces[36] && cB == Globals.aPieces[38] && cB == Globals.aPieces[42] && cB == Globals.aPieces[44])
                {
                    break;
                }
                //1770
                await MakeTurnAsync(Globals.turnUpCW);
            }

            return true;
        }

        // Tumbling edges
        private static async Task<bool> SolveBottomLayerTumblingEdgesAsync()
        {
            bool bO, bP;
            string cB, cV, cX, cY;
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("nLoopTimes bottom layer tumbling edges: " + nLoopTimes);
                    return false;
                }

                //1810
                cB = Globals.aPieces[40];
                cV = Globals.aPieces[4];
                cX = Globals.aPieces[13];
                cY = Globals.aPieces[22];
                bO = false;
                bP = false;
                //1815
                if (cB == Globals.aPieces[37] && cB == Globals.aPieces[39] && cB == Globals.aPieces[41] && cB == Globals.aPieces[43])
                {
                    bO = true;
                }
                //1820
                if (cV == Globals.aPieces[1] && cX == Globals.aPieces[10] && cY == Globals.aPieces[19])
                {
                    bP = true;
                }
                //1825
                if (bO && bP)
                {
                    break;
                }
                //1830
                if (!bO)
                {
                    goto Line1850;
                }
                //1835
                if (cV == Globals.aPieces[10])
                {
                    await MakeTurnAsync(Globals.turnUpCW);
                    continue;
                }
                //1840
                if (cV == Globals.aPieces[28])
                {
                    await MakeTurnAsync(Globals.turnUpCCW);
                    continue;
                }
                //1845
                if (cV == Globals.aPieces[19])
                {
                    await MakeTurnAsync(Globals.turnUp2);
                    continue;
                }
            
            //1850
            Line1850:
                if (cB != Globals.aPieces[43])
                    goto Line1890;
                //1860
                if (cB != Globals.aPieces[41])
                {
                    await MakeTurnAsync(Globals.turnUpCW);
                    goto Line1890;
                }
                //1870
                if (cB != Globals.aPieces[39])
                {
                    await MakeTurnAsync(Globals.turnUpCCW);
                    goto Line1890;
                }
                //1880
                if (cB != Globals.aPieces[37])
                    await MakeTurnAsync(Globals.turnUp2);
                //1890
                Line1890:
                await MakeTurnAsync(Globals.turnFrontCW);
                await MakeTurnAsync(Globals.turnUpCW);
                await MakeTurnAsync(Globals.turnDownCCW);
                await MakeTurnAsync(Globals.turnLeft2);
                await MakeTurnAsync(Globals.turnUp2);
                await MakeTurnAsync(Globals.turnDown2);
                await MakeTurnAsync(Globals.turnRightCW);
                //1910
                if (cB != Globals.aPieces[41])
                {
                    goto Line1950;
                }
                //1920
                if (cB != Globals.aPieces[37])
                {
                    await MakeTurnAsync(Globals.turnUpCW);
                    goto Line1950;
                }
                //1930
                if (cB != Globals.aPieces[43])
                {
                    await MakeTurnAsync(Globals.turnUpCCW);
                    goto Line1950;
                }
                //1940
                if (cB != Globals.aPieces[39])
                {
                    await MakeTurnAsync(Globals.turnUp2);
                }
            
            //1950
            Line1950:
                await MakeTurnAsync(Globals.turnRightCCW);
                await MakeTurnAsync(Globals.turnDown2);
                await MakeTurnAsync(Globals.turnUp2);
                await MakeTurnAsync(Globals.turnLeft2);
                await MakeTurnAsync(Globals.turnDownCW);
                await MakeTurnAsync(Globals.turnUpCCW);
                await MakeTurnAsync(Globals.turnFrontCCW);
            }
            
            return true;
        }

        // Make a turn of the cube/face/side
        private static async Task MakeTurnAsync(string cTurnFaceAndDirection)
        {
            // Add the turn to the list
            Globals.lCubeTurns.Add(cTurnFaceAndDirection);

            // Turn the cube/face/side
            await ClassCubeTurns.TurnFaceCubeAsync(cTurnFaceAndDirection);
        }
    }
}
