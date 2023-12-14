Imports System
Imports System.Collections.Generic

Namespace Models
    Partial Public Class PasswordHistory
        Public Property Id As Long

        Public Property PasswordHash As String

        Public Property ChangeDate As Date

        Public Property PasswordSalt As String

        Public Property Active As Boolean

        Public Property UserId As Long

        Public Overridable Property User As User
    End Class
End Namespace
