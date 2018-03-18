using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDatabase
{
    /* This class will serve as my Model of The Model-View-Controller architecture. */
    class Person
    {
        private string firstName { get; set; }
        private string lastName { get; set; }
        private string dateOfBirth { get; set; }
        private string age { get; set; }
        private string gender { get; set; }

        public Person()
        {
            firstName = "";
            lastName = "";
            dateOfBirth = "";
            age = "";
            gender = "";
        }

        public Person(string firstArgument, string secondArgument,
                      string thirdArgument, string fourthArgument,
                      string fifthArgument)
        {
            firstName = firstArgument;
            lastName = secondArgument;
            dateOfBirth = thirdArgument;
            age = fourthArgument;
            gender = fifthArgument;
        }
    }
}

