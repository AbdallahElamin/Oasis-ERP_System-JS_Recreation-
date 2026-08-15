Imports System.Data.SqlClient

Public Class frmAddRegions

    Public Level As Integer
    Public State, Region, Area, RegionsName As String
    Public Saved As Boolean

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.ErrorProvider1.Clear()
        Try
            Me.Cursor = Cursors.WaitCursor
            If Me.txtAddRegions.Text.Trim.Length = 0 Then
                MsgBox("Please Enter Regions Name")
            Else
                Dim StrIns As String

                Select Case Level
                    Case 0
                        StrIns = "Insert Into Regions (State) Values (N'" & Me.txtAddRegions.Text & "')"
                    Case 1
                        StrIns = "Insert Into Regions (State,Region) Values (N'" & State & "',N'" & Me.txtAddRegions.Text & "')"
                    Case 2
                        StrIns = "Insert Into Regions (State,Region,Area) Values (N'" & State & "',N'" & Region & "',N'" & Me.txtAddRegions.Text & "')"

                End Select

                Dim cmd As New SqlCommand(StrIns, cnn)

                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()

                RegionsName = Me.txtAddRegions.Text
                Saved = True
                Me.Cursor = Cursors.Default
                Me.Close()
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub frmAddAccount_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Saved = False
    End Sub
End Class