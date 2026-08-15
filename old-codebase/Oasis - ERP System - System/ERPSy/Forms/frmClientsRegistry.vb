Imports System.Data.SqlClient

Public Class frmClientsRegistry
    Public SNo As Integer


    Sub FillClientsList()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cond As String
            If Me.RClientName.Checked = True Then
                cond = "Where Name Like N'%" & Me.txtClientNameSearch.Text.Trim & "%'"

            ElseIf Me.RState.Checked = True Then
                cond = "Where State=N'" & Me.combState.SelectedItem & "'"

            ElseIf Me.RSalesMan.Checked = True Then
                cond = "Where SalesMan=N'" & Me.CombSalesMan.SelectedItem & "'"

            ElseIf Me.RMedRepres.Checked = True Then
                cond = "Where MedicalRepresentative like N'%" & Me.TextBox1.Text.Trim & "'"

            ElseIf Me.RAll.Checked = True Then
                cond = ""
            End If

            Dim cmd As New SqlCommand("Select SNo,IsNull(Name,N'') Name,IsNull(Mobile,N'') Mobile,IsNull(ClientClass,N'') ClientClass," & _
                                      "IsNull(State,N'') State,IsNull(Region,N'') Region,IsNull(Area,N'') Area,IsNull(SalesMan,N'') SalesMan," & _
                                      "IsNull(MedicalRepresentative,N'') MedicalRepresentative " & _
                                      "From Clients " & cond & " Order By SNo", cnn)
            Dim Reader As SqlDataReader

            Me.DataGridView1.Rows.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.DataGridView1.Rows.Add(New String() {Reader.Item("SNo"), Reader.Item("Name"), Reader.Item("Mobile"), Reader.Item("ClientClass"), _
                                                        Reader.Item("State"), Reader.Item("Region"), Reader.Item("Area"), Reader.Item("SalesMan"), _
                                                        Reader.Item("MedicalRepresentative"), "Edit"})
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

    Sub FillSalesMan()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim List As ArrayList = GetSalesManList()
            Me.CombSalesMan.Items.Clear()

            For i As Integer = 0 To List.Count - 1
                Me.CombSalesMan.Items.Add(List(i))
            Next

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub FillMedicalRepresentatives()
        'Try
        '    Me.Cursor = Cursors.WaitCursor

        '    Dim List As ArrayList = GetMedicalRepresentativesList()
        '    Me.CombMedRepresentative.Items.Clear()

        '    For i As Integer = 0 To List.Count - 1
        '        Me.CombMedRepresentative.Items.Add(List(i))
        '    Next

        '    Me.Cursor = Cursors.Default
        'Catch ex As Exception
        '    Me.Cursor = Cursors.Default
        '    MsgBox(ex.ToString)
        'End Try
    End Sub

    Sub FillStates()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim List As ArrayList = GetStatesList()
            Me.combState.Items.Clear()

            For i As Integer = 0 To List.Count - 1
                Me.combState.Items.Add(List(i))
            Next

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 9 Then

            Dim a As New frmClientEdit
            a.SNo = Me.DataGridView1.CurrentRow.Cells(0).Value
            a.ShowDialog()

            FillClientsList()
        End If
    End Sub

    Private Sub btnsearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsearch.Click
        FillClientsList()
    End Sub

    Private Sub frmClientsRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        FillStates()
        FillSalesMan()
        FillMedicalRepresentatives()
    End Sub

    Private Sub RClientName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RClientName.CheckedChanged
        'Me.txtClientNameSearch.Enabled = True
        'Me.combState.Enabled = False
        'Me.CombSalesMan.Enabled = False
        'Me.CombMedRepresentative.Enabled = False
        'Me.combState.SelectedIndex = -1
        'Me.CombSalesMan.SelectedIndex = -1
        'Me.CombMedRepresentative.SelectedIndex = -1

    End Sub

    Private Sub RSalesMedical_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RSalesMan.CheckedChanged
        'Me.CombSalesMan.Enabled = True
        'Me.combState.Enabled = False
        'Me.CombMedRepresentative.Enabled = False
        'Me.txtClientNameSearch.Enabled = False
        'Me.combState.SelectedIndex = -1
        'Me.CombMedRepresentative.SelectedIndex = -1
        'Me.txtClientNameSearch.Clear()
    End Sub

    Private Sub RRegion_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RState.CheckedChanged
        'Me.combState.Enabled = True
        'Me.CombSalesMan.Enabled = False
        'Me.CombMedRepresentative.Enabled = False
        'Me.txtClientNameSearch.Enabled = False
        'Me.CombSalesMan.SelectedIndex = -1
        'Me.CombMedRepresentative.SelectedIndex = -1
        'Me.txtClientNameSearch.Clear()
    End Sub

    Private Sub RAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RAll.CheckedChanged
        'Me.CombSalesMan.Enabled = False
        'Me.CombMedRepresentative.Enabled = False
        'Me.txtClientNameSearch.Enabled = False
        'Me.combState.Enabled = False
        'Me.combState.SelectedIndex = -1
        'Me.CombSalesMan.SelectedIndex = -1
        'Me.CombMedRepresentative.SelectedIndex = -1
        'Me.txtClientNameSearch.Clear()
    End Sub

    Private Sub btnPrentList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMedRepres.CheckedChanged
        'Me.CombMedRepresentative.Enabled = True
        Me.txtClientNameSearch.Enabled = False
        Me.combState.Enabled = False
        Me.CombSalesMan.Enabled = False
        Me.txtClientNameSearch.Clear()
        Me.combState.SelectedIndex = -1
        Me.CombSalesMan.SelectedIndex = -1

    End Sub

    Private Sub ToolStripButton1_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton1.Click
        Dim a As New frmClientsAdd
        a.ShowDialog()
    End Sub
End Class