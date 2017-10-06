Public Class HelloWorld
    Public oApp As SAPbouiCOM.Application
    Private oSboGuiApi As SAPbouiCOM.SboGuiApi

    Private Function StartApp() As Integer
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
            oApp = oSboGuiApi.GetApplication()

            'oSboGuiApi = Nothing

            Return 0
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try

        Return 1
    End Function


    Public Sub New()
        'MyBase.New()

        If Not StartApp() <> 0 Then
            oApp.MessageBox("Hello World!")
        Else
            ' // The StartApp() Method displays the error message
        End If

    End Sub

End Class