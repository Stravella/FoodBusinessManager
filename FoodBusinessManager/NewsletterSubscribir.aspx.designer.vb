'------------------------------------------------------------------------------
' <generado automáticamente>
'     Este código fue generado por una herramienta.
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código. 
' </generado automáticamente>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class NewsletterSubscribir

    '''<summary>
    '''Control txtSubscripcion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtSubscripcion As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control reqMail.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents reqMail As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control formatMail.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents formatMail As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control lstCategorias.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lstCategorias As Global.System.Web.UI.WebControls.CheckBoxList

    '''<summary>
    '''Control btnSubscribrse.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnSubscribrse As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Propiedad Master.
    '''</summary>
    '''<remarks>
    '''Propiedad generada automáticamente.
    '''</remarks>
    Public Shadows ReadOnly Property Master() As FoodBusinessManager.Maestra
        Get
            Return CType(MyBase.Master, FoodBusinessManager.Maestra)
        End Get
    End Property
End Class
