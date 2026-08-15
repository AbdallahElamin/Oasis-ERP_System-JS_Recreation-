Imports System.Data.SqlClient

Module Utilities
    Public Function ValidateDate(ByVal D As Date) As Boolean
        Try
            Dim LockDate As Date
            Dim cmd As New SqlCommand("Select LockDate From LockDate", cnn)

            cnn.Open()
            LockDate = CDate(cmd.ExecuteScalar)
            cnn.Close()

            If D > LockDate Then
                Return True

            ElseIf D <= LockDate Then
                Return False
            End If
        Catch ex As Exception
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Function

    Public Function GetClientName(ByVal SNo As Integer) As String
        Try
            Dim cmd As New SqlCommand("Select Name From Clients Where SNo=" & SNo, cnn)
            Dim Name As String

            cnn.Open()
            Name = cmd.ExecuteScalar
            cnn.Close()

            Return Name
        Catch ex As Exception
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Function

    Public Function GetSalesManList() As ArrayList
        Try
            Dim cmd As New SqlCommand("Select SNo,Name From AgentDistributors", cnn)
            Dim Reader As SqlDataReader

            Dim List As New ArrayList

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                List.Add(Reader.Item("Name"))
            End While
            cnn.Close()

            Return List
        Catch ex As Exception
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Function

    Public Function GetMedicalRepresentativesList() As ArrayList
        Try
            Dim cmd As New SqlCommand("Select SNo,Name From AgentRepresentatives", cnn)
            Dim Reader As SqlDataReader

            Dim List As New ArrayList

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                List.Add(Reader.Item("Name"))
            End While
            cnn.Close()

            Return List
        Catch ex As Exception
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Function

    Public Function GetStatesList() As ArrayList
        Try
            Dim cmd As New SqlCommand("Select Distinct State From Regions Where State Is Not Null Order By State", cnn)
            Dim Reader As SqlDataReader

            Dim List As New ArrayList

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                List.Add(Reader.Item("State"))
            End While
            cnn.Close()

            Return List
        Catch ex As Exception
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Function

    Public Function GetClientAreaList() As ArrayList
        Try
            Dim cmd As New SqlCommand("Select Distinct Area From Regions Where Area Is Not Null Order By Area", cnn)
            Dim Reader As SqlDataReader

            Dim List As New ArrayList

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                List.Add(Reader.Item("Area"))
            End While
            cnn.Close()

            Return List
        Catch ex As Exception
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Function

    Public Function GetClientClassList() As ArrayList
        Try
            Dim cmd As New SqlCommand("Select Distinct Name From ClientClasses Where Name Is Not Null Order By Name", cnn)
            Dim Reader As SqlDataReader

            Dim List As New ArrayList

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                List.Add(Reader.Item("Name"))
            End While
            cnn.Close()

            Return List
        Catch ex As Exception
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Function

    Public Sub PrintPayVoucher(ByVal SNo As Integer, ByVal Year As Integer)
        Try
            Dim dap As New SqlDataAdapter("Select * From Transactions Where " & _
                                          "TotalIn=0 and Transtype=N'Pay' and PaperNo=" & SNo & _
                                          " and Year(TransDate)=" & Year, cnn)
            Dim das As New DataSet

            dap.Fill(das, "Transactions")

            Dim rpt As New PayVoucher
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

    Public Sub PrintReceiptVoucher(ByVal SNo As Integer, ByVal Year As Integer)
        Try
            Dim dap As New SqlDataAdapter("Select * From Transactions Where " & _
                                          "TotalIn=0 and Transtype=N'Receipt' and PaperNo=" & SNo & _
                                          " and Year(TransDate)=" & Year, cnn)
            Dim das As New DataSet

            dap.Fill(das, "Transactions")

            Dim rpt As New ReceiptVoucher
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

    Public Sub PrintInvoice(ByVal SNo As Integer, ByVal Year As Integer)
        Try
            Dim dap As New SqlDataAdapter("select * from VwInvoiceCust Where InvNo=" & SNo & _
                                          " and Year(TransDate)='" & Year & "'", cnn)

            Dim das As New DsInvoices
            Dim dt As New DataTable
            dap.Fill(dt)
            ' dap.Fill(das, "Result")
            Dim rpt As New Invoice
            'rpt.SetDataSource(das.Tables("Result"))
            rpt.SetDataSource(dt)
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

    Public Sub PrintQuotation(ByVal SNo As Integer, ByVal Year As Integer)
        Try
            Dim dap As New SqlDataAdapter("Select * From ViwQuotation Where InvNo=" & SNo & _
                                          " and Year(TransDate)='" & Year & "'", cnn)
            Dim das As New Quotation
            Dim dt As New DataTable
            dap.Fill(dt)
            'Dim das As New DsQuotation

            'dap.Fill(das, "reselt")

            Dim rpt As New Quotation
            rpt.SetDataSource(dt)
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

    Public Function GetItemQnt(ByVal StoreName As String, ByVal Item As String, ByVal BatchNo As String) As Double
        Try
            Dim cmd As New SqlCommand("Select IsNull(Sum(QntIn)-Sum(QntOut),0) From Stock " & _
                                      "Where StoreName=@StoreName and Item=@Item and BatchNo=@BatchNo", cnn)
            Dim Qnt As Double

            cnn.Open()
            cmd.Parameters.AddWithValue("@StoreName", StoreName)
            cmd.Parameters.AddWithValue("@Item", Item)
            cmd.Parameters.AddWithValue("@BatchNo", BatchNo)
            Qnt = CDbl(cmd.ExecuteScalar)
            cnn.Close()

            Return Qnt
        Catch ex As Exception
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Function

End Module