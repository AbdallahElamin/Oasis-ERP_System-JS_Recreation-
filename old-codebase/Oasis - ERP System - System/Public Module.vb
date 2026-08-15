Imports System.Data.SqlClient

Module Public_Module
    'new
    '  Public strConn As String = "Data Source=premsql2e.brinkster.com;Initial Catalog=mohmmedali1;user id=mohmmedali1; password=messi19barca;"
    Public strConn As String = "data source=(local);initial catalog=OasisERP-Mahadi;integrated security=SSPI"
    Public cnn, cnn1, cnn2, cnn3, cnn4 As New SqlConnection(strConn)

    Public rptViewer As New ReportViewer
    Public SelCustIDNo As Integer
    Public SelCustName, ExpireDate, CurrentUser, CurrentUserID, PWD, SelClientID, SelClientName As String
    Public SelPatIDNo, EmpNo, SelPatName, Employee, Priv, SelFloor, SelBed, SelSupplier, SelSupplierEmail As String
    Public G_OK As Boolean
    Public Mainfrm As New frmMain
End Module