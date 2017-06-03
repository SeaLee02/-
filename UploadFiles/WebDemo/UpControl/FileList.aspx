<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileList.aspx.cs" Inherits="WebDemo.UpControl.FileList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>文件下载查看</title>
    <link href="../CSS/TableCSS.css" rel="stylesheet" />
    <link href="../CSS/Show.css" rel="stylesheet" />
    <style>
        .test{
            width: 80px;
            height: 32px;
            float: left;
            line-height: 32px;
            color: #006BBB;
            text-align: center;
            background: #DFDFDF;
            margin-left: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h5>测试文件下载，看会不会出现乱码，多种方式下载</h5>
        <asp:LinkButton ID="lb_DownLoad" runat="server" CssClass="test" OnClick="lb_DownLoad_Click">测试下载</asp:LinkButton>

        <br />
        <hr />
        <asp:LinkButton ID="lb_YS" runat="server" CssClass="test" OnClick="lb_YS_Click" Width="200px">以压缩包的形式下载</asp:LinkButton>

        <div class="tableDiv">
            <asp:Repeater ID="DemoRepeater" runat="server" OnItemCommand="DemoRepeater_ItemCommand">
                <HeaderTemplate>
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <th>
                                <input type="checkbox" onclick="checkAll(this);"></th>
                            <th width="40">排序</th>
                            <th>名称</th>
                            <th>上传时间</th>
                            <th>大小</th>
                            <th width="160">操作</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Style="vertical-align: middle;" />
                            <asp:HiddenField ID="hidFilePath" Value='<%#Eval("FilePath")%>' runat="server" />
                        </td>
                        <td><%= RowIndex++ %></td>
                        <td><%# Eval("FileName") %></td>
                        <td><%# Eval("UpdateTime")%></td>
                        <td><%# Eval("FileSize") %></td>
                        <td>
                            <%--如果是Word，视屏可以直接给路径，图片则不能--%>
                            <%--  <a href='..<%#Eval("FilePath") %>' class="button b">下载</a>--%>
                            <asp:LinkButton ID="lbDownload" CssClass="button b" CommandName="download" CommandArgument='<%#Eval("FilePath") %>' runat="server">下载</asp:LinkButton>
                           <%--如果是视频，把m1改成m2,这里还没成功，就忽略视频查看吧--%>
                              <a class="button c" href='javascript:void(0);' onclick='showDiv("m1","<%# Eval("FilePath") %>")'>查看</a>
                        </td>
                </ItemTemplate>
                <FooterTemplate>
                    </table> 
                </FooterTemplate>
            </asp:Repeater>
        </div>


        <div>
            <a href="UploadDemo.aspx" class="test" >上传文件</a>
        </div>

        <%--显示图片--%>
        <div id="m1" class="shade" style="display: none;">
            <div class="picture">
                <a onclick="closeDiv('m1');" href="javascript:void();">关闭</a>
                <img id="imagePath" src="">
            </div>
        </div>

        <%--显示视屏--%>
        <div id="m2" class="shade" style="display: none;">
            <div class="picture">
                <a onclick="closeDiv('m2');" href="javascript:void();">关闭</a>
                <div id="DIV1">Loading the player ...</div>
            </div>
        </div>


        <script src="../JS/jquery-1.11.0.min.js"></script>
        <script src="../JS/jquery.tableui.js"></script>
        <script src="../JS/jwplayer.js"></script>
        <script>
            $(function () {
                $("table").tableUI();
            })


            function checkAll(chkobj) {
                if (chkobj.checked == true) {
                    $(".ckb input:enabled").prop("checked", true);
                } else {
                    $(".ckb input:enabled").prop("checked", false);
                }
            }

            function closeDiv(divid) {
                if (divid != "m1") {
                    videoplay.remove(); //移除
                }
                document.getElementById(divid).style.display = "none";
            }
            function showDiv(divid, imagePath) {
                if (divid == "m1") {
                    document.getElementById("imagePath").src = imagePath;
                    document.getElementById(divid).style.display = "block";
                }
                else {
                    document.getElementById(divid).style.display = "block";
                    getPlayer("DIV1", "", imagePath);
                }
            }




            function getPlayer(playerId, image, mediaplayer) {
                videoplay = jwplayer(playerId);
                videoplay.setup({
                    autostart: false,
                    skin: "../files/glow.zip",
                    stretching: "fill",
                    flashplayer: "../Video/player.swf",
                    //image:image,
                    width: 460,
                    height: 313,
                    levels: [{ file: "../Scripts/bkaovAYt-1287469.webm" }, { file: mediaplayer }]
                    //暴风视屏
                });
            }
        </script>
    </form>
</body>
</html>
