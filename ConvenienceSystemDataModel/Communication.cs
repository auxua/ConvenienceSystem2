﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ConvenienceSystemDataModel
{
    /*
     * 
     * Providing common DataTypes for communication
     * 
     */

    #region Responses

    /// <summary>
    /// Basic information that should be supported by every response of the system
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// A status, indicating success for the request
        /// </summary>
        public bool status;
        
        /// <summary>
        /// If an error occured, provide a description
        /// </summary>
        public string errorDescription;

        /// <summary>
        /// possibly return one ore more comments on what was happening
        /// </summary>
        public string comments;
    }

    public class UsersResponse : BaseResponse
    {
        public List<User> dataSet;
    }

    public class ProductsResponse : BaseResponse
    {
        public List<Product> dataSet;
    }

    public class AccountingElementsResponse : BaseResponse
    {
        public List<AccountingElement> dataSet;
    }

    public class KeyDatesResponse : BaseResponse
    {
        public List<KeyDate> dataSet;
    }

    public class MailsResponse : BaseResponse
    {
        public List<Mail> dataSet;
    }

    public class DevicesResponse : BaseResponse
    {
        public List<Device> dataSet;
    }

    public class ProductsCountResponse : BaseResponse
    {
        public List<ProductCount> dataSet;
    }

    public class UpdateResponse : BaseResponse
    {
        public List<int> dataset; // IDs of items that have been updated with success (mainly used in case of error where only some items were updated)
    }

    #endregion

    #region Requests

    public class BaseRequest
    {
        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }

    public class InsertKeyDateRequest : BaseRequest
    {
        public string comment;
        // maybe Date for manual dating possible...
    }

    public class BuyProductsRequest : BaseRequest
    {
        public string username;
        public List<string> products;
    }

    public class EmptyMailRequest : BaseRequest
    {
        public string message;
    }

    public class WebLoginRequest : BaseRequest
    {
        public string username;
        public string password;
    }

    public class CreateuserRequest : BaseRequest
    {
        public string user;
        public string comment;
        public string state;
    }

    public class CreateProductRequest : BaseRequest
    {
        public string product;
        public double price;
        public string comment;
    }

    public class UpdateUsersRequest : BaseRequest
    {
        public List<User> dataSet;
    }

    #endregion
}
