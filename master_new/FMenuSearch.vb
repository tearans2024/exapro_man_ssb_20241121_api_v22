﻿Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FMenuSearch
    Public _row, _en_id As Integer
    Public _obj As Object
    Public _type As String

    Private Sub FSiteSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        'Me.Width = 371
        'Me.Height = 360
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()

        add_column(gv_master, "Menu ID", "menuid", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Menu Name", "menudesc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Menu ID Parent", "menuid_parent", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Menu Parent", "menudesc_parent", DevExpress.Utils.HorzAlignment.Default)
      
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = "SELECT a.menuid,a.menuname,a.menuid_parent,a.menudesc,b.menudesc as menudesc_parent " _
                & " from tconfmenucollection  a left outer join tconfmenucollection b on a.menuid_parent=b.menuid " _
                & " where a.menuname ~~* '%" & te_search.Text & "%' or a.menudesc ~~* '%" & te_search.Text & "%'  order by menudesc"

        Return get_sequel
    End Function

    Private Sub sb_retrieve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles sb_retrieve.Click
        help_load_data(True)
        gc_master.ForceInitialize()
        gv_master.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle
        gv_master.FocusedColumn = gv_master.VisibleColumns(1)
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

    Public Overrides Sub fill_data()

        Try

            Dim _row_gv As Integer
            _row_gv = BindingContext(ds.Tables(0)).Position


            If fobject.name = "FUserGroup" Then
                _obj.editvalue = ds.Tables(0).Rows(_row_gv).Item("menudesc")
                fobject.__menuid = ds.Tables(0).Rows(_row_gv).Item("menuid")


            End If


        Catch ex As Exception
            Pesan(Err)
        End Try

    End Sub


End Class
