 
/*
 * Name: Alyssa Bell
 * Date: 4/20/17
 * Filename: NewYorkShippingLTD
 * Purpose/Description: Stores the data coming in from the import shipments file, as well as the data coming 
 * in from the export shipments file before being sent to arrays
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYorkShippingLTDForm
{
    public class InfoStruct
    {
        public string itemType;
        public int palletsRequired;
        public int shipmentDate;
    }
}
