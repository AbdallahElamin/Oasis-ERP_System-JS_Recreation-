Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmSalesReports


    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim StrSel As String

            If RItem.Checked = True Then
                ' StrSel = "Select N'" & Me.DTPBFrom.Value.ToShortDateString & " 10:10:10' Employee," & _
                '    "N'" & Me.DTPBTo.Value.ToShortDateString & " 10:10:10' " & _
                StrSel = "Select  Item,Sum(Price*Qnt) Price From Invoices  Where " & _
                         "TransDate > N'" & Me.DTPBFrom.Value.ToShortDateString & " 00:00:01' and " & _
                         "TransDate < N'" & Me.DTPBTo.Value.ToShortDateString & " 23:59:59' " & _
                         "Group By Item " & _
                         "Order By Price Desc"

            ElseIf Me.RMonth.Checked = True Then
                StrSel = "Select N'" & Me.DTPBFrom.Value.ToShortDateString & " 10:10:10' Employee," & _
                         "Year(TransDate) SNo,Month(TransDate) InvNo,Sum(Price*Qnt) Price From Invoices  " & _
                         "Where Year(TransDate)=" & Me.DTPBFrom.Value.Year & _
                         " Group By Year(TransDate),Month(TransDate) Order By Month(TransDate)"

                ''N'" & Me.DTPBTo.Value.ToShortDateString & " 10:10:10' " befor Year(TransDate) SNo
            End If

            Dim dap As New SqlDataAdapter(StrSel, cnn)
            Dim das As New DataSet

            cnn.Open()
            dap.Fill(das, "Invoices")
            cnn.Close()

            If RItem.Checked = True Then
                Dim rpt As New SalesByItem
                rpt.SetDataSource(das)


                Dim crParameterDiscreteValue As New CrystalDecisions.Shared.ParameterDiscreteValue
                Dim crParameterFieldDefinitions As ParameterFieldDefinitions
                Dim crParameterFieldLocation As ParameterFieldDefinition
                Dim crParameterValues As New CrystalDecisions.Shared.ParameterValues
                crParameterFieldDefinitions = rpt.DataDefinition.ParameterFields

                'FIRST PARAMETER
                crParameterFieldLocation = crParameterFieldDefinitions.Item("MinDate")
                crParameterValues = crParameterFieldLocation.CurrentValues
                crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                crParameterDiscreteValue.Value = Me.DTPBFrom.Value
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldLocation.ApplyCurrentValues(crParameterValues)


                'SECOND PARAMETER
                crParameterFieldLocation = crParameterFieldDefinitions.Item("MaxDate")
                crParameterValues = crParameterFieldLocation.CurrentValues
                crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                crParameterDiscreteValue.Value = Me.DTPBTo.Value
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldLocation.ApplyCurrentValues(crParameterValues)
                ''''''''''''''''''''''''

                ReportViewer.CrystalReportViewer1.ReportSource = rpt
                ReportViewer.CrystalReportViewer1.Refresh()
                ReportViewer.ShowDialog()
            ElseIf Me.RMonth.Checked = True Then
                Dim rpt As New SalesByMonth
                rpt.SetDataSource(das)


                rptViewer.CrystalReportViewer1.ReportSource = rpt
                rptViewer.CrystalReportViewer1.RefreshReport()
                rptViewer.ShowDialog()
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

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmSalesReports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class