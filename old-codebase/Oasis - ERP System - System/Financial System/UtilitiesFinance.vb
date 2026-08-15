Imports System.Data.SqlClient

Module UtilitiesFinance

    Public SelAcc1, SelAcc2, SelAcc3, SelAcc4 As String

    Public Function GetMoveNo(ByVal Year As Integer) As Integer
        Try
            Dim MoveNo As Integer
            Dim cmdMoveNo As New SqlCommand("Select IsNull(Max(MoveNo),0) From Transactions Where Year(TransDate)=" & Year.ToString, cnn1)

            cnn1.Open()
            MoveNo = CInt(cmdMoveNo.ExecuteScalar.ToString) + 1
            cnn1.Close()

            Return MoveNo
        Catch ex As Exception
            If cnn1.State = ConnectionState.Open Then
                cnn1.Close()
            End If
            Return 1
        End Try
    End Function

    Public Function GetDocSNo(ByVal DocType As String) As Integer
        Try
            Dim MoveNo As Integer
            Dim cmdMoveNo As New SqlCommand("Select Max(SNo2) From Transactions Where Transtype=N'" & DocType & "'", cnn1)

            cnn1.Open()
            MoveNo = CInt(cmdMoveNo.ExecuteScalar.ToString) + 1
            cnn1.Close()

            Return MoveNo
        Catch ex As Exception
            If cnn1.State = ConnectionState.Open Then
                cnn1.Close()
            End If
            Return 1
        End Try
    End Function

    Public Sub PrintBill(ByVal TransType As String, ByVal PaymentType As String, ByVal SNo As Integer)
        Try

            Dim StrSel As String
            If TransType = "Pay Voucher" Then
                StrSel = "Select * From Transactions Where TransType=N'Pay Voucher' and PaymentType=N'" & PaymentType & "' and TotalIn<>0 and SNo2=" & SNo

            ElseIf TransType = "Receipt Voucher" Then
                StrSel = "Select * From Transactions Where TransType=N'Receipt Voucher' and PaymentType=N'" & PaymentType & "' and TotalOut<>0 and SNo2=" & SNo
            End If

            Dim dap As New SqlDataAdapter(StrSel, cnn)
            Dim das As New DataSet

            dap.Fill(das, "Transactions")

            If TransType = "Pay Voucher" Then
                Dim rpt As New PayVoucher
                rpt.SetDataSource(das)
                rptViewer.CrystalReportViewer1.ReportSource = rpt
                rptViewer.CrystalReportViewer1.RefreshReport()
                rptViewer.ShowDialog()

            ElseIf TransType = "Receipt Voucher" Then
                Dim rpt As New ReceiptVoucher
                rpt.SetDataSource(das)
                rptViewer.CrystalReportViewer1.ReportSource = rpt
                rptViewer.CrystalReportViewer1.RefreshReport()
                rptViewer.ShowDialog()
            End If


        Catch ex As Exception
            If cnn1.State = ConnectionState.Open Then
                cnn1.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub PrintStudantReceipt(ByVal TransType As String, ByVal PaymentType As String, ByVal SNo As Integer)
        Try

            Dim StrSel As String
            If TransType = "Pay Voucher" Then
                StrSel = "Select * From Transactions Where TransType=N'Pay Voucher' and PaymentType=N'" & PaymentType & "' and TotalIn<>0 and SNo2=" & SNo

            ElseIf TransType = "Receipt Voucher" Then
                StrSel = "Select * From Transactions Where TransType=N'Receipt Voucher' and PaymentType=N'" & PaymentType & "' and TotalOut<>0 and SNo2=" & SNo
            End If

            Dim dap As New SqlDataAdapter(StrSel, cnn)
            Dim das As New DataSet

            dap.Fill(das, "Transactions")

            If TransType = "Pay Voucher" Then
                Dim rpt As New PayVoucher
                rpt.SetDataSource(das)
                rptViewer.CrystalReportViewer1.ReportSource = rpt
                rptViewer.CrystalReportViewer1.RefreshReport()
                rptViewer.ShowDialog()

            ElseIf TransType = "Receipt Voucher" Then
                Dim rpt As New ReceiptVoucher
                rpt.SetDataSource(das)
                rptViewer.CrystalReportViewer1.ReportSource = rpt
                rptViewer.CrystalReportViewer1.RefreshReport()
                rptViewer.ShowDialog()
            End If


        Catch ex As Exception
            If cnn1.State = ConnectionState.Open Then
                cnn1.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub PrintCheq(ByVal TransType As String, ByVal SNo As Integer)
        Try
            Dim dap As New SqlDataAdapter("Select CheqDate,DestSource DestName,Writting WrittenAmount,TotalOut Amount,TransDate " & _
                                          "From Transactions  Where Transtype=N'" & TransType & "' and SNo2=" & SNo & _
                                          " and ChqDate Is Not Null", cnn)
            Dim das As New DataSet

            dap.Fill(das, "Cheques")

            Dim rpt As New Cheque
            rpt.SetDataSource(das)
            rptViewer.CrystalReportViewer1.ReportSource = rpt
            rptViewer.CrystalReportViewer1.RefreshReport()
            rptViewer.ShowDialog()
        Catch ex As Exception
            If cnn1.State = ConnectionState.Open Then
                cnn1.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Function GetBalancePack(ByVal Pack As String) As Double
        Try
            Dim Balance As Double
            Dim cmdBalance As New SqlCommand("Select Sum(TotalIn)-Sum(TotalOut) From Transactions " & _
                                             "Where Package=N'" & Pack & "'", cnn1)

            cnn1.Open()
            Balance = CDbl(cmdBalance.ExecuteScalar.ToString)
            cnn1.Close()

            Return Balance
        Catch ex As Exception
            If cnn1.State = ConnectionState.Open Then
                cnn1.Close()
            End If
            Return 0
        End Try
    End Function

    Public Function GetBalanceAcc(ByVal Acc1 As String) As Double
        Try
            Dim cmd As New SqlCommand("Select Case When Sum(TotalIn)-Sum(TotalOut) Is Null Then 0 Else Sum(TotalIn)-Sum(TotalOut) End " & _
                                      "From Transactions Where Acc1=N'" & Acc1 & "'", cnn4)
            Dim Balance As Double

            cnn4.Open()
            Balance = CDbl(cmd.ExecuteScalar)
            cnn4.Close()

            Return Balance
        Catch ex As Exception
            If cnn4.State = ConnectionState.Open Then
                cnn4.Close()
            End If
        End Try
    End Function

    Public Function GetBalanceAcc(ByVal Acc1 As String, ByVal Acc2 As String) As Double
        Try
            Dim cmd As New SqlCommand("Select Case When Sum(TotalIn)-Sum(TotalOut) Is Null Then 0 Else Sum(TotalIn)-Sum(TotalOut) End " & _
                                      "From Transactions Where Acc1=N'" & Acc1 & "' And Acc2=N'" & Acc2 & "'", cnn4)
            Dim Balance As Double

            cnn4.Open()
            Balance = CDbl(cmd.ExecuteScalar)
            cnn4.Close()

            Return Balance
        Catch ex As Exception
            If cnn4.State = ConnectionState.Open Then
                cnn4.Close()
            End If
        End Try
    End Function

    Public Function GetBalanceAcc(ByVal Acc1 As String, ByVal Acc2 As String, ByVal Acc3 As String) As Double
        Try
            Dim cmd As New SqlCommand("Select Case When Sum(TotalIn)-Sum(TotalOut) Is Null Then 0 Else Sum(TotalIn)-Sum(TotalOut) End " & _
                                      "From Transactions Where Acc1=N'" & Acc1 & "' And Acc2=N'" & Acc2 & _
                                      "' And Acc3=N'" & Acc3 & "'", cnn4)
            Dim Balance As Double

            cnn4.Open()
            Balance = CDbl(cmd.ExecuteScalar)
            cnn4.Close()

            Return Balance
        Catch ex As Exception
            If cnn4.State = ConnectionState.Open Then
                cnn4.Close()
            End If
        End Try
    End Function

    Public Function GetBalanceAcc(ByVal Acc1 As String, ByVal Acc2 As String, ByVal Acc3 As String, ByVal Acc4 As String) As Double
        Try
            Dim cmd As New SqlCommand("Select Case When Sum(TotalIn)-Sum(TotalOut) Is Null Then 0 Else Sum(TotalIn)-Sum(TotalOut) End " & _
                                      "From Transactions Where Acc1=N'" & Acc1 & "' And Acc2=N'" & Acc2 & _
                                      "' And Acc3=N'" & Acc3 & "' And Acc4=N'" & Acc4 & "'", cnn4)
            Dim Balance As Double

            cnn4.Open()
            Balance = CDbl(cmd.ExecuteScalar)
            cnn4.Close()

            Return Balance
        Catch ex As Exception
            If cnn4.State = ConnectionState.Open Then
                cnn4.Close()
            End If
        End Try
    End Function

    Public Sub PrintVoucher(ByVal MoveNo As Integer, ByVal Year As Integer)
        Try
            Dim dap As New SqlDataAdapter("select * From Transactions Where MoveNo=" & MoveNo & " and Year(TransDate)=" & Year.ToString, cnn)
            Dim das As New DataSet
            das.Clear()

            cnn.Open()
            dap.Fill(das, "Transactions")
            cnn.Close()

            Dim rpt As New Vouchers
            rpt.SetDataSource(das)
            rptViewer.CrystalReportViewer1.ReportSource = rpt
            rptViewer.CrystalReportViewer1.RefreshReport()
            rptViewer.ShowDialog()
        Catch ex As Exception
            If cnn1.State = ConnectionState.Open Then
                cnn1.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub

End Module

