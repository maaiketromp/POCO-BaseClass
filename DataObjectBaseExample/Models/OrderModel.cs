// <summary>
// Example of a POCO Base object.
// </summary>
// <copyright file="OrderModel.cs" company="">
// Copyright (C) 2020 Maaike Tromp

namespace DataObjectBaseExample.Models
{
    using System.Collections.Generic;
    using System.Data;
    using Microsoft.Data.SqlClient;
    using DataObjectBaseExample.DataObjects;
    using DataObjectBaseExample.Extensions;
    using DataObjectBaseExample.Interfaces;
    
    /// <summary>
    /// Retrieves data from database relating to orders.
    /// </summary>
    public class OrderModel
    {
        private readonly IDatabaseConnector db;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderModel"/> class.
        /// </summary>
        /// <param name="db">Database connection.</param>
        /// <param name="orderId">Id of order to retrieve.</param>
        public OrderModel(IDatabaseConnector db)
        {
            this.db = db;
        }

        /// <summary>
        /// Retrieves all orders done by one customer.
        /// </summary>
        /// <param name="customerId">Id of the customer.</param>
        /// <returns>A List of Order objects.</returns>
        public List<Order> GetAllCustomerOrders(int customerId)
        {
            List<Order> outputList = new List<Order>();
            string sql = "SELECT * FROM ORDER WHERE CustomerId=@Id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", SqlDbType.Int, customerId),
            };

            var modelData = this.db.PrepareAndExecuteQuery(commandText: sql, parameters: parameters)
                .ToList();

            foreach(var record in modelData)
            {
                outputList.Add(new Order(this.db, record));
            }

            return outputList;
        }
    }
}
