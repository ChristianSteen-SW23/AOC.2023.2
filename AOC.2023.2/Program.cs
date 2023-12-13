using System.Runtime.CompilerServices;

namespace AOC._2023._2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "DataBig.txt";

            string[] lines = ReadFile(filePath);

            int maxRed = 12;
            int maxGreen = 13;
            int maxBlue = 14;

            Game[] games = new Game[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                games[i] = ReadGame(lines[i]);
                games[i].gameIndex = i+1;
            }

            int countPart1 = 0;
            for (int i = 0;i < games.Length; i++)
            {
                if (games[i].red <= maxRed && games[i].green <= maxGreen && games[i].blue <= maxBlue)
                {
                    countPart1 += games[i].gameIndex;
                    Console.WriteLine("Game " + i + " is posible");
                }
            }
            Console.WriteLine("\nPart 1: "+countPart1);

            int countPart2 = 0;
            for (int i = 0; i < games.Length; i++)
            {
                countPart2 += games[i].red* games[i].green* games[i].blue;

            }
            Console.WriteLine("\nPart 2: " + countPart2);
        }
        static string[] ReadFile(string filePath)
        {
            try
            {
                int lineCount = 0;
                // Finds the amount of lines in a the data file
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (sr.ReadLine() != null)
                    {
                        lineCount++;
                    }
                }

                Console.WriteLine("Line count is: " + lineCount);

                using (StreamReader sr = new StreamReader(filePath))
                {
                    // Sets up the array to storge the data
                    string[] lines = new string[lineCount];

                    // Makes a forloop to scan in all the data
                    for (int i = 0; i < lineCount; i++)
                    {
                        lines[i] = sr.ReadLine();
                    }
                    /*foreach (string line in lines)
                    {
                        Console.WriteLine(line);
                    }*/
                    return lines;
                }

            }
            catch
            {
                Console.WriteLine("Did not find the file");
                Environment.Exit(-1);
                return null;
            }
        }

        static Game ReadGame(string game)
        {
            Game gameData = new Game();
            gameData.red = 0;
            gameData.blue = 0;
            gameData.green = 0;

            int subsetCount = 1;
            for (int ch = 0;ch < game.Length;ch++)
            {
                if (game[ch] == ';') subsetCount++;
            }

            string subsetString = game.Substring(game.IndexOf(":")+1);
            int number = 0;
            string color = "0";
            for (int i = 0; i < subsetCount; i++)
            {
                while (true)
                {
                    number = Convert.ToInt32(FindNumberFromChar(subsetString, 1));
                    Console.WriteLine(subsetString);
                    if ((subsetString.IndexOf(";") < subsetString.IndexOf(",") || subsetString.IndexOf(",") == -1) && subsetString.IndexOf(";") != -1)
                        color = CheckColor(subsetString, subsetString.IndexOf(";") - 1);
                    else if (subsetString.IndexOf(",") != -1)
                        color = CheckColor(subsetString, subsetString.IndexOf(",") - 1);
                    else
                    {
                        color = CheckColor(subsetString, subsetString.Length-1);
                    }
                    //Console.WriteLine(color+" "+number);

                    if (color == "blue")
                    {
                        gameData.blue = Math.Max(gameData.blue, number);
                    }
                    if (color == "red")
                    {
                        gameData.red = Math.Max(gameData.red, number);
                    }
                    if (color == "green")
                    {
                        gameData.green = Math.Max(gameData.green, number);
                    }


                    if (subsetString.IndexOf(";") > subsetString.IndexOf(",") && subsetString.IndexOf(",") != -1)
                        subsetString = subsetString.Substring(subsetString.IndexOf(",") + 1);
                    else if (subsetString.IndexOf(";") < subsetString.IndexOf(",") && subsetString.IndexOf(";") != -1)
                        subsetString = subsetString.Substring(subsetString.IndexOf(";") + 1);
                    else if (subsetString.IndexOf(";") == -1 && subsetString.IndexOf(",") == -1)
                        break;
                    else if (subsetString.IndexOf(";") != -1)
                        subsetString = subsetString.Substring(subsetString.IndexOf(";") + 1);
                    else if (subsetString.IndexOf(",") != -1)
                        subsetString = subsetString.Substring(subsetString.IndexOf(",") + 1);

                    if (subsetString.IndexOf(";") == -1 && subsetString.IndexOf(",") == -1)
                        break;
                }
            }
            Console.WriteLine("red=" + gameData.red + " blue=" + gameData.blue + " green=" + gameData.green);


            return gameData;
        }
        static string CheckColor(string line, int index)
        {
            if (line[index - 4] == 'g' && line[index - 3] == 'r' && line[index - 2] == 'e' && line[index - 1] == 'e' && line[index] == 'n')
                return "green";
            if (line[index - 3] == 'b' && line[index - 2] == 'l' && line[index - 1] == 'u' && line[index] == 'e')
                return "blue";
            if (line[index - 2] == 'r' && line[index - 1] == 'e' && line[index] == 'd')
                return "red";
            
            return null;
        }
        static string FindNumberFromChar(string line, int ch)
        {
            if (!CheckForNumberChar(line[ch])) return "0";

            int leftCount = 0;
            for (int h = 0; ch - h >= 0 && ch + h <= 139 && CheckForNumberChar(line[ch - h]); h++)
            {
                leftCount = h;
            }

            int rightCount = 0;
            for (int h = 0; ch - h >= 0 && ch + h <= 139 && CheckForNumberChar(line[ch + h]); h++)
            {
                rightCount = h;
            }

            string number = line[ch].ToString();
            for (int h = 1; h < leftCount + 1; h++)
            {
                number = string.Concat(line[ch - h].ToString(), number);
            }
            for (int h = 1; h < rightCount + 1; h++)
            {
                number = string.Concat(number, line[ch + h].ToString());
            }

            return number;
        }
        static bool CheckForNumberChar(char ch)
        {
            switch (ch)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return true;
                    break;
                default:
                    return false;
                    break;
            }
        }
    }

    class Game
    {
        public int blue = 0;
        public int red = 0;
        public int green = 0;
        public int gameIndex = 0;
    }


}