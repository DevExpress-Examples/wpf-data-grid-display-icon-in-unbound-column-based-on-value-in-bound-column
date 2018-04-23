Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Grid
Imports System.Collections.ObjectModel

Namespace UnboundColumnWithIcons
	''' <summary>
	''' Interaction logic for Window1.xaml
	''' </summary>
	Partial Public Class Window1
		Inherits Window
		Private dataSource As ObservableCollection(Of MyObject)
		Public Sub New()
			InitializeComponent()

			dataSource = New ObservableCollection(Of MyObject)()
			dataSource.Add(New MyObject("cut"))
			dataSource.Add(New MyObject("copy"))
			dataSource.Add(New MyObject("paste"))
			dataSource.Add(New MyObject("delete"))
			grid.DataSource = dataSource
		End Sub

		Private Sub GridControl_CustomUnboundColumnData(ByVal sender As Object, ByVal e As GridColumnDataEventArgs)
			If e.Column.FieldName = "IconUnbound" Then
				If e.IsGetData Then
					Dim row As MyObject = dataSource(e.ListSourceRowIndex)
					Dim resourceName As String = GetResourceName(row.Action)
					e.Value = GetImage(resourceName)
				End If
			End If
		End Sub
		Private Function GetImage(ByVal resourceName As String) As BitmapFrame
			Dim uri As New Uri("pack://application:,,,/Icons/" & resourceName, UriKind.Absolute)
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
			Action = action
		End Sub
		Private privateAction As String
		Public Property Action() As String
			Get
				Return privateAction
			End Get
			Set(ByVal value As String)
				privateAction = value
			End Set
		End Property
	End Class
End Namespace