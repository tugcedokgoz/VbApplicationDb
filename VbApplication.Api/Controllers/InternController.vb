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

    Public Sub New(internBs As IInternBs, mapper As IMapper, userBs As IUserBs, userRepository As IUserRepository)
        _internBs = internBs
        _mapper = mapper
        _userBs = userBs
        _userRepository = userRepository
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
        ' CustomerPostDto'dan User'a dönüştürme
        Dim existingUser = Await _userRepository.FindUserByNameAsync(dto.UserName)


        If existingUser IsNot Nothing Then
            ' Kullanıcı varsa, mevcut kullanıcıyı güncelle
            _mapper.Map(dto, existingUser)
            _userRepository.Update(existingUser)

        Else
            ' Kullanıcı yoksa, yeni bir kullanıcı oluştur
            Dim newUser As User = _mapper.Map(Of User)(dto)
            Await _userRepository.Post(newUser)
            existingUser = newUser
        End If

        ' Başarılı bir yanıt döndür
        Return Ok(_mapper.Map(Of InternPostDto)(existingUser))
    End Function
End Class
