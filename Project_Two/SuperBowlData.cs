using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project_Two
{
    class SuperBowlData
    {

		static void Main(string[] args)
		{
            /**Your application should allow the end user to pass a file path for output 
			* or guide them through generating the file.
			**/

            string filePath = Directory.GetCurrentDirectory();
            string backOne = Directory.GetParent(filePath).ToString();
            string backTwo = Directory.GetParent(backOne).ToString();
            string backThree = Directory.GetParent(backTwo).ToString();
            string SBData = @"E:\P2\Project_Two\Super_Bowl_Project.csv";
            Console.WriteLine(backThree);

            if (Directory.Exists(backThree))
            {
                Console.WriteLine("It exists.");
                if (File.Exists(SBData))
                {
                    Console.WriteLine("It exists.");
                    List<SuperBowl> sbList = new List<SuperBowl>();

                    FileStream csvFile = new FileStream(SBData, FileMode.Open, FileAccess.Read);
                    StreamReader reader = new StreamReader(csvFile);

                    var newDirectory = Directory.GetParent(backThree);
                    Console.WriteLine("This will print out a .txt file containing data on Super Bowls.");
                    Console.WriteLine("Please enter a name for the new .txt file.");
                    string newTxtFile = Console.ReadLine();

                    string newFilePath = newDirectory + "/" + newTxtFile + ".txt";

                    FileStream txtFile = new FileStream(newFilePath, FileMode.Append, FileAccess.Write);
                    StreamWriter writer = new StreamWriter(txtFile);
                    
                    string row = reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        row = reader.ReadLine();
                        string[] SBDataTwo = row.Split(",");
                        SuperBowl SBDTwo = new SuperBowl()
                        {
                            Date = SBDataTwo[0],
                            SB = SBDataTwo[1],
                            Attendance = Convert.ToInt32(SBDataTwo[2]),
                            QBWinner = SBDataTwo[3],
                            CoachWinner = SBDataTwo[4],
                            Winner = SBDataTwo[5],
                            WinnerPts = Convert.ToInt32(SBDataTwo[6]),
                            QBLoser = SBDataTwo[7],
                            CoachLoser = SBDataTwo[8],
                            Loser = SBDataTwo[9],
                            LoserPts = Convert.ToInt32(SBDataTwo[10]),
                            Mvp = SBDataTwo[11],
                            Stadium = SBDataTwo[12],
                            City = SBDataTwo[13],
                            State = SBDataTwo[14]
                        };
                        sbList.Add(SBDTwo);
                    }

                    writer.WriteLine("Super Bowl Winners:");
                    writer.WriteLine("/n");
                    string sbNum = "SB";
                    string sbWinner = "Winner";
                    string sbLoser = "Loser";
                    string sbYear = "Year";
                    string sbQuarterBack = "Quarter Back";
                    string sbCoachOne = "Coach";
                    string sbMVP = "Most Valuable Player";
                    string sbPointDif = "Point Difference";
                    string sbCity = "City";
                    string sbState = "State";
                    string sbStadium = "Stadium";

                    writer.WriteLine("{0,-8}{1,-21}{2,-5}{3,-27}{4,-16}{5,-26}{6,-7}", sbNum, sbWinner, sbYear, sbQuarterBack, sbCoachOne, sbMVP, sbPointDif);
                    writer.WriteLine(new string('-', 110));
                    foreach (SuperBowl winner in sbList)
                    {
                        double ptDifference = winner.WinnerPts - winner.LoserPts;
                        writer.WriteLine($"{winner.SB,-8}{winner.Winner,-20} '{winner.Date.Substring(winner.Date.Length - 2),-3} {winner.QBWinner,-26} {winner.CoachWinner,-15} {winner.Mvp,-25} {ptDifference,-7}");
                        
                    }
                    var topAttendance = (from superBowl in sbList
                                         orderby superBowl.Attendance descending
                                         select superBowl).Take(5);

                    writer.WriteLine("");
                    writer.WriteLine("Top 5 attended super bowls:");
                    writer.WriteLine("{0,-5}{1,-20}{2,-20}{3,-10}{4,-11}{5,-16}", sbYear, sbWinner, sbLoser, sbCity, sbState, sbStadium);
                    writer.WriteLine(new string('-', 81));

                    foreach (SuperBowl superBowl in topAttendance)
                    {
                        writer.WriteLine("'{0,-4}{1,-20}{2,-20}{3,-10}{4,-11}{5,-16}", superBowl.Date.Substring(superBowl.Date.Length - 2), superBowl.Winner, superBowl.Loser, superBowl.City, superBowl.State, superBowl.Stadium);
                    }

                    var HostingState = sbList
                        .GroupBy(state => state.State)
                        .OrderByDescending(group => group.Count())
                        .First().Key;

                    var HostingCity = sbList
                        .GroupBy(city => city.City)
                        .OrderByDescending(group => group.Count())
                        .First().Key;

                    var HostingStadium = sbList
                        .GroupBy(stadium => stadium.Stadium)
                        .OrderByDescending(group => group.Count())
                        .First().Key;

                    writer.WriteLine("");
                    writer.WriteLine("The state that hosted the most super bowl games:\n\t{0}\n" +
                                      "The city that hosted the most super bowl games:\n\t{1}\n" +
                                      "The stadium that hosted the most super bowl games:\n\t{2}",
                                      HostingState, HostingCity, HostingStadium);


                    writer.WriteLine("");
                    writer.WriteLine("These are the players who have won MVP multiple times: ");
                    var mvpGroup = from superBowl in sbList
                                   group superBowl by superBowl.Mvp into mvps
                                   where mvps.Count() > 1
                                   orderby mvps.Key
                                   select mvps;

                    foreach (var player in mvpGroup)
                    {
                        writer.WriteLine(player.Key);
                        foreach (var mvp in player)
                        {
                            writer.WriteLine("\t {0} V.S. {1}", mvp.Winner, mvp.Loser);
                        }
                    }


                    var coachLosses = sbList
                        .GroupBy(coach => coach.CoachLoser)
                        .OrderByDescending(coach => coach.Count())
                        .First().Key;

                    writer.WriteLine("");
                    writer.WriteLine("Which coach lost the most super bowls?\n\t{0}", coachLosses);


                    var coachWins = sbList
                        .GroupBy(coach => coach.CoachWinner)
                        .OrderByDescending(coach => coach.Count())
                        .First().Key;

                    writer.WriteLine("");
                    writer.WriteLine("Which coach won the most super bowls?\n\t{0}", coachWins);


                    var teamWins = sbList
                        .GroupBy(team => team.Winner)
                        .OrderByDescending(team => team.Count())
                        .First().Key;

                    writer.WriteLine("");
                    writer.WriteLine("Which team has won the most super bowls?\n\t{0}", teamWins);


                    var teamLosses = sbList
                        .GroupBy(team => team.Loser)
                        .OrderByDescending(team => team.Count())
                        .First().Key;

                    writer.WriteLine("");
                    writer.WriteLine("Which team lost the most super bowls?\n\t{0}", teamLosses);


                    var ptDiffQ = from superBowl in sbList
                                  orderby (superBowl.WinnerPts - superBowl.LoserPts) descending
                                  select superBowl;

                    writer.WriteLine("");
                    writer.WriteLine("Which super bowl had the greatest point difference?\n\tSuper bowl {0}", ptDiffQ.First().SB);

                    int maxAttendance = 0;
                    foreach (SuperBowl superBowl in sbList)
                    {
                        maxAttendance = maxAttendance + superBowl.Attendance;
                    }
                    int averageAttendance = maxAttendance / sbList.Count();

                    writer.WriteLine("");
                    writer.WriteLine("What is the average attendance of all the super bowls? \n\t{0} people?", averageAttendance);

                    csvFile.Close();
                    reader.Close();
                    txtFile.Close();
                }            
            }
            else
            {
                Console.WriteLine("Something went wrong with the program...");
                Console.WriteLine("Please contact tech support at 426-5678 (IAM-LOST).");
                Console.WriteLine("This program will now close. Have a nice day.");
            }


		}
	}


	
}
