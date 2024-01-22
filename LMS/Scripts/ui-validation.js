function fnValidate() {

    var error = "";
    var notValid = 0;

   
    
    //required field validator
    $("input.required,textarea.required").each(function (i) {
        this.value = $.trim(this.value);
       
        notValid += MarkError(this, this.value.length > 0);
    });

    $("input.password").each(function (i) {
        notValid += MarkError(this, this.value.length > 0);
    });

    //integer field validator
    $("input.integer").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        notValid += MarkError(this, IsInteger(this.value));
    });

    $("input.integerWithCommaSeperator").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        notValid += MarkError(this, IsInteger(this.value));
    });

    $("input.integerWithCommaSeperatorNR").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsInteger(this.value));
    });

    //integer field validator
    $("input.integerNR").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsInteger(this.value));
    });

    //decimal field validator
    $("input.double").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        notValid += MarkError(this, IsDouble(this.value));
    });

    //decimal field validator
    $("input.doubleNR").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsDouble(this.value));
    });

    //Date field validator dd-MM-yyyy
    $("input.date").each(function (i) {
        if (this.style.visibility != "hidden") {
            this.value = $.trim(this.value);
            notValid += MarkError(this, IsDate(this.value, false));
        }
    });

    //Date field validator dd-MM-yyyy
    $("input.dateNR").each(function (i) {
        this.value = $.trim(this.value);
        if (this.value == 'DD-MM-YYYY') {
            this.value = '';
        }
        notValid += MarkError(this, IsDate(this.value, true));
    });

    //Date field validator dd-MM-yyyy and Not future date
    $("input.dateNRF").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsDateF(this.value, true));
    });

    //Time field validator hh:mm tt
    $("input.time").each(function (i) {
        if (this.style.visibility != "hidden") {
            this.value = $.trim(this.value);
            notValid += MarkError(this, IsTime(this.value));
        }
    });

    //Time field validator hh:mm tt (allow blank)
    $("input.timeNR").each(function (i) {
        this.value = $.trim(this.value);
        if (this.value.length < 1) return true;
        notValid += MarkError(this, IsTime(this.value));
    });

    // drop down List Validation
    $("select.required").each(function (i) {
        notValid += MarkErrorForDDL(this, this.selectedIndex > 0);
    });

    //required field validator
    $("input.invalid").each(function (i) {
        this.value = $.trim(this.value);
        
        notValid += MarkError(this, false);
    });

    //Set focus on the first required field.
    $("*").each(function () {
        if (this.tagName == "INPUT") {
            if (this.type == "text") {
                if ($(this).css('border-color') == 'red') {
                    // To solved the invisible field focussing error.
                    try {
                        this.focus();
                    }
                    catch (e) {
                    }
                    return false;
                }
            }
        }

        if (this.tagName == "SELECT") {
            if ($(this).css('color') == 'red') {
                this.focus();
                return false;
            }
        }
    });


    if (notValid > 0) {
        $("#lblMsg").html("<b>Please provide necessary information.</b><br>" + error).css("color", "red");
        //$(document).scrollTop(0);
        return false;
    }
    else {
        $("#lblMsg").html("").css("color", "Green");
        return true;
    }

}

 

function fnValidateDateTime() {

    var error = "";
    var notValid = 0;
    //Date field validator dd-MM-yyyy
    $("input.date").each(function (i) {
        if (this.style.visibility != "hidden") {
            this.value = $.trim(this.value);
            notValid += MarkError(this, IsDate(this.value, false));
        }
    });

    $("input.time").each(function (i) {
        if (this.style.visibility != "hidden") {
            this.value = $.trim(this.value);
            notValid += MarkError(this, IsTime(this.value));
        }
    });

    if (notValid > 0) {
        window.scrollTo(0, 0);
        return false;
    }
    else {
        return true;
    }
}

function fnValidatePopup() {

    var error = "";
    var notValid = 0;

    //required field validator
    $("input.prequired,textarea.prequired").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, this.value.length > 0);
    });

    //integer field validator
    $("input.pinteger").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        notValid += MarkError(this, IsInteger(this.value));
    });

    $("input.pintegerWithCommaSeperator").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        notValid += MarkError(this, IsInteger(this.value));
    });

    $("input.pintegerWithCommaSeperatorNR").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsInteger(this.value));
    });

    //integer field validator
    $("input.pintegerNR").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsInteger(this.value));
    });

    //decimal field validator
    $("input.pdouble").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        notValid += MarkError(this, IsDouble(this.value));
    });

    //decimal field validator
    $("input.pdoubleNR").each(function (i) {
        this.value = $.trim(this.value);
        this.value = this.value.replace(/,/g, "");
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsDouble(this.value));
    });

    //Date field validator dd-MM-yyyy
    $("input.pdate").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsDate(this.value, false));
    });

    //Date field validator dd-MM-yyyy
    $("input.pdateNR").each(function (i) {
        this.value = $.trim(this.value);
        if (this.value == 'DD-MM-YYYY') {
            this.value = '';
        }
        notValid += MarkError(this, IsDate(this.value, true));
    });

    //Date field validator dd-MM-yyyy and Not future date
    $("input.pdateNRF").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsDateF(this.value, true));
    });

    //Time field validator hh:mm tt
    $("input.ptime").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsTime(this.value));
    });

    //Time field validator hh:mm tt (allow blank)
    $("input.ptimeNR").each(function (i) {
        this.value = $.trim(this.value);
        if (this.value.length < 1) return true;
        notValid += MarkError(this, IsTime(this.value));
    });

    // drop down List Validation
    $("select.prequired").each(function (i) {
        notValid += MarkErrorForDDL(this, this.selectedIndex > 0);
    });

    //Set focus on the first required field.
    $("*").each(function () {
        if (this.tagName == "INPUT") {
            if (this.type == "text") {
                if ($(this).css('border-color') == 'red') {
                    // To solved the invisible field focussing error.
                    try {
                        this.focus();
                    }
                    catch (e) {
                    }
                    return false;
                }
            }
        }

        if (this.tagName == "SELECT") {
            if ($(this).css('color') == 'red') {
                this.focus();
                return false;
            }
        }
    });

    if (notValid > 0) {
        $("span[id$=lblMsg]").html("<b>Please provide necessary information.</b><br>" + error).css("color", "red");
        //$(document).scrollTop(0);
        return false;
    }
    else {
        $("span[id$=lblMsg]").html("").css("color", "green");
        return true;
    }

}


function fnValidateRow(bItem) {
    var error = "";

    var Row = bItem.parentNode.parentNode;

    var notValid = 0;


    //required field validator
    $(Row).find("input[class='required'],textarea[class='required']").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, this.value.length > 0);
    });

    //integer required field validator
    $(Row).find("input.integer").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsInteger(this.value));
    });

    //integer field validator
    $(Row).find("input.integerNR").each(function (i) {
        this.value = $.trim(this.value);
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsInteger(this.value));
    });

    //decimal field validator
    $(Row).find("input.double").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsDouble(this.value));
    });

    //decimal field validator
    $(Row).find("input.doubleNR").each(function (i) {
        this.value = $.trim(this.value);
        if (this.value.length < 1) this.value = 0;
        notValid += MarkError(this, IsDouble(this.value));
    });


    //Date field validator dd-MM-yyyy
    $(Row).find("input.date").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsDate(this.value, false));
    });

    //Date field validator dd-MM-yyyy
    $(Row).find("input.dateNR").each(function (i) {
        this.value = $.trim(this.value);
        notValid += MarkError(this, IsDate(this.value, true));
    });

    // drop down List Validation
    $(Row).find("select.required").each(function (i) {
        notValid += MarkErrorForDDL(this, this.selectedIndex > 0);
    });


    if (notValid > 0) {
        $("span[id$=lblMsg]").html("<b>Please provide necessary information.</b><br>" + error).css("color", "red");
        return false;
    }
    else {
        $("span[id$=lblMsg]").html("").css("color", "black");
        return true;
    }
}

//check valid integer 1,15,18
function IsInteger(val) {
    var re = new RegExp("^[0-9]+$");
    return val.match(re);
}

//check valid date 25/12/1989
function IsDate(val, allowBalnk) {
    if (allowBalnk && val.length == 0) return true;
    var dateaprts = val.split('-');
    var dt = new Date(dateaprts[2], dateaprts[1] - 1, dateaprts[0]);
    return (dt.getDate() == dateaprts[0] && dt.getMonth() == dateaprts[1] - 1 && dt.getFullYear() == dateaprts[2])
}

//check valid date 25/12/1989 and Not future date
function IsDateF(val, allowBalnk) {
    var curDate = new Date();
    if (allowBalnk && val.length == 0) return true;
    var dateaprts = val.split('-');
    var dt = new Date(dateaprts[2], dateaprts[1] - 1, dateaprts[0]);
    return (dt.getDate() == dateaprts[0] && dt.getMonth() == dateaprts[1] - 1 && dt.getFullYear() == dateaprts[2] && dt <= curDate)
}

//check valid time 12:15 PM
function IsTime(val) {
    var err = 0
    if (val.length != 8) err = 1
    hh = val.substring(0, 2)// Hour f
    c = val.substring(2, 3)// ':'
    mm = val.substring(3, 5)// Min b
    e = val.substring(5, 6)// ' '
    tt = val.substring(6, 8)// AM/PM

    if (hh < 0 || hh > 12) err = 1
    if (mm < 0 || mm > 59) err = 1
    if (tt != 'AM' && tt != 'PM') err = 1
    if (err == 1) { return false; }
    else { return true };
}

//check valid decimal number  125,125.50 
function IsDouble(val) {
    val = val.replace(/,/g, '');
    return !isNaN(val) && (val.length > 0);
}



function MarkError(control, isValid) {
    //$(control.offsetParent).find("BR,p").remove();
    if (isValid) {
        $(control).css("border-color", "#C5D3E4");
        //$(control).css("border", "solid 1px #C5D3E4");
        return 0;
    }
    else {
        //    $(control.offsetParent).append("<br><p style='color:red'>Required</p>");
        //error +=$(this).attr('id').split('_txt')[1]+'<br>';
        $(control).css("border-color", "red");
        // alert(control.id);
        return 1;
    }

}



function MarkErrorForDDL(control, isValid) {
    if (isValid) {
        $(control).css("color", "black");
        return 0;
    }
    else {
        $(control).css("color", "red");
        return 1;
    }

}

function CheckTime(min1, min2) {

    min1 = MakeInMinutes(min1);
    min2 = MakeInMinutes(min2);

    return min1 > min2;
}

function MakeInMinutes(val) {
    var vampm = "";
    vampm = val.substr(6, 2);
    val = val.substr(0, val.length - 3);
    var av = val.split(':');
    var h, m, retval;
    h = av[0];
    m = av[1];
    h = parseInt(h);
    m = parseInt(m);
    if (vampm.toUpperCase() == "PM") {
        h = h + 12;
    }
    return retval = (h * 60) + m;
}

