﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnlineEdit.aspx.cs" Inherits="WebDemo.COM.OnlineEdit" %>

<%@ Register Assembly="PageOffice, Version=2.0.0.1, Culture=neutral, PublicKeyToken=1d75ee5788809228"
    Namespace="PageOffice" TagPrefix="po" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     
    <div style="width:800px;margin:50px auto;">
        <po:PageOfficeCtrl ID="PageOfficeCtrl1" runat="server" Theme="Office2010">
        </po:PageOfficeCtrl>
    </div>
    </form>
</body>
</html>
