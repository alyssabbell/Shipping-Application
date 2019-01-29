/*
 * Name: Alyssa Bell
 * Date: 4/20/2017
 * Filename: NewYorkShippingLTD
 * 
 * Purpose/Description: This program reads in input files containing incoming and outgoing shipping orders for New York Shipping Company,
 * sorts the data using the QuickSort Method (sorted by date) and outputs the sorted data in order for
 * the user to view. It also notifies the user if there are any orders that cannot be taken in or fulfilled 
 * due to capacity/stock issues.
 * 
 * Error Checking: This program forces the user to enter values greater than 0 for all measurements. It also checks
 * for empty fields, forcing the user to enter a value before the data can be submitted and stored.
 * 
 * Assumptions: The import and export files contain the same exact format, and that no error-checking is needed.
 * Also, that the import and export files have the same length.
 * 
 *
 *  * Summary of Methods: 
 * - public void GetQuantity() - seperates an int value out of a string for calculation purposes
 * - public void ArrayQuickSort() - recursive function that reads through the left and right pointers that read the array
 * - public void Rearrange() - rearranges the values within the array using QuickSort
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NewYorkShippingLTDForm
{
    public partial class Form1 : Form
    {
        bool emptyField = false;
        const int MAX_DAYS = 30;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            InfoStruct import = new InfoStruct();
            InfoStruct export = new InfoStruct();

            string inventoryFile = "";
            string importFile = "";
            string exportFile = "";
            emptyField = true;

            if (inventoryTxtBx.Text == string.Empty)
            {
                MessageBox.Show("Please enter the inventory file name.");
                inventoryTxtBx.Focus();
            }

            else if (importsTxtBx.Text == string.Empty)
            {
                MessageBox.Show("Please enter the correct inbound shipment file.");
                importsTxtBx.Focus();
            }

            else if (exportsTxtBx.Text == string.Empty)
            {
                MessageBox.Show("Please enter the correct outbound shipment file.");
                exportsTxtBx.Focus();
            }

            else
            {
                emptyField = false;
                inventoryFile = inventoryTxtBx.Text;
                importFile = importsTxtBx.Text;
                exportFile = exportsTxtBx.Text;

            }


            StreamReader din;
            StreamWriter dout;
            StreamWriter dout1;

            // **** these variables are for reading the iventory.txt file ****
            string shippingFacility = "";
            string maxCapacity = "";
            int maxCapacityInt = 0;
            string widgetPallets = "";
            int widgetPalletsInt = 0;
            string doodadsPallets = "";
            int doodadsPalletsInt = 0;
            string gizmosPallets = "";
            int gizmosPalletsInt = 0;

            din = new StreamReader(inventoryFile);

            shippingFacility = din.ReadLine();

            maxCapacity = din.ReadLine();
            bool ifCapacityInt = Int32.TryParse(maxCapacity, out maxCapacityInt);
            maxCapacityInt = GetQuantity(maxCapacity);

            widgetPallets = din.ReadLine();
            bool ifWidgetsInt = Int32.TryParse(widgetPallets, out widgetPalletsInt);
            widgetPalletsInt = GetQuantity(widgetPallets);

            gizmosPallets = din.ReadLine();
            bool ifGizmosInt = Int32.TryParse(gizmosPallets, out gizmosPalletsInt);
            gizmosPalletsInt = GetQuantity(gizmosPallets);


            doodadsPallets = din.ReadLine();
            bool ifDoodadsInt = Int32.TryParse(doodadsPallets, out doodadsPalletsInt);
            doodadsPalletsInt = GetQuantity(doodadsPallets);

            din.Close();

            // ************ Processing Imports File ***********
            StreamReader din1;
            din1 = new StreamReader(importFile);

            string readToCount = "";
            int lineCount = 0;          // this is the array length!
            readToCount = din1.ReadLine();
            while (readToCount != null)
            {
                lineCount++;
                readToCount = din1.ReadLine();
            }


            din1.Close();

            StreamReader din2;
            din2 = new StreamReader(importFile);

            string importLineRead = "";
            importLineRead = din2.ReadLine();


            InfoStruct[] importArray = new InfoStruct[lineCount];
            int i = 0;

            while (importLineRead != null)
            {
                import = new InfoStruct();

                string[] importLnRdArray = importLineRead.Split(',');
                string localItemType = importLnRdArray[0];
                import.itemType = localItemType;

                import.palletsRequired = Convert.ToInt32(importLnRdArray[1]);
                import.shipmentDate = Convert.ToInt32(importLnRdArray[2]);

                importArray[i] = import;
                i++;
                importLineRead = din2.ReadLine();

            }

            din2.Close();

            int left = 0;
            int right = importArray.Length - 1;


            ArrayQuickSort(/*In & Out*/importArray, /* IN */left, /* IN */right);


            // ************** Processing exports file **************
            StreamReader din3;
            din3 = new StreamReader(exportFile);

            string readExpToCount = "";
            int ExpLineCount = 0;          // this is the exports array length!
            readExpToCount = din3.ReadLine();
            while (readExpToCount != null)
            {
                ExpLineCount++;
                readExpToCount = din3.ReadLine();
            }

            din3.Close();

            StreamReader din4;
            din4 = new StreamReader(exportFile);

            string exportLineRead = "";
            exportLineRead = din4.ReadLine();


            InfoStruct[] exportArray = new InfoStruct[ExpLineCount];
            int j = 0;

            while (exportLineRead != null)
            {
                export = new InfoStruct();

                string[] exportLnRdArray = exportLineRead.Split(',');
                string exItemType = exportLnRdArray[0];
                export.itemType = exItemType;

                export.palletsRequired = Convert.ToInt32(exportLnRdArray[1]);
                export.shipmentDate = Convert.ToInt32(exportLnRdArray[2]);
                exportArray[j] = export;
                j++;
                exportLineRead = din4.ReadLine();

            }

            din4.Close();

            // Initialize values before calling ArrayQuickSort
            left = 0;
            right = exportArray.Length - 1;

            ArrayQuickSort(/*In & Out*/exportArray, /* IN */left, /* IN */right);
            // *************** End of reading input files *****************


            // ****************** Write sorted data to Arrays *************************
            dout = new StreamWriter("importedArraySorted.txt");

            for (int p = 0; p < importArray.Length; p++)
            {
                dout.WriteLine("shipment date: {0}, item type: {1}, quantity: {2}", importArray[p].shipmentDate, importArray[p].itemType, importArray[p].palletsRequired);
            }

            dout.Close();


            dout1 = new StreamWriter("exportedArraySorted.txt");

            for (int q = 0; q < exportArray.Length; q++)
            {
                dout1.WriteLine("shipment date: {0}, item type: {1}, quantity: {2}", exportArray[q].shipmentDate, exportArray[q].itemType, exportArray[q].palletsRequired);
            }

            dout1.Close();


            int localMaxCapacity = maxCapacityInt;
            int localWidgets = widgetPalletsInt;
            int localGizmos = gizmosPalletsInt;
            int localDoodads = doodadsPalletsInt;
            int usedPallets = localWidgets + localGizmos + localDoodads;
            int availablePallets = maxCapacityInt - usedPallets;

            // loop through the sorted arrays and perform calculations on the inventory based in incoming and outgoing orders
            // This is a problem if the export array is a different length than the import array but for now they're the same length.
            for (int a = 0; a < importArray.Length; a++)
            {
   
                // while looping in the same position of each array, if the date in importArray comes before the date in exportArray, add imported items to stock
                // PUT THE ADDITION AND SUBTRACTION CHUNKS IN THEIR OWN METHODS IF HAVE TIME!!!!!!!!!!! 
                if (importArray[a].shipmentDate <= exportArray[a].shipmentDate)
                {
                    
                    if (importArray[a].itemType == "widgets")
                    {
                        if (importArray[a].palletsRequired <= availablePallets)
                        {
                            localWidgets = importArray[a].palletsRequired + localWidgets;
                            usedPallets = usedPallets + importArray[a].palletsRequired;
                            availablePallets = availablePallets - importArray[a].palletsRequired;
                        }
                        else
                        {
                            try
                            {
                                if (importArray[a].palletsRequired > availablePallets)
                                {
                                    throw new MaximumCapacity();
                                }
                            }
                            catch
                            {
                                MessageBox.Show("This order cannot be accepted due to exceeding maximum capacity. Ship date: " + importArray[a].shipmentDate + ", Item type: " + importArray[a].itemType + ", Item Quantity: " + importArray[a].palletsRequired);
                            }
                        }
                    }


                    if (importArray[a].itemType == "gizmos")
                    {
                        if (importArray[a].palletsRequired <= availablePallets)
                        {
                            localGizmos = importArray[a].palletsRequired + localGizmos;
                            usedPallets = usedPallets + importArray[a].palletsRequired;
                            availablePallets = availablePallets - importArray[a].palletsRequired;
                        }
                        else
                        {
                            try
                            {
                                if (importArray[a].palletsRequired > availablePallets)
                                {
                                    throw new MaximumCapacity();
                                }
                            }
                            catch
                            {

                                MessageBox.Show("This order cannot be accepted due to exceeding maximum capacity. Ship date: " + importArray[a].shipmentDate + ", Item type: " + importArray[a].itemType + ", Item Quantity: " + importArray[a].palletsRequired);
                            }
                        }
                    }


                    if (importArray[a].itemType == "doodads")
                    {
                        if (importArray[a].palletsRequired <= availablePallets)
                        {
                            localDoodads = importArray[a].palletsRequired + localDoodads;
                            usedPallets = usedPallets + importArray[a].palletsRequired;
                            availablePallets = availablePallets - importArray[a].palletsRequired;
                        }
                        else
                        {
                            try
                            {
                                if (importArray[a].palletsRequired > availablePallets)
                                {
                                    throw new MaximumCapacity();
                                }
                            }
                            catch
                            {

                                MessageBox.Show("This order cannot be accepted due to exceeding maximum capacity. Ship date: " + importArray[a].shipmentDate + ", Item type: " + importArray[a].itemType + ", Item Quantity: " + importArray[a].palletsRequired);
                            }
                        }
                    }
                }

                if (importArray[a].shipmentDate <= exportArray[a].shipmentDate)
                {
                    if (exportArray[a].itemType == "widgets")
                    {
                        if (exportArray[a].palletsRequired <= localWidgets)
                        {
                            localWidgets = localWidgets - exportArray[a].palletsRequired;
                            usedPallets = usedPallets - exportArray[a].palletsRequired;
                            availablePallets = availablePallets + exportArray[a].palletsRequired;
                        }
                        else
                        {
                            if (exportArray[a].palletsRequired <= localWidgets)
                            {
                                try
                                {
                                    throw new UnfulfilledOrder();
                                }
                                catch
                                {
                                    MessageBox.Show("The following order cannnot be fulfilled due to not enough stock - shipment date: " + exportArray[a].shipmentDate + ", item type: " + exportArray[a].itemType + ", item quantity: " + exportArray[a].palletsRequired);
                                }
                            }
                        }
                    }
                    if (exportArray[a].itemType == "gizmos")
                    {
                        if (exportArray[a].palletsRequired <= localGizmos)
                        {
                            localGizmos = localGizmos - exportArray[a].palletsRequired;
                            usedPallets = usedPallets - exportArray[a].palletsRequired;
                            availablePallets = availablePallets + exportArray[a].palletsRequired;
                        }
                        else
                        {
                            if (exportArray[a].palletsRequired <= localGizmos)
                            {
                                try
                                {
                                    throw new UnfulfilledOrder();
                                }
                                catch
                                {
                                    MessageBox.Show("The following order cannnot be fulfilled due to not enough stock - shipment date: " + exportArray[a].shipmentDate + ", item type: " + exportArray[a].itemType + ", item quantity: " + exportArray[a].palletsRequired);
                                }
                            }
                        }
                    }


                    if (exportArray[a].itemType == "doodads")
                    {
                        if (exportArray[a].palletsRequired <= localDoodads)
                        {
                            localDoodads = localDoodads - exportArray[a].palletsRequired;
                            usedPallets = usedPallets - exportArray[a].palletsRequired;
                            availablePallets = availablePallets + exportArray[a].palletsRequired;
                        }
                        else
                        {
                            if (exportArray[a].palletsRequired <= localDoodads)
                            {
                                try
                                {
                                    throw new UnfulfilledOrder();
                                }
                                catch
                                {
                                    MessageBox.Show("The following order cannnot be fulfilled due to not enough stock - shipment date: " + exportArray[a].shipmentDate + ", item type: " + exportArray[a].itemType + ", item quantity: " + exportArray[a].palletsRequired);
                                }
                            }
                        }
                    }
                    
                } // end of import date < export date calculations

                // IN CURRENT POSITION, THE EXPORT DATE COMES FIRST THEN PERFORM OUTGOING ORDERS THEN INCOMING ORDERS
                if (exportArray[a].shipmentDate < importArray[a].shipmentDate)
                {
                    //Subtract outgoing shipments from inventory first
                    if (exportArray[a].itemType == "widgets")
                    {
                        if (exportArray[a].palletsRequired <= localWidgets)
                        {
                            localWidgets = localWidgets - exportArray[a].palletsRequired;
                            usedPallets = usedPallets - exportArray[a].palletsRequired;
                            availablePallets = availablePallets + exportArray[a].palletsRequired;
                        }
                        else
                        {
                            if (exportArray[a].palletsRequired > localWidgets)
                            {
                                try
                                {
                                    throw new UnfulfilledOrder();
                                }
                                catch
                                {
                                    MessageBox.Show("The following order cannnot be fulfilled due to not enough stock - shipment date: " + exportArray[a].shipmentDate + ", item type: " + exportArray[a].itemType + ", item quantity: " + exportArray[a].palletsRequired);
                                }
                            }
                        }
                    }

                    else if (exportArray[a].itemType == "gizmos")
                    {
                        if (exportArray[a].palletsRequired <= localGizmos)
                        {
                            localGizmos = localGizmos - exportArray[a].palletsRequired;
                            usedPallets = usedPallets - exportArray[a].palletsRequired;
                            availablePallets = availablePallets + exportArray[a].palletsRequired;
                        }
                        else
                        {
                            if (exportArray[a].palletsRequired > localGizmos)
                            {
                                try
                                {
                                    throw new UnfulfilledOrder();
                                }
                                catch
                                {
                                    MessageBox.Show("The following order cannnot be fulfilled due to not enough stock - shipment date: " + exportArray[a].shipmentDate + ", item type: " + exportArray[a].itemType + ", item quantity: " + exportArray[a].palletsRequired);
                                }
                            }
                        }
                    }


                    else
                    {
                        if (exportArray[a].palletsRequired <= localDoodads)
                        {
                            localDoodads = localDoodads - exportArray[a].palletsRequired;
                            usedPallets = usedPallets - exportArray[a].palletsRequired;
                            availablePallets = availablePallets + exportArray[a].palletsRequired;
                        }
                        else
                        {
                            if (exportArray[a].palletsRequired > localDoodads)
                            {
                                try
                                {
                                    throw new UnfulfilledOrder();
                                }
                                catch
                                {
                                    MessageBox.Show("The following order cannnot be fulfilled due to not enough stock - shipment date: " + exportArray[a].shipmentDate + ", item type: " + exportArray[a].itemType + ", item quantity: " + exportArray[a].palletsRequired);
                                }
                            }
                        }
                    }
                }

                // add incoming shipments here
                if (exportArray[a].shipmentDate < importArray[a].shipmentDate)
                {
                    if (importArray[a].itemType == "widgets")
                    {
                        if (importArray[a].palletsRequired <= availablePallets)
                        {
                            localWidgets = importArray[a].palletsRequired + localWidgets;
                            usedPallets = usedPallets + importArray[a].palletsRequired;
                            availablePallets = availablePallets - importArray[a].palletsRequired;
                        }
                        else
                        {
                            try
                            {
                                if (importArray[a].palletsRequired > availablePallets)
                                {
                                    throw new MaximumCapacity();
                                }
                            }
                            catch
                            {

                                MessageBox.Show("This order cannot be accepted due to exceeding maximum capacity. Ship date: " + importArray[a].shipmentDate + ", Item type: " + importArray[a].itemType + ", Item Quantity: " + importArray[a].palletsRequired);
                            }
                        }
                    }



                    if (importArray[a].itemType == "gizmos")
                    {
                        if (importArray[a].palletsRequired <= availablePallets)
                        {
                            localGizmos = importArray[a].palletsRequired + localGizmos;
                            usedPallets = usedPallets + importArray[a].palletsRequired;
                            availablePallets = availablePallets - importArray[a].palletsRequired;
                        }
                        else
                        {
                            try
                            {
                                if (importArray[a].palletsRequired > availablePallets)
                                {
                                    throw new MaximumCapacity();
                                }
                            }
                            catch
                            {

                                MessageBox.Show("This order cannot be accepted due to exceeding maximum capacity. Ship date: " + importArray[a].shipmentDate + ", Item type: " + importArray[a].itemType + ", Item Quantity: " + importArray[a].palletsRequired);
                            }
                        }
                    }


                    if (importArray[a].itemType == "doodads")
                    {
                        if (importArray[a].palletsRequired <= availablePallets)
                        {
                            localDoodads = importArray[a].palletsRequired + localDoodads;
                            usedPallets = usedPallets + importArray[a].palletsRequired;
                            availablePallets = availablePallets - importArray[a].palletsRequired;
                        }
                        else
                        {
                            try
                            {
                                if (importArray[a].palletsRequired > availablePallets)
                                {
                                    throw new MaximumCapacity();
                                }
                            }
                            catch
                            {

                                MessageBox.Show("This order cannot be accepted due to exceeding maximum capacity. Ship date: " + importArray[a].shipmentDate + ", Item type: " + importArray[a].itemType + ", Item Quantity: " + importArray[a].palletsRequired);
                            }
                        }
                    }



                    // end of exportArray[a].shipmentDate < importArray[a].shipmentDate calculations
                }



            }
        }


        /* pre: readline (line from Inventory file) contains a combo of letters and number values
         * post: the quantity is extracted from each line in the Inventory file and is placed into an int variable
         * purpose: pulls only integer values from each readline string and returns an int value for calculations
         */
        public static int GetQuantity(string str)
        {
            string input = str;
            string conversion = "";
            char currentVal = ' ';
            int quantity = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(str, i))
                {
                    currentVal = input[i];
                    conversion = conversion + currentVal;
                }
            }
            quantity = Convert.ToInt32(conversion);

            return quantity;

        }


        /* pre: import/export arrays contain data
         * post: the data in the import/export arrays has been sorted by date from earliest to latest
         * purpose: recursivle function sort an array from smallest to largest using QuickSort until the values are in order
         */
        public static void ArrayQuickSort(/*In & Out*/InfoStruct[] list, /* IN */int left, /* IN */int right)
        {


            int leftPointer = left;
            int rightPointer = right;
            int num = (left + right) / 2;
            int pivotVal = list[num].shipmentDate;

            Rearrange(/* IN & OUT */list, /* IN & OUT */ ref leftPointer,/* IN & OUT */ ref rightPointer, /* IN */pivotVal);

            if (leftPointer < right)
            {
                ArrayQuickSort(/* IN & OUT */list,/* IN */ leftPointer,/* IN */ right);
            }

            if (left < rightPointer)
            {
                ArrayQuickSort(/* IN & OUT */list, /* IN */left, /* IN */rightPointer);
            }



        }

        /* pre: inside import/export arrays, the leftPointer must be less than the right val, or left val must be less than the rightPointer
         * post: the data in the import/export arrays has been rearranged in order from smallest to largest
         * purpose: rearrange each sorted sublists in the import/export arrays from smallest to largest 
         */
        public static void Rearrange(/* IN & OUT */InfoStruct[] list, /* IN & OUT */ref int leftPointer, /* IN & OUT */ref int rightPointer, /* IN & OUT */int pivotVal)
        {
            while (leftPointer <= rightPointer)
            {
                while (list[leftPointer].shipmentDate < pivotVal)
                {
                    leftPointer++;

                }

                while (list[rightPointer].shipmentDate > pivotVal)
                {
                    rightPointer--;
                }

                if (leftPointer <= rightPointer)
                {
                    int tempDate = list[leftPointer].shipmentDate;
                    string tempItem = list[leftPointer].itemType;
                    int tempQuantity = list[leftPointer].palletsRequired;

                    list[leftPointer].shipmentDate = list[rightPointer].shipmentDate;
                    list[leftPointer].itemType = list[rightPointer].itemType;
                    list[leftPointer].palletsRequired = list[rightPointer].palletsRequired;

                    list[rightPointer].shipmentDate = tempDate;
                    list[rightPointer].itemType = tempItem;
                    list[rightPointer].palletsRequired = tempQuantity;
                    leftPointer++;
                    rightPointer--;

                }

            }
        }

        
        private void exitBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Your sorted results have been sent to ImportsSorted.txt and ExportsSorted.txt");
            this.Close();
        }
    }
}
