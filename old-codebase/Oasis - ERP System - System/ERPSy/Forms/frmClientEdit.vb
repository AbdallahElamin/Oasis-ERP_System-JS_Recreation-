Imports System.Data.SqlClient

Public Class frmClientEdit

    Public SNo As Integer

    Sub FillClientDetails()
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim cmd As New SqlCommand("Select * From Clients Where SNo=" & SNo, cnn)
            Dim Reader As SqlDataReader

            cnn.Open()
            Reader = cmd.ExecuteReader
            While Reader.Read
                Me.txtName.Text = Reader.Item("Name")
                Me.txtLicNo.Text = Reader.Item("LicNo")
                Me.txtTaxNo.Text = Reader.Item("TaxNo")
                Me.txtMobile.Text = Reader.Item("Mobile")
                Me.CombClientClass.Text = Reader.Item("ClientClass")
                Me.combState.Text = Reader.Item("State")
                Me.CombRegion.Text = Reader.Item("Region")
                Me.CombAreaName.Text = Reader.Item("Area")
                Me.TxtCity.Text = Reader.Item("City")
                Me.txtTown.Text = Reader.Item("Town")
                Me.txtDistrict.Text = Reader.Item("District")
                Me.txtStreet.Text = Reader.Item("Street")
                Me.txtBuildingNo.Text = Reader.Item("BuildingNo")
                Me.CombSalesMan.Text = Reader.Item("SalesMan")
                Me.CombMedRepresentative.Text = Reader.Item("MedicalRepresentative")
                Me.txtPharmacyOwner.Text = Reader.Item("PharmacyOwner")
                Me.txtPharOwnMob.Text = Reader.Item("PharmacyOwnerMob")
                Me.txtPharmacyDoctor.Text = Reader.Item("PharmacyDoctor")
                Me.txtPhrDrMob.Text = Reader.Item("PharmacyDoctorMob")
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

            ElseIf Me.CombClientClass.Text.Trim.Length = 0 Then
                ErrProv.SetError(Me.CombClientClass, "Please Fill In Client Classificationadd")

            ElseIf Me.combState.Text.Trim.Length = 0 Then
                ErrProv.SetError(Me.combState, "Please Fill In State ")

            ElseIf Me.CombRegion.Text.Trim.Length = 0 Then
                ErrProv.SetError(Me.CombRegion, "Please Fill In Region")

            ElseIf Me.CombAreaName.Text.Trim.Length = 0 Then
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

            ElseIf Me.CombSalesMan.Text.Trim.Length = 0 Then
                ErrProv.SetError(Me.CombSalesMan, "Please Fill In Sales Man")

            ElseIf Me.CombMedRepresentative.Text.Trim.Length = 0 Then
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

                cnn.Open()
                cmd.Connection = cnn
                Trans = cnn.BeginTransaction
                cmd.Transaction = Trans

                cmd.CommandText = "Update Clients Set " & _
                                  "Name=@Name,LicNo=@LicNo,Mobile=@Mobile,State=@State,Region=@Region,Area=@Area,City=@City,Town=@Town,District=@District,Street=@Street," & _
                                  "BuildingNo=@BuildingNo,SalesMan=@SalesMan,MedicalRepresentative=@MedicalRepresentative,TaxNo=@TaxNo," & _
                                  "ClientClass=@ClientClass,PharmacyOwner=@PharmacyOwner,PharmacyDoctor=@PharmacyDoctor," & _
                                  "PharmacyOwnerMob=@PharmacyOwnerMob,PharmacyDoctorMob=@PharmacyDoctorMob Where SNo=" & SNo

                cmd.Parameters.Clear()
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
                cmd.ExecuteNonQuery()

                Trans.Commit()
                cnn.Close()

                Me.Cursor = Cursors.Default

                MsgBox("Updated Successfully")

                Me.Close()
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.ToString)
            If cnn.State = ConnectionState.Open Then
                cnn.Close()
            End If
        End Try
    End Sub

    Private Sub frmClientEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillClientDetails()


        FillClientClasses()
        FillStates()
        FillSalesMan()
        FillMedicalRepresentatives()
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
                                           "' and Region Is Not Null Order By Region", cnn1)
                Dim Reader As SqlDataReader

                Me.CombRegion.Items.Clear()

                cnn1.Open()
                Reader = cmd.ExecuteReader
                While Reader.Read
                    Me.CombRegion.Items.Add(Reader.Item(0))
                End While
                cnn1.Close()

                Me.Cursor = Cursors.Default
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                If cnn1.State = ConnectionState.Open Then
                    cnn1.Close()
                End If
                MsgBox(ex.ToString)
            End Try
        End If
    End Sub

    Private Sub CombRegion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CombRegion.SelectedIndexChanged
        FillAreaName()
    End Sub

    Private Sub btnNar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNar.Click
        Dim a As New frmClientClassification
        a.ShowDialog()

        FillClientClasses()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim a As New frmRegionsStatesArea
        a.ShowDialog()

        FillStates()
    End Sub
End Class