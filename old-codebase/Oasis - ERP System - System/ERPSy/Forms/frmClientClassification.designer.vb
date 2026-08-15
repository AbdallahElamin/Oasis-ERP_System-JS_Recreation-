<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmClientClassification
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmClientClassification))
        Me.txtClieClassName = New System.Windows.Forms.TextBox()
        Me.ListClientClassification = New System.Windows.Forms.ListView()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtClieClassName
        '
        Me.txtClieClassName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtClieClassName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtClieClassName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClieClassName.Location = New System.Drawing.Point(55, 8)
        Me.txtClieClassName.Name = "txtClieClassName"
        Me.txtClieClassName.Size = New System.Drawing.Size(184, 20)
        Me.txtClieClassName.TabIndex = 0
        '
        'ListClientClassification
        '
        Me.ListClientClassification.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3})
        Me.ListClientClassification.FullRowSelect = True
        Me.ListClientClassification.GridLines = True
        Me.ListClientClassification.HideSelection = False
        Me.ListClientClassification.Location = New System.Drawing.Point(7, 36)
        Me.ListClientClassification.Name = "ListClientClassification"
        Me.ListClientClassification.Size = New System.Drawing.Size(312, 332)
        Me.ListClientClassification.TabIndex = 2
        Me.ListClientClassification.UseCompatibleStateImageBehavior = False
        Me.ListClientClassification.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Client Classification"
        Me.ColumnHeader3.Width = 296
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(2, 11)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(53, 13)
        Me.Label11.TabIndex = 42
        Me.Label11.Text = "Add New "
        '
        'btnClose
        '
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(245, 374)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(74, 23)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(245, 8)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(74, 23)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Add"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(7, 374)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(74, 23)
        Me.btnRemove.TabIndex = 43
        Me.btnRemove.Text = "Delete"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'frmClientClassification
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(326, 405)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtClieClassName)
        Me.Controls.Add(Me.ListClientClassification)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(342, 443)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(342, 443)
        Me.Name = "frmClientClassification"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Client Classification"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtClieClassName As System.Windows.Forms.TextBox
    Friend WithEvents ListClientClassification As System.Windows.Forms.ListView
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
End Class
