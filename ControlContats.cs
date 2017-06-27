using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ovning16._1
{
    public class ControlContats
    {
        List<Person> contacts = new List<Person>();


        public ControlContats()
        {
        }
        public void In()
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
                            index += 5;
                        }
                        else if (temp2[index] == "#PHO")
                        {
                            contacts[i].myPhonNr.Add(new PhonNr(temp2[index + 1], temp2[index + 2]));
                            index += 3;
                        }
                    }
                }
            }
        } //Initiera, läser från text fill. Om det finns någon


        public int CountNumberOfAddresses(int index)
        {
            int temp = 0;
            if (index >= 0)
            {
                temp = contacts[index].myAddress.Count;
            }
            return temp;
        }
        public int CountNumberOfPhone(int index)
        {
            int temp = 0;
            if (index >= 0)
            {
                temp = contacts[index].myPhonNr.Count;
            }
            return temp;
 
        }
        public int CountNumberOfContacs()
        {
            int temp = contacts.Count;
            return temp;
        }


        public string [] Addresses (int index, int indexSecond)
        {
            string[] temp = new string[4];
            
            temp[0] = contacts[index].myAddress[indexSecond].Type;
            temp[1] = contacts[index].myAddress[indexSecond].Street;
            temp[2] = contacts[index].myAddress[indexSecond].ZipCode;
            temp[3] = contacts[index].myAddress[indexSecond].City;

            return temp;
        }
        public string[] Phone(int index, int indexSecond)
        {
            string[] temp = new string[2];
            temp[0] = contacts[index].myPhonNr[indexSecond].Type;
            temp[1] = contacts[index].myPhonNr[indexSecond].NR;

            return temp;
        }
        public string[] GetContacs(int index)
        {
            string[] temp = new string[4];

            temp[0] = contacts[index].FirstName;
            temp[1] = contacts[index].LastName;
            temp[2] = contacts[index].SocialSecurityNumber;

            return temp;
        }


        public void RemovContact(int index)
        {
            if (index > -1)
            {
                contacts.RemoveAt(index);
            }
        }
        public void RemovPhone(int index, int indexForList)
        {
            if (index > -1)
            {
                contacts[index].myPhonNr.RemoveAt(indexForList);
            }
        }
        public void RemovAddress(int index, int indexForList)
        {
            if (index > -1)
            {
                contacts[index].myAddress.RemoveAt(indexForList);
            }
        }


        public string [] ReadListBasInfo (int index)
        {
            string[] returnList = new string[3];
            returnList[0] = contacts[index].FirstName;
            returnList[1] = contacts[index].LastName;

            // Sätter in ett bindesträck innan de 4 sista
            string tempOriginal = contacts[index].SocialSecurityNumber;
            int temoLength = tempOriginal.Length;
            char[] tempCharArray = tempOriginal.ToCharArray();
            string tempFinal = "";
            int j = 0;

            for (int i = 0; i < temoLength + 1; i++)
            {
                if (temoLength - 4 == i)
                {
                    tempFinal += "-";
                }
                else
                {
                    tempFinal += tempCharArray[j];
                    j++;
                }
            }
            returnList[2] = tempFinal;

            return returnList;
        }

        public string[] ReadListBasInfoPhone(int index, int indexSecond)
        {
            string[] returnList = new string[2];
            returnList[0] = contacts[index].myPhonNr[indexSecond].Type;
            returnList[1] = contacts[index].myPhonNr[indexSecond].NR;
            return returnList;
        }

        public string[] ReadListBasInfoAddress(int index, int indexSecond)
        {
            string[] returnList = new string[4];
            returnList[0] = contacts[index].myAddress[indexSecond].Type;
            returnList[1] = contacts[index].myAddress[indexSecond].Street;
            returnList[2] = contacts[index].myAddress[indexSecond].ZipCode;
            returnList[3] = contacts[index].myAddress[indexSecond].City;

            return returnList;
        }

        public string [] AddContact (string texFirstName, string texLastName, string texSocialSecurityNumber, string texPhoneType, string textPhoneNR, string texAddressType, string texAddressStreet, string texAddressCity, string texAddressZipCode)
        {
            string toUperFirst = texFirstName.First().ToString().ToUpper() + texFirstName.Substring(1);
            string toUperLAst = texLastName.First().ToString().ToUpper() + texLastName.Substring(1);

            contacts.Add(new Person(toUperFirst, toUperLAst, texSocialSecurityNumber));
            contacts[contacts.Count - 1].myPhonNr.Add(new PhonNr(texPhoneType, textPhoneNR));
            contacts[contacts.Count - 1].myAddress.Add(new Address(texAddressType, texAddressStreet, texAddressCity, texAddressZipCode));
            string[] temp = new string[2];
            temp[0] = texFirstName;
            temp[1] = texLastName;
            return temp;
        }

        public void AddContactInfo (int index, string texPhoneType, string textPhoneNR, string texAddressType, string texAddressStreet, string texAddressCity, string texAddressZipCode)
        {
            if (texPhoneType.Length > 0 && textPhoneNR.Length > 0)
            {
                contacts[index].myPhonNr.Add(new PhonNr(texPhoneType, textPhoneNR));
            }
            if (texAddressType.Length > 0 && texAddressStreet.Length > 0 && texAddressCity.Length > 0 && texAddressZipCode.Length > 0)
            {
                contacts[index].myAddress.Add(new Address(texAddressType, texAddressStreet, texAddressCity, texAddressZipCode));
            }
        }

        public void SaveToTextFile()
        {
            string[] temp = new string[contacts.Count];

            string path = @"personRegister.txt";
            for (int i = 0; i < contacts.Count; i++)
            {
                temp[i] = contacts[i].FirstName + ";";
                temp[i] += contacts[i].LastName + ";";
                temp[i] += contacts[i].SocialSecurityNumber;


                for (int j = 0; j < contacts[i].myAddress.Count; j++)
                {
                    temp[i] += ";" + "#ADR" + ";";
                    temp[i] += contacts[i].myAddress[j].Type + ";";
                    temp[i] += contacts[i].myAddress[j].Street + ";";
                    temp[i] += contacts[i].myAddress[j].ZipCode + ";";
                    temp[i] += contacts[i].myAddress[j].City;
                }
                for (int j = 0; j < contacts[i].myPhonNr.Count; j++)
                {
                    temp[i] += ";" + "#PHO" + ";";
                    temp[i] += contacts[i].myPhonNr[j].Type + ";";
                    temp[i] += contacts[i].myPhonNr[j].NR;
                }
            }
            File.WriteAllLines(path, temp);
        }

        public bool CheckIfSocialSecurityNumberIsAvailable (string SSN)
        {

            bool answer = false;
            for (int i = 0; i < contacts.Count; i++)
            {
                if (contacts[i].SocialSecurityNumber == SSN)
                {
                    answer = true;
                }

            }
            return answer;
        } 

        public string CheckIfSocialSecurityNumberType(string SSN)
        {
            string[] stringReturnSplit = SSN.Split('-', ' ');
            string stringReturn = "";
            for (int i = 0; i < stringReturnSplit.Length; i++)
            {
                stringReturn += stringReturnSplit[i];
            }
            return stringReturn;
        } 
    }
}