<%@ Control Language="vb" AutoEventWireup="false" Explicit="true" Codebehind="base.ascx.vb" Inherits="DONEIN_NET.Meetup.Base" %>
<%@ Register TagPrefix="dnn" Assembly="DotNetNuke" Namespace="DotNetNuke.UI.WebControls"%>
<ASP:IMAGE RUNAT="server" ID="img_meetup_logo" IMAGEALIGN="Left" IMAGEURL="images/meetup.png" WIDTH="120" HEIGHT="81" ALTERNATETEXT="Meetup" TOOLTIP="Meetup"  />
<BR />
<BR />
<ASP:REPEATER RUNAT="server" ID="rpt_meetup">
	<ITEMTEMPLATE>
		<DIV CLASS="donein_meetup_meeting" STYLE="width: <%# Container.DataItem("donein_width")%>px;" >
			<DIV CLASS="donein_meetup_left">
				<DIV CLASS="donein_meetup_calendar_month">
					<%# CType(Container.DataItem("time"),DateTime).Tostring("MMM") %>
					<DIV CLASS="donein_meetup_calendar_date">
						<%# CType(Container.DataItem("time"),DateTime).Tostring("dd") %>
					</DIV>
				</DIV>
				<SPAN CLASS="donein_meetup_calendar_day"><%# CType(Container.DataItem("time"),DateTime).Tostring("ddd") %></SPAN> 
				<SPAN CLASS="donein_meetup_calendar_time"><%# CType(Container.DataItem("time"),DateTime).Tostring("t") %></SPAN> 
			</DIV>
			<DIV CLASS="donein_meetup_right" STYLE="width: <%# CInt(Container.DataItem("donein_width")) - 75%>px;" >
				<DIV CLASS="donein_meetup_title_text"><ASP:HYPERLINK RUNAT="server" ID="lnk_donein_meetup_title_link" CSSCLASS="donein_meetup_title_link" NAVIGATEURL='<%#Container.DataItem("event_url")%>' TARGET="_blank" TEXT='<%#Container.DataItem("name")%>' /></DIV>
				<DIV CLASS="donein_meetup_more_text"><ASP:LINKBUTTON RUNAT="server" ID="lnk_donein_meetup_more_link" CSSCLASS="donein_meetup_more_link" COMMANDARGUMENT='<%#Container.DataItem("id")%>' COMMANDNAME='show' TEXT='<%#Container.DataItem("donein_label_more_details")%>' CAUSESVALIDATION="false" /></DIV>
			</DIV>			
		</DIV>
		<DIV RUNAT="server" ID="div_meetup_detail" CLASS="donein_meetup_detail" VISIBLE="false">
			<TABLE BORDER="0" WIDTH="100%" CELLPADDING="0" CELLSPACING="0">
				<TR>
					<TD CLASS="donein_meetup_detail_left">
						<ASP:LABEL RUNAT="server" ID="lbl_donein_meetup_rsvp" TEXT='<%#Container.DataItem("donein_label_rsvp")%>' />
					</TD>
					<TD CLASS="donein_meetup_detail_right" STYLE="width: <%# CInt(Container.DataItem("donein_width")) - 85%>px;">
						<%#Container.DataItem("rsvpcount")%>
					</TD>
				</TR>
				<TR>
					<TD CLASS="donein_meetup_detail_left">
						<ASP:LABEL RUNAT="server" ID="lbl_donein_meetup_venue" TEXT='<%#Container.DataItem("donein_label_venue")%>' />
					</TD>
					<TD CLASS="donein_meetup_detail_right" STYLE="width: <%# CInt(Container.DataItem("donein_width")) - 85%>;">
						<A CSSCLASS="donein_meetup_venue_link" HREF="http://maps.google.com/maps?geocode=&q=<%#Container.DataItem("venue_lat")%>,<%#Container.DataItem("venue_lon")%>" TARGET="_blank"><%#Container.DataItem("venue_name")%></A>
					</TD>
				</TR>
				<TR>
					<TD CLASS="donein_meetup_detail_left">
						<ASP:LABEL RUNAT="server" ID="lbl_donein_meetup_fee" TEXT='<%#Container.DataItem("donein_label_fee")%>' />
					</TD>
					<TD CLASS="donein_meetup_detail_right" STYLE="width: <%# CInt(Container.DataItem("donein_width")) - 85%>;">
						<%# CType(Container.DataItem("fee"), Decimal).ToString("c") %>&nbsp;(<%#Container.DataItem("feecurrency")%>)
					</TD>
				</TR>
				<TR>
					<TD CLASS="donein_meetup_detail_left">
						<ASP:LABEL RUNAT="server" ID="lbl_donein_meetup_description" TEXT='<%#Container.DataItem("donein_label_description")%>' />
					</TD>
					<TD CLASS="donein_meetup_detail_right" STYLE="width: <%# CInt(Container.DataItem("donein_width")) - 85%>;">
						<%#Container.DataItem("description").Replace("]", ">").Replace("[url","<A HREF=""").Replace("[/url","</A").Replace("[","<")%>
					</TD>
				</TR>
				
				
			</TABLE>
		</DIV>
		<DIV CLASS="donein_meetup_border" STYLE="width: <%# Container.DataItem("donein_width")%>px;">
		</DIV>
	</ITEMTEMPLATE>
</ASP:REPEATER>
<ASP:LABEL RUNAT="server" ID="lbl_message" CSSCLASS="donein_meetup_message" STYLE="padding-top: 5px; padding-bottom: 5px;" ENABLEVIEWSTATE="false" />
