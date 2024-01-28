namespace RubiksCube
{
    internal class ClassSolveCubeBas1
    {
        // Solve the cube.  From Basic-80 to C# - 1984-04-10
        public async Task<bool> SolveTheCubeBasAsync()
        {
            // Check if the cube is already solved
            if (ClassCheckColorsCube.CheckIfSolved())
            {
                return true;
            }

            // Declare variables
            int O, P, Q, R, S, V, X, Y, Z;
            string cB, cO, cP, cQ, cR, cV, cX, cY, cZ;
            int nLoopTimes = 0;

        // 500
        // Top layer
        // Solve the corners of the top layer - Chapter 6, page 16
        // 510
        Line510:
            nLoopTimes++;
            if (nLoopTimes > 200)
            {
                return false;
            }

            cB = Globals.aPieces[40];
            O = 0;
            P = 0;
            Q = 0;
            R = 0;
            // 515
            if (cB == Globals.aPieces[36] && cB == Globals.aPieces[38] && cB == Globals.aPieces[42] && cB == Globals.aPieces[44])
            {
                O = 1;
            }
            // 520
            if (Globals.aPieces[0] == Globals.aPieces[2])
            {
                P = 1;
            }
            // 525
            if (Globals.aPieces[9] == Globals.aPieces[11])
            {
                Q = 1;
            }
            // 530
            if (Globals.aPieces[18] == Globals.aPieces[20])
            {
                R = 1;
            }
            // 535
            if (O == 1 && P == 1 && Q == 1 && R == 1)
            {
                goto Line710;
            }
            // 540
            O = 0;
            if (cB == Globals.aPieces[38] && cB == Globals.aPieces[42] && cB == Globals.aPieces[44])
            {
                O = 1;
            }
            // 545
            if (O == 1 && P == 1 && Q == 1)
            {
                await MakeTurnAsync(Globals.TurnUp2);
                goto Line600;
            }
            // 550
            O = 0;
            if (cB == Globals.aPieces[38] && cB == Globals.aPieces[44])
            {
                O = 1;
            }
            // 555
            if (O == 1 && Q == 1)
            {
                await MakeTurnAsync(Globals.TurnUp2);
                goto Line600;
            }
            // 560
            O = 0;
            if (cB == Globals.aPieces[42] && cB == Globals.aPieces[44])
            {
                O = 1;
            }
            // 565
            if (O == 1 && P == 1)
            {
                await MakeTurnAsync(Globals.TurnUpCW);
                goto Line600;
            }
            // 570
            O = 0;
            if (cB == Globals.aPieces[36] && cB == Globals.aPieces[38])
                O = 1;
            // 575
            if (O == 1 && R == 1)
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
            goto Line510;

        // 680
        Line680:
            if (cB == Globals.aPieces[8])
            {
                await MakeTurnAsync(Globals.TurnFrontCW);
                await MakeTurnAsync(Globals.TurnDownCW);
                await MakeTurnAsync(Globals.TurnFrontCCW);
                goto Line510;
            }
            // 685
            if (cB == Globals.aPieces[15])
            {
                await MakeTurnAsync(Globals.TurnRightCCW);
                await MakeTurnAsync(Globals.TurnDownCCW);
                await MakeTurnAsync(Globals.TurnRightCW);
                goto Line510;
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
            // 695
            goto Line510;

        // Solve the edges of the top layer - Chapter 4, page 14-3
        // 710
        Line710:
            nLoopTimes++;
            if (nLoopTimes > 400)
            {
                return false;
            }

            cB = Globals.aPieces[40];
            V = 0;
            X = 0;
            Y = 0;
            Z = 0;

            if (cB == Globals.aPieces[43] && Globals.aPieces[0] == Globals.aPieces[1])
            {
                V = 1;
            }
            // 720
            if (cB == Globals.aPieces[41] && Globals.aPieces[9] == Globals.aPieces[10])
            {
                X = 1;
            }
            // 730
            if (cB == Globals.aPieces[37] && Globals.aPieces[18] == Globals.aPieces[19])
            {
                Y = 1;
            }
            // 740
            if (cB == Globals.aPieces[39] && Globals.aPieces[27] == Globals.aPieces[28])
            {
                Z = 1;
            }
            // 750
            if (V == 1 && X == 1 && Y == 1 && Z == 1)
            {
                goto Line1010;
            }
            // 760
            O = 0;
            P = 0;
            Q = 0;

            if (cB == Globals.aPieces[5] || cB == Globals.aPieces[10] || cB == Globals.aPieces[12] || cB == Globals.aPieces[14])
            {
                O = 1;
            }
            // 770
            if (cB == Globals.aPieces[21] || cB == Globals.aPieces[16] || cB == Globals.aPieces[50])
            {
                P = 1;
            }
            // 780
            if (cB == Globals.aPieces[41])                  // 780 IF B = D(41) AND X <> D(10) THEN Q = 1
            {
                Q = 1;
            }
            // 790
            if (O == 1 || P == 1 || Q == 1)
            {
                goto Line810;
            }
            // 800
            await MakeTurnAsync(Globals.TurnCubeFrontToLeft);
            goto Line710;

        // 810
        Line810:
            if (V == 1 && Y == 1 && Z == 1)
            {
                goto Line910;
            }
            // 820
            if (Y == 1 && Z == 1)
            {
                await MakeTurnAsync(Globals.TurnUpCCW);
                goto Line910;
            }
            // 830
            if (Y == 1)
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
                goto Line710;
            }
            // 915
            if (cB == Globals.aPieces[5] && cX == Globals.aPieces[12])
            {
                await MakeTurnAsync(Globals.TurnUpCCW);
                await MakeTurnAsync(Globals.TurnBackCCW);
                await MakeTurnAsync(Globals.TurnUpCW);
                await MakeTurnAsync(Globals.TurnDownCCW);
                await MakeTurnAsync(Globals.TurnRightCW);
                goto Line710;
            }
            // 920
            if (cB == Globals.aPieces[12] && cX == Globals.aPieces[5])
            {
                await MakeTurnAsync(Globals.TurnUp2);
                await MakeTurnAsync(Globals.TurnLeftCW);
                await MakeTurnAsync(Globals.TurnUpCCW);
                await MakeTurnAsync(Globals.TurnDownCW);
                await MakeTurnAsync(Globals.TurnFrontCCW);
                goto Line710;
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
                goto Line710;
            }
            // 930
            if (cB == Globals.aPieces[16] && cX == Globals.aPieces[50])
            {
                await MakeTurnAsync(Globals.TurnRightCCW);
                await MakeTurnAsync(Globals.TurnUpCCW);
                await MakeTurnAsync(Globals.TurnDownCW);
                await MakeTurnAsync(Globals.TurnBackCW);
                goto Line710;
            }
            // 935
            if (cB == Globals.aPieces[21] && cX == Globals.aPieces[14])
            {
                await MakeTurnAsync(Globals.TurnUpCW);
                await MakeTurnAsync(Globals.TurnFrontCW);
                await MakeTurnAsync(Globals.TurnUpCCW);
                await MakeTurnAsync(Globals.TurnDownCCW);
                await MakeTurnAsync(Globals.TurnRightCCW);
                goto Line710;
            }
            // 940
            if (cB == Globals.aPieces[14] && cX == Globals.aPieces[21])
            {
                await MakeTurnAsync(Globals.TurnUp2);
                await MakeTurnAsync(Globals.TurnLeftCCW);
                await MakeTurnAsync(Globals.TurnUpCW);
                await MakeTurnAsync(Globals.TurnDownCCW);
                await MakeTurnAsync(Globals.TurnBackCW);
                goto Line710;
            }
            // 945
            if (cB == Globals.aPieces[41] && cX != Globals.aPieces[10])
            {
                await MakeTurnAsync(Globals.TurnRightCCW);
                await MakeTurnAsync(Globals.TurnUpCCW);
                await MakeTurnAsync(Globals.TurnDownCW);
                await MakeTurnAsync(Globals.TurnBackCW);
                goto Line710;
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
            goto Line710;

        // 1000
        // Solve the middle layer - Chapter 10, page 21
        // 1010
        Line1010:
            nLoopTimes++;
            if (nLoopTimes > 400)
            {
                return false;
            }
            cV = Globals.aPieces[4];
            cX = Globals.aPieces[13];
            cY = Globals.aPieces[22];
            cZ = Globals.aPieces[31];
            O = 0;
            P = 0;
            Q = 0;
            R = 0;
            S = 0;

            // 1015
            if (cV == Globals.aPieces[1] && cX == Globals.aPieces[10] && cY == Globals.aPieces[19])
            {
                O = 1;
            }
            //1020
            if (cV == Globals.aPieces[3] && cV == Globals.aPieces[5])
            {
                P = 1;
            }
            //1025
            if (cX == Globals.aPieces[12] && cX == Globals.aPieces[14])
            {
                Q = 1;
            }
            //1030
            if (cY == Globals.aPieces[21] && cY == Globals.aPieces[23])
            {
                R = 1;
            }
            //1035
            if (cZ == Globals.aPieces[30] && cZ == Globals.aPieces[32])
            {
                S = 1;
            }
            //1040
            if (O == 1 && P == 1 && Q == 1 && R == 1 && S == 1)
            {
                goto Line1510;
            }
            //1050
            if (cV == Globals.aPieces[10])
            {
                await MakeTurnAsync(Globals.TurnUpCW);
                goto Line1010;
            }
            //1060
            if (cV == Globals.aPieces[28])
            {
                await MakeTurnAsync(Globals.TurnUpCCW);
                goto Line1010;
            }
            //1070
            if (cV == Globals.aPieces[19])
            {
                await MakeTurnAsync(Globals.TurnUp2);
                goto Line1010;
            }
            //1080
            if (P == 1 && cX == Globals.aPieces[12] && cZ == Globals.aPieces[32])
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
            goto Line1010;

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
            goto Line1010;

        //1460
        Line1460:
            await MakeTurnAsync(Globals.TurnCubeFrontToLeft);
            goto Line1010;

        //1470
        Line1470:
            await MakeTurnAsync(Globals.TurnCubeFrontToRight);
            goto Line1010;

        //1480
        Line1480:
            await MakeTurnAsync(Globals.TurnCubeFrontToLeft);
            await MakeTurnAsync(Globals.TurnCubeFrontToLeft);
            goto Line1010;

        //1500
        // Bottom layer
        // Corners on the right place
        //1510
        Line1510:
            //1512
            await MakeTurnAsync(Globals.TurnCubeUpToRight);
            await MakeTurnAsync(Globals.TurnCubeUpToRight);
        //1515
        Line1515:
            nLoopTimes++;
            if (nLoopTimes > 400)
            {
                return false;
            }

            cV = Globals.aPieces[4];
            cX = Globals.aPieces[13];
            cY = Globals.aPieces[22];
            cZ = Globals.aPieces[31];
            //1520
            O = 0;
            P = 0;
            Q = 0;
            R = 0;
            if (cV == Globals.aPieces[2] || cV == Globals.aPieces[9] || cV == Globals.aPieces[44])
            {
                O = 1;
            }
            //1525
            if (cX == Globals.aPieces[2] || cX == Globals.aPieces[9] || cX == Globals.aPieces[44])
            {
                P = 1;
            }
            //1530
            if (cX == Globals.aPieces[11] || cX == Globals.aPieces[18] || cX == Globals.aPieces[38])
            {
                Q = 1;
            }
            //1535
            if (cV == Globals.aPieces[0] || cV == Globals.aPieces[29] || cV == Globals.aPieces[42])
            {
                R = 1;
            }
            //1540
            if (O == 1 && P == 1 && Q == 1 && R == 1)
            {
                goto Line1610;
            }
            //1545
            if (O == 1 && P == 1)
            {
                goto Line1560;
            }
            //1550
            O = 0;
            if (cV == Globals.aPieces[11] || cV == Globals.aPieces[18] || cV == Globals.aPieces[38])
            {
                O = 1;
            }
            //1552
            if (O == 1 && Q == 1)
            {
                await MakeTurnAsync(Globals.TurnUpCW);
                goto Line1515;
            }
            //1554
            O = 0;
            if (cX == Globals.aPieces[0] || cX == Globals.aPieces[29] || cX == Globals.aPieces[42])
            {
                O = 1;
            }
            //1556
            if (O == 1 && R == 1)
            {
                await MakeTurnAsync(Globals.TurnUpCCW);
                goto Line1515;
            }
            //1558
            await MakeTurnAsync(Globals.TurnUp2);
            goto Line1515;

        //1560
        Line1560:
            O = 0;
            P = 0;
            if (cV == Globals.aPieces[11] || cV == Globals.aPieces[18] || cV == Globals.aPieces[38])
            {
                O = 1;
            }
            //1565
            if (cX == Globals.aPieces[20] || cX == Globals.aPieces[27] || cX == Globals.aPieces[36])
            {
                P = 1;
            }
            //1570
            if (O == 1 && P == 1)
            {
                await MakeTurnAsync(Globals.TurnLeftCCW);
                await MakeTurnAsync(Globals.TurnUpCW);
                await MakeTurnAsync(Globals.TurnRightCW);
                await MakeTurnAsync(Globals.TurnUpCCW);
                await MakeTurnAsync(Globals.TurnLeftCW);
                await MakeTurnAsync(Globals.TurnUpCW);
                await MakeTurnAsync(Globals.TurnRightCCW);
                await MakeTurnAsync(Globals.TurnUpCCW);
                goto Line1515;
            }
            //1575
            O = 0;
            P = 0;
            if (cV == Globals.aPieces[20] || cV == Globals.aPieces[27] || cV == Globals.aPieces[36])
            {
                O = 1;
            }
            //1580
            if (cX == Globals.aPieces[0] || cX == Globals.aPieces[29] || cX == Globals.aPieces[42])
            {
                P = 1;
            }
            //1582
            if (O == 1 && P == 1)
            {
                await MakeTurnAsync(Globals.TurnUpCW);
                await MakeTurnAsync(Globals.TurnRightCW);
                await MakeTurnAsync(Globals.TurnUpCCW);
                await MakeTurnAsync(Globals.TurnLeftCCW);
                await MakeTurnAsync(Globals.TurnUpCW);
                await MakeTurnAsync(Globals.TurnRightCCW);
                await MakeTurnAsync(Globals.TurnUpCCW);
                await MakeTurnAsync(Globals.TurnLeftCW);
                goto Line1515;
            }
            //1584
            O = 0;
            P = 0;
            if (cX == Globals.aPieces[20] || cX == Globals.aPieces[27] || cX == Globals.aPieces[36])
            {
                O = 1;
            }
            //1586
            if (cZ == Globals.aPieces[11] || cZ == Globals.aPieces[18] || cZ == Globals.aPieces[38])
            {
                P = 1;
            }
            //1588
            if (O == 1 && P == 1)
            {
                await MakeTurnAsync(Globals.TurnFrontCW);
                await MakeTurnAsync(Globals.TurnUpCCW);
                await MakeTurnAsync(Globals.TurnBackCCW);
                await MakeTurnAsync(Globals.TurnUpCW);
                await MakeTurnAsync(Globals.TurnFrontCCW);
                await MakeTurnAsync(Globals.TurnUpCCW);
                await MakeTurnAsync(Globals.TurnBackCW);
                await MakeTurnAsync(Globals.TurnUp2);
                goto Line1515;
            }
            //1590
            O = 0;
            P = 0;
            if (cV == Globals.aPieces[11] || cV == Globals.aPieces[18] || cV == Globals.aPieces[38])
            {
                O = 1;
            }
            //1592
            if (cX == Globals.aPieces[0] || cX == Globals.aPieces[29] || cX == Globals.aPieces[42])
            {
                P = 1;
            }
            //1594
            if (O == 1 && P == 1)
            {
                await MakeTurnAsync(Globals.TurnUpCW);
                await MakeTurnAsync(Globals.TurnFrontCW);
                await MakeTurnAsync(Globals.TurnUpCW);
                await MakeTurnAsync(Globals.TurnRightCW);
                await MakeTurnAsync(Globals.TurnUpCCW);
                await MakeTurnAsync(Globals.TurnRightCCW);
                await MakeTurnAsync(Globals.TurnFrontCCW);
                goto Line1515;

            }
            //1596
            await MakeTurnAsync(Globals.TurnCubeFrontToLeft);
            goto Line1515;

        //1600
        // Edges on the right place
        //1610
        Line1610:
            nLoopTimes++;
            if (nLoopTimes > 400)
            {
                return false;
            }

            cV = Globals.aPieces[4];
            cX = Globals.aPieces[13];
            cY = Globals.aPieces[22];
            cZ = Globals.aPieces[31];
            //1615
            O = 0;
            P = 0;
            Q = 0;
            R = 0;
            if (cV == Globals.aPieces[1] || cV == Globals.aPieces[43])
            {
                O = 1;
            }
            //1620
            if (cX == Globals.aPieces[10] || cX == Globals.aPieces[41])
            {
                P = 1;
            }
            //1625
            if (cY == Globals.aPieces[19] || cY == Globals.aPieces[37])
            {
                Q = 1;
            }
            //1630
            if (cZ == Globals.aPieces[28] || cZ == Globals.aPieces[39])
            {
                R = 1;
            }
            //1635
            if (O == 1 && P == 1 && Q == 1 && R == 1)
            {
                goto Line1710;
            }
            //1640
            O = 0;
            P = 0;
            if (cV == Globals.aPieces[19] || cV == Globals.aPieces[37])
            {
                O = 1;
            }
            //1645
            if (cY == Globals.aPieces[28] || cY == Globals.aPieces[39])
            {
                P = 1;
            }
            //1650
            if (O == 1 && P == 1)
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
                goto Line1610;
            }
            //1655
            O = 0;
            P = 0;
            if (cV == Globals.aPieces[28] || cV == Globals.aPieces[39])
            {
                O = 1;
            }
            //1660
            if (cY == Globals.aPieces[1] || cY == Globals.aPieces[43])
            {
                P = 1;
            }
            //1665
            if (O == 1 && P == 1)
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
                goto Line1610;
            }
            //1670
            O = 0;
            P = 0;
            if (cV == Globals.aPieces[19] || cV == Globals.aPieces[37])
            {
                O = 1;
            }
            //1675
            if (cY == Globals.aPieces[1] || cY == Globals.aPieces[43])
            {
                P = 1;
            }
            //1680
            if (O == 1 && P == 1)
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
                goto Line1610;
            }
            //1685
            O = 0;
            P = 0;
            if (cV == Globals.aPieces[28] || cV == Globals.aPieces[39])
            {
                O = 1;
            }
            //1690
            if (cX == Globals.aPieces[19] || cX == Globals.aPieces[37])
            {
                P = 1;
            }
            //1692
            if (O == 1 && P == 1)
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
            goto Line1610;

        //1700
        // Tumbling corners
        //1710
        Line1710:
            nLoopTimes++;
            if (nLoopTimes > 400)
            {
                return false;
            }

            cB = Globals.aPieces[40];
            if (cB == Globals.aPieces[36] && cB == Globals.aPieces[38] && cB == Globals.aPieces[42] && cB == Globals.aPieces[44])
            {
                goto Line1810;
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
                goto Line1810;
            }
            //1770
            await MakeTurnAsync(Globals.TurnUpCW);
            goto Line1710;

        //1800
        // Tumbling edges
        //1810
        Line1810:
            nLoopTimes++;
            if (nLoopTimes > 400)
            {
                return false;
            }

            cB = Globals.aPieces[40];
            cV = Globals.aPieces[4];
            cX = Globals.aPieces[13];
            cY = Globals.aPieces[22];
            O = 0;
            P = 0;
            //1815
            if (cB == Globals.aPieces[37] && cB == Globals.aPieces[39] && cB == Globals.aPieces[41] && cB == Globals.aPieces[43])
            {
                O = 1;
            }
            //1820
            if (cV == Globals.aPieces[1] && cX == Globals.aPieces[10] && cY == Globals.aPieces[19])
            {
                P = 1;
            }
            //1825
            if (O == 1 && P == 1)
            {
                goto Line2010;
            }
            //1830
            if (O == 0)
            {
                goto Line1850;
            }
            //1835
            if (cV == Globals.aPieces[10])
            {
                await MakeTurnAsync(Globals.TurnUpCW);
                goto Line1810;
            }
            //1840
            if (cV == Globals.aPieces[28])
            {
                await MakeTurnAsync(Globals.TurnUpCCW);
                goto Line1810;
            }
            //1845
            if (cV == Globals.aPieces[19])
            {
                await MakeTurnAsync(Globals.TurnUp2);
                goto Line1810;
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
            goto Line1810;

        //2010
        // Check if the cube is solved
        Line2010:
            if (ClassCheckColorsCube.CheckIfSolved())
            {
                return true;
            }

            return false;
        }

        // Make a turn of the cube/face/side
        private async Task MakeTurnAsync(string cTurnFaceAndDirection)
        {
            // Add the turn to the list
            Globals.lCubeTurns.Add(cTurnFaceAndDirection);

            // Turn the cube/face/side
            ClassCubeTurns classCubeTurns = new();
            await classCubeTurns.TurnFaceCubeAsync(cTurnFaceAndDirection);
        }
    }
}
