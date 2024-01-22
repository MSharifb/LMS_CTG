<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<LMS.Web.Models.ReportModel.ReportViewerViewModel>" %>


<iframe src="<%= Model.ReportPath%>" style="width: 100%; height: 1000px; border: none;">
</iframe>
