using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConvenienceSystemDataModel
{
    public class User
    {
        public string username;
        public double debt;
        public string state;
        public string comment;
        public int ID;
    }

    public class Product
    {
        public string product;
        public double price;
        public string comment;
        public int ID;
    }

    public class AccountingElement
    {
        public int ID;
        public string date;
        public string user;
        public double price;
        public string comment;
        public string device;
    }

    public class KeyDate
    {
        public string keydate;
        public string comment;
    }

    public class Mail
    {
        public string username;
        public string adress;
        public bool active;
    }

    public class Device
    {
        public string code;
        public string rights;
        public string comment;
    }

    public class ProductCount
    {
        public string product;
        public int amount;
        public double price;
    }

    public class FireWallElement
    {
        public int ID;
        public string ip;
        public string comment;
    }
    
}
