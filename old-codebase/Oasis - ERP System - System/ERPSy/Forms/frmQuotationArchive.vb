Imports System.Data.SqlClient

Public Class frmQuotationArchive


    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            If e.ColumnIndex = 4 Then
                Me.Cursor = Cursors.WaitCursor
                PrintQuotation(CInt(Me.DataGridView1.Rows(e.RowIndex).Cells(0).Value), _
                             CDate(Me.DataGridView1.Rows(e.RowIndex).Cells(3).Value).Year)
                Me.Cursor = Cursors.Default
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Try
            If e.RowIndex <> -1 Then
                Me.Cursor = Cursors.WaitCursor
                PrintQuotation(CInt(Me.DataGridView1.Rows(e.RowIndex).Cells(0).Value), _
                             CDate(Me.DataGridView1.Rows(e.RowIndex).Cells(3).Value).Year)
                Me.Cursor = Cursors.Default
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrintAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintAll.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim dap As New SqlDataAdapter("Select * From Quotation Where " & _
                                          "TransDate > N'" & Me.DTPBFrom.Value.ToShortDateString & " 00:00:01' and " & _
                                          "TransDate < N'" & Me.DTPBTo.Value.ToShortDateString & " 23:59:59' " & _
                                          "Order By InvNo", cnn)
            Dim das As New DataSet

            cnn.Open()
            dap.Fill(das, "Quotation")
            cnn.Close()

            Dim rpt As New QuotationsList
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

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select Distinct InvNo,CustID,CustName,TransDate From Quotation Where " & _
                                      "TransDate > N'" & Me.DTPBFrom.Value.ToShortDateString & " 00:00:01' and " & _
                                      "TransDate < N'" & Me.DTPBTo.Value.ToShortDateString & " 23:59:59' " & _
                                      "Order By InvNo", cnn)
            Dim Reader As SqlDataReader

            Me.DataGridView1.Rows.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.DataGridView1.Rows.Add(New String() {Reader.Item(0), Reader.Item(1), Reader.Item(2), _
                                                        Format(CDate(Reader.Item(3)), "dd-MMM-yyyy"), "Print"})
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
End Class