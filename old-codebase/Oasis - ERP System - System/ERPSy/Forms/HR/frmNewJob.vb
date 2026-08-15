Imports System.Data.SqlClient

Public Class frmNewJob


    Sub FillComboDepartment()

        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select Distinct Department From Department where Department is not null ", cnn)
            Dim Reader As SqlDataReader

            Me.ComboDepartment.Items.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Dim Item As New ComboBox
                Me.ComboDepartment.Items.Add(Reader.Item(0))
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

   
    Sub clear()
        Me.ComboJobDescribtion.SelectedIndex = -1
        Me.ComboLevel.SelectedIndex = -1
        Me.txtMaxAge.Text = ""
        Me.txtMinAge.Text = ""
        Me.txtPreReq.Text = ""
        Me.txtQualification.Text = ""
        Me.ComboReplace.Text = ""
        Me.txtTotalSal.Text = ""
        Me.ComboDepartment.SelectedIndex = -1
        Me.ChkPartTime.CheckState = 0
        Me.ChkPermanent.CheckState = 0
        Me.ChkTemporary.CheckState = 0
        Me.RadLabor.Checked = True
        Me.RadEmployee.Checked = True


    End Sub

    Sub PrintRpt()
        Try

            Me.Cursor = Cursors.WaitCursor
            Dim dap As New SqlDataAdapter("select * from NewJob where SNO=(select max(SNO) from NewJob)", cnn)
            Dim das As New DataSet
            dap.Fill(das, "NewJob")

            Dim rpt As New rptNewJob
            rpt.SetDataSource(das)
            frmReportViewer.CrystalReportViewer1.ReportSource = rpt
            frmReportViewer.CrystalReportViewer1.RefreshReport()
            frmReportViewer.ShowDialog()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn1.State = ConnectionState.Open Then
                cnn1.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub frmNewJob_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillJobDescribtion()
        FillComboLevel()
        FillComboDepartment()
        Me.RadEmployee.Checked = True
    End Sub

    Sub FillJobDescribtion()
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.ComboJobDescribtion.Items.Clear()
            Dim cmd As New SqlCommand("select Distinct JobDescribtionEn From JobDescribtion where JobDescribtionEn is not null ", cnn)
            Dim rdr As SqlDataReader

            cnn.Open()
            rdr = cmd.ExecuteReader
            While rdr.Read
                Me.ComboJobDescribtion.Items.Add(rdr.Item(0))
            End While
            cnn.Close()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            MsgBox(ex.Message)
        End Try
    End Sub
    Sub FillComboLevel()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select Distinct GradeLevel From Levels where GradeLevel is not null  ", cnn)
            Dim Reader As SqlDataReader

            Me.ComboLevel.Items.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Dim Item As New ComboBox
                Me.ComboLevel.Items.Add(Reader.Item(0))
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

    Sub listArcive()
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.ListArchive1.Items.Clear()
            Dim cmd As New SqlCommand("select SNo,Department, JobDescribtion, StartDate from NewJob where StartDate >'" & Me.DateTimeFrom.Value.ToString("MM/dd/yyyy 00:00:01") & _
                                      "'  and StartDate < '" & Me.DateTimeTo.Value.ToString("MM/dd/yyyy 23:59:59") & "' order by Startdate", cnn)
            Dim Reader As SqlDataReader
            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read

                With ListArchive1.Items.Add(Reader.Item(0))
                    .subitems.add(Reader.Item(1))
                    .subitems.add(Reader.Item(2))
                    .subitems.add(Format(CDate(Reader.Item(3)), "dd-MMM-yyyy"))
                End With
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

    Private Sub DateTimeFrom_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimeFrom.ValueChanged
        listArcive()
    End Sub

    Private Sub DateTimeTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimeTo.ValueChanged
        listArcive()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If Me.ListArchive1.SelectedItems.Count = 0 Then
            MsgBox("please select Item from list")
            Exit Sub
        End If
        Try
            Me.Cursor = Cursors.WaitCursor
            If MsgBox("confirm delete ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then


                Dim cmd As New SqlCommand("delete from NewJob where SNo=" & Me.ListArchive1.SelectedItems(0).Text, cnn)
                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()
                listArcive()
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

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dap As New SqlDataAdapter("select  * from NewJob where  StartDate > N'" & Me.DateTimeFrom.Value.ToString("MM/dd/yyyy") & _
                                      " 00:00:01' and StartDate < N'" & Me.DateTimeTo.Value.ToString("MM/dd/yyyy ") & " 23:59:59' ", cnn)
            Dim das As New DataSet

            dap.Fill(das, "NewJob")

            Dim rpt As New rptNewJobList
            rpt.SetDataSource(das)
            frmReportViewer.CrystalReportViewer1.ReportSource = rpt
            frmReportViewer.CrystalReportViewer1.RefreshReport()
            frmReportViewer.ShowDialog()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn1.State = ConnectionState.Open Then
                cnn1.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Button31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button31.Click
        If Me.ComboDepartment.SelectedIndex = -1 Or Me.ComboJobDescribtion.SelectedIndex = -1 Or Me.ComboReplace.Text = "" _
            Or Me.txtTotalSal.Text = "" Then
            MsgBox("Please complete the necessary fields ")
        Else
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim cmd As New SqlCommand("insert into NewJob ( Department, levl, StartDate, JobDescribtion," & _
                                          "TotalSalary, Replace, Qualifications, MinAge, MaxAge, Experience, Perminant," & _
                                          "Temporary, PartTime, Employee, Labor) values (N'" & Me.ComboDepartment.SelectedItem & _
                                          "',N'" & Me.ComboLevel.Text & "',N'" & Me.DateTimePicker1.Value.ToString & _
                                          "',N'" & Me.ComboJobDescribtion.Text & "',N'" & Me.txtTotalSal.Text & "',N'" & ComboReplace.Text & _
                                          "','" & txtQualification.Text & "',N'" & Me.txtMinAge.Text & "','" & Me.txtMaxAge.Text & _
                                          "',N'" & Me.txtPreReq.Text & "'," & Me.ChkPermanent.CheckState & _
                                          "," & Me.ChkTemporary.CheckState & "," & Me.ChkPartTime.CheckState & _
                                          "," & CInt(Me.RadEmployee.Checked) & "," & CInt(Me.RadLabor.Checked) & ")", cnn)
                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()
                MsgBox("Saved Successfully")
                PrintRpt()
                clear()
                Me.Cursor = Cursors.Default

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                    MsgBox(ex.ToString)
                End If

            End Try

        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim a As New frmJobDescEn
        a.ShowDialog()
        FillJobDescribtion()
    End Sub

    Private Sub Button32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button32.Click
        Me.Close()
    End Sub

    Private Sub ListArchive1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListArchive1.DoubleClick


        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dap As New SqlDataAdapter("select  * from NewJob where SNo = " & Me.ListArchive1.SelectedItems(0).Text & " ", cnn)
            Dim das As New DataSet

            dap.Fill(das, "NewJob")

            Dim rpt As New rptNewJob
            rpt.SetDataSource(das)
            frmReportViewer.CrystalReportViewer1.ReportSource = rpt
            frmReportViewer.CrystalReportViewer1.RefreshReport()
            frmReportViewer.ShowDialog()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn1.State = ConnectionState.Open Then
                cnn1.Close()
            End If
            MsgBox(ex.ToString)
        End Try

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim a As New frmDepartmentEn
        a.ShowDialog()
        FillComboDepartment()

    End Sub
End Class