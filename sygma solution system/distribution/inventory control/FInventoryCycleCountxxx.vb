Imports CoreLab.PostgreSql

Imports master_new.ModFunction
Imports DevExpress.XtraExport

Public Class FInventoryCycleCount
    Dim _now As DateTime
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Dim _pt_ls As String = ""
    Dim _pt_cost As Double
    Dim _ds_select As New DataSet
    Dim _error As Boolean = False
    Dim _ds_cek_pt As New DataSet
    Dim _ds_err As New DataSet

    Private Sub FInventoryCycleCount_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now
        pr_txttglawal.DateTime = _now
        pr_txttglakhir.DateTime = _now
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        ccre_en_id.Properties.DataSource = dt_bantu
        ccre_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        ccre_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        ccre_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_ccre_type())
        ccre_type.Properties.DataSource = dt_bantu
        ccre_type.Properties.DisplayMember = dt_bantu.Columns("display").ToString
        ccre_type.Properties.ValueMember = dt_bantu.Columns("value").ToString
        ccre_type.ItemIndex = 0
    End Sub

    Private Sub ccre_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ccre_en_id.EditValueChanged
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_pt_mstr(ccre_en_id.EditValue))
        'ccre_pt_id.Properties.DataSource = dt_bantu
        'ccre_pt_id.Properties.DisplayMember = dt_bantu.Columns("pt_code").ToString
        'ccre_pt_id.Properties.ValueMember = dt_bantu.Columns("pt_id").ToString
        'ccre_pt_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_si_mstr(ccre_en_id.EditValue))
        ccre_si_id.Properties.DataSource = dt_bantu
        ccre_si_id.Properties.DisplayMember = dt_bantu.Columns("si_desc").ToString
        ccre_si_id.Properties.ValueMember = dt_bantu.Columns("si_id").ToString
        ccre_si_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_loc_mstr(ccre_en_id.EditValue))
        ccre_loc_id.Properties.DataSource = dt_bantu
        ccre_loc_id.Properties.DisplayMember = dt_bantu.Columns("loc_desc").ToString
        ccre_loc_id.Properties.ValueMember = dt_bantu.Columns("loc_id").ToString
        ccre_loc_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_code_mstr(ccre_en_id.EditValue, "unitmeasure"))
        'ccre_um_id.Properties.DataSource = dt_bantu
        'ccre_um_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        'ccre_um_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        'ccre_um_id.ItemIndex = 0

        'ccre_um_id.EditValue = ccre_pt_id.GetColumnValue("pt_um")
    End Sub

    Private Sub ccre_pt_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'ccre_um_id.EditValue = ccre_pt_id.GetColumnValue("pt_um")

        'Try
        '    Using objcek As New master_new.WDABasepgsql("", "")
        '        With objcek
        '            .Connection.Open()
        '            .Command = .Connection.CreateCommand
        '            .Command.CommandType = CommandType.Text
        '            .Command.CommandText = "select pt_cost, pt_ls from pt_mstr where pt_id = " + ccre_pt_id.EditValue.ToString
        '            .InitializeCommand()
        '            .DataReader = .Command.ExecuteReader
        '            While .DataReader.Read
        '                ccre_cost.Text = .DataReader("pt_cost").ToString
        '                _pt_ls = .DataReader("pt_ls")
        '            End While
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub

    Public Overrides Sub format_grid()
        add_column_copy(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date", "ccre_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "Type", "ccre_type", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Part Number", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description1", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description2", "pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Lot/Serial", "ccre_lot_serial", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Qty", "ccre_qty", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "UM", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "UM Conversion", "ccre_um_conv", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Qty Real", "ccre_qty_real", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "Cost", "ccre_cost", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n")
        add_column_copy(gv_master, "User Create", "ccre_add_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Create", "ccre_add_date", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_master, "User Update", "ccre_upd_by", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Date Update", "ccre_upd_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT  " _
                    & "  ccre_oid, " _
                    & "  ccre_date, " _
                    & "  ccre_add_by, " _
                    & "  ccre_add_date, " _
                    & "  ccre_upd_by, " _
                    & "  ccre_upd_date, " _
                    & "  ccre_type, " _
                    & "  ccre_pt_id, " _
                    & "  ccre_si_id, " _
                    & "  ccre_loc_id, " _
                    & "  ccre_lot_serial, " _
                    & "  ccre_qty, " _
                    & "  ccre_um_id, " _
                    & "  ccre_um_conv, " _
                    & "  ccre_qty_real, " _
                    & "  ccre_cost, " _
                    & "  ccre_dt, " _
                    & "  ccre_en_id, " _
                    & "  en_desc, " _
                    & "  pt_code, " _
                    & "  pt_desc1, " _
                    & "  pt_desc2, " _
                    & "  si_desc, " _
                    & "  loc_desc, " _
                    & "  um_master.code_name as um_name " _
                    & "FROM  " _
                    & "  public.ccre_mstr " _
                    & "  inner join en_mstr on en_id = ccre_en_id " _
                    & "  inner join pt_mstr on pt_id = ccre_pt_id " _
                    & "  inner join si_mstr on si_id = ccre_si_id " _
                    & "  inner join loc_mstr on loc_id = ccre_loc_id " _
                    & "  inner join code_mstr um_master on um_master.code_id = ccre_um_id" _
                    & " where ccre_date >= " + SetDate(pr_txttglawal.DateTime.Date) _
                    & " and ccre_date <= " + SetDate(pr_txttglakhir.DateTime.Date) _
                    & " and ccre_en_id in (select user_en_id from tconfuserentity " _
                                       & " where userid = " + master_new.ClsVar.sUserID.ToString + ")"
        Return get_sequel
    End Function

    Public Overrides Sub insert_data_awal()
        ccre_en_id.Focus()
        ccre_en_id.ItemIndex = 0
        'ccre_pt_id.ItemIndex = 0
        ccre_loc_id.ItemIndex = 0
        ccre_type.ItemIndex = 0
        'ccre_lot_serial.Text = ""
        'ccre_qty.EditValue = 0
        'ccre_um_conv.EditValue = 1
        'ccre_cost.EditValue = 0
        'ccre_qty_real.EditValue = 0
        'ccre_qty_real.Properties.ReadOnly = True
    End Sub

    Public Overrides Function delete_data() As Boolean
        MessageBox.Show("Delete Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function edit_data() As Boolean
        MessageBox.Show("Edit Data Not Available For This Menu..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        Dim _date As Date = func_coll.get_tanggal_sistem
        Dim _gcald_det_status As String = func_data.get_gcald_det_status(ccre_en_id.EditValue, "gcald_ic", _date)

        If _gcald_det_status = "" Then
            MessageBox.Show("GL Calendar Doesn't Exist For This Periode :" + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf _gcald_det_status.ToUpper = "Y" Then
            MessageBox.Show("Closed Transaction At GL Calendar For This Periode : " + _date, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If _error = True Then
            MessageBox.Show("Save Data Tidak Bisa Dilakukan,,! Terdapat Kesalahan Part Number atau UM,,! Silahkan Cek error log,,!", "err", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        'If _pt_ls.ToUpper = "S" Then
        '    If ccre_qty.EditValue > 1 Then
        '        MessageBox.Show("Qty Can't Higher Than 1 For Serial Item..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If
        'End If

        'If _pt_ls.ToUpper = "S" Then
        '    If Trim(ccre_lot_serial.Text) = "" Then
        '        MessageBox.Show("Serial Number Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If
        'End If

        'If _pt_ls.ToUpper = "L" Then
        '    If Trim(ccre_lot_serial.Text) = "" Then
        '        MessageBox.Show("Lot Number Can't Empty..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return False
        '    End If
        'End If

        'If ccre_type.EditValue.ToString.ToUpper = "I" Then
        '    If ccre_qty_real.EditValue < 1 Then
        '        MessageBox.Show("Qty Can't Lower Than 1..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        ccre_qty.Focus()
        '        Return False
        '    End If
        'End If

        Return before_save
    End Function

    Function GetPtID(ByVal _par_code As String) As Integer
        GetPtID = 0
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "SELECT pt_id,pt_code FROM  " _
                                        & "  public.pt_mstr  " _
                                        & "WHERE  " _
                                        & " pt_code = " + SetSetring(_par_code) & "  " _
                                        & ";"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        GetPtID = .DataReader("pt_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return GetPtID
    End Function

    Function GetUmID(ByVal _par_um_code As String) As Integer
        GetUmID = 0
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "SELECT code_id,code_name FROM  " _
                                        & "  public.code_mstr  " _
                                        & "WHERE  " _
                                        & " code_name = " + SetSetring(_par_um_code) & "  " _
                                        & ";"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        GetUmID = .DataReader("code_id")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return GetUmID
    End Function

    Public Sub GetLSCost(ByVal _par_pt_code As String)
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select pt_cost, pt_ls from pt_mstr where pt_code = " + SetSetring(_par_pt_code)
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        _pt_cost = .DataReader("pt_cost")
                        _pt_ls = .DataReader("pt_ls")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Overrides Function insert_data() As Boolean
        MyBase.insert_data()
        Try
            _ds_select.Tables(0).Clear()
        Catch ex As Exception

        End Try

        path_file.Text = ""

    End Function

    Public Function get_qty_old() As Double
        get_qty_old = 0
        Try
            Using objcek As New master_new.WDABasepgsql("", "")
                With objcek
                    .Connection.Open()
                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "select coalesce(sum(invc_qty),0) as sum_qty from invc_mstr " _
                                         & " where invc_en_id = " + ccre_en_id.EditValue.ToString _
                                         & " and invc_si_id =  " + ccre_si_id.EditValue.ToString _
                                         & " and invc_loc_id =  " + ccre_loc_id.EditValue.ToString _
                                         & " and invc_pt_id =  " + _pt_id_global.ToString _
                                         & " and coalesce(invc_serial,'') = '" + Trim(ccre_lot_serial.Text) + "'"
                    .InitializeCommand()
                    .DataReader = .Command.ExecuteReader
                    While .DataReader.Read
                        get_qty_old = .DataReader("sum_qty")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Function
        End Try

        Return get_qty_old
    End Function

    Public Overrides Function insert() As Boolean
        Dim _ccre_oid As Guid

        Dim _serial, _cost_methode, _pt_code As String
        Dim _tran_id, _en_id, _si_id, _loc_id, _pt_id, _um_id, _qty As Integer
        Dim _cost As Double
        Dim ssqls As New ArrayList

        If ccre_type.EditValue.ToString.ToUpper = "I" Then
            _tran_id = func_coll.get_id_tran_mstr("cyc-cnt")
        ElseIf ccre_type.EditValue.ToString.ToUpper = "R" Then
            _tran_id = func_coll.get_id_tran_mstr("cyc-rcnt")
        End If

        If _tran_id = -1 Then
            MessageBox.Show("Inventory Begining Balance In Transaction Master Doesn't Exist", "Error..", MessageBoxButtons.OK, MessageBoxIcon.Error)
            insert = False
            Exit Function
        End If

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        For Each _dr As DataRow In _ds_select.Tables(0).Rows
                            GetLSCost(_dr("part_number").ToString())

                            _ccre_oid = Guid.NewGuid
                            _pt_id = GetPtID(_dr("part_number").ToString())
                            _um_id = GetUmID(_dr("um").ToString())

                            .Command.CommandType = CommandType.Text
                            .Command.CommandText = "INSERT INTO  " _
                                                & "  public.ccre_mstr " _
                                                & "( " _
                                                & "  ccre_oid, " _
                                                & "  ccre_date, " _
                                                & "  ccre_add_by, " _
                                                & "  ccre_add_date, " _
                                                & "  ccre_type, " _
                                                & "  ccre_pt_id, " _
                                                & "  ccre_si_id, " _
                                                & "  ccre_loc_id, " _
                                                & "  ccre_lot_serial, " _
                                                & "  ccre_qty, " _
                                                & "  ccre_um_id, " _
                                                & "  ccre_um_conv, " _
                                                & "  ccre_qty_real, " _
                                                & "  ccre_cost, " _
                                                & "  ccre_qty_old, " _
                                                & "  ccre_dt, " _
                                                & "  ccre_en_id " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(_ccre_oid.ToString) & ",  " _
                                                & "current_date " & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "current_timestamp " & ",  " _
                                                & SetSetring(ccre_type.EditValue) & ",  " _
                                                & SetInteger(_pt_id) & ", " _
                                                & SetInteger(ccre_si_id.EditValue) & ",  " _
                                                & SetInteger(ccre_loc_id.EditValue) & ",  " _
                                                & SetSetring(_dr("lot_serial")) & ",  " _
                                                & SetDbl(_dr("qty")) & ",  " _
                                                & SetInteger(_um_id) & ", " _
                                                & SetDbl(_dr("um_convertion")) & ",  " _
                                                & SetDbl(_dr("qty_real")) & ",  " _
                                                & SetDbl(_pt_cost) & ",  " _
                                                & SetDbl(get_qty_old()) & ",  " _
                                                & "current_timestamp" & ",  " _
                                                & SetInteger(ccre_en_id.EditValue) & "  " _
                                                & ")"
                            ssqls.Add(.Command.CommandText)
                            .Command.ExecuteNonQuery()
                            .Command.Parameters.Clear()

                            If ccre_type.EditValue = "R" Then
                                'Update Table Inventory Dan Cost Inventory Dan History Inventory
                                '1. Update invc_mstr dan invh_mstr untuk barang yang bukan serial

                                If _dr("qty_real") > 0 Then
                                    _en_id = ccre_en_id.EditValue
                                    _si_id = ccre_si_id.EditValue
                                    _loc_id = ccre_loc_id.EditValue
                                    _serial = IIf(Trim(_dr("lot_serial")) = "", "''", Trim(_dr("lot_serial")))
                                    _qty = _dr("qty_real")
                                    If func_coll.update_invc_mstr_plus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If

                                    'Update History Inventory                                    
                                    _cost = _pt_cost
                                    If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, 1, _en_id, "", _ccre_oid.ToString, "Cycle Count Recount", "R", _si_id, _loc_id, _pt_id, _qty, _cost, "") = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If

                                    '2. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                                    _cost_methode = func_coll.get_pt_cost_method(_pt_id.ToString.ToUpper)
                                    _en_id = ccre_en_id.EditValue
                                    _qty = _dr("qty_real")
                                    _cost = _pt_cost
                                    If _cost_methode = "F" Or _cost_methode = "L" Then
                                        If func_coll.update_invct_table_plus(ssqls, objinsert, _en_id, _pt_id, _qty, _cost) = False Then
                                            sqlTran.Rollback()
                                            insert = False
                                            Exit Function
                                        End If
                                    ElseIf _cost_methode = "A" Then
                                        If func_coll.update_item_cost_avg(ssqls, objinsert, "+", _en_id, _si_id, _pt_id, _qty, _cost) = False Then
                                            sqlTran.Rollback()
                                            insert = False
                                            Exit Function
                                        End If
                                    End If

                                    'Insert ke table glt_det
                                    'Di proses ini langsung ke prodline account tidak ke prodline location account...
                                    'untuk sementara saja..kalau dibutuhkan tinggal dirubah kodingannya untuk baca account prodline aja
                                    'If func_coll.insert_glt_det_ic(objinsert, ds_edit, _
                                    '                         rcv_en_id.EditValue, rcv_en_id.GetColumnValue("en_code"), _
                                    '                         _rcv_oid.ToString, _rcv_code, _
                                    '                         func_coll.get_tanggal_sistem, _
                                    '                         rcv_cu_id.EditValue, rcv_exc_rate.EditValue, _
                                    '                         "IC", "IC-RPO") = False Then
                                    '    sqlTran.Rollback()
                                    '    insert = False
                                    '    Exit Function
                                    'End If
                                ElseIf _dr("qty_real") < 0 Then
                                    _en_id = ccre_en_id.EditValue
                                    _si_id = ccre_si_id.EditValue
                                    _loc_id = ccre_loc_id.EditValue
                                    _pt_code = _dr("part_number")
                                    _serial = IIf(Trim(_dr("lot_serial")) = "", "''", Trim(_dr("lot_serial")))
                                    _qty = _dr("qty_real")
                                    If func_coll.update_invc_mstr_minus(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _pt_code, _serial, _qty) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If

                                    'Update History Inventory                                    
                                    _cost = _pt_cost
                                    If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, 1, _en_id, "", _ccre_oid.ToString, "Cycle Count Recount", "R", _si_id, _loc_id, _pt_id, _qty, _cost, "") = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If

                                    '2. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                                    _cost_methode = func_coll.get_pt_cost_method(GetPtID(_dr("part_number").ToString()).ToString.ToUpper)
                                    _en_id = ccre_en_id.EditValue
                                    _qty = _dr("qty_real")
                                    _cost = _pt_cost
                                    If _cost_methode = "F" Or _cost_methode = "L" Then
                                        If func_coll.update_invct_table_minus(ssqls, objinsert, _en_id, _pt_id, _qty, _cost_methode) = False Then
                                            sqlTran.Rollback()
                                            insert = False
                                            Exit Function
                                        End If
                                    ElseIf _cost_methode = "A" Then
                                        If func_coll.update_item_cost_avg(ssqls, objinsert, "-", _en_id, _si_id, _pt_id, _qty, _cost) = False Then
                                            sqlTran.Rollback()
                                            insert = False
                                            Exit Function
                                        End If
                                    End If

                                    'Insert ke table glt_det
                                    'Di proses ini langsung ke prodline account tidak ke prodline location account...
                                    'untuk sementara saja..kalau dibutuhkan tinggal dirubah kodingannya untuk baca account prodline aja
                                    'If func_coll.insert_glt_det_ic(objinsert, ds_edit, _
                                    '                         rcv_en_id.EditValue, rcv_en_id.GetColumnValue("en_code"), _
                                    '                         _rcv_oid.ToString, _rcv_code, _
                                    '                         func_coll.get_tanggal_sistem, _
                                    '                         rcv_cu_id.EditValue, rcv_exc_rate.EditValue, _
                                    '                         "IC", "IC-RPO") = False Then
                                    '    sqlTran.Rollback()
                                    '    insert = False
                                    '    Exit Function
                                    'End If
                                End If
                            ElseIf ccre_type.EditValue = "I" Then
                                If _dr("qty_real") > 0 Then
                                    _en_id = ccre_en_id.EditValue
                                    _si_id = ccre_si_id.EditValue
                                    _loc_id = ccre_loc_id.EditValue
                                    _serial = IIf(Trim(_dr("lot_serial")) = "", "''", Trim(_dr("lot_serial")))
                                    _qty = _dr("qty_real")
                                    If func_coll.update_invc_mstr_adj(ssqls, objinsert, _en_id, _si_id, _loc_id, _pt_id, _serial, _qty) = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If

                                    'Update History Inventory                                    
                                    _cost = _pt_cost
                                    If func_coll.update_invh_mstr(ssqls, objinsert, _tran_id, 1, _en_id, "", _ccre_oid.ToString, "Cycle Count Recount", "I", _si_id, _loc_id, _pt_id, _qty, _cost, "") = False Then
                                        sqlTran.Rollback()
                                        insert = False
                                        Exit Function
                                    End If

                                    '2. Update invct_table untuk barang yang pt_cost_methode Fifo dan Lifo dan Average
                                    _cost_methode = func_coll.get_pt_cost_method(_pt_id.ToString().ToString.ToUpper)
                                    _en_id = ccre_en_id.EditValue
                                    _qty = _dr("qty_real")
                                    _cost = _pt_cost
                                    If _cost_methode = "F" Or _cost_methode = "L" Then
                                        'If func_coll.update_invct_table_plus(objinsert, _en_id, _pt_id, _qty, _cost) = False Then
                                        '    sqlTran.Rollback()
                                        '    insert = False
                                        '    Exit Function
                                        'End If
                                        MessageBox.Show("Coding Not Available..please contact your programer..")
                                        insert = False
                                        Exit Function
                                    ElseIf _cost_methode = "A" Then
                                        If func_coll.update_item_cost_begining_balance(ssqls, objinsert, _en_id, _pt_id, _cost) = False Then
                                            sqlTran.Rollback()
                                            insert = False
                                            Exit Function
                                        End If
                                    End If

                                    'Insert ke table glt_det
                                    'Di proses ini langsung ke prodline account tidak ke prodline location account...
                                    'untuk sementara saja..kalau dibutuhkan tinggal dirubah kodingannya untuk baca account prodline aja
                                    'If func_coll.insert_glt_det_ic(objinsert, ds_edit, _
                                    '                         rcv_en_id.EditValue, rcv_en_id.GetColumnValue("en_code"), _
                                    '                         _rcv_oid.ToString, _rcv_code, _
                                    '                         func_coll.get_tanggal_sistem, _
                                    '                         rcv_cu_id.EditValue, rcv_exc_rate.EditValue, _
                                    '                         "IC", "IC-RPO") = False Then
                                    '    sqlTran.Rollback()
                                    '    insert = False
                                    '    Exit Function
                                    'End If
                                End If
                            End If

                            If MyPGDll.PGSqlConn.status_sync = True Then
                                For Each Data As String In MyPGDll.ClassSync.FinsertSQL2Array(ssqls)
                                    .Command.CommandType = CommandType.Text
                                    .Command.CommandText = Data
                                    .Command.ExecuteNonQuery()
                                    .Command.Parameters.Clear()
                                Next
                            End If

                        Next


                        sqlTran.Commit()
                        after_success()
                        set_row(_ccre_oid.ToString, "ccre_oid")
                        insert = True
                    Catch ex As PgSqlException
                        sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
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

    'Private Sub ccre_qty_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    ccre_qty_real.EditValue = ccre_qty.EditValue * ccre_um_conv.EditValue
    'End Sub

    'Private Sub ccre_um_conv_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    ccre_qty_real.EditValue = ccre_qty.EditValue * ccre_um_conv.EditValue
    'End Sub

    Private Sub path_file_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles path_file.ButtonClick
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Text Files (*.xls)|*.xls|All Files (*.xls*)|*.xls*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            path_file.Text = OpenFileDialog.FileName

            master_new.ClsVar._filename = path_file.Text
            load_from_excel(True, gc_import)

        End If
    End Sub

    Public Sub load_from_excel(ByVal arg As Boolean, ByVal gc As DevExpress.XtraGrid.GridControl)
        Cursor = Cursors.WaitCursor

        _ds_select = New DataSet
        If arg <> False Then
            Try
                Using objload As New master_new.WDABaseexcell("", "")
                    With objload
                        .SQL = "select part_number, " + _
                               " lot_serial,qty,um,um_convertion,qty_real " + _
                               " from [Sheet1$] " + _
                               " order by part_number asc "
                        .InitializeCommand()
                        .FillDataSet(_ds_select, "select")
                        gc.DataSource = _ds_select.Tables(0)
                        bestfit_column()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If

        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()

                    .Command = .Connection.CreateCommand
                    .Command.CommandType = CommandType.Text
                    .Command.CommandText = "DELETE FROM  " _
                                        & "  public.err_log  " _
                                        & "WHERE  " _
                                        & "err_usr_id = " + SetInteger(master_new.ClsVar.sUserID) & "  " _
                                        & ";"
                    .Command.ExecuteNonQuery()
                    .Command.Parameters.Clear()
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try


        For Each _dr As DataRow In _ds_select.Tables("select").Rows
            _ds_cek_pt = New DataSet
            Try
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        .SQL = "select pt_code,pt_id,pt_um,code_name " + _
                               " from pt_mstr " + _
                               " inner join code_mstr on code_id = pt_um " + _
                               " where pt_code = " + SetSetring(_dr("part_number")) + _
                               " and code_name = " + SetSetring(_dr("um"))
                        .InitializeCommand()
                        .FillDataSet(_ds_cek_pt, "cek")
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            If _ds_cek_pt.Tables(0).Rows.Count = 0 Then
                _error = True
                Try
                    Using objinsert As New master_new.WDABasepgsql("", "")
                        With objinsert
                            .Connection.Open()
                            Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                            Try
                                .Command = .Connection.CreateCommand
                                .Command.Transaction = sqlTran

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "DELETE FROM  " _
                                                    & "  public.err_log  " _
                                                    & "WHERE  " _
                                                    & "err_usr_id = " + SetInteger(master_new.ClsVar.sUserID) & "  " _
                                                    & ";"

                                .Command.ExecuteNonQuery()
                                .Command.Parameters.Clear()

                                .Command.CommandType = CommandType.Text
                                .Command.CommandText = "INSERT INTO  " _
                                                    & "  public.err_log " _
                                                    & "( " _
                                                    & "  err_code_1, " _
                                                    & "  err_code_2, " _
                                                    & "  err_code_3, " _
                                                    & "  err_usr_id " _
                                                    & ")  " _
                                                    & "VALUES ( " _
                                                    & SetSetring(_dr("part_number")) & ",  " _
                                                    & "'-'" & ", " _
                                                    & "'-'" & ", " _
                                                    & SetInteger(master_new.ClsVar.sUserID) & "  " _
                                                    & ");"
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
                    row = 0
                    MessageBox.Show(ex.Message)
                End Try

            End If

        Next

        If _error = True Then
            Dim filename As String = "c:\syspro\error_log.xls"
            Dim _ds_err As New DataSet
            Try
                Using objload As New master_new.WDABasepgsql("", "")
                    With objload
                        .SQL = "select err_code_1,err_code_2,err_code_3 " + _
                               " from err_log "
                        .InitializeCommand()
                        .FillDataSet(_ds_err, "err")
                        gc_export.DataSource = _ds_err.Tables(0)
                        gv_export.BestFitColumns()
                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

            MessageBox.Show("Terdapat Kesalahan Part Number atau Unit Measure,,! Silahkan Cek error log,,!", "err", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ExportTo(gv_export, New ExportXlsProvider(filename))
        End If

        Cursor = Cursors.Arrow
    End Sub

    'Private Sub btn_retrieve_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_retrieve.Click
    '    master_new.ClsVar._filename = path_file.Text
    '    load_from_excel(True, gc_import)
    'End Sub

End Class
