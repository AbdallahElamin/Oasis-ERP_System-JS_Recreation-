<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGradeLevelAllowances
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGradeLevelAllowances))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.comboLevel = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Button31 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtUniform = New System.Windows.Forms.TextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.txtMeal = New System.Windows.Forms.TextBox
        Me.Label31 = New System.Windows.Forms.Label
        Me.txtOnCall = New System.Windows.Forms.TextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.txtMedical = New System.Windows.Forms.TextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.txtTransportation = New System.Windows.Forms.TextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.txtHospitality = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.txtAccommodation = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.txtCola = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.comboLevel)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(491, 52)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "New"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(210, 18)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(38, 24)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "+"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'comboLevel
        '
        Me.comboLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboLevel.FormattingEnabled = True
        Me.comboLevel.Location = New System.Drawing.Point(83, 18)
        Me.comboLevel.Name = "comboLevel"
        Me.comboLevel.Size = New System.Drawing.Size(121, 21)
        Me.comboLevel.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Grade Level :"
        '
        'Button31
        '
        Me.Button31.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button31.Location = New System.Drawing.Point(291, 167)
        Me.Button31.Name = "Button31"
        Me.Button31.Size = New System.Drawing.Size(85, 32)
        Me.Button31.TabIndex = 2
        Me.Button31.Text = "Save"
        Me.Button31.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(411, 167)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(85, 32)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Close"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtUniform)
        Me.GroupBox2.Controls.Add(Me.Label32)
        Me.GroupBox2.Controls.Add(Me.txtMeal)
        Me.GroupBox2.Controls.Add(Me.Label31)
        Me.GroupBox2.Controls.Add(Me.txtOnCall)
        Me.GroupBox2.Controls.Add(Me.Label30)
        Me.GroupBox2.Controls.Add(Me.txtMedical)
        Me.GroupBox2.Controls.Add(Me.Label29)
        Me.GroupBox2.Controls.Add(Me.txtTransportation)
        Me.GroupBox2.Controls.Add(Me.Label28)
        Me.GroupBox2.Controls.Add(Me.txtHospitality)
        Me.GroupBox2.Controls.Add(Me.Label27)
        Me.GroupBox2.Controls.Add(Me.txtAccommodation)
        Me.GroupBox2.Controls.Add(Me.Label26)
        Me.GroupBox2.Controls.Add(Me.txtCola)
        Me.GroupBox2.Controls.Add(Me.Label25)
        Me.GroupBox2.Location = New System.Drawing.Point(5, 59)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(491, 102)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Allowances"
        '
        'txtUniform
        '
        Me.txtUniform.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUniform.Location = New System.Drawing.Point(262, 72)
        Me.txtUniform.Name = "txtUniform"
        Me.txtUniform.Size = New System.Drawing.Size(74, 20)
        Me.txtUniform.TabIndex = 7
        Me.txtUniform.Text = "0.00"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(209, 75)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(51, 13)
        Me.Label32.TabIndex = 282
        Me.Label32.Text = "Uniform :" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'txtMeal
        '
        Me.txtMeal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMeal.Location = New System.Drawing.Point(91, 72)
        Me.txtMeal.Name = "txtMeal"
        Me.txtMeal.Size = New System.Drawing.Size(75, 20)
        Me.txtMeal.TabIndex = 6
        Me.txtMeal.Text = "0.00"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(3, 75)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(87, 13)
        Me.Label31.TabIndex = 280
        Me.Label31.Text = "Meal Allowance :"
        '
        'txtOnCall
        '
        Me.txtOnCall.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOnCall.Location = New System.Drawing.Point(262, 45)
        Me.txtOnCall.Name = "txtOnCall"
        Me.txtOnCall.Size = New System.Drawing.Size(74, 20)
        Me.txtOnCall.TabIndex = 4
        Me.txtOnCall.Text = "0.00"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(212, 48)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(48, 13)
        Me.Label30.TabIndex = 278
        Me.Label30.Text = "On Call :"
        '
        'txtMedical
        '
        Me.txtMedical.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMedical.Location = New System.Drawing.Point(411, 46)
        Me.txtMedical.Name = "txtMedical"
        Me.txtMedical.Size = New System.Drawing.Size(75, 20)
        Me.txtMedical.TabIndex = 5
        Me.txtMedical.Text = "0.00"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(362, 48)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(49, 13)
        Me.Label29.TabIndex = 276
        Me.Label29.Text = "Medical :"
        '
        'txtTransportation
        '
        Me.txtTransportation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransportation.Location = New System.Drawing.Point(91, 45)
        Me.txtTransportation.Name = "txtTransportation"
        Me.txtTransportation.Size = New System.Drawing.Size(75, 20)
        Me.txtTransportation.TabIndex = 3
        Me.txtTransportation.Text = "0.00"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(29, 48)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(61, 13)
        Me.Label28.TabIndex = 274
        Me.Label28.Text = "Transport :"
        '
        'txtHospitality
        '
        Me.txtHospitality.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHospitality.Location = New System.Drawing.Point(411, 18)
        Me.txtHospitality.Name = "txtHospitality"
        Me.txtHospitality.Size = New System.Drawing.Size(75, 20)
        Me.txtHospitality.TabIndex = 2
        Me.txtHospitality.Text = "0.00"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(345, 21)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(64, 13)
        Me.Label27.TabIndex = 272
        Me.Label27.Text = "Hospitality :"
        '
        'txtAccommodation
        '
        Me.txtAccommodation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAccommodation.Location = New System.Drawing.Point(262, 18)
        Me.txtAccommodation.Name = "txtAccommodation"
        Me.txtAccommodation.Size = New System.Drawing.Size(74, 20)
        Me.txtAccommodation.TabIndex = 1
        Me.txtAccommodation.Text = "0.00"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(170, 21)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(89, 13)
        Me.Label26.TabIndex = 270
        Me.Label26.Text = "Accommodation :"
        '
        'txtCola
        '
        Me.txtCola.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCola.Location = New System.Drawing.Point(91, 18)
        Me.txtCola.Name = "txtCola"
        Me.txtCola.Size = New System.Drawing.Size(75, 20)
        Me.txtCola.TabIndex = 0
        Me.txtCola.Text = "0.00"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(54, 21)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(35, 13)
        Me.Label25.TabIndex = 268
        Me.Label25.Text = "Cola :"
        '
        'frmGradeLevelAllowances
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(506, 206)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button31)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmGradeLevelAllowances"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Allowances "
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents comboLevel As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button31 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtUniform As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtMeal As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtOnCall As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtMedical As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtTransportation As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtHospitality As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtAccommodation As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtCola As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
End Class
