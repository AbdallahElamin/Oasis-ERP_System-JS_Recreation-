Imports System.Data.SqlClient

Public Class frmPaySheetArchive


    Sub SavePaySheet()
        If Me.DataGridView1.Rows.Count = 0 Then
            MsgBox("No records to update!")
        Else
            Try
                Me.Cursor = Cursors.WaitCursor

                For Each row As DataGridViewRow In Me.DataGridView1.Rows
                    row.Cells(12).Value() = CDbl(row.Cells(2).Value()) + CDbl(row.Cells(3).Value()) + _
                    CDbl(row.Cells(4).Value()) + CDbl(row.Cells(5).Value()) + CDbl(row.Cells(6).Value()) + _
                    CDbl(row.Cells(7).Value()) + CDbl(row.Cells(8).Value()) + CDbl(row.Cells(9).Value()) + _
                    CDbl(row.Cells(10).Value() + CDbl(row.Cells(11).Value))

                    row.Cells(22).Value() = CDbl(row.Cells(13).Value()) + CDbl(row.Cells(14).Value()) + _
           CDbl(row.Cells(15).Value()) + CDbl(row.Cells(16).Value()) + CDbl(row.Cells(17).Value()) + _
           CDbl(row.Cells(18).Value()) + CDbl(row.Cells(19).Value()) + CDbl(row.Cells(20).Value()) + _
           CDbl(row.Cells(21).Value())

                    row.Cells(23).Value() = CDbl(row.Cells(12).Value()) - CDbl(row.Cells(22).Value())




                    Dim cmd1 As New SqlCommand("select count(*) from EmployeeSalary where EmpID='" & row.Cells(0).Value() & _
                                               "' and Month(Month)<=" & _
                                               CInt(Me.PickerMonth.Value.Month.ToString) & " and Year(Month)<=" & _
                                               CInt(Me.PickerMonth.Value.Year.ToString) & " and GradeLevel is not null", cnn)
                    cnn.Open()
                    If cmd1.ExecuteScalar > 0 Then
                        Dim cmd2 As New SqlCommand("delete from EmployeeSalary where EmpID='" & row.Cells(0).Value() & _
                                               "' and Month(Month)=" & _
                                               CInt(Me.PickerMonth.Value.Month.ToString) & " and Year(Month)=" & _
                                               CInt(Me.PickerMonth.Value.Year.ToString) & " and GradeLevel is not null", cnn)
                        cmd2.ExecuteNonQuery()
                    End If
                    cnn.Close()
                    Dim cmd As New SqlCommand("insert into EmployeeSalary ( EmpID, EmpName, BasicSalary, Cola, Accommodation, Hospitality, Transport," & _
                                              "OnCall, Medical, Meal, Uniform, New, GrossSalary, IncomeTax, StampTax, Zakat, Insurance, Insurance2, SalaryAdvance,kitching,Tkafol, " & _
                                              "Other, TotalDeduct, NetSalary, GradeLevel,JobTitle, Month, CurrentUser) values ( N'" & row.Cells(0).Value() & _
                                              "',N'" & row.Cells(1).Value() & "'," & row.Cells(2).Value() & _
                                              "," & row.Cells(3).Value() & "," & row.Cells(4).Value() & "," & row.Cells(5).Value() & _
                                              "," & row.Cells(6).Value() & "," & row.Cells(7).Value() & "," & row.Cells(8).Value() & _
                                              "," & row.Cells(9).Value() & "," & row.Cells(10).Value() & "," & row.Cells(11).Value() & _
                                              "," & row.Cells(12).Value() & "," & row.Cells(13).Value() & "," & row.Cells(14).Value() & _
                                              "," & row.Cells(15).Value() & "," & row.Cells(16).Value() & "," & row.Cells(17).Value() & _
                                              "," & row.Cells(18).Value & "," & row.Cells(19).Value() & "," & row.Cells(20).Value() & _
                                              "," & row.Cells(21).Value & "," & row.Cells(22).Value() & "," & row.Cells(23).Value() & ",N'" & row.Cells(24).Value() & _
                                              "',N'" & row.Cells(25).Value() & "',N'" & Me.PickerMonth.Value.ToString("MM / dd / yyyy") & _
                                              "',N'" & CurrentUser & "')", cnn)

                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()
                Next
                TotalContribution()
                MsgBox("Pay sheet Saved")
                PaySheet()


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
    
    Sub PaySheet()

        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dap As New SqlDataAdapter("select * from EmployeeSalary where  Month(Month)=" & CInt(Me.PickerMonth.Value.Month.ToString) & _
                                          " and Year(Month)=" & CInt(Me.PickerMonth.Value.Year.ToString) & " and GradeLevel is not null ", cnn)
            Dim das As New DataSet

            dap.Fill(das, "EmployeeSalary")

            Dim rpt As New RptPaySheet
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

    Private Sub frmPaySheetArchive_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'FillGrid()
    End Sub

    Sub FillGrid()
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.DataGridView1.Rows.Clear()


            Dim cmd1 As New SqlCommand("select Cola, Accommodation, Hospitality, Transport, OnCall," & _
                                      "Medical, Meal, Uniform, GradeLevel from GradeLevelAllowance where GradeLevel is not null", cnn1)
            Dim Reader1 As SqlDataReader
            cnn1.Open()
            Reader1 = cmd1.ExecuteReader
            While Reader1.Read

                Dim cmd As New SqlCommand("select EmpID, EmpName,GradeLevel, BasicSalary, IncomeTax, StampTax, Zakat, Insurance, Insurance2, " & _
                                          "SalaryAdvance,other, New,isnull(Kitching,0)Kitching,isnull(Tkafol,0)Tkafol,isnull(JobTitle,N'')JobTitle " & _
                                          "from EmployeeSalary where GradeLevel=N'" & Reader1.Item(8) & _
                                          "' and Month(Month)<=" & CInt(Me.PickerMonth.Value.Month.ToString) & _
                                          " and Year(Month)<=" & CInt(Me.PickerMonth.Value.Year.ToString) & " and GradeLevel is not null", cnn)
                Dim Reader As SqlDataReader
                cnn.Open()
                Reader = cmd.ExecuteReader

                While Reader.Read

                    Dim GrossSal As Double = CDbl(Reader.Item("BasicSalary")) + CDbl(Reader1.Item("Cola")) + CDbl(Reader1.Item("Accommodation")) + _
                                             CDbl(Reader1.Item("Hospitality")) + CDbl(Reader1.Item("Transport")) + CDbl(Reader1.Item("OnCall")) + _
                                             CDbl(Reader1.Item("Medical")) + CDbl(Reader1.Item("Meal")) + CDbl(Reader1.Item("Uniform"))

                    Dim SalaryDetuct As Double = CDbl(Reader.Item("IncomeTax")) + CDbl(Reader.Item("StampTax")) + CDbl(Reader.Item("Zakat")) + _
                                                 CDbl(Reader.Item("Insurance")) + CDbl(Reader.Item("Insurance2")) + CDbl(Reader.Item("SalaryAdvance")) + _
                                                 CDbl(Reader.Item("other") + CDbl(Reader.Item("Kitching")) + CDbl(Reader.Item("Tkafol")))

                    Dim TotalSalary As Double = CDbl(GrossSal) - CDbl(SalaryDetuct)

                    Me.DataGridView1.Rows.Add(New String() {Reader.Item("EmpID"), Reader.Item("EmpName"), Reader.Item("BasicSalary"), _
                                                           Reader1.Item("Cola"), Reader1.Item("Accommodation"), Reader1.Item("Hospitality"), _
                                                           Reader1.Item("Transport"), Reader1.Item("OnCall"), Reader1.Item("Medical"), _
                                                           Reader1.Item("Meal"), Reader1.Item("Uniform"), Reader.Item("New"), GrossSal, _
                                                           Reader.Item("IncomeTax"), Reader.Item("StampTax"), Reader.Item("Zakat"), _
                                                           Reader.Item("Insurance"), Reader.Item("Insurance2"), Reader.Item("SalaryAdvance"), _
                                                           Reader.Item("Kitching"), Reader.Item("Tkafol"), Reader.Item("Other"), SalaryDetuct, TotalSalary, _
                                                           Reader.Item("GradeLevel"), Reader.Item("JobTitle")})



                End While
                cnn.Close()
            End While

            cnn1.Close()


            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            If cnn1.State = ConnectionState.Open Then
                cnn1.Close()
            End If
        End Try
    End Sub

    Private Sub DataGridView1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each row As DataGridViewRow In Me.DataGridView1.Rows
            row.Cells(12).Value() = CDbl(row.Cells(2).Value()) + CDbl(row.Cells(3).Value()) + _
                    CDbl(row.Cells(4).Value()) + CDbl(row.Cells(5).Value()) + CDbl(row.Cells(6).Value()) + _
                    CDbl(row.Cells(7).Value()) + CDbl(row.Cells(8).Value()) + CDbl(row.Cells(9).Value()) + _
                    CDbl(row.Cells(10).Value() + CDbl(row.Cells(11).Value))

            row.Cells(22).Value() = CDbl(row.Cells(13).Value()) + CDbl(row.Cells(14).Value()) + _
           CDbl(row.Cells(15).Value()) + CDbl(row.Cells(16).Value()) + CDbl(row.Cells(17).Value()) + _
           CDbl(row.Cells(18).Value()) + CDbl(row.Cells(19).Value()) + CDbl(row.Cells(20).Value()) + _
           CDbl(row.Cells(21).Value())

            row.Cells(23).Value() = CDbl(row.Cells(12).Value()) - CDbl(row.Cells(22).Value())

        Next
    End Sub

    Private Sub DataGridView1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridView1.Click
        For Each row As DataGridViewRow In Me.DataGridView1.Rows
            row.Cells(12).Value() = CDbl(row.Cells(2).Value()) + CDbl(row.Cells(3).Value()) + _
                    CDbl(row.Cells(4).Value()) + CDbl(row.Cells(5).Value()) + CDbl(row.Cells(6).Value()) + _
                    CDbl(row.Cells(7).Value()) + CDbl(row.Cells(8).Value()) + CDbl(row.Cells(9).Value()) + _
                    CDbl(row.Cells(10).Value() + CDbl(row.Cells(11).Value))

            row.Cells(22).Value() = CDbl(row.Cells(13).Value()) + CDbl(row.Cells(14).Value()) + _
            CDbl(row.Cells(15).Value()) + CDbl(row.Cells(16).Value()) + CDbl(row.Cells(17).Value()) + _
            CDbl(row.Cells(18).Value()) + CDbl(row.Cells(19).Value()) + CDbl(row.Cells(20).Value()) + _
            CDbl(row.Cells(21).Value())

            row.Cells(23).Value() = CDbl(row.Cells(12).Value()) - CDbl(row.Cells(22).Value())

        Next
    End Sub

    Private Sub DataGridView1_RowLeave(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.RowLeave
        For Each row As DataGridViewRow In Me.DataGridView1.Rows
            row.Cells(12).Value() = CDbl(row.Cells(2).Value()) + CDbl(row.Cells(3).Value()) + _
                    CDbl(row.Cells(4).Value()) + CDbl(row.Cells(5).Value()) + CDbl(row.Cells(6).Value()) + _
                    CDbl(row.Cells(7).Value()) + CDbl(row.Cells(8).Value()) + CDbl(row.Cells(9).Value()) + _
                    CDbl(row.Cells(10).Value() + CDbl(row.Cells(11).Value))

            row.Cells(22).Value() = CDbl(row.Cells(13).Value()) + CDbl(row.Cells(14).Value()) + _
            CDbl(row.Cells(15).Value()) + CDbl(row.Cells(16).Value()) + CDbl(row.Cells(17).Value()) + _
            CDbl(row.Cells(18).Value()) + CDbl(row.Cells(19).Value()) + CDbl(row.Cells(20).Value()) + _
            CDbl(row.Cells(21).Value())

            row.Cells(23).Value() = CDbl(row.Cells(12).Value()) - CDbl(row.Cells(22).Value())



        Next
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        FillGrid()
    End Sub

    Private Sub DataGridView1_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyUp
        If e.KeyCode = Keys.Enter Then
            For Each row As DataGridViewRow In Me.DataGridView1.Rows
                row.Cells(12).Value() = CDbl(row.Cells(2).Value()) + CDbl(row.Cells(3).Value()) + _
                    CDbl(row.Cells(4).Value()) + CDbl(row.Cells(5).Value()) + CDbl(row.Cells(6).Value()) + _
                    CDbl(row.Cells(7).Value()) + CDbl(row.Cells(8).Value()) + CDbl(row.Cells(9).Value()) + _
                    CDbl(row.Cells(10).Value() + CDbl(row.Cells(11).Value))

                row.Cells(22).Value() = CDbl(row.Cells(13).Value()) + CDbl(row.Cells(14).Value()) + _
           CDbl(row.Cells(15).Value()) + CDbl(row.Cells(16).Value()) + CDbl(row.Cells(17).Value()) + _
           CDbl(row.Cells(18).Value()) + CDbl(row.Cells(19).Value()) + CDbl(row.Cells(20).Value()) + _
           CDbl(row.Cells(21).Value())

                row.Cells(23).Value() = CDbl(row.Cells(12).Value()) - CDbl(row.Cells(22).Value())

            Next
        ElseIf e.KeyCode = Keys.Delete Then
            If Me.DataGridView1.SelectedRows.Count = 0 Then
                MsgBox("Please select an employee from the list")
            ElseIf Me.DataGridView1.SelectedRows.Count > 1 Then
                Exit Sub
            End If

            Try
                Me.Cursor = Cursors.WaitCursor
                If MsgBox("Confirm delete ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then


                    Dim cmd As New SqlCommand("delete from EmployeeSalary where Month(Month) =N'" & Me.PickerMonth.Value.Month.ToString & _
                                              "' and EmpID=N'" & Me.DataGridView1.CurrentRow.Cells(0).Value, cnn)
                    Dim cmd2 As New SqlCommand("update StaffProfiles set Active=0 where SNo=N'" & Me.DataGridView1.CurrentRow.Cells(0).Value, cnn)
                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cmd2.ExecuteNonQuery()
                    cnn.Close()
                    FillGrid()
                    Deactive()

                End If
                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Sub Deactive()

        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("update StaffProfiles set Active=0 where SNo=N'" & Me.DataGridView1.CurrentRow.Cells(0).Value, cnn)
            cnn.Open()
            cmd.ExecuteNonQuery()
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


    Private Sub DataGridView1_CursorChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridView1.CursorChanged
        For Each row As DataGridViewRow In Me.DataGridView1.Rows
            row.Cells(12).Value() = CDbl(row.Cells(2).Value()) + CDbl(row.Cells(3).Value()) + _
                    CDbl(row.Cells(4).Value()) + CDbl(row.Cells(5).Value()) + CDbl(row.Cells(6).Value()) + _
                    CDbl(row.Cells(7).Value()) + CDbl(row.Cells(8).Value()) + CDbl(row.Cells(9).Value()) + _
                    CDbl(row.Cells(10).Value() + CDbl(row.Cells(11).Value))

            row.Cells(22).Value() = CDbl(row.Cells(13).Value()) + CDbl(row.Cells(14).Value()) + _
           CDbl(row.Cells(15).Value()) + CDbl(row.Cells(16).Value()) + CDbl(row.Cells(17).Value()) + _
           CDbl(row.Cells(18).Value()) + CDbl(row.Cells(19).Value()) + CDbl(row.Cells(20).Value()) + _
           CDbl(row.Cells(21).Value())

            row.Cells(23).Value() = CDbl(row.Cells(12).Value()) - CDbl(row.Cells(22).Value())


        Next
    End Sub

    Private Sub DataGridView1_CellLeave(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellLeave
        For Each row As DataGridViewRow In Me.DataGridView1.Rows
            row.Cells(12).Value() = CDbl(row.Cells(2).Value()) + CDbl(row.Cells(3).Value()) + _
                    CDbl(row.Cells(4).Value()) + CDbl(row.Cells(5).Value()) + CDbl(row.Cells(6).Value()) + _
                    CDbl(row.Cells(7).Value()) + CDbl(row.Cells(8).Value()) + CDbl(row.Cells(9).Value()) + _
                    CDbl(row.Cells(10).Value() + CDbl(row.Cells(11).Value))

            row.Cells(22).Value() = CDbl(row.Cells(13).Value()) + CDbl(row.Cells(14).Value()) + _
            CDbl(row.Cells(15).Value()) + CDbl(row.Cells(16).Value()) + CDbl(row.Cells(17).Value()) + _
            CDbl(row.Cells(18).Value()) + CDbl(row.Cells(19).Value()) + CDbl(row.Cells(20).Value()) + _
            CDbl(row.Cells(21).Value())

            row.Cells(23).Value() = CDbl(row.Cells(12).Value()) - CDbl(row.Cells(22).Value())


        Next
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        SavePaySheet()

    End Sub

    Sub TotalContribution()
       
        Try

            Dim NSI17, TotalContr, TotalAllwance As Double
            For Each row As DataGridViewRow In Me.DataGridView1.Rows
                TotalAllwance = CDbl(row.Cells(2).Value()) + CDbl(row.Cells(3).Value()) + _
                            CDbl(row.Cells(4).Value()) + CDbl(row.Cells(5).Value()) + CDbl(row.Cells(6).Value()) + _
                            CDbl(row.Cells(7).Value()) + CDbl(row.Cells(8).Value()) + CDbl(row.Cells(9).Value()) + _
                            CDbl(row.Cells(10).Value())



                NSI17 = TotalAllwance * 17 / 100

                TotalContr = CDbl(row.Cells(17).Value) + NSI17


                Dim cmd1 As New SqlCommand("select count(*) from TotalContribution where EmpID=N'" & row.Cells(0).Value & _
                                           "' and Month(Month)=" & _
                                           CInt(Me.PickerMonth.Value.Month) & " and Year(Month)=" & _
                                           CInt(Me.PickerMonth.Value.Year) & "", cnn)
                cnn.Open()
                If cmd1.ExecuteScalar > 0 Then
                    Dim cmd2 As New SqlCommand("delete from TotalContribution where EmpID=N'" & row.Cells(0).Value & _
                                           "'  and Month(Month)=" & _
                                           CInt(Me.PickerMonth.Value.Month) & " and Year(Month)=" & _
                                           CInt(Me.PickerMonth.Value.Year) & " ", cnn)
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
                                  "Month) values(N'" & row.Cells(0).Value & "',N'" & row.Cells(1).Value & "'," & TotalAllwance & _
                                  "," & CDbl(row.Cells(17).Value) & "," & NSI17 & "," & TotalContr & ",N'" & Me.PickerMonth.Value.ToString("MM / dd / yyyy") & "')"
                cmd.ExecuteNonQuery()
                Trans.Commit()
                cnn.Close()

                Me.Cursor = Cursors.Default
            Next
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
                MsgBox(ex.ToString)
            End If

        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim dap As New SqlDataAdapter("Select * From EmployeeSalary Where Month(Month)=" & Me.PickerMonth.Value.Month & "", cnn)
            Dim Das As New DataSet
            cnn.Open()
            dap.Fill(Das, "EmployeeSalary")
            cnn.Close()

            Dim rpt As New RprtSalSlip
            rpt.SetDataSource(Das)
            rptViewer.CrystalReportViewer1.ReportSource = rpt
            rptViewer.CrystalReportViewer1.RefreshReport()
            rptViewer.ShowDialog()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub
End Class


