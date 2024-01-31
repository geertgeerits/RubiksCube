namespace RubiksCube
{
    internal class ClassSolveCubeBas
    {
        // Solve the cube.  From Basic-80 to C# - 1984-04-10
        public static async Task<bool> SolveTheCubeBasAsync()
        {
            // Declare variables
            bool bO, bP, bQ, bR, bS, bV, bX, bY, bZ;
            string cB, cO, cP, cQ, cR, cV, cX, cY, cZ;
            int nLoopTimes = 0;
            int nLoopTimesMax = 400;

            // Top layer
            // Solve the corners of the top layer - Chapter 6, page 16
            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
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
                    await MakeTurnAsync(Globals.TurnUp2);
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
                    await MakeTurnAsync(Globals.TurnUp2);
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
                    await MakeTurnAsync(Globals.TurnUpCW);
                    goto Line600;
                }
                // 570
                bO = false;
                if (cB == Globals.aPieces[36] && cB == Globals.aPieces[38])
                    bO = true;
                // 575
                if (bO && bR)
                {
                    await MakeTurnAsync(Globals.TurnUpCCW);
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
                    await MakeTurnAsync(Globals.TurnUpCW);
                    goto Line600;
                }
                // 590
                if (cB != Globals.aPieces[42])
                {
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    goto Line600;
                }
                // 595
                if (cB != Globals.aPieces[36])
                {
                    await MakeTurnAsync(Globals.TurnUp2);
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
                    await MakeTurnAsync(Globals.TurnDownCCW);
                    goto Line680;
                }
                // 610
                if (cB == Globals.aPieces[6] || cB == Globals.aPieces[35] || cB == Globals.aPieces[45])
                {
                    await MakeTurnAsync(Globals.TurnDownCW);
                    goto Line680;
                }
                // 615
                if (cB == Globals.aPieces[26] || cB == Globals.aPieces[33] || cB == Globals.aPieces[51])
                {
                    await MakeTurnAsync(Globals.TurnDown2);
                    goto Line680;
                }
                // 670
                await MakeTurnAsync(Globals.TurnRight2);
                continue;

            // 680
            Line680:
                if (cB == Globals.aPieces[8])
                {
                    await MakeTurnAsync(Globals.TurnFrontCW);
                    await MakeTurnAsync(Globals.TurnDownCW);
                    await MakeTurnAsync(Globals.TurnFrontCCW);
                    continue;
                }
                // 685
                if (cB == Globals.aPieces[15])
                {
                    await MakeTurnAsync(Globals.TurnRightCCW);
                    await MakeTurnAsync(Globals.TurnDownCCW);
                    await MakeTurnAsync(Globals.TurnRightCW);
                    continue;
                }
                // 690
                if (cB == Globals.aPieces[47])
                {
                    await MakeTurnAsync(Globals.TurnRightCCW);
                    await MakeTurnAsync(Globals.TurnDownCW);
                    await MakeTurnAsync(Globals.TurnRightCW);
                    await MakeTurnAsync(Globals.TurnDown2);
                    await MakeTurnAsync(Globals.TurnRightCCW);
                    await MakeTurnAsync(Globals.TurnDownCCW);
                    await MakeTurnAsync(Globals.TurnRightCW);
                }
            }

            // Solve the edges of the top layer - Chapter 4, page 14-3
            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    return false;
                }

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
                await MakeTurnAsync(Globals.TurnCubeFrontToLeft);
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
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    goto Line910;
                }
                // 830
                if (bY)
                {
                    await MakeTurnAsync(Globals.TurnUp2);
                    goto Line910;
                }
                // 840
                await MakeTurnAsync(Globals.TurnUpCW);

            // 910
            Line910:
                cX = Globals.aPieces[9];
                if (cB == Globals.aPieces[10] && cX == Globals.aPieces[41])
                {
                    await MakeTurnAsync(Globals.TurnRightCCW);
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    await MakeTurnAsync(Globals.TurnDownCW);
                    await MakeTurnAsync(Globals.TurnBackCW);
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    await MakeTurnAsync(Globals.TurnLeftCW);
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    await MakeTurnAsync(Globals.TurnDownCW);
                    await MakeTurnAsync(Globals.TurnFrontCCW);
                    continue;
                }
                // 915
                if (cB == Globals.aPieces[5] && cX == Globals.aPieces[12])
                {
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    await MakeTurnAsync(Globals.TurnBackCCW);
                    await MakeTurnAsync(Globals.TurnUpCW);
                    await MakeTurnAsync(Globals.TurnDownCCW);
                    await MakeTurnAsync(Globals.TurnRightCW);
                    continue;
                }
                // 920
                if (cB == Globals.aPieces[12] && cX == Globals.aPieces[5])
                {
                    await MakeTurnAsync(Globals.TurnUp2);
                    await MakeTurnAsync(Globals.TurnLeftCW);
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    await MakeTurnAsync(Globals.TurnDownCW);
                    await MakeTurnAsync(Globals.TurnFrontCCW);
                    continue;
                }
                // 925
                if (cB == Globals.aPieces[50] && cX == Globals.aPieces[16])
                {
                    await MakeTurnAsync(Globals.TurnRightCCW);
                    await MakeTurnAsync(Globals.TurnUp2);
                    await MakeTurnAsync(Globals.TurnDown2);
                    await MakeTurnAsync(Globals.TurnLeftCW);
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    await MakeTurnAsync(Globals.TurnFrontCW);
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    await MakeTurnAsync(Globals.TurnDownCW);
                    await MakeTurnAsync(Globals.TurnRightCCW);
                    continue;
                }
                // 930
                if (cB == Globals.aPieces[16] && cX == Globals.aPieces[50])
                {
                    await MakeTurnAsync(Globals.TurnRightCCW);
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    await MakeTurnAsync(Globals.TurnDownCW);
                    await MakeTurnAsync(Globals.TurnBackCW);
                    continue;
                }
                // 935
                if (cB == Globals.aPieces[21] && cX == Globals.aPieces[14])
                {
                    await MakeTurnAsync(Globals.TurnUpCW);
                    await MakeTurnAsync(Globals.TurnFrontCW);
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    await MakeTurnAsync(Globals.TurnDownCCW);
                    await MakeTurnAsync(Globals.TurnRightCCW);
                    continue;
                }
                // 940
                if (cB == Globals.aPieces[14] && cX == Globals.aPieces[21])
                {
                    await MakeTurnAsync(Globals.TurnUp2);
                    await MakeTurnAsync(Globals.TurnLeftCCW);
                    await MakeTurnAsync(Globals.TurnUpCW);
                    await MakeTurnAsync(Globals.TurnDownCCW);
                    await MakeTurnAsync(Globals.TurnBackCW);
                    continue;
                }
                // 945
                if (cB == Globals.aPieces[41] && cX != Globals.aPieces[10])
                {
                    await MakeTurnAsync(Globals.TurnRightCCW);
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    await MakeTurnAsync(Globals.TurnDownCW);
                    await MakeTurnAsync(Globals.TurnBackCW);
                    continue;
                }
                // 950
                if (cB == Globals.aPieces[10] && cX != Globals.aPieces[41])
                {
                    await MakeTurnAsync(Globals.TurnRightCCW);
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    await MakeTurnAsync(Globals.TurnDownCW);
                    await MakeTurnAsync(Globals.TurnBackCW);
                }
                // 960
            }

            // Solve the middle layer - Chapter 10, page 21
            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
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
                    await MakeTurnAsync(Globals.TurnUpCW);
                    continue;
                }
                //1060
                if (cV == Globals.aPieces[28])
                {
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    continue;
                }
                //1070
                if (cV == Globals.aPieces[19])
                {
                    await MakeTurnAsync(Globals.TurnUp2);
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
                    await MakeTurnAsync(Globals.TurnDownCCW);
                    goto Line1410;
                }
                //1140
                if (cV == cP && cZ == Globals.aPieces[50])
                {
                    await MakeTurnAsync(Globals.TurnDownCCW);
                    goto Line1420;
                }
                //1150
                if (cV == cQ && cX == Globals.aPieces[52])
                {
                    await MakeTurnAsync(Globals.TurnDown2);
                    goto Line1410;
                }
                //1160
                if (cV == cQ && cZ == Globals.aPieces[52])
                {
                    await MakeTurnAsync(Globals.TurnDown2);
                    goto Line1420;
                }
                //1170
                if (cV == cR && cX == Globals.aPieces[48])
                {
                    await MakeTurnAsync(Globals.TurnDownCW);
                    goto Line1410;
                }
                //1180
                if (cV == cR && cZ == Globals.aPieces[48])
                {
                    await MakeTurnAsync(Globals.TurnDownCW);
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
                await MakeTurnAsync(Globals.TurnCubeFrontToLeft);

            //1410
            Line1410:
                await MakeTurnAsync(Globals.TurnDownCCW);
                await MakeTurnAsync(Globals.TurnRightCCW);
                await MakeTurnAsync(Globals.TurnDownCW);
                await MakeTurnAsync(Globals.TurnRightCW);
                await MakeTurnAsync(Globals.TurnDownCW);
                await MakeTurnAsync(Globals.TurnFrontCW);
                await MakeTurnAsync(Globals.TurnDownCCW);
                await MakeTurnAsync(Globals.TurnFrontCCW);
                continue;

            //1420
            Line1420:
                await MakeTurnAsync(Globals.TurnDownCW);
                await MakeTurnAsync(Globals.TurnLeftCW);
                await MakeTurnAsync(Globals.TurnDownCCW);
                await MakeTurnAsync(Globals.TurnLeftCCW);
                await MakeTurnAsync(Globals.TurnDownCCW);
                await MakeTurnAsync(Globals.TurnFrontCCW);
                await MakeTurnAsync(Globals.TurnDownCW);
                await MakeTurnAsync(Globals.TurnFrontCW);
                continue;

            //1460
            Line1460:
                await MakeTurnAsync(Globals.TurnCubeFrontToLeft);
                continue;

            //1470
            Line1470:
                await MakeTurnAsync(Globals.TurnCubeFrontToRight);
                continue;

            //1480
            Line1480:
                await MakeTurnAsync(Globals.TurnCubeFrontToLeft2);
            }

            // Bottom layer
            // Corners on the right place
            //1512
            await MakeTurnAsync(Globals.TurnCubeUpToRight2);

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
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
                    await MakeTurnAsync(Globals.TurnUpCW);
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
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    continue;
                }
                //1558
                await MakeTurnAsync(Globals.TurnUp2);
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
                    await MakeTurnAsync(Globals.TurnLeftCCW);
                    await MakeTurnAsync(Globals.TurnUpCW);
                    await MakeTurnAsync(Globals.TurnRightCW);
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    await MakeTurnAsync(Globals.TurnLeftCW);
                    await MakeTurnAsync(Globals.TurnUpCW);
                    await MakeTurnAsync(Globals.TurnRightCCW);
                    await MakeTurnAsync(Globals.TurnUpCCW);
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
                    await MakeTurnAsync(Globals.TurnUpCW);
                    await MakeTurnAsync(Globals.TurnRightCW);
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    await MakeTurnAsync(Globals.TurnLeftCCW);
                    await MakeTurnAsync(Globals.TurnUpCW);
                    await MakeTurnAsync(Globals.TurnRightCCW);
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    await MakeTurnAsync(Globals.TurnLeftCW);
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
                    await MakeTurnAsync(Globals.TurnFrontCW);
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    await MakeTurnAsync(Globals.TurnBackCCW);
                    await MakeTurnAsync(Globals.TurnUpCW);
                    await MakeTurnAsync(Globals.TurnFrontCCW);
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    await MakeTurnAsync(Globals.TurnBackCW);
                    await MakeTurnAsync(Globals.TurnUp2);
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
                    await MakeTurnAsync(Globals.TurnUpCW);
                    await MakeTurnAsync(Globals.TurnFrontCW);
                    await MakeTurnAsync(Globals.TurnUpCW);
                    await MakeTurnAsync(Globals.TurnRightCW);
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    await MakeTurnAsync(Globals.TurnRightCCW);
                    await MakeTurnAsync(Globals.TurnFrontCCW);
                    continue;
                }
                //1596
                await MakeTurnAsync(Globals.TurnCubeFrontToLeft);
            }

            // Edges on the right place
            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
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
                    await MakeTurnAsync(Globals.TurnLeft2);
                    await MakeTurnAsync(Globals.TurnUpCW);
                    await MakeTurnAsync(Globals.TurnFrontCCW);
                    await MakeTurnAsync(Globals.TurnBackCW);
                    await MakeTurnAsync(Globals.TurnLeft2);
                    await MakeTurnAsync(Globals.TurnFrontCW);
                    await MakeTurnAsync(Globals.TurnBackCCW);
                    await MakeTurnAsync(Globals.TurnUpCW);
                    await MakeTurnAsync(Globals.TurnLeft2);
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
                    await MakeTurnAsync(Globals.TurnLeft2);
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    await MakeTurnAsync(Globals.TurnFrontCCW);
                    await MakeTurnAsync(Globals.TurnBackCW);
                    await MakeTurnAsync(Globals.TurnLeft2);
                    await MakeTurnAsync(Globals.TurnFrontCW);
                    await MakeTurnAsync(Globals.TurnBackCCW);
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    await MakeTurnAsync(Globals.TurnLeft2);
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
                    await MakeTurnAsync(Globals.TurnRight2);
                    await MakeTurnAsync(Globals.TurnLeft2);
                    await MakeTurnAsync(Globals.TurnDownCW);
                    await MakeTurnAsync(Globals.TurnRight2);
                    await MakeTurnAsync(Globals.TurnLeft2);
                    await MakeTurnAsync(Globals.TurnUp2);
                    await MakeTurnAsync(Globals.TurnRight2);
                    await MakeTurnAsync(Globals.TurnLeft2);
                    await MakeTurnAsync(Globals.TurnDownCW);
                    await MakeTurnAsync(Globals.TurnRight2);
                    await MakeTurnAsync(Globals.TurnLeft2);
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
                    await MakeTurnAsync(Globals.TurnRightCW);
                    await MakeTurnAsync(Globals.TurnBackCW);
                    await MakeTurnAsync(Globals.TurnUpCW);
                    await MakeTurnAsync(Globals.TurnBackCCW);
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    await MakeTurnAsync(Globals.TurnRight2);
                    await MakeTurnAsync(Globals.TurnFrontCCW);
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    await MakeTurnAsync(Globals.TurnFrontCW);
                    await MakeTurnAsync(Globals.TurnUpCW);
                    await MakeTurnAsync(Globals.TurnRightCW);
                }
                //1694
                await MakeTurnAsync(Globals.TurnCubeFrontToLeft);
                continue;
            }

            // Tumbling corners
            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
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
                    await MakeTurnAsync(Globals.TurnUpCW);
                    goto Line1735;
                }
                //1725
                if (cB == Globals.aPieces[29])
                {
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    goto Line1735;
                }
                //1730
                if (cB == Globals.aPieces[20])
                {
                    await MakeTurnAsync(Globals.TurnUp2);
                }
            //1735
            Line1735:
                await MakeTurnAsync(Globals.TurnRightCCW);
                await MakeTurnAsync(Globals.TurnDownCW);
                await MakeTurnAsync(Globals.TurnRightCW);
                await MakeTurnAsync(Globals.TurnFrontCW);
                await MakeTurnAsync(Globals.TurnDownCW);
                await MakeTurnAsync(Globals.TurnFrontCCW);
                //1740
                if (cB == Globals.aPieces[9])
                {
                    goto Line1760;
                }
                //1745
                if (cB == Globals.aPieces[18])
                {
                    await MakeTurnAsync(Globals.TurnUpCW);
                    goto Line1760;
                }
                //1750
                if (cB == Globals.aPieces[0])
                {
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    goto Line1760;
                }
                //1755
                await MakeTurnAsync(Globals.TurnUp2);
            //1760
            Line1760:
                await MakeTurnAsync(Globals.TurnFrontCW);
                await MakeTurnAsync(Globals.TurnDownCCW);
                await MakeTurnAsync(Globals.TurnFrontCCW);
                await MakeTurnAsync(Globals.TurnRightCCW);
                await MakeTurnAsync(Globals.TurnDownCCW);
                await MakeTurnAsync(Globals.TurnRightCW);
                //1765
                if (cB == Globals.aPieces[36] && cB == Globals.aPieces[38] && cB == Globals.aPieces[42] && cB == Globals.aPieces[44])
                {
                    break;
                }
                //1770
                await MakeTurnAsync(Globals.TurnUpCW);
            }

            // Tumbling edges
            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
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
                    await MakeTurnAsync(Globals.TurnUpCW);
                    continue;
                }
                //1840
                if (cV == Globals.aPieces[28])
                {
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    continue;
                }
                //1845
                if (cV == Globals.aPieces[19])
                {
                    await MakeTurnAsync(Globals.TurnUp2);
                    continue;
                }
            //1850
            Line1850:
                if (cB != Globals.aPieces[43])
                    goto Line1890;
                //1860
                if (cB != Globals.aPieces[41])
                {
                    await MakeTurnAsync(Globals.TurnUpCW);
                    goto Line1890;
                }
                //1870
                if (cB != Globals.aPieces[39])
                {
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    goto Line1890;
                }
                //1880
                if (cB != Globals.aPieces[37])
                    await MakeTurnAsync(Globals.TurnUp2);
            //1890
            Line1890:
                await MakeTurnAsync(Globals.TurnFrontCW);
                await MakeTurnAsync(Globals.TurnUpCW);
                await MakeTurnAsync(Globals.TurnDownCCW);
                await MakeTurnAsync(Globals.TurnLeft2);
                await MakeTurnAsync(Globals.TurnUp2);
                await MakeTurnAsync(Globals.TurnDown2);
                await MakeTurnAsync(Globals.TurnRightCW);
                //1910
                if (cB != Globals.aPieces[41])
                {
                    goto Line1950;
                }
                //1920
                if (cB != Globals.aPieces[37])
                {
                    await MakeTurnAsync(Globals.TurnUpCW);
                    goto Line1950;
                }
                //1930
                if (cB != Globals.aPieces[43])
                {
                    await MakeTurnAsync(Globals.TurnUpCCW);
                    goto Line1950;
                }
                //1940
                if (cB != Globals.aPieces[39])
                {
                    await MakeTurnAsync(Globals.TurnUp2);
                }
            //1950
            Line1950:
                await MakeTurnAsync(Globals.TurnRightCCW);
                await MakeTurnAsync(Globals.TurnDown2);
                await MakeTurnAsync(Globals.TurnUp2);
                await MakeTurnAsync(Globals.TurnLeft2);
                await MakeTurnAsync(Globals.TurnDownCW);
                await MakeTurnAsync(Globals.TurnUpCCW);
                await MakeTurnAsync(Globals.TurnFrontCCW);
            }

            // Check if the cube is solved
            if (ClassCheckColorsCube.CheckIfSolved())
            {
                return true;
            }

            return false;
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
