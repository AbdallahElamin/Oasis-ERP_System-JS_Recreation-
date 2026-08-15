Imports System.Data.SqlClient

Public Class frmResumeApprovalSup

    Private Sub frmResumeApprovalSup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
                                          "DaysAfterThsStge,ResumeDate,ResumeOn,ActualVacationDays " & _
                                          " From ResumeDuty Where Approved = N'Pending' and sendTo =N'" & CurrentUser & "'", cnn)
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
                                                        "Approve", "Reject"})
            End While
            cnn.Close()

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
            If e.ColumnIndex = 12 Then
                Me.Cursor = Cursors.WaitCursor
                Dim cmd As New SqlCommand("Update ResumeDuty Set Approved=N'Supervisor Approved', Supervisor=N'" & CurrentUser & _
                                          "', SupDate=N'" & Now & "' Where SNo=" & Me.DataGridView1.Rows(e.RowIndex).Cells(0).Value, cnn)

                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()
                PrintRptResume()
                PendingRequests()
                Me.Cursor = Cursors.Default

            ElseIf e.ColumnIndex = 13 Then

                Me.Cursor = Cursors.WaitCursor
                Dim cmd As New SqlCommand("Update ResumeDuty Set Approved=N'Supervisor Rejected',Supervisor=N'" & CurrentUser & _
                                          "', SupDate=N'" & Now & "' Where SNo=" & Me.DataGridView1.Rows(e.RowIndex).Cells(0).Value, cnn)

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
End Class