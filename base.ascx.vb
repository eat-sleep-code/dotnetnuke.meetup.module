Imports DotNetNuke
Imports System.Xml
Imports System.Text

Namespace DONEIN_NET.Meetup

	Public Class Base
		Inherits DotNetNuke.Entities.Modules.PortalModuleBase
		Implements Entities.Modules.IActionable
        'Implements Entities.Modules.IPortable
        Implements Entities.Modules.ISearchable
		
		
		
		#Region " Declare: Shared Classes "

			Private module_info As New Module_Info()
			Private date_time As New Date_Time()
			
		#End Region





		#Region " Declare: Local Objects "
			Protected WithEvents lbl_message As System.Web.UI.WebControls.Label
			Protected WithEvents img_meetup_logo As System.Web.UI.WebControls.Image
			Protected WithEvents rpt_meetup As System.Web.UI.WebControls.Repeater
			
		#End Region





		#Region " Page: Load "

			Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
				If Request.QueryString.Item("debug") <> "" Then
					module_info.get_info(Request.QueryString.Item("debug").Trim, ModuleID, TabID)
				End If
				
				If Not IsPostBack Then
					
					'// GET MODULE SETTINGS
					Dim tmp_meetup_api_key As String = CType(Settings("donein_meetup_api_key"), String) + ""
					Dim tmp_meetup_group As String = CType(Settings("donein_meetup_group"), String) + ""
					Dim tmp_meetup_show_number As String = CType(Settings("donein_meetup_show_number"), String) + ""
					Dim tmp_meetup_description_length As String = CType(Settings("donein_meetup_description_length"), String) + ""
					Dim tmp_meetup_width As String = CType(Settings("donein_meetup_module_width"), String) + ""
					Dim tmp_meetup_show_logo As String = CType(Settings("donein_meetup_show_logo"), String) + ""
					
					Dim dt_meetup As New DataTable	
						
					If tmp_meetup_description_length = "" Then
						tmp_meetup_description_length = "1024"
					End If
					
					If tmp_meetup_width = "" Then
						tmp_meetup_width = "300"
					End If
					Session.Add("session_donein_meetup_width", tmp_meetup_width)
					
					If tmp_meetup_show_logo = "" Then
						tmp_meetup_show_logo = "1"
					End If
					If tmp_meetup_show_logo = 1 Then
						img_meetup_logo.Visible = True
					Else
						img_meetup_logo.Visible = False
					End If
					
					If tmp_meetup_api_key = "" Then
						lbl_message.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_message_configure.Text", LocalResourceFile)
						lbl_message.Visible = True
					Else
					
						If Session.Item("session_donein_meetup_table") Is Nothing Then
							'// SET THE CONNECTION STRING
							Dim xml_connection_string As String = ""
							xml_connection_string = "http://api.meetup.com/events.xml?key=" + tmp_meetup_api_key + "&group_urlname=" + tmp_meetup_group.Replace("http://www.meetup.com/", "").Replace("https://","").Replace("http://","").Replace("www.meetup.com","").Replace("meetup.com","").Replace("/","").Replace("\","") + "&order=time"
							'Response.Write(xml_connection_string + "<BR />")
							'// INITIALIZE THE XML READER
							Dim xml_reader_settings As New XmlReaderSettings()
							Dim xml_reader As XmlReader = XmlReader.Create(xml_connection_string, xml_reader_settings)
							xml_reader.ReadToFollowing("items")
							
							'// INITIALIZE THE XML DOCUMENT
							Dim xml_document As New XmlDocument
							xml_document.Load(xml_reader)
							Dim xml_root As XmlNode = xml_document.DocumentElement
						
							
							Dim item_limit As Integer = CInt(tmp_meetup_show_number) - 1
							If xml_root.ChildNodes.Count < item_limit Then
								item_limit = xml_root.ChildNodes.Count
							End If
							For t As Integer = 0 To item_limit
								Try
									
									'// ADD COLUMNS
									If dt_meetup.Columns.Count = 0 Then 
										For r As Integer = 0 To xml_root.ChildNodes(t).ChildNodes.Count
											Try
												Dim dc_meetup As New DataColumn(xml_root.ChildNodes(t).ChildNodes.Item(r).Name, GetType(String))
												dt_meetup.Columns.Add(dc_meetup)
											Catch ex As Exception
												'// SUPPRESS ERROR
											End Try
										Next
										'// ADD IN SOME ADDITIONAL COLUMNS TO STORE SOME ADDITIONAL DATA
										Dim dc_donein_width As New DataColumn("donein_width", GetType(String))
										Dim dc_donein_label_more_details As New DataColumn("donein_label_more_details", GetType(String))
										Dim dc_donein_label_venue As New DataColumn("donein_label_venue", GetType(String))
										Dim dc_donein_label_rsvp As New DataColumn("donein_label_rsvp", GetType(String))
										Dim dc_donein_label_fee As New DataColumn("donein_label_fee", GetType(String))
										Dim dc_donein_label_description As New DataColumn("donein_label_description", GetType(String))
										dt_meetup.Columns.Add(dc_donein_width)
										dt_meetup.Columns.Add(dc_donein_label_more_details)
										dt_meetup.Columns.Add(dc_donein_label_venue)
										dt_meetup.Columns.Add(dc_donein_label_rsvp)
										dt_meetup.Columns.Add(dc_donein_label_fee)
										dt_meetup.Columns.Add(dc_donein_label_description)
									End If
									
									'// ADD ROWS
									Dim dr_meetup As DataRow = dt_meetup.NewRow()
									For r As Integer = 0 To xml_root.ChildNodes(t).ChildNodes.Count 
										Try
											If xml_root.ChildNodes(t).ChildNodes.Item(r).Name = "time" Or xml_root.ChildNodes(t).ChildNodes.Item(r).Name = "updated" Then
												Try 
													Dim month As String = xml_root.ChildNodes(t).ChildNodes.Item(r).InnerText.Substring(4, 3)
													Dim day As String = xml_root.ChildNodes(t).ChildNodes.Item(r).InnerText.Substring(8, 2)
													Dim year As String = xml_root.ChildNodes(t).ChildNodes.Item(r).InnerText.Substring(24, 4)
													Dim time As String = xml_root.ChildNodes(t).ChildNodes.Item(r).InnerText.Substring(11, 8)
													dr_meetup(xml_root.ChildNodes(t).ChildNodes.Item(r).Name) = CDate(month + " " + day + " " + year + " " + time).ToString
												Catch ex As Exception
													dr_meetup(xml_root.ChildNodes(t).ChildNodes.Item(r).Name) = xml_root.ChildNodes(t).ChildNodes.Item(r).InnerText
												End Try
											Else If xml_root.ChildNodes(t).ChildNodes.Item(r).Name = "description" And xml_root.ChildNodes(t).ChildNodes.Item(r).InnerText.Length > CInt(tmp_meetup_description_length) Then
												dr_meetup(xml_root.ChildNodes(t).ChildNodes.Item(r).Name) = Left(xml_root.ChildNodes(t).ChildNodes.Item(r).InnerText, CInt(tmp_meetup_description_length)) + "..."
											Else
												dr_meetup(xml_root.ChildNodes(t).ChildNodes.Item(r).Name) = xml_root.ChildNodes(t).ChildNodes.Item(r).InnerText
											End If	
										Catch ex As Exception
											'// SUPPRESS ERROR
										End Try
									Next
									
									dr_meetup("donein_width") = tmp_meetup_width.ToString
									dr_meetup("donein_label_more_details") = DotNetNuke.Services.Localization.Localization.GetString("pl_donein_meetup_more_link.Text", LocalResourceFile)
									dr_meetup("donein_label_venue") = DotNetNuke.Services.Localization.Localization.GetString("pl_donein_meetup_venue.Text", LocalResourceFile)
									dr_meetup("donein_label_rsvp") = DotNetNuke.Services.Localization.Localization.GetString("pl_donein_meetup_rsvp.Text", LocalResourceFile)
									dr_meetup("donein_label_fee") = DotNetNuke.Services.Localization.Localization.GetString("pl_donein_meetup_fee.Text", LocalResourceFile)
									dr_meetup("donein_label_description") = DotNetNuke.Services.Localization.Localization.GetString("pl_donein_meetup_description.Text", LocalResourceFile)
									dt_meetup.Rows.Add(dr_meetup)
									
									
								Catch ex As Exception
									'// SUPPRESS ERROR
								End Try
							Next
							'Response.Write("Columns: " + dt_meetup.Columns.Count.ToString + "  Rows: " + dt_meetup.Rows.Count.ToString + "<BR />")
							
							'// ADD DATATABLE TO SESSION (REDUCE LOADTIME AND REDUCE CALLS TO MEETUP)
							Session.Add("session_donein_meetup_table", dt_meetup)
						End If
						
						
						
					End If
					
					Dim dt_meetup_show As DataTable = CType(Session.Item("session_donein_meetup_table"), DataTable)
					rpt_meetup.DataSource = dt_meetup_show
					rpt_meetup.DataBind()
					
				End If
				
			End Sub

		#End Region
		
		
	



		#Region " Page: PreRender "

			Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
				module_localize() '// LOCALIZE THE MODULE
			End Sub

		#End Region





		#Region " Page: Localization "

 			Private Sub module_localize()
 				Try
					'lnk_example.ToolTip = DotNetNuke.Services.Localization.Localization.GetString("pl_lnk_example.Tooltip", LocalResourceFile)
					
				Catch ex As Exception
					Response.Write("Error Reading Localization File<BR />")
				End Try
			End Sub 

		#End Region


		
		
		#Region " Details: Show "
			
  			Private Sub rpt_meetup_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rpt_meetup.ItemCommand
				If e.CommandName = "show" Then
					Dim div_results As HtmlGenericControl = CType(e.Item.FindControl("div_meetup_detail"), HtmlGenericControl)
					Dim lnk_amount_of_detail As LinkButton = CType(e.Item.FindControl("lnk_donein_meetup_more_link"), LinkButton)
					If Not div_results Is Nothing Then
						If div_results.Visible = True Then
							div_results.Visible = False
							lnk_amount_of_detail.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_donein_meetup_more_link.Text", LocalResourceFile)
						Else
							div_results.Style.Add("width", Session.Item("session_donein_meetup_width") + "px")
							div_results.Visible = True
							lnk_amount_of_detail.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_donein_meetup_less_link.Text", LocalResourceFile)
						End If
					End If
				End If
			End Sub
			
			
			
		#End Region
		
		
		
		
		#Region " Web Form Designer Generated Code "

				'This call is required by the Web Form Designer.
				<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

				End Sub

				Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
					'CODEGEN: This method call is required by the Web Form Designer
					'Do not modify it using the code editor.
					InitializeComponent()
				End Sub

		#End Region
		
		
		
		

		#Region " Optional Interfaces "

			Public ReadOnly Property ModuleActions() As Entities.Modules.Actions.ModuleActionCollection Implements Entities.Modules.IActionable.ModuleActions
				Get
					Dim Actions As New Entities.Modules.Actions.ModuleActionCollection
						Actions.Add(GetNextActionID, DotNetNuke.Services.Localization.Localization.GetString(Entities.Modules.Actions.ModuleActionType.ContentOptions, LocalResourceFile), Entities.Modules.Actions.ModuleActionType.ContentOptions, "", "", EditUrl("Edit"), False, Security.SecurityAccessLevel.Edit, True, False)
					Return Actions
				End Get
			End Property


			'Public Function ExportModule(ByVal ModuleID As Integer) As String Implements Entities.Modules.IPortable.ExportModule
			'	' included as a stub only so that the core knows this module Implements Entities.Modules.IPortable
			'End Function

			'Public Sub ImportModule(ByVal ModuleID As Integer, ByVal Content As String, ByVal Version As String, ByVal UserId As Integer) Implements Entities.Modules.IPortable.ImportModule
			'	' included as a stub only so that the core knows this module Implements Entities.Modules.IPortable
			'End Sub

			Public Function GetSearchItems(ByVal ModInfo As Entities.Modules.ModuleInfo) As Services.Search.SearchItemInfoCollection Implements Entities.Modules.ISearchable.GetSearchItems
				' included as a stub only so that the core knows this module Implements Entities.Modules.ISearchable
			End Function

		#End Region
		
		
  		
  End Class

End Namespace
