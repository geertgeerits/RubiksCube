namespace RubiksCube
{
    internal class ClassSolveCubeBas
    {
        // Note: comment out the lines after 710 to before 1010 to get the cross on the top layer,
        //       and change 'return false' to 'return true'
        
        // Solve the cube.  From Basic-80 to C#
        public async Task<bool> SolveTheCubeBasAsync()
        {
            // Declare variables
            int O, P, Q, R, S, V, X, Y, Z;
            string cB, cO, cP, cQ, cR, cV, cX, cY, cZ;
            int nLoopTimes = 0;
            
        // 500
        // Top layer
        // Solve the edges of the top layer - Chapter 4, page 14-3

        // !!!!!!!!!!!!! Does not get out of the loop between line 510 and 695 !!!!!!!!!!!!!

        Line510:
            cB = Globals.aPieces[40];
            V = 0;
            X = 0;
            Y = 0;
            Z = 0;
            nLoopTimes++;
            if (nLoopTimes > 100)
            {
                return true;  // false;
            }
            if (cB == Globals.aPieces[43] && Globals.aPieces[0] == Globals.aPieces[1])
                V = 1;
            // 520
            if (cB == Globals.aPieces[41] && Globals.aPieces[9] == Globals.aPieces[10])
                X = 1;
            // 530
            if (cB == Globals.aPieces[37] && Globals.aPieces[18] == Globals.aPieces[19])
                Y = 1;
            // 540
            if (cB == Globals.aPieces[39] && Globals.aPieces[27] == Globals.aPieces[28])
                Z = 1;
            // 550
            if (V == 1 && X == 1 && Y == 1 && Z == 1)
                goto Line710;
            // 560
            O = 0;
            P = 0;
            Q = 0;

            if (cB == Globals.aPieces[5] || cB == Globals.aPieces[10] || cB == Globals.aPieces[12] || cB == Globals.aPieces[14])
                O = 1;
            // 570
            if (cB == Globals.aPieces[21] || cB == Globals.aPieces[16] || cB == Globals.aPieces[50])
                P = 1;
            // 580
            //cX = X.ToString();
            //if (cB == Globals.aPieces[41] && cX != Globals.aPieces[10])         // 580 IF B = D(41) AND X <> D(10) THEN Q = 1
            //    Q = 1;
            if (cB == Globals.aPieces[41])                                  // 580 IF B = D(41) AND X <> D(10) THEN Q = 1
                Q = 1;
            // 590
            if (O == 1 || P == 1 || Q == 1)
                goto Line610;
            // 600
            await MakeTurnAsync("TurnCubeFrontToLeft");
            goto Line510;

        Line610:
            if (V == 1 && Y == 1 && Z == 1)
                goto Line650;
            // 620
            if (Y == 1 && Z == 1)
            {
                await MakeTurnAsync("TurnUp-");
                goto Line650;
            }
            // 630
            if (Y == 1)
            {
                await MakeTurnAsync("TurnUp++");
                goto Line650;
            }
            // 640
            await MakeTurnAsync("TurnUp+");
        // 650
        Line650:
            cX = Globals.aPieces[9];
            if (cB == Globals.aPieces[10] && cX == Globals.aPieces[41])
            {
                await MakeTurnAsync("TurnRight-");
                await MakeTurnAsync("TurnUp-");
                await MakeTurnAsync("TurnDown+");
                await MakeTurnAsync("TurnBack+");
                await MakeTurnAsync("TurnUp-");
                await MakeTurnAsync("TurnLeft+");
                await MakeTurnAsync("TurnUp-");
                await MakeTurnAsync("TurnDown+");
                await MakeTurnAsync("TurnFront-");
                goto Line510;
            }
            // 655
            if (cB == Globals.aPieces[5] && cX == Globals.aPieces[12])
            {
                await MakeTurnAsync("TurnUp-");
                await MakeTurnAsync("TurnBack-");
                await MakeTurnAsync("TurnUp+");
                await MakeTurnAsync("TurnDown-");
                await MakeTurnAsync("TurnRight+");
                goto Line510;
            }
            // 660
            if (cB == Globals.aPieces[12] && cX == Globals.aPieces[5])
            {
                await MakeTurnAsync("TurnUp--");
                await MakeTurnAsync("TurnLeft+");
                await MakeTurnAsync("TurnUp-");
                await MakeTurnAsync("TurnDown+");
                await MakeTurnAsync("TurnFront-");
                goto Line510;
            }
            // 665
            if (cB == Globals.aPieces[50] && cX == Globals.aPieces[16])
            {
                await MakeTurnAsync("TurnRight-");
                await MakeTurnAsync("TurnUp--");
                await MakeTurnAsync("TurnDown++");
                await MakeTurnAsync("TurnLeft+");
                await MakeTurnAsync("TurnUp-");
                await MakeTurnAsync("TurnFront+");
                await MakeTurnAsync("TurnUp-");
                await MakeTurnAsync("TurnDown+");
                await MakeTurnAsync("TurnRight-");
                goto Line510;
            }
            // 670
            if (cB == Globals.aPieces[16] && cX == Globals.aPieces[50])
            {
                await MakeTurnAsync("TurnRight-");
                await MakeTurnAsync("TurnUp-");
                await MakeTurnAsync("TurnDown+");
                await MakeTurnAsync("TurnBack+");
                goto Line510;
            }
            // 675
            if (cB == Globals.aPieces[21] && cX == Globals.aPieces[14])
            {
                await MakeTurnAsync("TurnUp+");
                await MakeTurnAsync("TurnFront+");
                await MakeTurnAsync("TurnUp-");
                await MakeTurnAsync("TurnDown-");
                await MakeTurnAsync("TurnRight-");
                goto Line510;
            }
            // 680
            if (cB == Globals.aPieces[14] && cX == Globals.aPieces[21])
            {
                await MakeTurnAsync("TurnUp--");
                await MakeTurnAsync("TurnLeft-");
                await MakeTurnAsync("TurnUp+");
                await MakeTurnAsync("TurnDown-");
                await MakeTurnAsync("TurnBack+");
                goto Line510;
            }
            // 685
            if (cB == Globals.aPieces[41] && cX != Globals.aPieces[10])
            {
                await MakeTurnAsync("TurnRight-");
                await MakeTurnAsync("TurnUp-");
                await MakeTurnAsync("TurnDown+");
                await MakeTurnAsync("TurnBack+");
                goto Line510;
            }
            // 690
            if (cB == Globals.aPieces[10] && cX != Globals.aPieces[41])
            {
                await MakeTurnAsync("TurnRight-");
                await MakeTurnAsync("TurnUp-");
                await MakeTurnAsync("TurnDown+");
                await MakeTurnAsync("TurnBack+");
            }
            // 695
            goto Line510;

        // Solve the corners of the top layer - Chapter 6, page 16
        Line710:
            cB = Globals.aPieces[40];
            O = 0;
            P = 0;
            Q = 0;
            R = 0;
            // 715
            if (cB == Globals.aPieces[36] && cB == Globals.aPieces[38] && cB == Globals.aPieces[42] && cB == Globals.aPieces[44])
                O = 1;
            // 720
            if (Globals.aPieces[0] == Globals.aPieces[2])
                P = 1;
            // 725
            if (Globals.aPieces[9] == Globals.aPieces[11])
                Q = 1;
            // 730
            if (Globals.aPieces[18] == Globals.aPieces[20])
                R = 1;
            // 735
            if (O == 1 && P == 1 && Q == 1 && R == 1)
                goto Line1010;
            // 740
            O = 0;
            if (cB == Globals.aPieces[38] && cB == Globals.aPieces[42] && cB == Globals.aPieces[44])
                O = 1;
            // 745
            if (O == 1 && P == 1 && Q == 1)
            {
                await MakeTurnAsync("TurnUp++");
                goto Line800;
            }
            // 750
            O = 0;
            if (cB == Globals.aPieces[38] && cB == Globals.aPieces[44])
                O = 1;
            // 755
            if (O == 1 && Q == 1)
            {
                await MakeTurnAsync("TurnUp++");
                goto Line800;
            }
            // 760
            O = 0;
            if (cB == Globals.aPieces[42] && cB == Globals.aPieces[44])
                O = 1;
            // 765
            if (O == 1 && P == 1)
            {
                await MakeTurnAsync("TurnUp+");
                goto Line800;
            }
            // 770
            O = 0;
            if (cB == Globals.aPieces[36] && cB == Globals.aPieces[38])
                O = 1;
            // 775
            if (O == 1 && R == 1)
            {
                await MakeTurnAsync("TurnUp-");
                goto Line800;
            }
            // 780
            if (cB != Globals.aPieces[44])
                goto Line800;
            // 785
            if (cB != Globals.aPieces[38])
            {
                await MakeTurnAsync("TurnUp+");
                goto Line800;
            }
            // 790
            if (cB != Globals.aPieces[42])
            {
                await MakeTurnAsync("TurnUp-");
                goto Line800;
            }
            // 795
            if (cB != Globals.aPieces[36])
            {
                await MakeTurnAsync("TurnUp++");
            }
        // 800
        Line800:
            if (cB == Globals.aPieces[8] || cB == Globals.aPieces[15] || cB == Globals.aPieces[47])
                goto Line880;
            // 805
            if (cB == Globals.aPieces[17] || cB == Globals.aPieces[24] || cB == Globals.aPieces[53])
            {
                await MakeTurnAsync("TurnDown-");
                goto Line880;
            }
            // 810
            if (cB == Globals.aPieces[6] || cB == Globals.aPieces[35] || cB == Globals.aPieces[45])
            {
                await MakeTurnAsync("TurnDown+");
                goto Line880;
            }
            // 815
            if (cB == Globals.aPieces[26] || cB == Globals.aPieces[33] || cB == Globals.aPieces[51])
            {
                await MakeTurnAsync("TurnDown++");
                goto Line880;
            }
            // 870
            await MakeTurnAsync("TurnRight++");
            goto Line710;
        // 880
        Line880:
            if (cB == Globals.aPieces[8])
            {
                await MakeTurnAsync("TurnFront+");
                await MakeTurnAsync("TurnDown+");
                await MakeTurnAsync("TurnFront-");
                goto Line710;
            }
            // 885
            if (cB == Globals.aPieces[15])
            {
                await MakeTurnAsync("TurnRight-");
                await MakeTurnAsync("TurnDown-");
                await MakeTurnAsync("TurnRight+");
                goto Line710;
            }
            // 890
            if (cB == Globals.aPieces[47])
            {
                await MakeTurnAsync("TurnRight-");
                await MakeTurnAsync("TurnDown+");
                await MakeTurnAsync("TurnRight+");
                await MakeTurnAsync("TurnDown++");
                await MakeTurnAsync("TurnRight-");
                await MakeTurnAsync("TurnDown-");
                await MakeTurnAsync("TurnRight+");
            }
            // 895
            goto Line710;

        // 1000
        // Solve the middle layer - Chapter 10, page 21
        Line1010:
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
                O = 1;
            //1020
            if (cV == Globals.aPieces[3] && cV == Globals.aPieces[5])
                P = 1;
            //1025
            if (cX == Globals.aPieces[12] && cX == Globals.aPieces[14])
                Q = 1;
            //1030
            if (cY == Globals.aPieces[21] && cY == Globals.aPieces[23])
                R = 1;
            //1035
            if (cZ == Globals.aPieces[30] && cZ == Globals.aPieces[32])
                S = 1;
            //1040
            if (O == 1 && P == 1 && Q == 1 && R == 1 && S == 1)
                goto Line1510;
            //1050
            if (cV == Globals.aPieces[10])
            {
                await MakeTurnAsync("TurnUp+");
                goto Line1010;
            }
            //1060
            if (cV == Globals.aPieces[28])
            {
                await MakeTurnAsync("TurnUp-");
                goto Line1010;
            }
            //1070
            if (cV == Globals.aPieces[19])
            {
                await MakeTurnAsync("TurnUp++");
                goto Line1010;
            }
            //1080
            if (P == 1 && cX == Globals.aPieces[12] && cZ == Globals.aPieces[32])
                goto Line1460;

            //1100
            cO = Globals.aPieces[7];
            cP = Globals.aPieces[16];
            cQ = Globals.aPieces[25];
            cR = Globals.aPieces[34];
            
            //1110
            if (cV == cO && cX == Globals.aPieces[46])
                goto Line1410;
            //1120
            if (cV == cO && cZ == Globals.aPieces[46])
                goto Line1420;
            //1130
            if (cV == cP && cX == Globals.aPieces[50])
            {
                await MakeTurnAsync("TurnDown-");
                goto Line1410;
            }
            //1140
            if (cV == cP && cZ == Globals.aPieces[50])
            {
                await MakeTurnAsync("TurnDown-");
                goto Line1420;
            }
            //1150
            if (cV == cQ && cX == Globals.aPieces[52])
            {
                await MakeTurnAsync("TurnDown++");
                goto Line1410;
            }
            //1160
            if (cV == cQ && cZ == Globals.aPieces[52])
            {
                await MakeTurnAsync("TurnDown++");
                goto Line1420;
            }
            //1170
            if (cV == cR && cX == Globals.aPieces[48])
            {
                await MakeTurnAsync("TurnDown+");
                goto Line1410;
            }
            //1180
            if (cV == cR && cZ == Globals.aPieces[48])
            {
                await MakeTurnAsync("TurnDown+");
                goto Line1420;
            }
            //1210
            if (cX == cO && cV == Globals.aPieces[46])
                goto Line1460;
            //1215
            if (cX == cO && cY == Globals.aPieces[46])
                goto Line1460;
            //1220
            if (cX == cP && cV == Globals.aPieces[50])
                goto Line1460;
            //1225
            if (cX == cP && cY == Globals.aPieces[50])
                goto Line1460;
            //1230
            if (cX == cQ && cV == Globals.aPieces[52])
                goto Line1460;
            //1235
            if (cX == cQ && cY == Globals.aPieces[52])
                goto Line1460;
            //1240
            if (cX == cR && cV == Globals.aPieces[48])
                goto Line1460;
            //1245
            if (cX == cR && cY == Globals.aPieces[48])
                goto Line1460;
            //1250
            if (cZ == cO && cV == Globals.aPieces[46])
                goto Line1470;
            //1255
            if (cZ == cO && cY == Globals.aPieces[46])
                goto Line1470;
            //1260
            if (cZ == cP && cV == Globals.aPieces[50])
                goto Line1470;
            //1265
            if (cZ == cP && cY == Globals.aPieces[50])
                goto Line1470;
            //1270
            if (cZ == cQ && cV == Globals.aPieces[52])
                goto Line1470;
            //1275
            if (cZ == cQ && cY == Globals.aPieces[52])
                goto Line1470;
            //1280
            if (cZ == cR && cV == Globals.aPieces[48])
                goto Line1470;
            //1285
            if (cZ == cR && cY == Globals.aPieces[48])
                goto Line1470;
            //1290
            if (cY == cO && cX == Globals.aPieces[46])
                goto Line1480;
            //1295
            if (cY == cO && cZ == Globals.aPieces[46])
                goto Line1480;
            //1300
            if (cY == cP && cX == Globals.aPieces[50])
                goto Line1480;
            //1305
            if (cY == cP && cZ == Globals.aPieces[50])
                goto Line1480;
            //1310
            if (cY == cQ && cX == Globals.aPieces[52])
                goto Line1480;
            //1315
            if (cY == cQ && cZ == Globals.aPieces[52])
                goto Line1480;
            //1320
            if (cY == cR && cX == Globals.aPieces[48])
                goto Line1480;
            //1325
            if (cY == cR && cZ == Globals.aPieces[48])
                goto Line1480;
            //1360
            if (cV != Globals.aPieces[5])
                goto Line1410;
            //1370
            if (cV != Globals.aPieces[3])
                goto Line1420;
            //1380
            await MakeTurnAsync("TurnCubeFrontToLeft");

        //1410
        Line1410:
            await MakeTurnAsync("TurnDown-");
            await MakeTurnAsync("TurnRight-");
            await MakeTurnAsync("TurnDown+");
            await MakeTurnAsync("TurnRight+");
            await MakeTurnAsync("TurnDown+");
            await MakeTurnAsync("TurnFront+");
            await MakeTurnAsync("TurnDown-");
            await MakeTurnAsync("TurnFront-");
            goto Line1010;

        //1420
        Line1420:
            await MakeTurnAsync("TurnDown+");
            await MakeTurnAsync("TurnLeft+");
            await MakeTurnAsync("TurnDown-");
            await MakeTurnAsync("TurnLeft-");
            await MakeTurnAsync("TurnDown-");
            await MakeTurnAsync("TurnFront-");
            await MakeTurnAsync("TurnDown+");
            await MakeTurnAsync("TurnFront+");
            goto Line1010;

        //1460
        Line1460:
            await MakeTurnAsync("TurnCubeFrontToLeft");
            goto Line1010;

        //1470
        Line1470:
            await MakeTurnAsync("TurnCubeFrontToRight");
            goto Line1010;

        //1480
        Line1480:
            await MakeTurnAsync("TurnCubeFrontToLeft");
            await MakeTurnAsync("TurnCubeFrontToLeft");

        //1500
        // Bottom layer
        // Corners on the right place
        //1510
        Line1510:





            if (ClassCheckColorsCube.CheckIfSolved())
            {
                return true;
            }
            
            return true;        // false
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
