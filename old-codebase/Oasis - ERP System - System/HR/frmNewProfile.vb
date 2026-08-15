Imports System.Data.SqlClient
Imports System.IO

Public Class frmHR
    Dim PicName As String

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Me.OpenFileDialog1.Filter = "Image Files (*.BMP;*.JPG;*.GIF;*.TIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.TIF;*.PNG|All files (*.*)|*.*"
            If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.PictureBox1.ImageLocation = Me.OpenFileDialog1.FileName
                Dim F As New FileInfo(Me.OpenFileDialog1.FileName)

                PicName = F.Name
            End If
        Catch ex As Exception
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
                Dim EmpNo As String
                EmpNo = Me.txtId.Text & "-" & Me.Combyear.SelectedItem
                Me.Cursor = Cursors.WaitCursor
                Dim cmd As New SqlCommand("Insert Into StaffProfiles (SNo,year,EmpID,Title,Name,Birth,HireDate,EAddress,Address,Mobile," & _
                                          "IDDetailes,Resume,ResumeName,StaffImage,ImageName,Entitlement,Job,JobDes,JobDesc) " & _
                                          "Values (" & Me.txtId.Text & "," & Me.Combyear.SelectedItem & ",N'" & EmpNo & "',N'" & Me.CombTitle.Text.Trim & "',N'" & _
                                          Me.TxtName.Text.Trim & "',N'" & Me.DTBirth.Value.ToString("MM / dd / yyyy") & "',N'" & _
                                          Me.DTHireDate.Value.ToString("MM / dd / yyyy") & "',N'" & Me.txtEmail.Text.Trim & "',N'" & _
                                          Me.txtAddress.Text.Trim & "',N'" & Me.txtMobile.Text & "',N'" & Me.txtIDDetailes.Text & _
                                          "',@Resume,@ResumeName,@StaffImage,N'" & PicName & "'," & Me.txtEntitlement.Text & ",@Job,@JobDes,N'" & _
                                          Me.ComboJobDescribtion.SelectedItem & "')", cnn)
                'fill Resume
                If Me.txtResume.Text.Trim.Length > 0 Then
                    Dim f As New FileInfo(Me.txtResume.Text)
                    Dim fs As New FileStream(Me.txtResume.Text, FileMode.Open)
                    Dim FileByte As Byte() = New Byte(fs.Length) {}
                    fs.Read(FileByte, 0, fs.Length)
                    fs.Close()

                    cmd.Parameters.AddWithValue("@ResumeName", f.Name)
                    cmd.Parameters.AddWithValue("@Resume", FileByte)
                Else
                    cmd.Parameters.AddWithValue("@Resume", SqlTypes.SqlBinary.Null)
                    cmd.Parameters.AddWithValue("@ResumeName", "")
                End If
                
                'fill job Description
                If Me.TxtJobDesc.Text.Trim.Length > 0 Then
    
                    Dim f2 As New FileInfo(Me.TxtJobDesc.Text)
                    Dim fs2 As New FileStream(Me.TxtJobDesc.Text, FileMode.Open)
                    Dim FileByte2 As Byte() = New Byte(fs2.Length) {}
                    fs2.Read(FileByte2, 0, fs2.Length)
                    fs2.Close()

                    cmd.Parameters.AddWithValue("@JobDes", FileByte2)
                    cmd.Parameters.AddWithValue("@Job", f2.Name)
                Else
                    cmd.Parameters.AddWithValue("@JobDes", SqlTypes.SqlBinary.Null)
                    cmd.Parameters.AddWithValue("@Job", "")
                End If


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

                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()

                MsgBox("Saved successfully")
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

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Me.OpenFileDialog2.Filter = "All Files (*.*)|*.*"
            If Me.OpenFileDialog2.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtResume.Text = Me.OpenFileDialog2.FileName
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Sub Clear()
        Me.TxtName.Clear()
        Me.txtMobile.Clear()
        Me.txtAddress.Clear()
        Me.txtIDDetailes.Clear()
        Me.txtEmail.Clear()
        Me.CombTitle.Text = ""
        Me.txtResume.Clear()
        Me.PictureBox1.Image = Nothing
        Me.CombTitle.Focus()
        Me.TxtJobDesc.Clear()
        Me.ComboJobDescribtion.SelectedIndex = -1
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click

        Clear()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            Me.OpenFileDialog2.Filter = "All Files (*.*)|*.*"
            If Me.OpenFileDialog2.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.TxtJobDesc.Text = Me.OpenFileDialog2.FileName
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub frmHR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtId.Focus()
        'GetEmpNo()
        FillJobDescribtion()
        fillyear()
    End Sub

    Sub GetEmpNo()

        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select IsNull(Max(SNo),0) From StaffProfiles where year=" & Me.Combyear.SelectedItem, cnn)
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
        GetEmpNo()
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
End Class
