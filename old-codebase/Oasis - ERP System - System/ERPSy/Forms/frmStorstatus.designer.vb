<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStorstatus
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStorstatus))
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DTPPeriodTo = New System.Windows.Forms.DateTimePicker()
        Me.DTPPeriodFRm = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.RAll = New System.Windows.Forms.RadioButton()
        Me.RStore = New System.Windows.Forms.RadioButton()
        Me.CombStore = New System.Windows.Forms.ComboBox()
        Me.ErrProv = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.btnShow = New System.Windows.Forms.Button()
        Me.CrystalReportViewer1 = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.GroupBox3.SuspendLayout()
        CType(Me.ErrProv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(306, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 16)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "From"
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(580, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(23, 16)
        Me.Label6.TabIndex = 30
        Me.Label6.Text = "To"
        '
        'DTPPeriodTo
        '
        Me.DTPPeriodTo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.DTPPeriodTo.CustomFormat = "dddd, dd/MM/yyyy"
        Me.DTPPeriodTo.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPPeriodTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPPeriodTo.Location = New System.Drawing.Point(610, 18)
        Me.DTPPeriodTo.Name = "DTPPeriodTo"
        Me.DTPPeriodTo.Size = New System.Drawing.Size(179, 23)
        Me.DTPPeriodTo.TabIndex = 28
        '
        'DTPPeriodFRm
        '
        Me.DTPPeriodFRm.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.DTPPeriodFRm.CustomFormat = "dddd, dd/MM/yyyy"
        Me.DTPPeriodFRm.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPPeriodFRm.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPPeriodFRm.Location = New System.Drawing.Point(343, 16)
        Me.DTPPeriodFRm.Name = "DTPPeriodFRm"
        Me.DTPPeriodFRm.Size = New System.Drawing.Size(179, 23)
        Me.DTPPeriodFRm.TabIndex = 27
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.DTPPeriodTo)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.RAll)
        Me.GroupBox3.Controls.Add(Me.RStore)
        Me.GroupBox3.Controls.Add(Me.CombStore)
        Me.GroupBox3.Controls.Add(Me.DTPPeriodFRm)
        Me.GroupBox3.Font = New System.Drawing.Font("Tahoma", 9.75!)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 7)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(833, 66)
        Me.GroupBox3.TabIndex = 62
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = " Select Store"
        '
        'RAll
        '
        Me.RAll.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RAll.AutoSize = True
        Me.RAll.Location = New System.Drawing.Point(13, 44)
        Me.RAll.Name = "RAll"
        Me.RAll.Size = New System.Drawing.Size(40, 20)
        Me.RAll.TabIndex = 35
        Me.RAll.Text = "All"
        Me.RAll.UseVisualStyleBackColor = True
        '
        'RStore
        '
        Me.RStore.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.RStore.AutoSize = True
        Me.RStore.Checked = True
        Me.RStore.Location = New System.Drawing.Point(13, 18)
        Me.RStore.Name = "RStore"
        Me.RStore.Size = New System.Drawing.Size(57, 20)
        Me.RStore.TabIndex = 3
        Me.RStore.TabStop = True
        Me.RStore.Text = "Store"
        Me.RStore.UseVisualStyleBackColor = True
        '
        'CombStore
        '
        Me.CombStore.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.CombStore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CombStore.FormattingEnabled = True
        Me.CombStore.Items.AddRange(New Object() {"Jeddah", "Riyadh"})
        Me.CombStore.Location = New System.Drawing.Point(76, 16)
        Me.CombStore.Name = "CombStore"
        Me.CombStore.Size = New System.Drawing.Size(179, 24)
        Me.CombStore.TabIndex = 2
        '
        'ErrProv
        '
        Me.ErrProv.ContainerControl = Me
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(847, 25)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(75, 32)
        Me.btnShow.TabIndex = 63
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'CrystalReportViewer1
        '
        Me.CrystalReportViewer1.ActiveViewIndex = -1
        Me.CrystalReportViewer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CrystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default
        Me.CrystalReportViewer1.Location = New System.Drawing.Point(5, 79)
        Me.CrystalReportViewer1.Name = "CrystalReportViewer1"
        Me.CrystalReportViewer1.Size = New System.Drawing.Size(925, 522)
        Me.CrystalReportViewer1.TabIndex = 64
        '
        'frmStorstatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(934, 605)
        Me.Controls.Add(Me.CrystalReportViewer1)
        Me.Controls.Add(Me.btnShow)
        Me.Controls.Add(Me.GroupBox3)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(950, 537)
        Me.Name = "frmStorstatus"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Stoke Statistics"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.ErrProv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DTPPeriodTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPPeriodFRm As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents RAll As System.Windows.Forms.RadioButton
    Friend WithEvents RStore As System.Windows.Forms.RadioButton
    Friend WithEvents CombStore As System.Windows.Forms.ComboBox
    Friend WithEvents ErrProv As System.Windows.Forms.ErrorProvider
    Friend WithEvents CrystalReportViewer1 As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents btnShow As System.Windows.Forms.Button
End Class
