Imports System.Data.SqlClient

Public Class frmAppraisalApprove

    Private Sub frmAppraisalApprove_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillGrid()

    End Sub

    Sub FillGrid()
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.DataGridView1.Rows.Clear()
            Dim cmd As New SqlCommand("select SNo, Name, JobDesc, Year,LevelAchievenment,ImpactContract, " & _
                                      "isnull(DecisionCoordinatoin,N'')DecisionCoordinatoin  from StaffAppraisal where Approve=N'Pending'", cnn)
            Dim Reader As SqlDataReader
            cnn.Open()
            Reader = cmd.ExecuteReader
            Me.DataGridView1.Rows.Clear()
            While Reader.Read
                Me.DataGridView1.Rows.Add(New String() {Reader.Item("SNo"), Reader.Item("Name"), Reader.Item("JobDesc"), Reader.Item("Year"), _
                                                        Reader.Item("LevelAchievenment"), Reader.Item("ImpactContract"), _
                                                        Reader.Item("DecisionCoordinatoin"), "Approve"})

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

    Private Sub DataGridView1_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.ColumnIndex = 7 Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim cmd As New SqlCommand("Update StaffAppraisal Set Approve=N'Approved', HRUser=N'" & CurrentUser & _
                                          "',DecisionCoordinatoin=N'" & Me.DataGridView1.CurrentRow.Cells(6).Value & _
                                          "' Where SNo=" & Me.DataGridView1.Rows(e.RowIndex).Cells(0).Value, cnn)

                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()
                PrintRpt()
                FillGrid()
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

    Sub PrintRpt()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dap As New SqlDataAdapter("select * from StaffAppraisal where SNo =N'" & Me.DataGridView1.CurrentRow.Cells(0).Value & "'", cnn)
            Dim das As New DataSet

            dap.Fill(das, "StaffAppraisal")

            Dim rpt As New StaffAppr
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

    Private Sub DataGridView1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        PrintRpt()
    End Sub
End Class