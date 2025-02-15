﻿Imports master_new.ModFunction

Public Class FPartnerVendorSearch
    Public _row As Integer
    Public _en_id, _par_ptnr_id, _ptnrg_id As Integer
    Public _obj As Object

    Dim func_data As New function_data
    Dim dt_bantu As DataTable

    Private Sub FPartnerVendorSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        load_cb()
        help_load_data(True)

        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

        If fobject.name = FPartnerTaxGroup.Name Then
            scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Both
            gv_master.Columns("status").Visible = True
        Else
            scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
            gv_master.Columns("status").Visible = False
        End If
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        par_entity.Properties.DataSource = dt_bantu
        par_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        par_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        par_entity.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_edit(gv_master, "#", "status", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "ptnr_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        'If fobject.name = FSalesQuotationConsigmentAlocated.Name Then
        '    add_column(gv_master, "Sales Area", "area_name", DevExpress.Utils.HorzAlignment.Default)
        'End If
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""

        Dim _en_id_coll As String = func_data.entity_parent(_en_id)


        If fobject.name = "FCashOut" Or fobject.name = FVoucher.Name Or FPartnerAll.Name = FVoucher.Name Then

            get_sequel = "SELECT  " _
                   & "  a.ptnr_oid, " _
                   & "  a.ptnr_dom_id, " _
                   & "  a.ptnr_en_id, " _
                   & "  a.ptnr_id, " _
                   & "  a.ptnr_code, " _
                   & "  a.ptnr_name, " _
                   & "  a.ptnr_bank, " _
                   & "  a.ptnr_no_rek, " _
                   & "  a.ptnr_rek_name, " _
                   & "  en_desc, " _
                   & "  ptnr_ac_ar_id, " _
                   & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                   & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                   & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                   & "FROM " _
                   & "  public.ptnr_mstr a " _
                   & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                   & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                   & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                   & " where ptnr_en_id  in (" + _en_id_coll + ")" _
                   & " and ptnr_active ~~* 'Y' and ptnr_is_vend ~~* 'Y' " _
                   & " order by ptnr_name"

        ElseIf fobject.name = FRequisition.Name Then
            get_sequel = "SELECT  " _
                   & "  a.ptnr_oid, " _
                   & "  a.ptnr_dom_id, " _
                   & "  a.ptnr_en_id, " _
                   & "  a.ptnr_id, " _
                   & "  a.ptnr_code, " _
                   & "  a.ptnr_name, " _
                   & "  en_desc, " _
                   & "  ptnr_ac_ar_id, " _
                   & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                   & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                   & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                   & "FROM " _
                   & "  public.ptnr_mstr a " _
                   & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                   & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                   & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                   & " where ptnr_en_id  in (" + _en_id_coll + ")" _
                   & " and ptnr_active ~~* 'Y' and ptnr_is_emp ~~* 'Y' " _
                   & " order by ptnr_name"

        ElseIf fobject.name = FPartnerAll.Name Then

            get_sequel = "SELECT  " _
                   & "  a.ptnr_oid, " _
                   & "  a.ptnr_dom_id, " _
                   & "  a.ptnr_en_id, " _
                   & "  a.ptnr_id, " _
                   & "  a.ptnr_code, " _
                   & "  a.ptnr_name, " _
                   & "  a.ptnr_sex, a.ptnr_ktp," _
                   & "  a.ptnr_goldarah, " _
                   & "  a.ptnr_birthcity, " _
                   & "  a.ptnr_birthday, " _
                   & "  a.ptnr_negara, " _
                   & "  a.ptnr_bp_date, " _
                   & "  a.ptnr_bp_type, " _
                   & "  a.ptnr_waris_name, " _
                   & "  a.ptnr_waris_ktp, " _
                   & "  en_desc " _
                   & "FROM " _
                   & "  public.ptnr_mstr a " _
                   & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                   & " where  ptnr_active ~~* 'Y'  " _
                   & " order by ptnr_name"

            'ptnr_en_id  in (" + _en_id_coll + ")" _
            '       & " and

        ElseIf fobject.name = "FCashIn" Or fobject.name = FLocation.Name Or fobject.name = FARMerge.Name Or fobject.name = FSOShipMerge.Name Or fobject.name = FDPackingSheet.Name Or fobject.name = FDPackingSheetNew.Name Then
            'ElseIf fobject.name = "FCashIn" Or fobject.name = FLocation.Name Or fobject.name = FARMerge.Name Or fobject.name = FARMergeByShipment.Name Or fobject.name = FSOShipMerge.Name Then
            get_sequel = "SELECT  " _
                   & "  a.ptnr_oid, " _
                   & "  a.ptnr_dom_id, " _
                   & "  a.ptnr_en_id, " _
                   & "  a.ptnr_id, " _
                   & "  a.ptnr_code, " _
                   & "  a.ptnr_name, " _
                   & "  en_desc, " _
                   & "  ptnr_ac_ar_id, " _
                   & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                   & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                   & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                   & "FROM " _
                   & "  public.ptnr_mstr a " _
                   & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                   & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                   & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                   & " where ptnr_en_id  in (" + _en_id_coll + ")" _
                   & " and ptnr_active ~~* 'Y' and ptnr_is_cust ~~* 'Y' " _
                   & " order by ptnr_name"

        ElseIf fobject.name = FInventoryReturns.Name Then
            get_sequel = "SELECT  " _
                   & "  a.ptnr_oid, " _
                   & "  a.ptnr_dom_id, " _
                   & "  a.ptnr_en_id, " _
                   & "  a.ptnr_id, " _
                   & "  a.ptnr_code, " _
                   & "  a.ptnr_name, " _
                   & "  en_desc, " _
                   & "  ptnr_ac_ar_id, " _
                   & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                   & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                   & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                   & "FROM " _
                   & "  public.ptnr_mstr a " _
                   & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                   & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                   & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                   & " where ptnr_en_id  = " + _en_id.ToString + " " _
                   & " and ptnr_active ~~* 'Y' and ptnr_is_cust ~~* 'Y' " _
                   & " order by ptnr_name"

        ElseIf fobject.name = FDBPointGroup.Name Then
            get_sequel = "SELECT a.ptnr_oid, " _
                    & "a.ptnr_dom_id, " _
                    & "a.ptnr_en_id, " _
                    & "a.ptnr_id, " _
                    & "a.ptnr_code, " _
                    & "a.ptnr_name, " _
                    & "a.ptnr_ptnrg_id, " _
                    & "b.ptnrg_name, " _
                    & "c.en_desc " _
                    & "FROM public.ptnr_mstr a " _
                    & "INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id) " _
                    & "INNER JOIN public.ptnrg_grp b ON (a.ptnr_ptnrg_id = b.ptnrg_id)" _
                   & " where ptnr_en_id  = " + _en_id.ToString + " " _
                   & " and ptnr_ptnrg_id  = " + _ptnrg_id.ToString + " " _
                   & " and ptnr_active ~~* 'Y' and ptnr_is_cust ~~* 'Y' " _
                   & " order by ptnr_name"

            'ElseIf fobject.name = "FARMergeByShipment" Then
            '    get_sequel = "SELECT  " _
            '           & "  a.ptnr_oid, " _
            '           & "  a.ptnr_dom_id, " _
            '           & "  a.ptnr_en_id, " _
            '           & "  a.ptnr_id, " _
            '           & "  a.ptnr_code, " _
            '           & "  a.ptnr_name, " _
            '           & "  en_desc, " _
            '           & "  ptnr_ac_ar_id, " _
            '           & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
            '           & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
            '           & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
            '           & "FROM " _
            '           & "  public.ptnr_mstr a " _
            '           & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
            '           & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
            '           & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
            '           & " where ptnr_en_id  in (" + _en_id + ")" _
            '           & " and ptnr_active ~~* 'Y' and ptnr_is_cust ~~* 'Y' " _
            '           & " order by ptnr_name"

        ElseIf fobject.name = "FDRCRMemo" Then
            get_sequel = "SELECT  " _
                   & "  a.ptnr_oid, " _
                   & "  a.ptnr_dom_id, " _
                   & "  a.ptnr_en_id, " _
                   & "  a.ptnr_id, " _
                   & "  a.ptnr_code, " _
                   & "  a.ptnr_name, " _
                   & "  en_desc, " _
                   & "  ptnr_ac_ar_id, " _
                   & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                   & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                   & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                   & "FROM " _
                   & "  public.ptnr_mstr a " _
                   & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                   & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                   & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                   & " where ptnr_en_id  in (select user_en_id from tconfuserentity " _
                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                   & " and ptnr_active ~~* 'Y' " _
                   & " and ptnr_is_cust ~~* 'Y' " _
                   & " order by ptnr_name"

        ElseIf fobject.name = "FPartNumberSKUGenerator" Then
            get_sequel = "SELECT  " _
                   & "  a.ptnr_oid, " _
                   & "  a.ptnr_dom_id, " _
                   & "  a.ptnr_en_id, " _
                   & "  a.ptnr_id, " _
                   & "  a.ptnr_code, " _
                   & "  a.ptnr_name, " _
                   & "  en_desc, " _
                   & "  ptnr_ac_ar_id, " _
                   & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                   & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                   & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                   & "FROM " _
                   & "  public.ptnr_mstr a " _
                   & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                   & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                   & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                   & " where ptnr_en_id  in (select user_en_id from tconfuserentity " _
                   & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                   & " and ptnr_active ~~* 'Y' " _
                   & " and ptnr_is_vend ~~* 'Y' " _
                   & " order by ptnr_name"

        ElseIf fobject.name = FPartnerTaxGroup.Name Then
            get_sequel = "SELECT  false as status, " _
                    & "  a.ptnr_oid, " _
                    & "  a.ptnr_dom_id, " _
                    & "  a.ptnr_en_id, " _
                    & "  a.ptnr_id, " _
                    & "  a.ptnr_code, " _
                    & "  a.ptnr_name, " _
                    & "  en_desc, " _
                    & "  ptnr_ac_ar_id, " _
                    & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                    & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                    & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                    & "FROM " _
                    & "  public.ptnr_mstr a " _
                    & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                    & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                    & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                    & " where ptnr_en_id = " + par_entity.EditValue.ToString _
                    & " and ptnr_active ~~* 'Y' " _
                    & " and ptnr_is_cust ~~* 'Y' " _
                    & " order by ptnr_name"

        ElseIf fobject.name = "FSalesQuotation" Or fobject.name = FSalesQuotationConsigment.Name Or fobject.name = FSalesQuotationConsigmentAloc.Name Then
            get_sequel = "SELECT  " _
                    & "  ptnr_oid, " _
                    & "  ptnr_dom_id, " _
                    & "  ptnr_en_id, " _
                    & "  ptnr_id, " _
                    & "  ptnr_code, " _
                    & "  ptnr_name, ptnr_email, " _
                    & "  ptnr_sex, ptnr_ktp, " _
                    & "  ptnr_goldarah, " _
                    & "  ptnr_birthcity, " _
                    & "  ptnr_birthday, " _
                    & "  ptnr_negara, " _
                    & "  ptnr_bp_date, " _
                    & "  ptnr_bp_type, " _
                    & "  ptnr_waris_name, " _
                    & "  ptnr_waris_ktp, " _
                    & "  en_desc, " _
                    & "  ptnr_area_id, " _
                    & "  area_name, " _
                    & "  ptnr_ac_ar_id, " _
                    & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                    & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                    & "  coalesce(ptnra_line_3,'') as ptnra_line_3, " _
                    & "  coalesce(ptnra_phone_1,'') as ptnra_phone_1, " _
                    & "  coalesce(ptnra_phone_2,'') as ptnra_phone_2, " _
                    & "  coalesce(ptnra_zip,'') as ptnra_zip, " _
                    & "  ptnratt_oid, " _
                    & "  ptnratt_kjb_code, " _
                    & "  ptnratt_bekerja_pada, " _
                    & "  ptnratt_jabatan_bagian, " _
                    & "  ptnratt_kantor_alamat_1, " _
                    & "  ptnratt_kantor_alamat_2, " _
                    & "  ptnratt_kantor_lantai, " _
                    & "  ptnratt_kantor_telp, " _
                    & "  ptnratt_ktp, " _
                    & "  ptnratt_email, " _
                    & "  ptnratt_rumah_alamat_1, " _
                    & "  ptnratt_rumah_alamat_2, " _
                    & "  ptnratt_rumah_kode_pos, " _
                    & "  ptnratt_rumah_telp, " _
                    & "  ptnratt_rumah_hp, " _
                    & "  ptnratt_status_alamat_kirim, " _
                    & "  ptnratt_status_alamat_tagih, " _
                    & "  ptnratt_suami_nama, " _
                    & "  ptnratt_suami_bekerja, " _
                    & "  ptnratt_suami_jabatan, " _
                    & "  ptnratt_suami_kantor_alamat_1, " _
                    & "  ptnratt_suami_kantor_alamat_2, " _
                    & "  ptnratt_suami_telp, " _
                    & "  ptnratt_suami_hp, " _
                    & "  ptnratt_anak_nama_1, " _
                    & "  ptnratt_anak_tgl_lahir_1, " _
                    & "  ptnratt_anak_sekolah_1, " _
                    & "  ptnratt_anak_nama_2, " _
                    & "  ptnratt_anak_tgl_lahir_2, " _
                    & "  ptnratt_anak_sekolah_2, " _
                    & "  ptnratt_anak_nama_3, " _
                    & "  ptnratt_anak_tgl_lahir_3, " _
                    & "  ptnratt_anak_sekolah_3, " _
                    & "  ptnratt_keluarga_dekat_nama, " _
                    & "  ptnratt_keluarga_dekat_alamat_1, " _
                    & "  ptnratt_keluarga_dekat_alamat_2, " _
                    & "  ptnratt_keluarga_dekat_telp, " _
                    & "  ptnratt_keluarga_dekat_hp, " _
                    & "  ptnratt_status_tempat_tinggal, " _
                    & "  ptnratt_jenis_kartu_kredit, " _
                    & "  ptnratt_no_kartu_kredit, " _
                    & "  ptnratt_berlaku_sd, " _
                    & "  ptnratt_dt, " _
                    & "  ptnratt_bank, " _
                    & "  ptnratt_status_rumah_id, " _
                    & "  ptnratt_lama_tinggal_id, " _
                    & "  ptnratt_masa_kerja_id, " _
                    & "  ptnratt_income_id, " _
                    & "  ptnratt_kepribadian_id, " _
                    & "  ptnratt_tanggungan_id, " _
                    & "  ptnratt_jaminan_id, " _
                    & "  sr.code_name as status_rumah, " _
                    & "  lt.code_name as lama_tinggal, " _
                    & "  ms.code_name as masa_kerja, " _
                    & "  ic.code_name as income, " _
                    & "  kp.code_name as kepribadian, " _
                    & "  tg.code_name as tanggungan, " _
                    & "  jm.code_name as jaminan " _
                    & "FROM " _
                    & "  public.ptnr_mstr " _
                    & "  INNER JOIN public.en_mstr ON (ptnr_en_id = en_id)" _
                    & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                    & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                    & "  INNER JOIN public.area_mstr on area_id = ptnr_area_id " _
                    & "  LEFT OUTER JOIN public.ptnratt_attr on ptnratt_ptnr_oid = ptnr_oid " _
                    & "  LEFT OUTER JOIN public.code_mstr sr on sr.code_id = ptnratt_status_rumah_id " _
                    & "  LEFT OUTER JOIN public.code_mstr lt on lt.code_id = ptnratt_lama_tinggal_id " _
                    & "  LEFT OUTER JOIN public.code_mstr ms on ms.code_id = ptnratt_masa_kerja_id " _
                    & "  LEFT OUTER JOIN public.code_mstr ic on ic.code_id = ptnratt_income_id " _
                    & "  LEFT OUTER JOIN public.code_mstr kp on kp.code_id = ptnratt_kepribadian_id " _
                    & "  LEFT OUTER JOIN public.code_mstr tg on tg.code_id = ptnratt_tanggungan_id " _
                    & "  LEFT OUTER JOIN public.code_mstr jm on jm.code_id = ptnratt_jaminan_id " _
                    & " where ptnr_en_id  in (" + _en_id_coll + ")" _
                    & " and ptnr_active ~~* 'Y' " _
                    & " and ptnr_is_cust ~~* 'Y' " _
                    & " order by ptnr_name"

        ElseIf fobject.name = "FSalesQuotationConsigmentAlocated" Or fobject.name = FSalesOrderAlocated.Name Then
            get_sequel = "SELECT ptnr_oid, " _
                    & "       ptnr_dom_id, " _
                    & "       ptnr_en_id, " _
                    & "       ptnr_id, " _
                    & "       ptnr_code, " _
                    & "       ptnr_name, " _
                    & "       ptnr_ptnrg_id, " _
                    & "       ptnrg_name, " _
                    & "       ptnr_email, " _
                    & "       ptnr_sex, " _
                    & "       ptnr_ktp, " _
                    & "       ptnr_goldarah, " _
                    & "       ptnr_birthcity, " _
                    & "       ptnr_birthday, " _
                    & "       ptnr_negara, " _
                    & "       ptnr_bp_date, " _
                    & "       ptnr_bp_type, " _
                    & "       ptnr_waris_name, " _
                    & "       ptnr_waris_ktp, " _
                    & "       ptnrg_limit_credit, " _
                    & "       ptnr_limit_credit, " _
                    & "       (SUM(public.ar_mstr.ar_amount) - SUM(public.ar_mstr.ar_pay_amount)) AS AR_amount, " _
                    & "       ((COALESCE (public.ptnrg_grp.ptnrg_limit_credit,0)) - (SUM (COALESCE (public.ar_mstr.ar_amount,0)) - SUM (COALESCE (public.ar_mstr.ar_pay_amount,0)))) AS ptnrg_limit_amount, " _
                    & "       ((COALESCE(public.ptnr_mstr.ptnr_limit_credit,0)) - (SUM (COALESCE (public.ar_mstr.ar_amount,0)) - SUM (COALESCE (public.ar_mstr.ar_pay_amount,0)))) AS ptnr_limit_amount, " _
                    & "       en_desc, " _
                    & "       ptnr_area_id, " _
                    & "       area_name, " _
                    & "       ptnr_ac_ar_id, " _
                    & "       coalesce(ptnra_line_1, '') as ptnra_line_1, " _
                    & "       coalesce(ptnra_line_2, '') as ptnra_line_2, " _
                    & "       coalesce(ptnra_line_3, '') as ptnra_line_3, " _
                    & "       coalesce(ptnra_line_4, '') as ptnra_line_4, " _
                    & "       coalesce(ptnra_line_5, '') as ptnra_line_5, " _
                    & "       coalesce(ptnra_phone_1, '') as ptnra_phone_1, " _
                    & "       coalesce(ptnra_phone_2, '') as ptnra_phone_2, " _
                    & "       coalesce(ptnra_zip, '') as ptnra_zip, " _
                    & "       ptnratt_oid, " _
                    & "       ptnratt_kjb_code, " _
                    & "       ptnratt_bekerja_pada, " _
                    & "       ptnratt_jabatan_bagian, " _
                    & "       ptnratt_kantor_alamat_1, " _
                    & "       ptnratt_kantor_alamat_2, " _
                    & "       ptnratt_kantor_lantai, " _
                    & "       ptnratt_kantor_telp, " _
                    & "       ptnratt_ktp, " _
                    & "       ptnratt_email, " _
                    & "       ptnratt_rumah_alamat_1, " _
                    & "       ptnratt_rumah_alamat_2, " _
                    & "       ptnratt_rumah_kode_pos, " _
                    & "       ptnratt_rumah_telp, " _
                    & "       ptnratt_rumah_hp, " _
                    & "       ptnratt_status_alamat_kirim, " _
                    & "       ptnratt_status_alamat_tagih, " _
                    & "       ptnratt_suami_nama, " _
                    & "       ptnratt_suami_bekerja, " _
                    & "       ptnratt_suami_jabatan, " _
                    & "       ptnratt_suami_kantor_alamat_1, " _
                    & "       ptnratt_suami_kantor_alamat_2, " _
                    & "       ptnratt_suami_telp, " _
                    & "       ptnratt_suami_hp, " _
                    & "       ptnratt_anak_nama_1, " _
                    & "       ptnratt_anak_tgl_lahir_1, " _
                    & "       ptnratt_anak_sekolah_1, " _
                    & "       ptnratt_anak_nama_2, " _
                    & "       ptnratt_anak_tgl_lahir_2, " _
                    & "       ptnratt_anak_sekolah_2, " _
                    & "       ptnratt_anak_nama_3, " _
                    & "       ptnratt_anak_tgl_lahir_3, " _
                    & "       ptnratt_anak_sekolah_3, " _
                    & "       ptnratt_keluarga_dekat_nama, " _
                    & "       ptnratt_keluarga_dekat_alamat_1, " _
                    & "       ptnratt_keluarga_dekat_alamat_2, " _
                    & "       ptnratt_keluarga_dekat_telp, " _
                    & "       ptnratt_keluarga_dekat_hp, " _
                    & "       ptnratt_status_tempat_tinggal, " _
                    & "       ptnratt_jenis_kartu_kredit, " _
                    & "       ptnratt_no_kartu_kredit, " _
                    & "       ptnratt_berlaku_sd, " _
                    & "       ptnratt_dt, " _
                    & "       ptnratt_bank, " _
                    & "       ptnratt_status_rumah_id, " _
                    & "       ptnratt_lama_tinggal_id, " _
                    & "       ptnratt_masa_kerja_id, " _
                    & "       ptnratt_income_id, " _
                    & "       ptnratt_kepribadian_id, " _
                    & "       ptnratt_tanggungan_id, " _
                    & "       ptnratt_jaminan_id, " _
                    & "       sr.code_name as status_rumah, " _
                    & "       lt.code_name as lama_tinggal, " _
                    & "       ms.code_name as masa_kerja, " _
                    & "       ic.code_name as income, " _
                    & "       kp.code_name as kepribadian, " _
                    & "       tg.code_name as tanggungan, " _
                    & "       jm.code_name as jaminan" _
                    & " FROM public.ptnr_mstr " _
                    & " INNER JOIN public.en_mstr ON (ptnr_en_id = en_id) " _
                    & " INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                    & " INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                    & " INNER JOIN public.area_mstr on area_id = ptnr_area_id " _
                    & " LEFT OUTER JOIN public.ptnrg_grp ON (ptnr_ptnrg_id = ptnrg_id) " _
                    & " LEFT OUTER JOIN public.ptnratt_attr on ptnratt_ptnr_oid = ptnr_oid " _
                    & " LEFT OUTER JOIN public.code_mstr sr on sr.code_id = ptnratt_status_rumah_id " _
                    & " LEFT OUTER JOIN public.code_mstr lt on lt.code_id = ptnratt_lama_tinggal_id " _
                    & " LEFT OUTER JOIN public.code_mstr ms on ms.code_id = ptnratt_masa_kerja_id " _
                    & " LEFT OUTER JOIN public.code_mstr ic on ic.code_id = ptnratt_income_id " _
                    & " LEFT OUTER JOIN public.code_mstr kp on kp.code_id = ptnratt_kepribadian_id " _
                    & " LEFT OUTER JOIN public.code_mstr tg on tg.code_id = ptnratt_tanggungan_id " _
                    & " LEFT OUTER JOIN public.code_mstr jm on jm.code_id = ptnratt_jaminan_id " _
                    & " LEFT OUTER JOIN public.ar_mstr ON (public.ptnr_mstr.ptnr_id = public.ar_mstr.ar_bill_to) " _
                    & " where ptnr_en_id  in (" + _en_id_coll + ")" _
                    & " and ptnr_active ~~* 'Y' " _
                    & " and ptnr_is_cust ~~* 'Y' " _
                    & " GROUP BY  " _
                    & " ptnr_oid, " _
                    & " ptnr_dom_id, " _
                    & " ptnr_en_id, " _
                    & " ptnr_id, " _
                    & " ptnr_code, " _
                    & " ptnr_name, " _
                    & " ptnr_ptnrg_id, " _
                    & " ptnrg_name, " _
                    & " ptnr_email, " _
                    & " ptnr_sex, " _
                    & " ptnr_ktp, " _
                    & " ptnr_goldarah, " _
                    & " ptnr_birthcity, " _
                    & " ptnr_birthday, " _
                    & " ptnr_negara, " _
                    & " ptnr_bp_date, " _
                    & " ptnr_bp_type, " _
                    & " ptnr_waris_name, " _
                    & " ptnr_waris_ktp, " _
                    & " ptnrg_limit_credit, " _
                    & " ptnr_limit_credit, " _
                    & " en_desc, " _
                    & " ptnr_area_id, " _
                    & " area_name, " _
                    & " ptnr_ac_ar_id, " _
                    & " ptnra_line_1, " _
                    & " ptnra_line_2, " _
                    & " ptnra_line_3, " _
                    & " ptnra_line_4, " _
                    & " ptnra_line_5, " _
                    & " ptnra_phone_1, " _
                    & " ptnra_phone_2, " _
                    & " ptnra_zip, " _
                    & " ptnratt_oid, " _
                    & " ptnratt_kjb_code, " _
                    & " ptnratt_bekerja_pada, " _
                    & " ptnratt_jabatan_bagian, " _
                    & " ptnratt_kantor_alamat_1, " _
                    & " ptnratt_kantor_alamat_2, " _
                    & " ptnratt_kantor_lantai, " _
                    & " ptnratt_kantor_telp, " _
                    & " ptnratt_ktp, " _
                    & " ptnratt_email, " _
                    & " ptnratt_rumah_alamat_1, " _
                    & " ptnratt_rumah_alamat_2, " _
                    & " ptnratt_rumah_kode_pos, " _
                    & " ptnratt_rumah_telp, " _
                    & " ptnratt_rumah_hp, " _
                    & " ptnratt_status_alamat_kirim, " _
                    & " ptnratt_status_alamat_tagih, " _
                    & " ptnratt_suami_nama, " _
                    & " ptnratt_suami_bekerja, " _
                    & " ptnratt_suami_jabatan, " _
                    & " ptnratt_suami_kantor_alamat_1, " _
                    & " ptnratt_suami_kantor_alamat_2, " _
                    & " ptnratt_suami_telp, " _
                    & " ptnratt_suami_hp, " _
                    & " ptnratt_anak_nama_1, " _
                    & " ptnratt_anak_tgl_lahir_1, " _
                    & " ptnratt_anak_sekolah_1, " _
                    & " ptnratt_anak_nama_2, " _
                    & " ptnratt_anak_tgl_lahir_2, " _
                    & " ptnratt_anak_sekolah_2, " _
                    & " ptnratt_anak_nama_3, " _
                    & " ptnratt_anak_tgl_lahir_3, " _
                    & " ptnratt_anak_sekolah_3, " _
                    & " ptnratt_keluarga_dekat_nama, " _
                    & " ptnratt_keluarga_dekat_alamat_1, " _
                    & " ptnratt_keluarga_dekat_alamat_2, " _
                    & " ptnratt_keluarga_dekat_telp, " _
                    & " ptnratt_keluarga_dekat_hp, " _
                    & " ptnratt_status_tempat_tinggal, " _
                    & " ptnratt_jenis_kartu_kredit, " _
                    & " ptnratt_no_kartu_kredit, " _
                    & " ptnratt_berlaku_sd, " _
                    & " ptnratt_dt, " _
                    & " ptnratt_bank, " _
                    & " ptnratt_status_rumah_id, " _
                    & " ptnratt_lama_tinggal_id, " _
                    & " ptnratt_masa_kerja_id, " _
                    & " ptnratt_income_id, " _
                    & " ptnratt_kepribadian_id, " _
                    & " ptnratt_tanggungan_id, " _
                    & " ptnratt_jaminan_id, " _
                    & " status_rumah, " _
                    & " lama_tinggal, " _
                    & " masa_kerja, " _
                    & " income, " _
                    & " kepribadian, " _
                    & " tanggungan, " _
                    & " jaminan " _
                    & " order by ptnr_name "

        ElseIf fobject.name = FSalesPlan.Name Then

            If _obj.name = "gv_edit" Then
                get_sequel = "SELECT  " _
                  & "  a.ptnr_oid, " _
                  & "  a.ptnr_dom_id, " _
                  & "  a.ptnr_en_id, " _
                  & "  a.ptnr_id, " _
                  & "  a.ptnr_code, " _
                  & "  a.ptnr_name, " _
                  & "  en_desc, " _
                  & "  ptnr_ac_ar_id, " _
                  & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                  & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                  & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                  & "FROM " _
                  & "  public.ptnr_mstr a " _
                  & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                  & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                  & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                  & " where ptnr_en_id  in (" + _en_id_coll + ") " _
                  & " and ptnr_active ~~* 'Y' and ptnr_is_cust ~~* 'Y'  " _
                  & "  " _
                  & " order by ptnr_name"
            Else
                get_sequel = "SELECT  " _
                  & "  a.ptnr_oid, " _
                  & "  a.ptnr_dom_id, " _
                  & "  a.ptnr_en_id, " _
                  & "  a.ptnr_id, " _
                  & "  a.ptnr_code, " _
                  & "  a.ptnr_name, " _
                  & "  en_desc, " _
                  & "  ptnr_ac_ar_id, " _
                  & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                  & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                  & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                  & "FROM " _
                  & "  public.ptnr_mstr a " _
                  & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                  & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                  & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                  & " where ptnr_en_id  = " + _en_id.ToString + " " _
                  & " and ptnr_active ~~* 'Y' " _
                  & " and ptnr_is_member ~~* 'Y' " _
                  & " order by ptnr_name"
            End If

        Else
            get_sequel = "SELECT  " _
                    & "  a.ptnr_oid, " _
                    & "  a.ptnr_dom_id, " _
                    & "  a.ptnr_en_id, " _
                    & "  a.ptnr_id, " _
                    & "  a.ptnr_code, " _
                    & "  a.ptnr_name, a.ptnr_email," _
                    & "  a.ptnr_sex, a.ptnr_ktp," _
                    & "  a.ptnr_goldarah, " _
                    & "  a.ptnr_birthcity, " _
                    & "  a.ptnr_birthday, " _
                    & "  a.ptnr_negara, " _
                    & "  a.ptnr_bp_date, " _
                    & "  a.ptnr_bp_type, " _
                    & "  a.ptnr_waris_name, " _
                    & "  a.ptnr_waris_ktp, " _
                    & "  en_desc, " _
                    & "  ptnr_ac_ar_id, " _
                    & "  coalesce(ptnra_line_1,'') as ptnra_line_1, " _
                    & "  coalesce(ptnra_line_2,'') as ptnra_line_2, " _
                    & "  coalesce(ptnra_line_3,'') as ptnra_line_3 " _
                    & "FROM " _
                    & "  public.ptnr_mstr a " _
                    & "  INNER JOIN public.en_mstr c ON (a.ptnr_en_id = c.en_id)" _
                    & "  INNER JOIN public.ptnra_addr on ptnra_ptnr_oid = ptnr_oid " _
                    & "  INNER JOIN public.ac_mstr on ac_id = ptnr_ac_ar_id " _
                    & " where ptnr_en_id  in (" + _en_id_coll + ")" _
                    & " and ptnr_active ~~* 'Y' " _
                    & " and ptnr_is_cust ~~* 'Y' " _
                    & " order by ptnr_name"
        End If

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
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position

        If fobject.name = "FSalesOrder" Then
            fobject._so_ptnr_id_sold_mstr = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
            fobject.so_ptnr_id_sold.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            fobject.so_ar_ac_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnr_ac_ar_id")
            fobject.so_bantu_address.text = Trim(ds.Tables(0).Rows(_row_gv).Item("ptnra_line_1") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_2") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_3"))

        ElseIf fobject.name = "FSalesOrderAlocated" Then
            fobject._so_ptnr_id_sold_mstr = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
            fobject.so_ptnr_id_sold.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            fobject.so_ar_ac_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnr_ac_ar_id")
            fobject.so_bantu_address.text = Trim(ds.Tables(0).Rows(_row_gv).Item("ptnra_line_1") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_2") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_3"))
            fobject.so_pidd_area_id.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_area_id")
            fobject.so_pidd_area_id.text = SetString(ds.Tables(0).Rows(_row_gv).Item("area_name"))

        ElseIf fobject.name = "FSalesQuotation" Then
            fobject._ptnratt_ptnr_oid = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnr_oid"))
            'fobject.sqa_kjb_code.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_kjb_code"))
            fobject.sqa_bekerja_pada.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_bekerja_pada"))
            fobject.sqa_jabatan_bagian.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_jabatan_bagian"))
            fobject.sqa_kantor_alamat_1.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_kantor_alamat_1"))
            fobject.sqa_kantor_alamat_2.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_kantor_alamat_2"))
            fobject.sqa_kantor_lantai.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_kantor_lantai"))
            fobject.sqa_kantor_telp.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_kantor_telp"))
            fobject.sqa_ktp.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnr_ktp"))
            fobject.sqa_email.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnr_email"))
            fobject.sqa_rumah_alamat_1.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnra_line_1"))
            fobject.sqa_rumah_alamat_2.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnra_line_2"))
            fobject.sqa_rumah_kode_pos.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnra_zip"))
            fobject.sqa_rumah_telp.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnra_phone_2"))
            fobject.sqa_rumah_hp.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnra_phone_1"))
            fobject.sqa_status_alamat_kirim.EditValue = SetBitYNB(ds.Tables(0).Rows(_row_gv).Item("ptnratt_status_alamat_kirim"))
            fobject.sqa_status_alamat_tagih.EditValue = SetBitYNB(ds.Tables(0).Rows(_row_gv).Item("ptnratt_status_alamat_tagih"))
            fobject.sqa_suami_nama.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_suami_nama"))
            fobject.sqa_suami_bekerja.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_suami_bekerja"))
            fobject.sqa_suami_jabatan.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_suami_jabatan"))
            fobject.sqa_suami_kantor_alamat_1.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_suami_kantor_alamat_1"))
            fobject.sqa_suami_kantor_alamat_2.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_suami_kantor_alamat_2"))
            fobject.sqa_suami_telp.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_suami_telp"))
            fobject.sqa_suami_hp.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_suami_hp"))
            fobject.sqa_anak_nama_1.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_anak_nama_1"))
            'fobject.sqa_anak_tgl_lahir_1.DateTime = SetDate(ds.Tables(0).Rows(_row_gv).Item("ptnratt_anak_tgl_lahir_1"))
            fobject.sqa_anak_sekolah_1.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_anak_sekolah_1"))
            fobject.sqa_anak_nama_2.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_anak_nama_2"))
            'fobject.sqa_anak_tgl_lahir_2.DateTime = SetDate(ds.Tables(0).Rows(_row_gv).Item("ptnratt_anak_tgl_lahir_2"))
            fobject.sqa_anak_sekolah_2.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_anak_sekolah_2"))
            fobject.sqa_anak_nama_3.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_anak_nama_3"))
            'fobject.sqa_anak_tgl_lahir_3.DateTime = ds.Tables(0).Rows(_row_gv).Item("ptnratt_anak_tgl_lahir_3")
            fobject.sqa_anak_sekolah_3.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_anak_sekolah_3"))
            fobject.sqa_keluarga_dekat_nama.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_keluarga_dekat_nama"))
            fobject.sqa_keluarga_dekat_alamat_1.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_keluarga_dekat_alamat_1"))
            fobject.sqa_keluarga_dekat_alamat_2.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_keluarga_dekat_alamat_2"))
            fobject.sqa_keluarga_dekat_telp.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_keluarga_dekat_telp"))
            fobject.sqa_keluarga_dekat_hp.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_keluarga_dekat_hp"))
            fobject.sqa_status_tempat_tinggal.EditValue = SetBitYNB(ds.Tables(0).Rows(_row_gv).Item("ptnratt_status_tempat_tinggal"))
            fobject.sqa_jenis_kartu_kredit.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_jenis_kartu_kredit"))
            fobject.sqa_no_kartu_kredit.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_no_kartu_kredit"))
            'fobject.sqa_berlaku_sd.Text = ds.Tables(0).Rows(_row_gv).Item("ptnratt_berlaku_sd")
            fobject.sqa_bank.Text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnratt_bank"))
            fobject._sq_ptnr_id_sold_mstr = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
            fobject.sq_ptnr_id_sold.text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnr_name"))
            fobject.sq_ar_ac_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnr_ac_ar_id")
            fobject.sq_bantu_address.text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnra_line_1") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_2") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_3"))
            fobject.sqa_status_rumah_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnratt_status_rumah_id")
            fobject.sqa_status_rumah_id.text = SetSetring(ds.Tables(0).Rows(_row_gv).Item("status_rumah"))
            fobject.sqa_lama_tinggal_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnratt_lama_tinggal_id")
            fobject.sqa_lama_tinggal_id.text = SetSetring(ds.Tables(0).Rows(_row_gv).Item("lama_tinggal"))
            fobject.sqa_masa_kerja_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnratt_masa_kerja_id")
            fobject.sqa_masa_kerja_id.text = SetSetring(ds.Tables(0).Rows(_row_gv).Item("masa_kerja"))
            fobject.sqa_income_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnratt_income_id")
            fobject.sqa_income_id.text = SetSetring(ds.Tables(0).Rows(_row_gv).Item("income"))
            fobject.sqa_kepribadian_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnratt_kepribadian_id")
            fobject.sqa_kepribadian_id.text = SetSetring(ds.Tables(0).Rows(_row_gv).Item("kepribadian"))
            fobject.sqa_tanggungan_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnratt_tanggungan_id")
            fobject.sqa_tanggungan_id.text = SetSetring(ds.Tables(0).Rows(_row_gv).Item("tanggungan"))
            fobject.sqa_jaminan_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnratt_jaminan_id")
            fobject.sqa_jaminan_id.text = SetSetring(ds.Tables(0).Rows(_row_gv).Item("jaminan"))
            fobject.sqa_status_rumah_id.ClosePopup()
            fobject.sqa_lama_tinggal_id.ClosePopup()
            fobject.sqa_masa_kerja_id.ClosePopup()
            fobject.sqa_income_id.ClosePopup()
            fobject.sqa_kepribadian_id.ClosePopup()
            fobject.sqa_tanggungan_id.ClosePopup()
            fobject.sqa_jaminan_id.ClosePopup()

        ElseIf fobject.name = FSalesQuotationConsigment.Name Then
            fobject.sq_ptnr_id_sold.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
            fobject.sq_ptnr_id_sold.text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnr_name"))
            fobject.sq_ar_ac_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnr_ac_ar_id")
            fobject.sq_bantu_address.text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnra_line_1") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_2") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_3"))

        ElseIf fobject.name = FPartNumberSKUGenerator.Name Then
            fobject.psplan_ptnr_id.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
            fobject.psplan_ptnr_id.text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnr_name"))

        ElseIf fobject.name = "FSalesOrderCBA" Then
            fobject._sq_ptnr_id_sold_mstr = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
            fobject.sq_ptnr_id_sold.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            fobject.sq_ar_ac_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnr_ac_ar_id")
            fobject.sq_bantu_address.text = Trim(ds.Tables(0).Rows(_row_gv).Item("ptnra_line_1") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_2") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_3"))

        ElseIf fobject.name = "FSalesQuotation" Or fobject.name = FSalesQuotationConsigment.Name Or fobject.name = FSalesQuotationConsigmentAloc.Name Or fobject.name = FSalesQuotationConsigmentAlocated.Name Then
            fobject.sq_ptnr_id_sold.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
            fobject.sq_ptnr_id_sold.text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnr_name"))
            fobject.sq_pidd_area_id.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_area_id")
            fobject.sq_pidd_area_id.text = SetString(ds.Tables(0).Rows(_row_gv).Item("area_name"))
            fobject.sq_ptnr_id_sold.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
            fobject.sq_ptnr_id_sold.text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnr_name"))
            If ds.Tables(0).Rows(_row_gv).Item("ptnr_limit_credit") = 0 Then
                fobject.sq_crlmt_reff.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnrg_limit_amount")
            Else
                fobject.sq_crlmt_reff.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnr_limit_amount")
            End If
            fobject.sq_ar_ac_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnr_ac_ar_id")
            'fobject.sq_pi_area_id.editvalue = ds.Tables(0).Rows(_row_gv).Item("sq_pi_area_id")
            fobject.sq_bantu_address.text = SetString(ds.Tables(0).Rows(_row_gv).Item("ptnra_line_1") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_2") + ds.Tables(0).Rows(_row_gv).Item("ptnra_line_3"))


        ElseIf fobject.name = "FRouting" Then
            fobject.gv_edit.SetRowCellValue(_row, "rod_ptnr_id", ds.Tables(0).Rows(_row_gv).Item("ptnr_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ptnr_code", ds.Tables(0).Rows(_row_gv).Item("ptnr_code"))
            fobject.gv_edit.SetRowCellValue(_row, "ptnr_name", ds.Tables(0).Rows(_row_gv).Item("ptnr_name"))

        ElseIf fobject.name = "FProject" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            fobject.__prj_ptnr_id = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")

        ElseIf fobject.name = FCashOut.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            fobject.__ptnr_id = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")

        ElseIf fobject.name = FRequisition.Name Then
            If _obj.name = "req_requested_ptnr_id" Then
                _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
                fobject.__ptnr_id = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
            ElseIf _obj.name = "req_end_user_ptnr_id" Then
                _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
                fobject.__end_ptnr_id = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
            End If

        ElseIf fobject.name = FCashIn.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            fobject.__ptnr_id = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")

        ElseIf fobject.name = FDRCRMemo.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            fobject._par_cus_id = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")

        ElseIf fobject.name = FPartNumberSKUGenerator.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            fobject._par_vend_id = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")

        ElseIf fobject.name = FDBCRReScheduleSDI.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            _obj.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")

        ElseIf fobject.name = FVoucher.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            fobject._par_vendor_id = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")

        ElseIf fobject.name = FPartnerTaxGroup.Name Then
            fobject.gv_edit.SetRowCellValue(_row, "tipgd_en_id", ds.Tables(0).Rows(_row_gv).Item("ptnr_en_id"))
            fobject.gv_edit.SetRowCellValue(_row, "en_desc", ds.Tables(0).Rows(_row_gv).Item("en_desc"))
            fobject.gv_edit.SetRowCellValue(_row, "tipgd_ptnr_id", ds.Tables(0).Rows(_row_gv).Item("ptnr_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ptnr_name", ds.Tables(0).Rows(_row_gv).Item("ptnr_name"))
            fobject.gv_edit.BestFitColumns()

        ElseIf fobject.name = FLocation.Name Or fobject.name = FARMerge.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            _obj.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
            'ElseIf fobject.name = FLocation.Name Or fobject.name = FARMergeByShipment.Name Then
            '    _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            '    _obj.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")

        ElseIf fobject.name = "FDBPointGroup" Then
            fobject.gv_edit.SetRowCellValue(_row, "dbgd_ptnr_id", ds.Tables(0).Rows(_row_gv).Item("ptnr_id"))
            fobject.gv_edit.SetRowCellValue(_row, "ptnr_name", ds.Tables(0).Rows(_row_gv).Item("ptnr_name"))

        ElseIf fobject.name = FInventoryReturns.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            '_obj.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
            fobject._par_ptnr_id = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
            fobject._par_ptnr_id = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")

        ElseIf fobject.name = FLocation.Name Or fobject.name = FDPackingSheet.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            _obj.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")

        ElseIf fobject.name = FLocation.Name Or fobject.name = FDPackingSheetNew.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            _obj.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")

        ElseIf fobject.name = FLocation.Name Or fobject.name = FSOShipMerge.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            _obj.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")

        ElseIf fobject.name = FPartnerAll.Name Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            _obj.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")

        ElseIf fobject.name = FSalesPlan.Name Then
            If _obj.name = "plans_sales_id" Then
                _obj.tag = ds.Tables(0).Rows(_row_gv).Item("ptnr_id")
                _obj.editvalue = ds.Tables(0).Rows(_row_gv).Item("ptnr_name")
            Else
                fobject.gv_edit.SetRowCellValue(_row, "plansd_ptnr_id", ds.Tables(0).Rows(_row_gv).Item("ptnr_id"))
                fobject.gv_edit.SetRowCellValue(_row, "ptnr_name", ds.Tables(0).Rows(_row_gv).Item("ptnr_name"))
                fobject.gv_edit.SetRowCellValue(_row, "ptnr_code", ds.Tables(0).Rows(_row_gv).Item("ptnr_code"))
                fobject.gv_edit.BestFitColumns()
            End If
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

    Private Sub sb_fill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_fill.Click
        Try
            Dim _row_pos As Integer
            Dim jml As Integer = 0
            ds.Tables(0).AcceptChanges()

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                If ds.Tables(0).Rows(i).Item("status") = True Then
                    jml = jml + 1
                End If
            Next
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                If ds.Tables(0).Rows(i).Item("status") = True Then
                    If fobject.name = FPartnerTaxGroup.Name Then
                        If jml = 0 Then
                            fobject.gv_edit.SetRowCellValue(_row, "tipgd_en_id", ds.Tables(0).Rows(i).Item("ptnr_en_id"))
                            fobject.gv_edit.SetRowCellValue(_row, "en_desc", ds.Tables(0).Rows(i).Item("en_desc"))
                            fobject.gv_edit.SetRowCellValue(_row, "tipgd_ptnr_id", ds.Tables(0).Rows(i).Item("ptnr_id"))
                            fobject.gv_edit.SetRowCellValue(_row, "ptnr_name", ds.Tables(0).Rows(i).Item("ptnr_name"))
                            jml = jml + 1
                        Else
                            fobject.gv_edit.AddNewRow()
                            _row_pos = fobject.gv_edit.FocusedRowHandle()
                            fobject.gv_edit.SetRowCellValue(_row_pos, "tipgd_en_id", ds.Tables(0).Rows(i).Item("ptnr_en_id"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "en_desc", ds.Tables(0).Rows(i).Item("en_desc"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "tipgd_ptnr_id", ds.Tables(0).Rows(i).Item("ptnr_id"))
                            fobject.gv_edit.SetRowCellValue(_row_pos, "ptnr_name", ds.Tables(0).Rows(i).Item("ptnr_name"))
                        End If

                        fobject.gv_edit.BestFitColumns()
                    End If
                End If
            Next
            If jml = 0 Then
                MsgBox("Please checklist data first")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Me.Close()
    End Sub
End Class
