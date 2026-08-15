Imports System.Data.SqlClient

Public Class frmExpensesAnalysis

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim dap As New SqlDataAdapter("Select " & Me.DTPFrom.Value.Year & " PaperNo,Month(TransDate) SNo,Acc2," & _
                                          "TotalOut-TotalIn TotalIn From Transactions Where " & _
                                          "Year(TransDate)=" & Me.DTPFrom.Value.Year & " and Acc1=N'Expenses'", cnn)
            Dim das As New DataSet

            cnn.Open()
            dap.Fill(das, "Transactions")
            cnn.Close()

            Dim rpt As New ExpensesAnalysis
            rpt.SetDataSource(das)
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