<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmClientEdit
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmClientEdit))
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtPhrDrMob = New System.Windows.Forms.TextBox()
        Me.txtPharOwnMob = New System.Windows.Forms.TextBox()
        Me.CombSalesMan = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtPharmacyDoctor = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtPharmacyOwner = New System.Windows.Forms.TextBox()
        Me.CombMedRepresentative = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.CombAreaName = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.CombRegion = New System.Windows.Forms.ComboBox()
        Me.combState = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtCity = New System.Windows.Forms.TextBox()
        Me.txtTown = New System.Windows.Forms.TextBox()
        Me.txtDistrict = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtStreet = New System.Windows.Forms.TextBox()
        Me.txtBuildingNo = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnNar = New System.Windows.Forms.Button()
        Me.CombClientClass = New System.Windows.Forms.ComboBox()
        Me.txtMobile = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTaxNo = New System.Windows.Forms.TextBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.txtLicNo = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.ErrProv = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.ErrProv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(669, 363)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 32)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(556, 363)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 32)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Update"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Location = New System.Drawing.Point(8, 343)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(736, 8)
        Me.GroupBox4.TabIndex = 45
        Me.GroupBox4.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label17)
        Me.GroupBox3.Controls.Add(Me.Label18)
        Me.GroupBox3.Controls.Add(Me.txtPhrDrMob)
        Me.GroupBox3.Controls.Add(Me.txtPharOwnMob)
        Me.GroupBox3.Controls.Add(Me.CombSalesMan)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.txtPharmacyDoctor)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.txtPharmacyOwner)
        Me.GroupBox3.Controls.Add(Me.CombMedRepresentative)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 227)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(736, 104)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(377, 76)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(122, 13)
        Me.Label17.TabIndex = 18
        Me.Label17.Text = "Pharmacy Owner Mobile"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(4, 76)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(122, 13)
        Me.Label18.TabIndex = 19
        Me.Label18.Text = "Pharmacy Doctor Mobile"
        '
        'txtPhrDrMob
        '
        Me.txtPhrDrMob.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPhrDrMob.Location = New System.Drawing.Point(132, 72)
        Me.txtPhrDrMob.Name = "txtPhrDrMob"
        Me.txtPhrDrMob.Size = New System.Drawing.Size(237, 20)
        Me.txtPhrDrMob.TabIndex = 5
        '
        'txtPharOwnMob
        '
        Me.txtPharOwnMob.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPharOwnMob.Location = New System.Drawing.Point(505, 76)
        Me.txtPharOwnMob.Name = "txtPharOwnMob"
        Me.txtPharOwnMob.Size = New System.Drawing.Size(220, 20)
        Me.txtPharOwnMob.TabIndex = 3
        '
        'CombSalesMan
        '
        Me.CombSalesMan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CombSalesMan.FormattingEnabled = True
        Me.CombSalesMan.Location = New System.Drawing.Point(133, 19)
        Me.CombSalesMan.Name = "CombSalesMan"
        Me.CombSalesMan.Size = New System.Drawing.Size(236, 21)
        Me.CombSalesMan.TabIndex = 0
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(380, 50)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(119, 13)
        Me.Label15.TabIndex = 14
        Me.Label15.Text = "Pharmacy Owner Name"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(38, 50)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(89, 13)
        Me.Label16.TabIndex = 15
        Me.Label16.Text = "Pharmacy Doctor"
        '
        'txtPharmacyDoctor
        '
        Me.txtPharmacyDoctor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPharmacyDoctor.Location = New System.Drawing.Point(133, 46)
        Me.txtPharmacyDoctor.Name = "txtPharmacyDoctor"
        Me.txtPharmacyDoctor.Size = New System.Drawing.Size(236, 20)
        Me.txtPharmacyDoctor.TabIndex = 4
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(380, 23)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(119, 13)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "Medical Representative"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(71, 23)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(55, 13)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Sales Man"
        '
        'txtPharmacyOwner
        '
        Me.txtPharmacyOwner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPharmacyOwner.Location = New System.Drawing.Point(505, 50)
        Me.txtPharmacyOwner.Name = "txtPharmacyOwner"
        Me.txtPharmacyOwner.Size = New System.Drawing.Size(221, 20)
        Me.txtPharmacyOwner.TabIndex = 2
        '
        'CombMedRepresentative
        '
        Me.CombMedRepresentative.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CombMedRepresentative.FormattingEnabled = True
        Me.CombMedRepresentative.Location = New System.Drawing.Point(505, 19)
        Me.CombMedRepresentative.Name = "CombMedRepresentative"
        Me.CombMedRepresentative.Size = New System.Drawing.Size(220, 21)
        Me.CombMedRepresentative.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.CombAreaName)
        Me.GroupBox2.Controls.Add(Me.Button1)
        Me.GroupBox2.Controls.Add(Me.CombRegion)
        Me.GroupBox2.Controls.Add(Me.combState)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.TxtCity)
        Me.GroupBox2.Controls.Add(Me.txtTown)
        Me.GroupBox2.Controls.Add(Me.txtDistrict)
        Me.GroupBox2.Controls.Add(Me.Label19)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtStreet)
        Me.GroupBox2.Controls.Add(Me.txtBuildingNo)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 116)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(736, 105)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Address"
        '
        'CombAreaName
        '
        Me.CombAreaName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CombAreaName.FormattingEnabled = True
        Me.CombAreaName.Location = New System.Drawing.Point(504, 45)
        Me.CombAreaName.Name = "CombAreaName"
        Me.CombAreaName.Size = New System.Drawing.Size(219, 21)
        Me.CombAreaName.TabIndex = 6
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(385, 16)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(35, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "+"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'CombRegion
        '
        Me.CombRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CombRegion.FormattingEnabled = True
        Me.CombRegion.Location = New System.Drawing.Point(505, 18)
        Me.CombRegion.Name = "CombRegion"
        Me.CombRegion.Size = New System.Drawing.Size(218, 21)
        Me.CombRegion.TabIndex = 2
        '
        'combState
        '
        Me.combState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.combState.FormattingEnabled = True
        Me.combState.Location = New System.Drawing.Point(82, 17)
        Me.combState.Name = "combState"
        Me.combState.Size = New System.Drawing.Size(295, 21)
        Me.combState.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(219, 49)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(33, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Town"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(460, 21)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(40, 13)
        Me.Label12.TabIndex = 11
        Me.Label12.Text = "Region"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(52, 49)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(26, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "City"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(44, 21)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(33, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "State"
        '
        'TxtCity
        '
        Me.TxtCity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtCity.Location = New System.Drawing.Point(82, 45)
        Me.TxtCity.Name = "TxtCity"
        Me.TxtCity.Size = New System.Drawing.Size(121, 20)
        Me.TxtCity.TabIndex = 3
        '
        'txtTown
        '
        Me.txtTown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTown.Location = New System.Drawing.Point(256, 45)
        Me.txtTown.Name = "txtTown"
        Me.txtTown.Size = New System.Drawing.Size(121, 20)
        Me.txtTown.TabIndex = 4
        '
        'txtDistrict
        '
        Me.txtDistrict.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDistrict.Location = New System.Drawing.Point(504, 70)
        Me.txtDistrict.Name = "txtDistrict"
        Me.txtDistrict.Size = New System.Drawing.Size(121, 20)
        Me.txtDistrict.TabIndex = 5
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(463, 47)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(30, 13)
        Me.Label19.TabIndex = 13
        Me.Label19.Text = "Area"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(41, 76)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(37, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Street"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(460, 74)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(40, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "District"
        '
        'txtStreet
        '
        Me.txtStreet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStreet.Location = New System.Drawing.Point(82, 72)
        Me.txtStreet.Name = "txtStreet"
        Me.txtStreet.Size = New System.Drawing.Size(121, 20)
        Me.txtStreet.TabIndex = 6
        '
        'txtBuildingNo
        '
        Me.txtBuildingNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBuildingNo.Location = New System.Drawing.Point(256, 72)
        Me.txtBuildingNo.Name = "txtBuildingNo"
        Me.txtBuildingNo.Size = New System.Drawing.Size(121, 20)
        Me.txtBuildingNo.TabIndex = 7
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(208, 70)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(44, 26)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Building" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Number"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnNar)
        Me.GroupBox1.Controls.Add(Me.CombClientClass)
        Me.GroupBox1.Controls.Add(Me.txtMobile)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtTaxNo)
        Me.GroupBox1.Controls.Add(Me.txtName)
        Me.GroupBox1.Controls.Add(Me.txtLicNo)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(736, 105)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "New Client"
        '
        'btnNar
        '
        Me.btnNar.Location = New System.Drawing.Point(383, 69)
        Me.btnNar.Name = "btnNar"
        Me.btnNar.Size = New System.Drawing.Size(35, 23)
        Me.btnNar.TabIndex = 5
        Me.btnNar.Text = "+"
        Me.btnNar.UseVisualStyleBackColor = True
        '
        'CombClientClass
        '
        Me.CombClientClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CombClientClass.FormattingEnabled = True
        Me.CombClientClass.Location = New System.Drawing.Point(132, 70)
        Me.CombClientClass.Name = "CombClientClass"
        Me.CombClientClass.Size = New System.Drawing.Size(245, 21)
        Me.CombClientClass.TabIndex = 4
        '
        'txtMobile
        '
        Me.txtMobile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMobile.Location = New System.Drawing.Point(504, 44)
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.Size = New System.Drawing.Size(131, 20)
        Me.txtMobile.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(64, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Client Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(418, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(82, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "License Number"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(420, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Mobile  Number"
        '
        'txtTaxNo
        '
        Me.txtTaxNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTaxNo.Location = New System.Drawing.Point(132, 44)
        Me.txtTaxNo.Name = "txtTaxNo"
        Me.txtTaxNo.Size = New System.Drawing.Size(118, 20)
        Me.txtTaxNo.TabIndex = 2
        '
        'txtName
        '
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Location = New System.Drawing.Point(132, 19)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(245, 20)
        Me.txtName.TabIndex = 1
        '
        'txtLicNo
        '
        Me.txtLicNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLicNo.Location = New System.Drawing.Point(504, 17)
        Me.txtLicNo.Name = "txtLicNo"
        Me.txtLicNo.Size = New System.Drawing.Size(131, 20)
        Me.txtLicNo.TabIndex = 1
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(60, 48)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(68, 13)
        Me.Label13.TabIndex = 12
        Me.Label13.Text = "Tax  Number"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(29, 74)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(99, 13)
        Me.Label14.TabIndex = 13
        Me.Label14.Text = "Client Classification"
        '
        'ErrProv
        '
        Me.ErrProv.ContainerControl = Me
        '
        'frmClientEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(753, 411)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(769, 449)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(769, 449)
        Me.Name = "frmClientEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Edit Client Profile"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.ErrProv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtPhrDrMob As System.Windows.Forms.TextBox
    Friend WithEvents txtPharOwnMob As System.Windows.Forms.TextBox
    Friend WithEvents CombSalesMan As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtPharmacyDoctor As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtPharmacyOwner As System.Windows.Forms.TextBox
    Friend WithEvents CombMedRepresentative As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents CombRegion As System.Windows.Forms.ComboBox
    Friend WithEvents combState As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtCity As System.Windows.Forms.TextBox
    Friend WithEvents txtTown As System.Windows.Forms.TextBox
    Friend WithEvents txtDistrict As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtStreet As System.Windows.Forms.TextBox
    Friend WithEvents txtBuildingNo As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents CombAreaName As System.Windows.Forms.ComboBox
    Friend WithEvents btnNar As System.Windows.Forms.Button
    Friend WithEvents CombClientClass As System.Windows.Forms.ComboBox
    Friend WithEvents txtMobile As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTaxNo As System.Windows.Forms.TextBox
    Friend WithEvents txtLicNo As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents ErrProv As System.Windows.Forms.ErrorProvider
    Friend WithEvents txtName As System.Windows.Forms.TextBox
End Class
