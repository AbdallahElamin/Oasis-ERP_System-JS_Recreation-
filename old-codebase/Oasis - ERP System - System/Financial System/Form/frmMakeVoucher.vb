Imports System.Data.SqlClient

Public Class frmMakeVoucher

    Sub Calculate()
        If Me.GridVouchers.Rows.Count = 0 Then
            Me.txtCrd.Text = "0"
            Me.txtDep.Text = "0"
            Me.txtBalance.Text = "0"
        Else
            Try
                Dim Crd, Dep As Double
                Dim i As Integer

                ' Iterate through a dictionary
                For i = 0 To Me.GridVouchers.Rows.Count - 1
                    Crd = Crd + Me.GridVouchers.Rows(i).Cells(6).Value
                    Dep = Dep + Me.GridVouchers.Rows(i).Cells(5).Value
                Next

                If Crd = 0 Then
                    Me.txtCrd.Text = "0.00"
                Else
                    Me.txtCrd.Text = Format(Crd, "##,###.##")
                End If

                If Dep = 0 Then
                    Me.txtDep.Text = "0.00"
                Else
                    Me.txtDep.Text = Format(Dep, "##,###.##")
                End If

                If Crd - Dep = 0 Then
                    Me.txtBalance.Text = "0.00"
                Else
                    Me.txtBalance.Text = Format(CDbl(Crd - Dep), "##,###.##")
                End If
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.ErrProv.Clear()
            If Me.txtAcc1.Text.Trim.Length = 0 Then
                Me.ErrProv.SetError(Me.txtAcc1, "Please fill in")
            ElseIf Me.combChType.SelectedIndex = -1 Then
                Me.ErrProv.SetError(Me.combChType, "Please fill in")
                Me.combChType.Focus()
            ElseIf Len(Me.txtAmount.Text) = 0 Then
                Me.ErrProv.SetError(Me.txtAmount, "Please fill in")
                Me.txtAmount.Focus()
                Exit Sub
            ElseIf Me.txtDescr.Text.Trim.Length = 0 Then
                Me.ErrProv.SetError(Me.txtDescr, "Please fill in")
                Me.txtDescr.Focus()
                Exit Sub

        Else
            Try
                Me.Cursor = Cursors.WaitCursor

                'Validate amount
                Try
                    Dim X As Double = CDbl(Me.txtAmount.Text)
                Catch ex As Exception
                    Me.Cursor = Cursors.Default
                    Me.ErrProv.SetError(Me.txtAmount, "Please fill in")
                    Me.txtAmount.Clear()
                    Me.txtAmount.Focus()
                    Exit Sub
                End Try

                Dim Row(5) As String
                If Me.combChType.SelectedItem = "Debit" Then

                    Dim DepRow As String() = {Me.txtAcc1.Text, Me.txtAcc2.Text, Me.txtAcc3.Text, Me.txtAcc4.Text, _
                                               Me.txtDescr.Text.Trim, CDbl(Me.txtAmount.Text).ToString("N2"), "0", "Delete"}
                    Row = DepRow

                
                Else


                    Dim CrdRow As String() = {Me.txtAcc1.Text, Me.txtAcc2.Text, Me.txtAcc3.Text, Me.txtAcc4.Text, _
                                               Me.txtDescr.Text.Trim, "0", CDbl(Me.txtAmount.Text).ToString("N2"), "Delete"}
                    Row = CrdRow
                    

                End If

                Me.GridVouchers.Rows.Add(Row)
                Calculate()

                Me.txtAcc1.Clear()
                Me.txtAcc2.Clear()
                Me.txtAcc3.Clear()
                Me.txtAcc4.Clear()
                Me.combChType.SelectedIndex = -1
                Me.txtAmount.Clear()
                Me.txtDescr.Clear()

                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub DataGridView1_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles GridVouchers.RowsRemoved
        Calculate()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Me.GridVouchers.Rows.Count = 0 Then
            Exit Sub
        ElseIf CDbl(Me.txtCrd.Text - Me.txtDep.Text) <> 0 Then
            Me.ErrProv.SetError(Me.txtBalance, "Please complete voucher")
            Exit Sub
        Else
            Try
                Me.Cursor = Cursors.WaitCursor

                Dim MoveNo As Integer
                Dim i As Integer
                Dim cmd As New SqlCommand
                Dim Trans As SqlTransaction
                Dim TransDate As String = "N'" & Me.DTPTrans.Value.ToString("MM/dd/yyyy") & " 10:10:10'"

                cnn.Open()
                Trans = cnn.BeginTransaction
                cmd.Connection = cnn
                cmd.Transaction = Trans

                cmd.CommandText = "Select IsNull(Max(MoveNo),0) from TempVouchers Where Year(TransDate)=Year(GetDate())"
                MoveNo = CInt(cmd.ExecuteScalar) + 1


                For i = 0 To Me.GridVouchers.Rows.Count - 1
                    cmd.CommandText = "Insert into TempVouchers (MoveNo,Descr,Acc1,Acc2,Acc3,Acc4,TotalValueIn,TotalValueOut,UserName,TransDate) " & _
                             "Values (@MoveNo,@Descr,@Acc1,@Acc2,@Acc3,@Acc4,@TotalValueIn,@TotalValueOut,@UserName," & TransDate & ")"

                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@MoveNo", MoveNo)
                    cmd.Parameters.AddWithValue("@Descr", Me.GridVouchers.Rows(i).Cells(4).Value)
                    cmd.Parameters.AddWithValue("@Acc1", Me.GridVouchers.Rows(i).Cells(0).Value)
                    cmd.Parameters.AddWithValue("@Acc2", Me.GridVouchers.Rows(i).Cells(1).Value)
                    cmd.Parameters.AddWithValue("@Acc3", Me.GridVouchers.Rows(i).Cells(2).Value)
                    cmd.Parameters.AddWithValue("@Acc4", Me.GridVouchers.Rows(i).Cells(3).Value)
                    cmd.Parameters.AddWithValue("@TotalValueOut", CDbl(Me.GridVouchers.Rows(i).Cells(5).Value))
                    cmd.Parameters.AddWithValue("@TotalValueIn", CDbl(Me.GridVouchers.Rows(i).Cells(6).Value))
                    cmd.Parameters.AddWithValue("@UserName", CurrentUser)

                    cmd.ExecuteNonQuery()
                Next

                Trans.Commit()
                cnn.Close()

                MsgBox("Saved Successfully")

                'Reset controls
                Me.GridVouchers.Rows.Clear()
                Me.txtAcc1.Clear()
                Me.txtAcc2.Clear()
                Me.txtAcc3.Clear()
                Me.txtAcc4.Clear()
                Me.combChType.SelectedIndex = -1
                Me.txtAmount.Clear()
                Me.txtDescr.Clear()
                Me.DTPTrans.Value = Today.Date

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

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridVouchers.CellClick
        If e.ColumnIndex = 7 Then
            Me.GridVouchers.Rows.RemoveAt(e.RowIndex)
        End If
    End Sub

    Private Sub TreeAcc_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeAcc.AfterSelect
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

    Private Sub frmMakeVoucher_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        FillTree()
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.txtAcc1.Enabled = False
        Me.txtAcc2.Enabled = False
        Me.txtAcc3.Enabled = False
        Me.txtAcc4.Enabled = False
        Me.txtAcc1.Clear()
        Me.txtAcc2.Clear()
        Me.txtAcc3.Clear()
        Me.txtAcc4.Clear()
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.GridVouchers.Rows.Clear()
        Me.txtAcc1.Clear()
        Me.txtAcc2.Clear()
        Me.txtAcc3.Clear()
        Me.txtAcc4.Clear()
        Me.combChType.SelectedIndex = -1
        Me.txtAmount.Clear()
        Me.txtDescr.Clear()
        Me.DTPTrans.Value = Today.Date
    End Sub
End Class