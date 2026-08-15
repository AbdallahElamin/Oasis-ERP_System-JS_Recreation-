Imports ZXing.Common
Imports ZXing
Imports ZXing.QrCode

Public Class Form1
    Dim aymen As New QrCodeEncodingOptions

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        aymen.DisableECI = True
        aymen.CharacterSet = "UTF-8"
        aymen.Width = 150
        aymen.Height = 150
        Dim writer = New BarcodeWriter()
        writer.Format = BarcodeFormat.QR_CODE
        writer.Options = aymen

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles btnQR.Click
        If String.IsNullOrEmpty(TextBox1.Text.Trim) = False Then
            Dim x = New BarcodeWriter()
            x.Options = aymen
            x.Format = BarcodeFormat.QR_CODE
            Dim result = New Bitmap(x.Write(Me.TextBox1.Text.Trim))
            Me.PictureBox1.Image = result
            Me.TextBox1.Clear()
        Else
            Me.PictureBox1.Image = Nothing
            Me.TextBox1.Clear()
            MessageBox.Show("معذرة لا يوجد نص", "رسالة تنبيه!", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Me.TextBox1.Focus()
        End If
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim bitmab As Bitmap = New Bitmap(Me.PictureBox1.Image)
        Dim reder As New BarcodeReader
        reder.AutoRotate = True
        ' reder.TryInverted = True
        Dim result As Result = reder.Decode(bitmab)
        Dim decoded As String = result.ToString().Trim()
        Me.TextBox1.Text = decoded

    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        If PictureBox1.Image IsNot Nothing Then
            Dim sfd As New SaveFileDialog
            sfd.CreatePrompt = True
            sfd.OverwritePrompt = True
            sfd.FileName = Me.TextBox1.Text.Trim
            '  sfd.Filter
        End If
    End Sub
End Class