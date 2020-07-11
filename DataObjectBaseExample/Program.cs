// <summary>
// Example of a POCO Base object.
// </summary>
// <copyright file="Program.cs" company="">
// Copyright (C) 2020 Maaike Tromp

namespace DataObjectBaseExample
{
    using System.Collections.Generic;
    using DataObjectBaseExample.Data;
    using DataObjectBaseExample.DataObjects;
    using DataObjectBaseExample.Models;
    
    class Program
    {
        static void Main(string[] args)
        {
            // example usage of a Poco BaseClass.

            int customerId = 1;
            string connString = "Enter here your connection string.";
            DatabaseConnector db = new DatabaseConnector(connString);

            OrderModel model = new OrderModel(db);

            List<Order> orders = model.GetAllCustomerOrders(customerId);
        }
    }
}
