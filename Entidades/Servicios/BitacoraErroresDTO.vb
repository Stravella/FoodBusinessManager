﻿'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''
''  BitacoraErroresDTO.vb
''  Implementation of the Class BitacoraErroresDTO
''  Generated by Enterprise Architect
''  Created on:      10-abr.-2019 8:16:12
''  Original author: seba_
''  
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''  Modification history:
''  
''
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Public Class BitacoraErroresDTO
    Inherits BitacoraDTO
    Private _id_bitacora_error As Integer
    Private _excepcion As String
    Private _stackTrace As String

    Public Property id_bitacora_error() As Integer
        Get
            Return _id_bitacora_error
        End Get
        Set(ByVal Value As Integer)
            _id_bitacora_error = Value
        End Set
    End Property


    Public Property excepcion() As String
        Get
            Return _excepcion
        End Get
        Set(ByVal Value As String)
            _excepcion = Value
        End Set
    End Property

    Public Property stackTrace() As String
            Get
                Return _stackTrace
            End Get
            Set(ByVal Value As String)
                _stackTrace = Value
            End Set
        End Property


End Class ' BitacoraErroresDTO
