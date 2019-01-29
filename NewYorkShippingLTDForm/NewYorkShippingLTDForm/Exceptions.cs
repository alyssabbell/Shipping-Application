
/*
* Name: Alyssa Bell
 * Filename: NewYorkShippingLTD
 * Date: 4.20.17
 * Purpose/Description: To catch exceptions from input files of incoming and outgoing shipments -
 * if the order causes the facility exceed maximum capacity or if there are not enough items in stock to fulfill an order
 * 
 * Error Checking/Exceptions:
 * UnfulfilledOrder() - thrown if there are not enough items in stock to fulfill an order
 * MaximumCapacity() - thrown if an incoming shipment exceeds the amount of available storage space
 * 
 *  

 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYorkShippingLTDForm
{
    class UnfulfilledOrder : Exception
    {
        public UnfulfilledOrder()
        {

        }

    }

    class MaximumCapacity : Exception
    {
        public MaximumCapacity()
        {

        }

    }
}
