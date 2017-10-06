<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormViewClients
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
        Me.txtClients = New System.Windows.Forms.TextBox()
        Me.btnClientsBegin = New System.Windows.Forms.Button()
        Me.btnClientsPrev = New System.Windows.Forms.Button()
        Me.btnClientsNext = New System.Windows.Forms.Button()
        Me.btnClientsEnd = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtClients
        '
        Me.txtClients.Location = New System.Drawing.Point(120, 52)
        Me.txtClients.Name = "txtClients"
        Me.txtClients.ReadOnly = True
        Me.txtClients.Size = New System.Drawing.Size(150, 20)
        Me.txtClients.TabIndex = 0
        '
        'btnClientsBegin
        '
        Me.btnClientsBegin.Location = New System.Drawing.Point(12, 50)
        Me.btnClientsBegin.Name = "btnClientsBegin"
        Me.btnClientsBegin.Size = New System.Drawing.Size(42, 23)
        Me.btnClientsBegin.TabIndex = 1
        Me.btnClientsBegin.Text = "|<"
        Me.btnClientsBegin.UseVisualStyleBackColor = True
        '
        'btnClientsPrev
        '
        Me.btnClientsPrev.Location = New System.Drawing.Point(60, 50)
        Me.btnClientsPrev.Name = "btnClientsPrev"
        Me.btnClientsPrev.Size = New System.Drawing.Size(42, 23)
        Me.btnClientsPrev.TabIndex = 2
        Me.btnClientsPrev.Text = "<<"
        Me.btnClientsPrev.UseVisualStyleBackColor = True
        '
        'btnClientsNext
        '
        Me.btnClientsNext.Location = New System.Drawing.Point(288, 50)
        Me.btnClientsNext.Name = "btnClientsNext"
        Me.btnClientsNext.Size = New System.Drawing.Size(42, 23)
        Me.btnClientsNext.TabIndex = 3
        Me.btnClientsNext.Text = ">>"
        Me.btnClientsNext.UseVisualStyleBackColor = True
        '
        'btnClientsEnd
        '
        Me.btnClientsEnd.Location = New System.Drawing.Point(336, 50)
        Me.btnClientsEnd.Name = "btnClientsEnd"
        Me.btnClientsEnd.Size = New System.Drawing.Size(42, 23)
        Me.btnClientsEnd.TabIndex = 4
        Me.btnClientsEnd.Text = ">|"
        Me.btnClientsEnd.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(278, 126)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(100, 25)
        Me.btnExit.TabIndex = 5
        Me.btnExit.Text = "Salir"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'ViewClients
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(393, 163)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnClientsEnd)
        Me.Controls.Add(Me.btnClientsNext)
        Me.Controls.Add(Me.btnClientsPrev)
        Me.Controls.Add(Me.btnClientsBegin)
        Me.Controls.Add(Me.txtClients)
        Me.Name = "ViewClients"
        Me.Text = "Navegador Clientes"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtClients As System.Windows.Forms.TextBox
    Friend WithEvents btnClientsBegin As System.Windows.Forms.Button
    Friend WithEvents btnClientsPrev As System.Windows.Forms.Button
    Friend WithEvents btnClientsNext As System.Windows.Forms.Button
    Friend WithEvents btnClientsEnd As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
End Class
