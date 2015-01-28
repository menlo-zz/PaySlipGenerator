<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="PaySlipGeneration.frmLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table align="center">
        <tr>
            <td>User Id</td>
             <td> 
            <asp:TextBox ID="txtUserId" runat="server"></asp:TextBox>
              </td>
        </tr>
        <tr>
           <td>Password</td>
             <td> 
            <asp:TextBox ID="txtPwd" runat="server"></asp:TextBox>
              </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
