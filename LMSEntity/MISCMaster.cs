using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Globalization;

namespace LMSEntity
{
    public class MISCMaster : EntityBase
    {
        IList<MISCMaster> lstMISCMaster;

        //private string _StrDesignation;
        //public string StrDesignation
        //{
        //    get { return _StrDesignation; }
        //    set { _StrDesignation = value; }
        //}

        //public string _strdepartment;
        //public string Strdepartment
        //{
        //    get { return _strdepartment; }
        //    set { _strdepartment = value; }
        //}

        //private string _empName;
        //public string EmpName
        //{
        //    get { return _empName; }
        //    set { _empName = value; }
        //}

        private bool isNew;
        public bool IsNew
        {
            get { return isNew; }
            set { isNew = value; }
        }

        //public IList<MISCMaster> LstMISCMaster
        //{
        //    get
        //    {
        //        if (lstMISCMaster == null)
        //            lstMISCMaster = new List<MISCMaster>();

        //        return lstMISCMaster;
        //    }
        //    set { lstMISCMaster = value; }
        //}

        [DataMember, DataColumn(true)]
        public string Strdepartment { get; set; }

        [DataMember, DataColumn(true)]
        public string StrDesignation { get; set; }

        [DataMember, DataColumn(true)]
        public string EmpName { get; set; }

        [DataMember, DataColumn(true)]
        public Int64 MISCMASTERID { get; set; }

        [DataMember, DataColumn(true)]
        public string STRCOMPANYID { get; set; }

        [DataMember, DataColumn(true)]
        public string STREMPID { get; set; }

        [DataMember, DataColumn(true)]
        public string STRIUSERID { get; set; }

        [DataMember, DataColumn(true)]
        public string STREUSERID { get; set; }

        [DataMember, DataColumn(true)]
        public int UNITID { get; set; }

        [DataMember, DataColumn(true)]
        public DateTime MISCDATE { get; set; }

        [DataMember, DataColumn(true)]
        public String strMISCDATE {
            get
            {
                return MISCDATE.ToString("dd-MM-yyyy");
            }
            set
            {
                if (value != null)
                {
                    MISCDATE = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
                }
                else
                {
                    MISCDATE = DateTime.Now;
                }
            }
        }

        [DataMember, DataColumn(true)]
        public DateTime MISCToDATE { get; set; }

        [DataMember, DataColumn(true)]
        public String strMISCToDATE
        {
            get
            {
                return MISCToDATE.ToString("dd-MM-yyyy");
            }
            set
            {
                if (value != null)
                {
                    MISCToDATE = DateTime.Parse(value, new CultureInfo("fr-Fr", true), DateTimeStyles.None);
                }
                else
                {
                    MISCToDATE = DateTime.Now;
                }
            }
        }

        [DataMember, DataColumn(true)]
        public string STRAUTHORID { get; set; }
        

    }
}
