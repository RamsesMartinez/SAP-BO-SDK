Imports System.Windows.Forms

Public Class SimpleUDO
    Private WithEvents oApplication As SAPbouiCOM.Application
    Private oCompany As SAPbobsCOM.Company
    Private oSboGuiApi As SAPbouiCOM.SboGuiApi
    Private oFilters As SAPbouiCOM.EventFilters
    Private oFilter As SAPbouiCOM.EventFilter

    Private oDBDataSource As SAPbouiCOM.DBDataSource
    Private oUserDataSource As SAPbouiCOM.UserDataSource
    Private countIDForm As Integer = 0


    Private Sub StartApp()
        Dim sConnectionString As String

        Try
            ' // Set the AddOn identifier
            sConnectionString = "0030002C0030002C00530041005000420044005F00440061007400650076002C0050004C006F006D0056004900490056"

            oSboGuiApi = New SAPbouiCOM.SboGuiApi

            ' // Connect to the SAP Business One application
            oSboGuiApi.Connect(sConnectionString)

            ' // Get an initialized application object
            oApplication = oSboGuiApi.GetApplication()

            ' // Set The connection context 
            SetConnectionContext()

            ' // Connect to Company Data Base
            If Not ConnectToCompany() = 0 Then
                oApplication.SetStatusBarMessage("Falló la conexión a la base de datos", SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                End ' Terminating the Add-On Application
            End If

            oApplication.SetStatusBarMessage("DI API conectada a: " & oCompany.CompanyName, SAPbouiCOM.BoMessageTime.bmt_Short, False)

        Catch ex As Exception
            MsgBox(ex.Message)
            End
        End Try

    End Sub

    Private Function SetConnectionContext() As Integer

        Dim sCookie As String
        Dim sConnectionContext As String

        oCompany = New SAPbobsCOM.Company

        '// Acquire the connection context cookie from the DI API.
        sCookie = oCompany.GetContextCookie
        sConnectionContext = oApplication.Company.GetConnectionContext(sCookie)

        '// Before setting the SBO Login Context make sure the company is not connected
        If oCompany.Connected = True Then
            oCompany.Disconnect()
        End If

        '// Set the connection context information to the DI API.
        SetConnectionContext = oCompany.SetSboLoginContext(sConnectionContext)

    End Function

    Private Function ConnectToCompany() As Integer

        '// Establish the connection to the company database.
        ConnectToCompany = oCompany.Connect

    End Function



    ' *****************************
    ' ********* FILTERS  **********
    ' *****************************

    Private Sub SetFilters()
        ' // Set EventFilters Object
        Dim oFilters As SAPbouiCOM.EventFilters = New SAPbouiCOM.EventFilters

        Try

            ' // Add the Others Event Types to the Container
            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_MENU_CLICK)
            oFilter.AddEx("SampleFormUDO")

            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED)
            oFilter.AddEx("SampleFormUDO")

            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_LOST_FOCUS)
            oFilter.AddEx("SampleFormUDO")

            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_VALIDATE)
            oFilter.AddEx("SampleFormUDO")
            
            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_LOAD)
            oFilter.AddEx("SampleFormUDO")

            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_ACTIVATE)
            oFilter.AddEx("SampleFormUDO")

            oApplication.SetFilter(oFilters)

        Catch ex As Exception
            oApplication.SetStatusBarMessage("Error SetFilter: " & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)

        End Try

    End Sub

    Private Sub oApplication_MenuEvent(ByRef pVal As SAPbouiCOM.MenuEvent, ByRef BubbleEvent As Boolean) Handles oApplication.MenuEvent
        Dim oForm As SAPbouiCOM.Form
        Dim oItem As SAPbouiCOM.Item

        If pVal.BeforeAction Then
            oForm = oApplication.Forms.ActiveForm
            Select Case pVal.MenuUID

                Case "SubMenuUDO"
                    ' // Create the UDO Form or return False
                    If Not CreateSampleForm("SampleUDOForm.srf") Then
                        BubbleEvent = False

                    End If

                Case "1281"  ' // Search Mode
                    If oForm.TypeEx = "SampleFormUDO" Then
                        ' // Enable Navigation Items
                        oForm.EnableMenu("1288", True)
                        oForm.EnableMenu("1289", True)
                        oForm.EnableMenu("1290", True)
                        oForm.EnableMenu("1291", True)

                    End If

                Case "1282"  ' // Add Mode
                    If oForm.TypeEx = "SampleFormUDO" Then
                        ' // Disable Navigation Items
                        oForm.EnableMenu("1288", False)
                        oForm.EnableMenu("1289", False)
                        oForm.EnableMenu("1290", False)
                        oForm.EnableMenu("1291", False)


                    End If
                    
                Case "1288"  ' // Move NEXT
                    DeactivateItems(oForm)

                    If oForm.TypeEx = "SampleFormUDO" Then
                        oItem = oForm.Items.Item("btnAddRow")
                        oItem.Enabled = True
                    End If


                Case "1289"  ' // LAST
                    DeactivateItems(oForm)
                    If oForm.TypeEx = "SampleFormUDO" Then
                        oItem = oForm.Items.Item("btnAddRow")
                        oItem.Enabled = True
                    End If

                Case "1290"  ' // Move BEGIN
                    DeactivateItems(oForm)
                    If oForm.TypeEx = "SampleFormUDO" Then
                        oItem = oForm.Items.Item("btnAddRow")
                        oItem.Enabled = True
                    End If

                Case "1291"  ' // Move END
                    DeactivateItems(oForm)
                    If oForm.TypeEx = "SampleFormUDO" Then
                        oItem = oForm.Items.Item("btnAddRow")
                        oItem.Enabled = True
                    End If

            End Select

        Else
            oForm = oApplication.Forms.ActiveForm

            Select Case pVal.MenuUID
                Case "SubMenuUDO"

                    If oForm.TypeEx = "SampleFormUDO" Then
                        oItem = oForm.Items.Item("btnAddRow")
                        oItem.Enabled = True
                    End If

                Case "1281"  ' // Search Mode
                    
                    ' // UDO - Search Mode
                    If oForm.TypeEx = "SampleFormUDO" Then
                        oItem = oForm.Items.Item("txtCode")
                        oItem.Enabled = True

                        oItem = oForm.Items.Item("txtName")
                        oItem.Enabled = True

                        ' // Deactive btn to Add Row to Matrix
                        oItem = oForm.Items.Item("btnAddRow")
                        oItem.Enabled = True

                    End If

                Case "1282"  ' // Add Mode
                    ' // UDO - Create Mode
                    If oForm.TypeEx = "SampleFormUDO" Then
                        oItem = oForm.Items.Item("txtCode")
                        oItem.Enabled = False

                        oItem = oForm.Items.Item("txtName")
                        oItem.Enabled = False

                        SetNewUDOCode(oForm)

                        ' // Active Btn to Add Row to matrix
                        oItem = oForm.Items.Item("btnAddRow")
                        oItem.Enabled = True

                    End If

            End Select
        End If

    End Sub


    Private Sub oApplication_ItemEvent(ByVal FormUID As String, ByRef pVal As SAPbouiCOM.ItemEvent, ByRef BubbleEvent As Boolean) Handles oApplication.ItemEvent
        Dim oForm As SAPbouiCOM.Form

        Try

            If pVal.BeforeAction Then

                If pVal.FormTypeEx = "SampleFormUDO" Then

                    Select Case pVal.EventType
                        Case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED

                            If pVal.ItemUID = "1" Then
                                oForm = oApplication.Forms.Item(FormUID)

                                If oForm.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then

                                    Dim lMaxUdoCode As Integer
                                    Dim oEditText As SAPbouiCOM.EditText

                                    oForm = oApplication.Forms.Item(FormUID)
                                    ' // Validate Max Code in DB
                                    oEditText = oForm.Items.Item("txtCode").Specific

                                    If ExistsUDOID(oEditText.Value) Then
                                        lMaxUdoCode = GetMaxUdoCode()
                                        lMaxUdoCode += 1

                                        ' // Set New Value To Code, Name, and Index

                                        ' // Code
                                        oEditText.String = lMaxUdoCode.ToString

                                        ' // Index
                                        oEditText = oForm.Items.Item("txtUDOID").Specific
                                        oEditText.String = lMaxUdoCode.ToString

                                        ' // Name
                                        oEditText = oForm.Items.Item("txtName").Specific
                                        oEditText.String = lMaxUdoCode.ToString

                                    End If
                                End If



                            End If
                        Case SAPbouiCOM.BoEventTypes.et_VALIDATE

                            If pVal.ItemUID = "txtCode" Then
                                Dim oEditText As SAPbouiCOM.EditText
                                Dim sCode As String

                                ' // Copy txtCode in txtUDOID
                                oForm = oApplication.Forms.Item(FormUID)

                                oEditText = oForm.Items.Item("txtCode").Specific
                                sCode = oEditText.String

                                oEditText = oForm.Items.Item("txtUDOID").Specific
                                oEditText.String = sCode

                            End If

                    End Select

                End If  ' pVal.FormType


            ElseIf Not pVal.BeforeAction Then

                If pVal.FormTypeEx = "SampleFormUDO" Then

                    Select Case pVal.EventType

                        Case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED
                            oForm = oApplication.Forms.Item(FormUID)

                            If pVal.ItemUID = "1" Then

                                ' // UDO CREATED
                                If oForm.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then
                                    SetNewUDOCode(oForm)

                                ElseIf oForm.Mode = SAPbouiCOM.BoFormMode.fm_VIEW_MODE Then
                                    ' // Deactivate edit texts
                                    DeactivateItems(oForm)

                                ElseIf oForm.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then
                                    ' // Deactivate edit texts
                                    DeactivateItems(oForm)

                                    ' // Deactivate Navigation Menu
                                    oForm.EnableMenu("1288", False)
                                    oForm.EnableMenu("1289", False)
                                    oForm.EnableMenu("1290", False)
                                    oForm.EnableMenu("1291", False)
                                End If

                            End If

                        Case SAPbouiCOM.BoEventTypes.et_FORM_ACTIVATE
                            oForm = oApplication.Forms.Item(FormUID)

                            If oForm.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then
                                ' // Deactivate edit texts
                                DeactivateItems(oForm)
                            End If

                            ' // Checks Form Mode Status

                        Case SAPbouiCOM.BoEventTypes.et_FORM_LOAD
                            oForm = oApplication.Forms.Item(FormUID)
                            ' // Deactivate Navigation Menu
                            oForm.EnableMenu("1288", False)
                            oForm.EnableMenu("1289", False)
                            oForm.EnableMenu("1290", False)
                            oForm.EnableMenu("1291", False)
                    End Select

                End If  ' pVal.FormType

            End If  ' pVal.Before Action

        Catch ex As Exception
            oApplication.SetStatusBarMessage("Error Item Event: " & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)

        End Try

    End Sub



    ' *****************************
    ' ******** MENU ITEMS  ****+***
    ' *****************************

    Private Sub SetMenuItems()
        Dim oMenus As SAPbouiCOM.Menus
        Dim oMenuItem As SAPbouiCOM.MenuItem
        Dim oCreationPackage As SAPbouiCOM.MenuCreationParams

        Try
            oMenuItem = oApplication.Menus.Item("43520")
            oMenus = oMenuItem.SubMenus

            oCreationPackage = oApplication.CreateObject(
                SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)


            '// Menu UDO
            ' ***********************
            oMenuItem = oApplication.Menus.Item("43520")
            oMenus = oMenuItem.SubMenus

            ' // Set New Menu Item values into the MenuCreationPackage Object
            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP
            oCreationPackage.UniqueID = "UDOMenu"
            oCreationPackage.String = "Ejemplo UDO"
            oCreationPackage.Enabled = True
            oCreationPackage.Position = 12

            ' // Add the new Menu
            If oApplication.Menus.Exists("UDOMenu") Then
                oApplication.Menus.RemoveEx("UDOMenu")
            End If

            oMenus.AddEx(oCreationPackage)

            ' // Sets config to New SubMenu Item
            oMenuItem = oApplication.Menus.Item("UDOMenu")
            oMenus = oMenuItem.SubMenus
            oCreationPackage.UniqueID = "SubMenuUDO"
            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING
            oCreationPackage.String = "Ventana UDO"
            oCreationPackage.Enabled = True

            ' // Add the New Sum Menu Item
            oMenus.AddEx(oCreationPackage)

        Catch ex As Exception
            oApplication.SetStatusBarMessage("Error SetMenuItems: " & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)

        End Try
    End Sub



    ' *****************************
    ' ******** FUNCTIONS **********
    ' *****************************

    Private Function CreateSampleForm(ByVal FileName As String) As Boolean
        Dim oCreationParams As SAPbouiCOM.FormCreationParams
        Dim oXmlDoc As Xml.XmlDocument
        Dim sPath As String
        Dim oForm As SAPbouiCOM.Form

        Try

            ' // Creating the New form
            ' *************************

            ' // Create the FormCreationParams Object
            oCreationParams = oApplication.CreateObject(
                SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams)

            ' // Specify the parameters in the object
            countIDForm += 1
            oCreationParams.UniqueID = countIDForm.ToString
            oCreationParams.FormType = "SampleFormUDO"
            oCreationParams.BorderStyle = SAPbouiCOM.BoFormBorderStyle.fbs_Fixed
            oCreationParams.ObjectType = "UDO1"

            oXmlDoc = New Xml.XmlDocument

            '// load the content of the XML File
            sPath = IO.Directory.GetParent(IO.Directory.GetParent(Application.StartupPath).ToString).ToString

            oXmlDoc.Load(sPath & "\XMLForms\" & FileName)

            '// load the form to the SBO application in one batch
            'oApplication.LoadBatchActions(oXmlDoc.InnerXml)

            oCreationParams.XmlData = oXmlDoc.InnerXml

            oForm = oApplication.Forms.AddEx(oCreationParams)


            '' // Set data Source to form
            '' **********************************
            SetDataSourceToForm(oForm)


            '// Bind the Form's items with the desired data source
            BindDataToForm(oForm)


            ' // Obtains all the form data
            'GetMatrixDataFromDataSource(oForm)

            ' // Set the Form behaviour

            oForm.DataBrowser.BrowseBy = "txtUDOID"
            oForm.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE

            SetNewUDOCode(oForm)

        Catch ex As Exception
            oApplication.SetStatusBarMessage("Error CreateSampleFormSRF" & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)
            Return False

        End Try

        Return True

    End Function

    Private Sub SetDataSourceToForm(ByRef oForm As SAPbouiCOM.Form)
        oForm.DataSources.DBDataSources.Add("@ENCABEZADO")
        oForm.DataSources.DBDataSources.Add("@DETALLE")

    End Sub

    Private Sub BindDataToForm(ByRef oForm As SAPbouiCOM.Form)
        Dim oColumn As SAPbouiCOM.Column
        Dim oColumns As SAPbouiCOM.Columns
        Dim oMatrix As SAPbouiCOM.Matrix
        Dim oItem As SAPbouiCOM.Item
        Dim oEditText As SAPbouiCOM.EditText

        Try
            ' // Bind Data to EditTexts
            ' ************************
            oItem = oForm.Items.Item("txtUDOID")
            oEditText = oItem.Specific
            oEditText.DataBind.SetBound(True, "@ENCABEZADO", "U_INDEX")

            '// txtCode
            oItem = oForm.Items.Item("txtCode")
            oEditText = oItem.Specific
            oEditText.DataBind.SetBound(True, "@ENCABEZADO", "Code")

            '// txtName
            oItem = oForm.Items.Item("txtName")
            oEditText = oItem.Specific
            oEditText.DataBind.SetBound(True, "@ENCABEZADO", "Name")

            '// txtField1
            oItem = oForm.Items.Item("txtField1")
            oEditText = oItem.Specific
            oEditText.DataBind.SetBound(True, "@ENCABEZADO", "U_CAMPO1")



            ' // Bind Data to Matrix
            ' ************************

            oMatrix = oForm.Items.Item("matrixUDO").Specific

            oColumns = oMatrix.Columns

            ' // Code
            oColumn = oColumns.Item(1)
            oColumn.DataBind.SetBound(True, "@DETALLE", "Code")

            ' // LineID
            oColumn = oColumns.Item(2)
            oColumn.DataBind.SetBound(True, "@DETALLE", "LineID")

            ' // CAMPO_1
            oColumn = oColumns.Item(3)
            oColumn.DataBind.SetBound(True, "@DETALLE", "U_CAMPO1")


        Catch ex As Exception
            oApplication.SetStatusBarMessage("Error BindDataToForm: " & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)

        End Try
    End Sub

    Private Sub GetMatrixDataFromDataSource(ByRef oForm As SAPbouiCOM.Form)
        Dim oMatrix As SAPbouiCOM.Matrix

        Try
            oMatrix = oForm.Items.Item("matrixUDO").Specific
            oMatrix.Clear()
            oMatrix.AutoResizeColumns()
            oDBDataSource.Query()
            oMatrix.LoadFromDataSource()


        Catch ex As Exception
            oApplication.SetStatusBarMessage("Error GetMatrixDataDS: " & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)

        End Try
    End Sub

    Private Function ExistForm(ByVal sFormID As String) As Boolean
        For i As Integer = 0 To oApplication.Forms.Count - 1
            If oApplication.Forms.Item(i).UniqueID = sFormID Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function GetMaxUdoCode() As Integer
        Dim oRecordSet As SAPbobsCOM.Recordset

        oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRecordSet.DoQuery("SELECT MAX(CAST(Code AS INT)) AS INT FROM [@ENCABEZADO]")
        GetMaxUdoCode = Convert.ToUInt32(oRecordSet.Fields.Item(0).Value)

    End Function

    Private Function ExistsUDOID(ByVal sCode As String) As Boolean
        Dim oRecordSet As SAPbobsCOM.Recordset

        oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRecordSet.DoQuery("SELECT Code FROM [@ENCABEZADO] WHERE Code=" & sCode & "")

        If oRecordSet.Fields.Item(0).Value = sCode Then
            Return True
        End If

        Return False
    End Function

    Private Sub SetNewUDOCode(ByRef oForm As SAPbouiCOM.Form)

        Dim lNewUDOID As Integer

        lNewUDOID = GetMaxUdoCode() + 1

        Dim oEditText As SAPbouiCOM.EditText

        ' // Set Default data 

        oEditText = oForm.Items.Item("txtUDOID").Specific
        oEditText.String = lNewUDOID

        oEditText = oForm.Items.Item("txtCode").Specific
        oEditText.String = lNewUDOID

        oEditText = oForm.Items.Item("txtName").Specific
        oEditText.String = lNewUDOID

    End Sub

    Private Sub DeactivateItems(ByRef oForm As SAPbouiCOM.Form)
        Dim oItem As SAPbouiCOM.Item
        Dim oEdiTtext As SAPbouiCOM.EditText

        ' // Deactivate edit texts

        oItem = oForm.Items.Item("txtCode")
        oEdiTtext = oItem.Specific
        oEdiTtext.Active = False
        oItem.Enabled = False


        oItem = oForm.Items.Item("txtName")
        oEdiTtext = oItem.Specific
        oEdiTtext.Active = False
        oItem.Enabled = False

    End Sub

    Public Sub New()
        StartApp()

        SetMenuItems()

        SetFilters()

    End Sub


End Class

