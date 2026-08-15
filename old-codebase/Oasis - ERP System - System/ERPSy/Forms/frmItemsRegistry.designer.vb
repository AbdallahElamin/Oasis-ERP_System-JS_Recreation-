<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmItemsRegistry
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmItemsRegistry))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnNar = New System.Windows.Forms.Button()
        Me.CombCompanyName = New System.Windows.Forms.ComboBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.GridItems = New System.Windows.Forms.DataGridView()
        Me.Column0 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtMinLevel = New System.Windows.Forms.NumericUpDown()
        Me.txtRPrice = New System.Windows.Forms.NumericUpDown()
        Me.txtWPrice = New System.Windows.Forms.NumericUpDown()
        Me.txtPack = New System.Windows.Forms.TextBox()
        Me.txtGenericName = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtItem = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ErrProv = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.btnSave = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox2.SuspendLayout()
        CType(Me.GridItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtMinLevel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtWPrice, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrProv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.btnNar)
        Me.GroupBox2.Controls.Add(Me.CombCompanyName)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(814, 52)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Company"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 13)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Company Name"
        '
        'btnNar
        '
        Me.btnNar.Location = New System.Drawing.Point(412, 18)
        Me.btnNar.Name = "btnNar"
        Me.btnNar.Size = New System.Drawing.Size(59, 23)
        Me.btnNar.TabIndex = 1
        Me.btnNar.Text = "Edit List"
        Me.btnNar.UseVisualStyleBackColor = True
        '
        'CombCompanyName
        '
        Me.CombCompanyName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CombCompanyName.FormattingEnabled = True
        Me.CombCompanyName.Location = New System.Drawing.Point(98, 19)
        Me.CombCompanyName.Name = "CombCompanyName"
        Me.CombCompanyName.Size = New System.Drawing.Size(308, 21)
        Me.CombCompanyName.TabIndex = 0
        '
        'btnClear
        '
        Me.btnClear.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnClear.Location = New System.Drawing.Point(701, 141)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 1
        Me.btnClear.Text = "Clear"
        '
        'GridItems
        '
        Me.GridItems.AllowUserToAddRows = False
        Me.GridItems.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Khaki
        Me.GridItems.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GridItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridItems.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column0, Me.Column5, Me.Column1, Me.Column6, Me.Column4, Me.Column2, Me.Column3, Me.Column8, Me.Column7})
        Me.GridItems.Location = New System.Drawing.Point(9, 170)
        Me.GridItems.Name = "GridItems"
        Me.GridItems.Size = New System.Drawing.Size(814, 202)
        Me.GridItems.TabIndex = 2
        '
        'Column0
        '
        Me.Column0.HeaderText = "SNo"
        Me.Column0.Name = "Column0"
        Me.Column0.Visible = False
        '
        'Column5
        '
        Me.Column5.HeaderText = "Trade Item"
        Me.Column5.Name = "Column5"
        Me.Column5.Width = 200
        '
        'Column1
        '
        Me.Column1.HeaderText = "Generic Name"
        Me.Column1.Name = "Column1"
        Me.Column1.Width = 200
        '
        'Column6
        '
        Me.Column6.HeaderText = "Pack"
        Me.Column6.Name = "Column6"
        '
        'Column4
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column4.DefaultCellStyle = DataGridViewCellStyle2
        Me.Column4.HeaderText = "Minimum Level"
        Me.Column4.Name = "Column4"
        '
        'Column2
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column2.DefaultCellStyle = DataGridViewCellStyle3
        Me.Column2.HeaderText = "W. Price"
        Me.Column2.Name = "Column2"
        Me.Column2.Width = 75
        '
        'Column3
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column3.DefaultCellStyle = DataGridViewCellStyle4
        Me.Column3.HeaderText = "R. Price"
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 75
        '
        'Column8
        '
        Me.Column8.HeaderText = "Update"
        Me.Column8.Name = "Column8"
        Me.Column8.Width = 75
        '
        'Column7
        '
        Me.Column7.HeaderText = "Delete"
        Me.Column7.Name = "Column7"
        Me.Column7.Text = "Delete"
        Me.Column7.ToolTipText = "Delete"
        Me.Column7.Width = 75
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.txtMinLevel)
        Me.GroupBox1.Controls.Add(Me.txtRPrice)
        Me.GroupBox1.Controls.Add(Me.txtWPrice)
        Me.GroupBox1.Controls.Add(Me.txtPack)
        Me.GroupBox1.Controls.Add(Me.txtGenericName)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtItem)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 58)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(814, 77)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Items Details"
        '
        'txtMinLevel
        '
        Me.txtMinLevel.Location = New System.Drawing.Point(303, 45)
        Me.txtMinLevel.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.txtMinLevel.Name = "txtMinLevel"
        Me.txtMinLevel.Size = New System.Drawing.Size(74, 20)
        Me.txtMinLevel.TabIndex = 3
        Me.txtMinLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtRPrice
        '
        Me.txtRPrice.DecimalPlaces = 2
        Me.txtRPrice.Location = New System.Drawing.Point(617, 45)
        Me.txtRPrice.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.txtRPrice.Name = "txtRPrice"
        Me.txtRPrice.Size = New System.Drawing.Size(74, 20)
        Me.txtRPrice.TabIndex = 5
        Me.txtRPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtWPrice
        '
        Me.txtWPrice.DecimalPlaces = 2
        Me.txtWPrice.Location = New System.Drawing.Point(476, 45)
        Me.txtWPrice.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        Me.txtWPrice.Name = "txtWPrice"
        Me.txtWPrice.Size = New System.Drawing.Size(74, 20)
        Me.txtWPrice.TabIndex = 4
        Me.txtWPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtPack
        '
        Me.txtPack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPack.Location = New System.Drawing.Point(98, 45)
        Me.txtPack.Name = "txtPack"
        Me.txtPack.Size = New System.Drawing.Size(108, 20)
        Me.txtPack.TabIndex = 2
        '
        'txtGenericName
        '
        Me.txtGenericName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGenericName.Location = New System.Drawing.Point(476, 19)
        Me.txtGenericName.Name = "txtGenericName"
        Me.txtGenericName.Size = New System.Drawing.Size(279, 20)
        Me.txtGenericName.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(63, 49)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 13)
        Me.Label7.TabIndex = 53
        Me.Label7.Text = "Pack"
        '
        'txtItem
        '
        Me.txtItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItem.Location = New System.Drawing.Point(98, 19)
        Me.txtItem.Name = "txtItem"
        Me.txtItem.Size = New System.Drawing.Size(279, 20)
        Me.txtItem.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(397, 23)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(73, 13)
        Me.Label6.TabIndex = 53
        Me.Label6.Text = "Generic Name"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(27, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 13)
        Me.Label4.TabIndex = 53
        Me.Label4.Text = "Trade Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(222, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(75, 13)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Minimum Level"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(567, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "R. Price"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(422, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 48
        Me.Label1.Text = "W. Price"
        '
        'ErrProv
        '
        Me.ErrProv.ContainerControl = Me
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(602, 141)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(748, 378)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 30)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Close"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 154)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 13)
        Me.Label8.TabIndex = 57
        Me.Label8.Text = "Items List"
        '
        'frmItemsRegistry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(832, 416)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.GridItems)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(801, 454)
        Me.Name = "frmItemsRegistry"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Items Registry"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.GridItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.txtMinLevel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtWPrice, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrProv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnNar As System.Windows.Forms.Button
    Friend WithEvents CombCompanyName As System.Windows.Forms.ComboBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents GridItems As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ErrProv As System.Windows.Forms.ErrorProvider
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtItem As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtGenericName As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents txtPack As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtWPrice As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtMinLevel As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtRPrice As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Column0 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewButtonColumn
End Class
