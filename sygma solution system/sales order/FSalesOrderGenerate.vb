Imports master_new.PGSqlConn
Imports master_new.ModFunction
Imports CoreLab.PostgreSql


Public Class FSalesOrderGenerate
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim func_bill As New Cls_Bilangan
    Dim sSQL As String
    Dim dt_edit As New DataTable
    Dim dt_sodas As New DataTable

    Private Sub FSalesOrderShipmentPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        de_start.EditValue = CekTanggal()
        de_end.EditValue = CekTanggal()

    End Sub

    

    Private Sub BtGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtGenerate.Click
        Try
            Dim sSQLs As New ArrayList

            sSQL = "SELECT  " _
                & "  a.id, " _
                & "  a.pricelist_id, " _
                & "  a.invoice, " _
                & "  a.user_id, " _
                & "  a.meja_id, " _
                & "  a.nama_pelanggan, " _
                & "  a.status, " _
                & "  a.bayar, " _
                & "  a.created_at, " _
                & "  a.updated_at, " _
                & "  a.karyawan_id, " _
                & "  a.status_transaksi, " _
                & "  a.shift_id, " _
                & "  a.tanggal_transaksi, " _
                & "  a.pay_type_id, " _
                & "  c.code_code, " _
                & "  c.code_name, " _
                & "  a.en_id, " _
                & "  d.en_code, " _
                & "  d.en_desc, " _
                & "  a.so_number,total_bayar,b.ptnr_id,a.bk_id,a.loc_id, " _
                & " c.code_usr_1 " _
                & "FROM " _
                & "  public.pos_transaksi a " _
                & "  INNER JOIN public.pi_mstr e ON (a.pricelist_id = e.pi_id) " _
                & "  INNER JOIN public.code_mstr c ON (a.pay_type_id = c.code_id) " _
                & "  INNER JOIN public.en_mstr d ON (a.en_id = d.en_id) " _
                 & "  INNER JOIN public.users b ON (a.user_id = b.id) " _
                & "WHERE " _
                & "  a.so_number IS NULL and status_batal='N' and coalesce(a.status,'')='selesai' and  a.bk_id is not null and a.loc_id is not null and a.tanggal_transaksi between " & SetDateNTime00(de_start.DateTime) & " and " & SetDateNTime00(de_end.DateTime) _
                & " ORDER BY " _
                & "  a.id"

            Dim dt_transaksi_master As New DataTable
            dt_transaksi_master = GetTableData(sSQL)

            sSQL = "select " _
                & "  sodas_oid, " _
                & "  sodas_so_oid,'' as pt_code, " _
                & "  sodas_pt_id_sod, " _
                & "  sodas_pt_id, " _
                & "  sodas_qty, " _
                & "  sodas_sod_oid, " _
                & "  sodas_qty_sold from sodas_assembly where sodas_oid is null"


            dt_sodas = GetTableData(sSQL)

            dt_sodas.Rows.Clear()

            Dim dt_transaksi_detail As New DataTable
            Dim y As Integer = 1


            Try
                Using objinsert As New master_new.CustomCommand
                    With objinsert
.Command.Open()
                        ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            '.Command = .Connection.CreateCommand
                            '.Command.Transaction = sqlTran


                            For Each dr_tr As DataRow In dt_transaksi_master.Rows

                                Dim _so_oid As String
                                _so_oid = Guid.NewGuid.ToString

                                Dim _so_code, _so_terbilang As String
                                Dim _so_total, _so_total_ppn, _so_total_pph, _sod_qty, _sod_price, _sod_disc, _so_total_temp, _tax_rate As Double
                                Dim i As Integer

                                _so_code = func_coll.get_transaction_number("SO", dr_tr("en_code"), "so_mstr", "so_code")

                                Dim _so_trn_status As String
                                Dim ds_bantu As New DataSet

                                If dr_tr("code_usr_1") = 0 Then
                                    _so_trn_status = "C" 'Lansung Default Ke Close
                                Else
                                    _so_trn_status = "D" 'Lansung Default Ke Draft
                                End If

                                Dim _id_ptnr_so As String
                                If dr_tr("code_usr_1") = 0 Then
                                    _id_ptnr_so = GetRowInfo("select value from pos_konf where var='pelanggan_id'")(0)
                                Else
                                    '_id_ptnr_so = dr_tr("karyawan_id")
                                    _id_ptnr_so = GetRowInfo("select value from pos_konf where var='pelanggan_id'")(0)
                                End If

                                Dim so_credit_term As String = GetRowInfo("select value from pos_konf where var='so_credit_term'")(0)
                                Dim so_tax_class As String = GetRowInfo("select value from pos_konf where var='so_tax_class'")(0)
                                Dim so_ppn_type As String = GetRowInfo("select value from pos_konf where var='so_ppn_type'")(0)
                                Dim so_si_id As String = GetRowInfo("select value from pos_konf where var='so_si_id'")(0)
                                Dim so_pay_type As String '= GetRowInfo("select value from pos_konf where var='so_pay_type'")(0)
                                Dim so_pay_method As String = GetRowInfo("select value from pos_konf where var='so_pay_method'")(0)
                                Dim so_ar_ac_id As String = GetRowInfo("select value from pos_konf where var='so_ar_ac_id'")(0)

                                Dim so_cu_id As String = GetRowInfo("select value from pos_konf where var='so_cu_id'")(0)
                                Dim so_bk_id As String = SetString(dr_tr("bk_id")) 'GetRowInfo("select value from pos_konf where var='so_bk_id'")(0)
                                Dim sod_loc_id As String = SetString(dr_tr("loc_id")) 'GetRowInfo("select value from pos_konf where var='sod_loc_id'")(0)

                                LabelControl2.Text = y & " of " & dt_transaksi_master.Rows.Count - 1 & " ID : " & dr_tr("id") & ", entity : " & dr_tr("en_desc") & " loc id : " & dr_tr("loc_id")
                                System.Windows.Forms.Application.DoEvents()
                                y = y + 1


                                so_pay_type = dr_tr("pay_type_id")
                                Dim so_sales_person As String = ""

                                If dr_tr("code_usr_1") = 0 Then

                                    sSQL = "SELECT  " _
                                        & "  b.ptnr_id, " _
                                        & "  a.user_id, " _
                                        & "  a.total_bayar, " _
                                        & "  a.shift_id " _
                                        & "FROM " _
                                        & "  public.pos_pembayaran a " _
                                        & "  INNER JOIN public.users b ON (a.user_id = b.id) " _
                                        & "WHERE " _
                                        & "  a.transaksi_id =" & dr_tr("id")


                                    If GetRowInfo(sSQL) Is Nothing Then
                                        sSQLs.Clear()
                                        Continue For
                                    End If
                                    so_sales_person = SetNumber(GetRowInfo(sSQL)(0))
                                    If so_sales_person = "0" Then
                                        Box("Sales Person error ")
                                        Exit Sub
                                    End If
                                    _so_total = SetNumber(GetRowInfo(sSQL)("total_bayar"))

                                Else
                                    so_sales_person = dr_tr("ptnr_id")
                                    _so_total = SetNumber(dr_tr("total_bayar"))
                                End If

                                _so_terbilang = func_bill.TERBILANG_FIX(_so_total)

                                '.Command.ExecuteNonQuery()
                                ''.Command.Parameters.Clear()

                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                        & "  public.so_mstr " _
                                        & "( " _
                                        & "  so_oid, " _
                                        & "  so_dom_id, " _
                                        & "  so_en_id, " _
                                        & "  so_add_by, " _
                                        & "  so_add_date, " _
                                        & "  so_code, " _
                                        & "  so_ptnr_id_sold, " _
                                        & "  so_ptnr_id_bill, " _
                                        & "  so_ref_po_code, " _
                                        & "  so_ref_po_oid, " _
                                        & "  so_date, " _
                                        & "  so_credit_term, " _
                                        & "  so_taxable, " _
                                        & "  so_tax_class, " _
                                        & "  so_ppn_type, " _
                                        & "  so_si_id, " _
                                        & "  so_type, " _
                                        & "  so_sales_person, " _
                                        & "  so_pi_id, " _
                                        & "  so_pay_type, " _
                                        & "  so_pay_method, " _
                                        & "  so_cons, " _
                                        & "  so_ar_ac_id, " _
                                        & "  so_ar_sb_id, " _
                                        & "  so_ar_cc_id, " _
                                        & "  so_dp, " _
                                        & "  so_disc_header, " _
                                        & "  so_total, " _
                                        & "  so_payment_date, " _
                                        & "  so_tran_id, " _
                                        & "  so_trans_id, " _
                                        & "  so_trans_rmks, " _
                                        & "  so_dt, " _
                                        & "  so_cu_id, " _
                                        & "  so_bk_id, " _
                                        & "  so_total_ppn, " _
                                        & "  so_total_pph, " _
                                        & "  so_payment, " _
                                        & "  so_exc_rate, " _
                                        & "  so_tax_inc,so_manufacture, " _
                                        & "  so_interval,so_is_package,so_pt_id,so_price, " _
                                        & "  so_terbilang " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(_so_oid) & ",  " _
                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetInteger(dr_tr("en_id")) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                        & SetSetring(_so_code) & ",  " _
                                        & SetInteger(_id_ptnr_so) & ",  " _
                                        & SetInteger(_id_ptnr_so) & ",  " _
                                        & SetSetring("") & ",  " _
                                        & SetSetring("") & ",  " _
                                        & SetDate(dr_tr("tanggal_transaksi")) & ",  " _
                                        & SetInteger(so_credit_term) & ",  " _
                                        & SetBitYN(False) & ",  " _
                                        & SetInteger(so_tax_class) & ",  " _
                                        & SetSetring(so_ppn_type) & ",  " _
                                        & SetInteger(so_si_id) & ",  " _
                                        & SetSetring("R") & ",  " _
                                        & SetInteger(so_sales_person) & ",  " _
                                        & SetInteger(dr_tr("pricelist_id")) & ",  " _
                                        & SetInteger(so_pay_type) & ",  " _
                                        & SetInteger(so_pay_method) & ",  " _
                                        & SetBitYN(False) & ",  " _
                                        & SetInteger(so_ar_ac_id) & ",  " _
                                        & SetInteger("0") & ",  " _
                                        & SetInteger("0") & ",  " _
                                        & SetDbl(0) & ",  " _
                                        & SetDbl(0) & ",  " _
                                        & SetDbl(_so_total) & ",  " _
                                        & SetDate("") & ",  " _
                                        & SetInteger("") & ",  " _
                                        & SetSetring(_so_trn_status) & ",  " _
                                        & SetSetring("SO Generate invoice id : " & dr_tr("invoice")) & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                        & SetInteger(so_cu_id) & ",  " _
                                        & SetInteger(so_bk_id) & ",  " _
                                        & SetDbl(_so_total_ppn) & ",  " _
                                        & SetDbl(_so_total_pph) & ",  " _
                                        & SetDbl(0) & ",  " _
                                        & SetDbl(1) & ",  " _
                                        & SetBitYN(False) & ",  " _
                                        & SetBitYN(False) & ",  " _
                                        & SetInteger(0) & ",  " _
                                        & SetBitYN(False) & ",  " _
                                        & SetInteger("") & ",  " _
                                        & SetDec(0) & ",  " _
                                        & SetSetring(_so_terbilang) & "  " _
                                        & ")"

                                sSQLs.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()





                                sSQL = "SELECT  " _
                                        & "  sod_oid, " _
                                        & "  sod_dom_id, " _
                                        & "  sod_en_id, " _
                                        & "  sod_add_by, " _
                                        & "  sod_add_date, " _
                                        & "  sod_upd_by, " _
                                        & "  sod_upd_date, " _
                                        & "  sod_so_oid, " _
                                        & "  sod_seq, " _
                                        & "  sod_is_additional_charge, " _
                                        & "  sod_si_id, " _
                                        & "  sod_pt_id, " _
                                        & "  sod_rmks, " _
                                        & "  sod_qty, " _
                                        & "  sod_qty_allocated, " _
                                        & "  sod_qty_picked, " _
                                        & "  sod_qty_shipment, " _
                                        & "  sod_qty_pending_inv, " _
                                        & "  sod_qty_invoice, " _
                                        & "  sod_um, " _
                                        & "  sod_cost, " _
                                        & "  sod_price, " _
                                        & "  sod_disc, " _
                                        & "  sod_sales_ac_id, " _
                                        & "  sod_sales_sb_id, " _
                                        & "  sod_sales_cc_id, " _
                                        & "  sod_disc_ac_id, " _
                                        & "  sod_um_conv, " _
                                        & "  sod_qty_real, " _
                                        & "  sod_taxable, " _
                                        & "  sod_tax_inc, " _
                                        & "  sod_tax_class, " _
                                        & "  sod_ppn_type, " _
                                        & "  sod_status, " _
                                        & "  sod_dt, " _
                                        & "  sod_payment, " _
                                        & "  sod_dp, " _
                                        & "  sod_sales_unit, sod_commision, sod_commision_total, " _
                                        & "  sod_loc_id, " _
                                        & "  sod_sales_unit_total,0.0 as sod_price_ori_aft_disc_aft_tax_ext,0.0 as sod_price_ori_aft_disc_before_tax_ext,0.0 as sod_price_net, " _
                                        & "  sod_serial, " _
                                        & "  en_desc, " _
                                        & "  si_desc, " _
                                        & "  pt_code, " _
                                        & "  pt_desc1, " _
                                        & "  pt_desc2, " _
                                        & "  pt_type, " _
                                        & "  pt_ls, " _
                                        & "  um_mstr.code_name as um_name, " _
                                        & "  ac_mstr_sales.ac_code as ac_code_sales, " _
                                        & "  ac_mstr_sales.ac_name as ac_name_sales, " _
                                        & "  sb_desc, " _
                                        & "  cc_desc, " _
                                        & "  ac_mstr_disc.ac_code as ac_code_disc, " _
                                        & "  ac_mstr_disc.ac_name as ac_name_disc, " _
                                        & "  tax_class.code_name as sod_tax_class_name, " _
                                        & "  loc_desc, " _
                                        & "  sod_pod_oid,sod_sqd_oid " _
                                        & "FROM  " _
                                        & "  public.sod_det " _
                                        & "  inner join so_mstr on so_oid = sod_so_oid " _
                                        & "  inner join en_mstr on en_id = sod_en_id " _
                                        & "  inner join si_mstr on si_id = sod_si_id " _
                                        & "  inner join pt_mstr on pt_id = sod_pt_id " _
                                        & "  inner join code_mstr um_mstr on um_mstr.code_id = sod_um	 " _
                                        & "  inner join ac_mstr ac_mstr_sales on ac_mstr_sales.ac_id = sod_sales_ac_id " _
                                        & "  inner join sb_mstr sb_mstr_sales on sb_mstr_sales.sb_id = sod_sales_sb_id " _
                                        & "  inner join cc_mstr cc_mstr_sales on cc_mstr_sales.cc_id = sod_sales_cc_id " _
                                        & "  left outer join ac_mstr ac_mstr_disc on ac_mstr_disc.ac_id = sod_sales_ac_id " _
                                        & "  inner join code_mstr tax_class on tax_class.code_id = sod_tax_class	 " _
                                        & "  left outer join loc_mstr on loc_id = sod_loc_id" _
                                        & " where sod_det.sod_seq = -99"


                                dt_edit = GetTableData(sSQL)

                                dt_edit.Rows.Clear()


                                sSQL = "SELECT  " _
                                       & "  a.id, " _
                                       & "  a.pricelist_id, " _
                                       & "  a.invoice, " _
                                       & "  a.user_id, " _
                                       & "  a.meja_id, " _
                                       & "  a.nama_pelanggan, " _
                                       & "  a.status, " _
                                       & "  a.bayar, " _
                                       & "  a.created_at, " _
                                       & "  a.updated_at, " _
                                       & "  a.karyawan_id, " _
                                       & "  a.status_transaksi, " _
                                       & "  a.shift_id, " _
                                       & "  a.tanggal_transaksi, " _
                                       & "  a.pay_type_id, " _
                                       & "  c.code_code, " _
                                       & "  c.code_name, " _
                                       & "  a.en_id, " _
                                       & "  d.en_code, " _
                                       & "  d.en_desc, " _
                                       & "  a.so_number, " _
                                       & "  b.id, " _
                                       & "  b.barang_id, " _
                                       & "  b.kode_barang, " _
                                       & "  b.nama_barang, " _
                                       & "  b.harga_barang, " _
                                       & "  b.qty, " _
                                       & "  b.total_harga, " _
                                       & "  f.pt_id, " _
                                       & "  f.pt_code, " _
                                       & "  f.pt_desc1, " _
                                       & "  f.pt_desc2, " _
                                       & "  f.pt_um, " _
                                       & "  f.pt_pl_id, " _
                                       & "  f.pt_type, " _
                                       & "  f.pt_its_id, " _
                                       & "  f.pt_taxable, " _
                                       & "  f.pt_ls, " _
                                       & "  f.pt_class, " _
                                       & "  f.pt_ppn_type, " _
                                       & "  f.pt_tax_class, " _
                                       & "  f.pt_si_id, " _
                                       & "  f.pt_tax_inc, " _
                                       & " c.code_usr_1 " _
                                       & "FROM " _
                                       & "  public.pos_transaksi a " _
                                       & "  INNER JOIN public.pos_detail_transaksi b ON (a.id = b.transaksi_id) " _
                                       & "  INNER JOIN public.pi_mstr e ON (a.pricelist_id = e.pi_id) " _
                                       & "  INNER JOIN public.code_mstr c ON (a.pay_type_id = c.code_id) " _
                                       & "  INNER JOIN public.en_mstr d ON (a.en_id = d.en_id) " _
                                       & "  INNER JOIN public.pt_mstr f ON (b.barang_id = cast(f.pt_id as varchar)) " _
                                       & "WHERE " _
                                       & "  a.id= " & SetInteger(dr_tr("id")) _
                                       & " ORDER BY " _
                                       & "  a.id"

                                dt_transaksi_detail = GetTableData(sSQL)



                                For Each dr_det As DataRow In dt_transaksi_detail.Rows

                                    Dim _row As DataRow = dt_edit.NewRow

                                    _row("sod_oid") = Guid.NewGuid.ToString
                                    _row("sod_dom_id") = 1
                                    _row("sod_en_id") = dr_tr("en_id")
                                    _row("sod_so_oid") = _so_oid
                                    _row("sod_si_id") = dr_det("pt_si_id")

                                    _row("sod_pt_id") = dr_det("pt_id")
                                    _row("sod_qty") = dr_det("qty")

                                    _row("sod_um") = dr_det("pt_um")

                                    Dim _cost As Double
                                    _cost = SetNumber(GetRowInfo("select invct_cost from invct_table where invct_pt_id=" & dr_det("pt_id") & " limit 1")(0))

                                    _row("sod_cost") = _cost
                                    _row("sod_price") = dr_det("harga_barang")

                                    _row("sod_disc") = 0.0
                                    _row("pt_code") = dr_det("pt_code")


                                    Try
                                        Using objcb As New master_new.CustomCommand
                                            With objcb
                                                .SQL = "select pla_ac_id, ac_code, ac_name, pla_sb_id, sb_desc, pla_cc_id, cc_desc " _
                                                                    & "From pla_mstr  " _
                                                                    & "inner join ac_mstr on ac_id = pla_ac_id " _
                                                                    & "inner join sb_mstr on sb_id = pla_sb_id " _
                                                                    & "inner join cc_mstr on cc_id = pla_cc_id " _
                                                                    & "where pla_pl_id = " + dr_det("pt_pl_id").ToString _
                                                                    & "and pla_code in('SL_SLACC','SL_SLDACC') order by pla_code"
                                                .InitializeCommand()
                                                .FillDataSet(ds_bantu, "prodline")

                                                If ds_bantu.Tables(0).Rows.Count = 0 Then
                                                    Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                                        dr_det("pt_pl_id").ToString) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' kosong")
                                                    Exit Sub
                                                ElseIf ds_bantu.Tables(0).Rows(0).Item(0) Is System.DBNull.Value Then
                                                    Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                                    dr_det("pt_pl_id").ToString) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' belum di setting product line nya")
                                                    Exit Sub
                                                ElseIf ds_bantu.Tables(0).Rows(0).Item(0) = 0 Then

                                                    Box("Data product line = " & master_new.PGSqlConn.GetNameByID("pl_mstr", "pl_id", "pl_desc", _
                                                        dr_det("pt_pl_id").ToString) & ", baris = 'SL_SLACC' dan/atau 'SL_SLDACC' masih - di setting product line nya")

                                                    Exit Sub

                                                End If


                                            End With

                                        End Using

                                    Catch ex As Exception


                                        MessageBox.Show(ex.Message)

                                    End Try

                                    _row("sod_sales_ac_id") = ds_bantu.Tables(0).Rows(0).Item("pla_ac_id")
                                    _row("sod_sales_sb_id") = ds_bantu.Tables(0).Rows(0).Item("pla_sb_id")
                                    _row("sod_sales_cc_id") = ds_bantu.Tables(0).Rows(0).Item("pla_cc_id")
                                    _row("sod_disc_ac_id") = ds_bantu.Tables(0).Rows(1).Item("pla_ac_id")

                                    _row("sod_um_conv") = 1.0
                                    _row("sod_qty_real") = dr_det("qty")
                                    _row("sod_taxable") = dr_det("pt_taxable")
                                    _row("sod_tax_inc") = dr_det("pt_tax_inc")
                                    _row("sod_tax_class") = dr_det("pt_tax_class")
                                    _row("sod_ppn_type") = dr_det("pt_ppn_type")
                                    _row("sod_loc_id") = sod_loc_id
                                    _row("pt_type") = dr_det("pt_type")
                                    _row("pt_ls") = dr_det("pt_ls")


                                    dt_edit.Rows.Add(_row)

                                    dt_edit.AcceptChanges()


                                Next


                                For i = 0 To dt_edit.Rows.Count - 1

                                    '.Command.CommandType = CommandType.Text
                                    .Command.CommandText = "INSERT INTO  " _
                                            & "  public.sod_det " _
                                            & "( " _
                                            & "  sod_oid, " _
                                            & "  sod_dom_id, " _
                                            & "  sod_en_id, " _
                                            & "  sod_add_by, " _
                                            & "  sod_add_date, " _
                                            & "  sod_so_oid, " _
                                            & "  sod_seq, " _
                                            & "  sod_is_additional_charge, " _
                                            & "  sod_si_id, " _
                                            & "  sod_pt_id, " _
                                            & "  sod_rmks, " _
                                            & "  sod_qty, " _
                                            & "  sod_um, " _
                                            & "  sod_cost, " _
                                            & "  sod_price, " _
                                            & "  sod_disc, " _
                                            & "  sod_sales_ac_id, " _
                                            & "  sod_sales_sb_id, " _
                                            & "  sod_sales_cc_id, " _
                                            & "  sod_um_conv, " _
                                            & "  sod_qty_real, " _
                                            & "  sod_taxable, " _
                                            & "  sod_tax_inc, " _
                                            & "  sod_tax_class, " _
                                            & "  sod_ppn_type, " _
                                            & "  sod_dt, " _
                                            & "  sod_payment, " _
                                            & "  sod_dp, " _
                                            & "  sod_loc_id, " _
                                            & "  sod_sales_unit,sod_ppn,sod_pph,sod_commision,sod_commision_total, " _
                                            & "  sod_pod_oid " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(dt_edit.Rows(i).Item("sod_oid")) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetSetring(dt_edit.Rows(i).Item("sod_en_id")) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_so_oid.ToString) & ",  " _
                                            & SetInteger(i) & ",  " _
                                            & SetSetring(dt_edit.Rows(i).Item("sod_is_additional_charge")) & ",  " _
                                            & SetInteger(dt_edit.Rows(i).Item("sod_si_id")) & ",  " _
                                            & SetInteger(dt_edit.Rows(i).Item("sod_pt_id")) & ",  " _
                                            & SetSetringDB(dt_edit.Rows(i).Item("sod_rmks")) & ",  " _
                                            & SetDbl(dt_edit.Rows(i).Item("sod_qty")) & ",  " _
                                            & SetInteger(dt_edit.Rows(i).Item("sod_um")) & ",  " _
                                            & SetDbl(dt_edit.Rows(i).Item("sod_cost")) & ",  " _
                                            & SetDbl(dt_edit.Rows(i).Item("sod_price")) & ",  " _
                                            & SetDbl(dt_edit.Rows(i).Item("sod_disc")) & ",  " _
                                            & SetInteger(dt_edit.Rows(i).Item("sod_sales_ac_id")) & ",  " _
                                            & SetInteger(dt_edit.Rows(i).Item("sod_sales_sb_id")) & ",  " _
                                            & SetInteger(dt_edit.Rows(i).Item("sod_sales_cc_id")) & ",  " _
                                            & SetInteger(dt_edit.Rows(i).Item("sod_um_conv")) & ",  " _
                                            & SetDbl(dt_edit.Rows(i).Item("sod_qty_real")) & ",  " _
                                            & SetSetring(dt_edit.Rows(i).Item("sod_taxable")) & ",  " _
                                            & SetSetring(dt_edit.Rows(i).Item("sod_tax_inc")) & ",  " _
                                            & SetInteger(dt_edit.Rows(i).Item("sod_tax_class")) & ",  " _
                                            & SetSetring(dt_edit.Rows(i).Item("sod_ppn_type")) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetDblDB(dt_edit.Rows(i).Item("sod_payment")) & ",  " _
                                            & SetDblDB(dt_edit.Rows(i).Item("sod_dp")) & ",  " _
                                            & SetInteger(dt_edit.Rows(i).Item("sod_loc_id")) & ",  " _
                                            & SetDblDB(dt_edit.Rows(i).Item("sod_sales_unit")) & ",  " _
                                            & SetDbl(0) & "," _
                                            & SetDbl(0) & "," _
                                            & SetDblDB(dt_edit.Rows(i).Item("sod_commision")) & ",  " _
                                            & SetDblDB(dt_edit.Rows(i).Item("sod_commision_total")) & ",  " _
                                            & SetSetring(dt_edit.Rows(i).Item("sod_pod_oid").ToString) & "  " _
                                            & ")"

                                    sSQLs.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()




                                Next


                                Dim dt_pt As New DataTable



                                For i = 0 To dt_edit.Rows.Count - 1


                                    Dim ssql As String


                                    ssql = "SELECT  " _
                                         & "  b.psd_pt_bom_id, " _
                                         & "  c.pt_code, " _
                                         & "  c.pt_desc1, " _
                                         & "  c.pt_desc2, " _
                                         & "  sum(b.psd_qty) as psd_qty " _
                                         & "FROM " _
                                         & "  public.psd_det b " _
                                         & "  INNER JOIN public.pt_mstr c ON (b.psd_pt_bom_id = c.pt_id) " _
                                         & "  INNER JOIN public.ps_mstr a ON (b.psd_ps_oid = a.ps_oid) " _
                                         & "WHERE " _
                                         & "  a.ps_pt_bom_id =" & SetInteger(dt_edit.Rows(i).Item("sod_pt_id")) _
                                         & " group by psd_pt_bom_id,pt_code,pt_desc1,pt_desc2"


                                    dt_pt = master_new.PGSqlConn.GetTableData(ssql)


                                    If dt_pt.Rows.Count = 0 Then

                                        '.Command.CommandType = CommandType.Text
                                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.sodas_assembly " _
                                            & "( " _
                                            & "  sodas_oid, " _
                                            & "  sodas_so_oid, " _
                                            & "  sodas_pt_id_sod, " _
                                            & "  sodas_pt_id, " _
                                            & "  sodas_qty, " _
                                            & "  sodas_sod_oid, " _
                                            & "  sodas_qty_sold " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetSetring(_so_oid.ToString) & ",  " _
                                            & SetInteger(dt_edit.Rows(i).Item("sod_pt_id")) & ",  " _
                                            & SetInteger(dt_edit.Rows(i).Item("sod_pt_id")) & ",  " _
                                            & SetDec(dt_edit.Rows(i).Item("sod_qty")) & ",  " _
                                            & SetSetring(dt_edit.Rows(i).Item("sod_oid")) & ",  " _
                                            & SetDec(dt_edit.Rows(i).Item("sod_qty")) & "  " _
                                            & ")"

                                        sSQLs.Add(.Command.CommandText)
                                        .Command.ExecuteNonQuery()
                                        '.Command.Parameters.Clear()

                                        Dim _row_sodas As DataRow

                                        _row_sodas = dt_sodas.NewRow

                                        _row_sodas("sodas_oid") = Guid.NewGuid.ToString
                                        _row_sodas("sodas_so_oid") = _so_oid.ToString
                                        _row_sodas("sodas_pt_id_sod") = dt_edit.Rows(i).Item("sod_pt_id")
                                        _row_sodas("sodas_pt_id") = dt_edit.Rows(i).Item("sod_pt_id")
                                        _row_sodas("pt_code") = dt_edit.Rows(i).Item("pt_code")
                                        _row_sodas("sodas_qty") = dt_edit.Rows(i).Item("sod_qty")
                                        _row_sodas("sodas_sod_oid") = dt_edit.Rows(i).Item("sod_oid")
                                        _row_sodas("sodas_qty_sold") = dt_edit.Rows(i).Item("sod_qty")

                                        dt_sodas.Rows.Add(_row_sodas)




                                    Else


                                        'Dim _dtrow As DataRow

                                        For Each dr As DataRow In dt_pt.Rows

                                            '.Command.CommandType = CommandType.Text
                                            .Command.CommandText = "INSERT INTO  " _
                                                       & "  public.sodas_assembly " _
                                                       & "( " _
                                                       & "  sodas_oid, " _
                                                       & "  sodas_so_oid, " _
                                                       & "  sodas_pt_id_sod, " _
                                                       & "  sodas_pt_id, " _
                                                       & "  sodas_qty, " _
                                                       & "  sodas_sod_oid, " _
                                                       & "  sodas_qty_sold " _
                                                       & ")  " _
                                                       & "VALUES ( " _
                                                       & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                                       & SetSetring(_so_oid.ToString) & ",  " _
                                                       & SetInteger(dt_edit.Rows(i).Item("sod_pt_id")) & ",  " _
                                                       & SetInteger(dr("psd_pt_bom_id")) & ",  " _
                                                       & SetDec(dr("psd_qty")) & ",  " _
                                                       & SetSetring(dt_edit.Rows(i).Item("sod_oid")) & ",  " _
                                                       & SetDec(dr("psd_qty") * SetNumber(dt_edit.Rows(i).Item("sod_qty"))) & "  " _
                                                       & ")"


                                            sSQLs.Add(.Command.CommandText)
                                            .Command.ExecuteNonQuery()
                                            '.Command.Parameters.Clear()


                                            Dim _row_sodas As DataRow

                                            _row_sodas = dt_sodas.NewRow

                                            _row_sodas("sodas_oid") = Guid.NewGuid.ToString
                                            _row_sodas("sodas_so_oid") = _so_oid.ToString
                                            _row_sodas("sodas_pt_id_sod") = dt_edit.Rows(i).Item("sod_pt_id")
                                            _row_sodas("sodas_pt_id") = dr("psd_pt_bom_id")
                                            _row_sodas("pt_code") = dr("pt_code")
                                            _row_sodas("sodas_qty") = dr("psd_qty")
                                            _row_sodas("sodas_sod_oid") = dt_edit.Rows(i).Item("sod_oid")
                                            _row_sodas("sodas_qty_sold") = dr("psd_qty") * SetNumber(dt_edit.Rows(i).Item("sod_qty"))

                                            dt_sodas.Rows.Add(_row_sodas)




                                        Next
                                        dt_sodas.AcceptChanges()



                                    End If



                                Next


                                sSQL = "update pos_transaksi set so_number=" & SetSetring(_so_code) & " where id=" & dr_tr("id")

                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = sSQL
                                sSQLs.Add(.Command.CommandText)
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()





                                'apabila chash maka langsung bisa generate sales order shipment....
                                If dr_tr("code_usr_1") = 0 Then
                                    If insert_shipment(objinsert, _so_oid.ToString, _so_code, _
                                                       dr_tr("en_code").ToString, dr_tr("en_id").ToString, _
                                                       dr_tr("tanggal_transaksi"), so_si_id, sSQLs, CInt(so_cu_id), CDbl(1.0), CInt(so_bk_id)) = False Then

                                        'sqlTran.Rollback()
                                        'insert = False
                                        Exit Sub
                                    End If

                                    '.Command.CommandType = CommandType.Text
                                    .Command.CommandText = "update so_mstr set so_close_date = current_date " _
                                                         & " where so_code = " + SetSetring(_so_code)
                                    sSQLs.Add(.Command.CommandText)
                                    .Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()
                                End If







                            Next

                            If master_new.PGSqlConn.status_sync = True Then
                                For Each Data As String In master_new.ModFunction.FinsertSQL2Array(sSQLs)
                                    '.Command.CommandType = CommandType.Text
                                    .Command.CommandText = Data
                                    .Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()
                                Next
                            End If


                            .Command.Commit()

                            MsgBox("Success")

                            'after_success()
                            'set_row(_so_oid.ToString, "so_oid")
                            'dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                            'insert = True
                        Catch ex As PgSqlException
                            'sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                            'insert = False
                        End Try
                    End With
                End Using
            Catch ex As Exception
                row = 0
                'insert = False
                MessageBox.Show(ex.Message)
            End Try

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function insert_shipment(ByVal par_obj As Object, ByVal par_so_oid As String, _
                                     ByVal par_so_code As String, ByVal par_en_code As String, _
                                     ByVal par_en_id As String, ByVal par_date As Date, _
                                     ByVal par_si_id As String, ByVal par_ssqls As ArrayList, _
                                     ByVal par_cu_id As Integer, ByVal par_exc_rate As Double, _
                                     ByVal par_bk_id As Integer) As Boolean
        insert_shipment = True
        Dim _soship_oid As Guid
        Dim _soship_code, _serial, _pt_code As String
        'Dim _cost_methode As String 
        Dim _en_id, _si_id, _loc_id, _pt_id As Integer
        Dim _tran_id As Integer
        Dim _cost, _cost_avg, _qty As Double
        Dim i, i_2 As Integer
        Dim ds_bantu_ry As New DataSet

        _soship_oid = Guid.NewGuid

        _soship_code = func_coll.get_transaction_number("SS", par_en_code, "soship_mstr", "soship_code")

        _tran_id = func_coll.get_id_tran_mstr("iss-so")
        If _tran_id = -1 Then
            MessageBox.Show("Sales Order Shipment In Transaction Master Doesn't Exist", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        With par_obj
            '.Command.CommandType = CommandType.Text
            .Command.CommandText = "INSERT INTO  " _
                                & "  public.soship_mstr " _
                                & "( " _
                                & "  soship_oid, " _
                                & "  soship_dom_id, " _
                                & "  soship_en_id, " _
                                & "  soship_add_by, " _
                                & "  soship_add_date, " _
                                & "  soship_code, " _
                                & "  soship_date, " _
                                & "  soship_so_oid, " _
                                & "  soship_si_id, " _
                                & "  soship_is_shipment, " _
                                & "  soship_dt " _
                                & ")  " _
                                & "VALUES ( " _
                                & SetSetring(_soship_oid.ToString) & ",  " _
                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                & SetInteger(par_en_id) & ",  " _
                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                & SetSetring(_soship_code) & ",  " _
                                & SetDate(par_date) & ",  " _
                                & SetSetring(par_so_oid.ToString) & ",  " _
                                & SetInteger(par_si_id) & ",  " _
                                & SetSetring("Y") & ",  " _
                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                & ")"
            par_ssqls.Add(.Command.CommandText)
            .Command.ExecuteNonQuery()
            '.Command.Parameters.Clear()

            For i = 0 To dt_edit.Rows.Count - 1
                '.Command.CommandType = CommandType.Text
                .Command.CommandText = "INSERT INTO  " _
                                    & "  public.soshipd_det " _
                                    & "( " _
                                    & "  soshipd_oid, " _
                                    & "  soshipd_soship_oid, " _
                                    & "  soshipd_sod_oid, " _
                                    & "  soshipd_seq, " _
                                    & "  soshipd_qty, " _
                                    & "  soshipd_um, " _
                                    & "  soshipd_um_conv, " _
                                    & "  soshipd_qty_real, " _
                                    & "  soshipd_si_id, " _
                                    & "  soshipd_loc_id, " _
                                    & "  soshipd_lot_serial, " _
                                    & "  soshipd_rea_code_id, " _
                                    & "  soshipd_dt " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(_soship_oid.ToString) & ",  " _
                                    & SetSetring(dt_edit.Rows(i).Item("sod_oid").ToString) & ",  " _
                                    & SetInteger(i) & ",  " _
                                    & SetDbl(dt_edit.Rows(i).Item("sod_qty") * -1) & ",  " _
                                    & SetInteger(dt_edit.Rows(i).Item("sod_um")) & ",  " _
                                    & SetDbl(dt_edit.Rows(i).Item("sod_um_conv")) & ",  " _
                                    & SetSetring(dt_edit.Rows(i).Item("sod_qty_real") * -1) & ",  " _
                                    & SetInteger(dt_edit.Rows(i).Item("sod_si_id")) & ",  " _
                                    & SetInteger(dt_edit.Rows(i).Item("sod_loc_id")) & ",  " _
                                    & SetSetringDB("") & ",  " _
                                    & " null " & ",  " _
                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                    & ")"
                par_ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                '.Command.Parameters.Clear()

                'Update sod_det
                '.Command.CommandType = CommandType.Text
                .Command.CommandText = "update sod_det set sod_qty_shipment = coalesce(sod_qty_shipment,0) + " + dt_edit.Rows(i).Item("sod_qty").ToString _
                                     & " where sod_oid = '" + dt_edit.Rows(i).Item("sod_oid").ToString + "'"
                par_ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                '.Command.Parameters.Clear()
            Next

            'Update Table Inventory Dan Cost Inventory Dan History Inventory
            '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial
            i_2 = 0
            For i = 0 To dt_edit.Rows.Count - 1
                If dt_edit.Rows(i).Item("pt_type").ToString.ToUpper = "I" Then
                    If dt_edit.Rows(i).Item("sod_qty_real") > 0 Then
                        If dt_edit.Rows(i).Item("pt_ls").ToString.ToUpper = "N" Then
                            i_2 += 1

                            _en_id = dt_edit.Rows(i).Item("sod_en_id")
                            _si_id = dt_edit.Rows(i).Item("sod_si_id")
                            _loc_id = dt_edit.Rows(i).Item("sod_loc_id")


                            For Each row_sodas As DataRow In dt_sodas.Rows
                                If SetNumber(dt_edit.Rows(i).Item("sod_pt_id")) = SetNumber(row_sodas("sodas_pt_id_sod")) Then

                                    _pt_id = row_sodas("sodas_pt_id")
                                    _pt_code = row_sodas("pt_code")
                                    _serial = "''"
                                    _qty = row_sodas("sodas_qty_sold")

                                    If func_coll.update_invc_mstr_minus(par_ssqls, par_obj, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                                        Return False
                                    End If

                                End If
                            Next




                            'Update History Inventory                        
                            _qty = _qty * -1.0
                            _cost = dt_edit.Rows(i).Item("sod_cost") - (dt_edit.Rows(i).Item("sod_cost") * dt_edit.Rows(i).Item("sod_disc"))
                            _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
                            If func_coll.update_invh_mstr(par_ssqls, par_obj, _tran_id, i_2, _en_id, _soship_code, _
                                                          _soship_oid.ToString, "SO Shipment", "", _si_id, _loc_id, _pt_id, _qty, _
                                                          _cost, _cost_avg, "", par_date) = False Then
                                Return False
                            End If
                        Else
                            MessageBox.Show("Error Serial..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Return False
                        End If
                    End If
                End If
            Next

            ''3. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
            'For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            '    If ds_edit.Tables(0).Rows(i).Item("pt_type").ToString.ToUpper = "I" Then
            '        _cost_methode = func_coll.get_pt_cost_method(ds_edit.Tables(0).Rows(i).Item("sod_pt_id").ToString.ToUpper)
            '        _en_id = ds_edit.Tables(0).Rows(i).Item("sod_en_id")
            '        _si_id = ds_edit.Tables(0).Rows(i).Item("sod_si_id")
            '        _pt_id = ds_edit.Tables(0).Rows(i).Item("sod_pt_id")
            '        _qty = ds_edit.Tables(0).Rows(i).Item("sod_qty_real")
            '        _cost = ds_edit.Tables(0).Rows(i).Item("sod_cost") - (ds_edit.Tables(0).Rows(i).Item("sod_cost") * ds_edit.Tables(0).Rows(i).Item("sod_disc"))

            '        If _cost_methode = "F" Or _cost_methode = "L" Then
            '            MessageBox.Show("Fifo or Lifo Not Aplicable..", "Error", MessageBoxButtons.OK)
            '            Return False
            '            'If func_coll.update_invct_table_minus(ssqls, par_obj, _en_id, _pt_id, _qty, _cost_methode) = False Then
            '            '    Return False
            '            'End If
            '        ElseIf _cost_methode = "A" Then
            '            _cost_avg = func_coll.get_cost_average("-", _en_id, _si_id, _pt_id, _qty, _cost)
            '            If func_coll.update_item_cost_avg(ssqls, par_obj, _si_id, _pt_id, _cost_avg) = False Then
            '                Return False
            '            End If
            '        End If
            '    End If
            'Next

            ' TIdak jadi karena sudah ada di menu khusus untuk penghitungan royalti
            ' ''Update Ke Table Royalti 'soshipd_qty
            ''For i = 0 To ds_edit.Tables(0).Rows.Count - 1
            ''    _soshipd_qty_real = ds_edit.Tables(0).Rows(i).Item("soshipd_qty_real")

            ''    If ds_edit.Tables(0).Rows(i).Item("soshipd_qty_real") > 0 Then
            ''        Try
            ''            Using objcb As New master_new.CustomCommand
            ''                With objcb
            ''                    .SQL = "select royt_oid, royt_pt_id, royt_seq, royt_qty_royalti - royt_qty_so as royt_qty_open " + _
            ''                       " from royt_table " + _
            ''                       " where royt_qty_royalti > royt_qty_so " + _
            ''                       " and royt_pt_id  = " + ds_edit.Tables(0).Rows(i).Item("pt_id").ToString + _
            ''                       " order by royt_seq "
            ''                    .InitializeCommand()
            ''                    .FillDataSet(ds_bantu_ry, "royalti")
            ''                End With
            ''            End Using
            ''        Catch ex As Exception
            ''            MessageBox.Show(ex.Message)
            ''        End Try

            ''        For j = 0 To ds_bantu_ry.Tables(0).Rows.Count - 1
            ''            If _soshipd_qty_real > ds_bantu_ry.Tables(0).Rows(j).Item("royt_qty_open") Then
            ''                '.Command.CommandType = CommandType.Text
            ''                .Command.CommandText = "update royt_table set royt_qty_so = royt_qty_so + " + ds_bantu_ry.Tables(0).Rows(j).Item("royt_qty_open").ToString _
            ''                                     & " where royt_oid = '" + ds_bantu_ry.Tables(0).Rows(j).Item("royt_oid").ToString + "'"
            ''                ssqls.Add(.Command.CommandText)
            ''                .Command.ExecuteNonQuery()
            ''                '.Command.Parameters.Clear()

            ''                _soshipd_qty_real = _soshipd_qty_real - ds_bantu_ry.Tables(0).Rows(j).Item("royt_qty_open")
            ''            Else
            ''                '.Command.CommandType = CommandType.Text
            ''                .Command.CommandText = "update royt_table set royt_qty_so = royt_qty_so + " + _soshipd_qty_real.ToString _
            ''                                     & " where royt_oid = '" + ds_bantu_ry.Tables(0).Rows(j).Item("royt_oid").ToString + "'"
            ''                ssqls.Add(.Command.CommandText)
            ''                .Command.ExecuteNonQuery()
            ''                '.Command.Parameters.Clear()
            ''                Exit For 'karena nilai _shosipd_qty_real sudah habis...
            ''            End If
            ''        Next
            ''    End If
            ''Next
            ' ''**********************************************************
            Dim _create_jurnal As Boolean = func_coll.get_create_jurnal_status

            If _create_jurnal = True Then
                If glt_det_so_shipment(par_ssqls, par_obj, dt_edit, _soship_oid.ToString, _
                                       _soship_code, par_en_code, par_en_id, par_date, par_si_id, par_cu_id, par_exc_rate) = False Then
                    Return False
                End If

                If jurnal_payment(par_obj, par_so_oid, par_so_code, par_en_code, par_en_id, _
                                  par_date, par_si_id, par_cu_id, par_exc_rate, par_bk_id, par_ssqls) = False Then
                    Return False
                End If
            End If
        End With
    End Function

    Private Function glt_det_so_shipment(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, _
                                         ByVal par_ds As DataTable, ByVal par_soship_oid As String, _
                                         ByVal par_soship_code As String, ByVal par_en_code As String, _
                                     ByVal par_en_id As String, ByVal par_date As Date, _
                                     ByVal par_si_id As String, ByVal par_cu_id As Integer, ByVal par_exc_rate As Double) As Boolean
        glt_det_so_shipment = True
        Dim i, _pl_id As Integer
        Dim _glt_code As String
        Dim dt_bantu As DataTable
        Dim _date As Date = par_date
        Dim _cost As Double
        _glt_code = func_coll.get_transaction_number("IC", par_en_code, "glt_det", "glt_code")
        Dim _seq As Integer = -1

        For i = 0 To dt_edit.Rows.Count - 1
            _seq = _seq + 1

            With par_obj
                Try
                    If dt_edit.Rows(i).Item("sod_qty") > 0 Then
                        _pl_id = func_coll.get_prodline(dt_edit.Rows(i).Item("sod_pt_id"))
                        '_cost = par_ds.Tables(0).Rows(i).Item("sod_qty") * (par_ds.Tables(0).Rows(i).Item("sod_cost") - (par_ds.Tables(0).Rows(i).Item("sod_cost") * par_ds.Tables(0).Rows(i).Item("sod_disc")))
                        _cost = dt_edit.Rows(i).Item("sod_qty") * (dt_edit.Rows(i).Item("sod_cost"))

                        dt_bantu = New DataTable
                        dt_bantu = (func_coll.get_prodline_account(_pl_id, "SL_CMACC"))

                        'Insert Untuk Yang Debet

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(dt_edit.Rows(i).Item("sod_en_id")) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(_date) & ",  " _
                                            & SetSetring("IC") & ",  " _
                                            & SetInteger(par_cu_id) & ",  " _
                                            & SetDbl(par_exc_rate) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                            & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                            & SetSetringDB("SO Shipment") & ",  " _
                                            & SetDblDB(_cost) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetSetring(par_soship_oid) & ",  " _
                                            & SetSetring(par_soship_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring("IC-SOS") & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, _date, _
                                                         dt_bantu.Rows(0).Item("pla_ac_id"), _
                                                         dt_bantu.Rows(0).Item("pla_sb_id"), _
                                                         dt_bantu.Rows(0).Item("pla_cc_id"), _
                                                         dt_edit.Rows(i).Item("sod_en_id"), par_cu_id, _
                                                         par_exc_rate, _cost, "D") = False Then

                            Return False
                            Exit Function
                        End If
                    End If

                    _seq = _seq + 1

                    dt_bantu = New DataTable
                    dt_bantu = (func_coll.get_prodline_account(_pl_id, "INV_ACCT"))

                    '.Command.CommandType = CommandType.Text
                    .Command.CommandText = "INSERT INTO  " _
                                        & "  public.glt_det " _
                                        & "( " _
                                        & "  glt_oid, " _
                                        & "  glt_dom_id, " _
                                        & "  glt_en_id, " _
                                        & "  glt_add_by, " _
                                        & "  glt_add_date, " _
                                        & "  glt_code, " _
                                        & "  glt_date, " _
                                        & "  glt_type, " _
                                        & "  glt_cu_id, " _
                                        & "  glt_exc_rate, " _
                                        & "  glt_seq, " _
                                        & "  glt_ac_id, " _
                                        & "  glt_sb_id, " _
                                        & "  glt_cc_id, " _
                                        & "  glt_desc, " _
                                        & "  glt_debit, " _
                                        & "  glt_credit, " _
                                        & "  glt_ref_oid, " _
                                        & "  glt_ref_trans_code, " _
                                        & "  glt_posted, " _
                                        & "  glt_dt, " _
                                        & "  glt_daybook " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                        & SetInteger(dt_edit.Rows(i).Item("sod_en_id")) & ",  " _
                                        & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                        & SetSetring(_glt_code) & ",  " _
                                        & SetDate(_date) & ",  " _
                                        & SetSetring("IC") & ",  " _
                                        & SetInteger(par_cu_id) & ",  " _
                                        & SetDbl(par_exc_rate) & ",  " _
                                        & SetInteger(_seq) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_ac_id")) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_sb_id")) & ",  " _
                                        & SetInteger(dt_bantu.Rows(0).Item("pla_cc_id")) & ",  " _
                                        & SetSetring("SO Shipment") & ",  " _
                                        & SetDblDB(0) & ",  " _
                                        & SetDblDB(_cost) & ",  " _
                                        & SetSetring(par_soship_oid) & ",  " _
                                        & SetSetring(par_soship_code) & ",  " _
                                        & SetSetring("N") & ",  " _
                                        & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                        & SetSetring("IC-SOS") & "  " _
                                        & ")"
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    '.Command.Parameters.Clear()

                    If func_coll.update_unposted_glbal_balance_tran(par_ssqls, par_obj, _date, _
                                                     dt_bantu.Rows(0).Item("pla_ac_id"), _
                                                     dt_bantu.Rows(0).Item("pla_sb_id"), _
                                                     dt_bantu.Rows(0).Item("pla_cc_id"), _
                                                     dt_edit.Rows(i).Item("sod_en_id"), par_cu_id, _
                                                     par_exc_rate, _cost, "C") = False Then

                        Return False
                        Exit Function
                    End If
                    '********************** finish untuk yang credit
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return False
                End Try
            End With
        Next
    End Function

    Private Function jurnal_payment(ByVal par_obj As Object, ByVal par_so_oid As String, ByVal par_so_code As String, _
                                    ByVal par_en_code As String, _
                                     ByVal par_en_id As String, ByVal par_date As Date, _
                                     ByVal par_si_id As String, ByVal par_cu_id As Integer, ByVal par_exc_rate As Double, _
                                     ByVal par_bk_id As Integer, ByVal par_ssqls As ArrayList) As Boolean
        jurnal_payment = True
        'buat struktur dulu datasetnya...dengan data yang kosong
        Dim ds_edit_shipment As New DataSet
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    .SQL = "SELECT  " _
                        & "  ars_oid, True as ceklist, " _
                        & "  ars_ar_oid, " _
                        & "  ars_seq, " _
                        & "  ars_soshipd_oid, " _
                        & "  soship_code, " _
                        & "  pt_id, " _
                        & "  pt_code, " _
                        & "  pt_desc1, " _
                        & "  pt_desc2, " _
                        & "  ars_taxable, " _
                        & "  ars_tax_class_id, " _
                        & "  code_name as taxclass_name, " _
                        & "  ars_tax_inc, " _
                        & "  ars_open, " _
                        & "  ars_invoice, " _
                        & "  ars_so_price, " _
                        & "  ars_so_disc_value, " _
                        & "  ars_invoice_price, " _
                        & "  ars_close_line, " _
                        & "  ars_dt " _
                        & "FROM  " _
                        & "  public.ars_ship " _
                        & "  inner join public.soshipd_det on public.ars_ship.ars_soshipd_oid = public.soshipd_det.soshipd_oid " _
                        & "  inner join public.soship_mstr on public.soshipd_det.soshipd_soship_oid = public.soship_mstr.soship_oid " _
                        & "  inner join public.sod_det on public.soshipd_det.soshipd_sod_oid = public.sod_det.sod_oid " _
                        & "  inner join public.pt_mstr on public.sod_det.sod_pt_id = public.pt_mstr.pt_id " _
                        & "  inner join public.code_mstr on public.ars_ship.ars_tax_class_id = public.code_mstr.code_id" _
                        & "  inner join public.ar_mstr on public.ars_ship.ars_ar_oid = public.ar_mstr.ar_oid" _
                        & " where ars_seq = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_shipment, "shipment")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim ds_edit_dist As New DataSet
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    .SQL = "SELECT  " _
                        & "  public.ard_dist.ard_oid, " _
                        & "  public.ard_dist.ard_ar_oid, " _
                        & "  public.ard_dist.ard_tax_distribution, " _
                        & "  public.ard_dist.ard_taxable, " _
                        & "  public.ard_dist.ard_tax_inc, " _
                        & "  public.ard_dist.ard_tax_class_id, " _
                        & "  public.ard_dist.ard_ac_id, " _
                        & "  public.ard_dist.ard_sb_id, " _
                        & "  public.ard_dist.ard_cc_id, " _
                        & "  public.ard_dist.ard_amount, " _
                        & "  public.ard_dist.ard_remarks, " _
                        & "  public.ard_dist.ard_dt, " _
                        & "  public.ac_mstr.ac_code, " _
                        & "  public.ac_mstr.ac_name, " _
                        & "  public.sb_mstr.sb_desc, " _
                        & "  public.code_mstr.code_name as taxclass_name, " _
                        & "  public.cc_mstr.cc_desc " _
                        & "FROM " _
                        & "  public.ard_dist " _
                        & "  INNER JOIN public.ar_mstr ON (public.ard_dist.ard_ar_oid = public.ar_mstr.ar_oid) " _
                        & "  INNER JOIN public.ac_mstr ON (public.ard_dist.ard_ac_id = public.ac_mstr.ac_id) " _
                        & "  left outer join public.sb_mstr ON (public.ard_dist.ard_sb_id = public.sb_mstr.sb_id) " _
                        & "  left outer join public.cc_mstr ON (public.ard_dist.ard_cc_id = public.cc_mstr.cc_id) " _
                        & "  left outer join public.code_mstr ON (public.ard_dist.ard_tax_class_id = public.code_mstr.code_id)" _
                        & " where ard_amount = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_dist, "dist")
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Dim i As Integer

        Dim _dtrow As DataRow
        For i = 0 To dt_edit.Rows.Count - 1
            _dtrow = ds_edit_shipment.Tables(0).NewRow
            _dtrow("ars_oid") = Guid.NewGuid.ToString
            _dtrow("ceklist") = True
            _dtrow("ars_soshipd_oid") = par_so_oid 'ini hanya formalitas saja
            _dtrow("soship_code") = par_so_code ''ini hanya formalitas saja
            _dtrow("pt_id") = dt_edit.Rows(i).Item("sod_pt_id")
            _dtrow("pt_code") = dt_edit.Rows(i).Item("pt_code")
            _dtrow("pt_desc1") = dt_edit.Rows(i).Item("pt_desc1")
            _dtrow("pt_desc2") = dt_edit.Rows(i).Item("pt_desc2")
            _dtrow("ars_taxable") = dt_edit.Rows(i).Item("sod_taxable")
            _dtrow("ars_tax_class_id") = dt_edit.Rows(i).Item("sod_tax_class")
            _dtrow("taxclass_name") = dt_edit.Rows(i).Item("sod_tax_class_name")
            _dtrow("ars_tax_inc") = dt_edit.Rows(i).Item("sod_tax_inc")
            _dtrow("ars_open") = dt_edit.Rows(i).Item("sod_qty")
            _dtrow("ars_invoice") = dt_edit.Rows(i).Item("sod_qty")
            _dtrow("ars_so_price") = dt_edit.Rows(i).Item("sod_price")
            _dtrow("ars_so_disc_value") = dt_edit.Rows(i).Item("sod_price") * dt_edit.Rows(i).Item("sod_disc")
            '_dtrow("ars_invoice_price") = ds_edit.Tables(0).Rows(i).Item("sod_price") - (ds_edit.Tables(0).Rows(i).Item("sod_price") * ds_edit.Tables(0).Rows(i).Item("sod_disc"))
            _dtrow("ars_invoice_price") = dt_edit.Rows(i).Item("sod_price") '- (ds_edit.Tables(0).Rows(i).Item("sod_price") * ds_edit.Tables(0).Rows(i).Item("sod_disc"))
            _dtrow("ars_close_line") = "Y"
            ds_edit_shipment.Tables(0).Rows.Add(_dtrow)
        Next
        ds_edit_shipment.Tables(0).AcceptChanges()

        'pindah kan ke table distribution
        Dim j As Integer

        ds_edit_dist.Tables(0).Clear()
        Dim _search As Boolean = False
        Dim _invoice_price, _line_tr_pph, _line_tr_ppn, _tax_rate As Double
        _invoice_price = 0
        _line_tr_pph = 0
        _line_tr_ppn = 0
        _tax_rate = 0

        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
            If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
                If ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 Then
                    'Mencari prodline account untuk masing2 line receipt
                    dt_bantu = New DataTable
                    dt_bantu = (func_data.get_prodline_account_ar(ds_edit_shipment.Tables(0).Rows(i).Item("pt_id")))
                    _search = False
                    For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                        'If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(0).Item("ac_id")) And _
                        '(ds_edit_dist.Tables(0).Rows(j).Item("ard_tax_class_id") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")) Then
                        If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(0).Item("ac_id")) Then
                            _search = True
                            Exit For
                        End If
                    Next

                    If _search = True Then
                        If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                            'Item Price - Tax Amount = Taxable Base                            
                            '100.00 - 9.09 = 90.91 
                            'disini hanya line ppn saja
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")
                            _tax_rate = func_coll.get_ppn(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
                            _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
                            ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _invoice_price
                        Else
                            ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _
                                                                            (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _
                                                                             ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price"))
                            ds_edit_dist.Tables(0).AcceptChanges()
                        End If
                    Else
                        _dtrow = ds_edit_dist.Tables(0).NewRow
                        _dtrow("ard_oid") = Guid.NewGuid.ToString

                        _dtrow("ard_ac_id") = dt_bantu.Rows(0).Item("ac_id")
                        _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")

                        _dtrow("ard_sb_id") = 0
                        _dtrow("sb_desc") = "-"
                        _dtrow("ard_cc_id") = 0
                        _dtrow("cc_desc") = "-"
                        _dtrow("ard_taxable") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable")
                        _dtrow("ard_tax_class_id") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")
                        _dtrow("taxclass_name") = ds_edit_shipment.Tables(0).Rows(i).Item("taxclass_name")
                        _dtrow("ard_remarks") = dt_bantu.Rows(0).Item("ac_name")

                        If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                            'Item Price - Tax Amount = Taxable Base                            
                            '100.00 - 9.09 = 90.91 
                            'disini hanya dicari ppn nya saja
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")
                            _tax_rate = func_coll.get_ppn(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
                            _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
                            _dtrow("ard_amount") = _invoice_price
                        Else
                            _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")
                            _dtrow("ard_amount") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price
                        End If


                        _dtrow("ard_tax_distribution") = "Y"

                        ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                        ds_edit_dist.Tables(0).AcceptChanges()
                    End If
                End If
            End If
        Next

        'Untuk PPN dan PPH
        Dim _ppn, _pph As Double
        _ppn = 0
        _pph = 0

        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
            If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then

                If ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 And ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable").ToString.ToUpper = "Y" Then
                    'Mencari taxrate account ar untuk masing2 line receipt
                    dt_bantu = New DataTable
                    dt_bantu = (func_data.get_taxrate_ar_account(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")))

                    '1. PPN
                    _search = False
                    For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                        If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")) Then 'rows(0) karena PPH
                            _search = True
                            Exit For
                        End If
                    Next
                    'Exit Sub
                    If _search = True Then
                        If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                            'Item Price - Tax Amount = Taxable Base                            
                            '100.00 - 9.09 = 90.91 
                            '_sod_cost = ds_edit.Tables(0).Rows(i).Item("sod_cost") - (_tax_rate * (ds_edit.Tables(0).Rows(i).Item("sod_cost") / (1 + _tax_rate)))
                            _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                            _ppn = _ppn * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                        Else
                            _ppn = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                        End If

                        ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _ppn
                        ds_edit_dist.Tables(0).AcceptChanges()
                    Else
                        _dtrow = ds_edit_dist.Tables(0).NewRow
                        _dtrow("ard_oid") = Guid.NewGuid.ToString

                        _dtrow("ard_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")
                        _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")

                        _dtrow("ard_sb_id") = 0
                        _dtrow("sb_desc") = "-"
                        _dtrow("ard_cc_id") = 0
                        _dtrow("cc_desc") = "-"
                        _dtrow("ard_taxable") = "N"
                        _dtrow("ard_tax_class_id") = DBNull.Value
                        _dtrow("taxclass_name") = DBNull.Value
                        _dtrow("ard_remarks") = dt_bantu.Rows(0).Item("ac_name")

                        If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                            _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                            _ppn = _ppn * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                        Else
                            _ppn = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                        End If

                        _dtrow("ard_amount") = _ppn
                        _dtrow("ard_tax_distribution") = "Y"
                        ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                        ds_edit_dist.Tables(0).AcceptChanges()
                    End If

                    '1. PPH
                    _search = False
                    For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                        If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")) Then 'rows(1) karena PPH
                            _search = True
                            Exit For
                        End If
                    Next

                    If _search = True Then
                        If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                            'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                            '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                            'Item Price - Tax Amount = Taxable Base                            
                            '100.00 - 9.09 = 90.91 
                            '_pph = (dt_bantu.Rows(1).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(1).Item("taxr_rate") / 100))
                            '_pph = _pph * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")

                            _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
                            _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") - _ppn) * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                            _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                        Else
                            _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")) * (dt_bantu.Rows(1).Item("taxr_rate") / 100)
                        End If

                        ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _pph
                        ds_edit_dist.Tables(0).AcceptChanges()
                    Else
                        _dtrow = ds_edit_dist.Tables(0).NewRow
                        _dtrow("ard_oid") = Guid.NewGuid.ToString

                        _dtrow("ard_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")
                        _dtrow("ac_code") = dt_bantu.Rows(1).Item("ac_code")

                        _dtrow("ard_sb_id") = 0
                        _dtrow("sb_desc") = "-"
                        _dtrow("ard_cc_id") = 0
                        _dtrow("cc_desc") = "-"
                        _dtrow("ard_taxable") = "N"
                        _dtrow("ard_tax_class_id") = DBNull.Value
                        _dtrow("taxclass_name") = DBNull.Value
                        _dtrow("ard_remarks") = dt_bantu.Rows(1).Item("ac_name")
                        If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                            '_pph = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'ini tetep mengacu ke ppn
                            '_pph = _pph * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")

                            _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn
                            _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") - _ppn) * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                            _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                        Else
                            _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price")) * (dt_bantu.Rows(1).Item("taxr_rate") / 100)
                        End If

                        _dtrow("ard_amount") = _pph
                        _dtrow("ard_tax_distribution") = "Y"
                        ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                        ds_edit_dist.Tables(0).AcceptChanges()
                    End If
                End If
            End If
        Next

        'Ini untuk ar discount
        _search = False
        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
            If ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 Then
                If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
                    If ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value") > 0 Then
                        'Mencari prodline account untuk masing2 line receipt
                        dt_bantu = New DataTable
                        dt_bantu = (func_data.get_prodline_account_ar_discount(ds_edit_shipment.Tables(0).Rows(i).Item("pt_id")))
                        _search = False
                        For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                            If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(0).Item("ac_id")) Then
                                _search = True
                                Exit For
                            End If
                        Next

                        If _search = True Then
                            If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                'Item Price - Tax Amount = Taxable Base                            
                                '100.00 - 9.09 = 90.91 
                                'disini hanya line ppn saja
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus kali -1 agar mengurangi
                                _tax_rate = func_coll.get_ppn(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
                                _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
                                ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _invoice_price
                            Else
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus kali -1 agar mengurangi

                                ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _
                                                                                (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _
                                                                                 _invoice_price)
                                ds_edit_dist.Tables(0).AcceptChanges()
                            End If
                        Else
                            _dtrow = ds_edit_dist.Tables(0).NewRow
                            _dtrow("ard_oid") = Guid.NewGuid.ToString

                            _dtrow("ard_ac_id") = dt_bantu.Rows(0).Item("ac_id")
                            _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
                            _dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")

                            _dtrow("ard_sb_id") = 0
                            _dtrow("sb_desc") = "-"
                            _dtrow("ard_cc_id") = 0
                            _dtrow("cc_desc") = "-"
                            _dtrow("ard_taxable") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable")
                            _dtrow("ard_tax_class_id") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")
                            _dtrow("taxclass_name") = ds_edit_shipment.Tables(0).Rows(i).Item("taxclass_name")
                            _dtrow("ard_remarks") = dt_bantu.Rows(0).Item("ac_name")

                            If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                'Item Price - Tax Amount = Taxable Base                            
                                '100.00 - 9.09 = 90.91 
                                'disini hanya dicari ppn nya saja
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'agar mengurangi
                                _tax_rate = func_coll.get_ppn(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id"))
                                _line_tr_ppn = _tax_rate * (_invoice_price / (1 + _tax_rate))
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * (_invoice_price - _line_tr_ppn)
                                _dtrow("ard_amount") = _invoice_price
                            Else
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1
                                _dtrow("ard_amount") = ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price
                            End If


                            _dtrow("ard_tax_distribution") = "Y"

                            ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                            ds_edit_dist.Tables(0).AcceptChanges()
                        End If
                    End If
                End If
            End If
        Next

        'Untuk PPN dan PPH yang ar discount
        _search = False
        _ppn = 0
        _pph = 0

        For i = 0 To ds_edit_shipment.Tables(0).Rows.Count - 1
            If ds_edit_shipment.Tables(0).Rows(i).Item("ceklist") = True Then
                If ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") <> 0 And ds_edit_shipment.Tables(0).Rows(i).Item("ars_taxable").ToString.ToUpper = "Y" Then
                    If ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value") > 0 Then
                        'Mencari taxrate account ap untuk masing2 line receipt
                        dt_bantu = New DataTable
                        dt_bantu = (func_data.get_taxrate_ar_account(ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_class_id")))

                        '1. PPN
                        _search = False
                        For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                            If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")) Then 'rows(0) karena PPH
                                _search = True
                                Exit For
                            End If
                        Next

                        If _search = True Then
                            If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                'Item Price - Tax Amount = Taxable Base                            
                                '100.00 - 9.09 = 90.91 
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali satu agar mengurangi
                                _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                                _ppn = _ppn * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                            Else
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali satu agar mengurangi
                                _ppn = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                            End If

                            ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _ppn
                            ds_edit_dist.Tables(0).AcceptChanges()
                        Else
                            _dtrow = ds_edit_dist.Tables(0).NewRow
                            _dtrow("ard_oid") = Guid.NewGuid.ToString

                            _dtrow("ard_ac_id") = dt_bantu.Rows(0).Item("taxr_ac_sales_id")
                            _dtrow("ac_code") = dt_bantu.Rows(0).Item("ac_code")
                            _dtrow("ac_name") = dt_bantu.Rows(0).Item("ac_name")

                            _dtrow("ard_sb_id") = 0
                            _dtrow("sb_desc") = "-"
                            _dtrow("ard_cc_id") = 0
                            _dtrow("cc_desc") = "-"
                            _dtrow("ard_taxable") = "N"
                            _dtrow("ard_tax_class_id") = DBNull.Value
                            _dtrow("taxclass_name") = DBNull.Value
                            _dtrow("ard_remarks") = dt_bantu.Rows(0).Item("ac_name")

                            If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali 1 agar mengurangi
                                _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100))
                                _ppn = _ppn * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                            Else
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali 1 agar mengurangi
                                _ppn = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price) * (dt_bantu.Rows(0).Item("taxr_rate") / 100)
                            End If

                            _dtrow("ard_amount") = _ppn
                            _dtrow("ard_tax_distribution") = "Y"
                            ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                            ds_edit_dist.Tables(0).AcceptChanges()
                        End If

                        '1. PPH
                        _search = False
                        For j = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
                            If (ds_edit_dist.Tables(0).Rows(j).Item("ard_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")) Then 'rows(1) karena PPH
                                _search = True
                                Exit For
                            End If
                        Next

                        If _search = True Then
                            If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                'Tax Rate * (Item Price/(1 + Tax Rate) = Tax Amount            
                                '0.10 * (100.00/1.10) = 0.10 * 90.90 = 9.09   
                                'Item Price - Tax Amount = Taxable Base                            
                                '100.00 - 9.09 = 90.91 
                                '_pph = (dt_bantu.Rows(1).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(1).Item("taxr_rate") / 100))
                                '_pph = _pph * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")

                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi
                                _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn ''harus ke po cost agar selisihnya masuk ke ap_rate variance 
                                _pph = (_invoice_price - _ppn) * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                            Else
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi
                                _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price) * (dt_bantu.Rows(1).Item("taxr_rate") / 100)
                            End If

                            ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") = ds_edit_dist.Tables(0).Rows(j).Item("ard_amount") + _pph
                            ds_edit_dist.Tables(0).AcceptChanges()
                        Else
                            _dtrow = ds_edit_dist.Tables(0).NewRow
                            _dtrow("ard_oid") = Guid.NewGuid.ToString

                            _dtrow("ard_ac_id") = dt_bantu.Rows(1).Item("taxr_ac_sales_id")
                            _dtrow("ac_code") = dt_bantu.Rows(1).Item("ac_code")
                            _dtrow("ac_name") = dt_bantu.Rows(1).Item("ac_name")

                            _dtrow("ard_sb_id") = 0
                            _dtrow("sb_desc") = "-"
                            _dtrow("ard_cc_id") = 0
                            _dtrow("cc_desc") = "-"
                            _dtrow("ard_taxable") = "N"
                            _dtrow("ard_tax_class_id") = DBNull.Value
                            _dtrow("taxclass_name") = DBNull.Value
                            _dtrow("ard_remarks") = dt_bantu.Rows(1).Item("ac_name")
                            If ds_edit_shipment.Tables(0).Rows(i).Item("ars_tax_inc").ToString.ToUpper = "Y" Then
                                '_pph = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice_price") / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'ini tetep mengacu ke ppn
                                '_pph = _pph * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi

                                _ppn = (dt_bantu.Rows(0).Item("taxr_rate") / 100) * (_invoice_price / (1 + dt_bantu.Rows(0).Item("taxr_rate") / 100)) 'tetep mengacu ke ppn 
                                _pph = (_invoice_price - _ppn) * ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice")
                                _pph = _pph * (dt_bantu.Rows(1).Item("taxr_rate") / 100) ' mengacu ke pph
                            Else
                                _invoice_price = ds_edit_shipment.Tables(0).Rows(i).Item("ars_so_disc_value")
                                _invoice_price = _invoice_price * -1 'harus dikali 1 karena ini mengurangi
                                _pph = (ds_edit_shipment.Tables(0).Rows(i).Item("ars_invoice") * _invoice_price) * (dt_bantu.Rows(1).Item("taxr_rate") / 100) 'harus ke po cost agar selisihnya masuk ke ap_rate variance
                            End If

                            _dtrow("ard_amount") = _pph
                            _dtrow("ard_tax_distribution") = "Y"
                            ds_edit_dist.Tables(0).Rows.Add(_dtrow)
                            ds_edit_dist.Tables(0).AcceptChanges()
                        End If
                    End If
                End If
            End If
        Next
        '**************************************************

        For i = ds_edit_dist.Tables(0).Rows.Count - 1 To 1 Step -1
            If ds_edit_dist.Tables(0).Rows(i).Item("ard_amount") = 0 Then
                ds_edit_dist.Tables(0).Rows(i).Delete()
            End If
        Next
        ds_edit_dist.Tables(0).AcceptChanges()

        Dim _bk_ac_id As Integer
        Dim _bk_ac_name As String = ""
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    '.Connection.Open()
                    '.Command = .Connection.CreateCommand
                    '.Command.CommandType = CommandType.Text
                    .Command.CommandText = "select bk_ac_id, ac_name from bk_mstr inner join ac_mstr on ac_id = bk_ac_id where bk_id = " + par_bk_id.ToString
                    .InitializeCommand()
                    .DataReader = .ExecuteReader
                    While .DataReader.Read
                        _bk_ac_id = .DataReader("bk_ac_id").ToString
                        _bk_ac_name = .DataReader("ac_name").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        Dim _ard_total_amount As Double = 0

        For i = 0 To ds_edit_dist.Tables(0).Rows.Count - 1
            _ard_total_amount = _ard_total_amount + ds_edit_dist.Tables(0).Rows(i).Item("ard_amount")
        Next

        If insert_glt_det_ar(par_ssqls, par_obj, ds_edit_dist, _
                                                       par_en_id, par_en_code, _
                                                       par_so_oid.ToString, par_so_code, _
                                                       par_date, _
                                                       par_cu_id, par_exc_rate, _
                                                       "AY", "AR-PAY", _
                                                       _bk_ac_id, 0, 0, _
                                                       _bk_ac_name, _ard_total_amount) = False Then

            Return False
        End If

    End Function

    Private Function insert_glt_det_ar(ByVal par_ssqls As ArrayList, ByVal par_obj As Object, ByVal par_ds As DataSet, _
                                   ByVal par_en_id As Integer, ByVal par_en_code As String, _
                                   ByVal par_oid As String, ByVal par_trans_code As String, _
                                   ByVal par_date As Date, ByVal par_cu_id As Integer, _
                                   ByVal par_exc_rate As Double, _
                                   ByVal par_type As String, ByVal par_daybook As String, _
                                   ByVal par_ac_id As Integer, ByVal par_sb_id As Integer, _
                                   ByVal par_cc_id As Integer, _
                                   ByVal par_desc As String, ByVal par_amount As Double) As Boolean

        'Return False
        'Exit Function
        insert_glt_det_ar = True
        Dim i As Integer
        Dim _glt_code As String = func_coll.get_transaction_number(par_type, par_en_code, "glt_det", "glt_code")
        Dim _seq As Integer = -1

        'Insert Untuk Yang Debet dan Credit, Looping dari dataset
        For i = 0 To par_ds.Tables(0).Rows.Count - 1
            _seq = _seq + 1

            If par_ds.Tables(0).Rows(i).Item("ard_amount") > 0 Then
                With par_obj
                    Try
                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(par_en_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(par_date) & ",  " _
                                            & SetSetring(par_type) & ",  " _
                                            & SetInteger(par_cu_id) & ",  " _
                                            & SetDbl(par_exc_rate) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(par_ds.Tables(0).Rows(i).Item("ard_ac_id")) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetSetringDB(par_ds.Tables(0).Rows(i).Item("ard_remarks")) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetDblDB(par_ds.Tables(0).Rows(i).Item("ard_amount")) & ",  " _
                                            & SetSetring(par_oid) & ",  " _
                                            & SetSetring(par_trans_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(par_daybook) & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
                                                             par_ds.Tables(0).Rows(i).Item("ard_ac_id"), _
                                                             SetIntegerDB(par_ds.Tables(0).Rows(i).Item("ard_sb_id")), _
                                                             SetIntegerDB(par_ds.Tables(0).Rows(i).Item("ard_cc_id")), _
                                                             par_en_id, par_cu_id, _
                                                             par_exc_rate, par_ds.Tables(0).Rows(i).Item("ard_amount"), "C") = False Then

                            Return False
                            Exit Function
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Return False
                    End Try
                End With
            ElseIf par_ds.Tables(0).Rows(i).Item("ard_amount") < 0 Then
                With par_obj
                    Try
                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.glt_det " _
                                            & "( " _
                                            & "  glt_oid, " _
                                            & "  glt_dom_id, " _
                                            & "  glt_en_id, " _
                                            & "  glt_add_by, " _
                                            & "  glt_add_date, " _
                                            & "  glt_code, " _
                                            & "  glt_date, " _
                                            & "  glt_type, " _
                                            & "  glt_cu_id, " _
                                            & "  glt_exc_rate, " _
                                            & "  glt_seq, " _
                                            & "  glt_ac_id, " _
                                            & "  glt_sb_id, " _
                                            & "  glt_cc_id, " _
                                            & "  glt_desc, " _
                                            & "  glt_debit, " _
                                            & "  glt_credit, " _
                                            & "  glt_ref_oid, " _
                                            & "  glt_ref_trans_code, " _
                                            & "  glt_posted, " _
                                            & "  glt_dt, " _
                                            & "  glt_daybook " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(par_en_id) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(_glt_code) & ",  " _
                                            & SetDate(par_date) & ",  " _
                                            & SetSetring(par_type) & ",  " _
                                            & SetInteger(par_cu_id) & ",  " _
                                            & SetDbl(par_exc_rate) & ",  " _
                                            & SetInteger(_seq) & ",  " _
                                            & SetInteger(par_ds.Tables(0).Rows(i).Item("ard_ac_id")) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetIntegerDB(0) & ",  " _
                                            & SetSetringDB(par_ds.Tables(0).Rows(i).Item("ard_remarks")) & ",  " _
                                            & SetDblDB(par_ds.Tables(0).Rows(i).Item("ard_amount") * -1) & ",  " _
                                            & SetDblDB(0) & ",  " _
                                            & SetSetring(par_oid) & ",  " _
                                            & SetSetring(par_trans_code) & ",  " _
                                            & SetSetring("N") & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                            & SetSetring(par_daybook) & "  " _
                                            & ")"
                        par_ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
                                                             par_ds.Tables(0).Rows(i).Item("ard_ac_id"), _
                                                             SetIntegerDB(par_ds.Tables(0).Rows(i).Item("ard_sb_id")), _
                                                             SetIntegerDB(par_ds.Tables(0).Rows(i).Item("ard_cc_id")), _
                                                             par_en_id, par_cu_id, _
                                                             par_exc_rate, par_ds.Tables(0).Rows(i).Item("ard_amount") * -1, "D") = False Then

                            Return False
                            Exit Function
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                        Return False
                    End Try
                End With
            End If
        Next

        _seq = _seq + 1

        With par_obj
            Try
                'Insert untuk yang credit yang account hutang nya
                '.Command.CommandType = CommandType.Text
                .Command.CommandText = "INSERT INTO  " _
                                    & "  public.glt_det " _
                                    & "( " _
                                    & "  glt_oid, " _
                                    & "  glt_dom_id, " _
                                    & "  glt_en_id, " _
                                    & "  glt_add_by, " _
                                    & "  glt_add_date, " _
                                    & "  glt_code, " _
                                    & "  glt_date, " _
                                    & "  glt_type, " _
                                    & "  glt_cu_id, " _
                                    & "  glt_exc_rate, " _
                                    & "  glt_seq, " _
                                    & "  glt_ac_id, " _
                                    & "  glt_sb_id, " _
                                    & "  glt_cc_id, " _
                                    & "  glt_desc, " _
                                    & "  glt_debit, " _
                                    & "  glt_credit, " _
                                    & "  glt_ref_oid, " _
                                    & "  glt_ref_trans_code, " _
                                    & "  glt_posted, " _
                                    & "  glt_dt, " _
                                    & "  glt_daybook " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                    & SetInteger(par_en_id) & ",  " _
                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                    & SetSetring(_glt_code) & ",  " _
                                    & SetDate(par_date) & ",  " _
                                    & SetSetring(par_type) & ",  " _
                                    & SetInteger(par_cu_id) & ",  " _
                                    & SetDbl(par_exc_rate) & ",  " _
                                    & SetInteger(_seq) & ",  " _
                                    & SetInteger(par_ac_id) & ",  " _
                                    & SetIntegerDB(par_sb_id) & ",  " _
                                    & SetIntegerDB(par_cc_id) & ",  " _
                                    & SetSetringDB(par_desc) & ",  " _
                                    & SetDblDB(par_amount) & ",  " _
                                    & SetDblDB(0) & ",  " _
                                    & SetSetring(par_oid) & ",  " _
                                    & SetSetring(par_trans_code) & ",  " _
                                    & SetSetring("N") & ",  " _
                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                    & SetSetring(par_daybook) & "  " _
                                    & ")"
                par_ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                '.Command.Parameters.Clear()

                If func_coll.update_unposted_glbal_balance(par_ssqls, par_obj, par_date, _
                                                 par_ac_id, _
                                                 par_sb_id, _
                                                 par_cc_id, _
                                                 par_en_id, par_cu_id, _
                                                 par_exc_rate, par_amount, "D") = False Then

                    Return False
                    Exit Function
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With
    End Function

    Private Function insert_sokp_piutang(ByVal par_obj As Object, ByVal par_so_oid As String, _
                                         ByVal par_ref As String, ByVal par_dp As Double, _
                                         ByVal par_amount As Double, ByVal par_so_pay_type As Integer, ByVal par_date As Date, _
                                         ByVal par_ssqls As ArrayList) As Boolean
        insert_sokp_piutang = True
        Dim i, _interval As Integer

        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    '.Connection.Open()
                    '.Command = .Connection.CreateCommand
                    '.Command.CommandType = CommandType.Text
                    .Command.CommandText = "select code_usr_1 From code_mstr " + _
                                           " where code_field = 'payment_type' " + _
                                           " and code_id = " + par_so_pay_type
                    .InitializeCommand()
                    .DataReader = .ExecuteReader
                    While .DataReader.Read
                        _interval = .DataReader("code_usr_1").ToString
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

        'If so_payment_date.Text = "" Then
        '    MessageBox.Show("Payment Date Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return False
        'End If

        Dim _date As Date = par_date

        With par_obj
            Try
                'Untuk Insert Yang DP
                'DP harus masuk juga ke kartu piutang....agar pada saat ar jadi pas..
                '.Command.CommandType = CommandType.Text
                .Command.CommandText = "INSERT INTO  " _
                                    & "  public.sokp_piutang " _
                                    & "( " _
                                    & "  sokp_oid, " _
                                    & "  sokp_so_oid, " _
                                    & "  sokp_seq, " _
                                    & "  sokp_ref, " _
                                    & "  sokp_amount, " _
                                    & "  sokp_due_date, " _
                                    & "  sokp_amount_pay, " _
                                    & "  sokp_description " _
                                    & ")  " _
                                    & "VALUES ( " _
                                    & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                    & SetSetring(par_so_oid) & ",  " _
                                    & SetInteger(0) & ",  " _
                                    & SetSetring(par_ref) & ",  " _
                                    & SetDbl(par_dp) & ",  " _
                                    & SetDate(_date) & "," _
                                    & SetDbl(0) & ",  " _
                                    & SetSetring("-") & "  " _
                                    & ")"
                par_ssqls.Add(.Command.CommandText)
                .Command.ExecuteNonQuery()
                '.Command.Parameters.Clear()

                For i = 0 To _interval - 1
                    _date = _date.AddMonths(1)

                    '.Command.CommandType = CommandType.Text
                    .Command.CommandText = "INSERT INTO  " _
                                        & "  public.sokp_piutang " _
                                        & "( " _
                                        & "  sokp_oid, " _
                                        & "  sokp_so_oid, " _
                                        & "  sokp_seq, " _
                                        & "  sokp_ref, " _
                                        & "  sokp_amount, " _
                                        & "  sokp_due_date, " _
                                        & "  sokp_amount_pay, " _
                                        & "  sokp_description " _
                                        & ")  " _
                                        & "VALUES ( " _
                                        & SetSetring(Guid.NewGuid.ToString) & ",  " _
                                        & SetSetring(par_so_oid) & ",  " _
                                        & SetInteger(i + 1) & ",  " _
                                        & SetSetring(par_ref) & ",  " _
                                        & SetDbl(par_amount) & ",  " _
                                        & SetDate(_date) & ",  " _
                                        & SetDbl(0) & ",  " _
                                        & SetSetring("-") & "  " _
                                        & ")"
                    par_ssqls.Add(.Command.CommandText)
                    .Command.ExecuteNonQuery()
                    '.Command.Parameters.Clear()
                Next
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Return False
            End Try
        End With

        Return insert_sokp_piutang
    End Function
End Class
