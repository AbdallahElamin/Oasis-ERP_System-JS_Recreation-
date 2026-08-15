Imports System.Data.SqlClient
Imports EgyCurr.CurText

Public Class frmMakeGetBill

    Sub Clear()
        Me.ErrProv.Clear()
        Me.txtAcc1.Clear()
        Me.txtAcc2.Clear()
        Me.txtAcc3.Clear()
        Me.txtAcc4.Clear()
        Me.txtDescr.Clear()
        Me.GridVouchers.Rows.Clear()
        Me.txtTotalAmount.Clear()
        Me.txtSource.Clear()
        Me.txtWrittenValue.Clear()
        Me.DTPTrans.Value = Today.Date
        Me.RCash.Checked = True
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Clear()
    End Sub

    Private Sub btnGClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Sub FillTree()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim cmd As New SqlCommand("Select Distinct Acc1 From ACCs Where Acc1 Is Not Null", cnn)
            Dim Reader, Reader1, Reader2, Reader3 As SqlDataReader
            Dim i, i1, i2, i3 As Integer

            Me.TreeAcc.Nodes.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.TreeAcc.Nodes.Add(Reader.Item(0))
                Dim cmd1 As New SqlCommand("Select Distinct Acc2 From ACCs Where Acc1=N'" & Reader.Item(0) & "' and Acc2 Is Not Null", cnn1)

                cnn1.Open()
                Reader1 = cmd1.ExecuteReader
                While Reader1.Read
                    Me.TreeAcc.Nodes(i).Nodes.Add(Reader1.Item(0))
                    Dim cmd2 As New SqlCommand("Select Distinct Acc3 From ACCs Where Acc1=N'" & Reader.Item(0) & "' and " & _
                                               "Acc2=N'" & Reader1.Item(0) & "' and Acc3 Is Not Null", cnn2)

                    cnn2.Open()
                    Reader2 = cmd2.ExecuteReader
                    While Reader2.Read
                        Me.TreeAcc.Nodes(i).Nodes(i1).Nodes.Add(Reader2.Item(0))
                        Dim cmd3 As New SqlCommand("Select Distinct Acc4 From ACCs Where Acc1=N'" & Reader.Item(0) & "' and " & _
                                                  "Acc2=N'" & Reader1.Item(0) & "' and Acc3=N'" & Reader2.Item(0) & _
                                                  "' and Acc4 Is Not Null", cnn3)

                        cnn3.Open()
                        Reader3 = cmd3.ExecuteReader
                        While Reader3.Read
                            Me.TreeAcc.Nodes(i).Nodes(i1).Nodes(i2).Nodes.Add(Reader3.Item(0))
                        End While
                        cnn3.Close()
                        i2 += 1
                    End While
                    cnn2.Close()
                    i2 = 0
                    i1 += 1
                End While

                cnn1.Close()
                i2 = 0
                i1 = 0
                i += 1
            End While
            cnn.Close()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            If cnn1.State = ConnectionState.Open Then
                cnn1.Close()
            End If
            If cnn2.State = ConnectionState.Open Then
                cnn2.Close()
            End If
            If cnn3.State = ConnectionState.Open Then
                cnn3.Close()
            End If
            If cnn4.State = ConnectionState.Open Then
                cnn4.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        FillTree()
        Clear()

        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select Distinct Acc4 From ACCs Where Acc1=N'Current Assets' " & _
                                      "and Acc2=N'Cash & Banks' and Acc3=N'Bank Accounts' and Acc4 Is Not Null", cnn)
            Dim Reader As SqlDataReader

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.CombBank.Items.Add(Reader.Item(0))
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

    Private Sub txtAmount_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtTotalAmount.TextChanged
        Try
            If Me.txtTotalAmount.Text.Trim.Length = 0 Then
                Me.txtWrittenValue.Clear()
            Else
                Me.txtWrittenValue.Text = SpellNumber(CDbl(Me.txtTotalAmount.Text)).ToString

                Me.txtWrittenValue.Text = Me.txtWrittenValue.Text.Replace("Dollar", "Pound")
                Me.txtWrittenValue.Text = Me.txtWrittenValue.Text.Replace("and No Cent", "")
                Me.txtWrittenValue.Text = Me.txtWrittenValue.Text & " Only"
            End If
        Catch ex As Exception
            Me.txtTotalAmount.Clear()
            Me.txtTotalAmount.Focus()
        End Try
    End Sub

    Sub CheckRadio()
        If RCash.Checked = True Then
            Me.txtChNo.Text = "Cash"
            Me.txtChNo.Enabled = False
            Me.CombBank.SelectedIndex = -1
            Me.CombBank.Enabled = False
        Else
            Me.txtChNo.Clear()
            Me.txtChNo.Enabled = True
            Me.CombBank.Enabled = True
            Me.txtChNo.Focus()
        End If
    End Sub

    Private Sub RCash_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RCash.CheckedChanged
        CheckRadio()
    End Sub

    Private Sub RBank_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles RBank.CheckedChanged
        CheckRadio()
    End Sub

    Private Sub btnGSave_Click(sender As System.Object, e As System.EventArgs) Handles btnGSave.Click
        Me.ErrProv.Clear()
        If Me.txtSource.Text.Trim.Length = 0 Then
            Me.ErrProv.SetError(Me.txtSource, "Please fill in")
            Me.txtSource.Focus()
        ElseIf Me.txtDescr.Text.Trim.Length = 0 Then
            Me.ErrProv.SetError(Me.txtDescr, "Please fill in")
            Me.txtDescr.Focus()
        ElseIf Me.GridVouchers.Rows.Count = 0 Then
            Me.ErrProv.SetError(Me.txtAcc1, "Please fill in")
            Me.txtAcc1.Focus()
        Else
            If Me.RBank.Checked = True Then
                If Me.txtChNo.Text.Trim.Length = 0 Then
                    Me.ErrProv.SetError(Me.txtChNo, "Please fill in")
                    Me.txtChNo.Focus()
                    Exit Sub
                ElseIf Me.CombBank.SelectedIndex = -1 Then
                    Me.ErrProv.SetError(Me.CombBank, "Please fill in")
                    Me.CombBank.Focus()
                    Exit Sub
                End If
            End If

            Try
                Me.Cursor = Cursors.WaitCursor

                Dim MoveNo As Integer
                Dim SNo As Integer
                Dim cmd As New SqlCommand
                Dim Trans As SqlTransaction
                Dim TransDate, PaymentType As String

                cnn.Open()
                Trans = cnn.BeginTransaction
                cmd.Connection = cnn
                cmd.Transaction = Trans

                'Get Trans. Date
                TransDate = "N'" & Me.DTPTrans.Value.ToString("MM/dd/yyyy") & " 10:10:10'"

                'Get Payment Type
                If Me.RCash.Checked = True Then
                    PaymentType = "C"
                Else
                    PaymentType = "B"
                End If

                'Get Move No.
                cmd.CommandText = "Select IsNull(Max(MoveNo),0) From Transactions Where Year(TransDate)=Year(Getdate())"
                MoveNo = CInt(cmd.ExecuteScalar) + 1

                'Get Bill No.
                cmd.CommandText = "Select IsNull(Max(SNo2),0) From Transactions Where TransType=N'Receipt Voucher' and PaymentType=N'" & PaymentType & "'"
                SNo = CInt(cmd.ExecuteScalar) + 1

                For Each row As DataGridViewRow In Me.GridVouchers.Rows
                    'Credit side
                    cmd.CommandText = "Insert Into Transactions (MoveNo,TransType,PaymentType,SNo2,DestSource,Descr,Acc1,Acc2,Acc3,Acc4,ChNo," & _
                                      "Writting,TotalIn,Employee,TransDate) " & _
                                      "Values (@MoveNo,@TransType,@PaymentType,@SNo2,@DestSource,@Descr,@Acc1,@Acc2,@Acc3,@Acc4,@ChNo," & _
                                      "@Writting,@TotalIn,@Employee," & TransDate & ")"

                    'Add values
                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@MoveNo", MoveNo)
                    cmd.Parameters.AddWithValue("@TransType", "Receipt Voucher")
                    cmd.Parameters.AddWithValue("@PaymentType", PaymentType)
                    cmd.Parameters.AddWithValue("@SNo2", SNo)
                    cmd.Parameters.AddWithValue("@DestSource", Me.txtSource.Text.Trim)
                    cmd.Parameters.AddWithValue("@Descr", Me.txtDescr.Text.Trim)
                    cmd.Parameters.AddWithValue("@Acc1", row.Cells(0).Value)
                    cmd.Parameters.AddWithValue("@Acc2", row.Cells(1).Value)
                    cmd.Parameters.AddWithValue("@Acc3", row.Cells(2).Value)
                    cmd.Parameters.AddWithValue("@Acc4", row.Cells(3).Value)
                    cmd.Parameters.AddWithValue("@ChNo", Me.txtChNo.Text.Trim)
                    cmd.Parameters.AddWithValue("@Writting", Me.txtWrittenValue.Text.Trim)
                    cmd.Parameters.AddWithValue("@TotalIn", CDbl(row.Cells(4).Value))
                    cmd.Parameters.AddWithValue("@Employee", CurrentUser)
                    cmd.ExecuteNonQuery()
                Next


                'Debit Side
                cmd.CommandText = "Insert Into Transactions (MoveNo,TransType,PaymentType,SNo2,DestSource,Descr,Acc1,Acc2,Acc3,Acc4,ChNo," & _
                                  "Writting,TotalOut,Employee,TransDate) " & _
                                  "Values (@MoveNo,@TransType,@PaymentType,@SNo2,@DestSource,@Descr,@Acc1,@Acc2,@Acc3,@Acc4,@ChNo," & _
                                  "@Writting,@TotalOut,@Employee," & TransDate & ")"

                'Add values
                cmd.Parameters.Clear()
                cmd.Parameters.AddWithValue("@MoveNo", MoveNo)
                cmd.Parameters.AddWithValue("@TransType", "Receipt Voucher")
                cmd.Parameters.AddWithValue("@PaymentType", PaymentType)
                cmd.Parameters.AddWithValue("@SNo2", SNo)
                cmd.Parameters.AddWithValue("@DestSource", Me.txtSource.Text.Trim)
                cmd.Parameters.AddWithValue("@Descr", Me.txtDescr.Text.Trim)
                cmd.Parameters.AddWithValue("@Acc1", "Current Assets")
                cmd.Parameters.AddWithValue("@Acc2", "Cash & Banks")
                If RCash.Checked = True Then 'Cash
                    cmd.Parameters.AddWithValue("@Acc3", "Cash")
                    cmd.Parameters.AddWithValue("@Acc4", "Cash on Hand")
                Else 'Bank
                    cmd.Parameters.AddWithValue("@Acc3", "Bank Accounts")
                    cmd.Parameters.AddWithValue("@Acc4", Me.CombBank.Text)
                End If
                cmd.Parameters.AddWithValue("@ChNo", Me.txtChNo.Text.Trim)
                cmd.Parameters.AddWithValue("@Writting", Me.txtWrittenValue.Text.Trim)
                cmd.Parameters.AddWithValue("@TotalOut", CDbl(Me.txtTotalAmount.Text.Trim))
                cmd.Parameters.AddWithValue("@Employee", CurrentUser)
                cmd.ExecuteNonQuery()

                Trans.Commit()
                cnn.Close()

                MsgBox("Saved Successfully")

                PrintBill("Receipt Voucher", PaymentType, SNo)
                PrintVoucher(MoveNo, Me.DTPTrans.Value.Year)

                Clear()

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

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs)
        Dim a As New frmSelectAccount
        a.ShowDialog()

        If SelAcc1 <> "" Then
            Me.txtAcc1.Text = SelAcc1
            Me.txtAcc2.Text = SelAcc2
            Me.txtAcc3.Text = SelAcc3
            Me.txtAcc4.Text = SelAcc4
        End If
    End Sub

    Private Sub TreeAcc_AfterSelect(sender As System.Object, e As System.Windows.Forms.TreeViewEventArgs) Handles TreeAcc.AfterSelect
        If e.Node.Level <> 3 Then
            Me.txtAcc1.Clear()
            Me.txtAcc2.Clear()
            Me.txtAcc3.Clear()
            Me.txtAcc4.Clear()
        Else
            Me.txtAcc1.Text = e.Node.Parent.Parent.Parent.Text
            Me.txtAcc2.Text = e.Node.Parent.Parent.Text
            Me.txtAcc3.Text = e.Node.Parent.Text
            Me.txtAcc4.Text = e.Node.Text
            Me.txtAmount.Focus()
        End If
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Me.ErrProv.Clear()
        If Me.txtAcc1.Text.Trim.Length = 0 Then
            Me.ErrProv.SetError(Me.txtAcc1, "Please fill in")
        ElseIf Len(Me.txtAmount.Text) = 0 Then
            Me.ErrProv.SetError(Me.txtAmount, "Please fill in")
            Me.txtAmount.Focus()
            Exit Sub
        Else
            Try
                Me.Cursor = Cursors.WaitCursor

                'Validate amount
                Try
                    If CDbl(Me.txtAmount.Text) = 0 Then
                        Me.ErrProv.SetError(Me.txtAmount, "Please fill in")
                        Me.txtAmount.Focus()
                        Me.Cursor = Cursors.Default
                        Exit Sub
                    End If
                Catch ex As Exception
                    Me.Cursor = Cursors.Default
                    Me.ErrProv.SetError(Me.txtAmount, "Please fill in")
                    Me.txtAmount.Clear()
                    Me.txtAmount.Focus()
                    Exit Sub
                End Try

                Me.GridVouchers.Rows.Add(New String() {Me.txtAcc1.Text, Me.txtAcc2.Text, Me.txtAcc3.Text, Me.txtAcc4.Text, _
                                                       CDbl(Me.txtAmount.Text).ToString("N2"), "Delete"})
                Calculate()

                Me.txtAcc1.Clear()
                Me.txtAcc2.Clear()
                Me.txtAcc3.Clear()
                Me.txtAcc4.Clear()
                Me.txtAmount.Clear()

                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Sub Calculate()
        If Me.GridVouchers.Rows.Count = 0 Then
            Me.txtTotalAmount.Text = "0.00"
        Else
            Try
                Me.Cursor = Cursors.Default

                Dim TotalAmount As Double

                ' Iterate through the list
                For Each row As DataGridViewRow In Me.GridVouchers.Rows
                    TotalAmount += CDbl(row.Cells(4).Value)
                Next

                Me.txtTotalAmount.Text = TotalAmount.ToString("N2")

                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub GridVouchers_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridVouchers.CellClick
        If e.ColumnIndex = 5 Then
            Me.GridVouchers.Rows.RemoveAt(e.RowIndex)
        End If
    End Sub

    Private Sub GridVouchers_RowsRemoved(sender As Object, e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles GridVouchers.RowsRemoved
        Calculate()
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Dim a As New frmSelectAccount
        a.ShowDialog()

        If SelAcc1 <> "" Then
            Me.txtAcc1.Text = SelAcc1
            Me.txtAcc2.Text = SelAcc2
            Me.txtAcc3.Text = SelAcc3
            Me.txtAcc4.Text = SelAcc4
        End If
    End Sub
End Class