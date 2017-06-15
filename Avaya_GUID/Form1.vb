﻿Imports System.IO

Public Class Form1
    Dim DT2 As New DataTable


    Private Sub test3()
        Dim tb1 As TextBox = TextBox1
        Dim DS As New DataSet()
        Dim fs As FileStream
        Dim sr As StreamReader
        Dim DR As DataRow
        Dim DT As DataTable

        tb1.Clear()

        fs = New FileStream("c:\GUID.xml", FileMode.Open, FileAccess.Read) ' --- Datei öffnen
        sr = New StreamReader(fs) ' --- Datei einlesen
        DS.ReadXml(sr) ' --- Stream in DataSet einlesen

        DT = DS.Tables("Extension")

        Dim column As New DataColumn
        Dim row As DataRow

        column = New DataColumn
        column.DataType = System.Type.GetType("System.Boolean")
        column.ColumnName = "Use"
        DT2.Columns.Add(column)

        column = New DataColumn
        column.DataType = System.Type.GetType("System.Int16")
        column.ColumnName = "ExtNo"
        DT2.Columns.Add(column)

        column = New DataColumn
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = "Guid"
        DT2.Columns.Add(column)

        With DT
            For Each DR In .Rows
                row = DT2.NewRow()
                row("Use") = True
                row("ExtNo") = DR.Item((DT.Columns.IndexOf("ExtNo")))
                row("Guid") = DR.Item((DT.Columns.IndexOf("GUID")))
                DT2.Rows.Add(row)
            Next
        End With

        DT2.DefaultView.Sort = "ExtNo ASC"
        DT2 = DT2.DefaultView.ToTable

        Dim dgv1 = DataGridView1
        dgv1.DataSource = DT2

        Button3.Enabled = True
    End Sub

    Private Sub Ausgabe_Tabelle2()
        'DT2.AcceptChanges()

        Dim dgv2 = DataGridView2
        Dim DT As New DataTable
        Dim column As New DataColumn
        Dim row As DataRow

        column = New DataColumn
        column.DataType = System.Type.GetType("System.Int16")
        column.ColumnName = "ExtNo"
        DT.Columns.Add(column)

        column = New DataColumn
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = "Guid"
        DT.Columns.Add(column)

        For Each DR As DataRow In DT2.Rows
            If CBool(DR("Use")) Then
                row = DT.NewRow()
                row("ExtNo") = DR.Item((DT2.Columns.IndexOf("ExtNo")))
                row("Guid") = DR.Item((DT2.Columns.IndexOf("GUID")))
                DT.Rows.Add(row)
            End If
        Next
        dgv2.DataSource = DT
        TextBox1.Clear()


        TextBox1.AppendText("[IP_OFFICE]" & vbCrLf)
        TextBox1.AppendText("host = localhost" & vbCrLf)
        TextBox1.AppendText("port = 8085" & vbCrLf)
        TextBox1.AppendText("[USERS]" & vbCrLf)

        For Each DR As DataRow In DT2.Rows
            If CBool(DR("Use")) = True Then
                TextBox1.AppendText(DR.Item("ExtNo").ToString & "=" & DR.Item("GUID").ToString & vbCrLf)
            End If
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        test3()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Ausgabe_Tabelle2()
    End Sub

    Private Sub DataGridView1_MouseUp(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseUp
        'DT2.AcceptChanges()
        DataGridView1.DataSource = DT2
        Ausgabe_Tabelle2()
    End Sub

    Private Sub DataGridView1_MouseDown(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseDown
        Ausgabe_Tabelle2()
    End Sub


End Class
