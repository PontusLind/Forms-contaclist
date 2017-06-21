using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ovning16._1
{
    static class Program
    {
        static List<Person> contacts = new List<Person>();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            
        }
        private static void In()
        {

            if (File.Exists("personRegister.txt"))
            {
                string[] fileInfo = File.ReadAllLines("personRegister.txt", Encoding.Default);
                string[] temp = File.ReadAllLines("personRegister.txt");
                for (int i = 0; i < temp.Length; i++)
                {
                    int index = 0;
                    string[] temp2 = temp[i].Split(';');
                    contacts.Add(new Person(temp2[index], temp2[index + 1], temp2[index + 2]));
                    index += 3;

                    while (index < temp2.Length)
                    {
                        if (temp2[index] == "#ADR")
                        {
                            contacts[i].myAddress.Add(new Address(temp2[index + 1], temp2[index + 2], temp2[index + 3], temp2[index + 4]));
                            index += 4;
                        }
                        else if (temp2[index] == "#PHO")
                        {
                            contacts[i].myPhonNr.Add(new PhonNr(temp2[index + 1], temp2[index + 2]));
                            index += 3;
                        }
                    }
                }
            }
        } //Nytt vet inte ifall funkar
    }
}
