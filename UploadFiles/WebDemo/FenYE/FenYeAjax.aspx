
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FenYeAjax.aspx.cs" Inherits="WebDemo.FenYE.FenYeAjax" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../CSS/TableCSS.css" rel="stylesheet" />
    <link href="../CSS/pagination.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="tableDiv">
            <asp:Repeater ID="repList" runat="server">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <th>序号</th>
                            <th>名字</th>
                            <th>父级ID</th>
                            <th>显示</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# RowIndex++ %></td>
                        <td><%# Eval("Name")%></td>
                        <td><%# Eval("Pid") %></td>
                        <td><%# Eval("LevalNum") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="pagelistbody boxsizing">
            <div class="pagebox">
                <div class="page flickr">
                </div>
            </div>
            <div class="pagemsg">
                <p>共<span><%= pcount %></span>条数据，共<span><%=Math.Ceiling(decimal.Parse(pcount.ToString())/pagesize) %></span>页，当前第<span><%= page+1 %></span>页</p>
            </div>
        </div>
        <script src="../JS/jquery-1.11.0.min.js"></script>
        <script src="../JS/jquery.tableui.js"></script>
        <script src="../JS/jquery.pagination.js"></script>

        <script>
            $(function () {
                $("table").tableUI();

                //分页插件方法
                $(".page").pagination(<%= pcount %>, {     
                    num_edge_entries:1,  //边缘页数
                    num_display_entries:4,  //主体页数
                    callback:pageCallback,  //回调函数  
                    items_per_page:<%= pagesize%>,  	//每页显示条数                   
                    current_page:<%= page%>,  //当前页                  
                    prev_text: "上一页",
                    next_text: "下一页"
                    //分页链接   strUrl是需要传递的参数  get提交                
            });
            //回调函数    
                function pageCallback(page_id,jq){
                    //page_id  为 当前页码-1  注意这个page_id，你点第一页它是0，你点第3页它是2  需要小心了
                    $.ajax({
                        type:"Post",
                        url:"FenYe.ashx",
                        dataType:"json",
                        data:"page="+page_id+"&pageSize="+<%=pagesize %>,
                        async:true,
                        success:function(data){
                            $("table tr:gt(0)").remove();
                            var GetData=eval(data);                           
                            $.each(GetData,function(i,item){
                                var dd="<tr";
                                i=++i;
                                if (i%2!=0) {
                                    dd+=" class=\"odd\"";
                                }else {
                                    dd+=" class=\"even\"";
                                }
                                dd+="><td>"+i+"</td><td>"+item.Name+"</td><td>"+item.Pid+"</td><td>"+item.LevalNum+"</td></tr>";
                               // $("table").append("<tr class=\"even\"><td>"+(++i)+"</td><td>"+item.Name+"</td><td>"+item.Pid+"</td><td>"+item.LevalNum+"</td></tr>");           
                                $("table").append(dd);
                            })
                        }                       
                    })
                    return true;  //返回真就刷新，返回假不会刷新
                }
            });                      
        </script>

    
    </form>
</body>
</html>
