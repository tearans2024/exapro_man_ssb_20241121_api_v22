﻿Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FProfitLossdata
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _now As DateTime
    Dim ssql As String


    Private Sub FBalanceSheetReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            _now = func_coll.get_now
            de_first.DateTime = _now
            de_end.DateTime = _now

            dt_bantu = New DataTable
            dt_bantu = (func_data.load_dom_mstr())
            le_domain.Properties.DataSource = dt_bantu
            le_domain.Properties.DisplayMember = dt_bantu.Columns("dom_desc").ToString
            le_domain.Properties.ValueMember = dt_bantu.Columns("dom_id").ToString
            le_domain.ItemIndex = 0

            init_le(le_gcal, "gcal_mstr")
            init_le(le_entity, "en_mstr")
            ssql = "select DISTINCT ac_level from ac_mstr where ac_level > 1 order by ac_level "
            InsertCombo(ssql, Cb_Level)
            Cb_Level.EditValue = 2

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

    Private Sub le_entity_EditalueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_entity.EditValueChanged

    End Sub
    

    Public Overloads Sub load_data_detail()
        Dim level, dom, en, sb, cc As Integer
        Dim posisi As String = ""
        Dim ssqls As New ArrayList


        Try
            dom = 0
            en = 0
            sb = 0
            cc = 0

            If le_domain.EditValue > 0 Then
                'level = 1
                dom = CInt(le_domain.EditValue)
                'If le_entity.EditValue > 0 Then
                level = 2
                '    en = CInt(le_entity.EditValue)
                'End If
            Else
                level = 1
                dom = 1
            End If


            ssql = "SELECT     " _
                    & "a.pl_oid,   a.pl_footer,   a.pl_sign,   a.pl_number,   b.pls_oid,   b.pls_item,    " _
                    & "b.pls_number,   c.pla_ac_id,   d.ac_code,   d.ac_name,   c.pla_ac_hirarki, " _
                    & "(select  sum(v_nilai) as jml  " _
                    & "  from  " _
                    & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
                    & "        x.ac_code,   x.ac_name,   x.ac_type, " _
                    & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",18, " _
                    & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
                    & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
                    & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
                    & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_pst, " _
                    & " (select  sum(v_nilai) as jml  " _
                    & "  from  " _
                    & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
                    & "        x.ac_code,   x.ac_name,   x.ac_type, " _
                    & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",19, " _
                    & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
                    & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
                    & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
                    & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_jbr, " _
                    & "  (select  sum(v_nilai) as jml  " _
                    & "  from  " _
                    & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
                    & "        x.ac_code,   x.ac_name,   x.ac_type, " _
                    & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",20, " _
                    & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
                    & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
                    & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
                    & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_jkt, " _
                    & "  (select  sum(v_nilai) as jml  " _
                    & "  from  " _
                    & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
                    & "        x.ac_code,   x.ac_name,   x.ac_type, " _
                    & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",21, " _
                    & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
                    & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
                    & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
                    & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_jatim, " _
                    & " (select  sum(v_nilai) as jml  " _
                    & "  from  " _
                    & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
                    & "        x.ac_code,   x.ac_name,   x.ac_type, " _
                    & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",22, " _
                    & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
                    & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
                    & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
                    & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_jateng, " _
                    & "  (select  sum(v_nilai) as jml  " _
                    & "  from  " _
                    & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
                    & "        x.ac_code,   x.ac_name,   x.ac_type, " _
                    & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",23, " _
                    & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
                    & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
                    & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
                    & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_lmpng, " _
                    & "    (select  sum(v_nilai) as jml  " _
                    & "  from  " _
                    & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
                    & "        x.ac_code,   x.ac_name,   x.ac_type, " _
                    & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",24, " _
                    & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
                    & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
                    & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
                    & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_bntn, " _
                    & " (select  sum(v_nilai) as jml  " _
                    & "  from  " _
                    & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
                    & "        x.ac_code,   x.ac_name,   x.ac_type, " _
                    & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",25, " _
                    & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
                    & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
                    & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
                    & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_mdn, " _
                    & "  (select  sum(v_nilai) as jml  " _
                    & "  from  " _
                    & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
                    & "        x.ac_code,   x.ac_name,   x.ac_type, " _
                    & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",26, " _
                    & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
                    & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
                    & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
                    & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_plmbng " _
                    & "         " _
                    & "         " _
                    & "FROM   public.pl_setting_mstr a    " _
                    & "INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid)    " _
                    & "INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid)    " _
                    & "INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id)  " _
                    & "ORDER BY   a.pl_number,   b.pls_number"



            'Dim rpt As New rptProfitLossReport
            'With rpt
            '    Dim ds As New DataSet
            '    ds = ReportDataset(ssql)
            '    If ds.Tables(0).Rows.Count < 1 Then
            '        Box("Maaf data kosong")
            '        Exit Sub
            '    End If

            '.vtglawal = tanggal.ToString
            '.vtglakhir = EndOfMonth(tanggal, 0).ToString

            '.vlevel = level
            '.vdom = dom
            '.ven = en
            '.vsb = sb
            '.vcc = cc



            ''ssql = "select * from dom_mstr where dom_id=" & le_domain.EditValue

            ''Dim dt As New DataTable
            ''dt = GetTableData(ssql)

            ''For Each dr As DataRow In dt.Rows
            ''    .XrLabelTitle.Text = dr("dom_company")
            ''Next

            ''If Ce_Posting.EditValue = True Then
            ''    .Posting_Option = True
            ''Else
            ''    .Posting_Option = False
            ''End If

            ''.periode = de_end.DateTime.ToString("dd MMMM yyyy")
            ''.DataSource = ds
            ''.DataMember = "Table"
            '.Parameters("PPeriode").Value = "PERIODE : " & de_first.EditValue & " " & de_end.EditValue
            '.Parameters("PPosisi").Value = posisi

            ''Dim ps As New DevExpress.XtraPrinting.PrintingSystem()
            ''ps.PreviewFormEx.Text = "Profit And Loss Report"
            ''.PrintingSystem = ps
            ''.ShowPreview()


            pgc_profilloss.DataSource = Nothing
            pgc_profilloss.DataMember = Nothing



            Using objload As New master_new.CustomCommand
                With objload

                    .SQL = ssql

                    .InitializeCommand()
                    .FillDataSet(ds, "DS_profit1")
                    pgc_profilloss.DataSource = ds.Tables("DS_profit1")
                    pgc_profilloss.BestFit()

                End With
            End Using




            'End With
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    'Public Overrides Function get_sequel() As String
    '    Dim level, dom, en, sb, cc As Integer
    '    Dim posisi As String = ""
    '    Dim ssqls As New ArrayList


    '        dom = 0
    '        en = 0
    '        sb = 0
    '        cc = 0

    '        If le_domain.EditValue > 0 Then
    '            'level = 1
    '            dom = CInt(le_domain.EditValue)
    '            'If le_entity.EditValue > 0 Then
    '            level = 2
    '            '    en = CInt(le_entity.EditValue)
    '            'End If
    '        Else
    '            level = 1
    '            dom = 1
    '        End If


    '    ssql = "SELECT     " _
    '            & "a.pl_oid,   a.pl_footer,   a.pl_sign,   a.pl_number,   b.pls_oid,   b.pls_item,    " _
    '            & "b.pls_number,   c.pla_ac_id,   d.ac_code,   d.ac_name,   c.pla_ac_hirarki, " _
    '            & "(select  sum(v_nilai) as jml  " _
    '            & "  from  " _
    '            & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
    '            & "        x.ac_code,   x.ac_name,   x.ac_type, " _
    '            & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",18, " _
    '            & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
    '            & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
    '            & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
    '            & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_pst, " _
    '            & " (select  sum(v_nilai) as jml  " _
    '            & "  from  " _
    '            & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
    '            & "        x.ac_code,   x.ac_name,   x.ac_type, " _
    '            & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",19, " _
    '            & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
    '            & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
    '            & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
    '            & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_jbr, " _
    '            & "  (select  sum(v_nilai) as jml  " _
    '            & "  from  " _
    '            & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
    '            & "        x.ac_code,   x.ac_name,   x.ac_type, " _
    '            & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",20, " _
    '            & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
    '            & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
    '            & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
    '            & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_jkt, " _
    '            & "  (select  sum(v_nilai) as jml  " _
    '            & "  from  " _
    '            & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
    '            & "        x.ac_code,   x.ac_name,   x.ac_type, " _
    '            & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",21, " _
    '            & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
    '            & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
    '            & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
    '            & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_jatim, " _
    '            & " (select  sum(v_nilai) as jml  " _
    '            & "  from  " _
    '            & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
    '            & "        x.ac_code,   x.ac_name,   x.ac_type, " _
    '            & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",22, " _
    '            & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
    '            & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
    '            & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
    '            & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_jateng, " _
    '            & "  (select  sum(v_nilai) as jml  " _
    '            & "  from  " _
    '            & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
    '            & "        x.ac_code,   x.ac_name,   x.ac_type, " _
    '            & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",23, " _
    '            & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
    '            & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
    '            & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
    '            & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_lmpng, " _
    '            & "    (select  sum(v_nilai) as jml  " _
    '            & "  from  " _
    '            & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
    '            & "        x.ac_code,   x.ac_name,   x.ac_type, " _
    '            & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",24, " _
    '            & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
    '            & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
    '            & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
    '            & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_bntn, " _
    '            & " (select  sum(v_nilai) as jml  " _
    '            & "  from  " _
    '            & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
    '            & "        x.ac_code,   x.ac_name,   x.ac_type, " _
    '            & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",25, " _
    '            & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
    '            & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
    '            & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
    '            & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_mdn, " _
    '            & "  (select  sum(v_nilai) as jml  " _
    '            & "  from  " _
    '            & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
    '            & "        x.ac_code,   x.ac_name,   x.ac_type, " _
    '            & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",26, " _
    '            & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
    '            & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
    '            & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
    '            & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_plmbng " _
    '            & "         " _
    '            & "         " _
    '            & "FROM   public.pl_setting_mstr a    " _
    '            & "INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid)    " _
    '            & "INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid)    " _
    '            & "INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id)  " _
    '            & "ORDER BY   a.pl_number,   b.pls_number"

    '    Return get_sequel

    'End Function
    Public Overrides Sub load_data_many(ByVal arg As Boolean)
        Cursor = Cursors.WaitCursor
        If arg <> False Then
            '================================================================
            Try
                ds = New DataSet
                Using objload As New master_new.CustomCommand
                    With objload

                        load_profitloss()

                    End With
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Cursor = Cursors.Arrow
    End Sub

    Public Sub load_profitloss()
        Dim level, dom, en, sb, cc As Integer
        Dim posisi As String = ""
        Dim ssqls As New ArrayList


        Try
            dom = 0
            en = 0
            sb = 0
            cc = 0

            If le_domain.EditValue > 0 Then
                'level = 1
                dom = CInt(le_domain.EditValue)
                'If le_entity.EditValue > 0 Then
                level = 2
                '    en = CInt(le_entity.EditValue)
                'End If
            Else
                level = 1
                dom = 1
            End If


            ssql = "SELECT     " _
                    & "a.pl_oid,   a.pl_footer,   a.pl_sign,   a.pl_number,   b.pls_oid,   b.pls_item,    " _
                    & "b.pls_number,   c.pla_ac_id,   d.ac_code,   d.ac_name,   c.pla_ac_hirarki, " _
                    & "(select  sum(v_nilai) as jml  " _
                    & "  from  " _
                    & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
                    & "        x.ac_code,   x.ac_name,   x.ac_type, " _
                    & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",18, " _
                    & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
                    & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
                    & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
                    & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_pst, " _
                    & " (select  sum(v_nilai) as jml  " _
                    & "  from  " _
                    & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
                    & "        x.ac_code,   x.ac_name,   x.ac_type, " _
                    & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",19, " _
                    & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
                    & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
                    & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
                    & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_jbr, " _
                    & "  (select  sum(v_nilai) as jml  " _
                    & "  from  " _
                    & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
                    & "        x.ac_code,   x.ac_name,   x.ac_type, " _
                    & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",20, " _
                    & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
                    & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
                    & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
                    & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_jkt, " _
                    & "  (select  sum(v_nilai) as jml  " _
                    & "  from  " _
                    & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
                    & "        x.ac_code,   x.ac_name,   x.ac_type, " _
                    & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",21, " _
                    & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
                    & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
                    & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
                    & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_jatim, " _
                    & " (select  sum(v_nilai) as jml  " _
                    & "  from  " _
                    & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
                    & "        x.ac_code,   x.ac_name,   x.ac_type, " _
                    & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",22, " _
                    & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
                    & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
                    & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
                    & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_jateng, " _
                    & "  (select  sum(v_nilai) as jml  " _
                    & "  from  " _
                    & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
                    & "        x.ac_code,   x.ac_name,   x.ac_type, " _
                    & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",23, " _
                    & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
                    & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
                    & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
                    & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_lmpng, " _
                    & "    (select  sum(v_nilai) as jml  " _
                    & "  from  " _
                    & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
                    & "        x.ac_code,   x.ac_name,   x.ac_type, " _
                    & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",24, " _
                    & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
                    & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
                    & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
                    & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_bntn, " _
                    & " (select  sum(v_nilai) as jml  " _
                    & "  from  " _
                    & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
                    & "        x.ac_code,   x.ac_name,   x.ac_type, " _
                    & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",25, " _
                    & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
                    & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
                    & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
                    & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_mdn, " _
                    & "  (select  sum(v_nilai) as jml  " _
                    & "  from  " _
                    & "  	( SELECT    x.ac_id,   x.ac_code_hirarki,    " _
                    & "        x.ac_code,   x.ac_name,   x.ac_type, " _
                    & "        f_get_balance_sheet_pl(x.ac_id," & level & "," & dom & ",26, " _
                    & "        cast('" & le_gcal.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") as v_nilai  " _
                    & "        FROM   public.ac_mstr x WHERE   substring(ac_code_hirarki, 1,  " _
                    & "        length(c.pla_ac_hirarki)) = c.pla_ac_hirarki AND     " _
                    & "        ac_is_sumlevel = 'N') as temp) * pls_value as v_plmbng " _
                    & "         " _
                    & "         " _
                    & "FROM   public.pl_setting_mstr a    " _
                    & "INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid)    " _
                    & "INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid)    " _
                    & "INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id)  " _
                    & "ORDER BY   a.pl_number,   b.pls_number"



            'Dim rpt As New rptProfitLossReport
            'With rpt
            '    Dim ds As New DataSet
            '    ds = ReportDataset(ssql)
            '    If ds.Tables(0).Rows.Count < 1 Then
            '        Box("Maaf data kosong")
            '        Exit Sub
            '    End If

            '.vtglawal = tanggal.ToString
            '.vtglakhir = EndOfMonth(tanggal, 0).ToString

            '.vlevel = level
            '.vdom = dom
            '.ven = en
            '.vsb = sb
            '.vcc = cc



            ''ssql = "select * from dom_mstr where dom_id=" & le_domain.EditValue

            ''Dim dt As New DataTable
            ''dt = GetTableData(ssql)

            ''For Each dr As DataRow In dt.Rows
            ''    .XrLabelTitle.Text = dr("dom_company")
            ''Next

            ''If Ce_Posting.EditValue = True Then
            ''    .Posting_Option = True
            ''Else
            ''    .Posting_Option = False
            ''End If

            ''.periode = de_end.DateTime.ToString("dd MMMM yyyy")
            ''.DataSource = ds
            ''.DataMember = "Table"
            '.Parameters("PPeriode").Value = "PERIODE : " & de_first.EditValue & " " & de_end.EditValue
            '.Parameters("PPosisi").Value = posisi

            ''Dim ps As New DevExpress.XtraPrinting.PrintingSystem()
            ''ps.PreviewFormEx.Text = "Profit And Loss Report"
            ''.PrintingSystem = ps
            ''.ShowPreview()


            pgc_profilloss.DataSource = Nothing
            pgc_profilloss.DataMember = Nothing



            Using objload As New master_new.CustomCommand
                With objload

                    .SQL = ssql

                    .InitializeCommand()
                    .FillDataSet(ds, "DS_profitdata1")
                    pgc_profilloss.DataSource = ds.Tables("DS_profitdata1")
                    pgc_profilloss.BestFit()

                End With
            End Using




            'End With
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub


    'Private Sub load_profitloss(ByVal par_obj As Object)
    '    pgc_profilloss.DataSource = Nothing
    '    pgc_profilloss.DataMember = Nothing

    '    With par_obj
    '        .SQL = par_obj

    '        .InitializeCommand()
    '        .FillDataSet(ds, "book_total")
    '        pgc_profilloss.DataSource = ds.Tables("book_total")
    '        pgc_profilloss.BestFit()
    '    End With
    'End Sub

    Private Sub le_gcal_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_gcal.EditValueChanged
        Try
            de_first.EditValue = le_gcal.GetColumnValue("gcal_start_date")
            de_end.EditValue = le_gcal.GetColumnValue("gcal_end_date")

            If le_gcal.GetColumnValue("gcal_closing") = "Y" Then
                Ce_Posting.EditValue = True
            Else
                Ce_Posting.EditValue = False
            End If
        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
