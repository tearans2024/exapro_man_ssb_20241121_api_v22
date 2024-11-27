Public Class FCOutPrint
    Public dt_bantu As DataTable
    Public func_data As New function_data

    Private Sub FCOutPrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_en_mstr_tran())
        'le_entity.Properties.DataSource = dt_bantu
        'le_entity.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        'le_entity.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        'le_entity.ItemIndex = 0
        init_le(le_entity, "en_mstr")
    End Sub

    Private Sub be_first_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_first.ButtonClick
        Dim frm As New FCOutSearch()
        frm.set_win(Me)
        frm._en_id = le_entity.EditValue
        frm._obj = be_first
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    Private Sub be_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_to.ButtonClick
        Dim frm As New FCOutSearch()
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
        _table = "casho_out"
        _initial = "casho"
        _code_awal = Trim(be_first.Text)
        _code_akhir = Trim(be_to.Text)

        func_coll.insert_tranaprvd_det(_en_id, _type, _table, _initial, _code_awal, _code_akhir, func_coll.get_tanggal_sistem)

        Dim ds_bantu As New DataSet
        Dim _sql As String

        _sql = "SELECT  " _
            & "  casho_oid, " _
            & "  casho_dom_id, " _
            & "  casho_en_id, " _
            & "  casho_add_by, " _
            & "  casho_add_date, " _
            & "  casho_upd_by, " _
            & "  casho_upd_date, " _
            & "  casho_bk_id, " _
            & "  casho_ptnr_id, " _
            & "  casho_code, " _
            & "  casho_date, " _
            & "  casho_remarks, " _
            & "  casho_reff, " _
            & "  casho_cu_id, " _
            & "  casho_exc_rate, " _
            & "  casho_amount, " _
            & "  casho_amount * casho_exc_rate as casho_amount_ext, " _
            & "  casho_check_number, " _
            & "  casho_post_dated_check, " _
            & "  cashod_oid, " _
            & "  cashod_casho_oid, " _
            & "  cashod_ac_id, " _
            & "  cashod_amount, " _
            & "  cashod_amount * casho_exc_rate as cashod_amount_ext, " _
            & "  cashod_remarks, " _
            & "  cashod_seq, " _
            & "  bk_name, " _
            & "  ptnr_name, " _
            & "  ac_code, " _
            & "  ac_name, " _
            & "  cmaddr_name, " _
            & "  cmaddr_line_1, " _
            & "  cmaddr_line_2, " _
            & "  cmaddr_line_3 " _
            & "FROM  " _
            & "  casho_out " _
            & "inner join cashod_detail on cashod_casho_oid = casho_oid " _
            & "inner join bk_mstr on bk_id = casho_bk_id " _
            & "inner join ptnr_mstr on ptnr_id = casho_ptnr_id " _
            & "inner join cu_mstr on cu_id = casho_cu_id " _
            & "inner join ac_mstr on ac_id = cashod_ac_id " _
            & "inner join cmaddr_mstr on cmaddr_en_id = casho_en_id" _
            & "    where casho_code >= '" + be_first.Text + "'" _
            & "    and casho_code <= '" + be_to.Text + "'" _
            & "    order by casho_code"

        Dim frm As New frmPrintDialog
        frm._ssql = _sql
        frm._report = "XRCashOutPrint"
        frm._remarks = be_first.Text & " >> " & be_to.Text
        frm.ShowDialog()

    End Sub
End Class
