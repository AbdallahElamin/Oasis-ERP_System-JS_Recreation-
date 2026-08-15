Imports System.Data.SqlClient

Public Class frmPaySheet

    Private Sub frmPaySheet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'FillComboEmpName()
        FillComboLevel()

    End Sub

    Sub ConvertToZero()
        Me.txtTkafol.Text = 0.0
        Me.txtUniform.Text = 0.0
        Me.txtSalaryAdvance.Text = 0.0
        Me.TxtInsurance2.Text = 0.0
        Me.txtHouse.Text = 0.0
        Me.txtCola.Text = 0.0
        Me.txtHospitality.Text = 0.0
        Me.txtMeal.Text = 0.0
        Me.txtMedical.Text = 0.0
        Me.txtOnCall.Text = 0.0
        Me.txtTransportation.Text = 0.0
        Me.txtBasicSalary.Text = 0.0
        Me.txtIncomeTax.Text = 0.0
        Me.TxtZakat.Text = 0.0
        Me.txtInsurance.Text = 0.0
        Me.txtGrossSalary.Text = 0.0
        Me.txtBasicSalary.Text = 0.0
        Me.TxtNetSalary.Text = 0.0
        Me.TxtTotalDeduct.Text = 0.0
        Me.txtOther.Text = 0.0
        Me.txtKitching.Text = 0.0
        Me.txtAward.Text = 0.0
    End Sub

    Sub FillComboLevel()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select distinct GradeLevel From Levels where GradeLevel is not null", cnn)
            Dim Reader As SqlDataReader

            Me.ComboLevels.Items.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Dim Item As New ComboBox
                Me.ComboLevels.Items.Add(Reader.Item(0))
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

    Sub clear()
        Me.TxtEmpNo.Clear()
        Me.txtEmpName.Clear()
        Me.ComboLevels.SelectedIndex = -1
        Me.txtJobTitle.Clear()
        ConvertToZero()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Sub FillAllowance()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select isnull(Cola,0)cola,isnull(Accommodation,0) Accommodation," & _
                                      "isnull(Hospitality,0) Hospitality,isnull(Transport,0) transport," & _
                                      "isnull(OnCall,0) OnCall,isnull(Medical,0) Medical,isnull(Meal,0) Meal," & _
                                      "isnull(Uniform,0) Uniform,isnull(Award,0)Award From GradeLevelAllowance where GradeLevel=N'" & Me.ComboLevels.SelectedItem & "'", cnn)
            Dim Reader As SqlDataReader
            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read

                Me.txtCola.Text = Reader.Item(0)
                Me.txtHouse.Text = Reader.Item(1)
                Me.txtHospitality.Text = Reader.Item(2)
                Me.txtTransportation.Text = Reader.Item(3)
                Me.txtOnCall.Text = Reader.Item(4)
                Me.txtMedical.Text = Reader.Item(5)
                Me.txtMeal.Text = Reader.Item(6)
                Me.txtUniform.Text = Reader.Item(7)
                Me.txtAward.Text = Reader.Item(8)
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

    Private Sub Button31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button31.Click
        If Me.TxtNetSalary.Text = 0 Then
            Exit Sub
        End If
        If Me.TxtEmpNo.Text.Trim.Trim.Length = 0 Then

            MsgBox("Please insert a valid employee number")
        ElseIf Me.ComboLevels.SelectedIndex = -1 Then
            MsgBox("Please select employee level")
        ElseIf Me.txtBasicSalary.Text = 0 Then
            MsgBox("Please Add the basic salary")

        Else
            Try

                Me.Cursor = Cursors.WaitCursor

                Dim cmd1 As New SqlCommand("select count(*) from EmployeeSalary where EmpID='" & Me.TxtEmpNo.Text & _
                                         "' and Month(Month)=" & _
                                         CInt(Me.DateTimePicker1.Value.Month) & " and Year(Month)=" & _
                                         CInt(Me.DateTimePicker1.Value.Year) & " and GradeLevel is not null", cnn)
                cnn.Open()
                If cmd1.ExecuteScalar > 0 Then
                    Dim cmd2 As New SqlCommand("delete from EmployeeSalary where EmpID='" & Me.TxtEmpNo.Text & _
                                           "'  and Month(Month)=" & _
                                           CInt(Me.DateTimePicker1.Value.Month) & " and Year(Month)=" & _
                                           CInt(Me.DateTimePicker1.Value.Year) & " and GradeLevel is not null", cnn)
                    cmd2.ExecuteNonQuery()
                End If
                cnn.Close()

                Dim cmd As New SqlCommand
                Dim Trans As SqlTransaction

                cnn.Open()
                cmd.Connection = cnn
                Trans = cnn.BeginTransaction
                cmd.Transaction = Trans

                cmd.CommandText = "insert  into EmployeeSalary ( EmpID, EmpName, JobTitle, BasicSalary, GradeLevel, Cola, Accommodation," & _
                                  " Hospitality, Transport, OnCall, Medical, Meal, Uniform, New, GrossSalary, IncomeTax, StampTax, " & _
                                  " Zakat, Insurance, Insurance2, SalaryAdvance,Other,Kitching,Tkafol, TotalDeduct, NetSalary, Month,CurrentUser) " & _
                                  "values('" & Me.TxtEmpNo.Text & "',N'" & Me.txtEmpName.Text & "',N'" & Me.txtJobTitle.Text & _
                                  "',N'" & CDbl(Me.txtBasicSalary.Text) & "' ,N'" & Me.ComboLevels.SelectedItem & "'," & CDbl(Me.txtCola.Text) & _
                                  "," & CDbl(Me.txtHouse.Text) & "," & CDbl(Me.txtHospitality.Text) & "," & CDbl(Me.txtTransportation.Text) & _
                                  "," & CDbl(Me.txtOnCall.Text) & "," & CDbl(Me.txtMedical.Text) & "," & CDbl(Me.txtMeal.Text) & _
                                  "," & CDbl(Me.txtUniform.Text) & "," & CDbl(Me.txtAward.Text) & "," & CDbl(Me.txtGrossSalary.Text) & _
                                  "," & CDbl(Me.txtIncomeTax.Text) & "," & CDbl(Me.txtStampTax.Text) & "," & CDbl(Me.TxtZakat.Text) & _
                                  "," & CDbl(Me.txtInsurance.Text) & "," & CDbl(Me.TxtInsurance2.Text) & "," & CDbl(Me.txtSalaryAdvance.Text) & _
                                  "," & CDbl(Me.txtOther.Text) & "," & CDbl(Me.txtKitching.Text) & "," & CDbl(Me.txtTkafol.Text) & "," & CDbl(Me.TxtTotalDeduct.Text) & "," & CDbl(Me.TxtNetSalary.Text) & _
                                  ",N'" & Me.DateTimePicker1.Value.ToString("MM / dd / yyyy") & "','" & CurrentUser & "')"

                cmd.ExecuteNonQuery()
                Trans.Commit()
                cnn.Close()
                TotalContribution()
                MsgBox("Saved Successfully")
                'PaySheet()
                clear()
                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                    MsgBox(ex.ToString)
                End If
            End Try
        End If
    End Sub


    Private Sub ComboLevels_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboLevels.SelectedIndexChanged
        FillAllowance()
        Me.txtGrossSalary.Text = 0.0
        Me.TxtTotalDeduct.Text = 0.0
        Me.TxtNetSalary.Text = 0.0
        If Me.ComboLevels.SelectedItem = "Special Contract" Or Me.ComboLevels.SelectedItem = "Level 7" _
        Or Me.ComboLevels.SelectedItem = "Level 6" Or Me.ComboLevels.SelectedItem = "Level 5" Then
            Me.txtTkafol.Text = 15.0
        ElseIf Me.ComboLevels.SelectedItem = "Level 4" Or Me.ComboLevels.SelectedItem = "Level 3" _
       Or Me.ComboLevels.SelectedItem = "Level 2" Then
            Me.txtTkafol.Text = 10.0
        ElseIf Me.ComboLevels.SelectedItem = "level 1" Then
            Me.txtTkafol.Text = 5.0


        End If
    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Me.txtBasicSalary.Text < 1 Then
            MsgBox("Please fill the basic Salary")
        ElseIf Me.txtEmpName.Text.Trim.Length = 0 Then
            MsgBox("Please select a valid employee number")
        ElseIf Me.ComboLevels.SelectedIndex = -1 Then
            MsgBox("Select grade level from the list")
        Else
            Try
                Dim a As Double

                a = (CDbl(Me.txtBasicSalary.Text) + CDbl(Me.txtCola.Text) + CDbl(Me.txtHouse.Text) + CDbl(Me.txtHospitality.Text) + _
                CDbl(Me.txtTransportation.Text) + CDbl(Me.txtOnCall.Text) + CDbl(Me.txtMedical.Text) + CDbl(Me.txtUniform.Text) + _
                CDbl(Me.txtMeal.Text)).ToString("N2")

                Me.TxtInsurance2.Text = (a * 0.08).ToString("N2")
                If a > 3500 Then
                    Me.TxtZakat.Text = (a - 3500) * 2.5 / 100
                ElseIf a < 3500 Then
                    Me.TxtZakat.Text = 0.0
                End If

                '(L11-74.95-36-50-20-P11-787.5)*15%+2.5

                If a > 1000 Then

                    If Me.txtTransportation.Text > 0 And Me.txtMeal.Text > 0 And Me.txtHospitality.Text > 0 Then
                        Me.txtIncomeTax.Text = (((a - 74.95 - 36.0 - 50.0 - 20 - (a * 0.08) - 787.5)) * (15 / 100) + 2.5).ToString("N2")
                        'ElseIf Me.txtTransportation.Text = 0 Then
                        '    Me.txtIncomeTax.Text = (((a - 74.95 - 50.0 - 20 - (a * 0.08) - 787.5)) * (15 / 100) + 2.5).ToString("N2")
                        'ElseIf Me.txtMeal.Text = 0 Then
                        '    Me.txtIncomeTax.Text = (((a - 74.95 - 36.0 - 20 - (a * 0.08) - 787.5)) * (15 / 100) + 2.5).ToString("N2")
                        'ElseIf Me.txtHospitality.Text = 0 Then
                        '    Me.txtIncomeTax.Text = (((a - 74.95 - 36.0 - 50.0 - (a * 0.08) - 787.5)) * (15 / 100) + 2.5).ToString("N2")
                        'ElseIf Me.txtTransportation.Text = 0 And Me.txtMeal.Text = 0 Then
                        '    Me.txtIncomeTax.Text = (((a - 74.95 - 20 - (a * 0.08) - 787.5)) * (15 / 100) + 2.5).ToString("N2")
                        'ElseIf Me.txtTransportation.Text = 0 And Me.txtHospitality.Text = 0 Then
                        '    Me.txtIncomeTax.Text = (((a - 74.95 - 50.0 - (a * 0.08) - 787.5)) * (15 / 100) + 2.5).ToString("N2")
                    ElseIf Me.txtMeal.Text = 0 And Me.txtHospitality.Text = 0 Then
                        Me.txtIncomeTax.Text = (((a - 74.95 - 36.0 - (a * 0.08) - 787.5)) * (15 / 100) + 2.5).ToString("N2")
                    End If

                ElseIf a < 1000 Then

                    Me.txtIncomeTax.Text = 0.0
                End If

                Me.TxtTotalDeduct.Text = (CDbl(Me.txtSalaryAdvance.Text) + CDbl(Me.txtIncomeTax.Text) + _
                                          CDbl(Me.txtStampTax.Text) + CDbl(Me.TxtZakat.Text) + _
                                          CDbl(Me.txtInsurance.Text) + CDbl(Me.TxtInsurance2.Text) + _
                                          CDbl(Me.txtKitching.Text) + CDbl(Me.txtOther.Text) + _
                                          CDbl(Me.txtTkafol.Text)).ToString("N2")


                If (a > 0 And a < 950) Or a = 950 Then
                    Me.txtAward.Text = 100.0
                ElseIf (a > 950 And a < 1500) Or a = 1500 Then
                    Me.txtAward.Text = 70.0
                ElseIf (a > 1500 And a < 2000) Or a = 2000 Then
                    Me.txtAward.Text = 50.0
                ElseIf a > 2000 Then
                    Me.txtAward.Text = 0

                End If


                Me.txtGrossSalary.Text = (a + CDbl(Me.txtAward.Text)).ToString("N2")
                Me.TxtNetSalary.Text = (CDbl(Me.txtGrossSalary.Text) - CDbl(Me.TxtTotalDeduct.Text)).ToString("N2")

                Me.Button31.Enabled = True
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub txtBasicSalary_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBasicSalary.TextChanged
        Me.TxtTotalDeduct.Text = 0
        Me.txtGrossSalary.Text = 0
        Me.TxtNetSalary.Text = 0
        Me.TxtZakat.Text = 0
        Me.TxtInsurance2.Text = 0
        Me.txtIncomeTax.Text = 0
        If Me.txtBasicSalary.Text = "" Then
            Me.txtBasicSalary.Text = 0
        End If

    End Sub

    Private Sub TxtZakat_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtZakat.TextChanged
        Me.TxtTotalDeduct.Text = 0
        Me.txtGrossSalary.Text = 0
        Me.TxtNetSalary.Text = 0
        Me.Button31.Enabled = False
    End Sub

    Private Sub txtStampTax_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStampTax.TextChanged
        Me.TxtTotalDeduct.Text = 0
        Me.txtGrossSalary.Text = 0
        Me.TxtNetSalary.Text = 0
        Me.Button31.Enabled = False
        If Me.txtStampTax.Text = "" Then
            Me.txtStampTax.Text = 0
        End If
    End Sub

    Private Sub txtIncomeTax_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIncomeTax.TextChanged
        Me.TxtTotalDeduct.Text = 0
        Me.txtGrossSalary.Text = 0
        Me.TxtNetSalary.Text = 0
        Me.Button31.Enabled = False
    End Sub

    Private Sub txtSalaryAdvance_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSalaryAdvance.TextChanged
        Me.TxtTotalDeduct.Text = 0
        Me.txtGrossSalary.Text = 0
        Me.TxtNetSalary.Text = 0
        Me.Button31.Enabled = False
        If Me.txtSalaryAdvance.Text = "" Then
            Me.txtSalaryAdvance.Text = 0
        End If
    End Sub

    Private Sub TxtInsurance2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtInsurance2.TextChanged
        Me.TxtTotalDeduct.Text = 0
        Me.txtGrossSalary.Text = 0
        Me.TxtNetSalary.Text = 0
        If Me.TxtInsurance2.Text = "" Then
            Me.TxtInsurance2.Text = 0
        End If
        Me.Button31.Enabled = False

    End Sub

    Private Sub txtInsurance_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInsurance.TextChanged
        Me.TxtTotalDeduct.Text = 0
        Me.txtGrossSalary.Text = 0
        Me.TxtNetSalary.Text = 0
        Me.Button31.Enabled = False
        If Me.txtInsurance.Text = "" Then
            Me.txtInsurance.Text = 0
        End If
    End Sub

    Private Sub TxtEmpNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtEmpNo.TextChanged
        ConvertToZero()
        Me.txtEmpName.Clear()
        Me.ComboLevels.SelectedIndex = -1
        Me.txtJobTitle.Clear()


    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If Me.TxtEmpNo.Text.Trim.Length > 0 Then
            FillData()
        End If
    End Sub

    Sub FillData()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select Name,jobDesc From StaffProfiles Where EmpID=N'" & Me.TxtEmpNo.Text & "'", cnn)
            Dim Reader As SqlDataReader

            Me.txtJobTitle.Clear()
            Me.txtEmpName.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read

                Me.txtEmpName.Text = Reader.Item("Name")
                Me.txtJobTitle.Text = Reader.Item("jobDesc")
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



    Private Sub TxtEmpNo_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtEmpNo.KeyUp
        If e.KeyCode = Keys.Enter Then
            If Me.TxtEmpNo.Text.Trim.Length > 0 Then
                FillData()
            End If
        End If

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.TxtEmpNo.Clear()
        Dim a As New frmSearchEmpID
        a.ShowDialog()

        If SelPatIDNo = "" Then
            Exit Sub
        End If

        Me.TxtEmpNo.Text = SelPatIDNo
        FillData()

    End Sub

    Private Sub txtBasicSalary_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBasicSalary.KeyPress, txtKitching.KeyPress

        If Char.IsDigit(e.KeyChar) _
       OrElse e.KeyChar = ","c _
           OrElse e.KeyChar = "."c OrElse _
          Char.GetUnicodeCategory(e.KeyChar) = Globalization.UnicodeCategory.Control OrElse _
                   Char.GetUnicodeCategory(e.KeyChar) _
               = Globalization.UnicodeCategory.CurrencySymbol _
                    OrElse Char.GetUnicodeCategory(e.KeyChar) = Globalization.UnicodeCategory.Format Then

            e.Handled = False

        Else
            e.Handled = True

        End If

    End Sub

    Private Sub txtInsurance_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStampTax.KeyPress, txtSalaryAdvance.KeyPress, txtInsurance.KeyPress, txtOther.KeyPress, txtTkafol.KeyPress
        If Char.IsDigit(e.KeyChar) _
       OrElse e.KeyChar = ","c _
           OrElse e.KeyChar = "."c OrElse _
          Char.GetUnicodeCategory(e.KeyChar) = Globalization.UnicodeCategory.Control OrElse _
                   Char.GetUnicodeCategory(e.KeyChar) _
               = Globalization.UnicodeCategory.CurrencySymbol _
                    OrElse Char.GetUnicodeCategory(e.KeyChar) = Globalization.UnicodeCategory.Format Then

            e.Handled = False

        Else
            e.Handled = True

        End If

    End Sub

    Private Sub txtBasicSalary_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBasicSalary.Validated
        Me.txtBasicSalary.Text = CDbl(Me.txtBasicSalary.Text).ToString("N2")
    End Sub

    Private Sub txtInsurance_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInsurance.Validated
        Me.txtInsurance.Text = CDbl(Me.txtInsurance.Text).ToString("N2")
    End Sub

    Private Sub txtSalaryAdvance_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSalaryAdvance.Validated
        Me.txtSalaryAdvance.Text = CDbl(Me.txtSalaryAdvance.Text).ToString("N2")
    End Sub

    Private Sub txtOther_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOther.TextChanged
        Me.TxtTotalDeduct.Text = 0
        Me.txtGrossSalary.Text = 0
        Me.TxtNetSalary.Text = 0
        Me.Button31.Enabled = False
        If Me.txtOther.Text = "" Then
            Me.txtOther.Text = 0
        End If
    End Sub

    Private Sub txtOther_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOther.Validated
        Me.txtOther.Text = CDbl(Me.txtOther.Text).ToString("N2")
    End Sub

    Sub TotalContribution()
        Dim NSI17, TotalContr, TotalAllwance As Double

        TotalAllwance = (CDbl(Me.txtBasicSalary.Text) + CDbl(Me.txtCola.Text) + CDbl(Me.txtHouse.Text) + CDbl(Me.txtHospitality.Text) + _
        CDbl(Me.txtTransportation.Text) + CDbl(Me.txtOnCall.Text) + CDbl(Me.txtMedical.Text) + CDbl(Me.txtUniform.Text) + _
        CDbl(Me.txtMeal.Text)).ToString("N2")

        NSI17 = TotalAllwance * 17 / 100

        TotalContr = CDbl(Me.TxtInsurance2.Text) + NSI17
        Try

            Dim cmd1 As New SqlCommand("select count(*) from TotalContribution where EmpID='" & Me.TxtEmpNo.Text & _
                                       "' and Month(Month)=" & _
                                       CInt(Me.DateTimePicker1.Value.Month) & " and Year(Month)=" & _
                                       CInt(Me.DateTimePicker1.Value.Year) & "", cnn)
            cnn.Open()
            If cmd1.ExecuteScalar > 0 Then
                Dim cmd2 As New SqlCommand("delete from TotalContribution where EmpID='" & Me.TxtEmpNo.Text & _
                                       "' and Month(Month)=" & _
                                       CInt(Me.DateTimePicker1.Value.Month) & " and Year(Month)=" & _
                                       CInt(Me.DateTimePicker1.Value.Year) & " ", cnn)
                cmd2.ExecuteNonQuery()
            End If
            cnn.Close()

            Me.Cursor = Cursors.WaitCursor
            Dim cmd As New SqlCommand
            Dim Trans As SqlTransaction

            cnn.Open()
            cmd.Connection = cnn
            Trans = cnn.BeginTransaction
            cmd.Transaction = Trans

            cmd.CommandText = "insert into TotalContribution (EmpID,EmpName, GrossSal, NSI8, NSI17, TotalCOntribution," & _
                                           "Month) values(N'" & Me.TxtEmpNo.Text & "',N'" & Me.txtEmpName.Text & "'," & CDbl(Me.txtGrossSalary.Text) & _
                                           "," & CDbl(Me.TxtInsurance2.Text) & "," & NSI17 & "," & TotalContr & ",N'" & Me.DateTimePicker1.Value.ToString("MM / dd / yyyy") & "')"
            cmd.ExecuteNonQuery()
            Trans.Commit()
            cnn.Close()

            Me.Cursor = Cursors.Default

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
                MsgBox(ex.ToString)
            End If
        End Try

    End Sub


    Private Sub txtKitching_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtKitching.TextChanged
        Me.TxtTotalDeduct.Text = 0
        Me.txtGrossSalary.Text = 0
        Me.TxtNetSalary.Text = 0
        Me.Button31.Enabled = False
        If Me.txtKitching.Text = "" Then
            Me.txtKitching.Text = 0
        End If
    End Sub

    Private Sub txtTkafol_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTkafol.TextChanged
        Me.TxtTotalDeduct.Text = 0
        Me.txtGrossSalary.Text = 0
        Me.TxtNetSalary.Text = 0
        Me.Button31.Enabled = False
        If Me.txtTkafol.Text = "" Then
            Me.txtTkafol.Text = 0
        End If
    End Sub

    Private Sub txtTkafol_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTkafol.Validated
        Me.txtTkafol.Text = CDbl(Me.txtTkafol.Text).ToString("N2")
    End Sub
End Class