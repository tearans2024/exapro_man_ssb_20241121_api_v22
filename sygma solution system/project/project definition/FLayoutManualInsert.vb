Imports master_new.ModFunction
Imports CoreLab.PostgreSql
Imports master_new.MasterWITwo
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes
Imports System.Text.RegularExpressions
Imports master_new

Public Class FLayoutManualInsert
    Dim dt_bantu As DataTable
    Dim func_data As New function_data
    Dim func_coll As New function_collection
    Public _dt_load As DataTable
    Public _func_lookup As New function_data
    Dim _prjl_oid_mstr As String
    Dim int_groupid, int_menuid As Integer
    Dim _ds_layout As DataSet
    Dim _parent_id, _current_id As Integer

    Dim dragNode, targetNode As TreeListNode
    Dim tl As TreeList
    Dim p As Point

    Public _prjg_oid As String
    Dim _root, _data_type, _isnull As String
    Dim _is_edit As Boolean
    Public _en_id, _tran_id As Integer

    Private Sub FProjectLayoutMaintenance_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _is_edit = False
    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_tran_column())
        prjl_tranc_id.Properties.DataSource = dt_bantu
        prjl_tranc_id.Properties.DisplayMember = dt_bantu.Columns("tranc_desc").ToString
        prjl_tranc_id.Properties.ValueMember = dt_bantu.Columns("tranc_id").ToString
        prjl_tranc_id.ItemIndex = 0

        ''Custom data
        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_code_mstr(_en_id, "project_layout"))
        'lyt_desc_id.Properties.DataSource = dt_bantu
        'lyt_desc_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        'lyt_desc_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        'lyt_desc_id.ItemIndex = 0

        'dt_bantu = New DataTable
        'dt_bantu = (func_data.load_code_mstr(_en_id, "data_type"))
        'lyt_data_type.Properties.DataSource = dt_bantu
        'lyt_data_type.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        'lyt_data_type.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        'lyt_data_type.ItemIndex = 0
    End Sub

    Public Sub load_custom()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_layout_parent_id(_prjg_oid.ToString()))
        prjl_parent_id.Properties.DataSource = dt_bantu
        prjl_parent_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        prjl_parent_id.Properties.ValueMember = dt_bantu.Columns("lyt_id").ToString
        prjl_parent_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(_en_id, "project_layout"))
        prjl_desc_id.Properties.DataSource = dt_bantu
        prjl_desc_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        prjl_desc_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        prjl_desc_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_code_mstr(_en_id, "data_type"))
        prjl_data_type1.Properties.DataSource = dt_bantu
        prjl_data_type1.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        prjl_data_type1.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        prjl_data_type1.ItemIndex = 0
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
                        & "  mstr.prjl_tran_id,tran_name,coalesce(trancusr_usr_id,0) as trancusr_usr_id " _
                        & " FROM  " _
                        & " public.prjl_layout mstr " _
                        & " inner join prjg_group on prjg_oid = mstr.prjl_prjg_oid " _
                        & " inner join tran_mstr on tran_id = mstr.prjl_tran_id  " _
                        & " inner join tranu_usr on tranu_tran_id = tran_id   " _
                        & " inner join code_mstr desc_mstr on desc_mstr.code_id = mstr.prjl_desc_id   " _
                        & " inner join tranc_coll on tranc_id = mstr.prjl_tranc_id   " _
                        & " inner join lyt_mstr pr on pr.lyt_id = mstr.prjl_parent_id   " _
                        & " inner join code_mstr desc_pr on desc_pr.code_id = pr.lyt_desc_id  " _
                        & " left outer join trancusr_user on trancusr_tranc_id = tranc_id  " _
                        & " where prjl_prjg_oid = " + SetSetring(_prjg_oid.ToString()) _
                        & " and mstr.prjl_id <> 0 " _
                        & " order by prjl_seq asc "
                    .InitializeCommand()
                    .FillDataSet(_ds_layout, "layout")
                    tl_layout.DataSource = _ds_layout.Tables("layout")
                    tl_layout.ExpandAll()
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

    Public Overrides Function delete_data() As Boolean
        delete_data = True

        MessageBox.Show("This form unable to dlete data..!", "err", MessageBoxButtons.OK, MessageBoxIcon.Error)
        delete_data = False

        Return delete_data
    End Function

    Private Sub be_prjg_code_ButtonClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles be_prjg_code.ButtonClick
        Dim frm As New FProjectGroupSearch()
        frm.set_win(Me)
        frm.type_form = True
        frm.ShowDialog()
    End Sub


    Private Function CekValueNextSeq() As Boolean
        CekValueNextSeq = True

        Dim _ds_value As New DataSet
        Try
            Using objselect As New master_new.CustomCommand
                With objselect
                    .SQL = "SELECT  " _
                        & "  prjl_oid, " _
                        & "  prjl_id, " _
                        & "  prjl_seq, " _
                        & "  coalesce(prjl_user_value,'') as prjl_user_value  " _
                        & "FROM  " _
                        & "  public.prjl_layout " _
                        & "  where prjl_prjg_oid = " & SetSetring(_prjg_oid.ToString()) & "  " _
                        & "  and prjl_parent_id = " & SetSetring(prjl_parent_id.EditValue) & "  " _
                        & "  and prjl_seq >= " & SetInteger(prjl_seq.Value) & " " _
                        & "  order by prjl_seq asc "
                    .InitializeCommand()
                    .FillDataSet(_ds_value, "value")
                End With
            End Using
        Catch ex As Exception
        End Try

        For Each _dr As DataRow In _ds_value.Tables(0).Rows
            If Trim(_dr("prjl_user_value")) <> "" Then
                Return False
            End If
        Next

        Return CekValueNextSeq
    End Function

    Private Sub sb_add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_add.Click
        If CekValueNextSeq() = False Then
            MessageBox.Show("Data sudah di isi pada seq. dibawahnya..! Insert data tidak dapat dilakukan..!", "err", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        _ds_layout.Tables(0).AcceptChanges()

        Try
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        '.Command = .Connection.CreateCommand
                        '.Command.Transaction = sqlTran

                        'select dulu data yg seq lebih besar dari pada seq yg di inputkan
                        Dim _ds_upd_seq As New DataSet
                        Try
                            Using objselect As New master_new.CustomCommand
                                With objselect
                                    .SQL = "SELECT  " _
                                        & "  prjl_oid, " _
                                        & "  prjl_prjg_oid, " _
                                        & "  prjl_id, " _
                                        & "  prjl_isnull, " _
                                        & "  prjl_data_type, " _
                                        & "  prjl_parent_id, " _
                                        & "  prjl_user_value, " _
                                        & "  prjl_is_root, " _
                                        & "  prjl_tranc_id, " _
                                        & "  prjl_desc_id, " _
                                        & "  prjl_tran_id, " _
                                        & "  prjl_seq " _
                                        & "FROM  " _
                                        & "  public.prjl_layout " _
                                        & "  where prjl_prjg_oid = " & SetSetring(_prjg_oid.ToString()) & "  " _
                                        & "  and prjl_parent_id = " & SetSetring(prjl_parent_id.EditValue) & "  " _
                                        & "  and prjl_seq >= " & SetInteger(prjl_seq.Value) & " " _
                                        & "  order by prjl_seq asc "
                                    .InitializeCommand()
                                    .FillDataSet(_ds_upd_seq, "update_seq")
                                End With
                            End Using
                        Catch ex As Exception
                        End Try

                        For Each _dr As DataRow In _ds_upd_seq.Tables(0).Rows
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                                & "  public.prjl_layout   " _
                                                & "SET  " _
                                                & "  prjl_seq = prjl_seq + 1 " _
                                                & "WHERE  " _
                                                & "  prjl_oid = " & SetSetring(_dr("prjl_oid").ToString()) & "  " _
                                                & ";"
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()
                        Next
                        '==================================================================================

                        Dim _prjl_id As Integer
                        _prjl_id = SetNewID_OLD("prjl_layout", "prjl_id")
                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "INSERT INTO  " _
                                                & "  public.prjl_layout " _
                                                & "( " _
                                                & "  prjl_oid, " _
                                                & "  prjl_dom_id, " _
                                                & "  prjl_en_id, " _
                                                & "  prjl_add_by, " _
                                                & "  prjl_add_date, " _
                                                & "  prjl_dt, " _
                                                & "  prjl_seq, " _
                                                & "  prjl_prjg_oid, " _
                                                & "  prjl_id, " _
                                                & "  prjl_isnull, " _
                                                & "  prjl_data_type, " _
                                                & "  prjl_parent_id, " _
                                                & "  prjl_user_value, " _
                                                & "  prjl_is_root, " _
                                                & "  prjl_tranc_id, " _
                                                & "  prjl_desc_id, " _
                                                & "  prjl_tran_id " _
                                                & ")  " _
                                                & "VALUES ( " _
                                                & SetSetring(Guid.NewGuid().ToString()) & ",  " _
                                                & SetInteger(master_new.ClsVar.sdom_id) & ",  " _
                                                & SetInteger(_en_id) & ",  " _
                                                & SetSetring(master_new.ClsVar.sNama) & ",  " _
                                                & "current_timestamp" & ",  " _
                                                & "current_timestamp" & ",  " _
                                                & SetInteger(prjl_seq.Value) & ",  " _
                                                & SetSetring(_prjg_oid.ToString()) & ",  " _
                                                & SetInteger(_prjl_id) & ",  " _
                                                & SetBitYN(prjl_isnull1.EditValue) & ",  " _
                                                & SetSetring(prjl_data_type1.Text) & ",  " _
                                                & SetInteger(prjl_parent_id.EditValue) & ",  " _
                                                & SetSetring("") & ",  " _
                                                & SetBitYN(prjl_is_root1.EditValue) & ",  " _
                                                & SetInteger(prjl_tranc_id.EditValue) & ",  " _
                                                & SetInteger(prjl_desc_id.EditValue) & ",  " _
                                                & SetInteger(_tran_id) & "  " _
                                                & ");"
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        .Command.Commit()
                        MessageBox.Show("Insert/Update successfull..!", "succ", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        get_data()
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

    Private Sub sb_delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles sb_delete.Click
        If _ds_layout.Tables("layout").Rows(BindingContext(_ds_layout.Tables("layout")).Position).Item("trancusr_usr_id") <> master_new.ClsVar.sUserID Then
            MessageBox.Show("User anda tidak di izinkan untuk mengapus line ini..!", "err", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Trim(_ds_layout.Tables("layout").Rows(BindingContext(_ds_layout.Tables("layout")).Position).Item("prjl_user_value")) <> "" Then
            MessageBox.Show("Data sudah di isi delete data tidak dapat dilakukan..!", "err", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If MessageBox.Show("Are you sure to delete this data..?", "conf", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        _ds_layout.Tables(0).AcceptChanges()
        Try
            Using objinsert As New master_new.CustomCommand
                With objinsert
.Command.Open()
                    ''Dim sqlTran As PgSqlTransaction = .Connection.BeginTransaction()
                    Try
                        '.Command = .Connection.CreateCommand
                        '.Command.Transaction = sqlTran

                        'select dulu data yg seq lebih besar dari pada seq yg di inputkan
                        Dim _ds_upd_seq As New DataSet
                        Try
                            Using objselect As New master_new.CustomCommand
                                With objselect
                                    .SQL = "SELECT  " _
                                        & "  prjl_oid, " _
                                        & "  prjl_prjg_oid, " _
                                        & "  prjl_id, " _
                                        & "  prjl_isnull, " _
                                        & "  prjl_data_type, " _
                                        & "  prjl_parent_id, " _
                                        & "  prjl_user_value, " _
                                        & "  prjl_is_root, " _
                                        & "  prjl_tranc_id, " _
                                        & "  prjl_desc_id, " _
                                        & "  prjl_tran_id, " _
                                        & "  prjl_seq " _
                                        & "FROM  " _
                                        & "  public.prjl_layout " _
                                        & "  where prjl_prjg_oid = " & SetSetring(_prjg_oid.ToString()) & "  " _
                                        & "  and prjl_parent_id = " & SetSetring(_ds_layout.Tables(0).Rows(BindingContext(_ds_layout.Tables(0)).Position).Item("prjl_parent_id")) & "  " _
                                        & "  and prjl_seq > " & SetInteger(_ds_layout.Tables(0).Rows(BindingContext(_ds_layout.Tables(0)).Position).Item("prjl_seq")) & " " _
                                        & "  order by prjl_seq asc "
                                    .InitializeCommand()
                                    .FillDataSet(_ds_upd_seq, "update_seq")
                                End With
                            End Using
                        Catch ex As Exception
                        End Try

                        For Each _dr As DataRow In _ds_upd_seq.Tables(0).Rows
                            '.Command.CommandType = CommandType.Text
                            .Command.CommandText = "UPDATE  " _
                                                & "  public.prjl_layout   " _
                                                & "SET  " _
                                                & "  prjl_seq = prjl_seq - 1 " _
                                                & "WHERE  " _
                                                & "  prjl_oid = " & SetSetring(_dr("prjl_oid").ToString()) & "  " _
                                                & ";"
                            .Command.ExecuteNonQuery()
                            '.Command.Parameters.Clear()
                        Next

                        '.Command.CommandType = CommandType.Text
                        .Command.CommandText = "DELETE FROM  " _
                                            & " public.prjl_layout  " _
                                            & " WHERE  " _
                                            & " prjl_oid = " & SetSetring(_ds_layout.Tables(0).Rows(BindingContext(_ds_layout.Tables(0)).Position).Item("prjl_oid").ToString()) & "  " _
                                            & ";"
                        .Command.ExecuteNonQuery()
                        '.Command.Parameters.Clear()

                        .Command.Commit()
                        MessageBox.Show("Delete data successfull..!", "succ", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        get_data()
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

End Class
