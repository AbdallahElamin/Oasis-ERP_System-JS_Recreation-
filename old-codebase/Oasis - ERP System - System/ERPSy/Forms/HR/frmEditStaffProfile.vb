Imports System.Data.SqlClient
Imports System.IO

Public Class frmEditStaffProfile

    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtId.KeyUp
        If e.KeyCode = Keys.Enter Then
            FillData()
        End If
    End Sub

    Sub FillData()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim cmd As New SqlCommand("Select Title,Name,Birth,HireDate,EAddress,Address,Mobile,IDDetailes,Resume,ResumeName" & _
                                      ",StaffImage,Entitlement,JobDesc,Job From StaffProfiles Where EmpID=N'" & Me.txtId.Text.Trim & "'", cnn)
            Dim Reader As SqlDataReader

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.CombTitle.Text = Reader.Item("Title")
                Me.TxtName.Text = Reader.Item("Name")
                Me.DTBirth.Value = Reader.Item("Birth")
                Me.DTHireDate.Value = Reader.Item("HireDate")
                Me.txtEmail.Text = Reader.Item("EAddress")
                Me.txtAddress.Text = Reader.Item("Address")
                Me.txtMobile.Text = Reader.Item("Mobile")
                Me.txtIDDetailes.Text = Reader.Item("IDDetailes")
                Me.txtEntitlement.Text = Reader.Item("Entitlement")
                Me.ComboJobDescribtion.Text = Reader.Item("JobDesc")
                Dim FileByte As Byte() = CType(Reader.Item("StaffImage"), Byte())
                Dim ms As New MemoryStream(FileByte)
                Me.PictureBox1.Image = Image.FromStream(ms)
                Me.txtJobdescr.Text = Reader.Item("Job")
                Me.txtResume.Text = Reader.Item("ResumeName")
                Me.txtJobdescr.Tag = Reader.Item("Job")
                Me.txtResume.Tag = Reader.Item("ResumeName")
                'Dim FileByte1 As Byte() = CType(Reader.Item("Resume"), Byte())
                'Dim ms1 As New MemoryStream(FileByte1)

                'Me.TextBox1.Text = Image.FromFile(FileByte1, )
                Me.txtEntitlement.Text = Reader.Item("Entitlement")
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

    Sub Clear()
        Me.TxtName.Clear()
        Me.txtMobile.Clear()
        Me.txtAddress.Clear()
        Me.txtIDDetailes.Clear()
        Me.txtEmail.Clear()
        Me.CombTitle.Text = ""
        Me.txtResume.Clear()
        Me.PictureBox1.Image = Nothing
        Me.OpenFileDialog1.FileName = ""
        Me.OpenFileDialog2.FileName = ""
        Me.txtJobdescr.Clear()
        Me.txtEntitlement.Clear()
        Me.ComboJobDescribtion.SelectedIndex = -1
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Clear()
        Me.txtId.Clear()
        Me.txtId.Focus()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub txtId_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtId.TextChanged
        Clear()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            Me.OpenFileDialog1.Filter = "Image Files (*.BMP;*.JPG;*.GIF;*.TIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.TIF;*.PNG|All files (*.*)|*.*"
            If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.PictureBox1.ImageLocation = Me.OpenFileDialog1.FileName
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            Me.OpenFileDialog2.Filter = "All Files (*.*)|*.*"
            If Me.OpenFileDialog2.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtResume.Text = Me.OpenFileDialog2.FileName
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Me.txtId.Text.Trim.Length = 0 Then
                MsgBox("Please enter a valid ID no.")
            ElseIf Me.CombTitle.Text.Trim.Length = 0 OrElse Me.TxtName.Text.Trim.Length = 0 OrElse Me.txtMobile.Text.Trim.Length = 0 _
               OrElse Me.txtAddress.Text.Trim.Length = 0 OrElse Me.txtIDDetailes.Text.Trim.Length = 0 _
               OrElse Me.txtEmail.Text.Trim.Length = 0 Then
                MsgBox("Please complete all personal details")
            ElseIf Me.PictureBox1.Image Is Nothing Then
                MsgBox("Please add an image")
                'ElseIf Me.TextBox1.Text.Trim.Length = 0 Then
                '    MsgBox("Please complete all job details")
            Else
                Dim cmd As New SqlCommand("update StaffProfiles set Title=N'" & Me.CombTitle.Text & "',Name=N'" & Me.TxtName.Text & "',Birth=N'" & Me.DTBirth.Value.ToString & "',HireDate=N'" & Me.DTHireDate.Value.ToString & _
                                          "',EAddress=N'" & Me.txtEmail.Text & "',Address=N'" & Me.txtAddress.Text & "',Mobile=N'" & Me.txtMobile.Text & _
                                          "',IDDetailes=N'" & Me.txtIDDetailes.Text & "',Entitlement=" & Me.txtEntitlement.Text & _
                                          ",JobDesc=N'" & Me.ComboJobDescribtion.SelectedItem & "' where EmpID=N'" & Me.txtId.Text & "'", cnn)
                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()

                If Me.txtResume.Text.Trim.Length <> 0 AndAlso Me.txtResume.Text <> Me.txtResume.Tag Then
                    SaveResume()
                End If

                If Me.txtJobdescr.Text.Trim.Length <> 0 AndAlso Me.txtJobdescr.Text <> Me.txtJobdescr.Tag Then
                    SavejobDesc()
                End If

                If Me.OpenFileDialog1.FileName <> "" Then
                    SaveImage()
                End If

                MsgBox("Saved Successfully")

                Clear()
                Me.txtId.Clear()
                Me.txtId.Focus()
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
        End Try
    End Sub

    Sub SaveResume()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim cmd As New SqlCommand("Update StaffProfiles Set Resume=@Resume,ResumeName=@ResumeName Where EmpID=N'" & Me.txtId.Text.Trim & "'", cnn)

            Dim f As New FileInfo(Me.txtResume.Text)
            Dim fs As New FileStream(Me.txtResume.Text, FileMode.Open)
            Dim FileByte As Byte() = New Byte(fs.Length) {}
            fs.Read(FileByte, 0, fs.Length)
            fs.Close()

            cmd.Parameters.AddWithValue("@ResumeName", f.Name)
            cmd.Parameters.AddWithValue("@Resume", FileByte)

            cnn.Open()
            cmd.ExecuteNonQuery()
            cnn.Close()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
        End Try
    End Sub

    Sub SavejobDesc()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim cmd As New SqlCommand("Update StaffProfiles Set JobDes=@JobDes,Job=@Job Where EmpID=N'" & Me.txtId.Text.Trim & "'", cnn)

            Dim f As New FileInfo(Me.txtJobdescr.Text)
            Dim fs As New FileStream(Me.txtJobdescr.Text, FileMode.Open)
            Dim FileByte As Byte() = New Byte(fs.Length) {}
            fs.Read(FileByte, 0, fs.Length)
            fs.Close()

            cmd.Parameters.AddWithValue("@Job", f.Name)
            cmd.Parameters.AddWithValue("@JobDes", FileByte)

            cnn.Open()
            cmd.ExecuteNonQuery()
            cnn.Close()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
        End Try
    End Sub

    Sub SaveImage()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim cmd As New SqlCommand("Update StaffProfiles Set StaffImage=@StaffImage Where EmpID=N'" & Me.txtId.Text.Trim & "'", cnn)

            If Me.PictureBox1.Image Is Nothing Then
                cmd.Parameters.AddWithValue("@StaffImage", SqlTypes.SqlBinary.Null)
            Else
                Dim str As String = Me.OpenFileDialog1.FileName
                Dim fs1 As New FileStream(str, FileMode.Open)
                Dim FileByte1 As Byte() = New Byte(fs1.Length) {}

                fs1.Read(FileByte1, 0, fs1.Length)
                fs1.Close()

                cmd.Parameters.AddWithValue("@StaffImage", FileByte1)
            End If

            cnn.Open()
            cmd.ExecuteNonQuery()
            cnn.Close()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Me.txtId.Text.Trim.Length = 0 OrElse Me.TxtName.Text.Trim.Length = 0 Then
            MsgBox("Please anter a valid ID no.")
        Else
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim cmd As New SqlCommand("Select Resume,ResumeName From StaffProfiles Where EmpID=N'" & Me.txtId.Text.Trim & "'", cnn)
                Dim Reader As SqlDataReader
                Dim FullPath As String

                cnn.Open()
                Reader = cmd.ExecuteReader
                While Reader.Read
                    FullPath = "C:\" + Reader.Item("ResumeName")


                    Dim fs As New FileStream(FullPath, FileMode.Create, FileAccess.Write)
                    Dim FileByte As Byte()

                    FileByte = CType(Reader.Item("Resume"), Byte())

                    fs.Write(FileByte, 0, FileByte.Length)

                    fs.Close()
                End While
                cnn.Close()

                System.Diagnostics.Process.Start(FullPath)
                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox(ex.ToString)
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
            End Try
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            Me.OpenFileDialog2.Filter = "All Files (*.*)|*.*"
            If Me.OpenFileDialog2.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtJobdescr.Text = Me.OpenFileDialog2.FileName
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Me.txtId.Text.Trim.Length = 0 OrElse Me.TxtName.Text.Trim.Length = 0 Then
            MsgBox("Please anter a valid ID no.")
        Else
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim cmd As New SqlCommand("Select JobDes,Job From StaffProfiles Where EmpID=N'" & Me.txtId.Text.Trim & "'", cnn)
                Dim Reader As SqlDataReader
                Dim FullPath As String

                cnn.Open()
                Reader = cmd.ExecuteReader
                While Reader.Read
                    FullPath = "C:\" + Reader.Item("Job")


                    Dim fs As New FileStream(FullPath, FileMode.Create, FileAccess.Write)
                    Dim FileByte As Byte()

                    FileByte = CType(Reader.Item("JobDes"), Byte())

                    fs.Write(FileByte, 0, FileByte.Length)

                    fs.Close()
                End While
                cnn.Close()

                System.Diagnostics.Process.Start(FullPath)
                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox(ex.ToString)
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
            End Try
        End If
    End Sub

    Private Sub frmEditStaffProfile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillJobDescribtion()
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

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim a As New frmJobDescEn
        a.ShowDialog()
        FillJobDescribtion()

    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim a As New frmSearchEmpID
        a.ShowDialog()
        If SelPatIDNo = "" Then
            Exit Sub
        End If
        Me.txtId.Text = SelPatIDNo
        FillData()
    End Sub

    Private Sub Label12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label12.Click

    End Sub
End Class