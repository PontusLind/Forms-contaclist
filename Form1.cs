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
            LoadContacsList(); //Nytt vet inte ifall funkar
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
            string [] temp = myController.AddContact(texFirstName.Text, texLastName.Text, texSocialSecurityNumber.Text, texPhoneType.Text, textPhoneNR.Text, texAddressType.Text, texAddressStreet.Text, texAddressCity.Text, texAddressZipCode.Text);
            texListBoxContacts.Items.Add($"Förnamn: {temp[0]}, Efternamn: {temp[1]}");
            ClerTextBox();
        }


        private void button1_Click(object sender, EventArgs e) //TABORT
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

        private void butAddContact_Click(object sender, EventArgs e)//Lägg till
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
            else if (tempBool == false)                                                 //Uppdatera lista
            {
                if (indexForRef >= 0)
                {
                    myController.AddContactInfo(indexForRef, texPhoneType.Text, textPhoneNR.Text, texAddressType.Text, texAddressStreet.Text, texAddressCity.Text, texAddressZipCode.Text);
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
            else if (index > 0 && tempBool == false)
            {
                string[] temp2 = myController.ReadListBasInfo(indexForLater);
                texFirstName.Text = temp2[0];
                texLastName.Text = temp2[1];
                texSocialSecurityNumber.Text = temp2[2];

                if (index - 1 <= myController.CountNumberOfPhone(indexForRef) )
                {
                    string[] temp = myController.ReadListBasInfoPhone(indexForRef, index - 1);
                    texPhoneType.Text = temp[0];
                    textPhoneNR.Text = temp[1];
                }
                else if ( index - myController.CountNumberOfPhone(indexForRef) - 2 <= myController.CountNumberOfAddresses(indexForRef))
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
    }
}
