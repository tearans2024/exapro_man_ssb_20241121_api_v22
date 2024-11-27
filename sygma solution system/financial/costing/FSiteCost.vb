Imports CoreLab.PostgreSql
Imports master_new.ModFunction

Public Class FSiteCost

    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _oid_mstr As String
    Dim dt_edit As New DataTable
    Dim sSQL As String

    Private Sub FOrganizationStructure_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        sct_en_id.Properties.DataSource = dt_bantu
        sct_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        sct_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        sct_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_data("entity", ""))
        en_id.Properties.DataSource = dt_bantu
        en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        en_id.ItemIndex = 0
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Partnumber Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "PT Desc1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "PT Desc2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Product Line", "pl_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost Set", "cs_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Cost Set Default", "cs_is_default", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Rollup Date", "sct_rollup_date", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Rollup PS Status", "sct_rollup_ps", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Rollup Routing Status", "sct_rollup_routing", DevExpress.Utils.HorzAlignment.Default)

        add_column_copy(gv_master, "Total Cost", "sct_total", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Material This Level", "sct_mtl_tl", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Labor This Level", "sct_lbr_tl", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Burden This Level", "sct_bdn_tl", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Overhead This Level", "sct_ovh_tl", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Subcont This Level", "sct_sub_tl", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Material Lower Level", "sct_mtl_ll", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Labor Lower Level", "sct_lbr_ll", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Burden Lower Level", "sct_bdn_ll", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Overhead Lower Level", "sct_ovh_ll", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Subcont Lower Level", "sct_sub_ll", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Price", "sct_price", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "User Create", "sct_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "sct_add_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")
        add_column_copy(gv_master, "User Update", "sct_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "sct_upd_date", DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.FormatType.DateTime, "G")

        add_column(gv_detail, "sctd_oid", False)
        add_column_copy(gv_detail, "Category Code", "csc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Category Name", "csc_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost Set Element", "csd_element", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Cost Set Desc", "csd_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "This Level Amount", "sctd_tl_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Lower Level Amount", "sctd_ll_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_detail, "Amount", "sctd_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")


        add_column(gv_edit, "sctd_oid", False)
        add_column_copy(gv_edit, "Category Code", "csc_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Category Name", "csc_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Cost Set Element", "csd_element", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_edit, "Cost Set Desc", "csd_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_edit, "sctd_ac_id", False)
        add_column_browse(gv_edit, "Account Code", "ac_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_browse(gv_edit, "Account Name", "ac_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_edit, "This Level Amount", "sctd_tl_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Lower Level Amount", "sctd_ll_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column(gv_edit, "Amount", "sctd_amount", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")

        init_le(sct_si_id, "site", sct_en_id.EditValue)
        init_le(sct_cs_id, "cost_set", sct_en_id.EditValue)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  a.sct_oid, " _
                    & "  a.sct_dom_id, " _
                    & "  a.sct_en_id, " _
                    & "  b.en_desc, " _
                    & "  a.sct_si_id, " _
                    & "  e.si_desc, " _
                    & "  a.sct_pt_id, " _
                    & "  d.pt_code, " _
                    & "  d.pt_desc1, " _
                    & "  d.pt_desc2, " _
                    & "  pl_desc, " _
                    & "  a.sct_cs_id, " _
                    & "  c.cs_name,cs_is_default, " _
                    & "  a.sct_total, " _
                    & "  a.sct_price, " _
                    & "  a.sct_rollup_date, " _
                    & "  a.sct_mtl_tl, " _
                    & "  a.sct_lbr_tl, " _
                    & "  a.sct_bdn_tl, " _
                    & "  a.sct_ovh_tl, " _
                    & "  a.sct_sub_tl, " _
                    & "  a.sct_mtl_ll, " _
                    & "  a.sct_lbr_ll, " _
                    & "  a.sct_bdn_ll, " _
                    & "  a.sct_ovh_ll, " _
                    & "  a.sct_sub_ll, " _
                    & "  a.sct_add_by, " _
                    & "  a.sct_add_date, " _
                    & "  a.sct_upd_by, " _
                    & "  a.sct_upd_date,sct_rollup_ps,sct_rollup_routing " _
                    & "FROM " _
                    & "  public.sct_mstr a " _
                    & "  INNER JOIN public.en_mstr b ON (a.sct_en_id = b.en_id) " _
                    & "  INNER JOIN public.si_mstr e ON (a.sct_si_id = e.si_id) " _
                    & "  INNER JOIN public.pt_mstr d ON (a.sct_pt_id = d.pt_id) " _
                    & "  INNER JOIN public.pl_mstr ON (pt_pl_id = pl_id) " _
                    & "  INNER JOIN public.cs_mstr c ON (a.sct_cs_id = c.cs_id) " _
                    & " where cs_en_id in (select user_en_id from tconfuserentity " _
                    & " where userid = " + master_new.ClsVar.sUserID.ToString + ") " _
                    & " and sct_pt_id in (select pt_id from pt_mstr where pt_code between '" _
                    & pt_id_from.Text & "' and '" & pt_id_to.Text _
                    & "' )"


        Return get_sequel
    End Function

    Public Overrides Sub load_data_grid_detail()

    End Sub

    Private Sub load_detail()
        Dim sSQL As String
        Try

            If ds.Tables(0).Rows.Count = 0 Then
                Exit Sub
            End If

            sSQL = "SELECT  " _
                & "  a.sctd_oid, " _
                & "  a.sctd_sct_oid, " _
                & "  a.sctd_csd_oid, " _
                & "  c.csc_code, " _
                & "  c.csc_name, " _
                & "  b.csd_element, " _
                & "  b.csd_desc, " _
                & "  a.sctd_ac_id, " _
                & "  d.ac_code, " _
                & "  d.ac_name, " _
                & "  a.sctd_tl_amount, " _
                & "  a.sctd_ll_amount, " _
                & "  a.sctd_amount " _
                & "FROM " _
                & "  public.sctd_det a " _
                & "  INNER JOIN public.csd_det b ON (a.sctd_csd_oid = b.csd_oid) " _
                & "  INNER JOIN public.csc_category c ON (b.csd_csc_id = c.csc_id) " _
                & "  INNER JOIN public.ac_mstr d ON (d.ac_id = a.sctd_ac_id) " _
                & "WHERE " _
                & "  a.sctd_sct_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sct_oid").ToString & "'"

            gc_detail.DataSource = master_new.PGSqlConn.GetTableData(sSQL)
            gv_detail.BestFitColumns()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Overrides Sub relation_detail()
        Try

        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function delete_data() As Boolean
        MessageBox.Show("Delete Data Not Available For This Menu")
    End Function

    Public Overrides Function insert_data() As Boolean
        MessageBox.Show("Insert Data Not Available For This Menu")
    End Function

    Public Overrides Function before_delete() As Boolean
        Try
            If ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("cs_is_default") = "Y" Then
                Box("Can't delete default site cost")
                Return False
            End If

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Overrides Function edit_data() As Boolean
        Dim sSQL As String
        If MyBase.edit_data = True Then

            row = BindingContext(ds.Tables(0)).Position

            With ds.Tables(0).Rows(row)

                'If .Item("cs_is_manual") = "Y" Then
                '    sct_cs_id.Enabled = False
                'Else
                '    sct_cs_id.Enabled = True
                'End If

                sct_cs_id.Enabled = False
                sct_si_id.Enabled = False
                sct_pt_id.Enabled = False

                _oid_mstr = .Item("sct_oid")
                sct_en_id.EditValue = .Item("sct_en_id")
                sct_pt_id.Text = .Item("pt_code")
                sct_pt_id.Tag = .Item("sct_pt_id")
                pt_desc1.Text = SetString(.Item("pt_desc1"))
                pt_desc2.Text = SetString(.Item("pt_desc2"))
                sct_si_id.EditValue = .Item("sct_si_id")
                sct_cs_id.EditValue = .Item("sct_cs_id")

                sct_mtl_tl.EditValue = .Item("sct_mtl_tl")
                sct_ovh_tl.EditValue = .Item("sct_ovh_tl")
                sct_lbr_tl.EditValue = .Item("sct_lbr_tl")
                sct_bdn_tl.EditValue = .Item("sct_bdn_tl")
                sct_sub_tl.EditValue = .Item("sct_sub_tl")

                sct_mtl_ll.EditValue = .Item("sct_mtl_ll")
                sct_ovh_ll.EditValue = .Item("sct_ovh_ll")
                sct_lbr_ll.EditValue = .Item("sct_lbr_ll")
                sct_bdn_ll.EditValue = .Item("sct_bdn_ll")
                sct_sub_ll.EditValue = .Item("sct_sub_ll")

                sct_total.EditValue = .Item("sct_total")
                sct_price.EditValue = .Item("sct_price")
            End With
            sSQL = "SELECT  " _
                & "  a.sctd_oid, " _
                & "  a.sctd_sct_oid, " _
                & "  a.sctd_csd_oid, " _
                & "  c.csc_code, " _
                & "  c.csc_name, " _
                & "  b.csd_element, " _
                & "  b.csd_desc, " _
                & "  a.sctd_tl_amount, " _
                & "  a.sctd_ll_amount, " _
                & "  a.sctd_amount, " _
                & "  a.sctd_ac_id, " _
                & "  d.ac_code, " _
                & "  d.ac_name " _
                & "FROM " _
                & "  public.sctd_det a " _
                & "  INNER JOIN public.csd_det b ON (a.sctd_csd_oid = b.csd_oid) " _
                & "  INNER JOIN public.csc_category c ON (b.csd_csc_id = c.csc_id) " _
                & "  INNER JOIN public.ac_mstr d ON (a.sctd_ac_id = d.ac_id) " _
                & "WHERE " _
                & "  a.sctd_sct_oid = '" & ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sct_oid").ToString & "'"

            dt_edit = master_new.PGSqlConn.GetTableData(sSQL)

            gc_edit.DataSource = dt_edit
            gv_edit.BestFitColumns()

            sct_en_id.Focus()
            edit_data = True
        End If
    End Function

    Private Sub hitung_total()
        Try
            gv_edit.UpdateCurrentRow()
            dt_edit.AcceptChanges()

            Dim _mtl_tl, _lbr_tl, _bdn_tl, _ovh_tl, _sub_tl, _ttl_tl As Double
            _mtl_tl = 0
            _lbr_tl = 0
            _bdn_tl = 0
            _ovh_tl = 0
            _sub_tl = 0
            _ttl_tl = 0

            For Each dr As DataRow In dt_edit.Rows
                If dr("csc_code") = "CMTL" Then
                    _mtl_tl += SetNumber(dr("sctd_tl_amount"))
                ElseIf dr("csc_code") = "CLBR" Then
                    _lbr_tl += SetNumber(dr("sctd_tl_amount"))
                ElseIf dr("csc_code") = "CBDN" Then
                    _bdn_tl += SetNumber(dr("sctd_tl_amount"))
                ElseIf dr("csc_code") = "COVH" Then
                    _ovh_tl += SetNumber(dr("sctd_tl_amount"))
                ElseIf dr("csc_code") = "CSBC" Then
                    _sub_tl += SetNumber(dr("sctd_tl_amount"))
                End If
            Next


            sct_mtl_tl.EditValue = _mtl_tl
            sct_ovh_tl.EditValue = _ovh_tl
            sct_lbr_tl.EditValue = _lbr_tl
            sct_bdn_tl.EditValue = _bdn_tl
            sct_sub_tl.EditValue = _sub_tl

            sct_total.EditValue = sct_mtl_tl.EditValue + sct_ovh_tl.EditValue + sct_lbr_tl.EditValue + sct_bdn_tl.EditValue + sct_sub_tl.EditValue _
                                + sct_mtl_ll.EditValue + sct_ovh_ll.EditValue + sct_lbr_ll.EditValue + sct_bdn_ll.EditValue + sct_sub_ll.EditValue


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Public Overrides Function edit()
        edit = True
        Try
            hitung_total()
            gv_edit.UpdateCurrentRow()
            dt_edit.AcceptChanges()
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                & "  public.sct_mstr   " _
                                & "SET  " _
                                & "  sct_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                & "  sct_upd_date = " & "current_timestamp" & ",  " _
                                & "  sct_price = " & SetDbl(sct_price.EditValue) & ",  " _
                                & "  sct_total = " & SetDbl(sct_total.EditValue) & ",  " _
                                & "  sct_mtl_tl = " & SetDbl(sct_mtl_tl.EditValue) & ",  " _
                                & "  sct_lbr_tl = " & SetDbl(sct_lbr_tl.EditValue) & ",  " _
                                & "  sct_bdn_tl = " & SetDbl(sct_bdn_tl.EditValue) & ",  " _
                                & "  sct_ovh_tl = " & SetDbl(sct_ovh_tl.EditValue) & ",  " _
                                & "  sct_sub_tl = " & SetDbl(sct_sub_tl.EditValue) & "  " _
                                & "WHERE  " _
                                & "  sct_oid = " & SetSetring(_oid_mstr) & "  "

                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        '.Command.CommandType = CommandType.Text
                        '.Command.CommandText = "DELETE FROM  " _
                        '        & "  public.csd_det  " _
                        '        & "WHERE  " _
                        '        & " csd_cs_oid = '" & _oid_mstr & "' "

                        '.Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        For i As Integer = 0 To dt_edit.Rows.Count - 1
                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                    & "  public.sctd_det   " _
                                    & "SET  " _
                                    & "  sctd_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                    & "  sctd_upd_date = " & "current_timestamp" & ",  " _
                                    & "  sctd_ac_id = " & SetInteger(dt_edit.Rows(i).Item("sctd_ac_id")) & ",  " _
                                    & "  sctd_tl_amount = " & SetDbl(dt_edit.Rows(i).Item("sctd_tl_amount")) & ",  " _
                                    & "  sctd_amount = " & SetDbl(dt_edit.Rows(i).Item("sctd_amount")) & "  " _
                                    & "WHERE  " _
                                    & "  sctd_oid = " & SetSetring(dt_edit.Rows(i).Item("sctd_oid")) & " "

                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()
                        Next

                        sqlTran.Commit()

                        after_success()
                        set_row(_oid_mstr, "sct_oid")
                        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                        edit = True
                    Catch ex As PgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            edit = False
            MessageBox.Show(ex.Message)
        End Try
        Return edit
    End Function

    Private Sub gv_master_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_master.Click
        load_detail()
    End Sub

    Private Sub gv_master_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Data.SelectionChangedEventArgs) Handles gv_master.SelectionChanged
        load_detail()
    End Sub

    Private Sub cs_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles sct_en_id.EditValueChanged
        init_le(sct_si_id, "site", sct_en_id.EditValue)
        init_le(sct_cs_id, "cost_set", sct_en_id.EditValue)
    End Sub

    Public Overrides Function cancel_data() As Boolean
        MyBase.cancel_data()
        dp_detail.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
    End Function

    Private Sub sct_pt_id_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles sct_pt_id.ButtonClick
        Try
            Dim frm As New FPartNumberSearch()
            frm.set_win(Me)
            frm._en_id = sct_en_id.EditValue
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles gv_edit.CellValueChanged
        If e.Column.Name = "sctd_tl_amount" Then
            Dim _amount As Double
            _amount = e.Value + gv_edit.GetFocusedRowCellValue("sctd_ll_amount")
            gv_edit.SetFocusedRowCellValue("sctd_amount", _amount)
            hitung_total()
        End If

    End Sub

    Private Sub gv_edit_RowUpdated(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gv_edit.RowUpdated
        hitung_total()
    End Sub

    Private Sub pt_id_from_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles pt_id_from.ButtonClick
        Try
            Dim frm As New FPartNumberSearch()
            frm.set_win(Me)
            frm._en_id = en_id.EditValue
            frm._obj = pt_id_from
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub pt_id_to_ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles pt_id_to.ButtonClick
        Try
            Dim frm As New FPartNumberSearch()
            frm.set_win(Me)
            frm._en_id = en_id.EditValue
            frm._obj = pt_id_to
            frm.type_form = True
            frm.ShowDialog()
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub gv_edit_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_edit.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            browse_data()
        End If
    End Sub

    Private Sub gv_edit_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv_edit.DoubleClick
        browse_data()
    End Sub

    Private Sub browse_data()
        Dim _col As String = gv_edit.FocusedColumn.Name
        Dim _row As Integer = gv_edit.FocusedRowHandle
        Dim _en_id As Integer = sct_en_id.EditValue

        If _col = "ac_code" Or _col = "ac_name" Then
            Dim frm As New FAccountSearch
            frm.set_win(Me)
            frm._row = _row
            frm.type_form = True
            frm.ShowDialog()
        End If
    End Sub

End Class
