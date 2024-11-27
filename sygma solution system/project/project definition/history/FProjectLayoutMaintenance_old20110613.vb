Imports master_new.ModFunction
Imports CoreLab.PostgreSql
Imports master_new.MasterWITwo
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes
Imports System.Text.RegularExpressions
Imports master_new
Imports DevExpress.XtraExport
Imports DevExpress.XtraGrid.Export

Public Class FProjectLayoutMaintenance
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public _dt_load As DataTable
    Public _func_lookup As New function_data
    Dim _prjl_oid_mstr As String
    Dim int_groupid, int_menuid As Integer
    Dim _ds_layout, _ds_project_detail As DataSet
    Dim _parent_id, _current_id As Integer

    Dim dragNode, targetNode As TreeListNode
    Dim tl As TreeList
    Dim p As Point

    Public _prjg_oid As String
    Dim _root, _data_type, _isnull As String
    Dim _is_edit As Boolean

    Private Sub FProjectLayoutMaintenance_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _is_edit = False
        xtp_project.PageVisible = True
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr("project_layout"))
        prjl_desc_id.Properties.DataSource = dt_bantu
        prjl_desc_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        prjl_desc_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        prjl_desc_id.ItemIndex = 0
    End Sub

    Public Overrides Sub form_first_load()
        format_grid()
        get_data()
        get_data_detail_project()
        load_cb()
        on_load()
        spv_master = scc_master.PanelVisibility
        xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True
        xtp_edit.PageVisible = False
        xtp_project.PageVisible = True
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_detail, "prjgd_oid", False)
        add_column(gv_detail, "prjgd_prjg_oid", False)
        add_column(gv_detail, "prjgd_prjc_oid", False)
        add_column_copy(gv_detail, "Project Code", "prj_code", DevExpress.Utils.HorzAlignment.Center)
        add_column_copy(gv_detail, "Part Number", "cp_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description 1", "prjc_pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Description 2", "prjc_pt_desc2", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Location", "loc_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_detail, "Site", "si_desc", DevExpress.Utils.HorzAlignment.Default)
    End Sub

    Public Overrides Sub find()
        get_data()
        get_data_detail_project()
    End Sub

    Private Sub get_data()
        _ds_layout = New DataSet
        Try
            Using objselect As New master_new.WDABasepgsql("", "")
                With objselect
                    .SQL = "SELECT  " _
                        & "  mstr.prjl_oid, " _
                        & "  mstr.prjl_dom_id, " _
                        & "  mstr.prjl_en_id, " _
                        & "  mstr.prjl_add_by, " _
                        & "  mstr.prjl_add_date, " _
                        & "  mstr.prjl_upd_by, " _
                        & "  mstr.prjl_upd_date, " _
                        & "  mstr.prjl_dt, " _
                        & "  mstr.prjl_seq, " _
                        & "  mstr.prjl_prjg_oid,prjg_code,prjg_name, " _
                        & "  mstr.prjl_id, " _
                        & "  mstr.prjl_isnull, " _
                        & "  mstr.prjl_data_type, " _
                        & "  mstr.prjl_parent_id,desc_pr.code_name as desc_parent_id, " _
                        & "  coalesce(mstr.prjl_user_value,'') as prjl_user_value, " _
                        & "  mstr.prjl_is_root, " _
                        & "  mstr.prjl_tranc_id,tranc_desc, " _
                        & "  mstr.prjl_desc_id,desc_mstr.code_name as desc_id, " _
                        & "  mstr.prjl_tran_id,tran_name,trancusr_usr_id,'' as is_allow " _
                        & " FROM  " _
                        & " public.prjl_layout mstr " _
                        & " inner join prjg_group on prjg_oid = mstr.prjl_prjg_oid " _
                        & " inner join tran_mstr on tran_id = mstr.prjl_tran_id  " _
                        & " inner join code_mstr desc_mstr on desc_mstr.code_id = mstr.prjl_desc_id   " _
                        & " inner join tranc_coll on tranc_id = mstr.prjl_tranc_id   " _
                        & " inner join lyt_mstr pr on pr.lyt_id = mstr.prjl_parent_id   " _
                        & " inner join code_mstr desc_pr on desc_pr.code_id = pr.lyt_desc_id  " _
                        & " left outer join trancusr_user on trancusr_tranc_id = tranc_id  " _
                        & " where prjl_prjg_oid = " + SetSetring(_prjg_oid.ToString()) _
                        & " and mstr.prjl_id <> 0 " _
                        & " order by prjl_seq asc "
                    '& " and tranu_user_id = " + SetInteger(master_new.ClsVar.sUserID) _
                    .InitializeCommand()
                    .FillDataSet(_ds_layout, "layout")
                    tl_layout.DataSource = _ds_layout.Tables("layout")
                    tl_layout.ExpandAll()
                End With
            End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub get_data_detail_project()
        _ds_project_detail = New DataSet
        Try
            Using objselect As New master_new.WDABasepgsql("", "")
                With objselect
                    .SQL = "SELECT  " _
                        & "  prjgd_oid, " _
                        & "  prjgd_dom_id, " _
                        & "  prjgd_en_id, " _
                        & "  prjgd_add_by, " _
                        & "  prjgd_add_date, " _
                        & "  prjgd_upd_by, " _
                        & "  prjgd_upd_date, " _
                        & "  prjgd_dt, " _
                        & "  prjgd_prjg_oid, " _
                        & "  prjgd_prjc_oid, " _
                        & "  cp_code,prjc_pt_desc1,prjc_pt_desc2, " _
                        & "  loc_desc,si_desc,prj_code " _
                        & "FROM  " _
                        & "  public.prjgd_det " _
                        & "  inner join prjg_group on prjg_oid = prjgd_prjg_oid  " _
                        & "  inner join prjc_cust on prjc_oid = prjgd_prjc_oid " _
                        & "  inner join cp_mstr on cp_id = prjc_cp_id " _
                        & "  inner join loc_mstr on loc_id = prjc_loc_id " _
                        & "  inner join si_mstr on si_id = prjc_si_id " _
                        & "  inner join prj_mstr on prj_oid = prjc_prj_oid " _
                        & " where prjgd_prjg_oid = " + SetSetring(_prjg_oid.ToString()) _
                        & " order by cp_code asc "
                    .InitializeCommand()
                    .FillDataSet(_ds_project_detail, "project_detail")
                    gc_detail.DataSource = _ds_project_detail.Tables("project_detail")
                    gv_detail.BestFitColumns()
                End With
            End Using
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Function insert_data() As Boolean
        MessageBox.Show("This form unable to insert data..!", "err", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Return False
        Return insert_data
    End Function

    Private Function CekParentID(ByVal par_prjg_oid As String, ByVal par_current_parent_id As Integer, ByVal par_root_seq As Integer) As Boolean
        CekParentID = True

        Dim _ds_parent As New DataSet
        Try
            Using objselect As New master_new.WDABasepgsql("", "")
                With objselect
                    .SQL = "SELECT  " _
                        & "  mstr.prjl_oid, " _
                        & "  mstr.prjl_dom_id, " _
                        & "  mstr.prjl_en_id, " _
                        & "  mstr.prjl_add_by, " _
                        & "  mstr.prjl_add_date, " _
                        & "  mstr.prjl_upd_by, " _
                        & "  mstr.prjl_upd_date, " _
                        & "  mstr.prjl_dt, " _
                        & "  mstr.prjl_seq, " _
                        & "  mstr.prjl_prjg_oid, " _
                        & "  mstr.prjl_id, " _
                        & "  mstr.prjl_isnull, " _
                        & "  mstr.prjl_data_type, " _
                        & "  mstr.prjl_parent_id, " _
                        & "  coalesce(mstr.prjl_user_value,'') as prjl_user_value, " _
                        & "  mstr.prjl_is_root, " _
                        & "  mstr.prjl_tranc_id, " _
                        & "  mstr.prjl_desc_id, " _
                        & "  mstr.prjl_tran_id, " _
                        & "  pr_seq.prjl_seq " _
                        & " FROM  " _
                        & " public.prjl_layout mstr " _
                        & " inner join prjg_group on prjg_oid = mstr.prjl_prjg_oid " _
                        & " inner join prjl_layout pr_seq on pr_seq.prjl_id = mstr.prjl_parent_id  " _
                        & " where mstr.prjl_prjg_oid = " + SetSetring(par_prjg_oid.ToString()) _
                        & " and mstr.prjl_id <> 0 " _
                        & " and mstr.prjl_parent_id <> " & SetInteger(par_current_parent_id) _
                        & " and mstr.prjl_isnull <> 'Y' " _
                        & " and pr_seq.prjl_seq < " & SetInteger(par_root_seq) _
                        & " order by mstr.prjl_seq asc "
                    .InitializeCommand()
                    .FillDataSet(_ds_parent, "parent")
                End With
            End Using
        Catch ex As Exception
        End Try

        For Each _dr As DataRow In _ds_parent.Tables(0).Rows
            If _dr("prjl_user_value") = "" AndAlso _dr("prjl_is_root") = "N" Then
                Return False
            End If
        Next

        Return CekParentID
    End Function

    Private Function GetSeqCurrentRoot(ByVal par_current_parent_id As Integer) As Integer
        Dim _ds_seq_root As New DataSet
        Try
            Using objselect As New master_new.WDABasepgsql("", "")
                With objselect
                    .SQL = "SELECT  " _
                        & "  prjl_seq " _
                        & " FROM  " _
                        & " public.prjl_layout " _
                        & " where prjl_id = " + SetInteger(par_current_parent_id)
                    .InitializeCommand()
                    .FillDataSet(_ds_seq_root, "seq_root")
                End With
            End Using
        Catch ex As Exception
        End Try

        For Each _dr As DataRow In _ds_seq_root.Tables(0).Rows
            GetSeqCurrentRoot = _dr("prjl_seq")
        Next

        Return GetSeqCurrentRoot
    End Function

    Private Function CekSeqAtas(ByVal par_current_parent_id As Integer, ByVal par_current_seq As Integer) As Boolean
        CekSeqAtas = True
        For Each _dr As DataRow In _ds_layout.Tables(0).Rows
            If _dr("prjl_parent_id") = par_current_parent_id Then
                If _dr("prjl_seq") < par_current_seq AndAlso _dr("prjl_isnull") = "N" AndAlso SetString(_dr("prjl_user_value")) = "" Then
                    Return False
                End If
            End If
        Next

        Return CekSeqAtas
    End Function

    Public Overrides Function edit_data() As Boolean
        edit_data = True
        If _ds_layout.Tables("layout").Rows(BindingContext(_ds_layout.Tables("layout")).Position).Item("prjl_is_root") = "Y" Then
            'MessageBox.Show("This line not allowed to edit data..!", "err", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        If _ds_layout.Tables("layout").Rows(BindingContext(_ds_layout.Tables("layout")).Position).Item("trancusr_usr_id") <> master_new.ClsVar.sUserID Then
            MessageBox.Show("User anda tidak di izinkan untuk mengedit transaksi line ini..!", "err", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        'cek seq di atasnya udah di isi apa belum ===========================================================================
        If CekSeqAtas(_ds_layout.Tables("layout").Rows(BindingContext(_ds_layout.Tables("layout")).Position).Item("prjl_parent_id"), _
                      _ds_layout.Tables("layout").Rows(BindingContext(_ds_layout.Tables("layout")).Position).Item("prjl_seq")) = False Then
            MessageBox.Show("Terdapat data pada baris sebelumnya yg belum di isi..! Silahkan isi terlebih dahulu..!", "err", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim _current_seq_root As Integer
        _current_seq_root = GetSeqCurrentRoot(_ds_layout.Tables("layout").Rows(BindingContext(_ds_layout.Tables("layout")).Position).Item("prjl_parent_id"))

        If CekParentID(_ds_layout.Tables("layout").Rows(BindingContext(_ds_layout.Tables("layout")).Position).Item("prjl_prjg_oid"), _
                      _ds_layout.Tables("layout").Rows(BindingContext(_ds_layout.Tables("layout")).Position).Item("prjl_parent_id"), _
                       _current_seq_root) = False Then
            MessageBox.Show("Terdapat data pada baris sebelumnya yg belum di isi..! Silahkan isi terlebih dahulu..!", "err", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        '===================================================================================================================

        If _ds_layout.Tables.Count = 0 Then
            edit_data = False
            Exit Function
        ElseIf _ds_layout.Tables(0).Rows.Count = 0 Then
            edit_data = False
            Exit Function
        End If

        xtc_master.SelectedTabPageIndex = 1
        xtp_edit.PageVisible = True
        xtp_data.PageVisible = False
        xtp_project.PageVisible = False

        scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        sc(True)

        row = BindingContext(_ds_layout.Tables("layout")).Position
        With _ds_layout.Tables("layout").Rows(row)
            _prjl_oid_mstr = .Item("prjl_oid")
            prjl_user_value.Focus()
            prjl_desc_id.EditValue = .Item("prjl_desc_id")
            prjl_user_value.Text = SetString(.Item("prjl_user_value"))

            _data_type = .Item("prjl_data_type")
            _isnull = .Item("prjl_isnull")
            prjl_desc_id.Enabled = False
            _is_edit = True
        End With

        If _data_type = "Numeric" Then
            prjl_user_value.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            prjl_user_value.Properties.DisplayFormat.FormatString = "n"

            prjl_user_value.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            prjl_user_value.Properties.EditFormat.FormatString = "n"

            prjl_user_value.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
            prjl_user_value.Properties.Mask.EditMask = "n"
        ElseIf _data_type = "Character" Then
            prjl_user_value.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None
            prjl_user_value.Properties.DisplayFormat.FormatString = ""

            prjl_user_value.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.None
            prjl_user_value.Properties.EditFormat.FormatString = ""

            prjl_user_value.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
            prjl_user_value.Properties.Mask.EditMask = ""
        ElseIf _data_type = "Date" Then
            prjl_user_value.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            prjl_user_value.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"

            prjl_user_value.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
            prjl_user_value.Properties.EditFormat.FormatString = "dd/MM/yyyy"

            prjl_user_value.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime
            prjl_user_value.Properties.Mask.EditMask = "dd/MM/yyyy"
        End If

        insert_edit = False
        Return edit_data
    End Function

    Public Overrides Function before_save() As Boolean
        before_save = True

        Dim _date As Date = func_coll.get_tanggal_sistem
        'Dim i As Integer

        If _isnull = "N" Then
            If Trim(prjl_user_value.Text) = "" Then
                MessageBox.Show("Value in this line can't null..!", "err", MessageBoxButtons.OK, MessageBoxIcon.Error)
                prjl_user_value.Focus()
                Return False
            End If
        End If

        If _is_edit = True Then
            If _data_type = "Date" Then
                If IsDate(prjl_user_value.Text) = False Then
                    MessageBox.Show("Please enter Date only", "conf", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    prjl_user_value.Focus()
                    Return False
                End If
            End If
        End If

        Return before_save
    End Function

    Public Overrides Function cancel_data() As Boolean
        Dim my As MasterMDI = CType(Me.ParentForm, MasterMDI)
        my.configurasi_menu("awal_transaksi")

        panel_visibility()
        xtc_master.SelectedTabPageIndex = 0
        xtp_edit.PageVisible = False
        xtp_data.PageVisible = True
        xtp_project.PageEnabled = True

        For Each ctrl In Me.Controls
            If TypeOf ctrl Is DevExpress.XtraBars.Docking.DockPanel Then
                dp = CType(ctrl, DevExpress.XtraBars.Docking.DockPanel)
                'dp.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible
                dp.Show()
            End If
        Next
        sc(False)
        kosong()
        _is_edit = False
    End Function

    Public Overrides Function edit()
        edit = True
        Try
            Using objinsert As New master_new.WDABasepgsql("", "")
                With objinsert
                    .Connection.Open()
                    Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        .Command = .Connection.CreateCommand
                        .Command.Transaction = sqlTran

                        .Command.CommandType = CommandType.Text
                        .Command.CommandText = "UPDATE  " _
                                        & "  public.prjl_layout   " _
                                        & "SET   " _
                                        & "  prjl_user_value = " & SetSetring(prjl_user_value.Text) & ",  " _
                                        & "  prjl_upd_by = " & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                        & "  prjl_upd_date = current_timestamp, " _
                                        & "  prjl_dt = current_timestamp " _
                                        & "WHERE  " _
                                        & "  prjl_oid = " & SetSetring(_prjl_oid_mstr.ToString()) & "  " _
                                        & ";"
                        .Command.ExecuteNonQuery()
                        .Command.Parameters.Clear()

                        sqlTran.Commit()
                        after_success()
                        edit = True
                        xtp_project.PageVisible = True
                        xtc_master.SelectedTabPageIndex = 0
                        xtp_data.PageVisible = True
                        xtp_project.PageEnabled = True
                    Catch ex As PgSqlException
                        sqlTran.Rollback()
                        _is_edit = False
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

    Public Overrides Function delete_data() As Boolean
        delete_data = True

        MessageBox.Show("This form unable to delete data..!", "err", MessageBoxButtons.OK, MessageBoxIcon.Error)
        delete_data = False

        Return delete_data
    End Function

    Public Overrides Sub after_success()
        xtc_master.SelectedTabPageIndex = 0
        get_data()
        sc(False)
        kosong()
        If insert_edit = True Then
            MessageBox.Show("Save Data Successfull..! ", "Save Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ElseIf insert_edit = False Then
            MessageBox.Show("Update Data Successfull..! ", "Update Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Public Overrides Sub set_row(ByVal par_code As String, ByVal par_column As String)
        Dim i As Integer
        For i = 0 To _ds_layout.Tables("layout").Rows.Count - 1
            If par_code = _ds_layout.Tables("layout").Rows(i).Item(par_column) Then
                BindingContext(_ds_layout.Tables("layout")).Position = i
                Exit Sub
            End If
        Next
    End Sub

    Private Sub tl_menu_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tl_layout.KeyDown
        If e.KeyCode = Keys.Right Then
            tl_layout.FocusedNode.Expanded = True
        ElseIf e.KeyCode = Keys.Left Then
            tl_layout.FocusedNode.Expanded = False
        End If
    End Sub

    Private Sub tl_menu_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles tl_layout.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            If _ds_layout.Tables("layout").Rows(BindingContext(_ds_layout.Tables("layout")).Position).Item("prjl_is_root") = "Y" Then
                tl_layout.FocusedNode.Expanded = True
            End If
        End If
    End Sub

    Private Sub tl_menu_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tl_layout.DragDrop
        Dim dragNode, targetNode As TreeListNode
        Dim tl As TreeList = TryCast(sender, TreeList)
        Dim p As Point = tl.PointToClient(New Point(e.X, e.Y))

        dragNode = TryCast(e.Data.GetData(GetType(TreeListNode)), TreeListNode)
        targetNode = tl.CalcHitInfo(p).Node

        tl.SetNodeIndex(dragNode, tl.GetNodeIndex(targetNode))

        'If dragNode.ParentNode.Id = targetNode.ParentNode.Id Then
        '    e.Effect = DragDropEffects.Move
        'Else
        '    e.Effect = DragDropEffects.None
        'End If

        'If dragNode IsNot Nothing AndAlso targetNode IsNot Nothing Then
        '    e.Effect = DragDropEffects.Move
        'Else
        '    e.Effect = DragDropEffects.None
        'End If

        'e.Effect = DragDropEffects.Move


        'Try
        '    Using objinsert As New master_new.WDABasepgsql("", "")
        '        With objinsert
        '            .Connection.Open()
        '            Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
        '            Try
        '                .Command = .Connection.CreateCommand
        '                .Command.Transaction = sqlTran

        '                .Command.CommandType = CommandType.Text
        '                .Command.CommandText = "UPDATE  " _
        '                                & "  public.lyt_mstr   " _
        '                                & "SET  " _
        '                                & "  lyt_parent_id = " & tl.CalcHitInfo(p).Node.ParentNode("lyt_id").ToString() & "  " _
        '                                & "  " _
        '                                & "WHERE  " _
        '                                & "  lyt_id = " & tl.FocusedNode("lyt_id").ToString() & "  " _
        '                                & ";"
        '                .Command.ExecuteNonQuery()
        '                .Command.Parameters.Clear()

        '                sqlTran.Commit()
        '                'find()
        '                'focus_row(tl.FocusedNode("menu_id").ToString(), "menu_id")
        '            Catch ex As PgSqlException
        '                sqlTran.Rollback()
        '                MessageBox.Show(ex.Message)
        '            End Try
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub

    Private Function GetDragDropEffect(ByVal tl As TreeList, ByVal dragNode As TreeListNode) As DragDropEffects
        '    Dim targetNode As TreeListNode
        '    Dim p As Point = tl.PointToClient(MousePosition)
        '    targetNode = tl.CalcHitInfo(p).Node

        '    'If dragNode IsNot Nothing AndAlso targetNode IsNot Nothing AndAlso dragNode IsNot targetNode AndAlso dragNode.ParentNode Is targetNode.ParentNode Then
        '    '    Return DragDropEffects.Move
        '    'Else
        '    '    Return DragDropEffects.None
        '    'End If

        '    If dragNode IsNot Nothing AndAlso targetNode IsNot Nothing AndAlso dragNode IsNot targetNode Then
        '        Return DragDropEffects.Move
        '    Else
        '        Return DragDropEffects.None
        '    End If

        '    'If targetNode IsNot Nothing Then
        '    '    Return DragDropEffects.Move
        '    'Else
        '    '    Return DragDropEffects.None
        '    'End If
        '    'Return DragDropEffects.Move
    End Function

    'Private Sub tl_menu_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tl_menu.DragOver
    '    '    Dim dragNode As TreeListNode = TryCast(e.Data.GetData(GetType(TreeListNode)), TreeListNode)
    '    '    e.Effect = GetDragDropEffect(TryCast(sender, TreeList), dragNode)
    'End Sub

    Private Sub sb_update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Try
        '    Using objinsert As New master_new.WDABasepgsql("", "")
        '        With objinsert
        '            .Connection.Open()
        '            Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
        '            Try
        '                .Command = .Connection.CreateCommand
        '                .Command.Transaction = sqlTran

        '                _ds_layout.Tables(0).AcceptChanges()
        '                Dim _seq As Integer = 0
        '                For Each _dr As DataRow In _ds_layout.Tables(0).Rows
        '                    .Command.CommandType = CommandType.Text
        '                    .Command.CommandText = "UPDATE  " _
        '                                    & "  public.lyt_mstr   " _
        '                                    & "SET  " _
        '                                    & "  lyt_seq = " & SetInteger(_dr("lyt_seq")) & "  " _
        '                                    & "  " _
        '                                    & "WHERE  " _
        '                                    & "  lyt_id = " & SetInteger(_dr("lyt_id")) & "  " _
        '                                    & ";"
        '                    .Command.ExecuteNonQuery()
        '                    .Command.Parameters.Clear()
        '                    _seq = _seq + 1
        '                Next

        '                sqlTran.Commit()
        '                MessageBox.Show("Update Change successfull..!", "succ", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '                get_data()
        '                'focus_row(tl.FocusedNode("menu_id").ToString(), "menu_id")
        '            Catch ex As PgSqlException
        '                sqlTran.Rollback()
        '                MessageBox.Show(ex.Message)
        '            End Try
        '        End With
        '    End Using
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub

    Public Sub focus_row(ByVal par_code As Integer, ByVal par_column As String)
        Dim i As Integer
        For i = 0 To _ds_layout.Tables("layout").Rows.Count - 1
            If par_code = _ds_layout.Tables("layout").Rows(i).Item(par_column) Then
                BindingContext(_ds_layout.Tables("layout")).Position = i
                Exit Sub
            End If
        Next
    End Sub

    Private Sub be_prjg_code_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_prjg_code.ButtonClick
        Dim frm As New FProjectGroupSearch()
        frm.set_win(Me)
        frm.type_form = True
        frm.ShowDialog()
    End Sub

    'Private _allowedCharacters As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ/:=+-%!\|@#$^&*()_?><"

    'Private Sub prjl_user_value_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles prjl_user_value.Enter
    '    If _data_type = "Numeric" Then
    '        prjl_user_value.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
    '        prjl_user_value.Properties.DisplayFormat.FormatString = "n"

    '        prjl_user_value.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric
    '        prjl_user_value.Properties.EditFormat.FormatString = "n"

    '        prjl_user_value.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
    '        prjl_user_value.Properties.Mask.EditMask = "n"
    '    ElseIf _data_type = "Character" Then
    '        prjl_user_value.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None
    '        prjl_user_value.Properties.DisplayFormat.FormatString = ""

    '        prjl_user_value.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.None
    '        prjl_user_value.Properties.EditFormat.FormatString = ""

    '        prjl_user_value.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None
    '        prjl_user_value.Properties.Mask.EditMask = ""
    '    ElseIf _data_type = "Date" Then
    '        prjl_user_value.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
    '        prjl_user_value.Properties.DisplayFormat.FormatString = "dd/MM/yyyy"

    '        prjl_user_value.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime
    '        prjl_user_value.Properties.EditFormat.FormatString = "dd/MM/yyyy"

    '        prjl_user_value.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime
    '        prjl_user_value.Properties.Mask.EditMask = "dd/MM/yyyy"
    '    End If
    'End Sub

    'Private Sub prjl_user_value_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles prjl_user_value.GotFocus

    'End Sub

    'Private Sub prjl_user_value_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles prjl_user_value.KeyPress
    '    If _data_type = "Numeric" Then
    '        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
    '            MessageBox.Show("Please enter numbers only", "conf", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '            e.Handled = True
    '        End If
    '    ElseIf _data_type = "Character" Then
    '        If Not _allowedCharacters.Contains(e.KeyChar) AndAlso e.KeyChar <> ChrW(Keys.Back) AndAlso e.KeyChar <> ChrW(Keys.Space) AndAlso e.KeyChar <> ChrW(Keys.Enter) Then
    '            MessageBox.Show("Please enter character only", "conf", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '            e.Handled = True
    '        End If
    '    End If
    'End Sub

    Private Sub prjl_user_value_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles prjl_user_value.Validated
        If _is_edit = True Then
            If _data_type = "Date" Then
                If IsDate(prjl_user_value.Text) = False Then
                    MessageBox.Show("Please enter Date only", "conf", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    prjl_user_value.Text = ""
                    prjl_user_value.Focus()
                End If
            End If
        End If
    End Sub

    Public Overrides Function export_data() As Boolean
        Dim fileName As String = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls")
        If fileName <> "" Then
            'ExportTo(get_gv(), New ExportXlsProvider(fileName))
            tl_layout.ExportToXls(fileName)
            OpenFile(fileName)
        End If
    End Function

    
End Class
