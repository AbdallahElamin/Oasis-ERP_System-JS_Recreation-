Imports System.Data.SqlClient
Imports BarcodeLib.Barcode
Public Class frmReplaceItem
    Sub FillComponyList()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select Distinct CompanyName From Stock Where CompanyName Is Not Null Order By CompanyName", cnn)
            Dim Reader As SqlDataReader

            Me.CombCompany.Items.Clear()
            Me.CombCompany.AutoCompleteCustomSource.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.CombCompany.Items.Add(Reader.Item(0))
                Me.CombCompany.AutoCompleteCustomSource.Add(Reader.Item(0))
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
    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub frmReplaceItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillComponyList()
    End Sub
End Class