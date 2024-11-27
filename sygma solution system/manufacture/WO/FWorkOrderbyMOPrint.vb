Public Class FWorkOrderbyMOPrint
    Public dt_bantu As DataTable
    Public func_data As New function_data

    Private Sub FWorkOrderbyMOPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'le_entity.Properties.DataSource = dt_bantu
        'le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'le_entity.ItemIndex = 0
        init_le(le_entity, "en_mstr")
    End Sub

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        Dim frm As New FWOSearchbyMO()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_first
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_to.ButtonClick
        Dim frm As New FWOSearchbyMO()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_to
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Public Overrides Sub preview()
        Dim _en_id As Integer
        Dim _type, _table, _initial, _code_awal, _code_akhir As String
        Dim func_coll As New function_collection

        _en_id = le_entity.EditValue
        _type = 5
        _table = "cashi_in"
        _initial = "cashi"
        _code_awal = Trim(be_first.Text)
        _code_akhir = Trim(be_to.Text)

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "  cashi_oid, " _
            & "  cashi_dom_id, " _
            & "  cashi_en_id, " _
            & "  cashi_add_by, " _
            & "  cashi_add_date, " _
            & "  cashi_upd_by, " _
            & "  cashi_upd_date, " _
            & "  cashi_bk_id, " _
            & "  cashi_ptnr_id, " _
            & "  cashi_code, " _
            & "  cashi_date, " _
            & "  cashi_remarks, " _
            & "  cashi_reff, " _
            & "  cashi_cu_id, " _
            & "  cashi_exc_rate, " _
            & "  cashi_amount, " _
            & "  cashi_amount * cashi_exc_rate as cashi_amount_ext, " _
            & "  cashi_check_number, " _
            & "  cashi_post_dated_check, " _
            & "  cashid_oid, " _
            & "  cashid_cashi_oid, " _
            & "  cashid_ac_id, " _
            & "  cashid_amount, " _
            & "  cashid_amount * cashi_exc_rate as cashid_amount_ext, " _
            & "  cashid_remarks, " _
            & "  cashid_seq, " _
            & "  bk_name, " _
            & "  ptnr_name, " _
            & "  ac_code, " _
            & "  ac_name, " _
            & "  cmaddr_name, " _
            & "  cmaddr_line_1, " _
            & "  cmaddr_line_2, " _
            & "  cmaddr_line_3 " _
            & "FROM  " _
            & "  cashi_in " _
            & "inner join cashid_detail on cashid_cashi_oid = cashi_oid " _
            & "inner join bk_mstr on bk_id = cashi_bk_id " _
            & "inner join ptnr_mstr on ptnr_id = cashi_ptnr_id " _
            & "inner join cu_mstr on cu_id = cashi_cu_id " _
            & "inner join ac_mstr on ac_id = cashid_ac_id " _
            & "inner join cmaddr_mstr on cmaddr_en_id = cashi_en_id" _
            & "    where cashi_code >= '" + be_first.Text + "'" _
            & "    and cashi_code <= '" + be_to.Text + "'" _
            & "    order by cashi_code"

        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRCashInPrint"
        frm._remarks = be_first.Text & " >> " & be_to.Text
        frm.ShowDialog()

    End Sub
End Class
