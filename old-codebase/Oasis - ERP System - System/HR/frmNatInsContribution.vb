Imports System.Data.SqlClient
Public Class frmNatInsContribution

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim das As New DataSet
            Dim dap As New SqlDataAdapter("select  EmpName,GrossSal, NSI8, NSI17, TotalContribution, Month from TotalContribution where " & _
                                          "Month(Month)=" & CInt(Me.DateTimePicker1.Value.Month.ToString) & _
                                          "and Year(Month)=" & CInt(Me.DateTimePicker1.Value.Year.ToString), cnn)


            dap.Fill(das, "TotalContribution")

            Dim rpt As New RptTotalContribution
            rpt.SetDataSource(das)
            frmReportViewer.CrystalReportViewer1.ReportSource = rpt
            frmReportViewer.CrystalReportViewer1.RefreshReport()
            frmReportViewer.ShowDialog()



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