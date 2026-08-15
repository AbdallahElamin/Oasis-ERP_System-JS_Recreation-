<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPaySheet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPaySheet))
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button31 = New System.Windows.Forms.Button
        Me.TxtNetSalary = New System.Windows.Forms.TextBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.txtTkafol = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.txtKitching = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.txtOther = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtSalaryAdvance = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.TxtInsurance2 = New System.Windows.Forms.TextBox
        Me.txtInsurance = New System.Windows.Forms.TextBox
        Me.txtIncomeTax = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtStampTax = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.TxtZakat = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtAward = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
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
        Me.txtHouse = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.txtCola = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.txtGrossSalary = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtEmpName = New System.Windows.Forms.TextBox
        Me.Button3 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.TxtEmpNo = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtBasicSalary = New System.Windows.Forms.TextBox
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtJobTitle = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.ComboLevels = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.TxtTotalDeduct = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(482, 397)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(85, 32)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Close"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button31
        '
        Me.Button31.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button31.Enabled = False
        Me.Button31.Location = New System.Drawing.Point(365, 397)
        Me.Button31.Name = "Button31"
        Me.Button31.Size = New System.Drawing.Size(85, 32)
        Me.Button31.TabIndex = 3
        Me.Button31.Text = "Save"
        Me.Button31.UseVisualStyleBackColor = True
        '
        'TxtNetSalary
        '
        Me.TxtNetSalary.BackColor = System.Drawing.Color.Black
        Me.TxtNetSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtNetSalary.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNetSalary.ForeColor = System.Drawing.Color.Lime
        Me.TxtNetSalary.Location = New System.Drawing.Point(476, 18)
        Me.TxtNetSalary.Name = "TxtNetSalary"
        Me.TxtNetSalary.ReadOnly = True
        Me.TxtNetSalary.Size = New System.Drawing.Size(74, 21)
        Me.TxtNetSalary.TabIndex = 320
        Me.TxtNetSalary.Text = "0.00"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtTkafol)
        Me.GroupBox3.Controls.Add(Me.Label19)
        Me.GroupBox3.Controls.Add(Me.txtKitching)
        Me.GroupBox3.Controls.Add(Me.Label18)
        Me.GroupBox3.Controls.Add(Me.txtOther)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.txtSalaryAdvance)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.TxtInsurance2)
        Me.GroupBox3.Controls.Add(Me.txtInsurance)
        Me.GroupBox3.Controls.Add(Me.txtIncomeTax)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.txtStampTax)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.TxtZakat)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 214)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(560, 101)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Deductions"
        '
        'txtTkafol
        '
        Me.txtTkafol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTkafol.Location = New System.Drawing.Point(279, 71)
        Me.txtTkafol.Name = "txtTkafol"
        Me.txtTkafol.Size = New System.Drawing.Size(75, 20)
        Me.txtTkafol.TabIndex = 7
        Me.txtTkafol.Text = "0.00"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(234, 74)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(43, 13)
        Me.Label19.TabIndex = 322
        Me.Label19.Text = "Tkafol :"
        '
        'txtKitching
        '
        Me.txtKitching.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKitching.Location = New System.Drawing.Point(100, 69)
        Me.txtKitching.Name = "txtKitching"
        Me.txtKitching.Size = New System.Drawing.Size(75, 20)
        Me.txtKitching.TabIndex = 6
        Me.txtKitching.Text = "0.00"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(47, 72)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(51, 13)
        Me.Label18.TabIndex = 320
        Me.Label18.Text = "Kitching :"
        '
        'txtOther
        '
        Me.txtOther.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOther.Location = New System.Drawing.Point(475, 69)
        Me.txtOther.Name = "txtOther"
        Me.txtOther.Size = New System.Drawing.Size(75, 20)
        Me.txtOther.TabIndex = 8
        Me.txtOther.Text = "0.00"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(431, 73)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(42, 13)
        Me.Label16.TabIndex = 318
        Me.Label16.Text = "Other :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(385, 46)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(89, 13)
        Me.Label11.TabIndex = 316
        Me.Label11.Text = "Salary Advance :"
        '
        'txtSalaryAdvance
        '
        Me.txtSalaryAdvance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalaryAdvance.Location = New System.Drawing.Point(476, 43)
        Me.txtSalaryAdvance.Name = "txtSalaryAdvance"
        Me.txtSalaryAdvance.Size = New System.Drawing.Size(74, 20)
        Me.txtSalaryAdvance.TabIndex = 5
        Me.txtSalaryAdvance.Text = "0.00"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(186, 19)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(92, 13)
        Me.Label15.TabIndex = 314
        Me.Label15.Text = "Social Insurance :"
        '
        'TxtInsurance2
        '
        Me.TxtInsurance2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtInsurance2.Location = New System.Drawing.Point(279, 17)
        Me.TxtInsurance2.Name = "TxtInsurance2"
        Me.TxtInsurance2.Size = New System.Drawing.Size(74, 20)
        Me.TxtInsurance2.TabIndex = 1
        Me.TxtInsurance2.Text = "0.00"
        '
        'txtInsurance
        '
        Me.txtInsurance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInsurance.Location = New System.Drawing.Point(100, 43)
        Me.txtInsurance.Name = "txtInsurance"
        Me.txtInsurance.Size = New System.Drawing.Size(75, 20)
        Me.txtInsurance.TabIndex = 3
        Me.txtInsurance.Text = "0.00"
        '
        'txtIncomeTax
        '
        Me.txtIncomeTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIncomeTax.Location = New System.Drawing.Point(100, 17)
        Me.txtIncomeTax.Name = "txtIncomeTax"
        Me.txtIncomeTax.Size = New System.Drawing.Size(75, 20)
        Me.txtIncomeTax.TabIndex = 0
        Me.txtIncomeTax.Text = "0.00"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(1, 47)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(99, 13)
        Me.Label6.TabIndex = 310
        Me.Label6.Text = " Health Insurance :"
        '
        'txtStampTax
        '
        Me.txtStampTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStampTax.Location = New System.Drawing.Point(279, 43)
        Me.txtStampTax.Name = "txtStampTax"
        Me.txtStampTax.Size = New System.Drawing.Size(74, 20)
        Me.txtStampTax.TabIndex = 4
        Me.txtStampTax.Text = "0.50"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(207, 45)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(71, 13)
        Me.Label8.TabIndex = 306
        Me.Label8.Text = "  Stamp Tax :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(30, 19)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 13)
        Me.Label9.TabIndex = 304
        Me.Label9.Text = "Income Tax :"
        '
        'TxtZakat
        '
        Me.TxtZakat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtZakat.Location = New System.Drawing.Point(476, 17)
        Me.TxtZakat.Name = "TxtZakat"
        Me.TxtZakat.Size = New System.Drawing.Size(74, 20)
        Me.TxtZakat.TabIndex = 2
        Me.TxtZakat.Text = "0.00"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(433, 20)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 13)
        Me.Label7.TabIndex = 308
        Me.Label7.Text = "Zakat :"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtAward)
        Me.GroupBox2.Controls.Add(Me.Label17)
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
        Me.GroupBox2.Controls.Add(Me.txtHouse)
        Me.GroupBox2.Controls.Add(Me.Label26)
        Me.GroupBox2.Controls.Add(Me.txtCola)
        Me.GroupBox2.Controls.Add(Me.Label25)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 114)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(560, 98)
        Me.GroupBox2.TabIndex = 318
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Allowances"
        '
        'txtAward
        '
        Me.txtAward.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAward.Location = New System.Drawing.Point(475, 72)
        Me.txtAward.Name = "txtAward"
        Me.txtAward.ReadOnly = True
        Me.txtAward.Size = New System.Drawing.Size(75, 20)
        Me.txtAward.TabIndex = 327
        Me.txtAward.Text = "0.00"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(428, 75)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(45, 13)
        Me.Label17.TabIndex = 328
        Me.Label17.Text = "Award :" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'txtUniform
        '
        Me.txtUniform.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUniform.Location = New System.Drawing.Point(279, 68)
        Me.txtUniform.Name = "txtUniform"
        Me.txtUniform.ReadOnly = True
        Me.txtUniform.Size = New System.Drawing.Size(74, 20)
        Me.txtUniform.TabIndex = 297
        Me.txtUniform.Text = "0.00"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(226, 71)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(51, 13)
        Me.Label32.TabIndex = 298
        Me.Label32.Text = "Uniform :"
        '
        'txtMeal
        '
        Me.txtMeal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMeal.Location = New System.Drawing.Point(97, 70)
        Me.txtMeal.Name = "txtMeal"
        Me.txtMeal.ReadOnly = True
        Me.txtMeal.Size = New System.Drawing.Size(75, 20)
        Me.txtMeal.TabIndex = 295
        Me.txtMeal.Text = "0.00"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(8, 73)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(87, 13)
        Me.Label31.TabIndex = 296
        Me.Label31.Text = "Meal Allowance :"
        '
        'txtOnCall
        '
        Me.txtOnCall.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOnCall.Location = New System.Drawing.Point(279, 42)
        Me.txtOnCall.Name = "txtOnCall"
        Me.txtOnCall.ReadOnly = True
        Me.txtOnCall.Size = New System.Drawing.Size(74, 20)
        Me.txtOnCall.TabIndex = 293
        Me.txtOnCall.Text = "0.00"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(229, 45)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(48, 13)
        Me.Label30.TabIndex = 294
        Me.Label30.Text = "On Call :"
        '
        'txtMedical
        '
        Me.txtMedical.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMedical.Location = New System.Drawing.Point(475, 46)
        Me.txtMedical.Name = "txtMedical"
        Me.txtMedical.ReadOnly = True
        Me.txtMedical.Size = New System.Drawing.Size(75, 20)
        Me.txtMedical.TabIndex = 291
        Me.txtMedical.Text = "0.00"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(373, 49)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(100, 13)
        Me.Label29.TabIndex = 292
        Me.Label29.Text = "Medical Allowance :"
        '
        'txtTransportation
        '
        Me.txtTransportation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransportation.Location = New System.Drawing.Point(97, 44)
        Me.txtTransportation.Name = "txtTransportation"
        Me.txtTransportation.ReadOnly = True
        Me.txtTransportation.Size = New System.Drawing.Size(75, 20)
        Me.txtTransportation.TabIndex = 289
        Me.txtTransportation.Text = "0.00"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(33, 47)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(61, 13)
        Me.Label28.TabIndex = 290
        Me.Label28.Text = "Transport :"
        '
        'txtHospitality
        '
        Me.txtHospitality.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHospitality.Location = New System.Drawing.Point(475, 21)
        Me.txtHospitality.Name = "txtHospitality"
        Me.txtHospitality.ReadOnly = True
        Me.txtHospitality.Size = New System.Drawing.Size(75, 20)
        Me.txtHospitality.TabIndex = 287
        Me.txtHospitality.Text = "0.00"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(409, 24)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(64, 13)
        Me.Label27.TabIndex = 288
        Me.Label27.Text = "Hospitality :"
        '
        'txtHouse
        '
        Me.txtHouse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHouse.Location = New System.Drawing.Point(279, 17)
        Me.txtHouse.Name = "txtHouse"
        Me.txtHouse.ReadOnly = True
        Me.txtHouse.Size = New System.Drawing.Size(74, 20)
        Me.txtHouse.TabIndex = 285
        Me.txtHouse.Text = "0.00"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(188, 20)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(89, 13)
        Me.Label26.TabIndex = 286
        Me.Label26.Text = "Accommodation :"
        '
        'txtCola
        '
        Me.txtCola.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCola.Location = New System.Drawing.Point(97, 19)
        Me.txtCola.Name = "txtCola"
        Me.txtCola.ReadOnly = True
        Me.txtCola.Size = New System.Drawing.Size(75, 20)
        Me.txtCola.TabIndex = 283
        Me.txtCola.Text = "0.00"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(59, 22)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(35, 13)
        Me.Label25.TabIndex = 284
        Me.Label25.Text = "Cola :"
        '
        'txtGrossSalary
        '
        Me.txtGrossSalary.BackColor = System.Drawing.Color.Black
        Me.txtGrossSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGrossSalary.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGrossSalary.ForeColor = System.Drawing.Color.Lime
        Me.txtGrossSalary.Location = New System.Drawing.Point(100, 18)
        Me.txtGrossSalary.Name = "txtGrossSalary"
        Me.txtGrossSalary.ReadOnly = True
        Me.txtGrossSalary.Size = New System.Drawing.Size(75, 21)
        Me.txtGrossSalary.TabIndex = 315
        Me.txtGrossSalary.Text = "0.00"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(14, 21)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(84, 13)
        Me.Label14.TabIndex = 316
        Me.Label14.Text = "Gross Salary :"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtEmpName)
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Controls.Add(Me.Button4)
        Me.GroupBox1.Controls.Add(Me.TxtEmpNo)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtBasicSalary)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtJobTitle)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.ComboLevels)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(560, 106)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Employee Details"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(9, 19)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(80, 13)
        Me.Label13.TabIndex = 330
        Me.Label13.Text = "Employee No. :"
        '
        'txtEmpName
        '
        Me.txtEmpName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpName.Location = New System.Drawing.Point(91, 43)
        Me.txtEmpName.Name = "txtEmpName"
        Me.txtEmpName.ReadOnly = True
        Me.txtEmpName.Size = New System.Drawing.Size(186, 20)
        Me.txtEmpName.TabIndex = 299
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(194, 15)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(36, 23)
        Me.Button3.TabIndex = 328
        Me.Button3.Text = "=>"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(241, 15)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(36, 23)
        Me.Button4.TabIndex = 329
        Me.Button4.Text = "..."
        Me.Button4.UseVisualStyleBackColor = True
        '
        'TxtEmpNo
        '
        Me.TxtEmpNo.BackColor = System.Drawing.Color.Black
        Me.TxtEmpNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtEmpNo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEmpNo.ForeColor = System.Drawing.Color.Lime
        Me.TxtEmpNo.Location = New System.Drawing.Point(92, 16)
        Me.TxtEmpNo.Name = "TxtEmpNo"
        Me.TxtEmpNo.Size = New System.Drawing.Size(96, 21)
        Me.TxtEmpNo.TabIndex = 0
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(333, 50)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(44, 13)
        Me.Label12.TabIndex = 312
        Me.Label12.Text = "Month :"
        '
        'txtBasicSalary
        '
        Me.txtBasicSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBasicSalary.Location = New System.Drawing.Point(379, 75)
        Me.txtBasicSalary.Name = "txtBasicSalary"
        Me.txtBasicSalary.Size = New System.Drawing.Size(80, 20)
        Me.txtBasicSalary.TabIndex = 2
        Me.txtBasicSalary.Text = "0.00"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CalendarFont = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1.Checked = False
        Me.DateTimePicker1.CustomFormat = "MMM/yyyy"
        Me.DateTimePicker1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(379, 46)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(171, 22)
        Me.DateTimePicker1.TabIndex = 311
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(323, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Job Title :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(306, 78)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 13)
        Me.Label4.TabIndex = 300
        Me.Label4.Text = "Basic Salary :"
        '
        'txtJobTitle
        '
        Me.txtJobTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJobTitle.Location = New System.Drawing.Point(379, 17)
        Me.txtJobTitle.Name = "txtJobTitle"
        Me.txtJobTitle.ReadOnly = True
        Me.txtJobTitle.Size = New System.Drawing.Size(171, 20)
        Me.txtJobTitle.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Grade Level :"
        '
        'ComboLevels
        '
        Me.ComboLevels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboLevels.FormattingEnabled = True
        Me.ComboLevels.Location = New System.Drawing.Point(90, 74)
        Me.ComboLevels.Name = "ComboLevels"
        Me.ComboLevels.Size = New System.Drawing.Size(187, 21)
        Me.ComboLevels.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(-1, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Employee Name :"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(486, 321)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 21)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Calculate"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TxtTotalDeduct
        '
        Me.TxtTotalDeduct.BackColor = System.Drawing.Color.Black
        Me.TxtTotalDeduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtTotalDeduct.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTotalDeduct.ForeColor = System.Drawing.Color.Lime
        Me.TxtTotalDeduct.Location = New System.Drawing.Point(278, 18)
        Me.TxtTotalDeduct.Name = "TxtTotalDeduct"
        Me.TxtTotalDeduct.ReadOnly = True
        Me.TxtTotalDeduct.Size = New System.Drawing.Size(75, 21)
        Me.TxtTotalDeduct.TabIndex = 324
        Me.TxtTotalDeduct.Text = "0.00"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(190, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(85, 13)
        Me.Label5.TabIndex = 325
        Me.Label5.Text = "Total Deduct :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(402, 22)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(71, 13)
        Me.Label10.TabIndex = 326
        Me.Label10.Text = "Net Salary :"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label10)
        Me.GroupBox4.Controls.Add(Me.TxtNetSalary)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.Label14)
        Me.GroupBox4.Controls.Add(Me.txtGrossSalary)
        Me.GroupBox4.Controls.Add(Me.TxtTotalDeduct)
        Me.GroupBox4.Location = New System.Drawing.Point(6, 341)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(560, 50)
        Me.GroupBox4.TabIndex = 327
        Me.GroupBox4.TabStop = False
        '
        'frmPaySheet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(573, 434)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button31)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmPaySheet"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Set New Salary"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button31 As System.Windows.Forms.Button
    Friend WithEvents TxtNetSalary As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents TxtInsurance2 As System.Windows.Forms.TextBox
    Friend WithEvents txtInsurance As System.Windows.Forms.TextBox
    Friend WithEvents txtIncomeTax As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TxtZakat As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtStampTax As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtGrossSalary As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
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
    Friend WithEvents txtHouse As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtCola As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtBasicSalary As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtJobTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ComboLevels As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TxtTotalDeduct As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtSalaryAdvance As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtEmpNo As System.Windows.Forms.TextBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents txtEmpName As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtOther As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtAward As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtKitching As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtTkafol As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
End Class
