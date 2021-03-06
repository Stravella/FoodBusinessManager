﻿Public Class CategoriaDTO

    Private _id As Integer
    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Private _nombre As String
    Public Property nombre As String
        Get
            Return _nombre
        End Get
        Set(ByVal value As String)
            _nombre = value
        End Set
    End Property

    Private _subscriptores As List(Of SubscriptorDTO)
    Public Property subscriptores() As List(Of SubscriptorDTO)
        Get
            Return _subscriptores
        End Get
        Set(ByVal value As List(Of SubscriptorDTO))
            _subscriptores = value
        End Set
    End Property

End Class
