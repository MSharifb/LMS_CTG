using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using LMS.BLL;
using LMS.DAL;
using LMSEntity;

namespace LMS.Web.Models
{
    public class CommonConfigModels
    {

        private SelectList _LMSTables;
        private CommonConfig _CommonConfig;
        private HRMTableMapping _HRMTableMapping;
        private HRMColumnMapping _HRMColumnMapping;
        private SelectList _YesNoList;
        private CommonConfigBLL objBLL = new CommonConfigBLL();
        private HRMTableMappingBLL objHRMBLL = new HRMTableMappingBLL();
        private HRMColumnMappingBLL objHRMClm = new HRMColumnMappingBLL();
        private string strYesNo;
        private string _Message;
        private int _intTableID;
        bool isFirstTime;

        public bool IsFirstTime
        {
            get { return isFirstTime; }
            set { isFirstTime = value; }
        }

        public int IntTableID
        {
            get { return _intTableID; }
            set { _intTableID = value; }
        }

        

        public string StrYesNo
        {
            get { return strYesNo; }
            set { strYesNo = value; }
        }

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public SelectList YesNoList
        {
            get
            {
                List<SelectListItem> itemList = new List<SelectListItem>();
                SelectListItem item = new SelectListItem();
                item.Value = "No";
                item.Text = "No";
                itemList.Add(item);
                item = new SelectListItem();

                item.Value = "Yes";
                item.Text = "Yes";
                itemList.Add(item);

                this._YesNoList = new SelectList(itemList, "Value", "Text");
                return _YesNoList;
            }
            set { _YesNoList = value; }
        }

        public CommonConfig CommonConfig
        {
            get
            {
                if (_CommonConfig == null)
                {
                    _CommonConfig = new CommonConfig ();
                }
                return _CommonConfig;
            }
            set { _CommonConfig = value; }
        }
        
        public HRMTableMapping HRMTableMapping
        {
            get 
            {
                if (_HRMTableMapping == null)
                {
                    _HRMTableMapping = new HRMTableMapping();
                }
                return _HRMTableMapping; 
            }
            set { _HRMTableMapping = value; }
        }

        public HRMColumnMapping HRMColumnMapping
        {
            get
            {
                if (_HRMColumnMapping == null)
                {
                    _HRMColumnMapping = new HRMColumnMapping();
                }
                return _HRMColumnMapping;
            }
            set { _HRMColumnMapping = value; }
        }


        public SelectList LMSTables
        {
            get
            {
                objHRMBLL = new HRMTableMappingBLL();
                List<SelectListItem> itemList = new List<SelectListItem>();
                List<HRMTableMapping> lstTable = new List<HRMTableMapping>();
                lstTable = objHRMBLL.HRMTableMappingGetAll().ToList();

                foreach (HRMTableMapping lt in lstTable)
                {
                    SelectListItem item = new SelectListItem();
                    item.Value = lt.TableID.ToString();
                    item.Text = lt.TableName;
                    itemList.Add(item);
                }
                this._LMSTables = new SelectList(itemList, "Value", "Text");
                return _LMSTables;
            }
            set { _LMSTables = value; }
        }



        public void SaveData(CommonConfigModels model, out string strmsg)
        {
            strmsg = "";
            objBLL = new CommonConfigBLL();
            try
            {
                objBLL.Edit(model.CommonConfig.LstCommonConfig, out strmsg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Save(CommonConfigModels model, out string strmsg)
        {
            strmsg = "";
            objHRMBLL = new HRMTableMappingBLL();
            try
            {
                objHRMBLL.Edit(model.HRMTableMapping.LstHRMTableMapping, out strmsg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SaveColumn(CommonConfigModels model, out string strmsg)
        {
            strmsg = "";
            objHRMClm = new HRMColumnMappingBLL();
            try
            {
                objHRMClm.Edit(model.HRMColumnMapping.LstHRMColumnMapping, out strmsg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CommonConfig> GetCommonConfigAll()
        {
            objBLL = new CommonConfigBLL();
            return CommonConfig.LstCommonConfig = objBLL.CommonConfigGetAll();
        }

        public List<HRMTableMapping> GetTableConfigurationAll()
        {
            objHRMBLL = new HRMTableMappingBLL();
            return HRMTableMapping.LstHRMTableMapping = objHRMBLL.HRMTableMappingGetAll();
        }


        public HRMColumnMapping GetColumnConfigurationByColumnId(Int32 intColumnID)
        {
            objHRMClm = new HRMColumnMappingBLL();
            return objHRMClm.HRMColumnMappingGetByColumnID(intColumnID);
        }

        public List<HRMColumnMapping> GetColumnConfigurationByTableId(Int32 intTableID)
        {
            objHRMClm = new HRMColumnMappingBLL();
            return HRMColumnMapping.LstHRMColumnMapping = objHRMClm.HRMColumnMappingGetByTableID(intTableID);
        }

        public List<HRMColumnMapping> GetColumnConfigurationAll()
        {
            objHRMClm = new HRMColumnMappingBLL();
            return HRMColumnMapping.LstHRMColumnMapping = objHRMClm.HRMColumnMappingGetAll();
        }


    
    }
}