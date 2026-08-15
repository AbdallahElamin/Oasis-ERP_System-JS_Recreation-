Imports System.Data.SqlClient

Public Class frmDepartmentEn

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If Me.txtSysType.Text.Trim.Length = 0 Then
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            Dim cmd As New SqlCommand("Insert Into Department (Department) Values (N'" & Me.txtSysType.Text.Trim & "')", cnn)

            cnn.Open()
            cmd.ExecuteNonQuery()
            cnn.Close()

            Filllistview()

            Me.txtSysType.Clear()
            Me.txtSysType.Focus()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub
    Sub Filllistview()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select Distinct Department From Department where Department is not null", cnn)
            Dim Reader As SqlDataReader

            Me.ListItem.Items.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Dim Item As New ListBox
                Me.ListItem.Items.Add(Reader.Item(0))
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

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Me.ListItem.SelectedIndex <> -1 Then
            If MsgBox("Confirm delete?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Try
                    Me.Cursor = Cursors.WaitCursor
                    Dim cmd As New SqlCommand("Delete From Department Where Department=N'" & Me.ListItem.SelectedItem & "'", cnn)

                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()

                    Filllistview()

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

    Private Sub frmDepartmentEn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Filllistview()
    End Sub
End Class