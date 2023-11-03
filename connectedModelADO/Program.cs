using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace connectedModelADO
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Roll Number: ");
            string rollNumber = Console.ReadLine();

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter CGPA: ");
            float cgpa = float.Parse(Console.ReadLine());

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            dataBase db = new dataBase();
            // db.insert(rollNumber, name, cgpa, password); // Tested, and its working
            // db.insertWithParameters(rollNumber, name, cgpa, password); // Tested, and its working
            // db.readData(rollNumber, name, cgpa, password); // Tested, and its working
            // db.deleteWithParameters(rollNumber, name, cgpa, password); // Tested, and its working
            // db.updateRowWithParameters(rollNumber, name, cgpa, password); // Tested, and its working
            // db.readWithParameter(rollNumber, name, cgpa, password); // Tested, and its not working
            // db.singleObjectExample();  // Tested, and its working

        }
    }

    public class dataBase
    {
        //After pasting the string remove all double qoute in the string
        string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=c:\users\tassawer\documents\visual studio 2013\Projects\connectedModelADO\connectedModelADO\conModelDB.mdf;Integrated Security=True";

        // Its working :)
        public void insert(string roll, string nm, float grade, string pass)
        {
            // 1st make connection with datBase that we have created
            SqlConnection conncetionEstablished = new SqlConnection(connectionString);

            // 2nd write a query
            string query = "insert into Student (RollNumber, Name, GPA, Password) values ('" + roll + "','" + nm + "'," + grade + ",'" + pass + "')" ;
        
            // 3rd open Connection
            conncetionEstablished.Open();

            // 4th Execute sqlCommand
            SqlCommand command = new SqlCommand(query, conncetionEstablished);

            // ExecuteNonQuery add/update/delete the data into Db and return number of rows that are affected by the query
            /*
             * if we want to deal with database two things will happen i.e; Modifying,Retrieving
                
             * Modifying:
               In Modifying Section,we have Insert, Delete ,Update,...queries.so for this we need to use 
               ExecuteNonQuery command.why because we are not querying a database, we are modifying.
               sytax:   cmd.ExecuteNonQuery Method
             
             * Retrieving:
               In this we query a database by using Select Statement.For this we use ExecuteReader(),ExecuteScalar()
               If select Query return more than one record.we need to use ExecuteReader()
               If select Query return only one record.we need to use ExecuteScalar()
               sytax:       cmd.ExecuteReader() Method
                            cmd.ExecuteScalar() Method
               The above statements(ExecuteReader(),ExecuteScalar(),SqlCommand.ExecuteNonQuery()) are used to execute 
               command statement which you give in SqlCommand.If you dont use, your command not be executed.
             */
            int numberOfInsertRows = command.ExecuteNonQuery();

            Console.WriteLine("Number of insert Row: {0}", numberOfInsertRows);

            // 5th close connection
            conncetionEstablished.Close();
        }

        // its working
        public void insertWithParameters(string roll, string nm, float grade, string pass)
        {
            // 1st make connection with datBase that we have created
            SqlConnection conncetionEstablished = new SqlConnection(connectionString);

            // 2nd write a query
            // Differemce in query
            string query = "insert into Student (RollNumber, Name, GPA, Password) values (@RollNumber, @Name, @GPA, @Password)";

            // 3rd open Connection
            conncetionEstablished.Open();

            // 4th Execute sqlCommand
            SqlCommand command = new SqlCommand(query, conncetionEstablished);

            // 5th create Parameters
            SqlParameter p1 = new SqlParameter("RollNumber", roll);
            SqlParameter p2 = new SqlParameter("Name", nm);
            SqlParameter p3 = new SqlParameter("GPA", grade);
            SqlParameter p4 = new SqlParameter("Password", pass);

            // 6th now add parameters into dataBase
            command.Parameters.Add(p1);
            command.Parameters.Add(p2);
            command.Parameters.Add(p3);
            command.Parameters.Add(p4);

            int numberOfInsertRows = command.ExecuteNonQuery();
            Console.WriteLine("Number of insert Row in Parameter Function: {0}", numberOfInsertRows);

            // 7th close connection
            conncetionEstablished.Close();
        }

        // its Working
        public void readData(string roll, string nm, float grade, string pass)
        {
            // 1st make connection with datBase that we have created
            SqlConnection conncetionEstablished = new SqlConnection(connectionString);

            // 2nd write a query
            // Differemce in query
            string query = "select * from Student where RollNumber = '" + roll +"' and Name ='" + nm +"' and GPA = " + grade +" and Password ='"+ pass +"'";

            // 3rd open Connection
            conncetionEstablished.Open();

            // 4th Execute sqlCommand
            SqlCommand command = new SqlCommand(query, conncetionEstablished);

            // 5th create Data Reader
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
                Console.WriteLine("User Authenticated");
            else
                Console.WriteLine("Invalid Data");
        }

        // its Working
        public void deleteWithParameters(string roll, string nm, float grade, string pass)
        {
            // 1st make connection with datBase that we have created
            SqlConnection conncetionEstablished = new SqlConnection(connectionString);

            // 2nd write a query
            // Difference in query
            // Error generate cause => string query = "delete from Student where RollNumber = @roll";
            // when we use parameter to read and remove data the value in query be the same as table field
            string query = "delete from Student where RollNumber = @RollNumber";

            
            // 3rd open Connection
            conncetionEstablished.Open();

            // 4th Execute sqlCommand
            SqlCommand command = new SqlCommand(query, conncetionEstablished);
        
            // 5th Create Parameters
            SqlParameter p1 = new SqlParameter("RollNumber", roll);

            command.Parameters.Add(p1);

            int numberOfEffectedRows = command.ExecuteNonQuery();
            Console.WriteLine("No. of Users Deleted: {0}", numberOfEffectedRows);

            // 6th Close connection
            conncetionEstablished.Close();
        }

        // its Working
        public void updateRowWithParameters(string roll, string nm, float grade, string pass)
        {
            // 1st make connection with datBase that we have created
            SqlConnection conncetionEstablished = new SqlConnection(connectionString);

            // 2nd write a query
            // Differemce in query
            Console.Write("Enter New Password: ");
            string nPass = Console.ReadLine();

            // This query cause to generate error, modefication required
            // string query = "update Student set Password = @nPass where RollNumber = @roll";
            string query = "update Student set Password = @Password where RollNumber = @RollNumber";

            // 3rd open Connection
            conncetionEstablished.Open();

            // 4th Execute sqlCommand
            SqlCommand command = new SqlCommand(query, conncetionEstablished);
        
            // 5th create parameters
            SqlParameter p1 = new SqlParameter("RollNumber", roll);
            SqlParameter p2 = new SqlParameter("Password", nPass);
            command.Parameters.Add(p1);
            command.Parameters.Add(p2);

            int numberOfEffectedRows = command.ExecuteNonQuery();
            Console.WriteLine("No. of Users Updated: {0}", numberOfEffectedRows);

            // 6th Close connection
            conncetionEstablished.Close();
        }

        // Not Working
        public void readWithParameter(string roll, string nm, float grade, string pass)
        {

            // 1st make connection with datBase that we have created
            SqlConnection conncetionEstablished = new SqlConnection(connectionString);
            
            Console.WriteLine("Reading from DB using SQL Parameter to prevent SQL Injection");

            // 2nd write a query
            // To Preventing Injection we use Parameters functions
            // This Query produce error, need modification
            // string query = "select * from Student where RollNumber = @roll and Password = @pass";
            string query = "select * from Student where RollNumber = @RollNumber and Password = @Password";

            // 3rd open Connection
            conncetionEstablished.Open();

            // 4th Execute sqlCommand
            SqlCommand command = new SqlCommand(query, conncetionEstablished);

            // 5th create parameters
            SqlParameter p1 = new SqlParameter("RollNumber", roll);
            SqlParameter p2 = new SqlParameter("Password", pass);
            command.Parameters.Add(p1);
            command.Parameters.Add(p2);

            // 6th Create SQL Reader
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
                Console.WriteLine("User Authenticated");
            else
                Console.WriteLine("Invalid Data");
           
            int numberOfEffectedRows = command.ExecuteNonQuery();
            Console.WriteLine("No. of Users Read: {0}", numberOfEffectedRows);

            // 7th Close connection
            conncetionEstablished.Close();
        }

        // its working
        // Return number of record by counting the first column entries
        public void singleObjectExample()
        {
            // 1st make connection with datBase that we have created
            SqlConnection conncetionEstablished = new SqlConnection(connectionString);

            // 2nd write a query
            string query = "select count (*) cnt from Student";

            // 3rd open Connection
            conncetionEstablished.Open();

            // 4th Execute sqlCommand
            SqlCommand command = new SqlCommand(query, conncetionEstablished);

            object value = command.ExecuteScalar();

            Console.WriteLine("Count of Users  = " + value);
        }
    }
}
