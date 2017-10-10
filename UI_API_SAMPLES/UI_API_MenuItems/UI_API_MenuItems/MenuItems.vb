Public Class MenuItems
    Private WithEvents oApplication As SAPbouiCOM.Application
    Private oSboGuiApi As SAPbouiCOM.SboGuiApi
    Private oFilters As SAPbouiCOM.EventFilters
    Private oFilter As SAPbouiCOM.EventFilter

    Private oMatrix As SAPbouiCOM.Matrix
    Private oColumns As SAPbouiCOM.Columns
    Private oColumn As SAPbouiCOM.Column

    Private oForm As SAPbouiCOM.Form
    Private oDBDataSource As SAPbouiCOM.DBDataSource


    ' *****************************
    ' ******** FUNCTIONS **********
    ' *****************************

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


        Catch ex As Exception
            MsgBox(ex.Message)
            End
        End Try

    End Sub


    Private Sub SetFilters()
        ' // Set EventFilters Object
        Dim oFilters As SAPbouiCOM.EventFilters = New SAPbouiCOM.EventFilters

        Try

            ' // Add the Others Event Types to the Container
            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_MENU_CLICK)
            oFilter.AddEx("SampleFormType")  ' Sales Order Form

            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST)
            oFilter.AddEx("SampleFormType")  ' Sales Order Form

            oApplication.SetFilter(oFilters)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub SetDataSourceToForm()
        oForm.DataSources.UserDataSources.Add("DSCardCode", SAPbouiCOM.BoDataType.dt_SHORT_TEXT)
        oForm.DataSources.UserDataSources.Add("DSCardName", SAPbouiCOM.BoDataType.dt_SHORT_TEXT)
        oForm.DataSources.UserDataSources.Add("DSIDUser", SAPbouiCOM.BoDataType.dt_SHORT_NUMBER)
        oDBDataSource = oForm.DataSources.DBDataSources.Add("OUSR")

    End Sub


    Private Sub GetDataFromDataSource()
        '// Ready Matrix to populate data
        oMatrix.Clear()
        oMatrix.AutoResizeColumns()

        '// Execute the query with the conditions collection
        oDBDataSource.Query()

        oMatrix.LoadFromDataSource()
    End Sub


    Private Sub CreateForm()
        Dim oCreationParams As SAPbouiCOM.FormCreationParams
        Dim oItem As SAPbouiCOM.Item
        Dim oStaticText As SAPbouiCOM.StaticText
        Dim oEditText As SAPbouiCOM.EditText
        Dim oButton As SAPbouiCOM.Button
        Dim oLink As SAPbouiCOM.LinkedButton

        Try
            ' // Creating the New form
            ' *************************

            ' // Create the FormCreationParams Object
            oCreationParams = oApplication.CreateObject(
                SAPbouiCOM.BoCreatableObjectType.cot_FormCreationParams)

            ' // Specify the parameters in the object
            oCreationParams.UniqueID = "SampleFormID"
            oCreationParams.FormType = "SampleFormType"
            oCreationParams.BorderStyle = SAPbouiCOM.BoFormBorderStyle.fbs_Fixed


            ' // Add The New Form to SBO Application
            If Existform(oCreationParams.UniqueID) Then
                oApplication.Forms.Item(oCreationParams.UniqueID).Close()
                oApplication.SetStatusBarMessage("Ya hay un formulario de Ejercicio Abierto. Cerrado Con Éxito.", SAPbouiCOM.BoMessageTime.bmt_Short, True)
                'Exit Sub
            End If

            oForm = oApplication.Forms.AddEx(oCreationParams)
            oForm.Width = 390
            oForm.Height = 400
            oForm.Title = "Ejercicio"


            '' // Set data Source to form
            '' **********************************
            SetDataSourceToForm()


            ' // Add Choose From List
            ' *****************************
            AddChooseFromList()


            ' // Set Card Code items
            ' ************************

            ' // Adding Static text for CardCode
            oItem = oForm.Items.Add("LblCCode", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            With oItem
                .Left = 40
                .Width = 90
                .Top = 50
                .Height = 16
                oStaticText = oItem.Specific
                oStaticText.Caption = "Código del cliente:"

            End With


            ' // Adding Edit Text for CardCode
            oItem = oForm.Items.Add("TxtCCode", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            With oItem
                .Left = 160
                .Width = 160
                .Top = 50
                .Height = 16
                oEditText = oItem.Specific
                oEditText.DataBind.SetBound(True, "", "DSCardCode")
                oEditText.ChooseFromListUID = "CFLCardCod"
                oEditText.ChooseFromListAlias = "CardCode"
            End With


            ' // Adding Linked Button for CardCode
            oItem = oForm.Items.Add("lnkCCode", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
            With oItem
                .LinkTo = "TxtCCode"
                .Left = 130
                .Width = 30
                .Top = 50
                .Height = 16
                oLink = oForm.Items.Item("lnkCCode").Specific
                oLink.LinkedObject = SAPbouiCOM.BoLinkedObject.lf_BusinessPartner

            End With


            ' // Set CardName items
            ' ************************

            ' // Adding Static Text for CardName
            oItem = oForm.Items.Add("LblCName", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            With oItem
                .Left = 40
                .Width = 110
                .Top = 70
                .Height = 16
                oStaticText = oItem.Specific
                oStaticText.Caption = "Nombre del cliente:"

            End With

            ' // Adding Edit Text for CardName
            oItem = oForm.Items.Add("TxtCName", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            With oItem
                .Left = 160
                .Width = 160
                .Height = 16
                .Top = 70
                .LinkTo = "TxtCCode"
                oEditText = oItem.Specific
                oEditText.DataBind.SetBound(True, "", "DSCardName")
                oEditText.ChooseFromListUID = "CFLCardNam"
                oEditText.ChooseFromListAlias = "CardName"

            End With


            ' // Set Buttons
            ' ************************

            ' // Adding <Actualizar> Button
            oItem = oForm.Items.Add("1", SAPbouiCOM.BoFormItemTypes.it_BUTTON)
            With oItem
                .Left = 6
                .Width = 65
                .Top = 300
                .Height = 19
                oButton = oItem.Specific
                oButton.Caption = "Actualizar"

            End With


            ' // Adding <Cancelar> Button
            oItem = oForm.Items.Add("2", SAPbouiCOM.BoFormItemTypes.it_BUTTON)
            With oItem
                .Left = 75
                .Width = 65
                .Top = 300
                .Height = 19
                oButton = oItem.Specific
                oButton.Caption = "Cancelar"
            End With


            ' // Add the matrix and Set form visible
            AddMatrixToForm()

        Catch ex As Exception
            oApplication.MessageBox(ex.Message)

        End Try
    End Sub


    Private Sub AddMatrixToForm()

        '// we will use the following object to add items to our form
        Dim oItem As SAPbouiCOM.Item

        '// Adding a Matrix item
        '//***************************

        oItem = oForm.Items.Add("Matrix1", SAPbouiCOM.BoFormItemTypes.it_MATRIX)
        oItem.Left = 30
        oItem.Width = 320
        oItem.Top = 120
        oItem.Height = 150

        oMatrix = oItem.Specific
        oColumns = oMatrix.Columns

        ' // Add a column for User SAP ID
        oColumn = oColumns.Add("DSUserID", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        With oColumn
            .TitleObject.Caption = "#"
            .Width = 30
            .Editable = False
        End With


        '// Add a column for BP Card Name
        oColumn = oColumns.Add("DSUserName", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        With oColumn
            .TitleObject.Caption = "UserName"
            .Width = 150
            .Editable = True
        End With


        '// Add a column for BP Card Name
        oColumn = oColumns.Add("DSEmail", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        With oColumn
            .TitleObject.Caption = "e-mail"
            .Width = 190
            .Editable = True
        End With

    End Sub


    Private Sub BindDataToMatrix()
        '// getting the matrix column by the UID
        oColumn = oColumns.Item("DSUserName")
        oColumn.DataBind.SetBound(True, "OUSR", "USER_CODE")

        oColumn = oColumns.Item("DSEmail")
        oColumn.DataBind.SetBound(True, "OUSR", "E_Mail")

        oColumn = oColumns.Item("DSUserID")
        oColumn.DataBind.SetBound(True, "", "DSIDUser")
    End Sub


    Private Sub AddMatrixIDs()
        For i As Integer = 1 To oMatrix.RowCount
            oMatrix.Columns.Item("DSUserID").Cells.Item(i).Specific.Value = i.ToString
        Next

    End Sub


    Private Function Existform(ByVal sFormID As String) As Boolean
        For i As Integer = 0 To oApplication.Forms.Count - 1
            If oApplication.Forms.Item(i).UniqueID = sFormID Then
                Return True
            End If
        Next
        Return False
    End Function


    Private Sub AddChooseFromList()
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

            ' Adding CFL for CardCode
            oCFLCreationParams.UniqueID = "CFLCardCod"
            oCFL = oCFLs.Add(oCFLCreationParams)

            ' Adding Conditions to CFLCardCod
            oCons = oCFL.GetConditions()
            oCon = oCons.Add()
            oCon.Alias = "CardType"
            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
            oCon.CondVal = "C"
            oCFL.SetConditions(oCons)


            ' Adding CFL for CFLCardNam
            oCFLCreationParams.UniqueID = "CFLCardNam"
            oCFL = oCFLs.Add(oCFLCreationParams)

            ' Adding Conditions to CFLCardNam
            oCons = oCFL.GetConditions()
            oCon = oCons.Add()
            oCon.Alias = "CardType"
            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
            oCon.CondVal = "C"
            oCFL.SetConditions(oCons)


        Catch
            MsgBox(Err.Description)

        End Try
    End Sub


    ' *****************************
    ' ********* MENU ITEMS  **********
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

            ' // Set New Menu Item values into the MenuCreationPackage Object
            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP
            oCreationPackage.UniqueID = "SampleMenu"
            oCreationPackage.String = "Ejercicio"
            oCreationPackage.Enabled = True
            oCreationPackage.Position = 10

            ' // Add the new Menu
            If oApplication.Menus.Exists("SampleMenu") Then
                oApplication.Menus.RemoveEx("SampleMenu")
            End If
            oMenus.AddEx(oCreationPackage)

            ' // Sets config to New SubMenu Item
            oMenuItem = oApplication.Menus.Item("SampleMenu")
            oMenus = oMenuItem.SubMenus
            oCreationPackage.UniqueID = "SubMenu001"
            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING
            oCreationPackage.String = "Ventana ejercicio"
            oCreationPackage.Enabled = True

            ' // Add the New Sum Menu Item
            oMenus.AddEx(oCreationPackage)

        Catch ex As Exception
            oApplication.MessageBox(ex.Message)
        End Try
    End Sub


    Private Sub SetFormMenuItems()
        Dim oCreationPackage As SAPbouiCOM.MenuCreationParams

        Try
            oCreationPackage = oApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)
            oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING
            oCreationPackage.UniqueID = "MyMenu001"
            oCreationPackage.String = "Mi menú Ir A"


            oForm.Menu.AddEx(oCreationPackage)
        Catch ex As Exception
            oApplication.MessageBox(ex.Message)
        End Try

    End Sub


    ' *****************************
    ' ********* FILTERS  **********
    ' *****************************

    Private Sub oApplication_MenuEvent(ByRef pVal As SAPbouiCOM.MenuEvent, ByRef BubbleEvent As Boolean) Handles oApplication.MenuEvent

        If pVal.BeforeAction Then
            If pVal.MenuUID = "SubMenu001" Then
                Try
                    CreateForm()
                    '// Bind the Form's items with the desired data source
                    BindDataToMatrix()
                    GetDataFromDataSource()
                    AddMatrixIDs()
                    ' // Show the Form
                    oForm.Visible = True
                Catch ex As Exception
                    oApplication.SetStatusBarMessage("Error: " & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Medium, True)
                End Try

            ElseIf pVal.MenuUID = "MyMenu001" Then
                oApplication.SetStatusBarMessage("Menu del formulario presionado".ToString, SAPbouiCOM.BoMessageTime.bmt_Short, False)
            End If

        Else
            If pVal.MenuUID = "SubMenu001" Then

                SetFormMenuItems()

            End If
        End If

    End Sub


    Private Sub oApplication_ItemEvent(ByVal FormUID As String, ByRef pVal As SAPbouiCOM.ItemEvent, ByRef BubbleEvent As Boolean) Handles oApplication.ItemEvent
        Try
            If Not pVal.BeforeAction Then

                ' // Cath Actions from Message box
                If pVal.FormType = "0" Then

                    If pVal.EventType = SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST Then

                        If pVal.ItemUID = "TxtCCode" Or pVal.ItemUID = "TxtCName" Then

                            Dim oCFLEvento As SAPbouiCOM.IChooseFromListEvent
                            Dim oCFL As SAPbouiCOM.ChooseFromList
                            Dim oDataTable As SAPbouiCOM.DataTable
                            Dim sCFL_ID, sCardCode, sCardName As String

                            oCFLEvento = pVal
                            sCFL_ID = oCFLEvento.ChooseFromListUID
                            oCFL = oForm.ChooseFromLists.Item(sCFL_ID)
                            oDataTable = oCFLEvento.SelectedObjects

                            ' // Get Cardr Values from table result
                            sCardCode = oDataTable.GetValue(0, 0)
                            sCardName = oDataTable.GetValue(1, 0)
                            ' // Set new values to Edit Texts
                            oForm.DataSources.UserDataSources.Item("DSCardCode").ValueEx = sCardCode
                            oForm.DataSources.UserDataSources.Item("DSCardName").ValueEx = sCardName

                        End If

                    End If  ' pVal.EventType

                End If  ' pVal.FormType

            End If  ' pVal.Before Action

        Catch ex As Exception
            oApplication.SetStatusBarMessage("Error" & ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, True)
        End Try

    End Sub


    Public Sub New()
        StartApp()

        SetMenuItems()

        SetFilters()

    End Sub
End Class
