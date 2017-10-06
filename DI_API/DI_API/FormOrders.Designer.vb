<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormOrders
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.comboBusinessPartners = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.txtItemQuantity = New System.Windows.Forms.TextBox()
        Me.txtItemPrice = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblClientCode = New System.Windows.Forms.Label()
        Me.dataGridNewOrder = New System.Windows.Forms.DataGridView()
        Me.No = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NoArticulo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Descripcion = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cantidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Precio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnItemAdd = New System.Windows.Forms.Button()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.lblFinalTotalDec = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCloseNewOrder = New System.Windows.Forms.Button()
        Me.btnExportXML = New System.Windows.Forms.Button()
        Me.btnImportXML = New System.Windows.Forms.Button()
        CType(Me.dataGridNewOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(36, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Cliente"
        '
        'comboBusinessPartners
        '
        Me.comboBusinessPartners.FormattingEnabled = True
        Me.comboBusinessPartners.Location = New System.Drawing.Point(81, 32)
        Me.comboBusinessPartners.Name = "comboBusinessPartners"
        Me.comboBusinessPartners.Size = New System.Drawing.Size(211, 21)
        Me.comboBusinessPartners.TabIndex = 1
        Me.comboBusinessPartners.Text = "Elige un cliente"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(36, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "No. de Artículo"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(36, 110)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Cantidad"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(36, 150)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Precio"
        '
        'txtItemCode
        '
        Me.txtItemCode.Enabled = False
        Me.txtItemCode.Location = New System.Drawing.Point(135, 69)
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.Size = New System.Drawing.Size(157, 20)
        Me.txtItemCode.TabIndex = 2
        '
        'txtItemQuantity
        '
        Me.txtItemQuantity.Enabled = False
        Me.txtItemQuantity.Location = New System.Drawing.Point(135, 107)
        Me.txtItemQuantity.Name = "txtItemQuantity"
        Me.txtItemQuantity.Size = New System.Drawing.Size(100, 20)
        Me.txtItemQuantity.TabIndex = 3
        '
        'txtItemPrice
        '
        Me.txtItemPrice.Enabled = False
        Me.txtItemPrice.Location = New System.Drawing.Point(135, 147)
        Me.txtItemPrice.Name = "txtItemPrice"
        Me.txtItemPrice.Size = New System.Drawing.Size(100, 20)
        Me.txtItemPrice.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(435, 35)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Código"
        '
        'lblClientCode
        '
        Me.lblClientCode.AutoSize = True
        Me.lblClientCode.Location = New System.Drawing.Point(504, 35)
        Me.lblClientCode.Name = "lblClientCode"
        Me.lblClientCode.Size = New System.Drawing.Size(49, 13)
        Me.lblClientCode.TabIndex = 9
        Me.lblClientCode.Text = "0000000"
        '
        'dataGridNewOrder
        '
        Me.dataGridNewOrder.AllowUserToAddRows = False
        Me.dataGridNewOrder.AllowUserToDeleteRows = False
        Me.dataGridNewOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataGridNewOrder.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.NoArticulo, Me.Descripcion, Me.Cantidad, Me.Precio, Me.Total})
        Me.dataGridNewOrder.Location = New System.Drawing.Point(29, 179)
        Me.dataGridNewOrder.Name = "dataGridNewOrder"
        Me.dataGridNewOrder.ReadOnly = True
        Me.dataGridNewOrder.Size = New System.Drawing.Size(596, 219)
        Me.dataGridNewOrder.TabIndex = 10
        '
        'No
        '
        Me.No.HeaderText = "No"
        Me.No.Name = "No"
        Me.No.ReadOnly = True
        Me.No.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.No.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.No.Width = 50
        '
        'NoArticulo
        '
        Me.NoArticulo.HeaderText = "NoArticulo"
        Me.NoArticulo.Name = "NoArticulo"
        Me.NoArticulo.ReadOnly = True
        Me.NoArticulo.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.NoArticulo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Descripcion
        '
        Me.Descripcion.HeaderText = "Descripcion"
        Me.Descripcion.Name = "Descripcion"
        Me.Descripcion.ReadOnly = True
        Me.Descripcion.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Cantidad
        '
        Me.Cantidad.HeaderText = "Cantidad"
        Me.Cantidad.Name = "Cantidad"
        Me.Cantidad.ReadOnly = True
        Me.Cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Precio
        '
        Me.Precio.HeaderText = "Precio"
        Me.Precio.Name = "Precio"
        Me.Precio.ReadOnly = True
        Me.Precio.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Precio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Total
        '
        Me.Total.HeaderText = "Total"
        Me.Total.Name = "Total"
        Me.Total.ReadOnly = True
        Me.Total.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Total.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'btnItemAdd
        '
        Me.btnItemAdd.Enabled = False
        Me.btnItemAdd.Location = New System.Drawing.Point(488, 150)
        Me.btnItemAdd.Name = "btnItemAdd"
        Me.btnItemAdd.Size = New System.Drawing.Size(127, 23)
        Me.btnItemAdd.TabIndex = 5
        Me.btnItemAdd.Text = "Agregar"
        Me.btnItemAdd.UseVisualStyleBackColor = True
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Location = New System.Drawing.Point(441, 422)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(34, 13)
        Me.lblTotal.TabIndex = 12
        Me.lblTotal.Text = "Total:"
        '
        'lblFinalTotalDec
        '
        Me.lblFinalTotalDec.AutoSize = True
        Me.lblFinalTotalDec.Location = New System.Drawing.Point(561, 422)
        Me.lblFinalTotalDec.Name = "lblFinalTotalDec"
        Me.lblFinalTotalDec.Size = New System.Drawing.Size(28, 13)
        Me.lblFinalTotalDec.TabIndex = 13
        Me.lblFinalTotalDec.Text = "0.00"
        '
        'btnSave
        '
        Me.btnSave.Enabled = False
        Me.btnSave.Location = New System.Drawing.Point(429, 446)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(90, 23)
        Me.btnSave.TabIndex = 8
        Me.btnSave.Text = "Guardar"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCloseNewOrder
        '
        Me.btnCloseNewOrder.Location = New System.Drawing.Point(525, 446)
        Me.btnCloseNewOrder.Name = "btnCloseNewOrder"
        Me.btnCloseNewOrder.Size = New System.Drawing.Size(90, 23)
        Me.btnCloseNewOrder.TabIndex = 9
        Me.btnCloseNewOrder.Text = "Salir"
        Me.btnCloseNewOrder.UseVisualStyleBackColor = True
        '
        'btnExportXML
        '
        Me.btnExportXML.Enabled = False
        Me.btnExportXML.Location = New System.Drawing.Point(39, 417)
        Me.btnExportXML.Name = "btnExportXML"
        Me.btnExportXML.Size = New System.Drawing.Size(95, 23)
        Me.btnExportXML.TabIndex = 6
        Me.btnExportXML.Text = "Exportar XML"
        Me.btnExportXML.UseVisualStyleBackColor = True
        '
        'btnImportXML
        '
        Me.btnImportXML.Location = New System.Drawing.Point(39, 446)
        Me.btnImportXML.Name = "btnImportXML"
        Me.btnImportXML.Size = New System.Drawing.Size(95, 23)
        Me.btnImportXML.TabIndex = 7
        Me.btnImportXML.Text = "Importar XML"
        Me.btnImportXML.UseVisualStyleBackColor = True
        '
        'FormOrders
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(651, 485)
        Me.Controls.Add(Me.btnImportXML)
        Me.Controls.Add(Me.btnExportXML)
        Me.Controls.Add(Me.btnCloseNewOrder)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.lblFinalTotalDec)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.btnItemAdd)
        Me.Controls.Add(Me.dataGridNewOrder)
        Me.Controls.Add(Me.lblClientCode)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtItemPrice)
        Me.Controls.Add(Me.txtItemQuantity)
        Me.Controls.Add(Me.txtItemCode)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.comboBusinessPartners)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FormOrders"
        Me.Text = "FormOrders"
        CType(Me.dataGridNewOrder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents comboBusinessPartners As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtItemCode As System.Windows.Forms.TextBox
    Friend WithEvents txtItemQuantity As System.Windows.Forms.TextBox
    Friend WithEvents txtItemPrice As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblClientCode As System.Windows.Forms.Label
    Friend WithEvents dataGridNewOrder As System.Windows.Forms.DataGridView
    Friend WithEvents btnItemAdd As System.Windows.Forms.Button
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents lblFinalTotalDec As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCloseNewOrder As System.Windows.Forms.Button
    Friend WithEvents No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NoArticulo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Descripcion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Cantidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Precio As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Total As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnExportXML As System.Windows.Forms.Button
    Friend WithEvents btnImportXML As System.Windows.Forms.Button
End Class
