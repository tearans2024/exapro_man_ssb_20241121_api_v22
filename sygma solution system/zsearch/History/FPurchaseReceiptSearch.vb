﻿Imports master_new.ModFunction

Public Class FPurchaseReceiptSearch

    Public _row As Integer
    Public _en_id As Integer
    Public _po_code As String
    Public _obj As Object
    Dim func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FPurchaseReceiptSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
        pr_txttglawal.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Receive Number", "rcv_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Receive Date", "rcv_date", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        'add_column(gv_master, "Qty Open", "qty_open", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n")
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        get_sequel = "SELECT  " _
                        & "  rcv_code, " _
                        & "  rcv_date " _
                        & "FROM  " _
                        & "  public.rcv_mstr " _
                        & "  where rcv_is_receive ~~* 'Y' " _
                        & "  and rcv_en_id = " & _en_id.ToString _
                        & " and rcv_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                        & " and rcv_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                        & "  order by rcv_code "
        Return get_sequel
    End Function

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            Me.Close()
        End If
    End Sub

    Private Sub fill_data()
        If fobject.name = "FPurchaseReceiptPrint" Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("rcv_code")
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub
End Class
