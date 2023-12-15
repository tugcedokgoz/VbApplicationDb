Imports System
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.Extensions.Logging
Imports VbApplication.Business
Imports VbApplication.Modell

Namespace VbApplication.Api.Controllers
    <ApiController>
    <Route("[controller]")>
    Public Class AuthenticationController
        Inherits ControllerBase

        Private ReadOnly _userBs As IUserBs

        Public Sub New(userBs As IUserBs)
            _userBs = userBs
        End Sub

        <HttpPost("LoginAsync")>
        Public Async Function LoginAsync(<FromBody> dto As UserLoginDto) As Task(Of IActionResult)
            Dim user = Await _userBs.LoginAsync(dto.UserName, dto.Password)
            If user IsNot Nothing Then
                Return Ok(user)
            Else
                Return BadRequest()
            End If
        End Function

    End Class
End Namespace
