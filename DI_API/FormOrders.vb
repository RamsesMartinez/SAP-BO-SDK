Public Class FormOrders
    Private oRecordSet As SAPbobsCOM.Recordset

    ' *** VALIDACIONES ***
    Private Function ValidateAddItemInputs(ByVal sItemCode As String, ByVal sQuantity As String, ByVal sPrice As String) As Integer
        If sItemCode = "" Then
            Return 1
        ElseIf sQuantity = "" Then
            Return 2
        ElseIf sPrice = "" Then
            Return 3
        End If
        Return 0
    End Function

    Private Function ExistsItemCode(ByVal sItemCode As String, ByRef sItemName As String) As Boolean
        ' oSBObob = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoBridge)
        ' oRecordSet = oSBObob.GetItemList()

        Try
            oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecordSet.DoQuery("SELECT ItemCode, ItemName FROM OITM")
            'oItems.Browser.Recordset = oRecordSet

            While Not oRecordSet.EoF
                If oRecordSet.Fields.Item("ItemCode").Value.ToString = sItemCode Then
                    sItemName = oRecordSet.Fields.Item("ItemName").Value.ToString
                    ' // Clean Memory
                    GC.Collect()
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordSet)
                    oRecordSet = Nothing
                    Return True
                End If
                oRecordSet.MoveNext()
            End While

            ' // Clean Memory
            GC.Collect()
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordSet)
            oRecordSet = Nothing

        Catch ex As Exception
            MsgBox("Error: " + ex.ToString)

        End Try

        Return False
    End Function


    ' *** FORM LISTENERS ***
    Private Sub FormOrders_FormClosed(sender As System.Object, e As System.EventArgs) Handles MyBase.FormClosed
        FormConnection.Show()
    End Sub

    Private Sub FormOrders_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim BusinessPartners As SAPbobsCOM.BusinessPartners

        Try
            '// Getting a new BusinessPartners object
            BusinessPartners = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners)
            oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)

            ' // Set Business Partners with Query
            oRecordSet.DoQuery(
                "SELECT CardCode FROM OCRD WHERE CardType='C' " & vbCrLf & _
                "ORDER BY CardCode")
            '// asigning (linking) the Recordset object to the Browser.Recordset property
            BusinessPartners.Browser.Recordset = oRecordSet

            Do Until BusinessPartners.Browser.EoF = True
                ' // Fill comboBusinessPartners with Recordsets
                comboBusinessPartners.Items.Add(BusinessPartners.CardName)
                BusinessPartners.Browser.MoveNext()
            Loop

            ' Block clients combo box
            comboBusinessPartners.DropDownStyle = ComboBoxStyle.DropDownList

            ' // Clean Memory
            GC.Collect()
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordSet)
            oRecordSet = Nothing
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

    End Sub


    '// *** LISTENERS ***
    Private Sub txtItemQuantity_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtItemQuantity.KeyPress
        Dim ch As Char = e.KeyChar
        If Not Char.IsDigit(ch) And (ch <> Chr(8)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtItemPrice_KeyP(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtItemPrice.KeyPress
        Dim ch As Char = e.KeyChar
        Dim tb As TextBox = sender

        If ch = Chr(46) And tb.Text.IndexOf(".") <> -1 Then
            e.Handled = True
        End If

        If Not Char.IsDigit(ch) And (ch <> Chr(8)) And (ch <> Chr(46)) Then
            e.Handled = True
        End If

        If Char.IsDigit(ch) Or ch = "." Then

            'Insertar en una variable el caracter presionado, siempre y cuando sea un digito númerico.
            Dim result As String = tb.Text.Substring(0, tb.SelectionStart) _
                                   + e.KeyChar _
                                   + tb.Text.Substring(tb.SelectionStart + tb.SelectionLength)

            Dim parts() As String = result.Split(".") 'Declarar un arreglo y llenar 
            'El primer elemento tendra la parte entera.
            'El segundo elemento contendra la parte decimal.    

            If parts.Length > 1 Then  'Verificar que el arreglo tenga mas de un elemento.
                If parts(1).Length > 2 Then 'Validar Cantidad de Decimales.
                    e.Handled = True
                End If
            End If

        End If
    End Sub

    Private Sub txtItemPrice_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtItemPrice.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        ElseIf e.KeyChar = "." And Not txtItemPrice.Text.IndexOf(".") Then
            e.Handled = True
        ElseIf e.KeyChar = "." Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub


    ' *** ITEMS DATA GRID ***
    Private Sub AddDataGridItem(ByVal sItemCode As String, ByVal sItemName As String, ByVal sItemQuantity As String, ByVal sItemPrice As String)
        Dim dItemtotal, dItemPrice As Decimal
        Dim bItemExists As Boolean = False
        Dim dItemPriceDG As Decimal
        Dim indexItems As Integer = 0

        dItemPrice = System.Convert.ToDecimal(sItemPrice)
        indexItems = dataGridNewOrder.Rows.Count + 1

        If dataGridNewOrder.Rows.Count > 0 Then
            For i As Integer = 0 To dataGridNewOrder.Rows.Count - 1
                dItemPriceDG = System.Convert.ToDecimal(dataGridNewOrder.Rows(i).Cells(4).Value.ToString)

                ' The Item already exists and has the same price
                If dataGridNewOrder.Rows(i).Cells(1).Value.ToString = sItemCode And _
                     dItemPriceDG = dItemPrice Then

                    sItemQuantity = System.Convert.ToString(
                            System.Convert.ToInt32(dataGridNewOrder.Rows(i).Cells(3).Value.ToString) + System.Convert.ToInt32(sItemQuantity)
                        )
                    dItemtotal = System.Convert.ToDecimal(sItemQuantity) * dItemPrice

                    ' // Sets new item quantity
                    dataGridNewOrder.Rows(i).Cells(3).Value = sItemQuantity.ToString
                    ' // Sets new item Total
                    dataGridNewOrder.Rows(i).Cells(5).Value = dItemtotal.ToString("N2")
                    bItemExists = True
                    Exit For
                End If
            Next

            If bItemExists = False Then
                ' // The item hasn't the same price
                dItemtotal = System.Convert.ToDecimal(sItemQuantity) * System.Convert.ToDecimal(sItemPrice)
                dataGridNewOrder.Rows.Add(indexItems, sItemCode, sItemName, sItemQuantity, dItemPrice.ToString("N2"), dItemtotal.ToString("N2"))
            End If
        Else
            ' // The item is the first element in the data grid
            dItemtotal = System.Convert.ToDecimal(sItemQuantity) * dItemPrice

            dataGridNewOrder.Rows.Add(
                indexItems,
                sItemCode,
                sItemName,
                sItemQuantity,
                dItemPrice.ToString("N2"),
                dItemtotal.ToString("N2")
                )
        End If

        UpdateOrderTotalLabel()

    End Sub

    Private Sub UpdateOrderTotalLabel()
        Dim dNewTotal As Decimal = 0.0

        For i As Integer = 0 To dataGridNewOrder.Rows.Count - 1
            dNewTotal = System.Convert.ToDecimal(dataGridNewOrder.Rows(i).Cells(5).Value.ToString) + dNewTotal
        Next

        lblFinalTotalDec.Text = dNewTotal.ToString("N2")
    End Sub


    ' *** ACTIONS ***
    Private Sub comboBusinessPartners_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles comboBusinessPartners.SelectedIndexChanged
        Dim oSBObob As SAPbobsCOM.SBObob

        Dim sClient As String = comboBusinessPartners.SelectedItem.ToString

        Try
            oSBObob = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoBridge)
            oRecordSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecordSet = oSBObob.GetObjectKeyBySingleValue(SAPbobsCOM.BoObjectTypes.oBusinessPartners, _
                                    "CardName", sClient, _
                                    SAPbobsCOM.BoQueryConditions.bqc_Equal)

            lblClientCode.Text = oRecordSet.Fields.Item(0).Value

            txtItemCode.Enabled = True
            txtItemPrice.Enabled = True
            txtItemQuantity.Enabled = True
            btnItemAdd.Enabled = True

            ' // Clean Memory
            GC.Collect()
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordSet)
            oRecordSet = Nothing
        Catch ex As Exception
            MsgBox("Exception: " + ex.Message)

        End Try

    End Sub

    Private Sub CreateNewOrderSale()
        Dim sNewObjKey As String
        Dim lRetCode As Integer

        Dim oOrder As SAPbobsCOM.Documents

        Try
            ' // Add Order Headers
            oOrder = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders)
            oOrder.CardCode = lblClientCode.Text
            oOrder.CardName = comboBusinessPartners.Text
            oOrder.DocDueDate = Now
            oOrder.DocDate = Now
            oOrder.TaxDate = Now
            oOrder.EndDeliveryDate = Now
            oOrder.Comments = "Comentario de Orden"

            For i As Integer = 0 To dataGridNewOrder.Rows.Count() - 1
                oOrder.Lines.ItemCode = dataGridNewOrder.Rows(i).Cells(1).Value
                oOrder.Lines.ItemDescription = dataGridNewOrder.Rows(i).Cells(2).Value
                oOrder.Lines.Quantity = dataGridNewOrder.Rows(i).Cells(3).Value
                oOrder.Lines.UnitPrice = dataGridNewOrder.Rows(i).Cells(4).Value
                oOrder.Lines.LineTotal = dataGridNewOrder.Rows(i).Cells(5).Value

                ' // Save the new order line
                oOrder.Lines.Add()
            Next

            ' // Save the order in database
            lRetCode = oOrder.Add()

            If lRetCode <> 0 Then
                oCompany.GetLastError(iCompErrCode, sCompErrMsg)
                MsgBox(iCompErrCode & " " & sCompErrMsg)

            Else

                Dim msgResult As Integer
                sNewObjKey = oCompany.GetNewObjectKey

                msgResult = MessageBox.Show("Nueva Orden Creada: " + sNewObjKey + ". ¿Generar factura?", "caption", MessageBoxButtons.YesNo)
                btnItemAdd.Enabled = True

                If msgResult = DialogResult.No Then
                    MessageBox.Show("Orden Generada: " + sNewObjKey)
                ElseIf msgResult = DialogResult.Yes Then
                    ' // Create de Invoice
                    CreateNewInvoice(sNewObjKey)
                End If

                ' Release oOrder Object
                GC.Collect()
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oOrder)
                oOrder = Nothing
            End If

        Catch ex As Exception
            MsgBox("Error: " + ex.ToString)
        End Try
        CleanForm()
    End Sub

    Private Sub CleanForm()
        lblFinalTotalDec.Text = "0.00"
        dataGridNewOrder.Rows.Clear()
        comboBusinessPartners.Enabled = True
    End Sub

    Private Sub CleanInputs()
        txtItemCode.Text = ""
        txtItemPrice.Text = ""
        txtItemQuantity.Text = ""
    End Sub

    Private Sub CreateNewInvoice(ByVal orderDocEntry As String)

        Dim oInvoice As SAPbobsCOM.Documents

        Dim iRet As Integer
        Dim sNewObjKey As String

        Try
            oInvoice = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices)

            ' // Add invoice headers
            oInvoice.CardCode = lblClientCode.Text
            oInvoice.DocDate = Now
            oInvoice.DocDueDate = Now
            oInvoice.TaxDate = Now

            oInvoice.Comments = "Comentari ode Factura"

            For i As Integer = 0 To dataGridNewOrder.Rows.Count - 1
                oInvoice.Lines.BaseEntry = orderDocEntry
                oInvoice.Lines.BaseLine = i
                oInvoice.Lines.BaseType = SAPbobsCOM.BoObjectTypes.oOrders

                ' // Save the new order line
                oInvoice.Lines.Add()
            Next

            iRet = oInvoice.Add()

            ' If return value is not zero, check for error description
            If iRet <> 0 Then
                oCompany.GetLastError(iCompErrCode, sCompErrMsg)
                MsgBox(iCompErrCode & " (" & CStr(sCompErrMsg) & ")" & "Factura no generada")
            Else
                sNewObjKey = oCompany.GetNewObjectKey
                MsgBox("Factura Creada: " + sNewObjKey)
            End If

            GC.Collect()
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oInvoice)
            oInvoice = Nothing

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub NewXMLOrderSale(ByVal sXMLPath As String)
        Dim oOrder As SAPbobsCOM.Documents

        Try
            oCompany.XmlExportType = SAPbobsCOM.BoXmlExportTypes.xet_ExportImportMode

            ' // Add Order Headers
            oOrder = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders)
            oOrder.CardCode = lblClientCode.Text
            oOrder.CardName = comboBusinessPartners.Text
            oOrder.DocDueDate = Now
            oOrder.DocDate = Now
            oOrder.TaxDate = Now
            oOrder.EndDeliveryDate = Now
            oOrder.Comments = "Comentario de Orden"

            For i As Integer = 0 To dataGridNewOrder.Rows.Count() - 1
                oOrder.Lines.ItemCode = dataGridNewOrder.Rows(i).Cells(1).Value
                oOrder.Lines.ItemDescription = dataGridNewOrder.Rows(i).Cells(2).Value
                oOrder.Lines.Quantity = dataGridNewOrder.Rows(i).Cells(3).Value
                oOrder.Lines.UnitPrice = dataGridNewOrder.Rows(i).Cells(4).Value
                oOrder.Lines.LineTotal = dataGridNewOrder.Rows(i).Cells(5).Value

                ' // Save the new order line
                oOrder.Lines.Add()
            Next

            oOrder.SaveXML(sXMLPath)

            ' // Release oOrder Object
            oOrder = Nothing

            MsgBox("Archivo almacenado en: " & sXMLPath)

        Catch ex As Exception
            MsgBox("Error generando el archivo xml: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadXMLOrderSale(ByVal sXMLPath As String)
        Dim oOrder As SAPbobsCOM.Documents
        Dim sItemCode, sItemDesc, sItemQuant, sItemPrice, sItemTotal As String
        Dim dlgRes As Integer

        Try
            oOrder = oCompany.GetBusinessObjectFromXML(sXMLPath, 0)
            If oOrder.Lines.Count > 0 And dataGridNewOrder.Rows.Count > 0 Then
                dlgRes = MessageBox.Show("¿Cargar orden de venta desde archivo xml?" & vbLf & vbCrLf & _
                                         "Se sobreescrbirá la orden de venta actual.", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If Not dlgRes = Windows.Forms.DialogResult.Yes Then
                    Exit Sub
                End If
            End If

            ' // Reset the DataGridtable 
            dataGridNewOrder.Rows.Clear()

            For i As Integer = 0 To oOrder.Lines.Count - 1
                oOrder.Lines.SetCurrentLine(i)
                sItemCode = oOrder.Lines.ItemCode
                sItemDesc = oOrder.Lines.ItemDescription
                sItemQuant = oOrder.Lines.Quantity
                sItemPrice = oOrder.Lines.UnitPrice
                sItemTotal = oOrder.Lines.LineTotal
                dataGridNewOrder.Rows.Add(
                    (i + 1).ToString,
                    sItemCode,
                    sItemDesc,
                    sItemQuant,
                    sItemPrice,
                    sItemTotal
                    )
            Next
            comboBusinessPartners.Text = oOrder.CardName
            lblClientCode.Text = oOrder.CardCode
            comboBusinessPartners.Enabled = False
            UpdateOrderTotalLabel()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    ' *** BUTTON ACTIONS ***
    Private Sub btnItemAdd_Click(sender As System.Object, e As System.EventArgs) Handles btnItemAdd.Click
        Dim sItemCode, sItemPrice, sItemQuantity, sItemName As String
        Dim iVAII As Integer

        ' // Items Fields
        sItemCode = txtItemCode.Text
        sItemPrice = txtItemPrice.Text
        sItemQuantity = txtItemQuantity.Text
        sItemName = ""


        ' // Get item fields validation code
        iVAII = ValidateAddItemInputs(sItemCode, sItemQuantity, sItemPrice)

        If iVAII = 0 Then
            If ExistsItemCode(sItemCode, sItemName) = True Then
                AddDataGridItem(sItemCode, sItemName, sItemQuantity, sItemPrice)
                comboBusinessPartners.Enabled = False
                CleanInputs()

                ' // Activate Butttons
                btnExportXML.Enabled = True
                btnSave.Enabled = True
            Else
                MsgBox("Item no encontrado")
            End If

        ElseIf iVAII = 1 Then
            MsgBox("Ingresa la clave del articulo")
        ElseIf iVAII = 2 Then
            MsgBox("Ingresa  la cantidad")
        ElseIf iVAII = 3 Then
            MsgBox("Ingresa el precio")
        End If

    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        ' // Block the actual button
        btnSave.Enabled = False

        ' // Make the new order sale
        CreateNewOrderSale()

    End Sub

    Private Sub btnCloseNewOrder_Click(sender As System.Object, e As System.EventArgs) Handles btnCloseNewOrder.Click
        Me.Close()
    End Sub

    Private Sub btnImportXML_Click(sender As System.Object, e As System.EventArgs) Handles btnImportXML.Click
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*"

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            LoadXMLOrderSale(openFileDialog1.FileName)
        End If

    End Sub

    Private Sub btnExportXML_Click(sender As System.Object, e As System.EventArgs) Handles btnExportXML.Click
        Dim saveFileDialog1 As New SaveFileDialog()
        saveFileDialog1.Filter = "xml Files (*.xml)|*.xml|All files (*.*)|*.*"
        saveFileDialog1.Title = "Guardar Reporte de Ventas"

        If saveFileDialog1.ShowDialog() = DialogResult.OK Then
            NewXMLOrderSale(saveFileDialog1.FileName)
        End If


    End Sub

End Class