<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReplaceItem
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReplaceItem))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtStoreName = New System.Windows.Forms.TextBox()
        Me.btnReplace = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtRPrice = New System.Windows.Forms.TextBox()
        Me.txtWPrice = New System.Windows.Forms.TextBox()
        Me.txtPack = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.CombBatchNo = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.ChBonus = New System.Windows.Forms.CheckBox()
        Me.txtQnt = New System.Windows.Forms.NumericUpDown()
        Me.txtAvailableQnt = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.CombItem = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.CombCompany = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.GroupBox2.SuspendLayout()
        CType(Me.txtQnt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.GroupBox2.Controls.Add(Me.txtStoreName)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtRPrice)
        Me.GroupBox2.Controls.Add(Me.txtWPrice)
        Me.GroupBox2.Controls.Add(Me.txtPack)
        Me.GroupBox2.Controls.Add(Me.Label21)
        Me.GroupBox2.Controls.Add(Me.CombBatchNo)
        Me.GroupBox2.Controls.Add(Me.Label19)
        Me.GroupBox2.Controls.Add(Me.ChBonus)
        Me.GroupBox2.Controls.Add(Me.txtQnt)
        Me.GroupBox2.Controls.Add(Me.txtAvailableQnt)
        Me.GroupBox2.Controls.Add(Me.Label20)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.CombItem)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.CombCompany)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Location = New System.Drawing.Point(4, 7)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(728, 105)
        Me.GroupBox2.TabIndex = 162
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "New Items"
        '
        'txtStoreName
        '
        Me.txtStoreName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStoreName.Location = New System.Drawing.Point(436, 73)
        Me.txtStoreName.Name = "txtStoreName"
        Me.txtStoreName.ReadOnly = True
        Me.txtStoreName.Size = New System.Drawing.Size(121, 20)
        Me.txtStoreName.TabIndex = 85
        '
        'btnReplace
        '
        Me.btnReplace.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnReplace.Location = New System.Drawing.Point(654, 118)
        Me.btnReplace.Name = "btnReplace"
        Me.btnReplace.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnReplace.Size = New System.Drawing.Size(78, 28)
        Me.btnReplace.TabIndex = 4
        Me.btnReplace.Text = "Replace"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(367, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 86
        Me.Label3.Text = "Store Name"
        '
        'txtRPrice
        '
        Me.txtRPrice.BackColor = System.Drawing.Color.Black
        Me.txtRPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRPrice.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRPrice.ForeColor = System.Drawing.Color.LawnGreen
        Me.txtRPrice.Location = New System.Drawing.Point(226, 71)
        Me.txtRPrice.Name = "txtRPrice"
        Me.txtRPrice.ReadOnly = True
        Me.txtRPrice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRPrice.Size = New System.Drawing.Size(77, 21)
        Me.txtRPrice.TabIndex = 84
        Me.txtRPrice.Text = "0.00"
        Me.txtRPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtWPrice
        '
        Me.txtWPrice.BackColor = System.Drawing.Color.Black
        Me.txtWPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWPrice.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWPrice.ForeColor = System.Drawing.Color.LawnGreen
        Me.txtWPrice.Location = New System.Drawing.Point(94, 72)
        Me.txtWPrice.Name = "txtWPrice"
        Me.txtWPrice.ReadOnly = True
        Me.txtWPrice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWPrice.Size = New System.Drawing.Size(77, 21)
        Me.txtWPrice.TabIndex = 83
        Me.txtWPrice.Text = "0.00"
        Me.txtWPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtPack
        '
        Me.txtPack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPack.Location = New System.Drawing.Point(436, 45)
        Me.txtPack.Name = "txtPack"
        Me.txtPack.ReadOnly = True
        Me.txtPack.Size = New System.Drawing.Size(121, 20)
        Me.txtPack.TabIndex = 5
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(401, 48)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(29, 13)
        Me.Label21.TabIndex = 80
        Me.Label21.Text = "Pack"
        '
        'CombBatchNo
        '
        Me.CombBatchNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CombBatchNo.FormattingEnabled = True
        Me.CombBatchNo.Location = New System.Drawing.Point(436, 16)
        Me.CombBatchNo.Name = "CombBatchNo"
        Me.CombBatchNo.Size = New System.Drawing.Size(121, 21)
        Me.CombBatchNo.Sorted = True
        Me.CombBatchNo.TabIndex = 2
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(380, 20)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(50, 13)
        Me.Label19.TabIndex = 77
        Me.Label19.Text = "Batch No"
        '
        'ChBonus
        '
        Me.ChBonus.AutoSize = True
        Me.ChBonus.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChBonus.Location = New System.Drawing.Point(644, 76)
        Me.ChBonus.Name = "ChBonus"
        Me.ChBonus.Size = New System.Drawing.Size(66, 20)
        Me.ChBonus.TabIndex = 3
        Me.ChBonus.Text = "Bonus"
        Me.ChBonus.UseVisualStyleBackColor = True
        '
        'txtQnt
        '
        Me.txtQnt.Location = New System.Drawing.Point(644, 45)
        Me.txtQnt.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        Me.txtQnt.Name = "txtQnt"
        Me.txtQnt.Size = New System.Drawing.Size(77, 20)
        Me.txtQnt.TabIndex = 4
        Me.txtQnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtAvailableQnt
        '
        Me.txtAvailableQnt.BackColor = System.Drawing.Color.Black
        Me.txtAvailableQnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAvailableQnt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAvailableQnt.ForeColor = System.Drawing.Color.LawnGreen
        Me.txtAvailableQnt.Location = New System.Drawing.Point(644, 17)
        Me.txtAvailableQnt.Name = "txtAvailableQnt"
        Me.txtAvailableQnt.ReadOnly = True
        Me.txtAvailableQnt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAvailableQnt.Size = New System.Drawing.Size(77, 21)
        Me.txtAvailableQnt.TabIndex = 74
        Me.txtAvailableQnt.Text = "0"
        Me.txtAvailableQnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(563, 21)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(75, 13)
        Me.Label20.TabIndex = 73
        Me.Label20.Text = "Available Qnt."
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(29, 48)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(59, 13)
        Me.Label17.TabIndex = 66
        Me.Label17.Text = "Item Name"
        '
        'CombItem
        '
        Me.CombItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CombItem.FormattingEnabled = True
        Me.CombItem.Location = New System.Drawing.Point(94, 44)
        Me.CombItem.Name = "CombItem"
        Me.CombItem.Size = New System.Drawing.Size(270, 21)
        Me.CombItem.Sorted = True
        Me.CombItem.TabIndex = 1
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(6, 20)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(82, 13)
        Me.Label18.TabIndex = 65
        Me.Label18.Text = "Company Name"
        '
        'CombCompany
        '
        Me.CombCompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.CombCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CombCompany.FormattingEnabled = True
        Me.CombCompany.Location = New System.Drawing.Point(94, 17)
        Me.CombCompany.Name = "CombCompany"
        Me.CombCompany.Size = New System.Drawing.Size(270, 21)
        Me.CombCompany.Sorted = True
        Me.CombCompany.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(173, 76)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(47, 13)
        Me.Label5.TabIndex = 56
        Me.Label5.Text = " R. Price"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(38, 76)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(50, 13)
        Me.Label6.TabIndex = 56
        Me.Label6.Text = " W. Price"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(589, 49)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(49, 13)
        Me.Label16.TabIndex = 54
        Me.Label16.Text = "Quantity"
        '
        'BtnClose
        '
        Me.BtnClose.Location = New System.Drawing.Point(534, 118)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(78, 28)
        Me.BtnClose.TabIndex = 163
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'frmReplaceItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(736, 150)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnReplace)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmReplaceItem"
        Me.Text = "Replace Item"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.txtQnt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtStoreName As System.Windows.Forms.TextBox
    Friend WithEvents btnReplace As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtRPrice As System.Windows.Forms.TextBox
    Friend WithEvents txtWPrice As System.Windows.Forms.TextBox
    Friend WithEvents txtPack As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents CombBatchNo As System.Windows.Forms.ComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents ChBonus As System.Windows.Forms.CheckBox
    Friend WithEvents txtQnt As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtAvailableQnt As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents CombItem As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents CombCompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents BtnClose As System.Windows.Forms.Button
End Class
