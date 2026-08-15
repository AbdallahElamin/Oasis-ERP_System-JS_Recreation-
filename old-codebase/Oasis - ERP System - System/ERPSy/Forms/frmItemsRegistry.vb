Imports System.Data.SqlClient

Public Class frmItemsRegistry

    Sub Clear()
        Me.ErrProv.Clear()
        Me.txtItem.Clear()
        Me.txtGenericName.Clear()
        Me.txtPack.Clear()
        Me.txtMinLevel.Text = 0
        Me.txtWPrice.Text = 0
        Me.txtRPrice.Text = 0
        Me.txtItem.Focus()
    End Sub

    Sub FillCompaniesList()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select Distinct CompanyName From Company Where CompanyName Is Not Null Order By CompanyName", cnn)
            Dim Reader As SqlDataReader

            Me.CombCompanyName.Items.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.CombCompanyName.Items.Add(Reader.Item(0))
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

    Private Sub btnClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Sub FillItemsList()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select * From ItemsRegistry Where CompanyName=@CompanyName", cnn)
            Dim Reader As SqlDataReader

            Me.GridItems.Rows.Clear()

            cnn.Open()
            cmd.Parameters.AddWithValue("@CompanyName", Me.CombCompanyName.Text)
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.GridItems.Rows.Add(New String() {Reader.Item("SNo"), Reader.Item("Item"), Reader.Item("GenericName"), Reader.Item("Pack"), _
                                                        Reader.Item("MinLevel"), CDbl(Reader.Item("WPrice")).ToString("N2"), _
                                                        CDbl(Reader.Item("RPrice")).ToString("N2"), "Update", "Delete"})
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

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridItems.CellContentClick
        If e.ColumnIndex = 7 Then
            Try
                Me.Cursor = Cursors.WaitCursor

                Dim cmd As New SqlCommand
                Dim Trans As SqlTransaction

                cnn.Open()
                Trans = cnn.BeginTransaction
                cmd.Transaction = Trans
                cmd.Connection = cnn

                For Each row As DataGridViewRow In Me.GridItems.Rows
                    cmd.CommandText = "Update ItemsRegistry Set " & _
                                        "Item=@Item,GenericName=@GenericName,Pack=@Pack,MinLevel=@MinLevel,WPrice=@WPrice,RPrice=@RPrice " & _
                                        "Where SNo=" & row.Cells(0).Value

                    cmd.Parameters.Clear()
                    cmd.Parameters.AddWithValue("@Item", row.Cells(1).Value)
                    cmd.Parameters.AddWithValue("@GenericName", row.Cells(2).Value)
                    cmd.Parameters.AddWithValue("@Pack", row.Cells(3).Value)
                    cmd.Parameters.AddWithValue("@MinLevel", row.Cells(4).Value)
                    cmd.Parameters.AddWithValue("@WPrice", CDbl(row.Cells(5).Value))
                    cmd.Parameters.AddWithValue("@RPrice", CDbl(row.Cells(6).Value))
                    cmd.ExecuteNonQuery()
                Next

                Trans.Commit()
                cnn.Close()

                MsgBox("Updated Successfully!")

                FillItemsList()

                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                MsgBox(ex.ToString)
            End Try
        ElseIf e.ColumnIndex = 8 Then
            If MsgBox("Confirm delete?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Try
                    Me.Cursor = Cursors.WaitCursor

                    Dim cmd As New SqlCommand("Delete From ItemsRegistry Where SNo=" & Me.GridItems.Rows(e.RowIndex).Cells(0).Value, cnn)

                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()

                    Me.GridItems.Rows.RemoveAt(e.RowIndex)

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

    Private Sub frmItemsRegistry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        FillCompaniesList()
    End Sub

    Private Sub btnNar_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNar.Click
        Dim a As New frmAddCompany
        a.ShowDialog()

        FillCompaniesList()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        clear()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Me.ErrProv.Clear()

        If Me.CombCompanyName.SelectedIndex = -1 Then
            ErrProv.SetError(Me.CombCompanyName, "Please select a CombCompanyName  ")

        ElseIf Me.txtItem.Text.Trim.Length = 0 Then
            ErrProv.SetError(Me.txtItem, "Please Enter Trade Name")

        ElseIf Me.txtGenericName.Text.Trim.Length = 0 Then
            ErrProv.SetError(Me.txtGenericName, "Please Enter Generic Name")

        ElseIf Me.txtPack.Text.Trim.Length = 0 Then
            ErrProv.SetError(Me.txtPack, "Please Enter Item Pack")

        Else
            Try
                Me.Cursor = Cursors.WaitCursor

                Dim cmd As New SqlCommand("Insert Into ItemsRegistry(companyName,Item,GenericName,Pack,MinLevel,WPrice,RPrice) Values " & _
                                          "(@companyName,@Item,@GenericName,@Pack,@MinLevel,@WPrice,@RPrice)", cnn)

                cnn.Open()
                cmd.Parameters.AddWithValue("@companyName", Me.CombCompanyName.Text.Trim)
                cmd.Parameters.AddWithValue("@Item", Me.txtItem.Text.Trim)
                cmd.Parameters.AddWithValue("@GenericName", Me.txtGenericName.Text.Trim)
                cmd.Parameters.AddWithValue("@Pack", Me.txtPack.Text.Trim)
                cmd.Parameters.AddWithValue("@MinLevel", Me.txtMinLevel.Text.Trim)
                cmd.Parameters.AddWithValue("@WPrice", Me.txtWPrice.Text.Trim)
                cmd.Parameters.AddWithValue("@RPrice", Me.txtRPrice.Text.Trim)
                cmd.ExecuteNonQuery()
                cnn.Close()

                FillItemsList()

                clear()

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

    Private Sub CombCompanyName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CombCompanyName.SelectedIndexChanged
        If Me.CombCompanyName.SelectedIndex = -1 Then
            Me.GridItems.Rows.Clear()
        Else
            FillItemsList()
        End If
    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

End Class