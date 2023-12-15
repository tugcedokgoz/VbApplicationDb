Imports AutoMapper
Imports Microsoft.AspNetCore.Mvc
Imports VbApplication.Business
Imports VbApplication.DataAccess
Imports VbApplication.Modell
Imports VbApplication.Modell.Models

<ApiController>
<Route("[controller]")>
Public Class CustomerController
    Inherits ControllerBase

    Private ReadOnly _customerBs As ICustomerBs
    Private ReadOnly _mapper As IMapper
    Private ReadOnly _userBs As IUserBs
    Private ReadOnly _userRepository As IUserRepository
    Private ReadOnly _passwordHistoryRepository As IPasswordHistoryRepository

    Public Sub New(customerBs As ICustomerBs, mapper As IMapper, userBs As IUserBs, userRepository As IUserRepository, passwordHistoryRepository As IPasswordHistoryRepository)
        _customerBs = customerBs
        _mapper = mapper
        _userBs = userBs
        _userRepository = userRepository
        _passwordHistoryRepository = passwordHistoryRepository
    End Sub

    <HttpGet(Name:="GetAllCustomers")>
    Public Async Function GetAllCustomers() As Task(Of IActionResult)
        Dim response = Await _customerBs.GetAllCustomersAsync()
        Return Ok(response)
    End Function

    <HttpGet("{id}", Name:="CustomerGetUserById")>
    Public Async Function GetUserById(<FromRoute> id As Long) As Task(Of IActionResult)
        Dim response = Await _customerBs.GetUserById(id)
        Return Ok(response)

    End Function

    <HttpDelete("{id}", Name:="DeleteCustomer")>
    Public Async Function DeleteCustomerAsync(<FromRoute> id As Long) As Task(Of IActionResult)
        Try
            _customerBs.DeleteAsync(id)

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

    <HttpPost(Name:="PostCustomerAsync")>
    Public Async Function PostCustomerAsync(dto As CustomerPostDto) As Task(Of IActionResult)
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
        Return Ok(_mapper.Map(Of CustomerPostDto)(existingUser))
    End Function



End Class
'<HttpPost(Name:="PostCustomerAsync")>
'Public Async Function PostCustomerAsync(customerGetDto As CustomerGetDto) As Task(Of IActionResult)
'    Try
'        Await _customerBs.PostCustomerAsync(customerGetDto)

'        ' Başarı durumunu 201 Created ile döndürülebilir.
'        Return StatusCode(201)
'    Catch ex As Exception
'        ' Hata durumunu 500 Internal Server Error ile döndürülebilir.
'        Return StatusCode(500, ex.Message)
'    End Try
'End Function

'<HttpPost("{id}", Name:="UpdateCustomer")>
'Public Async Function UpdateCustomer(<FromRoute> id As Long, customerPostDto As CustomerPostDto) As Task(Of IActionResult)
'    Try
'        ' Kullanıcıyı veritabanından ID'ye göre çekmek için uygun bir işlemi gerçekleştirin
'        Dim existingCustomer As CustomerGetDto = Await _customerBs.GetUserById(id)

'        ' Eğer kullanıcı bulunamazsa 404 Not Found durumu ile döndürülebilir.
'        If existingCustomer Is Nothing Then
'            Return NotFound($"Customer with ID {id} not found.")
'        End If

'        ' Güncellenen müşteri verilerini CustomerPostDto üzerinden alın
'        Dim updatedCustomerDto As CustomerPostDto = _mapper.Map(Of CustomerPostDto)(existingCustomer)
'        _mapper.Map(customerPostDto, updatedCustomerDto)

'        ' Kullanıcıyı veritabanında güncelleme işlemi için uygun bir metodun çağrılması
'        _customerBs.UpdateCustomerAsync(updatedCustomerDto)

'        ' Başarı durumunu 204 No Content ile döndürülebilir.
'        Return NoContent()
'    Catch ex As Exception
'        ' Hata durumunu 500 Internal Server Error ile döndürülebilir.
'        Return StatusCode(500, ex.Message)
'    End Try
'End Function