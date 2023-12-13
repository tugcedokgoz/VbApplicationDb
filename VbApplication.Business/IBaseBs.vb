Imports Microsoft
Imports VbApplication.Modell

Public Interface IBaseBs
    Function GetAllUsersAsync() As Task(Of List(Of UserGetDto))
    Sub CreateUserAsync(userGetDto As UserGetDto)
    Function GetUserById(userid As Long) As Task(Of UserGetDto)
    Function UpdateUserAsync(userPostDto As UserPostDto) As Task
    Function DeleteAsync(userid As Long) As Task
End Interface
