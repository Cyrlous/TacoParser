using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // Objective: Find the two Taco Bells that are the farthest apart from one another.

            logger.LogInfo("Log initialized");

            // Use File.ReadAllLines(path) to grab all the lines from your csv file.
            // Optional: Log an error if you get 0 lines and a warning if you get 1 line
            var lines = File.ReadAllLines(csvPath);
            if (lines.Length == 0)
            {
                logger.LogError("Log file is empty");
            }
            else if (lines.Length == 1)
            {
                logger.LogWarning("Log file has insufficient number of lines");
            }

            // This will display the first item in your lines array
            logger.LogInfo($"Lines: {lines[0]}");

            // Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // Use the Select LINQ method to parse every line in lines collection
            var locations = lines.Select(parser.Parse).ToArray();

  
            // Complete the Parse method in TacoParser class first and then START BELOW ----------

            // These will be used to store your two Taco Bells that are the farthest from each other.
            ITrackable tacoBell1 = null;
            ITrackable tacoBell2 = null;

            var distance = 0.0;

            // Look up what methods you have access to within this library.

            // NESTED LOOPS SECTION----------------------------

            // FIRST FOR LOOP -
            // This loop will let you select one location at a time to act as the "starting point" or "origin" location.
            // Naming suggestion for variable: `locA`
            for (int i = 0; i < locations.Length - 1; i++)
            {
                var locA = locations[i];

                var corA = new GeoCoordinate()
                {
                    Latitude = locA.Location.Latitude,
                    Longitude = locA.Location.Longitude
                };

                // SECOND FOR LOOP -
                // This allows you to pick a "destination" location for each "origin" location from the first loop. 
                // Naming suggestion for variable: `locB`
                for (int j = i + 1; j < locations.Length; j++)
                {
                    var locB = locations[j];
                    
                    var corB = new GeoCoordinate()
                    {
                        Latitude = locB.Location.Latitude,
                        Longitude = locB.Location.Longitude
                    };

                    // If the distance is greater than the currently saved distance, update the distance variable and the two `ITrackable` variables you set above.
                    if (corA.GetDistanceTo(corB) > distance)
                    {
                        distance = corA.GetDistanceTo(corB);
                        tacoBell1 = locA;
                        tacoBell2 = locB;
                    }
                }
            }
            // NESTED LOOPS SECTION COMPLETE ---------------------

            // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.
            // Display these two Taco Bell locations to the console.
            Console.WriteLine("The two Taco Bells furthest from one another are as follows:\n");
            Console.WriteLine($"{tacoBell1.Name} at latitude {tacoBell1.Location.Latitude} and longitude {tacoBell1.Location.Longitude}\n");
            Console.WriteLine($"{tacoBell2.Name} at latitude {tacoBell2.Location.Latitude} and longitude {tacoBell2.Location.Longitude}\n");
        }
    }
}
