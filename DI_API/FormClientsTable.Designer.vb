<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormClientsTable
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
        Me.gbUserTable = New System.Windows.Forms.GroupBox()
        Me.btnAddUser = New System.Windows.Forms.Button()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.lnlName = New System.Windows.Forms.Label()
        Me.txtTableDescription = New System.Windows.Forms.TextBox()
        Me.txtTableName = New System.Windows.Forms.TextBox()
        Me.gbUserFields = New System.Windows.Forms.GroupBox()
        Me.btnAddField = New System.Windows.Forms.Button()
        Me.txtFieldDescription = New System.Windows.Forms.TextBox()
        Me.txtFieldName = New System.Windows.Forms.TextBox()
        Me.comboUserTablesMD = New System.Windows.Forms.ComboBox()
        Me.lblFieldDescription = New System.Windows.Forms.Label()
        Me.lblFieldName = New System.Windows.Forms.Label()
        Me.lblTable = New System.Windows.Forms.Label()
        Me.btnAddData = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.gbUserTable.SuspendLayout()
        Me.gbUserFields.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbUserTable
        '
        Me.gbUserTable.Controls.Add(Me.btnAddUser)
        Me.gbUserTable.Controls.Add(Me.lblDescription)
        Me.gbUserTable.Controls.Add(Me.lnlName)
        Me.gbUserTable.Controls.Add(Me.txtTableDescription)
        Me.gbUserTable.Controls.Add(Me.txtTableName)
        Me.gbUserTable.Location = New System.Drawing.Point(12, 23)
        Me.gbUserTable.Name = "gbUserTable"
        Me.gbUserTable.Size = New System.Drawing.Size(375, 159)
        Me.gbUserTable.TabIndex = 0
        Me.gbUserTable.TabStop = False
        Me.gbUserTable.Text = "Tabla de Usuario"
        '
        'btnAddUser
        '
        Me.btnAddUser.Location = New System.Drawing.Point(264, 120)
        Me.btnAddUser.Name = "btnAddUser"
        Me.btnAddUser.Size = New System.Drawing.Size(85, 23)
        Me.btnAddUser.TabIndex = 3
        Me.btnAddUser.Text = "Agregar"
        Me.btnAddUser.UseVisualStyleBackColor = True
        '
        'lblDescription
        '
        Me.lblDescription.AutoSize = True
        Me.lblDescription.Location = New System.Drawing.Point(27, 78)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(63, 13)
        Me.lblDescription.TabIndex = 0
        Me.lblDescription.Text = "Descripción"
        '
        'lnlName
        '
        Me.lnlName.AutoSize = True
        Me.lnlName.Location = New System.Drawing.Point(27, 38)
        Me.lnlName.Name = "lnlName"
        Me.lnlName.Size = New System.Drawing.Size(44, 13)
        Me.lnlName.TabIndex = 0
        Me.lnlName.Text = "Nombre"
        '
        'txtTableDescription
        '
        Me.txtTableDescription.Location = New System.Drawing.Point(101, 75)
        Me.txtTableDescription.Name = "txtTableDescription"
        Me.txtTableDescription.Size = New System.Drawing.Size(248, 20)
        Me.txtTableDescription.TabIndex = 2
        '
        'txtTableName
        '
        Me.txtTableName.Location = New System.Drawing.Point(101, 38)
        Me.txtTableName.Name = "txtTableName"
        Me.txtTableName.Size = New System.Drawing.Size(175, 20)
        Me.txtTableName.TabIndex = 1
        '
        'gbUserFields
        '
        Me.gbUserFields.Controls.Add(Me.btnAddField)
        Me.gbUserFields.Controls.Add(Me.txtFieldDescription)
        Me.gbUserFields.Controls.Add(Me.txtFieldName)
        Me.gbUserFields.Controls.Add(Me.comboUserTablesMD)
        Me.gbUserFields.Controls.Add(Me.lblFieldDescription)
        Me.gbUserFields.Controls.Add(Me.lblFieldName)
        Me.gbUserFields.Controls.Add(Me.lblTable)
        Me.gbUserFields.Location = New System.Drawing.Point(12, 206)
        Me.gbUserFields.Name = "gbUserFields"
        Me.gbUserFields.Size = New System.Drawing.Size(375, 190)
        Me.gbUserFields.TabIndex = 0
        Me.gbUserFields.TabStop = False
        Me.gbUserFields.Text = "Campos de Usuario"
        '
        'btnAddField
        '
        Me.btnAddField.Location = New System.Drawing.Point(264, 151)
        Me.btnAddField.Name = "btnAddField"
        Me.btnAddField.Size = New System.Drawing.Size(85, 23)
        Me.btnAddField.TabIndex = 7
        Me.btnAddField.Text = "Agregar"
        Me.btnAddField.UseVisualStyleBackColor = True
        '
        'txtFieldDescription
        '
        Me.txtFieldDescription.Location = New System.Drawing.Point(101, 106)
        Me.txtFieldDescription.Name = "txtFieldDescription"
        Me.txtFieldDescription.Size = New System.Drawing.Size(248, 20)
        Me.txtFieldDescription.TabIndex = 6
        '
        'txtFieldName
        '
        Me.txtFieldName.Location = New System.Drawing.Point(101, 70)
        Me.txtFieldName.Name = "txtFieldName"
        Me.txtFieldName.Size = New System.Drawing.Size(175, 20)
        Me.txtFieldName.TabIndex = 5
        '
        'comboUserTablesMD
        '
        Me.comboUserTablesMD.FormattingEnabled = True
        Me.comboUserTablesMD.Location = New System.Drawing.Point(101, 32)
        Me.comboUserTablesMD.Name = "comboUserTablesMD"
        Me.comboUserTablesMD.Size = New System.Drawing.Size(248, 21)
        Me.comboUserTablesMD.TabIndex = 4
        '
        'lblFieldDescription
        '
        Me.lblFieldDescription.AutoSize = True
        Me.lblFieldDescription.Location = New System.Drawing.Point(27, 106)
        Me.lblFieldDescription.Name = "lblFieldDescription"
        Me.lblFieldDescription.Size = New System.Drawing.Size(63, 13)
        Me.lblFieldDescription.TabIndex = 0
        Me.lblFieldDescription.Text = "Descripción"
        '
        'lblFieldName
        '
        Me.lblFieldName.AutoSize = True
        Me.lblFieldName.Location = New System.Drawing.Point(27, 70)
        Me.lblFieldName.Name = "lblFieldName"
        Me.lblFieldName.Size = New System.Drawing.Size(44, 13)
        Me.lblFieldName.TabIndex = 0
        Me.lblFieldName.Text = "Nombre"
        '
        'lblTable
        '
        Me.lblTable.AutoSize = True
        Me.lblTable.Location = New System.Drawing.Point(27, 35)
        Me.lblTable.Name = "lblTable"
        Me.lblTable.Size = New System.Drawing.Size(34, 13)
        Me.lblTable.TabIndex = 0
        Me.lblTable.Text = "Tabla"
        '
        'btnAddData
        '
        Me.btnAddData.Location = New System.Drawing.Point(185, 421)
        Me.btnAddData.Name = "btnAddData"
        Me.btnAddData.Size = New System.Drawing.Size(85, 23)
        Me.btnAddData.TabIndex = 8
        Me.btnAddData.Text = "Agregar datos"
        Me.btnAddData.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(276, 421)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(85, 23)
        Me.btnExit.TabIndex = 9
        Me.btnExit.Text = "Salir"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'FormClientsTable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(413, 467)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnAddData)
        Me.Controls.Add(Me.gbUserFields)
        Me.Controls.Add(Me.gbUserTable)
        Me.Name = "FormClientsTable"
        Me.Text = "ClientsTable"
        Me.gbUserTable.ResumeLayout(False)
        Me.gbUserTable.PerformLayout()
        Me.gbUserFields.ResumeLayout(False)
        Me.gbUserFields.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbUserTable As System.Windows.Forms.GroupBox
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents lnlName As System.Windows.Forms.Label
    Friend WithEvents txtTableDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtTableName As System.Windows.Forms.TextBox
    Friend WithEvents btnAddUser As System.Windows.Forms.Button
    Friend WithEvents gbUserFields As System.Windows.Forms.GroupBox
    Friend WithEvents btnAddField As System.Windows.Forms.Button
    Friend WithEvents txtFieldDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtFieldName As System.Windows.Forms.TextBox
    Friend WithEvents comboUserTablesMD As System.Windows.Forms.ComboBox
    Friend WithEvents lblFieldDescription As System.Windows.Forms.Label
    Friend WithEvents lblFieldName As System.Windows.Forms.Label
    Friend WithEvents lblTable As System.Windows.Forms.Label
    Friend WithEvents btnAddData As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
End Class
