﻿'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''
''  BitacoraDTO.vb
''  Implementation of the Class BitacoraDTO
''  Generated by Enterprise Architect
''  Created on:      10-abr.-2019 8:15:59
''  Original author: seba_
''  
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''  Modification history:
''  
''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Public Class BitacoraDTO

    Private _id As Integer
    Private _usuario As UsuarioDTO
    Private _usuarioVal As String
    Private _FechaHora As DateTime
    Private _tipoSuceso As SucesoBitacoraDTO
    Private _tipoSucesoVal As String
    Private _ValorAnterior As String
    Private _NuevoValor As String
    Private _BitacoraError As BitacoraErroresDTO
    Private _observaciones As String


    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal Value As Integer)
            _id = Value
        End Set
    End Property

    Public Property usuario() As UsuarioDTO
        Get
            Return _usuario
        End Get
        Set(ByVal Value As UsuarioDTO)
            _usuario = Value
        End Set
    End Property

    Public ReadOnly Property usuarioVal() As String
        Get
            Return usuario.username
        End Get
    End Property

    Public Property FechaHora() As DateTime
            Get
                Return _FechaHora
            End Get
            Set(ByVal Value As DateTime)
                _FechaHora = Value
            End Set
        End Property

    Public Property tipoSuceso() As SucesoBitacoraDTO
        Get
            Return _tipoSuceso
        End Get
        Set(ByVal Value As SucesoBitacoraDTO)
            _tipoSuceso = Value
        End Set
    End Property

    Public ReadOnly Property tipoSucesoVal() As String
        Get
            Return _tipoSuceso.descripcion
        End Get
    End Property

    Public Property ValorAnterior() As String
        Get
            Return _ValorAnterior
        End Get
        Set(ByVal Value As String)
            _ValorAnterior = Value
        End Set
    End Property

    Public Property NuevoValor() As String
        Get
            Return _NuevoValor
        End Get
        Set(ByVal Value As String)
            _NuevoValor = Value
        End Set
    End Property

    Public Property BitacoraError() As BitacoraErroresDTO
        Get
            Return _BitacoraError
        End Get
        Set(ByVal value As BitacoraErroresDTO)
            _BitacoraError = value
        End Set
    End Property

    Public Property observaciones() As String
        Get
            Return _observaciones
        End Get
        Set(ByVal value As String)
            _observaciones = value
        End Set
    End Property

End Class ' BitacoraDTO


