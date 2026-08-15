Imports System.Data.SqlClient

Public Class frmHrAuthorities

    Private Sub frmHrAuthorities_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillUsers()
    End Sub
    Sub FillUsers()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim cmd1 As New SqlCommand("Select SNo,FullName From Users Order By SNo", cnnLogin)
            Dim SqlReader As SqlDataReader

            Me.CombUser.Items.Clear()

            cnnLogin.Open()
            SqlReader = cmd1.ExecuteReader
            While SqlReader.Read
                Me.CombUser.Items.Add(SqlReader.Item(1))
            End While
            cnnLogin.Close()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
            If cnnLogin.State = ConnectionState.Open Then
                cnnLogin.Close()
            End If
        End Try
    End Sub


    Private Sub CombUser_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CombUser.SelectedIndexChanged
        Try
            If Me.CombUser.SelectedIndex = -1 Then
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            Dim cmd As New SqlCommand("Select * From Users where FullName=N'" & Me.CombUser.SelectedItem & "'", cnnLogin)
            Dim Reader As SqlDataReader

            cnnLogin.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read

                Me.ChSP.Checked = CBool(Reader.Item("ChSP"))
                Me.ChCont.Checked = CBool(Reader.Item("ChkCont"))
                Me.ChNewJb.Checked = CBool(Reader.Item("chkNewJb"))
                Me.ChPSht.Checked = CBool(Reader.Item("ChkPaySht"))
                Me.ChkHRApproval.Checked = CBool(Reader.Item("ChkHRApproval"))
                Me.ChkVReq.Checked = CBool(Reader.Item("ChkVReq"))
                Me.ChkSupApproval.Checked = CBool(Reader.Item("ChkSupApproval"))
                Me.ChkLP.Checked = CBool(Reader.Item("ChkLP"))
                Me.ChHR.Checked = CBool(Reader.Item("ChkHR"))
              
            End While
            cnnLogin.Close()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
            If cnnLogin.State = ConnectionState.Open Then
                cnnLogin.Close()
            End If
        End Try
    End Sub

    Sub UnCheckAll()
        Me.ChSP.Checked = False
        Me.ChCont.Checked = False
        Me.ChNewJb.Checked = False
        Me.ChPSht.Checked = False
        Me.ChkHRApproval.Checked = False
        Me.ChkSupApproval.Checked = False
        Me.ChkVReq.Checked = False
        Me.ChkLP.Checked = False
        Me.ChHR.Checked = False
       
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Me.CombUser.SelectedIndex = -1 Then
            MsgBox("Please select a valid user")
        Else
            Try
                Dim strUpdate As String
                strUpdate = "Update Users Set " & _
                            " ChSP=" & CInt(Me.ChSP.CheckState) & _
                            ",ChkCont=" & CInt(Me.ChCont.CheckState) & _
                            ",chkNewJb=" & CInt(Me.ChNewJb.CheckState) & _
                            ",ChkPaySht=" & CInt(Me.ChPSht.CheckState) & _
                            ",ChkHRApproval=" & CInt(Me.ChkHRApproval.CheckState) & _
                            ",ChkSupApproval=" & CInt(Me.ChkSupApproval.CheckState) & _
                            ", ChkVReq=" & CInt(Me.ChkVReq.CheckState) & _
                            ",ChkLP=" & CInt(Me.ChkLP.CheckState) & _
                            ",ChkHR=" & CInt(Me.ChHR.CheckState) & _
                            " Where FullName = N'" & Me.CombUser.SelectedItem & "'"

                Dim cmd As New SqlCommand(strUpdate, cnnLogin)

                cnnLogin.Open()
                cmd.ExecuteNonQuery()
                cnnLogin.Close()

                MsgBox("Saved Successfully")

                UnCheckAll()
                Me.CombUser.SelectedIndex = -1
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                If cnnLogin.State = ConnectionState.Open Then
                    cnnLogin.Close()
                End If
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

End Class