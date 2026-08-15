Imports System.Data.SqlClient

Public Class FrmReturndInvoice
    Sub clear()
        Me.txtInvNo.Clear()
        Me.txtCustID.Clear()
        Me.txtCustName.Clear()
        Me.txtDiscount.Text = "0.00"
        Me.txtVAT.Text = "0.00"
        Me.txtNetAmount.Text = "0.00"
        Me.txtWrittenValue.Clear()
        Me.DataGridView1.Rows.Clear()
    End Sub
    Sub FillReturnList()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim cmd As New SqlCommand("Select * From Invoices Where InvNo=" & Me.txtInvNo.Text.Trim, cnn)
            Dim Reader As SqlDataReader

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.txtCustID.Text = Reader.Item("CustID")
                Me.txtCustName.Text = Reader.Item("CustName")
                Me.txtDiscount.Text = Reader.Item("Disc")
                Me.txtVAT.Text = Reader.Item("VAT")
                Me.txtNetAmount.Text = Reader.Item("NetAmount")
                Me.txtWrittenValue.Text = Reader.Item("AmountInWords")
                Me.DataGridView1.Rows.Add(New String() {Reader.Item("StoreName"), Reader.Item("Item"), Reader.Item("BatchNo"), Reader.Item("Pack"), _
                                                        Reader.Item("Price"), Reader.Item("Rpric"), Reader.Item("Qnt")})
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
    Private Sub txtInvNo_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then

            FillReturnList()

        End If
    End Sub
    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        '    'If e.ColumnIndex = 7 Then
        '    '    If MsgBox("Confirm delete?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '    '        Me.DataGridView1.Rows.RemoveAt(e.RowIndex)
        '    '    End If
        '    'End If
        '    If e.ColumnIndex = 7 Then
        '        Dim a As New frmQuantitativechange
        '        a.SNo = Me.DataGridView1.CurrentRow.Cells(0).Value
        '        a.ShowDialog()

        '        FillReturn()
        '        'Try
        '        '    Me.Cursor = Cursors.WaitCursor

        '        '    Dim cmd As New SqlCommand
        '        '    Dim Trans As SqlTransaction

        '        '    cnn.Open()
        '        '    Trans = cnn.BeginTransaction
        '        '    cmd.Transaction = Trans
        '        '    cmd.Connection = cnn

        '        '    For Each row As DataGridViewRow In Me.DataGridView1.Rows
        '        '        cmd.CommandText = "Update Invoices Set " & _
        '        '                            "CompanyName=@CompanyName,Item=@Item,Price=@Price,Qnt=@Qnt,TotalSDG=@TotalSDG,StoreName=@StoreName " & _
        '        '                            "Where InvNo=" & Me.txtInvNo.Text

        '        '        cmd.Parameters.Clear()
        '        '        cmd.Parameters.AddWithValue("@CompanyName", row.Cells(1).Value)
        '        '        cmd.Parameters.AddWithValue("@Item", row.Cells(1).Value)
        '        '        cmd.Parameters.AddWithValue("@Price", CDbl(row.Cells(2).Value))
        '        '        cmd.Parameters.AddWithValue("@Qnt", CDbl(row.Cells(3).Value))
        '        '        cmd.Parameters.AddWithValue("@TotalSDG", row.Cells(4).Value)
        '        '        cmd.Parameters.AddWithValue("@StoreName", row.Cells(5).Value)
        '        '        cmd.ExecuteNonQuery()
        '        '    Next
        '        '    Trans.Commit()
        '        '    cnn.Close()
        '        '    MsgBox("Updated Successfully!")
        '        '    'Filldata()
        '        '    Me.Cursor = Cursors.Default
        '        'Catch ex As Exception
        '        '    Me.Cursor = Cursors.Default
        '        '    If cnn.State = ConnectionState.Open Then
        '        '        cnn.Close()
        '        '    End If
        '        '    MsgBox(ex.ToString)
        '        'End Try
        '    ElseIf e.ColumnIndex = 8 Then
        '        If MsgBox("Confirm delete? ", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '            Try
        '                Me.Cursor = Cursors.WaitCursor

        '                Dim cmd As New SqlCommand("Delete From Invoices Where InvNo=" & Me.txtInvNo.Text, cnn)

        '                cnn.Open()
        '                cmd.ExecuteNonQuery()
        '                cnn.Close()

        '                Me.DataGridView1.Rows.RemoveAt(e.RowIndex)
        '                Me.Cursor = Cursors.Default
        '            Catch ex As Exception
        '                Me.Cursor = Cursors.Default
        '                If cnn.State = ConnectionState.Open Then
        '                    cnn.Close()
        '                End If
        '                MsgBox(ex.ToString)
        '            End Try
        '        End If
        '    End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        clear()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub FrmReturndInvoice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub btnSave_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            Me.Cursor = Cursors.WaitCursor

            Dim InvNo, MoveNo As Integer
            Dim cmd, cmd1 As New SqlCommand
            Dim Trans As SqlTransaction
            Dim Reader As SqlDataReader

            cnn.Open()
            cnn1.Open()

            Trans = cnn.BeginTransaction
            cmd.Transaction = Trans
            cmd.Connection = cnn
            cmd1.Connection = cnn1
            For Each row As DataGridViewRow In Me.DataGridView1.Rows

                'Insert into Stock
                cmd.CommandText = "Insert Into Stock (StoreName,Item,BatchNo,Pack,WPrice,RPrice,QntIn,Details,Employee,CustID,CustName,TransType) Values " & _
                                      "(@StoreName,@Item,@BatchNo,@Pack,@WPrice,@RPrice,@QntIn,@Details,@Employee,@CustID,@CustName,N'Returned Invoice')"

                cmd.Parameters.Clear()
                cmd.Parameters.AddWithValue("@StoreName", row.Cells(0).Value)
                cmd.Parameters.AddWithValue("@Item", row.Cells(1).Value)
                cmd.Parameters.AddWithValue("@BatchNo", row.Cells(2).Value)
                cmd.Parameters.AddWithValue("@Pack", row.Cells(3).Value)
                cmd.Parameters.AddWithValue("@WPrice", CDbl(row.Cells(4).Value))
                cmd.Parameters.AddWithValue("@RPrice", CDbl(row.Cells(5).Value))
                cmd.Parameters.AddWithValue("@QntIn", CDbl(row.Cells(6).Value))
                cmd.Parameters.AddWithValue("@Details", "Recovered Invoice#" & Me.txtInvNo.Text)
                cmd.Parameters.AddWithValue("@Employee", CurrentUser)
                cmd.Parameters.AddWithValue("@CustID", Me.txtCustID.Text)
                cmd.Parameters.AddWithValue("@CustName", Me.txtCustName.Text)
                cmd.ExecuteNonQuery()


                ' ''''Financial Part
                'cmd.CommandText = "Select IsNull(Max(MoveNo),0) From Transactions"
                'MoveNo = CInt(cmd.ExecuteScalar) + 1

                'cmd.CommandText = "Insert Into Transactions (MoveNo,CustID,CustName,Ref,Acc1,Acc2,TotalIn,Employee,TransDate) " & _
                '            "Values (" & MoveNo & "," & Me.txtCustID.Text & ",N'" & Me.txtCustName.Text.Trim & _
                '            "',N'Return Invoice#" & InvNo & "',N'Clients',N'" & Me.txtCustName.Text.Trim & _
                '            "'," & CDbl(Me.txtNetAmount.Text.Trim) & _
                '            ",N'" & CurrentUser & "',N'" & Me.DateTimePicker1.Value.ToString("MM/dd/yyyy") & " 10:10:10')"
                'cmd.ExecuteNonQuery()

                'cmd.CommandText = "Insert Into Transactions (MoveNo,CustID,CustName,Ref,Acc1,Acc2,TotalOut,Employee,TransDate) " & _
                '            "Values (" & MoveNo & "," & Me.txtCustID.Text & ",N'" & Me.txtCustName.Text.Trim & _
                '            "',N'Return Invoice#" & InvNo & "',N'Purchase & Sales',N'Sales'," & CDbl(Me.txtNetAmount.Text.Trim) & _
                '            ",N'" & CurrentUser & "',N'" & Me.DateTimePicker1.Value.ToString("MM/dd/yyyy") & " 10:10:10')"
                'cmd.ExecuteNonQuery()
            Next
            Trans.Commit()
            cnn.Close()
            cnn1.Close()

            MsgBox("Return Successfully!")

            clear()

            Me.Cursor = Cursors.Default

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            If cnn1.State = ConnectionState.Open Then
                cnn1.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub
End Class