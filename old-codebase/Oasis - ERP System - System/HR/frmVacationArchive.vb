Imports System.Data.SqlClient

Public Class frmVacationArchive

    Private Sub frmVacationArchive_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PendingVacation()
    End Sub

    Sub PendingVacation()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim cmd As New SqlCommand("select Sno,Name,Position,Substract,DateFrom,DateTo," & _
                                      "VacationDays,NoWorkingDys,DaysAfterThsStge,Notes," & _
                                      "isnull(Supervisor,N'')Supervisor,isnull(HRUser,N'')HRUser, Approved from vacation where DateFrom>N'" & Me.DTFrom.Value.ToShortDateString & _
                                      "' and DateFrom<N'" & Me.DTTo.Value.ToShortDateString & "'", cnn)
            Dim Reader As SqlDataReader

            Me.DataGridView1.Rows.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read

                Me.DataGridView1.Rows.Add(New String() {Reader.Item("Sno"), Reader.Item("Name"), Reader.Item("Position"), _
                                                        Format(CDate(Reader.Item("DateFrom")), "dd/MM/yyyy"), _
                                                        Format(CDate(Reader.Item("DateTo")), "dd/MM/yyyy"), _
                                                        Reader.Item("VacationDays"), Reader.Item("NoWorkingDys"), _
                                                        Reader.Item("DaysAfterThsStge"), _
                                                        Reader.Item("Substract"), Reader.Item("Notes"), Reader.Item("Supervisor"), _
                                                        Reader.Item("HRUser"), Reader.Item("Approved")})

                For Each row As DataGridViewRow In Me.DataGridView1.Rows
                    If row.Cells(8).Value = "-1" Then
                        row.Cells(8).Value = "Yes"
                    ElseIf row.Cells(8).Value = "0" Then
                        row.Cells(8).Value = "No"
                    End If
                Next
                GetColour()
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

    Sub GetColour()

        For Each row As DataGridViewRow In Me.DataGridView1.Rows
            If row.Cells(12).Value = "Pending" Then
                row.Cells(0).Style.BackColor = Color.White
                row.Cells(1).Style.BackColor = Color.White
                row.Cells(2).Style.BackColor = Color.White
                row.Cells(3).Style.BackColor = Color.White
                row.Cells(4).Style.BackColor = Color.White
                row.Cells(5).Style.BackColor = Color.White
                row.Cells(6).Style.BackColor = Color.White
                row.Cells(7).Style.BackColor = Color.White
                row.Cells(8).Style.BackColor = Color.White
                row.Cells(9).Style.BackColor = Color.White
                row.Cells(10).Style.BackColor = Color.White
                row.Cells(11).Style.BackColor = Color.White
            ElseIf row.Cells(12).Value = "Supervisor Approved" Then
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
            ElseIf row.Cells(12).Value = "HR Approved" Then
                row.Cells(0).Style.BackColor = Color.DodgerBlue
                row.Cells(1).Style.BackColor = Color.DodgerBlue
                row.Cells(2).Style.BackColor = Color.DodgerBlue
                row.Cells(3).Style.BackColor = Color.DodgerBlue
                row.Cells(4).Style.BackColor = Color.DodgerBlue
                row.Cells(5).Style.BackColor = Color.DodgerBlue
                row.Cells(6).Style.BackColor = Color.DodgerBlue
                row.Cells(7).Style.BackColor = Color.DodgerBlue
                row.Cells(8).Style.BackColor = Color.DodgerBlue
                row.Cells(9).Style.BackColor = Color.DodgerBlue
                row.Cells(10).Style.BackColor = Color.DodgerBlue
                row.Cells(11).Style.BackColor = Color.DodgerBlue
            ElseIf row.Cells(12).Value = "HR Rejected" Then
                row.Cells(0).Style.BackColor = Color.Red
                row.Cells(1).Style.BackColor = Color.Red
                row.Cells(2).Style.BackColor = Color.Red
                row.Cells(3).Style.BackColor = Color.Red
                row.Cells(4).Style.BackColor = Color.Red
                row.Cells(5).Style.BackColor = Color.Red
                row.Cells(6).Style.BackColor = Color.Red
                row.Cells(7).Style.BackColor = Color.Red
                row.Cells(8).Style.BackColor = Color.Red
                row.Cells(9).Style.BackColor = Color.Red
                row.Cells(10).Style.BackColor = Color.Red
                row.Cells(11).Style.BackColor = Color.Red
            End If
        Next
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTFrom.ValueChanged
        PendingVacation()
    End Sub

    Private Sub DateTimePicker2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTTo.ValueChanged
        PendingVacation()
    End Sub

    Sub PrintRptNewVacation()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dap As New SqlDataAdapter("select * from Vacation where Sno=" & Me.DataGridView1.CurrentRow.Cells(0).Value, cnn)
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

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        PrintRptNewVacation()
    End Sub
End Class