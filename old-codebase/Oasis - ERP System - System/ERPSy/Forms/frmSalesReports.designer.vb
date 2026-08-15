<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSalesReports
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSalesReports))
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.RItem = New System.Windows.Forms.RadioButton()
        Me.RMonth = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.DTPBTo = New System.Windows.Forms.DateTimePicker()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.DTPBFrom = New System.Windows.Forms.DateTimePicker()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox6
        '
        Me.GroupBox6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox6.Controls.Add(Me.RItem)
        Me.GroupBox6.Controls.Add(Me.RMonth)
        Me.GroupBox6.Location = New System.Drawing.Point(9, 4)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(542, 46)
        Me.GroupBox6.TabIndex = 73
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "1) Analysis Type"
        '
        'RItem
        '
        Me.RItem.AutoSize = True
        Me.RItem.Checked = True
        Me.RItem.Location = New System.Drawing.Point(20, 19)
        Me.RItem.Name = "RItem"
        Me.RItem.Size = New System.Drawing.Size(62, 17)
        Me.RItem.TabIndex = 0
        Me.RItem.TabStop = True
        Me.RItem.Text = "By Item"
        Me.RItem.UseVisualStyleBackColor = True
        '
        'RMonth
        '
        Me.RMonth.AutoSize = True
        Me.RMonth.Location = New System.Drawing.Point(168, 19)
        Me.RMonth.Name = "RMonth"
        Me.RMonth.Size = New System.Drawing.Size(70, 17)
        Me.RMonth.TabIndex = 1
        Me.RMonth.Text = "By Month"
        Me.RMonth.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.DTPBTo)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.DTPBFrom)
        Me.GroupBox3.Location = New System.Drawing.Point(9, 53)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(542, 52)
        Me.GroupBox3.TabIndex = 72
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "2) Period"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Label10.Location = New System.Drawing.Point(296, 22)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(19, 13)
        Me.Label10.TabIndex = 132
        Me.Label10.Text = "To"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DTPBTo
        '
        Me.DTPBTo.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.DTPBTo.Location = New System.Drawing.Point(321, 18)
        Me.DTPBTo.Name = "DTPBTo"
        Me.DTPBTo.Size = New System.Drawing.Size(208, 20)
        Me.DTPBTo.TabIndex = 1
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.Label11.Location = New System.Drawing.Point(17, 22)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(31, 13)
        Me.Label11.TabIndex = 130
        Me.Label11.Text = "From"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DTPBFrom
        '
        Me.DTPBFrom.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.DTPBFrom.Location = New System.Drawing.Point(54, 18)
        Me.DTPBFrom.Name = "DTPBFrom"
        Me.DTPBFrom.Size = New System.Drawing.Size(208, 20)
        Me.DTPBFrom.TabIndex = 0
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(150, 121)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(75, 32)
        Me.btnShow.TabIndex = 138
        Me.btnShow.Text = "Show"
        '
        'GroupBox5
        '
        Me.GroupBox5.Location = New System.Drawing.Point(9, 107)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(542, 8)
        Me.GroupBox5.TabIndex = 140
        Me.GroupBox5.TabStop = False
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(335, 121)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 32)
        Me.btnClose.TabIndex = 139
        Me.btnClose.Text = "Close"
        '
        'frmSalesReports
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(561, 158)
        Me.Controls.Add(Me.btnShow)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox3)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(577, 196)
        Me.MinimumSize = New System.Drawing.Size(577, 196)
        Me.Name = "frmSalesReports"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sales Analysis"
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents RItem As System.Windows.Forms.RadioButton
    Friend WithEvents RMonth As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents DTPBTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents DTPBFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
End Class
