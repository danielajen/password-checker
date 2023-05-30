using System;
using System.IO;
using System.Collections.Generic;

namespace password_checker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // data
            string filename = "userdatabase.txt";
            List<string> pass = new List<string>();
            // ask for user and pass
            Console.WriteLine("Enter a username:");
            string username = Console.ReadLine().ToLower();
            Console.WriteLine("Enter a password:");
            string password = Console.ReadLine().ToLower();
            // read data and split
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] split = line.Split(" ");
                    string user = split[0];
                    string passwords = split[1];
                    string userPass = user + ":" + passwords;
                    pass.Add(userPass);
                }
            }

            // insertion sort
            string[] userPasswordsArray = pass.ToArray();
            for (int i = 1; i < userPasswordsArray.Length; i++)
            {
                string current = userPasswordsArray[i];
                int j = i - 1;

                while (j >= 0 && userPasswordsArray[j].CompareTo(current) > 0)
                {
                    userPasswordsArray[j + 1] = userPasswordsArray[j];
                    j--;
                }
                userPasswordsArray[j + 1] = current;
            }

            bool found = false;
            // binary search
            int left = 0;
            int right = userPasswordsArray.Length - 1;
            while (left <= right)
            {
                int mid = (left + right) / 2;
                string[] midvalue = userPasswordsArray[mid].Split(":");
                if (midvalue[0] == username)
                {
                    found = true;
                    if (midvalue[1] == password)
                    {
                        Console.WriteLine("Access Granted!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong password.");
                    }
                    break;
                }
                else if (string.Compare(midvalue[0], username) == 1)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            // invalid
            if (!found)
            {
                Console.WriteLine("Invalid username.");
            }
        }
    }
}
