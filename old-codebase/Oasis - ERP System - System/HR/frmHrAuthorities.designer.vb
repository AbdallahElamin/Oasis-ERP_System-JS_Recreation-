<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHrAuthorities
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHrAuthorities))
        Me.ChHR = New System.Windows.Forms.CheckBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.ChkVReq = New System.Windows.Forms.CheckBox
        Me.ChkSupApproval = New System.Windows.Forms.CheckBox
        Me.ChkLP = New System.Windows.Forms.CheckBox
        Me.ChkHRApproval = New System.Windows.Forms.CheckBox
        Me.ChPSht = New System.Windows.Forms.CheckBox
        Me.ChNewJb = New System.Windows.Forms.CheckBox
        Me.ChCont = New System.Windows.Forms.CheckBox
        Me.ChSP = New System.Windows.Forms.CheckBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.CombUser = New System.Windows.Forms.ComboBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ChHR
        '
        Me.ChHR.AutoSize = True
        Me.ChHR.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChHR.Location = New System.Drawing.Point(23, 284)
        Me.ChHR.Name = "ChHR"
        Me.ChHR.Size = New System.Drawing.Size(231, 20)
        Me.ChHR.TabIndex = 25
        Me.ChHR.Text = "Human Resources Authorities"
        Me.ChHR.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.ChkVReq)
        Me.GroupBox3.Controls.Add(Me.ChkSupApproval)
        Me.GroupBox3.Controls.Add(Me.ChkLP)
        Me.GroupBox3.Controls.Add(Me.ChkHRApproval)
        Me.GroupBox3.Controls.Add(Me.ChPSht)
        Me.GroupBox3.Controls.Add(Me.ChNewJb)
        Me.GroupBox3.Controls.Add(Me.ChCont)
        Me.GroupBox3.Controls.Add(Me.ChSP)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 70)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(620, 169)
        Me.GroupBox3.TabIndex = 21
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Privileges"
        '
        'ChkVReq
        '
        Me.ChkVReq.AutoSize = True
        Me.ChkVReq.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkVReq.Location = New System.Drawing.Point(230, 19)
        Me.ChkVReq.Name = "ChkVReq"
        Me.ChkVReq.Size = New System.Drawing.Size(288, 20)
        Me.ChkVReq.TabIndex = 22
        Me.ChkVReq.Text = "Request Vacation and Resuming Duty"
        Me.ChkVReq.UseVisualStyleBackColor = True
        '
        'ChkSupApproval
        '
        Me.ChkSupApproval.AutoSize = True
        Me.ChkSupApproval.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkSupApproval.Location = New System.Drawing.Point(230, 57)
        Me.ChkSupApproval.Name = "ChkSupApproval"
        Me.ChkSupApproval.Size = New System.Drawing.Size(372, 20)
        Me.ChkSupApproval.TabIndex = 21
        Me.ChkSupApproval.Text = "Supervisor Vacation and Resuming Duty Approval"
        Me.ChkSupApproval.UseVisualStyleBackColor = True
        '
        'ChkLP
        '
        Me.ChkLP.AutoSize = True
        Me.ChkLP.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkLP.Location = New System.Drawing.Point(230, 125)
        Me.ChkLP.Name = "ChkLP"
        Me.ChkLP.Size = New System.Drawing.Size(105, 20)
        Me.ChkLP.TabIndex = 20
        Me.ChkLP.Text = "Leave Plan"
        Me.ChkLP.UseVisualStyleBackColor = True
        '
        'ChkHRApproval
        '
        Me.ChkHRApproval.AutoSize = True
        Me.ChkHRApproval.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkHRApproval.Location = New System.Drawing.Point(230, 90)
        Me.ChkHRApproval.Name = "ChkHRApproval"
        Me.ChkHRApproval.Size = New System.Drawing.Size(326, 20)
        Me.ChkHRApproval.TabIndex = 16
        Me.ChkHRApproval.Text = "HR Vacations And Resuming duty Approval"
        Me.ChkHRApproval.UseVisualStyleBackColor = True
        '
        'ChPSht
        '
        Me.ChPSht.AutoSize = True
        Me.ChPSht.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChPSht.Location = New System.Drawing.Point(17, 125)
        Me.ChPSht.Name = "ChPSht"
        Me.ChPSht.Size = New System.Drawing.Size(98, 20)
        Me.ChPSht.TabIndex = 15
        Me.ChPSht.Text = "Pay Sheet"
        Me.ChPSht.UseVisualStyleBackColor = True
        '
        'ChNewJb
        '
        Me.ChNewJb.AutoSize = True
        Me.ChNewJb.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChNewJb.Location = New System.Drawing.Point(17, 90)
        Me.ChNewJb.Name = "ChNewJb"
        Me.ChNewJb.Size = New System.Drawing.Size(128, 20)
        Me.ChNewJb.TabIndex = 13
        Me.ChNewJb.Text = "Open New Job"
        Me.ChNewJb.UseVisualStyleBackColor = True
        '
        'ChCont
        '
        Me.ChCont.AutoSize = True
        Me.ChCont.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChCont.Location = New System.Drawing.Point(17, 57)
        Me.ChCont.Name = "ChCont"
        Me.ChCont.Size = New System.Drawing.Size(92, 20)
        Me.ChCont.TabIndex = 11
        Me.ChCont.Text = "Contracts"
        Me.ChCont.UseVisualStyleBackColor = True
        '
        'ChSP
        '
        Me.ChSP.AutoSize = True
        Me.ChSP.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChSP.Location = New System.Drawing.Point(17, 24)
        Me.ChSP.Name = "ChSP"
        Me.ChSP.Size = New System.Drawing.Size(115, 20)
        Me.ChSP.TabIndex = 9
        Me.ChSP.Text = "Staff Profiles"
        Me.ChSP.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.CombUser)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 21)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(620, 43)
        Me.GroupBox1.TabIndex = 20
        Me.GroupBox1.TabStop = False
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
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(551, 284)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 32)
        Me.Button2.TabIndex = 23
        Me.Button2.Text = "Close"
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(6, 274)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(620, 4)
        Me.GroupBox2.TabIndex = 24
        Me.GroupBox2.TabStop = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(446, 284)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 32)
        Me.Button1.TabIndex = 22
        Me.Button1.Text = "Save"
        '
        'frmHrAuthorities
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(631, 328)
        Me.Controls.Add(Me.ChHR)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Button1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmHrAuthorities"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Human Resources Authorities"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ChHR As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents ChkHRApproval As System.Windows.Forms.CheckBox
    Friend WithEvents ChPSht As System.Windows.Forms.CheckBox
    Friend WithEvents ChNewJb As System.Windows.Forms.CheckBox
    Friend WithEvents ChCont As System.Windows.Forms.CheckBox
    Friend WithEvents ChSP As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CombUser As System.Windows.Forms.ComboBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ChkSupApproval As System.Windows.Forms.CheckBox
    Friend WithEvents ChkLP As System.Windows.Forms.CheckBox
    Friend WithEvents ChkVReq As System.Windows.Forms.CheckBox
End Class
