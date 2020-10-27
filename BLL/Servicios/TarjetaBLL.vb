Imports Entidades
Imports DAL

Public Class TarjetaBLL
#Region "Singleton"
    Public Sub New()

    End Sub
    Private Shared _instancia As TarjetaBLL
    Public Shared Function ObtenerInstancia() As TarjetaBLL
        If _instancia Is Nothing Then
            _instancia = New TarjetaBLL
        End If
        Return _instancia
    End Function
#End Region

    Function Obtener(tarjeta As TarjetaDTO) As TarjetaDTO
        Try
            tarjeta.nro = CriptografiaBLL.ObtenerInstancia.EncriptarSimetrico(tarjeta.nro)
            Dim tarj As TarjetaDTO = TarjetaDAL.ObtenerInstancia.Obtener(tarjeta)
            If tarj IsNot Nothing Then
                With tarj
                    .nro = CriptografiaBLL.ObtenerInstancia.Desencriptar(.nro)
                    .nombre = CriptografiaBLL.ObtenerInstancia.Desencriptar(.nombre)
                    .marca = CriptografiaBLL.ObtenerInstancia.Desencriptar(.marca)
                    .vencimiento = CriptografiaBLL.ObtenerInstancia.Desencriptar(.vencimiento)
                    .codigo_seguridad = CriptografiaBLL.ObtenerInstancia.Desencriptar(.codigo_seguridad)
                End With
            End If
            Return tarj
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
