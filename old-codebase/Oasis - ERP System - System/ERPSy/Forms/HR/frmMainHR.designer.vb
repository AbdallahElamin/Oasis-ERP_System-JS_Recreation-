<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainHR
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainHR))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.ChkSp = New System.Windows.Forms.ToolStripMenuItem
        Me.NewProfileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.UpdateProfileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PrintIDCardToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.StaffListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ChangePasswordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ChkCont = New System.Windows.Forms.ToolStripMenuItem
        Me.ChkNewJob = New System.Windows.Forms.ToolStripMenuItem
        Me.ChkSA = New System.Windows.Forms.ToolStripMenuItem
        Me.StaffAppraisalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ChkSAA = New System.Windows.Forms.ToolStripMenuItem
        Me.ChkPaySht = New System.Windows.Forms.ToolStripMenuItem
        Me.AddGradeLevelAllowanceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SetEmployeeSalaryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ViewNationalInsuranceContributionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AddNewBankAccountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ViewPaySheetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.VacationManagementToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ChkVReq = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ChkVSupApprove = New System.Windows.Forms.ToolStripMenuItem
        Me.ChkRSupApprove = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.HumanResourcesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ChkHrApprove = New System.Windows.Forms.ToolStripMenuItem
        Me.ChkHrRapprove = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.ChkLeaveArchive = New System.Windows.Forms.ToolStripMenuItem
        Me.ChkDutyArchive = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.ChkLP = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.ChkHr = New System.Windows.Forms.ToolStripMenuItem
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(0, 29)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(1028, 428)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 4
        Me.PictureBox1.TabStop = False
        '
        'ChkSp
        '
        Me.ChkSp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewProfileToolStripMenuItem, Me.UpdateProfileToolStripMenuItem, Me.PrintIDCardToolStripMenuItem, Me.ToolStripSeparator1, Me.StaffListToolStripMenuItem})
        Me.ChkSp.Name = "ChkSp"
        Me.ChkSp.Size = New System.Drawing.Size(110, 25)
        Me.ChkSp.Text = "Staff Profiles"
        '
        'NewProfileToolStripMenuItem
        '
        Me.NewProfileToolStripMenuItem.Name = "NewProfileToolStripMenuItem"
        Me.NewProfileToolStripMenuItem.Size = New System.Drawing.Size(179, 26)
        Me.NewProfileToolStripMenuItem.Text = "New Profile"
        '
        'UpdateProfileToolStripMenuItem
        '
        Me.UpdateProfileToolStripMenuItem.Name = "UpdateProfileToolStripMenuItem"
        Me.UpdateProfileToolStripMenuItem.Size = New System.Drawing.Size(179, 26)
        Me.UpdateProfileToolStripMenuItem.Text = "Update Profile"
        '
        'PrintIDCardToolStripMenuItem
        '
        Me.PrintIDCardToolStripMenuItem.Name = "PrintIDCardToolStripMenuItem"
        Me.PrintIDCardToolStripMenuItem.Size = New System.Drawing.Size(179, 26)
        Me.PrintIDCardToolStripMenuItem.Text = "Print ID Card"
        Me.PrintIDCardToolStripMenuItem.Visible = False
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(176, 6)
        '
        'StaffListToolStripMenuItem
        '
        Me.StaffListToolStripMenuItem.Name = "StaffListToolStripMenuItem"
        Me.StaffListToolStripMenuItem.Size = New System.Drawing.Size(179, 26)
        Me.StaffListToolStripMenuItem.Text = "Staff List"
        '
        'ChangePasswordToolStripMenuItem
        '
        Me.ChangePasswordToolStripMenuItem.Name = "ChangePasswordToolStripMenuItem"
        Me.ChangePasswordToolStripMenuItem.Size = New System.Drawing.Size(146, 25)
        Me.ChangePasswordToolStripMenuItem.Text = "Change Password"
        '
        'ChkCont
        '
        Me.ChkCont.Name = "ChkCont"
        Me.ChkCont.Size = New System.Drawing.Size(81, 25)
        Me.ChkCont.Text = "Contract"
        '
        'ChkNewJob
        '
        Me.ChkNewJob.Name = "ChkNewJob"
        Me.ChkNewJob.Size = New System.Drawing.Size(140, 25)
        Me.ChkNewJob.Text = "New job Request"
        '
        'ChkSA
        '
        Me.ChkSA.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StaffAppraisalToolStripMenuItem, Me.ChkSAA})
        Me.ChkSA.Name = "ChkSA"
        Me.ChkSA.Size = New System.Drawing.Size(123, 25)
        Me.ChkSA.Text = "Staff Appraisal"
        '
        'StaffAppraisalToolStripMenuItem
        '
        Me.StaffAppraisalToolStripMenuItem.Name = "StaffAppraisalToolStripMenuItem"
        Me.StaffAppraisalToolStripMenuItem.Size = New System.Drawing.Size(248, 26)
        Me.StaffAppraisalToolStripMenuItem.Text = "New appraisal"
        '
        'ChkSAA
        '
        Me.ChkSAA.Name = "ChkSAA"
        Me.ChkSAA.Size = New System.Drawing.Size(248, 26)
        Me.ChkSAA.Text = "Staff Appraisal Approval"
        '
        'ChkPaySht
        '
        Me.ChkPaySht.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddGradeLevelAllowanceToolStripMenuItem, Me.SetEmployeeSalaryToolStripMenuItem, Me.ViewNationalInsuranceContributionToolStripMenuItem, Me.AddNewBankAccountToolStripMenuItem, Me.ViewPaySheetToolStripMenuItem})
        Me.ChkPaySht.Name = "ChkPaySht"
        Me.ChkPaySht.Size = New System.Drawing.Size(90, 25)
        Me.ChkPaySht.Text = "Pay Sheet"
        '
        'AddGradeLevelAllowanceToolStripMenuItem
        '
        Me.AddGradeLevelAllowanceToolStripMenuItem.Name = "AddGradeLevelAllowanceToolStripMenuItem"
        Me.AddGradeLevelAllowanceToolStripMenuItem.Size = New System.Drawing.Size(337, 26)
        Me.AddGradeLevelAllowanceToolStripMenuItem.Text = "Add Grade Level Allowance"
        '
        'SetEmployeeSalaryToolStripMenuItem
        '
        Me.SetEmployeeSalaryToolStripMenuItem.Name = "SetEmployeeSalaryToolStripMenuItem"
        Me.SetEmployeeSalaryToolStripMenuItem.Size = New System.Drawing.Size(337, 26)
        Me.SetEmployeeSalaryToolStripMenuItem.Text = "Set Employee Salary"
        '
        'ViewNationalInsuranceContributionToolStripMenuItem
        '
        Me.ViewNationalInsuranceContributionToolStripMenuItem.Name = "ViewNationalInsuranceContributionToolStripMenuItem"
        Me.ViewNationalInsuranceContributionToolStripMenuItem.Size = New System.Drawing.Size(337, 26)
        Me.ViewNationalInsuranceContributionToolStripMenuItem.Text = "View National Insurance contribution"
        '
        'AddNewBankAccountToolStripMenuItem
        '
        Me.AddNewBankAccountToolStripMenuItem.Name = "AddNewBankAccountToolStripMenuItem"
        Me.AddNewBankAccountToolStripMenuItem.Size = New System.Drawing.Size(337, 26)
        Me.AddNewBankAccountToolStripMenuItem.Text = "Add New Bank Account"
        '
        'ViewPaySheetToolStripMenuItem
        '
        Me.ViewPaySheetToolStripMenuItem.Name = "ViewPaySheetToolStripMenuItem"
        Me.ViewPaySheetToolStripMenuItem.Size = New System.Drawing.Size(337, 26)
        Me.ViewPaySheetToolStripMenuItem.Text = "View Pay Sheet"
        '
        'VacationManagementToolStripMenuItem
        '
        Me.VacationManagementToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ChkVReq, Me.ToolStripSeparator2, Me.ChkVSupApprove, Me.ChkRSupApprove, Me.ToolStripSeparator3, Me.HumanResourcesToolStripMenuItem})
        Me.VacationManagementToolStripMenuItem.Name = "VacationManagementToolStripMenuItem"
        Me.VacationManagementToolStripMenuItem.Size = New System.Drawing.Size(178, 25)
        Me.VacationManagementToolStripMenuItem.Text = "Vacation Management"
        '
        'ChkVReq
        '
        Me.ChkVReq.Name = "ChkVReq"
        Me.ChkVReq.Size = New System.Drawing.Size(467, 26)
        Me.ChkVReq.Text = "Send""Staff Leave and Duty Resuming Application Form"""
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(464, 6)
        '
        'ChkVSupApprove
        '
        Me.ChkVSupApprove.Name = "ChkVSupApprove"
        Me.ChkVSupApprove.Size = New System.Drawing.Size(467, 26)
        Me.ChkVSupApprove.Text = "Supervisor""Staff Leave  Application"" Approval"
        '
        'ChkRSupApprove
        '
        Me.ChkRSupApprove.Name = "ChkRSupApprove"
        Me.ChkRSupApprove.Size = New System.Drawing.Size(467, 26)
        Me.ChkRSupApprove.Text = "Supervisor""Staff Resuming Duty  Application"" Approval"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(464, 6)
        '
        'HumanResourcesToolStripMenuItem
        '
        Me.HumanResourcesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ChkHrApprove, Me.ChkHrRapprove, Me.ToolStripSeparator4, Me.ChkLeaveArchive, Me.ChkDutyArchive, Me.ToolStripSeparator5, Me.ChkLP})
        Me.HumanResourcesToolStripMenuItem.Name = "HumanResourcesToolStripMenuItem"
        Me.HumanResourcesToolStripMenuItem.Size = New System.Drawing.Size(467, 26)
        Me.HumanResourcesToolStripMenuItem.Text = "Human Resources"
        '
        'ChkHrApprove
        '
        Me.ChkHrApprove.Name = "ChkHrApprove"
        Me.ChkHrApprove.Size = New System.Drawing.Size(385, 26)
        Me.ChkHrApprove.Text = "HR Approval - Staff Leave Application Form"
        '
        'ChkHrRapprove
        '
        Me.ChkHrRapprove.Name = "ChkHrRapprove"
        Me.ChkHrRapprove.Size = New System.Drawing.Size(385, 26)
        Me.ChkHrRapprove.Text = "HR Approval - Duty Resuming Application"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(382, 6)
        '
        'ChkLeaveArchive
        '
        Me.ChkLeaveArchive.Name = "ChkLeaveArchive"
        Me.ChkLeaveArchive.Size = New System.Drawing.Size(385, 26)
        Me.ChkLeaveArchive.Text = """Staff Leave Application Forms"" Archive"
        '
        'ChkDutyArchive
        '
        Me.ChkDutyArchive.Name = "ChkDutyArchive"
        Me.ChkDutyArchive.Size = New System.Drawing.Size(385, 26)
        Me.ChkDutyArchive.Text = """Duty Resuming Application Forms"" Archive"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(382, 6)
        '
        'ChkLP
        '
        Me.ChkLP.Name = "ChkLP"
        Me.ChkLP.Size = New System.Drawing.Size(385, 26)
        Me.ChkLP.Text = "Staff Leave Plan"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ChkSp, Me.ChkCont, Me.ChkNewJob, Me.ChkSA, Me.ChkPaySht, Me.VacationManagementToolStripMenuItem, Me.ChkHr, Me.ChangePasswordToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.MenuStrip1.Size = New System.Drawing.Size(1028, 29)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ChkHr
        '
        Me.ChkHr.Name = "ChkHr"
        Me.ChkHr.Size = New System.Drawing.Size(123, 25)
        Me.ChkHr.Text = "HR Authorities"
        '
        'frmMainHR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 457)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMainHR"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Kernel Investment company - HR"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents ChkSp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewProfileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UpdateProfileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintIDCardToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents StaffListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChangePasswordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChkCont As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChkNewJob As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChkSA As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChkPaySht As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VacationManagementToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChkVReq As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ChkVSupApprove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChkRSupApprove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HumanResourcesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChkHrApprove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChkHrRapprove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ChkLeaveArchive As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChkDutyArchive As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ChkLP As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents AddGradeLevelAllowanceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SetEmployeeSalaryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewNationalInsuranceContributionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StaffAppraisalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChkSAA As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddNewBankAccountToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewPaySheetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChkHr As System.Windows.Forms.ToolStripMenuItem
End Class
