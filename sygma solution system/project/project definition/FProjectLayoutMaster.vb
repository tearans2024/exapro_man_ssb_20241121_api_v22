Imports master_new.ModFunction
Imports CoreLab.PostgreSql
Imports master_new.MasterWITwo
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes

Public Class FProjectLayoutMaster
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public _dt_load As DataTable
    Public _func_lookup As New function_data
    Dim _layout_oid_mstr As String
    Dim int_groupid, int_menuid As Integer
    Dim _ds_layout As DataSet
    Dim _parent_id, _current_id As Integer

    Dim dragNode, targetNode As TreeListNode
    Dim tl As TreeList
    Dim p As Point

    Private Sub FProjectLayoutMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_tran())
        lyt_en_id.Properties.DataSource = dt_bantu
        lyt_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        lyt_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        lyt_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_layout_name())
        lyt_tran_id.Properties.DataSource = dt_bantu
        lyt_tran_id.Properties.DisplayMember = dt_bantu.Columns("tran_name").ToString
        lyt_tran_id.Properties.ValueMember = dt_bantu.Columns("tran_id").ToString
        lyt_tran_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_tran_column())
        lyt_tranc_id.Properties.DataSource = dt_bantu
        lyt_tranc_id.Properties.DisplayMember = dt_bantu.Columns("tranc_desc").ToString
        lyt_tranc_id.Properties.ValueMember = dt_bantu.Columns("tranc_id").ToString
        lyt_tranc_id.ItemIndex = 0
    End Sub

    Public Overrides Sub refresh_data()
        MyBase.refresh_data()

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_layout_parent_id(lyt_tran_id.EditValue))
        lyt_parent_id.Properties.DataSource = dt_bantu
        lyt_parent_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        lyt_parent_id.Properties.ValueMember = dt_bantu.Columns("lyt_id").ToString
        lyt_parent_id.ItemIndex = 0
    End Sub


    Private Sub lyt_en_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lyt_en_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(lyt_en_id.EditValue, "project_layout"))
        lyt_desc_id.Properties.DataSource = dt_bantu
        lyt_desc_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        lyt_desc_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        lyt_desc_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(lyt_en_id.EditValue, "data_type"))
        lyt_data_type.Properties.DataSource = dt_bantu
        lyt_data_type.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        lyt_data_type.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        lyt_data_type.ItemIndex = 0
    End Sub

    Private Sub lyt_tran_id_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lyt_tran_id.EditValueChanged
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_layout_parent_id(lyt_tran_id.EditValue))
        lyt_parent_id.Properties.DataSource = dt_bantu
        lyt_parent_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        lyt_parent_id.Properties.ValueMember = dt_bantu.Columns("lyt_id").ToString
        lyt_parent_id.ItemIndex = 0
    End Sub

    Public Overrides Sub form_first_load()
        get_data()
        load_cb()
        on_load()
        spv_master = scc_master.PanelVisibility
        xtc_master.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False
        xtp_edit.PageVisible = False
    End Sub

    Public Overrides Sub find()
        get_data()
    End Sub

    Private Sub get_data()
        _ds_layout = New DataSet
        Try
            Using objselect As New master_new.CustomCommand
                With objselect
                    .SQL = "SELECT  " _
                            & "  mstr.lyt_oid, " _
                            & "  mstr.lyt_dom_id,mstr.lyt_en_id," _
                            & "  mstr.lyt_tran_id,tran_name, " _
                            & "  mstr.lyt_id, " _
                            & "  mstr.lyt_seq, " _
                            & "  mstr.lyt_isnull, " _
                            & "  mstr.lyt_data_type, " _
                            & "  mstr.lyt_parent_id,desc_pr.code_name as desc_parent_id, " _
                            & "  mstr.lyt_is_root, " _
                            & "  mstr.lyt_tranc_id,tranc_desc, " _
                            & "  mstr.lyt_desc_id,desc_mstr.code_name as desc_id, " _
                            & "  tran_oid " _
                            & " FROM  " _
                            & "  public.lyt_mstr mstr " _
                            & "  inner join tran_mstr on tran_id = mstr.lyt_tran_id" _
                            & "  inner join code_mstr desc_mstr on desc_mstr.code_id = mstr.lyt_desc_id " _
                            & "  inner join tranc_coll on tranc_id = mstr.lyt_tranc_id " _
                            & "  left outer join lyt_mstr pr on pr.lyt_id = mstr.lyt_parent_id " _
                            & "  inner join code_mstr desc_pr on desc_pr.code_id = pr.lyt_desc_id " _
                            & "  where mstr.lyt_en_id in (select user_en_id from tconfuserentity " _
                                            & " where userid = " + master_new.ClsVar.sUserID.ToString + ")" _
                            & "  and mstr.lyt_id <> 0 " _
                            & "  order by tran_name,lyt_seq asc "
                    .InitializeCommand()
                    .FillDataSet(_ds_layout, "layout")
                    tl_layout.DataSource = _ds_layout.Tables("layout")
                    tl_layout.ExpandAll()

                End With
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Public Overrides Sub insert_data_awal()
        lyt_en_id.Focus()
        lyt_en_id.ItemIndex = 0
        lyt_tran_id.ItemIndex = 0
        lyt_seq.Value = 0
        lyt_is_root.Checked = False
        lyt_desc_id.ItemIndex = 0
        lyt_data_type.ItemIndex = 0
        lyt_isnull.Checked = True
        lyt_parent_id.ItemIndex = 0
        lyt_tranc_id.ItemIndex = 0
    End Sub

    Public Overrides Function insert() As Boolean
        Dim _lyt_id As Integer
        _lyt_id = SetNewID_OLD("lyt_mstr", "lyt_id")

        Try
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    'Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction
                    Try
                        '.Command = .Connection.CreateCommand
                        '.Command.Transaction = sqlTran

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                            & "  public.lyt_mstr " _
                                            & "( " _
                                            & "  lyt_oid, " _
                                            & "  lyt_dom_id, " _
                                            & "  lyt_en_id, " _
                                            & "  lyt_tran_id, " _
                                            & "  lyt_id, " _
                                            & "  lyt_seq, " _
                                            & "  lyt_isnull, " _
                                            & "  lyt_data_type, " _
                                            & "  lyt_parent_id, " _
                                            & "  lyt_is_root, " _
                                            & "  lyt_tranc_id, " _
                                            & "  lyt_desc_id " _
                                            & ")  " _
                                            & "VALUES ( " _
                                            & SetSetring(Guid.NewGuid().ToString()) & ",  " _
                                            & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                            & SetInteger(lyt_en_id.EditValue) & ",  " _
                                            & SetInteger(lyt_tran_id.EditValue) & ",  " _
                                            & SetInteger(_lyt_id) & ",  " _
                                            & SetInteger(lyt_seq.Value) & ",  " _
                                            & SetBitYN(lyt_isnull.EditValue) & ",  " _
                                            & SetSetring(lyt_data_type.Text) & ",  " _
                                            & SetInteger(lyt_parent_id.EditValue) & ",  " _
                                            & SetBitYN(lyt_is_root.EditValue) & ",  " _
                                            & SetInteger(lyt_tranc_id.EditValue) & ",  " _
                                            & SetInteger(lyt_desc_id.EditValue) & "  " _
                                            & ");"
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        .Command.Commit()
                        set_to_data_insert()
                        after_success()
                        set_row(Trim(_lyt_id), "lyt_id")
                        insert = True
                    Catch ex As CoreLab.PostgreSql.PgSqlException
                        row = 0
                        insert = False
                        'sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As CoreLab.PostgreSql.PgSqlException
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Overrides Function edit_data() As Boolean
        edit_data = True
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

        scc_master.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2
        sc(True)

        row = BindingContext(_ds_layout.Tables("layout")).Position
        With _ds_layout.Tables("layout").Rows(row)
            _layout_oid_mstr = .Item("lyt_oid")
            lyt_en_id.Focus()

            lyt_en_id.EditValue = .Item("lyt_en_id")
            lyt_tran_id.EditValue = .Item("lyt_tran_id")
            lyt_seq.Value = .Item("lyt_seq")
            lyt_is_root.EditValue = SetBitYNB(.Item("lyt_is_root"))
            lyt_desc_id.EditValue = .Item("lyt_desc_id")
            lyt_data_type.EditValue = .Item("lyt_data_type")
            lyt_isnull.EditValue = SetBitYNB(.Item("lyt_isnull"))
            lyt_parent_id.EditValue = .Item("lyt_parent_id")
            lyt_tranc_id.EditValue = .Item("lyt_tranc_id")
        End With

        insert_edit = False
        Return edit_data
    End Function

    Public Overrides Function edit()
        edit = True
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
                                        & "  public.lyt_mstr   " _
                                        & "SET  " _
                                        & "  lyt_dom_id = " & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                        & "  lyt_en_id = " & SetInteger(lyt_en_id.EditValue) & ",  " _
                                        & "  lyt_tran_id = " & SetInteger(lyt_tran_id.EditValue) & ",  " _
                                        & "  lyt_seq = " & SetInteger(lyt_seq.Value) & ",  " _
                                        & "  lyt_isnull = " & SetBitYN(lyt_isnull.EditValue) & ",  " _
                                        & "  lyt_data_type = " & SetSetring(lyt_data_type.Text) & ",  " _
                                        & "  lyt_parent_id = " & SetInteger(lyt_parent_id.EditValue) & ",  " _
                                        & "  lyt_is_root = " & SetBitYN(lyt_is_root.EditValue) & ",  " _
                                        & "  lyt_tranc_id = " & SetInteger(lyt_tranc_id.EditValue) & ",  " _
                                        & "  lyt_desc_id = " & SetInteger(lyt_desc_id.EditValue) & "  " _
                                        & "  " _
                                        & "WHERE  " _
                                        & "  lyt_oid = " & SetSetring(_layout_oid_mstr.ToString()) & "  " _
                                        & ";"
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        .Command.Commit()
                        after_success()
                        edit = True
                    Catch ex As PgSqlException
                        'sqlTran.Rollback()
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
        If _ds_layout.Tables.Count = 0 Then
            delete_data = False
            Exit Function
        ElseIf _ds_layout.Tables("layout").Rows.Count = 0 Then
            delete_data = False
            Exit Function
        End If

        If MessageBox.Show("Yakin Data Ini Akan Dihapus..?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Function
        End If

        If before_delete() = True Then
            row = BindingContext(_ds_layout.Tables("layout")).Position

            If row = BindingContext(_ds_layout.Tables("layout")).Count - 1 And BindingContext(_ds_layout.Tables("layout")).Count > 1 Then
                row = row - 1
            ElseIf BindingContext(_ds_layout.Tables("layout")).Count = 1 Then
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
                            .Command.CommandText = "delete from lyt_mstr where lyt_oid = " + SetSetring(_ds_layout.Tables("layout").Rows(BindingContext(_ds_layout.Tables("layout")).Position).Item("lyt_oid").ToString())
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()

                            .Command.Commit()
                            get_data()
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

    Private Sub tl_menu_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Right Then
            tl_layout.FocusedNode.Expanded = True
        ElseIf e.KeyCode = Keys.Left Then
            tl_layout.FocusedNode.Expanded = False
        End If
    End Sub

    Private Sub tl_menu_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            If _ds_layout.Tables("layout").Rows(BindingContext(_ds_layout.Tables("layout")).Position).Item("lyt_is_root") = "Y" Then
                tl_layout.FocusedNode.Expanded = True
            End If
        End If
    End Sub

    Private Sub tl_menu_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs)
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
                                        & "  public.lyt_mstr   " _
                                        & "SET  " _
                                        & "  lyt_parent_id = " & tl.CalcHitInfo(p).Node.ParentNode("lyt_id").ToString() & "  " _
                                        & "  " _
                                        & "WHERE  " _
                                        & "  lyt_id = " & tl.FocusedNode("lyt_id").ToString() & "  " _
                                        & ";"
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        .Command.Commit()
                        'find()
                        'focus_row(tl.FocusedNode("menu_id").ToString(), "menu_id")
                    Catch ex As PgSqlException
                        'sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Private Function GetDragDropEffect(ByVal tl As TreeList, ByVal dragNode As TreeListNode) As DragDropEffects
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
    'End Function

    'Private Sub tl_menu_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tl_menu.DragOver
    '    Dim dragNode As TreeListNode = TryCast(e.Data.GetData(GetType(TreeListNode)), TreeListNode)
    '    e.Effect = GetDragDropEffect(TryCast(sender, TreeList), dragNode)
    'End Sub

    'Private Sub tl_menu_CalcNodeDragImageIndex(ByVal sender As System.Object, ByVal e As DevExpress.XtraTreeList.CalcNodeDragImageIndexEventArgs) Handles tl_menu.CalcNodeDragImageIndex
    '    Dim tl As TreeList = TryCast(sender, TreeList)
    '    If GetDragDropEffect(tl, tl.FocusedNode) = DragDropEffects.None Then
    '        e.ImageIndex = -1 ' no icon
    '    Else
    '        e.ImageIndex = 1 ' the reorder icon (a curved arrow)
    '    End If
    'End Sub

    Private Sub sb_update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_update.Click
        Try
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        '.Command = .Connection.CreateCommand
                        '.Command.Transaction = sqlTran

                        _ds_layout.Tables(0).AcceptChanges()
                        Dim _seq As Integer = 0
                        For Each _dr As DataRow In _ds_layout.Tables(0).Rows
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                            & "  public.lyt_mstr   " _
                                            & "SET  " _
                                            & "  lyt_seq = " & SetInteger(_dr("lyt_seq")) & "  " _
                                            & "  " _
                                            & "WHERE  " _
                                            & "  lyt_id = " & SetInteger(_dr("lyt_id")) & "  " _
                                            & ";"
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()
                            _seq = _seq + 1
                        Next

                        .Command.Commit()
                        MessageBox.Show("Update Change successfull..!", "succ", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        get_data()
                        'focus_row(tl.FocusedNode("menu_id").ToString(), "menu_id")
                    Catch ex As PgSqlException
                        'sqlTran.Rollback()
                        MessageBox.Show(ex.Message)
                    End Try
                End With
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
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

    Private Sub lyt_is_root_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lyt_is_root.CheckedChanged
        If lyt_is_root.Checked = True Then
            lyt_data_type.ItemIndex = 0
            lyt_isnull.Checked = True
            'lyt_tranc_id.ItemIndex = 0

            lyt_data_type.Enabled = False
            lyt_isnull.Enabled = False
            'lyt_tranc_id.Enabled = False
        ElseIf lyt_is_root.Checked = False Then
            lyt_data_type.ItemIndex = 0
            lyt_isnull.Checked = False
            'lyt_tranc_id.ItemIndex = 0

            lyt_data_type.Enabled = True
            lyt_isnull.Enabled = True
            'lyt_tranc_id.Enabled = True
        End If
    End Sub

End Class
