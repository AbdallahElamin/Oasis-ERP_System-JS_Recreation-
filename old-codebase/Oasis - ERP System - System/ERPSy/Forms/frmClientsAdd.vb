Imports System.Data.SqlClient

Public Class frmClientsAdd

    Sub Clear()
        Me.txtName.Clear()
        Me.txtLicNo.Clear()
        Me.txtTaxNo.Clear()
        Me.txtMobile.Clear()
        Me.CombClientClass.SelectedIndex = -1
        Me.CombAreaName.SelectedIndex = -1
        Me.combState.SelectedIndex = -1
        Me.CombRegion.SelectedIndex = -1
        Me.TxtCity.Clear()
        Me.txtTown.Clear()
        Me.txtDistrict.Clear()
        Me.txtStreet.Clear()
        Me.txtBuildingNo.Clear()
        Me.CombSalesMan.SelectedIndex = -1
        Me.CombMedRepresentative.SelectedIndex = -1
        Me.txtPharmacyOwner.Clear()
        Me.txtPharmacyDoctor.Clear()
        Me.txtName.Focus()
        Me.txtPharOwnMob.Clear()
        Me.txtPhrDrMob.Clear()
        Me.txtName.Focus()
    End Sub

    Private Sub frmClientsAdd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillStates()
        FillSalesMan()
        FillMedicalRepresentatives()
        FillClientClasses()
        Me.CombMedRepresentative.SelectedIndex = -1
        Me.CombSalesMan.SelectedIndex = -1
    End Sub

    Sub FillSalesMan()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim List As ArrayList = GetSalesManList()
            Me.CombSalesMan.Items.Clear()

            For i As Integer = 0 To List.Count - 1
                Me.CombSalesMan.Items.Add(List(i))
            Next

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub FillMedicalRepresentatives()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim List As ArrayList = GetMedicalRepresentativesList()
            Me.CombMedRepresentative.Items.Clear()

            For i As Integer = 0 To List.Count - 1
                Me.CombMedRepresentative.Items.Add(List(i))
            Next

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub FillStates()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim List As ArrayList = GetStatesList()
            Me.combState.Items.Clear()

            For i As Integer = 0 To List.Count - 1
                Me.combState.Items.Add(List(i))
            Next

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub FillAreaName()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim List As ArrayList = GetClientAreaList()
            Me.CombAreaName.Items.Clear()

            For i As Integer = 0 To List.Count - 1
                Me.CombAreaName.Items.Add(List(i))
            Next

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
        End Try
    End Sub

    Sub FillClientClasses()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim List As ArrayList = GetClientClassList()
            Me.CombClientClass.Items.Clear()

            For i As Integer = 0 To List.Count - 1
                Me.CombClientClass.Items.Add(List(i))
            Next

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub combState_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles combState.SelectedIndexChanged
        If Me.combState.SelectedIndex = -1 Then
            Me.CombRegion.Items.Clear()
            Me.CombRegion.Enabled = False
        Else
            Me.CombRegion.Enabled = True
            Try
                Me.Cursor = Cursors.WaitCursor
                Dim cmd As New SqlCommand("Select Distinct Region From Regions Where State=N'" & Me.combState.SelectedItem & _
                                           "' and Region Is Not Null Order By Region", cnn)
                Dim Reader As SqlDataReader

                Me.CombRegion.Items.Clear()

                cnn.Open()
                Reader = cmd.ExecuteReader
                While Reader.Read
                    Me.CombRegion.Items.Add(Reader.Item(0))
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
        End If
    End Sub

    Private Sub btnNar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNar.Click
        Dim a As New frmClientClassification
        a.ShowDialog()

        FillClientClasses()
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        ErrProv.Clear()
        Try
            If Me.txtName.Text.Trim.Length = 0 Then
                ErrProv.SetError(Me.txtName, "Please Fill In Name")

            ElseIf Me.txtLicNo.Text.Trim.Length = 0 Then
                ErrProv.SetError(Me.txtLicNo, "Please Fill In License Number")

            ElseIf Me.txtTaxNo.Text.Trim.Length = 0 Then
                ErrProv.SetError(Me.txtTaxNo, "Please Fill In Tax  Number")

            ElseIf Me.txtMobile.Text.Trim.Length = 0 Then
                ErrProv.SetError(Me.txtMobile, "Please Fill In Mobile  Number")

            ElseIf Me.CombClientClass.SelectedIndex = -1 Then
                ErrProv.SetError(Me.CombClientClass, "Please Fill In Client Classificationadd")

            ElseIf Me.combState.SelectedIndex = -1 Then
                ErrProv.SetError(Me.combState, "Please Fill In State ")

            ElseIf Me.CombRegion.SelectedIndex = -1 Then
                ErrProv.SetError(Me.CombRegion, "Please Fill In Region")

            ElseIf Me.CombAreaName.SelectedIndex = -1 Then
                ErrProv.SetError(Me.CombAreaName, "Please Fill In Area")

            ElseIf Me.TxtCity.Text.Trim.Length = 0 Then
                ErrProv.SetError(Me.TxtCity, "Please Fill In City")

            ElseIf Me.txtTown.Text.Trim.Length = 0 Then
                ErrProv.SetError(Me.txtTown, "Please Fill In Town")

            ElseIf Me.txtDistrict.Text.Trim.Length = 0 Then
                ErrProv.SetError(Me.txtDistrict, "Please Fill In District")

            ElseIf Me.txtStreet.Text.Trim.Length = 0 Then
                ErrProv.SetError(Me.txtStreet, "Please Fill In Street")

            ElseIf Me.txtBuildingNo.Text.Trim.Length = 0 Then
                ErrProv.SetError(Me.txtBuildingNo, "Please Fill In BuildingNo ")

            ElseIf Me.CombSalesMan.SelectedIndex = -1 Then
                ErrProv.SetError(Me.CombSalesMan, "Please Fill In Sales Man")

            ElseIf Me.CombMedRepresentative.SelectedIndex = -1 Then
                ErrProv.SetError(Me.CombMedRepresentative, "Please Fill In Medical Representatives")

            ElseIf Me.txtPharmacyOwner.Text.Trim.Length = 0 Then
                ErrProv.SetError(Me.txtPharmacyOwner, "Please Fill In Pharmacy Owner Name")

            ElseIf Me.txtPharOwnMob.Text.Trim.Length = 0 Then
                ErrProv.SetError(Me.txtPharOwnMob, "Please Fill In Pharmacy Owner Mobile")

            ElseIf Me.txtPharmacyDoctor.Text.Trim.Length = 0 Then
                ErrProv.SetError(Me.txtPharmacyDoctor, "Please Fill In Pharmacy Doctor Name")

            ElseIf Me.txtPhrDrMob.Text.Trim.Length = 0 Then
                ErrProv.SetError(Me.txtPhrDrMob, "Please Fill In Pharmacy Doctor Mobile")

            Else
                Me.Cursor = Cursors.WaitCursor

                Dim cmd As New SqlCommand()
                Dim Trans As SqlTransaction
                Dim ClientID As Integer

                cnn.Open()
                cmd.Connection = cnn
                Trans = cnn.BeginTransaction
                cmd.Transaction = Trans

                cmd.CommandText = "Insert Into Clients(Name,LicNo,Mobile,State,Region,City,Town,District,Street,BuildingNo,SalesMan,MedicalRepresentative," & _
                                  "TaxNo,ClientClass,Area,PharmacyOwner,PharmacyDoctor,PharmacyOwnerMob,PharmacyDoctorMob,UserName)" & _
                                  " Values (@Name,@LicNo,@Mobile,@State,@Region,@City,@Town,@District,@Street,@BuildingNo,@SalesMan,@MedicalRepresentative," & _
                                  "@TaxNo,@ClientClass,@Area,@PharmacyOwner,@PharmacyDoctor,@PharmacyOwnerMob,@PharmacyDoctorMob,@UserName) Select Scope_Identity()"

                cmd.Parameters.AddWithValue("@Name", Me.txtName.Text.Trim)
                cmd.Parameters.AddWithValue("@LicNo", Me.txtLicNo.Text.Trim)
                cmd.Parameters.AddWithValue("@TaxNo", Me.txtTaxNo.Text.Trim)
                cmd.Parameters.AddWithValue("@Mobile", Me.txtMobile.Text.Trim)
                cmd.Parameters.AddWithValue("@ClientClass", Me.CombClientClass.Text.Trim)
                cmd.Parameters.AddWithValue("@State", Me.combState.Text.Trim)
                cmd.Parameters.AddWithValue("@Region", Me.CombRegion.Text.Trim)
                cmd.Parameters.AddWithValue("@Area", Me.CombAreaName.Text.Trim)
                cmd.Parameters.AddWithValue("@City", Me.TxtCity.Text.Trim)
                cmd.Parameters.AddWithValue("@Town", Me.txtTown.Text.Trim)
                cmd.Parameters.AddWithValue("@District", Me.txtDistrict.Text.Trim)
                cmd.Parameters.AddWithValue("@Street", Me.txtStreet.Text.Trim)
                cmd.Parameters.AddWithValue("@BuildingNo", Me.txtBuildingNo.Text.Trim)
                cmd.Parameters.AddWithValue("@SalesMan", Me.CombSalesMan.Text.Trim)
                cmd.Parameters.AddWithValue("@MedicalRepresentative", Me.CombMedRepresentative.Text.Trim)
                cmd.Parameters.AddWithValue("@PharmacyOwner", Me.txtPharmacyOwner.Text.Trim)
                cmd.Parameters.AddWithValue("@PharmacyOwnerMob", Me.txtPharOwnMob.Text.Trim)
                cmd.Parameters.AddWithValue("@PharmacyDoctor", Me.txtPharmacyDoctor.Text.Trim)
                cmd.Parameters.AddWithValue("@PharmacyDoctorMob", Me.txtPhrDrMob.Text.Trim)
                cmd.Parameters.AddWithValue("@UserName", CurrentUser)
                ClientID = CInt(cmd.ExecuteScalar)

                'Open Account
                cmd.Parameters.Clear()
                cmd.CommandText = "Insert Into Accs (Acc1,Acc2,Acc3,Acc4) Values (N'Assets',N'Current Assets',N'Clients',@ClientName)"
                cmd.Parameters.AddWithValue("@ClientName", Me.txtName.Text.Trim)
                cmd.ExecuteNonQuery()

                Trans.Commit()
                cnn.Close()

                MsgBox("Saved Successfully" & Chr(13) & "Client ID: " & ClientID)

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

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Clear()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim a As New frmRegionsStatesArea
        a.ShowDialog()

        FillStates()
    End Sub

    Private Sub CombClientClass_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CombClientClass.SelectedIndexChanged

    End Sub

    Private Sub CombRegion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CombRegion.SelectedIndexChanged
        FillAreaName()
    End Sub
End Class
