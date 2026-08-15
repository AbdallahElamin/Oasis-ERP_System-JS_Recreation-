Imports System.Data.SqlClient

Public Class frmRegionsStatesArea

    Sub FillTree()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim cmd As New SqlCommand("Select Distinct State From Regions Where State Is Not Null Order By State", cnn)
            Dim Reader, Reader1, Reader2 As SqlDataReader
            Dim i, i1, i2 As Integer

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
                    Dim cmd2 As New SqlCommand("Select Distinct Area From Regions Where Region=N'" & Reader.Item(0) & "' and " & _
                                               "Area=N'" & Reader1.Item(0) & "' and Area Is Not Null", cnn2)

                    cnn2.Open()
                    Reader2 = cmd2.ExecuteReader
                    While Reader2.Read
                        Me.TreeRegions.Nodes(i).Nodes(i1).Nodes.Add(Reader2.Item(0))
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
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim a As New frmAddRegions
        a.Level = 0
        a.ShowDialog()
        If a.Saved = True Then
            Me.TreeRegions.Nodes.Add(a.RegionsName)
        End If
    End Sub

    Private Sub frmMngChrtOfAcc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillTree()
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Try
            If Me.TreeRegions.SelectedNode.Index = -1 Then
                Exit Sub
            Else
                Dim a As New frmAddRegions
                Select Case Me.TreeRegions.SelectedNode.Level
                    Case 0
                        a.State = Me.TreeRegions.SelectedNode.Text
                        a.Level = 1
                    Case 1
                        a.State = Me.TreeRegions.SelectedNode.Parent.Text
                        a.Region = Me.TreeRegions.SelectedNode.Text
                        a.Level = 2
                    Case 2
                        a.State = Me.TreeRegions.SelectedNode.Parent.Parent.Text
                        a.Region = Me.TreeRegions.SelectedNode.Parent.Text
                        a.Area = Me.TreeRegions.SelectedNode.Text
                        a.Level = 3
                End Select
                a.ShowDialog()

                If a.Saved = True Then
                    Me.TreeRegions.SelectedNode.Nodes.Add(a.RegionsName)
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

            If MsgBox("Confirm delete?", MsgBoxStyle.YesNoCancel) = MsgBoxResult.Yes Then
                Me.Cursor = Cursors.WaitCursor
                Dim StrIns As String

                Select Case Me.TreeRegions.SelectedNode.Level
                    Case 0
                        StrIns = "Delete From Regions Where State=N'" & Me.TreeRegions.SelectedNode.Text & "'"
                    Case 1
                        StrIns = "Delete From Regions Where State=N'" & Me.TreeRegions.SelectedNode.Parent.Text & _
                                 "' and Region=N'" & Me.TreeRegions.SelectedNode.Text & "'"
                    Case 2
                        StrIns = "Delete From Regions Where State=N'" & Me.TreeRegions.SelectedNode.Parent.Parent.Text & _
                                 "' and Region=N'" & Me.TreeRegions.SelectedNode.Parent.Text & _
                                 "' and Area=N'" & Me.TreeRegions.SelectedNode.Text & "'"
                End Select
                Dim cmd As New SqlCommand(StrIns, cnn)

                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()

                FillTree()
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


    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        FillTree()
    End Sub

End Class