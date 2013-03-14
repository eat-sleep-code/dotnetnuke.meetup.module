Imports DotNetNuke
Imports DotNetNuke.Common.Globals
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Services.Exceptions

Namespace DONEIN_NET.Meetup

    Public Class Settings
        Inherits DotNetNuke.Entities.Modules.PortalModuleBase
        Implements Entities.Modules.IActionable
        'Implements Entities.Modules.IPortable
        'Implements Entities.Modules.ISearchable



		#Region " Declare: Shared Classes "

		#End Region





		#Region " Declare: Local Objects "
			
			Protected WithEvents txt_api_key As System.Web.UI.WebControls.TextBox
			Protected WithEvents lnk_get_api_key As System.Web.UI.WebControls.HyperLink
			Protected WithEvents txt_meetup_group As System.Web.UI.WebControls.TextBox
			Protected WithEvents ddl_show_meetups As System.Web.UI.WebControls.DropDownList
			Protected WithEvents txt_description_length As System.Web.UI.WebControls.TextBox
			Protected WithEvents txt_module_width As System.Web.UI.WebControls.TextBox
			Protected WithEvents rad_show_logo As System.Web.UI.WebControls.RadioButtonList
			Protected WithEvents lbl_legal As System.Web.UI.WebControls.Label
			
			Protected WithEvents btn_update As System.Web.UI.WebControls.LinkButton
			Protected WithEvents btn_cancel As System.Web.UI.WebControls.LinkButton

		#End Region
		
		
		
		

		#Region " Page: Load "

			Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
				
				If Not IsPostBack Then
				
					module_localize() '// LOCALIZE THE MODULE
					
					'// GET MODULE SETTINGS
					Dim tmp_meetup_api_key As String = CType(Settings("donein_meetup_api_key"), String)
					Dim tmp_meetup_group As String = CType(Settings("donein_meetup_group"), String)
					Dim tmp_meetup_show_number As String = CType(Settings("donein_meetup_show_number"), String)
					Dim tmp_meetup_description_length As String = CType(Settings("donein_meetup_description_length"), String)
					Dim tmp_meetup_module_width As String = CType(Settings("donein_meetup_module_width"), String)
					Dim tmp_meetup_show_logo As String = CType(Settings("donein_meetup_show_logo"), String)
					
					
					If tmp_meetup_api_key = "" Then
						txt_api_key.Text = ""
					Else
						txt_api_key.Text = tmp_meetup_api_key
					End If
					
					
					
					If tmp_meetup_group = "" Then
						txt_meetup_group.Text = ""
					Else
						txt_meetup_group.Text = tmp_meetup_group
					End If
					
					
					
					For i As Integer = 1 To 5
						ddl_show_meetups.Items.Add(New ListItem(i.ToString, i.ToString))
					Next
					If tmp_meetup_show_number = "" Then
						ddl_show_meetups.SelectedValue = "1"
					Else
						ddl_show_meetups.SelectedValue = tmp_meetup_show_number
					End If
					
					
					
					If tmp_meetup_description_length = "" Then
						txt_description_length.Text = "1024"
					Else
						txt_description_length.Text = tmp_meetup_description_length
					End If
					
					
					
					If tmp_meetup_module_width = "" Then
						txt_module_width.Text = "300"
					Else
						txt_module_width.Text = tmp_meetup_module_width
					End If
					
					
					
					If tmp_meetup_show_logo = "" Then
						rad_show_logo.SelectedValue = "1"
					Else
						rad_show_logo.SelectedValue = tmp_meetup_show_logo
					End If

					
				End If
				
			End Sub

		#End Region



		
		
		#Region " Page: Localize "

 			Private Sub module_localize()
 				
				btn_update.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_btn_update.Text", LocalResourceFile)
				btn_cancel.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_btn_cancel.Text", LocalResourceFile)
				
				lbl_legal.Text = DotNetNuke.Services.Localization.Localization.GetString("pl_legal.Text", LocalResourceFile)
				
				rad_show_logo.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_yes.Text", LocalResourceFile),"1"))
				rad_show_logo.Items.Add(New ListItem(DotNetNuke.Services.Localization.Localization.GetString("pl_no.Text", LocalResourceFile),"-1"))
				
				lnk_get_api_key.Text = Services.Localization.Localization.GetString("pl_get_api_key.Text", LocalResourceFile)
				lnk_get_api_key.ToolTip = Services.Localization.Localization.GetString("pl_get_api_key.Tooltip", LocalResourceFile)
				
							
			End Sub 

		#End Region
			
					
		
		
		
		#Region " Handle: Update Button (btn_update) "

			Private Sub btn_update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_update.Click
				Try
					Dim obj_modules As New ModuleController
					obj_modules.UpdateModuleSetting(ModuleId, "donein_meetup_api_key", txt_api_key.Text.Trim)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_meetup_group", txt_meetup_group.Text.Trim)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_meetup_show_number", ddl_show_meetups.SelectedValue)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_meetup_description_length", txt_description_length.Text.Trim)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_meetup_module_width", txt_module_width.Text.Trim)
					obj_modules.UpdateModuleSetting(ModuleId, "donein_meetup_show_logo", rad_show_logo.SelectedValue)
					
					Try
						Session.Remove("session_donein_meetup_table")
						Session.Remove("session_donein_meetup_width")
					Catch ex As Exception
						'// SUPPRESS ERROR
					End Try
					Response.Redirect(NavigateURL(), True)
				Catch ex As Exception 
					ProcessModuleLoadException(Me, ex)
				End Try
			End Sub
			
		#End Region
		
		
		
		
		
		#Region " Handle: Cancel Button (btn_cancel) "

			Private Sub btn_cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
				Try
					Response.Redirect(NavigateURL(), True)
				Catch ex As Exception		  
					ProcessModuleLoadException(Me, ex)
				End Try
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

		
		
		
		
		#Region "Optional Interfaces"

			Public ReadOnly Property ModuleActions() As Entities.Modules.Actions.ModuleActionCollection Implements Entities.Modules.IActionable.ModuleActions
				Get
					Dim Actions As New Entities.Modules.Actions.ModuleActionCollection
					'// DO NOTHING
					Return Actions
				End Get
			End Property

			'Public Function ExportModule(ByVal ModuleID As Integer) As String Implements Entities.Modules.IPortable.ExportModule
			'	' included as a stub only so that the core knows this module Implements Entities.Modules.IPortable
			'End Function

			'Public Sub ImportModule(ByVal ModuleID As Integer, ByVal Content As String, ByVal Version As String, ByVal UserId As Integer) Implements Entities.Modules.IPortable.ImportModule
			'	' included as a stub only so that the core knows this module Implements Entities.Modules.IPortable
			'End Sub

			'Public Function GetSearchItems(ByVal ModInfo As Entities.Modules.ModuleInfo) As Services.Search.SearchItemInfoCollection Implements Entities.Modules.ISearchable.GetSearchItems
			'	' included as a stub only so that the core knows this module Implements Entities.Modules.ISearchable
			'End Function

		#End Region
		
		
		
	
    	
    End Class

End Namespace