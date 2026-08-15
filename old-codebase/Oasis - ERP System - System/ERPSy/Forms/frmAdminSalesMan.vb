Imports System.Data.SqlClient

Public Class frmAdminSalesMan
    Sub clear()
        Me.txtName.Clear()
        Me.txtMobile.Clear()
        Me.txtRequieredVisit.Clear()
        Me.txtName.Focus()
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Me.txtName.Text.Trim.Length = 0 Then
                MsgBox("Please Fill In Name Data")
            ElseIf Me.txtMobile.Text.Trim.Length = 0 Then
                MsgBox("Please Fill In Mobile Data")
            ElseIf Me.txtRequieredVisit.Text.Trim.Length = 0 OrElse Me.txtRequieredVisit.Text.Trim.Length > 0 And Not System.Text.RegularExpressions.Regex.IsMatch(Me.txtRequieredVisit.Text.Trim, "^[0-9]*[0-9]$") Then
                MsgBox("Please Fill RequierdVisit by Number Only")
            Else
                Me.Cursor = Cursors.WaitCursor
                Dim cmd As New SqlCommand()
                cnn.Open()
                cmd.Connection = cnn
                cmd.CommandText = "Insert Into AgentDistributors (Name,Mobile,RequieredVisit)" & _
                                         " Values (@Name,@Mobile,@RequieredVisit)"
                cmd.Parameters.AddWithValue("@Name", Me.txtName.Text.Trim)
                cmd.Parameters.AddWithValue("@Mobile", Me.txtMobile.Text.Trim)
                cmd.Parameters.AddWithValue("@RequieredVisit", Me.txtRequieredVisit.Text.Trim)
                cmd.ExecuteNonQuery()
                cnn.Close()
                MsgBox("Saved Successfully")
                clear()
                FillList()
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

    Sub FillList()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim cmd As New SqlCommand("Select IsNull(SNo,'')Sno,IsNull(Name,'')Name,Isnull(Mobile,'')Mobile,IsNull(RequieredVisit,'')RequieredVisit From AgentDistributors", cnn)
            Dim Reader As SqlDataReader

            Me.DataGridView1.Rows.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.DataGridView1.Rows.Add(New String() {Reader.Item(0), Reader.Item(1), Reader.Item(2), Reader.Item(3), "Update", "Delete"})
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

    Private Sub frmAdminSalesAgents_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        FillList()
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.ColumnIndex = 4 Then
            Try
                Dim Name, Mobile, VisitNO As String
                Name = Me.DataGridView1.Rows(e.RowIndex).Cells(1).Value
                Mobile = Me.DataGridView1.Rows(e.RowIndex).Cells(2).Value
                VisitNO = Me.DataGridView1.Rows(e.RowIndex).Cells(3).Value
                If Name.Trim.Length = 0 OrElse Mobile.Trim.Length = 0 Then
                    MsgBox("RequieredVisit")
                ElseIf VisitNO.Trim.Length = 0 OrElse VisitNO.Trim.Length > 0 And Not System.Text.RegularExpressions.Regex.IsMatch(VisitNO.Trim, "^[0-9]*[0-9]$") Then
                    MsgBox("Please Update RequierdVisit by Number Only")
                Else
                    Me.Cursor = Cursors.WaitCursor

                    Dim cmd As New SqlCommand("Update AgentDistributors Set Name=N'" & Name & "',Mobile=N'" & Mobile & "',RequieredVisit=N'" & VisitNO & "' " & _
                                              "Where SNo=" & Me.DataGridView1.Rows(e.RowIndex).Cells(0).Value, cnn)

                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()

                    MsgBox("Update Successfully")

                    Me.Cursor = Cursors.Default
                End If
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                MsgBox(ex.ToString)
            End Try

        ElseIf e.ColumnIndex = 5 Then
            If MsgBox("Confirm delete?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Try
                    Me.Cursor = Cursors.WaitCursor

                    Dim cmd As New SqlCommand("Delete From AgentDistributors Where SNo=" & Me.DataGridView1.Rows(e.RowIndex).Cells(0).Value, cnn)

                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()

                    Me.DataGridView1.Rows.RemoveAt(e.RowIndex)

                    Me.Cursor = Cursors.Default
                Catch ex As Exception
                    Me.Cursor = Cursors.Default
                    If cnn.State = ConnectionState.Open Then
                        cnn.Close()
                    End If
                    MsgBox(ex.ToString)
                End Try
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class