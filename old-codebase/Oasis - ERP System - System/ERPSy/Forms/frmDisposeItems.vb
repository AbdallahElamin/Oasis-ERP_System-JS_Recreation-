Imports System.Data.SqlClient

Public Class frmDisposeItems
    Sub FillStoreNameList()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select Distinct StoreName From Stock  Where StoreName Is Not Null Order By StoreName", cnn)
            Dim Reader As SqlDataReader

            Me.ComboStoreName.Items.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.ComboStoreName.Items.Add(Reader.Item(0))
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

    Sub FillItemsList()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select Distinct Item From stock Where CompanyName=N'" & Me.ComboStoreName.SelectedItem & "' and Item Is Not Null", cnn)
            Dim Reader As SqlDataReader

            Me.CombItem.Items.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.CombItem.Items.Add(Reader.Item(0))
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

    Sub FillBatchNoList()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select Distinct BatchNo From Stock Where Item=N'" & _
                                          Me.CombItem.SelectedItem & "' and BatchNo Is Not Null Order By BatchNo", cnn)
            Dim Reader As SqlDataReader

            Me.CombBatchNo.Items.Clear()
            Me.CombItem.AutoCompleteCustomSource.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.CombBatchNo.Items.Add(Reader.Item(0))
                Me.CombBatchNo.AutoCompleteCustomSource.Add(Reader.Item(0))
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

    Sub Clear()
        Me.ComboStoreName.SelectedIndex = -1
        Me.CombItem.Items.Clear()
        Me.CombBatchNo.Items.Clear()
        Me.txtAvailableQnt.Text = "0"
        Me.txtQnt.Value = 0
        Me.txtRemarks.Clear()

    End Sub

    Sub ClearAll()
        Clear()
        Me.GrdItems.Rows.Clear()
    End Sub

    Private Sub frmDisposeItems_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        FillStoreNameList()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ErrPDisp.Clear()
        If Me.ComboStoreName.SelectedIndex = -1 Then
            ErrPDisp.SetError(Me.ComboStoreName, "Please fill in all details")

        ElseIf Me.CombItem.SelectedIndex = -1 Then
            ErrPDisp.SetError(Me.CombItem, "Please fill in all details")

        ElseIf Me.CombBatchNo.SelectedIndex = -1 Then
            ErrPDisp.SetError(Me.CombBatchNo, "Please fill in all details")

        ElseIf Me.txtQnt.Value = 0 Then
            ErrPDisp.SetError(Me.txtQnt, "Please fill in all details")
        Else
            Try
                Me.Cursor = Cursors.WaitCursor

                Me.GrdItems.Rows.Add(New String() {Me.ComboStoreName.Text, Me.CombItem.Text, Me.CombBatchNo.Text, _
                                                   CDbl(Me.txtQnt.Value).ToString("N0"), Me.txtRemarks.Text.Trim, "Delete"})

                Me.ComboStoreName.SelectedIndex = -1
                Me.CombItem.Items.Clear()
                Me.CombBatchNo.Items.Clear()
                Me.txtQnt.Value = 0
                Me.txtAvailableQnt.Text = "0"
                Me.txtRemarks.Clear()
                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        If e.ColumnIndex = 5 Then
            If MsgBox("Confirm Delete?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Me.GrdItems.Rows.RemoveAt(e.RowIndex)
            End If
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ClearAll()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.GrdItems.Rows.Count > 0 Then
            Try
                Me.Cursor = Cursors.WaitCursor

                Dim cmd As New SqlCommand
                Dim Trans As SqlTransaction

                cnn.Open()
                cmd.Connection = cnn
                Trans = cnn.BeginTransaction
                cmd.Transaction = Trans

                cmd.CommandText = "Insert Into Stock (StoreName,Item,BatchNo,QntOut,Details,Employee,TransType) Values " & _
                                  "(@StoreName,@Item,@BatchNo,@Qntout,@Details,@Employee,N'Dispose Item')"

                For Each row As DataGridViewRow In Me.GrdItems.Rows
                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@StoreName", row.Cells(0).Value.ToString)
                    cmd.Parameters.AddWithValue("@Item", row.Cells(1).Value.ToString)
                    cmd.Parameters.AddWithValue("BatchNo", row.Cells(2).Value.ToString)
                    cmd.Parameters.AddWithValue("@Qntout", CDbl(row.Cells(3).Value.ToString))
                    cmd.Parameters.AddWithValue("@Details", row.Cells(4).Value.ToString)
                    cmd.Parameters.AddWithValue("@Employee", CurrentUser)
                    cmd.ExecuteNonQuery()
                Next

                Trans.Commit()
                cnn.Close()

                MsgBox("Saved Successfully!")

                ClearAll()

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

    Private Sub CombItem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.CombBatchNo.SelectedIndex = -1
        Me.CombBatchNo.Items.Clear()

        If Me.CombItem.SelectedIndex > -1 Then
            FillBatchNoList()
        End If
    End Sub

    Private Sub CombBatchNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If Me.CombBatchNo.SelectedIndex > -1 Then
            Me.txtAvailableQnt.Text = CDbl(GetItemQnt(Me.ComboStoreName.Text, Me.CombItem.Text, Me.CombBatchNo.Text).ToString("N0"))
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Clear()
    End Sub

    Private Sub ComboStoreName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.txtAvailableQnt.Text = "0"
        Me.CombItem.Items.Clear()
        Me.CombBatchNo.Items.Clear()
        If Me.ComboStoreName.SelectedIndex > -1 Then
            FillItemsList()
        End If
    End Sub
End Class