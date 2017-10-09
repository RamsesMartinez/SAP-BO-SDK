Imports System.Collections.Generic

Public Class Events
    Private WithEvents oSBOApp As SAPbouiCOM.Application
    Private oSboGuiApi As SAPbouiCOM.SboGuiApi
    Private oFilters As SAPbouiCOM.EventFilters
    Private oFilter As SAPbouiCOM.EventFilter

    ' *** FUNCITONS ***
    Private Sub AddNewButton(ByVal sFormUID As String)
        Dim oForm As SAPbouiCOM.Form = oSBOApp.Forms.Item(sFormUID)
        Dim oItem As SAPbouiCOM.Item = oForm.Items.Add("btnNew", SAPbouiCOM.BoFormItemTypes.it_BUTTON)

        oItem.Top = oForm.Items.Item("2").Top
        oItem.Left = oForm.Items.Item("2").Left + _
            oForm.Items.Item("2").Width + 10
        oItem.Specific.Caption = "New Button"
    End Sub


    Private Sub StartApp()
        Dim sConnectionString As String

        Try
            ' // Set the AddOn identifier
            'sConnectionString = Environment.GetCommandLineArgs.GetValue(1)
            sConnectionString = "0030002C0030002C00530041005000420044005F00440061007400650076002C0050004C006F006D0056004900490056"

            oSboGuiApi = New SAPbouiCOM.SboGuiApi

            ' // This identifier is for development license - DO NOT USE
            'SboGuiApi.AddonIdentifier = "4CC5B8A4E0213A68489E38CB4052855EE8678CD237F64D1C11C22707A54DBD2D5D5F6E4050A09B9F9FB80FAC44F6"

            ' // Connect to the SAP Business One application
            oSboGuiApi.Connect(sConnectionString)

            ' // Get an initialized application object
            oSBOApp = oSboGuiApi.GetApplication()

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

    End Sub


    Private Sub Setfilters()
        ' // Set EventFilters Object
        Dim oFilters As SAPbouiCOM.EventFilters = New SAPbouiCOM.EventFilters

        Try

            ' // Add the Others Event Types to the Container
            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_LOAD)
            oFilter.AddEx("139")  ' Sales Order Form

            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED)
            oFilter.AddEx("139")  ' Sales Order Form

            oSBOApp.SetFilter(oFilters)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub oApp_ItemEvent(ByVal FormUID As String, ByRef pVal As SAPbouiCOM.ItemEvent, ByRef BubbleEvent As Boolean) Handles oSBOApp.ItemEvent
        Try
            If pVal.BeforeAction Then
                ' // Cath an Order Sale Form
                If pVal.FormType = "139" Then
                    If pVal.EventType = SAPbouiCOM.BoEventTypes.et_FORM_LOAD Then
                        oSBOApp.MessageBox("Se creará Formulario de Orden de Venta")

                    End If

                End If

            Else
                'pVal.BeforeAction = False
                If pVal.FormType = "139" Then
                    If pVal.EventType = SAPbouiCOM.BoEventTypes.et_FORM_LOAD Then
                        oSBOApp.MessageBox("Formulario de Orden de Venta Creado")
                        AddNewButton(FormUID)

                    End If

                    If pVal.EventType = SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED Then
                        If pVal.ItemUID = "btnNew" Then
                            oSBOApp.MessageBox("Evento capturado")

                        End If

                    End If

                End If

            End If

        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
    End Sub


    Public Sub New()
        StartApp()
        Setfilters()

    End Sub
End Class
