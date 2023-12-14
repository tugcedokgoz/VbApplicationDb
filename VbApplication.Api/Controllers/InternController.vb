Imports AutoMapper
Imports Microsoft.AspNetCore.Mvc
Imports VbApplication.Business
Imports VbApplication.DataAccess
Imports VbApplication.Modell
Imports VbApplication.Modell.Models

<ApiController>
<Route("[controller]")>
Public Class InternController
    Inherits ControllerBase
    Private ReadOnly _internBs As IInternBs
    Private ReadOnly _mapper As IMapper
    Private ReadOnly _userBs As IUserBs
    Private ReadOnly _userRepository As IUserRepository
    Private ReadOnly _passwordHistoryRepository As IPasswordHistoryRepository

    Public Sub New(internBs As IInternBs, mapper As IMapper, userBs As IUserBs, userRepository As IUserRepository, passwordHistoryRepository As IPasswordHistoryRepository)
        _internBs = internBs
        _mapper = mapper
        _userBs = userBs
        _userRepository = userRepository
        _passwordHistoryRepository = passwordHistoryRepository
    End Sub


    <HttpGet(Name:="GetAllIntern")>
    Public Async Function GetAllIntern() As Task(Of IActionResult)
        Dim response = Await _internBs.GetAllInternAsync()
        Return Ok(response)
    End Function

    <HttpGet("{id}", Name:="InternGetUserById")>
    Public Async Function GetUserById(<FromRoute> id As Long) As Task(Of IActionResult)
        Dim response = Await _internBs.GetUserById(id)
        Return Ok(response)

    End Function

    <HttpDelete("{id}", Name:="DeleteIntern")>
    Public Async Function DeleteEmployeeAsync(<FromRoute> id As Long) As Task(Of IActionResult)
        Try
            _internBs.DeleteAsync(id)

            ' Başarı durumunu 204 No Content ile döndürülebilir.
            Return NoContent()
        Catch ex As InvalidOperationException
            ' Eğer kullanıcı bulunamazsa 404 Not Found durumu ile döndürülebilir.
            Return NotFound(ex.Message)
        Catch ex As Exception
            ' Diğer hatalar için 500 Internal Server Error durumu ile döndürülebilir.
            Return StatusCode(500, ex.Message)
        End Try
    End Function
    <HttpPost(Name:="PostInternAsync")>
    Public Async Function PostInternAsync(dto As InternPostDto) As Task(Of IActionResult)
        Dim existingUser = Await _userRepository.FindUserByNameAsync(dto.UserName)


        If existingUser IsNot Nothing Then
            ' Kullanıcı varsa, mevcut kullanıcıyı güncelle
            _mapper.Map(dto, existingUser)
            _userRepository.Update(existingUser)
        Else
            ' Kullanıcı yoksa, yeni bir kullanıcı oluştur
            Dim newUser As User = _mapper.Map(Of User)(dto)
            Dim pass = PasswordHasher.Hash(dto.Password)
            newUser.Password = pass
            Dim user = Await _userRepository.Post(newUser)
            Dim hashed = pass
            Dim history = New PasswordHistory() With {
                .ChangeDate = DateTime.Now,
                .PasswordHash = hashed,
                .PasswordSalt = hashed,
                .Active = True,
                .UserId = user.Id
            }
            _passwordHistoryRepository.AddPasswordHistory(history)
            existingUser = newUser
        End If

        ' Başarılı bir yanıt döndür
        Return Ok(_mapper.Map(Of InternPostDto)(existingUser))
    End Function
End Class
