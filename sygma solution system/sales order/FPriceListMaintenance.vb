Imports CoreLab.PostgreSql
Imports master_new.ModFunction
Imports master_new.PGSqlConn


Public Class FPriceListMaintenance
    Public dt_bantu As DataTable
    Public func_data As New function_data
    Public func_coll As New function_collection
    Dim _pi_oid_mstr As String
    Dim _now As DateTime
    Dim load_awal As Boolean = True

    Private Sub FPriceListHeader_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        form_first_load()
        _now = func_coll.get_now

    End Sub

    Public Overrides Sub load_cb()
        dt_bantu = New DataTable
        dt_bantu = (func_data.load_en_mstr_mstr())
        le_en_id.Properties.DataSource = dt_bantu
        le_en_id.Properties.DisplayMember = dt_bantu.Columns("en_desc").ToString
        le_en_id.Properties.ValueMember = dt_bantu.Columns("en_id").ToString
        le_en_id.ItemIndex = 0

        dt_bantu = New DataTable
        dt_bantu = (func_data.load_pay_type_all(le_en_id.EditValue))
        payment_type_id.Properties.DataSource = dt_bantu
        payment_type_id.Properties.DisplayMember = dt_bantu.Columns("code_name").ToString
        payment_type_id.Properties.ValueMember = dt_bantu.Columns("code_id").ToString
        payment_type_id.ItemIndex = 0

    End Sub

    Public Overrides Sub load_cb_en()
        Try
            dt_bantu = New DataTable
            dt_bantu = (func_data.load_pi_mstr(le_en_id.EditValue))
            pl_id.Properties.DataSource = dt_bantu
            pl_id.Properties.DisplayMember = dt_bantu.Columns("pi_desc").ToString
            pl_id.Properties.ValueMember = dt_bantu.Columns("pi_id").ToString
            pl_id.ItemIndex = 0
        Catch ex As Exception

        End Try
       

    End Sub

    Private Sub pi_en_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles le_en_id.EditValueChanged
        load_cb_en()
    End Sub

    Public Overrides Sub format_grid()

        add_column_copy(gv_master, "Part Number Code", "pt_code", DevExpress.Utils.HorzAlignment.Default)
        add_column_copy(gv_master, "Description", "pt_desc1", DevExpress.Utils.HorzAlignment.Default)
        add_column_edit(gv_master, "Price", "pidd_price", DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.FormatType.Numeric, "n4")
        load_awal = False
    End Sub

    Public Overrides Function get_sequel() As String
       

        get_sequel = "SELECT  " _
                & "  c.pidd_oid, " _
                & "  b.pid_pt_id, " _
                & "  d.pt_code, " _
                & "  d.pt_desc1, " _
                & "  c.pidd_price " _
                & "FROM " _
                & "  public.pidd_det c " _
                & "  INNER JOIN public.pid_det b ON (c.pidd_pid_oid = b.pid_oid) " _
                & "  INNER JOIN public.pi_mstr a ON (b.pid_pi_oid = a.pi_oid) " _
                & "  INNER JOIN public.pt_mstr d ON (b.pid_pt_id = d.pt_id) " _
                & "WHERE " _
                & "  a.pi_id = " & SetInteger(pl_id.EditValue) & " AND  " _
                & "  c.pidd_payment_type = " & SetInteger(payment_type_id.EditValue) & "  AND  " _
                & "  a.pi_active = 'Y' order by pt_desc1"

        Return get_sequel
    End Function

    Private Sub payment_type_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles payment_type_id.EditValueChanged
        If load_awal = False Then
            help_load_data(True)
        End If



    End Sub

    Private Sub pl_id_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles pl_id.EditValueChanged
        If load_awal = False Then
            help_load_data(True)
        End If
    End Sub

    Private Sub BtSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtSave.Click
        Try
            If MessageBox.Show("Are you sure to save?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If
            Dim ssqls As New ArrayList
            gc_master.EmbeddedNavigator.Buttons.DoClick(gc_master.EmbeddedNavigator.Buttons.EndEdit)
            ds.Tables(0).AcceptChanges()

            Dim sSQL As String = ""
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                sSQL = "UPDATE pidd_det set pidd_price=" & SetDec(ds.Tables(0).Rows(i).Item("pidd_price")) & " where pidd_oid=" & SetSetring(ds.Tables(0).Rows(i).Item("pidd_oid"))

                ssqls.Add(sSQL)
            Next

            If master_new.PGSqlConn.status_sync = True Then
                'Dim _data As String = arr_to_str(ssqls)
                If DbRunTran(ssqls, "", master_new.ModFunction.FinsertSQL2Array(ssqls), "") = False Then

                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then

                    Exit Sub
                End If
                ssqls.Clear()
            End If

            help_load_data(True)
            MessageBox.Show("Success", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information)


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DeleteItemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteItemToolStripMenuItem.Click
        Try
            If MessageBox.Show("Are you sure to delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Exit Sub
            End If
            Dim ssqls As New ArrayList
            gc_master.EmbeddedNavigator.Buttons.DoClick(gc_master.EmbeddedNavigator.Buttons.EndEdit)
            ds.Tables(0).AcceptChanges()

            Dim sSQL As String = ""
            'For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            sSQL = "Delete from pidd_det  where pidd_oid=" & SetSetring(ds.Tables(0).Rows(BindingContext(ds.Tables(0)).Position).Item("pidd_oid"))

            ssqls.Add(sSQL)
            'Next

            If master_new.PGSqlConn.status_sync = True Then
                'Dim _data As String = arr_to_str(ssqls)
                If DbRunTran(ssqls, "", master_new.ModFunction.FinsertSQL2Array(ssqls), "") = False Then

                    Exit Sub
                End If
                ssqls.Clear()
            Else
                If DbRunTran(ssqls, "") = False Then

                    Exit Sub
                End If
                ssqls.Clear()
            End If

            help_load_data(True)
            MessageBox.Show("Success", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
