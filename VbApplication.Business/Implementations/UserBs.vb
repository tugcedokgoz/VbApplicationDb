Imports AutoMapper
Imports VbApplication.DataAccess
Imports VbApplication.Modell
Imports VbApplication.Modell.Models

Public Class UserBs
    Implements IUserBs
    Private ReadOnly _userRepository As IUserRepository
    Private ReadOnly _mapper As IMapper

    Public Sub New(userRepository As IUserRepository, mapper As IMapper)
        _userRepository = userRepository
        _mapper = mapper
    End Sub
    Public Async Sub UpdateUserAsync(userPostDto As UserPostDto) Implements IUserBs.UpdateUserAsync
        ' AutoMapper kullanarak UserPostDto'yu User sınıfına dönüştür
        Dim updatedUser As User = _mapper.Map(Of User)(userPostDto)

        ' Kullanıcıyı veritabanında güncelleme işlemi için uygun bir metodun çağrılması
        _userRepository.Update(updatedUser)
    End Sub

    Public Async Sub DeleteAsync(userid As Long) Implements IUserBs.DeleteAsync
        Dim user As User = Await _userRepository.GetById(userid)

        ' Kullanıcı var mı diye kontrol yapın
        If user IsNot Nothing Then
            ' Kullanıcıyı veritabanından silme işlemi için uygun bir metodun çağrılması
            _userRepository.Delete(userid)
        Else
            ' Kullanıcı bulunamadıysa uygun bir işlemi gerçekleştirin (loglama, hata mesajı, vb.)
            Throw New InvalidOperationException($"User with ID {userid} not found.")
        End If
    End Sub

    Public Async Function GetAllUsersAsync() As Task(Of List(Of UserGetDto)) Implements IUserBs.GetAllUsersAsync
        Dim users As List(Of User) = Await _userRepository.GetAllAsync()
        Dim userDtos As List(Of UserGetDto) = _mapper.Map(Of List(Of UserGetDto))(users)
        Return userDtos
    End Function

    Public Async Function GetUserById(userid As Long) As Task(Of UserGetDto) Implements IUserBs.GetUserById
        ' Kullanıcıyı veritabanından id'ye göre çekmek için uygun bir işlemi gerçekleştirin
        Dim user As User = Await _userRepository.GetById(userid)

        ' AutoMapper kullanarak User sınıfını UserGetDto'ya dönüştür
        Dim userDto As UserGetDto = _mapper.Map(Of UserGetDto)(user)

        Return userDto
    End Function

    'Public Async Function PostUserAsync(userPostDto As UserPostDto) As Task(Of UserPostDto) Implements IUserBs.PostUserAsync
    '    Dim newUser As User = _mapper.Map(Of User)(userPostDto)

    '    Await _userRepository.Post(newUser)
    'End Function


    'Public Async Function PostUserAsync(userPostDto As UserPostDto) As Task(Of UserPostDto) Implements IUserBs.PostUserAsync
    '    ' Kullanıcı adına göre veritabanında mevcut bir kullanıcıyı getir
    '    Dim existingUserList = Await _userRepository.GetAllAsync()
    '    Dim existingUser = existingUserList.Where(Function(u) u.UserName = userPostDto.UserName).FirstOrDefault()

    '    If existingUser IsNot Nothing Then
    '        ' Kullanıcı zaten varsa, güncelleme yap
    '        Dim updatedUser As User = _mapper.Map(Of User)(userPostDto)
    '        updatedUser.Id = existingUser.Id ' Mevcut kullanıcının ID'sini koru

    '        ' Güncelleme işlemini UserRepository üzerinden yap
    '        _userRepository.Update(updatedUser)

    '        ' Güncellenmiş kullanıcıyı döndür
    '        Return _mapper.Map(Of UserPostDto)(updatedUser)
    '    Else
    '        ' Kullanıcı yoksa, yeni bir kullanıcı oluştur
    '        Dim newUser As User = _mapper.Map(Of User)(userPostDto)

    '        ' Yeni kullanıcıyı UserRepository üzerinden ekleyerek ID'sini al
    '        Dim addedUser = (Await _userRepository.Post(newUser)).FirstOrDefault()

    '        ' Eklenen kullanıcıyı döndür
    '        Return _mapper.Map(Of UserPostDto)(addedUser)
    '    End If
    'End Function

    Public Async Function PostUserAsync(userPostDto As UserPostDto) As Task(Of UserPostDto) Implements IUserBs.PostUserAsync
        ' _userRepository ve _mapper'ın null olmadığından emin olun
        If _userRepository Is Nothing OrElse _mapper Is Nothing Then
            Throw New InvalidOperationException("Repository or Mapper is not initialized.")
        End If

        ' existingUserList'in null olmamasını sağlamak için kontrol ekleyin
        Dim existingUserList = Await _userRepository.GetAllAsync()
        If existingUserList Is Nothing Then
            ' Uygun bir hata mesajı veya işlem yapın
        End If

        Dim existingUser = existingUserList.FirstOrDefault(Function(u) u.UserName = userPostDto.UserName)

        If existingUser IsNot Nothing Then
            ' ... (Mevcut kodunuz)
            Dim updatedUser As User = _mapper.Map(Of User)(userPostDto)
            updatedUser.Id = existingUser.Id ' Mevcut kullanıcının ID'sini koru

            ' Güncelleme işlemini UserRepository üzerinden yap
            _userRepository.Update(updatedUser)

            ' Güncellenmiş kullanıcıyı döndür
            Return _mapper.Map(Of UserPostDto)(updatedUser)
        Else
            ' ... (Mevcut kodunuz)
            ' Kullanıcı yoksa, yeni bir kullanıcı oluştur
            Dim newUser As User = _mapper.Map(Of User)(userPostDto)

            ' Yeni kullanıcıyı UserRepository üzerinden ekleyerek ID'sini al
            Dim addedUser = Await _userRepository.Post(newUser)

            ' Eklenen kullanıcıyı döndür
            Return _mapper.Map(Of UserPostDto)(addedUser)
        End If
    End Function

    Public Async Function LoginAsync(userName As String, password As String) As Task(Of UserGetDto) Implements IUserBs.LoginAsync
        Dim user = Await _userRepository.LoginAsync(userName)
        Dim passTest As Boolean
        If (user IsNot Nothing) Then

            passTest = PasswordHasher.Verify(password, user.Password)
        End If
        If passTest Then
            Dim dto = _mapper.Map(Of UserGetDto)(user)

            Return dto
        Else
            Throw New KeyNotFoundException($"User not found")
        End If
    End Function
End Class
