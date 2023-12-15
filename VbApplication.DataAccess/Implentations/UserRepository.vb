Imports Microsoft.EntityFrameworkCore
Imports VbApplication.Modell.Models
Imports System.Collections.Generic
Imports System.Threading.Tasks
Imports VbApplication.DataAccess.Models

Public Class UserRepository : Implements IUserRepository

    Private ReadOnly _context As Models.VbApplicationDbContext

    Public Sub New()
        _context = New Models.VbApplicationDbContext()
    End Sub

    Public Sub Update(updatedUser As User) Implements IUserRepository.Update
        Dim _context = New Models.VbApplicationDbContext()

        _context.Entry(updatedUser).State = EntityState.Modified
        _context.SaveChanges()
    End Sub

    'Public Sub Delete(userId As Long) Implements IUserRepository.Delete
    '    Dim _context = New Models.VbApplicationDbContext()

    '    Dim userToDelete = _context.Users.Find(userId)
    '    If userToDelete IsNot Nothing Then
    '        _context.Users.Remove(userToDelete)
    '        _context.SaveChanges()
    '    End If
    'End Sub
    Public Sub Delete(userId As Long) Implements IUserRepository.Delete
        Dim _context = New Models.VbApplicationDbContext()

        Dim userToDelete = _context.Users.Find(userId)
        If userToDelete IsNot Nothing Then
            ' Kullanıcının Active durumunu False olarak güncelle
            userToDelete.Active = False
            _context.Entry(userToDelete).State = EntityState.Modified
            _context.SaveChanges()
        Else
            ' Eğer kullanıcı bulunamazsa uygun bir işlemi gerçekleştirin (örneğin, hata fırlatın)
            Throw New KeyNotFoundException($"User with ID {userId} not found.")
        End If
    End Sub


    Public Async Function GetAllAsync() As Task(Of List(Of User)) Implements IUserRepository.GetAllAsync
        Dim _context = New Models.VbApplicationDbContext()

        ' Sadece Active özelliği True olan kullanıcıları döndür
        Return Await _context.Users.Where(Function(u) u.Active = True).ToListAsync()
    End Function
    Public Async Function Post(Of T)(userDto As T) As Task(Of User) Implements IUserRepository.Post
        Dim _context = New Models.VbApplicationDbContext()

        If GetType(T).IsClass AndAlso GetType(T) = GetType(User) Then
            Dim user = DirectCast(DirectCast(userDto, Object), User)
            Dim newUser = _context.Users.Add(user)
            Await _context.SaveChangesAsync()
            Return newUser.Entity
        Else
            ' T türü User türünden değilse veya bir referans türü değilse, uygun bir işlem gerçekleştirin
            Throw New ArgumentException("userDto must be of type User")
        End If
    End Function

    Public Async Function GetById(userId As Long) As Task(Of User) Implements IUserRepository.GetById

        Return _context.Users.Where(Function(u) u.Id = userId).SingleOrDefault()
    End Function

    Public Async Function FindUserByNameAsync(userName As String) As Task(Of User) Implements IUserRepository.FindUserByNameAsync
        Dim _context = New Models.VbApplicationDbContext()

        Return Await _context.Users.FirstOrDefaultAsync(Function(u) u.UserName = userName)
    End Function

    Public Async Function SaveChangesAsync() As Task Implements IUserRepository.SaveChangesAsync

        Await _context.SaveChangesAsync()
    End Function

    Public Async Function LoginAsync(userName As String) As Task(Of User) Implements IUserRepository.LoginAsync
        Dim _context = New Models.VbApplicationDbContext()

        Return Await _context.Users.Where(Function(u) u.UserName = userName).FirstOrDefaultAsync()
    End Function



End Class
