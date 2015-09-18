using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace cosineSimilarity
{
    class Program
    {
        public static List<int> mdt_values = new List<int>();
        public static List<int> mdt_col_ind = new List<int>();
        public static List<int> mdt_row_acc = new List<int>();

        static void Main(string[] args)
        {
            readFile();
            displayMenu();
        }

        static void readFile()
        {
            StreamReader file = new StreamReader(@"C:\Users\scotmait\Desktop\matrix.txt");

            string line;
            int counter = 0;

            mdt_row_acc.Add(1);
            while ((line = file.ReadLine()) != null)
            {
                counter++;
                int rowAcc = 0;
                for (int i = 0; i < line.Length; i++)
                {
                    int val = Int32.Parse(line.Substring(i, 1));
                    if (val != 0)
                    {
                        mdt_values.Add(val);
                        mdt_col_ind.Add(i);
                        rowAcc++;
                    }
                }
                mdt_row_acc.Add(rowAcc);
            }

            for (int j = 1; j < mdt_row_acc.Count; j++)
            {
                mdt_row_acc[j] = mdt_row_acc[j] + mdt_row_acc[j - 1];
            }

            string values_array = string.Join("|", mdt_values.ToArray());
            string col_ind_array = string.Join("|", mdt_col_ind.ToArray());
            string row_acc_array = string.Join("|", mdt_row_acc.ToArray());

            //Console.WriteLine("Non-zero values:" + values_array);
            //Console.WriteLine("Column Index of non-zero values: " + col_ind_array);
            //Console.WriteLine("Accumulation of NNZ: " + row_acc_array);
            Console.WriteLine("Your matrix has been uploaded.");
        }

        static void displayMenu()
        {
            Console.Clear();
            Console.WriteLine("********************************************");
            Console.WriteLine("                 MENU                       ");
            Console.WriteLine("********************************************");
            Console.WriteLine("Please select a number that corresponds to the menu item you wish to execute.");
            Console.WriteLine("1) Get element");
            Console.WriteLine("2) Get item-item similarity of two columns");
            Console.WriteLine("3) Get user-user similarity of two rows");
            Console.WriteLine("4) Exit Program");
            Console.WriteLine("");
            string x = Console.ReadLine();
            int x_int = Int32.Parse(x);

            switch (x_int)
            {
                case 1:
                    getElement();
                    break;
                case 2:
                    itemItemSimilarity();
                    break;
                case 3:
                    userUserSimilarity();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    displayMenu();
                    break;
            }
             

        }

        static void getElement()
        {
            Console.Clear();
            Console.WriteLine("********************************************");
            Console.WriteLine("             GET ELEMENT                    ");
            Console.WriteLine("********************************************");
            Console.WriteLine();
            Console.WriteLine("Please enter the column index of the column you wish to access:");
            string col_ptr = Console.ReadLine();
            Console.WriteLine("Please enter the row index of the row you wish to access:");
            string row_ptr = Console.ReadLine();

            int row_ptr_int = Int32.Parse(row_ptr);
            int col_ptr_int = Int32.Parse(col_ptr);

            Console.WriteLine("You are trying to access the element at ({0},{1})", col_ptr, row_ptr);
            int row_acc_count;
            if (row_ptr_int == 0)
            {
                row_acc_count = mdt_row_acc[0];
            }
            else
            {
                row_acc_count = mdt_row_acc[row_ptr_int] - mdt_row_acc[row_ptr_int - 1];
            }
            int value = 0;
            for (int i = mdt_row_acc[row_ptr_int]; i < mdt_row_acc[row_ptr_int] + row_acc_count; i++)
            {
                if (mdt_col_ind[i] == col_ptr_int)
                {
                    value = mdt_values[i];
                    break;
                }
            }
            Console.WriteLine("The value at ({0},{1}) is: {2}", col_ptr, row_ptr, value.ToString());
            Console.ReadLine();
            revertToMenu();
        }

        static void itemItemSimilarity()
        {
            Console.Clear();
            Console.WriteLine("********************************************");
            Console.WriteLine("           item-item similarity             ");
            Console.WriteLine("********************************************");
            Console.WriteLine("Please enter the index of the first column you would like to compare");
            string item_col_i = Console.ReadLine();
            Console.WriteLine("Please enter the index of the second column you would like to compare.");
            string item_col_j = Console.ReadLine();

            int item_col_i_int = Int32.Parse(item_col_i);
            int item_col_j_int = Int32.Parse(item_col_j);

            Console.WriteLine("You are comparing columns {0} and {1}.", item_col_i, item_col_j);
            Console.ReadLine();
        }

        static void userUserSimilarity()
        {
            Console.Clear();
            Console.WriteLine("********************************************");
            Console.WriteLine("           user-user similarity             ");
            Console.WriteLine("********************************************");
            Console.WriteLine("Please enter the index of the first row you would like to compare");
            string item_row_i = Console.ReadLine();
            Console.WriteLine("Please enter the index of the second row you would like to compare.");
            string item_row_j = Console.ReadLine();

            int item_row_i_int = Int32.Parse(item_row_i);
            int item_row_j_int = Int32.Parse(item_row_j);

            Console.WriteLine("You are comparing rows {0} and {1}.", item_row_i, item_row_j);

            
        }

        static void revertToMenu()
        {
            Console.WriteLine("Press '1' to go back to Main Menu.");
            string input = Console.ReadLine();

            if(input.Equals("1"))
            {
                displayMenu();
            }

        }
    }
}
