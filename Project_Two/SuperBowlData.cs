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


                }
            }
            else
            {
                Console.WriteLine("Doesn't exists.");
            }


		}
	}


	
}
