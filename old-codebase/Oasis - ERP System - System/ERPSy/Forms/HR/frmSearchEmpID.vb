Imports System.Data.SqlClient

Public Class frmSearchEmpID

    Private Sub frmSearchEmpID_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Sub LoadEmp()
        Try
            Dim cmd As New SqlCommand("SELECT EmpID,Name from StaffProfiles Where Name like N'%" & Me.txtEmpName.Text & "%' Order by Sno", cnn)
            Dim Reader As SqlDataReader
            Me.ListView1.Items.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While (Reader.Read)
                With ListView1.Items.Add(Reader.Item(0))
                    .SubItems.Add(Reader.Item(1))
                End With
            End While
            cnn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
        End Try
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Len(Me.txtEmpName.Text.Trim) = 0 Then
            Exit Sub
        End If

        LoadEmp()
    End Sub

    Private Sub txtCustName_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtEmpName.KeyUp
        If e.KeyCode = Keys.Enter Then
            If Len(Me.txtEmpName.Text.Trim) = 0 Then
                Exit Sub
            End If

            LoadEmp()
        End If
    End Sub

    Private Sub frmSearchCust_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SelPatIDNo = ""
        SelPatName = ""
        Me.txtEmpName.Focus()
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        If Me.ListView1.SelectedIndices.Count <> 0 Then
            SelPatIDNo = Me.ListView1.SelectedItems.Item(0).Text
            SelPatName = Me.ListView1.SelectedItems.Item(0).SubItems(1).Text
            Me.Close()
        End If
    End Sub
End Class