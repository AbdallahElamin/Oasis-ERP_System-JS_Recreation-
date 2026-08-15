
Imports System.Data.SqlClient

Public Class frmAddItemsToStock

    Sub FillItemList()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select Distinct Item From ItemsRegistry Where Item Is Not Null order by Item", cnn)
            Dim Reader As SqlDataReader

            Me.combItem.Items.Clear()

            cnn.Open()

            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.combItem.Items.Add(Reader.Item(0))
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

    Sub FillStoreNameList()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select Distinct StoreName From StoreName Where StoreName Is Not Null", cnn)
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
    Private Sub frmAddItemsToStock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        FillStoreNameList()
    End Sub

    Sub Clear()
        Me.combItem.SelectedIndex = -1
        Me.ComboStoreName.SelectedIndex = -1
        Me.txtPack.Clear()
        Me.txtBatch.Clear()
        Me.CheckBox1.Checked = False
        Me.DTPExpiryDate.Value = Today.Date
        Me.txtQnt.Value = 0
        Me.txtWPrice.Text = "0.00"
        Me.txtRPrice.Text = "0.00"
        Me.txtRemarks.Clear()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Me.GridItems.Rows.Count > 0 Then
            Try
                Me.Cursor = Cursors.WaitCursor

                Dim cmd As New SqlCommand
                Dim Trans As SqlTransaction

                cnn.Open()
                cmd.Connection = cnn
                Trans = cnn.BeginTransaction
                cmd.Transaction = Trans

                For Each row As DataGridViewRow In Me.GridItems.Rows

                    cmd.CommandText = "Insert Into Stock (StoreName,Item,Pack,BatchNo,QntIn,WPrice,RPrice,ExpireDate,Details,Employee,TransType) Values " & _
                                      "(@StoreName,@Item,@Pack,@BatchNo,@QntIn,@WPrice,@RPrice,@ExpireDate,@Details,@Employee,N'Addition')"


                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@StoreName", row.Cells(0).Value)
                    cmd.Parameters.AddWithValue("@Item", row.Cells(1).Value)
                    cmd.Parameters.AddWithValue("@Pack", row.Cells(2).Value)
                    cmd.Parameters.AddWithValue("@BatchNo", row.Cells(3).Value)
                    cmd.Parameters.AddWithValue("@QntIn", CDbl(row.Cells(4).Value))
                    cmd.Parameters.AddWithValue("@WPrice", CDbl(row.Cells(5).Value))
                    cmd.Parameters.AddWithValue("@RPrice", CDbl(row.Cells(6).Value))

                    Dim ExpireDate As String
                    If row.Cells(7).Value = "" Then
                        cmd.Parameters.AddWithValue("@ExpireDate", DBNull.Value)
                    Else
                        'Check expiry date
                        ExpireDate = row.Cells(7).Value.ToString.Substring(3, 2) + "/" + _
                                     row.Cells(7).Value.ToString.Substring(0, 2) + "/" + row.Cells(7).Value.ToString.Substring(6, 4)
                        cmd.Parameters.AddWithValue("@ExpireDate", ExpireDate & " 10:10:10")
                    End If
                    cmd.Parameters.AddWithValue("@Details", row.Cells(8).Value)
                    cmd.Parameters.AddWithValue("@Employee", CurrentUser)

                    cmd.ExecuteNonQuery()

                    'Update Price
                    cmd.Parameters.Clear()
                    cmd.CommandText = "Update ItemsRegistry Set WPrice=" & CDbl(row.Cells(5).Value) & ",RPrice=" & CDbl(row.Cells(6).Value) & _
                                      " Where Item=@Item"
                    cmd.Parameters.AddWithValue("@Item", row.Cells(1).Value.ToString)
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

    Sub ClearAll()
        Clear()
        Me.GridItems.Rows.Clear()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearAll()
    End Sub

    Private Sub ButClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButClose.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        ErrProv.Clear()
        If Me.ComboStoreName.SelectedIndex = -1 Then
            ErrProv.SetError(Me.ComboStoreName, "Please fill in all details")

        ElseIf Me.combItem.SelectedIndex = -1 Then
            ErrProv.SetError(Me.combItem, "Please fill in all details")

        ElseIf Me.txtQnt.Value = 0 Then
            ErrProv.SetError(Me.txtQnt, "Please fill in all details")

        ElseIf Me.txtBatch.Text.Trim.Length = 0 Then
            ErrProv.SetError(Me.txtBatch, "Please fill in all details")
        Else

            Try
                Me.Cursor = Cursors.WaitCursor

                Dim ExpireDate As String
                If Me.CheckBox1.Checked = True Then
                    ExpireDate = Format(Me.DTPExpiryDate.Value, "dd/MM/yyyy")
                Else
                    ExpireDate = ""
                End If

                Me.GridItems.Rows.Add(New String() {Me.ComboStoreName.Text, Me.combItem.Text, Me.txtPack.Text, Me.txtBatch.Text, _
                                                    CDbl(Me.txtQnt.Value).ToString("N0"), Me.txtWPrice.Text, Me.txtRPrice.Text, ExpireDate, _
                                                    Me.txtRemarks.Text.Trim, "Delete"})
                Me.combItem.SelectedIndex = -1
                Me.txtPack.Clear()
                Me.txtBatch.Clear()
                Me.CheckBox1.Checked = False
                Me.DTPExpiryDate.Value = Date.Today
                Me.txtQnt.Value = 0
                Me.txtWPrice.Text = "0.00"
                Me.txtRPrice.Text = "0.00"
                Me.txtRemarks.Clear()


                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If Me.CheckBox1.Checked = True Then
            Me.DTPExpiryDate.Enabled = True
        ElseIf Me.CheckBox1.Checked = False Then
            Me.DTPExpiryDate.Enabled = False
        End If
    End Sub

    Private Sub combItem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles combItem.SelectedIndexChanged
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select WPrice,RPrice,Pack From ItemsRegistry Where  Item=@Item", cnn)
            Dim Reader As SqlDataReader

            cnn.Open()
            cmd.Parameters.AddWithValue("@Item", Me.combItem.Text)

            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.txtWPrice.Text = CDbl(Reader.Item("WPrice"))
                Me.txtRPrice.Text = CDbl(Reader.Item("RPrice"))
                Me.txtPack.Text = Reader.Item("Pack")
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


    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Clear()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim a As New frmAddStoreName
        a.ShowDialog()

        FillStoreNameList()

    End Sub

    Private Sub GridItems_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridItems.CellClick
        If e.ColumnIndex = 9 Then
            If MsgBox("Confirm delete?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Me.GridItems.Rows.RemoveAt(e.RowIndex)
            End If
        End If
    End Sub

    Private Sub ComboStoreName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboStoreName.SelectedIndexChanged
        If Me.ComboStoreName.SelectedIndex = -1 Then
            Me.combItem.Items.Clear()
        Else
            FillItemList()
        End If
    End Sub

End Class
