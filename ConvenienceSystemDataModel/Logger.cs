﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConvenienceSystemDataModel
{
    public class Logger
    {
        /// <summary>
        /// Generic Interace for the Logger Output.
        /// 
        /// </summary>
        public interface ILoggerOutput
        {
            void write(string text);
        }

        /// <summary>
        /// switch for the Logger being active
        /// </summary>
        private bool isActive = false;

        public bool IsActive
        {
            get 
            {
                return isActive;
            }
            set 
            {
                if (Output != null)
                {
                    isActive = value;
                }
                else
                    isActive = false;
            }
        }


        private ILoggerOutput output;

        public ILoggerOutput Output
        {
            get
            {
                return output;
            }
            set
            {
                if (value == null)
                {
                    isActive = false;
                }
                output = value;
            }
        }

        private static Logger instance;

        private Logger() { }

        private static Logger Instance
        {
            get
            {
                if (instance == null) instance = new Logger();
                return instance;
            }
        }

        private void log(String text)
        {
            // Inactive? just do nothing
            if (!this.IsActive)
                return;
            
            DateTime dt = DateTime.Now;
            String datum = String.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);

            Output.write(text);


        }

        public static void Log(String text)
        {
            Instance.log(text);
        }

        public static void Log(String Sender, String text)
        {
            String s = "[" + Sender + "] " + text;
            Instance.log(s);
        }
    }
}
