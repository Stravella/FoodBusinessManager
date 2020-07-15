Imports Entidades
Imports BLL
Imports System.Web.HttpContext

Public Class LogIn
    Inherits System.Web.UI.Page
    Private usuarioLogeado As New UsuarioDTO

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged

    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim usuario As New UsuarioDTO
        Try
            'Validar los formatos de lo ingresado
            usuario.username = txtUser.Text
            usuario.password = txtPassword.Text

            'Verifico que exista el usuario
            If UsuarioBLL.ObtenerInstancia.ChequearExistenciaUsuario(usuario) = True Then


                usuarioLogeado = UsuarioBLL.ObtenerInstancia.LogIn(usuario)

                If usuarioLogeado Is Nothing Then 'El usuario existe pero la contraseña no corresponde
                    dvMensaje.Visible = True
                    lblRespuesta.Text = "La contraseña es incorrecta"
                Else
                    If usuarioLogeado.bloqueado = True Then
                        dvMensaje.Visible = True
                        Response.Redirect("NuevaContraseña.aspx", False)
                    Else
                        'Camino feliz

                        Current.Session("usuario") = usuarioLogeado

                        DigitoVerificadorBLL.ObtenerInstancia.VerificarIntegridad()

                        Dim tablasCorruptas As List(Of String) = DigitoVerificadorBLL.ObtenerInstancia.VerificarIntegridad()
                        If tablasCorruptas.Count > 0 Then
                            Dim msg As String
                            For Each tabla As String In tablasCorruptas
                                msg += "Tabla: " & tabla
                                For Each fila As String In DigitoVerificadorBLL.ObtenerInstancia.VerificarFilas(tabla)
                                    msg += "Fila: " & fila & ";"
                                Next
                            Next
                            'Grabo Bitacora - Suceso Login = 1
                            Dim registroBitacora As New BitacoraDTO With {.FechaHora = Date.Now,
                                                .tipoSuceso = New SucesoBitacoraDTO With {.id = 1},
                                                .usuario = usuarioLogeado,
                                                .ValorAnterior = "",
                                                .NuevoValor = "",
                                                .observaciones = msg,
                                                .DVH = DigitoVerificadorBLL.ObtenerInstancia.CalcularDVH(registroBitacora)}
                            BitacoraBLL.ObtenerInstancia.Agregar(registroBitacora)

                        Else
                            'Grabo Bitacora - Suceso Login = 1
                            Dim registroBitacora As New BitacoraDTO With {.FechaHora = Date.Now,
                                                .tipoSuceso = New SucesoBitacoraDTO With {.id = 1},
                                                .usuario = usuarioLogeado,
                                                .ValorAnterior = "",
                                                .NuevoValor = "",
                                                .observaciones = ""
                                                }
                            registroBitacora.DVH = DigitoVerificadorBLL.ObtenerInstancia.CalcularDVH(registroBitacora)
                            BitacoraBLL.ObtenerInstancia.Agregar(registroBitacora)

                            Response.Redirect("Default1.aspx", False)
                        End If
                    End If
                End If
                'Si el usuario no existe
            Else
                dvMensaje.Visible = True
                lblRespuesta.Text = "El usuario no existe"
            End If

        Catch ex As Exception
            Dim Bitacora As BitacoraDTO = BitacoraBLL.ObtenerInstancia.ObtenerUltimaBitacora()
            Dim BitacoraError As New BitacoraErroresDTO With {.excepcion = ex.Message,
                                                .stackTrace = ex.StackTrace
                }
            BitacoraBLL.ObtenerInstancia.AgregarError(Bitacora, BitacoraError)
        End Try
    End Sub


End Class