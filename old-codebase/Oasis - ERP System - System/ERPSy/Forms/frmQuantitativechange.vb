Imports System.Data.SqlClient

Public Class frmQuantitativechange
    Public SNo As Integer
    Sub FillInviceList()
        Try
            Me.Cursor = Cursors.WaitCursor


            Dim cmd As New SqlCommand("Select IsNull(Price,N'')Price,IsNull(Qnt,N'')Qnt From Invoices Where SNo=" & SNo, cnn)
            Dim Reader As SqlDataReader


            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                txtQnt.Value = Reader.Item("Qnt")
                Me.txtWPrice.Text = Reader.Item("Price")
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

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChange.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim cmd As New SqlCommand()
            Dim Trans As SqlTransaction
            cnn.Open()
            cmd.Connection = cnn
            Trans = cnn.BeginTransaction
            cmd.Transaction = Trans
            cmd.CommandText = "Update Invoices Set Qnt=@Qnt Where SNo = " & SNo
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@Qnt", Me.txtQnt.Value)
            cmd.ExecuteNonQuery()

            Trans.Commit()
            cnn.Close()

            Me.Cursor = Cursors.Default

            MsgBox("Updated Successfully")

            Me.Close()

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
        End Try
    End Sub

    Private Sub frmUpdateReturnInvoice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillInviceList()
    End Sub
End Class