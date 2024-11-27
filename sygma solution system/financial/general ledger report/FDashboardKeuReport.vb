Imports master_new.ModFunction
Imports master_new.PGSqlConn

Public Class FDashboardKeuReport
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
            init_le(le_gcal2, "gcal_mstr")
            init_le(le_entity, "en_mstr")
            ssql = "select DISTINCT ac_level from ac_mstr where ac_level > 1 order by ac_level "
            InsertCombo(ssql, Cb_Level)
            Cb_Level.EditValue = 2

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
    Private Function periode_to_bln(ByVal par_periode As Integer) As String
        If par_periode = 1 Then
            Return "JAN"
        ElseIf par_periode = 2 Then
            Return "FEB"

        ElseIf par_periode = 3 Then
            Return "MAR"
        ElseIf par_periode = 4 Then
            Return "APR"
        ElseIf par_periode = 5 Then
            Return "MEI"
        ElseIf par_periode = 6 Then
            Return "JUN"
        ElseIf par_periode = 7 Then
            Return "JUL"
        ElseIf par_periode = 8 Then
            Return "AGS"
        ElseIf par_periode = 9 Then
            Return "SEP"
        ElseIf par_periode = 10 Then
            Return "OKT"
        ElseIf par_periode = 11 Then
            Return "NOP"
        ElseIf par_periode = 12 Then
            Return "DES"
        Else
            Return ""
        End If
    End Function
    Private Sub le_entity_EditalueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_entity.EditValueChanged

    End Sub

    Public Overrides Sub preview()
        Dim level, dom, en, sb, cc As Integer
        Dim posisi As String = ""
        Dim ssqls As New ArrayList


        Try
            dom = 0
            en = 0
            sb = 0
            cc = 0

            If le_domain.EditValue > 0 Then
                level = 1
                dom = CInt(le_domain.EditValue)
                If le_entity.EditValue > 0 Then
                    level = 2
                    en = CInt(le_entity.EditValue)
                End If
            Else
                level = 1
                dom = 1
            End If


            ssql = "SELECT  " _
                   & "  a.gcal_oid, " _
                   & "  a.gcal_year, " _
                   & "  a.gcal_periode, " _
                   & "  a.gcal_start_date, " _
                   & "  a.gcal_end_date,coalesce(a.gcal_pra_closing,'N') as gcal_pra_closing,coalesce(gcal_closing,'N') as  gcal_closing " _
                   & "FROM " _
                   & "  public.gcal_mstr a " _
                   & " Where gcal_start_date between  " & SetSetring(le_gcal.GetColumnValue("gcal_start_date")) & " and " _
                   & SetSetring(le_gcal2.GetColumnValue("gcal_start_date")) _
                   & " ORDER BY " _
                   & "  a.gcal_year , " _
                   & "  a.gcal_periode "

            Dim dt_gcal As New DataTable
            dt_gcal = GetTableData(ssql)
            If dt_gcal.Rows.Count > 12 Then
                Box("Max data 12 periode")
                Exit Sub
            End If

            Dim rpt1 As New rptDashboardKeuangan
            ssql = ""
            For Each dr_gcal As DataRow In dt_gcal.Rows
                ssql = ssql & "SELECT  " _
                 & "  a.pl_oid, " _
                 & "  a.pl_footer, " _
                 & "  a.pl_sign, " _
                 & "  a.pl_number, " _
                 & "  b.pls_oid, " _
                 & "  b.pls_item, " _
                 & "  b.pls_number, " _
                 & "  c.pla_ac_id, " _
                 & "  d.ac_code, " _
                 & "  d.ac_name,y.ac_code as ac_code_det,y.ac_name as ac_name_det, " _
                 & "  f_get_balance_sheet_pl(y.ac_id," _
                 & level & "," & dom & "," & en & ",cast('" & dr_gcal("gcal_oid") & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") * pls_value / 1000000 as v_nilai, " _
                 & "  c.pla_ac_hirarki " _
                 & "FROM " _
                 & "  public.pl_setting_mstr a " _
                 & "  INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid) " _
                 & "  INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid) " _
                 & "  INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id) " _
                 & "  LEFT OUTER JOIN public.ac_mstr y ON (c.pla_ac_hirarki = substring(y.ac_code_hirarki, 1, length(c.pla_ac_hirarki))) " _
                 & " WHERE  coalesce(y.ac_is_sumlevel,'N') = 'N' " _
                 & " union all "
            Next

            ssql = Microsoft.VisualBasic.Left(ssql, ssql.Length - 11)

            Dim ds_sub_report As New DataSet
            ds_sub_report = ReportDataset(ssql)

            Dim rptLRSub As New rptDashboardKeuanganSubLR

            rpt1.XrSubLR.ReportSource = rptLRSub
            rptLRSub.DataSource = ds_sub_report
            rptLRSub.DataMember = "Table"

            '=====================================
           

            rpt1.Lbl1.Text = ""
            rpt1.Lbl2.Text = ""
            rpt1.Lbl3.Text = ""
            rpt1.Lbl4.Text = ""
            rpt1.Lbl5.Text = ""
            rpt1.Lbl6.Text = ""
            rpt1.Lbl7.Text = ""
            rpt1.Lbl8.Text = ""
            rpt1.Lbl9.Text = ""
            rpt1.Lbl10.Text = ""
            rpt1.Lbl11.Text = ""
            rpt1.Lbl12.Text = ""

            Dim x As Integer = 1
            For Each dr_gcal As DataRow In dt_gcal.Rows
                If x = 1 Then
                    ssql = "SELECT  " _
                        & "  a.pl_oid, " _
                        & "  a.pl_footer, " _
                        & "  a.pl_sign, " _
                        & "  a.pl_number, " _
                        & "  b.pls_oid, " _
                        & "  b.pls_item, " _
                        & "  b.pls_number, " _
                        & "  c.pla_ac_id, " _
                        & "  d.ac_code, " _
                        & "  d.ac_name,y.ac_code as ac_code_det,y.ac_name as ac_name_det, " _
                        & "  f_get_balance_sheet_pl(y.ac_id," _
                        & level & "," & dom & "," & en & ",cast('" & dr_gcal("gcal_oid") & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") * pls_value / 1000000 as v_nilai, " _
                        & "  c.pla_ac_hirarki " _
                        & "FROM " _
                        & "  public.pl_setting_mstr a " _
                        & "  INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid) " _
                        & "  INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid) " _
                        & "  INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id) " _
                        & "  LEFT OUTER JOIN public.ac_mstr y ON (c.pla_ac_hirarki = substring(y.ac_code_hirarki, 1, length(c.pla_ac_hirarki))) " _
                        & " WHERE  coalesce(y.ac_is_sumlevel,'N') = 'N' " _
                        & "ORDER BY " _
                        & "  a.pl_number, " _
                        & "  b.pls_number "

                    Dim ds_sub_report_rl1 As New DataSet
                    ds_sub_report_rl1 = ReportDataset(ssql)

                    Dim rptLRSub1 As New rptDashboardKeuanganSubLR1

                    rpt1.XrSubTrend1.ReportSource = rptLRSub1
                    rptLRSub1.DataSource = ds_sub_report_rl1
                    rptLRSub1.DataMember = "Table"

                    rpt1.Lbl1.Text = periode_to_bln(SetNumber(dr_gcal("gcal_periode")))

                ElseIf x = 2 Then

                    ssql = "SELECT  " _
                       & "  a.pl_oid, " _
                       & "  a.pl_footer, " _
                       & "  a.pl_sign, " _
                       & "  a.pl_number, " _
                       & "  b.pls_oid, " _
                       & "  b.pls_item, " _
                       & "  b.pls_number, " _
                       & "  c.pla_ac_id, " _
                       & "  d.ac_code, " _
                       & "  d.ac_name,y.ac_code as ac_code_det,y.ac_name as ac_name_det, " _
                       & "  f_get_balance_sheet_pl(y.ac_id," _
                       & level & "," & dom & "," & en & ",cast('" & dr_gcal("gcal_oid") & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") * pls_value / 1000000 as v_nilai, " _
                       & "  c.pla_ac_hirarki " _
                       & "FROM " _
                       & "  public.pl_setting_mstr a " _
                       & "  INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid) " _
                       & "  INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid) " _
                       & "  INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id) " _
                       & "  LEFT OUTER JOIN public.ac_mstr y ON (c.pla_ac_hirarki = substring(y.ac_code_hirarki, 1, length(c.pla_ac_hirarki))) " _
                       & " WHERE  coalesce(y.ac_is_sumlevel,'N') = 'N' " _
                       & "ORDER BY " _
                       & "  a.pl_number, " _
                       & "  b.pls_number "

                    Dim ds_sub_report_rl2 As New DataSet
                    ds_sub_report_rl2 = ReportDataset(ssql)

                    Dim rptLRSub2 As New rptDashboardKeuanganSubLR2

                    rpt1.XrSubTrend2.ReportSource = rptLRSub2
                    rptLRSub2.DataSource = ds_sub_report_rl2
                    rptLRSub2.DataMember = "Table"

                    rpt1.Lbl2.Text = periode_to_bln(SetNumber(dr_gcal("gcal_periode")))

                ElseIf x = 3 Then
                    ssql = "SELECT  " _
                    & "  a.pl_oid, " _
                    & "  a.pl_footer, " _
                    & "  a.pl_sign, " _
                    & "  a.pl_number, " _
                    & "  b.pls_oid, " _
                    & "  b.pls_item, " _
                    & "  b.pls_number, " _
                    & "  c.pla_ac_id, " _
                    & "  d.ac_code, " _
                    & "  d.ac_name,y.ac_code as ac_code_det,y.ac_name as ac_name_det, " _
                    & "  f_get_balance_sheet_pl(y.ac_id," _
                    & level & "," & dom & "," & en & ",cast('" & dr_gcal("gcal_oid") & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") * pls_value / 1000000 as v_nilai, " _
                    & "  c.pla_ac_hirarki " _
                    & "FROM " _
                    & "  public.pl_setting_mstr a " _
                    & "  INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid) " _
                    & "  INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid) " _
                    & "  INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id) " _
                    & "  LEFT OUTER JOIN public.ac_mstr y ON (c.pla_ac_hirarki = substring(y.ac_code_hirarki, 1, length(c.pla_ac_hirarki))) " _
                    & " WHERE  coalesce(y.ac_is_sumlevel,'N') = 'N' " _
                    & "ORDER BY " _
                    & "  a.pl_number, " _
                    & "  b.pls_number "

                    Dim ds_sub_report_rl3 As New DataSet
                    ds_sub_report_rl3 = ReportDataset(ssql)

                    Dim rptLRSub3 As New rptDashboardKeuanganSubLR2

                    rpt1.XrSubTrend3.ReportSource = rptLRSub3
                    rptLRSub3.DataSource = ds_sub_report_rl3
                    rptLRSub3.DataMember = "Table"
                    rpt1.Lbl3.Text = periode_to_bln(SetNumber(dr_gcal("gcal_periode")))
                ElseIf x = 4 Then
                    ssql = "SELECT  " _
                    & "  a.pl_oid, " _
                    & "  a.pl_footer, " _
                    & "  a.pl_sign, " _
                    & "  a.pl_number, " _
                    & "  b.pls_oid, " _
                    & "  b.pls_item, " _
                    & "  b.pls_number, " _
                    & "  c.pla_ac_id, " _
                    & "  d.ac_code, " _
                    & "  d.ac_name,y.ac_code as ac_code_det,y.ac_name as ac_name_det, " _
                    & "  f_get_balance_sheet_pl(y.ac_id," _
                    & level & "," & dom & "," & en & ",cast('" & dr_gcal("gcal_oid") & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") * pls_value / 1000000 as v_nilai, " _
                    & "  c.pla_ac_hirarki " _
                    & "FROM " _
                    & "  public.pl_setting_mstr a " _
                    & "  INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid) " _
                    & "  INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid) " _
                    & "  INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id) " _
                    & "  LEFT OUTER JOIN public.ac_mstr y ON (c.pla_ac_hirarki = substring(y.ac_code_hirarki, 1, length(c.pla_ac_hirarki))) " _
                    & " WHERE  coalesce(y.ac_is_sumlevel,'N') = 'N' " _
                    & "ORDER BY " _
                    & "  a.pl_number, " _
                    & "  b.pls_number "

                    Dim ds_sub_report_rl4 As New DataSet
                    ds_sub_report_rl4 = ReportDataset(ssql)

                    Dim rptLRSub4 As New rptDashboardKeuanganSubLR2

                    rpt1.XrSubTrend4.ReportSource = rptLRSub4
                    rptLRSub4.DataSource = ds_sub_report_rl4
                    rptLRSub4.DataMember = "Table"
                    rpt1.Lbl4.Text = periode_to_bln(SetNumber(dr_gcal("gcal_periode")))
                ElseIf x = 5 Then
                    ssql = "SELECT  " _
                    & "  a.pl_oid, " _
                    & "  a.pl_footer, " _
                    & "  a.pl_sign, " _
                    & "  a.pl_number, " _
                    & "  b.pls_oid, " _
                    & "  b.pls_item, " _
                    & "  b.pls_number, " _
                    & "  c.pla_ac_id, " _
                    & "  d.ac_code, " _
                    & "  d.ac_name,y.ac_code as ac_code_det,y.ac_name as ac_name_det, " _
                    & "  f_get_balance_sheet_pl(y.ac_id," _
                    & level & "," & dom & "," & en & ",cast('" & dr_gcal("gcal_oid") & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") * pls_value / 1000000 as v_nilai, " _
                    & "  c.pla_ac_hirarki " _
                    & "FROM " _
                    & "  public.pl_setting_mstr a " _
                    & "  INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid) " _
                    & "  INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid) " _
                    & "  INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id) " _
                    & "  LEFT OUTER JOIN public.ac_mstr y ON (c.pla_ac_hirarki = substring(y.ac_code_hirarki, 1, length(c.pla_ac_hirarki))) " _
                    & " WHERE  coalesce(y.ac_is_sumlevel,'N') = 'N' " _
                    & "ORDER BY " _
                    & "  a.pl_number, " _
                    & "  b.pls_number "

                    Dim ds_sub_report_rl5 As New DataSet
                    ds_sub_report_rl5 = ReportDataset(ssql)

                    Dim rptLRSub5 As New rptDashboardKeuanganSubLR2

                    rpt1.XrSubTrend5.ReportSource = rptLRSub5
                    rptLRSub5.DataSource = ds_sub_report_rl5
                    rptLRSub5.DataMember = "Table"
                    rpt1.Lbl5.Text = periode_to_bln(SetNumber(dr_gcal("gcal_periode")))
                ElseIf x = 6 Then
                    ssql = "SELECT  " _
                    & "  a.pl_oid, " _
                    & "  a.pl_footer, " _
                    & "  a.pl_sign, " _
                    & "  a.pl_number, " _
                    & "  b.pls_oid, " _
                    & "  b.pls_item, " _
                    & "  b.pls_number, " _
                    & "  c.pla_ac_id, " _
                    & "  d.ac_code, " _
                    & "  d.ac_name,y.ac_code as ac_code_det,y.ac_name as ac_name_det, " _
                    & "  f_get_balance_sheet_pl(y.ac_id," _
                    & level & "," & dom & "," & en & ",cast('" & dr_gcal("gcal_oid") & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") * pls_value / 1000000 as v_nilai, " _
                    & "  c.pla_ac_hirarki " _
                    & "FROM " _
                    & "  public.pl_setting_mstr a " _
                    & "  INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid) " _
                    & "  INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid) " _
                    & "  INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id) " _
                    & "  LEFT OUTER JOIN public.ac_mstr y ON (c.pla_ac_hirarki = substring(y.ac_code_hirarki, 1, length(c.pla_ac_hirarki))) " _
                    & " WHERE  coalesce(y.ac_is_sumlevel,'N') = 'N' " _
                    & "ORDER BY " _
                    & "  a.pl_number, " _
                    & "  b.pls_number "

                    Dim ds_sub_report_rl6 As New DataSet
                    ds_sub_report_rl6 = ReportDataset(ssql)

                    Dim rptLRSub6 As New rptDashboardKeuanganSubLR2

                    rpt1.XrSubTrend6.ReportSource = rptLRSub6
                    rptLRSub6.DataSource = ds_sub_report_rl6
                    rptLRSub6.DataMember = "Table"
                    rpt1.Lbl6.Text = periode_to_bln(SetNumber(dr_gcal("gcal_periode")))
                ElseIf x = 7 Then
                    ssql = "SELECT  " _
                    & "  a.pl_oid, " _
                    & "  a.pl_footer, " _
                    & "  a.pl_sign, " _
                    & "  a.pl_number, " _
                    & "  b.pls_oid, " _
                    & "  b.pls_item, " _
                    & "  b.pls_number, " _
                    & "  c.pla_ac_id, " _
                    & "  d.ac_code, " _
                    & "  d.ac_name,y.ac_code as ac_code_det,y.ac_name as ac_name_det, " _
                    & "  f_get_balance_sheet_pl(y.ac_id," _
                    & level & "," & dom & "," & en & ",cast('" & dr_gcal("gcal_oid") & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") * pls_value / 1000000 as v_nilai, " _
                    & "  c.pla_ac_hirarki " _
                    & "FROM " _
                    & "  public.pl_setting_mstr a " _
                    & "  INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid) " _
                    & "  INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid) " _
                    & "  INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id) " _
                    & "  LEFT OUTER JOIN public.ac_mstr y ON (c.pla_ac_hirarki = substring(y.ac_code_hirarki, 1, length(c.pla_ac_hirarki))) " _
                    & " WHERE  coalesce(y.ac_is_sumlevel,'N') = 'N' " _
                    & "ORDER BY " _
                    & "  a.pl_number, " _
                    & "  b.pls_number "

                    Dim ds_sub_report_rl7 As New DataSet
                    ds_sub_report_rl7 = ReportDataset(ssql)

                    Dim rptLRSub7 As New rptDashboardKeuanganSubLR2

                    rpt1.XrSubTrend7.ReportSource = rptLRSub7
                    rptLRSub7.DataSource = ds_sub_report_rl7
                    rptLRSub7.DataMember = "Table"
                    rpt1.Lbl7.Text = periode_to_bln(SetNumber(dr_gcal("gcal_periode")))
                ElseIf x = 8 Then
                    ssql = "SELECT  " _
                    & "  a.pl_oid, " _
                    & "  a.pl_footer, " _
                    & "  a.pl_sign, " _
                    & "  a.pl_number, " _
                    & "  b.pls_oid, " _
                    & "  b.pls_item, " _
                    & "  b.pls_number, " _
                    & "  c.pla_ac_id, " _
                    & "  d.ac_code, " _
                    & "  d.ac_name,y.ac_code as ac_code_det,y.ac_name as ac_name_det, " _
                    & "  f_get_balance_sheet_pl(y.ac_id," _
                    & level & "," & dom & "," & en & ",cast('" & dr_gcal("gcal_oid") & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") * pls_value / 1000000 as v_nilai, " _
                    & "  c.pla_ac_hirarki " _
                    & "FROM " _
                    & "  public.pl_setting_mstr a " _
                    & "  INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid) " _
                    & "  INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid) " _
                    & "  INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id) " _
                    & "  LEFT OUTER JOIN public.ac_mstr y ON (c.pla_ac_hirarki = substring(y.ac_code_hirarki, 1, length(c.pla_ac_hirarki))) " _
                    & " WHERE  coalesce(y.ac_is_sumlevel,'N') = 'N' " _
                    & "ORDER BY " _
                    & "  a.pl_number, " _
                    & "  b.pls_number "

                    Dim ds_sub_report_rl8 As New DataSet
                    ds_sub_report_rl8 = ReportDataset(ssql)

                    Dim rptLRSub8 As New rptDashboardKeuanganSubLR2

                    rpt1.XrSubTrend8.ReportSource = rptLRSub8
                    rptLRSub8.DataSource = ds_sub_report_rl8
                    rptLRSub8.DataMember = "Table"
                    rpt1.Lbl8.Text = periode_to_bln(SetNumber(dr_gcal("gcal_periode")))
                ElseIf x = 9 Then
                    ssql = "SELECT  " _
                    & "  a.pl_oid, " _
                    & "  a.pl_footer, " _
                    & "  a.pl_sign, " _
                    & "  a.pl_number, " _
                    & "  b.pls_oid, " _
                    & "  b.pls_item, " _
                    & "  b.pls_number, " _
                    & "  c.pla_ac_id, " _
                    & "  d.ac_code, " _
                    & "  d.ac_name,y.ac_code as ac_code_det,y.ac_name as ac_name_det, " _
                    & "  f_get_balance_sheet_pl(y.ac_id," _
                    & level & "," & dom & "," & en & ",cast('" & dr_gcal("gcal_oid") & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") * pls_value / 1000000 as v_nilai, " _
                    & "  c.pla_ac_hirarki " _
                    & "FROM " _
                    & "  public.pl_setting_mstr a " _
                    & "  INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid) " _
                    & "  INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid) " _
                    & "  INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id) " _
                    & "  LEFT OUTER JOIN public.ac_mstr y ON (c.pla_ac_hirarki = substring(y.ac_code_hirarki, 1, length(c.pla_ac_hirarki))) " _
                    & " WHERE  coalesce(y.ac_is_sumlevel,'N') = 'N' " _
                    & "ORDER BY " _
                    & "  a.pl_number, " _
                    & "  b.pls_number "

                    Dim ds_sub_report_rl9 As New DataSet
                    ds_sub_report_rl9 = ReportDataset(ssql)

                    Dim rptLRSub9 As New rptDashboardKeuanganSubLR2

                    rpt1.XrSubTrend9.ReportSource = rptLRSub9
                    rptLRSub9.DataSource = ds_sub_report_rl9
                    rptLRSub9.DataMember = "Table"
                    rpt1.Lbl9.Text = periode_to_bln(SetNumber(dr_gcal("gcal_periode")))
                ElseIf x = 10 Then
                    ssql = "SELECT  " _
                    & "  a.pl_oid, " _
                    & "  a.pl_footer, " _
                    & "  a.pl_sign, " _
                    & "  a.pl_number, " _
                    & "  b.pls_oid, " _
                    & "  b.pls_item, " _
                    & "  b.pls_number, " _
                    & "  c.pla_ac_id, " _
                    & "  d.ac_code, " _
                    & "  d.ac_name,y.ac_code as ac_code_det,y.ac_name as ac_name_det, " _
                    & "  f_get_balance_sheet_pl(y.ac_id," _
                    & level & "," & dom & "," & en & ",cast('" & dr_gcal("gcal_oid") & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") * pls_value / 1000000 as v_nilai, " _
                    & "  c.pla_ac_hirarki " _
                    & "FROM " _
                    & "  public.pl_setting_mstr a " _
                    & "  INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid) " _
                    & "  INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid) " _
                    & "  INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id) " _
                    & "  LEFT OUTER JOIN public.ac_mstr y ON (c.pla_ac_hirarki = substring(y.ac_code_hirarki, 1, length(c.pla_ac_hirarki))) " _
                    & " WHERE  coalesce(y.ac_is_sumlevel,'N') = 'N' " _
                    & "ORDER BY " _
                    & "  a.pl_number, " _
                    & "  b.pls_number "

                    Dim ds_sub_report_rl10 As New DataSet
                    ds_sub_report_rl10 = ReportDataset(ssql)

                    Dim rptLRSub10 As New rptDashboardKeuanganSubLR2

                    rpt1.XrSubTrend10.ReportSource = rptLRSub10
                    rptLRSub10.DataSource = ds_sub_report_rl10
                    rptLRSub10.DataMember = "Table"
                    rpt1.Lbl10.Text = periode_to_bln(SetNumber(dr_gcal("gcal_periode")))
                ElseIf x = 11 Then
                    ssql = "SELECT  " _
                    & "  a.pl_oid, " _
                    & "  a.pl_footer, " _
                    & "  a.pl_sign, " _
                    & "  a.pl_number, " _
                    & "  b.pls_oid, " _
                    & "  b.pls_item, " _
                    & "  b.pls_number, " _
                    & "  c.pla_ac_id, " _
                    & "  d.ac_code, " _
                    & "  d.ac_name,y.ac_code as ac_code_det,y.ac_name as ac_name_det, " _
                    & "  f_get_balance_sheet_pl(y.ac_id," _
                    & level & "," & dom & "," & en & ",cast('" & dr_gcal("gcal_oid") & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") * pls_value / 1000000 as v_nilai, " _
                    & "  c.pla_ac_hirarki " _
                    & "FROM " _
                    & "  public.pl_setting_mstr a " _
                    & "  INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid) " _
                    & "  INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid) " _
                    & "  INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id) " _
                    & "  LEFT OUTER JOIN public.ac_mstr y ON (c.pla_ac_hirarki = substring(y.ac_code_hirarki, 1, length(c.pla_ac_hirarki))) " _
                    & " WHERE  coalesce(y.ac_is_sumlevel,'N') = 'N' " _
                    & "ORDER BY " _
                    & "  a.pl_number, " _
                    & "  b.pls_number "

                    Dim ds_sub_report_rl11 As New DataSet
                    ds_sub_report_rl11 = ReportDataset(ssql)

                    Dim rptLRSub11 As New rptDashboardKeuanganSubLR2

                    rpt1.XrSubTrend11.ReportSource = rptLRSub11
                    rptLRSub11.DataSource = ds_sub_report_rl11
                    rptLRSub11.DataMember = "Table"
                    rpt1.Lbl11.Text = periode_to_bln(SetNumber(dr_gcal("gcal_periode")))
                ElseIf x = 12 Then
                    ssql = "SELECT  " _
                    & "  a.pl_oid, " _
                    & "  a.pl_footer, " _
                    & "  a.pl_sign, " _
                    & "  a.pl_number, " _
                    & "  b.pls_oid, " _
                    & "  b.pls_item, " _
                    & "  b.pls_number, " _
                    & "  c.pla_ac_id, " _
                    & "  d.ac_code, " _
                    & "  d.ac_name,y.ac_code as ac_code_det,y.ac_name as ac_name_det, " _
                    & "  f_get_balance_sheet_pl(y.ac_id," _
                    & level & "," & dom & "," & en & ",cast('" & dr_gcal("gcal_oid") & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ") * pls_value / 1000000 as v_nilai, " _
                    & "  c.pla_ac_hirarki " _
                    & "FROM " _
                    & "  public.pl_setting_mstr a " _
                    & "  INNER JOIN public.pl_setting_sub b ON (a.pl_oid = b.pls_pl_oid) " _
                    & "  INNER JOIN public.pl_setting_account c ON (b.pls_oid = c.pla_pls_oid) " _
                    & "  INNER JOIN public.ac_mstr d ON (c.pla_ac_id = d.ac_id) " _
                    & "  LEFT OUTER JOIN public.ac_mstr y ON (c.pla_ac_hirarki = substring(y.ac_code_hirarki, 1, length(c.pla_ac_hirarki))) " _
                    & " WHERE  coalesce(y.ac_is_sumlevel,'N') = 'N' " _
                    & "ORDER BY " _
                    & "  a.pl_number, " _
                    & "  b.pls_number "

                    Dim ds_sub_report_rl12 As New DataSet
                    ds_sub_report_rl12 = ReportDataset(ssql)

                    Dim rptLRSub12 As New rptDashboardKeuanganSubLR2

                    rpt1.XrSubTrend12.ReportSource = rptLRSub12
                    rptLRSub12.DataSource = ds_sub_report_rl12
                    rptLRSub12.DataMember = "Table"
                    rpt1.Lbl12.Text = periode_to_bln(SetNumber(dr_gcal("gcal_periode")))
                End If

                x = x + 1
            Next

            





            'end of lr setahun

            ssql = ""
            For Each dr_gcal As DataRow In dt_gcal.Rows
                ssql = ssql & "SELECT  " _
                 & "  a.code, " _
                 & "  a.remark, " _
                 & "  a.sort_number, " _
                 & "  a.remark_header, " _
                 & "  a.remark_footer, " _
                 & "  a.cfsign_header, " _
                 & "  a.cf_value_sign, " _
                 & "  b.cfdet_oid, " _
                 & "  b.cfdet_pk, " _
                 & "  b.seq, " _
                 & "  b.sub_header,b.sub_header2, case when b.sub_header='' then b.sub_header2 else b.sub_header end as sub_header_new,  " _
                 & "  c.code as code_det, " _
                 & "  c.seq as seq_det, " _
                 & "  c.ac_hirarki, " _
                 & "  d.ac_id, " _
                 & "  d.ac_code, " _
                 & "  d.ac_name,f_get_cfvalue(d.ac_id,'" & dr_gcal("gcal_oid") & "','Y') * c.ac_value / 1000000 as cf_value,0.0 as cf_value_beginning,0.0 as cf_value_ending " _
                 & "FROM " _
                 & "  public.tconfsettingcashflow a " _
                 & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                 & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code) " _
                 & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
                 & "WHERE " _
                 & "  a.cfsign_header = 'T' and d.ac_is_sumlevel='N' and cf_type='I' " _
                 & " union all "

            Next
            ssql = Microsoft.VisualBasic.Left(ssql, ssql.Length - 11)

            Dim ds_sub_report2 As New DataSet
            ds_sub_report2 = ReportDataset(ssql)


            ssql = "SELECT  " _
                  & " sum((SELECT  " _
                  & "       sum(glbal_balance_open/ 1000000) AS jml " _
                  & "       FROM " _
                  & "       public.glbal_balance x " _
                  & "       WHERE " _
                  & "       x.glbal_gcal_oid = '" & le_gcal.EditValue & "' AND   " _
                  & "       x.glbal_ac_id = d.ac_id )) as total " _
                  & "FROM " _
                  & "  public.tconfsettingcashflow a " _
                  & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
                  & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code) " _
                  & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
                  & "WHERE " _
                  & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='I' "

            Dim _awal, _akhir As Double
            _awal = 0
            _akhir = 0

            Dim dt_temp As New DataTable

            dt_temp = master_new.PGSqlConn.GetTableData(ssql)

            For Each dr As DataRow In dt_temp.Rows
                _awal = dr("total")
            Next

            ssql = "SELECT  " _
               & " sum((SELECT  " _
               & "       sum(glbal_balance_open + glbal_balance_posted )/ 1000000 AS jml " _
               & "       FROM " _
               & "       public.glbal_balance x " _
               & "       WHERE " _
               & "       x.glbal_gcal_oid = '" & le_gcal2.EditValue & "' AND   " _
               & "       x.glbal_ac_id = d.ac_id )) as total " _
               & "FROM " _
               & "  public.tconfsettingcashflow a " _
               & "  INNER JOIN public.tconfsettingcashflowdet b ON (a.code = b.code) " _
               & "  INNER JOIN public.tconfsettingcashflowdet_item c ON (b.cfdet_pk = c.code) " _
               & "  INNER JOIN public.ac_mstr d ON  ( substring(d.ac_code_hirarki, 1, length(c.ac_hirarki)) = c.ac_hirarki) " _
               & "WHERE " _
               & "  a.cfsign_header = 'B' and d.ac_is_sumlevel='N' and cf_type='I' "


            dt_temp = master_new.PGSqlConn.GetTableData(ssql)

            For Each dr As DataRow In dt_temp.Rows
                _akhir = dr("total")
            Next

            For Each dr As DataRow In ds_sub_report2.Tables(0).Rows
                dr("cf_value_beginning") = _awal
                dr("cf_value_ending") = _akhir
            Next

            ds_sub_report2.AcceptChanges()

            Dim rptCF As New rptDashboardKeuanganSubCF

            rpt1.XrSubCF.ReportSource = rptCF
            rptCF.DataSource = ds_sub_report2
            rptCF.DataMember = "Table"

            'ssql = ""
            'x = 1
            'For Each dr_gcal As DataRow In dt_gcal.Rows
            '    If x = 1 Then

            '            & " union all "
            '    Else
            '        ssql = ssql & "SELECT a.bs_number,  a.bs_caption,  a.bs_group,  a.bs_remarks,  b.bsd_number,  b.bsd_caption,  b.bsd_remarks, " _
            '            & "c.bsdi_number,  c.bsdi_caption,  c.bsdi_oid, y.ac_code, " _
            '            & "  y.ac_name, " _
            '            & "  y.ac_type,f_get_balance_sheet_trans(y.ac_id," _
            '            & level & "," & dom & "," & en & ",cast('" & dr_gcal("gcal_oid") & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ")/ 1000000 as z_nilai " _
            '            & "FROM " _
            '            & "  public.bs_mstr a " _
            '            & "  INNER JOIN public.bsd_det b ON (a.bs_number = b.bsd_bs_number) " _
            '            & "  INNER JOIN public.bsdi_det_item c ON (b.bsd_oid = c.bsdi_bsd_oid) " _
            '            & "  INNER JOIN public.bsda_account x ON (x.bsda_bsdi_oid = c.bsdi_oid) " _
            '            & "  INNER JOIN public.ac_mstr y ON (substring(y.ac_code_hirarki, 1, length(x.bsda_ac_hirarki)) = x.bsda_ac_hirarki)  " _
            '            & " Where y.ac_is_sumlevel='N' " _
            '            & " union all "
            '    End If


            '    x = x + 1
            'Next

            'ssql = Microsoft.VisualBasic.Left(ssql, ssql.Length - 11)

            ssql = "SELECT a.bs_number,  a.bs_caption,  a.bs_group,  a.bs_remarks,  b.bsd_number,  b.bsd_caption,  b.bsd_remarks, " _
                       & "c.bsdi_number,  c.bsdi_caption,  c.bsdi_oid, y.ac_code, " _
                       & "  y.ac_name, " _
                       & "  y.ac_type,f_get_balance_sheet(y.ac_id," _
                       & level & "," & dom & "," & en & ",cast('" & le_gcal2.EditValue & "' as uuid)," & SetBitYN(Ce_Posting.EditValue) & ")/ 1000000 as z_nilai " _
                       & "FROM " _
                       & "  public.bs_mstr a " _
                       & "  INNER JOIN public.bsd_det b ON (a.bs_number = b.bsd_bs_number) " _
                       & "  INNER JOIN public.bsdi_det_item c ON (b.bsd_oid = c.bsdi_bsd_oid) " _
                       & "  INNER JOIN public.bsda_account x ON (x.bsda_bsdi_oid = c.bsdi_oid) " _
                       & "  INNER JOIN public.ac_mstr y ON (substring(y.ac_code_hirarki, 1, length(x.bsda_ac_hirarki)) = x.bsda_ac_hirarki)  " _
                       & " Where y.ac_is_sumlevel='N' "

            Dim ds_sub_report3 As New DataSet
            ds_sub_report3 = ReportDataset(ssql)

            Dim rptBS As New rptDashboardKeuanganSubBS

            rpt1.XrSubBS.ReportSource = rptBS
            rptBS.DataSource = ds_sub_report3
            rptBS.DataMember = "Table"


            ssql = "select cmaddr_mstr.cmaddr_name,  " _
            & "cmaddr_mstr.cmaddr_line_1, " _
            & "cmaddr_mstr.cmaddr_line_2,  " _
            & "cmaddr_mstr.cmaddr_line_3 " _
            & " from cmaddr_mstr where cmaddr_en_id = 0 "
            Dim dt_cmaddr As New DataTable
            dt_cmaddr = GetTableData(ssql)

            For Each dr As DataRow In dt_cmaddr.Rows
                rpt1.XrLabelTitle.Text = dr(0).ToString
            Next

            rpt1.XrLabelPeriode.Text = CDate(le_gcal.GetColumnValue("gcal_start_date")).ToString("MMM/yyyy") _
                & " - " & CDate(le_gcal2.GetColumnValue("gcal_start_date")).ToString("MMM/yyyy")

            Dim ps As New DevExpress.XtraPrinting.PrintingSystem()
            ps.PreviewFormEx.Text = "Dashboard Report"
            rpt1.PrintingSystem = ps
            rpt1.ShowPreview()


        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub

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

    Private Sub le_gcal2_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_gcal2.EditValueChanged
        Try
            de_first2.EditValue = le_gcal2.GetColumnValue("gcal_start_date")
            de_end2.EditValue = le_gcal2.GetColumnValue("gcal_end_date")

            'If le_gcal2.GetColumnValue("gcal_closing") = "Y" Then
            '    Ce_Posting2.EditValue = True
            'Else
            '    Ce_Posting2.EditValue = False
            'End If

        Catch ex As Exception
            Pesan(Err)
        End Try
    End Sub
End Class
