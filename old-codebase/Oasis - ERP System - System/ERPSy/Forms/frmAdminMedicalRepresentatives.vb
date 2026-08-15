Imports System.Data.SqlClient
Imports System.Xml

Public Class frmAdminMedicalRepresentatives

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Me.txtName.Text.Trim.Length = 0 OrElse Me.txtMobile.Text.Trim.Length = 0 Then
                MsgBox("Please Fill All Data ")
            Else
                Me.Cursor = Cursors.WaitCursor

                Dim cmd As New SqlCommand("Insert Into AgentRepresentatives (Name,Mobile) Values (N'" & Me.txtName.Text.Trim & _
                                          "',N'" & Me.txtMobile.Text.Trim & "')", cnn)

                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()

                Me.txtName.Clear()
                Me.txtMobile.Clear()
                Me.txtName.Focus()

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

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.ColumnIndex = 6 Then
            Try
                Dim Name, Mobile, units, Clinics, Pharmacies As String
                Name = Me.DataGridView1.Rows(e.RowIndex).Cells(1).Value
                Mobile = Me.DataGridView1.Rows(e.RowIndex).Cells(2).Value
                units = Me.DataGridView1.Rows(e.RowIndex).Cells(3).Value
                Clinics = Me.DataGridView1.Rows(e.RowIndex).Cells(4).Value
                Pharmacies = Me.DataGridView1.Rows(e.RowIndex).Cells(5).Value

                If Name.Trim.Length = 0 OrElse Mobile.Trim.Length = 0 Then
                    MsgBox("Please Fill In All Data")

                ElseIf units.Trim.Length = 0 OrElse units.Trim.Length > 0 And Not System.Text.RegularExpressions.Regex.IsMatch(units.Trim, "^[0-9]*[0-9]$") Then
                    MsgBox("Please Update RequierdVisitunits by Number Only")
                ElseIf Clinics.Trim.Length = 0 OrElse Clinics.Trim.Length > 0 And Not System.Text.RegularExpressions.Regex.IsMatch(Clinics.Trim, "^[0-9]*[0-9]$") Then
                    MsgBox("Please Update RequierdVisitClinics by Number Only")
                ElseIf Pharmacies.Trim.Length = 0 OrElse Pharmacies.Trim.Length > 0 And Not System.Text.RegularExpressions.Regex.IsMatch(Pharmacies.Trim, "^[0-9]*[0-9]$") Then
                    MsgBox("Please Update RequierdVisitPharmacies by Number Only")
                Else
                    Me.Cursor = Cursors.WaitCursor

                    Dim cmd As New SqlCommand("Update AgentRepresentatives Set Name=N'" & Name & "',Mobile=N'" & Mobile & "', " & _
                                              "requierdVisitUnit= N'" & units & "',requierdVisitClinic=N'" & Clinics & "',requierdVisitPharma=N'" & Pharmacies & "' " & _
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
        ElseIf e.ColumnIndex = 7 Then
            If MsgBox("Confirm delete?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Try
                    Me.Cursor = Cursors.WaitCursor

                    Dim cmd As New SqlCommand("Delete From AgentRepresentatives Where SNo=" & Me.DataGridView1.Rows(e.RowIndex).Cells(0).Value, cnn)

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

    Sub FillList()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim cmd As New SqlCommand("Select SNo,Name,Mobile,IsNull(requierdVisitUnit,'')requierdVisitUnit," & _
                                      "IsNull(requierdVisitClinic,'')requierdVisitClinic,IsNull(requierdVisitPharma,'')requierdVisitPharma " & _
                                      "From AgentRepresentatives", cnn)
            Dim Reader As SqlDataReader

            Me.DataGridView1.Rows.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.DataGridView1.Rows.Add(New String() {Reader.Item(0), Reader.Item(1), Reader.Item(2), _
                                                        Reader.Item(3), Reader.Item(4), Reader.Item(5), "Update", "Delete"})
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

    Private Sub frmAdminSalesRepresentatives_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        FillList()
    End Sub
    Sub clear()
        Me.txtName.Clear()
        Me.txtMobile.Clear()
        Me.txtRequierdVisitUnit.Clear()
        Me.txtRequierdVisitClinic.Clear()
        Me.txtRequierdVisitPharma.Clear()
        Me.txtName.Focus()
    End Sub
    Private Sub btnSave_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Me.txtName.Text.Trim.Length = 0 Then
                MsgBox("Please Fill In Name Data")
            ElseIf txtMobile.Text.Trim.Length = 0 Then
                MsgBox("Please Fill In Mobile Data")
            ElseIf Me.txtRequierdVisitClinic.Text.Trim.Length = 0 OrElse Me.txtRequierdVisitClinic.Text.Trim.Length > 0 And Not System.Text.RegularExpressions.Regex.IsMatch(Me.txtRequierdVisitClinic.Text.Trim, _
"^[0-9]*[0-9]$") Then
                MsgBox("Please Fill RequierdVisitClinic by Number Only")
            ElseIf Me.txtRequierdVisitPharma.Text.Trim.Length = 0 OrElse Me.txtRequierdVisitPharma.Text.Trim.Length > 0 And Not System.Text.RegularExpressions.Regex.IsMatch(Me.txtRequierdVisitPharma.Text.Trim, _
                 "^[0-9]*[0-9]$") Then
                MsgBox("Please Fill RequierdVisitPharma by Number Only")
            ElseIf Me.txtRequierdVisitUnit.Text.Trim.Length = 0 OrElse Me.txtRequierdVisitUnit.Text.Trim.Length > 0 And Not System.Text.RegularExpressions.Regex.IsMatch(Me.txtRequierdVisitUnit.Text.Trim, _
                           "^[0-9]*[0-9]$") Then
                MsgBox("Please Fill RequierdVisitUnit by Number Only")

            Else
                Me.Cursor = Cursors.WaitCursor
                Dim cmd As New SqlCommand()
                cnn.Open()
                cmd.Connection = cnn
                cmd.CommandText = "Insert Into AgentRepresentatives (Name,Mobile,requierdVisitUnit,requierdVisitClinic,requierdVisitPharma)" & _
                                         " Values (@Name,@Mobile,@requierdVisitUnit,@requierdVisitClinic,@requierdVisitPharma)"
                cmd.Parameters.AddWithValue("@Name", Me.txtName.Text.Trim)
                cmd.Parameters.AddWithValue("@Mobile", Me.txtMobile.Text.Trim)
                cmd.Parameters.AddWithValue("@requierdVisitUnit", Me.txtRequierdVisitUnit.Text.Trim)
                cmd.Parameters.AddWithValue("@requierdVisitClinic", Me.txtRequierdVisitClinic.Text.Trim)
                cmd.Parameters.AddWithValue("@requierdVisitPharma", Me.txtRequierdVisitPharma.Text.Trim)
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
End Class