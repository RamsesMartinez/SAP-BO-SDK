Public Class FormConnection

    ' *** VALIDATIONS ***
    Private Function ValidateConnectionInputs(ByVal sUsername As String, ByVal sPassword As String, ByVal sServer As String) As Integer
        If sUsername = "" Then
            Return 1
        ElseIf sPassword = "" Then
            Return 2
        ElseIf sServer = "" Then
            Return 3
        End If
        Return 0
    End Function

    Private Function ValidateCompanyInputs(ByVal sUsername As String, ByVal sPassword As String) As Integer
        If sUsername = "" Then
            Return 1
        ElseIf sPassword = "" Then
            Return 2
        End If
        Return 0
    End Function


    ' *** LISTENERS ***
    Private Sub TxtServer_TextChanged(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TxtServer.KeyPress

        If oCompany IsNot Nothing Then

            If oCompany.Connected Then
                oCompany.Disconnect()
                MsgBox("Conexiones terminadas")
                ChangeTooltipStatus("Desconectado")
            End If


            oCompany = Nothing
            ClearCompanyInputs()
            ClearDBInputs()

            ActivateDBInputs()
            DeactivateCompanyInputs()

        End If
    End Sub



    ' *** FORM ***
    Private Sub FormConnection_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        comboCompanys.DropDownStyle = ComboBoxStyle.DropDownList
        TxtPassword.PasswordChar = "*"c
        TxtcompanyPassword.PasswordChar = "*"c

    End Sub


    ' *** FORM LISTENERS ***
    Private Sub FormConnection_FromClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim dlgRes As DialogResult

        Select Case e.CloseReason
            Case CloseReason.ApplicationExitCall
            Case CloseReason.FormOwnerClosing
            Case CloseReason.MdiFormClosing
            Case CloseReason.None
            Case CloseReason.TaskManagerClosing
            Case CloseReason.UserClosing

                If oCompany IsNot Nothing Then
                    If oCompany.Connected Then
                        dlgRes = MessageBox.Show("Aun hay conecciones activas. ¿Desea cerrarlas y cerrar?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                        If dlgRes = Windows.Forms.DialogResult.Yes Then
                            oCompany.Disconnect()
                            MsgBox("Conexiones terminadas")
                        Else
                            e.Cancel = True
                        End If
                    End If
                End If

            Case CloseReason.WindowsShutDown
        End Select
    End Sub


    ' *** ENABLED INPUTS ***
    Private Function DeactivateCompanyInputs() As Integer
        TxtCompanyUser.Enabled = False
        TxtcompanyPassword.Enabled = False
        comboCompanys.Enabled = False
        btnConnectCompany.Enabled = False
        btnDisconnectCompany.Enabled = False
        Return 0
    End Function ' DeactivateCompanyInputs()

    Private Sub ActivateCompanyInputs()
        TxtCompanyUser.Enabled = True
        TxtcompanyPassword.Enabled = True
        comboCompanys.Enabled = True
        btnConnectCompany.Enabled = True
        btnDisconnectCompany.Enabled = False
    End Sub ' DeactivateCompanyInputs()

    Private Sub DeactivateDBInputs()
        TxtUser.Enabled = False
        TxtPassword.Enabled = False
        'TxtServer.Enabled = False
        btnServer.Enabled = False
    End Sub ' DeactivateDBInputs

    Private Sub ActivateDBInputs()
        TxtUser.Enabled = True
        TxtPassword.Enabled = True
        ' TxtServer.Enabled = True
        btnServer.Enabled = True
    End Sub ' ActivateDBInputs


    ' *** CLEAR INPUTS ***
    Private Sub ClearDBInputs()
        TxtServer.Text = ""
        TxtUser.Text = ""
        TxtPassword.Text = ""
    End Sub

    Private Sub ClearCompanyInputs()
        TxtCompanyUser.Text = ""
        TxtcompanyPassword.Text = ""
        comboCompanys.Items.Clear()
    End Sub


    ' *** TOOLTIP STATUS ***
    Private Sub ChangeTooltipStatus(ByVal status As String)
        StatusStripConnection.Items.Item(0).Text = status
        StatusStripConnection.Update()
    End Sub


    ' *** ACTIONS ***
    Private Sub LoadCompaniesList(ByVal sUsername As String, ByVal sPassword As String, ByVal sServer As String)
        Dim oRecordSet As SAPbobsCOM.Recordset

        GC.Collect()
        Try
            oCompany = New SAPbobsCOM.Company
            oCompany.Server = sServer
            oCompany.language = SAPbobsCOM.BoSuppLangs.ln_Spanish
            oCompany.UseTrusted = False
            oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014

            '*** db Cponnection ***
            oCompany.DbUserName = sUsername
            oCompany.DbPassword = sPassword

            oRecordSet = oCompany.GetCompanyList

            oCompany.GetLastError(iCompErrCode, sCompErrMsg)

            If iCompErrCode <> 0 Then
                MsgBox(sCompErrMsg)
                MsgBox("Codigo: " + iCompErrCode.ToString + " Error: " + sCompErrMsg)
            Else
                comboCompanys.Items.Clear()
                Do Until oRecordSet.EoF = True
                    '// add the value of the first field of the Recordset
                    comboCompanys.Items.Add(oRecordSet.Fields.Item(0).Value)

                    '// move the record pointer to the next row
                    oRecordSet.MoveNext()
                Loop

                If comboCompanys.Items.Count <= 0 Then
                    comboCompanys.SelectedIndex = -1
                Else
                    comboCompanys.SelectedIndex = 32
                End If ' ComboCompanys.SelectedItem
            End If 'lErrcode
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        GC.Collect()
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordSet)
        oRecordSet = Nothing

    End Sub


    Private Sub BtnServer_Click(sender As System.Object, e As System.EventArgs) Handles btnServer.Click
        Dim sUsername, sPassword, sServer As String
        Dim iDBResult As Integer

        sUsername = TxtUser.Text
        sPassword = TxtPassword.Text
        sServer = TxtServer.Text

        iDBResult = ValidateConnectionInputs(sServer, sUsername, sPassword)

        If iDBResult = 0 Then
            ' // Active company inputs and Deactivate DB inputs
            DeactivateDBInputs()
            System.Threading.Thread.Sleep(300)
            Try
                LoadCompaniesList(sUsername, sPassword, sServer)
                ActivateCompanyInputs()

            Catch ex As Exception
                ActivateDBInputs()
                oCompany.GetLastError(iCompErrCode, sCompErrMsg)

                If iCompErrCode <> 0 Then
                    MsgBox(sCompErrMsg)
                End If
                oCompany = Nothing
            End Try
        ElseIf iDBResult = 1 Then
            MsgBox("Ingresa el nombre del servidor")
        ElseIf iDBResult = 2 Then
            MsgBox("Ingresa el usuario")
        ElseIf iDBResult = 3 Then
            MsgBox("Ingresa la contraseña")
        End If

    End Sub

    Private Sub BtnConnectCompany_Click(sender As System.Object, e As System.EventArgs) Handles btnConnectCompany.Click
        Dim sCompanyUsername, sCompanyPassword, sCompanyDB As String
        Dim iCompanyResult As Integer

        sCompanyUsername = TxtCompanyUser.Text
        sCompanyPassword = TxtcompanyPassword.Text
        sCompanyDB = comboCompanys.Text

        iCompanyResult = ValidateCompanyInputs(sCompanyUsername, sCompanyPassword)

        If iCompanyResult = 0 Then
            oCompany.UserName = sCompanyUsername
            oCompany.Password = sCompanyPassword
            oCompany.CompanyDB = sCompanyDB
            Try
                DeactivateCompanyInputs()
                oCompany.Connect()

            Catch ex As Exception
                ActivateCompanyInputs()
                MsgBox(ex.Message.ToString)
            End Try

            oCompany.GetLastError(iCompErrCode, sCompErrMsg)

            If iCompErrCode <> 0 Then
                MsgBox(sCompErrMsg)
                ActivateCompanyInputs()
            Else
                btnDisconnectCompany.Enabled = True
                btnNewOrder.Enabled = True
                btnViewClients.Enabled = True
                btnUsersTable.Enabled = True

                ChangeTooltipStatus("Conectado")
                btnNewOrder.Select()
            End If

        ElseIf iCompanyResult = 1 Then
            MsgBox("Ingresa el usuario")

        ElseIf iCompanyResult = 2 Then
            MsgBox("Ingresa la contraseña")
        End If

    End Sub

    Private Sub BtnDisconnectCompany_Click(sender As System.Object, e As System.EventArgs) Handles btnDisconnectCompany.Click
        If oCompany.Connected Then
            oCompany.Disconnect()
        End If

        ActivateDBInputs()
        DeactivateCompanyInputs()
        ClearDBInputs()
        ClearCompanyInputs()

        ChangeTooltipStatus("Desconectado")
        MsgBox("Desconectado!")

        btnNewOrder.Enabled = False
        btnUsersTable.Enabled = False
        btnViewClients.Enabled = False
    End Sub

    Private Sub BtnExit_Click(sender As System.Object, e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub


    Private Sub BtnNewOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewOrder.Click
        FormOrders.Show()
        Me.Hide()

    End Sub

    Private Sub btnViewClients_Click(sender As System.Object, e As System.EventArgs) Handles btnViewClients.Click
        FormViewClients.Show()
        Me.Hide()
    End Sub

    Private Sub btnUsersTable_Click(sender As System.Object, e As System.EventArgs) Handles btnUsersTable.Click
        FormClientsTable.Show()
        Me.Hide()
    End Sub

End Class
