namespace AOC._2023._2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
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
    }
}