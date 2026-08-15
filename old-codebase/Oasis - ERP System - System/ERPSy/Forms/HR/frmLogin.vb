Imports System.Data.SqlClient

Public Class frmLogin
    Inherits System.Windows.Forms.Form

    Dim i As Integer = 0


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
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents LblError As System.Windows.Forms.Label
    Friend WithEvents txtSNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPassWord As System.Windows.Forms.TextBox
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogin))
        Me.btnConnect = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.LblError = New System.Windows.Forms.Label
        Me.txtSNo = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtPassWord = New System.Windows.Forms.TextBox
        Me.txtUserName = New System.Windows.Forms.TextBox
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnConnect
        '
        Me.btnConnect.BackColor = System.Drawing.Color.Transparent
        Me.btnConnect.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.btnConnect.Location = New System.Drawing.Point(444, 245)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(95, 30)
        Me.btnConnect.TabIndex = 1
        Me.btnConnect.Text = "Login"
        Me.btnConnect.UseVisualStyleBackColor = False
        '
        'ToolTip1
        '
        Me.ToolTip1.ShowAlways = True
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 10
        '
        'Timer2
        '
        Me.Timer2.Interval = 10
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.LblError)
        Me.GroupBox1.Controls.Add(Me.txtSNo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtPassWord)
        Me.GroupBox1.Controls.Add(Me.txtUserName)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.GroupBox1.Location = New System.Drawing.Point(177, 95)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(362, 144)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'LblError
        '
        Me.LblError.AutoSize = True
        Me.LblError.Font = New System.Drawing.Font("Tahoma", 12.0!)
        Me.LblError.ForeColor = System.Drawing.Color.Red
        Me.LblError.Location = New System.Drawing.Point(97, 118)
        Me.LblError.Name = "LblError"
        Me.LblError.Size = New System.Drawing.Size(223, 19)
        Me.LblError.TabIndex = 3
        Me.LblError.Text = "Invalid username or password"
        '
        'txtSNo
        '
        Me.txtSNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSNo.Location = New System.Drawing.Point(101, 17)
        Me.txtSNo.Name = "txtSNo"
        Me.txtSNo.Size = New System.Drawing.Size(88, 27)
        Me.txtSNo.TabIndex = 0
        Me.txtSNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 19)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "User ID:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 19)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Full Name:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 87)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(82, 19)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Password:"
        '
        'txtPassWord
        '
        Me.txtPassWord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPassWord.Location = New System.Drawing.Point(101, 85)
        Me.txtPassWord.Name = "txtPassWord"
        Me.txtPassWord.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassWord.Size = New System.Drawing.Size(250, 27)
        Me.txtPassWord.TabIndex = 1
        '
        'txtUserName
        '
        Me.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserName.Location = New System.Drawing.Point(101, 51)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.ReadOnly = True
        Me.txtUserName.Size = New System.Drawing.Size(250, 27)
        Me.txtUserName.TabIndex = 2
        '
        'frmLogin
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(551, 349)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnConnect)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(567, 387)
        Me.MinimumSize = New System.Drawing.Size(567, 387)
        Me.Name = "frmLogin"
        Me.Opacity = 0
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Kernel Investments "
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Sub Clear()
        Me.txtPassWord.Clear()
        Me.txtUserName.Clear()
        Me.txtUserName.Focus()
    End Sub

    Sub Login()
        Try
            If Me.txtUserName.Text.Trim.Length = 0 OrElse Me.txtPassWord.Text.Trim.Length = 0 Then
                Exit Sub
            End If

            Me.Cursor = Cursors.WaitCursor
            Dim Pass As String
            Dim B As Boolean = False

            Dim cmd As New SqlCommand("Select Pass From Users Where SNo=" & Me.txtSNo.Text.Trim, cnnLogin)
            Dim Reader As SqlDataReader

            cnnLogin.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Pass = CStr(Reader.Item(0))
                If Pass = CStr(Me.txtPassWord.Text) Then
                    CurrentUser = Me.txtUserName.Text
                    Employee = Me.txtUserName.Text
                    CurrentUserID = Me.txtSNo.Text
                    EmpNo = Me.txtSNo.Text
                    PWD = CStr(Me.txtPassWord.Text)
                    CurrentUser = Me.txtUserName.Text
                    PWD = Pass
                    'Priv = Reader.Item(1)
                    Me.Cursor = Cursors.Default
                    Reader.Close()
                    cnnLogin.Close()
                    frmMainHR.Show()
                    Me.Close()
                    'B = True
                    Exit Sub
                Else
                    Me.LblError.Text = "Invalid username or password"
                End If
            End While
            cnnLogin.Close()

            'If B = True Then
            '    Me.Close()
            'End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnnLogin.State = ConnectionState.Open Then
                cnnLogin.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub frmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Clear()
        Me.LblError.Text = ""
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.Opacity = Me.Opacity + 0.003
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Me.Timer1.Enabled = False
        Me.Opacity = Me.Opacity - 0.0027
        If Me.Opacity < 0.15 Then
            frmMainHR.ShowDialog()
            Me.Close()
        End If
    End Sub

    Private Sub txtSNo_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSNo.KeyUp
        If Me.txtSNo.Text.Trim.Length = 0 Then
            Exit Sub
        Else
            If e.KeyCode = Keys.Enter Then
                Try
                    Me.Cursor = Cursors.WaitCursor

                    Dim cmd As New SqlCommand("Select FullName From Users Where SNo=" & Me.txtSNo.Text, cnnLogin)

                    cnnLogin.Open()
                    Me.txtUserName.Text = CStr(cmd.ExecuteScalar)
                    cnnLogin.Close()

                    Me.Cursor = Cursors.Default
                Catch ex As Exception
                    Me.Cursor = Cursors.Default
                    MsgBox(ex.ToString)
                    If cnnLogin.State = ConnectionState.Open Then
                        cnnLogin.Close()
                    End If
                End Try
            End If
        End If
    End Sub

    Private Sub txtSNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSNo.TextChanged
        Me.txtUserName.Clear()
        Me.LblError.Text = ""
    End Sub

    Private Sub txtPassWord_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPassWord.KeyUp
        If e.KeyCode = Keys.Enter Then
            Login()
        End If
    End Sub

    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
        Login()
    End Sub

End Class
