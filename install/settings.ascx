<%@ Control Language="vb" AutoEventWireup="false" Codebehind="settings.ascx.vb" Inherits="DONEIN_NET.Meetup.Settings" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<SCRIPT language=Javascript>
	<!--
	function isNumberKey(evt)
	{
		var charCode = (evt.which) ? evt.which : event.keyCode
		if (charCode > 31 && (charCode < 48 || charCode > 57))
		return false;

		return true;
	}
	//-->
</SCRIPT>

<TABLE BORDER="0" CELLSPACING="0" CELLPADDING="2" ALIGN="left">
	<TR HEIGHT="38">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="top">
			<DNN:LABEL RUNAT="server" ID="pl_api_key" CONTROLNAME="txt_api_key" SUFFIX=":" />
		</TD>
		<TD WIDTH="420" ALIGN="left" VALIGN="top">
			<ASP:TEXTBOX RUNAT="server" ID="txt_api_key" CSSCLASS="NormalTextBox" MAXLENGTH="128" STYLE="width: 400px;"/>
			<BR />
			<ASP:HYPERLINK RUNAT="server" ID="lnk_get_api_key" CSSCLASS="Normal" NAVIGATEURL="http://www.meetup.com/meetup_api/key/" />
			<BR />
		</TD>
	</TR>
	
	<TR HEIGHT="38">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="top">
			<DNN:LABEL RUNAT="server" ID="pl_meetup_group" CONTROLNAME="txt_meetup_group" SUFFIX=":" />
		</TD>
		<TD WIDTH="420" ALIGN="left" VALIGN="top">
			<ASP:TEXTBOX RUNAT="server" ID="txt_meetup_group" CSSCLASS="NormalTextBox" MAXLENGTH="1024" STYLE="width: 400px;" />
			<BR />
		</TD>
	</TR>
	
	<TR HEIGHT="38">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="top">
			<DNN:LABEL RUNAT="server" ID="pl_show_meetups" CONTROLNAME="ddl_show_meetups" SUFFIX=":" />
		</TD>
		<TD WIDTH="420" ALIGN="left" VALIGN="top">
			<ASP:DROPDOWNLIST RUNAT="server" ID="ddl_show_meetups" CSSCLASS="NormalTextBox" STYLE="width: 400px;"/>
			<BR />
		</TD>
	</TR>
	<TR HEIGHT="38">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="top">
			<DNN:LABEL RUNAT="server" ID="pl_description_length" CONTROLNAME="txt_description_length" SUFFIX=":" />
		</TD>
		<TD WIDTH="420" ALIGN="left" VALIGN="top">
			<ASP:TEXTBOX RUNAT="server" ID="txt_description_length" CSSCLASS="NormalTextBox" MAXLENGTH="5" STYLE="width: 400px;" ONKEYPRESS="return isNumberKey(event)" />
			<BR />
		</TD>
	</TR>
	<TR HEIGHT="38">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="top">
			<DNN:LABEL RUNAT="server" ID="pl_module_width" CONTROLNAME="txt_module_width" SUFFIX=":" />
		</TD>
		<TD WIDTH="420" ALIGN="left" VALIGN="top">
			<ASP:TEXTBOX RUNAT="server" ID="txt_module_width" CSSCLASS="NormalTextBox" MAXLENGTH="4" STYLE="width: 400px;" ONKEYPRESS="return isNumberKey(event)" />
			<BR />
		</TD>
	</TR>
	<TR HEIGHT="38">
		<TD WIDTH="180" CLASS="SubHead" ALIGN="left" VALIGN="top">
			<DNN:LABEL RUNAT="server" ID="pl_show_logo" CONTROLNAME="rad_show_logo" SUFFIX=":" />
		</TD>
		<TD WIDTH="420" ALIGN="left" VALIGN="top">
			<ASP:RADIOBUTTONLIST RUNAT="server" ID="rad_show_logo" CSSCLASS="NormalTextBox"  REPEATDIRECTION="Horizontal"></ASP:RADIOBUTTONLIST>
			<BR />
		</TD>
	</TR>
	<TR HEIGHT="38">
		<TD COLSPAN="2" ALIGN="center" VALIGN="top">
			<HR NOSHADE />
		</TD>
	</TR>
	<TR HEIGHT="38">
		<TD COLSPAN="2" ALIGN="left" VALIGN="top">
			<ASP:LINKBUTTON RUNAT="server" ID="btn_update" />
			&nbsp;&nbsp;	
			<ASP:LINKBUTTON RUNAT="server" ID="btn_cancel" />
		</TD>        
	</TR>
	<TR HEIGHT="38">
		<TD COLSPAN="2" ALIGN="left" VALIGN="top">
			<BR />
			<ASP:LABEL RUNAT="server" ID="lbl_legal" STYLE="font-size: 80%;" />
		</TD>        
	</TR>
</TABLE>
