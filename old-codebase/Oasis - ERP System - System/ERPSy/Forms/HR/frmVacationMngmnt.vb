Imports System.Data.SqlClient

Public Class frmVacationMngmnt

    Sub GetNoOfDays()
        Dim DateOne As Date
        Dim DateTwo As Date
        Dim arrDate As New ArrayList
        Dim iNumOfDays As Long
        Dim iLoop As Integer

        DateOne = New Date(DtTo.Value.Year, DtTo.Value.Month, DtTo.Value.Day)
        DateTwo = New Date(DtFrom.Value.Year, DtFrom.Value.Month, DtFrom.Value.Day)

        iNumOfDays = DateDiff(DateInterval.Day, DateTwo, DateOne)

        For iLoop = 0 To iNumOfDays
            arrDate.Add(DateOne.AddDays(iLoop))
        Next

        For iLoop = 0 To arrDate.Count - 1
            Me.txtDays.Text = arrDate.Count

        Next
    End Sub

    Sub FillGrid()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim cmd As New SqlCommand("select Sno,Name,Position,DateFrom,DateTo,NoOfDays from LeavePlan order by DateFrom", cnn)
            Dim Reader As SqlDataReader

            Me.DataGridView1.Rows.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.DataGridView1.Rows.Add(New String() {Reader.Item("Sno"), Reader.Item("Name"), Reader.Item("Position"), Reader.Item("DateFrom"), Reader.Item("DateTo"), Reader.Item("NoOfDays")})
            End While
            cnn.Close()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
        End Try

    End Sub

    Sub Clear()
        Me.txtDays.Text = ""
        Me.txtPosition.Text = ""
        Me.txtEmpNo.Text = ""
        Me.TxtName.Text = ""
        Me.txtDays.Text = 0
        Me.DTFrom.Value = Now
        Me.DTTo.Value = Now
    End Sub

    Private Sub DTTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTTo.ValueChanged
        GetNoOfDays()
    End Sub

    Private Sub DTFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTFrom.ValueChanged
        GetNoOfDays()
    End Sub

    Sub FillEmpData()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select Name,jobDesc From StaffProfiles Where Sno=N'" & Me.txtEmpNo.Text & "'", cnn)
            Dim Reader As SqlDataReader

            Me.TxtName.Clear()
            Me.txtPosition.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.TxtName.Text = Reader.Item(0)
                Me.txtPosition.Text = Reader.Item(1)

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

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim cmd As New SqlCommand("insert into LeavePlan values (N'" & Me.txtEmpNo.Text & "',N'" & Me.TxtName.Text & _
                                      "',N'" & Me.txtPosition.Text & "',N'" & Me.DTFrom.Value.ToShortDateString & _
                                      "',N'" & Me.DTTo.Value.ToShortDateString & "'," & Me.txtDays.Text & ")", cnn)
            cnn.Open()
            cmd.ExecuteNonQuery()
            cnn.Close()
            MsgBox("Saved successfully")
            FillGrid()
            Clear()

            Me.Cursor = Cursors.Default

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
                MsgBox(ex.ToString)
            End If
        End Try
    End Sub

    Private Sub txtEmpNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmpNo.TextChanged
        Me.TxtName.Text = ""
        Me.txtPosition.Text = ""
    End Sub

    Private Sub txtEmpNo_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtEmpNo.KeyUp
        If e.KeyCode = Keys.Enter Then
            If Me.txtEmpNo.Text.Trim.Length > 0 Then
                FillEmpData()
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.txtEmpNo.Clear()
        Dim a As New frmSearchEmpID
        a.ShowDialog()

        If SelPatIDNo = 0 Then
            Exit Sub
        End If

        Me.txtEmpNo.Text = SelPatIDNo
        FillEmpData()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If Me.txtEmpNo.Text.Trim.Length > 0 Then
            FillEmpData()
        End If

    End Sub

    Private Sub frmVacationMngmnt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillGrid()
    End Sub
End Class