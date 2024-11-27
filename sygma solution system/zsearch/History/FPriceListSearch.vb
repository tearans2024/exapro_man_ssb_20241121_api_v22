Imports master_new.ModFunction

Public Class FPriceListSearch
    Public _row, _en_id, _pi_id, _pi_oid As Integer
    Public _obj, _objk As Object
    Public _type As String
    Dim func_data As New function_data

    Private Sub FPriceListSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        te_search.Focus()
    End Sub

    Public Overrides Sub format_grid()
        add_column(gv_master, "Entity", "en_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Code", "pi_code", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Description", "pi_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "SO Type", "pi_so_type", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Promotion", "promo_desc", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Currency", "cu_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Sales Programe", "sales_program_name", DevExpress.Utils.HorzAlignment.Default)
        add_column(gv_master, "Start Date", "pi_start_date", DevExpress.Utils.HorzAlignment.Center)
        add_column(gv_master, "End Date", "pi_end_date", DevExpress.Utils.HorzAlignment.Center)
    End Sub

    Public Overrides Function get_sequel() As String
        get_sequel = ""

        get_sequel = "SELECT  " _
                    & "  pi_oid, " _
                    & "  pi_dom_id, " _
                    & "  pi_en_id, " _
                    & "  en_desc, " _
                    & "  pi_add_by, " _
                    & "  pi_add_date, " _
                    & "  pi_upd_by, " _
                    & "  pi_upd_date, " _
                    & "  pi_id, " _
                    & "  pi_code, " _
                    & "  pi_desc, " _
                    & "  pi_so_type, " _
                    & "  pi_promo_id, " _
                    & "  promo_desc, " _
                    & "  pi_cu_id, " _
                    & "  cu_name, " _
                    & "  pi_sales_program, " _
                    & "  code_name as sales_program_name, " _
                    & "  pi_start_date, " _
                    & "  pi_end_date, " _
                    & "  pi_active, " _
                    & "  pi_dt " _
                    & "FROM  " _
                    & "  public.pi_mstr " _
                    & " inner join en_mstr on en_id = pi_en_id " _
                    & " inner join cu_mstr on cu_id = pi_cu_id " _
                    & " inner join promo_mstr on promo_id = pi_promo_id " _
                    & " inner join code_mstr on code_id = pi_sales_program " _
                    & " where pi_en_id = " + _en_id.ToString 

        Return get_sequel
    End Function

    Private Sub gv_master_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv_master.DoubleClick
        fill_data()
        Me.Close()
    End Sub

    Private Sub fill_data()
        Dim _row_gv As Integer
        _row_gv = BindingContext(ds.Tables(0)).Position
        Dim dt_bantu As New DataTable()
        Dim func_coll As New function_collection

        If fobject.name = "FPriceListDetail" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("pi_desc")
            '_objk.text = ds.Tables(0).Rows(_row_gv).Item("pi_id")
            fobject._pi_oid = ds.Tables(0).Rows(_row_gv).Item("pi_oid")
        ElseIf fobject.name = "FPriceListCopy" Then
            If _type = "from" Then
                _obj.text = ds.Tables(0).Rows(_row_gv).Item("pi_code")
                fobject._pi_oid_from = ds.Tables(0).Rows(_row_gv).Item("pi_oid")
            ElseIf _type = "to" Then
                _obj.text = ds.Tables(0).Rows(_row_gv).Item("pi_code")
                fobject._pi_oid_to = ds.Tables(0).Rows(_row_gv).Item("pi_oid")
            End If
        ElseIf fobject.name = "FItemQRCodePrint" Then
            _obj.text = ds.Tables(0).Rows(_row_gv).Item("pi_desc")
            '_objk.text = ds.Tables(0).Rows(_row_gv).Item("pi_code")
            fobject._pi_id = ds.Tables(0).Rows(_row_gv).Item("pi_id")
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

    Private Sub gv_master_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles gv_master.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            fill_data()
            Me.Close()
        End If
    End Sub
End Class
