using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ovning16._1
{
    public partial class Form1 : Form

    {
        ControlContats myController = new ControlContats();
        bool tempBool = true;
        int indexForRef = 0;

        public Form1()
        {
            InitializeComponent();
            myController.In();
            LoadContacsList();
        }

        private void LoadContacsList()
        {
            for (int i = 0; i < myController.CountNumberOfContacs(); i++)
            {
                string[] temp = myController.GetContacs(i);
                texListBoxContacts.Items.Add($"Förnamn: {temp[0]}, Efternamn: {temp[1]}");
            }
        }

        private void button4_Click(object sender, EventArgs e) //Skapa en kontakt
        {
            if (texFirstName.Text.Length > 0 && texLastName.Text.Length > 0 && texSocialSecurityNumber.Text.Length > 9 && texPhoneType.Text.Length > 0 && textPhoneNR.Text.Length > 0 && texAddressType.Text.Length > 0 && texAddressStreet.Text.Length > 0 && texAddressCity.Text.Length > 0 && texAddressZipCode.Text.Length > 0)
            {
                string SSN = myController.CheckIfSocialSecurityNumberType(texSocialSecurityNumber.Text);
                bool alreadyExists = myController.CheckIfSocialSecurityNumberIsAvailable(SSN);
                if (tempBool == true && alreadyExists == false)
                {
                    string[] temp = myController.AddContact(texFirstName.Text, texLastName.Text, SSN, texPhoneType.Text, textPhoneNR.Text, texAddressType.Text, texAddressStreet.Text, texAddressCity.Text, texAddressZipCode.Text);
                    texListBoxContacts.Items.Add($"Förnamn: {temp[0]}, Efternamn: {temp[1]}");
                    ClerTextBox();
                }
                else if (alreadyExists == true)
                {
                    MessageBox.Show("Personnumret finns redan");
                }
                else if (tempBool == false)
                {
                    MessageBox.Show("Det går bara att skapa en kontakt i förra lägget \nTryck på tillbaka och försök igen");
                }
                else if (texSocialSecurityNumber.Text.Length <= 9)
                {
                    MessageBox.Show("Personnumret måste vara minns 10 siffror långt");
                }
                else
                {
                    MessageBox.Show("Något gick fel och kontakten skapades inte");
                }
            }
            else
            {
                MessageBox.Show("Vänligen fyll i alla fält");
            }
        }


        private void button1_Click(object sender, EventArgs e) //TABORT
        {
            if (tempBool == true)
            {

                int index = texListBoxContacts.SelectedIndex;
                if (index > -1)
                {
                    texListBoxContacts.Items.RemoveAt(index); // Tar bort från texListBoxContacts
                    myController.RemovContact(index);         // Tar bort från contact listan
                }
                else
                {
                    MessageBox.Show("Vänligen markera ett namn");
                }
            }
            else if (tempBool == false)
            {
                int marktIndex = texListBoxContacts.SelectedIndex;

                if (marktIndex > -1)
                {
                    int index = indexForRef;


                    if (marktIndex <= myController.CountNumberOfPhone(indexForRef) && marktIndex != 0)
                    {
                        myController.RemovPhone(index, marktIndex - 1 );
                        texListBoxContacts.Items.RemoveAt(marktIndex);
                    }
                    else if (marktIndex - myController.CountNumberOfPhone(indexForRef) - 1 <= myController.CountNumberOfAddresses(indexForRef) && marktIndex - myController.CountNumberOfPhone(indexForRef) - 1 != 0 && marktIndex != 0)
                    {
                        myController.RemovAddress(index, marktIndex - 2 - myController.CountNumberOfPhone(index));
                        texListBoxContacts.Items.RemoveAt(marktIndex);
                    }
                    else
                        MessageBox.Show("Går inte att ta bort denna");
                }
                else
                {
                    MessageBox.Show("Vänligen markera ett namn");
                }
            }
            ClerTextBox();
        }

        private void button2_Click(object sender, EventArgs e)//Ändra FIXA
        {
            int index = texListBoxContacts.SelectedIndex;
            if (tempBool == true)
            {
                if (texListBoxContacts.SelectedIndex >= 0)
                {
                    myController.AddContactInfo(texListBoxContacts.SelectedIndex, texPhoneType.Text, textPhoneNR.Text, texAddressType.Text, texAddressStreet.Text, texAddressCity.Text, texAddressZipCode.Text);
                }
                else
                {
                    MessageBox.Show("Vänligen markera ett namn");
                }
            }
            else if (tempBool == false)                                                 //Uppdatera lista
            {
                if (indexForRef >= 0)
                {
                    myController.AddContactInfo(indexForRef, texPhoneType.Text, textPhoneNR.Text, texAddressType.Text, texAddressStreet.Text, texAddressCity.Text, texAddressZipCode.Text);
                }
            }
            ClerTextBox();
        }

        private void butAddContact_Click(object sender, EventArgs e)//Lägg till information
        {

            if (tempBool == true)
            {
                if (texListBoxContacts.SelectedIndex >= 0)
                {
                    myController.AddContactInfo(texListBoxContacts.SelectedIndex, texPhoneType.Text, textPhoneNR.Text, texAddressType.Text, texAddressStreet.Text, texAddressCity.Text, texAddressZipCode.Text);
                }
                else
                {
                    MessageBox.Show("Vänligen markera ett namn");
                }
            }
            else if (tempBool == false)                                                 
            {
                if (indexForRef >= 0)
                {
                    myController.AddContactInfo(indexForRef, texPhoneType.Text, textPhoneNR.Text, texAddressType.Text, texAddressStreet.Text, texAddressCity.Text, texAddressZipCode.Text);
                    texListBoxContacts.Items.Clear();
                    texListBoxContacts.Items.Add("Telefon");
                    int index = indexForRef;
                    for (int i = 0; i < myController.CountNumberOfPhone(index); i++)
                    {
                        texListBoxContacts.Items.Add($"Typ: {myController.Phone(index, i)[0]} telefon nummer: {myController.Phone(index, i)[1]}");
                    }
                    texListBoxContacts.Items.Add("Address");
                    for (int i = 0; i < myController.CountNumberOfAddresses(index); i++)
                    {
                        texListBoxContacts.Items.Add($"Typ: {myController.Addresses(index, i)[0]} Gata: {myController.Addresses(index, i)[1]}");
                    }
                }       
            }
            ClerTextBox();

        }

        private void ClerTextBox()
        {
            Form1.ActiveForm.Controls.OfType<TextBox>().ToList().ForEach(TextBox => TextBox.Clear());
        }

        private void texListBoxContacts_MouseDown(object sender, MouseEventArgs e)
        {
            ClerTextBox();
            int index = texListBoxContacts.SelectedIndex;
            int indexForLater = 0;

            if (index >= 0 && tempBool == true)
            {
                string[] temp = myController.ReadListBasInfo(index);
                texFirstName.Text = temp[0];
                texLastName.Text = temp[1];
                texSocialSecurityNumber.Text = temp[2];
                indexForLater = index;
            }         
            else if (index > - 1 && tempBool == false)
            {
                string[] temp2 = myController.ReadListBasInfo(indexForRef);
                texFirstName.Text = temp2[0];
                texLastName.Text = temp2[1];
                texSocialSecurityNumber.Text = temp2[2];

                if (index  <= myController.CountNumberOfPhone(indexForRef) && index != 0)
                {
                    string[] temp = myController.ReadListBasInfoPhone(indexForRef, index - 1);
                    texPhoneType.Text = temp[0];
                    textPhoneNR.Text = temp[1];
                }
                else if ( index - myController.CountNumberOfPhone(indexForRef) - 1 <= myController.CountNumberOfAddresses(indexForRef) && index - myController.CountNumberOfPhone(indexForRef) - 1 !=  0 && index != 0)
                {
                    string[] temp = myController.ReadListBasInfoAddress(indexForRef, index - myController.CountNumberOfPhone(indexForRef) - 2);
                    texAddressType.Text = temp[0];
                    texAddressStreet.Text = temp[1];
                    texAddressZipCode.Text = temp[2];
                    texAddressCity.Text = temp[3];
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            /*int index = texListBoxContacts.SelectedIndex;
            int indexLocal = 0;
            if (indexLocal <= contacts[index].myPhonNr.Count - 1)
            {
                texPhoneType.Text = contacts[index].myPhonNr[indexLocal].Type;
                textPhoneNR.Text = contacts[index].myPhonNr[indexLocal].NR;
            }
            else
            {
                MessageBox.Show("Finns inte så många addresser");
            }*/
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {/*

            int index = texListBoxContacts.SelectedIndex;
            int indexLocal = numericUpDown2.TabIndex;

            if (indexLocal <= contacts[index].myPhonNr.Count - 1)
            {
                texAddressType.Text = contacts[index].myAddress[indexLocal].Type;
                texAddressStreet.Text = contacts[index].myAddress[indexLocal].Street;
                texAddressCity.Text = contacts[index].myAddress[indexLocal].City;
                texAddressZipCode.Text = contacts[index].myAddress[indexLocal].ZipCode;
            }
            else
            {
                MessageBox.Show("Finns inte så många addresser");
            }
            */
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            myController.SaveToTextFile();
        }

        private void texListBoxContacts_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (tempBool == true)
            {
                tempBool = false;
                int index = texListBoxContacts.SelectedIndex;
                indexForRef = texListBoxContacts.SelectedIndex;
                texListBoxContacts.Items.Clear();
                texListBoxContacts.Items.Add("Telefon");
                for (int i = 0; i < myController.CountNumberOfPhone(index); i++)
                {
                    texListBoxContacts.Items.Add($"Typ: {myController.Phone(index, i)[0]} telefon nummer: {myController.Phone(index, i)[1]}");
                }
                texListBoxContacts.Items.Add("Address");
                for (int i = 0; i < myController.CountNumberOfAddresses(index); i++)
                {
                    texListBoxContacts.Items.Add($"Typ: {myController.Addresses(index, i)[0]} Gata: {myController.Addresses(index, i)[1]}");
                }
            }

        }

        private void butBack_Click(object sender, EventArgs e)
        {
            tempBool = true;
            texListBoxContacts.Items.Clear();
            for (int i = 0; i < myController.CountNumberOfContacs(); i++)
            {
                texListBoxContacts.Items.Add($"Förnamn: {myController.GetContacs(i)[0]}, Efternamn: {myController.GetContacs(i)[1]}");
            }
            ClerTextBox();
        }

        private void texListBoxContacts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
