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
            Console.WriteLine("enter a username");
            string username = Console.ReadLine().ToLower();
            Console.WriteLine("enter a pass");
            string passwords = Console.ReadLine().ToLower();
            //read data and splits
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] split = line.Split(" ");
                    string user = split[1];
                    string password = split[2];
                    string userPass = user + ":" + password;
                    pass.Add(userPass);
                }

            }

            //interation sort?
            string[] userPasswordsArray = pass.ToArray();
            for (int i = 1; i < userPasswordsArray.Length; i++)
            {

                string current = userPasswordsArray[i];
                int j = i - 1;

                while (j > 0 && userPasswordsArray[j].CompareTo(current) > 0)
                {
                    userPasswordsArray[j + 1] = userPasswordsArray[j];
                    j--;
                }
                userPasswordsArray[j + 1] = current;
            }

            /* targets
            string target1 = username;
            string target2 = passwords;
            */
            bool found = false;
            // find targets for left and right
            int mid = 0;
            int left = 0;
            int right = userPasswordsArray.Length - 1;
            string[] midvalue;
            while (left <= right)
            {
                // less than right divide by 2 find middle and check if matches
                mid = (left + right) / 2;
                midvalue = userPasswordsArray[mid].Split(":");
                string usernameFromData = midvalue[0];
                string passwordFromData = midvalue[1];
                if (midvalue[0] == username)
                {
                    found = true;
                    if (midvalue[1] == passwords)
                    {
                        Console.WriteLine("match found!");
                        found = true;
                        break;

                    }
                    else Console.WriteLine("incorrect password :(");
                    break;
                }

                int comparison = string.Compare(usernameFromData, username + String.Compare(passwordFromData, passwords));

       
                // less than iteraite and move to next
                if (comparison < 0)
                {
                    left = mid + 1;
                }
                // 
                else
                {
                    right = mid - 1;
                }
            }
            //invalid
            if (!found)
            {
                Console.WriteLine("Invalid username or password.");
            }

        }
    }

}
