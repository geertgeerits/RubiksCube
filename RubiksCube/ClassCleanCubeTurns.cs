using System.Diagnostics;
using static RubiksCube.Globals;

namespace RubiksCube
{
    internal sealed class ClassCleanCubeTurns
    {
        //// Declare the variables
        private const string cNone = "None";
        private const char cApos = '\'';
        private const char c2 = '2';

        /// <summary>
        /// Clean the list with the cube turns by replacing or removing turns
        /// <para>The list is passed by reference. This means that the method receives a reference to the original list, not a copy of it. Therefore, any changes made to the list within the method will affect the original list.</para>
        /// </summary>
        /// <param name="lCubeTurnsToClean"></param>
        /// <param name="bCubeTurnsSave"></param>
        public static void CleanListCubeTurns(List<string> lCubeTurnsToClean, bool bCubeTurnsSave)
        {
            //long startTime = Stopwatch.GetTimestamp();
#if DEBUG
            // Save the list with the cube turns before the cleaning to a file, for testing purposes
            if (bCubeTurnsSave)
            {
                _ = ClassSaveRestoreCube.CubeTurnsSave("CubeTurnsBeforeClean.txt");
            }
#endif
            if (lCubeTurns.Count < 5)
            {
                return;
            }

            // Do the cleaning between 2 and 9 times
            for (int nNumberCleanings = 1; nNumberCleanings < 10; nNumberCleanings++)
            {
                for (int i = 0; i < lCubeTurnsToClean.Count - 1; i++)
                {
                    // Replace two same turns with one turn or no turn
                    if (lCubeTurnsToClean[i] == lCubeTurnsToClean[i + 1])
                    {
                        // U & U -> U2
                        if (lCubeTurnsToClean[i].Length == 1)
                        {
                            lCubeTurnsToClean[i] = lCubeTurnsToClean[i] + c2;
                            lCubeTurnsToClean[i + 1] = cNone;
                        }

                        // U' & U' -> U2
                        else if (lCubeTurnsToClean[i].EndsWith(cApos))
                        {
                            lCubeTurnsToClean[i] = lCubeTurnsToClean[i][0] + "2";
                            lCubeTurnsToClean[i + 1] = cNone;
                        }

                        // U2 & U2 -> None
                        else if (lCubeTurnsToClean[i].EndsWith(c2))
                        {
                            lCubeTurnsToClean[i] = cNone;
                            lCubeTurnsToClean[i + 1] = cNone;
                        }
                    }

                    // Replace two same first letters of a turn with another turn or no turn
                    else if (lCubeTurnsToClean[i][0] == lCubeTurnsToClean[i + 1][0])
                    {
                        // U & U' -> None
                        if (lCubeTurnsToClean[i].Length == 1 && lCubeTurnsToClean[i + 1].EndsWith(cApos))
                        {
                            lCubeTurnsToClean[i] = cNone;
                            lCubeTurnsToClean[i + 1] = cNone;
                        }

                        // U' & U -> None
                        else if (lCubeTurnsToClean[i].EndsWith(cApos) && lCubeTurnsToClean[i + 1].Length == 1)
                        {
                            lCubeTurnsToClean[i] = cNone;
                            lCubeTurnsToClean[i + 1] = cNone;
                        }

                        // U & U2 -> U'
                        else if (lCubeTurnsToClean[i].Length == 1 && lCubeTurnsToClean[i + 1].EndsWith(c2))
                        {
                            lCubeTurnsToClean[i] = lCubeTurnsToClean[i][0] + "'";
                            lCubeTurnsToClean[i + 1] = cNone;
                        }

                        // U2 & U -> U'
                        else if (lCubeTurnsToClean[i].EndsWith(c2) && lCubeTurnsToClean[i + 1].Length == 1)
                        {
                            lCubeTurnsToClean[i] = lCubeTurnsToClean[i + 1] + cApos;
                            lCubeTurnsToClean[i + 1] = cNone;
                        }

                        // U' & U2 -> U
                        else if (lCubeTurnsToClean[i].EndsWith(cApos) && lCubeTurnsToClean[i + 1].EndsWith(c2))
                        {
                            lCubeTurnsToClean[i] = lCubeTurnsToClean[i][0] + "";
                            lCubeTurnsToClean[i + 1] = cNone;
                        }

                        // U2 & U' -> U
                        else if (lCubeTurnsToClean[i].EndsWith(c2) && lCubeTurnsToClean[i + 1].EndsWith(cApos))
                        {
                            lCubeTurnsToClean[i] = lCubeTurnsToClean[i][0] + "";
                            lCubeTurnsToClean[i + 1] = cNone;
                        }
                    }

                    // Whole cube turns
                    // Replace y' & x & y -> z
                    else if (lCubeTurnsToClean[i] == "y'" && lCubeTurnsToClean[i + 1] == "x" && lCubeTurnsToClean[i + 2] == "y")
                    {
                        lCubeTurnsToClean[i] = cNone;
                        lCubeTurnsToClean[i + 1] = "z";
                        lCubeTurnsToClean[i + 2] = cNone;
                    }
                    // Replace y & x & y' -> z'
                    else if (lCubeTurnsToClean[i] == "y" && lCubeTurnsToClean[i + 1] == "x" && lCubeTurnsToClean[i + 2] == "y'")
                    {
                        lCubeTurnsToClean[i] = cNone;
                        lCubeTurnsToClean[i + 1] = "z'";
                        lCubeTurnsToClean[i + 2] = cNone;
                    }
                    // Replace y2 & x & y2 -> x'
                    else if (lCubeTurnsToClean[i] == "y2" && lCubeTurnsToClean[i + 1] == "x" && lCubeTurnsToClean[i + 2] == "y2")
                    {
                        lCubeTurnsToClean[i] = cNone;
                        lCubeTurnsToClean[i + 1] = "x'";
                        lCubeTurnsToClean[i + 2] = cNone;
                    }

                    // Delete the first and the third 1/2 turn who are the same and leave the second turn that is on the opposite face
                    // Replace U2 & D & U2 -> D
                    else if (lCubeTurnsToClean[i] == "U2" && lCubeTurnsToClean[i + 1].StartsWith('D') && lCubeTurnsToClean[i + 2] == "U2")
                    {
                        lCubeTurnsToClean[i] = cNone;
                        lCubeTurnsToClean[i + 2] = cNone;
                    }
                    // Replace D2 & U & D2 -> U
                    else if (lCubeTurnsToClean[i] == "D2" && lCubeTurnsToClean[i + 1].StartsWith('U') && lCubeTurnsToClean[i + 2] == "D2")
                    {
                        lCubeTurnsToClean[i] = cNone;
                        lCubeTurnsToClean[i + 2] = cNone;
                    }
                    // Replace F2 & B & F2 -> B
                    else if (lCubeTurnsToClean[i] == "F2" && lCubeTurnsToClean[i + 1].StartsWith('B') && lCubeTurnsToClean[i + 2] == "F2")
                    {
                        lCubeTurnsToClean[i] = cNone;
                        lCubeTurnsToClean[i + 2] = cNone;
                    }
                    // Replace B2 & F & B2 -> F
                    else if (lCubeTurnsToClean[i] == "B2" && lCubeTurnsToClean[i + 1].StartsWith('F') && lCubeTurnsToClean[i + 2] == "B2")
                    {
                        lCubeTurnsToClean[i] = cNone;
                        lCubeTurnsToClean[i + 2] = cNone;
                    }
                    // Replace L2 & R & L2 -> R
                    else if (lCubeTurnsToClean[i] == "L2" && lCubeTurnsToClean[i + 1].StartsWith('R') && lCubeTurnsToClean[i + 2] == "L2")
                    {
                        lCubeTurnsToClean[i] = cNone;
                        lCubeTurnsToClean[i + 2] = cNone;
                    }
                    // Replace R2 & L & R2 -> L
                    else if (lCubeTurnsToClean[i] == "R2" && lCubeTurnsToClean[i + 1].StartsWith('L') && lCubeTurnsToClean[i + 2] == "R2")
                    {
                        lCubeTurnsToClean[i] = cNone;
                        lCubeTurnsToClean[i + 2] = cNone;
                    }

                    // Delete the first and the third 1/4 turn who are reversed and leave the second turn that is on the opposite face
                    // Replace U & D & U' -> D
                    else if (lCubeTurnsToClean[i] == "U" && lCubeTurnsToClean[i + 1].StartsWith('D') && lCubeTurnsToClean[i + 2] == "U'")
                    {
                        lCubeTurnsToClean[i] = cNone;
                        lCubeTurnsToClean[i + 2] = cNone;
                    }
                    // Replace U' & D & U -> D
                    else if (lCubeTurnsToClean[i] == "U'" && lCubeTurnsToClean[i + 1].StartsWith('D') && lCubeTurnsToClean[i + 2] == "U")
                    {
                        lCubeTurnsToClean[i] = cNone;
                        lCubeTurnsToClean[i + 2] = cNone;
                    }
                }

                // Remove the last turn if it is turning the whole cube (starts with x, y or z)
                if (lCubeTurnsToClean[^1].StartsWith('x') || lCubeTurnsToClean[^1].StartsWith('y') || lCubeTurnsToClean[^1].StartsWith('z'))
                {
                    lCubeTurnsToClean[^1] = cNone;
                }

                Debug.WriteLine($"nNumberCleanings: {nNumberCleanings}, lCubeTurnsToClean: {lCubeTurnsToClean.Count}");  // For testing purposes

                if (lCubeTurnsToClean.Contains(cNone))
                {
                    // Remove the items with 'None'
                    lCubeTurnsToClean.RemoveAll(x => x == cNone);
                }
                else if (nNumberCleanings > 1)
                {
                    // If the for loop was executed 2 times, break the loop
                    break;
                }
            }

            //TimeSpan delta = Stopwatch.GetElapsedTime(startTime);
            //_ = Application.Current!.Windows[0].Page!.DisplayAlert("Time", $"Time elapsed (hh:mm:ss.xxxxxxx): {delta}", "OK");
#if DEBUG
            // Save the list with the cube turns after the cleaning to a file, for testing purposes
            if (bCubeTurnsSave)
            {
                _ = ClassSaveRestoreCube.CubeTurnsSave("CubeTurnsAfterClean.txt");
            }
#endif
        }
    }
}
