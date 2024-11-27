﻿Imports CoreLab.PostgreSql
Imports master_new.PGSqlConn
Imports master_new.ModFunction

Public Class FPartnerAllBaru
    Dim ssql As String
    Dim _ptnr_oid As String
    Public dt_bantu As DataTable
    'Public _sc_pp_ptnr_ptnrg_id As Integer
    Public func_data As New function_data
    Public func_coll As New function_collection
    Public ds_edit_address, ds_edit_cp As DataSet

    Private Sub FPartnerAllBaru_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()

        de_ptnr_birthday.EditValue = Now.Date
        de_ptnr_bp_date.EditValue = Now.Date

        AddHandler gv_edit_address.FocusedRowChanged, AddressOf relation_detail
        AddHandler gv_edit_address.ColumnFilterChanged, AddressOf relation_detail

        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
        init_le(par_entity, "en_mstr")

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_partner_type())
        par_type.Properties.DataSource = dt_bantu
        par_type.Properties.DisplayMember = dt_bantu.Columns("display").ToString
        par_type.Properties.ValueMember = dt_bantu.Columns("value").ToString
        par_type.ItemIndex = 0

    End Sub

    Public Overrides Sub load_cb()
        init_le(le_ptnr_sex, "Jenis_Kelamin")
        init_le(le_ptnr_goldarah, "gol_darah")
        init_le(le_ptnr_bp_type, "bp_type")
        init_le(le_ptnr_negara, "WNegara")

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cu_mstr())
        ptnr_cu_id.Properties.DataSource = dt_bantu
        ptnr_cu_id.Properties.DisplayMember = dt_bantu.Columns("cu_name").ToString
        ptnr_cu_id.Properties.ValueMember = dt_bantu.Columns("cu_id").ToString
        ptnr_cu_id.ItemIndex = 0

        init_le(sc_le_ptnr_en_id, "en_mstr")
        init_le(ptnr_ac_ar_id, "account")
        init_le(ptnr_ac_ap_id, "account")

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_periode_mstr_se())
        ptnr_start_periode.Properties.DataSource = dt_bantu
        ptnr_start_periode.Properties.DisplayMember = dt_bantu.Columns("seperiode_code").ToString
        ptnr_start_periode.Properties.ValueMember = dt_bantu.Columns("seperiode_code").ToString
        ptnr_start_periode.ItemIndex = 0

       End Sub

    Public Overrides Sub load_cb_en()
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_data("ptnrg_grp", sc_le_ptnr_en_id.EditValue))
        'sc_le_ptnr_ptnrg_id.Properties.DataSource = dt_bantu
        'sc_le_ptnr_ptnrg_id.Properties.DisplayMember = dt_bantu.Columns("ptnrg_name").ToString
        'sc_le_ptnr_ptnrg_id.Properties.ValueMember = dt_bantu.Columns("ptnrg_id").ToString
        'sc_le_ptnr_ptnrg_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sb_mstr(sc_le_ptnr_en_id.EditValue))
        ptnr_sb_ap_id.Properties.DataSource = dt_bantu
        ptnr_sb_ap_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        ptnr_sb_ap_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        ptnr_sb_ap_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_sb_mstr(sc_le_ptnr_en_id.EditValue))
        ptnr_sb_ar_id.Properties.DataSource = dt_bantu
        ptnr_sb_ar_id.Properties.DisplayMember = dt_bantu.Columns("sb_desc").ToString
        ptnr_sb_ar_id.Properties.ValueMember = dt_bantu.Columns("sb_id").ToString
        ptnr_sb_ar_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cc_mstr(sc_le_ptnr_en_id.EditValue))
        ptnr_cc_ap_id.Properties.DataSource = dt_bantu
        ptnr_cc_ap_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        ptnr_cc_ap_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        ptnr_cc_ap_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_cc_mstr(sc_le_ptnr_en_id.EditValue))
        ptnr_cc_ar_id.Properties.DataSource = dt_bantu
        ptnr_cc_ar_id.Properties.DisplayMember = dt_bantu.Columns("cc_desc").ToString
        ptnr_cc_ar_id.Properties.ValueMember = dt_bantu.Columns("cc_id").ToString
        ptnr_cc_ar_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(sc_le_ptnr_en_id.EditValue, "fakturpajak_transactioncode"))
        ptnr_transaction_code_id.Properties.DataSource = dt_bantu
        ptnr_transaction_code_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        ptnr_transaction_code_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        ptnr_transaction_code_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_level())
        ptnr_lvl_id.Properties.DataSource = dt_bantu
        ptnr_lvl_id.Properties.DisplayMember = dt_bantu.Columns("lvl_name").ToString
        ptnr_lvl_id.Properties.ValueMember = dt_bantu.Columns("lvl_id").ToString
        ptnr_lvl_id.ItemIndex = 0


    End Sub

    Private Sub sc_le_ptnr_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sc_le_ptnr_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Faktur Pajak Trans. Code", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Faktur Pajak Trans. Name", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "ID", "ptnr_id", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Name", "ptnr_user_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Branch Manager", "ptnr_is_bm", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Tax Name", "ptnr_name_alt", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Group", "ptnrg_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "URL", "ptnr_url", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Email", "ptnr_email", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "NPWP", "ptnr_npwp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Contact Tax", "ptnr_contact_tax", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Address Tax", "ptnr_address_tax", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "NPPKP", "ptnr_nppkp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Remarks", "ptnr_remarks", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Customer", "ptnr_is_cust", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Vendor", "ptnr_is_vend", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Member", "ptnr_is_member", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Employee", "ptnr_is_emp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Writer", "ptnr_is_writer", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is Personal Sales", "ptnr_is_ps", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Is Volunteer", "ptnr_is_volunteer", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Is SBM", "ptnr_is_sbm", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Parent", "parent_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Level", "lvl_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Start Periode", "ptnr_start_periode", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Bank", "ptnr_bank", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Number", "ptnr_no_rek", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Account Name", "ptnr_rek_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Birthday", "ptnr_birthday", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "Birth City", "ptnr_birthcity", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Kewarganegaraan", "ptnr_negara_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "KTP", "ptnr_ktp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Jenis Kelamin", "ptnr_sex_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Golongan Darah", "ptnr_goldarah_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Nama Ahli Waris", "ptnr_waris_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "KTP Ahli Waris", "ptnr_waris_ktp", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "BP Date", "ptnr_bp_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "BP Type", "ptnr_bp_name", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "AR Account Code", "ac_code_ar", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AR Account Name", "ac_name_ar", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AR Sub Account", "sb_desc_ar", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AR Cost Center", "cc_desc_ar", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AP Account Code", "ac_code_ap", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AP Account Name", "ac_name_ap", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AP Sub Account", "sb_desc_ap", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "AP Cost Center", "cc_desc_ap", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "IMEI", "ptnr_imei", DevExpress.Utils.HorzAlignment.Default)
        'ptnr_imei
        add_column_copy(gv_master, "Limit Credit", "ptnr_limit_credit", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master, "Prepayment Balance", "ptnr_prepaid_balance", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        add_column_copy(gv_master, "Is Active", "ptnr_active", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "User Create", "ptnr_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "ptnr_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "ptnr_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "ptnr_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail_address, "ptnra_ptnr_oid", False)
        add_column_copy(gv_detail_address, "Address Type", "address_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_address, "Line1", "ptnra_line_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_address, "Line2", "ptnra_line_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_address, "Line3", "ptnra_line_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_address, "Phone1", "ptnra_phone_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_address, "Phone2", "ptnra_phone_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_address, "Fax1", "ptnra_fax_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_address, "Fax2", "ptnra_fax_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_address, "Zip", "ptnra_zip", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_address, "Comment", "ptnra_comment", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_address, "Active", "ptnra_active", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_detail_cp, "ptnra_ptnr_oid", False)
        add_column_copy(gv_detail_cp, "Contact Person", "ptnrac_contact_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_cp, "Phone 1", "ptnrac_phone_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_cp, "Phone 2", "ptnrac_phone_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail_cp, "Email", "ptnrac_email", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_address, "ptnra_oid", False)
        add_column(gv_edit_address, "ptnra_addr_type", False)
        add_column(gv_edit_address, "Address Type", "address_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_address, "Line1", "ptnra_line_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_address, "Line2", "ptnra_line_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_address, "Line3", "ptnra_line_3", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_address, "Phone1", "ptnra_phone_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_address, "Phone2", "ptnra_phone_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_address, "Fax1", "ptnra_fax_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_address, "Fax2", "ptnra_fax_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_address, "Zip", "ptnra_zip", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_address, "Comment", "ptnra_comment", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_address, "Active", "ptnra_active", DevExpress.Utils.HorzAlignment.Default)

        add_column(gv_edit_cp, "ptnrac_oid", False)
        add_column(gv_edit_cp, "addrc_ptnra_oid", False)
        add_column(gv_edit_cp, "ptnrac_function", False)
        add_column(gv_edit_cp, "Function", "ptnrac_function_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_cp, "Contact Person", "ptnrac_contact_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_cp, "Phone 1", "ptnrac_phone_1", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_cp, "Phone 2", "ptnrac_phone_2", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit_cp, "Email", "ptnrac_email", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function export_data() As Boolean

        Dim ssql As String
        Try
            ssql = "SELECT  " _
                & "  trans.code_code, " _
                & "  trans.code_name, " _
                & "  a.ptnr_add_by, " _
                & "  a.ptnr_add_date, " _
                & "  a.ptnr_upd_by, " _
                & "  a.ptnr_upd_date, " _
                & "  a.ptnr_id, " _
                & "  a.ptnr_code, " _
                & "  a.ptnr_name,a.ptnr_address_tax,a.ptnr_contact_tax, " _
                & "  ptnrg_name, " _
                & "  ptnr_name_alt, " _
                & "  a.ptnr_url, " _
                & "  a.ptnr_email, " _
                & "  a.ptnr_remarks, " _
                & "  a.ptnr_parent, " _
                & "  a.ptnr_is_cust, " _
                & "  a.ptnr_is_vend, " _
                & "  a.ptnr_is_member, " _
                & "  a.ptnr_is_emp,coalesce(a.ptnr_is_ps,'N') as  ptnr_is_ps, " _
                & "  a.ptnr_is_writer, " _
                & "  a.ptnr_npwp, " _
                & "  a.ptnr_nppkp, " _
                & "  a.ptnr_active, " _
                & "  a.ptnr_ktp, " _
                & "  a.ptnr_sex, " _
                & "  a.ptnr_goldarah, " _
                & "  a.ptnr_birthcity, " _
                & "  a.ptnr_birthday, " _
                & "  a.ptnr_negara, " _
                & "  a.ptnr_bp_date, " _
                & "  a.ptnr_bp_type, " _
                & "  a.ptnr_waris_name, " _
                & "  a.ptnr_waris_ktp, " _
                & "  c.en_desc, " _
                & "  b.dom_desc, " _
                & "  ac_mstr_ar.ac_code as ac_code_ar, " _
                & "  ac_mstr_ar.ac_name as ac_name_ar, " _
                & "  sb_mstr_ar.sb_desc as sb_desc_ar, " _
                & "  cc_mstr_ar.cc_desc as cc_desc_ar, " _
                & "  ac_mstr_ap.ac_code as ac_code_ap, " _
                & "  ac_mstr_ap.ac_name as ac_name_ap, " _
                & "  sb_mstr_ap.sb_desc as sb_desc_ap, " _
                & "  cc_mstr_ap.cc_desc as cc_desc_ap, " _
                & "  ptnr_user_name,ptnr_is_bm, " _
                & "  cu_name,ptnr_start_periode, " _
                & "  ptnr_prepaid_balance, " _
                & "  a.ptnr_limit_credit, ptnr_lvl_id,ptnr_parent,lvl_name,(select x.ptnr_name from ptnr_mstr x where x.ptnr_id=a.ptnr_parent) as parent_name, " _
                & "  d.ptnra_line, " _
                & "  d.ptnra_line_1, " _
                & "  d.ptnra_line_2, " _
                & "  d.ptnra_line_3, " _
                & "  d.ptnra_phone_1, " _
                & "  d.ptnra_phone_2, " _
                & "  d.ptnra_fax_1, " _
                & "  d.ptnra_fax_2, " _
                & "  d.ptnra_zip, " _
                & "  d.ptnra_ptnr_oid, " _
                & "  d.ptnra_addr_type, " _
                & "  d.ptnra_comment, wn.code_name as ptnr_negara_name, " _
                & "  d.ptnra_active, sex.code_name as ptnr_sex_type, darah.code_name as ptnr_goldarah_name, bp.code_name as ptnr_bp_name " _
                & " FROM " _
                & "  public.ptnr_mstr a " _
                & "  INNER JOIN public.dom_mstr b ON (a.ptnr_dom_id = b.dom_id) " _
                & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                & "  INNER JOIN public.ptnrg_grp ON ptnrg_id = ptnr_ptnrg_id " _
                & "  INNER JOIN public.cu_mstr ON cu_id = ptnr_cu_id " _
                & "  INNER JOIN public.ac_mstr ac_mstr_ap ON ptnr_ac_ap_id = ac_mstr_ap.ac_id " _
                & "  INNER JOIN public.sb_mstr sb_mstr_ap ON ptnr_sb_ap_id = sb_mstr_ap.sb_id " _
                & "  INNER JOIN public.cc_mstr cc_mstr_ap ON ptnr_cc_ap_id = cc_mstr_ap.cc_id " _
                & "  INNER JOIN public.ac_mstr ac_mstr_ar ON ptnr_ac_ar_id = ac_mstr_ar.ac_id " _
                & "  INNER JOIN public.sb_mstr sb_mstr_ar ON ptnr_sb_ar_id = sb_mstr_ar.sb_id " _
                & "  INNER JOIN public.cc_mstr cc_mstr_ar ON ptnr_cc_ar_id = cc_mstr_ar.cc_id " _
                & "  left outer join public.code_mstr trans ON code_id = ptnr_transaction_code_id " _
                & "  left outer join public.code_mstr sex ON sex.code_id = a.ptnr_sex " _
                & "  left outer join public.code_mstr darah ON darah.code_id = a.ptnr_goldarah " _
                & "  left outer join public.code_mstr bp ON bp.code_id = a.ptnr_bp_type " _
                & "  left outer join public.code_mstr wn ON wn.code_id = a.ptnr_negara " _
                & "  left outer join public.pslvl_mstr ON lvl_id = ptnr_lvl_id " _
                & "  INNER JOIN public.ptnra_addr d ON (ptnr_oid = ptnra_ptnr_oid) " _
                & " where 1+1=2 "

            If par_entity.Text = "-" Then
                ssql = ssql & " and ptnr_en_id in (select user_en_id from tconfuserentity " _
                                           & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
            Else
                ssql = ssql & " and ptnr_en_id = " + par_entity.EditValue.ToString
            End If

            Dim sql_tambahan As String = ""

            If par_type.EditValue = "A" Then
                sql_tambahan = " "
            ElseIf par_type.EditValue = "V" Then
                ssql = ssql & " and ptnr_is_vend ~~* 'Y' " _
                             & " order by ptnr_name"
            ElseIf par_type.EditValue = "C" Then
                ssql = ssql & " and ptnr_is_cust ~~* 'Y' " _
                             & " order by ptnr_name"
            ElseIf par_type.EditValue = "M" Then
                ssql = ssql & " and ptnr_is_member ~~* 'Y' " _
                             & " order by ptnr_name"
            ElseIf par_type.EditValue = "E" Then
                ssql = ssql & " and ptnr_is_emp ~~* 'Y' " _
                            & " order by ptnr_name"
            ElseIf par_type.EditValue = "W" Then
                ssql = ssql & " and ptnr_is_writer ~~* 'Y' " _
                             & " order by ptnr_name"
            End If
            If export_to_excel(ssql) = False Then
                Return False
                Exit Function
            End If

        Catch ex As Exception
            Pesan(Err)
            Return False
        End Try

    End Function

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                & "  a.ptnr_oid, " _
                & "  a.ptnr_dom_id, " _
                & "  a.ptnr_en_id, " _
                & "  a.ptnr_transaction_code_id, " _
                & "  trans.code_code, " _
                & "  trans.code_name, " _
                & "  a.ptnr_add_by, " _
                & "  a.ptnr_add_date, " _
                & "  a.ptnr_upd_by, " _
                & "  a.ptnr_upd_date, " _
                & "  a.ptnr_id, " _
                & "  a.ptnr_code, " _
                & "  a.ptnr_name,a.ptnr_address_tax,a.ptnr_contact_tax, " _
                & "  a.ptnr_ptnrg_id, " _
                & "  ptnrg_name, " _
                & "  ptnr_name_alt, " _
                & "  a.ptnr_url, " _
                & "  a.ptnr_email, " _
                & "  a.ptnr_remarks, " _
                & "  a.ptnr_parent, " _
                & "  a.ptnr_is_cust, " _
                & "  a.ptnr_is_vend,ptnr_is_volunteer,ptnr_is_sbm, " _
                & "  a.ptnr_is_member, " _
                & "  a.ptnr_is_emp,coalesce(a.ptnr_is_ps,'N') as  ptnr_is_ps, " _
                & "  a.ptnr_is_writer, " _
                & "  a.ptnr_npwp, " _
                & "  a.ptnr_nppkp, " _
                & "  a.ptnr_active, " _
                & "  a.ptnr_dt, " _
                & "  a.ptnr_ktp, " _
                & "  a.ptnr_sex, " _
                & "  a.ptnr_goldarah, " _
                & "  a.ptnr_birthcity, " _
                & "  a.ptnr_birthday, " _
                & "  a.ptnr_negara, " _
                & "  a.ptnr_bp_date, " _
                & "  a.ptnr_bp_type, " _
                & "  a.ptnr_waris_name, wn.code_name as ptnr_negara_name, " _
                & "  a.ptnr_waris_ktp, sex.code_name as ptnr_sex_type, darah.code_name as ptnr_goldarah_name, bp.code_name as ptnr_bp_name, " _
                & "  c.en_desc, " _
                & "  b.dom_desc, " _
                & "  ac_mstr_ar.ac_code as ac_code_ar, " _
                & "  ac_mstr_ar.ac_name as ac_name_ar, " _
                & "  sb_mstr_ar.sb_desc as sb_desc_ar, " _
                & "  cc_mstr_ar.cc_desc as cc_desc_ar, " _
                & "  ac_mstr_ap.ac_code as ac_code_ap, " _
                & "  ac_mstr_ap.ac_name as ac_name_ap, " _
                & "  sb_mstr_ap.sb_desc as sb_desc_ap, " _
                & "  cc_mstr_ap.cc_desc as cc_desc_ap, " _
                & "  a.ptnr_ac_ar_id, " _
                & "  a.ptnr_sb_ar_id, " _
                & "  a.ptnr_cc_ar_id, " _
                & "  a.ptnr_ac_ap_id, " _
                & "  a.ptnr_sb_ap_id, " _
                & "  a.ptnr_cc_ap_id, " _
                & "  a.ptnr_cu_id,ptnr_user_name,ptnr_is_bm, " _
                & "  cu_name,ptnr_start_periode,ptnr_imei, " _
                & "  ptnr_prepaid_balance,ptnr_bank,ptnr_no_rek,ptnr_rek_name, " _
                & "  a.ptnr_limit_credit,ptnr_lvl_id,ptnr_parent,lvl_name,(select x.ptnr_name from ptnr_mstr x where x.ptnr_id=a.ptnr_parent) as parent_name " _
                & " FROM " _
                & "  public.ptnr_mstr a " _
                & "  INNER JOIN public.dom_mstr b ON (a.ptnr_dom_id = b.dom_id) " _
                & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                & "  INNER JOIN public.ptnrg_grp ON ptnrg_id = ptnr_ptnrg_id " _
                & "  INNER JOIN public.cu_mstr ON cu_id = ptnr_cu_id " _
                & "  INNER JOIN public.ac_mstr ac_mstr_ap ON ptnr_ac_ap_id = ac_mstr_ap.ac_id " _
                & "  INNER JOIN public.sb_mstr sb_mstr_ap ON ptnr_sb_ap_id = sb_mstr_ap.sb_id " _
                & "  INNER JOIN public.cc_mstr cc_mstr_ap ON ptnr_cc_ap_id = cc_mstr_ap.cc_id " _
                & "  INNER JOIN public.ac_mstr ac_mstr_ar ON ptnr_ac_ar_id = ac_mstr_ar.ac_id " _
                & "  INNER JOIN public.sb_mstr sb_mstr_ar ON ptnr_sb_ar_id = sb_mstr_ar.sb_id " _
                & "  INNER JOIN public.cc_mstr cc_mstr_ar ON ptnr_cc_ar_id = cc_mstr_ar.cc_id " _
                & "  left outer join public.code_mstr trans ON code_id = ptnr_transaction_code_id " _
                & "  left outer join public.code_mstr sex ON sex.code_id = a.ptnr_sex " _
                & "  left outer join public.code_mstr darah ON darah.code_id = a.ptnr_goldarah " _
                & "  left outer join public.code_mstr wn ON wn.code_id = a.ptnr_negara " _
                & "  left outer join public.code_mstr bp ON bp.code_id = a.ptnr_bp_type " _
                & "  left outer join public.pslvl_mstr ON lvl_id = ptnr_lvl_id " _
                & " where 1+1=2 "

        If par_entity.Text = "-" Then
            get_sequel = get_sequel & " and ptnr_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Else
            get_sequel = get_sequel & " and ptnr_en_id = " + par_entity.EditValue.ToString
        End If

        Dim sql_tambahan As String = ""

        If par_type.EditValue = "A" Then
            sql_tambahan = " "
        ElseIf par_type.EditValue = "V" Then
            sql_tambahan = " and ptnr_is_vend ~~* 'Y' " _
                         & " order by ptnr_name"
        ElseIf par_type.EditValue = "C" Then
            sql_tambahan = " and ptnr_is_cust ~~* 'Y' " _
                         & " order by ptnr_name"
        ElseIf par_type.EditValue = "M" Then
            sql_tambahan = " and ptnr_is_member ~~* 'Y' " _
                         & " order by ptnr_name"
        ElseIf par_type.EditValue = "E" Then
            sql_tambahan = " and ptnr_is_emp ~~* 'Y' " _
                        & " order by ptnr_name"
        ElseIf par_type.EditValue = "W" Then
            sql_tambahan = " and ptnr_is_writer ~~* 'Y' " _
                         & " order by ptnr_name"
        End If

        get_sequel = get_sequel + sql_tambahan

        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        sc_le_ptnr_en_id.ItemIndex = 0
        ptnr_transaction_code_id.ItemIndex = 0
        sc_te_ptnr_name.Text = ""
        sc_te_ptnr_url.Text = ""
        ptnr_npwp.Text = ""
        ptnr_name_alt.Text = ""
        ptnr_nppkp.Text = ""
        sc_te_ptnr_remarks.Text = ""
        'sc_le_ptnr_ptnrg_id.ItemIndex = 0 'par_entity

        sc_pp_ptnr_ptnrg_id.Text = ""
        sc_pp_ptnr_ptnrg_id.Enabled = True

        sc_ce_ptnr_active.EditValue = True
        sc_ce_ptnr_is_cust.EditValue = False
        sc_ce_ptnr_is_vend.EditValue = False
        ptnr_is_writer.EditValue = False
        ptnr_is_member.EditValue = False
        ptnr_is_emp.EditValue = False
        ptnr_cu_id.ItemIndex = 0
        ptnr_ac_ar_id.ItemIndex = 0
        ptnr_ac_ap_id.ItemIndex = 0
        ptnr_email.Text = ""
        sc_le_ptnr_en_id.Focus()
        ptnr_is_ps.EditValue = False
        ptnr_parent.Tag = ""
        ptnr_parent.EditValue = ""
        ptnr_lvl_id.EditValue = ""
        ptnr_user_name.EditValue = ""
        ptnr_is_bm.EditValue = False
        ptnr_imei.EditValue = ""
        ptnr_is_volunteer.EditValue = False
        ptnr_is_sbm.EditValue = False

        de_ptnr_birthday.EditValue = Date.Now
        de_ptnr_bp_date.EditValue = Date.Now
        te_ptnr_ktp.Text = ""
        te_ptnr_waris_ktp.Text = ""
        te_ptnr_waris_name.Text = ""
        le_ptnr_birthcity.Text = ""
        le_ptnr_bp_type.EditValue = False
        le_ptnr_goldarah.EditValue = False
        le_ptnr_negara.EditValue = False
        le_ptnr_sex.EditValue = False

        Try
            tcg_header.SelectedTabPageIndex = 0
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()

        ds_edit_address = New DataSet
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    .SQL = "SELECT  " _
                        & "  a.ptnra_oid, " _
                        & "  a.ptnra_id, " _
                        & "  a.ptnra_dom_id, " _
                        & "  a.ptnra_en_id, " _
                        & "  a.ptnra_add_by, " _
                        & "  a.ptnra_add_date, " _
                        & "  a.ptnra_upd_by, " _
                        & "  a.ptnra_upd_date, " _
                        & "  a.ptnra_line, " _
                        & "  a.ptnra_line_1, " _
                        & "  a.ptnra_line_2, " _
                        & "  a.ptnra_line_3, " _
                        & "  a.ptnra_phone_1, " _
                        & "  a.ptnra_phone_2, " _
                        & "  a.ptnra_fax_1, " _
                        & "  a.ptnra_fax_2, " _
                        & "  a.ptnra_zip, " _
                        & "  a.ptnra_ptnr_oid, " _
                        & "  a.ptnra_addr_type, " _
                        & "  a.ptnra_comment, " _
                        & "  a.ptnra_active, " _
                        & "  a.ptnra_dt, " _
                        & "  b.dom_desc, " _
                        & "  c.en_desc, " _
                        & "  code_name as address_type, " _
                        & "  public.ptnr_mstr.ptnr_name " _
                        & "FROM " _
                        & "  public.ptnra_addr a " _
                        & "  INNER JOIN public.dom_mstr b ON (a.ptnra_dom_id = b.dom_id) " _
                        & "  INNER JOIN public.en_mstr c ON (a.ptnra_en_id = c.en_id) " _
                        & "  INNER JOIN public.ptnr_mstr ON (a.ptnra_ptnr_oid = public.ptnr_mstr.ptnr_oid) " _
                        & "  inner join public.code_mstr on code_id = ptnra_addr_type " _
                        & " where ptnra_line = -99 "
                    .InitializeCommand()
                    .FillDataSet(ds_edit_address, "address")
                    gc_edit_address.DataSource = ds_edit_address.Tables(0)
                    gv_edit_address.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        ds_edit_cp = New DataSet
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    .SQL = "SELECT  " _
                        & "  a.ptnrac_oid, " _
                        & "  a.addrc_ptnra_oid, " _
                        & "  a.ptnrac_add_by, " _
                        & "  a.ptnrac_add_date, " _
                        & "  a.ptnrac_seq, " _
                        & "  a.ptnrac_function, " _
                        & "  a.ptnrac_contact_name, " _
                        & "  a.ptnrac_phone_1, " _
                        & "  a.ptnrac_phone_2, " _
                        & "  a.ptnrac_email, " _
                        & "  a.ptnrac_dt, " _
                        & "  b.ptnra_line, " _
                        & "  code_name as ptnrac_function_name, " _
                        & "  ptnra_ptnr_oid, " _
                        & "  ptnr_name " _
                        & "FROM " _
                        & "  public.ptnrac_cntc a " _
                        & "  INNER JOIN public.ptnra_addr b ON (a.addrc_ptnra_oid = b.ptnra_oid)" _
                        & "  Inner join public.ptnr_mstr on ptnr_oid = ptnra_ptnr_oid " _
                        & "  Inner join public.code_mstr on code_id = ptnrac_function " _
                        & " where ptnrac_seq = -99"
                    .InitializeCommand()
                    .FillDataSet(ds_edit_cp, "contactperson")
                    gc_edit_cp.DataSource = ds_edit_cp.Tables(0)
                    gv_edit_cp.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Function

    Private Function GetID_Local(ByVal par_en_code As String) As Integer
        Try
            Using objcb As New master_new.CustomCommand
                With objcb
                    '.Connection.Open()
                    '.Command = .Connection.CreateCommand
                    '.Command.CommandType = CommandType.Text

                    .Command.CommandText = "select coalesce(max(cast(substring(cast(ptnr_id as varchar),5,100) as integer)),0) as max_col  from ptnr_mstr " + _
                                           " where substring(cast(ptnr_id as varchar),5,100) <> ''"
                    .InitializeCommand()

                    .DataReader = .ExecuteReader
                    While .DataReader.Read

                        GetID_Local = .DataReader("max_col") + 1
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        If par_en_code = "0" Then
            par_en_code = "99"
        End If

        GetID_Local = CInt(par_en_code + master_new.ClsVar.sServerCode + GetID_Local.ToString)

        Return GetID_Local
    End Function

    Public Overrides Function insert() As Boolean
        Dim _ptnr_oid As Guid
        _ptnr_oid = Guid.NewGuid

        Dim _ptnr_code As String = ""
        Dim _ptnr_id, i As Integer
        Dim ssqls As New ArrayList

        '_ptnr_id = SetInteger(func_coll.GetID("ptnr_mstr", sc_le_ptnr_en_id.GetColumnValue("en_code"), "ptnr_id", "ptnr_en_id", sc_le_ptnr_en_id.EditValue.ToString))
        _ptnr_id = SetInteger(GetID_Local(sc_le_ptnr_en_id.GetColumnValue("en_code")))

        If sc_ce_ptnr_is_cust.EditValue = True Then
            _ptnr_code = _ptnr_code + "CU"
        End If
        If sc_ce_ptnr_is_vend.EditValue = True Then
            _ptnr_code = _ptnr_code + "SP"
        End If
        If ptnr_is_member.EditValue = True Then
            _ptnr_code = _ptnr_code + "SL"
        End If
        If ptnr_is_emp.EditValue = True Then
            _ptnr_code = _ptnr_code + "EM"
        End If
        If ptnr_is_writer.EditValue = True Then
            _ptnr_code = _ptnr_code + "WR"
        End If

        If Len(_ptnr_code) = 2 Then
            _ptnr_code = _ptnr_code + "00"
        End If

        Dim _ptnr_id_s As String = _ptnr_id.ToString.Substring(4, Len(_ptnr_id.ToString) - 4)

        'If Len(_ptnr_id_s) = 1 Then
        '    _ptnr_id_s = "000000" + _ptnr_id_s.ToString
        'ElseIf Len(_ptnr_id_s) = 2 Then
        '    _ptnr_id_s = "00000" + _ptnr_id_s.ToString
        'ElseIf Len(_ptnr_id_s) = 3 Then
        '    _ptnr_id_s = "0000" + _ptnr_id_s.ToString
        'ElseIf Len(_ptnr_id_s) = 4 Then
        '    _ptnr_id_s = "000" + _ptnr_id_s.ToString
        'ElseIf Len(_ptnr_id_s) = 5 Then
        '    _ptnr_id_s = "00" + _ptnr_id_s.ToString
        'ElseIf Len(_ptnr_id_s) = 6 Then
        '    _ptnr_id_s = "0" + _ptnr_id_s.ToString
        'ElseIf Len(_ptnr_id_s) = 7 Then
        '    _ptnr_id_s = _ptnr_id_s.ToString
        'End If

        If Len(_ptnr_id_s) = 1 Then
            _ptnr_id_s = master_new.ClsVar.sServerCode + "0000" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 2 Then
            _ptnr_id_s = master_new.ClsVar.sServerCode + "000" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 3 Then
            _ptnr_id_s = master_new.ClsVar.sServerCode + "00" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 4 Then
            _ptnr_id_s = master_new.ClsVar.sServerCode + "0" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 5 Then
            _ptnr_id_s = master_new.ClsVar.sServerCode + _ptnr_id_s.ToString
        End If

        _ptnr_code = _ptnr_code + IIf(sc_le_ptnr_en_id.GetColumnValue("en_code") = 0, "99", sc_le_ptnr_en_id.GetColumnValue("en_code")) + _ptnr_id_s.ToString

        Try
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        '.Command = .Connection.CreateCommand
                        '.Command.Transaction = sqlTran

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.ptnr_mstr " _
                                            & "( " _
                                            & "  ptnr_oid, " _
                                            & "  ptnr_dom_id, " _
                                            & "  ptnr_en_id, " _
                                            & "  ptnr_add_by, " _
                                            & "  ptnr_add_date, " _
                                            & "  ptnr_id, " _
                                            & "  ptnr_code,ptnr_address_tax,ptnr_contact_tax, " _
                                            & "  ptnr_name, " _
                                            & "  ptnr_name_alt, " _
                                            & "  ptnr_ptnrg_id, " _
                                            & "  ptnr_url, " _
                                            & "  ptnr_email, " _
                                            & "  ptnr_npwp, " _
                                            & "  ptnr_nppkp, " _
                                            & "  ptnr_remarks, " _
                                            & "  ptnr_is_cust, " _
                                            & "  ptnr_is_vend, " _
                                            & "  ptnr_is_member, " _
                                            & "  ptnr_is_emp, " _
                                            & "  ptnr_is_writer, " _
                                            & "  ptnr_ac_ar_id, " _
                                            & "  ptnr_sb_ar_id, " _
                                            & "  ptnr_cc_ar_id, " _
                                            & "  ptnr_ac_ap_id, " _
                                            & "  ptnr_sb_ap_id, " _
                                            & "  ptnr_cc_ap_id,ptnr_imei, " _
                                            & "  ptnr_cu_id,ptnr_bank,ptnr_no_rek,ptnr_rek_name, " _
                                            & "  ptnr_limit_credit,ptnr_user_name,ptnr_is_bm, " _
                                            & "  ptnr_active,ptnr_is_volunteer,ptnr_is_sbm, " _
                                            & "  ptnr_transaction_code_id,ptnr_is_ps,ptnr_lvl_id,ptnr_parent,ptnr_start_periode, " _
                                            & "  ptnr_dt, " _
                                            & "  ptnr_ktp, " _
                                            & "  ptnr_sex, " _
                                            & "  ptnr_goldarah, " _
                                            & "  ptnr_birthcity, " _
                                            & "  ptnr_birthday, " _
                                            & "  ptnr_negara, " _
                                            & "  ptnr_bp_date, " _
                                            & "  ptnr_bp_type, " _
                                            & "  ptnr_waris_name, " _
                                            & "  ptnr_waris_ktp " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(_ptnr_oid.ToString) & ",  " _
                                            & SetSetring(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetSetring(sc_le_ptnr_en_id.EditValue) & ",  " _
                                            & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                            & " " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                            & _ptnr_id & ",  " _
                                            & SetSetring(_ptnr_code) & ",  " _
                                            & SetSetring(ptnr_address_tax.Text) & ",  " _
                                            & SetSetring(ptnr_contact_tax.Text) & ",  " _
                                            & SetSetring(sc_te_ptnr_name.Text) & ",  " _
                                            & SetSetring(ptnr_name_alt.Text) & ",  " _
                                            & SetSetring(sc_pp_ptnr_ptnrg_id.Text) & ",  " _
                                            & SetSetring(sc_te_ptnr_url.Text) & ",  " _
                                            & SetSetring(ptnr_email.Text) & ",  " _
                                            & SetSetring(ptnr_npwp.Text) & ",  " _
                                            & SetSetring(ptnr_nppkp.Text) & ",  " _
                                            & SetSetring(sc_te_ptnr_remarks.Text) & ",  " _
                                            & SetBitYN(sc_ce_ptnr_is_cust.EditValue) & ",  " _
                                            & SetBitYN(sc_ce_ptnr_is_vend.EditValue) & ",  " _
                                            & SetBitYN(ptnr_is_member.EditValue) & ",  " _
                                            & SetBitYN(ptnr_is_emp.EditValue) & ",  " _
                                            & SetBitYN(ptnr_is_writer.EditValue) & ",  " _
                                            & SetInteger(ptnr_ac_ar_id.EditValue) & ",  " _
                                            & SetInteger(ptnr_sb_ar_id.EditValue) & ",  " _
                                            & SetInteger(ptnr_cc_ar_id.EditValue) & ",  " _
                                            & SetInteger(ptnr_ac_ap_id.EditValue) & ",  " _
                                            & SetInteger(ptnr_sb_ap_id.EditValue) & ",  " _
                                            & SetInteger(ptnr_cc_ap_id.EditValue) & ",  " _
                                            & SetSetring(ptnr_imei.EditValue) & ",  " _
                                            & SetInteger(ptnr_cu_id.EditValue) & ",  " _
                                            & SetSetring(ptnr_bank.EditValue) & ",  " _
                                            & SetSetring(ptnr_no_rek.EditValue) & ",  " _
                                            & SetSetring(ptnr_rek_name.EditValue) & ",  " _
                                            & SetDbl(ptnr_limit_credit.EditValue) & ",  " _
                                            & SetSetring(ptnr_user_name.EditValue) & ",  " _
                                            & SetBitYN(ptnr_is_bm.EditValue) & ",  " _
                                            & SetBitYN(sc_ce_ptnr_active.EditValue) & ",  " _
                                            & SetBitYN(ptnr_is_volunteer.EditValue) & ",  " _
                                            & SetBitYN(ptnr_is_sbm.EditValue) & ",  " _
                                            & SetInteger(ptnr_transaction_code_id.EditValue) & ",  " _
                                            & SetBitYN(ptnr_is_ps.EditValue) & ",  " _
                                            & SetInteger(ptnr_lvl_id.EditValue) & ",  " _
                                            & SetInteger(ptnr_parent.Tag) & ",  " _
                                            & SetSetring(ptnr_start_periode.EditValue) & ",  " _
                                            & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ",  " _
                                            & SetSetring(te_ptnr_ktp.Text) & ",  " _
                                            & SetInteger(le_ptnr_sex.EditValue) & ",  " _
                                            & SetInteger(le_ptnr_goldarah.EditValue) & ",  " _
                                            & SetSetring(le_ptnr_birthcity.Text) & ",  " _
                                            & SetDate(de_ptnr_birthday.Text) & ",  " _
                                            & SetInteger(le_ptnr_negara.EditValue) & ",  " _
                                            & SetDate(de_ptnr_bp_date.Text) & ",  " _
                                            & SetInteger(le_ptnr_bp_type.EditValue) & ",  " _
                                            & SetSetring(te_ptnr_waris_name.Text) & ",  " _
                                            & SetSetring(te_ptnr_waris_ktp.Text) & "  " _
                                            & ")"

                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        For i = 0 To ds_edit_address.Tables(0).Rows.Count - 1
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.ptnra_addr " _
                                                & "( " _
                                                & "  ptnra_oid, " _
                                                & "  ptnra_id, " _
                                                & "  ptnra_dom_id, " _
                                                & "  ptnra_en_id, " _
                                                & "  ptnra_add_by, " _
                                                & "  ptnra_add_date, " _
                                                & "  ptnra_line, " _
                                                & "  ptnra_line_1, " _
                                                & "  ptnra_line_2, " _
                                                & "  ptnra_line_3, " _
                                                & "  ptnra_phone_1, " _
                                                & "  ptnra_phone_2, " _
                                                & "  ptnra_fax_1, " _
                                                & "  ptnra_fax_2, " _
                                                & "  ptnra_zip, " _
                                                & "  ptnra_ptnr_oid, " _
                                                & "  ptnra_addr_type, " _
                                                & "  ptnra_comment, " _
                                                & "  ptnra_active, " _
                                                & "  ptnra_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_address.Tables(0).Rows(i).Item("ptnra_oid").ToString) & ",  " _
                                                & SetInteger(func_coll.GetID("ptnra_addr", sc_le_ptnr_en_id.GetColumnValue("en_code"), "ptnra_id", "ptnra_en_id", sc_le_ptnr_en_id.EditValue.ToString)) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(sc_le_ptnr_en_id.EditValue) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                & SetInteger(func_coll.GetID("ptnra_addr", sc_le_ptnr_en_id.GetColumnValue("en_code"), "ptnra_line", "ptnra_ptnr_oid", _ptnr_oid.ToString)) & ",  " _
                                                & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_line_1")) & ",  " _
                                                & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_line_2")) & ",  " _
                                                & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_line_3")) & ",  " _
                                                & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_phone_1")) & ",  " _
                                                & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_phone_2")) & ",  " _
                                                & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_fax_1")) & ",  " _
                                                & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_fax_2")) & ",  " _
                                                & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_zip")) & ",  " _
                                                & SetSetring(_ptnr_oid.ToString) & ",  " _
                                                & SetInteger(ds_edit_address.Tables(0).Rows(i).Item("ptnra_addr_type")) & ",  " _
                                                & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_comment")) & ",  " _
                                                & SetSetring(ds_edit_address.Tables(0).Rows(i).Item("ptnra_active").ToString.ToUpper) & ",  " _
                                                & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()
                        Next

                        For i = 0 To ds_edit_cp.Tables(0).Rows.Count - 1
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.ptnrac_cntc " _
                                                & "( " _
                                                & "  ptnrac_oid, " _
                                                & "  addrc_ptnra_oid, " _
                                                & "  ptnrac_add_by, " _
                                                & "  ptnrac_add_date, " _
                                                & "  ptnrac_seq, " _
                                                & "  ptnrac_function, " _
                                                & "  ptnrac_contact_name, " _
                                                & "  ptnrac_phone_1, " _
                                                & "  ptnrac_phone_2, " _
                                                & "  ptnrac_email, " _
                                                & "  ptnrac_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_cp.Tables(0).Rows(i).Item("ptnrac_oid").ToString) & ",  " _
                                                & SetSetring(ds_edit_cp.Tables(0).Rows(i).Item("addrc_ptnra_oid")) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & ",  " _
                                                & SetInteger(GetNewID("ptnrac_cntc", "ptnrac_seq", "addrc_ptnra_oid", ds_edit_cp.Tables(0).Rows(i).Item("addrc_ptnra_oid").ToString)) & ",  " _
                                                & SetInteger(ds_edit_cp.Tables(0).Rows(i).Item("ptnrac_function")) & ",  " _
                                                & SetSetringDB(ds_edit_cp.Tables(0).Rows(i).Item("ptnrac_contact_name")) & ",  " _
                                                & SetSetringDB(ds_edit_cp.Tables(0).Rows(i).Item("ptnrac_phone_1")) & ",  " _
                                                & SetSetringDB(ds_edit_cp.Tables(0).Rows(i).Item("ptnrac_phone_2")) & ",  " _
                                                & SetSetringDB(ds_edit_cp.Tables(0).Rows(i).Item("ptnrac_email")) & ",  " _
                                                & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()
                        Next

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Insert Partner Complete " & _ptnr_code)
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()
                            Next
                        End If

                        .Command.Commit()
                        after_success()
                        set_row(Trim(_ptnr_oid.ToString), "ptnr_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        insert = True
                    Catch ex As PgSqlException
                        'sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        insert = False
                    End Try
                End With
            End Using
        Catch ex As Exception
            row = 0
            insert = False
            MessageBox.Show(ex.Message)
        End Try
        Return insert
    End Function

    Public Overrides Function edit_data() As Boolean
        If MyBase.edit_data = True Then
            sc_le_ptnr_en_id.Focus()

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)
                _ptnr_oid = .Item("ptnr_oid")
                'sc_le_ptnr_en_id.EditValue = .Item("ptnr_en_id")
                sc_pp_ptnr_ptnrg_id.Text = SetString(.Item("ptnr_name_sold"))
                'sc_pp_ptnr_ptnrg_id.EditValue = .Item("ptnr_ptnrg_id")
                sc_te_ptnr_name.Text = SetString(.Item("ptnr_name"))
                ptnr_user_name.EditValue = .Item("ptnr_user_name")
                ptnr_is_bm.EditValue = SetBitYNB(.Item("ptnr_is_bm"))
                ptnr_no_rek.EditValue = .Item("ptnr_no_rek")
                ptnr_rek_name.EditValue = .Item("ptnr_rek_name")
                ptnr_bank.EditValue = .Item("ptnr_bank")
                ptnr_name_alt.Text = SetString(.Item("ptnr_name_alt"))
                sc_te_ptnr_url.Text = SetString(.Item("ptnr_url"))
                ptnr_email.Text = SetString(.Item("ptnr_email"))
                ptnr_npwp.Text = SetString(.Item("ptnr_npwp"))
                ptnr_nppkp.Text = SetString(.Item("ptnr_nppkp"))
                ptnr_address_tax.Text = SetString(.Item("ptnr_address_tax"))
                ptnr_contact_tax.Text = SetString(.Item("ptnr_contact_tax"))
                sc_te_ptnr_remarks.Text = SetString(.Item("ptnr_remarks"))
                sc_ce_ptnr_active.EditValue = SetBitYNB(.Item("ptnr_active"))
                sc_ce_ptnr_is_cust.EditValue = SetBitYNB(.Item("ptnr_is_cust"))
                sc_ce_ptnr_is_vend.EditValue = SetBitYNB(.Item("ptnr_is_vend"))

                ptnr_is_volunteer.EditValue = SetBitYNB(.Item("ptnr_is_volunteer"))
                ptnr_is_sbm.EditValue = SetBitYNB(.Item("ptnr_is_sbm"))

                ptnr_is_member.EditValue = SetBitYNB(.Item("ptnr_is_member"))
                ptnr_is_ps.EditValue = SetBitYNB(.Item("ptnr_is_ps"))
                ptnr_is_emp.EditValue = SetBitYNB(.Item("ptnr_is_emp"))
                ptnr_is_writer.EditValue = SetBitYNB(.Item("ptnr_is_writer"))
                ptnr_ac_ar_id.EditValue = .Item("ptnr_ac_ar_id")
                ptnr_sb_ar_id.EditValue = .Item("ptnr_sb_ar_id")
                ptnr_cc_ar_id.EditValue = .Item("ptnr_cc_ar_id")
                ptnr_ac_ap_id.EditValue = .Item("ptnr_ac_ap_id")
                ptnr_sb_ap_id.EditValue = .Item("ptnr_sb_ap_id")
                ptnr_cc_ap_id.EditValue = .Item("ptnr_cc_ap_id")
                ptnr_cu_id.EditValue = .Item("ptnr_cu_id")
                ptnr_limit_credit.EditValue = .Item("ptnr_limit_credit")
                ptnr_is_ps.EditValue = SetBitYNB(.Item("ptnr_is_ps"))
                ptnr_lvl_id.EditValue = .Item("ptnr_lvl_id")
                ptnr_parent.Tag = .Item("ptnr_parent")
                ptnr_parent.EditValue = .Item("parent_name")
                ptnr_start_periode.EditValue = .Item("ptnr_start_periode")
                ptnr_imei.EditValue = .Item("ptnr_imei")


                te_ptnr_ktp.Text = SetString(.Item("ptnr_ktp"))
                le_ptnr_sex.EditValue = .Item("ptnr_sex")
                le_ptnr_goldarah.EditValue = .Item("ptnr_goldarah")
                le_ptnr_birthcity.Text = SetString(.Item("ptnr_birthcity"))
                de_ptnr_birthday.EditValue = SetString(.Item("ptnr_birthday"))
                le_ptnr_negara.EditValue = .Item("ptnr_negara")
                de_ptnr_bp_date.EditValue = SetString(.Item("ptnr_bp_date"))
                le_ptnr_bp_type.EditValue = .Item("ptnr_bp_type")
                te_ptnr_waris_name.Text = SetString(.Item("ptnr_waris_name"))
                te_ptnr_waris_ktp.Text = SetString(.Item("ptnr_waris_ktp"))
                If IsDBNull(.Item("ptnr_transaction_code_id")) = True Then
                    ptnr_transaction_code_id.ItemIndex = 0
                Else
                    ptnr_transaction_code_id.EditValue = .Item("ptnr_transaction_code_id")
                End If
            End With

            ds_edit_address = New DataSet
            Try
                Using objcb As New master_new.CustomCommand
                    With objcb
                        .SQL = "SELECT  " _
                            & "  a.ptnra_oid, " _
                            & "  a.ptnra_id, " _
                            & "  a.ptnra_dom_id, " _
                            & "  a.ptnra_en_id, " _
                            & "  a.ptnra_add_by, " _
                            & "  a.ptnra_add_date, " _
                            & "  a.ptnra_upd_by, " _
                            & "  a.ptnra_upd_date, " _
                            & "  a.ptnra_line, " _
                            & "  a.ptnra_line_1, " _
                            & "  a.ptnra_line_2, " _
                            & "  a.ptnra_line_3, " _
                            & "  a.ptnra_phone_1, " _
                            & "  a.ptnra_phone_2, " _
                            & "  a.ptnra_fax_1, " _
                            & "  a.ptnra_fax_2, " _
                            & "  a.ptnra_zip, " _
                            & "  a.ptnra_ptnr_oid, " _
                            & "  a.ptnra_addr_type, " _
                            & "  a.ptnra_comment, " _
                            & "  a.ptnra_active, " _
                            & "  a.ptnra_dt, " _
                            & "  b.dom_desc, " _
                            & "  c.en_desc, " _
                            & "  code_name as address_type, " _
                            & "  public.ptnr_mstr.ptnr_name " _
                            & "FROM " _
                            & "  public.ptnra_addr a " _
                            & "  INNER JOIN public.dom_mstr b ON (a.ptnra_dom_id = b.dom_id) " _
                            & "  INNER JOIN public.en_mstr c ON (a.ptnra_en_id = c.en_id) " _
                            & "  INNER JOIN public.ptnr_mstr ON (a.ptnra_ptnr_oid = public.ptnr_mstr.ptnr_oid) " _
                            & "  inner join public.code_mstr on code_id = ptnra_addr_type " _
                            & " where ptnra_ptnr_oid = '" + _ptnr_oid + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_edit_address, "address")
                        gc_edit_address.DataSource = ds_edit_address.Tables(0)
                        gv_edit_address.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            ds_edit_cp = New DataSet
            Try
                Using objcb As New master_new.CustomCommand
                    With objcb
                        .SQL = "SELECT  " _
                            & "  a.ptnrac_oid, " _
                            & "  a.addrc_ptnra_oid, " _
                            & "  a.ptnrac_add_by, " _
                            & "  a.ptnrac_add_date, " _
                            & "  a.ptnrac_seq, " _
                            & "  a.ptnrac_function, " _
                            & "  a.ptnrac_contact_name, " _
                            & "  a.ptnrac_phone_1, " _
                            & "  a.ptnrac_phone_2, " _
                            & "  a.ptnrac_email, " _
                            & "  a.ptnrac_dt, " _
                            & "  b.ptnra_line, " _
                            & "  code_name as ptnrac_function_name, " _
                            & "  ptnra_ptnr_oid, " _
                            & "  ptnr_name " _
                            & "FROM " _
                            & "  public.ptnrac_cntc a " _
                            & "  INNER JOIN public.ptnra_addr b ON (a.addrc_ptnra_oid = b.ptnra_oid)" _
                            & "  Inner join public.ptnr_mstr on ptnr_oid = ptnra_ptnr_oid " _
                            & "  Inner join public.code_mstr on code_id = ptnrac_function " _
                            & " where ptnr_oid = '" + _ptnr_oid + "'"
                        .InitializeCommand()
                        .FillDataSet(ds_edit_cp, "contactperson")
                        gc_edit_cp.DataSource = ds_edit_cp.Tables(0)
                        gv_edit_cp.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            Try
                tcg_header.SelectedTabPageIndex = 0
            Catch ex As Exception
            End Try

            edit_data = True
        End If
    End Function

    Public Overrides Function edit()
        edit = True

        Dim _ptnr_code As String = ""
        Dim _ptnr_id As Integer = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnr_id")
        Dim i As Integer
        Dim ssqls As New ArrayList

        If sc_ce_ptnr_is_cust.EditValue = True Then
            _ptnr_code = _ptnr_code + "CU"
        End If
        If sc_ce_ptnr_is_vend.EditValue = True Then
            _ptnr_code = _ptnr_code + "SP"
        End If
        If ptnr_is_member.EditValue = True Then
            _ptnr_code = _ptnr_code + "SL"
        End If
        If ptnr_is_emp.EditValue = True Then
            _ptnr_code = _ptnr_code + "EM"
        End If
        If ptnr_is_writer.EditValue = True Then
            _ptnr_code = _ptnr_code + "WR"
        End If

        If Len(_ptnr_code) = 2 Then
            _ptnr_code = _ptnr_code + "00"
        End If

        Dim _ptnr_id_s As String = _ptnr_id.ToString.Substring(2, Len(_ptnr_id.ToString) - 2)

        If Len(_ptnr_id_s) = 1 Then
            _ptnr_id_s = "000000" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 2 Then
            _ptnr_id_s = "00000" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 3 Then
            _ptnr_id_s = "0000" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 4 Then
            _ptnr_id_s = "000" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 5 Then
            _ptnr_id_s = "00" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 6 Then
            _ptnr_id_s = "0" + _ptnr_id_s.ToString
        ElseIf Len(_ptnr_id_s) = 7 Then
            _ptnr_id_s = _ptnr_id_s.ToString
        End If

        _ptnr_code = _ptnr_code + IIf(sc_le_ptnr_en_id.GetColumnValue("en_code") = 0, "99", sc_le_ptnr_en_id.GetColumnValue("en_code")) + _ptnr_id_s.ToString

        Try
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        '.Command = .Connection.CreateCommand
                        '.Command.Transaction = sqlTran

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                                & "  public.ptnr_mstr  " _
                                                & "SET  " _
                                                & "  ptnr_en_id = " & SetInteger(sc_le_ptnr_en_id.EditValue) & ",  " _
                                                & "  ptnr_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "  ptnr_upd_date = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & " " & ",  " _
                                                & "  ptnr_code = " & SetSetring(_ptnr_code) & ",  " _
                                                & "  ptnr_name = " & SetSetring(sc_te_ptnr_name.Text) & ",  " _
                                                & "  ptnr_user_name = " & SetSetring(ptnr_user_name.Text) & ",  " _
                                                & "  ptnr_is_bm = " & SetBitYN(ptnr_is_bm.EditValue) & ",  " _
                                                & "  ptnr_name_alt = " & SetSetring(ptnr_name_alt.Text) & ",  " _
                                                 & "  ptnr_bank = " & SetSetring(ptnr_bank.Text) & ",  " _
                                                  & "  ptnr_no_rek = " & SetSetring(ptnr_no_rek.Text) & ",  " _
                                                   & "  ptnr_rek_name = " & SetSetring(ptnr_rek_name.Text) & ",  " _
                                                & "  ptnr_ptnrg_id = " & sc_pp_ptnr_ptnrg_id.EditValue & ",  " _
                                                & "  ptnr_url = " & SetSetring(sc_te_ptnr_url.Text) & ",  " _
                                                & "  ptnr_email = " & SetSetring(ptnr_email.Text) & ",  " _
                                                & "  ptnr_npwp = " & SetSetring(ptnr_npwp.Text) & ",  " _
                                                & "  ptnr_address_tax = " & SetSetring(ptnr_address_tax.Text) & ",  " _
                                                & "  ptnr_contact_tax = " & SetSetring(ptnr_contact_tax.Text) & ",  " _
                                                & "  ptnr_nppkp = " & SetSetring(ptnr_nppkp.Text) & ",  " _
                                                & "  ptnr_transaction_code_id = " & SetInteger(ptnr_transaction_code_id.EditValue) & ",  " _
                                                & "  ptnr_remarks = " & SetSetring(sc_te_ptnr_remarks.Text) & ",  " _
                                                & "  ptnr_imei = " & SetSetring(ptnr_imei.Text) & ",  " _
                                                & "  ptnr_is_cust = " & SetBitYN(sc_ce_ptnr_is_cust.EditValue) & ",  " _
                                                & "  ptnr_is_vend = " & SetBitYN(sc_ce_ptnr_is_vend.EditValue) & ",  " _
                                                & "  ptnr_is_member = " & SetBitYN(ptnr_is_member.EditValue) & ",  " _
                                                & "  ptnr_is_sbm = " & SetBitYN(ptnr_is_sbm.EditValue) & ",  " _
                                                & "  ptnr_is_volunteer = " & SetBitYN(ptnr_is_volunteer.EditValue) & ",  " _
                                                & "  ptnr_is_ps = " & SetBitYN(ptnr_is_ps.EditValue) & ",  " _
                                                & "  ptnr_is_emp = " & SetBitYN(ptnr_is_emp.EditValue) & ",  " _
                                                & "  ptnr_is_writer = " & SetBitYN(ptnr_is_writer.EditValue) & ",  " _
                                                & "  ptnr_active = " & SetBitYN(sc_ce_ptnr_active.EditValue) & ",  " _
                                                & "  ptnr_ac_ar_id = " & ptnr_ac_ar_id.EditValue & ",  " _
                                                & "  ptnr_sb_ar_id = " & ptnr_sb_ar_id.EditValue & ",  " _
                                                & "  ptnr_cc_ar_id = " & ptnr_cc_ar_id.EditValue & ",  " _
                                                & "  ptnr_ac_ap_id = " & ptnr_ac_ap_id.EditValue & ",  " _
                                                & "  ptnr_sb_ap_id = " & ptnr_sb_ap_id.EditValue & ",  " _
                                                & "  ptnr_cc_ap_id = " & ptnr_cc_ap_id.EditValue & ",  " _
                                                & "  ptnr_parent = " & SetInteger(ptnr_parent.Tag) & ",  " _
                                                & "  ptnr_lvl_id = " & SetInteger(ptnr_lvl_id.EditValue) & ",  " _
                                                & "  ptnr_cu_id = " & ptnr_cu_id.EditValue & ",  " _
                                                & "  ptnr_ktp =  " & SetSetring(te_ptnr_ktp.Text) & ", " _
                                                & "  ptnr_sex =  " & SetInteger(le_ptnr_sex.EditValue) & ", " _
                                                & "  ptnr_goldarah = " & SetInteger(le_ptnr_goldarah.EditValue) & ",  " _
                                                & "  ptnr_birthcity = " & SetSetring(le_ptnr_birthcity.Text) & ",  " _
                                                & "  ptnr_birthday = " & SetDate(de_ptnr_birthday.Text) & ",  " _
                                                & "  ptnr_negara = " & SetInteger(le_ptnr_negara.EditValue) & ",  " _
                                                & "  ptnr_bp_date = " & SetDate(de_ptnr_bp_date.Text) & ",  " _
                                                & "  ptnr_bp_type = " & SetInteger(le_ptnr_bp_type.EditValue) & ",  " _
                                                & "  ptnr_waris_name = " & SetSetring(te_ptnr_waris_name.Text) & ",  " _
                                                & "  ptnr_waris_ktp = " & SetSetring(te_ptnr_waris_ktp.Text) & ", " _
                                                & "  ptnr_start_periode = " & SetSetring(ptnr_start_periode.EditValue) & ",  " _
                                                & "  ptnr_limit_credit = " & SetDbl(ptnr_limit_credit.EditValue) & ",  " _
                                                & "  ptnr_dt = " & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                & "  " _
                                                & "WHERE  " _
                                                & "  ptnr_oid = '" & _ptnr_oid & "' "
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from ptnra_addr where ptnra_ptnr_oid = '" + _ptnr_oid.ToString + "'"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        For i = 0 To ds_edit_address.Tables(0).Rows.Count - 1
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.ptnra_addr " _
                                                    & "( " _
                                                    & "  ptnra_oid, " _
                                                    & "  ptnra_id, " _
                                                    & "  ptnra_dom_id, " _
                                                    & "  ptnra_en_id, " _
                                                    & "  ptnra_add_by, " _
                                                    & "  ptnra_add_date, " _
                                                    & "  ptnra_line, " _
                                                    & "  ptnra_line_1, " _
                                                    & "  ptnra_line_2, " _
                                                    & "  ptnra_line_3, " _
                                                    & "  ptnra_phone_1, " _
                                                    & "  ptnra_phone_2, " _
                                                    & "  ptnra_fax_1, " _
                                                    & "  ptnra_fax_2, " _
                                                    & "  ptnra_zip, " _
                                                    & "  ptnra_ptnr_oid, " _
                                                    & "  ptnra_addr_type, " _
                                                    & "  ptnra_comment, " _
                                                    & "  ptnra_active, " _
                                                    & "  ptnra_dt " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(ds_edit_address.Tables(0).Rows(i).Item("ptnra_oid").ToString) & ",  " _
                                                    & SetInteger(func_coll.GetID("ptnra_addr", sc_le_ptnr_en_id.GetColumnValue("en_code"), "ptnra_id", "ptnra_en_id", sc_le_ptnr_en_id.EditValue.ToString)) & ",  " _
                                                    & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                    & SetInteger(sc_le_ptnr_en_id.EditValue) & ",  " _
                                                    & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & ",  " _
                                                    & SetInteger(func_coll.GetID("ptnra_addr", sc_le_ptnr_en_id.GetColumnValue("en_code"), "ptnra_line", "ptnra_ptnr_oid", _ptnr_oid.ToString)) & ",  " _
                                                    & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_line_1")) & ",  " _
                                                    & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_line_2")) & ",  " _
                                                    & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_line_3")) & ",  " _
                                                    & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_phone_1")) & ",  " _
                                                    & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_phone_2")) & ",  " _
                                                    & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_fax_1")) & ",  " _
                                                    & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_fax_2")) & ",  " _
                                                    & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_zip")) & ",  " _
                                                    & SetSetring(_ptnr_oid.ToString) & ",  " _
                                                    & SetInteger(ds_edit_address.Tables(0).Rows(i).Item("ptnra_addr_type")) & ",  " _
                                                    & SetSetringDB(ds_edit_address.Tables(0).Rows(i).Item("ptnra_comment")) & ",  " _
                                                    & SetSetring(ds_edit_address.Tables(0).Rows(i).Item("ptnra_active").ToString.ToUpper) & ",  " _
                                                    & "" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & "" & "  " _
                                                    & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()
                        Next

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "delete from ptnrac_cntc where addrc_ptnra_oid in (select ptnra_oid from ptnra_addr " + _
                                                                                                " where ptnra_ptnr_oid = '" + _ptnr_oid.ToString + "')"
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        For i = 0 To ds_edit_cp.Tables(0).Rows.Count - 1
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.ptnrac_cntc " _
                                                & "( " _
                                                & "  ptnrac_oid, " _
                                                & "  addrc_ptnra_oid, " _
                                                & "  ptnrac_add_by, " _
                                                & "  ptnrac_add_date, " _
                                                & "  ptnrac_seq, " _
                                                & "  ptnrac_function, " _
                                                & "  ptnrac_contact_name, " _
                                                & "  ptnrac_phone_1, " _
                                                & "  ptnrac_phone_2, " _
                                                & "  ptnrac_email, " _
                                                & "  ptnrac_dt " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(ds_edit_cp.Tables(0).Rows(i).Item("ptnrac_oid").ToString) & ",  " _
                                                & SetSetring(ds_edit_cp.Tables(0).Rows(i).Item("addrc_ptnra_oid")) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & ",  " _
                                                & SetInteger(GetNewID("ptnrac_cntc", "ptnrac_seq", "addrc_ptnra_oid", ds_edit_cp.Tables(0).Rows(i).Item("addrc_ptnra_oid").ToString)) & ",  " _
                                                & SetInteger(ds_edit_cp.Tables(0).Rows(i).Item("ptnrac_function")) & ",  " _
                                                & SetSetringDB(ds_edit_cp.Tables(0).Rows(i).Item("ptnrac_contact_name")) & ",  " _
                                                & SetSetringDB(ds_edit_cp.Tables(0).Rows(i).Item("ptnrac_phone_1")) & ",  " _
                                                & SetSetringDB(ds_edit_cp.Tables(0).Rows(i).Item("ptnrac_phone_2")) & ",  " _
                                                & SetSetringDB(ds_edit_cp.Tables(0).Rows(i).Item("ptnrac_email")) & ",  " _
                                                & "(" & SetDateNTime(master_new.PGSqlConn.CekTanggal) & ")" & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()
                        Next


                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = insert_log("Edit Partner Complete " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnr_code"))
                        ssqls.Add(.Command.CommandText)
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        If master_new.PGSqlConn.status_sync = True Then
                            For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                                '.Command.CommandType = CommandType.Text
                                .Command.CommandText = Data
                                .Command.ExecuteNonQuery()
                                '.Command.Parameters.Clear()
                            Next
                        End If

                        .Command.Commit()
                        after_success()
                        set_row(Trim(_ptnr_oid.ToString), "ptnr_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        edit = True
                    Catch ex As PgSqlException
                        'sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                        edit = False
                    End Try
                End With
            End Using
        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
    End Function

    Public Overrides Function delete_data() As Boolean

        delete_data = True
        If ds.Tables.Count = 0 Then
            delete_data = False
            Exit Function
        ElseIf ds.Tables(0).Rows.Count = 0 Then
            delete_data = False
            Exit Function
        End If

        Dim ssqls As New ArrayList

        If MessageBox.Show("Yakin " + master_new.ClsVar.sNama + " Hapus Data Ini..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        If before_delete() = True Then
            row = BindingContext(ds.Tables(0)).Position

            If row = BindingContext(ds.Tables(0)).Count - 1 And BindingContext(ds.Tables(0)).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(ds.Tables(0)).Count = 1 Then
                row = 0
            End If

            Try
                Using objinsert As New master_new.CustomCommand
                    With objinsert
.Command.Open()
                        ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                        Try
                            '.Command = .Connection.CreateCommand
                            '.Command.Transaction = sqlTran

                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "delete from ptnr_mstr where ptnr_oid = '" + ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnr_oid") + "'"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = insert_log("Delete Partner Complete " & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnr_code"))
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            If master_new.PGSqlConn.status_sync = True Then
                                For Each Data As String In master_new.ModFunction.FinsertSQL2Array(ssqls)
                                    '.Command.CommandType = CommandType.Text
                                    .Command.CommandText = Data
                                    .Command.ExecuteNonQuery()
                                    '.Command.Parameters.Clear()
                                Next
                            End If

                            .Command.Commit()

                            help_load_data(True)
                            MessageBox.Show("Data Telah Berhasil Di Hapus..", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Catch ex As PgSqlException
                            'sqlTran.Rollback()
                            MessageBox.Show(ex.Message)
                        End Try
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        Return delete_data
    End Function

    Private Sub gv_edit_address_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_address.DoubleClick
        Dim _col As String = gv_edit_address.FocusedColumn.Name
        Dim _row As Integer = gv_edit_address.FocusedRowHandle

        If _col = "address_type" Then
            Dim frm As New FAddressTypeSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = sc_le_ptnr_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_cp_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit_cp.DoubleClick
        Dim _col As String = gv_edit_cp.FocusedColumn.Name
        Dim _row As Integer = gv_edit_cp.FocusedRowHandle

        If _col = "ptnrac_function_name" Then
            Dim frm As New FFunctionCPSearch
            frm.set_win(Me)
            frm._row = _row
            frm._en_id = sc_le_ptnr_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

    Private Sub gv_edit_address_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit_address.InitNewRow
        With gv_edit_address
            .SetRowCellValue(e.RowHandle, "ptnra_oid", Guid.NewGuid.ToString)
        End With
    End Sub

    Private Sub gv_edit_cp_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs) Handles gv_edit_cp.InitNewRow
        With gv_edit_cp
            .SetRowCellValue(e.RowHandle, "ptnrac_oid", Guid.NewGuid.ToString)
            .SetRowCellValue(e.RowHandle, "addrc_ptnra_oid", ds_edit_address.Tables(0).Rows(BindingContext(ds_edit_address.Tables(0)).Position).Item("ptnra_oid").ToString)
        End With
    End Sub

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True
        gv_edit_address.UpdateCurrentRow()
        gv_edit_cp.UpdateCurrentRow()

        ds_edit_address.AcceptChanges()
        ds_edit_cp.AcceptChanges()

        Dim i, j As Integer
        i = 0
        j = 0
        Dim _ptnra_oid As String = ""
        Dim _status As Boolean = False

        If ds_edit_address.Tables(0).Rows.Count = 0 Then
            MessageBox.Show("Address Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        For i = 0 To ds_edit_address.Tables(0).Rows.Count - 1
            _ptnra_oid = ds_edit_address.Tables(0).Rows(i).Item("ptnra_oid").ToString

            If SetString(ds_edit_address.Tables(0).Rows(i).Item("ptnra_line_1")) = "" Then
                MessageBox.Show("Address Column Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            If SetString(ds_edit_address.Tables(0).Rows(i).Item("ptnra_phone_1")) = "" Then
                MessageBox.Show("Phone Column Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If

            For j = 0 To ds_edit_cp.Tables(0).Rows.Count - 1
                If ds_edit_cp.Tables(0).Rows(j).Item("addrc_ptnra_oid") = _ptnra_oid Then
                    _status = True
                    Exit For
                End If
            Next

            If _status = False Then
                MessageBox.Show("Contact Person Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
        Next

        Return before_save
    End Function

    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        gv_master_SelectionChanged(Nothing, Nothing)
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        Try
            If ds.Tables(0).Rows.Count = 0 Then
                Exit Sub
            End If

            Dim sql As String = ""

            Dim sql_tambahan As String = ""

            If par_type.EditValue = "A" Then
                sql_tambahan = " "
            ElseIf par_type.EditValue = "V" Then
                sql_tambahan = " and ptnr_is_vend ~~* 'Y' " _
                            & " order by ptnr_name"
            ElseIf par_type.EditValue = "C" Then
                sql_tambahan = " and ptnr_is_cust ~~* 'Y' " _
                             & " order by ptnr_name"
            ElseIf par_type.EditValue = "M" Then
                sql_tambahan = " and ptnr_is_member ~~* 'Y' " _
                             & " order by ptnr_name"
            ElseIf par_type.EditValue = "E" Then
                sql_tambahan = " and ptnr_is_emp ~~* 'Y' " _
                            & " order by ptnr_name"
            ElseIf par_type.EditValue = "W" Then
                sql_tambahan = " and ptnr_is_writer ~~* 'Y' " _
                             & " order by ptnr_name"
            End If

            Try
                ds.Tables("address").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                & "  a.ptnra_oid, " _
                & "  a.ptnra_id, " _
                & "  a.ptnra_dom_id, " _
                & "  a.ptnra_en_id, " _
                & "  a.ptnra_add_by, " _
                & "  a.ptnra_add_date, " _
                & "  a.ptnra_upd_by, " _
                & "  a.ptnra_upd_date, " _
                & "  a.ptnra_line, " _
                & "  a.ptnra_line_1, " _
                & "  a.ptnra_line_2, " _
                & "  a.ptnra_line_3, " _
                & "  a.ptnra_phone_1, " _
                & "  a.ptnra_phone_2, " _
                & "  a.ptnra_fax_1, " _
                & "  a.ptnra_fax_2, " _
                & "  a.ptnra_zip, " _
                & "  a.ptnra_ptnr_oid, " _
                & "  a.ptnra_addr_type, " _
                & "  a.ptnra_comment, " _
                & "  a.ptnra_active, " _
                & "  a.ptnra_dt, " _
                & "  b.dom_desc, " _
                & "  c.en_desc, " _
                & "  code_name as address_type, " _
                & "  public.ptnr_mstr.ptnr_name " _
                & "FROM " _
                & "  public.ptnra_addr a " _
                & "  INNER JOIN public.dom_mstr b ON (a.ptnra_dom_id = b.dom_id) " _
                & "  INNER JOIN public.en_mstr c ON (a.ptnra_en_id = c.en_id) " _
                & "  INNER JOIN public.ptnr_mstr ON (a.ptnra_ptnr_oid = public.ptnr_mstr.ptnr_oid) " _
                & "  inner join public.code_mstr on code_id = ptnra_addr_type " _
                & " where ptnra_ptnr_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnr_oid").ToString & "'"


            sql = sql + sql_tambahan
            load_data_detail(sql, gc_detail_address, "address")

            Try
                ds.Tables("contactperson").Clear()
            Catch ex As Exception
            End Try

            sql = "SELECT  " _
                    & "  a.ptnrac_oid, " _
                    & "  a.addrc_ptnra_oid, " _
                    & "  a.ptnrac_add_by, " _
                    & "  a.ptnrac_add_date, " _
                    & "  a.ptnrac_seq, " _
                    & "  a.ptnrac_function, " _
                    & "  a.ptnrac_contact_name, " _
                    & "  a.ptnrac_phone_1, " _
                    & "  a.ptnrac_phone_2, " _
                    & "  a.ptnrac_email, " _
                    & "  a.ptnrac_dt, " _
                    & "  b.ptnra_line, " _
                    & "  code_name as ptnrac_function_name, " _
                    & "  ptnra_ptnr_oid, " _
                    & "  ptnr_name " _
                    & "FROM " _
                    & "  public.ptnrac_cntc a " _
                    & "  INNER JOIN public.ptnra_addr b ON (a.addrc_ptnra_oid = b.ptnra_oid)" _
                    & "  Inner join public.ptnr_mstr on ptnr_oid = ptnra_ptnr_oid " _
                    & "  Inner join public.code_mstr on code_id = ptnrac_function " _
                    & " Where ptnra_ptnr_oid='" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnr_oid").ToString & "'"

            sql = sql + sql_tambahan
            load_data_detail(sql, gc_detail_cp, "contactperson")

            sql = "select a.ptnr_id, a.ptnr_parent,a.ptnr_is_ps,a.ptnr_active, a.ptnr_name ,b.lvl_name  from ptnr_mstr a " _
                & "  left outer join public.pslvl_mstr b ON lvl_id = ptnr_lvl_id " _
                & " where ptnr_id in " _
                & " ( select menu_id from get_all_child(" _
                & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnr_id").ToString _
                & ")) or ptnr_id in (" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("ptnr_id").ToString & ") "

            Dim dt_tree As New DataTable
            dt_tree = GetTableData(sql)

            TreeList1.DataSource = dt_tree
            TreeList1.ExpandAll()

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub sc_pp_ptnr_ptnrg_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles sc_pp_ptnr_ptnrg_id.ButtonClick
        Try
            Dim frm As New FGroupPartnerSearch
            frm.set_win(Me)
            frm._obj = ptnr_parent
            frm.type_form = True
            frm._en_id = sc_le_ptnr_en_id.EditValue
            frm.ShowDialog()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub ptnr_parent_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles ptnr_parent.ButtonClick
        Try
            Dim frm As New FPartnerSearch
            frm.set_win(Me)
            frm._obj = ptnr_parent
            frm.type_form = True
            frm._en_id = sc_le_ptnr_en_id.EditValue
            frm.ShowDialog()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub btClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btClear.Click
        Try
            ptnr_parent.EditValue = ""
            ptnr_parent.Tag = ""
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub TreeList1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeList1.DoubleClick
        Try
            TreeList1.ExportToXls(master_new.ModFunction.AskSaveAsFile("Excel Files | *.xls"))
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

End Class
