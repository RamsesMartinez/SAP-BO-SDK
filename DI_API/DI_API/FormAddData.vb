Public Class FormAddData

    ' *** FORM ***
    Private Sub FormAddData_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        comboUserTables.DropDownStyle = ComboBoxStyle.DropDownList
        UpdateUserTablesCombo()
        comboUserTables.SelectedIndex = 0
    End Sub

    ' *** FORM LISTENERS ***
    Private Sub FormAddData_FormClosed(sender As System.Object, e As System.EventArgs) Handles MyBase.FormClosed
        FormClientsTable.Show()
    End Sub


    ' *** LISTENERS ***
    Private Sub comboUserTables_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles comboUserTables.SelectedIndexChanged
        UpdateUserFieldsContainer()
    End Sub


    ' *** FUNCTIONS ***
    Private Sub AddLabel(ByVal lblName As String, ByVal item As Integer)
        Dim lblField As New Label()

        With lblField
            .Text = lblName
            .Name = "lbl" & lblName
            .Location = New System.Drawing.Point(15, (item + 1) * 30)
            .Size() = New System.Drawing.Size(110, 15)
        End With

        panelFields.Controls.Add(lblField)
    End Sub

    Private Sub addTxtBox(ByVal txtName As String, ByVal item As Integer)
        Dim txtField As New TextBox()

        With txtField
            .Name = "txt" & txtName
            .Location = New System.Drawing.Point(150, (item + 1) * 30)
            .Size() = New System.Drawing.Size(110, 20)
        End With

        panelFields.Controls.Add(txtField)
    End Sub

    Private Sub UpdateUserTablesCombo()
        Dim oUserTablesMD As SAPbobsCOM.UserTablesMD
        Dim oRecordSet As SAPbobsCOM.Recordset

        GC.Collect()

        Try

            oUserTablesMD = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables)
            oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecordSet.DoQuery("SELECT * FROM OUTB")

            oUserTablesMD.Browser.Recordset = oRecordSet

            If oUserTablesMD.TableName <> "" Then
                For i As Integer = 0 To oUserTablesMD.Browser.RecordCount - 1
                    comboUserTables.Items.Add(oUserTablesMD.TableName)
                    oUserTablesMD.Browser.MoveNext()
                Next
            Else
                MsgBox("Tablas de usuarios vacia.")
            End If

            ' // Clean Memory
            GC.Collect()
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserTablesMD)
            oUserTablesMD = Nothing
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordSet)
            oRecordSet = Nothing

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub ClearUserFieldsContainer()
        Dim con As Control

        For controlIndex As Integer = panelFields.Controls.Count - 1 To 0 Step -1
            con = panelFields.Controls(controlIndex)
            panelFields.Controls.Remove(con)
        Next
    End Sub

    Private Sub ClearUserFields()
        Dim con As Control

        For controlIndex As Integer = panelFields.Controls.Count - 1 To 0 Step -1
            con = panelFields.Controls(controlIndex)
            If TypeOf con Is TextBox Then
                con.Text = ""
            End If

        Next
    End Sub

    Private Sub UpdateUserFieldsContainer()
        Dim oUserTablesMD As SAPbobsCOM.UserTablesMD
        Dim oRecordset As SAPbobsCOM.Recordset
        Dim oUserFieldsMD As SAPbobsCOM.UserFieldsMD

        Dim sUserTable As String

        oUserTablesMD = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables)
        oRecordset = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oUserFieldsMD = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields)

        GC.Collect()

        sUserTable = comboUserTables.Text

        Try
            oRecordset.DoQuery("SELECT * FROM CUFD WHERE TableID='@" & sUserTable & "'")
            oUserFieldsMD.Browser.Recordset = oRecordset
            ClearUserFieldsContainer()

            If oUserFieldsMD.Name <> "" Then
                For i As Integer = 0 To oUserFieldsMD.Browser.RecordCount - 1
                    AddLabel(oUserFieldsMD.Name, i)
                    addTxtBox(oUserFieldsMD.Name, i)
                    oUserFieldsMD.Browser.MoveNext()
                Next
            End If
            
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserTablesMD)
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserFieldsMD)
        oUserTablesMD = Nothing
        oUserFieldsMD = Nothing
        GC.Collect()
    End Sub

    Private Function ValidUserDataFields() As Integer
        Dim countControls As Integer = panelFields.Controls.Count

        If panelFields.Controls.Count > 0 Then
            For i As Integer = 1 To panelFields.Controls.Count - 1 Step 2
                If panelFields.Controls.Item(i).Text = "" Then
                    MsgBox("Ingrese un valor para " & panelFields.Controls.Item(i - 1).Text)
                    Return 1
                End If
            Next
        End If
        Return 0

    End Function


    ' *** BUTTONS ***
    Private Sub btnExit_Click(sender As System.Object, e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        GC.Collect()

        Dim oUserTable As SAPbobsCOM.UserTable
        Dim oRecordSet As SAPbobsCOM.Recordset
        Dim sUserKey As String
        Dim bValidFields As Boolean = True


        Try
            oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            If panelFields.Controls.Count > 0 Then
                If ValidUserDataFields() = 0 Then
                    oUserTable = oCompany.UserTables.Item(comboUserTables.Text)

                    oRecordSet.DoQuery("SELECT MAX(CAST(Code AS INT)) AS INT FROM [@" & comboUserTables.Text & "]")

                    sUserKey = oRecordSet.Fields.Item(0).Value

                    If sUserKey = "" Then
                        oUserTable.Name = "1"
                    Else
                        oUserTable.Name = Convert.ToInt32(sUserKey) + 1
                    End If

                    ' // Mandatory fields
                    oUserTable.Code = oUserTable.Name

                    ' // Validate User Fields
                    For i As Integer = 1 To panelFields.Controls.Count - 1 Step 2
                        Dim sFieldName As String = panelFields.Controls.Item(i - 1).Text
                        Dim sFieldValue As String = panelFields.Controls.Item(i).Text
                        Dim iFieldSize As Integer = oUserTable.UserFields.Fields.Item("U_" & sFieldName).Size

                        If sFieldValue.Length > iFieldSize Then
                            MsgBox(sFieldName & " Debe tener menos de " & iFieldSize.ToString)
                            bValidFields = False
                            Exit For
                        End If

                    Next

                    ' // Save User Fields
                    If bValidFields Then
                        For i As Integer = 1 To panelFields.Controls.Count - 1 Step 2
                            Dim sFieldName As String = panelFields.Controls.Item(i - 1).Text
                            Dim sFieldValue As String = panelFields.Controls.Item(i).Text
                            oUserTable.UserFields.Fields.Item("U_" & sFieldName).Value = sFieldValue
                        Next

                        If oUserTable.Add() <> 0 Then
                            oCompany.GetLastError(iCompErrCode, sCompErrMsg)
                            MsgBox("Codigo: " + iCompErrCode.ToString + " Error: " + sCompErrMsg)
                        Else
                            MsgBox("Datos agregados Exitosamente")
                            ClearUserFields()
                        End If
                    End If

                    ' // Clean Memory
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserTable)
                    oUserTable = Nothing
                    GC.Collect()

                End If
            Else
                MsgBox("La tabla no tiene campos")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

        GC.Collect()

    End Sub
End Class