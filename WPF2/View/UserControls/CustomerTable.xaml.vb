Imports DevExpress.Xpf.Grid
Imports Newtonsoft.Json
Imports System.Collections.ObjectModel
Imports System.Net.Http
Imports System.Threading.Tasks

Partial Public Class CustomerTable
    Inherits UserControl

    Public Sub New()
        InitializeComponent()
        AddHandler Me.Loaded, AddressOf UserControl_Loaded ' UserControl_Loaded olayını dinlemeye başlayın
    End Sub

    Private Async Sub UserControl_Loaded(sender As Object, e As RoutedEventArgs)
        Dim customers As ObservableCollection(Of Customer) = Await GetCustomersAsync()
        CustomerGrid.ItemsSource = customers
    End Sub

    Public Async Function GetCustomersAsync() As Task(Of ObservableCollection(Of Customer))
        Using client As New HttpClient()
            Dim response As HttpResponseMessage = Await client.GetAsync("https://localhost:53845/Customer")
            If response.IsSuccessStatusCode Then
                Dim json As String = Await response.Content.ReadAsStringAsync()
                Return JsonConvert.DeserializeObject(Of ObservableCollection(Of Customer))(json)
            Else
                ' Hata durumunu ele alın
                Return New ObservableCollection(Of Customer)()
            End If
        End Using
    End Function
End Class
