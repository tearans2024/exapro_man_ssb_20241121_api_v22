Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FSOARFPReport
    Public func_coll As New function_collection
    Dim _now As DateTime

    Private Sub FSOARFPReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_view1, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "SO Number", "so_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Effective Date", "so_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "Sold To", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Shipment / Return Number", "soship_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Shipment / Return Date", "soship_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "Is Shipment", "soship_is_shipment", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Exchange Rate", "so_exc_rate", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "UM", "um_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Reason Code", "reason_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Taxable", "sod_taxable", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Tax Include", "sod_tax_inc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Tax Class", "tax_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Qty", "soshipd_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_view1, "Price", "sod_price", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "Total Sales", "sales_ttl", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "Discount", "sod_disc", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "p")
        add_column_copy(gv_view1, "Discount Value", "disc_value", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "FP Price", "price_fp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "FP Disc. Value", "disc_fp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "Netto", "dpp", DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "Product Line", "pl_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "Vat Paid", "ppn_bayar", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "Vat Free", "ppn_bebas", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n", DevExpress.Data.SummaryItemType.Sum, "SUM={0:n}")
        add_column_copy(gv_view1, "AR Number", "ar_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "AR Date", "ar_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_view1, "FP Number", "fp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_view1, "FP Date", "fp_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        If xtc_master.SelectedTabPageIndex = 0 Then
                            .SQL = "SELECT    " _
                                & "  en_desc, " _
                                & "  so_code, " _
                                & "  so_date, " _
                                & "  ptnr_name, " _
                                & "  soship_code, " _
                                & "  soship_date, " _
                                & "  si_desc, " _
                                & "  soship_is_shipment, " _
                                & "  soshipd_seq, " _
                                & "  cu_name, " _
                                & "  so_exc_rate, " _
                                & "  pt_code, " _
                                & "  pt_desc1, " _
                                & "  pt_desc2, " _
                                & "  sod_taxable, " _
                                & "  sod_tax_inc, " _
                                & "  tax_mstr.code_name as tax_name, " _
                                & "  soshipd_qty * -1 as soshipd_qty, " _
                                & "  sod_price, " _
                                & "  soshipd_qty * -1 * sod_price as sales_ttl, " _
                                & "  sod_disc, " _
                                & "  soshipd_qty * -1 * sod_price * sod_disc as disc_value, " _
                                & "   " _
                                & "  case upper(sod_tax_inc) " _
                                & "  when 'N' then soshipd_qty * -1 * sod_price " _
                                & "  when 'Y' then (soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric (26,8)) " _
                                & "  end as price_fp, " _
                                & "   " _
                                & "  case upper(sod_tax_inc) " _
                                & "  when 'N' then soshipd_qty * -1 * sod_price * sod_disc " _
                                & "  when 'Y' then (soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric (26,8)) * sod_disc " _
                                & "  end as disc_fp, " _
                                & "   " _
                                & "  case upper(sod_tax_inc) " _
                                & "  when 'N' then (soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc) " _
                                & "  when 'Y' then ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric (26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric (26,8)) * sod_disc) " _
                                & "  end as dpp, " _
                                & "   " _
                                & "  pl_desc,  " _
                                & "   " _
                                & "  case pl_code " _
                                & "  when '990000000001' then " _
                                & "  					case upper(sod_tax_inc) " _
                                & "                    when 'N' then ((soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc)) * 0.1 " _
                                & "                    when 'Y' then ((((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric (26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric (26,8)) * sod_disc))) * 0.1 " _
                                & "                    end " _
                                & "  end as ppn_bayar, " _
                                & "   " _
                                & "  case pl_code " _
                                & "  when '990000000002' then " _
                                & "  					case upper(sod_tax_inc) " _
                                & "                    when 'N' then ((soshipd_qty * -1 * sod_price) - (soshipd_qty * -1 * sod_price * sod_disc)) * 0.1 " _
                                & "                    when 'Y' then ((((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric (26,8))) - ((soshipd_qty * -1 * sod_price) * cast((100.0/110.0) as numeric (26,8)) * sod_disc))) * 0.1  " _
                                & "                    end " _
                                & "  end as ppn_bebas, " _
                                & "               " _
                                & "  um_mstr.code_name as um_name, " _
                                & "  loc_desc, " _
                                & "  reason_mstr.code_name as reason_name, " _
                                & "  ar_code, " _
                                & "  ar_date, " _
                                & "  fp_code, " _
                                & "  fp_date " _
                                & "FROM  " _
                                & "  public.soship_mstr " _
                                & "  inner join soshipd_det on soshipd_soship_oid = soship_oid " _
                                & "  inner join so_mstr on so_oid = soship_so_oid " _
                                & "  inner join sod_det on sod_oid = soshipd_sod_oid " _
                                & "  inner join en_mstr on en_id = soship_en_id " _
                                & "  inner join si_mstr on si_id = soship_si_id " _
                                & "  inner join ptnr_mstr on ptnr_id = so_ptnr_id_sold " _
                                & "  inner join pt_mstr on pt_id = sod_pt_id " _
                                & "  inner join code_mstr as um_mstr on um_mstr.code_id = soshipd_um " _
                                & "  inner join loc_mstr on loc_id = soshipd_loc_id " _
                                & "  inner join cu_mstr on cu_id = so_cu_id " _
                                & "  inner join code_mstr as tax_mstr on tax_mstr.code_id = sod_tax_class " _
                                & "  left outer join code_mstr as reason_mstr on reason_mstr.code_id = soshipd_rea_code_id " _
                                & "  left outer join arso_so on arso_so_oid = so_oid " _
                                & "  left outer join ar_mstr on ar_oid = arso_ar_oid " _
                                & "  left outer join fpar_ar on fpar_ar_oid = ar_oid " _
                                & "  left outer join fp_mstr on fp_oid = fpar_fp_oid " _
                                & "  inner join pl_mstr on pl_id = pt_pl_id " _
                                & "  where so_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                                & "  and so_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                                & "  and so_en_id in (select user_en_id from tconfuserentity " _
                                                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
                            .InitializeCommand()
                            .FillDataSet(ds, "view1")
                            gc_view1.DataSource = ds.Tables("view1")
                        End If

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
