Imports System.Windows.Forms

Public Class Videoclub
    Private WithEvents oApplication As SAPbouiCOM.Application
    Private oCompany As SAPbobsCOM.Company
    Private oSboGuiApi As SAPbouiCOM.SboGuiApi
    Private oFilters As SAPbouiCOM.EventFilters
    Private oFilter As SAPbouiCOM.EventFilter

    Private oDBDataSource As SAPbouiCOM.DBDataSource
    Private oUserDataSource As SAPbouiCOM.UserDataSource
    Private countIDForm As Integer = 0

    ' // Msg and Code Error for company connections
    Public sCompErrMsg As String
    Public lCompErrCode As Integer

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
            MsgBox("EXCEPCION" & ex.Message)
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
            oFilter.AddEx("VC_Catalogo")

            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED)
            oFilter.AddEx("VC_Catalogo")
            oFilter.AddEx("VC_Renta")
            oFilter.AddEx("VC_Retorno")

            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST)
            oFilter.AddEx("VC_Renta")
            oFilter.AddEx("VC_Retorno")

            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_VALIDATE)
            oFilter.AddEx("VC_Renta")
            oFilter.AddEx("VC_Retorno")

            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_LOAD)
            oFilter.AddEx("VC_Catalogo")

            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_CLOSE)
            oFilter.AddEx("VC_Catalogo")

            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_VISIBLE)
            oFilter.AddEx("VC_Catalogo")

            oApplication.SetFilter(oFilters)

        Catch ex As Exception
            oApplication.SetStatusBarMessage("Error SetFilter: " & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)

        End Try

    End Sub

    Private Sub oApplication_MenuEvent(ByRef pVal As SAPbouiCOM.MenuEvent, ByRef BubbleEvent As Boolean) Handles oApplication.MenuEvent
        Dim oForm As SAPbouiCOM.Form
        Try

        
            If pVal.BeforeAction Then
                oForm = oApplication.Forms.ActiveForm

                Select Case pVal.MenuUID

                    Case "MenuCatalogo"
                        ' // Create the Form or return False
                        If Not CreateForm("Catalogo.srf") Then
                            BubbleEvent = False

                        End If

                    Case "MenuRenta"
                        ' // Create the Form or return False
                        If CreateForm("Renta.srf") Then
                            oApplication.Menus.Item("1281").Enabled = False
                            oApplication.Menus.Item("1282").Enabled = False

                        Else
                            BubbleEvent = False

                        End If

                    Case "MenuRetorno"
                        ' // Create the Form or return False
                        If CreateForm("Retorno.srf") Then
                            oApplication.Menus.Item("1281").Enabled = False
                            oApplication.Menus.Item("1282").Enabled = False
                        Else
                            BubbleEvent = False

                        End If

                    Case "MenuReporte"
                        ' // Create the Form or return False
                        If CreateForm("Reporte.srf") Then
                            oApplication.Menus.Item("1281").Enabled = False
                            oApplication.Menus.Item("1282").Enabled = False

                        Else
                            BubbleEvent = False

                        End If


                End Select

            ElseIf Not pVal.BeforeAction Then
                oForm = oApplication.Forms.ActiveForm

                Select Case pVal.MenuUID
                    Case "1281"  ' // Search Mode
                        If oForm.TypeEx = "VC_Catalogo" Then
                            ' // Enable Navigation Items
                            oForm.EnableMenu("1288", True)
                            oForm.EnableMenu("1289", True)
                            oForm.EnableMenu("1290", True)
                            oForm.EnableMenu("1291", True)
                            ActivateItems(oForm)

                        End If

                    Case "1282"  ' // Add Mode
                        If oForm.TypeEx = "VC_Catalogo" Then
                            Dim oComboBox As SAPbouiCOM.ComboBox
                            ' // Disable Navigation Items
                            oForm.EnableMenu("1288", False)
                            oForm.EnableMenu("1289", False)
                            oForm.EnableMenu("1290", False)
                            oForm.EnableMenu("1291", False)
                            DeactivateItems(oForm)
                            SetNewCode(oForm)

                            oComboBox = oForm.Items.Item("cbPlace").Specific
                            oComboBox.Select("Ubicacion 1")

                            oComboBox = oForm.Items.Item("cbGenre").Specific
                            oComboBox.Select("Genero 1")
                        End If

                    Case "1288"  ' // Move NEXT
                        If oForm.TypeEx = "VC_Catalogo" Then
                            DeactivateItems(oForm)
                        End If


                    Case "1289"  ' // LAST
                        If oForm.TypeEx = "VC_Catalogo" Then
                            DeactivateItems(oForm)
                        End If

                    Case "1290"  ' // Move BEGIN
                        If oForm.TypeEx = "VC_Catalogo" Then
                            DeactivateItems(oForm)
                        End If

                    Case "1291"  ' // Move END
                        If oForm.TypeEx = "VC_Catalogo" Then
                            DeactivateItems(oForm)
                        End If

                End Select
                oForm = oApplication.Forms.ActiveForm

            End If
        Catch ex As Exception
            oApplication.SetStatusBarMessage("Error MenuEvent: " & ex.Message)
        End Try
    End Sub

    Private Sub oApplication_ItemEvent(ByVal FormUID As String, ByRef pVal As SAPbouiCOM.ItemEvent, ByRef BubbleEvent As Boolean) Handles oApplication.ItemEvent
        Dim oForm As SAPbouiCOM.Form

        Try
            If pVal.BeforeAction Then
                If pVal.FormTypeEx = "VC_Catalogo" Then

                    Select Case pVal.EventType
                        Case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED
                            ' // BUTON 1 PRESSED
                            If pVal.ItemUID = "1" Then
                                oForm = oApplication.Forms.ActiveForm

                                If oForm.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then
                                    Dim lMaxUdoCode As Integer
                                    Dim oEditText As SAPbouiCOM.EditText

                                    ' // Validate fields
                                    If FieldsAreValid(oForm) Then
                                        ' // Validate Max Code in DB
                                        oEditText = oForm.Items.Item("txtCode").Specific

                                        If CodeExists(oEditText.Value) Then
                                            lMaxUdoCode = GetMaxCode()
                                            lMaxUdoCode += 1
                                            ' // Set New Value To Code and Index

                                            ' // Code
                                            oEditText.String = lMaxUdoCode.ToString
                                            ' // Index
                                            oEditText = oForm.Items.Item("txtIndex").Specific
                                            oEditText.String = lMaxUdoCode.ToString

                                        End If

                                    Else  ' // Invalid Fields
                                        BubbleEvent = False

                                    End If

                                End If

                            End If

                    End Select

                End If  ' pVal.FormType


            ElseIf Not pVal.BeforeAction Then

                If pVal.FormTypeEx = "VC_Catalogo" Then

                    Select Case pVal.EventType
                        Case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED

                            If pVal.ItemUID = "1" Then
                                oForm = oApplication.Forms.ActiveForm
                                ' // MOVIE CREATED AND UPDATE FORM
                                If oForm.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE Then
                                    Dim oComboBox As SAPbouiCOM.ComboBox

                                    oComboBox = oForm.Items.Item("cbPlace").Specific
                                    oComboBox.Select("Ubicacion 1")

                                    oComboBox = oForm.Items.Item("cbGenre").Specific
                                    oComboBox.Select("Genero 1")

                                    SetNewCode(oForm)

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
                            ' // Deactivate edit texts
                            If oForm.Mode = SAPbouiCOM.BoFormMode.fm_OK_MODE Then
                                DeactivateItems(oForm)

                            End If

                        Case SAPbouiCOM.BoEventTypes.et_FORM_LOAD
                            oForm = oApplication.Forms.Item(FormUID)

                            ' // Deactivate Navigation Menu
                            oForm.EnableMenu("1288", False)
                            oForm.EnableMenu("1289", False)
                            oForm.EnableMenu("1290", False)
                            oForm.EnableMenu("1291", False)

                    End Select

                ElseIf pVal.FormTypeEx = "VC_Renta" Then

                    Select Case pVal.EventType

                        Case SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST
                            Dim oCFLEvento As SAPbouiCOM.IChooseFromListEvent
                            Dim oCFL As SAPbouiCOM.ChooseFromList
                            Dim oDataTable As SAPbouiCOM.DataTable
                            Dim sCFL_ID, sCode, sName As String

                            oForm = oApplication.Forms.Item(FormUID)
                            oCFLEvento = pVal
                            sCFL_ID = oCFLEvento.ChooseFromListUID
                            oCFL = oForm.ChooseFromLists.Item(sCFL_ID)
                            oDataTable = oCFLEvento.SelectedObjects

                            If pVal.ItemUID = "txtClient" Or pVal.ItemUID = "txtName" Then

                                If Not oDataTable Is Nothing Then
                                    ' // Get Cardr Values from table result
                                    sCode = oDataTable.GetValue(0, 0)
                                    sName = oDataTable.GetValue(1, 0)

                                    ' // Set new values to Edit Texts
                                    oForm.DataSources.UserDataSources.Item("DSRen_Clie").ValueEx = sCode
                                    oForm.DataSources.UserDataSources.Item("DSRen_Name").ValueEx = sName

                                End If

                            ElseIf pVal.ItemUID = "txtMovie" Then

                                If Not oDataTable Is Nothing Then
                                    ' // Get Cardr Values from table result
                                    sName = oDataTable.GetValue(1, 0)
                                    sCode = oDataTable.GetValue(0, 0)
                                    ' // Set new values to Edit Texts
                                    oForm.DataSources.UserDataSources.Item("DSRen_Movi").ValueEx = sName
                                    oForm.DataSources.UserDataSources.Item("DSRen_MCod").ValueEx = sCode

                                End If

                            End If

                        Case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED
                            If pVal.ItemUID = "btnRent" Then
                                ' // Rent a Movie
                                Dim sSQL As String
                                Dim oRecordSet As SAPbobsCOM.Recordset
                                Dim sClientCode, sClientName, sMovieCode As String

                                oForm = oApplication.Forms.ActiveForm

                                If FieldsAreValid(oForm) Then
                                    sClientCode = oForm.DataSources.UserDataSources.Item("DSRen_Clie").Value
                                    sMovieCode = oForm.DataSources.UserDataSources.Item("DSRen_MCod").Value
                                    sClientName = oForm.DataSources.UserDataSources.Item("DSRen_Name").Value
                                    oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

                                    If IsValidUpdate(oForm, sMovieCode) Then
                                        ' // Change Movie Status
                                        sSQL = "UPDATE [@PELICULAS] SET U_STATUS='Rentada', U_CLIENTE='" & sClientCode & "' WHERE Code='" & sMovieCode & "'"
                                        oRecordSet.DoQuery(sSQL)
                                        oApplication.MessageBox("Éxito. Película rentada a " & sClientName)

                                    End If

                                    ' // Clean Movie Field
                                    oForm.DataSources.UserDataSources.Item("DSRen_Movi").Value = ""

                                End If

                            ElseIf pVal.ItemUID = "btnClean" Then
                                oForm = oApplication.Forms.ActiveForm
                                oForm.DataSources.UserDataSources.Item("DSRen_Clie").Value = ""
                                oForm.DataSources.UserDataSources.Item("DSRen_Name").Value = ""
                                oForm.DataSources.UserDataSources.Item("DSRen_Movi").Value = ""
                                oForm.DataSources.UserDataSources.Item("DSRen_MCod").Value = ""

                            End If

                        Case SAPbouiCOM.BoEventTypes.et_VALIDATE
                            oForm = oApplication.Forms.ActiveForm

                            ' // Clean txtName EditText or txtClient EditText if one of these is cleaned
                            If pVal.ItemUID = "txtClient" Or pVal.ItemUID = "txtName" Then
                                Dim oEditTextName, oEditTextCode As SAPbouiCOM.EditText

                                oEditTextCode = oForm.Items.Item("txtClient").Specific
                                oEditTextName = oForm.Items.Item("txtName").Specific

                                If oEditTextCode.String = "" Or oEditTextName.String = "" Then
                                    oForm.DataSources.UserDataSources.Item("DSRen_Clie").Value = ""
                                    oForm.DataSources.UserDataSources.Item("DSRen_Name").Value = ""

                                End If

                            End If

                    End Select

                ElseIf pVal.FormTypeEx = "VC_Retorno" Then
                    Select Case pVal.EventType
                        Case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED
                            oForm = oApplication.Forms.ActiveForm

                            If pVal.ItemUID = "btnClean" Then
                                Dim oMatrix As SAPbouiCOM.Matrix
                                oMatrix = oForm.Items.Item("mtxMovies").Specific
                                oForm.DataSources.UserDataSources.Item("DSRet_Clie").Value = ""
                                oForm.DataSources.UserDataSources.Item("DSRet_Name").Value = ""
                                oMatrix.Clear()

                            ElseIf pVal.ItemUID = "btnCheck" Then
                                If FieldsAreValid(oForm) Then
                                    GetMatrixData(oForm)
                                End If

                            ElseIf pVal.ItemUID = "btnReturn" Then
                                Dim oMatrix As SAPbouiCOM.Matrix
                                Dim oEditText As SAPbouiCOM.EditText
                                Dim oRecordSet As SAPbobsCOM.Recordset
                                Dim sSQL, sCode As String

                                oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                                oMatrix = oForm.Items.Item("mtxMovies").Specific

                                If oMatrix.RowCount = 0 Then
                                    oApplication.MessageBox("No hay peliculas en la lista.")

                                Else
                                    Dim bMoviesToReturn As Boolean = False
                                    Dim oCheckBox As SAPbouiCOM.CheckBox

                                    For i As Integer = 1 To oMatrix.RowCount
                                        oCheckBox = oMatrix.Columns.Item("V_0").Cells.Item(i).Specific

                                        If oCheckBox.Checked Then
                                            ' // Remove the User Code from Movie and Change Status to "Disponible"
                                            oEditText = oMatrix.Columns.Item("V_2").Cells.Item(i).Specific
                                            sCode = oEditText.String
                                            sSQL = "UPDATE [@PELICULAS] SET U_STATUS='Disponible', U_CLIENTE=NULL WHERE Code='" & sCode & "'"
                                            oRecordSet.DoQuery(sSQL)

                                            bMoviesToReturn = True

                                        End If

                                    Next

                                    If bMoviesToReturn Then
                                        oApplication.MessageBox("Películas regresadas")
                                        GetMatrixData(oForm)
                                    Else
                                        oApplication.SetStatusBarMessage("Ninguna pelicula seleccionada")
                                    End If


                                End If

                                
                            End If

                        Case SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST
                            Dim oCFLEvento As SAPbouiCOM.IChooseFromListEvent
                            Dim oCFL As SAPbouiCOM.ChooseFromList
                            Dim oDataTable As SAPbouiCOM.DataTable
                            Dim sCFL_ID, sCode, sName As String

                            oForm = oApplication.Forms.Item(FormUID)
                            oCFLEvento = pVal
                            sCFL_ID = oCFLEvento.ChooseFromListUID
                            oCFL = oForm.ChooseFromLists.Item(sCFL_ID)
                            oDataTable = oCFLEvento.SelectedObjects

                            If pVal.ItemUID = "txtClient" Or pVal.ItemUID = "txtName" Then

                                If Not oDataTable Is Nothing Then
                                    ' // Get Cardr Values from table result
                                    sCode = oDataTable.GetValue(0, 0)
                                    sName = oDataTable.GetValue(1, 0)

                                    ' // Set new values to Edit Texts
                                    oForm.DataSources.UserDataSources.Item("DSRet_Clie").ValueEx = sCode
                                    oForm.DataSources.UserDataSources.Item("DSRet_Name").ValueEx = sName

                                End If

                            End If

                        Case SAPbouiCOM.BoEventTypes.et_VALIDATE
                            oForm = oApplication.Forms.ActiveForm

                            ' // Clean txtName EditText or txtClient EditText if one of these is cleaned
                            If pVal.ItemUID = "txtClient" Or pVal.ItemUID = "txtName" Then
                                Dim oEditTextName, oEditTextCode As SAPbouiCOM.EditText

                                oEditTextCode = oForm.Items.Item("txtClient").Specific
                                oEditTextName = oForm.Items.Item("txtName").Specific

                                If oEditTextCode.String = "" Or oEditTextName.String = "" Then
                                    oForm.DataSources.UserDataSources.Item("DSRet_Clie").Value = ""
                                    oForm.DataSources.UserDataSources.Item("DSRet_Name").Value = ""

                                End If

                            End If

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


            '// Menu <VIDEO CLUB>
            ' ***********************
            oMenuItem = oApplication.Menus.Item("43520")
            oMenus = oMenuItem.SubMenus

            ' // Set New Menu Item values into the MenuCreationPackage Object
            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP
            oCreationPackage.UniqueID = "VideoClubMenu"
            oCreationPackage.String = "Video Club"
            oCreationPackage.Enabled = True
            oCreationPackage.Position = 12

            ' // Add the new Menu
            If oApplication.Menus.Exists("VideoClubMenu") Then
                oApplication.Menus.RemoveEx("VideoClubMenu")
            End If

            oMenus.AddEx(oCreationPackage)

            ' // Add the New <Catalogo de Películas> Menu
            oMenuItem = oApplication.Menus.Item("VideoClubMenu")
            oMenus = oMenuItem.SubMenus
            oCreationPackage.UniqueID = "MenuCatalogo"
            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING
            oCreationPackage.String = "Catalogo de Películas"
            oCreationPackage.Enabled = True
            oMenus.AddEx(oCreationPackage)

            ' // Add the New <Catalogo de Películas> Menu
            oMenuItem = oApplication.Menus.Item("VideoClubMenu")
            oMenus = oMenuItem.SubMenus
            oCreationPackage.UniqueID = "MenuRenta"
            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING
            oCreationPackage.String = "Renta de Películas"
            oCreationPackage.Enabled = True
            oMenus.AddEx(oCreationPackage)

            ' // Add the New <Catalogo de Películas> Menu
            oMenuItem = oApplication.Menus.Item("VideoClubMenu")
            oMenus = oMenuItem.SubMenus
            oCreationPackage.UniqueID = "MenuRetorno"
            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING
            oCreationPackage.String = "Retorno de Películas"
            oCreationPackage.Enabled = True
            oMenus.AddEx(oCreationPackage)

            ' // Add the New <Catalogo de Películas> Menu
            oMenuItem = oApplication.Menus.Item("VideoClubMenu")
            oMenus = oMenuItem.SubMenus
            oCreationPackage.UniqueID = "MenuReporte"
            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING
            oCreationPackage.String = "Reporte de Películas"
            oCreationPackage.Enabled = True
            oMenus.AddEx(oCreationPackage)

        Catch ex As Exception
            oApplication.SetStatusBarMessage("Error SetMenuItems: " & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)

        End Try
    End Sub



    ' *****************************
    ' ******** FUNCTIONS **********
    ' *****************************

    Private Function CreateForm(ByVal FileName As String) As Boolean
        Dim oCreationParams As SAPbouiCOM.FormCreationParams
        Dim oXmlDoc As Xml.XmlDocument
        Dim sPath As String
        Dim oForm As SAPbouiCOM.Form
        Dim oComboBox As SAPbouiCOM.ComboBox
        Dim oItem As SAPbouiCOM.Item

        Try

            ' // Creating the New form
            ' *************************

            ' // Create the FormCreationParams Object
            oCreationParams = oApplication.CreateObject(
                SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams)

            countIDForm += 1

            ' // Specify the parameters in the object
            oCreationParams.BorderStyle = SAPbouiCOM.BoFormBorderStyle.fbs_Fixed

            Select Case FileName
                Case "Catalogo.srf"
                    oCreationParams.UniqueID = "CAT_" & countIDForm.ToString
                    oCreationParams.FormType = "VC_Catalogo"
                    oCreationParams.ObjectType = "VIDEOCLUB"

                Case "Renta.srf"
                    oCreationParams.UniqueID = "REN_" & countIDForm.ToString
                    oCreationParams.FormType = "VC_Renta"


                Case "Reporte.srf"
                    oCreationParams.UniqueID = "REP_" & countIDForm.ToString
                    oCreationParams.FormType = "VC_Reporte"

                Case "Retorno.srf"
                    oCreationParams.UniqueID = "RET_" & countIDForm.ToString
                    oCreationParams.FormType = "VC_Retorno"

            End Select

            '// load the content of the XML File
            oXmlDoc = New Xml.XmlDocument
            sPath = IO.Directory.GetParent(IO.Directory.GetParent(Application.StartupPath).ToString).ToString
            oXmlDoc.Load(sPath & "\XMLForms\" & FileName)
            oCreationParams.XmlData = oXmlDoc.InnerXml
            oForm = oApplication.Forms.AddEx(oCreationParams)

            ' // Set extra content to forms
            Select Case FileName
                Case "Catalogo.srf"
                    oItem = oForm.Items.Item("txtCode")
                    oItem.AffectsFormMode = False

                    oItem = oForm.Items.Item("txtIndex")
                    oItem.AffectsFormMode = False

                    oItem = oForm.Items.Item("cbPlace")
                    oItem.AffectsFormMode = False
                    oComboBox = oItem.Specific
                    oComboBox.ValidValues.Add("Ubicacion 1", "Ubicacion 1")
                    oComboBox.ValidValues.Add("Ubicacion 2", "Ubicacion 2")
                    oComboBox.ValidValues.Add("Ubicacion 3", "Ubicacion 3")
                    oComboBox.ExpandType = SAPbouiCOM.BoExpandType.et_ValueOnly

                    oItem = oForm.Items.Item("cbGenre")
                    oItem.AffectsFormMode = False
                    oComboBox = oItem.Specific
                    oComboBox.ValidValues.Add("Genero 1", "Genero 1")
                    oComboBox.ValidValues.Add("Genero 2", "Genero 2")
                    oComboBox.ValidValues.Add("Genero 3", "Genero 3")
                    oComboBox.ExpandType = SAPbouiCOM.BoExpandType.et_ValueOnly

                    oForm.DefButton = "1"

                Case "Renta.srf"
                    Dim oLink As SAPbouiCOM.LinkedButton

                    oLink = oForm.Items.Item("lnkClient").Specific
                    oLink.LinkedObject = SAPbouiCOM.BoLinkedObject.lf_BusinessPartner


                Case "Retorno.srf"
                    Dim oLink As SAPbouiCOM.LinkedButton

                    oLink = oForm.Items.Item("lnkClient").Specific
                    oLink.LinkedObject = SAPbouiCOM.BoLinkedObject.lf_BusinessPartner

                Case "Reporte.srf"

            End Select

            ' // Set data Source to form
            '' **********************************
            SetDataSourceToForm(oForm)

            AddChooseFromList(oForm)

            '// Bind the Form's items with the desired data source
            BindDataToForm(oForm)


            ' // Set the initial behaviour to Form
            If oForm.TypeEx = "VC_Catalogo" Then
                ' // Set the Form behaviour
                oForm.DataBrowser.BrowseBy = "txtIndex"
                oForm.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE

                oComboBox = oForm.Items.Item("cbPlace").Specific
                oComboBox.Select("Ubicacion 1")

                oComboBox = oForm.Items.Item("cbGenre").Specific
                oComboBox.Select("Genero 1")

                SetNewCode(oForm)

            End If

            ' // Shows the information
            oForm.Visible = True

        Catch ex As Exception
            oApplication.SetStatusBarMessage("Error CreateSampleFormSRF" & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)
            Return False

        End Try

        Return True

    End Function

    Private Sub SetDataSourceToForm(ByRef oForm As SAPbouiCOM.Form)
        Try

            Select Case oForm.TypeEx
                Case "VC_Catalogo"
                    oForm.DataSources.DBDataSources.Add("@PELICULAS")

                Case "VC_Renta"
                    oForm.DataSources.UserDataSources.Add("DSRen_Clie", SAPbouiCOM.BoDataType.dt_SHORT_TEXT)
                    oForm.DataSources.UserDataSources.Add("DSRen_Name", SAPbouiCOM.BoDataType.dt_SHORT_TEXT)
                    oForm.DataSources.UserDataSources.Add("DSRen_Movi", SAPbouiCOM.BoDataType.dt_SHORT_TEXT)
                    oForm.DataSources.UserDataSources.Add("DSRen_MCod", SAPbouiCOM.BoDataType.dt_SHORT_TEXT)

                    oForm.DataSources.DBDataSources.Add("OUSR")
                    oForm.DataSources.DBDataSources.Add("@PELICULAS")

                Case "VC_Retorno"
                    oForm.DataSources.UserDataSources.Add("DSRet_Clie", SAPbouiCOM.BoDataType.dt_SHORT_TEXT)
                    oForm.DataSources.UserDataSources.Add("DSRet_Name", SAPbouiCOM.BoDataType.dt_SHORT_TEXT)
                    oForm.DataSources.UserDataSources.Add("DSRet_Ind", SAPbouiCOM.BoDataType.dt_SHORT_NUMBER)
                    oForm.DataSources.UserDataSources.Add("DSRet_Chec", SAPbouiCOM.BoDataType.dt_SHORT_TEXT, 1)

                    oForm.DataSources.DBDataSources.Add("OUSR")
                    oForm.DataSources.DBDataSources.Add("@PELICULAS")

                Case "VC_Reporte"

            End Select

        Catch ex As Exception
            oApplication.SetStatusBarMessage("Error SetDataSourcetoForm(): " & ex.Message)

        End Try

    End Sub

    Private Sub AddChooseFromList(ByRef oForm As SAPbouiCOM.Form)
        Dim oCFLs As SAPbouiCOM.ChooseFromListCollection
        Dim oCons As SAPbouiCOM.Conditions
        Dim oCon As SAPbouiCOM.Condition
        Dim oCFL As SAPbouiCOM.ChooseFromList
        Dim oCFLCreationParams As SAPbouiCOM.ChooseFromListCreationParams

        Try
            oCFLs = oForm.ChooseFromLists
            oCFLCreationParams = oApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams)
            oCFLCreationParams.MultiSelection = False
            oCFLCreationParams.ObjectType = SAPbouiCOM.BoLinkedObject.lf_BusinessPartner

            Select Case oForm.TypeEx
                Case "VC_Renta"
                    ' Adding CFL for Card Code
                    oCFLCreationParams.UniqueID = "CFLRen_Cli"
                    oCFL = oCFLs.Add(oCFLCreationParams)

                    ' Adding Conditions to Card Code
                    oCons = oCFL.GetConditions()
                    oCon = oCons.Add()
                    oCon.Alias = "CardType"
                    oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                    oCon.CondVal = "C"
                    oCFL.SetConditions(oCons)


                    ' Adding CFL for Card Name
                    oCFLCreationParams.UniqueID = "CFLRen_Name"
                    oCFL = oCFLs.Add(oCFLCreationParams)

                    ' Adding Conditions to Names
                    oCons = oCFL.GetConditions()
                    oCon = oCons.Add()
                    oCon.Alias = "CardType"
                    oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                    oCon.CondVal = "C"
                    oCFL.SetConditions(oCons)


                    oCFLCreationParams.ObjectType = "VIDEOCLUB"
                    ' Adding CFL for Movie Name
                    oCFLCreationParams.UniqueID = "CFLRen_Movi"
                    oCFL = oCFLs.Add(oCFLCreationParams)

                    ' Adding Conditions to Movies
                    oCons = oCFL.GetConditions()
                    oCon = oCons.Add()
                    oCon.Alias = "U_STATUS"
                    oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                    oCon.CondVal = "Disponible"
                    oCFL.SetConditions(oCons)

                Case "VC_Retorno"
                    ' Adding CFL for Card Code
                    oCFLCreationParams.UniqueID = "CFLRet_Cli"
                    oCFL = oCFLs.Add(oCFLCreationParams)

                    ' Adding Conditions to Card Code
                    oCons = oCFL.GetConditions()
                    oCon = oCons.Add()
                    oCon.Alias = "CardType"
                    oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                    oCon.CondVal = "C"
                    oCFL.SetConditions(oCons)


                    ' Adding CFL for Card Name
                    oCFLCreationParams.UniqueID = "CFLRet_Name"
                    oCFL = oCFLs.Add(oCFLCreationParams)

                    ' Adding Conditions to Names
                    oCons = oCFL.GetConditions()
                    oCon = oCons.Add()
                    oCon.Alias = "CardType"
                    oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                    oCon.CondVal = "C"
                    oCFL.SetConditions(oCons)

            End Select

        Catch
            MsgBox(Err.Description)

        End Try

    End Sub

    Private Sub BindDataToForm(ByRef oForm As SAPbouiCOM.Form)
        Dim oColumn As SAPbouiCOM.Column
        Dim oColumns As SAPbouiCOM.Columns
        Dim oMatrix As SAPbouiCOM.Matrix
        Dim oEditText As SAPbouiCOM.EditText
        Dim oComboBox As SAPbouiCOM.ComboBox

        Try

            Select Case oForm.TypeEx
                Case "VC_Catalogo"
                    ' // Bind Data to EditTexts
                    ' ************************
                    oEditText = oForm.Items.Item("txtCode").Specific
                    oEditText.DataBind.SetBound(True, "@PELICULAS", "Code")

                    oEditText = oForm.Items.Item("txtName").Specific
                    oEditText.DataBind.SetBound(True, "@PELICULAS", "Name")

                    oEditText = oForm.Items.Item("txtIndex").Specific
                    oEditText.DataBind.SetBound(True, "@PELICULAS", "U_INDEX")

                    oComboBox = oForm.Items.Item("cbPlace").Specific
                    oComboBox.DataBind.SetBound(True, "@PELICULAS", "U_UBICACION")

                    oComboBox = oForm.Items.Item("cbGenre").Specific
                    oComboBox.DataBind.SetBound(True, "@PELICULAS", "U_GENERO")

                Case "VC_Renta"
                    ' // Set Choose From List
                    oEditText = oForm.Items.Item("txtClient").Specific
                    oEditText.DataBind.SetBound(True, "", "DSRen_Clie")
                    oEditText.ChooseFromListUID = "CFLRen_Cli"
                    oEditText.ChooseFromListAlias = "CardCode"

                    oEditText = oForm.Items.Item("txtName").Specific
                    oEditText.DataBind.SetBound(True, "", "DSRen_Name")
                    oEditText.ChooseFromListUID = "CFLRen_Name"
                    oEditText.ChooseFromListAlias = "CardName"

                    oEditText = oForm.Items.Item("txtMovie").Specific
                    oEditText.DataBind.SetBound(True, "", "DSRen_Movi")
                    oEditText.ChooseFromListUID = "CFLRen_Movi"
                    oEditText.ChooseFromListAlias = "Name"

                Case "VC_Retorno"
                    ' // Set Choose From List
                    oEditText = oForm.Items.Item("txtClient").Specific
                    oEditText.DataBind.SetBound(True, "", "DSRet_Clie")
                    oEditText.ChooseFromListUID = "CFLRet_Cli"
                    oEditText.ChooseFromListAlias = "CardCode"

                    oEditText = oForm.Items.Item("txtName").Specific
                    oEditText.DataBind.SetBound(True, "", "DSRet_Name")
                    oEditText.ChooseFromListUID = "CFLRet_Name"
                    oEditText.ChooseFromListAlias = "CardName"

                    ' // Bind Data to Matrix
                    ' ************************

                    oMatrix = oForm.Items.Item("mtxMovies").Specific

                    oColumns = oMatrix.Columns

                    ' // Matrinx Index
                    oColumn = oColumns.Item(0)
                    oColumn.DataBind.SetBound(True, "", "DSRet_Ind")

                    ' // Code
                    oColumn = oColumns.Item(1)
                    oColumn.DataBind.SetBound(True, "@PELICULAS", "Code")

                    ' // Name
                    oColumn = oColumns.Item(2)
                    oColumn.DataBind.SetBound(True, "@PELICULAS", "Name")


                    ' // checked
                    oColumn = oColumns.Item(3)
                    oColumn.DataBind.SetBound(True, "", "DSRet_Chec")


                Case "VC_Reporte"

                   

            End Select

        Catch ex As Exception
            oApplication.SetStatusBarMessage("Error BindDataToForm: " & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)

        End Try
    End Sub

    Private Sub GetMatrixData(ByRef oForm As SAPbouiCOM.Form)
        Dim oMatrix As SAPbouiCOM.Matrix

        Try
            Select Case oForm.TypeEx
                Case "VC_Retorno"
                    Dim oConditions As SAPbouiCOM.Conditions
                    Dim oCondition As SAPbouiCOM.Condition

                    oConditions = New SAPbouiCOM.Conditions

                    oMatrix = oForm.Items.Item("mtxMovies").Specific
                    oMatrix.AutoResizeColumns()
                    oMatrix.Clear()

                    oDBDataSource = oForm.DataSources.DBDataSources.Item("@PELICULAS")

                    oCondition = oConditions.Add()
                    oCondition.Alias = "U_CLIENTE"
                    oCondition.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                    oCondition.CondVal = oForm.DataSources.UserDataSources.Item("DSRet_Clie").Value
                    oDBDataSource.Query(oConditions)

                    oUserDataSource = oForm.DataSources.UserDataSources.Item("DSRet_Ind")

                    For i As Integer = 0 To oDBDataSource.Size - 1
                        oDBDataSource.Offset = i
                        oUserDataSource.Value = i + 1
                        oMatrix.AddRow()
                    Next


            End Select

        Catch ex As Exception
            oApplication.SetStatusBarMessage("Error GetMatrixDataDS: " & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)

        End Try

    End Sub

    Private Function GetMaxCode() As Integer
        Dim oRecordSet As SAPbobsCOM.Recordset

        oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRecordSet.DoQuery("SELECT MAX(CAST(Code AS INT)) AS INT FROM [@PELICULAS]")
        GetMaxCode = Convert.ToUInt32(oRecordSet.Fields.Item(0).Value)

    End Function

    Private Function CodeExists(ByVal sCode As String) As Boolean
        Dim oRecordSet As SAPbobsCOM.Recordset

        oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
        oRecordSet.DoQuery("SELECT Code FROM [@PELICULAS] WHERE Code=" & sCode & "")

        If oRecordSet.Fields.Item(0).Value = sCode Then
            Return True
        End If

        Return False
    End Function

    Private Sub SetNewCode(ByRef oForm As SAPbouiCOM.Form)
        Dim lNewUDOID As Integer

        Try
            If oForm.TypeEx = "VC_Catalogo" Then

                lNewUDOID = GetMaxCode() + 1

                Dim oEditText As SAPbouiCOM.EditText

                ' // Set Default data 
                oEditText = oForm.Items.Item("txtIndex").Specific
                oEditText.String = lNewUDOID.ToString

                oEditText = oForm.Items.Item("txtCode").Specific
                oEditText.String = lNewUDOID.ToString
            End If
        Catch ex As Exception
            oApplication.SetStatusBarMessage("Error setNewCode(): " & ex.Message)

        End Try

    End Sub

    Private Sub DeactivateItems(ByRef oForm As SAPbouiCOM.Form)
        Dim oItem As SAPbouiCOM.Item
        Dim oEdiTtext As SAPbouiCOM.EditText

        ' // Deactivate edit texts
        oItem = oForm.Items.Item("txtCode")
        oEdiTtext = oItem.Specific
        oEdiTtext.Active = False
        oItem.Enabled = False

    End Sub

    Private Sub ActivateItems(ByRef oForm As SAPbouiCOM.Form)
        Dim oItem As SAPbouiCOM.Item
        Dim oEdiTtext As SAPbouiCOM.EditText

        ' // Activate edit texts

        oItem = oForm.Items.Item("txtCode")
        oItem.Enabled = True
        oEdiTtext = oItem.Specific
        oEdiTtext.Active = True

    End Sub


    ' *****************************
    ' ******** VALIDATIONS **********
    ' *****************************

    Private Function FieldsAreValid(ByRef oForm As SAPbouiCOM.Form) As Boolean
        Dim oEditText As SAPbouiCOM.EditText
        Dim oComboBox As SAPbouiCOM.ComboBox

        Try

            Select Case oForm.TypeEx
                Case "VC_Catalogo"
                    ' // Validates Name Field
                    oEditText = oForm.Items.Item("txtName").Specific
                    If oEditText.String = "" Then
                        oApplication.SetStatusBarMessage("Debe ingresar un nombre")
                        Return False

                    End If

                    ' // Validates Place Field
                    oComboBox = oForm.Items.Item("cbPlace").Specific
                    If oComboBox.Value = "" Then
                        oApplication.SetStatusBarMessage("Debe seleccionar una ubicación")
                        Return False

                    End If

                    ' // Validates Genre Field
                    oComboBox = oForm.Items.Item("cbGenre").Specific
                    If oComboBox.Value = "" Then
                        oApplication.SetStatusBarMessage("Debe seleccionar un género")
                        Return False

                    End If

                Case "VC_Renta"
                    ' // Validates Client Field
                    oEditText = oForm.Items.Item("txtClient").Specific
                    If oEditText.String = "" Then
                        oApplication.SetStatusBarMessage("Debe ingresar un Cliente")
                        Return False

                    End If

                    ' // Validates Movie Field
                    oEditText = oForm.Items.Item("txtMovie").Specific
                    If oEditText.String = "" Then
                        oApplication.SetStatusBarMessage("Debe ingresar una película")
                        Return False

                    End If

                Case "VC_Retorno"
                    ' // Validates Client Field
                    oEditText = oForm.Items.Item("txtClient").Specific
                    If oEditText.String = "" Then
                        oApplication.SetStatusBarMessage("Debe ingresar un Cliente")
                        Return False

                    End If

                Case "VC_Reporte"

            End Select

        Catch ex As Exception
            oApplication.SetStatusBarMessage("Error ValidateFields(): " & ex.Message)
            Return False
        End Try

        Return True
    End Function

    Private Function IsValidUpdate(ByRef oForm As SAPbouiCOM.Form, ByVal sMovieCode As String) As Boolean
        Dim oRecordSet As SAPbobsCOM.Recordset
        Dim sSQL As String

        Try
            Select Case oForm.TypeEx
                Case "VC_Renta"
                    oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                    sSQL = "SELECT * FROM [@PELICULAS] WHERE Code='" & sMovieCode & "' AND U_STATUS='Disponible'"

                    oRecordSet.DoQuery(sSQL)

                    If oRecordSet.Fields.Item(0).Value = "" Then
                        oApplication.SetStatusBarMessage("Esta película ya ha sido rentada. Elija otra.")
                        Return False

                    End If

            End Select

        Catch ex As Exception
            oApplication.SetStatusBarMessage("Error: IsValiDUpdate" & ex.Message)
            Return False
        End Try

        Return True

    End Function

    Public Sub New()
        StartApp()

        SetMenuItems()

        SetFilters()

    End Sub


End Class
