Imports System.Data.SqlClient

Public Class frmResumeAprovalHR

    Private Sub frmHRResumeAproval_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PendingRequests()
    End Sub


    Sub PrintRptResume()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dap As New SqlDataAdapter("select * from ResumeDuty where Sno =" & Me.DataGridView1.CurrentRow.Cells(0).Value, cnn)
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

    Sub PendingRequests()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim cmd As New SqlCommand("Select SNo, Name,job,DateFrom, DateTo, VacationDays," & _
                                          " NoDaysThsStage,  NoWorkingDys," & _
                                          "DaysAfterThsStge,ResumeDate,ResumeOn,Approved,ActualVacationDays From ResumeDuty where Approved <> N'HR Approved'" & _
                                      "and Approved <> N'HR Rejected' and Approved <> N'Pending'", cnn)
            Dim Reader As SqlDataReader

            Me.DataGridView1.Rows.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read

                Me.DataGridView1.Rows.Add(New String() {Reader.Item("Sno"), Reader.Item("Name"), Reader.Item("job"), _
                                                        Format(CDate(Reader.Item("DateFrom")), "dd/MM/yyyy"), Format(CDate(Reader.Item("DateTo")), "dd/MM/yyyy"), _
                                                        Reader.Item("VacationDays"), Reader.Item("NoDaysThsStage"), Reader.Item("NoWorkingDys"), _
                                                        Reader.Item("DaysAfterThsStge"), Format(CDate(Reader.Item("ResumeDate")), "dd/MM/yyyy"), _
                                                        Format(CDate(Reader.Item("ResumeOn")), "dd/MM/yyyy"), Reader.Item("ActualVacationDays"), _
                                                        Reader.Item("Approved"), "Approve", "Reject"})
            End While
            cnn.Close()
            GetColour()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        Try

            If e.ColumnIndex = 13 Then
                If Me.DataGridView1.Rows(e.RowIndex).Cells(12).Value = "Supervisor Rejected" Then
                    MsgBox("This request is rejected by supervisor")
                    PrintRptResume()
                Else
                    Me.Cursor = Cursors.WaitCursor

                    Dim cmd As New SqlCommand("Update ResumeDuty Set Approved=N'HR Approved',HRUser=N'" & CurrentUser & _
                                              "', HRDate=N'" & Now & "' Where SNo=" & Me.DataGridView1.Rows(e.RowIndex).Cells(0).Value, cnn)

                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()
                    PrintRptResume()
                    PendingRequests()

                    Me.Cursor = Cursors.Default
                End If
            ElseIf e.ColumnIndex = 14 Then

                Me.Cursor = Cursors.WaitCursor
                Dim cmd As New SqlCommand("Update ResumeDuty Set Approved=N'HR Rejected' ,HRUser=N'" & CurrentUser & _
                                              "',HRDate=N'" & Now & "' Where SNo=" & Me.DataGridView1.Rows(e.RowIndex).Cells(0).Value, cnn)

                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()
                PrintRptResume()
                PendingRequests()
                Me.Cursor = Cursors.Default
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            MsgBox(ex.ToString)

        End Try

    End Sub
    Sub GetColour()
        For Each row As DataGridViewRow In Me.DataGridView1.Rows
            If row.Cells(12).Value = "Supervisor Approved" Then
                row.Cells(0).Style.BackColor = Color.Cyan
                row.Cells(1).Style.BackColor = Color.Cyan
                row.Cells(2).Style.BackColor = Color.Cyan
                row.Cells(3).Style.BackColor = Color.Cyan
                row.Cells(4).Style.BackColor = Color.Cyan
                row.Cells(5).Style.BackColor = Color.Cyan
                row.Cells(6).Style.BackColor = Color.Cyan
                row.Cells(7).Style.BackColor = Color.Cyan
                row.Cells(8).Style.BackColor = Color.Cyan
                row.Cells(9).Style.BackColor = Color.Cyan
                row.Cells(10).Style.BackColor = Color.Cyan
                row.Cells(11).Style.BackColor = Color.Cyan
            ElseIf row.Cells(12).Value = "Supervisor Rejected" Then
                row.Cells(0).Style.BackColor = Color.LightSalmon
                row.Cells(1).Style.BackColor = Color.LightSalmon
                row.Cells(2).Style.BackColor = Color.LightSalmon
                row.Cells(3).Style.BackColor = Color.LightSalmon
                row.Cells(4).Style.BackColor = Color.LightSalmon
                row.Cells(5).Style.BackColor = Color.LightSalmon
                row.Cells(6).Style.BackColor = Color.LightSalmon
                row.Cells(7).Style.BackColor = Color.LightSalmon
                row.Cells(8).Style.BackColor = Color.LightSalmon
                row.Cells(9).Style.BackColor = Color.LightSalmon
                row.Cells(10).Style.BackColor = Color.LightSalmon
                row.Cells(11).Style.BackColor = Color.LightSalmon
            End If
        Next
    End Sub
End Class