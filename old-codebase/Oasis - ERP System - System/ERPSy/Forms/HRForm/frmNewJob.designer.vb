<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewJob
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNewJob))
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.ChkPartTime = New System.Windows.Forms.CheckBox
        Me.ChkTemporary = New System.Windows.Forms.CheckBox
        Me.ChkPermanent = New System.Windows.Forms.CheckBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.ComboReplace = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtPreReq = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtTotalSal = New System.Windows.Forms.TextBox
        Me.txtQualification = New System.Windows.Forms.TextBox
        Me.txtMaxAge = New System.Windows.Forms.TextBox
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtMinAge = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Button3 = New System.Windows.Forms.Button
        Me.Label17 = New System.Windows.Forms.Label
        Me.RadLabor = New System.Windows.Forms.RadioButton
        Me.RadEmployee = New System.Windows.Forms.RadioButton
        Me.ComboLevel = New System.Windows.Forms.ComboBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.ComboJobDescribtion = New System.Windows.Forms.ComboBox
        Me.ComboDepartment = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.Button4 = New System.Windows.Forms.Button
        Me.ListArchive1 = New System.Windows.Forms.ListView
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.Button2 = New System.Windows.Forms.Button
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.DateTimeTo = New System.Windows.Forms.DateTimePicker
        Me.DateTimeFrom = New System.Windows.Forms.DateTimePicker
        Me.Button32 = New System.Windows.Forms.Button
        Me.Button31 = New System.Windows.Forms.Button
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(5, 4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(514, 408)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(506, 382)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "New"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ChkPartTime)
        Me.GroupBox2.Controls.Add(Me.ChkTemporary)
        Me.GroupBox2.Controls.Add(Me.ChkPermanent)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.ComboReplace)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.txtPreReq)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtTotalSal)
        Me.GroupBox2.Controls.Add(Me.txtQualification)
        Me.GroupBox2.Controls.Add(Me.txtMaxAge)
        Me.GroupBox2.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtMinAge)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 113)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(492, 263)
        Me.GroupBox2.TabIndex = 124
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Requirements"
        '
        'ChkPartTime
        '
        Me.ChkPartTime.AutoSize = True
        Me.ChkPartTime.Location = New System.Drawing.Point(263, 169)
        Me.ChkPartTime.Name = "ChkPartTime"
        Me.ChkPartTime.Size = New System.Drawing.Size(71, 17)
        Me.ChkPartTime.TabIndex = 157
        Me.ChkPartTime.Text = "Part Time"
        Me.ChkPartTime.UseVisualStyleBackColor = True
        '
        'ChkTemporary
        '
        Me.ChkTemporary.AutoSize = True
        Me.ChkTemporary.Location = New System.Drawing.Point(182, 168)
        Me.ChkTemporary.Name = "ChkTemporary"
        Me.ChkTemporary.Size = New System.Drawing.Size(78, 17)
        Me.ChkTemporary.TabIndex = 156
        Me.ChkTemporary.Text = "Temporary"
        Me.ChkTemporary.UseVisualStyleBackColor = True
        '
        'ChkPermanent
        '
        Me.ChkPermanent.AutoSize = True
        Me.ChkPermanent.Location = New System.Drawing.Point(98, 168)
        Me.ChkPermanent.Name = "ChkPermanent"
        Me.ChkPermanent.Size = New System.Drawing.Size(78, 17)
        Me.ChkPermanent.TabIndex = 155
        Me.ChkPermanent.Text = "Permanent"
        Me.ChkPermanent.UseVisualStyleBackColor = True
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(8, 169)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(89, 13)
        Me.Label16.TabIndex = 154
        Me.Label16.Text = "Contract Period :"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(198, 201)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(34, 13)
        Me.Label13.TabIndex = 152
        Me.Label13.Text = "Years"
        '
        'ComboReplace
        '
        Me.ComboReplace.FormattingEnabled = True
        Me.ComboReplace.Items.AddRange(New Object() {"No"})
        Me.ComboReplace.Location = New System.Drawing.Point(10, 133)
        Me.ComboReplace.Name = "ComboReplace"
        Me.ComboReplace.Size = New System.Drawing.Size(217, 21)
        Me.ComboReplace.TabIndex = 153
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(198, 228)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(34, 13)
        Me.Label12.TabIndex = 151
        Me.Label12.Text = "Years"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(260, 204)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(74, 13)
        Me.Label5.TabIndex = 137
        Me.Label5.Text = "Gross Salary :"
        '
        'txtPreReq
        '
        Me.txtPreReq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPreReq.Location = New System.Drawing.Point(12, 39)
        Me.txtPreReq.Multiline = True
        Me.txtPreReq.Name = "txtPreReq"
        Me.txtPreReq.Size = New System.Drawing.Size(215, 71)
        Me.txtPreReq.TabIndex = 149
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(17, 197)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 13)
        Me.Label9.TabIndex = 141
        Me.Label9.Text = "Maximum Age :"
        '
        'txtTotalSal
        '
        Me.txtTotalSal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalSal.Location = New System.Drawing.Point(263, 221)
        Me.txtTotalSal.Name = "txtTotalSal"
        Me.txtTotalSal.Size = New System.Drawing.Size(90, 20)
        Me.txtTotalSal.TabIndex = 144
        '
        'txtQualification
        '
        Me.txtQualification.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQualification.Location = New System.Drawing.Point(260, 39)
        Me.txtQualification.Multiline = True
        Me.txtQualification.Name = "txtQualification"
        Me.txtQualification.Size = New System.Drawing.Size(218, 71)
        Me.txtQualification.TabIndex = 145
        '
        'txtMaxAge
        '
        Me.txtMaxAge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMaxAge.Location = New System.Drawing.Point(97, 197)
        Me.txtMaxAge.Name = "txtMaxAge"
        Me.txtMaxAge.Size = New System.Drawing.Size(95, 20)
        Me.txtMaxAge.TabIndex = 147
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(260, 133)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(215, 20)
        Me.DateTimePicker1.TabIndex = 150
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(7, 22)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(67, 13)
        Me.Label11.TabIndex = 143
        Me.Label11.Text = "Experience :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(21, 228)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(76, 13)
        Me.Label8.TabIndex = 140
        Me.Label8.Text = "Minimum Age :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 117)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 13)
        Me.Label6.TabIndex = 138
        Me.Label6.Text = "Replace :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(257, 22)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(78, 13)
        Me.Label7.TabIndex = 139
        Me.Label7.Text = "Qualifications :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(260, 117)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 13)
        Me.Label3.TabIndex = 136
        Me.Label3.Text = "Start Date :"
        '
        'txtMinAge
        '
        Me.txtMinAge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMinAge.Location = New System.Drawing.Point(97, 224)
        Me.txtMinAge.Name = "txtMinAge"
        Me.txtMinAge.Size = New System.Drawing.Size(95, 20)
        Me.txtMinAge.TabIndex = 146
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.RadLabor)
        Me.GroupBox1.Controls.Add(Me.RadEmployee)
        Me.GroupBox1.Controls.Add(Me.ComboLevel)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.ComboJobDescribtion)
        Me.GroupBox1.Controls.Add(Me.ComboDepartment)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(492, 107)
        Me.GroupBox1.TabIndex = 121
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Job Details"
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.Location = New System.Drawing.Point(286, 44)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(35, 23)
        Me.Button3.TabIndex = 141
        Me.Button3.Text = "+"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(58, 19)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(35, 13)
        Me.Label17.TabIndex = 140
        Me.Label17.Text = "New :"
        '
        'RadLabor
        '
        Me.RadLabor.AutoSize = True
        Me.RadLabor.Location = New System.Drawing.Point(179, 15)
        Me.RadLabor.Name = "RadLabor"
        Me.RadLabor.Size = New System.Drawing.Size(52, 17)
        Me.RadLabor.TabIndex = 139
        Me.RadLabor.TabStop = True
        Me.RadLabor.Text = "Labor"
        Me.RadLabor.UseVisualStyleBackColor = True
        '
        'RadEmployee
        '
        Me.RadEmployee.AutoSize = True
        Me.RadEmployee.Location = New System.Drawing.Point(95, 15)
        Me.RadEmployee.Name = "RadEmployee"
        Me.RadEmployee.Size = New System.Drawing.Size(71, 17)
        Me.RadEmployee.TabIndex = 138
        Me.RadEmployee.TabStop = True
        Me.RadEmployee.Text = "Employee"
        Me.RadEmployee.UseVisualStyleBackColor = True
        '
        'ComboLevel
        '
        Me.ComboLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboLevel.FormattingEnabled = True
        Me.ComboLevel.Location = New System.Drawing.Point(386, 44)
        Me.ComboLevel.Name = "ComboLevel"
        Me.ComboLevel.Size = New System.Drawing.Size(99, 21)
        Me.ComboLevel.TabIndex = 137
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(327, 74)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(35, 23)
        Me.Button1.TabIndex = 120
        Me.Button1.Text = "+"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ComboJobDescribtion
        '
        Me.ComboJobDescribtion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboJobDescribtion.FormattingEnabled = True
        Me.ComboJobDescribtion.Location = New System.Drawing.Point(95, 74)
        Me.ComboJobDescribtion.Name = "ComboJobDescribtion"
        Me.ComboJobDescribtion.Size = New System.Drawing.Size(226, 21)
        Me.ComboJobDescribtion.TabIndex = 136
        '
        'ComboDepartment
        '
        Me.ComboDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboDepartment.FormattingEnabled = True
        Me.ComboDepartment.Location = New System.Drawing.Point(94, 45)
        Me.ComboDepartment.Name = "ComboDepartment"
        Me.ComboDepartment.Size = New System.Drawing.Size(186, 21)
        Me.ComboDepartment.TabIndex = 130
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 77)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(87, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Job Description :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(340, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Grade :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Department :"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Button4)
        Me.TabPage2.Controls.Add(Me.ListArchive1)
        Me.TabPage2.Controls.Add(Me.Button2)
        Me.TabPage2.Controls.Add(Me.Label14)
        Me.TabPage2.Controls.Add(Me.Label15)
        Me.TabPage2.Controls.Add(Me.DateTimeTo)
        Me.TabPage2.Controls.Add(Me.DateTimeFrom)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(506, 382)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Archive"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.BackColor = System.Drawing.Color.White
        Me.Button4.Location = New System.Drawing.Point(397, 332)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(64, 29)
        Me.Button4.TabIndex = 130
        Me.Button4.Text = "Delete"
        Me.Button4.UseVisualStyleBackColor = False
        '
        'ListArchive1
        '
        Me.ListArchive1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader4, Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.ListArchive1.FullRowSelect = True
        Me.ListArchive1.Location = New System.Drawing.Point(40, 49)
        Me.ListArchive1.Name = "ListArchive1"
        Me.ListArchive1.Size = New System.Drawing.Size(421, 277)
        Me.ListArchive1.TabIndex = 129
        Me.ListArchive1.UseCompatibleStateImageBehavior = False
        Me.ListArchive1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "SNO"
        Me.ColumnHeader4.Width = 0
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Department"
        Me.ColumnHeader1.Width = 150
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Job Describtion"
        Me.ColumnHeader2.Width = 150
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Start Date"
        Me.ColumnHeader3.Width = 100
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(40, 332)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(64, 29)
        Me.Button2.TabIndex = 127
        Me.Button2.Text = "Print"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(266, 18)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(26, 13)
        Me.Label14.TabIndex = 126
        Me.Label14.Text = "To :"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(11, 19)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(38, 13)
        Me.Label15.TabIndex = 128
        Me.Label15.Text = "From :"
        '
        'DateTimeTo
        '
        Me.DateTimeTo.Location = New System.Drawing.Point(298, 11)
        Me.DateTimeTo.Name = "DateTimeTo"
        Me.DateTimeTo.Size = New System.Drawing.Size(200, 20)
        Me.DateTimeTo.TabIndex = 125
        '
        'DateTimeFrom
        '
        Me.DateTimeFrom.Location = New System.Drawing.Point(53, 12)
        Me.DateTimeFrom.Name = "DateTimeFrom"
        Me.DateTimeFrom.Size = New System.Drawing.Size(200, 20)
        Me.DateTimeFrom.TabIndex = 124
        '
        'Button32
        '
        Me.Button32.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button32.Location = New System.Drawing.Point(430, 418)
        Me.Button32.Name = "Button32"
        Me.Button32.Size = New System.Drawing.Size(85, 32)
        Me.Button32.TabIndex = 123
        Me.Button32.Text = "Close"
        Me.Button32.UseVisualStyleBackColor = True
        '
        'Button31
        '
        Me.Button31.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button31.Location = New System.Drawing.Point(302, 418)
        Me.Button31.Name = "Button31"
        Me.Button31.Size = New System.Drawing.Size(85, 32)
        Me.Button31.TabIndex = 122
        Me.Button31.Text = "Save"
        Me.Button31.UseVisualStyleBackColor = True
        '
        'frmNewJob
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(520, 455)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Button32)
        Me.Controls.Add(Me.Button31)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmNewJob"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "New Job"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents ComboReplace As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtPreReq As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtTotalSal As System.Windows.Forms.TextBox
    Friend WithEvents txtQualification As System.Windows.Forms.TextBox
    Friend WithEvents txtMaxAge As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtMinAge As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ComboLevel As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ComboJobDescribtion As System.Windows.Forms.ComboBox
    Friend WithEvents ComboDepartment As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Button32 As System.Windows.Forms.Button
    Friend WithEvents Button31 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents ListArchive1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents DateTimeTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimeFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents ChkPartTime As System.Windows.Forms.CheckBox
    Friend WithEvents ChkTemporary As System.Windows.Forms.CheckBox
    Friend WithEvents ChkPermanent As System.Windows.Forms.CheckBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents RadLabor As System.Windows.Forms.RadioButton
    Friend WithEvents RadEmployee As System.Windows.Forms.RadioButton
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
End Class
