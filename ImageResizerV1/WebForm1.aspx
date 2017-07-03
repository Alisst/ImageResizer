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

<%--http://imageresizing.net/docs/v4/plugins/remotereader
    https://stackoverflow.com/questions/31601381/does-imageresizer-work-on-external-urls
    https://www.google.com.tr/search?q=imageresizer+with+out+of+project+image&ie=utf-8&oe=utf-8&gws_rd=cr&ei=JUhaWbORNcLfUdqfrZAJ#q=asp.net+imageresizer+with+external+image
    --%>