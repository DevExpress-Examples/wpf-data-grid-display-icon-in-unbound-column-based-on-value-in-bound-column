Imports DevExpress.Xpf.Grid
Imports System
Imports System.Collections.ObjectModel
Imports System.Windows
Imports System.Windows.Media.Imaging

Namespace UnboundColumnWithIcons_CodeBehind

    Public Partial Class Window1
        Inherits Window

        Private dataSource As ObservableCollection(Of MyObject)

        Public Sub New()
            Me.InitializeComponent()
            dataSource = New ObservableCollection(Of MyObject)()
            dataSource.Add(New MyObject("cut"))
            dataSource.Add(New MyObject("copy"))
            dataSource.Add(New MyObject("paste"))
            dataSource.Add(New MyObject("delete"))
            Me.grid.ItemsSource = dataSource
        End Sub

        Private Sub GridControl_CustomUnboundColumnData(ByVal sender As Object, ByVal e As GridColumnDataEventArgs)
            If Equals(e.Column.FieldName, "IconUnbound") Then
                If e.IsGetData Then
                    Dim row As MyObject = dataSource(e.ListSourceRowIndex)
                    Dim resourceName As String = GetResourceName(row.Action)
                    e.Value = GetImage(resourceName)
                End If
            End If
        End Sub

        Private Function GetImage(ByVal resourceName As String) As BitmapFrame
            Dim uri As Uri = New Uri("pack://application:,,,/Icons/" & resourceName, UriKind.Absolute)
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
            End Select

            Return String.Empty
        End Function
    End Class

    Public Class MyObject

        Public Sub New()
        End Sub

        Public Sub New(ByVal action As String)
            Me.Action = action
        End Sub

        Public Property Action As String
    End Class
End Namespace
