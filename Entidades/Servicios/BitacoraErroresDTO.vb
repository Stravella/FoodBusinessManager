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

    Private _id As Integer
    Private _excepcion As String
    Private _stackTrace As String

    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
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