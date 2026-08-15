Imports System.Data.SqlClient

Public Class frmQuotation
    
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
                                      Me.CombItem.SelectedItem & "' and BatchNo Is Not Null", cnn)
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

    Private Sub frmQuotation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        FillStoreNameList()
    End Sub
    Sub Calculate()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim Total As Double

            For Each row As DataGridViewRow In Me.DataGridView1.Rows
                Total += CDbl(row.Cells(8).Value)
            Next

            Me.txtTotal.Text = Total.ToString("N2")
            Me.txtDiscount.Text = CDbl(Total * CDbl(Me.txtDiscPerc.Value) / 100).ToString("N2")
            Me.txtVAT.Text = CDbl((CDbl(Me.txtTotal.Text) - CDbl(Me.txtDiscount.Text)) * CDbl(Me.txtVATPerc.Value) / 100).ToString("N2")
            Me.txtNetAmount.Text = CDbl(CDbl(Me.txtTotal.Text) - CDbl(Me.txtDiscount.Text) + CDbl(Me.txtVAT.Text)).ToString("N2")

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
        Me.txtClientID.Clear()
        Me.txtClientName.Clear()
        Me.ComboStoreName.SelectedIndex = -1
        Me.CombItem.Items.Clear()
        Me.CombBatchNo.Items.Clear()
        Me.txtPack.Clear()
        Me.txtWPrice.Text = "0.00"
        Me.txtRPrice.Text = "0.00"
        Me.txtAvailableQnt.Text = "0"
        Me.txtQnt.Text = 0
        Me.txtDiscPerc.Value = 0
        Me.txtVATPerc.Value = 0
        Me.DataGridView1.Rows.Clear()
        Me.txtWrittenValue.Clear()
        Me.ChBonus.Checked = False
        Calculate()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Me.ErrProv.Clear()
        Try
            If Me.txtClientName.Text.Trim.Length = 0 Then
                ErrProv.SetError(txtClientName, "Please enter a valid customer ID")
            ElseIf Me.DataGridView1.Rows.Count = 0 Then
                ErrProv.SetError(Me.DataGridView1, "Please enter Quotation items")
            Else
                Me.Cursor = Cursors.WaitCursor

                Dim InvNo, MoveNo, Year As Integer
                Dim cmd As New SqlCommand
                cnn.Open()
                cmd.Connection = cnn

                cmd.CommandText = "Select Year(GetDate())"
                Year = CInt(cmd.ExecuteScalar)
                cmd.CommandText = "Select IsNull(Max(InvNo),0) From Quotation Where Year(TransDate)=Year(GetDate())"
                InvNo = CInt(cmd.ExecuteScalar) + 1

                For Each row As DataGridViewRow In Me.DataGridView1.Rows
                    'Quotation
                    cmd.CommandText = "Insert Into Quotation (InvNo,CustID,CustName," & _
                                      "StoreName,Item,BatchNo,Pack,WPrice,Rpric,Qnt,Disc,VAT,TotalSDG,prescription,Employee) Values " & _
                                      "(@InvNo,@CustID,@CustName,@StoreName,@Item,@BatchNo,@Pack," & _
                                      "@WPrice,@Rpric,@Qnt,@Disc,@VAT,@TotalSDG,@prescription,N'" & CurrentUser & "')"

                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@InvNo", InvNo)
                    cmd.Parameters.AddWithValue("@CustID", CInt(Me.txtClientID.Text))
                    cmd.Parameters.AddWithValue("@CustName", Me.txtClientName.Text.Trim)
                    cmd.Parameters.AddWithValue("@StoreName", row.Cells(0).Value)
                    cmd.Parameters.AddWithValue("@Item", row.Cells(1).Value)
                    cmd.Parameters.AddWithValue("@BatchNo", row.Cells(2).Value)
                    cmd.Parameters.AddWithValue("@Pack", row.Cells(3).Value)
                    cmd.Parameters.AddWithValue("@WPrice", CDbl(row.Cells(4).Value))
                    cmd.Parameters.AddWithValue("@Rpric", CDbl(row.Cells(5).Value))
                    cmd.Parameters.AddWithValue("@Qnt", CDbl(row.Cells(6).Value))
                    cmd.Parameters.AddWithValue("@Disc", Me.txtDiscPerc.Value)
                    cmd.Parameters.AddWithValue("@VAT", Me.txtVATPerc.Value)
                    cmd.Parameters.AddWithValue("@TotalSDG", CDbl(row.Cells(7).Value))
                    cmd.Parameters.AddWithValue("@prescription", row.Cells(8).Value)
                    cmd.ExecuteNonQuery()


                Next
                cnn.Close()
                MsgBox("Saved Successfully!")

                PrintQuotation(InvNo, Year)
                Clear()

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

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Clear()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        ErrProv.Clear()
        Try
            If Me.CombItem.SelectedIndex = -1 Then
                ErrProv.SetError(Me.CombItem, "Please select a valid item from the list")

            ElseIf Me.CombBatchNo.SelectedIndex = -1 Then
                ErrProv.SetError(Me.CombBatchNo, "Please select a valid item from the list")

            ElseIf Me.txtQnt.Value = 0 Then
                ErrProv.SetError(Me.txtQnt, "Please enter a valid quantity and price")

            Else
                Me.Cursor = Cursors.WaitCursor

                'Check if bonus
                Dim Total, WPrice As Double
                Dim Description As String

                If ChBonus.Checked = True Then
                    Description = "Bonus"
                    WPrice = 0
                    Total = 0
                Else
                    Description = "Sales"
                    WPrice = CDbl(Me.txtWPrice.Text)
                    Total = CDbl(CDbl(Me.txtWPrice.Text.Trim) * CDbl(Me.txtQnt.Text.Trim))
                End If
                'If CDbl(Me.txtAvailableQnt.Text - Me.txtQnt.Text) <= CDbl(Me.txtMinLevel.Text) Then
                '    MsgBox("Ammount Not Valid")
                'End If
                Me.DataGridView1.Rows.Add(New String() {Me.ComboStoreName.Text, Me.CombItem.Text, Me.CombBatchNo.Text.Trim, Me.txtPack.Text, _
                                                        WPrice, Me.txtRPrice.Text, Me.txtQnt.Text.Trim, _
                                                        Total.ToString("N2"), Description, "Delete"})

                Me.CombItem.SelectedIndex = -1
                Me.CombBatchNo.Items.Clear()
                Me.txtAvailableQnt.Text = "0"
                Me.txtQnt.Text = "0"
                Me.txtWPrice.Text = "0.00"
                Me.txtRPrice.Text = "0.00"
                Me.txtPack.Clear()
                Me.ChBonus.Checked = False

                Calculate()

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

    Private Sub txtNetAmount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNetAmount.TextChanged
        Try
            If Me.txtNetAmount.Text.Trim.Length = 0 Then
                Me.txtWrittenValue.Clear()
            Else
                Me.txtWrittenValue.Text = SpellNumber(CDbl(Me.txtNetAmount.Text)).ToString

                Me.txtWrittenValue.Text = Me.txtWrittenValue.Text.Replace("Dollar", "SDG")
                Me.txtWrittenValue.Text = Me.txtWrittenValue.Text.Replace("Genehs", "SDG")
                Me.txtWrittenValue.Text = Me.txtWrittenValue.Text.Replace("and No Piastre", "")
                Me.txtWrittenValue.Text = Me.txtWrittenValue.Text.Replace("Cent", "Piastre")
                Me.txtWrittenValue.Text = Me.txtWrittenValue.Text & " Only"
            End If
        Catch ex As Exception
            Me.txtNetAmount.Clear()
            Me.txtNetAmount.Focus()
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 9 Then
            If MsgBox("Confirm delete?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Me.DataGridView1.Rows.RemoveAt(e.RowIndex)
                Calculate()
            End If
        End If
    End Sub

    Private Sub txtDiscPerc_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDiscPerc.ValueChanged
        Calculate()
    End Sub

    Private Sub txtVATPerc_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVATPerc.ValueChanged
        Calculate()
    End Sub
   
    Private Sub CombItem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CombItem.SelectedIndexChanged
        If Me.CombItem.SelectedIndex > -1 Then
            FillBatchList()
        End If
    End Sub

    Private Sub CombBatchNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CombBatchNo.SelectedIndexChanged
        If Me.CombBatchNo.SelectedIndex > -1 Then
            Try
                Me.Cursor = Cursors.WaitCursor

                Me.txtAvailableQnt.Text = CDbl(GetItemQnt(Me.ComboStoreName.Text, Me.CombItem.Text, Me.CombBatchNo.Text).ToString("N0"))

                Dim cmd As New SqlCommand("Select WPrice,RPrice,Pack From Stock Where Item=N'" & _
                                              Me.CombItem.SelectedItem & "'", cnn)
                Dim Reader As SqlDataReader

                cnn.Open()
                Reader = cmd.ExecuteReader
                While Reader.Read
                    Me.txtWPrice.Text = CDbl(Reader.Item("WPrice")).ToString("N2")
                    Me.txtRPrice.Text = CDbl(Reader.Item("RPrice")).ToString("N2")
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
        End If
    End Sub

    Private Sub txtClientID_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtClientID.KeyUp
        If e.KeyCode = Keys.Enter Then
            If Me.txtClientID.Text.Trim.Length > 0 Then
                Try
                    Me.Cursor = Cursors.WaitCursor

                    Me.txtClientName.Text = GetClientName(CInt(Me.txtClientID.Text))

                    Me.Cursor = Cursors.Default
                Catch ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(ex.ToString)
                End Try
            End If
        End If
    End Sub

    Private Sub txtClientID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtClientID.TextChanged
        Me.txtClientName.Clear()
    End Sub

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        Dim a As New frmSearchClientID
        a.ShowDialog()

        If SelClientID <> "" Then
            Me.txtClientID.Text = SelClientID
            Me.txtClientName.Text = SelClientName
        End If
    End Sub

    Private Sub ComboStoreName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboStoreName.SelectedIndexChanged
        If Me.ComboStoreName.SelectedIndex > -1 Then

            FillItemList()

        End If
    End Sub

End Class