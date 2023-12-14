﻿Imports AutoMapper
Imports Microsoft.AspNetCore.Mvc
Imports VbApplication.Business
Imports VbApplication.DataAccess
Imports VbApplication.Modell
Imports VbApplication.Modell.Models

<ApiController>
<Route("[controller]")>
Public Class LegalPersonController
    Inherits ControllerBase
    Private ReadOnly _legalPersonBs As ILegalPersonBs
    Private ReadOnly _mapper As IMapper
    Private ReadOnly _userBs As IUserBs
    Private ReadOnly _userRepository As IUserRepository
    Private ReadOnly _passwordHistoryRepository As IPasswordHistoryRepository

    Public Sub New(legalPersonBs As ILegalPersonBs, mapper As IMapper, userBs As IUserBs, userRepository As IUserRepository, passwordHistoryRepository As IPasswordHistoryRepository)
        _legalPersonBs = legalPersonBs
        _mapper = mapper
        _userBs = userBs
        _userRepository = userRepository
        _passwordHistoryRepository = passwordHistoryRepository
    End Sub

    <HttpGet(Name:="GetAllLegalPerson")>
    Public Async Function GetAllLegalPerson() As Task(Of IActionResult)
        Dim response = Await _legalPersonBs.GetAllLegalPersonAsync()
        Return Ok(response)
    End Function

    <HttpGet("{id}", Name:="LeaglPersonGetUserById")>
    Public Async Function GetUserById(<FromRoute> id As Long) As Task(Of IActionResult)
        Dim response = Await _legalPersonBs.GetUserById(id)
        Return Ok(response)

    End Function
    <HttpDelete("{id}", Name:="DeleteLegalperson")>
    Public Async Function DeleteLegalPersonAsync(<FromRoute> id As Long) As Task(Of IActionResult)
        Try
            _legalPersonBs.DeleteAsync(id)

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
    <HttpPost(Name:="PostLegalPersonAsync")>
    Public Async Function PostLegalPersonAsync(dto As LegalPersonPostDto) As Task(Of IActionResult)
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
        Return Ok(_mapper.Map(Of LegalPersonPostDto)(existingUser))
    End Function
End Class
