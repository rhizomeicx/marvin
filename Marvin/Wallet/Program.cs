using System;

namespace Wallet
{
    class Program
    {
        static void Main(string[] args)
        {
            int userInput = 0;
            do
            {
                userInput = DisplayMenu();

                switch (userInput)
                {
                    case 1:
                        CreateKeyStore();
                        break;
                    case 2:
                   //     DisplayBalance();
                        break;
                    

                }
            } while (userInput != 5);

        }

        static int DisplayMenu()
        {
            Console.WriteLine("Choose and option...");
            Console.WriteLine();
            Console.WriteLine("1. Create Keystore");
            Console.WriteLine("2. View Balance");
            var result = Console.ReadLine();
            return Convert.ToInt32(result);
        }

        static void CreateKeyStore()
        {
            Console.WriteLine("Enter location to save Keystore file...");
            Console.WriteLine("--eg: F:/temp/keystore.icx");
            var result = string.Empty;

            while(string.IsNullOrWhiteSpace(result))
            { 
                Console.WriteLine("File path cannot be blank please try again");
                Console.WriteLine("Enter location to save Keystore file...");
                result = Console.ReadLine();
            }



        }

    }
}
