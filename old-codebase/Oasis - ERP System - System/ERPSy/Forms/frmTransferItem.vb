Imports System.Data.SqlClient

Public Class frmTransferItem
    Sub Transfer()
        Me.ErrProv.Clear()
       
            If Me.DataGridView1.Rows.Count = 0 Then
                ErrProv.SetError(Me.DataGridView1, "Please enter invoice items")
        Else
            Try
                Me.Cursor = Cursors.WaitCursor
                cnn.Open()
                For Each row As DataGridViewRow In Me.DataGridView1.Rows

                    'Insert into Stock QntOut
                    Dim cmd1 As New SqlCommand("Insert Into Stock (StoreName,Item,BatchNo,QntOut,Details,Employee,TransType) Values " & _
                                               " (@StoreName,@Item,@BatchNo,@QntOut,@Details,@Employee,@TransType)", cnn)

                    cmd1.Parameters.Clear()
                    cmd1.Parameters.AddWithValue("@StoreName", row.Cells(0).Value)
                    cmd1.Parameters.AddWithValue("@Item", row.Cells(1).Value)
                    cmd1.Parameters.AddWithValue("@BatchNo", row.Cells(2).Value)
                    cmd1.Parameters.AddWithValue("@QntOut", CDbl(row.Cells(3).Value))
                    cmd1.Parameters.AddWithValue("@Details", row.Cells(5).Value)
                    cmd1.Parameters.AddWithValue("@Employee", CurrentUser)
                    cmd1.Parameters.AddWithValue("@TransType", "Transfer Frome" & row.Cells(0).Value)
                    'If ExpireDate = "" Then
                    '    cmd1.Parameters.AddWithValue("ExpireDate", DBNull.Value)
                    'Else
                    '    cmd1.Parameters.AddWithValue("ExpireDate", CDate(ExpireDate))
                    'End If
                    cmd1.ExecuteNonQuery()
                    'Insert into Stock QntIn
                    Dim cmd2 As New SqlCommand("Insert Into Stock (StoreName,Item,BatchNo,QntIn,Details,Employee,TransType)" & _
                                               "Values (@StoreName,@Item,@BatchNo,@QntIn,@Details,@Employee,@TransType)", cnn)

                    cmd2.Parameters.Clear()
                    cmd2.Parameters.AddWithValue("@StoreName", row.Cells(4).Value)
                    cmd2.Parameters.AddWithValue("@Item", row.Cells(1).Value)
                    cmd2.Parameters.AddWithValue("@BatchNo", row.Cells(2).Value)
                    cmd2.Parameters.AddWithValue("@QntIn", CDbl(row.Cells(3).Value))
                    cmd2.Parameters.AddWithValue("@Details", row.Cells(5).Value)
                    cmd2.Parameters.AddWithValue("@Employee", CurrentUser)
                    cmd2.Parameters.AddWithValue("@TransType", "Transfer To" & row.Cells(4).Value)
                    'If ExpireDate = "" Then
                    '    cmd2.Parameters.AddWithValue("ExpireDate", DBNull.Value)
                    'Else
                    '    cmd2.Parameters.AddWithValue("ExpireDate", CDate(ExpireDate))
                    'End If
                    cmd2.ExecuteNonQuery()
                Next
                cnn.Close()
                MsgBox("Transfered Done")
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
   
    Sub FillItemList()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select Distinct Item From Stock Where StoreName=N'" & _
                                      Me.ComboStoreName.SelectedItem & "' and Item Is Not Null", cnn)
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
    Sub FillBatchList()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select Distinct BatchNo From Stock Where Item=N'" & _
                                      Me.CombItem.SelectedItem & "' and StoreName=N'" & _
                                      Me.ComboStoreName.SelectedItem & "' and BatchNo Is Not Null", cnn)
            Dim Reader As SqlDataReader

            Me.CombBatchNo.Items.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.CombBatchNo.Items.Add(Reader.Item(0))
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
        Me.CombStockTo.SelectedIndex = -1
        Me.txtQnt.Text = 0
        Me.txtRemark.Clear()
        Me.DataGridView1.Rows.Clear()
    End Sub

    Sub FillStoreList()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select Distinct StoreName From StoreName  Where StoreName Is Not Null", cnn)
            Dim Reader As SqlDataReader

            Me.CombStockTo.Items.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.CombStockTo.Items.Add(Reader.Item(0))
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

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim a As New frmAddStoreName
        a.ShowDialog()

        FillStoreNameList()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 6 Then
            If MsgBox("Confirm delete?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Me.DataGridView1.Rows.RemoveAt(e.RowIndex)

            End If
        End If
    End Sub

    Private Sub btnSave_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Transfer()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        ErrProv.Clear()
        Try
            If Me.CombItem.SelectedIndex = -1 Then
                ErrProv.SetError(Me.CombItem, "Please select a valid item from the list")

            ElseIf Me.CombBatchNo.SelectedIndex = -1 Then
                ErrProv.SetError(Me.CombBatchNo, "Please select a valid Batch No from the list")

            ElseIf Me.txtQnt.Value = 0 Then
                ErrProv.SetError(Me.txtQnt, "Please enter a valid quantity and price")

            ElseIf Me.CombStockTo.SelectedIndex = -1 Then
                ErrProv.SetError(Me.CombStockTo, "Please enter a valid Store Name")

              
            Else
                If CDbl(Me.txtAvailableQnt.Text) < CDbl(Me.txtQnt.Value) Then
                    MsgBox("This Quantity is Not Available")
                End If
                Me.Cursor = Cursors.WaitCursor

                Me.DataGridView1.Rows.Add(New String() {Me.ComboStoreName.Text, Me.CombItem.Text, Me.CombBatchNo.Text.Trim, _
                                                         Me.txtQnt.Text.Trim, Me.CombStockTo.Text, Me.txtRemark.Text, "Delete"})

                Me.CombItem.SelectedIndex = -1
                Me.CombBatchNo.Items.Clear()
                Me.txtAvailableQnt.Text = "0"
                Me.CombStockTo.SelectedIndex = -1
                Me.txtQnt.Text = "0"
                Me.txtRemark.Clear()

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

    Private Sub frmTransferItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        FillStoreNameList()
        FillStoreList()
    End Sub

    Private Sub ComboStoreName_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboStoreName.SelectedIndexChanged
        If Me.ComboStoreName.SelectedIndex = -1 Then
            Me.CombItem.Items.Clear()
        Else
            FillItemList()
        End If
    End Sub

    Private Sub CombItem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CombItem.SelectedIndexChanged
        If Me.CombItem.SelectedIndex = -1 Then
            Me.CombBatchNo.Items.Clear()
        Else
            FillBatchList()
        End If
    End Sub

    Private Sub CombBatchNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CombBatchNo.SelectedIndexChanged
        If Me.CombBatchNo.SelectedIndex > -1 Then

            Me.txtAvailableQnt.Text = CDbl(GetItemQnt(Me.ComboStoreName.Text, Me.CombItem.Text, Me.CombBatchNo.Text).ToString("N0"))

        End If
    End Sub

    Private Sub btnClear_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Clear()
    End Sub

    Private Sub btnClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class