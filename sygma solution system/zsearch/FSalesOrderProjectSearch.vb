Imports master_new.ModFunction
Imports CoreLab.PostgreSql

Public Class FSalesOrderProjectSearch
    Public _row As Integer
    Public _en_id As Integer
    Public _obj As Object
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _now As DateTime
    Public _sopjg_oid_group As String
    Public _layout_id As Integer

    Private Sub FSalesOrderProjectSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "SO Number", "sopj_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Customer", "ptnr_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""
        If fobject.name = "FSoPjGroup" Or fobject.name = FBillOfQuantity.Name Then
            get_sequel = "SELECT  " _
                    & "  sopj_oid, " _
                    & "  sopj_dom_id, " _
                    & "  sopj_en_id, " _
                    & "  sopj_add_by, " _
                    & "  sopj_add_date, " _
                    & "  sopj_upd_by, " _
                    & "  sopj_upd_date, " _
                    & "  sopj_code, " _
                    & "  sopj_ptnr_id, " _
                    & "  sopj_date, " _
                    & "  sopj_si_id, " _
                    & "  en_desc, " _
                    & "  ptnr_name, " _
                    & "  si_desc " _
                    & "FROM  " _
                    & "  public.sopj_mstr " _
                    & "  inner join en_mstr on en_id = sopj_en_id " _
                    & "  inner join ptnr_mstr on ptnr_mstr.ptnr_id = sopj_ptnr_id " _
                    & "  inner join si_mstr on si_id = sopj_si_id " _
                    & "  where sopj_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and sopj_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and sopj_en_id = " + _en_id.ToString
        ElseIf fobject.name = "FSalesOrderProjectTransaction" Or fobject.name = "FSalesOrderProjectTransactionDP" Then
            get_sequel = "SELECT  " _
                    & "  sopj_oid, " _
                    & "  sopj_dom_id, " _
                    & "  sopj_en_id, " _
                    & "  sopj_add_by, " _
                    & "  sopj_add_date, " _
                    & "  sopj_upd_by, " _
                    & "  sopj_upd_date, " _
                    & "  sopj_code, " _
                    & "  sopj_ptnr_id, " _
                    & "  sopj_date, " _
                    & "  en_desc, " _
                    & "  ptnr_name " _
                    & "FROM  " _
                    & "  public.sopj_mstr " _
                    & "  inner join en_mstr on en_id = sopj_en_id " _
                    & "  inner join ptnr_mstr on ptnr_mstr.ptnr_id = sopj_ptnr_id " _
                    & "  where sopj_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & "  and sopj_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & "  and sopj_en_id = " + _en_id.ToString
        End If
        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gv_master.Focus()
    End Sub

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

        Dim ds_bantu As New DataSet

        If fobject.name = "FSoPjGroup" Then
            If _sopjg_oid_group <> "" Then
                Try
                    Using objinsert As New master_new.WDABasepgsql("", "")
                        With objinsert
                            .Connection.Open()
                            Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                            Try
                                .Command = .Connection.CreateCommand
                                .Command.Transaction = sqlTran

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "update sopjd_det set sopjd_sopjg_oid = null where sopjd_sopjg_oid = '" + _sopjg_oid_group + "'"
                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                sqlTran.Commit()

                            Catch ex As PgSqlException
                                sqlTran.Rollback()
                                MessageBox.Show(ex.Message)
                            End Try
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End If

            fobject._sopj_oid = ds.Tables(0).Rows(_row_gv).Item("sopj_oid")
            Try
                Using objcb As New master_new.WDABasepgsql("", "")
                    With objcb
                        .SQL = "SELECT  " _
                        & "  public.sopjd_det.sopjd_oid, " _
                        & "  public.sopjd_det.sopjd_dom_id, " _
                        & "  public.sopjd_det.sopjd_en_id, " _
                        & "  public.en_mstr.en_desc, " _
                        & "  public.sopjd_det.sopjd_add_by, " _
                        & "  public.sopjd_det.sopjd_add_date, " _
                        & "  public.sopjd_det.sopjd_upd_by, " _
                        & "  public.sopjd_det.sopjd_upd_date, " _
                        & "  public.sopjd_det.sopjd_sopj_oid, " _
                        & "  public.sopjd_det.sopjd_seq, " _
                        & "  public.sopjd_det.sopjd_si_id, " _
                        & "  public.si_mstr.si_desc, " _
                        & "  public.sopjd_det.sopjd_pt_id, " _
                        & "  public.pt_mstr.pt_code, " _
                        & "  public.pt_mstr.pt_desc1, " _
                        & "  public.pt_mstr.pt_desc2, " _
                        & "  public.sopjd_det.sopjd_rmks, " _
                        & "  public.sopjd_det.sopjd_qty, " _
                        & "  public.sopjd_det.sopjd_qty_allocated, " _
                        & "  public.sopjd_det.sopjd_qty_shipment, " _
                        & "  public.sopjd_det.sopjd_um, " _
                        & "  public.code_mstr.code_name, " _
                        & "  public.sopjd_det.sopjd_cost, " _
                        & "  public.sopjd_det.sopjd_price, " _
                        & "  public.sopjd_det.sopjd_disc, " _
                        & "  public.sopjd_det.sopjd_um_conv, " _
                        & "  public.sopjd_det.sopjd_qty_real, " _
                        & "  public.sopjd_det.sopjd_status, " _
                        & "  public.sopjd_det.sopjd_sopjg_oid, " _
                        & "  public.sopjg_group.sopjg_group_number, " _
                        & "  public.sopjd_det.sopjd_dt, " _
                        & "  sopjd_loc_eu_site_id, loc_desc, " _
                        & "  sopjd_taxable, sopjd_tax_inc, sopjd_tax_class, " _
                        & "  taxclass_mstr.code_name as sopjd_tax_class_name " _
                        & "  FROM " _
                        & "  public.sopjd_det " _
                        & "  INNER JOIN public.en_mstr ON (public.sopjd_det.sopjd_en_id = public.en_mstr.en_id) " _
                        & "  INNER JOIN public.si_mstr ON (public.sopjd_det.sopjd_si_id = public.si_mstr.si_id) " _
                        & "  INNER JOIN public.pt_mstr ON (public.sopjd_det.sopjd_pt_id = public.pt_mstr.pt_id) " _
                        & "  INNER JOIN public.code_mstr ON (public.sopjd_det.sopjd_um = public.code_mstr.code_id) " _
                        & "  INNER JOIN public.sopj_mstr ON (public.sopjd_det.sopjd_sopj_oid = public.sopj_mstr.sopj_oid) " _
                        & "  INNER JOIN public.code_mstr as taxclass_mstr ON taxclass_mstr.code_id  = public.sopjd_det.sopjd_tax_class " _
                        & "  LEFT OUTER JOIN public.loc_mstr ON (public.sopjd_det.sopjd_loc_eu_site_id = public.loc_mstr.loc_id) " _
                        & "  LEFT OUTER JOIN public.sopjg_group ON (public.sopjd_det.sopjd_sopjg_oid = public.sopjg_group.sopjg_oid) " _
                        & "  where sopjd_sopj_oid = '" + ds.Tables(0).Rows(_row_gv).Item("sopj_oid") + "'" _
                        & "  and sopjd_sopjg_oid is null "

                        .InitializeCommand()
                        .FillDataSet(ds_bantu, "sopjd_det")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            fobject._sopj_oid = ds.Tables(0).Rows(_row_gv).Item("sopj_oid")
            fobject.sopj_code.text = ds.Tables(0).Rows(_row_gv).Item("sopj_code")
            fobject.ds_edit = New DataSet
            fobject.ds_edit = ds_bantu
            fobject.ds_edit.Tables(0).AcceptChanges()
            fobject.gc_edit.DataSource = fobject.ds_edit.tables(0)
            fobject.gv_edit.BestFitColumns()

            If ds_bantu.Tables(0).Rows.Count < 1 Then
                MsgBox("All Sales Order Project Has Been in Group", MsgBoxStyle.Critical, "No Data Available")
                fobject.sopj_code.text = ""
            End If
        ElseIf fobject.name = "FSalesOrderProjectTransaction" Then
            Dim _seq As Integer = -1
            Dim _sql As String = ""
            Try
                Using objcek As New master_new.WDABasepgsql("", "")
                    With objcek
                        .Connection.Open()
                        .Command = .Connection.CreateCommand
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "select sopjdd_seq from sopjdd_det where sopjdd_sopj_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sopj_oid")) + _
                                               " and sopjdd_layout_id = " + _layout_id.ToString
                        .InitializeCommand()
                        .DataReader = .Command.ExecuteReader
                        While .DataReader.Read
                            _seq = .DataReader("sopjdd_seq")
                        End While
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            If _seq = 1 Then
                _sql = "select sopjd_oid, sopjd_pt_id, pt_code, pt_desc1, pt_desc2, '' as sotrand_related_oid, " _
                    & " sopjd_qty, sopjd_qty_full, sopjd_qty - coalesce(sopjd_qty_full,0) as qty_open,  sopjd_qty - coalesce(sopjd_qty_full,0) as sotrand_qty " _
                    & " from sopjd_det " _
                    & " inner join sopj_mstr on sopj_oid = sopjd_sopj_oid " _
                    & " inner join pt_mstr on pt_id = sopjd_pt_id " _
                    & " where sopj_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sopj_oid"))

                Try
                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            .SQL = _sql
                            .InitializeCommand()
                            .FillDataSet(ds_bantu, "data")
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try

                fobject.ds_edit = New DataSet
                fobject.ds_edit = ds_bantu
                fobject.ds_edit.Tables(0).AcceptChanges()
                fobject.gc_edit.DataSource = fobject.ds_edit.tables(0)
                fobject.gv_edit.BestFitColumns()
            End If

            fobject._sopj_oid = ds.Tables(0).Rows(_row_gv).Item("sopj_oid")
            fobject.sopj_code.text = ds.Tables(0).Rows(_row_gv).Item("sopj_code")

            If _seq = 1 Then
                fobject.gc_edit.EmbeddedNavigator.Buttons.Append.Visible = False
                fobject.gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
            Else
                fobject.gc_edit.EmbeddedNavigator.Buttons.Append.Visible = True
                fobject.gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
            End If
        ElseIf fobject.name = "FSalesOrderProjectTransactionDP" Then
            Dim _seq As Integer = -1
            Dim _sql As String = ""
            Try
                Using objcek As New master_new.WDABasepgsql("", "")
                    With objcek
                        .Connection.Open()
                        .Command = .Connection.CreateCommand
                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "select sopjdp_seq from sopjdp_det where sopjdp_sopj_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sopj_oid")) + _
                                               " and sopjdp_layout_id = " + _layout_id.ToString
                        .InitializeCommand()
                        .DataReader = .Command.ExecuteReader
                        While .DataReader.Read
                            _seq = .DataReader("sopjdp_seq")
                        End While
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            If _seq = 1 Then
                _sql = "select sopjd_oid, sopjd_pt_id, pt_code, pt_desc1, pt_desc2, '' as sotrand_related_oid, " _
                    & " sopjd_qty, sopjd_qty_full, sopjd_qty - coalesce(sopjd_dp_qty_full,0) as qty_open,  sopjd_qty - coalesce(sopjd_dp_qty_full,0) as sotrand_qty " _
                    & " from sopjd_det " _
                    & " inner join sopj_mstr on sopj_oid = sopjd_sopj_oid " _
                    & " inner join pt_mstr on pt_id = sopjd_pt_id " _
                    & " where sopj_oid = " + SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("sopj_oid"))

                Try
                    Using objcb As New master_new.WDABasepgsql("", "")
                        With objcb
                            .SQL = _sql
                            .InitializeCommand()
                            .FillDataSet(ds_bantu, "data")
                        End With
                    End Using
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try

                fobject.ds_edit = New DataSet
                fobject.ds_edit = ds_bantu
                fobject.ds_edit.Tables(0).AcceptChanges()
                fobject.gc_edit.DataSource = fobject.ds_edit.tables(0)
                fobject.gv_edit.BestFitColumns()
            End If

            fobject._sopj_oid = ds.Tables(0).Rows(_row_gv).Item("sopj_oid")
            fobject.sopj_code.text = ds.Tables(0).Rows(_row_gv).Item("sopj_code")

            If _seq = 1 Then
                fobject.gc_edit.EmbeddedNavigator.Buttons.Append.Visible = False
                fobject.gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = False
            Else
                fobject.gc_edit.EmbeddedNavigator.Buttons.Append.Visible = True
                fobject.gc_edit.EmbeddedNavigator.Buttons.Remove.Visible = True
            End If
        ElseIf fobject.name = FBillOfQuantity.Name Then
            fobject.boq_sopj_oid.text = ds.Tables(0).Rows(_row_gv).Item("sopj_code")
            fobject.boq_sopj_oid.tag = ds.Tables(0).Rows(_row_gv).Item("sopj_oid")

            Dim ssql As String
            ssql = "SELECT  " _
                & "  a.sopjd_pt_id, " _
                & "  b.pt_code, " _
                & "  b.pt_desc1, " _
                & "  b.pt_desc2, " _
                & "  a.sopjd_um, " _
                & "  a.sopjd_qty as boqd_qty, " _
                & "  c.code_code,a.sopjd_oid " _
                & "FROM " _
                & "  public.sopjd_det a " _
                & "  INNER JOIN public.pt_mstr b ON (a.sopjd_pt_id = b.pt_id) " _
                & "  INNER JOIN public.code_mstr c ON (a.sopjd_um = c.code_id) " _
                & "WHERE " _
                & "  a.sopjd_sopj_oid = '" & ds.Tables(0).Rows(_row_gv).Item("sopj_oid") & "' " _
                & " ORDER by sopjd_seq "

            Dim dt As New DataTable
            dt = master_new.PGSqlConn.GetTableData(ssql)

            fobject.gc_edit.datasource = dt
            fobject.gv_edit.BestFitColumns()

            ssql = ""
            For i As Integer = 0 To dt.Rows.Count - 1
                With dt.Rows(i)

                    ssql += "SELECT  psd_pt_bom_id as pt_id,psd_comp as pt_code,   " _
                       & "psd_desc as pt_desc1,psd_ref as pt_desc2, psdgroupdesc as code_code,psd_group as pt_um, " _
                       & "psd_qty as boqs_qty,cast('N' as CHAR) as boqs_is_manual " _
                       & " from public.get_all_simulated('" & .Item("pt_code") & "',  " _
                       & SetNumber(.Item("boqd_qty")) & ", " & "'Y'" & "," _
                       & SetNumber(ds.Tables(0).Rows(_row_gv).Item("sopj_en_id")) & ",'Y', CURRENT_DATE) "

                    If i <> dt.Rows.Count - 1 Then
                        ssql += " UNION "
                    End If

                End With
            Next

            ssql = "SELECT pt_id,pt_code,pt_desc1,pt_desc2,code_code,boqs_is_manual,sum(boqs_qty) as boqs_qty from (" & ssql _
                & ") as temp group by pt_id,pt_code,pt_desc1,pt_desc2,code_code,boqs_is_manual"

            dt = master_new.PGSqlConn.GetTableData(ssql)

            fobject.gc_stand_edit.datasource = dt
            fobject.gv_stand_edit.BestFitColumns()

            fobject.gc_stand_edit.EmbeddedNavigator.Buttons.Append.visible = False
            fobject.gc_stand_edit.EmbeddedNavigator.Buttons.Remove.visible = False
            fobject.gv_stand_edit.OptionsBehavior.Editable = False
        End If
    End Sub
End Class

