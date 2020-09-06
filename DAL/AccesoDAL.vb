
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Configuration

Public Class AccesoDAL

#Region "Singleton"
    Private Shared _instancia As AccesoDAL
    Public Shared Function ObtenerInstancia() As AccesoDAL
        If _instancia Is Nothing Then
            _instancia = New AccesoDAL
        End If
        Return _instancia
    End Function
#End Region

    Private STR As String = ConfigurationManager.ConnectionStrings("CxLocal").ConnectionString

    Private CX As New SqlConnection(STR)
    Private Cmd As SqlCommand

    Public Function EjecutarSP(ByVal nomSP As String, params As List(Of SqlParameter)) As Integer
        If CX.State = ConnectionState.Closed Then
            CX.Open()
        End If

        Dim i As Integer = 0
        Dim cmd As New SqlCommand With {.CommandType = CommandType.StoredProcedure, .CommandText = nomSP, .Connection = Me.cx}
        For Each s As SqlParameter In params
            cmd.Parameters.Add(s)
        Next

        Try
            i = cmd.ExecuteNonQuery
        Catch ex As Exception
            MsgBox("Se produjo el siguiente error al operar en la Base de Datos:  " & ex.Message, MsgBoxStyle.Critical, "OrderCenter")
            i = -1
        Finally
            CX.Close()
            cmd.Parameters.Clear()
        End Try

        Return i
    End Function

    Public Overloads Function LeerBD(ByVal nomSP As String, Optional params As List(Of SqlParameter) = Nothing) As DataTable
        If CX.State = ConnectionState.Closed Then
            CX.Open()
        End If
        Try
            Dim DA As New SqlDataAdapter
            Dim DT As New DataTable
            Dim cmd As New SqlCommand With {.CommandType = CommandType.StoredProcedure, .CommandText = nomSP, .Connection = Me.CX}
            DA.SelectCommand = cmd
            If params IsNot Nothing Then
                cmd.Parameters.AddRange(params.ToArray)
            End If

            DA.Fill(DT)

            Return DT
        Catch ex As Exception
            Throw ex
        Finally
            CX.Close()
        End Try

    End Function

    Public Overloads Function LeerBDconParams(ByVal cmdText As String, Optional params As List(Of SqlParameter) = Nothing) As DataTable
        If CX.State = ConnectionState.Closed Then
            CX.Open()
        End If
        Try
            Dim DA As New SqlDataAdapter
            Dim DT As New DataTable
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text, .CommandText = cmdText, .Connection = Me.CX}
            If params IsNot Nothing Then
                cmd.Parameters.AddRange(params.ToArray)
            End If
            DA.SelectCommand = cmd

            DA.Fill(DT)

            Return DT
        Catch ex As Exception

        Finally
            CX.Close()
        End Try

    End Function

    Public Overloads Function LeerBD(ByVal cmdText As String) As DataTable
        Try
            If CX.State = ConnectionState.Closed Then
                CX.Open()
            End If

            Dim da As New SqlDataAdapter
            Dim dt As New DataTable
            Dim cmd As New SqlCommand With {.CommandType = CommandType.Text, .CommandText = cmdText, .Connection = Me.CX}
            da.SelectCommand = cmd
            da.Fill(dt)
            Return dt
        Catch ex As Exception
        Finally
            CX.Close()
        End Try
    End Function

    Public Sub EscribirBD(ByVal cmdText As String)
        If CX.State = ConnectionState.Closed Then
            CX.Open()
        End If

        Dim i As Integer = 0
        Dim cmd As New SqlCommand With {.CommandType = CommandType.Text, .CommandText = cmdText, .Connection = Me.cx}
        Try
            i = cmd.ExecuteNonQuery
        Catch ex As Exception
            MsgBox("Se produjo el siguiente error al operar en la Base de Datos:  " & ex.Message, MsgBoxStyle.Critical, "OrderCenter")
            i = -1
        Finally
            CX.Close()
        End Try

    End Sub

    Public Overloads Function CrearParametro(ByVal Campo As String, ByVal vNum As Integer) As SqlParameter
        Dim par As New SqlParameter With {.ParameterName = Campo, .Value = vNum, .DbType = DbType.Int32}
        Return par
    End Function

    Public Overloads Function CrearParametro(ByVal Campo As String, ByVal vText As String) As SqlParameter
        Dim par As New SqlParameter With {.ParameterName = Campo, .Value = vText, .DbType = DbType.String}
        Return par
    End Function

    Public Overloads Function CrearParametro(ByVal Campo As String, ByVal vFecha As DateTime) As SqlParameter
        Dim par As New SqlParameter With {.ParameterName = Campo, .Value = vFecha, .DbType = DbType.DateTime}
        Return par
    End Function

    Public Overloads Function CrearParametro(ByVal Campo As String, ByVal vValor As Boolean) As SqlParameter
        Dim par As New SqlParameter With {.ParameterName = Campo, .Value = vValor, .DbType = DbType.Boolean}
        Return par
    End Function

    Public Overloads Function CrearParametro(ByVal Campo As String, ByVal vDec As Decimal) As SqlParameter
        Dim par As New SqlParameter With {.ParameterName = Campo, .Value = vDec, .DbType = DbType.Decimal}
        Return par
    End Function

    Public Function GetNextID(ByVal PKfield As String, ByVal Tablename As String) As Integer
        Dim par As New List(Of SqlParameter)
        par.Add(AccesoDAL.ObtenerInstancia.CrearParametro("@PKfield", PKfield))
        par.Add(AccesoDAL.ObtenerInstancia.CrearParametro("@Tablename", Tablename))
        Dim dt = AccesoDAL.ObtenerInstancia.LeerBD("GetNextID", par)
        Return dt.Rows(0)(0)
    End Function

    Public Function ObtenerNroPaginas(ByVal unaTabla, ByVal cantidadRegistros) As Integer
        Try
            Dim DT As DataTable = AccesoDAL.ObtenerInstancia.LeerBD("SELECT COUNT(*)/" + cantidadRegistros + " FROM " + unaTabla)
            Return DT.Rows(0)(0)
        Catch ex As Exception

        End Try
    End Function

End Class
