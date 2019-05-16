Imports System.Security.Cryptography
Imports System.Reflection
Imports System.Text
Imports DAL

Public Class DigitoVerificadorBLL

    Private Shared _instancia As DigitoVerificadorBLL
    Public Shared Function ObtenerInstancia() As DigitoVerificadorBLL
        If _instancia Is Nothing Then
            _instancia = New DigitoVerificadorBLL
        End If
        Return _instancia
    End Function


    'Devuelve un string con las propiedades del objeto
    Public Function CrearParametros(ByVal Elemento As Object) As List(Of String)
        Dim listaParam As New List(Of String)
        Try
            Dim tipo As Type = Elemento.GetType()
            Dim propiedades() As PropertyInfo = tipo.GetProperties()
            'recorro las propiedades del elemento
            For Each propiedad As PropertyInfo In propiedades
                If propiedad.PropertyType.FullName.Contains("Entidades.") Then
                    'Si no es una colleccion, obtengo sus propiedades
                    If Not propiedad.PropertyType.FullName.Contains("Collections.") Then
                        If propiedad.PropertyType.GetProperties.Count > 0 Then
                            For Each propiedad2 As PropertyInfo In propiedad.PropertyType.GetProperties
                                Dim Objeto As Object = propiedad.GetValue(Elemento, Nothing)
                                If propiedad2.Name.Contains("ID") Then
                                    If IsNothing(Objeto) Then
                                        listaParam.Add(DBNull.Value.ToString)
                                    Else
                                        listaParam.Add(propiedad.GetValue(Objeto, Nothing).ToString)
                                    End If

                                    Exit For
                                End If
                            Next
                        Else
                            listaParam.Add(propiedad.GetValue(Elemento, Nothing))
                        End If
                    End If
                Else
                    listaParam.Add(propiedad.GetValue(Elemento, Nothing))
                End If
            Next
            Return listaParam
        Catch ex As Exception

        End Try
    End Function



    Public Function CalcularDVH(ByVal Elemento As Object) As String
        Try

            Dim Parametros As List(Of String) = CrearParametros(Elemento)
            Dim filaParametros As String = ""
            For Each Param In Parametros
                filaParametros += Param
            Next
            If filaParametros = "" Then
                Return Nothing
            End If
            Return Encriptar(filaParametros)
        Catch ex As Exception

        End Try
    End Function


    Public Function Encriptar(ByRef filaParametros As String) As String
        Try
            Dim UE As New UnicodeEncoding
            Dim bHash As Byte()
            Dim bString() As Byte = UE.GetBytes(filaParametros)
            Dim ServicioSHA As New SHA1CryptoServiceProvider
            bHash = ServicioSHA.ComputeHash(bString)
            Dim StringEncriptado As String
            StringEncriptado = Convert.ToBase64String(bHash)
            Return StringEncriptado
        Catch ex As Exception

        End Try
    End Function

    Public Function CalcularDVVTabla(unaTabla As String) As String
        Dim DVV As String
        'Seleccionar todos los DVH de una tabla
        For Each row As DataRow In DigitoVerificadorDAL.ObtenerInstancia.ObtenerTodoDVH(unaTabla).Rows
            DVV = DVV + row.Item("dvh").ToString
        Next
        'vovler a hashearlos
        Return DVV = Encriptar(DVV)
    End Function

    Public Sub ActualizarDVV(unaTabla As String)
        Try
            If DigitoVerificadorDAL.ObtenerInstancia.TieneRegistros(unaTabla) = 0 Then
                DigitoVerificadorDAL.ObtenerInstancia.Agregar(unaTabla, CalcularDVVTabla(unaTabla))
            Else
                DigitoVerificadorDAL.ObtenerInstancia.Modificar(unaTabla, CalcularDVVTabla(unaTabla))
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function VerificarIntegridad() As List(Of String)
        Try
            Dim tablasCorruptas As List(Of String)
            Dim DVVnuevo As String
            For Each row As DataRow In DigitoVerificadorDAL.ObtenerInstancia.ListarTodos.Rows
                DVVnuevo = CalcularDVVTabla(row.Item("tabla").ToString)
                If Not row.Item("dvv").ToString = DVVnuevo Then
                    tablasCorruptas.Add(row.Item("tabla").ToString)
                End If
            Next
            'Devuelvo el nombre de las tablas corruptas. Sí es NULL está todo OK
            Return tablasCorruptas
        Catch ex As Exception

        End Try
    End Function


    Public Function VerificarIntegridad(unaTabla As String) As Boolean
        Try
            Dim DVVviejo As String = DigitoVerificadorDAL.ObtenerInstancia.Obtener(unaTabla)
            Dim DVVnuevo As String = CalcularDVVTabla(unaTabla)
            If DVVviejo = DVVnuevo Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception

        End Try
    End Function

    Public Function VerificarFilas(unaTabla As String) As List(Of String)
        Try
            Dim DVHviejo As String
            Dim DVHnuevo As String
            Dim fila As Object
            Dim idsCorruptos As List(Of String)
            Dim tipo As Type
            Dim propiedades() As PropertyInfo


            For Each row As DataRow In DigitoVerificadorDAL.ObtenerInstancia.ObtenerTabla(unaTabla).Rows
                DVHviejo = row.Item("dvh").ToString
                row.Item("dvh") = ""
                fila = TryCast(row, Object)
                DVHnuevo = CalcularDVH(fila)
                tipo = fila.GetType()
                propiedades = tipo.GetProperties()
                If Not DVHviejo = DVHnuevo Then
                    For Each propiedad As PropertyInfo In propiedades
                        If propiedad.Name.Contains("dvh") Then
                            idsCorruptos.Add(propiedad.ToString)
                        End If
                    Next
                End If
            Next

            Return idsCorruptos
        Catch ex As Exception

        End Try
    End Function


End Class
