Imports System.Data.SqlClient

Public Class frmRegionStatesSetup

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim UnitName As String

            UnitName = InputBox("please enter name of state")

            If UnitName = "" Then
                Me.Cursor = Cursors.Default
                Exit Sub
            Else
                Dim cmd As New SqlCommand("Insert Into Regions (State) Values (N'" & UnitName & "')", cnn)

                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()

                FillTree()
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

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Try
            If Me.TreeRegions.SelectedNode.Index = -1 Then
                Exit Sub
            Else
                Me.Cursor = Cursors.WaitCursor

                Dim ServiceName As String

                ServiceName = InputBox("please enter name of Region")

                If ServiceName = "" Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                Else
                    Dim StrIns As String

                    If Me.TreeRegions.SelectedNode.Level = 0 Then
                        StrIns = "Insert Into Regions (State,Region) Values (N'" & Me.TreeRegions.SelectedNode.Text & "',N'" & ServiceName & "')"
                    End If

                    Dim cmd As New SqlCommand(StrIns, cnn)

                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()

                    FillTree()
                End If
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

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Try
            If Me.TreeRegions.SelectedNode.Level = -1 Then
                Exit Sub
            Else
                If MsgBox("confirming delete?", MsgBoxStyle.YesNoCancel) = MsgBoxResult.Yes Then
                    Me.Cursor = Cursors.WaitCursor
                    Dim StrIns As String

                    Select Case Me.TreeRegions.SelectedNode.Level
                        Case 0
                            StrIns = "Delete From Regions Where State=N'" & Me.TreeRegions.SelectedNode.Text & "'"
                        Case 1
                            StrIns = "Delete From Regions Where State=N'" & Me.TreeRegions.SelectedNode.Parent.Text & _
                                     "' and Region=N'" & Me.TreeRegions.SelectedNode.Text & "'"
                    End Select

                    Dim cmd As New SqlCommand(StrIns, cnn)

                    cnn.Open()
                    cmd.ExecuteNonQuery()
                    cnn.Close()

                    FillTree()
                    Me.Cursor = Cursors.Default
                End If
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub FillTree()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim cmd As New SqlCommand("Select Distinct State From Regions Where State Is Not Null Order By State", cnn)
            Dim Reader, Reader1 As SqlDataReader
            Dim i As Integer

            Me.TreeRegions.Nodes.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.TreeRegions.Nodes.Add(Reader.Item(0))
                Dim cmd1 As New SqlCommand("Select Distinct Region From Regions Where State=N'" & Reader.Item(0) & _
                                           "' and Region Is Not Null Order By Region", cnn1)

                cnn1.Open()
                Reader1 = cmd1.ExecuteReader
                While Reader1.Read
                    Me.TreeRegions.Nodes(i).Nodes.Add(Reader1.Item(0))
                End While

                cnn1.Close()
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
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub frmAdminSalesRegions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillTree()
    End Sub
End Class