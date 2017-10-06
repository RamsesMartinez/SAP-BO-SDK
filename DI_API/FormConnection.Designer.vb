<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormConnection
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
        Me.LblServerName = New System.Windows.Forms.Label()
        Me.LblDBCompanys = New System.Windows.Forms.Label()
        Me.TxtServer = New System.Windows.Forms.TextBox()
        Me.comboCompanys = New System.Windows.Forms.ComboBox()
        Me.btnServer = New System.Windows.Forms.Button()
        Me.LblUser = New System.Windows.Forms.Label()
        Me.TxtUser = New System.Windows.Forms.TextBox()
        Me.LblPassword = New System.Windows.Forms.Label()
        Me.TxtPassword = New System.Windows.Forms.TextBox()
        Me.GroupDB = New System.Windows.Forms.GroupBox()
        Me.GroupCompany = New System.Windows.Forms.GroupBox()
        Me.btnDisconnectCompany = New System.Windows.Forms.Button()
        Me.btnConnectCompany = New System.Windows.Forms.Button()
        Me.TxtcompanyPassword = New System.Windows.Forms.TextBox()
        Me.LblCompanyPassword = New System.Windows.Forms.Label()
        Me.TxtCompanyUser = New System.Windows.Forms.TextBox()
        Me.LblCompanyUser = New System.Windows.Forms.Label()
        Me.StatusStripConnection = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusConnection = New System.Windows.Forms.ToolStripStatusLabel()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnNewOrder = New System.Windows.Forms.Button()
        Me.btnViewClients = New System.Windows.Forms.Button()
        Me.btnUsersTable = New System.Windows.Forms.Button()
        Me.GroupDB.SuspendLayout()
        Me.GroupCompany.SuspendLayout()
        Me.StatusStripConnection.SuspendLayout()
        Me.SuspendLayout()
        '
        'LblServerName
        '
        Me.LblServerName.AutoSize = True
        Me.LblServerName.Location = New System.Drawing.Point(21, 27)
        Me.LblServerName.Name = "LblServerName"
        Me.LblServerName.Size = New System.Drawing.Size(46, 13)
        Me.LblServerName.TabIndex = 0
        Me.LblServerName.Text = "Servidor"
        '
        'LblDBCompanys
        '
        Me.LblDBCompanys.AutoSize = True
        Me.LblDBCompanys.Location = New System.Drawing.Point(21, 33)
        Me.LblDBCompanys.Name = "LblDBCompanys"
        Me.LblDBCompanys.Size = New System.Drawing.Size(54, 13)
        Me.LblDBCompanys.TabIndex = 1
        Me.LblDBCompanys.Text = "Compañia"
        '
        'TxtServer
        '
        Me.TxtServer.Location = New System.Drawing.Point(113, 24)
        Me.TxtServer.Name = "TxtServer"
        Me.TxtServer.Size = New System.Drawing.Size(166, 20)
        Me.TxtServer.TabIndex = 1
        Me.TxtServer.Text = "DEVSRV01"
        '
        'comboCompanys
        '
        Me.comboCompanys.Enabled = False
        Me.comboCompanys.FormattingEnabled = True
        Me.comboCompanys.Location = New System.Drawing.Point(113, 30)
        Me.comboCompanys.Name = "comboCompanys"
        Me.comboCompanys.Size = New System.Drawing.Size(166, 21)
        Me.comboCompanys.TabIndex = 5
        Me.comboCompanys.Text = "Elige una compañia"
        '
        'btnServer
        '
        Me.btnServer.Location = New System.Drawing.Point(152, 132)
        Me.btnServer.Name = "btnServer"
        Me.btnServer.Size = New System.Drawing.Size(127, 23)
        Me.btnServer.TabIndex = 4
        Me.btnServer.Text = "Cargar Compañias"
        Me.btnServer.UseVisualStyleBackColor = True
        '
        'LblUser
        '
        Me.LblUser.AutoSize = True
        Me.LblUser.Location = New System.Drawing.Point(21, 63)
        Me.LblUser.Name = "LblUser"
        Me.LblUser.Size = New System.Drawing.Size(43, 13)
        Me.LblUser.TabIndex = 5
        Me.LblUser.Text = "Usuario"
        '
        'TxtUser
        '
        Me.TxtUser.Location = New System.Drawing.Point(113, 60)
        Me.TxtUser.Name = "TxtUser"
        Me.TxtUser.Size = New System.Drawing.Size(166, 20)
        Me.TxtUser.TabIndex = 2
        Me.TxtUser.Text = "SA"
        '
        'LblPassword
        '
        Me.LblPassword.AutoSize = True
        Me.LblPassword.Location = New System.Drawing.Point(21, 99)
        Me.LblPassword.Name = "LblPassword"
        Me.LblPassword.Size = New System.Drawing.Size(61, 13)
        Me.LblPassword.TabIndex = 8
        Me.LblPassword.Text = "Contraseña"
        '
        'TxtPassword
        '
        Me.TxtPassword.Location = New System.Drawing.Point(113, 96)
        Me.TxtPassword.Name = "TxtPassword"
        Me.TxtPassword.Size = New System.Drawing.Size(166, 20)
        Me.TxtPassword.TabIndex = 3
        Me.TxtPassword.Text = "SBO"
        '
        'GroupDB
        '
        Me.GroupDB.Controls.Add(Me.TxtPassword)
        Me.GroupDB.Controls.Add(Me.btnServer)
        Me.GroupDB.Controls.Add(Me.LblPassword)
        Me.GroupDB.Controls.Add(Me.TxtServer)
        Me.GroupDB.Controls.Add(Me.LblServerName)
        Me.GroupDB.Controls.Add(Me.TxtUser)
        Me.GroupDB.Controls.Add(Me.LblUser)
        Me.GroupDB.Location = New System.Drawing.Point(12, 12)
        Me.GroupDB.Name = "GroupDB"
        Me.GroupDB.Size = New System.Drawing.Size(305, 165)
        Me.GroupDB.TabIndex = 10
        Me.GroupDB.TabStop = False
        Me.GroupDB.Text = "Credenciales de la Base de Datos"
        '
        'GroupCompany
        '
        Me.GroupCompany.Controls.Add(Me.btnDisconnectCompany)
        Me.GroupCompany.Controls.Add(Me.btnConnectCompany)
        Me.GroupCompany.Controls.Add(Me.TxtcompanyPassword)
        Me.GroupCompany.Controls.Add(Me.LblCompanyPassword)
        Me.GroupCompany.Controls.Add(Me.TxtCompanyUser)
        Me.GroupCompany.Controls.Add(Me.LblCompanyUser)
        Me.GroupCompany.Controls.Add(Me.comboCompanys)
        Me.GroupCompany.Controls.Add(Me.LblDBCompanys)
        Me.GroupCompany.Location = New System.Drawing.Point(12, 195)
        Me.GroupCompany.Name = "GroupCompany"
        Me.GroupCompany.Size = New System.Drawing.Size(305, 185)
        Me.GroupCompany.TabIndex = 11
        Me.GroupCompany.TabStop = False
        Me.GroupCompany.Text = "Conectar con Compañia"
        '
        'btnDisconnectCompany
        '
        Me.btnDisconnectCompany.Enabled = False
        Me.btnDisconnectCompany.Location = New System.Drawing.Point(181, 143)
        Me.btnDisconnectCompany.Name = "btnDisconnectCompany"
        Me.btnDisconnectCompany.Size = New System.Drawing.Size(98, 23)
        Me.btnDisconnectCompany.TabIndex = 14
        Me.btnDisconnectCompany.Text = "Desconectar"
        Me.btnDisconnectCompany.UseVisualStyleBackColor = True
        '
        'btnConnectCompany
        '
        Me.btnConnectCompany.Enabled = False
        Me.btnConnectCompany.Location = New System.Drawing.Point(67, 143)
        Me.btnConnectCompany.Name = "btnConnectCompany"
        Me.btnConnectCompany.Size = New System.Drawing.Size(98, 23)
        Me.btnConnectCompany.TabIndex = 13
        Me.btnConnectCompany.Text = "Conectar"
        Me.btnConnectCompany.UseVisualStyleBackColor = True
        '
        'TxtcompanyPassword
        '
        Me.TxtcompanyPassword.Enabled = False
        Me.TxtcompanyPassword.Location = New System.Drawing.Point(113, 105)
        Me.TxtcompanyPassword.Name = "TxtcompanyPassword"
        Me.TxtcompanyPassword.Size = New System.Drawing.Size(166, 20)
        Me.TxtcompanyPassword.TabIndex = 7
        Me.TxtcompanyPassword.Text = "1234"
        '
        'LblCompanyPassword
        '
        Me.LblCompanyPassword.AutoSize = True
        Me.LblCompanyPassword.Location = New System.Drawing.Point(21, 108)
        Me.LblCompanyPassword.Name = "LblCompanyPassword"
        Me.LblCompanyPassword.Size = New System.Drawing.Size(61, 13)
        Me.LblCompanyPassword.TabIndex = 12
        Me.LblCompanyPassword.Text = "Contraseña"
        '
        'TxtCompanyUser
        '
        Me.TxtCompanyUser.Enabled = False
        Me.TxtCompanyUser.Location = New System.Drawing.Point(113, 69)
        Me.TxtCompanyUser.Name = "TxtCompanyUser"
        Me.TxtCompanyUser.Size = New System.Drawing.Size(166, 20)
        Me.TxtCompanyUser.TabIndex = 6
        Me.TxtCompanyUser.Text = "ENT2"
        '
        'LblCompanyUser
        '
        Me.LblCompanyUser.AutoSize = True
        Me.LblCompanyUser.Location = New System.Drawing.Point(21, 72)
        Me.LblCompanyUser.Name = "LblCompanyUser"
        Me.LblCompanyUser.Size = New System.Drawing.Size(43, 13)
        Me.LblCompanyUser.TabIndex = 10
        Me.LblCompanyUser.Text = "Usuario"
        '
        'StatusStripConnection
        '
        Me.StatusStripConnection.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.StatusStripConnection.Enabled = False
        Me.StatusStripConnection.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusConnection})
        Me.StatusStripConnection.Location = New System.Drawing.Point(0, 463)
        Me.StatusStripConnection.Name = "StatusStripConnection"
        Me.StatusStripConnection.Size = New System.Drawing.Size(337, 22)
        Me.StatusStripConnection.TabIndex = 12
        Me.StatusStripConnection.Text = "StatusStrip1"
        '
        'ToolStripStatusConnection
        '
        Me.ToolStripStatusConnection.Name = "ToolStripStatusConnection"
        Me.ToolStripStatusConnection.Size = New System.Drawing.Size(82, 17)
        Me.ToolStripStatusConnection.Text = "Desconectado"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(219, 437)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(98, 23)
        Me.btnExit.TabIndex = 13
        Me.btnExit.Text = "Salir"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnNewOrder
        '
        Me.btnNewOrder.Enabled = False
        Me.btnNewOrder.Location = New System.Drawing.Point(12, 386)
        Me.btnNewOrder.Name = "btnNewOrder"
        Me.btnNewOrder.Size = New System.Drawing.Size(85, 25)
        Me.btnNewOrder.TabIndex = 14
        Me.btnNewOrder.Text = "Nueva Orden"
        Me.btnNewOrder.UseVisualStyleBackColor = True
        '
        'btnViewClients
        '
        Me.btnViewClients.Enabled = False
        Me.btnViewClients.Location = New System.Drawing.Point(103, 386)
        Me.btnViewClients.Name = "btnViewClients"
        Me.btnViewClients.Size = New System.Drawing.Size(85, 25)
        Me.btnViewClients.TabIndex = 15
        Me.btnViewClients.Text = "Clientes"
        Me.btnViewClients.UseVisualStyleBackColor = True
        '
        'btnUsersTable
        '
        Me.btnUsersTable.Enabled = False
        Me.btnUsersTable.Location = New System.Drawing.Point(194, 386)
        Me.btnUsersTable.Name = "btnUsersTable"
        Me.btnUsersTable.Size = New System.Drawing.Size(123, 25)
        Me.btnUsersTable.TabIndex = 16
        Me.btnUsersTable.Text = "Tablas de Usuarios"
        Me.btnUsersTable.UseVisualStyleBackColor = True
        '
        'FormConnection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(337, 485)
        Me.Controls.Add(Me.btnUsersTable)
        Me.Controls.Add(Me.btnViewClients)
        Me.Controls.Add(Me.btnNewOrder)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.StatusStripConnection)
        Me.Controls.Add(Me.GroupCompany)
        Me.Controls.Add(Me.GroupDB)
        Me.Name = "FormConnection"
        Me.Text = "Conexión"
        Me.GroupDB.ResumeLayout(False)
        Me.GroupDB.PerformLayout()
        Me.GroupCompany.ResumeLayout(False)
        Me.GroupCompany.PerformLayout()
        Me.StatusStripConnection.ResumeLayout(False)
        Me.StatusStripConnection.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblServerName As System.Windows.Forms.Label
    Friend WithEvents LblDBCompanys As System.Windows.Forms.Label
    Friend WithEvents TxtServer As System.Windows.Forms.TextBox
    Friend WithEvents comboCompanys As System.Windows.Forms.ComboBox
    Friend WithEvents btnServer As System.Windows.Forms.Button
    Friend WithEvents LblUser As System.Windows.Forms.Label
    Friend WithEvents TxtUser As System.Windows.Forms.TextBox
    Friend WithEvents LblPassword As System.Windows.Forms.Label
    Friend WithEvents TxtPassword As System.Windows.Forms.TextBox
    Friend WithEvents GroupDB As System.Windows.Forms.GroupBox
    Friend WithEvents GroupCompany As System.Windows.Forms.GroupBox
    Friend WithEvents TxtcompanyPassword As System.Windows.Forms.TextBox
    Friend WithEvents LblCompanyPassword As System.Windows.Forms.Label
    Friend WithEvents TxtCompanyUser As System.Windows.Forms.TextBox
    Friend WithEvents LblCompanyUser As System.Windows.Forms.Label
    Friend WithEvents StatusStripConnection As System.Windows.Forms.StatusStrip
    Friend WithEvents btnDisconnectCompany As System.Windows.Forms.Button
    Friend WithEvents btnConnectCompany As System.Windows.Forms.Button
    Friend WithEvents ToolStripStatusConnection As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnNewOrder As System.Windows.Forms.Button
    Friend WithEvents btnViewClients As System.Windows.Forms.Button
    Friend WithEvents btnUsersTable As System.Windows.Forms.Button

End Class
