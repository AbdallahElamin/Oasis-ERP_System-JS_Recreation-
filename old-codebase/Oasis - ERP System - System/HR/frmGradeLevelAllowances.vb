Imports System.Data.SqlClient

Public Class frmGradeLevelAllowances


    Private Sub Button31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button31.Click
        Dim cmd1 As New SqlCommand("select count(*) from GradeLevelAllowance where GradeLevel=N'" & Me.comboLevel.SelectedItem & "'", cnn)
        cnn.Open()
        If cmd1.ExecuteScalar > 0 Then



            Dim cmd2 As New SqlCommand("delete from GradeLevelAllowance  where GradeLevel=N'" & Me.comboLevel.SelectedItem & "'", cnn)
            cmd2.ExecuteNonQuery()
        End If
        cnn.Close()

        If Me.comboLevel.SelectedIndex = -1 Then
            MsgBox("please Choose a level from the list")

        Else
            Try
                Me.Cursor = Cursors.WaitCursor

                Dim cmd As New SqlCommand("insert into GradeLevelAllowance values (N'" & Me.comboLevel.SelectedItem & _
                                          "'," & CDbl(Me.txtCola.Text) & "," & CDbl(Me.txtAccommodation.Text) & "," & CDbl(Me.txtHospitality.Text) & _
                                          "," & CDbl(Me.txtTransportation.Text) & "," & CDbl(Me.txtOnCall.Text) & "," & CDbl(Me.txtMedical.Text) & _
                                          "," & CDbl(Me.txtMeal.Text) & "," & CDbl(Me.txtUniform.Text) & ") ", cnn)
                cnn.Open()
                cmd.ExecuteNonQuery()
                cnn.Close()
                MsgBox("Saved Successfully")
                clear()
                Me.Cursor = Cursors.Default

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                If cnn.State = ConnectionState.Open Then
                    cnn.Close()
                    MsgBox(ex.ToString)
                End If
            End Try
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim a As New frmAddLevel
        a.ShowDialog()

        FillComboLevel()
    End Sub
    Sub FillComboLevel()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select Distinct GradeLevel From Levels where GradeLevel is not null", cnn)
            Dim Reader As SqlDataReader

            Me.comboLevel.Items.Clear()

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Dim Item As New ComboBox
                Me.comboLevel.Items.Add(Reader.Item(0))
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

    Private Sub frmGradeLevelAllowances_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillComboLevel()
        ConvertToZero()
    End Sub
    Sub ConvertToZero()

        If Me.txtCola.Text.Trim = "" Then
            Me.txtCola.Text = 0

        End If
        If Me.txtAccommodation.Text.Trim = "" Then
            Me.txtAccommodation.Text = 0

        End If
        If Me.txtHospitality.Text.Trim = "" Then
            Me.txtHospitality.Text = 0

        End If
        If Me.txtTransportation.Text.Trim = "" Then
            Me.txtTransportation.Text = 0

        End If
        If Me.txtOnCall.Text.Trim = "" Then
            Me.txtOnCall.Text = 0

        End If
        If Me.txtMedical.Text.Trim = "" Then
            Me.txtMedical.Text = 0

        End If
        If Me.txtMeal.Text.Trim = "" Then
            Me.txtMeal.Text = 0

        End If
        If Me.txtUniform.Text.Trim = "" Then
            Me.txtUniform.Text = 0

        End If


    End Sub
    Sub clear()
        Me.comboLevel.SelectedIndex = -1
        Me.txtAccommodation.Text = 0.0
        Me.txtCola.Text = 0.0
        Me.txtHospitality.Text = 0.0
        Me.txtMeal.Text = 0.0
        Me.txtMedical.Text = 0.0
        Me.txtOnCall.Text = 0.0
        Me.txtTransportation.Text = 0.0
        Me.txtUniform.Text = 0.0


    End Sub

    Private Sub txtCola_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUniform.KeyPress, txtTransportation.KeyPress, txtOnCall.KeyPress, txtMedical.KeyPress, txtMeal.KeyPress, txtHospitality.KeyPress, txtCola.KeyPress, txtAccommodation.KeyPress
        If Char.IsDigit(e.KeyChar) _
       OrElse e.KeyChar = ","c _
           OrElse e.KeyChar = "."c OrElse _
          Char.GetUnicodeCategory(e.KeyChar) = Globalization.UnicodeCategory.Control OrElse _
                   Char.GetUnicodeCategory(e.KeyChar) _
               = Globalization.UnicodeCategory.CurrencySymbol _
                    OrElse Char.GetUnicodeCategory(e.KeyChar) = Globalization.UnicodeCategory.Format Then

            e.Handled = False

        Else
            e.Handled = True

        End If

    End Sub

    Private Sub txtCola_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCola.TextChanged
        If Me.txtCola.Text = "" Then
            Me.txtCola.Text = 0.0
        End If
    End Sub

    Private Sub txtAccommodation_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAccommodation.TextChanged
        If Me.txtAccommodation.Text = "" Then
            Me.txtAccommodation.Text = 0.0
        End If
    End Sub

    Private Sub txtHospitality_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHospitality.TextChanged
        If Me.txtHospitality.Text = "" Then
            Me.txtHospitality.Text = 0.0
        End If
    End Sub

    Private Sub txtTransportation_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTransportation.TextChanged
        If Me.txtTransportation.Text = "" Then
            Me.txtTransportation.Text = 0.0
        End If
    End Sub

    Private Sub txtOnCall_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOnCall.TextChanged
        If Me.txtOnCall.Text = "" Then
            Me.txtOnCall.Text = 0.0
        End If
    End Sub

    Private Sub txtMedical_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMedical.TextChanged
        If Me.txtMedical.Text = "" Then
            Me.txtMedical.Text = 0.0
        End If
    End Sub

    Private Sub txtMeal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMeal.TextChanged
        If Me.txtMeal.Text = "" Then
            Me.txtMeal.Text = 0.0
        End If
    End Sub

    Private Sub txtUniform_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUniform.TextChanged
        If Me.txtUniform.Text = "" Then
            Me.txtUniform.Text = 0.0
        End If
    End Sub

    Private Sub txtCola_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCola.Validated
        Me.txtCola.Text = CDbl(Me.txtCola.Text).ToString("N2")
    End Sub

    Private Sub txtAccommodation_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAccommodation.Validated
        Me.txtAccommodation.Text = CDbl(Me.txtAccommodation.Text).ToString("N2")
    End Sub

    Private Sub txtHospitality_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHospitality.Validated
        Me.txtHospitality.Text = CDbl(Me.txtHospitality.Text).ToString("N2")
    End Sub

    Private Sub txtTransportation_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTransportation.Validated
        Me.txtTransportation.Text = CDbl(Me.txtTransportation.Text).ToString("N2")
    End Sub

    Private Sub txtOnCall_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOnCall.Validated
        Me.txtOnCall.Text = CDbl(Me.txtOnCall.Text).ToString("N2")
    End Sub

    Private Sub txtMedical_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMedical.Validated
        Me.txtMedical.Text = CDbl(Me.txtMedical.Text).ToString("N2")
    End Sub

    Private Sub txtMeal_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMeal.Validated
        Me.txtMeal.Text = CDbl(Me.txtMeal.Text).ToString("N2")
    End Sub

    Private Sub txtUniform_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUniform.Validated
        Me.txtUniform.Text = CDbl(Me.txtUniform.Text).ToString("N2")
    End Sub

    Private Sub comboLevel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboLevel.SelectedIndexChanged
        Me.txtAccommodation.Text = 0.0
        Me.txtCola.Text = 0.0
        Me.txtHospitality.Text = 0.0
        Me.txtMeal.Text = 0.0
        Me.txtMedical.Text = 0.0
        Me.txtOnCall.Text = 0.0
        Me.txtTransportation.Text = 0.0
        Me.txtUniform.Text = 0.0

        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select isnull(Cola,0)cola,isnull(Accommodation,0) Accommodation," & _
                                      "isnull(Hospitality,0) Hospitality,isnull(Transport,0) transport," & _
                                      "isnull(OnCall,0) OnCall,isnull(Medical,0) Medical,isnull(Meal,0) Meal," & _
                                      "isnull(Uniform,0) Uniform From GradeLevelAllowance where GradeLevel=N'" & Me.comboLevel.SelectedItem & "'", cnn)
            Dim Reader As SqlDataReader
            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read

                Me.txtCola.Text = Reader.Item(0)
                Me.txtAccommodation.Text = Reader.Item(1)
                Me.txtHospitality.Text = Reader.Item(2)
                Me.txtTransportation.Text = Reader.Item(3)
                Me.txtOnCall.Text = Reader.Item(4)
                Me.txtMedical.Text = Reader.Item(5)
                Me.txtMeal.Text = Reader.Item(6)
                Me.txtUniform.Text = Reader.Item(7)
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

End Class