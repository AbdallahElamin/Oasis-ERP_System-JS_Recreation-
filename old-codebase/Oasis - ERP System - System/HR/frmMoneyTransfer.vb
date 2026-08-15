Imports System.Data.SqlClient
Imports EgyCurr.CurText

Public Class frmMoney_Transfer

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("update BankAccounts set TotalAr=N'" & Me.txtAmountChar.Text & "', Total=" & Me.txtAmount.Text, cnn)
            cnn.Open()
            cmd.ExecuteNonQuery()
            cnn.Close()
            PrintRpt()
            Me.Cursor = Cursors.Default

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            MsgBox(ex.ToString)
        End Try

    End Sub

    Sub PrintRpt()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dap As New SqlDataAdapter("select * from BankAccounts ", cnn)
            Dim das As New DataSet

            dap.Fill(das, "BankAccounts")

            Dim rpt As New RptMoneyTransfer
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
    Private Sub frmMoney_Transfer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillGrid()
        Write()
    End Sub
    Sub FillGrid()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim cmd As New SqlCommand("select Sno, NameAr, AccountNo,NetSalary from BankAccounts ", cnn)
            Dim Reader As SqlDataReader

            Me.DataGridView1.Rows.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.DataGridView1.Rows.Add(New String() {Reader.Item("Sno"), Reader.Item("NameAr"), Reader.Item("AccountNo"), Reader.Item("NetSalary")})
            End While
            cnn.Close()
            Total()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
        End Try

    End Sub

    Sub FillEmpData()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select EmpName,jobTitle,NetSalary From EmployeeSalary Where EmpID=N'" & Me.TxtEmpNo.Text & "'", cnn)
            Dim Reader As SqlDataReader

            Me.txtNameEn.Clear()
            Me.txtJobTitle.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.txtNameEn.Text = Reader.Item("EmpName")
                Me.txtJobTitle.Text = Reader.Item("jobTitle")
                Me.TxtNetSalary.Text = Reader.Item("NetSalary")
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
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        If Me.txtNameEn.Text.Trim.Length = 0 Then
            MsgBox("الرجاء إدخال رقم الموظف ")
        ElseIf Me.TxtNetSalary.Text = 0 Then
            MsgBox("الرجاء إدخال المبلغ المراد صرفه للموظف")
            'ElseIf Me.txtEmpNameAr.Text.Trim.Length = 0 Or Me.txtAccountNo.Text.Trim.Length = 0 Then
            '    MsgBox("الرجاء إدخال إسم الموظف ورقم الحساب باللغة العربية ")
        Else
            Try

                Me.Cursor = Cursors.WaitCursor

                Dim cmd1 As New SqlCommand("select count(*) from BankAccounts where EmpID=N'" & Me.TxtEmpNo.Text & "'", cnn)
                cnn.Open()
                If cmd1.ExecuteScalar > 0 Then
                    Dim cmd2 As New SqlCommand("delete from BankAccounts where EmpID=N'" & Me.TxtEmpNo.Text & "'", cnn)
                    cmd2.ExecuteNonQuery()
                End If
                cnn.Close()

                Dim cmd As New SqlCommand
                Dim Trans As SqlTransaction

                cnn.Open()
                cmd.Connection = cnn
                Trans = cnn.BeginTransaction
                cmd.Transaction = Trans

                cmd.CommandText = "insert  into BankAccounts (  EmpID, NameEn, NetSalary, NameAr, JobTitle, AccountNo) " & _
                                          "values(N'" & Me.TxtEmpNo.Text & "',N'" & Me.txtNameEn.Text & "',N'" & CDbl(Me.TxtNetSalary.Text) & _
                                         "',N'" & Me.txtEmpNameAr.Text & "' ,N'" & Me.txtJobTitle.Text & "',N'" & Me.txtAccountNo.Text & "')"

                cmd.ExecuteNonQuery()
                Trans.Commit()
                cnn.Close()

                FillGrid()
                Total()
                Write()
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

    Sub Clear()
        Me.txtNameEn.Text = ""
        Me.txtJobTitle.Text = ""
        Me.TxtNetSalary.Text = 0
        Me.txtEmpNameAr.Text = ""
        '  Me.txtAccountNo.Text = ""

    End Sub
    Sub Write()
        Try
            If Me.txtAmount.Text.Trim.Length = 0 Then
                Me.txtAmountChar.Clear()
            Else
                Me.txtAmountChar.Text = ChangeTo(CDbl(Me.txtAmount.Text)).ToString
                Me.txtAmountChar.Text = Me.txtAmountChar.Text.Replace("(", "")
                Me.txtAmountChar.Text = Me.txtAmountChar.Text.Replace(")", "")
            End If
        Catch ex As Exception
            Me.txtAmount.Clear()
            Me.txtAmount.Focus()
        End Try
    End Sub
    Sub Total()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select IsNull(Sum(NetSalary),0) From BankAccounts ", cnn)
            Dim Reader As SqlDataReader
            Dim a As Double


            Me.txtAmount.Text = ""

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                a = Reader.Item(0)
                Me.txtAmount.Text = a

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

    Private Sub TxtEmpNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtEmpNo.TextChanged
        Clear()
    End Sub

    Private Sub TxtEmpNo_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtEmpNo.KeyUp
        If e.KeyCode = Keys.Enter Then
            FillEmpData()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        FillEmpData()
    End Sub

    Private Sub txtAmount_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAmount.TextChanged
        Write()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim a As New frmSearchEmpID
        a.ShowDialog()
        If SelPatIDNo = "" Then
            Exit Sub
        End If

        Me.TxtEmpNo.Text = SelPatIDNo
        FillEmpData()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If Me.DataGridView1.SelectedRows.Count = 0 Then
            MsgBox("الرجاء إختيار إسم من القائمة")
            Exit Sub
        End If
        Try
            Me.Cursor = Cursors.WaitCursor
            If MsgBox("تأكيد الحذف؟", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then


                Dim cmd As New SqlCommand("delete from BankAccounts where SNo=" & Me.DataGridView1.CurrentRow.Cells(0).Value, cnn)
                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()
                FillGrid()


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

    Private Sub TxtNetSalary_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtNetSalary.KeyPress

        If Char.IsDigit(e.KeyChar) _
       OrElse e.KeyChar = ","c _
           OrElse e.KeyChar = "."c OrElse _
          Char.GetUnicodeCategory(e.KeyChar) = Globalization.UnicodeCategory.Control OrElse _
                   Char.GetUnicodeCategory(e.KeyChar) _
               = Globalization.UnicodeCategory.CurrencySymbol _
                    OrElse Char.GetUnicodeCategory(e.KeyChar) = Globalization.UnicodeCategory.Format Then

            e.Handled = False

        Else
            e.Handled = True

        End If
    End Sub

    Private Sub TxtNetSalary_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNetSalary.Validated
        Me.TxtNetSalary.Text = CDbl(Me.TxtNetSalary.Text).ToString("N2")
    End Sub
End Class