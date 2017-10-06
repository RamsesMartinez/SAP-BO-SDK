<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormAddData
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
        Me.lblTable = New System.Windows.Forms.Label()
        Me.comboUserTables = New System.Windows.Forms.ComboBox()
        Me.panelFields = New System.Windows.Forms.Panel()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lblTable
        '
        Me.lblTable.AutoSize = True
        Me.lblTable.Location = New System.Drawing.Point(47, 27)
        Me.lblTable.Name = "lblTable"
        Me.lblTable.Size = New System.Drawing.Size(37, 13)
        Me.lblTable.TabIndex = 0
        Me.lblTable.Text = "Tabla:"
        '
        'comboUserTables
        '
        Me.comboUserTables.FormattingEnabled = True
        Me.comboUserTables.Location = New System.Drawing.Point(105, 24)
        Me.comboUserTables.Name = "comboUserTables"
        Me.comboUserTables.Size = New System.Drawing.Size(288, 21)
        Me.comboUserTables.TabIndex = 1
        '
        'panelFields
        '
        Me.panelFields.AutoScroll = True
        Me.panelFields.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.panelFields.Location = New System.Drawing.Point(32, 61)
        Me.panelFields.Name = "panelFields"
        Me.panelFields.Size = New System.Drawing.Size(379, 286)
        Me.panelFields.TabIndex = 2
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(185, 363)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(95, 25)
        Me.btnSave.TabIndex = 3
        Me.btnSave.Text = "Guardar"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(298, 363)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(95, 25)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "Salir"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'FormAddData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(441, 411)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.panelFields)
        Me.Controls.Add(Me.comboUserTables)
        Me.Controls.Add(Me.lblTable)
        Me.Name = "FormAddData"
        Me.Text = "Añadir Datos"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblTable As System.Windows.Forms.Label
    Friend WithEvents comboUserTables As System.Windows.Forms.ComboBox
    Friend WithEvents panelFields As System.Windows.Forms.Panel
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
End Class
