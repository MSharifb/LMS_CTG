using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMSEntity;
using LMS.BLL;
using MvcPaging;
using MvcContrib.Pagination;

namespace LMS.Web.Models
{
    public class SkillTypeNameModels
    {
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;
        string _message;
        private SkillTypeName skillTypeNameObj;
        private List<SkillTypeName> lstSkillTypeName;
        private IPagedList<SkillTypeName> _lstSkillTypeNamePaging;


        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public SkillTypeName SkillTypeNameObj
        {
            get { return skillTypeNameObj; }
            set { skillTypeNameObj = value; }
        }

        public IPagedList<SkillTypeName> LstSkillTypeNamePaging
        {
            get { return _lstSkillTypeNamePaging; }
            set { _lstSkillTypeNamePaging = value; }
        }

        public int startRowIndex
        {
            get { return _startRowIndex; }
            set { _startRowIndex = value; }
        }

        public int maximumRows
        {
            get { return _maximumRows; }
            set { _maximumRows = value; }
        }

        public int numTotalRows
        {
            get { return _numTotalRows; }
            set { _numTotalRows = value; }
        }

        public List<SkillTypeName> LstSkillTypeName
        {
            get { return lstSkillTypeName; }
            set { lstSkillTypeName = value; }
        }


        public List<SkillTypeName> GetData(int SkillTypeNameID, string SkillTypeName, int startRow, int maxRows, out int P)
        {
            int numTotal = 0;
            LstSkillTypeName = SkillTypeNameBLL.GetItemList(SkillTypeNameID, SkillTypeName, startRow, maxRows, out numTotal);
            numTotalRows = numTotal;
            P = numTotal;
            return LstSkillTypeName;
        }

        public SkillTypeName GetDataByID(int SkillTypeNameID)
        {
            int numTotal = 0;
            LstSkillTypeName = SkillTypeNameBLL.GetItemList(SkillTypeNameID, "", 1, 10, out numTotal);
            numTotalRows = numTotal;

            return LstSkillTypeName[0];
        }

        public int SaveData(SkillTypeName obj)
        {
            return SkillTypeNameBLL.SaveItem(obj);
        }

        public int UpdateData(SkillTypeName obj)
        {
            return SkillTypeNameBLL.UpdateItem(obj);
        }

        public int DeleteData(SkillTypeName obj)
        {
            return SkillTypeNameBLL.DeleteItem(obj);
        }
    }
}