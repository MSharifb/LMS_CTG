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
    public class JobTypeNameModels
    {
        int _startRowIndex;
        int _maximumRows;
        int _numTotalRows;
        string _message;
        private JobTypeName jobTypeNameObj;
        private List<JobTypeName> lstJobTypeName;
        private IPagedList<JobTypeName> _lstJobTypeNamePaging;


        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        public JobTypeName JobTypeNameObj
        {
            get { return jobTypeNameObj; }
            set { jobTypeNameObj = value; }
        }

        public IPagedList<JobTypeName> LstJobTypeNamePaging
        {
            get { return _lstJobTypeNamePaging; }
            set { _lstJobTypeNamePaging = value; }
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

        public List<JobTypeName> LstJobTypeName
        {
            get { return lstJobTypeName; }
            set { lstJobTypeName = value; }
        }


        public List<JobTypeName> GetData(int JobTypeNameID, string JobTypeName, int startRow, int maxRows, out int P)
        {
            int numTotal = 0;
            LstJobTypeName = JobTypeNameBLL.GetItemList(JobTypeNameID, JobTypeName, startRow, maxRows, out numTotal);
            numTotalRows = numTotal;
            P = numTotal;
            return LstJobTypeName;
        }

        public JobTypeName GetDataByID(int JobTypeNameID)
        {
            int numTotal = 0;
            LstJobTypeName = JobTypeNameBLL.GetItemList(JobTypeNameID, "", 1, 10, out numTotal);
            numTotalRows = numTotal;

            return LstJobTypeName[0];
        }

        public int SaveData(JobTypeName obj)
        {
            return JobTypeNameBLL.SaveItem(obj);
        }

        public int UpdateData(JobTypeName obj)
        {
            return JobTypeNameBLL.UpdateItem(obj);
        }

        public int DeleteData(JobTypeName obj)
        {
            return JobTypeNameBLL.DeleteItem(obj);
        }
    }
}