Imports System.Data.SqlClient

Public Class frmUsersMang
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtFullName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNewPass As System.Windows.Forms.TextBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents CombUserII As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUsersMang))
        Me.TabControl2 = New System.Windows.Forms.TabControl
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.Button5 = New System.Windows.Forms.Button
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtFullName = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.GroupBox8 = New System.Windows.Forms.GroupBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtNewPass = New System.Windows.Forms.TextBox
        Me.Button3 = New System.Windows.Forms.Button
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.Button4 = New System.Windows.Forms.Button
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.CombUserII = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.ComboBox2 = New System.Windows.Forms.ComboBox
        Me.TabControl2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl2
        '
        Me.TabControl2.Controls.Add(Me.TabPage3)
        Me.TabControl2.Controls.Add(Me.TabPage1)
        Me.TabControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl2.Location = New System.Drawing.Point(0, 0)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(644, 351)
        Me.TabControl2.TabIndex = 61
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.ListView1)
        Me.TabPage3.Controls.Add(Me.Button5)
        Me.TabPage3.Controls.Add(Me.GroupBox4)
        Me.TabPage3.Controls.Add(Me.GroupBox5)
        Me.TabPage3.Controls.Add(Me.btnSave)
        Me.TabPage3.Controls.Add(Me.btnClose)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(636, 325)
        Me.TabPage3.TabIndex = 0
        Me.TabPage3.Text = "Add/Remove Users"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader2, Me.ColumnHeader1})
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(319, 14)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.RightToLeftLayout = True
        Me.ListView1.Size = New System.Drawing.Size(310, 275)
        Me.ListView1.TabIndex = 61
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "User ID"
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Full Name"
        Me.ColumnHeader1.Width = 235
        '
        'Button5
        '
        Me.Button5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Button5.Location = New System.Drawing.Point(554, 295)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(75, 22)
        Me.Button5.TabIndex = 60
        Me.Button5.Text = "Remove"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.txtFullName)
        Me.GroupBox4.Controls.Add(Me.Label2)
        Me.GroupBox4.Controls.Add(Me.txtPassword)
        Me.GroupBox4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(8, 6)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(305, 79)
        Me.GroupBox4.TabIndex = 0
        Me.GroupBox4.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(7, 19)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Full Name:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtFullName
        '
        Me.txtFullName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFullName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFullName.Location = New System.Drawing.Point(70, 17)
        Me.txtFullName.Name = "txtFullName"
        Me.txtFullName.Size = New System.Drawing.Size(224, 21)
        Me.txtFullName.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(7, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Password:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtPassword
        '
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPassword.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.Location = New System.Drawing.Point(70, 47)
        Me.txtPassword.MaxLength = 8
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(112, 21)
        Me.txtPassword.TabIndex = 1
        '
        'GroupBox5
        '
        Me.GroupBox5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.Location = New System.Drawing.Point(8, 91)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(305, 4)
        Me.GroupBox5.TabIndex = 59
        Me.GroupBox5.TabStop = False
        '
        'btnSave
        '
        Me.btnSave.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSave.Location = New System.Drawing.Point(122, 101)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 32)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save"
        '
        'btnClose
        '
        Me.btnClose.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnClose.Location = New System.Drawing.Point(227, 101)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 32)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox8)
        Me.TabPage1.Controls.Add(Me.Button3)
        Me.TabPage1.Controls.Add(Me.GroupBox7)
        Me.TabPage1.Controls.Add(Me.Button4)
        Me.TabPage1.Controls.Add(Me.GroupBox6)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(636, 325)
        Me.TabPage1.TabIndex = 2
        Me.TabPage1.Text = "Change Password"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.Label5)
        Me.GroupBox8.Controls.Add(Me.txtNewPass)
        Me.GroupBox8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox8.Location = New System.Drawing.Point(8, 50)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(462, 50)
        Me.GroupBox8.TabIndex = 1
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "New Password"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(14, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(57, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Password:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtNewPass
        '
        Me.txtNewPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewPass.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewPass.Location = New System.Drawing.Point(77, 19)
        Me.txtNewPass.MaxLength = 8
        Me.txtNewPass.Name = "txtNewPass"
        Me.txtNewPass.Size = New System.Drawing.Size(112, 21)
        Me.txtNewPass.TabIndex = 0
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(395, 114)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 32)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "Close"
        '
        'GroupBox7
        '
        Me.GroupBox7.Location = New System.Drawing.Point(8, 104)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(462, 4)
        Me.GroupBox7.TabIndex = 10
        Me.GroupBox7.TabStop = False
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(290, 114)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 32)
        Me.Button4.TabIndex = 2
        Me.Button4.Text = "Save"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.Label6)
        Me.GroupBox6.Controls.Add(Me.CombUserII)
        Me.GroupBox6.Location = New System.Drawing.Point(8, 6)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(462, 43)
        Me.GroupBox6.TabIndex = 0
        Me.GroupBox6.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(14, 17)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 13)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "User"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CombUserII
        '
        Me.CombUserII.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CombUserII.DropDownWidth = 140
        Me.CombUserII.Location = New System.Drawing.Point(49, 14)
        Me.CombUserII.Name = "CombUserII"
        Me.CombUserII.Size = New System.Drawing.Size(229, 21)
        Me.CombUserII.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(395, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "«·„” Œœ„ :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ComboBox2
        '
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.DropDownWidth = 140
        Me.ComboBox2.Location = New System.Drawing.Point(162, 14)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(229, 21)
        Me.ComboBox2.TabIndex = 0
        '
        'frmUsersMang
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(644, 351)
        Me.Controls.Add(Me.TabControl2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmUsersMang"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "User Accounts Management"
        Me.TabControl2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Sub Clear()
        Me.txtFullName.Clear()
        Me.txtPassword.Clear()
    End Sub

    'Sub UnCheckAll()
    '    Me.ChAssetsMng.Checked = False
    '    Me.ChProcSys.Checked = False
    '    Me.ChMedSys.Checked = False
    '    Me.ChAdminSys.Checked = False
    '    Me.ChHumResources.Checked = False
    '    Me.ChUsersMng.Checked = False
    '    Me.ChBackUp.Checked = False
    'End Sub

    'Sub CheckAll()
    '    Me.ChAssetsMng.Checked = True
    '    Me.ChProcSys.Checked = True
    '    Me.ChMedSys.Checked = True
    '    Me.ChAdminSys.Checked = True
    '    Me.ChHumResources.Checked = True
    '    Me.ChUsersMng.Checked = True
    '    Me.ChBackUp.Checked = True
    'End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If Me.CombUse.SelectedIndex = -1 Then
    '        MsgBox("Please select a valid user")
    '    Else
    '        Try
    '            Dim strUpdate As String
    '            strUpdate = "Update Users Set " & _
    '                        " ChAssetsMng=" & CInt(Me.ChAssetsMng.CheckState) & _
    '                        ",ChProcSys=" & CInt(Me.ChProcSys.CheckState) & _
    '                        ",ChMedSys=" & CInt(Me.ChMedSys.CheckState) & _
    '                        ",ChAdminSys=" & CInt(Me.ChAdminSys.CheckState) & _
    '                        ",ChHumResources=" & CInt(Me.ChHumResources.CheckState) & _
    '                        ",ChUsersMng=" & CInt(Me.ChUsersMng.CheckState) & _
    '                        ",ChBackUp=" & CInt(Me.ChBackUp.CheckState) & _
    '                        " Where FullName = N'" & Me.CombUser.SelectedItem & "'"

    '            Dim cmd As New SqlCommand(strUpdate, cnn)

    ''            cnn.Open()
    '            cmd.ExecuteNonQuery()
    '            cnn.Close()

    '            MsgBox("Saved Successfully")

    '            UnCheckAll()
    '            Me.CombUser.SelectedIndex = -1
    '        Catch ex As Exception
    '            Me.Cursor = Cursors.Default
    '            MsgBox(ex.ToString)
    '            If cnn.State = ConnectionState.Open Then
    '                cnn.Close()
    '            End If
    '        End Try
    '    End If
    'End Sub

    'Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If Me.CombUserII.SelectedIndex = -1 Then
    '            Exit Sub
    '        End If

    '        Me.Cursor = Cursors.WaitCursor
    '        Dim cmd As New SqlCommand("Select * From Users where FullName=N'" & Me.CombUser.SelectedItem & "'", cnn)
    '        Dim Reader As SqlDataReader

    '        cnn.Open()
    '        Reader = cmd.ExecuteReader
    '        While Reader.Read
    '            Me.ChAssetsMng.Checked = CBool(Reader.Item("ChAssetsMng"))
    '            Me.ChProcSys.Checked = CBool(Reader.Item("ChProcSys"))
    '            Me.ChMedSys.Checked = CBool(Reader.Item("ChMedSys"))
    '            Me.ChAdminSys.Checked = CBool(Reader.Item("ChAdminSys"))
    '            Me.ChHumResources.Checked = CBool(Reader.Item("ChHumResources"))
    '            Me.ChUsersMng.Checked = CBool(Reader.Item("ChUsersMng"))
    '            Me.ChBackUp.Checked = CBool(Reader.Item("ChBackUp"))
    '        End While
    '        cnn.Close()
    '        Me.Cursor = Cursors.Default
    '    Catch ex As Exception
    '        Me.Cursor = Cursors.Default
    '        MsgBox(ex.ToString)
    '        If cnn.State = ConnectionState.Open Then
    '            cnn.Close()
    '        End If
    '    End Try
    'End Sub

    'Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    CheckAll()
    'End Sub

    'Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    UnCheckAll()
    'End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Me.txtFullName.Text.Length = 0 Then
            MsgBox("Please enter full name")
            Me.txtPassword.Focus()
        ElseIf Me.txtPassword.Text.Length = 0 Then
            MsgBox("Please enter password")
            Me.txtFullName.Focus()
        Else
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim cmd As New SqlCommand("Insert Into Users (FullName, Pass) Values (N'" & Me.txtFullName.Text.Trim & _
                                          "',N'" & Me.txtPassword.Text & "')", cnn)

                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()

                'RESTORE DEFAULTS
                Clear()
                FillUsers()
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

    Sub FillUsers()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim cmd1 As New SqlCommand("Select SNo,FullName From Users Order By SNo", cnn)
            Dim SqlReader As SqlDataReader

            Me.CombUserII.Items.Clear()
            Me.ListView1.Items.Clear()

            cnn.Open()
            SqlReader = cmd1.ExecuteReader
            While SqlReader.Read
                Me.CombUserII.Items.Add(SqlReader.Item(1))
                With Me.ListView1.Items.Add(SqlReader.Item(0))
                    .subitems.add(SqlReader.Item(1))
                End With
            End While
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

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmManageUsers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillUsers()
    End Sub

    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button4_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If Me.CombUserII.SelectedIndex = -1 Then
            MsgBox("Please select user from the list")
        ElseIf Me.txtNewPass.Text.Length = 0 Then
            MsgBox("Please enter a new password")
            Me.txtNewPass.Focus()
        Else
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim cmd As New SqlCommand("Update Users set Pass=N'" & Me.txtNewPass.Text.Trim & _
                                         "' Where FullName=N'" & Me.CombUserII.SelectedItem & "'", cnn)
                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()

                MsgBox("Saved Successfully")
                Me.txtNewPass.Clear()
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

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If Me.ListView1.SelectedItems.Count = 0 Then
            Exit Sub
        ElseIf MsgBox("Confirm Delete?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim cmd As New SqlCommand("Delete From Users Where FullName=N'" & Me.ListView1.SelectedItems(0).SubItems(1).Text & "'", cnn)

                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()

                FillUsers()

                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
End Class
