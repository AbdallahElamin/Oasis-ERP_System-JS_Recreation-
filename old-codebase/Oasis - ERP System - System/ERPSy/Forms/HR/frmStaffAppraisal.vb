Imports System.Data.SqlClient
Public Class g

    Sub FillGrid()
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.DataGridView1.Rows.Clear()
            Dim cmd As New SqlCommand("select SNo,Year,LevelAchievenment,ImpactContract,isnull(DecisionCoordinatoin,N'')DecisionCoordinatoin from StaffAppraisal where ID=" & Me.TxtEmpNo.Text, cnn)
            Dim Reader As SqlDataReader
            cnn.Open()
            Reader = cmd.ExecuteReader
            Me.DataGridView1.Rows.Clear()
            While Reader.Read
                Me.DataGridView1.Rows.Add(New String() {Reader.Item("SNo"), Reader.Item("Year"), Reader.Item("LevelAchievenment"), _
                                                        Reader.Item("ImpactContract"), Reader.Item("DecisionCoordinatoin")})
                
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

    Sub sumRad()
        Me.txtNumber0.Text = -CInt(Me.RBQualityofwork0.Checked) - CInt(Me.RBMeetingdeadlines0.Checked) - CInt(Me.RBDedicationtasks0.Checked) - _
        CInt(Me.RBCommitmentMotivation0.Checked) - CInt(Me.RBTheoreticalPractical0.Checked) - CInt(Me.RBInitiative0.Checked) - CInt(Me.RBSocialskills0.Checked) - _
        CInt(Me.RBOutfitrespectful0.Checked) - CInt(Me.RBAutonomy0.Checked) - CInt(Me.RBOrganisationalabilities0.Checked) - CInt(Me.RBInformaitionsharing0.Checked) - _
        CInt(Me.RBTeamwork0.Checked)

        Me.txtNumber1.Text = -CInt(Me.RBQualityofwork1.Checked) - CInt(Me.RBMeetingdeadlines1.Checked) - CInt(Me.RBDedicationtasks1.Checked) - _
       CInt(Me.RBCommitmentMotivation1.Checked) - CInt(Me.RBTheoreticalPractical1.Checked) - CInt(Me.RBInitiative1.Checked) - CInt(Me.RBSocialskills1.Checked) - _
       CInt(Me.RBOutfitrespectful1.Checked) - CInt(Me.RBAutonomy1.Checked) - CInt(Me.RBOrganisationalabilities1.Checked) - CInt(Me.RBTeamwork1.Checked) - _
       CInt(Me.RBInformaitionsharing1.Checked)

        Me.txtNumber2.Text = -CInt(Me.RBQualityofwork2.Checked) - CInt(Me.RBMeetingdeadlines2.Checked) - CInt(Me.RBDedicationtasks2.Checked) - _
       CInt(Me.RBCommitmentMotivation2.Checked) - CInt(Me.RBTheoreticalPractical2.Checked) - CInt(Me.RBInitiative2.Checked) - CInt(Me.RBSocialskills2.Checked) - _
       CInt(Me.RBOutfitrespectful2.Checked) - CInt(Me.RBAutonomy2.Checked) - CInt(Me.RBOrganisationalabilities2.Checked) - CInt(Me.RBTeamwork2.Checked) - _
       CInt(Me.RBInformaitionsharing2.Checked)

        Me.txtNumber3.Text = -CInt(Me.RBQualityofwork3.Checked) - CInt(Me.RBMeetingdeadlines3.Checked) - CInt(Me.RBDedicationtasks3.Checked) - _
      CInt(Me.RBCommitmentMotivation3.Checked) - CInt(Me.RBTheoreticalPractical3.Checked) - CInt(Me.RBInitiative3.Checked) - CInt(Me.RBSocialskills3.Checked) - _
      CInt(Me.RBOutfitrespectful3.Checked) - CInt(Me.RBAutonomy3.Checked) - CInt(Me.RBOrganisationalabilities3.Checked) - CInt(Me.RBTeamwork3.Checked) - _
      CInt(Me.RBInformaitionsharing3.Checked)
        Me.txtTotal.Text = Me.txtNumber0.Text * 0 + Me.txtNumber1.Text * 1 + Me.txtNumber2.Text * 2 + Me.txtNumber3.Text * 3
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBQualityofwork0.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBQualityofwork1.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBQualityofwork2.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBQualityofwork3.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBMeetingdeadlines0.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBMeetingdeadlines1.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBMeetingdeadlines2.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBMeetingdeadlines3.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton12_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBDedicationtasks0.CheckedChanged
        sumRad()
    End Sub


    Private Sub RadioButton11_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBDedicationtasks1.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBDedicationtasks2.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBDedicationtasks3.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton16_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBCommitmentMotivation0.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton15_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBCommitmentMotivation1.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton14_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBCommitmentMotivation2.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton13_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBCommitmentMotivation3.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton20_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBTheoreticalPractical0.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton19_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBTheoreticalPractical1.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton18_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBTheoreticalPractical2.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton17_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBTheoreticalPractical3.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton24_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBInitiative0.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton23_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBInitiative1.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton22_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBInitiative2.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton21_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBInitiative3.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton28_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBSocialskills0.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton27_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBSocialskills1.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton26_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBSocialskills2.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton25_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBSocialskills3.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton60_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBOutfitrespectful0.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton59_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBOutfitrespectful1.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton58_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBOutfitrespectful2.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton57_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBOutfitrespectful3.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton68_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBAutonomy0.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton67_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBAutonomy1.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton66_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBAutonomy2.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton65_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBAutonomy3.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton76_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBOrganisationalabilities0.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton75_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBOrganisationalabilities1.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton74_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBOrganisationalabilities2.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton73_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBOrganisationalabilities3.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton84_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBTeamwork0.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton83_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBTeamwork1.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton82_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBTeamwork2.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton81_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBTeamwork3.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton80_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBInformaitionsharing0.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton79_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBInformaitionsharing1.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton78_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBInformaitionsharing2.CheckedChanged
        sumRad()
    End Sub

    Private Sub RadioButton77_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBInformaitionsharing3.CheckedChanged
        sumRad()
    End Sub

    Private Sub Button32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button32.Click
        Me.Close()
    End Sub

    Private Sub Button31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button31.Click
        If Me.TxtEmpNo.Text.Trim.Length = 0 Or Me.txtName.Text.Trim.Length = 0 Then
            MsgBox("Please enter a valid employee number")
        ElseIf Me.RBQualityofwork0.Checked = False And Me.RBQualityofwork1.Checked = False _
        And Me.RBQualityofwork2.Checked = False And Me.RBQualityofwork3.Checked = False Then
            MsgBox("Quality of work achievement is not specified ")
        ElseIf Me.RBMeetingdeadlines0.Checked = False And Me.RBMeetingdeadlines1.Checked = False _
        And Me.RBMeetingdeadlines2.Checked = False And Me.RBMeetingdeadlines3.Checked = False Then
            MsgBox("Meeting dead lines achievement is not specified ")
        ElseIf RBDedicationtasks0.Checked = False And Me.RBDedicationtasks1.Checked = False _
        And Me.RBDedicationtasks2.Checked = False And Me.RBDedicationtasks3.Checked = False Then
            MsgBox("dedication in tasks achievement is not specified")
        ElseIf Me.RBCommitmentMotivation0.Checked = False And Me.RBCommitmentMotivation1.Checked = False _
        And Me.RBCommitmentMotivation2.Checked = False And Me.RBCommitmentMotivation3.Checked = False Then
            MsgBox("Commitment/Motivation achievement is not specified")
        ElseIf Me.RBTheoreticalPractical0.Checked = False And Me.RBTheoreticalPractical1.Checked = False _
        And Me.RBTheoreticalPractical2.Checked = False And Me.RBTheoreticalPractical3.Checked = False Then
            MsgBox("Theoretical and practical knowledge achievement is not specified")
        ElseIf Me.RBInitiative0.Checked = False And Me.RBInitiative1.Checked = False _
        And Me.RBInitiative2.Checked = False And Me.RBInitiative3.Checked = False Then
            MsgBox("Intitative achievement is not specified")
        ElseIf Me.RBSocialskills0.Checked = False And Me.RBSocialskills1.Checked = False _
         And Me.RBSocialskills2.Checked = False And Me.RBSocialskills3.Checked = False Then
            MsgBox("Social skills achievement is not specified")
        ElseIf Me.RBOutfitrespectful0.Checked = False And Me.RBOutfitrespectful1.Checked = False _
        And Me.RBOutfitrespectful2.Checked = False And Me.RBOutfitrespectful3.Checked = False Then
            MsgBox("Outfit/Respectful of Company image achievement is not specified")
        ElseIf Me.RBAutonomy0.Checked = False And Me.RBAutonomy1.Checked = False _
        And Me.RBAutonomy2.Checked = False And Me.RBAutonomy3.Checked = False Then
            MsgBox("Autonomy achievement is not specified")
        ElseIf Me.RBOrganisationalabilities0.Checked = False And Me.RBOrganisationalabilities1.Checked = False _
        And Me.RBOrganisationalabilities2.Checked = False And Me.RBOrganisationalabilities3.Checked = False Then
            MsgBox("Organisational abilities achievement is not specified")
        ElseIf Me.RBTeamwork0.Checked = False And Me.RBTeamwork1.Checked = False _
        And Me.RBTeamwork2.Checked = False And Me.RBTeamwork3.Checked = False Then
            MsgBox("Team work achievement is not specified")
        ElseIf Me.RBInformaitionsharing0.Checked = False And Me.RBInformaitionsharing1.Checked = False _
        And Me.RBInformaitionsharing2.Checked = False And Me.RBInformaitionsharing3.Checked = False Then
            MsgBox("Information sharing achievement is not specified")
        Else
            Try
                Dim LvlAchievment, Impact As String

                If Me.txtTotal.Text > 0 And Me.txtTotal.Text < 7 Then
                    LvlAchievment = "Totally Unsatisfactory"
                    Impact = "No contract extension"
                ElseIf Me.txtTotal.Text > 6 And Me.txtTotal.Text < 13 Then
                    LvlAchievment = "Very Unsatisfactory"
                    Impact = "High risk of no contract extension"
                ElseIf Me.txtTotal.Text > 12 And Me.txtTotal.Text < 19 Then
                    LvlAchievment = "Unsatisfactory"
                    Impact = "Risk of no contract extension"
                ElseIf Me.txtTotal.Text > 18 And Me.txtTotal.Text < 25 Then
                    LvlAchievment = "Satisfactory"
                    Impact = "No impact"
                ElseIf Me.txtTotal.Text > 24 And Me.txtTotal.Text < 31 Then
                    LvlAchievment = "Good"
                    Impact = "1 Step salary raise Proposed"
                ElseIf Me.txtTotal.Text > 30 And Me.txtTotal.Text < 37 Then
                    LvlAchievment = "Very Good"
                    Impact = "2 Step salary raise Proposed"

                End If
                Dim cmd1 As New SqlCommand("select count(*) from StaffAppraisal where ID=" & Me.TxtEmpNo.Text & _
                                         " and Year(Year)=" & CInt(Me.DateTimePicker1.Value.Year), cnn)
                cnn.Open()
                If cmd1.ExecuteScalar > 0 Then
                    Dim cmd2 As New SqlCommand("delete from StaffAppraisal where ID=" & Me.TxtEmpNo.Text & _
                                           "   and Year(Year)=" & CInt(Me.DateTimePicker1.Value.Year), cnn)
                    cmd2.ExecuteNonQuery()
                End If
                cnn.Close()

                Dim cmd As New SqlCommand
                Dim Trans As SqlTransaction

                cnn.Open()
                cmd.Connection = cnn
                Trans = cnn.BeginTransaction
                cmd.Transaction = Trans
                Me.Cursor = Cursors.WaitCursor
                cmd.CommandText = "insert into StaffAppraisal(Name,JobDesc, ID,Qualityofwork0,Qualityofwork1,Qualityofwork2,Qualityofwork3," & _
                                          "Qualityofwork,Meetingdeadlines0,Meetingdeadlines1,Meetingdeadlines2,Meetingdeadlines3,Meetingdeadlines," & _
                                          "Dedicationtasks0,Dedicationtasks1,Dedicationtasks2,Dedicationtasks3,Dedicationtasks,CommitmentMotivation0," & _
                                          "CommitmentMotivation1,CommitmentMotivation2,CommitmentMotivation3,CommitmentMotivation," & _
                                          "TheoreticalPractical0,TheoreticalPractical1,TheoreticalPractical2,TheoreticalPractical3," & _
                                          "TheoreticalPractical,Initiative0,Initiative1,Initiative2,Initiative3,Initiative,Socialskills0," & _
                                          "Socialskills1,Socialskills2,Socialskills3,Socialskills,Outfitrespectful0,Outfitrespectful1," & _
                                          "Outfitrespectful2,Outfitrespectful3,Outfitrespectful,Autonomy0,Autonomy1,Autonomy2,Autonomy3,Autonomy," & _
                                          "Organisationalabilities0,Organisationalabilities1,Organisationalabilities2,Organisationalabilities3," & _
                                          "Organisationalabilities,Teamwork0,Teamwork1,Teamwork2,Teamwork3,Teamwork,Informaitionsharing0," & _
                                          "Informaitionsharing1,Informaitionsharing2,Informaitionsharing3,Informaitionsharing,Number0,Number1," & _
                                          "Number2,Number3,Comments,total,year,LevelAchievenment,ImpactContract, CurrentUser) Values (N'" & Me.txtName.Text & _
                                          "',N'" & Me.txtJob.Text & "'," & Me.TxtEmpNo.Text & "," & -CInt(Me.RBQualityofwork0.Checked) & "," & -CInt(Me.RBQualityofwork1.Checked) & _
                                          "," & -CInt(Me.RBQualityofwork2.Checked) & "," & -CInt(Me.RBQualityofwork3.Checked) & _
                                          ",N'" & Me.txtQualityofwork.Text & "'," & -CInt(Me.RBMeetingdeadlines0.Checked) & _
                                          "," & -CInt(Me.RBMeetingdeadlines1.Checked) & "," & -CInt(Me.RBMeetingdeadlines2.Checked) & _
                                          "," & -CInt(Me.RBMeetingdeadlines3.Checked) & ",N'" & Me.txtMeetingdeadlines.Text & _
                                          "'," & -CInt(Me.RBDedicationtasks0.Checked) & "," & -CInt(Me.RBDedicationtasks1.Checked) & _
                                          "," & -CInt(Me.RBDedicationtasks2.Checked) & "," & -CInt(Me.RBDedicationtasks3.Checked) & _
                                          ",N'" & Me.txtDedicationtasks.Text & "'," & -CInt(Me.RBCommitmentMotivation0.Checked) & _
                                          "," & -CInt(Me.RBCommitmentMotivation1.Checked) & "," & -CInt(Me.RBCommitmentMotivation2.Checked) & _
                                          "," & -CInt(Me.RBCommitmentMotivation3.Checked) & ",N'" & Me.txtCommitmentMotivation.Text & _
                                          "'," & -CInt(Me.RBTheoreticalPractical0.Checked) & "," & -CInt(Me.RBTheoreticalPractical1.Checked) & _
                                          "," & -CInt(Me.RBTheoreticalPractical2.Checked) & "," & -CInt(Me.RBTheoreticalPractical3.Checked) & _
                                          ",N'" & Me.txtTheoreticalPractical.Text & "'," & -CInt(Me.RBInitiative0.Checked) & _
                                          "," & -CInt(Me.RBInitiative1.Checked) & "," & -CInt(Me.RBInitiative2.Checked) & _
                                          "," & -CInt(Me.RBInitiative3.Checked) & ",N'" & Me.txtInitiative.Text & "'," & -CInt(Me.RBSocialskills0.Checked) & _
                                          "," & -CInt(Me.RBSocialskills1.Checked) & "," & -CInt(Me.RBSocialskills2.Checked) & _
                                          "," & -CInt(Me.RBSocialskills3.Checked) & ",N'" & Me.txtSocialskills.Text & _
                                          "'," & -CInt(Me.RBOutfitrespectful0.Checked) & "," & -CInt(Me.RBOutfitrespectful1.Checked) & _
                                          "," & -CInt(Me.RBOutfitrespectful2.Checked) & "," & -CInt(Me.RBOutfitrespectful3.Checked) & _
                                          ",N'" & Me.txtOutfitrespectful.Text & "'," & -CInt(Me.RBAutonomy0.Checked) & _
                                          "," & -CInt(Me.RBAutonomy1.Checked) & "," & -CInt(Me.RBAutonomy2.Checked) & _
                                          "," & -CInt(Me.RBAutonomy3.Checked) & ",N'" & Me.txtAutonomy.Text & _
                                          "'," & -CInt(Me.RBOrganisationalabilities0.Checked) & "," & -CInt(Me.RBOrganisationalabilities1.Checked) & _
                                          "," & -CInt(Me.RBOrganisationalabilities2.Checked) & "," & -CInt(Me.RBOrganisationalabilities3.Checked) & _
                                          ",N'" & Me.txtOrganisationalabilities.Text & "'," & -CInt(Me.RBTeamwork0.Checked) & _
                                          "," & -CInt(Me.RBTeamwork1.Checked) & "," & -CInt(Me.RBTeamwork2.Checked) & _
                                          "," & -CInt(Me.RBTeamwork3.Checked) & ",N'" & Me.txtTeamwork.Text & _
                                          "'," & -CInt(Me.RBInformaitionsharing0.Checked) & "," & -CInt(Me.RBInformaitionsharing1.Checked) & _
                                          "," & -CInt(Me.RBInformaitionsharing2.Checked) & "," & -CInt(Me.RBInformaitionsharing3.Checked) & _
                                          ",N'" & Me.txtInformaitionsharing.Text & "'," & Me.txtNumber0.Text & "," & Me.txtNumber1.Text & _
                                          "," & Me.txtNumber2.Text & "," & Me.txtNumber3.Text & ",N'" & Me.txtComments.Text & _
                                          "'," & Me.txtTotal.Text & ",N'" & Me.DateTimePicker1.Value.ToString("MM / dd / yyyy") & "',N'" & LvlAchievment & _
                                          "',N'" & Impact & "',N'" & CurrentUser & "')"
                cmd.ExecuteNonQuery()
                Trans.Commit()
                cnn.Close()
                MsgBox("Saved Successfully")
                clear()
                FillGrid()
                'PrintRpt()

                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                End If
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Public Sub PrintRpt()
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dap As New SqlDataAdapter("select * from StaffAppraisal where SNo =(select Max(SNo) from StaffAppraisal)", cnn)
            Dim das As New DataSet

            dap.Fill(das, "StaffAppraisal")

            Dim rpt As New StaffAppr
            rpt.SetDataSource(das)
            frmReportViewer.CrystalReportViewer1.ReportSource = rpt
            frmReportViewer.CrystalReportViewer1.RefreshReport()
            frmReportViewer.ShowDialog()

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn1.State = ConnectionState.Open Then
                cnn1.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub FillData()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select Name,JobDesc From StaffProfiles Where EmpID=N'" & Me.TxtEmpNo.Text & "'", cnn)
            Dim Reader As SqlDataReader


            Me.txtName.Clear()
            Me.txtJob.Clear()
            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read

                Me.txtName.Text = Reader.Item("Name")
                Me.txtJob.Text = Reader.Item("JobDesc")

            End While
            cnn.Close()
            FillGrid()
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If Me.TxtEmpNo.Text.Trim.Length > 0 Then
            FillData()
            FillGrid()
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.TxtEmpNo.Clear()
        Dim a As New frmSearchEmpID
        a.ShowDialog()

        If SelPatIDNo = "" Then
            Exit Sub
        End If

        Me.TxtEmpNo.Text = SelPatIDNo
        FillData()
    End Sub

    Private Sub TxtEmpNo_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtEmpNo.KeyUp
        If e.KeyCode = Keys.Enter Then
            If Me.TxtEmpNo.Text.Trim.Length > 0 Then
                FillData()
                FillGrid()
            End If
        End If
    End Sub

    
    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Try
    '        Me.Cursor = Cursors.WaitCursor
    '        If Me.DataGridView1.SelectedRows.Count > 0 Then

    '            If MsgBox("Confirm Delete", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then


    '                Dim dat As Integer = CInt(Me.DataGridView1.CurrentRow.Cells(0).Value)
    '                Dim das As New DataSet
    '                Dim cmd As New SqlCommand("Delete from StaffAppraisal where SNo=" & dat, cnn)
    '                cnn.Open()
    '                cmd.ExecuteNonQuery()
    '                cnn.Close()
    '                FillGrid()
    '            End If

    '        End If
    '        Me.Cursor = Cursors.Default
    '    Catch ex As Exception
    '        Me.Cursor = Cursors.Default
    '        If cnn.State = ConnectionState.Open Then
    '            cnn.Close()
    '        End If
    '        MsgBox(ex.ToString)
    '    End Try
    'End Sub

    Sub clear()
        'Me.txtJob.Text = ""
        'Me.TxtEmpNo.Text = ""
        'Me.txtName.Text = ""

        Me.RBQualityofwork0.Checked = False
        Me.RBQualityofwork1.Checked = False
        Me.RBQualityofwork2.Checked = False
        Me.RBQualityofwork3.Checked = False

        Me.RBMeetingdeadlines0.Checked = False
        Me.RBMeetingdeadlines1.Checked = False
        Me.RBMeetingdeadlines2.Checked = False
        Me.RBMeetingdeadlines3.Checked = False

        Me.RBDedicationtasks0.Checked = False
        Me.RBDedicationtasks1.Checked = False
        Me.RBDedicationtasks2.Checked = False
        Me.RBDedicationtasks3.Checked = False

        Me.RBCommitmentMotivation0.Checked = False
        Me.RBCommitmentMotivation1.Checked = False
        Me.RBCommitmentMotivation2.Checked = False
        Me.RBCommitmentMotivation3.Checked = False

        Me.RBTheoreticalPractical0.Checked = False
        Me.RBTheoreticalPractical1.Checked = False
        Me.RBTheoreticalPractical2.Checked = False
        Me.RBTheoreticalPractical3.Checked = False

        Me.RBInitiative0.Checked = False
        Me.RBInitiative1.Checked = False
        Me.RBInitiative2.Checked = False
        Me.RBInitiative3.Checked = False

        Me.RBSocialskills0.Checked = False
        Me.RBSocialskills1.Checked = False
        Me.RBSocialskills2.Checked = False
        Me.RBSocialskills3.Checked = False

        Me.RBOutfitrespectful0.Checked = False
        Me.RBOutfitrespectful1.Checked = False
        Me.RBOutfitrespectful2.Checked = False
        Me.RBOutfitrespectful3.Checked = False

        Me.RBAutonomy0.Checked = False
        Me.RBAutonomy1.Checked = False
        Me.RBAutonomy2.Checked = False
        Me.RBAutonomy3.Checked = False

        Me.RBOrganisationalabilities0.Checked = False
        Me.RBOrganisationalabilities1.Checked = False
        Me.RBOrganisationalabilities2.Checked = False
        Me.RBOrganisationalabilities3.Checked = False

        Me.RBTeamwork0.Checked = False
        Me.RBTeamwork1.Checked = False
        Me.RBTeamwork2.Checked = False
        Me.RBTeamwork3.Checked = False

        Me.RBInformaitionsharing0.Checked = False
        Me.RBInformaitionsharing1.Checked = False
        Me.RBInformaitionsharing2.Checked = False
        Me.RBInformaitionsharing3.Checked = False

        Me.txtAutonomy.Text = ""
        Me.txtTeamwork.Text = ""
        Me.txtInformaitionsharing.Text = ""
        Me.txtInitiative.Text = ""
        Me.txtOrganisationalabilities.Text = ""
        Me.txtOutfitrespectful.Text = ""
        Me.txtSocialskills.Text = ""
        Me.txtTheoreticalPractical.Text = ""
        Me.txtCommitmentMotivation.Text = ""
        Me.txtMeetingdeadlines.Text = ""
        Me.txtQualityofwork.Text = ""
        Me.txtDedicationtasks.Text = ""
        Me.txtNumber0.Text = 0
        Me.txtNumber1.Text = 0
        Me.txtNumber2.Text = 0
        Me.txtNumber3.Text = 0
        Me.DataGridView1.Rows.Clear()



        'Me.RBQualityofwork0.Checked = True
        'Me.RBMeetingdeadlines0.Checked = True
        'Me.RBDedicationtasks0.Checked = True
        'Me.RBCommitmentMotivation0.Checked = True
        'Me.RBTheoreticalPractical0.Checked = True
        'Me.RBInitiative0.Checked = True
        'Me.RBSocialskills0.Checked = True
        'Me.RBOutfitrespectful0.Checked = True
        'Me.RBAutonomy0.Checked = True
        'Me.RBOrganisationalabilities0.Checked = True
        'Me.RBInformaitionsharing0.Checked = True
        'Me.RBTeamwork0.Checked = True
    End Sub

    Private Sub g_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        clear()
    End Sub

    Private Sub TxtEmpNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtEmpNo.TextChanged
        Me.txtName.Text = ""
        Me.txtJob.Text = ""
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim dat As Integer
            Dim das As New DataSet
            Dim dap As New SqlDataAdapter("select * from StaffAppraisal where SNo=" & Me.DataGridView1.CurrentRow.Cells(0).Value, cnn)

            dap.Fill(das, "StaffAppraisal")

            Dim rpt As New StaffAppr
            rpt.SetDataSource(das)
            frmReportViewer.CrystalReportViewer1.ReportSource = rpt
            frmReportViewer.CrystalReportViewer1.RefreshReport()
            frmReportViewer.ShowDialog()



            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
            MsgBox(ex.ToString)
        End Try
    End Sub
End Class