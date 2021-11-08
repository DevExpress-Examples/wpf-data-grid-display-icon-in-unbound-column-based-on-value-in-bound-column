Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.Xpf
Imports System
Imports System.Collections.ObjectModel
Imports System.Windows.Media.Imaging

Namespace UnboundColumnWithIcons_MVVM

    Public Class MyObject

        Public Property Action As String

        Public Sub New(ByVal action As String)
            Me.Action = action
        End Sub
    End Class

    Public Class ViewModel
        Inherits ViewModelBase

        Public Property Items As ObservableCollection(Of MyObject)

        Public Sub New()
            Items = New ObservableCollection(Of MyObject) From {New MyObject("cut"), New MyObject("copy"), New MyObject("paste"), New MyObject("delete")}
        End Sub

        <Command>
        Public Sub UnboundColumnData(ByVal args As UnboundColumnRowArgs)
            If args.FieldName Is "IconUnbound" AndAlso args.IsGetData Then
                Dim item = CType(args.Item, MyObject)
                Dim resourceName = GetResourceName(item.Action)
                args.Value = GetImage(resourceName)
            End If
        End Sub

        Private Function GetImage(ByVal resourceName As String) As BitmapFrame
            Dim uri = New Uri("pack://application:,,,/Icons/" & resourceName, UriKind.Absolute)
            Return BitmapFrame.Create(uri)
        End Function

        Private Function GetResourceName(ByVal action As String) As String
            Select Case action
                Case "copy"
                    Return "copy32x32.png"
                Case "cut"
                    Return "cut32x32.png"
                Case "delete"
                    Return "delete32x32.png"
                Case "paste"
                    Return "paste32x32.png"
                Case Else
                    Return String.Empty
            End Select
        End Function
    End Class
End Namespace
