Imports System
Imports System.Security.Cryptography
Imports System.Text

Public Class PasswordHasher

    Private Const SaltSize As Integer = 16 ' Tuzun (salt) boyutu
    Private Const KeySize As Integer = 32  ' Karma değerinin (hash) boyutu
    Private Const Iterations As Integer = 10000 ' İterasyon sayısı

    ' Parola hash'leme
    Public Shared Function Hash(password As String) As String
        Using rng = New RNGCryptoServiceProvider()
            Dim saltBytes(SaltSize - 1) As Byte
            rng.GetBytes(saltBytes)

            Using pbkdf2 = New Rfc2898DeriveBytes(password, saltBytes, Iterations)
                Dim key = pbkdf2.GetBytes(KeySize)
                Dim hashBytes = New Byte(SaltSize + KeySize - 1) {}
                Array.Copy(saltBytes, 0, hashBytes, 0, SaltSize)
                Array.Copy(key, 0, hashBytes, SaltSize, KeySize)

                Return Convert.ToBase64String(hashBytes)
            End Using
        End Using
    End Function

    ' Parola doğrulama
    Public Shared Function Verify(password As String, hashedPassword As String) As Boolean
        Dim hashBytes = Convert.FromBase64String(hashedPassword)
        Dim saltBytes(SaltSize - 1) As Byte
        Array.Copy(hashBytes, 0, saltBytes, 0, SaltSize)

        Using pbkdf2 = New Rfc2898DeriveBytes(password, saltBytes, Iterations)
            Dim key = pbkdf2.GetBytes(KeySize)
            For i = 0 To KeySize - 1
                If hashBytes(i + SaltSize) <> key(i) Then
                    Return False
                End If
            Next
        End Using

        Return True
    End Function

End Class
