Public Class FormClientsTable

    ' *** FORM ***
    Private Sub FormClientsTable_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        comboUserTablesMD.DropDownStyle = ComboBoxStyle.DropDownList
        UpdateUsersTablesCombo()
    End Sub

    ' *** FORM LISTENERS ***
    Private Sub FormClientsTable_FormClosed(sender As System.Object, e As System.EventArgs) Handles MyBase.FormClosed
        FormConnection.Show()
    End Sub



    ' *** VALIDATIONS ***
    Private Function ValidUserTableInputs(ByVal sName As String, ByVal sDescription As String, ByRef sErrorMsg As String) As Integer
        If sName = "" Then
            sErrorMsg = "Ingrese un nombre para la tabla de usuario"
            Return 1
        ElseIf sDescription = "" Then
            sErrorMsg = "Ingrese la descripción de la tabla de usuario"
            Return 2
        End If
        Return 0
    End Function

    Private Function ValidUserFieldInputs(ByVal sName As String, ByVal sDescription As String, ByRef sErrorMsg As String) As Integer
        If sName = "" Then
            sErrorMsg = "Ingrese un nombre para el nuevo campo."
            Return 1
        ElseIf sDescription = "" Then
            sErrorMsg = "Ingrese la descripción del nuevo campo."
            Return 2
        End If
        Return 0
    End Function

    ' *** LISTENER VALIDATION ***
    Private Sub txtValidator_KeyPressed(sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTableName.KeyPress, txtFieldName.KeyPress
        Dim ch As Char = e.KeyChar
        If Char.IsLetter(ch) Then
            e.Handled = False
        ElseIf Char.IsDigit(ch) Then
            e.Handled = False
        ElseIf ch = Chr(8) Or ch = Chr(95) Then
            e.Handled = False
        Else
            e.Handled = True
        End If

        If sender.Text = "" Then
            If Char.IsDigit(ch) Then
                e.Handled = True
            End If
        End If

    End Sub



    ' *** ACTIONS ***
    Private Sub CreateUserTable(ByVal sTableName As String, ByVal sTableDescription As String)
        Dim oUserTablesMD As SAPbobsCOM.UserTablesMD
        Dim iRetCode As Integer

        GC.Collect()

        Try
            ' // Create instance of UserTablesMD class  
            oUserTablesMD = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables)

            ' // Check whether table already exists    
            If Not oUserTablesMD.GetByKey(sTableName) Then

                oUserTablesMD.TableName = sTableName
                oUserTablesMD.TableDescription = sTableDescription
                iRetCode = oUserTablesMD.Add()

                If iRetCode <> 0 Then
                    oCompany.GetLastError(iCompErrCode, sCompErrMsg)
                    MsgBox(sCompErrMsg)
                Else
                    MsgBox("La tabla: " & oUserTablesMD.TableName & " se agregó correctamente.")
                    txtTableName.Text = ""
                    txtTableDescription.Text = ""
                    UpdateUsersTablesCombo()
                End If

            Else
                MsgBox("Ya existe una tabla con el mismo nombre")
            End If

            ' // Clean Memory
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserTablesMD)
            oUserTablesMD = Nothing
            GC.Collect()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub CreateUserField(ByVal sFieldName As String, ByVal sFieldDescription As String)
        Dim oUserFieldsMD As SAPbobsCOM.UserFieldsMD
        Dim oRecordSet As SAPbobsCOM.Recordset
        Dim iRetCode As Integer

        GC.Collect()

        Try
            oUserFieldsMD = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields)
            oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            oRecordSet.DoQuery("SELECT * FROM CUFD " & vbCrLf & _
                               "WHERE TableID='@" & comboUserTablesMD.Text & "'" & vbCrLf & _
                               "AND AliasID='" & txtFieldName.Text & "'"
                               )

            ' // Check whether table is empty
            If oRecordSet.Fields.Item("AliasID").Value = "" Then

                ' Add User Fields
                oUserFieldsMD.TableName = comboUserTablesMD.Text
                oUserFieldsMD.Name = txtFieldName.Text
                oUserFieldsMD.Description = txtFieldDescription.Text
                oUserFieldsMD.Type = SAPbobsCOM.BoFieldTypes.db_Alpha
                oUserFieldsMD.EditSize = 120

                GC.Collect()

                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordSet)
                oRecordSet = Nothing

                iRetCode = oUserFieldsMD.Add()

                If iRetCode <> 0 Then
                    oCompany.GetLastError(iCompErrCode, sCompErrMsg)
                    MsgBox(iCompErrCode.ToString & ": " & sCompErrMsg)

                Else
                    MsgBox("El campo: " & oUserFieldsMD.Name & " se agregó correctamente.")
                    txtFieldName.Text = ""
                    txtFieldDescription.Text = ""
                End If

            Else
                MsgBox("Ya existe un campo con el mismo nombre")
            End If

            ' // Clean Memory
            GC.Collect()
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserFieldsMD)
            oUserFieldsMD = Nothing

        Catch ex As Exception
            MsgBox(ex.Message)
            GC.Collect()
        End Try

    End Sub

    Private Sub UpdateUsersTablesCombo()
        Dim oUserTablesMD As SAPbobsCOM.UserTablesMD
        Dim oRecordSet As SAPbobsCOM.Recordset

        GC.Collect()

        oUserTablesMD = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserTables)
        oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRecordSet.DoQuery("SELECT * FROM OUTB")

        oUserTablesMD.Browser.Recordset = oRecordSet


        ' // Clear previous data from combo box
        comboUserTablesMD.Items.Clear()

        If oUserTablesMD.TableName <> "" Then
            For i As Integer = 0 To oUserTablesMD.Browser.RecordCount - 1
                comboUserTablesMD.Items.Add(oUserTablesMD.TableName)
                oUserTablesMD.Browser.MoveNext()
            Next
            comboUserTablesMD.SelectedIndex = 0
        Else
            MsgBox("Tablas de usuarios vacia.")
        End If


        System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserTablesMD)
        oUserTablesMD = Nothing

        GC.Collect()
    End Sub


    ' *** BUTTONS ***
    Private Sub btnAddUser_Click(sender As System.Object, e As System.EventArgs) Handles btnAddUser.Click
        Dim iRetTableVal As Integer
        Dim sTableName, sTableDescription As String

        sTableName = txtTableName.Text
        sTableDescription = txtTableDescription.Text

        ' // Get the code for evaluate valid table fields
        iRetTableVal = ValidUserTableInputs(sTableName, sTableDescription, sErrorMsg)

        ' // Valid table fields
        If Not iRetTableVal <> 0 Then
            CreateUserTable(sTableName, sTableDescription)
        Else
            MsgBox(sErrorMsg)
        End If


    End Sub

    Private Sub btnAddField_Click(sender As System.Object, e As System.EventArgs) Handles btnAddField.Click
        Dim iRetTableVal As Integer
        Dim sFieldName, sFieldDescription As String

        sFieldName = txtFieldName.Text
        sFieldDescription = txtFieldDescription.Text

        ' // Get the code for evaluate valid user fields
        iRetTableVal = ValidUserFieldInputs(sFieldName, sFieldDescription, sErrorMsg)

        ' // Valid user fields
        If Not iRetTableVal <> 0 Then
            CreateUserField(sFieldName, sFieldDescription)
        Else
            MsgBox(sErrorMsg)
        End If

    End Sub

    Private Sub btnAddData_Click(sender As System.Object, e As System.EventArgs) Handles btnAddData.Click
        FormAddData.Show()
        Me.Hide()
    End Sub

    Private Sub btnExit_Click(sender As System.Object, e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

End Class