Imports master_new.ModFunction

Public Class FUMConvSearch
    Public _row, _en_id, _pt_id, _pt_um As Integer
    Public _pt_cost, _pod_um_conv, _psd_um_conv, _prj_code As Double
    Public _obj As Object

    Private Sub FUMConvSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        Me.Width = 371
        Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        'add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "code_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Name", "code_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "code_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "UM Conv", "code_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "select en_desc, code_id, code_code, code_name, code_desc " + _
                     " from code_mstr " + _
                     " inner join en_mstr on code_en_id = en_id " + _
                     " where code_field ~~* 'unitmeasure'" _
                    & " and (code_code ~~* '%" + Trim(te_search.Text) + "%' or code_name ~~* '%" + Trim(te_search.Text) + "%' or code_desc ~~* '%" + Trim(te_search.Text) + "%')" _
                    & " and code_active ~~* 'Y'" _
                    & " and code_en_id in (0," + _en_id.ToString + ")" _
                    & " order by code_desc"
        Return get_sequel
        'get_sequel = "select um_conv from um_mstr " + _
        '                   " where um_pt_id = " + _pt_id.ToString + _
        '                   " and um_pt_um = " + _pt_um.ToString + _
        '                   " and um_pt_um_alt = (select pt_um from pt_mstr where pt_id = " + _pt_id.ToString + ") "
        'Return get_sequel

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
        Dim ds_bantu As New DataSet

        Dim _um_conv As Double = 1
        Dim _pod_qty As Double = 0
        Dim _psd_qty As Double = 0
        Try
            Using objcek As New master_new.CustomCommand
                With objcek
                    '.Connection.Open()
                    '.Command = .Connection.CreateCommand
                    '.Command.CommandType = CommandType.Text

                    .Command.CommandText = "select um_conv from um_mstr " + _
                           " where um_pt_id = " + _pt_id.ToString + _
                           " and um_pt_um = " + ds.Tables(0).Rows(_row_gv).Item("code_id").ToString + _
                           " and um_pt_um_alt = (select pt_um from pt_mstr where pt_id = " + _pt_id.ToString + ") "
                    .InitializeCommand()
                    .DataReader = .ExecuteReader
                    While .DataReader.Read
                        _um_conv = .DataReader("um_conv")
                    End While
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'Dim _pod_cost As Double = 0
        'If _pod_um_conv < _um_conv Then
        '    _pod_cost = _um_conv * _pt_cost
        'Else
        '    _pod_cost = _pt_cost / _pod_um_conv
        'End If


        If fobject.name = "FPurchaseOrder" Or fobject.name = "FPurchaseOrderFilm" Then
            fobject.gv_edit.SetRowCellValue(_row, "pod_um", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
            fobject.gv_edit.SetRowCellValue(_row, "pod_qty", _pod_qty)
            fobject.gv_edit.SetRowCellValue(_row, "pod_um_conv", _um_conv)
            'fobject.gv_edit.SetRowCellValue(_row, "pod_cost", _pod_cost)
            fobject.gv_edit.BestFitColumns()

        ElseIf fobject.name = "FRequisition" Then
            fobject.gv_edit.SetRowCellValue(_row, "reqd_um", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
            fobject.gv_edit.SetRowCellValue(_row, "reqd_um_conv", _um_conv)
            fobject.gv_edit.BestFitColumns()

        ElseIf fobject.name = "FProductStructure" Then
            fobject.gv_edit.SetRowCellValue(_row, "psd_um", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit.SetRowCellValue(_row, "um_desc", ds.Tables(0).Rows(_row_gv).Item("code_name"))
            'fobject.gv_edit.SetRowCellValue(_row, "psd_qty", _pod_qty)
            fobject.gv_edit.SetRowCellValue(_row, "psd_um_conv", _um_conv)
            fobject.gv_edit.BestFitColumns()

        ElseIf fobject.name = "FInventoryRequest" Then
            fobject.gv_edit.SetRowCellValue(_row, "pbd_um", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit.SetRowCellValue(_row, "code_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
            fobject.gv_edit.BestFitColumns()

        ElseIf fobject.name = "FSalesOrder" Then
            fobject.gv_edit.SetRowCellValue(_row, "sod_um", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit.SetRowCellValue(_row, "um_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
            fobject.gv_edit.BestFitColumns()

        ElseIf fobject.name = "FSalesOrderAlocated" Then
            fobject.gv_edit.SetRowCellValue(_row, "sod_um", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            fobject.gv_edit.SetRowCellValue(_row, "um_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
            fobject.gv_edit.BestFitColumns()

        ElseIf fobject.name = "FWorkOrderbyMO" Then
            _obj.text = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("code_name")
            _obj.tag = ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("code_id")
            fobject.wo_um_conv.editvalue = _um_conv

            'fobject.wo_um.editvalue = ds.Tables(0).Rows(_row_gv).Item("wo_um")
            'fobject.wo_um_conv.editvalue = ds.Tables(0).Rows(_row_gv).Item("wo_um")

            'fobject.gv_edit.SetRowCellValue(_row, "sod_um", ds.Tables(0).Rows(_row_gv).Item("code_id"))
            'fobject.gv_edit.SetRowCellValue(_row, "um_name", ds.Tables(0).Rows(_row_gv).Item("code_name"))
            'fobject.gv_edit.SetRowCellValue(_row, "pod_qty", _pod_qty)
            'fobject.gv_edit.SetRowCellValue(_row, "pod_um_conv", _um_conv)
            'fobject.gv_edit.BestFitColumns()
        End If
    End Sub

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        'gv_master.Focus()
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
        gv_master.Focus()

    End Sub
End Class
