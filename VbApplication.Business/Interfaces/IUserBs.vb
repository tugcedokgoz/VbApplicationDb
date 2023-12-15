Imports VbApplication.Modell

Public Interface IUserBs
    Function GetAllUsersAsync() As Task(Of List(Of UserGetDto))
    Function PostUserAsync(userPostDto As UserPostDto) As Task(Of UserPostDto)
    Function GetUserById(userid As Long) As Task(Of UserGetDto)
    Sub UpdateUserAsync(userPostDto As UserPostDto)
    Sub DeleteAsync(userid As Long)

    Function LoginAsync(userName As String, password As String) As Task(Of UserGetDto)
End Interface
