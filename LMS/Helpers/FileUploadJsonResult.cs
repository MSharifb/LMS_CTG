using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace LMS.Web.Helpers
{
    /// <summary>
    /// A JsonResult with ContentType of text/html and the serialized object contained within textarea tags
    /// </summary>
    /// <remarks>
    /// It is not possible to upload files using the browser's XMLHttpRequest
    /// object. So the jQuery Form Plugin uses a hidden iframe element. For a
    /// JSON response, a ContentType of application/json will cause bad browser
    /// behavior so the content-type must be text/html. Browsers can behave badly
    /// if you return JSON with ContentType of text/html. So you must surround
    /// the JSON in textarea tags. All this is handled nicely in the browser
    /// by the jQuery Form Plugin. But we need to overide the default behavior
    /// of the JsonResult class in order to achieve the desired result.
    /// </remarks>
    public class FileUploadJsonResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            this.ContentType = "text/html";
            context.HttpContext.Response.Write("<textarea>");
            base.ExecuteResult(context);
            context.HttpContext.Response.Write("</textarea>");
        }

        public static byte[] GetByteArrayFromStream(Stream arg)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(arg);

            try
            {
                long size = reader.BaseStream.Length;
                imageBytes = new byte[size];
                for (long i = 0; i < size; i++)
                {
                    imageBytes[i] = reader.ReadByte();
                }
            }
            finally
            {
                reader.Close();
            }

            return imageBytes;
        }

        public static void CreateFile(string strPath, byte[] buffer)
        {
            FileStream newFile = null;
            try
            {
                newFile = new FileStream(strPath, FileMode.Create);
                newFile.Write(buffer, 0, buffer.Length);
                newFile.Close();
            }
            finally
            {
                if (newFile != null) newFile.Close();
            }
        }
    }
}