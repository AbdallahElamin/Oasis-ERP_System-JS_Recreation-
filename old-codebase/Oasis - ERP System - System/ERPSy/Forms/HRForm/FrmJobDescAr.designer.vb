<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmJobDescAr
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmJobDescAr))
        Me.Button3 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.ListItem = New System.Windows.Forms.ListBox
        Me.txtJobDesc = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(9, 227)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(74, 23)
        Me.Button3.TabIndex = 14
        Me.Button3.Text = "إغلاق"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(231, 227)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(74, 23)
        Me.Button2.TabIndex = 13
        Me.Button2.Text = "مسح"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(9, 7)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(74, 23)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "إضافة"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ListItem
        '
        Me.ListItem.FormattingEnabled = True
        Me.ListItem.Location = New System.Drawing.Point(9, 35)
        Me.ListItem.Name = "ListItem"
        Me.ListItem.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ListItem.Size = New System.Drawing.Size(296, 186)
        Me.ListItem.TabIndex = 12
        '
        'txtJobDesc
        '
        Me.txtJobDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJobDesc.Location = New System.Drawing.Point(90, 9)
        Me.txtJobDesc.Name = "txtJobDesc"
        Me.txtJobDesc.Size = New System.Drawing.Size(215, 20)
        Me.txtJobDesc.TabIndex = 10
        '
        'FrmJobDescAr
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(310, 257)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ListItem)
        Me.Controls.Add(Me.txtJobDesc)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmJobDescAr"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "الوصف الوظيفي "
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ListItem As System.Windows.Forms.ListBox
    Friend WithEvents txtJobDesc As System.Windows.Forms.TextBox
End Class
