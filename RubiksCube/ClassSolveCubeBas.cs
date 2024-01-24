namespace RubiksCube
{
    internal class ClassSolveCubeBas
    {
        // Note: delete the lines after 710 to before 1010 to get the cross on the top layer,
        //       and change 'return false' to 'return true'
        
        // Solve the cube.  From Basic-80 to C#
        public async Task<bool> SolveTheCubeBasAsync()
        {
            // Declare variables
            int O, P, Q, R, V, X, Y, Z;
            string cB;
            string cX;
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
            if (nLoopTimes > 200)
            {
                return true;        // false
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
                await MakeTurnAsync("TurnUp-");
                await MakeTurnAsync("TurnUp-");
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
                await MakeTurnAsync("TurnUp-");
                await MakeTurnAsync("TurnUp-");
                await MakeTurnAsync("TurnDown+");
                await MakeTurnAsync("TurnDown+");
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
                await MakeTurnAsync("TurnUp-");
                await MakeTurnAsync("TurnUp-");
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
        //    cB = Globals.aPieces[40];
        //    O = 0;
        //    P = 0;
        //    Q = 0;
        //    R = 0;
        //    // 715
        //    if (cB == Globals.aPieces[36] && cB == Globals.aPieces[38] && cB == Globals.aPieces[42] && cB == Globals.aPieces[44])
        //        O = 1;
        //    // 720
        //    if (Globals.aPieces[0] == Globals.aPieces[2])
        //        P = 1;
        //    // 725
        //    if (Globals.aPieces[9] == Globals.aPieces[11])
        //        Q = 1;
        //    // 730
        //    if (Globals.aPieces[18] == Globals.aPieces[20])
        //        R = 1;
        //    // 735
        //    if (O == 1 && P == 1 && Q == 1 && R == 1)
        //        goto Line1010;
        //    // 740
        //    O = 0;
        //    if (cB == Globals.aPieces[38] && cB == Globals.aPieces[42] && cB == Globals.aPieces[44])
        //        O = 1;
        //    // 745
        //    if (O == 1 && P == 1 && Q == 1)
        //    {
        //        await MakeTurnAsync("TurnUp++");
        //        goto Line800;
        //    }
        //    // 750
        //    O = 0;
        //    if (cB == Globals.aPieces[38] && cB == Globals.aPieces[44])
        //        O = 1;
        //    // 755
        //    if (O == 1 && Q == 1)
        //    {
        //        await MakeTurnAsync("TurnUp++");
        //        goto Line800;
        //    }
        //    // 760
        //    O = 0;
        //    if (cB == Globals.aPieces[42] && cB == Globals.aPieces[44])
        //        O = 1;
        //    // 765
        //    if (O == 1 && P == 1)
        //    {
        //        await MakeTurnAsync("TurnUp+");
        //        goto Line800;
        //    }
        //    // 770
        //    O = 0;
        //    if (cB == Globals.aPieces[36] && cB == Globals.aPieces[38])
        //        O = 1;
        //    // 775
        //    if (O == 1 && R == 1)
        //    {
        //        await MakeTurnAsync("TurnUp-");
        //        goto Line800;
        //    }
        //    // 780
        //    if (cB != Globals.aPieces[44])
        //        goto Line800;
        //    // 785
        //    if (cB != Globals.aPieces[38])
        //    {
        //        await MakeTurnAsync("TurnUp+");
        //        goto Line800;
        //    }
        //    // 790
        //    if (cB != Globals.aPieces[42])
        //    {
        //        await MakeTurnAsync("TurnUp-");
        //        goto Line800;
        //    }
        //    // 795
        //    if (cB != Globals.aPieces[36])
        //    {
        //        await MakeTurnAsync("TurnUp++");
        //    }
        //// 800
        //Line800:
        //    if (cB == Globals.aPieces[8] || cB == Globals.aPieces[15] || cB == Globals.aPieces[47])
        //        goto Line880;
        //    // 805
        //    if (cB == Globals.aPieces[17] || cB == Globals.aPieces[24] || cB == Globals.aPieces[53])
        //    {
        //        await MakeTurnAsync("TurnDown-");
        //        goto Line880;
        //    }
        //    // 810
        //    if (cB == Globals.aPieces[6] || cB == Globals.aPieces[35] || cB == Globals.aPieces[45])
        //    {
        //        await MakeTurnAsync("TurnDown+");
        //        goto Line880;
        //    }
        //    // 815
        //    if (cB == Globals.aPieces[26] || cB == Globals.aPieces[33] || cB == Globals.aPieces[51])
        //    {
        //        await MakeTurnAsync("TurnDown++");
        //        goto Line880;
        //    }
        //    // 870
        //    await MakeTurnAsync("TurnRight++");
        //    goto Line710;
        //// 880
        //Line880:
        //    if (cB == Globals.aPieces[8])
        //    {
        //        await MakeTurnAsync("TurnFront+");
        //        await MakeTurnAsync("TurnDown+");
        //        await MakeTurnAsync("TurnFront-");
        //        goto Line710;
        //    }
        //    // 885
        //    if (cB == Globals.aPieces[15])
        //    {
        //        await MakeTurnAsync("TurnRight-");
        //        await MakeTurnAsync("TurnDown-");
        //        await MakeTurnAsync("TurnRight+");
        //        goto Line710;
        //    }
        //    // 890
        //    if (cB == Globals.aPieces[47])
        //    {
        //        await MakeTurnAsync("TurnRight-");
        //        await MakeTurnAsync("TurnDown+");
        //        await MakeTurnAsync("TurnRight+");
        //        await MakeTurnAsync("TurnDown+");
        //        await MakeTurnAsync("TurnDown+");
        //        await MakeTurnAsync("TurnRight-");
        //        await MakeTurnAsync("TurnDown-");
        //        await MakeTurnAsync("TurnRight+");
        //    }
        //    // 895
        //    goto Line710;

        //// 1000
        //// Solve the middle layer - Chapter 10, page 21
        Line1010:;




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
