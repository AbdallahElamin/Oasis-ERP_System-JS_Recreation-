Imports System.Data.SqlClient

Public Class frmMainHR

    Private Sub NewProfileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewProfileToolStripMenuItem.Click
        Dim a As New frmHR
        a.Show()
    End Sub

    Private Sub UpdateProfileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateProfileToolStripMenuItem.Click
        Dim a As New frmEditStaffProfile
        a.Show()
    End Sub

    'Private Sub PrintIDCardToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintIDCardToolStripMenuItem.Click
    '    Dim a As New frmPrintIDStaff
    '    a.Show()
    'End Sub

    Private Sub StaffListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StaffListToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim dap As New SqlDataAdapter("Select * From StaffProfiles where Active=1", cnn)
            Dim das As New DataSet

            cnn.Open()
            dap.Fill(das, "StaffProfiles")
            cnn.Close()

            Dim rpt As New StaffProfilesList
            rpt.SetDataSource(das)
            rptViewer.CrystalReportViewer1.ReportSource = rpt
            rptViewer.CrystalReportViewer1.RefreshReport()
            rptViewer.ShowDialog()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub ChangePasswordToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangePasswordToolStripMenuItem.Click
        Dim a As New frmChangePassword
        a.Show()
    End Sub

    Private Sub ContractToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkCont.Click
        Dim a As New frmOneYearContract
        a.Show()
    End Sub

    Private Sub NewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkNewJob.Click
        Dim a As New frmNewJob
        a.Show()
    End Sub

    'Private Sub ApplicationFormToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim a As New frmKbccApplication
    '    a.Show()
    'End Sub

    Private Sub LeavePlanToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim a As New frmVacationMngmnt
        a.Show()
    End Sub

    Private Sub VacationsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkVReq.Click
        Dim a As New frmVacationsRequest
        a.Show()
    End Sub

    
    Private Sub StaffLeavePlanToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkLP.Click
        Dim a As New frmVacationMngmnt
        a.Show()
    End Sub

    Private Sub SeStaffLeaveApplicationApprovalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkVSupApprove.Click
        Dim a As New frmVacationApprovalSup
        a.Show()
    End Sub

    Private Sub SupervisorStaffResumingDutyApplicationApprovalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkRSupApprove.Click
        Dim a As New frmResumeApprovalSup
        a.Show()
    End Sub

    Private Sub HRApprovalStaffLeaveApplicationFormToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkHrApprove.Click
        Dim a As New frmVacationApprovalHR
        a.Show()
    End Sub

    Private Sub HRApprovalDutyResumingApplicationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkHrRapprove.Click
        Dim a As New frmResumeAprovalHR
        a.Show()
    End Sub


    Private Sub ProcurementAuthoritiesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim a As New frmHrAuthorities
        a.Show()
    End Sub

    Private Sub frmMainHR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim cmd As New SqlCommand("Select * From Users where SNo=N'" & CurrentUserID & "'", cnnLogin)
            Dim Reader As SqlDataReader

            cnnLogin.Open() 'ChSP, ChkCont, chkNewJb, ChkPaySht, ChkHRApproval, ChkSupApproval, ChkVReq, ChkLP, ChkHR
            Reader = cmd.ExecuteReader
            While Reader.Read
                '' Me.ChkSp.Enabled = CBool(Reader.Item("ChSP"))
                'Me.ChkCont.Enabled = CBool(Reader.Item("ChkCont"))
                'Me.ChkNewJob.Enabled = CBool(Reader.Item("chkNewJb"))
                'Me.ChkPaySht.Enabled = CBool(Reader.Item("ChkPaySht"))
                'Me.ChkHrApprove.Enabled = CBool(Reader.Item("ChkHRApproval"))
                'Me.ChkHrRapprove.Enabled = CBool(Reader.Item("ChkHRApproval"))
                'Me.ChkLeaveArchive.Enabled = CBool(Reader.Item("ChkHRApproval"))
                'Me.ChkDutyArchive.Enabled = CBool(Reader.Item("ChkHRApproval"))
                'Me.ChkSAA.Enabled = Reader.Item("ChkHRApproval")
                'Me.ChkVReq.Enabled = CBool(Reader.Item("ChkVReq"))
                'Me.ChkRSupApprove.Enabled = CBool(Reader.Item("ChkSupApproval"))
                'Me.ChkSA.Enabled = CBool(Reader.Item("ChkSupApproval"))
                'Me.ChkVSupApprove.Enabled = CBool(Reader.Item("ChkSupApproval"))
                'Me.ChkLP.Enabled = CBool(Reader.Item("ChkLP"))
                'Me.ChkHr.Enabled = CBool(Reader.Item("ChkHR"))

            End While
            cnnLogin.Close()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnnLogin.State = ConnectionState.Open Then
                cnnLogin.Close()
            End If
            MsgBox(ex.ToString)
            End
        End Try
    End Sub

    Private Sub ViewPaySheetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewPaySheetToolStripMenuItem.Click
        Dim a As New frmPaySheetArchive
        a.Show()
    End Sub

    Private Sub SetEmployeeSalaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetEmployeeSalaryToolStripMenuItem.Click
        Dim a As New frmPaySheet
        a.Show()
    End Sub

    Private Sub AddGradeLevelAllowanceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddGradeLevelAllowanceToolStripMenuItem.Click
        Dim a As New frmGradeLevelAllowances
        a.Show()
    End Sub

    Private Sub ChkLeaveArchive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkLeaveArchive.Click
        Dim a As New frmVacationArchive
        a.Show()
    End Sub

    Private Sub ChkDutyArchive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkDutyArchive.Click
        Dim a As New frmResumeArchive
        a.Show()
    End Sub

    Private Sub ViewNationalInsuranceContributionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewNationalInsuranceContributionToolStripMenuItem.Click
        Dim a As New frmNatInsContribution
        a.Show()
    End Sub

    Private Sub StaffAppraisalToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StaffAppraisalToolStripMenuItem.Click
        Dim a As New g
        a.Show()
    End Sub

    Private Sub ChkSAA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkSAA.Click
        Dim a As New frmAppraisalApprove
        a.Show()
    End Sub

    Private Sub AddNewBankAccountToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNewBankAccountToolStripMenuItem.Click
        Dim a As New frmMoney_Transfer
        a.Show()
    End Sub

    Private Sub HRAuthoritiesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim a As New frmHrAuthorities
        a.Show()
    End Sub

    Private Sub ChkHr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkHr.Click
        Dim a As New frmHrAuthorities
        a.Show()
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub
End Class