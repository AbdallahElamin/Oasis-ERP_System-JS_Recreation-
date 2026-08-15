Imports System.Data.SqlClient

Public Class frmMain

    Private Sub UsersManagementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UsersManagementToolStripMenuItem.Click
        Dim a As New frmUsersMang
        a.Show()
    End Sub

    Private Sub ChangePasswordToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangePasswordToolStripMenuItem.Click
        Dim a As New frmChangePassword
        a.ShowDialog()
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lblUser.Text = "Current User: " & CurrentUser
    End Sub

    Private Sub AddItemsToStockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddItemsToStockToolStripMenuItem.Click
        Dim a As New frmAddItemsToStock
        a.MdiParent = Me
        a.Show()
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        Dim a As New frmClientsRegistry
        a.MdiParent = Me
        a.Show()
    End Sub

    Private Sub frmMain_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        End
    End Sub

    Private Sub MakeInvoiceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MakeInvoiceToolStripMenuItem.Click
        Dim a As New frmInvoice
        a.MdiParent = Me
        a.Show()
    End Sub

    Private Sub ArchiveToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ArchiveToolStripMenuItem2.Click
        Dim a As New frmInvoicesArchive
        a.Show()
    End Sub

    Private Sub SalesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SalesToolStripMenuItem.Click
        Dim a As New frmSalesReports
        a.Show()
    End Sub

    Private Sub StockStatusToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockStatusToolStripMenuItem1.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim dap As New SqlDataAdapter("Select CompanyName,Item,BatchNo,Sum(QntIn)-Sum(QntOut) QntIn,dbo.GetItemGenericName(Item) GenericName," & _
                                          "dbo.GetItemPack(Item) Pack,dbo.GetItemWPrice(Item) WPrice,dbo.GetItemRPrice(Item) RPrice " & _
                                          "From Stock Group By CompanyName,Item,BatchNo", cnn)
            Dim das As New DataSet

            cnn.Open()
            dap.Fill(das, "Stock")
            cnn.Close()

            Dim rpt As New StockStatus
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

    Private Sub StockTransactionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StockTransactionsToolStripMenuItem.Click
        'Dim a As New frmStockTransactions
        'a.Show()
    End Sub

    Private Sub DisposeItemsFromStockToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisposeItemsFromStockToolStripMenuItem.Click
        Dim a As New frmDisposeItems
        a.MdiParent = Me
        a.Show()
    End Sub

    Private Sub MakeQuotationToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MakeQuotationToolStripMenuItem.Click
        Dim a As New frmQuotation
        a.MdiParent = Me
        a.Show()
    End Sub

    Private Sub ArchiveToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ArchiveToolStripMenuItem3.Click
        Dim a As New frmQuotationArchive
        a.Show()
    End Sub

    Private Sub AdminSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AdminSToolStripMenuItem.Click
        
    End Sub

    Private Sub LegateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LegateToolStripMenuItem.Click
        Dim a As New frmAdminMedicalRepresentatives
        a.MdiParent = Me
        a.Show()
    End Sub

    Private Sub LegateToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LegateToolStripMenuItem1.Click
        Dim a As New frmAdminSalesMan
        a.MdiParent = Me
        a.Show()
    End Sub

    Private Sub ReturnInvoiceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReturnInvoiceToolStripMenuItem.Click
        Dim a As New FrmReturndInvoice
        a.MdiParent = Me
        a.Show()
    End Sub

    Private Sub FileToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FileToolStripMenuItem.Click

    End Sub

    Private Sub PrintListToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PrintListToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim dap As New SqlDataAdapter("Select * From ItemsRegistry ", cnn)

            Dim das As New DataSet

            dap.Fill(das, "ItemsRegistry")

            Dim rpt As New ItemsRegistry
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

    Private Sub ItemsRegistryToolStripMenuItem1_Click(sender As System.Object, e As System.EventArgs) Handles ItemsRegistryToolStripMenuItem1.Click
        Dim a As New frmItemsRegistry
        a.MdiParent = Me
        a.Show()
    End Sub

    Private Sub ChartOfAccountsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChartOfAccountsToolStripMenuItem.Click
        Dim a As New frmChartofAccounts
        a.MdiParent = Me
        a.Show()
    End Sub

    Private Sub MakeVoucherToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MakeVoucherToolStripMenuItem.Click
        Dim a As New frmMakeVoucher
        a.MdiParent = Me
        a.Show()
    End Sub

    Private Sub MakePayVouchersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MakePayVouchersToolStripMenuItem.Click
        Dim a As New frmMakePayBill
        a.MdiParent = Me
        a.Show()
    End Sub

    Private Sub MakeReceiptVouchersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MakeReceiptVouchersToolStripMenuItem.Click
        Dim a As New frmMakeGetBill
        a.MdiParent = Me
        a.Show()
    End Sub

    Private Sub ApprovingVouchersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApprovingVouchersToolStripMenuItem.Click
        Dim a As New frmApprovingVouchers
        a.MdiParent = Me
        a.Show()
    End Sub

    Private Sub VoucherReverseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VoucherReverseToolStripMenuItem.Click
        Dim a As New frmVoucherReverse
        a.MdiParent = Me
        a.Show()
    End Sub

    Private Sub JournalRegisterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JournalRegisterToolStripMenuItem.Click
        Dim a As New frmVouchersList
        a.Show()
    End Sub

    Private Sub TrialBalanceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrialBalanceToolStripMenuItem.Click
        Dim a As New frmTrialBalance
        a.Show()
    End Sub

    Private Sub AccountsStatementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AccountsStatementToolStripMenuItem.Click
        Dim a As New frmStatement
        a.Show()
    End Sub

    Private Sub BalancesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BalancesToolStripMenuItem.Click
        Dim a As New frmBalanceSheetLevels
        a.Show()
    End Sub

    Private Sub PayReceiptVoucherToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PayReceiptVoucherToolStripMenuItem.Click
        Dim a As New frmBillsArchive
        a.MdiParent = Me
        a.Show()
    End Sub

    Private Sub ChequeManagementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChequeManagementToolStripMenuItem.Click
        Dim a As New frmCheqClearingSystem
        a.MdiParent = Me
        a.Show()
    End Sub

    Private Sub PayReceiptVouchersArchiveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PayReceiptVouchersArchiveToolStripMenuItem.Click
        Dim a As New frmBillsArchive
        a.MdiParent = Me
        a.Show()
    End Sub

    Private Sub BudgetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BudgetToolStripMenuItem.Click
        Dim a As New frmBudget
        a.MdiParent = Me
        a.Show()
    End Sub

    Private Sub FixedAssetsManagementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim a As New frmFixedAssets
        a.Show()
    End Sub

    Private Sub ToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem4.Click
        Dim a As New frmTransferItem
        a.MdiParent = Me
        a.Show()
    End Sub

    Private Sub المناطقToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles المناطقToolStripMenuItem.Click
        Dim a As New frmRegionsStatesArea
        a.Show()
    End Sub

    Private Sub StorstatusToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StorstatusToolStripMenuItem.Click
        Dim a As New frmStorstatus
        a.Show()
    End Sub

    Private Sub JobProfileToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles JobProfileToolStripMenuItem.Click
        Dim a As New frmHR
        a.show()
    End Sub
End Class
