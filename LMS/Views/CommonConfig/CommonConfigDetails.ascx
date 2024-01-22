<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LMS.Web.Models.CommonConfigModels>" %>

<script type="text/javascript">

    $(document).ready(function () {

        $("#divStyleCommonConfig").dialog({ autoOpen: false, modal: true, height: 700, width: 920, resizable: false, title: 'Database Configuration', beforeclose: function (event, ui) { Closing(); } });
        preventSubmitOnEnter($("#frmCommonConfig"));
        refreshGrid();
        //FormatTextBox();

    });


       function Closing() {

    }

    function savedata() 
    {
        if (fnValidate() == true) 
        {
            executeAction('frmCommonConfig', '/LMS/CommonConfig/SaveCommonConfig', 'divDataList');
        }        
        return false;
    }

    function refreshGrid() {
        var objs = $("select");
        objs.each(function (i) {
            CheckRequiredField(this);
        });    
    }

//    function popupStyleAdd() {

//          var dbVal =$("#CommonConfig_LstCommonConfig_0__ConfigValue").val();

//          if(dbVal.length <1)
//          {
//            alert('HRM Database name is empty.');
//            return;
//          }
//            var host = window.location.host;
//            var url = 'http://' + host + '/LMS/CommonConfig/CommonConfigAdd';
//            $('#styleOpenerCommonConfig').attr({ src: url });
//            $("#divStyleCommonConfig").dialog('open');
//            return false;
//    }

    function CheckRequiredField(ctrl)
     {        
         var row = $(ctrl).parent().parent().parent().children().index($(ctrl).parent().parent());         
         var selId = document.getElementById('CommonConfig_LstCommonConfig_' + row + '__ConfigID').value;
         var selVal = document.getElementById('CommonConfig_LstCommonConfig_' + row + '__ConfigValue').value;

         var table = document.getElementById('tblList');
     
         if(table != null)
         {
             var rows = table.getElementsByTagName("tr");  
             var targetPVal = "";
             for(var i=0;i<rows.length;i++)
             {
                 //var numOfCol = $(rows[i]).children().length;
                 var targetPId = document.getElementById('CommonConfig_LstCommonConfig_' + i + '__intParentID').value;
                 var targetDType = document.getElementById('CommonConfig_LstCommonConfig_' + i + '__strDataType').value;

                 if (parseInt(selId) == parseInt(targetPId))
	                {
	                    targetPVal = document.getElementById('CommonConfig_LstCommonConfig_' + i + '__strIfParentVal').value;
	                    var IsChildMandatory = document.getElementById('CommonConfig_LstCommonConfig_' + i + '__bitIsChildMandatory').value;

	                    if (selVal == targetPVal) 
                        {
                            $('#CommonConfig_LstCommonConfig_' + i + '__ConfigValue').removeAttr('readonly');
                            
                            if (IsChildMandatory.toString() == 'True') {
                                $('#lblRequired_' + i).css('visibility', 'visible');                             
                                if (targetDType.toString() == "Number") {
                                    $('#CommonConfig_LstCommonConfig_' + i + '__ConfigValue').removeClass('doubleNR');
                                    $('#CommonConfig_LstCommonConfig_' + i + '__ConfigValue').addClass('double');
                                }
                                else {
                                    $('#CommonConfig_LstCommonConfig_' + i + '__ConfigValue').addClass('required');
                                }
                            }
                            else {
                                $('#lblRequired_' + i).css('visibility', 'hidden');
                                if (targetDType.toString() == "Number") {
                                    $('#CommonConfig_LstCommonConfig_' + i + '__ConfigValue').removeClass('double');
                                    $('#CommonConfig_LstCommonConfig_' + i + '__ConfigValue').addClass('doubleNR');
                                }
                                else {
                                    $('#CommonConfig_LstCommonConfig_' + i + '__ConfigValue').removeClass('required');
                                }   
                            }
	                    }
	                    else 
                        {
                            $('#lblRequired_' + i).css('visibility', 'hidden');
                            $('#CommonConfig_LstCommonConfig_' + i + '__ConfigValue').val("");
                            $('#CommonConfig_LstCommonConfig_' + i + '__ConfigValue').attr('readonly', 'readonly');

                            if (targetDType.toString() == "Number") {
                                $('#CommonConfig_LstCommonConfig_' + i + '__ConfigValue').removeClass('double');
                                $('#CommonConfig_LstCommonConfig_' + i + '__ConfigValue').addClass('doubleNR');
                            }
                            else {
                                $('#CommonConfig_LstCommonConfig_' + i + '__ConfigValue').removeClass('required');
                            }                            
	                    }
		                //break;
	                }  
             }

		    }
		    return false;
        }


</script>

<h3 class="page-title">System Configuration</h3>

<form id="frmCommonConfig" method="post" action="" enctype="multipart/form-data">
    <div id="divCommonConfig">    
        <div id="grid">
            <div id="grid-data" style="overflow: auto; width: 99%;">
                <table style="width: 99%;">
                    <thead>
                        <tr>
                            <th style="width: 5%;">
                                SL
                            </th>
                            <th style="width: 40%;">
                                Configuration Options
                            </th>
                            <th>
                                Settings
                            </th>                            
                        </tr>
                    </thead>
                  </table>
                 <div style="overflow-y: auto; overflow-x: hidden; max-height: 350px">
                  <table id="tblList" style="width: 99%;">
                            <% string strClass = ""; string lblRequiredId = ""; string strSpan = ""; bool IsRequired = false;
                                List<LMSEntity.CommonConfig> objList = Model.CommonConfig.LstCommonConfig; 
                                for (int j = 0; j < Model.CommonConfig.LstCommonConfig.Count; j++)
                                {
                                    IsRequired = false;
                                    lblRequiredId = "lblRequired_" + j;
                                    
                                    if (Model.CommonConfig.LstCommonConfig[j].strControlType == "ddList")
                                    {
                                        if(Model.CommonConfig.LstCommonConfig[j].bitIsMandatory == true)
                                        {
                                            IsRequired = true;
                                            strClass = "selectBoxRegular required";
                                        }
                                        else
                                        {
                                            strClass = "selectBoxRegular";
                                        }
                                    }
                                        
                                    else if (Model.CommonConfig.LstCommonConfig[j].strControlType == "txtBox")
                                    {
                                        if (Model.CommonConfig.LstCommonConfig[j].strDataType == "Number")
                                        {
                                            if (Model.CommonConfig.LstCommonConfig[j].bitIsMandatory == true)
                                            {
                                                IsRequired = true;
                                                strClass = "textRegular double";
                                            }
                                            else if (Model.CommonConfig.LstCommonConfig[j].bitIsMandatory == false)
                                            {
                                                if(Model.CommonConfig.LstCommonConfig[j].intParentID >0 && ! string.IsNullOrEmpty(Model.CommonConfig.LstCommonConfig[j].strIfParentVal))
                                                {
                                                    //if (objList.Where(c => c.ConfigID == Model.CommonConfig.LstCommonConfig[j].intParentID && c.ConfigValue == Model.CommonConfig.LstCommonConfig[j].strIfParentVal).Count() > 0)
                                                    if(Model.CommonConfig.LstCommonConfig[j].bitIsChildMandatory == true)
                                                    {
                                                        IsRequired = true;
                                                        strClass = "textRegular double";
                                                    }
                                                    else
                                                    {
                                                        strClass = "textRegular doubleNR";
                                                    }
                                                }
                                                else
                                                {
                                                    strClass = "textRegular doubleNR";
                                                }
                                            }
                                        }
                                        else if(Model.CommonConfig.LstCommonConfig[j].strDataType == "Text")
                                        {
                                            if (Model.CommonConfig.LstCommonConfig[j].bitIsMandatory == true)
                                            {
                                                IsRequired = true;
                                                strClass = "textRegular required";
                                            }
                                            else if (Model.CommonConfig.LstCommonConfig[j].bitIsMandatory == false)
                                            {
                                                if (Model.CommonConfig.LstCommonConfig[j].intParentID > 0 && !string.IsNullOrEmpty(Model.CommonConfig.LstCommonConfig[j].strIfParentVal))
                                                {
                                                    //if (objList.Where(c => c.ConfigID == Model.CommonConfig.LstCommonConfig[j].intParentID && c.ConfigValue == Model.CommonConfig.LstCommonConfig[j].strIfParentVal).Count() > 0)
                                                    if (Model.CommonConfig.LstCommonConfig[j].bitIsChildMandatory == true)
                                                    {
                                                        IsRequired = true;
                                                        strClass = "textRegular required";
                                                    }
                                                    else
                                                    {
                                                        strClass = "textRegular";
                                                    }
                                                }
                                                else
                                                {
                                                    strClass = "textRegular";
                                                }
                                            }
                                        }
                                    }
                            %>
                            
                            <tr>
                                <td style="width: 5%;">
                                    <%=Html.Hidden("CommonConfig.LstCommonConfig[" + j + "].strEUser", LMS.Web.LoginInfo.Current.strEmpID == null ? string.Empty : LMS.Web.LoginInfo.Current.strEmpID)%>
                                    <%=Html.Hidden("CommonConfig.LstCommonConfig[" + j + "].ConfigID", Model.CommonConfig.LstCommonConfig[j].ConfigID.ToString())%>
                                    <%=Html.Encode(Model.CommonConfig.LstCommonConfig[j].ConfigID)%>
                                </td>
                                <td style="width: 40%;">
                                    <%=Html.Hidden("CommonConfig.LstCommonConfig[" + j + "].ConfigKey", Model.CommonConfig.LstCommonConfig[j].ConfigKey.ToString())%>                                
                                    <%=Html.Encode(Model.CommonConfig.LstCommonConfig[j].ConfigKey)%>                                    
                                    <%if (IsRequired == true)
                                      {
                                          strSpan = "<span id=" + lblRequiredId + " style='visibility: visible' class='labelRequired'>*</span>";
                                      %>                                                                                        
                                                 
                                      <%} else
                                          strSpan = "<span id=" + lblRequiredId + " style='visibility: hidden' class='labelRequired'>*</span>";
                                    
                                        %>

                                      <%= strSpan %>
                                </td>
                                <td>
                                    <%=Html.Hidden("CommonConfig.LstCommonConfig[" + j + "].strControlType", Model.CommonConfig.LstCommonConfig[j].strControlType.ToString())%> 
                                    <%=Html.Hidden("CommonConfig.LstCommonConfig[" + j + "].strDataType", Model.CommonConfig.LstCommonConfig[j].strDataType.ToString())%>  
                                    <%=Html.Hidden("CommonConfig.LstCommonConfig[" + j + "].intParentID", Model.CommonConfig.LstCommonConfig[j].intParentID.ToString())%>  
                                    <%=Html.Hidden("CommonConfig.LstCommonConfig[" + j + "].bitIsMandatory", Model.CommonConfig.LstCommonConfig[j].bitIsMandatory.ToString())%> 
                                    <%=Html.Hidden("CommonConfig.LstCommonConfig[" + j + "].strIfParentVal", Model.CommonConfig.LstCommonConfig[j].strIfParentVal)%> 
                                    <%=Html.Hidden("CommonConfig.LstCommonConfig[" + j + "].bitIsChildMandatory", Model.CommonConfig.LstCommonConfig[j].bitIsChildMandatory.ToString())%> 
                                    
                                    <%if (Model.CommonConfig.LstCommonConfig[j].strControlType=="ddList")
                                        { %>                                         
                                            <%=Html.DropDownList("CommonConfig.LstCommonConfig[" + j + "].ConfigValue", new SelectList(Model.YesNoList, "Value", "Text", Model.CommonConfig.LstCommonConfig[j].ConfigValue), "...Select One...", new { @class = strClass.ToString(), @style = "width:350px; min-width:350px;", @onchange ="return CheckRequiredField(this);"})%>                
                                        <%}
                                        else if (Model.CommonConfig.LstCommonConfig[j].strControlType == "txtBox")
                                        { %>
                                            <%--<%
                                                if (Model.CommonConfig.LstCommonConfig[j].ConfigKey == "HRM Database")
                                                { %>
                                                    <div style="width: 100%; float: left; text-align: left;">
                                                        <div style="float: left; text-align: left;">
                                                            <%=Html.TextBox("CommonConfig.LstCommonConfig[" + j + "].ConfigValue", Model.CommonConfig.LstCommonConfig[j].ConfigValue, new { @class = strClass.ToString(), @style = "width:330px; min-width:330px;", maxlength = 500 })%>                                                            
                                                        </div>
                                                        
                                                        <div style="float: left; text-align: left; padding-left: 3px;  padding-top:3px;">
                                                            <a id="btngridAdd" href="#" class="gridAdd" onclick="return popupStyleAdd();"></a>
                                                        </div>                                                    
                                                    </div>
                                               <%}--%>
                                                 <%--else if (!string.IsNullOrEmpty(Model.CommonConfig.LstCommonConfig[j].strCaption) && Model.CommonConfig.LstCommonConfig[j].strDataType == "Number")--%>
                                                 
                                                 <%
                                                 if (!string.IsNullOrEmpty(Model.CommonConfig.LstCommonConfig[j].strCaption) && Model.CommonConfig.LstCommonConfig[j].strDataType == "Number")
                                                
                                                 {%>
                                                    <div style="width: 100%; float: left; text-align: left;">
                                                        <div style="float: left; text-align: left;">
                                                            <%=Html.TextBox("CommonConfig.LstCommonConfig[" + j + "].ConfigValue", Model.CommonConfig.LstCommonConfig[j].ConfigValue, new { @class = strClass.ToString(), @style = "width:130px; min-width:130px;", maxlength = 6 })%>                                                            
                                                        </div>

                                                        <div style="float: left; text-align: left; padding-left: 3px;  padding-top:3px;">
                                                            <%=Html.Encode(Model.CommonConfig.LstCommonConfig[j].strCaption)%>
                                                        </div>                                                    
                                                    </div>
                                                <%}
                                                else if (Model.CommonConfig.LstCommonConfig[j].strDataType == "Password")
                                                {%>
                                                   <%=Html.Password("CommonConfig.LstCommonConfig[" + j + "].ConfigValue", Model.CommonConfig.LstCommonConfig[j].ConfigValue, new { @class = strClass.ToString(), @style = "width:130px; min-width:130px;" })%>                                                            
                                                <%}                                                     
                                                else
                                                {%>
                                                    <%=Html.TextBox("CommonConfig.LstCommonConfig[" + j + "].ConfigValue", Model.CommonConfig.LstCommonConfig[j].ConfigValue, new { @class = strClass.ToString(),@style = "width:350px; min-width:350px;", maxlength = 500 })%>
                                               <%} %>                                            
                                        <%} %>                                              
                                </td>
                            </tr>
                            <%} %>
                        </table>
                 </div>
               </div>
           </div>                
    </div>     

    <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>
    <div class="divSpacer">
    </div>

    <div class="divButton">
        <%if (Page.User.Identity.Name.ToString().Contains(LMS.Web.AppConstant.SysInitializer) || LMS.Web.LoginInfo.Current.LoginName.ToString().Contains(LMS.Web.AppConstant.SysInitializer) || LMS.Web.Permission.IsMenuOperationPermited(LMS.Web.Permission.MenuNamesId.CommonConfig, LMS.Web.Permission.MenuOperation.Add))
         {%>
            <a id="btnImgSave" href="#" class="btnSave" onclick="return savedata();"></a>
        <%} %>
        <input id="btnSave" name="btnSave" style="visibility: hidden;" type="submit" value="Save" visible="false" />    
    </div>

    <div id="divMsgStd" class="divMsg">
        <%= ViewData["vdMsg"] = Model.Message == null ? "" : Model.Message%>
    </div>

</form>
<div id="divStyleCommonConfig">
    <iframe id="styleOpenerCommonConfig" src="" width="100%" height="98%"
        style="border: 0px solid white; margin-right: 0px; padding-right: 0px;">
        <p>
            Your browser does not support iframes.</p>
    </iframe>
</div>