Imports System.Data.SqlClient

Public Class frmApprovingVouchers
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
    Friend WithEvents ListVouchers As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnApprove As System.Windows.Forms.Button
    Friend WithEvents GridVouchers As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDescr As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents combChType As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtCrd As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtDep As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtBalance As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DTPTrans As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents txtAcc4 As System.Windows.Forms.TextBox
    Friend WithEvents txtAcc3 As System.Windows.Forms.TextBox
    Friend WithEvents txtAcc2 As System.Windows.Forms.TextBox
    Friend WithEvents txtAcc1 As System.Windows.Forms.TextBox
    Friend WithEvents Package As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Acc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Depit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Credit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmApprovingVouchers))
        Me.ListVouchers = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnApprove = New System.Windows.Forms.Button()
        Me.GridVouchers = New System.Windows.Forms.DataGridView()
        Me.Package = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Acc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Depit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Credit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDescr = New System.Windows.Forms.TextBox()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.combChType = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtCrd = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtDep = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtBalance = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DTPTrans = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtAcc4 = New System.Windows.Forms.TextBox()
        Me.txtAcc3 = New System.Windows.Forms.TextBox()
        Me.txtAcc2 = New System.Windows.Forms.TextBox()
        Me.txtAcc1 = New System.Windows.Forms.TextBox()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GridVouchers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListVouchers
        '
        Me.ListVouchers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListVouchers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.ListVouchers.FullRowSelect = True
        Me.ListVouchers.GridLines = True
        Me.ListVouchers.HideSelection = False
        Me.ListVouchers.Location = New System.Drawing.Point(9, 46)
        Me.ListVouchers.Name = "ListVouchers"
        Me.ListVouchers.Size = New System.Drawing.Size(86, 444)
        Me.ListVouchers.TabIndex = 53
        Me.ListVouchers.UseCompatibleStateImageBehavior = False
        Me.ListVouchers.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "No ."
        Me.ColumnHeader1.Width = 66
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.ListVouchers)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(104, 527)
        Me.GroupBox1.TabIndex = 54
        Me.GroupBox1.TabStop = False
        '
        'Button3
        '
        Me.Button3.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button3.Location = New System.Drawing.Point(9, 496)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(86, 23)
        Me.Button3.TabIndex = 55
        Me.Button3.Text = "Delete "
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Button1.Location = New System.Drawing.Point(9, 17)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(86, 23)
        Me.Button1.TabIndex = 54
        Me.Button1.Text = "Refresh"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox6.Location = New System.Drawing.Point(117, 486)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(903, 4)
        Me.GroupBox6.TabIndex = 106
        Me.GroupBox6.TabStop = False
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(945, 496)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 32)
        Me.btnClose.TabIndex = 103
        Me.btnClose.Text = "Close "
        '
        'btnApprove
        '
        Me.btnApprove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnApprove.Location = New System.Drawing.Point(832, 496)
        Me.btnApprove.Name = "btnApprove"
        Me.btnApprove.Size = New System.Drawing.Size(75, 32)
        Me.btnApprove.TabIndex = 102
        Me.btnApprove.Text = "Approve "
        '
        'GridVouchers
        '
        Me.GridVouchers.AllowUserToAddRows = False
        Me.GridVouchers.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Khaki
        Me.GridVouchers.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GridVouchers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridVouchers.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.GridVouchers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridVouchers.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Package, Me.Acc, Me.Column3, Me.Column4, Me.Column1, Me.Depit, Me.Credit, Me.Column2})
        Me.GridVouchers.Location = New System.Drawing.Point(117, 117)
        Me.GridVouchers.Name = "GridVouchers"
        Me.GridVouchers.ReadOnly = True
        Me.GridVouchers.Size = New System.Drawing.Size(903, 312)
        Me.GridVouchers.TabIndex = 101
        '
        'Package
        '
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Package.DefaultCellStyle = DataGridViewCellStyle2
        Me.Package.FillWeight = 406.0914!
        Me.Package.HeaderText = "Account"
        Me.Package.Name = "Package"
        Me.Package.ReadOnly = True
        '
        'Acc
        '
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Acc.DefaultCellStyle = DataGridViewCellStyle3
        Me.Acc.FillWeight = 56.27266!
        Me.Acc.HeaderText = "=>"
        Me.Acc.Name = "Acc"
        Me.Acc.ReadOnly = True
        '
        'Column3
        '
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column3.DefaultCellStyle = DataGridViewCellStyle4
        Me.Column3.FillWeight = 56.27266!
        Me.Column3.HeaderText = "=>"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column4
        '
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column4.DefaultCellStyle = DataGridViewCellStyle5
        Me.Column4.FillWeight = 56.27266!
        Me.Column4.HeaderText = "=>"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 150
        '
        'Column1
        '
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Column1.DefaultCellStyle = DataGridViewCellStyle6
        Me.Column1.FillWeight = 56.27266!
        Me.Column1.HeaderText = "Details"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 150
        '
        'Depit
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.Format = "N2"
        DataGridViewCellStyle7.NullValue = "0"
        Me.Depit.DefaultCellStyle = DataGridViewCellStyle7
        Me.Depit.FillWeight = 56.27266!
        Me.Depit.HeaderText = "Debit"
        Me.Depit.Name = "Depit"
        Me.Depit.ReadOnly = True
        Me.Depit.Width = 80
        '
        'Credit
        '
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.Format = "N2"
        DataGridViewCellStyle8.NullValue = "0"
        Me.Credit.DefaultCellStyle = DataGridViewCellStyle8
        Me.Credit.FillWeight = 56.27266!
        Me.Credit.HeaderText = "Credit"
        Me.Credit.Name = "Credit"
        Me.Credit.ReadOnly = True
        Me.Credit.Width = 80
        '
        'Column2
        '
        Me.Column2.HeaderText = "Delete "
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 75
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.txtAmount)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtDescr)
        Me.GroupBox2.Controls.Add(Me.Button6)
        Me.GroupBox2.Controls.Add(Me.combChType)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Location = New System.Drawing.Point(117, 62)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox2.Size = New System.Drawing.Size(903, 49)
        Me.GroupBox2.TabIndex = 100
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Transaction Details"
        '
        'txtAmount
        '
        Me.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.Location = New System.Drawing.Point(185, 19)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(103, 21)
        Me.txtAmount.TabIndex = 1
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(292, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 13)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "Description :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(14, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Type :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDescr
        '
        Me.txtDescr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescr.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescr.Location = New System.Drawing.Point(359, 19)
        Me.txtDescr.Name = "txtDescr"
        Me.txtDescr.Size = New System.Drawing.Size(273, 21)
        Me.txtDescr.TabIndex = 2
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(638, 19)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(72, 23)
        Me.Button6.TabIndex = 2
        Me.Button6.Text = "Add"
        '
        'combChType
        '
        Me.combChType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.combChType.DropDownWidth = 73
        Me.combChType.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.combChType.Items.AddRange(New Object() {"Debit", "Credit"})
        Me.combChType.Location = New System.Drawing.Point(52, 19)
        Me.combChType.Name = "combChType"
        Me.combChType.Size = New System.Drawing.Size(73, 21)
        Me.combChType.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(134, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 27
        Me.Label3.Text = "Amount :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.txtCrd)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.txtDep)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.txtBalance)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Location = New System.Drawing.Point(117, 433)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(903, 47)
        Me.GroupBox3.TabIndex = 107
        Me.GroupBox3.TabStop = False
        '
        'txtCrd
        '
        Me.txtCrd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCrd.BackColor = System.Drawing.Color.Black
        Me.txtCrd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCrd.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.txtCrd.ForeColor = System.Drawing.Color.GreenYellow
        Me.txtCrd.Location = New System.Drawing.Point(458, 13)
        Me.txtCrd.Name = "txtCrd"
        Me.txtCrd.ReadOnly = True
        Me.txtCrd.Size = New System.Drawing.Size(163, 26)
        Me.txtCrd.TabIndex = 43
        Me.txtCrd.Text = "0.00"
        Me.txtCrd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(396, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(60, 19)
        Me.Label5.TabIndex = 92
        Me.Label5.Text = "Credit :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDep
        '
        Me.txtDep.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDep.BackColor = System.Drawing.Color.Black
        Me.txtDep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDep.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.txtDep.ForeColor = System.Drawing.Color.GreenYellow
        Me.txtDep.Location = New System.Drawing.Point(193, 13)
        Me.txtDep.Name = "txtDep"
        Me.txtDep.ReadOnly = True
        Me.txtDep.Size = New System.Drawing.Size(163, 26)
        Me.txtDep.TabIndex = 42
        Me.txtDep.Text = "0.00"
        Me.txtDep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label9.Location = New System.Drawing.Point(657, 17)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(52, 19)
        Me.Label9.TabIndex = 94
        Me.Label9.Text = "Total :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtBalance
        '
        Me.txtBalance.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBalance.BackColor = System.Drawing.Color.Black
        Me.txtBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBalance.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.txtBalance.ForeColor = System.Drawing.Color.GreenYellow
        Me.txtBalance.Location = New System.Drawing.Point(710, 13)
        Me.txtBalance.Name = "txtBalance"
        Me.txtBalance.ReadOnly = True
        Me.txtBalance.Size = New System.Drawing.Size(163, 26)
        Me.txtBalance.TabIndex = 43
        Me.txtBalance.Text = "0.00"
        Me.txtBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label8.Location = New System.Drawing.Point(135, 17)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(55, 19)
        Me.Label8.TabIndex = 93
        Me.Label8.Text = "Debit :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(120, 500)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label6.Size = New System.Drawing.Size(34, 13)
        Me.Label6.TabIndex = 128
        Me.Label6.Text = "Date"
        '
        'DTPTrans
        '
        Me.DTPTrans.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.DTPTrans.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPTrans.Location = New System.Drawing.Point(160, 496)
        Me.DTPTrans.Name = "DTPTrans"
        Me.DTPTrans.Size = New System.Drawing.Size(212, 21)
        Me.DTPTrans.TabIndex = 129
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.Controls.Add(Me.Label1)
        Me.GroupBox5.Controls.Add(Me.txtAcc4)
        Me.GroupBox5.Controls.Add(Me.txtAcc3)
        Me.GroupBox5.Controls.Add(Me.txtAcc2)
        Me.GroupBox5.Controls.Add(Me.txtAcc1)
        Me.GroupBox5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.Location = New System.Drawing.Point(117, 12)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(903, 48)
        Me.GroupBox5.TabIndex = 127
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Account"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = " Account"
        '
        'txtAcc4
        '
        Me.txtAcc4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAcc4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAcc4.Location = New System.Drawing.Point(463, 19)
        Me.txtAcc4.Name = "txtAcc4"
        Me.txtAcc4.ReadOnly = True
        Me.txtAcc4.Size = New System.Drawing.Size(169, 21)
        Me.txtAcc4.TabIndex = 3
        '
        'txtAcc3
        '
        Me.txtAcc3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAcc3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAcc3.Location = New System.Drawing.Point(326, 19)
        Me.txtAcc3.Name = "txtAcc3"
        Me.txtAcc3.ReadOnly = True
        Me.txtAcc3.Size = New System.Drawing.Size(131, 21)
        Me.txtAcc3.TabIndex = 2
        '
        'txtAcc2
        '
        Me.txtAcc2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAcc2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAcc2.Location = New System.Drawing.Point(189, 19)
        Me.txtAcc2.Name = "txtAcc2"
        Me.txtAcc2.ReadOnly = True
        Me.txtAcc2.Size = New System.Drawing.Size(131, 21)
        Me.txtAcc2.TabIndex = 1
        '
        'txtAcc1
        '
        Me.txtAcc1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAcc1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAcc1.Location = New System.Drawing.Point(52, 19)
        Me.txtAcc1.Name = "txtAcc1"
        Me.txtAcc1.ReadOnly = True
        Me.txtAcc1.Size = New System.Drawing.Size(131, 21)
        Me.txtAcc1.TabIndex = 0
        '
        'frmApprovingVouchers
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(1029, 536)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.DTPTrans)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnApprove)
        Me.Controls.Add(Me.GridVouchers)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(952, 453)
        Me.Name = "frmApprovingVouchers"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Approval Form"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.GridVouchers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Sub Calculate()
        If Me.GridVouchers.Rows.Count = 0 Then
            Me.txtCrd.Text = "0.00"
            Me.txtDep.Text = "0.00"
            Me.txtBalance.Text = "0.00"
        Else
            Try
                Dim Crd, Dep As Double
                Dim i As Integer

                ' Iterate through a dictionary
                For i = 0 To Me.GridVouchers.Rows.Count - 1
                    Crd = Crd + Me.GridVouchers.Rows(i).Cells(6).Value
                    Dep = Dep + Me.GridVouchers.Rows(i).Cells(5).Value
                Next

                If Crd = 0 Then
                    Me.txtCrd.Text = "0.00"
                Else
                    Me.txtCrd.Text = Format(Crd, "##,###.##")
                End If

                If Dep = 0 Then
                    Me.txtDep.Text = "0.00"
                Else
                    Me.txtDep.Text = Format(Dep, "##,###.##")
                End If

                If Crd - Dep = 0 Then
                    Me.txtBalance.Text = "0.00"
                Else
                    Me.txtBalance.Text = Format(CDbl(Crd - Dep), "##,###.##")
                End If
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Sub FillTempVouchersNo()
        Try
            Me.Cursor = Cursors.Default

            Dim cmd As New SqlCommand("Select Distinct MoveNo From TempVouchers", cnn)
            Dim Reader As SqlDataReader

            Me.ListVouchers.Items.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.ListVouchers.Items.Add(Reader.Item(0))
            End While
            cnn.Close()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub frmBalancing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        FillTempVouchersNo()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If Me.ListVouchers.SelectedItems.Count > 0 Then
            Try
                If MsgBox("Are You Sure To Delete ?", MsgBoxStyle.YesNoCancel) = MsgBoxResult.Yes Then
                    Me.Cursor = Cursors.WaitCursor

                    Dim cmd As New SqlCommand("Delete From TempVouchers where MoveNo=" & Me.ListVouchers.SelectedItems(0).Text, cnn)

                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()

                    MsgBox("Deleted")

                    FillTempVouchersNo()

                    Me.Cursor = Cursors.Default
                End If
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        FillTempVouchersNo()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        If Me.ListVouchers.SelectedItems.Count = 0 Then
            MsgBox("Please Select Voucher ")
        ElseIf Me.txtAcc1.Text.Trim.Length = 0 Then
            MsgBox("Please select Account")
        ElseIf Me.combChType.SelectedIndex = -1 Then
            MsgBox("Please select Type of Move")
            Me.combChType.Focus()
        ElseIf Len(Me.txtAmount.Text) = 0 Then
            MsgBox("Please check the amount")
            Me.txtAmount.Focus()
            Exit Sub
        ElseIf Me.txtDescr.Text.Trim.Length = 0 Then
            MsgBox("Please enter a description of registration")
            Me.txtDescr.Focus()
            Exit Sub
        Else
            Try
                Me.Cursor = Cursors.WaitCursor

                'Validate amount
                Try
                    Dim X As Double = CDbl(Me.txtAmount.Text)
                Catch ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox("Please check the amount")
                    Me.txtAmount.Clear()
                    Me.txtAmount.Focus()
                    Exit Sub
                End Try

                Dim Row(5) As String
                If Me.combChType.SelectedItem = "Debit" Then

                    Dim DepRow As String() = {Me.txtAcc1.Text, Me.txtAcc2.Text, Me.txtAcc3.Text, Me.txtAcc4.Text, _
                                              Me.txtDescr.Text.Trim, CDbl(Me.txtAmount.Text).ToString("N2"), "0", "Delete"}
                    Row = DepRow

                Else
                    Dim CrdRow As String() = {Me.txtAcc1.Text, Me.txtAcc2.Text, Me.txtAcc3.Text, Me.txtAcc4.Text, _
                                              Me.txtDescr.Text.Trim, "0", CDbl(Me.txtAmount.Text).ToString("N2"), "Delete"}
                    Row = CrdRow
                End If

                Me.GridVouchers.Rows.Add(Row)
                Calculate()

                Me.txtAcc1.Clear()
                Me.txtAcc2.Clear()
                Me.txtAcc3.Clear()
                Me.txtAcc4.Clear()
                Me.combChType.SelectedIndex = -1
                Me.txtAmount.Clear()
                Me.txtDescr.Clear()

                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub btnApprove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApprove.Click
        If Me.ListVouchers.SelectedItems.Count = 0 Then
            MsgBox("Please Select Voucher")
        ElseIf Me.GridVouchers.Rows.Count = 0 Then
            Exit Sub
        ElseIf CDbl(Me.txtCrd.Text - Me.txtDep.Text) <> 0 Then
            MsgBox("Please complete the voucher")
            Exit Sub
        Else
            Try
                Me.Cursor = Cursors.WaitCursor

                Dim MoveNo As Integer
                Dim i As Integer
                Dim cmd As New SqlCommand
                Dim Trans As SqlTransaction
                Dim TransDate As String = "N'" & Me.DTPTrans.Value.ToString("MM/dd/yyyy") & " 10:10:10'"

                cnn.Open()
                Trans = cnn.BeginTransaction
                cmd.Connection = cnn
                cmd.Transaction = Trans


                cmd.CommandText = "Select IsNull(Max(MoveNo),0) From Transactions Where Year(TransDate)=Year(GetDate())"
                MoveNo = CInt(cmd.ExecuteScalar) + 1

                For i = 0 To Me.GridVouchers.Rows.Count - 1
                    cmd.CommandText = "Insert Into Transactions (TransType,MoveNo,Descr,Acc1,Acc2,Acc3,Acc4,TotalIn,TotalOut,Employee,TransDate) " & _
                                      "Values (@TransType,@MoveNo,@Descr,@Acc1,@Acc2,@Acc3,@Acc4,@TotalIn,@TotalOut,@Employee," & TransDate & ")"

                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@TransType", "Journal Voucher")
                    cmd.Parameters.AddWithValue("@MoveNo", MoveNo)
                    cmd.Parameters.AddWithValue("@Descr", Me.GridVouchers.Rows(i).Cells(4).Value)
                    cmd.Parameters.AddWithValue("@Acc1", Me.GridVouchers.Rows(i).Cells(0).Value)
                    cmd.Parameters.AddWithValue("@Acc2", Me.GridVouchers.Rows(i).Cells(1).Value)
                    cmd.Parameters.AddWithValue("@Acc3", Me.GridVouchers.Rows(i).Cells(2).Value)
                    cmd.Parameters.AddWithValue("@Acc4", Me.GridVouchers.Rows(i).Cells(3).Value)
                    cmd.Parameters.AddWithValue("@TotalOut", CDbl(Me.GridVouchers.Rows(i).Cells(5).Value))
                    cmd.Parameters.AddWithValue("@TotalIn", CDbl(Me.GridVouchers.Rows(i).Cells(6).Value))
                    cmd.Parameters.AddWithValue("@Employee", CurrentUser)

                    cmd.ExecuteNonQuery()
                Next

                'Delete from Temp. Vouchers
                cmd.CommandText = "Delete From TempVouchers Where MoveNo=" & Me.ListVouchers.SelectedItems(0).Text
                cmd.ExecuteNonQuery()

                Trans.Commit()
                cnn.Close()

                MsgBox("Approved Successfully")

                PrintVoucher(MoveNo, Me.DTPTrans.Value.Year)

                FillTempVouchersNo()

                'Reset controls
                Me.GridVouchers.Rows.Clear()
                Me.txtAcc1.Clear()
                Me.txtAcc2.Clear()
                Me.txtAcc3.Clear()
                Me.txtAcc4.Clear()
                Me.combChType.SelectedIndex = -1
                Me.txtAmount.Clear()
                Me.txtDescr.Clear()
                Me.txtCrd.Text = "0.00"
                Me.txtDep.Text = "0.00"
                Me.txtBalance.Text = "0.00"
                Me.DTPTrans.Value = Today.Date

                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub ListVouchers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListVouchers.SelectedIndexChanged
        Me.GridVouchers.Rows.Clear()

        If Me.ListVouchers.SelectedItems.Count > 0 Then
            Try
                Me.Cursor = Cursors.WaitCursor

                Dim cmd As New SqlCommand("Select * From TempVouchers Where MoveNo=" & Me.ListVouchers.SelectedItems(0).Text, cnn)
                Dim Reader As SqlDataReader

                Me.GridVouchers.Rows.Clear()

                cnn.Open()
                Reader = cmd.ExecuteReader
                While Reader.Read
                    Me.GridVouchers.Rows.Add(New String() {Reader.Item("Acc1"), Reader.Item("Acc2"), Reader.Item("Acc3"), Reader.Item("Acc4"), _
                                                           Reader.Item("Descr"), Reader.Item("TotalValueOut"), Reader.Item("TotalValueIn"), "Delete"})
                    Me.DTPTrans.Value = CDate(Reader.Item("TransDate"))
                End While
                cnn.Close()

                Calculate()

                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub GridVouchers_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridVouchers.CellClick
        If e.ColumnIndex = 7 Then
            Me.GridVouchers.Rows.RemoveAt(e.RowIndex)
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
