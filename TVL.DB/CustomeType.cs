using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace TVL.DB
{
    public class CustomeType
    {
        private string _varchar;
        private string _int;

        public string getParamType(string _paramatype)
        {

            XmlNode xnode;
            XmlElement root = null;
            XmlDocument xmldoc = new XmlDocument();

            xmldoc.Load(string.Format("{0}/ParamMapper.xml", Environment.CurrentDirectory));
            root = xmldoc.DocumentElement;
            xnode = root.SelectSingleNode("/parameterMappings");

            XmlNodeList xnlstParamtercollection = xnode.ChildNodes;

            if (xnlstParamtercollection != null && xnlstParamtercollection.Count > 0)
            {

                foreach (XmlNode node in xnlstParamtercollection)
                {
                    if (_paramatype == node.Attributes["name"].Value)
                    {

                        _paramatype = node.Attributes[Utility.strDBProvider].Value;
                    }
                }
            }

            return _paramatype;

        }







    }
}
