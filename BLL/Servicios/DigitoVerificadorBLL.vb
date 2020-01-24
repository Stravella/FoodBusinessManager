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

    'Devuelve una lista de strings con los valores de las propiedades de un objeto
    'sí se encuentra con un objeto child, devuelve solo su id
    Public Function convertirObjeto(ByVal Elemento As Object) As List(Of String)
        Try
            Dim lista As New List(Of String)
            Dim tipo As Type = Elemento.GetType()
            Dim propiedades() As PropertyInfo = tipo.GetProperties()
            For Each propiedad As PropertyInfo In propiedades
                'Sí es otro elemento, solo me quedo con el id
                If propiedad.PropertyType.FullName.Contains("Entidades.") Then
                    If Not propiedad.PropertyType.FullName.Contains("Collections.") Then
                        If propiedad.PropertyType.GetProperties.Count > 0 Then
                            For Each propiedadChild As PropertyInfo In propiedad.PropertyType.GetProperties
                                Dim ObjetoChild As Object = propiedad.GetValue(Elemento, Nothing)
                                If propiedadChild.Name.Contains("id") Then
                                    If IsNothing(ObjetoChild) Then
                                        lista.Add(DBNull.Value.ToString)
                                    Else
                                        lista.Add(propiedadChild.GetValue(ObjetoChild, Nothing))
                                    End If
                                    'Si ya encontré el id salgo del FOR
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                Else
                    If Not propiedad.Name.Contains("DVH") Then
                        lista.Add(propiedad.GetValue(Elemento, Nothing))
                    End If
                End If
            Next

            Return lista
        Catch ex As Exception

        End Try
    End Function

    Public Function CalcularDVH(ByVal Elemento As Object) As String
        Try

            Dim Parametros As List(Of String) = convertirObjeto(Elemento)
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


    Public Function Encriptar(ByRef input As String) As String
        Try
            Dim SHA256 As SHA256 = SHA256Managed.Create
            Dim valorHash As Byte()
            Dim objUTF8 As UTF8Encoding = New UTF8Encoding
            valorHash = SHA256.ComputeHash(objUTF8.GetBytes(input))
            Dim output As String = Convert.ToBase64String(valorHash)
            Return output
        Catch ex As Exception

        End Try
    End Function

    Public Function CalcularDVVTabla(unaTabla As String) As String
        Dim DVV As String
        Dim ListadoDVH As String
        'Seleccionar todos los DVH de una tabla
        For Each row As DataRow In DigitoVerificadorDAL.ObtenerInstancia.ObtenerTodoDVH(unaTabla).Rows
            ListadoDVH = ListadoDVH + row.Item("dvh").ToString
        Next
        'vovler a hashearlos
        DVV = Encriptar(ListadoDVH)
        Return DVV
    End Function

    Public Sub ActualizarDVV(unaTabla As String)
        Try
            If DigitoVerificadorDAL.ObtenerInstancia.TieneRegistros(unaTabla) = True Then
                DigitoVerificadorDAL.ObtenerInstancia.Modificar(unaTabla, CalcularDVVTabla(unaTabla))
            Else
                DigitoVerificadorDAL.ObtenerInstancia.Agregar(unaTabla, CalcularDVVTabla(unaTabla))
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Function VerificarIntegridad() As List(Of String)
        Try
            Dim tablasCorruptas As New List(Of String)
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


    Public Function ObtenerSALT() As String
        Dim miGUID As System.Guid = System.Guid.NewGuid()
        Dim sGUID As String = miGUID.ToString()
        Return sGUID
    End Function

End Class
