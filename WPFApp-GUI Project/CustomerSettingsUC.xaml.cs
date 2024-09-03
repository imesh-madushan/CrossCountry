using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFApp_GUI_Project
{
    /// <summary>
    /// Interaction logic for CustomerSettingsUC.xaml
    /// </summary>
    public partial class CustomerSettingsUC : UserControl
    {
        private string loggedUserID;
        private string oldUserName;
        private string oldPassword;

        private string newUsername;
        private string newPassword;
        private string newAddress;

        private int Flag = 0;
        private string responseFail;
        private string responsePass;

        private bool usernameReady = false;
        private bool passwordReady = false;
        private bool addressReady = false;

        CreateDBconnection con = new CreateDBconnection();
        
        public CustomerSettingsUC()
        {
            InitializeComponent();
            loggedUserID = ShoppingWindow.getLoggedID();
            oldUserName = ShoppingWindow.getLoggedUsername();
            
            textBoxUsername.Text = oldUserName;
        }
                
        //Validate Details
        private void validateDetails()
        {
            string sqlPasswordRead = $"SELECT Cus_Password FROM Customer WHERE Cus_ID = '{loggedUserID}'";
            string sqlUsernameRead = $"SELECT COUNT(*) FROM Customer WHERE Cus_Username = '{textBoxUsername.Text}'";

            try
            {
                //Check if new Username is enterd by user.
                if (!String.IsNullOrWhiteSpace(textBoxUsername.Text) && textBoxUsername.Text != oldUserName)
                {
                    //Check for Username length to be more than 8 characters
                    if (textBoxUsername.Text.Length >= 8)
                    {
                        SqlCommand cmdCheckUsername = new SqlCommand(sqlUsernameRead, con.GetDBconnetion());

                        //Check if the new username is already available
                        if (Convert.ToInt32(cmdCheckUsername.ExecuteScalar()) != 0)
                        {
                            Flag = 1;
                            responseFail += "\nUsername already taken !";
                        }
                        else
                        {
                            //If Username is new one. Make it ready to update in database
                            //Update will done in updateDetails() function
                            usernameReady = true;
                        }

                    }
                    else
                    {
                        Flag = 1;
                        responseFail += "\nUsername must contain 8 characters !";
                    }

                }

                //Check if user has enter data in New Password feild.
                if (!String.IsNullOrWhiteSpace(textBoxPasswordNew.Text))
                {
                    if (textBoxPasswordNew.Text.Length >= 8)
                    {
                        SqlCommand cmdCheckPwd = new SqlCommand(sqlPasswordRead, con.GetDBconnetion());

                        //Check if OldPassword is correct and make it ready to Update in databse
                        if (textBoxPasswordOld.Text == Convert.ToString(cmdCheckPwd.ExecuteScalar()))
                        {
                            //Update will done in updateDetails() function
                            passwordReady = true;
                        }
                        else
                        {
                            Flag = 1;
                            responseFail += "\nOld Password is incorret";
                        }
                    }
                    else
                    {
                        Flag = 1;
                        responseFail += "\nNew Password must contain 8 characters !";
                    }
                }
                else
                {
                    //If Old Password is entered But New Password is not provided
                    if (!String.IsNullOrWhiteSpace(textBoxPasswordOld.Text))
                    {
                        Flag = 1;
                        responseFail += "\nTo update Password please fill the New Password feild too !\n";
                    }
                }

                //Check if user has enter data in New Address feild and then make it readu to update in database.
                if (!String.IsNullOrWhiteSpace(textBoxAddressNew.Text))
                {
                    addressReady = true;
                }

            }
            catch (Exception ex)
            {
                Flag = 1;
                MessageBox.Show(ex.Message);
            }
        }

        //Update Deatails in database
        private void updateDetails()
        {
            string sqlUsernameUpdate = "UPDATE Customer Set " +
                                        $"Cus_Username = '{textBoxUsername.Text}' " +
                                        $"WHERE Cus_ID = '{loggedUserID}' ";

            string sqlPasswordUpdate = "UPDATE Customer Set " +
                                        $"Cus_Password = '{textBoxPasswordNew.Text}' " +
                                        $"WHERE Cus_ID = '{loggedUserID}' ";

            string sqlNewAddress = "INSERT INTO Customer_Address (Cus_ID, Address_ID, Address) " +
                                    $"VALUES ('{loggedUserID}', '{IDgenarate.createID("Address_ID")}', '{textBoxAddressNew.Text}')";

            //When validation success update in database
            if (Flag == 0)
            {
                try
                {
                    if (usernameReady == true)
                    {
                        SqlCommand cmdUpdateUname = new SqlCommand(sqlUsernameUpdate, con.GetDBconnetion());
                        cmdUpdateUname.ExecuteNonQuery();
                        oldUserName = textBoxUsername.Text;

                        if (Window.GetWindow(this) is ShoppingWindow shoppingWindow)
                        { 
                            shoppingWindow.labelProfileUsername.Content = oldUserName;
                        }
                        responsePass += "\nUsername Update Success !";

                    }

                    if (passwordReady == true)
                    {
                        SqlCommand cmdUpdatePwd = new SqlCommand(sqlPasswordUpdate, con.GetDBconnetion());
                        cmdUpdatePwd.ExecuteNonQuery();
                        responsePass += "\nPassword Update Success !";
                    }

                    if (addressReady == true)
                    {
                        SqlCommand cmdUpdateAddress = new SqlCommand(sqlNewAddress, con.GetDBconnetion());
                        cmdUpdateAddress.ExecuteNonQuery();
                        responsePass += "\nNew Address Added Success";
                    }

                    if (usernameReady == false && passwordReady == false && addressReady == false)
                    {
                        responsePass += "Please Enter Information That You Want To Update !";
                    }
                }
                catch (Exception ex) 
                {
                    Flag = 1;
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //Display Update Status msg
        private void displayUpdateStatus()
        {
            
            if (Flag == 0)
            {
                //When no erros in details and any of information ready to update
                if (usernameReady == true || passwordReady == true || addressReady == true)
                {
                    responsePass += "\n\nDetails Update Success !";
                    MsgWindow msg = new MsgWindow();
                    msg.textBoxMsg.Text = responsePass;
                    msg.textBoxMsg.Foreground = Brushes.Blue;
                    msg.Show();

                    textBoxAddressNew.Text = String.Empty;
                }
                //If any of information is not enterd
                else
                {
                    MsgWindow msg = new MsgWindow();
                    msg.textBoxMsg.Text = responsePass;
                    msg.textBoxMsg.Foreground = Brushes.Blue;
                    msg.Show();
                }
            }

            else
            {
                responseFail += "\n\nFail to update details, Try again !!!";
                MsgWindow msg = new MsgWindow();
                msg.textBoxMsg.Text = responseFail;
                msg.textBoxMsg.Foreground = Brushes.Red;
                msg.Show();
            }
        }
        //Button Clicks
        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            responsePass = String.Empty;
            responseFail = String.Empty;
            usernameReady = false;
            passwordReady = false;
            addressReady = false;
            Flag = 0;

            validateDetails();
            updateDetails();
            displayUpdateStatus();
        }
    }
}
