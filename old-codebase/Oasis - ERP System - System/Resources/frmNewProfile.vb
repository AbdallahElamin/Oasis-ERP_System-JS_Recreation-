Imports System.Data.SqlClient
Imports System.IO
Imports ZXing.Common
Imports ZXing
Imports ZXing.QrCode
Imports System.Drawing.Imaging


Public Class frmHR
    Dim PicName As String
    Dim aymen As New QrCodeEncodingOptions
    Dim options As New QrCodeEncodingOptions

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Me.OpenFileDialog1.Filter = "Image Files (*.BMP;*.JPG;*.GIF;*.TIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.TIF;*.PNG|All files (*.*)|*.*"
            If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.PictureBox1.ImageLocation = Me.OpenFileDialog1.FileName
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        'Try
        '    Me.OpenFileDialog1.Filter = "Image Files (*.BMP;*.JPG;*.GIF;*.TIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.TIF;*.PNG|All files (*.*)|*.*"
        '    If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
        '        Me.PictureBox1.ImageLocation = Me.OpenFileDialog1.FileName
        '        Dim F As New FileInfo(Me.OpenFileDialog1.FileName)

        '        PicName = F.Name
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.ToString)
        'End Try
        'Dim x = New BarcodeWriter()
        'x.Options = aymen
        'x.Format = BarcodeFormat.QR_CODE
        'Dim result = New Bitmap(x.Write(Me.txtId.Text.Trim))
        'Me.PictureBox2.Image = result
        ' Me.TextBox1.Clear()
    End Sub
    Public Sub PrintInvoice(ByVal SNo As String, ByVal title As String)
        Try
            'Dim x As String = "select * from StaffProfiles Where SNo=" & SNo & " and Title like'%" & title & "%'"
            ' MsgBox(x)
            Dim dap As New SqlDataAdapter("select * from StaffProfiles Where EmpID=" & SNo & " and Title like'%" & title & "%'", cnn)
            'Dim dap As New SqlDataAdapter("select * from StaffProfiles Where SNo=123 or Title='وكيل نيابة'", cnn)

            Dim das As New DaSetُEmp
            Dim dt As New DataTable
            dap.Fill(dt)
            ' dap.Fill(das, "Result")
            'Dim rpt As New card1
            If Me.CombTitle.Text.Trim = "وكيل نيابة" Then
                Dim rpt As New Car00
                rpt.SetDataSource(dt)
                rptViewer.CrystalReportViewer1.ReportSource = rpt
                rptViewer.CrystalReportViewer1.RefreshReport()
                rptViewer.ShowDialog()
            ElseIf Me.CombTitle.Text.Trim = "موظف" Then
                Dim rpt As New car01
                rpt.SetDataSource(dt)
                rptViewer.CrystalReportViewer1.ReportSource = rpt
                rptViewer.CrystalReportViewer1.RefreshReport()
                rptViewer.ShowDialog()
            Else
                Dim rpt As New car11
                rpt.SetDataSource(dt)
                rptViewer.CrystalReportViewer1.ReportSource = rpt
                rptViewer.CrystalReportViewer1.RefreshReport()
                rptViewer.ShowDialog()
            End If
            'rpt.SetDataSource(das.Tables("Result"))

        Catch ex As Exception
            If cnn1.State = ConnectionState.Open Then
                cnn1.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Me.CombTitle.Text.Trim.Length = 0 OrElse Me.TxtName.Text.Trim.Length = 0 OrElse Me.txtMobile.Text.Trim.Length = 0 _
               OrElse Me.txtAddress.Text.Trim.Length = 0 OrElse Me.txtIDDetailes.Text.Trim.Length = 0 _
               OrElse Me.txtEmail.Text.Trim.Length = 0 OrElse Me.txtEntitlement.Text.Trim.Length = 0 Then
                MsgBox("Please complete all personal details")
                'ElseIf Me.PictureBox1.Image Is Nothing Then
                '    MsgBox("Please add an image")
                'ElseIf Me.Combyear.Text.Trim.Length = 0 OrElse Me.txtResume.Text.Trim.Length = 0 Then
                '    MsgBox("Please complete all job details")
            Else
                Dim InvNo, MoveNo, Year As Integer
                Dim EmpNo As String
                'EmpNo = Me.txtId.Text & "-" & Me.Combyear.SelectedItem
                EmpNo = Me.txtId.Text.Trim
                Me.Cursor = Cursors.WaitCursor
                'Dim cmd As New SqlCommand("Insert Into StaffProfiles (SNo,year,EmpID,Title,Name,Birth,HireDate,EAddress,Address,Mobile," & _
                '                          "IDDetailes,Resume,ResumeName,StaffImage,ImageName,QR,Bold,Entitlement,Job,JobDes,JobDesc) " & _
                '                          "Values (" & Me.txtId.Text & "," & Me.Combyear.SelectedItem & ",N'" & EmpNo & "',N'" & Me.CombTitle.Text.Trim & "',N'" & _
                '                          Me.TxtName.Text.Trim & "',N'" & Me.DTBirth.Value.ToString("MM / dd / yyyy") & "',N'" & _
                '                          Me.DTHireDate.Value.ToString("MM / dd / yyyy") & "',N'" & Me.txtEmail.Text.Trim & "',N'" & _
                '                          Me.txtAddress.Text.Trim & "',N'" & Me.txtMobile.Text & "',N'" & Me.txtIDDetailes.Text & _
                '                          "',@Resume,@ResumeName,@StaffImage,N'" & PicName & "',@QR,Bold," & Me.txtEntitlement.Text & ",@Job,@JobDes,N'" & _
                '                          Me.ComboJobDescribtion.SelectedItem & "')", cnn)
                Dim cmd As New SqlCommand("Insert Into StaffProfiles (year,EmpID,Title,Name,Job,Birth,HireDate,EAddress,Address,Mobile," & _
                                          "IDDetailes,StaffImage,ImageName,QR,Bold,Entitlement) " & _
                                          "Values (" & Me.Combyear.SelectedItem & ",N'" & EmpNo & "',N'" & Me.CombTitle.Text.Trim & "',N'" & _
                                          Me.TxtName.Text.Trim & "',N'" & Me.ComboJobDescribtion.Text.Trim & "',N'" & Me.DTBirth.Value.ToString("MM / dd / yyyy") & "',N'" & _
                                          Me.DTHireDate.Value.ToString("MM / dd / yyyy") & "',N'" & Me.txtEmail.Text.Trim & "',N'" & _
                                          Me.txtAddress.Text.Trim & "',N'" & Me.txtMobile.Text & "',N'" & Me.txtIDDetailes.Text & _
                                          "',@StaffImage,@ImageName,@QR,@Bold," & Me.txtEntitlement.Text & ")", cnn)

                'fill Resume
                'If Me.txtResume.Text.Trim.Length > 0 Then
                '    Dim f As New FileInfo(Me.txtResume.Text)
                '    Dim fs As New FileStream(Me.txtResume.Text, FileMode.Open)
                '    Dim FileByte As Byte() = New Byte(fs.Length) {}
                '    fs.Read(FileByte, 0, fs.Length)
                '    fs.Close()

                '    cmd.Parameters.AddWithValue("@ResumeName", f.Name)
                '    cmd.Parameters.AddWithValue("@Resume", FileByte)
                'Else
                '    cmd.Parameters.AddWithValue("@Resume", SqlTypes.SqlBinary.Null)
                '    cmd.Parameters.AddWithValue("@ResumeName", "")
                'End If

                'fill job Description
                'If Me.TxtJobDesc.Text.Trim.Length > 0 Then

                '    Dim f2 As New FileInfo(Me.TxtJobDesc.Text)
                '    Dim fs2 As New FileStream(Me.TxtJobDesc.Text, FileMode.Open)
                '    Dim FileByte2 As Byte() = New Byte(fs2.Length) {}
                '    fs2.Read(FileByte2, 0, fs2.Length)
                '    fs2.Close()

                '    cmd.Parameters.AddWithValue("@JobDes", FileByte2)
                '    cmd.Parameters.AddWithValue("@Job", f2.Name)
                'Else
                '    cmd.Parameters.AddWithValue("@JobDes", SqlTypes.SqlBinary.Null)
                '    cmd.Parameters.AddWithValue("@Job", "")
                'End If


                'fill PictureBox

                If Me.PictureBox1.Image Is Nothing Then
                    cmd.Parameters.AddWithValue("@StaffImage", SqlTypes.SqlBinary.Null)
                    cmd.Parameters.AddWithValue("@ImageName", DBNull.Value)

                Else
                    Dim f2 As New FileInfo(Me.OpenFileDialog1.FileName)
                    Dim str As String = Me.OpenFileDialog1.FileName
                    Dim fs1 As New FileStream(str, FileMode.Open)
                    Dim FileByte1 As Byte() = New Byte(fs1.Length) {}

                    fs1.Read(FileByte1, 0, fs1.Length)
                    fs1.Close()

                    cmd.Parameters.AddWithValue("@StaffImage", FileByte1)
                    cmd.Parameters.AddWithValue("@ImageName", f2.Name)
                End If

                'If Me.PictureBox1.Image IsNot Nothing Then
                '    cmd.Parameters.AddWithValue("@QR", SqlTypes.SqlBinary.Null)
                'Else
                '    Dim stre As String = Me.OpenFileDialog1.FileName
                '    Dim s As New FileStream(stre, FileMode.Open)
                '    Dim q As Byte() = New Byte(s.Length) {}
                '    s.Read(q, 0, s.Length)
                '    s.Close()

                'If String.IsNullOrEmpty(txtId.Text.Trim) = False Then
                '    Dim qr = New BarcodeWriter()
                '    qr.Options = options
                '    qr.Format = BarcodeFormat.QR_CODE
                '    Dim result = New Bitmap(qr.Write(Me.txtId.Text.Trim()))
                '    Me.PictureBox2.Image = result
                Dim ms As New MemoryStream
                Dim img As Image = PictureBox2.Image
                Dim bmpImage As New Bitmap(img)
                bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg)
                Dim data As Byte() = ms.GetBuffer
                Dim p As New SqlParameter("@QR", SqlDbType.Image)
                p.Value = data
                cmd.Parameters.Add(p)


                '' Dim data As Byte() = ms.GetBuffer()
                'If Me.PictureBox1.Image Is Nothing Then
                '    cmd.Parameters.AddWithValue("@QR", SqlTypes.SqlBinary.Null)
                '    cmd.Parameters.AddWithValue("@QRName", DBNull.Value)

                'Else
                '    Dim f2 As New FileInfo(Me.OpenFileDialog1.FileName)
                '    Dim str As String = Me.OpenFileDialog1.FileName
                '    Dim fs1 As New FileStream(str, FileMode.Open)
                '    Dim FileByte1 As Byte() = New Byte(fs1.Length) {}

                '    fs1.Read(FileByte1, 0, fs1.Length)
                '    fs1.Close()

                '    cmd.Parameters.AddWithValue("@QR", FileByte1)
                '    cmd.Parameters.AddWithValue("@QRName", f2.Name)
                'End If
                'cmd.Parameters.AddWithValue("@QR", ms.ToArray())

                ' Else




                '  End If
                'cmd.Parameters.AddWithValue("@Bold", "O")
                cmd.Parameters.AddWithValue("@Bold", Me.ComboBox1.Text.Trim)

                'Dim sfd As New SaveFileDialog
                'sfd.CreatePrompt = True
                'sfd.OverwritePrompt = True
                ''sfd.FileName = Me.TextBox2.Text.Trim
                'sfd.Filter = "PNG|*.png|JPEG|*.jpg|BMP|*.bmp|GIF|*.gif"
                'cmd.Parameters.AddWithValue("@QR1", (sfd))
                'If sfd.ShowDialog() = DialogResult.OK Then
                '    Me.PictureBox1.Image.Save(sfd.FileName)
                '    sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                'End If
                ' cmd.Parameters.AddWithValue("@QR", sfd)

                ' MessageBox.Show("معذرة لا توجد صوره", "رسالة تنبيه!", MessageBoxButtons.OK, MessageBoxIcon.[Error])


                ' cmd.Parameters.AddWithValue("@QRN", imagbytearray1)

                'Dim ms As New IO.MemoryStream()

                'If Me.PictureBox2.Image Is Nothing Then
                '    cmd.Parameters.AddWithValue("@QR", SqlTypes.SqlBinary.Null)
                '    cmd.Parameters.AddWithValue("@QRN", DBNull.Value)

                'Else
                '    Dim f2 As New FileInfo(Me.OpenFileDialog1.FileName)
                '    Dim str As String = Me.OpenFileDialog1.FileName
                '    Dim fs1 As New FileStream(str, FileMode.Open)
                '    Dim FileByte1 As Byte() = New Byte(fs1.Length) {}

                '    fs1.Read(FileByte1, 0, fs1.Length)
                '    fs1.Close()

                '    cmd.Parameters.AddWithValue("@QR", Me.PictureBox2.Image)
                '    cmd.Parameters.AddWithValue("@QRN", Me.txtId.Text)
                'End If
                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()

                MsgBox("Saved successfully")
                PrintInvoice(Me.txtId.Text, Me.CombTitle.Text)
                Clear()

                Me.Cursor = Cursors.Default
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Try
        '    Me.OpenFileDialog2.Filter = "All Files (*.*)|*.*"
        '    If Me.OpenFileDialog2.ShowDialog = Windows.Forms.DialogResult.OK Then
        '        Me.txtResume.Text = Me.OpenFileDialog2.FileName
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.ToString)
        'End Try
    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Sub Clear()
        Me.txtId.Clear()
        Me.TxtName.Clear()
        Me.txtMobile.Clear()
        Me.txtAddress.Clear()
        Me.txtIDDetailes.Clear()
        Me.txtEmail.Clear()
        Me.CombTitle.Text = ""
        'Me.txtResume.Clear()
        Me.PictureBox1.Image = Nothing
        Me.PictureBox2.Image = Nothing
        Me.CombTitle.Focus()
        ' Me.TxtJobDesc.Clear()
        Me.ComboJobDescribtion.SelectedIndex = -1
        Me.Combyear.SelectedIndex = -1
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click

        Clear()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Try
        '    Me.OpenFileDialog2.Filter = "All Files (*.*)|*.*"
        '    If Me.OpenFileDialog2.ShowDialog = Windows.Forms.DialogResult.OK Then
        '        Me.TxtJobDesc.Text = Me.OpenFileDialog2.FileName
        '    End If
        'Catch ex As Exception
        '    MsgBox(ex.ToString)
        'End Try
    End Sub

    Private Sub frmHR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtId.Focus()
        ' GetEmpNo()
        FillJobDescribtion()
        fillyear()
        aymen.DisableECI = True
        aymen.CharacterSet = "UTF-8"
        aymen.Width = 150
        aymen.Height = 150
        Dim writer = New BarcodeWriter()
        writer.Format = BarcodeFormat.QR_CODE
        writer.Options = aymen
    End Sub

    Sub GetEmpNo()

        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select IsNull(Max(SNo),0) From StaffProfiles where year= N'" & Me.Combyear.SelectedItem, cnn)
            Dim Reader As SqlDataReader
            Me.txtId.Clear()
            Dim Id As Integer
            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Id = Reader.Item(0)
                Id += 1

                Me.txtId.Text = Id

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

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim a As New frmJobDescEn
        a.ShowDialog()
        FillJobDescribtion()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim a As New frmAddyear
        a.Show()
    End Sub

    Private Sub Combyear_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Combyear.SelectedIndexChanged
        ' GetEmpNo()
    End Sub
    Sub fillyear()
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.Combyear.Items.Clear()
            Dim cmd As New SqlCommand("select Distinct year From Hireyear where year is not null ", cnn)
            Dim rdr As SqlDataReader

            cnn.Open()
            rdr = cmd.ExecuteReader
            While rdr.Read
                Me.Combyear.Items.Add(rdr.Item(0))
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

    Private Sub Label6_Click(sender As System.Object, e As System.EventArgs) Handles Label6.Click

    End Sub

    Private Sub GroupBox3_Enter(sender As System.Object, e As System.EventArgs) Handles GroupBox3.Enter

    End Sub

    Private Sub GroupBox2_Enter(sender As System.Object, e As System.EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        If String.IsNullOrEmpty(txtId.Text.Trim) = False Then
            Dim x = New BarcodeWriter()
            x.Options = aymen
            x.Format = BarcodeFormat.QR_CODE

            Dim result = New Bitmap(x.Write(Me.txtId.Text.Trim))
            Me.PictureBox2.Image = result
            ' Me.PictureBox2.Image.Filter = "Image Files (*.BMP;*.JPG;*.GIF;*.TIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.TIF;*.PNG|All files (*.*)|*.*"

            
                ' Me.txtId.Clear()
            Else
                Me.PictureBox2.Image = Nothing

                MessageBox.Show("معذرة لا يوجد نص", "رسالة تنبيه!", MessageBoxButtons.OK, MessageBoxIcon.[Error])
                Me.txtId.Focus()
            End If
    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        PrintInvoice(123, Me.CombTitle.Text.Trim)
    End Sub
End Class
