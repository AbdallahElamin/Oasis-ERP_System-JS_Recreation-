Imports System.Data.SqlClient

Public Class frmVacationsRequest



    Sub FillVacationGrid()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim cmd As New SqlCommand("select SNo,Substract,DateFrom, DateTo," & _
                                      "VacationDays,Notes,ResumeDate,Approved from Vacation where EmpID=N'" & _
                                      Me.txtEmpNo.Text & "' order by DateFrom", cnn)
            Dim Reader As SqlDataReader

            Me.GridVacation.Rows.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.GridVacation.Rows.Add(New String() {Reader.Item("SNo"), Format(CDate(Reader.Item("DateFrom")), "dd/MM/yyyy"), _
                                                       Format(CDate(Reader.Item("DateTo")), "dd/MM/yyyy"), Reader.Item("VacationDays"), _
                                                       Reader.Item("Notes"), Reader.Item("Substract"), _
                                                       Format(CDate(Reader.Item("ResumeDate")), "dd/MM/yyyy"), Reader.Item("Approved")})
            End While
            cnn.Close()
            For Each row As DataGridViewRow In Me.GridVacation.Rows
                If row.Cells(5).Value = "-1" Then
                    row.Cells(5).Value = "Yes"
                Else
                    If row.Cells(5).Value = "0" Then
                        row.Cells(5).Value = "No"
                    End If
                End If
            Next
            ColorGridVacation()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
        End Try

    End Sub
    Sub PrintRptNewVacation()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dap As New SqlDataAdapter("select * from Vacation where SNo =" & Me.GridVacation.CurrentRow.Cells(0).Value, cnn)
            Dim das As New DataSet

            dap.Fill(das, "Vacation")

            Dim rpt As New RptVacation
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

    Sub PrintRptResume()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dap As New SqlDataAdapter("select * from ResumeDuty where SNo =" & Me.GridResume.CurrentRow.Cells(0).Value, cnn)
            Dim das As New DataSet

            dap.Fill(das, "ResumeDuty")

            Dim rpt As New rptResumeEmployee
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


    Sub FillResumeGrid()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim cmd As New SqlCommand("select SNo,DateFrom, DateTo," & _
                                      "VacationDays, ResumeDate, Approved,NoWorkingDys,ResumeOn,ActualVacationDays from ResumeDuty where EmpID=N'" & _
                                      Me.txtEmpNo2.Text & "' order by DateFrom", cnn)
            Dim Reader As SqlDataReader

            Me.GridResume.Rows.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.GridResume.Rows.Add(New String() {Reader.Item("Sno"), Reader.Item("DateFrom"), _
                                                    Reader.Item("DateTo"), Reader.Item("VacationDays"), _
                                                      Reader.Item("ResumeDate"), Reader.Item("NoWorkingDys"), _
                                                       Format(CDate(Reader.Item("ResumeOn")), "dd/MM/yyyy"), _
                                                       Reader.Item("ActualVacationDays"), Reader.Item("Approved")})
            End While
            cnn.Close()
            ColorGridResume()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
        End Try

    End Sub

    Sub ColorGridResume()
        Try
            Me.Cursor = Cursors.WaitCursor

            For Each row As DataGridViewRow In Me.GridResume.Rows
                If row.Cells(8).Value = "Pending" Then
                    row.Cells(0).Style.BackColor = Color.White
                    row.Cells(1).Style.BackColor = Color.White
                    row.Cells(2).Style.BackColor = Color.White
                    row.Cells(3).Style.BackColor = Color.White
                    row.Cells(4).Style.BackColor = Color.White
                    row.Cells(5).Style.BackColor = Color.White
                    row.Cells(6).Style.BackColor = Color.White
                    row.Cells(7).Style.BackColor = Color.White
                ElseIf row.Cells(8).Value = "Supervisor Approved" Then
                    row.Cells(0).Style.BackColor = Color.Cyan
                    row.Cells(1).Style.BackColor = Color.Cyan
                    row.Cells(2).Style.BackColor = Color.Cyan
                    row.Cells(3).Style.BackColor = Color.Cyan
                    row.Cells(4).Style.BackColor = Color.Cyan
                    row.Cells(5).Style.BackColor = Color.Cyan
                    row.Cells(6).Style.BackColor = Color.Cyan
                    row.Cells(7).Style.BackColor = Color.Cyan
                ElseIf row.Cells(8).Value = "Supervisor Rejected" Then
                    row.Cells(0).Style.BackColor = Color.LightSalmon
                    row.Cells(1).Style.BackColor = Color.LightSalmon
                    row.Cells(2).Style.BackColor = Color.LightSalmon
                    row.Cells(3).Style.BackColor = Color.LightSalmon
                    row.Cells(4).Style.BackColor = Color.LightSalmon
                    row.Cells(5).Style.BackColor = Color.LightSalmon
                    row.Cells(6).Style.BackColor = Color.LightSalmon
                    row.Cells(7).Style.BackColor = Color.LightSalmon
                ElseIf row.Cells(8).Value = "HR Approved" Then
                    row.Cells(0).Style.BackColor = Color.DodgerBlue
                    row.Cells(1).Style.BackColor = Color.DodgerBlue
                    row.Cells(2).Style.BackColor = Color.DodgerBlue
                    row.Cells(3).Style.BackColor = Color.DodgerBlue
                    row.Cells(4).Style.BackColor = Color.DodgerBlue
                    row.Cells(5).Style.BackColor = Color.DodgerBlue
                    row.Cells(6).Style.BackColor = Color.DodgerBlue
                    row.Cells(7).Style.BackColor = Color.DodgerBlue
                ElseIf row.Cells(8).Value = "HR Rejected" Then
                    row.Cells(0).Style.BackColor = Color.Red
                    row.Cells(1).Style.BackColor = Color.Red
                    row.Cells(2).Style.BackColor = Color.Red
                    row.Cells(3).Style.BackColor = Color.Red
                    row.Cells(4).Style.BackColor = Color.Red
                    row.Cells(5).Style.BackColor = Color.Red
                    row.Cells(6).Style.BackColor = Color.Red
                    row.Cells(7).Style.BackColor = Color.Red
                End If
            Next

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
        End Try
    End Sub
    Sub ColorGridVacation()
        Try
            Me.Cursor = Cursors.WaitCursor

            For Each row As DataGridViewRow In Me.GridVacation.Rows
                If row.Cells(7).Value = "Pending" Then
                    row.Cells(0).Style.BackColor = Color.White
                    row.Cells(1).Style.BackColor = Color.White
                    row.Cells(2).Style.BackColor = Color.White
                    row.Cells(3).Style.BackColor = Color.White
                    row.Cells(4).Style.BackColor = Color.White
                    row.Cells(5).Style.BackColor = Color.White
                    row.Cells(6).Style.BackColor = Color.White
                ElseIf row.Cells(7).Value = "Supervisor Approved" Then
                    row.Cells(0).Style.BackColor = Color.Cyan
                    row.Cells(1).Style.BackColor = Color.Cyan
                    row.Cells(2).Style.BackColor = Color.Cyan
                    row.Cells(3).Style.BackColor = Color.Cyan
                    row.Cells(4).Style.BackColor = Color.Cyan
                    row.Cells(5).Style.BackColor = Color.Cyan
                    row.Cells(6).Style.BackColor = Color.Cyan
                ElseIf row.Cells(7).Value = "Supervisor Rejected" Then
                    row.Cells(0).Style.BackColor = Color.LightSalmon
                    row.Cells(1).Style.BackColor = Color.LightSalmon
                    row.Cells(2).Style.BackColor = Color.LightSalmon
                    row.Cells(3).Style.BackColor = Color.LightSalmon
                    row.Cells(4).Style.BackColor = Color.LightSalmon
                    row.Cells(5).Style.BackColor = Color.LightSalmon
                    row.Cells(6).Style.BackColor = Color.LightSalmon
                ElseIf row.Cells(7).Value = "HR Approved" Then
                    row.Cells(0).Style.BackColor = Color.DodgerBlue
                    row.Cells(1).Style.BackColor = Color.DodgerBlue
                    row.Cells(2).Style.BackColor = Color.DodgerBlue
                    row.Cells(3).Style.BackColor = Color.DodgerBlue
                    row.Cells(4).Style.BackColor = Color.DodgerBlue
                    row.Cells(5).Style.BackColor = Color.DodgerBlue
                    row.Cells(6).Style.BackColor = Color.DodgerBlue
                ElseIf row.Cells(7).Value = "HR Rejected" Then
                    row.Cells(0).Style.BackColor = Color.Red
                    row.Cells(1).Style.BackColor = Color.Red
                    row.Cells(2).Style.BackColor = Color.Red
                    row.Cells(3).Style.BackColor = Color.Red
                    row.Cells(4).Style.BackColor = Color.Red
                    row.Cells(5).Style.BackColor = Color.Red
                    row.Cells(6).Style.BackColor = Color.Red
                End If
            Next

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
        End Try
    End Sub


    Sub FillEmpData()
        If Me.txtEmpNo.Text.Trim = "" Then
            Exit Sub
        Else

            Try
                Me.Cursor = Cursors.WaitCursor

                Dim cmd As New SqlCommand("Select Name,jobDesc From StaffProfiles Where EmpID=N'" & Me.txtEmpNo.Text & "'", cnn)
                Dim Reader As SqlDataReader

                Me.TxtName.Clear()
                Me.txtPosition.Clear()

                cnn.Open()
                Reader = cmd.ExecuteReader
                While Reader.Read
                    Me.TxtName.Text = Reader.Item(0)
                    Me.txtPosition.Text = Reader.Item(1)

                End While
                cnn.Close()
                AcualVacationDays()
                FillVacationGrid()
                TotalWorkingDays()
                BalanceOfDaysAtThisStage()
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

    Sub FillEmpData2()
        If Me.txtEmpNo2.Text.Trim = "" Then
            Exit Sub
        Else

            Try
                Me.Cursor = Cursors.WaitCursor

                Dim cmd As New SqlCommand("Select Name,jobDesc From StaffProfiles Where EmpID=N'" & Me.txtEmpNo2.Text & "'", cnn)
                Dim Reader As SqlDataReader

                Me.TxtName.Clear()
                Me.txtPosition.Clear()

                cnn.Open()
                Reader = cmd.ExecuteReader
                While Reader.Read
                    Me.txtName2.Text = Reader.Item(0)
                    Me.txtPosition2.Text = Reader.Item(1)

                End While
                cnn.Close()
                AcualVacationDays1()
                FillResumeData()
                FillResumeGrid()
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

    Sub GetNoOfDays()
        Dim DateOne As Date
        Dim DateTwo As Date
        Dim arrDate As New ArrayList
        Dim iNumOfDays As Long
        Dim iLoop As Integer

        DateOne = New Date(DtTo.Value.Year, DtTo.Value.Month, DtTo.Value.Day)
        DateTwo = New Date(DtFrom.Value.Year, DtFrom.Value.Month, DtFrom.Value.Day)

        iNumOfDays = DateDiff(DateInterval.Day, DateTwo, DateOne)

        For iLoop = 0 To iNumOfDays
            arrDate.Add(DateOne.AddDays(iLoop))
        Next

        For iLoop = 0 To arrDate.Count - 1
            Me.txtNoDays.Text = arrDate.Count

        Next
    End Sub

    'Sub BalanceOfDaysAtThisStage()
    '    Try
    '        Me.Cursor = Cursors.WaitCursor

    '        Dim cmd1 As New SqlCommand("Select isnull(Sum(VacationDays),0) From Vacation Where EmpNo=N'" & Me.txtEmpNo.Text & _
    '                                   "'and year(DateFrom)=" & CInt(Now.Year) & "", cnn)
    '        Dim Reader1 As SqlDataReader

    '        Dim NoOfDays As Integer
    '        cnn.Open()
    '        Me.txtBlnceThsStage.Text = ""
    '        Reader1 = cmd1.ExecuteReader

    '        While Reader1.Read
    '            NoOfDays = Reader1.Item(0)
    '            Me.txtBlnceThsStage.Text = NoOfDays
    '        End While

    '        cnn.Close()

    '        Me.Cursor = Cursors.Default
    '    Catch ex As Exception
    '        Me.Cursor = Cursors.Default
    '        If cnn.State = ConnectionState.Open Then
    '            cnn.Close()
    '        End If
    '        MsgBox(ex.ToString)
    '    End Try


    'End Sub

    Sub BalanceOfDaysAtThisStage()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd1 As New SqlCommand("Select isnull(Sum(VacationDays),0) From Vacation Where EmpID=N'" & Me.txtEmpNo.Text & _
                                       "' and year(DateFrom)=" & CInt(DtFrom.Value.Year) & "and Substract=-1", cnn1)
            Dim Reader1 As SqlDataReader

            Dim cmd2 As New SqlCommand("Select Entitlement from StaffProfiles Where EmpID=N'" & Me.txtEmpNo.Text & "'", cnn)
            Dim Reader2 As SqlDataReader

            Dim TotalVacatinDays As Integer
            Dim Entitlement As Integer
            cnn1.Open()
            cnn.Open()
            Me.txtBlnceThsStage.Text = ""
            Reader1 = cmd1.ExecuteReader

            While Reader1.Read
                TotalVacatinDays = Reader1.Item(0)
                Reader2 = cmd2.ExecuteReader
                While Reader2.Read

                    Entitlement = Reader2.Item(0)
                End While
            End While
            cnn.Close()
            cnn1.Close()
            Me.txtBlnceThsStage.Text = CInt(Entitlement) - CInt(TotalVacatinDays)

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            If cnn1.State = ConnectionState.Open Then
                cnn1.Close()
            End If
            MsgBox(ex.ToString)
        End Try


    End Sub

    Sub GetBalanceAfter()
        If Me.txtBlnceThsStage.Text <> "" And Me.txtNoDays.Text <> "" Then
            Try
                Me.txtBlncSfterThsStage.Text = CDbl(Me.txtBlnceThsStage.Text) - CDbl(Me.txtNoDays.Text)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub


    Sub AcualVacationDays()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd1 As New SqlCommand("Select DateFrom From Vacation Where EmpID=N'" & Me.txtEmpNo.Text & "'", cnn)
            Dim Reader1 As SqlDataReader
            cnn.Open()
            Me.txtActualVacationDays.Text = ""
            Reader1 = cmd1.ExecuteReader

           
            While Reader1.Read

              

                Dim DateOne As Date
                Dim date1 As Date
                Dim DateTwo As Date
                Dim arrDate As New ArrayList
                Dim iNumOfDays As Long
                Dim iLoop As Integer
                date1 = Reader1.Item(0)
                DateOne = New Date(DtResume2.Value.Year, DtResume2.Value.Month, DtResume2.Value.Day)
                DateTwo = New Date(date1.Year, date1.Month, date1.Day)

                iNumOfDays = DateDiff(DateInterval.Day, DateTwo, DateOne)

                For iLoop = 0 To iNumOfDays
                    arrDate.Add(DateOne.AddDays(iLoop))
                Next

                For iLoop = 0 To arrDate.Count - 1
                    Me.txtActualVacationDays.Text = arrDate.Count

                Next

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
    Sub AcualVacationDays1()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd1 As New SqlCommand("Select DateFrom From Vacation Where EmpID=N'" & Me.txtEmpNo2.Text & "'", cnn)
            Dim Reader1 As SqlDataReader
            cnn.Open()
            Me.txtActualVacationDays.Text = ""
            Reader1 = cmd1.ExecuteReader


            While Reader1.Read

                Dim DateOne As Date
                Dim date1 As Date
                Dim DateTwo As Date
                Dim arrDate As New ArrayList
                Dim iNumOfDays As Long
                Dim iLoop As Integer
                date1 = Reader1.Item(0)
                DateOne = New Date(DtResume2.Value.Year, DtResume2.Value.Month, DtResume2.Value.Day)
                DateTwo = New Date(date1.Year, date1.Month, date1.Day)

                iNumOfDays = DateDiff(DateInterval.Day, DateTwo, DateOne)

                For iLoop = 0 To iNumOfDays
                    arrDate.Add(DateOne.AddDays(iLoop))
                Next

                For iLoop = 0 To arrDate.Count - 1
                    Me.txtActualVacationDays.Text = arrDate.Count

                Next

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
    Sub TotalWorkingDays()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd1 As New SqlCommand("Select HireDate From StaffProfiles Where EmpID=N'" & Me.txtEmpNo.Text & "'", cnn)
            Dim Reader1 As SqlDataReader
            cnn.Open()
            Me.txtTotalWokDysNow.Text = ""
            Reader1 = cmd1.ExecuteReader

            While Reader1.Read
                Dim DateOne As Date
                Dim date1 As Date
                Dim DateTwo As Date
                Dim arrDate As New ArrayList
                Dim iNumOfDays As Long
                Dim iLoop As Integer
                date1 = Reader1.Item(0)
                DateOne = New Date(date1.Year, date1.Month, date1.Day)
                DateTwo = New Date(Now.Year, Now.Month, Now.Day)

                iNumOfDays = DateDiff(DateInterval.Day, DateOne, DateTwo)

                For iLoop = 0 To iNumOfDays
                    arrDate.Add(DateOne.AddDays(iLoop))
                Next

                For iLoop = 0 To arrDate.Count - 1
                    Me.txtTotalWokDysNow.Text = arrDate.Count

                Next


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


    Private Sub txtEmpNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmpNo.TextChanged
        Me.txtPosition.Text = ""
        Me.TxtName.Text = ""
        Me.txtBlnceThsStage.Text = 0
        Me.txtTotalWokDysNow.Text = 0
        Me.txtBlncSfterThsStage.Text = 0
        Me.GridVacation.Rows.Clear()

    End Sub

    Private Sub txtEmpNo_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtEmpNo.KeyUp
        If e.KeyCode = Keys.Enter Then
            FillEmpData()

        End If
    End Sub

    Private Sub DtTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DtTo.ValueChanged
        GetNoOfDays()
    End Sub

    Private Sub DtFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DtFrom.ValueChanged
        GetNoOfDays()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        FillEmpData()

    End Sub



    Sub FillJobTitle()

        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select FullName From users where ChkSupApproval=1", cnnLogin)
            Dim Reader As SqlDataReader
            Me.CombSendTo2.Items.Clear()
            cnnLogin.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read()
                Me.ComboSendTo.Items.Add(Reader.Item(0))
                Me.CombSendTo2.Items.Add(Reader.Item(0))
            End While
            cnnLogin.Close()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnnLogin.State = ConnectionState.Open Then
                cnnLogin.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Me.txtEmpNo.Text.Trim.Length = 0 Or Me.TxtName.Text.Trim.Length = 0 Then
            MsgBox("Please enter a valid employee number ")
        ElseIf Me.txtNoDays.Text = 0 Then
            MsgBox("Please specify your vacation period")
        ElseIf Me.RadNo.Checked = False And Me.RadYes.Checked = False Then
            MsgBox("Please Specify if you want to deduct from entitlement")
        ElseIf Me.RadYes.Checked = True And Me.txtBlnceThsStage.Text < 0 Then
            MsgBox("Sorry, you exceeded the maximum number of vacation days for this year")
        ElseIf Me.RadYes.Checked = True And Me.txtBlncSfterThsStage.Text < 0 Then
            MsgBox("Sorry, you exceeded the maximum number of vacation days for this year")
        Else
            Dim ReqNo As Integer = GetReqNo()
            Dim cmd As New SqlCommand
            Dim Trans As SqlTransaction
            cnn.Open()
            cmd.Connection = cnn
            Trans = cnn.BeginTransaction
            cmd.Transaction = Trans

            Me.Cursor = Cursors.WaitCursor

            Try
                Me.Cursor = Cursors.WaitCursor

                cmd.CommandText = "insert into Vacation (EmpID, Name, Position, Substract, DontSubstract, DateFrom, DateTo," & _
                                                         "VacationDays, NoDaysThsStage, NoWorkingDys, DaysAfterThsStge, Notes, Address," & _
                                                         "ResumeDate, SendTo,  Duty1, Who1, Duty2, Who2, Duty3, Who3, Duty4, Who4) values(N'" & Me.txtEmpNo.Text & _
                                                         "',N'" & Me.TxtName.Text & "',N'" & Me.txtPosition.Text & "'," & CInt(Me.RadYes.Checked) & _
                                                         "," & CInt(Me.RadNo.Checked) & ",N'" & Me.DtFrom.Value.ToString("MM / dd / yyyy") & "',N'" & Me.DtTo.Value.ToString("MM / dd / yyyy") & _
                                                         "'," & Me.txtNoDays.Text & "," & Me.txtBlnceThsStage.Text & "," & Me.txtTotalWokDysNow.Text & "," & Me.txtBlncSfterThsStage.Text & _
                                                         ",N'" & Me.txtNOtes.Text & "',N'" & Me.txtAddress.Text & "',N'" & Me.DTResumeDt.Value.ToString("MM / dd / yyyy") & _
                                                         "',N'" & Me.ComboSendTo.SelectedItem & _
                                                         "',N'" & Me.txtD1.Text & "',N'" & Me.txtWho1.Text & "',N'" & Me.txtD2.Text & "',N'" & Me.txtWho2.Text & _
                                                         "',N'" & Me.txtD3.Text & "',N'" & Me.txtWho3.Text & "',N'" & Me.txtD4.Text & "',N'" & Me.txtWho4.Text & "')"

                cmd.ExecuteNonQuery()

                Trans.Commit()
                cnn.Close()
                FillVacationGrid()
                BalanceOfDaysAtThisStage()
                GetBalanceAfter()
                Clear()
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


    Function GetReqNo() As Integer
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select IsNull(Max(SNo),0) From vacation", cnn)
            Dim ReqNo As Integer

            cnn.Open()
            ReqNo = CInt(cmd.ExecuteScalar)

            cnn.Close()

            Me.Cursor = Cursors.Default

            Return ReqNo
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Function

    Sub Clear()
        Me.DtFrom.Value = Now
        Me.DtTo.Value = Now
        Me.RadYes.Checked = False
        Me.RadNo.Checked = False
        Me.txtNoDays.Text = 1
        Me.txtNOtes.Text = ""
        Me.txtAddress.Text = ""
        Me.txtD1.Text = ""
        Me.txtWho1.Text = ""
        Me.txtD2.Text = ""
        Me.txtWho2.Text = ""
        Me.txtD3.Text = ""
        Me.txtWho3.Text = ""
        Me.txtD4.Text = ""
        Me.txtWho4.Text = ""

    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.txtEmpNo.Clear()
        Dim a As New frmSearchEmpID
        a.ShowDialog()

        If SelPatIDNo = "" Then
            Exit Sub
        End If

        Me.txtEmpNo.Text = SelPatIDNo
        FillEmpData()
    End Sub


    Private Sub frmVacationsRequest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.txtEmpNo.Text = CurrentUserID
        'Me.txtEmpNo2.Text = CurrentUserID
        'FillResumeData()
        Me.txtNoDays.Text = 1
        Me.txtBlnceThsStage.Text = 0
        FillResumeGrid()
        FillVacationGrid()
        'FillEmpData()
        FillJobTitle()
        Me.txtActualVacationDays.Text = 0
        'Me.txtTitle.Text = ""
        'Me.txtTitle2.Text = ""

    End Sub


    Sub FillResumeData()
        If Me.txtEmpNo2.Text.Trim = "" Then
            Exit Sub
        Else

            Try
                Me.Cursor = Cursors.WaitCursor

                Dim cmd As New SqlCommand("Select Name,Position,DateFrom, DateTo,isnull(VacationDays,0) VacationDays," & _
                                          "isnull(NoDaysThsStage,0) NoDaysThsStage, isnull(NoWorkingDys,0) NoWorkingDys," & _
                                          "isnull(DaysAfterThsStge,0)DaysAfterThsStge,ResumeDate From vacation Where EmpID=N'" & Me.txtEmpNo2.Text & "'", cnn)
                Dim Reader As SqlDataReader

                Me.txtName2.Clear()
                Me.txtPosition2.Clear()
                Me.txtFrom.Clear()
                Me.txtTo.Clear()
                Me.txtNoDays2.Clear()
                Me.txtBlncAfter2.Clear()
                Me.txtBlncDysThsStg2.Clear()
                Me.txtTotalWorkDays2.Clear()
                Me.txtResume.Clear()
                Me.GridResume.Rows.Clear()


                cnn.Open()
                Reader = cmd.ExecuteReader
                While Reader.Read
                    Me.txtName2.Text = Reader.Item("Name")
                    Me.txtPosition2.Text = Reader.Item("Position")
                    Me.txtFrom.Text = Reader.Item("DateFrom")
                    Me.txtTo.Text = Reader.Item("DateTo")
                    Me.txtNoDays2.Text = Reader.Item("VacationDays")
                    Me.txtBlncDysThsStg2.Text = Reader.Item("NoDaysThsStage")
                    Me.txtTotalWorkDays2.Text = Reader.Item("NoWorkingDys")
                    Me.txtBlncAfter2.Text = Reader.Item("DaysAfterThsStge")
                    Me.txtResume.Text = Reader.Item("ResumeDate")
                End While

                cnn.Close()
                FillResumeGrid()
                AcualVacationDays1()

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

    Private Sub CombSendTo2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CombSendTo2.TextChanged
        'Try
        '    Me.txtTitle2.Text = Me.ComboSendTo.SelectedValue.ToString
        'Catch ex As Exception
        '    MsgBox(ex.ToString)
        'End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.txtEmpNo2.Clear()
        Dim a As New frmSearchEmpID
        a.ShowDialog()

        If SelPatIDNo = "" Then
            Exit Sub
        End If

        Me.txtEmpNo2.Text = SelPatIDNo
        FillEmpData2()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        FillResumeData()
        FillResumeGrid()
    End Sub

    Private Sub txtEmpNo2_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtEmpNo2.KeyUp
        If e.KeyCode = Keys.Enter Then
            FillResumeData()
        End If
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        If Me.txtEmpNo2.Text.Trim.Length = 0 Then
            MsgBox("Please enter a valid employee number")
        ElseIf Me.txtName2.Text = "" Then
            MsgBox("Please complete resume duty details")

        Else

            Try

                Me.Cursor = Cursors.WaitCursor
                Dim cmd As New SqlCommand("Insert Into ResumeDuty (EmpID, Name, job, DateFrom, DateTo," & _
                                          "VacationDays, NoDaysThsStage, NoWorkingDys, DaysAfterThsStge," & _
                                          "ResumeDate, ResumeOn, SendTo, Title,ActualVacationDays) Values (N'" & Me.txtEmpNo2.Text & _
                                          "',N'" & Me.txtName2.Text & "',N'" & Me.txtPosition2.Text & "',N'" & Me.txtFrom.Text & _
                                          "',N'" & Me.txtTo.Text & "',N'" & Me.txtNoDays2.Text & "',N'" & Me.txtBlncDysThsStg2.Text & _
                                          "',N'" & Me.txtTotalWorkDays2.Text & "',N'" & Me.txtBlncAfter2.Text & "',N'" & Me.txtResume.Text & _
                                          "',N'" & Me.DtResume2.Value.ToString("MM / dd / yyyy") & "',N'" & Me.CombSendTo2.SelectedItem & _
                                          "',N'" & Me.txtTitle2.Text & "'," & Me.txtActualVacationDays.Text & ")", cnn)

                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()
                FillResumeGrid()
                ClearResume()
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


    Sub ClearResume()
        Me.txtFrom.Text = ""
        Me.txtTo.Text = ""
        Me.txtNoDays2.Text = ""
        Me.txtBlncDysThsStg2.Text = ""
        Me.txtTotalWorkDays2.Text = ""
        Me.txtBlncAfter2.Text = ""
        Me.txtResume.Text = ""
        Me.txtTitle2.Text = ""
    End Sub

    Private Sub txtNoDays_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNoDays.TextChanged
        GetBalanceAfter()
    End Sub

    Private Sub txtBlnceThsStage_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBlnceThsStage.TextChanged
        GetBalanceAfter()
    End Sub

    Private Sub txtEmpNo2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmpNo2.TextChanged
        Me.txtFrom.Text = ""
        Me.txtTo.Text = ""
        Me.txtNoDays2.Text = ""
        Me.txtBlncDysThsStg2.Text = ""
        Me.txtTotalWorkDays2.Text = ""
        Me.txtBlncAfter2.Text = ""
        Me.txtResume.Text = ""
        Me.txtTitle2.Text = ""
        Me.txtName2.Text = ""
        Me.txtPosition2.Text = ""
        Me.GridResume.Rows.Clear()
    End Sub

    Private Sub GridResume_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridResume.CellDoubleClick
        PrintRptResume()
    End Sub

    Private Sub GridVacation_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridVacation.CellDoubleClick

        PrintRptNewVacation()
    End Sub

    Private Sub DtResume2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DtResume2.ValueChanged
        AcualVacationDays1()
    End Sub
End Class