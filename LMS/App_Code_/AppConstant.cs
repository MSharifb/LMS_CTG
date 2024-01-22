using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMS.BLL;
using LMSEntity;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Configuration;
using System.Net;

namespace LMS.Web
{
    public class AppConstant
    {

        #region constants and class varibles
        private const string _SQLSERVER = "system.data.sqlclient";
        private const string _ORACLE = "system.data.oracleclient";
        private const string _OTHER = "Other";
        #endregion

        public static string ConnectionString
        {

            get { return System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString; }
        }

        public static Int32 CommandTimeout
        {

            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CommandTimeout"].ToString()); }
        }

        public static Int32 SessionTimeout
        {
            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SessionTimeout"].ToString()); }
        }

        public static Int32 PageSize
        {

            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["pageSize"].ToString()); }
        }

        public static Int32 PageSize10
        {

            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["pageSize10"].ToString()); }
        }

        public static Int32 PageSize1
        {

            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["pageSize1"].ToString()); }
        }


        public static Int32 PageSize12
        {

            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["pageSize12"].ToString()); }
        }

        public static Int32 PageSize15
        {

            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["pageSize15"].ToString()); }
        }

        public static Int32 PageSize20
        {

            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["pageSize20"].ToString()); }
        }

        public static string SysInitializer
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["SysInitializer"].ToString(); }
        }

        public static string ProjectName
        {
            get
            {
                string projectName = string.Empty;
                if (System.Configuration.ConfigurationManager.AppSettings["ProjectName"] != null)
                {
                    return System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();
                }
                else
                {
                    projectName = "MFS_IWM";
                }
                return projectName;
            }
        }
        public static string SiteUrl
        {

            get 
            {
                //return System.Configuration.ConfigurationManager.AppSettings["siteUrl"].ToString();
                                
                //--shaiful
                string strSiteUrl = string.Empty;
                List<CommonConfig> configList=new List<CommonConfig>();
                configList=Common.fetchCommonConfig().Where(c => c.ConfigKey == LMS.Util.CommonConfigKey.ApprovalFlowUrl).ToList();
                if (configList.Count>0)
                {
                    strSiteUrl = configList[0].ConfigValue.ToString();
                }
                return strSiteUrl;
            }
        }

        public static string OOASiteUrl
        {

            get
            {
                //return System.Configuration.ConfigurationManager.AppSettings["siteUrl"].ToString();

                //--shaiful
                string strSiteUrl = string.Empty;
                List<CommonConfig> configList = new List<CommonConfig>();
                configList = Common.fetchCommonConfig().Where(c => c.ConfigKey == LMS.Util.CommonConfigKey.OOAApprovalFlowUrl).ToList();
                if (configList.Count > 0)
                {
                    strSiteUrl = configList[0].ConfigValue.ToString();
                }
                return strSiteUrl;
            }
        }


        public static string OOAApplicationURL
        {

            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["OOAApplicationURL"].ToString(); 
            }
        }

        public static string MailSender
        {

            get { return System.Configuration.ConfigurationManager.AppSettings["mailSender"].ToString(); }
        }

        public static string ApplicationPath
        {

            get { return System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"].ToString(); }
        }
        public static string WebAppPath
        {

            get
            {
                //if (System.Configuration.ConfigurationManager.AppSettings["WebAppPath"] != null)
                //{
                //    return System.Configuration.ConfigurationManager.AppSettings["WebAppPath"].ToString();
                //}
                //else
                //{
                //    string url = HttpContext.Current.Request.Url.Scheme + "://" + Dns.GetHostName() + ":" + HttpContext.Current.Request.Url.Port + HttpContext.Current.Request.ApplicationPath;
                //    return url;
                //}


                //--shaiful
                string strUrl = string.Empty;
                try
                {
                    strUrl = HttpContext.Current.Request.Url.Scheme + "://" + Dns.GetHostName() + ":" + HttpContext.Current.Request.Url.Port + HttpContext.Current.Request.ApplicationPath;
                }
                catch (Exception ex)
                {
                }
                List<CommonConfig> configList = new List<CommonConfig>();
                configList = Common.fetchCommonConfig().Where(c => c.ConfigKey == LMS.Util.CommonConfigKey.WebApplicationUrl).ToList();
                if (configList.Count>0)
                {
                    if (configList[0].ConfigValue.ToString().Length > 0) 
                    { strUrl = configList[0].ConfigValue.ToString(); }
                }
                
                return strUrl;

            }
        }

        public static bool IsMailLinkEnable
        {

            get
            {                
                string strMailLinkEnable = string.Empty;

                //if (System.Configuration.ConfigurationManager.AppSettings["IsMailLinkEnable"] != null)
                //{
                //    strMailLinkEnable = System.Configuration.ConfigurationManager.AppSettings["IsMailLinkEnable"].ToString();
                //}
                
                //if (string.Compare(strMailLinkEnable, "true", true) == 0)
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}


                //--shaiful
                List<CommonConfig> configList = new List<CommonConfig>();
                configList = Common.fetchCommonConfig().Where(c => c.ConfigKey == LMS.Util.CommonConfigKey.AllowApprovalEmailLink).ToList();

                if (configList.Count>0)
                {
                    strMailLinkEnable = configList[0].ConfigValue.ToString();
                }
                if (string.Compare(strMailLinkEnable, "yes", true) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static MailSettingsSectionGroup settings
        {

            get
            {
                System.Configuration.Configuration config;
                try
                {
                    config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
                }
                catch (Exception ex)
                {
                    config = WebConfigurationManager.OpenWebConfiguration(AppConstant.ApplicationPath);
                }
                System.Net.Configuration.MailSettingsSectionGroup _settings = ((System.Net.Configuration.MailSettingsSectionGroup)(config.GetSectionGroup("system.net/mailSettings")));

                //--shaiful
                List<CommonConfig> configList = new List<CommonConfig>();
                configList = Common.fetchCommonConfig().ToList();
                foreach(CommonConfig obj in configList)
                {
                    CommonConfig nwobj = new CommonConfig();
                    nwobj.ConfigKey = obj.ConfigKey;
                    nwobj.ConfigValue = obj.ConfigValue;

                    if (nwobj.ConfigKey == LMS.Util.CommonConfigKey.SMTPServer)
                    {
                        _settings.Smtp.Network.Host = nwobj.ConfigValue;
                    }
                    else if (nwobj.ConfigKey == LMS.Util.CommonConfigKey.SMTPUserName)
                    {
                        _settings.Smtp.Network.UserName = nwobj.ConfigValue;
                    }
                    else if (nwobj.ConfigKey == LMS.Util.CommonConfigKey.SMTPUserPassword)
                    {
                        _settings.Smtp.Network.Password = nwobj.ConfigValue;
                    }
                    else if (nwobj.ConfigKey == LMS.Util.CommonConfigKey.SMTPPort)
                    {
                        _settings.Smtp.Network.Port = Convert.ToInt32(nwobj.ConfigValue);
                    }

                }
                
                return _settings;
            }
        }


        public static string strSmtpServer
        {

            get
            {
                return settings.Smtp.Network.Host;
            }
        }

        public static string strSmtpUserName
        {

            get
            {
                return settings.Smtp.Network.UserName;

            }
        }

        public static string strSmtpPassword
        {

            get
            {
                return settings.Smtp.Network.Password;
            }
        }


        public static string Provider
        {

            get
            {
                string strProviderName = "";
                string strProvider = "";

                strProviderName = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ProviderName;

                switch (strProviderName.ToLower())
                {

                    case _SQLSERVER:
                        strProvider = "sqlserver";
                        break;

                    case _ORACLE:
                        strProvider = "oracle";
                        break;

                    case _OTHER:
                        strProvider = "oledb";
                        break;
                }

                return strProvider;

            }
        }

        public static string EmailNotificationToAdminFinalApproval
        {
            get
            {
                string strSiteUrl = string.Empty;
                List<CommonConfig> configList = new List<CommonConfig>();
                configList = Common.fetchCommonConfig().Where(c => c.ConfigKey == LMS.Util.CommonConfigKey.EmailNotificationToAdminFinalApproval).ToList();
                if (configList.Count > 0)
                {
                    strSiteUrl = configList[0].ConfigValue.ToString();
                }
                return strSiteUrl;
            }
        }

     
    }
}
