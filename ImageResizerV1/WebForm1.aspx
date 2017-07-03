<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ImageResizer.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<form id="form1" runat="server">
    <div>
        <img src="images/beşiktaş.jpg" />
        <img src="images/beşiktaş.jpg?width=200&height=200&mode=stretch" />
    </div>
    <hr/>
    <da:MyImage ID="imgBesiktas" runat="server" ImageUrl="images/beşiktaş.jpg" MaxHeight="250"
                MaxWidth="250" Quality="90" Thumbnail="false" Scaled="true" />
</form>
</body>
</html>
