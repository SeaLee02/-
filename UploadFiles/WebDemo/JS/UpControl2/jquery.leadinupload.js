/// <reference path="jquery-1.11.1.min.js" />

//当我点击上传的时候，这个事件触发，然后主动让下面的事件发生
var btnfileaclick = function ($this) {

    $this.parent().siblings(".fileinput").find("input[type=file]").click();

}


var fileonchange = function ($this,formId) {

    var fileid = $this.attr("id"); //文件上传ID
    var txtid = $this.parent().siblings(".txtfilename").find("input[type=text]").attr("id"); //路径ID
    //formId是form的Id   ajaxSubmit的前提需要引用jquery.form.min.js
    $("#" + formId).ajaxSubmit({

        beforeSubmit: function (formData, jqForm, options) {
        },
        success: function (data) {
            $("#" + data.txtName).val(data.filePath);   //文本框赋值   data有id和value
        },
        error: function (data, status, e) {
            alert("上传失败");
        },
        url: "/Tools/Update.ashx",
        type: "post", //post提交
        data: { fileid: fileid, txtid: txtid },
        dataType: "json",
        timeout: 60000
    });

}


























