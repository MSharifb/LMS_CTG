using System;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;

namespace TVL.DB
{
    public static class Utility
    {
        private static System.String strConnection;
        private static System.String strProvider;
        private static System.Int32 intCommandTimeout;

        public static string strDBConnectionString
        {
            get
            {
                return strConnection;
            }
            set
            {
                strConnection = value;
            }
        }
        public static string strDBProvider
        {
            get
            {
                return strProvider.ToLower();
            }
            set
            {
                strProvider = value;
            }
        }

        public static Int32 intDBCommandTimeout
        {
            get
            {
                return intCommandTimeout;
            }
            set
            {
                intCommandTimeout = value;
            }
        }

        public static string GetPackageName()
        {
            return "pos.";
        }

        public static int ErrorCode = -10000000;
    }

}
