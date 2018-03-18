using System;
using System.Collections.Generic;
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

namespace PersonDatabase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //The submit button submits all persons into the database.
        private void submitButton__Click(object sender, RoutedEventArgs e)
        {
            Controller.addPerson(firstNameTextbox.Text,
                                 lastNameTextbox.Text,
                                 dateOfBirthTextbox.Text,
                                 ageTextbox.Text,
                                 genderTextbox.Text);
        }
        

        //This button clears al the text fields within the application.
        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            firstNameTextbox.Clear();
            lastNameTextbox.Clear();
            dateOfBirthTextbox.Clear();
            ageTextbox.Clear();
            genderTextbox.Clear();

        }

        //This button terminates the application.
        private void quitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        //This button deletes the data.
        private void deleteDataButton_Click(object sender, RoutedEventArgs e)
        {
            Controller.deleteData();
        }

        private void viewDataButton_Click(object sender, RoutedEventArgs e)
        {
            Controller.readData();
        }

        private void instructionsButton_Click(object sender, RoutedEventArgs e)
        {
            Controller.readInstructions();
        }
    }
}
