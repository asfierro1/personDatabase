using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Security.AccessControl;
using System.Security.Principal;

namespace PersonDatabase
{
    /* This is my controller for the Model-View-Controller architecture of this project.*/
    class Controller
    {
        private static string filePath = Directory.GetCurrentDirectory();       
        private static DirectorySecurity dirPermissions = Directory.GetAccessControl(filePath);
        private static string[] lines = new string[10];
        private static int index = 0;
        private static int errorCount = 0;
        private static bool errorFlag = false;
   
        /* addPerson will add the person into the Person database.
         * First, conversion of the age and gender from a String type will be 
         * converted into an integer and a chracter tpye, repsectively.
         * Next, the method will see if there is a file called 'persondatabase.txt,'
         * locally. If not, then the file wil be automatically created. Please refer
         * to the filePath string variable where it will be located at.
         * The method will then continue to add more "Person" objects into the local
         * text file that was recently created.*/
        public static void addPerson(string firstName, string lastName,
                              string dateOfBirth, string age, string gender)
        {
            int realAge = 0;
            errorFlag = true;
            errorCount = 0;
            DateTime realValueofDateTime = new DateTime();
            
            //Create a new Person 'person' object.
            Person person = new Person(firstName, lastName, dateOfBirth, age, gender);

            //Next, we must verify the information that the user entered is legitmate.
            try
            {
                errorFlag = false;
                realValueofDateTime = DateTime.Parse(dateOfBirth);
            }
            catch (FormatException E)
            {
                errorFlag = true;
                errorCount = errorCount + 1;
                MessageBox.Show(E.Message + "\nPlease enter in a valid date.");
            }

            if (errorFlag == false)
            {
                dateOfBirth = realValueofDateTime.ToShortDateString();
            }
   
            try
            {
                errorFlag = false;
                realAge = Convert.ToInt32(age);
            }
            catch (FormatException E)
            {
                errorFlag = true;
                errorCount = errorCount + 1;
                MessageBox.Show(E.Message + "\nAge is not valid. Please enter a numerical value.");
                //MessageBox.Show("realAge = " + realAge +
                //                "\nage = " + age);
            }

            //Make sure that the user entered a valid integer for age.
            if ((realAge <= -1) || (realAge >= 101))
            {
                MessageBox.Show("Please enter in a valid age." +
                                 "\nEnter in a value between 0 - 100 years of age.");
                errorFlag = true;
                errorCount = errorCount + 1;
            }
            else
            {
                /*Although, we have validated the value of the age, the age string can still be
                * with formatting errors. For example, say the user enters in "09" for the age string,
                * although we have validated the age of the user, we must convert it to an integer,
                * validate the value, and then clear the age string and input the proper format for the 
                * age string. The "0" will be cleared from the age string and instead will now be "9". */
               
                errorFlag = false;
                age = realAge.ToString();        
            }
                        
            //The format of the age variable is okay, the error flag will be assigned to false.
            //errorFlag = false;
            //MessageBox.Show("gender = " + gender);

            //Check to make sure that the user entered a legitmate character for gender.
            switch (gender)
            {
                case "m":
                    errorFlag = false;
                    break;
                case "f":
                    errorFlag = false;
                    break;
                default:
                    MessageBox.Show("Sorry, gender must be male('m') or female('f').");
                    errorFlag = true;
                    errorCount = errorCount + 1;
                    break;
            }

            //We now know that all of our values are okay. 
            //We now add the values into our file or database.

            //This try-catch statement signals to the user when the database has been exceeded.
            //Only ten records are allowed to be in this database.
            try
            {
                lines[index] = firstName + " " + lastName + " " + dateOfBirth +
                           " " + age + " " + gender;
            }
            catch (IndexOutOfRangeException IOR)
            {
                errorFlag = true; errorCount = errorCount + 1;
                MessageBox.Show(IOR.Message +
                                "\nDatabase has been exceeded." +
                                "\nOnly ten records are allowed in this database." +
                                "\nPlease delete your data to start over again.");
            }
                        
            if ( ( errorFlag == false ) && ( errorCount == 0 ) )
            {
                /* This condition must be here in case of two scenarios:
                 * 1) The user is using the application for the very first time and the file may not exist on his machine.
                 * 2) The user decides to delete his data and start over. Thus, a new file will essentially be created. */
                if (!File.Exists(filePath))
                {
                    /* This text is added only once to the file.
                     * The user's computer may not allow the directory that the user is currently in to create a 
                     * file for the text-based database. These next two statments allow the application to have full
                     * control over the file and to make access allowed. */

                    errorFlag = true; //We will assume initally that there is not data file.
                    while (errorFlag == true)
                    {
                        try
                        {
                            dirPermissions.AddAccessRule(new FileSystemAccessRule("Everyone",
                                           FileSystemRights.FullControl, AccessControlType.Allow));
                            Directory.SetAccessControl(filePath, dirPermissions);
                            filePath = filePath + "\\personDatabase.txt";
                            errorFlag = false;
                        }
                        catch (FileNotFoundException FE)
                        {
                            errorFlag = true;
                            MessageBox.Show(FE.Message + "\nYou have chosen to delete your original data file." +
                                            "\nA new data file will be created for you.");
                            filePath = Directory.GetCurrentDirectory();
                        }
                    }
                    
                    //Create a file to write to.
                    File.WriteAllText(filePath, lines[index]);
                    index = index + 1;
                }
                else
                {
                    // This text is always added, making the file longer over time
                    // if it is not deleted.
                    File.AppendAllText(filePath, ( "\n" + lines[index] ) );
                    index = index + 1;
                }

                //We have successfully added a person, show a message that our person that the 
                //user has added has been successfuly added into the database.
                MessageBox.Show("Person successfully added.");
            }
            else
            {
                //Show the user that a person was not successfully added into the database.
                //The user must be informed to correct his error(s).
                MessageBox.Show("Adding in person was unsuccessful.\n" +
                                "Please correct your error(s).");
            }
        }

        /* This function reads the data from the personDatabase text file.
         * We use a string value to view the data in a message box.
         * We want to keep our lines array private from the user. */
        public static void readData()
        {
            try
            {
                string data = System.IO.File.ReadAllText(filePath);
                MessageBox.Show(data);
            }
            catch (FileNotFoundException FE)
            {
                MessageBox.Show(FE.Message +
                                "\nIt appears that you have either erased all of your data or are beginning" +
                                " to enter in data for the first time. Please submit some data and then try this option" +
                                " again later.");
            }
            catch (UnauthorizedAccessException UA)
            {
                MessageBox.Show(UA.Message +
                                "\nIt appears that you have not entered in any data for this applciation." +
                                "Please do so as performing this operation is invalid.");
            }
            
        }

        /* This function deletes the data in personDatabase.txt. 
         * This is done by completely erasing the file. */
        public static void deleteData()
        {
            try
            {
                errorFlag = false;
                System.IO.File.Delete(filePath);
            }
            catch (FileNotFoundException FE)
            {
                errorFlag = true;
                MessageBox.Show(FE.Message + "\nYour data has already been deleted.");
            }
            catch (UnauthorizedAccessException UA)
            {
                errorFlag = true;
                MessageBox.Show(UA.Message +  
                                "\nIt appears that you have not entered in any data for this applciation." +
                                "Please do so as performing this operation is invalid.");
            }

            if (errorFlag == false)
            {
                MessageBox.Show("Your data has been deleted from this application.");
            }
        }

        public static void readInstructions()
        {
            try
            {
                string instructionsFilePath = Directory.GetCurrentDirectory() + "\\instructions.txt";
                string instructions = System.IO.File.ReadAllText(instructionsFilePath);
                MessageBox.Show(instructions);
            }
            catch (FileNotFoundException FE)
            {
                MessageBox.Show(FE.Message + 
                                "\nUnfortunately, the file for the instructions appear to be missing for this application.");
            }
        }
    }
}
