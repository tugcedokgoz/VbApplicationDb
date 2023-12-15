Imports System.Net.Http
Imports DevExpress.XtraPrinting.Native.WebClientUIControl
Imports System.Text
Imports Newtonsoft.Json

Public Class LoginWindow

    Private ReadOnly _httpClient As HttpClient = New HttpClient()


    'Private Sub OnTextBoxGotFocus(sender As Object, e As RoutedEventArgs)
    '    Dim textBox As TextBox = DirectCast(sender, TextBox)
    '    If textBox.Text = "PAROLA" Then
    '        textBox.Text = String.Empty
    '    End If
    'End Sub

    'Private Sub OnTextBoxLostFocus(sender As Object, e As RoutedEventArgs)
    '    Dim textBox As TextBox = DirectCast(sender, TextBox)
    '    If String.IsNullOrWhiteSpace(textBox.Text) Then
    '        textBox.Text = "PAROLA"
    '    End If
    'End Sub

    Private Sub MainWindow()
        Dim mainWindow As New MainWindow()
        mainWindow.Show()
    End Sub

    Private Async Sub SimpleButton_Click(sender As Object, e As RoutedEventArgs)
        Dim username = usernameTextEdit.Text
        Dim password As String
        Dim innerPasswordBox As PasswordBox = TryCast(passwordBox.Template.FindName("passwordTxt", passwordBox), PasswordBox)
        If innerPasswordBox IsNot Nothing Then
            password = innerPasswordBox.Password
            ' Şifre ile ilgili işlemlerinizi burada yapın
        End If

        Dim loginInfo = New With {.Username = username, .Password = password}
        Dim jsonContent = JsonConvert.SerializeObject(loginInfo)
        Dim content = New StringContent(jsonContent, Encoding.UTF8, "application/json")

        Dim response As HttpResponseMessage = Await _httpClient.PostAsync("https://localhost:53845/Authentication/LoginAsync", content)

        If response.StatusCode = Net.HttpStatusCode.OK Then
            MainWindow()
            Me.Close()
        Else
            MessageBox.Show("Bir hata oluştu: " & response.StatusCode.ToString(), "Hata", MessageBoxButton.OK, MessageBoxImage.Error)
        End If
    End Sub

    Private Sub SimpleButton_Click_1(sender As Object, e As RoutedEventArgs)
        Process.Start("https://accounts.google.com/v3/signin/identifier?continue=https%3A%2F%2Fwww.google.com%2F%3Fhl%3Dtr&ec=GAZAmgQ&hl=tr&ifkv=ASKXGp2cNBxRdzjtdlejLC5YVFxATRzhvxrjWDv6IRYkzquP-uhd4Fe4NYmiGwaVHte1SW7NZW7wJg&passive=true&flowName=GlifWebSignIn&flowEntry=ServiceLogin&dsh=S151181209%3A1702564853583666&theme=glif")

    End Sub

    Private Sub SimpleButton_Click_2(sender As Object, e As RoutedEventArgs)
        Process.Start("https://www.facebook.com/login")

    End Sub


    'Private Sub PART_Editor_LostFocus(sender As Object, e As RoutedEventArgs)
    '    Dim textBox As TextBox = DirectCast(sender, TextBox)
    '    If String.IsNullOrWhiteSpace(textBox.Text) Then
    '        textBox.Text = "Kullanıcı Adı Giriniz"
    '    End If
    'End Sub

    'Private Sub PART_Editor_GotFocus(sender As Object, e As RoutedEventArgs)
    '    Dim textBox As TextBox = DirectCast(sender, TextBox)
    '    If textBox.Text = "Kullanıcı Adı Giriniz" Then
    '        textBox.Text = String.Empty
    '    End If
    'End Sub

    'Private Sub Username_GotFocus(sender As Object, e As RoutedEventArgs)
    '    If usernameTextEdit.Text = "Kullanıcı Adı Giriniz" Then
    '        usernameTextEdit.Text = ""
    '        usernameTextEdit.Foreground = New SolidColorBrush(Colors.Black)
    '    End If
    'End Sub

    'Private Sub Username_LostFocus(sender As Object, e As RoutedEventArgs)
    '    If String.IsNullOrWhiteSpace(usernameTextEdit.Text) Then
    '        usernameTextEdit.Text = "Kullanıcı Adı Giriniz"
    '        usernameTextEdit.Foreground = New SolidColorBrush(Colors.Gray)
    '    End If
    'End Sub

    'Private Sub Password_GotFocus(sender As Object, e As RoutedEventArgs)
    '    If passwordTextEdit.Text = "PAROLA GİRİNİZ" Then
    '        passwordTextEdit.Text = ""
    '        passwordTextEdit.Foreground = New SolidColorBrush(Colors.Black)
    '    End If
    'End Sub

    'Private Sub Password_LostFocus(sender As Object, e As RoutedEventArgs)
    '    If String.IsNullOrWhiteSpace(passwordTextEdit.Text) Then
    '        passwordTextEdit.Text = "PAROLA GİRİNİZ"
    '        passwordTextEdit.Foreground = New SolidColorBrush(Colors.Gray)
    '    End If
    'End Sub
End Class
