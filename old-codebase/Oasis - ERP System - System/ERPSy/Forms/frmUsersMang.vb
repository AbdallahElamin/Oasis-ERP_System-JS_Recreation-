Imports System.Data.SqlClient

Public Class frmUsersMang
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CombUser As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSavePrivi As System.Windows.Forms.Button
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtFullName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNewPass As System.Windows.Forms.TextBox
    Friend WithEvents btnCloasChangPas As System.Windows.Forms.Button
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSaveCHang As System.Windows.Forms.Button
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents CombUserII As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents CHJ As System.Windows.Forms.CheckBox
    Friend WithEvents ChI As System.Windows.Forms.CheckBox
    Friend WithEvents ChH As System.Windows.Forms.CheckBox
    Friend WithEvents ChG As System.Windows.Forms.CheckBox
    Friend WithEvents ChF As System.Windows.Forms.CheckBox
    Friend WithEvents ChE As System.Windows.Forms.CheckBox
    Friend WithEvents ChD As System.Windows.Forms.CheckBox
    Friend WithEvents ChC As System.Windows.Forms.CheckBox
    Friend WithEvents ChB As System.Windows.Forms.CheckBox
    Friend WithEvents ChA As System.Windows.Forms.CheckBox
    Friend WithEvents btnUnCheckall As System.Windows.Forms.Button
    Friend WithEvents btnCheckall As System.Windows.Forms.Button
    Friend WithEvents btnClosePrivi As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUsersMang))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CombUser = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnSavePrivi = New System.Windows.Forms.Button()
        Me.btnClosePrivi = New System.Windows.Forms.Button()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtFullName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnUnCheckall = New System.Windows.Forms.Button()
        Me.btnCheckall = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.CHJ = New System.Windows.Forms.CheckBox()
        Me.ChI = New System.Windows.Forms.CheckBox()
        Me.ChH = New System.Windows.Forms.CheckBox()
        Me.ChG = New System.Windows.Forms.CheckBox()
        Me.ChF = New System.Windows.Forms.CheckBox()
        Me.ChE = New System.Windows.Forms.CheckBox()
        Me.ChD = New System.Windows.Forms.CheckBox()
        Me.ChC = New System.Windows.Forms.CheckBox()
        Me.ChB = New System.Windows.Forms.CheckBox()
        Me.ChA = New System.Windows.Forms.CheckBox()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtNewPass = New System.Windows.Forms.TextBox()
        Me.btnCloasChangPas = New System.Windows.Forms.Button()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.btnSaveCHang = New System.Windows.Forms.Button()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CombUserII = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "User"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CombUser
        '
        Me.CombUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CombUser.DropDownWidth = 140
        Me.CombUser.Location = New System.Drawing.Point(49, 14)
        Me.CombUser.Name = "CombUser"
        Me.CombUser.Size = New System.Drawing.Size(229, 21)
        Me.CombUser.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.CombUser)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(620, 43)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(8, 278)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(620, 4)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        '
        'btnSavePrivi
        '
        Me.btnSavePrivi.Location = New System.Drawing.Point(448, 288)
        Me.btnSavePrivi.Name = "btnSavePrivi"
        Me.btnSavePrivi.Size = New System.Drawing.Size(75, 32)
        Me.btnSavePrivi.TabIndex = 2
        Me.btnSavePrivi.Text = "Save"
        '
        'btnClosePrivi
        '
        Me.btnClosePrivi.Location = New System.Drawing.Point(553, 288)
        Me.btnClosePrivi.Name = "btnClosePrivi"
        Me.btnClosePrivi.Size = New System.Drawing.Size(75, 32)
        Me.btnClosePrivi.TabIndex = 3
        Me.btnClosePrivi.Text = "Close"
        '
        'TabControl2
        '
        Me.TabControl2.Controls.Add(Me.TabPage3)
        Me.TabControl2.Controls.Add(Me.TabPage4)
        Me.TabControl2.Controls.Add(Me.TabPage1)
        Me.TabControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl2.Location = New System.Drawing.Point(0, 0)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(644, 349)
        Me.TabControl2.TabIndex = 61
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.ListView1)
        Me.TabPage3.Controls.Add(Me.btnRemove)
        Me.TabPage3.Controls.Add(Me.GroupBox4)
        Me.TabPage3.Controls.Add(Me.GroupBox5)
        Me.TabPage3.Controls.Add(Me.btnSave)
        Me.TabPage3.Controls.Add(Me.btnClose)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(636, 323)
        Me.TabPage3.TabIndex = 0
        Me.TabPage3.Text = "Add/Remove Users"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2, Me.ColumnHeader1})
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(319, 14)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.RightToLeftLayout = True
        Me.ListView1.Size = New System.Drawing.Size(310, 275)
        Me.ListView1.TabIndex = 61
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "User ID"
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Full Name"
        Me.ColumnHeader1.Width = 235
        '
        'btnRemove
        '
        Me.btnRemove.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnRemove.Location = New System.Drawing.Point(554, 295)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(75, 22)
        Me.btnRemove.TabIndex = 60
        Me.btnRemove.Text = "Remove"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.txtFullName)
        Me.GroupBox4.Controls.Add(Me.Label2)
        Me.GroupBox4.Controls.Add(Me.txtPassword)
        Me.GroupBox4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(8, 6)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(305, 79)
        Me.GroupBox4.TabIndex = 0
        Me.GroupBox4.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(7, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Full Name:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtFullName
        '
        Me.txtFullName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFullName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFullName.Location = New System.Drawing.Point(70, 17)
        Me.txtFullName.Name = "txtFullName"
        Me.txtFullName.Size = New System.Drawing.Size(224, 21)
        Me.txtFullName.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(7, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Password:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtPassword
        '
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPassword.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.Location = New System.Drawing.Point(70, 47)
        Me.txtPassword.MaxLength = 8
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(112, 21)
        Me.txtPassword.TabIndex = 1
        '
        'GroupBox5
        '
        Me.GroupBox5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.Location = New System.Drawing.Point(8, 91)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(305, 4)
        Me.GroupBox5.TabIndex = 59
        Me.GroupBox5.TabStop = False
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSave.Location = New System.Drawing.Point(122, 101)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 32)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnClose.Location = New System.Drawing.Point(227, 101)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 32)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.GroupBox3)
        Me.TabPage4.Controls.Add(Me.GroupBox1)
        Me.TabPage4.Controls.Add(Me.btnClosePrivi)
        Me.TabPage4.Controls.Add(Me.GroupBox2)
        Me.TabPage4.Controls.Add(Me.btnSavePrivi)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(636, 323)
        Me.TabPage4.TabIndex = 1
        Me.TabPage4.Text = "Privileges"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnUnCheckall)
        Me.GroupBox3.Controls.Add(Me.btnCheckall)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.CHJ)
        Me.GroupBox3.Controls.Add(Me.ChI)
        Me.GroupBox3.Controls.Add(Me.ChH)
        Me.GroupBox3.Controls.Add(Me.ChG)
        Me.GroupBox3.Controls.Add(Me.ChF)
        Me.GroupBox3.Controls.Add(Me.ChE)
        Me.GroupBox3.Controls.Add(Me.ChD)
        Me.GroupBox3.Controls.Add(Me.ChC)
        Me.GroupBox3.Controls.Add(Me.ChB)
        Me.GroupBox3.Controls.Add(Me.ChA)
        Me.GroupBox3.Location = New System.Drawing.Point(10, 55)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(620, 217)
        Me.GroupBox3.TabIndex = 8
        Me.GroupBox3.TabStop = False
        '
        'btnUnCheckall
        '
        Me.btnUnCheckall.Image = CType(resources.GetObject("btnUnCheckall.Image"), System.Drawing.Image)
        Me.btnUnCheckall.Location = New System.Drawing.Point(267, 194)
        Me.btnUnCheckall.Name = "btnUnCheckall"
        Me.btnUnCheckall.Size = New System.Drawing.Size(46, 23)
        Me.btnUnCheckall.TabIndex = 56
        Me.btnUnCheckall.UseVisualStyleBackColor = True
        '
        'btnCheckall
        '
        Me.btnCheckall.Image = CType(resources.GetObject("btnCheckall.Image"), System.Drawing.Image)
        Me.btnCheckall.Location = New System.Drawing.Point(319, 194)
        Me.btnCheckall.Name = "btnCheckall"
        Me.btnCheckall.Size = New System.Drawing.Size(44, 23)
        Me.btnCheckall.TabIndex = 56
        Me.btnCheckall.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(379, 88)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(119, 16)
        Me.Label10.TabIndex = 55
        Me.Label10.Text = "Policies Module"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(379, 11)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(139, 16)
        Me.Label9.TabIndex = 54
        Me.Label9.Text = "Accounting Module"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(67, 88)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(110, 16)
        Me.Label8.TabIndex = 53
        Me.Label8.Text = "Claims Module"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(67, 11)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(110, 16)
        Me.Label7.TabIndex = 52
        Me.Label7.Text = "Clients Module"
        '
        'CHJ
        '
        Me.CHJ.AutoSize = True
        Me.CHJ.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CHJ.Location = New System.Drawing.Point(382, 165)
        Me.CHJ.Name = "CHJ"
        Me.CHJ.Size = New System.Drawing.Size(142, 20)
        Me.CHJ.TabIndex = 47
        Me.CHJ.Text = "Back-Up Module"
        Me.CHJ.UseVisualStyleBackColor = True
        '
        'ChI
        '
        Me.ChI.AutoSize = True
        Me.ChI.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChI.Location = New System.Drawing.Point(70, 165)
        Me.ChI.Name = "ChI"
        Me.ChI.Size = New System.Drawing.Size(220, 20)
        Me.ChI.TabIndex = 51
        Me.ChI.Text = "User Accounts Management"
        Me.ChI.UseVisualStyleBackColor = True
        '
        'ChH
        '
        Me.ChH.AutoSize = True
        Me.ChH.Location = New System.Drawing.Point(490, 116)
        Me.ChH.Name = "ChH"
        Me.ChH.Size = New System.Drawing.Size(64, 17)
        Me.ChH.TabIndex = 49
        Me.ChH.Text = "Reports"
        Me.ChH.UseVisualStyleBackColor = True
        '
        'ChG
        '
        Me.ChG.AutoSize = True
        Me.ChG.Location = New System.Drawing.Point(382, 116)
        Me.ChG.Name = "ChG"
        Me.ChG.Size = New System.Drawing.Size(78, 17)
        Me.ChG.TabIndex = 48
        Me.ChG.Text = "Data Entry"
        Me.ChG.UseVisualStyleBackColor = True
        '
        'ChF
        '
        Me.ChF.AutoSize = True
        Me.ChF.Location = New System.Drawing.Point(178, 116)
        Me.ChF.Name = "ChF"
        Me.ChF.Size = New System.Drawing.Size(64, 17)
        Me.ChF.TabIndex = 50
        Me.ChF.Text = "Reports"
        Me.ChF.UseVisualStyleBackColor = True
        '
        'ChE
        '
        Me.ChE.AutoSize = True
        Me.ChE.Location = New System.Drawing.Point(70, 116)
        Me.ChE.Name = "ChE"
        Me.ChE.Size = New System.Drawing.Size(78, 17)
        Me.ChE.TabIndex = 46
        Me.ChE.Text = "Data Entry"
        Me.ChE.UseVisualStyleBackColor = True
        '
        'ChD
        '
        Me.ChD.AutoSize = True
        Me.ChD.Location = New System.Drawing.Point(490, 39)
        Me.ChD.Name = "ChD"
        Me.ChD.Size = New System.Drawing.Size(64, 17)
        Me.ChD.TabIndex = 45
        Me.ChD.Text = "Reports"
        Me.ChD.UseVisualStyleBackColor = True
        '
        'ChC
        '
        Me.ChC.AutoSize = True
        Me.ChC.Location = New System.Drawing.Point(382, 39)
        Me.ChC.Name = "ChC"
        Me.ChC.Size = New System.Drawing.Size(78, 17)
        Me.ChC.TabIndex = 44
        Me.ChC.Text = "Data Entry"
        Me.ChC.UseVisualStyleBackColor = True
        '
        'ChB
        '
        Me.ChB.AutoSize = True
        Me.ChB.Location = New System.Drawing.Point(178, 39)
        Me.ChB.Name = "ChB"
        Me.ChB.Size = New System.Drawing.Size(64, 17)
        Me.ChB.TabIndex = 43
        Me.ChB.Text = "Reports"
        Me.ChB.UseVisualStyleBackColor = True
        '
        'ChA
        '
        Me.ChA.AutoSize = True
        Me.ChA.Location = New System.Drawing.Point(70, 39)
        Me.ChA.Name = "ChA"
        Me.ChA.Size = New System.Drawing.Size(78, 17)
        Me.ChA.TabIndex = 42
        Me.ChA.Text = "Data Entry"
        Me.ChA.UseVisualStyleBackColor = True
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox8)
        Me.TabPage1.Controls.Add(Me.btnCloasChangPas)
        Me.TabPage1.Controls.Add(Me.GroupBox7)
        Me.TabPage1.Controls.Add(Me.btnSaveCHang)
        Me.TabPage1.Controls.Add(Me.GroupBox6)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(636, 323)
        Me.TabPage1.TabIndex = 2
        Me.TabPage1.Text = "Change Password"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.Label5)
        Me.GroupBox8.Controls.Add(Me.txtNewPass)
        Me.GroupBox8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox8.Location = New System.Drawing.Point(8, 50)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(462, 50)
        Me.GroupBox8.TabIndex = 1
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "New Password"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(14, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(57, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Password:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtNewPass
        '
        Me.txtNewPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewPass.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewPass.Location = New System.Drawing.Point(77, 19)
        Me.txtNewPass.MaxLength = 8
        Me.txtNewPass.Name = "txtNewPass"
        Me.txtNewPass.Size = New System.Drawing.Size(112, 21)
        Me.txtNewPass.TabIndex = 0
        '
        'btnCloasChangPas
        '
        Me.btnCloasChangPas.Location = New System.Drawing.Point(395, 114)
        Me.btnCloasChangPas.Name = "btnCloasChangPas"
        Me.btnCloasChangPas.Size = New System.Drawing.Size(75, 32)
        Me.btnCloasChangPas.TabIndex = 3
        Me.btnCloasChangPas.Text = "Close"
        '
        'GroupBox7
        '
        Me.GroupBox7.Location = New System.Drawing.Point(8, 104)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(462, 4)
        Me.GroupBox7.TabIndex = 10
        Me.GroupBox7.TabStop = False
        '
        'btnSaveCHang
        '
        Me.btnSaveCHang.Location = New System.Drawing.Point(290, 114)
        Me.btnSaveCHang.Name = "btnSaveCHang"
        Me.btnSaveCHang.Size = New System.Drawing.Size(75, 32)
        Me.btnSaveCHang.TabIndex = 2
        Me.btnSaveCHang.Text = "Save"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.Label6)
        Me.GroupBox6.Controls.Add(Me.CombUserII)
        Me.GroupBox6.Location = New System.Drawing.Point(8, 6)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(462, 43)
        Me.GroupBox6.TabIndex = 0
        Me.GroupBox6.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(14, 17)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 13)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "User"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CombUserII
        '
        Me.CombUserII.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CombUserII.DropDownWidth = 140
        Me.CombUserII.Location = New System.Drawing.Point(49, 14)
        Me.CombUserII.Name = "CombUserII"
        Me.CombUserII.Size = New System.Drawing.Size(229, 21)
        Me.CombUserII.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(395, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "«·„” Œœ„ :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ComboBox2
        '
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.DropDownWidth = 140
        Me.ComboBox2.Location = New System.Drawing.Point(162, 14)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(229, 21)
        Me.ComboBox2.TabIndex = 0
        '
        'frmUsersMang
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(644, 349)
        Me.Controls.Add(Me.TabControl2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmUsersMang"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "User Accounts Management"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Sub Clear()
        Me.txtFullName.Clear()
        Me.txtPassword.Clear()
    End Sub

    Sub UnCheckAll()
        Me.ChA.Checked = False
        Me.ChB.Checked = False
        Me.ChC.Checked = False
        Me.ChD.Checked = False
        Me.ChE.Checked = False
        Me.ChF.Checked = False
        Me.ChG.Checked = False
        Me.ChH.Checked = False
        Me.ChI.Checked = False
        Me.CHJ.Checked = False
    End Sub

    Sub CheckAll()
        Me.ChA.Checked = True
        Me.ChB.Checked = True
        Me.ChC.Checked = True
        Me.ChD.Checked = True
        Me.ChE.Checked = True
        Me.ChF.Checked = True
        Me.ChG.Checked = True
        Me.ChH.Checked = True
        Me.ChI.Checked = True
        Me.CHJ.Checked = True
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CombUser.SelectedIndexChanged
        Try
            If Me.CombUser.SelectedIndex = -1 Then
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            Dim cmd As New SqlCommand("Select * From Users where FullName=N'" & Me.CombUser.SelectedItem & "'", cnn)
            Dim Reader As SqlDataReader

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.ChA.Checked = CBool(Reader.Item("ChA"))
                Me.ChB.Checked = CBool(Reader.Item("ChB"))
                Me.ChC.Checked = CBool(Reader.Item("ChC"))
                Me.ChD.Checked = CBool(Reader.Item("ChD"))
                Me.ChE.Checked = CBool(Reader.Item("ChE"))
                Me.ChF.Checked = CBool(Reader.Item("ChF"))
                Me.ChG.Checked = CBool(Reader.Item("ChG"))
                Me.ChH.Checked = CBool(Reader.Item("ChH"))
                Me.ChI.Checked = CBool(Reader.Item("ChI"))
                Me.CHJ.Checked = CBool(Reader.Item("ChJ"))
            End While
            cnn.Close()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Me.txtFullName.Text.Length = 0 Then
            MsgBox("Please enter full name")
            Me.txtPassword.Focus()
        ElseIf Me.txtPassword.Text.Length = 0 Then
            MsgBox("Please enter password")
            Me.txtFullName.Focus()
        Else
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim cmd As New SqlCommand("Insert Into Users (FullName, Pass) Values (N'" & Me.txtFullName.Text.Trim & _
                                          "',N'" & Me.txtPassword.Text & "')", cnn)

                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()

                'RESTORE DEFAULTS
                Clear()
                FillUsers()
                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox(ex.ToString)
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
            End Try
        End If
    End Sub

    Sub FillUsers()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim cmd1 As New SqlCommand("Select SNo,FullName From Users Order By SNo", cnn)
            Dim SqlReader As SqlDataReader

            Me.CombUser.Items.Clear()
            Me.CombUserII.Items.Clear()
            Me.ListView1.Items.Clear()

            cnn.Open()
            SqlReader = cmd1.ExecuteReader
            While SqlReader.Read
                Me.CombUser.Items.Add(SqlReader.Item(1))
                Me.CombUserII.Items.Add(SqlReader.Item(1))
                With Me.ListView1.Items.Add(SqlReader.Item(0))
                    .subitems.add(SqlReader.Item(1))
                End With
            End While
            cnn.Close()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmManageUsers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillUsers()
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If Me.ListView1.SelectedItems.Count = 0 Then
            Exit Sub
        ElseIf MsgBox("Confirm Delete?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim cmd As New SqlCommand("Delete From Users Where FullName=N'" & Me.ListView1.SelectedItems(0).SubItems(1).Text & "'", cnn)

                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()

                FillUsers()

                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnSavePrivi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSavePrivi.Click
        If Me.CombUser.SelectedIndex = -1 Then
            MsgBox("Please select a valid user")
        Else
            Try
                Dim strUpdate As String
                strUpdate = "Update Users Set " & _
                            " ChA=" & CInt(Me.ChA.CheckState) & _
                            ",ChB=" & CInt(Me.ChB.CheckState) & _
                            ",ChC=" & CInt(Me.ChC.CheckState) & _
                            ",ChD=" & CInt(Me.ChD.CheckState) & _
                            ",ChE=" & CInt(Me.ChE.CheckState) & _
                            ",ChF=" & CInt(Me.ChF.CheckState) & _
                            ",ChG=" & CInt(Me.ChG.CheckState) & _
                            ",ChH=" & CInt(Me.ChH.CheckState) & _
                            ",ChI=" & CInt(Me.ChI.CheckState) & _
                            ",ChJ=" & CInt(Me.CHJ.CheckState) & _
                            " Where FullName = N'" & Me.CombUser.SelectedItem & "'"

                Dim cmd As New SqlCommand(strUpdate, cnn)

                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()

                MsgBox("Saved Successfully")

                UnCheckAll()
                Me.CombUser.SelectedIndex = -1
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox(ex.ToString)
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
            End Try
        End If
    End Sub

    Private Sub btnClosePrivi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClosePrivi.Click
        Me.Close()
    End Sub

    Private Sub btnSaveCHang_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveCHang.Click
        If Me.CombUserII.SelectedIndex = -1 Then
            MsgBox("Please select user from the list")
        ElseIf Me.txtNewPass.Text.Length = 0 Then
            MsgBox("Please enter a new password")
            Me.txtNewPass.Focus()
        Else
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim cmd As New SqlCommand("Update Users set Pass=N'" & Me.txtNewPass.Text.Trim & _
                                         "' Where FullName=N'" & Me.CombUserII.SelectedItem & "'", cnn)
                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()

                MsgBox("Saved Successfully")

                Me.txtNewPass.Clear()

                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox(ex.ToString)
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
            End Try
        End If
    End Sub

    Private Sub btnCloasChangPas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloasChangPas.Click
        Me.Close()
    End Sub

    Private Sub TabPage4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage4.Click

    End Sub

    Private Sub btnUnCheckall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnCheckall.Click
        UnCheckAll()
    End Sub

    Private Sub btnCheckall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckall.Click
        CheckAll()
    End Sub
End Class
