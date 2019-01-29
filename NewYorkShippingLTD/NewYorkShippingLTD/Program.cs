using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NewYorkShippingLTD
{
    public static class Program
    {
        const int MAX_DAYS = 30;



        public static void Main(string[] args)
        {
            string inventoryFile;
            string importFile;
            string exportFile;

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

          

            InfoStruct import = new InfoStruct();
            InfoStruct export = new InfoStruct();



            // *************** Start reading input files *****************

            // *************** Processing inventory file *****************
            Console.WriteLine("enter inventory filename");
            inventoryFile = Console.ReadLine();

            while (!File.Exists(inventoryFile))
            {
                Console.WriteLine("Please enter a valid file name.");
                inventoryFile = Console.ReadLine();
            }

            // converting input from the inventory file
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

           // Console.WriteLine("Shipping Facility is: {0}, storage capacity is {1}, amount of widget pallets: {2}, amount of gizmo pallets: {3}, amount of doodad pallets: {4}", shippingFacility, maxCapacityInt, widgetPalletsInt, doodadsPalletsInt, gizmosPalletsInt);

            din.Close();


            // *************** Processing imports file *****************
            // ############     REMEMBER THESE ARE SEPARATED BY COMMAS    #############
            Console.WriteLine("enter the imports filename");
            importFile = Console.ReadLine();

            while (!File.Exists(importFile))
            {
                Console.WriteLine("Please enter a valid file name");
                importFile = Console.ReadLine();
            }


            // reading/converting input from imports file
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

            //Console.WriteLine("Imports File line count: {0}", lineCount);
            din1.Close();

            StreamReader din2;
            din2 = new StreamReader(importFile);
            //string localItemType;
            //localItemType = din1.ReadLine();

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

                //Console.WriteLine(importArray[i].itemType);
               // Console.WriteLine(importArray[i].shipmentDate);


                i++;
                importLineRead = din2.ReadLine();

            }

            din2.Close();

            int left = 0;
            int right = importArray.Length - 1;
            //int pivot = MAX_DAYS / 2;

            ArrayQuickSort(/*In & Out*/importArray, /* Out */left, /* Out */right);



            // *************** Processing exports file *****************
            Console.WriteLine("enter the exports filename");
            exportFile = Console.ReadLine();

            while (!File.Exists(exportFile))
            {
                Console.WriteLine("Please enter the correct filename");
                exportFile = Console.ReadLine();
            }

            // reading/converting input from exports file
            // din3 is to read the first line to check for data inside file
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

            //Console.WriteLine("Exports file line count is: {0}", ExpLineCount);
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


            ArrayQuickSort(/*In & Out*/exportArray, /* Out */left, /* Out */right);

            // *************** End of reading input files *****************


            // ****************** Write sorted data to Arrays *************************
            dout = new StreamWriter("importedArraySorted.txt");

            for(int p = 0; p < importArray.Length; p++)
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
            // ##################   Processing sorted data/throw exceptions  ####################

            int localMaxCapacity = maxCapacityInt;
            int localWidgets = widgetPalletsInt;
            int localGizmos = gizmosPalletsInt;
            int localDoodads = doodadsPalletsInt;
            int usedPallets = localWidgets + localGizmos + localDoodads;
            int availablePallets = maxCapacityInt - usedPallets;

            // loop through the sorted arrays and perform calculations on the inventory based in incoming and outgoing orders
            // This is a problem if the export array is a different length than the import array but for now they're the same length.
            for (int a = 0; a< importArray.Length; a++)
            {
                //for (int b = 0; b < exportArray.Length; b++)
                // {

                // If the date in importArray comes before the date in exportArray, add imported items to stock
                // PUT THE ADDITION AND SUBTRACTION CHUNKS IN THEIR OWN METHODS IF HAVE TIME!!!!!!!!!!! This looks like poop
                if (importArray[a].shipmentDate < exportArray[a].shipmentDate)
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

                                Console.WriteLine("This order cannot be accepted due to exceeding maximum capacity. Ship date: {0}, Item type: {1}, Quantity: {2}", importArray[a].shipmentDate, importArray[a].itemType, importArray[a].palletsRequired);
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

                                Console.WriteLine("This order cannot be accepted due to exceeding maximum capacity. Ship date: {0}, Item type: {1}, Quantity: {2}", importArray[a].shipmentDate, importArray[a].itemType, importArray[a].palletsRequired);
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

                                Console.WriteLine("This order cannot be accepted due to exceeding maximum capacity. Ship date: {0}, Item type: {1}, Quantity: {2}", importArray[a].shipmentDate, importArray[a].itemType, importArray[a].palletsRequired);
                            }
                        }
                    }
                }

                if (importArray[a].shipmentDate < exportArray[a].shipmentDate)
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
                                    Console.WriteLine("The following order cannnot be fulfilled due to not enough stock - shipment date: {0}, item type: {1}, quantity: {2}", exportArray[a].shipmentDate, exportArray[a].itemType, exportArray[a].palletsRequired);
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
                                    Console.WriteLine("The following order cannnot be fulfilled due to not enough stock - shipment date: {0}, item type: {1}, quantity: {2}", exportArray[a].shipmentDate, exportArray[a].itemType, exportArray[a].palletsRequired);
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
                                    Console.WriteLine("The following order cannnot be fulfilled due to not enough stock - shipment date: {0}, item type: {1}, quantity: {2}", exportArray[a].shipmentDate, exportArray[a].itemType, exportArray[a].palletsRequired);
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
                                    Console.WriteLine("The following order cannnot be fulfilled due to not enough stock - shipment date: {0}, item type: {1}, quantity: {2}", exportArray[a].shipmentDate, exportArray[a].itemType, exportArray[a].palletsRequired);
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
                                    Console.WriteLine("The following order cannnot be fulfilled due to not enough stock - shipment date: {0}, item type: {1}, quantity: {2}", exportArray[a].shipmentDate, exportArray[a].itemType, exportArray[a].palletsRequired);
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
                                    Console.WriteLine("The following order cannnot be fulfilled due to not enough stock - shipment date: {0}, item type: {1}, quantity: {2}", exportArray[a].shipmentDate, exportArray[a].itemType, exportArray[a].palletsRequired);
                                }
                            }
                        }
                    }
                }
                
                    // add incoming shipments here
                if (exportArray[a].shipmentDate < importArray[a].shipmentDate)
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

                            Console.WriteLine("This order cannot be accepted due to exceeding maximum capacity. Ship date: {0}, Item type: {1}, Quantity: {2}", importArray[a].shipmentDate, importArray[a].itemType, importArray[a].palletsRequired);
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

                                Console.WriteLine("This order cannot be accepted due to exceeding maximum capacity. Ship date: {0}, Item type: {1}, Quantity: {2}", importArray[a].shipmentDate, importArray[a].itemType, importArray[a].palletsRequired);
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

                                Console.WriteLine("This order cannot be accepted due to exceeding maximum capacity. Ship date: {0}, Item type: {1}, Quantity: {2}", importArray[a].shipmentDate, importArray[a].itemType, importArray[a].palletsRequired);
                            }
                        }
                    }



                    // end of exportArray[a].shipmentDate < importArray[a].shipmentDate calculations
                }

             
            }
            

        }



        // pulls only integer values from each readline string and returns an int value for calculations
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



        public static void ArrayQuickSort(/*In & Out*/InfoStruct[] list, /* Out */int left, /* Out */int right)
        {


            int leftPointer = left;
            int rightPointer = right;
            int num = (left + right) / 2;
            int pivotVal = list[num].shipmentDate;

            Rearrange(list, ref leftPointer, ref rightPointer, pivotVal);

            if (leftPointer < right)
            {
                ArrayQuickSort(list, leftPointer, right);
            }

            if (left < rightPointer)
            {
                ArrayQuickSort(list, left, rightPointer);
            }



        }

        public static void Rearrange(InfoStruct[] list, ref int leftPointer, ref int rightPointer, int pivotVal)
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


    }
}
