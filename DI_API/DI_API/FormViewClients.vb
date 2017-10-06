Public Class FormViewClients

    Private oBusinessPartners As SAPbobsCOM.BusinessPartners
    Private oRecordSet As SAPbobsCOM.Recordset
    Private oSBObob As SAPbobsCOM.SBObob
    Private counterBP As Integer

    ' *** FORM LISTENERS ***
    Private Sub ViewClients_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Try
            ' // Initialize SBObob ans RecordSet Objects
            oBusinessPartners = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners)
            oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oSBObob = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoBridge)

            ' // Get  all customers (Business Partners) list
            oRecordSet = oSBObob.GetBPList(SAPbobsCOM.BoCardTypes.cCustomer)
            oBusinessPartners.Browser.Recordset = oRecordSet

            ' // new counter to catch SAP BO Bug
            counterBP = 0

            ' // Set the first customers to txtClients
            SetTxtCustomer()

        Catch ex As Exception
            MsgBox("Ocurrio un problema: " & ex.Message)

        End Try

    End Sub

    Private Sub ViewClients_FormClosed(sender As System.Object, e As System.EventArgs) Handles MyBase.FormClosed
        FormConnection.Show()

        ' // Clean Memory
        GC.Collect()
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordSet)
        oRecordSet = Nothing
        System.Runtime.InteropServices.Marshal.ReleaseComObject(oBusinessPartners)
        oBusinessPartners = Nothing

    End Sub


    ' *** ACTIONS ***
    Private Sub SetTxtCustomer()
        txtClients.Text = oBusinessPartners.CardName
    End Sub


    ' *** BUTTONS ***
    Private Sub btnExit_Click(sender As System.Object, e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnClientsBegin_Click(sender As System.Object, e As System.EventArgs) Handles btnClientsBegin.Click
        If oBusinessPartners.Browser.BoF Then
            MsgBox("Ya estás en el inicio de la lista.")
        Else
            oBusinessPartners.Browser.MoveFirst()
            SetTxtCustomer()
            counterBP = 0
        End If
    End Sub

    Private Sub btnClientsPrev_Click(sender As System.Object, e As System.EventArgs) Handles btnClientsPrev.Click
        If oBusinessPartners.Browser.BoF Then
            MsgBox("Ya estás en el inicio de la lista")
        Else
            oBusinessPartners.Browser.MovePrevious()
            SetTxtCustomer()
            counterBP -= 1
        End If
    End Sub

    Private Sub btnClientsNext_Click(sender As System.Object, e As System.EventArgs) Handles btnClientsNext.Click
        ' // Auxiliar validation to catch SAP BO bug
        If counterBP = oBusinessPartners.Browser.RecordCount - 1 Then
            MsgBox("Ya estás en el final de la lista")
        Else
            oBusinessPartners.Browser.MoveNext()
            SetTxtCustomer()
            counterBP += 1
        End If
    End Sub

    Private Sub btnClientsEnd_Click(sender As System.Object, e As System.EventArgs) Handles btnClientsEnd.Click
        ' // Auxiliar validation to catch SAP BO bug
        If counterBP = oBusinessPartners.Browser.RecordCount - 1 Then
            MsgBox("Ya estás en el final de la lista.")
        Else
            oBusinessPartners.Browser.MoveLast()
            SetTxtCustomer()
            counterBP = oBusinessPartners.Browser.RecordCount - 1
        End If
    End Sub
End Class