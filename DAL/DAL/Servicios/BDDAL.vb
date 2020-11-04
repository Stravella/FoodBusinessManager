
Imports System.Data.SqlClient
Imports Entidades
Public Class BDDAL

    Private Shared _instancia As BDDAL
    Public Shared Function ObtenerInstancia() As BDDAL
        If _instancia Is Nothing Then
            _instancia = New BDDAL
        End If
        Return _instancia
    End Function

    Public Function GetNextID() As Integer
        Return AccesoDAL.ObtenerInstancia.GetNextID("id", "Bckp")
    End Function

    Public Function CrearParametros(ByVal backup As BackupDTO) As List(Of SqlParameter)
        Dim params As New List(Of SqlParameter)
        Try
            With AccesoDAL.ObtenerInstancia()
                params.Add(.CrearParametro("@id", backup.ID))
                params.Add(.CrearParametro("@nombre", backup.Nombre))
                params.Add(.CrearParametro("@fecha", backup.Fecha))
                params.Add(.CrearParametro("@tamaño", backup.Tamano))
                params.Add(.CrearParametro("@path", backup.Path))
                params.Add(.CrearParametro("@id_usuario", backup.Usuario.id))
            End With
        Catch ex As Exception
            Throw ex
        End Try
        Return params
    End Function

    Public Function Listar() As List(Of BackupDTO)
        Try
            Dim ls As New List(Of BackupDTO)
            For Each Row In AccesoDAL.ObtenerInstancia.LeerBD("Backup_listar").Rows
                Dim oBackup As New BackupDTO With {
                                                .ID = Row("id"),
                                                .Nombre = Row("nombre"),
                                                .Fecha = Row("fecha"),
                                                .Tamano = Row("tamaño"),
                                                .Path = Row("path"),
                                                .Usuario = UsuarioDAL.ObtenerInstancia.ObtenerPorId(Row("id"))
                }
                ls.Add(oBackup)
            Next
            Return ls
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub Agregar(ByVal backup As BackupDTO)
        Try
            AccesoDAL.ObtenerInstancia.EjecutarSP("Backup_agregar", CrearParametros(backup))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Sub Eliminar(id As Integer)
        Try
            Dim params As New List(Of SqlParameter)
            With AccesoDAL.ObtenerInstancia()
                params.Add((.CrearParametro("@id", id)))
            End With
            AccesoDAL.ObtenerInstancia.EjecutarSP("Backup_eliminar", params)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Obtener(id As Integer) As BackupDTO
        Try
            Dim oBackup As New BackupDTO
            For Each bck As BackupDTO In Listar()
                If bck.ID = id Then
                    oBackup = bck
                    Exit For
                End If
            Next
            Return oBackup
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ObtenerPorNombre(nombre As String) As BackupDTO
        Try
            Dim oBackup As New BackupDTO
            For Each bck As BackupDTO In Listar()
                If bck.Nombre = nombre Then
                    oBackup = bck
                    Exit For
                End If
            Next
            Return oBackup
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#Region "Backup y restore"
    Public Sub RestaurarBackup(ByVal backup As BackupDTO)
        Try
            ' Path = "C:\Bak\TryME.bak"
            Dim cmdtxt As String = "USE MASTER; ALTER DATABASE FBM SET OFFLINE WITH ROLLBACK IMMEDIATE RESTORE DATABASE FBM FROM DISK = '" & backup.Path & "' WITH REPLACE, STATS = 10"

            AccesoDAL.ObtenerInstancia.EscribirBD(cmdtxt)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Sub GenerarBackup(ByVal backup As BackupDTO)
        Try
            Dim cmdtxt As String = "USE master; BACKUP DATABASE FBM TO DISK = '" & backup.Path & "'"

            AccesoDAL.ObtenerInstancia.EscribirBD(cmdtxt)
        Catch ex As Exception

        End Try
    End Sub
#End Region



End Class
