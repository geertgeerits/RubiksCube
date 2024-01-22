namespace RubiksCube
{
    internal class ClassSolveCubeBas
    {
        // Note: delete the lines after 710 to before 1010 to get the cross on the top layer,
        //       and change 'return false' to 'return true'.
        
        // Solve the cube.  From Basic-80 to C#.
        public async Task<bool> SolveTheCubeBasAsync()
        {
            // Declare variables.
            int O, P, Q, R, V, X, Y, Z;
            string cB;
            string cX;
            int nLoopTimes = 0;
            
        // 500
        // Top layer.
        // Solve the edges of the top layer - Chapter 4, page 14-3.

        // !!!!!!!!!!!!! Does not get out of the loop between line 510 and 695 !!!!!!!!!!!!!

        Line510:
            cB = Globals.aUpFace[5];
            V = 0;
            X = 0;
            Y = 0;
            Z = 0;
            nLoopTimes++;
            if (nLoopTimes > 200)
            {
                return true;        // false
            }
            if (cB == Globals.aUpFace[8] && Globals.aFrontFace[1] == Globals.aFrontFace[2])
                V = 1;
            // 520
            if (cB == Globals.aUpFace[6] && Globals.aRightFace[1] == Globals.aRightFace[2])
                X = 1;
            // 530
            if (cB == Globals.aUpFace[2] && Globals.aBackFace[1] == Globals.aBackFace[2])
                Y = 1;
            // 540
            if (cB == Globals.aUpFace[4] && Globals.aLeftFace[1] == Globals.aLeftFace[2])
                Z = 1;
            // 550
            if (V == 1 && X == 1 && Y == 1 && Z == 1)
                goto Line710;
            // 560
            O = 0;
            P = 0;
            Q = 0;

            if (cB == Globals.aFrontFace[6] || cB == Globals.aRightFace[2] || cB == Globals.aRightFace[4] || cB == Globals.aRightFace[6])
                O = 1;
            // 570
            if (cB == Globals.aBackFace[4] || cB == Globals.aRightFace[8] || cB == Globals.aDownFace[6])
                P = 1;
            // 580
            cX = X.ToString();
            //if (cB == Globals.aUpFace[6] && cX != Globals.aRightFace[2])         // 580 IF B = D(41) AND X <> D(10) THEN Q = 1
            //    Q = 1;
            if (cB == Globals.aUpFace[6])                                  // 580 IF B = D(41) AND X <> D(10) THEN Q = 1
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
            cX = Globals.aRightFace[1];
            if (cB == Globals.aRightFace[2] && cX == Globals.aUpFace[6])
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
            if (cB == Globals.aFrontFace[6] && cX == Globals.aRightFace[4])
            {
                await MakeTurnAsync("TurnUp-");
                await MakeTurnAsync("TurnBack-");
                await MakeTurnAsync("TurnUp+");
                await MakeTurnAsync("TurnDown-");
                await MakeTurnAsync("TurnRight+");
                goto Line510;
            }
            // 660
            if (cB == Globals.aRightFace[4] && cX == Globals.aFrontFace[6])
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
            if (cB == Globals.aDownFace[6] && cX == Globals.aRightFace[8])
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
            if (cB == Globals.aRightFace[8] && cX == Globals.aDownFace[6])
            {
                await MakeTurnAsync("TurnRight-");
                await MakeTurnAsync("TurnUp-");
                await MakeTurnAsync("TurnDown+");
                await MakeTurnAsync("TurnBack+");
                goto Line510;
            }
            // 675
            if (cB == Globals.aBackFace[4] && cX == Globals.aRightFace[6])
            {
                await MakeTurnAsync("TurnUp+");
                await MakeTurnAsync("TurnFront+");
                await MakeTurnAsync("TurnUp-");
                await MakeTurnAsync("TurnDown-");
                await MakeTurnAsync("TurnRight-");
                goto Line510;
            }
            // 680
            if (cB == Globals.aRightFace[6] && cX == Globals.aBackFace[4])
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
            if (cB == Globals.aUpFace[6] && cX != Globals.aRightFace[2])
            {
                await MakeTurnAsync("TurnRight-");
                await MakeTurnAsync("TurnUp-");
                await MakeTurnAsync("TurnDown+");
                await MakeTurnAsync("TurnBack+");
                goto Line510;
            }
            // 690
            if (cB == Globals.aRightFace[2] && cX != Globals.aUpFace[6])
            {
                await MakeTurnAsync("TurnRight-");
                await MakeTurnAsync("TurnUp-");
                await MakeTurnAsync("TurnDown+");
                await MakeTurnAsync("TurnBack+");
            }
            // 695
            goto Line510;

        // Solve the corners of the top layer - Chapter 6, page 16.
        Line710:
        //    cB = Globals.aUpFace[5];
        //    O = 0;
        //    P = 0;
        //    Q = 0;
        //    R = 0;
        //    // 715
        //    if (cB == Globals.aUpFace[1] && cB == Globals.aUpFace[3] && cB == Globals.aUpFace[7] && cB == Globals.aUpFace[9])
        //        O = 1;
        //    // 720
        //    if (Globals.aFrontFace[1] == Globals.aFrontFace[3])
        //        P = 1;
        //    // 725
        //    if (Globals.aRightFace[1] == Globals.aRightFace[3])
        //        Q = 1;
        //    // 730
        //    if (Globals.aBackFace[1] == Globals.aBackFace[3])
        //        R = 1;
        //    // 735
        //    if (O == 1 && P == 1 && Q == 1 && R == 1)
        //        goto Line1010;
        //    // 740
        //    O = 0;
        //    if (cB == Globals.aUpFace[3] && cB == Globals.aUpFace[7] && cB == Globals.aUpFace[9])
        //        O = 1;
        //    // 745
        //    if (O == 1 && P == 1 && Q == 1)
        //    {
        //        await MakeTurnAsync("TurnUp++");
        //        goto Line800;
        //    }
        //    // 750
        //    O = 0;
        //    if (cB == Globals.aUpFace[3] && cB == Globals.aUpFace[9])
        //        O = 1;
        //    // 755
        //    if (O == 1 && Q == 1)
        //    {
        //        await MakeTurnAsync("TurnUp++");
        //        goto Line800;
        //    }
        //    // 760
        //    O = 0;
        //    if (cB == Globals.aUpFace[7] && cB == Globals.aUpFace[9])
        //        O = 1;
        //    // 765
        //    if (O == 1 && P == 1)
        //    {
        //        await MakeTurnAsync("TurnUp+");
        //        goto Line800;
        //    }
        //    // 770
        //    O = 0;
        //    if (cB == Globals.aUpFace[1] && cB == Globals.aUpFace[3])
        //        O = 1;
        //    // 775
        //    if (O == 1 && R == 1)
        //    {
        //        await MakeTurnAsync("TurnUp-");
        //        goto Line800;
        //    }
        //    // 780
        //    if (cB != Globals.aUpFace[9])
        //        goto Line800;
        //    // 785
        //    if (cB != Globals.aUpFace[3])
        //    {
        //        await MakeTurnAsync("TurnUp+");
        //        goto Line800;
        //    }
        //    // 790
        //    if (cB != Globals.aUpFace[7])
        //    {
        //        await MakeTurnAsync("TurnUp-");
        //        goto Line800;
        //    }
        //    // 795
        //    if (cB != Globals.aUpFace[1])
        //    {
        //        await MakeTurnAsync("TurnUp++");
        //    }
        //// 800
        //Line800:
        //    if (cB == Globals.aFrontFace[9] || cB == Globals.aRightFace[7] || cB == Globals.aDownFace[3])
        //        goto Line880;
        //    // 805
        //    if (cB == Globals.aRightFace[9] || cB == Globals.aBackFace[7] || cB == Globals.aDownFace[9])
        //    {
        //        await MakeTurnAsync("TurnDown-");
        //        goto Line880;
        //    }
        //    // 810
        //    if (cB == Globals.aFrontFace[7] || cB == Globals.aLeftFace[9] || cB == Globals.aDownFace[1])
        //    {
        //        await MakeTurnAsync("TurnDown+");
        //        goto Line880;
        //    }
        //    // 815
        //    if (cB == Globals.aBackFace[9] || cB == Globals.aLeftFace[7] || cB == Globals.aDownFace[7])
        //    {
        //        await MakeTurnAsync("TurnDown++");
        //        goto Line880;
        //    }
        //    // 870
        //    await MakeTurnAsync("TurnRight++");
        //    goto Line710;
        //// 880
        //Line880:
        //    if (cB == Globals.aFrontFace[9])
        //    {
        //        await MakeTurnAsync("TurnFront+");
        //        await MakeTurnAsync("TurnDown+");
        //        await MakeTurnAsync("TurnFront-");
        //        goto Line710;
        //    }
        //    // 885
        //    if (cB == Globals.aRightFace[7])
        //    {
        //        await MakeTurnAsync("TurnRight-");
        //        await MakeTurnAsync("TurnDown-");
        //        await MakeTurnAsync("TurnRight+");
        //        goto Line710;
        //    }
        //    // 890
        //    if (cB == Globals.aDownFace[3])
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
        //// Solve the middle layer - Chapter 10, page 21.
        Line1010:;




            if (ClassCheckColorsCube.CheckIfSolved())
            {
                return true;
            }
            
            return true;        // false
        }

        // Make a turn of the cube/face/side.
        private async Task MakeTurnAsync(string cTurnFaceAndDirection)
        {
            // Add the turn to the list.
            Globals.lCubeTurns.Add(cTurnFaceAndDirection);

            // Turn the cube/face/side.
            ClassCubeTurns classCubeTurns = new();
            await classCubeTurns.TurnFaceCubeAsync(cTurnFaceAndDirection);
        }
    }
}
