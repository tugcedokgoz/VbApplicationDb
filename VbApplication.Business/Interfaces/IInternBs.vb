Imports VbApplication.Modell

Public Interface IInternBs
    Function GetAllInternAsync() As Task(Of List(Of InternGetDto))
    Function PostInternAsync(internPostDto As InternPostDto) As Task(Of InternPostDto)
    Function GetUserById(userid As Long) As Task(Of InternGetDto)
    Sub UpdateInternAsync(internPostDto As InternPostDto)
    Sub DeleteAsync(Internid As Long)
End Interface
