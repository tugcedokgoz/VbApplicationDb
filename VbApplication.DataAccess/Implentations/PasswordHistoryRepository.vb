Imports VbApplication.DataAccess.Models
Imports VbApplication.Modell.Models

Public Class PasswordHistoryRepository : Implements IPasswordHistoryRepository

    Public Sub New()
    End Sub

    ' Yeni bir parola geçmişi ekler
    Public Sub AddPasswordHistory(history As PasswordHistory) Implements IPasswordHistoryRepository.AddPasswordHistory
        Using _context = New VbApplicationDbContext()
            _context.PasswordHistories.Add(history)
            _context.SaveChanges()
        End Using

    End Sub

    ' Belirli bir kullanıcının parola geçmişini alır
    Public Function GetPasswordHistoryByUserId(userId As Long) As IEnumerable(Of PasswordHistory) Implements IPasswordHistoryRepository.GetPasswordHistoryByUserId
        Using _context = New VbApplicationDbContext()

            Return _context.PasswordHistories.Where(Function(h) h.Id = userId).ToList()
        End Using
    End Function

    ' Parola geçmişini siler
    Public Sub DeletePasswordHistory(history As PasswordHistory) Implements IPasswordHistoryRepository.DeletePasswordHistory
        Using _context = New VbApplicationDbContext()

            _context.PasswordHistories.Remove(history)
            _context.SaveChanges()
        End Using
    End Sub

    ' Diğer gerekli metodlar...
End Class
