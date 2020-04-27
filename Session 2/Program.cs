using System;

namespace Session_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string output = "";
            string location = "World";
            string name = "";
            Random rand = new Random();
            string [] names = { "Ben", "Jason", "Barrie", "Rob", "Steve" };
            for (int iterator = 0; iterator < 100000; iterator++)
            {

                int selectedlocation = rand.Next(5);
                int selectedname = rand.Next(5);
                if (selectedname < 2)
                {

                }

                switch (selectedlocation)
                {
                    case 0:
                        location = "World";
                        break;
                    case 1:
                        location = "Room";
                        break;
                    case 2:
                        location = "House";
                        break;
                    case 3:
                        location = "Park Bench";
                        break;
                    case 4:
                        location = "Lake";
                        break;
                }
                output = string.Format("Hello {0}'s {1}", names[selectedname], location);
                Console.WriteLine(output);
            }
        }
    }
}
