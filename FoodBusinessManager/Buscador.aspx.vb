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
        Dim busquedas As New List(Of BusquedaDTO)
        Dim texto As String
        texto = Session("buscar")

        Dim Cliente As New ClienteDTO
        Cliente = DirectCast(Current.Session("Cliente"), ClienteDTO)
        Session("usuario") = Cliente.usuario
        Session("IDUser") = Cliente.usuario.id

        'la busqueda sin parametro trae todo
        busquedas = BusquedaBLL.ObtenerInstancia.Buscar(texto)
        Dim lista As New List(Of BusquedaDTO)

        For Each acceso As BusquedaDTO In busquedas
            If acceso.esPublico = True Then
                lista.Add(acceso)
            Else
                If Cliente.usuario.perfil.PuedeUsar(acceso.URL) Then
                    lista.Add(acceso)
                End If
            End If
        Next

        rp_busqueda.DataSource = Nothing
        rp_busqueda.DataSource = lista
        rp_busqueda.DataBind()

    End Sub


    Public Sub CargarBusquedaPublico()
        Dim lista As New List(Of BusquedaDTO)
        Dim texto As String
        texto = Session("buscar")

        'La busqueda es una busqueda publica.
        lista = BusquedaBLL.ObtenerInstancia.Buscar(texto, 1)

        rp_busqueda.DataSource = Nothing
        rp_busqueda.DataSource = lista
        rp_busqueda.DataBind()
    End Sub


End Class