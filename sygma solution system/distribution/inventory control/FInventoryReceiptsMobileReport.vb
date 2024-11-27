Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FInventoryReceiptsMobileReport
    Public func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FInventoryReceiptsMobileReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_detail_receipt, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receipt, "Receipt Code", "rium_type2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receipt, "Receipt Date", "riumd_dt", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receipt, "Receipt By", "rium_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receipt, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receipt, "Pt Desc1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receipt, "Qty Receipt", "riumd_qty", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receipt, "UM", "riumd_um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receipt, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receipt, "Remarks", "rium_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_receipt, "Inv Type", "rium_type", DevExpress.Utils.HorzAlignment.Default)

    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.CustomCommand
                    With objload
                        .SQL = "SELECT " _
                    & "  public.en_mstr.en_desc, " _
                    & "  public.riumd_det.riumd_dt, " _
                    & "  public.rium_mstr.rium_type2, " _
                    & "  public.rium_mstr.rium_add_by, " _
                    & "  public.riumd_det.riumd_pt_id, " _
                    & "  public.pt_mstr.pt_desc1, public.pt_mstr.pt_code, " _
                    & "  public.riumd_det.riumd_qty, " _
                    & "  code_name as riumd_um_name, " _
                    & "  public.loc_mstr.loc_desc, " _
                    & "  public.rium_mstr.rium_remarks, " _
                     & "  public.rium_mstr.rium_type " _
                    & "  FROM " _
                    & "  public.riumd_det " _
                    & "  INNER JOIN public.rium_mstr ON (public.riumd_det.riumd_rium_oid = public.rium_mstr.rium_oid) " _
                    & "  INNER JOIN public.loc_mstr ON (public.riumd_det.riumd_loc_id = public.loc_mstr.loc_id) " _
                    & "  INNER JOIN public.pt_mstr ON (public.riumd_det.riumd_pt_id = public.pt_mstr.pt_id) " _
                    & "  INNER JOIN public.code_mstr ON (public.riumd_det.riumd_um = public.code_mstr.code_id) " _
                    & "  LEFT OUTER JOIN public.en_mstr ON (public.rium_mstr.rium_en_id = public.en_mstr.en_id)" _
                    & "  where rium_mstr.rium_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and rium_mstr.rium_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and rium_mstr.rium_type ~~* 'R'" _
                    & "  and rium_en_id in (select user_en_id from tconfuserentity " _
                    & "  where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                    & "  order by  public.riumd_det.riumd_dt ASC "

                        .InitializeCommand()
                        .FillDataSet(ds, "view1")
                        gc_view1.DataSource = ds.Tables("view1")

                        bestfit_column()
                        ConditionsAdjustment()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub
End Class
