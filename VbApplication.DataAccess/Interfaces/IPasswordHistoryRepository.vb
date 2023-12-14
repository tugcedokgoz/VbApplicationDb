Imports VbApplication.Modell.Models

Public Interface IPasswordHistoryRepository
    ' Yeni bir parola geçmişi ekler
    Sub AddPasswordHistory(history As PasswordHistory)

    ' Belirli bir kullanıcının parola geçmişini alır
    Function GetPasswordHistoryByUserId(userId As Long) As IEnumerable(Of PasswordHistory)

    ' Parola geçmişini siler
    Sub DeletePasswordHistory(history As PasswordHistory)

    ' Diğer gerekli metodların imzaları...
End Interface
