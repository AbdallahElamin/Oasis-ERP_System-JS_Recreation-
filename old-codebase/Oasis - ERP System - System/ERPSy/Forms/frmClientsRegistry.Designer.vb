<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmClientsRegistry
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnsearch = New System.Windows.Forms.Button()
        Me.RMedRepres = New System.Windows.Forms.RadioButton()
        Me.RClientName = New System.Windows.Forms.RadioButton()
        Me.CombSalesMan = New System.Windows.Forms.ComboBox()
        Me.RSalesMan = New System.Windows.Forms.RadioButton()
        Me.combState = New System.Windows.Forms.ComboBox()
        Me.RState = New System.Windows.Forms.RadioButton()
        Me.txtClientNameSearch = New System.Windows.Forms.TextBox()
        Me.RAll = New System.Windows.Forms.RadioButton()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Column0 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewButtonColumn()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Khaki
        Me.DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column0, Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column9, Me.Column8})
        Me.DataGridView1.Location = New System.Drawing.Point(8, 17)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(1131, 327)
        Me.DataGridView1.TabIndex = 2
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.DataGridView1)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 118)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1146, 352)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Clients List"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.btnsearch)
        Me.GroupBox1.Controls.Add(Me.RMedRepres)
        Me.GroupBox1.Controls.Add(Me.RClientName)
        Me.GroupBox1.Controls.Add(Me.CombSalesMan)
        Me.GroupBox1.Controls.Add(Me.RSalesMan)
        Me.GroupBox1.Controls.Add(Me.combState)
        Me.GroupBox1.Controls.Add(Me.RState)
        Me.GroupBox1.Controls.Add(Me.txtClientNameSearch)
        Me.GroupBox1.Controls.Add(Me.RAll)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 28)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1146, 87)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Filter By"
        '
        'btnsearch
        '
        Me.btnsearch.Location = New System.Drawing.Point(761, 47)
        Me.btnsearch.Name = "btnsearch"
        Me.btnsearch.Size = New System.Drawing.Size(72, 23)
        Me.btnsearch.TabIndex = 7
        Me.btnsearch.Text = "Search"
        Me.btnsearch.UseVisualStyleBackColor = True
        '
        'RMedRepres
        '
        Me.RMedRepres.AutoSize = True
        Me.RMedRepres.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.RMedRepres.Location = New System.Drawing.Point(392, 48)
        Me.RMedRepres.Name = "RMedRepres"
        Me.RMedRepres.Size = New System.Drawing.Size(59, 17)
        Me.RMedRepres.TabIndex = 7
        Me.RMedRepres.Text = "By ID#"
        Me.RMedRepres.UseVisualStyleBackColor = True
        '
        'RClientName
        '
        Me.RClientName.AutoSize = True
        Me.RClientName.Checked = True
        Me.RClientName.Location = New System.Drawing.Point(10, 20)
        Me.RClientName.Name = "RClientName"
        Me.RClientName.Size = New System.Drawing.Size(97, 17)
        Me.RClientName.TabIndex = 0
        Me.RClientName.TabStop = True
        Me.RClientName.Text = "By Client Name"
        Me.RClientName.UseVisualStyleBackColor = True
        '
        'CombSalesMan
        '
        Me.CombSalesMan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CombSalesMan.FormattingEnabled = True
        Me.CombSalesMan.Location = New System.Drawing.Point(122, 47)
        Me.CombSalesMan.MaxDropDownItems = 25
        Me.CombSalesMan.Name = "CombSalesMan"
        Me.CombSalesMan.Size = New System.Drawing.Size(240, 21)
        Me.CombSalesMan.TabIndex = 5
        '
        'RSalesMan
        '
        Me.RSalesMan.AutoSize = True
        Me.RSalesMan.Location = New System.Drawing.Point(10, 49)
        Me.RSalesMan.Name = "RSalesMan"
        Me.RSalesMan.Size = New System.Drawing.Size(57, 17)
        Me.RSalesMan.TabIndex = 4
        Me.RSalesMan.Text = "By Job"
        Me.RSalesMan.UseVisualStyleBackColor = True
        '
        'combState
        '
        Me.combState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.combState.FormattingEnabled = True
        Me.combState.Location = New System.Drawing.Point(497, 18)
        Me.combState.MaxDropDownItems = 25
        Me.combState.Name = "combState"
        Me.combState.Size = New System.Drawing.Size(240, 21)
        Me.combState.TabIndex = 3
        '
        'RState
        '
        Me.RState.AutoSize = True
        Me.RState.Location = New System.Drawing.Point(392, 20)
        Me.RState.Name = "RState"
        Me.RState.Size = New System.Drawing.Size(65, 17)
        Me.RState.TabIndex = 2
        Me.RState.Text = "By Class"
        Me.RState.UseVisualStyleBackColor = True
        '
        'txtClientNameSearch
        '
        Me.txtClientNameSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClientNameSearch.Location = New System.Drawing.Point(122, 18)
        Me.txtClientNameSearch.Name = "txtClientNameSearch"
        Me.txtClientNameSearch.Size = New System.Drawing.Size(239, 20)
        Me.txtClientNameSearch.TabIndex = 0
        '
        'RAll
        '
        Me.RAll.AutoSize = True
        Me.RAll.Location = New System.Drawing.Point(761, 20)
        Me.RAll.Name = "RAll"
        Me.RAll.Size = New System.Drawing.Size(65, 17)
        Me.RAll.TabIndex = 6
        Me.RAll.Text = "Show All"
        Me.RAll.UseVisualStyleBackColor = True
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.ToolStrip1.Size = New System.Drawing.Size(1163, 25)
        Me.ToolStrip1.TabIndex = 7
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = Global.OasisERPSystem.My.Resources.Resources.Add
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(113, 22)
        Me.ToolStripButton1.Tag = "Add New Client"
        Me.ToolStripButton1.Text = "Add New Profile"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(497, 49)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(240, 20)
        Me.TextBox1.TabIndex = 8
        '
        'Column0
        '
        Me.Column0.HeaderText = "Emp ID"
        Me.Column0.Name = "Column0"
        Me.Column0.ReadOnly = True
        Me.Column0.Width = 75
        '
        'Column1
        '
        Me.Column1.HeaderText = "Name"
        Me.Column1.Name = "Column1"
        Me.Column1.Width = 200
        '
        'Column2
        '
        Me.Column2.HeaderText = "Mobile"
        Me.Column2.Name = "Column2"
        '
        'Column3
        '
        Me.Column3.HeaderText = "Class"
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 125
        '
        'Column4
        '
        Me.Column4.HeaderText = "State"
        Me.Column4.Name = "Column4"
        Me.Column4.Width = 125
        '
        'Column5
        '
        Me.Column5.HeaderText = "Region"
        Me.Column5.Name = "Column5"
        Me.Column5.Width = 150
        '
        'Column9
        '
        Me.Column9.HeaderText = "Group Name"
        Me.Column9.Name = "Column9"
        Me.Column9.Width = 150
        '
        'Column8
        '
        Me.Column8.HeaderText = "Edit"
        Me.Column8.Name = "Column8"
        Me.Column8.Width = 75
        '
        'frmClientsRegistry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1163, 480)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.MinimumSize = New System.Drawing.Size(720, 500)
        Me.Name = "frmClientsRegistry"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "طباعة بطاقة"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RClientName As System.Windows.Forms.RadioButton
    Friend WithEvents CombSalesMan As System.Windows.Forms.ComboBox
    Friend WithEvents RSalesMan As System.Windows.Forms.RadioButton
    Friend WithEvents combState As System.Windows.Forms.ComboBox
    Friend WithEvents RState As System.Windows.Forms.RadioButton
    Friend WithEvents txtClientNameSearch As System.Windows.Forms.TextBox
    Friend WithEvents RAll As System.Windows.Forms.RadioButton
    Friend WithEvents btnsearch As System.Windows.Forms.Button
    Friend WithEvents RMedRepres As System.Windows.Forms.RadioButton
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Column0 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewButtonColumn
End Class
