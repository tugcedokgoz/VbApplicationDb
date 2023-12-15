Imports System.Text
Imports DevExpress.Xpf.Core

''' <summary>
''' Interaction logic for MainWindow.xaml
''' </summary>
Partial Public Class MainWindow
    Inherits ThemedWindow
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub KullaniciButton_Click(sender As Object, e As RoutedEventArgs)
        Dim userTable As New UserTable()
        pageView.Content = userTable
    End Sub

    Private Sub SecondWindow()
        Dim hehe As New LoginWindow()
        hehe.Show()
    End Sub

    Private Sub Customers_Click(sender As Object, e As RoutedEventArgs)
        Dim customerTable As New CustomerTable()
        pageView.Content = customerTable
    End Sub

    Private Sub Interns_Click(sender As Object, e As MouseButtonEventArgs)
        ' Stajyerlerle ilgili sayfayı yükleyin
        'Dim internsPage As New InternsPage()
        'pageView.Content = internsPage
    End Sub

    Private Sub LegalPersons_Click(sender As Object, e As MouseButtonEventArgs)
        ' Tüzel Kişilerle ilgili sayfayı yükleyin
        'Dim legalPersonsPage As New LegalPersonsPage()
        'pageView.Content = legalPersonsPage
    End Sub

    Private Sub Employees_Click(sender As Object, e As MouseButtonEventArgs)
        ' Personellerle ilgili sayfayı yükleyin
        'Dim employeesPage As New EmployeesPage()
        'pageView.Content = employeesPage
    End Sub

    Private Sub Suppliers_Click(sender As Object, e As MouseButtonEventArgs)
        ' Tedarikçilerle ilgili sayfayı yükleyin
        'Dim suppliersPage As New SuppliersPage()
        'pageView.Content = suppliersPage
    End Sub

End Class
