Imports System.Data.SqlClient

Public Class frmOneYearContract

    Private Sub frmOneYearContract_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TotalSalary()
        FillComboDepartment()
        FillJobDescribtion()
        FillComboLevel()
        'listArcive()
    End Sub
    Sub FillComboLevel()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select Distinct GradeLevelAr From Levels where GradeLevelAr is not null", cnn)
            Dim Reader As SqlDataReader

            Me.comboLevel.Items.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Dim Item As New ComboBox
                Me.comboLevel.Items.Add(Reader.Item(0))
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

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim a As New frmAddSysType
        a.ShowDialog()

        FillComboDepartment()

    End Sub

    Sub FillComboDepartment()

        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select Distinct DepartmentAr From Department where DepartmentAr is not null", cnn)
            Dim Reader As SqlDataReader

            Me.ComboDepartment.Items.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Dim Item As New ComboBox
                Me.ComboDepartment.Items.Add(Reader.Item(0))
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
        If Me.txtFirstName.Text.Trim.Length = 0 Or Me.txtMiddleName.Text.Trim.Length = 0 Or Me.txtLastName.Text.Trim.Length = 0 Then
            MsgBox("الرجاء كتابة إسم الموظف  ")
        ElseIf Me.txtPlaceOfBirth.Text = "" Or Me.TxtTestPeriod.Text = "" Or Me.txtAddress.Text = "" _
        Or Me.ComboDepartment.SelectedIndex = -1 Or Me.ComboJobDescribtion.SelectedIndex = -1 Or Me.ComboLevel.SelectedIndex = -1 _
        Or Me.ComboReligion.SelectedIndex = -1 Or Me.ComboStatus.SelectedIndex = -1 Or Me.txtClosRlatvAddress.Text = "" _
        Or Me.txtWitness.Text = "" Or Me.txtKBCCWitness.Text = "" Or Me.txtEntitlement.Text = "" Then
            MsgBox("الرجاء ملأ جميع الخانات")
        Else
            Try

                Me.Cursor = Cursors.WaitCursor

                Dim cmd As New SqlCommand("insert into OneYearContract (FirstName,MidName,LastName,PlaceOfBirth,DateOfBirth,Status,HomeTown,Religion,Address,NextKinAddress " & _
                                          ",JobTitle,levl,Department,HireDate,TestPeriod,Entitlement,IDNo1type,IDNo1,Id1DateIssue,WorkNo,IDNo2type,IDNo2,Id2DateIssue " & _
                                          ",EmpName,KBCCWitness,EmployeeWitness,MainSalary,live,House,Hospitality,Transportation,AdditionalWork,Cure,Meal" & _
                                          ",Clothes,TotalSalary) values (N'" & Me.txtFirstName.Text & "',N'" & Me.txtMiddleName.Text & _
                                          "',N'" & Me.txtLastName.Text & "',N'" & Me.txtPlaceOfBirth.Text & _
                                          "',N'" & Me.PickerDateBirth.Value.ToString("MM/dd/yyyy") & "',N'" & Me.ComboStatus.Text & _
                                          "',N'" & Me.txtHomeTown.Text & "',N'" & Me.ComboReligion.Text & "',N'" & Me.txtAddress.Text & _
                                          "',N'" & Me.txtClosRlatvAddress.Text & "',N'" & Me.ComboJobDescribtion.SelectedItem & "',N'" & _
                                          Me.ComboLevel.SelectedItem & "',N'" & Me.ComboDepartment.Text & "',N'" & Me.PickerHireDate.Value.ToString("MM/dd/yyyy") & _
                                          "', N'" & Me.TxtTestPeriod.Text & "'," & Me.txtEntitlement.Text & ",N'" & Me.CombIDNo1Type.Text & "',N'" & Me.txtIDNo1.Text & _
                                          "',N'" & Me.PickerIDno.Value.ToString("MM/dd/yyyy") & "',N'" & Me.txtWorkNo.Text & "',N'" & Me.CombIDNo2Type.SelectedItem & _
                                          "',N'" & Me.txtIDNo2.Text & "',N'" & Me.PickerNationIssue.Value.ToString("MM/dd/yyyy") & "',N'" & Me.txtEmpName.Text & _
                                          "',N'" & Me.txtKBCCWitness.Text & "',N'" & Me.txtWitness.Text & "'," & Me.txtMainSalary.Text & "," & Me.txtLive.Text & _
                                          "," & Me.txtHouse.Text & "," & Me.txtHospitality.Text & "," & Me.txtTransportation.Text & "," & _
                                          Me.txtOverTime.Text & "," & Me.txtCure.Text & "," & Me.txtMeal.Text & "," & Me.txtClothes.Text & "," & Me.txtTotal.Text & _
                                          ")", cnn)
                'MsgBox(cmd.CommandText)
                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()
                MsgBox("تم الحفظ")
                listArcive()
                PrintContract()
                PrintPage2()
                printReport3()
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
    Sub TotalSalary()
        If Me.txtMainSalary.Text.Trim = "" Then
            Me.txtMainSalary.Text = 0

        End If
        If Me.txtLive.Text.Trim = "" Then
            Me.txtLive.Text = 0

        End If
        If Me.txtHouse.Text.Trim = "" Then
            Me.txtHouse.Text = 0

        End If
        If Me.txtHospitality.Text.Trim = "" Then
            Me.txtHospitality.Text = 0

        End If
        If Me.txtTransportation.Text.Trim = "" Then
            Me.txtTransportation.Text = 0

        End If
        If Me.txtOverTime.Text.Trim = "" Then
            Me.txtOverTime.Text = 0

        End If
        If Me.txtCure.Text.Trim = "" Then
            Me.txtCure.Text = 0

        End If
        If Me.txtMeal.Text.Trim = "" Then
            Me.txtMeal.Text = 0

        End If
        If Me.txtClothes.Text.Trim = "" Then
            Me.txtClothes.Text = 0

        End If

        Me.txtTotal.Text = CDbl(Me.txtMainSalary.Text) + CDbl(Me.txtLive.Text) + CDbl(Me.txtHouse.Text) + CDbl(Me.txtHospitality.Text) + CDbl(Me.txtTransportation.Text) + CDbl(Me.txtOverTime.Text) + CDbl(Me.txtCure.Text) + CDbl(Me.txtClothes.Text) + CDbl(Me.txtMeal.Text)
    End Sub

    Private Sub txtMainSalary_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMainSalary.TextChanged

        TotalSalary()
    End Sub

    Private Sub txtLive_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLive.TextChanged

        TotalSalary()
    End Sub

    Private Sub txtHouse_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHouse.TextChanged
        TotalSalary()
    End Sub

    Private Sub txtHospitality_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHospitality.TextChanged
        TotalSalary()
    End Sub

    Private Sub txtTransportation_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTransportation.TextChanged
        TotalSalary()
    End Sub

    Private Sub txtOverTime_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOverTime.TextChanged
        TotalSalary()
    End Sub

    Private Sub txtCure_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCure.TextChanged
        TotalSalary()
    End Sub

    Private Sub txtMeal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMeal.TextChanged
        TotalSalary()
    End Sub

    Private Sub txtClothes_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtClothes.TextChanged
        TotalSalary()
    End Sub
    Sub clear()
        Me.txtEmpName.Text = ""
        Me.txtFirstName.Text = ""
        Me.txtMiddleName.Text = ""
        Me.txtLastName.Text = ""
        Me.CombIDNo2Type.SelectedIndex = -1
        Me.txtIDNo2.Text = ""
        Me.txtOverTime.Text = ""
        Me.txtPlaceOfBirth.Text = ""
        Me.txtTotal.Text = ""
        Me.txtTransportation.Text = ""
        Me.TxtTestPeriod.Text = ""
        Me.txtWitness.Text = ""
        Me.txtWorkNo.Text = ""
        Me.txtLive.Text = ""
        Me.txtLive.Text = ""
        Me.txtHomeTown.Text = ""
        Me.txtHouse.Text = ""
        Me.txtAddress.Text = ""
        Me.txtClosRlatvAddress.Text = ""
        Me.txtClothes.Text = ""
        Me.txtCure.Text = ""
        Me.txtMainSalary.Text = ""
        Me.txtMeal.Text = ""
        Me.txtTotal.Text = 0
        Me.ComboDepartment.SelectedIndex = -1
        Me.ComboReligion.SelectedIndex = -1
        Me.ComboStatus.SelectedIndex = -1
        Me.txtPlaceOfBirth.Text = ""
        Me.ComboJobDescribtion.SelectedIndex = -1
        Me.CombIDNo1Type.SelectedIndex = -1
        Me.txtIDNo1.Text = ""
        Me.ComboLevel.SelectedIndex = -1
        Me.txtHospitality.Text = 0
        Me.txtKBCCWitness.Text = ""
        Me.txtEntitlement.Clear()
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim a As New frmAddSysType
        a.ShowDialog()

        FillComboDepartment()
    End Sub
    Sub PrintContract()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dap As New SqlDataAdapter("select * from OneYearContract where SNO=(select Max(SNo) from OneYearContract)", cnn)
            Dim das As New DataSet

            dap.Fill(das, "OneYearContract")

            Dim rpt As New rptOneYearContract
            rpt.SetDataSource(das)
            frmReportViewer.CrystalReportViewer1.ReportSource = rpt
            frmReportViewer.CrystalReportViewer1.RefreshReport()
            frmReportViewer.ShowDialog()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn1.State = ConnectionState.Open Then
                cnn1.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub
    Sub PrintPage2()

        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dap As New SqlDataAdapter("select Entitlement from OneYearContract where SNO=(select Max(SNo) from OneYearContract)", cnn)
            Dim das As New DataSet

            dap.Fill(das, "OneYearContract")
            Dim rpt As New rptOneYrCntrctCont2
            rpt.SetDataSource(das)
            frmReportViewer.CrystalReportViewer1.ReportSource = rpt
            frmReportViewer.CrystalReportViewer1.RefreshReport()
            frmReportViewer.ShowDialog()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default


            MsgBox(ex.ToString)
        End Try
    End Sub
    Sub printReport3()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dap As New SqlDataAdapter("select  * from OneYearContract where SNO=(select Max(SNo) from OneYearContract)", cnn)
            Dim das As New DataSet

            dap.Fill(das, "OneYearContract")

            Dim rpt As New rptOneYrContrctCont3
            rpt.SetDataSource(das)
            frmReportViewer.CrystalReportViewer1.ReportSource = rpt
            frmReportViewer.CrystalReportViewer1.RefreshReport()
            frmReportViewer.ShowDialog()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn1.State = ConnectionState.Open Then
                cnn1.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub listArcive()
        Try
            Me.Cursor = Cursors.WaitCursor

            Me.ListArchive1.Items.Clear()
            Dim cmd As New SqlCommand("select SNo, FirstName,MidName,LastName,HireDate,Department from OneYearContract " & _
                                      "where hireDate > N'" & Me.DateTimeFrom.Value.ToString("MM/dd/yyyy") & _
                                      " 00:00:01' and hireDate < N'" & Me.DateTimeTo.Value.ToString("MM/dd/yyyy ") & " 23:59:59' ", cnn)
            Dim Reader As SqlDataReader

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read

                With ListArchive1.Items.Add(Reader.Item(0))
                    .subitems.add(Reader.Item(1) + " " + Reader.Item(2) + " " + Reader.Item(3))
                    .subitems.add(Reader.Item(5))
                    .subitems.add(Format(CDate(Reader.Item(4)), "dd-MMM-yyyy"))
                End With
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

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If Me.ListArchive1.SelectedItems.Count = 0 Then
            MsgBox("الرجاء إختيار إسم الموظف من القائمة")
            Exit Sub
        End If
        Try
            Me.Cursor = Cursors.WaitCursor
            If MsgBox("هل تريد حذف هذا الموظف ؟", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then


                Dim cmd As New SqlCommand("delete from OneYearContract where SNo=" & Me.ListArchive1.SelectedItems(0).Text, cnn)
                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()
                listArcive()
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub



    Sub ShowReport1()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dap As New SqlDataAdapter("select * from OneYearContract where SNO=N'" & Me.ListArchive1.SelectedItems(0).Text & "'", cnn)
            Dim das As New DataSet

            dap.Fill(das, "OneYearContract")

            Dim rpt As New rptOneYearContract
            rpt.SetDataSource(das)
            frmReportViewer.CrystalReportViewer1.ReportSource = rpt
            frmReportViewer.CrystalReportViewer1.RefreshReport()
            frmReportViewer.ShowDialog()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn1.State = ConnectionState.Open Then
                cnn1.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub
    Sub ShowReport3()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dap As New SqlDataAdapter("select  * from OneYearContract where SNO=N'" & Me.ListArchive1.SelectedItems(0).Text & "'", cnn)
            Dim das As New DataSet

            dap.Fill(das, "OneYearContract")

            Dim rpt As New rptOneYrContrctCont3
            rpt.SetDataSource(das)
            frmReportViewer.CrystalReportViewer1.ReportSource = rpt
            frmReportViewer.CrystalReportViewer1.RefreshReport()
            frmReportViewer.ShowDialog()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn1.State = ConnectionState.Open Then
                cnn1.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        PrintList()
    End Sub

    Private Sub ListArchive1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListArchive1.DoubleClick
        ShowReport1()
        PrintPage2()
        ShowReport3()
    End Sub

    Private Sub Button32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button32.Click
        Me.Close()
    End Sub
    Sub PrintList()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dap As New SqlDataAdapter("select  FirstName,MidName,LastName,HireDate,Department from OneYearContract where hireDate > N'" & Me.DateTimeFrom.Value.ToString("MM/dd/yyyy 00:00:01") & _
                                          "' and hireDate < N'" & Me.DateTimeTo.Value.ToString("MM/dd/yyyy 23:59:59") & "' order by hiredate ", cnn)
            Dim das As New DataSet
            dap.Fill(das, "OneYearContract")

            Dim rpt As New rptContractorList
            rpt.SetDataSource(das)
            frmReportViewer.CrystalReportViewer1.ReportSource = rpt
            frmReportViewer.CrystalReportViewer1.RefreshReport()
            frmReportViewer.ShowDialog()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn1.State = ConnectionState.Open Then
                cnn1.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim a As New FrmJobDescAr
        a.ShowDialog()
        FillJobDescribtion()
    End Sub
    Sub FillJobDescribtion()
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.ComboJobDescribtion.Items.Clear()
            Dim cmd As New SqlCommand("select Distinct JobDescribtion From JobDescribtion where JobDescribtion is not null ", cnn)
            Dim rdr As SqlDataReader

            cnn.Open()
            rdr = cmd.ExecuteReader
            While rdr.Read
                Me.ComboJobDescribtion.Items.Add(rdr.Item(0))
            End While
            cnn.Close()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim a As New frmLevelAr
        a.ShowDialog()
        FillComboLevel()
    End Sub

    Private Sub DateTimeFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimeFrom.ValueChanged
        listArcive()
    End Sub

    Private Sub DateTimeTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimeTo.ValueChanged
        listArcive()
    End Sub
End Class