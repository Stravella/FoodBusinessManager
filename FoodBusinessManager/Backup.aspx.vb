Imports System.IO
Imports System.Web.HttpContext
Imports BLL
Imports Entidades

Public Class Backup
    Inherits System.Web.UI.Page


#Region "Modal"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Encadeno el evento de mi maste con mi handler
        AddHandler Master.AceptarModal, AddressOf modalAceptar_Click
    End Sub

    'Acá le doy el comportamiento según mis entidades.
    Private Sub modalAceptar_Click(ByVal sender As Object, ByVal e As CommandEventArgs)
        Try
            If Session("Restore") = Nothing Then
                ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "HideModal", "$('#myModal').modal('hide')", True)
                Response.Redirect("/Backup.aspx")
            Else
                If Session("Restore") = True Then
                    Clear()
                End If
            End If
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error",, True)
        End Try

    End Sub

    Public Sub MostrarModal(titulo As String, body As String, Optional grd As GridView = Nothing, Optional cancelar As Boolean = False)
        Dim panelMensaje As UpdatePanel = Master.FindControl("Modal")
        Dim tituloModal As Label = panelMensaje.FindControl("lblModalTitle")
        Dim bodyModal As Label = panelMensaje.FindControl("lblModalBody")
        If grd IsNot Nothing Then
            Dim grillaModal As GridView = panelMensaje.FindControl("grilla")
            grillaModal = grd
            grillaModal.Visible = True
        End If
        If cancelar = True Then
            Dim btnCancelar As Button = panelMensaje.FindControl("btnCancelar")
            btnCancelar.Visible = True
        Else
            Dim btnCancelar As Button = panelMensaje.FindControl("btnCancelar")
            btnCancelar.Visible = False
        End If
        tituloModal.Text = titulo
        bodyModal.Text = body

        ScriptManager.RegisterStartupScript(Me.Master.Page, Me.Master.GetType(), "myModal", "$('#myModal').modal();", True)
        panelMensaje.Update()
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            RefrescarGrilla()
        End If
    End Sub

    Private Sub RefrescarGrilla()
        grdBackup.DataSource = Nothing
        grdBackup.DataSource = BDBLL.ObtenerInstancia.Listar
        grdBackup.DataBind()
    End Sub

    Private Sub grdBackup_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdBackup.RowCommand
        Try
            'cuando se clickea el archivo se dispara este evento con el commandname "download" que definimos en el html
            'el command argument nos va a traer el nombre del archivo.
            If e.CommandName = "Download" Then
                Response.Clear()
                Response.ContentType = "application/octect-stream"
                Response.AppendHeader("content-disposition", "filename=" & e.CommandArgument)
                'Response.TransmitFile(Server.MapPath("~/Backups/") & e.CommandArgument)
                Response.TransmitFile(ConfigurationManager.AppSettings("Backup_Path") & e.CommandArgument)
                Response.End()
            End If

            If e.CommandName = "Restore" Then
                'tomo el archivo del folder donde se guardó y lo restoreo.
                Dim pth As String = ConfigurationManager.AppSettings("Backup_Path") & e.CommandArgument
                Dim cliente As ClienteDTO = DirectCast(Session("Cliente"), ClienteDTO)
                Dim bck As New BackupDTO With {.Nombre = e.CommandArgument, .Fecha = Date.Now, .Usuario = cliente.usuario, .Path = pth}

                'valido que el nombre del archivo exista en el folder por si lo borraron por afuera.
                'Dim query As String = Directory.GetFiles(pth, e.CommandArgument).FirstOrDefault
                If File.Exists(pth) Then
                    BDBLL.ObtenerInstancia.Restaurar(bck)
                    Dim bitacora As New BitacoraDTO With {
                        .FechaHora = Now(),
                        .usuario = cliente.usuario,
                        .tipoSuceso = New SucesoBitacoraDTO With {.id = 40}, 'Suceso: Restauracion db
                        .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
                        .observaciones = "Se restauro el backup :" & bck.Nombre
                    }
                    BitacoraBLL.ObtenerInstancia.Agregar(bitacora)

                    MostrarModal("Restore realizado", "Se realizo la restauracion seleccionada, será deslogueado y redirigido al home",,)
                    Current.Session("Restore") = True

                End If
            End If
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error",,)
        End Try

    End Sub

    Public Function Clear()
        Session.Clear()
        Response.Redirect("/Home.aspx")
    End Function


    Private Sub grdBackup_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdBackup.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim btnDelete = DirectCast(e.Row.FindControl("btnEliminar"), ImageButton)
            If Not IsNothing(btnDelete) Then
                btnDelete.Attributes.Add("onclick", "return window.confirm('¿Está seguro?')")
            End If
        End If
    End Sub

    Private Sub grdBackup_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles grdBackup.RowDeleting
        Try
            BDBLL.ObtenerInstancia.Eliminar(e.Keys("ID"))
            Dim cliente As ClienteDTO = DirectCast(Session("Cliente"), ClienteDTO)
            Dim bitacora As New BitacoraDTO With {
                        .FechaHora = Now(),
                        .usuario = cliente.usuario,
                        .tipoSuceso = New SucesoBitacoraDTO With {.id = 39}, 'Suceso: Modificacion caracteristica
                        .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
                        .observaciones = "Se elimino el backup :" & e.Keys("ID")
                    }
            BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
            RefrescarGrilla()
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error",,)
        End Try
    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Try
            If FileUpload1.HasFile Then
                Dim fileName = FileUpload1.FileName
                Dim bck As BackupDTO = BDBLL.ObtenerInstancia.ObtenerPorNombre(fileName)

                If bck.Nombre = "" Then
                    Dim pth As String = ConfigurationManager.AppSettings("Backup_Path") & fileName
                    FileUpload1.PostedFile.SaveAs(pth)

                    Dim cliente As ClienteDTO = DirectCast(Session("Cliente"), ClienteDTO)

                    Dim backup As New BackupDTO With {
                        .Nombre = fileName,
                        .Fecha = Date.Now,
                        .Path = pth & .Nombre,
                        .Usuario = cliente.usuario
                    }
                    For Each strFile In Directory.GetFiles(ConfigurationManager.AppSettings("Backup_Path"))
                        Dim fi As New FileInfo(strFile)
                        If fi.Name = fileName Then
                            backup.Tamano = fi.Length.ToString
                        End If
                    Next
                    BDBLL.ObtenerInstancia.Agregar(backup)
                    Dim bitacora As New BitacoraDTO With {
                        .FechaHora = Now(),
                        .usuario = cliente.usuario,
                        .tipoSuceso = New SucesoBitacoraDTO With {.id = 38}, 'Suceso: Modificacion caracteristica
                        .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
                        .observaciones = "Se subio al servidor el backup :" & backup.Nombre
                    }
                    BitacoraBLL.ObtenerInstancia.Agregar(bitacora)
                    RefrescarGrilla()
                Else
                    MostrarModal("Advertencia", "Ya existe un archivo con ese nombre cargado en la plataforma.",,)
                End If
            Else
                MostrarModal("Advertencia", "Seleccione el archivo que desea subir al servidor",,)
            End If
        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error",,)
        End Try
    End Sub

    Private Sub btnCrearBackUP_Click(sender As Object, e As EventArgs) Handles btnCrearBackUP.Click
        Try
            Dim cliente As ClienteDTO = DirectCast(Session("Cliente"), ClienteDTO)

            Dim backup As New BackupDTO With {
                        .Nombre = "FBM @" + Now.ToString("dd-MM-yyyy_hhmm") + ".bak",
                        .Fecha = Date.Now,
                        .Path = ConfigurationManager.AppSettings("Backup_Path") & .Nombre,
                        .Usuario = cliente.usuario
                    }

            'Hago el backup
            BDBLL.ObtenerInstancia.Generar(backup)
            'Guardo el registro
            For Each strFile In Directory.GetFiles(ConfigurationManager.AppSettings("Backup_Path"))
                Dim fi As New FileInfo(strFile)
                If fi.Name = backup.Nombre Then
                    backup.Tamano = fi.Length.ToString
                End If
            Next
            BDBLL.ObtenerInstancia.Agregar(backup)
            Dim bitacora As New BitacoraDTO With {
                        .FechaHora = Now(),
                        .usuario = cliente.usuario,
                        .tipoSuceso = New SucesoBitacoraDTO With {.id = 38}, 'Suceso: Creación backup
                        .criticidad = New CriticidadDTO With {.id = 3}, 'Criticidad: Alta
                        .observaciones = "Se creo el backup :" & backup.Nombre
                    }
            BitacoraBLL.ObtenerInstancia.Agregar(bitacora)

            MostrarModal("Se realizó exitosamente el BackUp", "Lo puede descargar haciendo click en su nombre",,)
            RefrescarGrilla()

        Catch ex As Exception
            MostrarModal("Error", "Lo siento! Ocurrio un error",,)
        End Try
    End Sub


End Class