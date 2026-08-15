Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmStorstatus
    Sub FillStoreNameList()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select Distinct StoreName From Stock  Where StoreName Is Not Null Order By StoreName", cnn)
            Dim Reader As SqlDataReader

            Me.CombStore.Items.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.CombStore.Items.Add(Reader.Item(0))
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
    Dim Store As String

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Me.ErrProv.Clear()

        If Me.RStore.Checked = True And Me.CombStore.SelectedIndex = -1 Then
            Me.ErrProv.SetError(Me.CombStore, "Please select a valid Store from the list")
        Else
            Try
                Me.Cursor = Cursors.WaitCursor

                If Me.RStore.Checked = True Then
                    Store = " StoreName=N'" & Me.CombStore.Text & "' "
                Else
                    Store = ""
                End If

                Dim dap As New SqlDataAdapter("Select * from Stock Where Done=1 " & Store & "and" & _
                                                  "TransDate > N'" & Me.DTPPeriodFRm.Value.ToShortDateString & " 00:00:01' and " & _
                                                  "TransDate < N'" & Me.DTPPeriodTo.Value.ToShortDateString & " 23:59:59' ", cnn)
                Dim das As New DataSet
                cnn.Open()
                dap.Fill(das, "Stock")
                cnn.Close()

                Dim rpt As New Storstatus
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
                crParameterDiscreteValue.Value = Me.DTPPeriodFRm.Value
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldLocation.ApplyCurrentValues(crParameterValues)


                'SECOND PARAMETER
                crParameterFieldLocation = crParameterFieldDefinitions.Item("MaxDate")
                crParameterValues = crParameterFieldLocation.CurrentValues
                crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                crParameterDiscreteValue.Value = Me.DTPPeriodFRm.Value
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldLocation.ApplyCurrentValues(crParameterValues)


                'Store PARAMETER
                crParameterFieldLocation = crParameterFieldDefinitions.Item("Store")
                crParameterValues = crParameterFieldLocation.CurrentValues
                crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
                If Me.RStore.Checked = True Then
                    crParameterDiscreteValue.Value = Me.CombStore.Text
                Else
                    crParameterDiscreteValue.Value = "ALL"
                End If
                ''''''''''''''''''''''''
                crParameterValues.Add(crParameterDiscreteValue)
                crParameterFieldLocation.ApplyCurrentValues(crParameterValues)

                Me.CrystalReportViewer1.ReportSource = rpt
                'ReportViewer.CrystalReportViewer1.ReportSource = rpt
                'ReportViewer.CrystalReportViewer1.Refresh()
                'ReportViewer.ShowDialog()

            Catch ex As Exception
                If cnn1.State = ConnectionState.Open Then
                    cnn1.Close()
                End If
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub frmStorstatus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillStoreNameList()
    End Sub

    Private Sub RAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RAll.CheckedChanged
        Me.CombStore.Enabled = False
    End Sub

    Private Sub RStore_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RStore.CheckedChanged
        Me.CombStore.Enabled = True
    End Sub
End Class