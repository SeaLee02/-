<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataToOut.aspx.cs" Inherits="WebDemo.COM.DataToOut" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
      <link href="../CSS/TableCSS.css" rel="stylesheet" />
    <link href="../CSS/dataTolead.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
       <div style="margin-left: 100px;">
           <asp:LinkButton CssClass="add" ID="lnkData" runat="server" OnClientClick="return Test()" OnClick="lnkData_Click">导出<br />
                数据</asp:LinkButton>
           <asp:HiddenField ID="hfID" runat="server" />
        </div>
        <br />
        <br />
        <div class="tableDiv">
            <asp:Repeater ID="DemoRepeater" runat="server">
                <HeaderTemplate>                  
                    <table>
                        <tbody>
                            <tr>
                                 <th width="40">
                                    <input type="checkbox" id="checkAll" name="checkAll" />
                                </th>
                                <th>序号
                                </th>
                                <th>姓名
                                </th>
                                <th>性别
                                </th>
                                <th>年龄
                                </th>
                                <th>电话号码</th>
                            </tr>                     
                  
                </HeaderTemplate>
                <ItemTemplate>
                  <tr>
                       <td align="center">
                                <input type="checkbox" name="chk" value="<%#Eval("ID") %>" onclick="ck();" />
                            </td>
                      <td><%#RowIndex++ %></td>
                      <td><%#Eval("Name") %></td>
                      <td><%#Eval("Sex") %></td>
                      <td><%#Eval("Age") %></td>
                      <td><%#Eval("Tel") %></td>
                   </tr>  
                </ItemTemplate>
                <FooterTemplate>
                       </tbody></table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <script src="../JS/jquery-1.11.0.min.js"></script>
        <script src="../JS/jquery.tableui.js"></script>
        <script>
        
            $(function () {
                $("table").tableUI();             
            });

            //全选/不全选
            $("#checkAll").click(function () {
                var f = $("#checkAll").is(":checked")
                if (f) {
                    $("input[name=chk]:checkbox").prop("checked", true);
                } else {
                    $("input[name=chk]:checkbox").prop("checked", false);
                }
            });

            //单选
            var d = $("input[name=chk]:checkbox").length;
            $("input[name=chk]:checkbox").click(function () {
                var dd = $("input[name=chk]:checked").length;
                if (parseInt(d) === parseInt(dd)) {
                    $("#checkAll").prop("checked", true);
                } else {
                    $("#checkAll").prop("checked", false);
                }
            });

            //取值
            function Test() {             
                var ids = "";            
                var dd = $("input[name=chk]:checked");               
                dd.each(function () {
                        ids += $(this).val() + ",";                       
                });
                if (ids=="") {
                    alert("请选择导入的数据");
                    return false;
                }
                ids = ids.substring(0, ids.length - 1);
                $("#hfID").val(ids);
                return true;
            };
       

           

        </script>
    </form>
</body>
</html>
