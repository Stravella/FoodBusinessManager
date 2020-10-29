Imports System.Web.HttpContext
Imports Entidades
Imports BLL
Public Class Buscador
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim cliente As New ClienteDTO
            cliente = Current.Session("Cliente")

            If Current.Session("Cliente") IsNot Nothing Then
                CargarBusqueda_Logeado()
            Else
                CargarBusquedaPublico()
            End If
        End If
    End Sub
    Public Sub CargarBusqueda_Logeado()
        Dim lista As New List(Of BusquedaDTO)
        Dim texto As String
        texto = Session("buscar")

        Dim Cliente As New ClienteDTO
        Cliente = DirectCast(Current.Session("Cliente"), ClienteDTO)
        Session("usuario") = Cliente.usuario
        Session("IDUser") = Cliente.usuario.id

        'Dim UsuarioRol As New Usuario_BE
        'UsuarioRol = Usuario_BLL.ObtenerInstancia.ListarObjeto(User)

        If Cliente.usuario.perfil.id_permiso = 18 Then
            lista = BusquedaBLL.ObtenerInstancia.Buscar(texto, 1)
            'Busca en la categoria Backend con el numero 1
            rp_busqueda.DataSource = Nothing
            rp_busqueda.DataSource = lista
            rp_busqueda.DataBind()
        Else
            lista = BusquedaBLL.ObtenerInstancia.Buscar(texto, 2)
            'Busca en la categoria Backend con el numero 2
            rp_busqueda.DataSource = Nothing
            rp_busqueda.DataSource = lista
            rp_busqueda.DataBind()
        End If
    End Sub


    Public Sub CargarBusquedaPublico()
        Dim lista As New List(Of BusquedaDTO)
        Dim texto As String
        texto = Session("buscar")

        lista = BusquedaBLL.ObtenerInstancia.Buscar(texto, 2)

        'Busca en la categoria Backend con el numero 0
        rp_busqueda.DataSource = Nothing
        rp_busqueda.DataSource = lista
        rp_busqueda.DataBind()
    End Sub


End Class