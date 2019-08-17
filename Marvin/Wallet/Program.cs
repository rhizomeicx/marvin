using System;
using System.IO;
using System.Numerics;
using IconSDK;
using IconSDK.Account;
using IconSDK.Types;
using SharedEntities;

namespace Wallet
{
    class Program
    {
        static void Main(string[] args)
        {
            int userInput = 0;
            while(userInput != 3)
            {
                userInput = DisplayMenu();
                switch (userInput)
                {
                    case 1:
                        CreateKeyStore();
                        break;
                    case 2:
                        DisplayBalance();
                        break;
                }
            } 
        }

        static int DisplayMenu()
        {
            Console.WriteLine("Choose an option...");
            Console.WriteLine();
            Console.WriteLine("1. Create Keystore");
            Console.WriteLine("2. Show Balance");
            Console.WriteLine("3. Exit");
            var result = Console.ReadLine();
            return Convert.ToInt32(result);
        }

        static void CreateKeyStore()
        {
            var keyStorePath = string.Empty;
            var match = false;

            while(string.IsNullOrWhiteSpace(keyStorePath))
            { 
                Console.WriteLine("Enter location to save Keystore file");
                Console.WriteLine("--eg: F:/temp/keystore.icx");
                keyStorePath = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(keyStorePath))
                {
                    Console.WriteLine("File path cannot be blank please try again");
                }
            }

            var keyStorePassword1 = string.Empty;
            var keyStorePassword2 = string.Empty;
        
            while(!match)
            {  
                Console.WriteLine("Enter password for your Keystore...");
                keyStorePassword1 = Console.ReadLine();
                Console.WriteLine("Enter password again for verification");
                keyStorePassword2 = Console.ReadLine();

                if (keyStorePassword1.Equals(keyStorePassword2))
                {
                    match = true;
                }
                else
                {
                   Console.WriteLine("Passwords do not match");
                }
            }


            var wallet = IconSDK.Account.Wallet.Create();
            Console.WriteLine("Creating Keystore please wait...");
            var keyStore = wallet.Store(keyStorePassword1, keyStorePath);

            if (!string.IsNullOrWhiteSpace(keyStore))
            {
                Console.WriteLine($"Keystore file created successfully at : {keyStorePath}");
                Console.WriteLine($"Your public address is : {wallet.Address}");
                Console.WriteLine($"You will need to send ICX to this address to use Marvin");
            }
        }
        static void DisplayBalance()
        {
            var keyStorePath = string.Empty;

            while (string.IsNullOrWhiteSpace(keyStorePath))
            {
                Console.WriteLine("Enter location to save Keystore file");
                Console.WriteLine("--eg: F:/temp/keystore.icx");
                keyStorePath = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(keyStorePath))
                {
                    Console.WriteLine("File path cannot be blank please try again");
                }
            }

            if (File.Exists(keyStorePath))
            {
                // This path is a file
                Console.WriteLine("Enter password for your Keystore...");
                var keyStorePassword = Console.ReadLine();
                try
                {
                    Console.WriteLine("Checking balance please wait...");
                    var wallet = IconSDK.Account.Wallet.Load(keyStorePassword, keyStorePath);
                    var balance  = wallet.GetBalance().Result;
                    Console.WriteLine($"Your current ICX balance is : {NumericHelper.Loop2ICX(balance.ToString())} ICX");
                }
                catch(Exception e)
                {
                    Console.WriteLine("Unable to open Keystore, did you type the correct password?");
                    DisplayBalance();
                }
            }
            else
            {
                Console.WriteLine("Marvin couldn't find a keystore file at that location, please try again.");
                DisplayBalance();
            }
        }
    }
}
