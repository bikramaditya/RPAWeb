<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TicketDetails.aspx.cs" Inherits="RPAWebApplicationv2.secureTicket.TicketDetails" %>
<html>
<head>
	<title>Ticket Details</title>
</head>
<body>
    <table id="scriptTable" border="1">
	    <thead>
			<th>TicketId</th>
			<th>TicketNumber</th>
			<th>TicketTitle</th>
			<th>userID</th>
	    </thead>
        <tr><td><%=ticket.TicketId %></td><td><%=ticket.TicketNumber %></td><td><%=ticket.TicketTitle %></td><td><%=ticket.userID %></td></tr>
        <tr height="50px"/>
        <thead>
			<th>Options</th>
			<th>SL No.</th>
			<th>Score</th>
			<th>User Conf Msg</th>
	    </thead>
        <% foreach (var match in ticket.Matches) { %>
    <tr><td><center><input type="radio" name="optradio"></center></td><td><%= match.ID%></td><td><%= match.Score%></td><td><%= match.UserConfirmationMsg%></td></tr>
  <% } %>
    </table>
</body>
</html>